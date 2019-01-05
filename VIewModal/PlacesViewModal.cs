using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.Modal;
using TravelManager.View;

namespace TravelManager.VIewModal
{
    public class PlacesViewModal:INotifyPropertyChanged,IViewModal<PlacesViewModal>
    {
        #region fields
        private ObservableCollection<Country> _countries;
        private City _city;
        private Country _selectedCountry;
        private Hotel _hotel;
        private bool _isVisible;


        private IService<Country> _countryService;
        private IService<City> _cityService;
        #endregion
        #region properties
                public ObservableCollection<Country> Countries { get { return _countryService.Get().ToObserverable<Country>(); } set { _countries = value; NotifyPropertyChanged("Countries"); } }
        

                public City SelectedCity { get { return _city; } set { _city = value; NotifyPropertyChanged("SelectedCity"); } }
                public Hotel SelectedHotel { get { return _hotel; } set { _hotel = value; NotifyPropertyChanged("SelectedHotel"); NotifyPropertyChanged("Selected"); } }
                public bool IsVisible
                {
                    get { return _isVisible; }
                    set { _isVisible = value; NotifyPropertyChanged("IsVisible"); }

                }
                public Country SelectedCountry { get => _selectedCountry; set { _selectedCountry = value; NotifyPropertyChanged("SelectedCountry");}
                }
                public bool Selected { get { if (SelectedHotel == null) return false; else return true; } }
        #endregion
        #region Methods
        public PlacesViewModal(IService<Country> countryService,IService<City> cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
            
            _isVisible = false;



        }
        public void Visible(bool t)
        {
            if (t == true)
            {
                _isVisible = true;

            }
            else
                _isVisible = false;
            NotifyPropertyChanged("IsVisible");
            NotifyPropertyChanged("Countries");
        }
        #endregion
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
