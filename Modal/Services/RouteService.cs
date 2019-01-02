using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class RouteService : AllServices<Route>
    {
        TravelContext _context;
        List<Route> ts;

        public RouteService(TravelContext context) : base(context)
        {
            _context = context;

            //ts = _context.Routes.ToList();
        }
    }
}
