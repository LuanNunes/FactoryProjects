using System.Data.Entity;
using Data.Access.Layer.Mapping;
using Data.Access.Layer.Model;

namespace Data.Access.Layer.Context
{
    public class FakeIItEasyContext : DbContext
    {
        public FakeIItEasyContext() : base("Name=MyContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserDetailMap());
        }
    }
}
