using Models;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml.Linq;

namespace BankApplicationServices
{
    public class CustomerServices
    {
        // Return type should be a boolean for methods starting with "Is" or "Has"
        public string IsCustomerRegistered(string customerName,string customerPassword)
        {
            foreach(var bank in BankDataList.BankList)
            {
                var BankUsers = bank.Users.Where(user => user.Name == customerName && user.Password == customerPassword && user.Type == UserTypes.Customer).ToList();
                foreach (var users in BankUsers)
                {
                   return users.AccountId;
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
                    if(customer.AccountId.Equals(accountId))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void DepositMoney(string customerName, string customerPassword, double amount, string currency)
        {
            string accountId="",bankName="";
            foreach (var bankList in BankDataList.BankList)
            {
                var BankUsers = bankList.Users.Where(user => user.Name == customerName && user.Password == customerPassword && user.Type == UserTypes.Customer).ToList();
                foreach (var customerList in BankUsers)
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
                        if (bankList.DefaultCurrency.ToLower().Equals("rupees") && currency.ToLower().Equals("dollar"))
                        {
                            account.Balance += (81*amount);
                            amount *= 81;
                        }
                        else if (bankList.DefaultCurrency.ToLower().Equals("dollar") && currency.ToLower().Equals("rupees"))
                        {
                            account.Balance += (0.012 * amount);
                            amount *= 0.012;
                        }
                        else
                        {
                            account.Balance += amount;
                        }
                        string comment = $"Amount of {amount} {bankList.DefaultCurrency} is Deposited to the account and the Transaction Id is {IdGenerator.TransactionId(bankName, customerName)}";
                        account.TransactionList.Add(new Transaction(amount,IdGenerator.TransactionId(bankName,customerName),comment));
                    }
                }
            }   
        }
    
        public void WithDrawMoney(string customerName, string customerPassword, double amount)
        {
            string accountId = "",bankName="";
            foreach (var bankList in BankDataList.BankList)
            {
                var BankUsers = bankList.Users.Where(user => user.Name == customerName && user.Password == customerPassword && user.Type == UserTypes.Customer).ToList();
                foreach (var customerList in BankUsers)
                {
                    if (customerList.Name.Equals(customerName) && customerList.Password.Equals(customerPassword) && customerList.Type.Equals(UserTypes.Customer))
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
                        string comment = $"Amount of {amount} {bankList.DefaultCurrency} is WithDrawed from the Account and the Transaction Id is {IdGenerator.TransactionId(bankName, customerName)}";
                        account.TransactionList.Add(new Transaction(amount, IdGenerator.TransactionId(bankName,customerName),comment));
                    }
                }
            }
        }
        public void TransferFunds(string customerName, string customerPassword, string transferCustomerName, string transferCustomerBankName, double transferAmount)
        {
            string senderAccountId = "", receiverAccountId = "",senderName="",receiverName="",senderBankName="",receiverBankName="";
            foreach (var bankList in BankDataList.BankList)
            {
                var customerDetails = from customerProperties in bankList.Users where (customerProperties.Name.Equals(customerName) && customerProperties.Password.Equals(customerPassword) && customerProperties.Type==UserTypes.Customer) select customerProperties;
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
                        receiverBankName = receiver.Bank;
                        receiverName = receiver.Name;
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
                var bankAccountId = bank.Account.Where(account => account.AccountId == accountId).ToList();
                foreach(var account in bankAccountId)
                {
                   transactionList = account.TransactionList;  
                }
            }
            return transactionList;
        }
        public bool CheckCredentials(string customerName, string customerPassword)
        {
            foreach(var bankList in BankDataList.BankList)
            {
                var customerProperties = bankList.Users.Where(user => user.Name == customerName && user.Password == customerPassword && user.Type == UserTypes.Customer).ToList();
                foreach (var customerList in customerProperties)
                {
                    return true;
                }
            }
            return false;
        }

    }
}