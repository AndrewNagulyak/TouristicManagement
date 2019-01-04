using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class Hotel:Base
    {
       public string Site { get; set; }
       public string Addres { get; set; }
       public string Name { get; set; }
       public FoodState FoodState { get; set; }
       public string Describe { get; set; }
       public City City { get; set; }
       public int PricePerNight { get; set; }
       public Stars Stars { get; set; }
       public byte[] HotelImage { get; set; }
    
        public override string ToString()
        {
            return Name;
        }
    }
    public enum FoodState
    {
        AllInclusive,
        None,
        Dinner
    }
    public enum Stars
    {
      One,
      Two,
      Three,
      Four,
      Five
    }
}
