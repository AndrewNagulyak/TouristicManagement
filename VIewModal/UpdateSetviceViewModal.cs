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
using System.Windows.Interactivity;
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
        private readonly DelegateCommand _delcountry;
        private readonly DelegateCommand _editcountry;
        private readonly DelegateCommand _delcity;
        private readonly DelegateCommand _editcity;
        private readonly DelegateCommand _delhotel;
        private readonly DelegateCommand _edithotel;
        public DelegateCommand OpenFile { get { return _openfile; } }
        public DelegateCommand VisibleCommand1 { get { return _visible1; } }
        public DelegateCommand VisibleCommand { get { return _visible; } }
        public DelegateCommand addCity { get { return _addcity; } }
        public DelegateCommand addCountry { get { return _addcountry; } }
        public DelegateCommand DeleteCountryCommand { get { return _delcountry; } }
        public DelegateCommand EditCountryCommand { get { return _editcountry; } }
        public DelegateCommand DeleteCityCommand { get { return _delcity; } }
        public DelegateCommand EditCityCommand { get { return _editcity; } }
        public DelegateCommand DeleteHotelCommand { get { return _delhotel; } }
        public DelegateCommand EditHotelCommand { get { return _edithotel; } }
        public DelegateCommand addHotel { get { return _addhotel; } }
        public DelegateCommand AddTouristRouteCommand { get { return _addroute; } }
        #endregion
        #region fields
        private ObservableCollection<City> fromcities;
        private Hotel _hotel;
        private Country _countryparam;
        private City _city;
        private List<City> _citiesTourist;
        private IService<City> _cityService;
        private IService<Hotel> _hotelService;
        private Country _hotelcountry;
        private Route _touristRoute;
        private IService<Country> _countryService;
        private IService<Route> _routeService;
        private Country _country;
        #endregion
        #region enum
        private string[] state;
        private string[] foodstate;
        private string[] stars;
        private string[] transport;
        public string[] State { get { return state; } }
        public string[] Stars { get { return stars; } }
        public string[] FoodState { get { return foodstate; } }
        public string[] Transport { get { return transport; } }
        #endregion

        #region properties
        public Route TouristRoute { get { return _touristRoute; } set { if (value != null) _touristRoute = new Route(null,null,null) {Id=value.Id}; NotifyPropertyChanged("TouristRoute"); } }
        public Country CountryParam { get { return _countryparam; } set { if (value != null) _countryparam = new Country() { CountryName = value.CountryName, Cities = value.Cities }; NotifyPropertyChanged("CountryParam"); } }
        public Country HotelCountry { get { return _hotelcountry; } set { if (value != null) _hotelcountry = new Country() { CountryName = value.CountryName, Cities = value.Cities }; NotifyPropertyChanged("HotelCountry"); NotifyPropertyChanged("Countrycities"); } }

      

        public Country Country { get { return _country; } set { if (value != null) _country = new Country() { CountryName = value.CountryName, Cities = value.Cities, Id = value.Id }; NotifyPropertyChanged("Country"); } }

        public ObservableCollection<City> Cities { get { return _cityService.Get().ToObserverable<City>(); ; } }
        public ObservableCollection<Hotel> Hotels { get { return _hotelService.Get().ToObserverable<Hotel>(); ; } }


        public ObservableCollection<City> FromCityList { get { return _cityService.Get(i=>i.State==CityState.From).ToObserverable<City>(); } }
        public City City { get { return _city; } set { if (value != null) _city = new City() { CityName = value.CityName,Country = value.Country, Id = value.Id }; NotifyPropertyChanged("City"); } }

        public ObservableCollection<Country> Countries { get { return _countryService.Get().ToObserverable<Country>(); } }

     
        public PlacesViewModal CountriesViewModal
        {
            get; set;
        }
        public Hotel Hotel { get { return _hotel; } set { if (value != null) _hotel = new Hotel() { Name = value.Name, Addres=value.Addres,Site=value.Site,Stars=value.Stars,Describe=value.Describe,City=value.City, Id = value.Id }; NotifyPropertyChanged("Hotel"); } }

        public ObservableCollection<City> Countrycities { get { if (_hotelcountry != null) return _cityService.Get(i => i.Country.CountryName == HotelCountry.CountryName).ToObserverable<City>(); else return null; } }

        public List<City> CitiesTourist { get => _citiesTourist; set => _citiesTourist = value; }


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

            _touristRoute = new Route(null, null, null);
            this._routeService = routeService;
            CountriesViewModal = countryViewModal;
            this._cityService = cityService;
            this._countryService = countryService;
            this._hotelService = hotelService;

            _visible = new DelegateCommand((Action<object>)Visible);
            _visible1 = new DelegateCommand((Action<object>)Visible1);
            _addcity = new DelegateCommand((Action<object>)AddCity);
            _addcountry = new DelegateCommand((Action<object>)AddCountry);
            _addroute = new DelegateCommand((Action<object>)AddTouristRoute);
            _addhotel = new DelegateCommand((Action<object>)AddHotel);
            _editcity = new DelegateCommand((Action<object>)EditCity);
            _editcountry = new DelegateCommand((Action<object>)EditCountry);
            _delcity = new DelegateCommand((Action<object>)DeleteCity);
            _delcountry = new DelegateCommand((Action<object>)DeleteCountry);
            _delhotel = new DelegateCommand((Action<object>)DeleteHotel);
            _edithotel = new DelegateCommand((Action<object>)EditHotel);

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
                _cityService.Add(city);
            City.CityName = null;
            City.Country = null;
            
            City.Km = 0;
            OnUpdate(null, null);

        }
        private void AddTouristRoute(object o)
        {

          
            try
            {
                City city = _cityService.FindById(CountriesViewModal.SelectedCity.Id);
                Hotel hotel = _hotelService.FindById(CountriesViewModal.SelectedHotel.Id);

                City city1 = _cityService.FindById(City.Id);
                Route route = new Route(city1, city,hotel) { TouristAmount = TouristRoute.TouristAmount, transport = TouristRoute.transport,type = Modal.Type.Vacation, StartDate = TouristRoute.StartDate, FinishDate = TouristRoute.FinishDate };
                route.Price = (city.Km - city1.Km) / 2 + (route.FinishDate.DayOfYear - route.StartDate.DayOfYear) * hotel.PricePerNight;
                _routeService.Add(route);
                TouristRoute = null;
                CountriesViewModal.SelectedCountry = null;
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
            private void AddHotel(object o)
        {
            Hotel hotel = new Hotel() { City = Hotel.City,Image=Hotel.Image, Addres = Hotel.Addres, Stars = Hotel.Stars, Site = Hotel.Site, FoodState = Hotel.FoodState, Describe = Hotel.Describe, Name = Hotel.Name,PricePerNight=Hotel.PricePerNight };
            _hotelService.Add(hotel);
            Hotel.Addres = null;
            Hotel.Name = null;
            Hotel.Image = null;
            Hotel.Site = null;
            Hotel.Describe = null;
            Hotel.Stars = 0;
            HotelCountry = null;
            Hotel.City = null;
            Hotel.PricePerNight = 0;


            OnUpdate(null, null);
            
        }
        private void AddCountry(object o)
        {
            try
            {
                Country country = new Country() { CountryName = CountryParam.CountryName };
                _countryService.Add(country);
                _countryparam.CountryName = null;
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
        private void EditCity(object o)
        {
            try
            {
                City c = _cityService.FindById(City.Id);
                
                _cityService.Update(c);
                OnUpdate(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void DeleteCity(object o)
        {
            try
            {
                _cityService.Remove(_cityService.FindById(City.Id));
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
        private void EditCountry(object o)
        {
            try
            {
                Country r = _countryService.FindById(CountryParam.Id);
                _countryService.Update(r);
                OnUpdate(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void DeleteCountry(object o)
        {
            try
            {

                Country c = _countryService.FindById(Country.Id);
                _countryService.Remove(c);
                OnUpdate(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void EditHotel(object o)
        {
            try
            {
                Hotel h = _hotelService.FindById(Hotel.Id);
                _hotelService.Update(h);
                OnUpdate(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void DeleteHotel(object o)
        {
            try
            {

                Hotel h = _hotelService.FindById(Hotel.Id);
                _hotelService.Remove(h);
                OnUpdate(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void OnUpdate(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Countries");
            NotifyPropertyChanged("Cities");
            NotifyPropertyChanged("TouristRoute");
            NotifyPropertyChanged("CountryParam");

            NotifyPropertyChanged("Hotels");


            NotifyPropertyChanged("City");
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