using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    interface IService<T> where T : class
    {
        void Add(T item);
    }
    

 
    class AllServices<T> : IService<T> where T : class
    {
        private DbSet<T> _dbset;
        private TravelContext _context;
        public AllServices(TravelContext context)
        {

            _context = context;
            _dbset = _context.Set<T>();
        }
        public virtual void Add(T item)
        {
            _dbset.Add(item);
            _context.Entry(item).State = EntityState.Added;

            _context.SaveChanges();
        }

    }
}
