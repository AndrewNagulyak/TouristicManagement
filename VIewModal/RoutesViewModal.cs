using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.Modal;
using TravelManager.Modal.Services;
using TravelManager.View;

namespace TravelManager.VIewModal
{
    public class RoutesViewModal:IViewModal<RoutesViewModal>
    {
        List<Route> Routes{ get => routeService.Get().ToList(); }
        private IService<Route> routeService;

        public RoutesViewModal(IService<Route> routeService)
        {
            this.routeService = routeService;

        }

    }
}
