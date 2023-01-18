using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankApplicationServices
{
    public static class IdGenerator
    {
        public static string CurrentDate()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("ddMMyyyy");
        }
        public static string BankIdPattern(string Name)
        {
            return Name.Substring(0, 3) + CurrentDate();
        }
        public static string AccountIdPattern(string Name)
        {
            return Name.Substring(0, 3) + CurrentDate();
        }
        public static string TransactionId(string bankname, string name)
        {
            return bankname.Substring(0, 3) + name.Substring(0, 3) + CurrentDate();
        }
    }
}
