using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class Tourist:Base
    {
        [Key]
        [Column(Order = 1)]
        public string Phone { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
         
        public string Surname { get; set; }
       
        public virtual ICollection<Order> Orders { get; set; }
        public Tourist()
        {
           Orders = new List<Order>();

        }

    }
}
