using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class RouteService : AllServices<Route>
    {
        DbContext _context;

        public RouteService(DbContext context) : base(context)
        {
            _context = context;

            //ts = _context.Routes.ToList();
        }
    }
}
