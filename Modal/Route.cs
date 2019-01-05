using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class Route : Base
    {

        private int days;
        public DateTime StartDate { get; set; }

        public int TouristAmount { get; set; }
        public Type type { get; set; }
        public Transport transport { get; set; }
        public DateTime FinishDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public City FromCity { get; set; }
        [Required]
        public City ToCity { get; set; }
        public int Price { get; set; }
        public Hotel Hotel { get; set; }
        public Route(City fromcity, City tocity, Hotel hotel)
        {
            Hotel = hotel;
            FromCity = fromcity;
            ToCity = tocity;
            StartDate = DateTime.Today;
            FinishDate = DateTime.Today;

            Orders = new List<Order>();

        }
    }


    public enum Transport
    {
        Bus,
        Air,
        BusAir
    }
    public enum Type
    {
        Tourist,
        Vacation,
        Resort
    }
}
