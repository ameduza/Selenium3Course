using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lesson7
{
    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public int Postcode{ get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Password { get; set; }

    }
}
