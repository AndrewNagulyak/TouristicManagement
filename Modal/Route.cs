using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    class Route:Base
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
                days = (FinishDate - StartDate).Days;
            }
        }
        public FoodState FoodState { get; set; }
        public int TouristAmount { get; set; }
        public int Promotion { get; set; }
        public Transport transport { get; set; }
        public DateTime FinishDate { get; set; }
        public City OutCity { get; set; }
        public City InCity { get; set; }

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
}
