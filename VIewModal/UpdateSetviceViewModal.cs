using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TravelManager.Modal;
using TravelManager.Modal.Services;

namespace TravelManager.VIewModal
{
    public static class CollectioConverter
    {

        public static ObservableCollection<T> ToObserverable<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
    public class UpdateServiceViewModal : INotifyPropertyChanged
    {
        #region commands
        private readonly DelegateCommand _visible;
        private readonly DelegateCommand _visible1;
        private readonly DelegateCommand _addcity;
        private readonly DelegateCommand _addcountry;
        private readonly DelegateCommand _addroute;
        #endregion
        #region fields
        private ObservableCollection<City> fromcities;
        private Country countryparam;
        private City city;
        private string[] state;
        private IService<City> cityService;
        private Route touristRoute;
        private IService<Country> countryService;
        private IService<Route> routeService;
        #endregion
        #region properties
        public Route TouristRoute { get { return touristRoute; } set { touristRoute = value; NotifyPropertyChanged("TouristRoute"); } }
        public Country CountryParam { get { return countryparam; } set { if (value != null) countryparam = new Country() { CountryName = value.CountryName, Cities = value.Cities }; NotifyPropertyChanged("CountryParam"); } }
        public string[] State
        {
            get
            {
                return state;
            }
        }
        public ObservableCollection<City> Cities { get { return cityService.Get().ToObserverable<City>(); ; } }
        public ObservableCollection<City> FromCityList { get { return fromcities; } set { fromcities = value; NotifyPropertyChanged("FromCityList"); } }
        public City City { get { return city; } set { if (value != null) city = new City() { CityName = value.CityName, State = CityState.To, Country = value.Country, Id = value.Id }; NotifyPropertyChanged("City"); } }
        public ObservableCollection<Country> Countries { get { return countryService.Get().ToObserverable<Country>(); } }
        public DelegateCommand VisibleCommand1 { get { return _visible1; } }
        public DelegateCommand VisibleCommand { get { return _visible; } }
        public DelegateCommand addCity { get { return _addcity; } }
        public DelegateCommand addCountry { get { return _addcountry; } }
        public DelegateCommand AddTouristRouteCommand { get { return _addroute; } }
        public PlacesViewModal CountriesViewModal
        {
            get; set;
        }
        #endregion
        //TouristRoute;
        public UpdateServiceViewModal(IService<Country> countryService, IService<Route> routeService, IService<City> cityService, PlacesViewModal countryViewModal)

        {
            state = Enum.GetNames(typeof(CityState))
                     .Select(x => x.ToString())
                      .ToArray();

            touristRoute = new Route();
            this.routeService = routeService;
            CountriesViewModal = countryViewModal;
            //FromCityList = cityService.GetObservableFrom();
            this.cityService = cityService;
            this.countryService = countryService;
            _visible = new DelegateCommand((Action<object>)Visible);
            _visible1 = new DelegateCommand((Action<object>)Visible1);
            _addcity = new DelegateCommand((Action<object>)AddCity);
            _addcountry = new DelegateCommand((Action<object>)AddCountry);
            _addroute = new DelegateCommand((Action<object>)AddTouristRoute);
            City = new City();
            CountryParam = new Country();



        }
        #region notify
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChangedEvent;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "City" && CollectionChangedEvent != null) CollectionChangedEvent.Invoke(null, null);

        }
        #endregion
        private void AddCity(object o)
        {
            try
            {
                City city = new City() { CityName = City.CityName, Country = City.Country, State = City.State };
                cityService.Add(city);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void AddTouristRoute(object o)
        {
            City c = cityService.GetObserver(CountriesViewModal.SelectedCity.CityName);
            City c1 = cityService.GetObserver(City.CityName);

            touristRoute.Cities.Add(c);
            touristRoute.Cities.Add(c1);

            touristRoute.transport = Transport.Air;
            touristRoute.type = Modal.Type.Tourist;
            try
            {
                Route route = TouristRoute;
                //  routeService.Create(route);
                routeService.Add(route);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void AddCountry(object o)
        {
            try
            {

                Country country = new Country() { CountryName = countryparam.CountryName };
                countryService.Add(country);
                OnUpdate(null, null);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    MessageBox.Show("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" +
                        eve.Entry.Entity.GetType().Name + eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show("- Property: \"{0}\", Error: \"{1}\"" +
                            ve.PropertyName + ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public void OnUpdate(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Countries");
            NotifyPropertyChanged("Cities");
            NotifyPropertyChanged("Routes");
        }
        private void Visible(object o)
        {
            if (CountriesViewModal.IsVisible == false)
                CountriesViewModal.Visible(true);


        }
        private void Visible1(object o)
        {
            if (CountriesViewModal.IsVisible == false)
                CountriesViewModal.Visible(true);
            else if (!(o is System.Windows.Controls.TextBox))
                CountriesViewModal.Visible(false);

        }



    }


}