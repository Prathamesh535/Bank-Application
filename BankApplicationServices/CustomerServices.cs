using Models;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml.Linq;

namespace BankApplicationServices
{
    public class CustomerServices
    {
        public string IsCustomerRegistered(string customerName,string customerPassword)
        {
            foreach(var bank in BankDataList.BankList)
            {
                foreach (var users in bank.Users)
                {
                    if (customerName == users.Name && customerPassword == users.Password && users.Type == UserTypes.Customer)
                    {
                        return users.AccountId;
                    }
                }
            }
            return null;
        }
        public bool IsAccountIdPresent(string accountId)
        {
            foreach(var bank in BankDataList.BankList)
            {
                foreach(var customer in bank.Account)
                {
                    if(customer.AccountId==accountId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void DepositMoney(string customerName, string customerPassword, double amount)
        {
            string accountId=null,bankName=null;
            foreach (var bankList in BankDataList.BankList)
            {
                foreach (var customerList in bankList.Users)
                {
                    if (customerList.Name.Equals(customerName) && customerList.Password.Equals(customerPassword) && customerList.Type == UserTypes.Customer)
                    {
                        bankName = customerList.Bank;
                        accountId = customerList.AccountId;
                        break;
                    }
                }
                foreach(var account in bankList.Account)
                {
                    if(accountId==account.AccountId)
                    {
                        account.Balance += amount;
                        string comment = $"Amount of {amount} is Deposited to the account and the Transaction Id is {IdGenerator.TransactionId(bankName, customerName)}";
                        account.TransactionList.Add(new Transaction(amount,IdGenerator.TransactionId(bankName,customerName),comment));
                    }
                }
            }   
        }
    
        public void WithDrawMoney(string customerName, string customerPassword, double amount)
        {
            string accountId = null,bankName=null;
            foreach (var bankList in BankDataList.BankList)
            {
                foreach (var customerList in bankList.Users)
                {
                    if (customerList.Name.Equals(customerName) && customerList.Password.Equals(customerPassword) && customerList.Type==UserTypes.Customer)
                    {
                        accountId = customerList.AccountId;
                        bankName = customerList.Bank;
                        break;
                    }
                }
                foreach(var account in bankList.Account)
                {
                    if (account.AccountId == accountId)
                    {
                        account.Balance -= amount;
                        string comment = $"Amount of {amount} is WithDrawed from the Account and the Transaction Id is {IdGenerator.TransactionId(bankName, customerName)}";
                        account.TransactionList.Add(new Transaction(amount, IdGenerator.TransactionId(bankName,customerName),comment));
                    }
                }
            }
        }
        public void TransferFunds(string customerName, string customerPassword, string transferCustomerName, string transferCustomerBankName, double transferAmount)
        {
            string senderAccountId = null, receiverAccountId = null,senderName=null,receiverName=null,senderBankName=null,receiverBankName=null;
            foreach (var bankList in BankDataList.BankList)
            {
                var customerDetails = from customerProperties in bankList.Users where (customerProperties.Name == customerName && customerProperties.Password == customerPassword &&customerProperties.Type==UserTypes.Customer) select customerProperties;
                foreach (var sender in customerDetails)
                {
                    senderAccountId = sender.AccountId;
                    senderName = sender.Name;
                    senderBankName = sender.Bank;
                }
                foreach(var receiver in bankList.Users)
                {
                    if (receiver.Name == transferCustomerName && receiver.Bank == transferCustomerBankName)
                    {
                        receiverAccountId = receiver.AccountId;
                        receiverBankName= receiver.Bank;
                        receiverName= receiver.Name;
                    }
                }
                foreach(var account in bankList.Account)
                {
                    if (account.AccountId == senderAccountId)
                    {
                        string comment = $"Amount of {transferAmount} sent to {transferCustomerName} and the Transaction Id is {IdGenerator.TransactionId(receiverBankName, receiverName)}";
                        account.Balance -= transferAmount;
                        account.TransactionList.Add(new Transaction( transferAmount, IdGenerator.TransactionId(senderBankName, senderName),comment));
                        if (senderBankName == receiverBankName)
                        {
                            account.Balance -=(account.Balance)*(StaffServices.ChargeForSameAccount/100);
                        }
                        else
                        {
                            account.Balance -= (account.Balance) * (StaffServices.ChargeForDifferentAccount/100);
                        }
                    }
                    if (account.AccountId == receiverAccountId)
                    {
                        string comment = $"Amount of {transferAmount} recieved from {customerName} and the Transaction Id is {IdGenerator.TransactionId(senderBankName, senderName)}";
                        account.Balance += transferAmount;
                        account.TransactionList.Add(new Transaction(transferAmount, IdGenerator.TransactionId(receiverBankName, receiverName), comment));
                    }
                }
            }           
        }
        public List<Transaction> DisplayTransaction(string accountId)
        {
            List<Transaction> transactionList = new List<Transaction>();
            foreach(var bank in BankDataList.BankList)
            {
                foreach(var account in bank.Account)
                {
                    if (accountId == account.AccountId)
                    {
                        transactionList = account.TransactionList;
                    }
                }
            }
            return transactionList;
        }
        public bool CheckCredentials(string customerName, string customerPassword)
        {
            foreach(var bankList in BankDataList.BankList)
            {
                foreach (var customerList in bankList.Users)
                {
                    if (customerList.Name == customerName && customerList.Password == customerPassword&&customerList.Type==UserTypes.Customer)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}