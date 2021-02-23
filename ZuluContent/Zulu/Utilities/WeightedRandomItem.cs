using System;
using System.Collections.Generic;

namespace Server
{
    public class WeightedRandomType <U>
    {
        private List<(double weight, Type t)> m_Entries = new List<(double weight, Type t)>();
        private double m_AccumulatedWeight;        

        public void AddEntry<T>(double weight) where T: U
        {
            m_AccumulatedWeight += weight;
            m_Entries.Add((m_AccumulatedWeight, typeof(T)));
        }

        public U GetRandom()
        {
            double r = Utility.RandomDouble() * m_AccumulatedWeight;

            return (U)Activator.CreateInstance(m_Entries.Find(p => p.weight >= r).t);
        }
    }
}
