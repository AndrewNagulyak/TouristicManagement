using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelManager.Modal;
using TravelManager.VIewModal;

namespace TravelManager
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
           // this.Show();
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());

            try
            {
                InitializeComponent();
                this.Width = 500;
                this.Height = 500;
                ViewModel = viewModel;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }
    }
}