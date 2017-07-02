using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Access.Layer.Model;

namespace Data.Access.Layer.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User", "fake");

            #region Keys

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasKey(x => x.Id);

            #endregion

            #region Properties

            


            #endregion

            #region Relationships

            

            #endregion
        }
    }
}
