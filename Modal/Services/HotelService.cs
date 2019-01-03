using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal.Services
{
    class HotelService:AllServices<Hotel>

    {
        DbContext _context;
        DbSet _dbSet;
        public HotelService(DbContext context) : base(context)
        {
            this._context = context;
            _dbSet = _context.Set<Hotel>();

        }
        public override void Add(Hotel item)
        {
            _dbSet.Add(item);
           _context.SaveChanges();

        }
    }
}
