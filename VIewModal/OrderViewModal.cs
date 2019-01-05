using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelManager.Modal;
using TravelManager.View;

namespace TravelManager.VIewModal
{
   public class OrderViewModal:IViewModal<OrderViewModal>,INotifyPropertyChanged
    {

        private readonly DelegateCommand _delroute;
        public DelegateCommand DeleteRouteCommand { get { return _delroute; } }
        private readonly DelegateCommand _addorder;
        public DelegateCommand AddOrderCommand { get { return _addorder; } }


        private IService<Route> routeService;
        private IService<Tourist> touristService;
        private IService<Order> orderService;


        Route route;
        public Route Route { get { return route; }set { route = value; }  }
        public Tourist Tourist { get; set; }
        public Order Order { get; set; }

        public OrderViewModal(IService<Route> routeService, IService<Order> orderService, IService<Tourist> touristService, Route r)
        {
            this.routeService = routeService;
            Route = r;
            _delroute = new DelegateCommand((Action<object>)DeleteRoute);

        }

        private void DeleteRoute(object obj)
        {
            Route r = routeService.FindById(Route.Id);
            routeService.Remove(r);
            Pages.OrderRoute.Close();
            Pages.SetPage(Pages.Routes);
        }

        private void AddOrder(object o)
        {
            Tourist t = new Tourist() { Email = Tourist.Email, Name = Tourist.Name, Phone = Tourist.Phone, Surname = Tourist.Surname };
            touristService.Add(t);
            Order r = new Order() { };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
