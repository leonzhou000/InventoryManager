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