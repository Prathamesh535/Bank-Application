using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Bank { get; set; }
        public string AccountId { get; set; }
        public UserTypes Type { get; set; }
    }
}
