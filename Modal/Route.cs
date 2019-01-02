using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class Route:Base
    {

        private int days;   
        public DateTime StartDate { get; set; }
        public int Days
        {
            get
            {
                return days;
            }
            set
            {
                days = 2;
            }
        }
        public FoodState FoodState { get; set; }
        public int TouristAmount { get; set; }
        public int Promotion { get; set; }
        public Type type { get; set; }
        public Transport transport { get; set; }
        public DateTime FinishDate { get; set; }
        public List<City> Cities { get; set; }

        public int Price { get; set; }
        public Hotel Hotel { get; set; }

    }
    public enum Transport
    {
        [Description("Bus")]
        Bus,
        [Description("Plane")]
        Air,
        [Description("BusPlane")]
        BusAir
    }
    public enum Type
    {
        [Description("Tourist")]
        Tourist,
        [Description("Vacation")]
        Vacation,
        [Description("Resort")]
        Resort
    }
}
