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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelManager.View
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : UserControl
    {
        public AdminMenu()
        {
            InitializeComponent();
        }
        private void Routes_click(object sender, MouseButtonEventArgs e) => Pages.SetPage(Pages.Routes);
        private void Service_click(object sender, MouseButtonEventArgs e) => Pages.SetPage(Pages.UpdateService);
        private void Order_click(object sender, MouseButtonEventArgs e) => Pages.SetPage(Pages.OrderTiket);
        private void Create_click(object sender, MouseButtonEventArgs e) => Pages.SetPage(Pages.CreateUser);

    }
}
