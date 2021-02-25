using System;
using System.Linq;
using System.Reflection;

namespace Server
{
    public abstract class BaseSingleton<T>
    {
        private static readonly Lazy<T> LazyInstance = new(InstanceCreator);

        // ReSharper disable once StaticMemberInGenericType
        private static int _instanceCount;

        /// <summary>
        /// </summary>
        protected BaseSingleton()
        {
            if (++_instanceCount > 1)
            {
                throw new Exception($"{nameof(T)} is Singleton but it has {_instanceCount} instances");
            }
        }

        public static T Instance => LazyInstance.Value;

        private static T InstanceCreator()
        {
            var constructors =
                typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (constructors.Any(c => c.IsPublic || c.GetParameters().Length > 0))
            {
                throw new ApplicationException(
                    $"{typeof(T)} has an invalid singleton constructor, it should be non-public and without parameters");
            }

            return (T) Activator.CreateInstance(typeof(T), true);
        }
    }
}