using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelManager.Modal
{
    public class TravelContext : DbContext
    {

        public TravelContext() : base("DBConnection")
        { }
        public DbSet<InternalUserData> UsersData { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TravelContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
