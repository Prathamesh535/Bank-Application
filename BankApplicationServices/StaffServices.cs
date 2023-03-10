using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace BankApplicationServices
{
    public class StaffServices
    {
        public static double ChargeForSameAccount;
        public static double ChargeForDifferentAccount;
        public void CreateBankAccount(string name,string password,string city,string bank)
        {
            var bankProperties = BankDataList.BankList.Where(bankName => bankName.BankName == bank).ToList();
            foreach(var bankList in bankProperties)
            {
                bankList.Users.Add(new Customer(name, password, city, bank, IdGenerator.AccountIdPattern(name)));
                bankList.Account.Add(new Account(IdGenerator.AccountIdPattern(name), 0));
            }
        }
        public void UpdateDetails(string name,string password,int customerIndex,string newName,string newPassword,int select)
        {
                switch (select)
                {
                    case 1:
                        foreach (var bankList in BankDataList.BankList)
                        {
                            foreach(var customerList in bankList.Users)
                            {
                                customerList.Name = newName;
                                customerList.Password = newPassword;
                            }
                        }                       
                        break;
                    case 2:
                        foreach (var bankList in BankDataList.BankList)
                        { 
                            bankList.Users.RemoveAt(customerIndex);                              
                        }
                        break;              
                }
        }

        public Bank Display(string customerName, string customerPassword)
        {
            Bank bankInformation = new Bank();
            foreach(var bank in BankDataList.BankList)
            {
                var customerProperties = bank.Users.Where(user => user.Name == customerName && user.Password == customerPassword && user.Type == UserTypes.Customer).ToList();
                foreach(var customer in customerProperties)
                {
                    bankInformation = bank;
                }
            }
            return bankInformation;
        }
        public List<Transaction> DisplayTransaction(string accountId)
        {
            List<Transaction> transactionList = new List<Transaction>();
            foreach (var bank in BankDataList.BankList)
            {
                var bankAccountId = bank.Account.Where(account => account.AccountId == accountId).ToList();
                foreach (var account in bankAccountId)
                {
                    transactionList = account.TransactionList;
                }
            }
            return transactionList;
        }
        public void AddChargesForSameAccount(double amountForSameAccount)
        {
            ChargeForSameAccount = amountForSameAccount;
        }
        public void AddChargesForDifferentAccount(double amountForDifferentAccount)
        {
            ChargeForDifferentAccount = amountForDifferentAccount;
        }
        public void RevertTransaction(string senderAccountId,string receiverAccountId)
        {
            double transferAmount=0;
            foreach(var bank in BankDataList.BankList)
            {
                foreach (var account in bank.Account)
                {
                    if (account.AccountId == senderAccountId)
                    {
                        foreach(var transaction in account.TransactionList)
                        {
                            transferAmount = transaction.AmountTransfered;
                        }
                        account.Balance += transferAmount;
                    }
                    if(account.AccountId== receiverAccountId)
                    {
                        account.Balance -= transferAmount;
                    }
                }
            }
        }

    }
}
