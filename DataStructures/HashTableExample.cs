using System;
using Hash_Table;

class Example
{
    static void Main()
    {
        HashSet<int> setA = new HashSet<int>();

        setA.Add(5);
        setA.Add(10);
        setA.Add(1);
        setA.Add(2);

        HashSet<int> setB = new HashSet<int>();
        setB.Add(1);
        setB.Add(100);
        setB.Add(2);
        setB.Add(1000);
        setB.Add(23);

        HashSet<int> unionSet = setA.UnionWith(setB);
        Console.WriteLine(string.Join(" ", unionSet));

        HashSet<int> intersectionSet = setA.IntersectWith(setB);
        Console.WriteLine(string.Join(" ", intersectionSet));

        HashSet<int> exceptedSet = setA.Except(setB);
        Console.WriteLine(string.Join(" ", exceptedSet));

        HashSet<int> symmetricExceptedSet = setA.SymmetricExcept(setB);
        Console.WriteLine(string.Join(" ", symmetricExceptedSet));

        //HashTable<string, int> grades = new HashTable<string, int>();
        //
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");
        //
        //grades.Add("Peter", 3);
        //grades.Add("Maria", 6);
        //grades["George"] = 5;
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");
        //
        //grades.AddOrReplace("Peter", 33);
        //grades.AddOrReplace("Tanya", 4);
        //grades["George"] = 55;
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");
        //
        //Console.WriteLine("Keys: " + string.Join(", ", grades.Keys));
        //Console.WriteLine("Values: " + string.Join(", ", grades.Values));
        //Console.WriteLine("Count = " + string.Join(", ", grades.Count));
        //Console.WriteLine("--------------------");
        //
        //grades.Remove("Peter");
        //grades.Remove("George");
        //grades.Remove("George");
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");
        //
        //Console.WriteLine("ContainsKey[\"Tanya\"] = " + grades.ContainsKey("Tanya"));
        //Console.WriteLine("ContainsKey[\"George\"] = " + grades.ContainsKey("George"));
        //Console.WriteLine("Grades[\"Tanya\"] = " + grades["Tanya"]);
        //Console.WriteLine("--------------------");
    }
}
