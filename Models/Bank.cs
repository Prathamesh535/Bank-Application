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
        public string BankId { get; set; }
        public string DefaultCurrency { get; set; }
        public List<Account> Account { get; set; } = new List<Account>();
        public List<User> Users { get; set; } = new List<User>();
        public Bank() { }
        public Bank(string bankName, string location, string ifscCode, string id,string currency)
        {
            this.BankName = bankName;
            this.Location = location;
            this.IFSC_Code = ifscCode;
            this.BankId = id;
            this.DefaultCurrency= currency;
        }
    }
}