using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.Infastructure
{
    public interface ISessionFuncs
    {
        T Get<T>(string key);
        T Get<T>(string key, Func<T> createDefault);
        void Set<T>(string name, T value);
        T Tryget<T>(string key);
        void Remove();


    }
}