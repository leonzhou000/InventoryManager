using InverntoryManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InverntoryManager.Data;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public User _user { get; set; }
        public Item item { get; set; }
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Item> _items;

        public InventoryPage()
        {
            _user = ConstentsUser.user;
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try { _connection = DependencyService.Get<ISQLiteDb>().GetConnection(); }
            catch { DisplayAlert("Error", "SQL Table Connection", "OK"); }

            _items = GetItems();
            itemCollectionView.ItemsSource = _items;
        }

        private  ObservableCollection<Item> GetItems(string searchText = null)
        {
            var items = _items;
            if (String.IsNullOrWhiteSpace(searchText))
                return items;

            return items.Where(c => c.Name.StartsWith(searchText)) as ObservableCollection<Item>;
        }
        protected override async void OnAppearing()
        {
            var items = await _connection.Table<Item>().ToListAsync();
            _items = new ObservableCollection<Item>(items);
            itemCollectionView.ItemsSource = _items;

            base.OnAppearing();
        }

        private async void OnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddItemPage(_user));
        }
        private async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UpdatePage());
        }
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (MultiSelect.IsChecked)
            {
                foreach(Item item in _items.ToList())
                {
                    if (item.Selected)
                    {
                        await _connection.DeleteAsync(item);
                        _items.Remove(item);
                    }
                }
            }
            else
            {
                var item = itemCollectionView.SelectedItem as Item;
                if (item == null) 
                {
                    await DisplayAlert("Alert", "No item selected", "OK");
                    return; 
                }
                await _connection.DeleteAsync(item);
                _items.Remove(item);
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            itemCollectionView.ItemsSource = GetItems(e.NewTextValue);
        }
    }
}