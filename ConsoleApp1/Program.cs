using System.ComponentModel;
using System.Text.Json;

class Dict
{
    public string a1;

    public Dict(string a)
    {
        this.a1 = a;
    }

}
class Program
{
    List<Dict> dicts = new List<Dict>();
    Dictionary<int, List<Dict>> keyValuePairs = new Dictionary<int, List<Dict>>();
    public void Add(int key, Dict dict)
    {
        dicts.Add(dict);
        if (!keyValuePairs.ContainsKey(key))
        {
            keyValuePairs.Add(key, dicts);
        }
        keyValuePairs[key]=dicts;
    }
    public void Display()
    {
        foreach (Dict dict in dicts)
        {
            Console.WriteLine(dict.a1);
        }
    }
    public void Print()
    {
        foreach(var item in keyValuePairs)
        {
            Console.WriteLine(item.Value[2].a1);
        }
        
    }
    public static void Main(string[] args)
    {
        Program a = new Program();
        a.Add(1, new Dict("Hello"));
        a.Add(1, new Dict("hi there"));
        a.Add(2, new Dict("siri"));        
        a.Add(2, new Dict("pratham"));
        a.Print();
    }
}

