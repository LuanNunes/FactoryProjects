namespace CodeChallengeFourKk
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
             CREATE TABLE [dbo].[Customer](
              [Id] [uniqueidentifier] NOT NULL,
              [FirstName] [nvarchar](150) NULL,
              [LastName] [nvarchar](150) NULL,
              [Age] [int] NULL,
              [DateOfBirth] [datetime2](7) NULL)
             */

            var customers = Customer.Generate(4000000);


            //TODO: Insert this in database
        }
    }
}
