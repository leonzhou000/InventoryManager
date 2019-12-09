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
    public partial class HomePage : ContentPage
    {
        public User _user { get; set; }
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Item> _items;

        public HomePage()
        {
            _user = ConstentsUser.user;
            InitializeComponent();
            itemCollectionView.SelectionMode = SelectionMode.None;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _user = ConstentsUser.user;
            DisplayAlert("Your username", _user.Username, "OK");
        }

        protected override async void OnAppearing()
        {
            try
            {
                var items = await _connection.Table<Item>().ToListAsync();
                _items = new ObservableCollection<Item>(items);
                _items = _items.OrderBy(e => e.AddDate) as ObservableCollection<Item>;
                itemCollectionView.ItemsSource = _items;
            }
            catch
            {
                return;
            }
            base.OnAppearing();
        }
    }
}