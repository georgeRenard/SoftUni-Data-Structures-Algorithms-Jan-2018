using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{
    private LinkedList<Invader> byInsertion = new LinkedList<Invader>();
    private Dictionary<int, List<LinkedListNode<Invader>>> byDistance = 
        new Dictionary<int, List<LinkedListNode<Invader>>>();
    private int steps = 0;
    private int energy;

    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }
        this.Energy = energy;
    }

    public int Energy
    {
        get
        {
            if (this.energy < 0) return 0;
            return this.energy;
        } 
        private set { this.energy = value; }
    }

    public void Skip(int turns)
    {
        steps += turns;

        this.byDistance = this.byDistance.Where((x) =>
        {

            int remDistance = x.Key - this.steps;
            
            if (remDistance <= 0)
            {
                this.Energy -= x.Value.Sum(y => y.Value.Damage);
                x.Value.ForEach(y => this.byInsertion.Remove(y));
            }

            return remDistance > 0;
        }).ToDictionary(x => x.Key, y => y.Value);

    }

    public void AddInvader(Invader invader)
    {

        LinkedListNode<Invader> node = 
            new LinkedListNode<Invader>(invader);
        if (!this.byDistance.ContainsKey(invader.Distance))
        {
            this.byDistance.Add(invader.Distance, new List<LinkedListNode<Invader>>());
        }
        this.byInsertion.AddLast(node);
        this.byDistance[invader.Distance].Add(node);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        foreach (var linkedListNode in this.byDistance.SelectMany(x => x.Value)
            .OrderBy(x => x.Value)
            .Take(count))
        {
            this.byInsertion.Remove(linkedListNode);
        }
        var newDict = this.byDistance.SelectMany(x => x.Value)
            .OrderBy(x => x.Value)
            .Skip(count);

        this.byDistance = new Dictionary<int, List<LinkedListNode<Invader>>>();
        foreach (var item in newDict)
        {
            if (!this.byDistance.ContainsKey(item.Value.Distance))
            {
                this.byDistance.Add(item.Value.Distance, new List<LinkedListNode<Invader>>());
            }
            this.byDistance[item.Value.Distance].Add(item);
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        this.byDistance = this.byDistance
            .Where(x =>
            {
                bool result = x.Key - this.steps > radius;

                if (!result)
                {
                    x.Value.ForEach(y => this.byInsertion.Remove(y));
                }
                return result;
            })
            .ToDictionary(x => x.Key, y => y.Value);
    }

    public IEnumerable<Invader> Invaders()
    {
        return this.byInsertion;
    }
}
