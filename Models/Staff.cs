using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Staff:User
    {
        public int Salary;
        public Staff() { }
        public Staff(string StaffName,string StaffPassword )
        {
            this.Name = StaffName;
            this.Password = StaffPassword;
            Type= UserTypes.Staff;
        } 
    }
}
