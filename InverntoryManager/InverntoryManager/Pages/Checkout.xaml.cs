using InverntoryManager.Data;
using InverntoryManager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checkout : ContentPage
    {
        public User _user { get; set; }
        public Item item { get; set; }
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Item> _items;

        private BindableProperty IsSearchingProperty =
            BindableProperty.Create("IsSearching", typeof(bool), typeof(ItemPage), false);

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value); }
        }

        public Checkout()
        {
            _user = ConstentsUser.user;
            InitializeComponent();
            Init();
        }

        private async void Init()
        {
            try { _connection = DependencyService.Get<ISQLiteDb>().GetConnection(); }
            catch { await DisplayAlert("Error", "SQL Table Connection", "OK"); }
            itemCollectionView.ItemsSource = await getData();
        }

        protected override async void OnAppearing()
        {
            try
            {
                _items = await getData();
                itemCollectionView.ItemsSource = _items;
            }
            catch
            {
                return;
            }
            base.OnAppearing();
        }

        private async Task FindItems(string Item)
        {
            try
            {
                IsSearching = true;
                var table = await _connection.Table<Item>().ToListAsync();
                var items = from item in table
                            where item.Name == Item
                            select item;
                itemCollectionView.ItemsSource = items;
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not retrieve the list of items.", "OK");
            }
            finally
            {
                IsSearching = false;
            }
        }

        private async Task<ObservableCollection<Item>> getData()
        {
            var items = await _connection.Table<Item>().ToListAsync();
            return new ObservableCollection<Item>(items);
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                return;

            await FindItems(e.NewTextValue);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            item = itemCollectionView.SelectedItem as Item;
            var temp = new Item
            {
                Name = item.Name,
                Stock = 1,
                ImageUrl = item.ImageUrl,
                Owner = _user.Username,
                Selected = false
            };
            _connection.InsertAsync(temp);

        }
    }
}