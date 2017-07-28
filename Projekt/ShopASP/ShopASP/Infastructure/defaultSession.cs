using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShopASP.Infastructure
{
    public class defaultSession : ISessionFuncs
    {
        private HttpSessionState session;

        public defaultSession()
        {
            session = HttpContext.Current.Session;
        }

        public T Get<T>(string key)
        {
            return (T)session[key];
        }

        public T Get<T>(string key, Func<T> createDefault)
        {
            T retval;

            if (session[key] != null && session[key].GetType() == typeof(T))
            {
                retval = (T)session[key];
            }
            else
            {
                retval = createDefault();
                session[key] = retval;
            }

            return retval;
        }

        public void Set<T>(string name, T value)
        {
            session[name] = value;
        }

        public T Tryget<T>(string key)
        {
            try
            {
                return (T)session[key];
            }
            catch (NullReferenceException)
            {

                return default(T);
            }
        }

        public void Remove()
        {
            session.Abandon();
        }
    }
}