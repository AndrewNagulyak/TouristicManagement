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
       public string Mail { get; set; }
       public string Describe { get; set; }
       public City City { get; set; }
       public int Stars { get; set; }

    }
    public enum FoodState
    {
        [Description("All")]
        All,
        [Description("None")]
        None,
        [Description("DinnerBreakfast")]
        Occupied
    }
}
