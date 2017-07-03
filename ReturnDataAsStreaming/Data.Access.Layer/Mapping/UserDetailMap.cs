using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Access.Layer.Model;

namespace Data.Access.Layer.Mapping
{
    public class UserDetailMap : EntityTypeConfiguration<UserDetail>
    {
        public UserDetailMap()
        {
            ToTable("UserDetails", "fake");

            #region Keys

            Property(x => x.Id)
                .HasColumnName("user_id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasKey(x => x.Id);

            #endregion

            #region Properties

            Property(x => x.UserName).HasColumnName("username");
            Property(x => x.FirstName).HasColumnName("first_name");
            Property(x => x.LastName).HasColumnName("last_name");
            Property(x => x.Gender).HasColumnName("gender");
            Property(x => x.Password).HasColumnName("password");
            Property(x => x.Status).HasColumnName("status");



            #endregion

            #region Relationships



            #endregion
        }
    }
}
