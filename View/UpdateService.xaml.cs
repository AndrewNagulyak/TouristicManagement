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
using TravelManager.VIewModal;

namespace TravelManager.View
{
    /// <summary>
    /// Логика взаимодействия для UpdateService.xaml
    /// </summary>
    public partial class UpdateService : UserControl
    {
        public UpdateService(UpdateServiceViewModal updateServiceViewModal)
        {
            InitializeComponent();
            ViewModal = updateServiceViewModal;
            tb1.Focus();
        }
        public UpdateServiceViewModal ViewModal
        {
            set { DataContext = value; }
        }
      

    }
}
