using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Repository.Model;

namespace Repository.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User", "dbo");

            #region Keys

            this.Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasKey(x => x.Id);

            #endregion

            #region Properties

            this.Property(x => x.Name);

            this.Property(x => x.Age);

            this.Property(x => x.Generally);

            #endregion

            #region Relationships

            #endregion
        }
    }
}
