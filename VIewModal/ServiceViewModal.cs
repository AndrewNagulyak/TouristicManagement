using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.Modal;
using TravelManager.Modal.Services;

namespace TravelManager.VIewModal
{
    class ServiceViewModal : INotifyPropertyChanged
    {
        private readonly DelegateCommand _addCommand;
        private IService<Country> countryService;
        public ServiceViewModal(IService<Country> countryService)
        {
            this.countryService = countryService;
            _addCommand = new DelegateCommand((Action<object>)Add);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand AddCommand { get { return _addCommand; } }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Add(object o)
        {
            try
            {
                Country c = new Country();
                countryService.Add(c);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}