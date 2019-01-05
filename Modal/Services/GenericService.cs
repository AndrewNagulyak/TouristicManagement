using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
        void Remove(T item);
        void Update(T item);
        IEnumerable<T> Get(Func<T, bool> predicate);
    }

    

 
    public class GenericService<T> : IService<T> where T : Base
    {
        DbContext _context;
        DbSet<T> _dbSet;
        public GenericService(DbContext context)
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
            try
            {

            
            _dbSet.Add(item);
            _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public virtual void Update(T item)
        {
            _context.SaveChanges();
        }
        public virtual void Remove(T item)
        {
            

            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public virtual T FindById(Guid id)
        {
            return _dbSet.Find(id);
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

     
        public virtual T GetObserver(string name)
        {
            DbSet<T> _dbset =_context.Set<T>();
            return _dbset.Find(name);
        }

        protected IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

      

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
