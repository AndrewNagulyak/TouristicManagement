using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class Country:Base
    {
        public virtual ICollection<City> Cities { get; set; }

        public Country()
            {
            Cities = new List<City>();
            }
        
        public string CountryName { get; set; }
        public override string ToString()
        {
            return CountryName;
        }
    }
}
