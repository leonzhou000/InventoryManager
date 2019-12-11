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
    public partial class ItemPage : ContentPage
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

        public ItemPage()
        {
            _user = ConstentsUser.user;
            InitializeComponent();
            Init();
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

        private async void Init()
        {
            try { _connection = DependencyService.Get<ISQLiteDb>().GetConnection(); }
            catch { await DisplayAlert("Error", "SQL Table Connection", "OK"); }
            itemCollectionView.ItemsSource = await getData();
        }

        private async Task<ObservableCollection<Item>> getData()
        {
            var table = await _connection.Table<Item>().ToListAsync();
            
            return new ObservableCollection<Item>(table);
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

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                return;

            await FindItems(e.NewTextValue);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}