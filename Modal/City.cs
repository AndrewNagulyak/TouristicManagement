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
    public class City
    {
        [Key]
        [Column(Order = 0)]
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
