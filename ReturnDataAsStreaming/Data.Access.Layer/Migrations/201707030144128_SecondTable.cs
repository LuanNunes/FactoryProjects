namespace Data.Access.Layer.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SecondTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "fake.UserDetails",
                c => new
                    {
                        user_id = c.Int(nullable: false),
                        username = c.String(),
                        first_name = c.String(),
                        last_name = c.String(),
                        gender = c.String(),
                        password = c.String(),
                        status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropTable("fake.UserDetails");
        }
    }
}
