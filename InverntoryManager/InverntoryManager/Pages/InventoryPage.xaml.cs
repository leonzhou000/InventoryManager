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
        public User user { get; set; }
        public Item item { get; set; }
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Item> _items;

        public InventoryPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            
        }

        ObservableCollection<Item> GetItem(string searchText = null)
        {
            if (String.IsNullOrWhiteSpace(searchText))
                return _items;

            return _items.Where(c => c.Name.StartsWith(searchText)) as ObservableCollection<Item>;
        }
        protected override async void OnAppearing()
        {
            var items = await _connection.Table<Item>().ToListAsync();
            _items = new ObservableCollection<Item>(items);
            itemCollectionView.ItemsSource = _items;

            if (MultiSelect.IsChecked)
            {
                itemCollectionView.SelectedItem = null;
            }

            base.OnAppearing();
        }

        private async void OnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddItemPage(user));
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
            itemCollectionView.ItemsSource = GetItem(e.NewTextValue);
        }
    }
}