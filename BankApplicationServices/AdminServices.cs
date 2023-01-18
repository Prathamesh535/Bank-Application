﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace BankApplicationServices
{
    public class AdminServices
    {
        public static string DefaultCurrency="";
        public bool AdminLogin(string adminName,string adminPassword)
        {
            if (adminName == "Admin" && adminPassword == "Admin@123")
            {
                return true;
            }
            return false;
        }
        public void AddStaff(string staffName,string staffPassword)
        {
            foreach (var bankList in BankDataList.BankList)
            {
                bankList.Users.Add(new Staff(staffName, staffPassword));
            }
        }
        public Staff DisplayStaff(string staffName,string staffPassword)
        {
            Staff staffInformation = new Staff();
            foreach(var bankList in BankDataList.BankList)
            {
                foreach (var staffList in bankList.Users)
                {
                    if (staffList.Name == staffName && staffList.Password == staffPassword && staffList.Type==UserTypes.Staff)
                    {
                        staffInformation.Name = staffName;
                        staffInformation.Password = staffPassword;
                        break;
                    }
                }
                
            }
            return staffInformation;
        }
        public void AddBankAccount(string bankName, string bankLocation, string ifscCode)
        {
            BankDataList.BankList.Add(new Bank(bankName, bankLocation, ifscCode, IdGenerator.BankIdPattern(bankName)));
        }
        public void AddDefaultCurrency(string currency)
        {
            if (currency == "")
            {
                DefaultCurrency = "Default Currency is not set";
            }
            else
            {
                DefaultCurrency = currency;
            }
        }
    }
}