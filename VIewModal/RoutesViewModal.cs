using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelManager.Modal;
using TravelManager.Modal.Services;
using TravelManager.View;

namespace TravelManager.VIewModal
{
    public class RoutesViewModal : IViewModal<RoutesViewModal>, INotifyPropertyChanged
    {
        private Country _country;
        public List<Route> Routes { get => routeService.Get().ToList(); }
        public List<Country> Countries { get => Routes.Select(x => x.ToCity.Country).Distinct().ToList(); }
        public Country Country { get { return _country; } set {  _country = value;  NotifyPropertyChanged("Country"); } }
        public Thickness margin
        {
            get
            {
                Thickness margin = new UserControl().Margin;
                margin.Left = 15;
                margin.Right = 15;
                margin.Top = 15;
                margin.Bottom = 15;
                return margin;
            }
        }
        private List<UserControl> userControls;

        public List<UserControl> UserControls
        {
            set
            {
                List<UserControl> Controls = new List<UserControl>();
                if (Country == null)

                    foreach (Route r in Routes)
                {
                    Controls.Add(new RouteControl() { DataContext = r, Margin = margin});
                    


                }
                else
                    foreach (Route r in Routes)
                    {
                        if(r.ToCity.Country==Country)
                        Controls.Add(new RouteControl() { DataContext = r, Margin = margin });



                    }
                userControls = Controls;
                NotifyPropertyChanged("UserControls");
            }
            get
            {
                return userControls;
            }
        }
        
      
        private IService<Route> routeService;

        public RoutesViewModal(IService<Route> routeService)
        {
            this.routeService = routeService;
            NotifyPropertyChanged("Route");

            var dic = Routes.Select((k, i) => new { k, v = UserControls[i] })
              .ToDictionary(x => x.k, x => x.v);
            foreach (var item in dic)
            {
                item.Value.PreviewMouseDoubleClick += (object sender, MouseButtonEventArgs e) => Pages.OpenWindow(item.Key);

            }
            NotifyPropertyChanged("UserControls");

        }

        #region notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (UserControls == null)
                UserControls = new List<UserControl>();
        }
        #endregion
    }
}
