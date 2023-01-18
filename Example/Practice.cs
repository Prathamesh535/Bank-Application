using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    abstract class example
    {
        public abstract void show();//abstract method should not contain a body
        public virtual void alpha()
        {
            //non-abstract methods should contain a body
        }
        
    }
    class Shape : example
    {
        public override void show()
        {

        }
        public override void alpha()
        {

        }
    }
    internal class Practice
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Practice class");
            
        }
    }
}
