using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class CityService : GenericService<City>
    {
        DbContext _context;


        public CityService(DbContext context) : base(context)
        {
            _context = context;

           
        }
        public override IEnumerable<City> Get()
        {
            IEnumerable<City> citiess = base.GetWithInclude(x => x.Hotels);
            return citiess;
        }


    }
}