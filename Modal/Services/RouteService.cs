using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class RouteService : GenericService<Route>
    {
        DbContext _context;

        public RouteService(DbContext context) : base(context)
        {
            _context = context;

            //ts = _context.Routes.ToList();
        }
        public override IEnumerable<Route> Get()
        {
            IEnumerable<Route> countries = base.GetWithInclude(x => x.ToCity,p=>p.Hotel,z=>z.FromCity, y => y.ToCity.Country);
            return countries;
        }
    }
}
