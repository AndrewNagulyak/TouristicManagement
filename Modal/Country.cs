using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    class Country
    {
        public virtual ICollection<City> Cities { get; set; }
        public Country()
            {
            Cities = new List<City>();
            }
        public string Name { get; set; }
    }
}
