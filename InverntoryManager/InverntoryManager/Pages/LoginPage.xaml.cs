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

        public List<User> login = new List<User> 
        { 
            new User { Username = "user", Password = "1234", FirstName = "User1"} 
        };

        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            UserName.Completed += (s, e) => password.Focus();
            password.Completed += (s, e) => signInBtm_Clicked(s, e);
            var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<User>();
        }

        private void signInBtm_Clicked(object sender, EventArgs e)
        {
            User user = new User(UserName.Text, password.Text);
            if (checkUser())
            {
                DisplayAlert("Sign in"," Signin Success","OK");
                MainPage page = new MainPage(user);
                Application.Current.MainPage = page;

            }
        }
        private bool checkUser()
        {
            if (UserName.Text == login[0].Username && 
                password.Text == login[0].Password)
            {
                return true;
            }
            else if (String.IsNullOrEmpty(UserName.Text) && 
                    String.IsNullOrEmpty(password.Text))
            {
                DisplayAlert("Sign in", "Invald Username or Password", "OK");
                return false;
            }
            else
            {
                DisplayAlert("Sign in", " Signin Failed", "OK");
                return false;
            }
        }
    }
}