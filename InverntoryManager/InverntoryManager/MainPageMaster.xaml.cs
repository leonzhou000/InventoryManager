using InverntoryManager.Models;
using InverntoryManager.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public User user { get; set; }

        public MainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainPageMasterViewModel(user);
            ListView = MenuItemsListView;
        }

        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

            public MainPageMasterViewModel(User user)
            {
                MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
                {
                    new MainPageMasterMenuItem { Id = 0, Title = "Home" , TargetType = typeof(HomePage), Profile = user},
                    new MainPageMasterMenuItem { Id = 1, Title = "Account" , TargetType = typeof(ProfilePage), Profile = user},
                    new MainPageMasterMenuItem { Id = 2, Title = "Inventory",TargetType = typeof(InventoryPage), Profile = user },
                    new MainPageMasterMenuItem { Id = 3, Title = "Report",TargetType = typeof(ReportPage), Profile = user }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}