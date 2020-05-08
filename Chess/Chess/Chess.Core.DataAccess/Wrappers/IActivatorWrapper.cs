using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Core.DataAccess.Wrappers
{
    public interface IActivatorWrapper
    {
        public T CreateInstance<T>(params object[] @params);        
    }
}
