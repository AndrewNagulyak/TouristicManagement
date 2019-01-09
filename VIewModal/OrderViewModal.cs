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
        private double pris;
        Tourist _tourist;
        Route _route;
        Order _order;
        public Route Route { get ;set ;}  
        public Tourist Tourist
        { get { return _tourist; } set { if (value != null) _tourist = new Tourist() { Id = value.Id }; NotifyPropertyChanged("Tourist"); } }
        public Order Order { get { return _order; } set { if (value != null) _order = new Order() { Id = value.Id }; NotifyPropertyChanged("Order"); } }


        public OrderViewModal(IService<Route> routeService, IService<Order> orderService, IService<Tourist> touristService, Route r)
        {
            Tourist = new Tourist();
            Order = new Order();
            this.touristService = touristService;
            this.orderService = orderService;
            this.routeService = routeService;
            Route = r;
            _delroute = new DelegateCommand((Action<object>)DeleteRoute);
            _addorder = new DelegateCommand((Action<object>)AddOrder);

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
            Order.RouteId = Route.Id;
            int count = touristService.Get().ToList().Select(x => x.Phone == Tourist.Phone).ToList().Count +1;
            if (count > 3)
                pris = routeService.FindById(Order.RouteId).Price* 0.7;
            else
               pris= routeService.FindById(Order.RouteId).Price* (1-(double)count / 10 );
            Tourist t = new Tourist() { Email = Tourist.Email, Name = Tourist.Name, Phone = Tourist.Phone, Surname = Tourist.Surname ,Id=Tourist.Id};
            touristService.Add(t);
            
            Order r = new Order() { Id=Order.Id,Phone=t.Phone,TouristId=t.Id,RouteId=Route.Id,Price=pris,Route=Route};
            orderService.Add(r);
            Pages.OrderRoute.Close();
            Pages.SetPage(Pages.Routes);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
