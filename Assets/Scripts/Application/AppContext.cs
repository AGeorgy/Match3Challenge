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

        public void Register<T>(T instance)
        {
            registered.Add(typeof(T), instance);
        }

        public void Register<T1, T2>(object instance)
        {
            registered.Add(typeof(T1), instance);
            registered.Add(typeof(T2), instance);
        }

        public void Register<T1, T2, T3>(object instance)
        {
            Register<T1, T2>(instance);
            registered.Add(typeof(T3), instance);
        }

        public void Register<T1, T2, T3, T4>(object instance)
        {
            Register<T1, T2, T3>(instance);
            registered.Add(typeof(T4), instance);
        }

        public void Register<T1, T2, T3, T4, T5>(object instance)
        {
            Register<T1, T2, T3, T4>(instance);
            registered.Add(typeof(T5), instance);
        }
    }
}