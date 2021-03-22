using System;
using System.Collections.Generic;

namespace Server
{
    public class WeightedRandomType<T>
    {
        private readonly List<(double weight, Type t)> m_Entries = new ();
        private double m_AccumulatedWeight;        

        public void Add<TU>(double weight) where TU: T
        {
            m_AccumulatedWeight += weight;
            m_Entries.Add((m_AccumulatedWeight, typeof(TU)));
        }
        
        public T GetRandom()
        {
            var r = Utility.RandomDouble() * m_AccumulatedWeight;
            return (T)Activator.CreateInstance(m_Entries.Find(p => p.weight >= r).t);
        }
    }
}
