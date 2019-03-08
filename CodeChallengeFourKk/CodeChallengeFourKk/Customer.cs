using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallengeFourKk
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public static IEnumerable<Customer> Generate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = "FirstName " + i,
                    LastName = "LastName " + i,
                    Age = 10 + i,
                    DateOfBirth = DateTime.UtcNow
                };
            }
        }
    }
}
