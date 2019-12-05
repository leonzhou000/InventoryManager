using InverntoryManager.Models;
using System;
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

        public SQLiteAsyncConnection _connection;

        public InventoryPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Item>();

            var items = await _connection.Table<Item>().ToListAsync();

            base.OnAppearing();
        }

        private void OnAdd_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("clicked", "Clicked", "OK");
        }
    }
}