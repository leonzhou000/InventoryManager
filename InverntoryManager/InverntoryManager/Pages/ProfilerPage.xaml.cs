using InverntoryManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public User user { get; set; }
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = user;
            //Init();
        }

        private void Init()
        {
            buildGrid();
        }

        private void buildGrid()
        {
            //MainGrid.RowSpacing = 5;
            //MainGrid.ColumnSpacing = 5;
            //for (int i = 0; i < 21; i++)
            //{
            //    var label = new Label { Text = "Hello " + i, BackgroundColor = Color.Silver };
            //    test.Add(label);
            //}

            //for (int j = 0; j < (test.Count / 2); j++)
            //{
            //    MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Absolute) });
            //}
            //for (int i = 0; i < 3; i++)
            //{
            //    MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //}

            //int row = 0;
            //int col = 0;
            //foreach(var l in test)
            //{
            //    MainGrid.Children.Add(l, (col%3), row);
            //    col++;
            //    if (col%3 == 0) { row++; }
            //}
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Sign out", "Signed out", "Ok");
            await Navigation.PushAsync(new OpeningPage());
        }
    }
}