using System;
using System.Collections.Generic;

namespace hStates
{
    public class ServiceLocator
    {
        // map that contains pairs of interfaces and
        // references to concrete implementations
        private readonly IDictionary<object, object> _services;

        public ServiceLocator(IDictionary<object, object> services)
        {
            _services = services;
        }

        public object GetServiceByString(string s)
        {
            try
            {
                return _services[s];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }

        public T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }
    }
}