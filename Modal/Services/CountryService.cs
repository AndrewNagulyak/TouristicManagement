using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.VIewModal;

namespace TravelManager.Modal.Services
{

    public class CountryService : AllServices<Country>
    {
        public CountryService(DbContext context) : base(context)
        {

        }
        public override IEnumerable<Country> Get()
        {
            IEnumerable<Country> countries = base.GetWithInclude(x => x.Cities);
            return countries;
        }

    }
}

