using InverntoryManager.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace InverntoryManager.Models
{
    class inventoryService
    {
        private static string AppUrl = @"https://inventorymanger.azurewebsites.net";
        
    }

    class ConstentsUser
    {
        private static User _LoginUser = null;

        public static User user
        {
            get { return _LoginUser; }
            set { _LoginUser = value; }
        }
    }

    class ConstItems
    {
        private List<Item> items = null;



        public async void getItem()
        {

        }



    }


    class MainPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

        public MainPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
            {
                    new MainPageMasterMenuItem { Id = 0, Title = "Home" , TargetType = typeof(HomePage) },
                    new MainPageMasterMenuItem { Id = 1, Title = "Account" , TargetType = typeof(ProfilePage) },
                    new MainPageMasterMenuItem { Id = 2, Title = "Inventory",TargetType = typeof(InventoryPage) },
                    new MainPageMasterMenuItem { Id = 3, Title = "Notifications",TargetType = typeof(ReportPage) }
            });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
