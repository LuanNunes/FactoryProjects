using Microsoft.EntityFrameworkCore;
using Repository.Model;

namespace Repository.Context
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\localhost;Initial Catalog=FakeItEasy;Integrated Security=True");
        }

        public DbSet<User> Users { get; set; }
    }
}
