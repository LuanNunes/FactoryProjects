using System;
using System.Data.SqlClient;
using Data.Access.Layer.Context;
using Data.Access.Layer.Model;
using System.Collections.Generic;
using System.Threading;

namespace Data.Access.Layer.Core
{
    public class UserRepository
    {
        private readonly FakeIItEasyContext _context;

        private string _connectionString = @"Data Source=.\localhost;Initial Catalog=FakeItEasy;User ID = teste; Password=teste";
        
        public UserRepository()
        {
            this._context = new FakeIItEasyContext();
        }

        public IEnumerable<User> GetUsers(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT Id, FirstName, LastName, Email, Gender, Age FROM [fake].[User] where FirstName like @name";
                    cmd.Parameters.AddWithValue("@name", String.Format("%{0}%", name));

                    connection.Open();
                    var reader = cmd.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                Gender = reader.GetString(4),
                                Age = reader.GetInt32(5)
                            };
                            Thread.Sleep(1000);
                            yield return user;
                        }
                    }

                }
            }
        }
    }
}
