using System.Data.Entity;
using Repository.Model;

namespace Repository.Context
{
    public class FakeItEasyContext : DbContext
    {
        public FakeItEasyContext() : base("Name=MyContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<User>().ToTable("User");

        }
    }
}
