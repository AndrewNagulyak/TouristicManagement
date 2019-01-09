using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TravelManager.Modal;
using TravelManager.View;

namespace TravelManager.VIewModal
{
    public class CreateUserViewModal : IViewModal<CreateUserViewModal>, INotifyPropertyChanged
    {
        private readonly DelegateCommand _addUser;
        IService<User> userService;
        IService<InternalUserData> userdataService;
        User _user;
        InternalUserData _userdata;
        public DelegateCommand AddUser { get { return _addUser; } }
        public User User { get  { return _user; } set { if (value != null) _user = new User() { Id = value.Id, Role = "user"}; NotifyPropertyChanged("User"); } }
        public InternalUserData UserData { get { return _userdata; } set { if (value != null) _userdata = new InternalUserData() { Id = value.Id ,Role="user",Username=User.Username}; NotifyPropertyChanged("UserData"); } }
        public string Pass { get; set; }

        public CreateUserViewModal(IService<User> userService, IService<InternalUserData> userdataService)

        {
            this.userdataService = userdataService;
            this.userService = userService;
            _addUser = new DelegateCommand((Action<object>)AddUserCommand);
            User = new User();
            UserData = new InternalUserData();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
           
        }
        void AddUserCommand(object o)
        {


            User user = new User() { Username = User.Username, Email=User.Email,Role=User.Role,Id=User.Id};
            Pass = CalculateHash(Pass, User.Username);

            InternalUserData userdata = new InternalUserData() { HashedPassword = Pass, Role = User.Role, Id = UserData.Id, Username = User.Username };
            userService.Add(user);
            userdataService.Add(userdata);
            User = null;
            UserData = null;
        }
        private static string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }
}
