using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class City:Base
    {

        
        public string CityName { get; set; }
        public CityState State { get;set; }
        public Country Country { get; set; }
        public override string ToString()
        {
            return CityName;
        }
    }
    public enum CityState
    {
        To,
        From,
       
    }
}
