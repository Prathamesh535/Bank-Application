using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account
    {
        public double Balance = 0;
        //public string  Number{ get; set; }
        public string AccountId { get; set; }
        public List<Transaction> TransactionList { get; set; } = new List<Transaction>();

        public Account(string accountId,long balance) 
        {
            this.AccountId = accountId;
            this.Balance = balance;
        }
    }
}
