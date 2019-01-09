using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class UserDataService : GenericService<InternalUserData>
    {
        DbContext _context;

        public UserDataService(DbContext context) : base(context)
        {
            _context = context;

        }
    }
}