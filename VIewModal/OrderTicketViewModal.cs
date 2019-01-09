using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelManager.Modal;
using TravelManager.View;

namespace TravelManager.VIewModal
{
    
    public class OrderTicketViewModal:IViewModal<OrderTicketViewModal>, INotifyPropertyChanged
    {
        IService<Order> orderService;
        ObservableCollection<Order> _orders;
        Order _order;

        public ObservableCollection<Order> Orders { get { return _orders; }set { _orders = value; NotifyPropertyChanged("Orders"); } }
        public Order Order { get { return _order; } set { _order = value;  NotifyPropertyChanged("Order"); } }
        
        public OrderTicketViewModal(IService<Route> routeService, IService<Order> orderService, IService<Tourist> touristService)
        {
            this.orderService = orderService;
            _orders=orderService.Get().ToObserverable();
           
            NotifyPropertyChanged("Orders");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
           
        }
    }
}
