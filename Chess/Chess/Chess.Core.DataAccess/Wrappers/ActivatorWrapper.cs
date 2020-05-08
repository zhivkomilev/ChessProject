using System;

namespace Chess.Core.DataAccess.Wrappers
{
    public class ActivatorWrapper : IActivatorWrapper
    {
        public T CreateInstance<T>(params object[] @params)
        {
            return (T)Activator.CreateInstance(typeof(T), @params);
        }
    }
}
