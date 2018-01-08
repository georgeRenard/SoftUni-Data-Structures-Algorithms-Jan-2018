using System;

public class Program
{
    public static void Main(string[] args)
    {

        ArrayList<string> list = new ArrayList<string>();
        list.Add("5");
        list.Add("6");

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }


    }
}
