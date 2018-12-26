using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class Country
    {
        public virtual ICollection<City> Cities { get; set; }

        public Country()
            {
            Cities = new List<City>();
            }
        [Key]
        [Column(Order = 0)]
        public string Name { get; set; }
    }
}
