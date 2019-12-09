using InverntoryManager.Models;
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
    public partial class ProfilePage : ContentPage
    {
        public User _user { get; set; }
        public string url { get; set; }
        public CollectionView CollectionView;
        public ProfilePage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _user = ConstentsUser.user;

            profileImageHandler();
        }

        public void profileImageHandler()
        {
            if (String.IsNullOrEmpty(_user.imageUrl)) 
            {
                profile.Source = ImageSource.FromResource("HelloWorld.Image.BlankProfile.jpg");
                return;
            }
            url = _user.imageUrl;
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