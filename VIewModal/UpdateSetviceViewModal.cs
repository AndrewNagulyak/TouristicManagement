using Microsoft.Win32;
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
using TravelManager.View;

namespace TravelManager.VIewModal
{
    public static class CollectioConverter
    {

        public static ObservableCollection<T> ToObserverable<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
    public interface IOpenFileService
    {
        string FileName { get; }
        bool OpenFileDialog();
    }
    public class UpdateServiceViewModal : INotifyPropertyChanged,IViewModal<UpdateServiceViewModal>
    {
        #region commands
        private readonly DelegateCommand _visible;
        private readonly DelegateCommand _visible1;
        private readonly DelegateCommand _addcity;
        private readonly DelegateCommand _addcountry;
        private readonly DelegateCommand _addhotel;
        private readonly DelegateCommand _addroute;
        private readonly DelegateCommand _openfile;
        #endregion
        #region fields
        private ObservableCollection<City> fromcities;
        private Hotel hotel;
        private Country countryparam;
        private City city;
        private string[] state;
        private string[] foodstate;
        private string[] stars;
        private string[] transport;
        private List<City> citiesTourist;
        private IService<City> cityService;
        private IService<Hotel> hotelService;
        private Country hotelcountry;
        private Route touristRoute;
        private IService<Country> countryService;
        private IService<Route> routeService;
        #endregion
        #region properties
        public Route TouristRoute { get { return touristRoute; } set { if (value != null) touristRoute = new Route(null,null,null) {Id=value.Id}; NotifyPropertyChanged("TouristRoute"); } }
        public Country CountryParam { get { return countryparam; } set { if (value != null) countryparam = new Country() { CountryName = value.CountryName, Cities = value.Cities }; NotifyPropertyChanged("CountryParam"); } }
        public Country HotelCountry { get { return hotelcountry; } set { if (value != null) hotelcountry = new Country() { CountryName = value.CountryName, Cities = value.Cities }; NotifyPropertyChanged("HotelCountry"); NotifyPropertyChanged("Countrycities"); } }

        public string[] State{get   { return state; }}
        public string[] Stars { get { return stars; } }
        public string[] FoodState { get { return foodstate; } }
        public string[] Transport { get { return transport; } }


        public ObservableCollection<City> Cities { get { return cityService.Get().ToObserverable<City>(); ; } }
        public ObservableCollection<City> FromCityList { get { return cityService.Get(i=>i.State==CityState.From).ToObserverable<City>(); } }
        public City City { get { return city; } set { if (value != null) city = new City() { CityName = value.CityName,Country = value.Country, Id = value.Id }; NotifyPropertyChanged("City"); } }

        public ObservableCollection<Country> Countries { get { return countryService.Get().ToObserverable<Country>(); } }

        public DelegateCommand OpenFile { get { return _openfile; } }
        public DelegateCommand VisibleCommand1 { get { return _visible1; } }
        public DelegateCommand VisibleCommand { get { return _visible; } }
        public DelegateCommand addCity { get { return _addcity; } }
        public DelegateCommand addCountry { get { return _addcountry; } }
        public DelegateCommand addHotel { get { return _addhotel; } }
        public DelegateCommand AddTouristRouteCommand { get { return _addroute; } }
        public PlacesViewModal CountriesViewModal
        {
            get; set;
        }
        public Hotel Hotel { get { return hotel; } set { if (value != null) hotel = new Hotel() { Name = value.Name, Addres=value.Addres,Site=value.Site,Stars=value.Stars,Describe=value.Describe,City=value.City, Id = value.Id }; NotifyPropertyChanged("Hotel"); } }

        public ObservableCollection<City> Countrycities { get { if (hotelcountry != null) return cityService.Get(i => i.Country.CountryName == HotelCountry.CountryName).ToObserverable<City>(); else return null; } }

        public List<City> CitiesTourist { get => citiesTourist; set => citiesTourist = value; }


        #endregion
        //TouristRoute;
        public UpdateServiceViewModal(IService<Country> countryService, IService<Route> routeService, IService<City> cityService,IService<Hotel> hotelService, PlacesViewModal countryViewModal)

        {
            state = Enum.GetNames(typeof(CityState))
                     .Select(x => x.ToString())
                      .ToArray();
            stars = Enum.GetNames(typeof(Stars))
                     .Select(x => x.ToString())
                      .ToArray();
            foodstate = Enum.GetNames(typeof(FoodState))
                     .Select(x => x.ToString())
                      .ToArray();
            transport = Enum.GetNames(typeof(Transport)).Select(x => x.ToString()).ToArray();

            touristRoute = new Route(null, null, null);
            this.routeService = routeService;
            CountriesViewModal = countryViewModal;
            this.cityService = cityService;
            this.countryService = countryService;
            this.hotelService = hotelService;

            _visible = new DelegateCommand((Action<object>)Visible);
            _visible1 = new DelegateCommand((Action<object>)Visible1);
            _addcity = new DelegateCommand((Action<object>)AddCity);
            _addcountry = new DelegateCommand((Action<object>)AddCountry);
            _addroute = new DelegateCommand((Action<object>)AddTouristRoute);
            _addhotel = new DelegateCommand((Action<object>)AddHotel);
            _openfile = new DelegateCommand((Action<object>)Openfile);
            City = new City();
            CitiesTourist = new List<City>();
            CountryParam = new Country();
            Hotel = new Hotel();


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
        private void Openfile(object o)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                Hotel.Image = dlg.FileName;
            }
        }

    
        private void AddCity(object o)
        {
           
                City city = new City() { CityName = City.CityName, Country = City.Country, State = City.State,Km=City.Km};
                cityService.Add(city);
            OnUpdate(null, null);

        }
        private void AddTouristRoute(object o)
        {

          
            try
            {
                City city = cityService.FindById(CountriesViewModal.SelectedCity.Id);
                Hotel hotel = hotelService.FindById(CountriesViewModal.SelectedHotel.Id);

                City city1 = cityService.FindById(City.Id);
                Route route = new Route(city1, city,hotel) { TouristAmount = TouristRoute.TouristAmount, transport = TouristRoute.transport,type = Modal.Type.Vacation, StartDate = TouristRoute.StartDate, FinishDate = TouristRoute.FinishDate };
                route.Price = (city.Km - city1.Km) / 2 + (route.FinishDate.DayOfYear - route.StartDate.DayOfYear) * hotel.PricePerNight;
                routeService.Add(route);
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
            private void AddHotel(object o)
        {
            Hotel hotel = new Hotel() { City = Hotel.City,Image=Hotel.Image, Addres = Hotel.Addres, Stars = Hotel.Stars, Site = Hotel.Site, FoodState = Hotel.FoodState, Describe = Hotel.Describe, Name = Hotel.Name,PricePerNight=Hotel.PricePerNight };
            hotelService.Add(hotel);
            OnUpdate(null, null);
            
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
            NotifyPropertyChanged("Countrycities");
            NotifyPropertyChanged("Hotel");

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