using System;
using System.Collections.Generic;

namespace Server
{
    public class WeightedRandom<T>
    {
        public List<(double Weight, T Value)> Entries { get; protected set; } = new ();
        public double AccumulatedWeight { get; protected set; }

        public void Add(double weight, T value)
        {
            AccumulatedWeight += weight;
            Entries.Add((AccumulatedWeight, value));
        }
        
        public T GetRandom()
        {
            var r = Utility.RandomDouble() * AccumulatedWeight;
            return Entries.Find(p => p.Weight >= r).Value;
        }
    }
}
