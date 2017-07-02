namespace Data.Access.Layer.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Access.Layer.Context.FakeIItEasyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FakeIItEasyContext";
        }

        protected override void Seed(Data.Access.Layer.Context.FakeIItEasyContext context)
        {
        }
    }
}
