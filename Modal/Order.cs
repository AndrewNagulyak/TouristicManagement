using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class Order:Base
    {
        [ForeignKey("Route")]
        [Column(Order = 1)]
        public Guid RouteId { get; set; }
        Route Route { get; set; }

        [ForeignKey("Tourist")]
        [Column(Order = 2)]
        public string TouristNumber { get; set; }
        Tourist Tourist { get; set; }
    }
}
