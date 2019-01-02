using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
   public interface IService<T> where T : class
    {
        void Add(T item);
        IEnumerable<T> Get();
        T  FindById(Guid g);
        T GetObserver(string s);
        IEnumerable<T> Get(Func<T, bool> predicate);
    }

    

 
    public class AllServices<T> : IService<T> where T : Base
    {
        DbContext _context;
        DbSet<T> _dbSet;
        public AllServices(DbContext context)
        {

            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        
        public virtual void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();

        }
        public virtual IEnumerable<T> Get()
        {
            return _dbSet.ToList();
        }
        public virtual ObservableCollection<T> GetObservable()
        {
            List<T> ts = _dbSet.AsNoTracking().ToList();
            ObservableCollection<T> collection = new ObservableCollection<T>();
            foreach (var item in ts)
            {
                collection.Add(item);
            }
            return collection;
        }

        public virtual T FindById(Guid id)
        {
            return _dbSet.Find(id);
        }
        public virtual T GetObserver(string name)
        {
            DbSet<T> _dbset =_context.Set<T>();
            return _dbset.Find(name);
        }
        
    }
}
