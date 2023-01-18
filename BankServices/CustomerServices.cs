using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication;
namespace BankServices
{
    public class CustomerServices
    {
        Staff ListOfCustomer = new Staff();
        public void DepositMoney(string CustomerName, string CustomerPassword)
        {
            Console.Write("Enter the Amount: ");
            long amount = Convert.ToInt64(Console.ReadLine());
            foreach (var item in ListOfCustomer.list)
            {
                if (item.Name.Equals(CustomerName) && item.Password.Equals(CustomerPassword))
                {
                    item.DepositAmount += amount;
                }
            }
            Console.WriteLine("Amount Deposited Successfully!");
            Console.WriteLine("------------------------------------");
        }
        public void WithDrawMoney(string CustomerName, string CustomerPassword)
        {
            Console.Write("Enter the Amount: ");
            long amount = Convert.ToInt64(Console.ReadLine());
            bool Deposited = false;
            foreach (var item in ListOfCustomer.list)
            {
                if (item.Name.Equals(CustomerName) && item.Password.Equals(CustomerPassword))
                {
                    if (item.DepositAmount >= amount)
                    {
                        item.DepositAmount -= amount;
                        Deposited = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("No Sufficent Amount in the Bank");
                        Console.WriteLine("------------------------------------");
                        break;
                    }
                }
            }
            if (Deposited)
            {
                Console.WriteLine("Amount WithDrawed Successfully!");
                Console.WriteLine("------------------------------------");
            }
        }
        public void TransferFunds(string CustomerName, string CustomerPassword)
        {
            Console.Write("Enter the Name of the Customer: ");
            string TransferCustomerName = Console.ReadLine();
            Console.Write("Enter the Bank Name of the Customer: ");
            string TransferCustomerBankName = Console.ReadLine();
            bool CorrectCredentials = false;
            foreach (var item in ListOfCustomer.list)
            {
                if (item.Name == TransferCustomerName && item.Bank == TransferCustomerBankName)
                {
                    CorrectCredentials = true;
                    break;
                }
            }
            if (CorrectCredentials)
            {
                Console.Write("Enter the Amount to Transfer: ");
                long TransferAmount = Convert.ToInt64(Console.ReadLine());
                var linq = from i in ListOfCustomer.list where (i.Name == CustomerName && i.Password == CustomerPassword) select i;
                foreach (var item in linq)
                {
                    if (item.DepositAmount >= TransferAmount)
                    {
                        string sent = $"Sent Amount of {TransferAmount} to {TransferCustomerName} and the Transaction Id is {item.TransactionIdPattern()}";
                        item.AddTransactionId(sent);
                        foreach (var i in ListOfCustomer.list)
                        {
                            if (i.Name == TransferCustomerName && i.Bank == TransferCustomerBankName)
                            {
                                i.DepositAmount += TransferAmount;
                                string received = $"Received Amount of {TransferAmount} from {item.Name} and the Transaction Id is {i.TransactionIdPattern()}";
                                i.AddTransactionId(received);
                            }
                        }
                        item.DepositAmount -= TransferAmount;
                        if (item.Bank == TransferCustomerBankName)
                        {
                            double Point = Convert.ToDouble(item.DepositAmount * 0.95);
                            item.DepositAmount = (long)Point;
                        }
                        else
                        {
                            double Point = Convert.ToDouble(item.DepositAmount * 0.92);
                            item.DepositAmount = (long)Point;
                        }
                        Console.WriteLine("Amount Transfered Successfully!");
                        Console.WriteLine("------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("InSufficient Balance! Please Increase the Credit Limit.");
                        Console.WriteLine("------------------------------------");
                    }
                }
            }
            else
            {
                Console.WriteLine("Wrong Credentials! Enter Correct Credentials.");
                Console.WriteLine("------------------------------------");
            }

        }
        public void DisplayTransaction(string CustomerName, string CustomerPassword)
        {
            foreach (var item in ListOfCustomer.list)
            {
                if (item.Name == CustomerName && item.Password == CustomerPassword)
                {
                    item.DisplayTransactionHistory();
                }
            }
            Console.WriteLine("------------------------------------");
        }
    }
}
