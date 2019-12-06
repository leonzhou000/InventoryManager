using InverntoryManager.Data;
using InverntoryManager.Models;
using SQLite;
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
    public partial class LoginPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private List<User> _users;
        private User info;

        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            UserName.Completed += (s, e) => password.Focus();
            password.Completed += (s, e) => signInBtm_Clicked(s, e);
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            getUsersTable();
        }

        private async void getUsersTable()
        {
            var users = await _connection.Table<User>().ToListAsync();
            _users = new List<User>(users);
        }

        private void signInBtm_Clicked(object sender, EventArgs e)
        {
            if (checkUser())
            {
                DisplayAlert("Sign in"," Signin Success","OK");
                MainPage page = new MainPage(info);
                Application.Current.MainPage = page;
            }
        }
        private bool checkUser()
        {
            if (String.IsNullOrEmpty(UserName.Text) && String.IsNullOrEmpty(password.Text))
            {
                DisplayAlert("Sign in", "Invald Username or Password", "OK");
                return false;
            }

            foreach (var user in _users)
            {
                if(UserName.Text == user.Username && password.Text != user.Password)
                {
                    DisplayAlert("Sign in", "Invald Password", "OK");
                    return false;
                }
                if (UserName.Text == user.Username && password.Text == user.Password)
                {
                    info = user;
                    return true;
                }
            }

            DisplayAlert("Sign in", " Sign in Failed", "OK");
            return false;
        }

        private void showPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (showPassword.IsChecked) 
            {
                password.IsPassword = false;
            }
            if (!showPassword.IsChecked)
            {
                password.IsPassword = true;
            }
        }
    }
}