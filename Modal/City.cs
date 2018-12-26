using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    class City
    {
        public string Name { get; set; }
        public CityState State { get;set; }
    }
    public enum CityState
    {
        [Description("From")]
        From,
        [Description("To")]
        To,
       
    }
}
