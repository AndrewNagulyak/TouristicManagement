using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using TravelManager.Modal;
using TravelManager.View;

namespace TravelManager.VIewModal
{

    public interface IViewModel { }

    public class AuthenticationViewModel : IViewModel, INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly DelegateCommand _loginCommand;
        private string _username;
        private string _status;
        private bool isActive;
        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            try
            {
                _authenticationService = authenticationService;
                _loginCommand = new DelegateCommand(Login, CanLogin);
                isActive = false;
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        #region Properties
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; NotifyPropertyChanged("IsActive"); NotifyPropertyChanged("IsInActive"); }
        }
        public bool IsInActive
        {
            get { return !isActive; }

        }
        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }

        #endregion

        #region Commands
        public DelegateCommand LoginCommand { get { return _loginCommand; } }
        #endregion

        private async Task Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
                isActive = true;
                NotifyPropertyChanged("IsActive"); NotifyPropertyChanged("IsInActive");
                //Validate credentials through the authentication service
                User user =  await Task.Factory.StartNew(()=>_authenticationService.AuthenticateUser(Username, clearTextPassword));
                if (user == null)
                    throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
                //Get the current principal object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Username, user.Role);

                _loginCommand.RaiseCanExecuteChanged();

                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;
                if (user.Role == "Administator")
                {
                    Pages.SetMenu(Pages.AdminMenu);
                }
                else
                {
                    Pages.SetWindow(Pages.Main);
                    Pages.SetMenu(Pages.Menu);

                   Pages.SetPage(Pages.MainControl);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }
            finally
            {
                IsActive = false;
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }




        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }



        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}