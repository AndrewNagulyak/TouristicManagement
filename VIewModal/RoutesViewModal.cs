using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.Modal;
using TravelManager.Modal.Services;
using TravelManager.View;

namespace TravelManager.VIewModal
{
    public class RoutesViewModal:IViewModal<RoutesViewModal>,INotifyPropertyChanged
    {
        //public List<Route> Route{ get => routeService.Get().ToList(); }
        private IService<Route> routeService;

        public RoutesViewModal(IService<Route> routeService)
        {
            this.routeService = routeService;
            NotifyPropertyChanged("Route");
        }
        #region notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
