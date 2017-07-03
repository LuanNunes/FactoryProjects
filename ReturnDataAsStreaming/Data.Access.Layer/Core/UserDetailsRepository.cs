using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Data.Access.Layer.Context;
using Data.Access.Layer.Model;

namespace Data.Access.Layer.Core
{
    public class UserDetailsRepository
    {
        private readonly FakeIItEasyContext _context;

        private string _connectionString = @"Data Source=.\localhost;Initial Catalog=FakeItEasy;User ID = teste; Password=teste";

        public UserDetailsRepository()
        {
            this._context = new FakeIItEasyContext();
        }

        public IEnumerable<UserDetail> GetUser(string name)
        {
            var query = from u in this._context.UserDetails
                        where u.UserName.Contains(name)
                        select new
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Gender = u.Gender
                        };

            var resul = new List<UserDetail>();
            foreach (var u in query)
            {
                resul.Add(new UserDetail
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Gender = u.Gender
                });
            }

            return resul;
        }

        public IEnumerable<UserDetail> GetUsersAsync(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT user_id,username,first_name,last_name,gender,password,status FROM [fake].[UserDetails] where username like @name";
                    cmd.Parameters.AddWithValue("@name", $"%{name}%");

                    connection.Open();
                    var reader = cmd.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            var user = new UserDetail
                            {
                                Id = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Gender = reader.GetString(4)
                            };
                            //Thread.Sleep(1000);
                            yield return user;
                        }
                    }

                }
            }
        }
    }
}
