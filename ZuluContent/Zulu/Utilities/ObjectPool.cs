using System;
using System.Collections.Concurrent;

namespace ZuluContent.Zulu.Utilities
{
    public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> m_Objects;
        private readonly Func<T> m_ObjectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            m_ObjectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
            m_Objects = new ConcurrentBag<T>();
        }

        public T Get() => m_Objects.TryTake(out T item) ? item : m_ObjectGenerator();

        public void Return(T item) => m_Objects.Add(item);
    }
}