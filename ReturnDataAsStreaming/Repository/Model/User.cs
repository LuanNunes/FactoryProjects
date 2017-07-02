using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Model
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }
    }
}
