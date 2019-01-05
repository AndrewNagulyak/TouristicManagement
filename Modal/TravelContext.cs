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

        public TravelContext() : base("DBConect")
        {
        }
        public DbSet<InternalUserData> UsersData { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Tourist> Tourists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

    }
}
