using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class UserService : GenericService<User>
    {
        DbContext _context;

        public UserService(DbContext context) : base(context)
        {
            _context = context;

        }
    }
}