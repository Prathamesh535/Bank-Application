using BankApplicationServices;
using Models;
using System.Diagnostics;
using System.Security.Principal;
using System.Transactions;
namespace Program
{
    public class Program
    {
        StaffServices StaffServices = new StaffServices();
        CustomerServices CustomerServices = new CustomerServices();
        AdminServices AdminServices = new AdminServices();
        public static void Main(string[] args)
        {
            AdminServices AdminServices = new AdminServices();
            Program Bank= new Program();
            bool exitFromBankApplicationOption = false;
            while (!exitFromBankApplicationOption)
            {
                Console.WriteLine("Bank Application Services");
                Console.WriteLine("1.Admin");
                Console.WriteLine("2.Staff");
                Console.WriteLine("3.Customer");
                Console.WriteLine("4.Exit");
                Console.Write("Select Option: ");
                int bankApplicationOption = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("------------------------------------");
                if (bankApplicationOption == 1)
                {
                    Console.Write("Enter the Name of the Admin: ");
                    string adminName = Console.ReadLine();
                    Console.Write("Enter the Password of the Admin: ");
                    string adminPassword = Console.ReadLine();
                    if (!AdminServices.AdminLogin(adminName, adminPassword))
                    {
                        Console.WriteLine("Wrong Credentials! Please Try Again.");
                    }
                    else
                    {
                        Bank.Admin();
                    }
                }
                else if (bankApplicationOption == 2)
                {
                    bool staffCredentials = false;
                    Console.Write("Enter the Name of the Staff: ");
                    string staffName = Console.ReadLine();
                    Console.Write("Enter the Password of the Staff: ");
                    string staffPassword = Console.ReadLine();
                    foreach(var bankList in BankDataList.BankList)
                    {
                        foreach(var staff in bankList.Users)
                        {
                            if (staff.Name == staffName && staff.Password == staffPassword&&staff.Type==UserTypes.Staff)
                            {
                                Bank.Staff();
                                staffCredentials = true;
                                break;
                            }
                        }
                    }
                    if (!staffCredentials)
                    {
                        Console.WriteLine("Staff Does Not Exists! Create a Staff");
                        Console.WriteLine("------------------------------------");
                    }
                }
                else if (bankApplicationOption == 3)
                {
                    Bank.Customer();                  
                }
                else
                {
                    Console.WriteLine("Exit");
                    exitFromBankApplicationOption = true;
                }

            }    
        }
        
        public void Admin() {
            bool exitFromTheAdmin = false;
            while (!exitFromTheAdmin)
            {
                Console.WriteLine("Admin Services");
                Console.WriteLine("1.Add Bank");
                Console.WriteLine("2.Create Staff");
                Console.WriteLine("3.Display Staff");
                Console.WriteLine("4.Add Default Currency");
                Console.WriteLine("5.Exit");
                Console.Write("Select Option: ");
                int adminOption = Convert.ToInt32(Console.ReadLine());
                switch (adminOption)
                {
                    case 1:
                        Console.Write("Enter the Name of the Bank: ");
                        string bankName = Console.ReadLine();
                        Console.Write("Enter the Location of the Bank: ");
                        string bankLocation = Console.ReadLine();
                        Console.Write("Enter the IFSC code of the Bank: ");
                        string ifscCode = Console.ReadLine();
                        AdminServices.AddBankAccount(bankName, bankLocation, ifscCode);
                        break;
                    case 2:
                        Console.Write("Enter the Name of the Staff: ");
                        string staffName = Console.ReadLine();
                        Console.Write("Enter the Password of the Staff: ");
                        string staffPassword = Console.ReadLine();
                        if (BankDataList.BankList.Count == 0)
                        {
                            Console.WriteLine("Please Create the Bank First");
                            Console.WriteLine("------------------------------------");
                        }
                        else
                        {
                            AdminServices.AddStaff(staffName, staffPassword);
                        }
                        break;
                    case 3:
                        Console.Write("Enter the Name of the Staff: ");
                        staffName = Console.ReadLine();
                        Console.Write("Enter the Password of the Staff: ");
                        staffPassword = Console.ReadLine();
                        var staffInformation = AdminServices.DisplayStaff(staffName, staffPassword);
                        if (staffInformation.Name != null && staffInformation.Password != null)
                        {
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("The Name of the Staff: " + staffInformation.Name);
                            Console.WriteLine("The Password of the Staff: " + staffInformation.Password);
                            Console.WriteLine("------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Staff Does Not Exists! Please Try Again");
                            Console.WriteLine("------------------------------------");
                        }
                        break;
                    case 4:
                        Console.Write("Enter the Default Currency: ");
                        string currency= Console.ReadLine();
                        AdminServices.AddDefaultCurrency(currency);
                        break;
                    default:
                        exitFromTheAdmin = true;
                        break;
                }
            }   
        }
        public void Staff()
        {
            bool exitFromStaffOption = false;
            while (!exitFromStaffOption)
            {
                Console.WriteLine("Staff Services");
                Console.WriteLine("1.Create Bank Account");
                Console.WriteLine("2.Update Bank Account Details");
                Console.WriteLine("3.Display Bank Account Details");
                Console.WriteLine("4.View Account Transaction History");
                Console.WriteLine("5.Add Charges for the Same Bank Accont");
                Console.WriteLine("6.Add Charges for the Different Bank Account");
                Console.WriteLine("7.Revert Transaction");
                Console.WriteLine("8.Exit");
                Console.Write("Select Option: ");
                int staffOption = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("------------------------------------");
                switch (staffOption)
                {
                    case 1:
                        Console.WriteLine("------------------------------------");
                        Console.Write("Enter the Name of the Customer: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter the Password of the Customer: ");
                        string password = Console.ReadLine();
                        Console.Write("Enter the City of the Customer: ");
                        string city = Console.ReadLine();
                        Console.Write("Enter the Bank of the Customer: ");
                        string bank = Console.ReadLine();
                        bool bankNameTrue = false;
                        while(!bankNameTrue)
                        {
                            foreach (var bankList in BankDataList.BankList)
                            {
                                if (bankList.BankName == bank)
                                {
                                    bankNameTrue = true;
                                    StaffServices.CreateBankAccount(name, password, city, bank);
                                    break;
                                }
                            }
                            if (!bankNameTrue)
                            {
                                Console.WriteLine("No Such Bank Account Exists! Add a Bank First");
                                Console.WriteLine("1.Exit");
                                Console.WriteLine("2.Retry");
                                int key = Convert.ToInt32(Console.ReadLine());
                                if (key == 1)
                                {
                                    bankNameTrue = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        Console.WriteLine("------------------------------------");
                        break;
                    case 2:
                        Console.Write("Enter the Name: ");
                        name = Console.ReadLine(); 
                        Console.Write("Enter the Password: ");
                        password = Console.ReadLine();
                        bool updateTheList = false;
                        int customerIndex = 0;
                        string newName=null, newPassword=null;
                        foreach (var bankList in BankDataList.BankList)
                        {
                                foreach (var customerList in bankList.Users)
                                {
                                    if (customerList.Name == name && customerList.Password == password &&customerList.Type==UserTypes.Customer)
                                    {
                                        updateTheList = true;
                                        break;
                                    }
                                    customerIndex++;
                                }
                            
                        }
                        if (!updateTheList)
                        {
                            Console.WriteLine("Match Not Found! Enter the Correct Details");
                            Console.WriteLine("------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Select Option ");
                            Console.WriteLine("1.Update");
                            Console.WriteLine("2.Delete");
                            Console.WriteLine("3.Exit");
                            int select = Convert.ToInt32(Console.ReadLine());
                            switch (select)
                            {
                                case 1:
                                    Console.WriteLine("Match Found!");
                                    Console.WriteLine("------------------------------------");
                                    Console.Write("Enter the New Name: ");
                                    newName = Console.ReadLine();
                                    Console.Write("Enter the New Password: ");
                                    newPassword = Console.ReadLine();
                                    Console.WriteLine("Changes made Successfully");
                                    Console.WriteLine("------------------------------------");
                                    break;
                                case 2:
                                    Console.WriteLine("Customer Deleted Successfully");
                                    Console.WriteLine("------------------------------------");
                                    break;

                            }
                            StaffServices.UpdateDetails(name, password,customerIndex,newName,newPassword,select);
                        }
                        break;
                    case 3:
                        Console.Write("Enter the Name of the Customer: ");
                        string enterName = Console.ReadLine();
                        Console.Write("Enter the Password of the Customer: ");
                        string enterPassword = Console.ReadLine();
                        string accountId = null;
                        foreach(var bankList in BankDataList.BankList)
                        {
                            foreach(var users in bankList.Users)
                            {
                                if (users.Name == enterName && users.Password == enterPassword && users.Type == UserTypes.Customer)
                                {
                                    Console.WriteLine("------------------------------------");
                                    Console.WriteLine($"Name of the Customer: {users.Name}\n" +
                                        $"Password of the Customer: {users.Password}\n" +
                                        $"City of the Customer: {users.City}\n" +
                                        $"Bank of the Customer: {users.Bank}");
                                    accountId = users.AccountId;
                                }
                            }
                            foreach(var account in bankList.Account)
                            {
                                if (accountId == account.AccountId)
                                {
                                    Console.WriteLine($"Bank Balance of the Customer: {account.Balance} +{AdminServices.DefaultCurrency}\n"+
                                        $"Account Id of the Customer: {account.AccountId}");
                                    Console.WriteLine("------------------------------------");
                                }
                            }
                        }
                        break;
                    case 4:
                        Console.Write("Enter the Name of the Customer: ");
                        enterName = Console.ReadLine();
                        Console.Write("Enter the Password of the Customer: ");
                        enterPassword = Console.ReadLine();
                        foreach (var bankList in BankDataList.BankList)
                        {
                            foreach (var customer in bankList.Users)
                            {
                                if (enterName == customer.Name && enterPassword == customer.Password && customer.Type == UserTypes.Customer)
                                {
                                    Console.WriteLine("------------------------------------");
                                    foreach (var transactionprint in StaffServices.DisplayTransaction(customer.AccountId))
                                    {
                                        Console.WriteLine(transactionprint.TransactionComment);

                                    }
                                    Console.WriteLine("------------------------------------");
                                }
                            }
                        }
                        
                        break;
                    case 5:
                        //Add charges for the same account
                        Console.WriteLine("------------------------------------");
                        Console.Write("Enter the Charge for Same Bank Account: ");
                        double chargeForSameAccount=Convert.ToDouble(Console.ReadLine());
                        StaffServices.AddChargesForSameAccount(chargeForSameAccount);
                        break;
                    case 6:
                        //Add charges for the different account
                        Console.WriteLine("------------------------------------");
                        Console.Write("Enter the Charge for Different Bank Account: ");
                        double chargeForDifferentAccount = Convert.ToDouble(Console.ReadLine());
                        StaffServices.AddChargesForDifferentAccount(chargeForDifferentAccount);
                        break;
                    case 7:
                        //Revert the following transaction
                        Console.Write("Enter the Sender Name: ");
                        string senderName=Console.ReadLine();
                        Console.Write("Enter the Receiver Name: ");
                        string receiverName = Console.ReadLine();
                        StaffServices.RevertTransaction(senderName,receiverName);
                        Console.WriteLine("Last Transaction Reverted Successfully");
                        Console.WriteLine("------------------------------------");
                        break;
                    default:
                        //Exit from the staff options
                        exitFromStaffOption = true;
                        break;
                }
            }
        }
        public void Customer()
        {
            bool exitFromCustomerOption = false;
            bool correctCredentials = false;
            string customerName = null, customerPassword = null;
            while (!correctCredentials)
            {
                Console.Write("Enter your Name: ");
                customerName = Console.ReadLine();
                Console.Write("Enter your Password: ");
                customerPassword = Console.ReadLine();
                Console.WriteLine("------------------------------------");
                if (CustomerServices.CheckCredentials(customerName, customerPassword))
                {
                    correctCredentials = true;
                }
                else
                {
                    Console.WriteLine("Wrong Credentials! Retry Again");
                    Console.WriteLine("------------------------------------");
                    break;
                }
            }
            if (CustomerServices.CheckCredentials(customerName, customerPassword))
            {
                while (!exitFromCustomerOption)
                {
                    Console.WriteLine("Select Option: ");
                    Console.WriteLine("1.Deposit Amount");
                    Console.WriteLine("2.WithDraw Amount");
                    Console.WriteLine("3.Transfer Funds");
                    Console.WriteLine("4.View Transaction History");
                    Console.WriteLine("5.Exit");
                    Console.WriteLine("------------------------------------");
                    int selectOption = Convert.ToInt32(Console.ReadLine());
                    switch (selectOption)
                    {
                        case 1:
                            Console.Write("Enter the Amount: ");
                            double amount = Convert.ToInt64(Console.ReadLine());
                            if(AdminServices.DefaultCurrency == "Default Currency is not set")
                            {
                                Console.WriteLine("Default Currency is not set by Admin");
                            }
                            else
                            {
                                CustomerServices.DepositMoney(customerName, customerPassword, amount);
                                Console.WriteLine("Amount Deposited Successfully!");
                                Console.WriteLine("------------------------------------");
                            }
                            break;
                        case 2:
                            Console.Write("Enter the Amount: ");
                            amount = Convert.ToInt64(Console.ReadLine());
                            foreach(var bank in BankDataList.BankList)
                            {
                                foreach(var account in bank.Account)
                                {
                                    if (CustomerServices.IsCustomerRegistered(customerName, customerPassword)==account.AccountId)
                                    {
                                        if (account.Balance >= amount)
                                        {
                                            CustomerServices.WithDrawMoney(customerName, customerPassword, amount);
                                            Console.WriteLine("Amount WithDrawed Successfully!");
                                            Console.WriteLine("------------------------------------");
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
                            }
                            break;
                        case 3:
                            Console.Write("Enter the Name of the Customer: ");
                            string transferCustomerName = Console.ReadLine();
                            Console.Write("Enter the Bank Name of the Customer: ");
                            string transferCustomerBankName = Console.ReadLine();
                            Console.Write("Enter the Amount to Transfer: ");
                            double transferAmount = Convert.ToInt64(Console.ReadLine());
                            bool transferDetails = false, greaterAmount = false ;
                            string transferId=null;
                            foreach (var bankList in BankDataList.BankList)
                            {
                                foreach (var customerList in bankList.Users)
                                {
                                    if (customerList.Name == transferCustomerName && customerList.Bank == transferCustomerBankName)
                                    {
                                        transferDetails = true;
                                    }
                                    if (customerList.Name == customerName && customerList.Password == customerPassword && customerList.Type==UserTypes.Customer)
                                    {
                                        transferId = customerList.AccountId;
                                    }
                                }
                                foreach(var account in bankList.Account)
                                {
                                    if (transferId == account.AccountId)
                                    {
                                        if(account.Balance>=transferAmount)
                                        {
                                            greaterAmount= true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!transferDetails)
                            {
                                Console.WriteLine("Wrong Credentials! Please try again.");
                                Console.WriteLine("------------------------------------");
                            }
                            else if (!greaterAmount)
                            {
                                Console.WriteLine("Please Increase the Credit Limit");
                                Console.WriteLine("------------------------------------");
                            }
                            else
                            {
                                CustomerServices.TransferFunds(customerName, customerPassword, transferCustomerName, transferCustomerBankName,transferAmount);
                                Console.WriteLine("Funds Transfered Successfully");
                                Console.WriteLine("------------------------------------");
                            }
                            break;
                        case 4:
                            foreach (var bank in BankDataList.BankList)
                            {
                                foreach(var customer in bank.Users)
                                {
                                    if (customerName == customer.Name && customerPassword == customer.Password && customer.Type == UserTypes.Customer)
                                    {
                                        Console.WriteLine("------------------------------------");
                                        foreach (var transactionprint in CustomerServices.DisplayTransaction(customer.AccountId))
                                        {
                                            Console.WriteLine(transactionprint.TransactionComment);

                                        }
                                        Console.WriteLine("------------------------------------");
                                    }
                                }
                            } 
                            
                            break;
                        case 5:
                            exitFromCustomerOption = true;
                            break;
                    }
                }
            }
        }

    }
    
}