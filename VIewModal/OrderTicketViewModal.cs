using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelManager.Modal;
using TravelManager.View;

namespace TravelManager.VIewModal
{
   public class OrderTicketViewModal:IViewModal<OrderTicketViewModal>
    {
        public OrderTicketViewModal(IService<Route> routeService, IService<Order> orderService, IService<Tourist> touristService)
        {

        }
    }
}
