using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InverntoryManager.Models
{
    public class User
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }
        
        [MaxLength(255)]
        public string Password { get; set; }

        private string _firstName;

        [MaxLength(255)]
        public string FirstName {
            get { return _firstName; } 
            set
            {
                if (_firstName == value)
                    return;

                _firstName = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        [MaxLength(255)]
        public string LastName { get; set; }
        public bool admin { get; set; }

        public User() { }
        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
           //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
