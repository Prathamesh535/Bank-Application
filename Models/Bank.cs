using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
namespace Models
{
    public class Bank
    {
        public string BankName { get; set; }
        public string Location { get; set; }
        public string IFSC_Code { get; set; }
        public string Bank_Id { get; set; }
        public List<Account> Account { get; set; } = new List<Account>();
        public List<User> Users { get; set; } = new List<User>();
        public Bank() { }
        public Bank(string bankName, string location, string ifsc_code, string id)
        {
            this.BankName = bankName;
            this.Location = location;
            this.IFSC_Code = ifsc_code;
            this.Bank_Id = id;
        }
    }
}