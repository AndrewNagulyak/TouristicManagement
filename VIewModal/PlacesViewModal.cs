using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.Modal;

namespace TravelManager.VIewModal
{
    public class PlacesViewModal:INotifyPropertyChanged
    {
        private ObservableCollection<Country> _countries;
        private City _city;
        private Country _selectedCountry;

 

        private IService<Country> _countryService;
        private IService<City> _cityService;

        public ObservableCollection<Country> Countries { get { return _countryService.Get().ToObserverable<Country>(); } set { _countries = value; NotifyPropertyChanged("Countries"); } }

        public City SelectedCity { get { return _city; } set { _city = value; NotifyPropertyChanged("SelectedCity"); } }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; NotifyPropertyChanged("IsVisible"); }

        }

        public Country SelectedCountry { get => _selectedCountry; set { _selectedCountry = value; NotifyPropertyChanged("SelectedCountry");}
        }

        public PlacesViewModal(IService<Country> countryService,IService<City> cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
            
            isVisible = false;



        }
        public void Visible(bool t)
        {
            if (t == true)
            {
                isVisible = true;

            }
            else
                isVisible = false;
            NotifyPropertyChanged("IsVisible");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
