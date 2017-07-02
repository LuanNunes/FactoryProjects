using System.Data.Entity;
using Data.Access.Layer.Mapping;
using Data.Access.Layer.Model;

namespace Data.Access.Layer.Context
{
    public class FakeIItEasyContext : DbContext
    {
        internal FakeIItEasyContext() : base("Name=MyContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
