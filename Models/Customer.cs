using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class Customer:User
    {
        public Customer() { }
        public Customer(string Name,string Password, string city, string bank,string id)
        {
            this.Name = Name;
            this.Password = Password;
            this.City = city;
            this.Bank = bank;
            this.AccountId= id;
            Type = UserTypes.Customer;
        }
        
    }
}
