using Example;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Formats.Asn1;
using System.Text;
using System;
using System.Drawing;

class program
{
    class Stack<T>
    {
        private List<T> stack = null;
        public static int size = 0;
        public Stack()
        {
            stack = new List<T>();
        }
        public void Push(T item)
        {
            stack.Add(item);
            size++;
        }
        public void Pop()
        {
            if (stack.Count > 0)
            {
                stack.RemoveAt((size-1));
                size--;
            }
        }
        public string Display()
        {
            StringBuilder sb=new StringBuilder();
            foreach (T item in stack)
            {
                sb.Append(item);
                sb.Append(',');
            }
            return sb.ToString().TrimEnd(',');
        }

    }
    public static void Main(string[] args)
    {
        int[] arr = new int[] {4,3,5,1,2};
        //BubbleSort(ref arr, arr.Count());
        //StringBuilder sb=new StringBuilder();
        //foreach(int i in arr)
        //{
        //  sb.Append(i);
        // sb.Append(",");
        //}
        //sb.ToString().TrimEnd(',');
        //Console.WriteLine(sb.ToString().TrimEnd(','));
        Stack<int> s = new Stack<int>();
        s.Push(1);
        s.Push(2);
        s.Push(3);
        s.Push(4);
        s.Pop();
        s.Pop();
        Console.WriteLine(s.Display());
        


    }
}
