using System;
using System.Collections.Generic;

namespace Tactile.TactileMatch3Challenge.Application
{
    public class AppContext : IAppContext
    {
        private readonly Dictionary<Type, object> registered = new();

        public T Resolve<T>()
        {
            return (T)registered[typeof(T)];
        }

        private void Register<T>(T instance)
        {
            registered.Add(typeof(T), instance);
        }

        private void Register<T1, T2>(object instance)
        {
            registered.Add(typeof(T1), instance);
            registered.Add(typeof(T2), instance);
        }
    }
}