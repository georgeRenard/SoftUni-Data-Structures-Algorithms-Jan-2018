using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main(string[] args)
    {

        Random random = new Random();
        Computer computer = new Computer(100);
        List<Invader> expected = new List<Invader>();

        for (int i = 1; i <= 100; i++)
        {
            var invader = new Invader(random.Next(50), random.Next(50));
            computer.AddInvader(invader);
            expected.Add(invader);
        }

        computer.DestroyHighestPriorityTargets(50);

        var toRemove = expected.OrderBy(x => x.Distance).ThenBy(x => -x.Damage).Take(50).ToList();
        expected.RemoveAll(x => toRemove.Contains(x));

        
    }
}
