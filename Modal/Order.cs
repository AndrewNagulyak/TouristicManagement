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
        
        
        [ForeignKey("Tourist")]
        [Column(Order = 2)]
        public Guid TouristId { get; set; }
        [ForeignKey("Tourist")]
        [Column(Order = 3)]
        public string Phone { get; set; }

        public virtual Tourist Tourist { get; set; }

        public virtual Route Route { get; set; }
        public double Price { get; set; }

    }
}
