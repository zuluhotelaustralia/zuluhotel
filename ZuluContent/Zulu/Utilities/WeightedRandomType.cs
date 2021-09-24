using System;
using System.Collections.Generic;

namespace Server
{
    public class WeightedRandomType<T> : WeightedRandom<Type>
    {
        public void Add<TU>(double weight) where TU: T => base.Add(weight, typeof(TU));

        public T GetRandomInstance() => (T)Activator.CreateInstance(GetRandom());
    }
}
