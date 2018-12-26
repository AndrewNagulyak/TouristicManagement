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
using TravelManager.View;
using TravelManager.VIewModal;

namespace TravelManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public void SetPage(UserControl page)
        {
            this.Main.Content = page;
        }
        public void SetMenu(UserControl page)
        {
           this.GridMenu.Content=page;
           
        }
      
        public IViewModel ViewModel
        {
            set { DataContext = value; }
        }

        
    }
}
