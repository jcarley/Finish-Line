using NHibernate;

namespace FinishLine.Library.Repository.NHibernate
{
    public interface ISessionSource
    {
        ISession CreateSession();
        ISession CreateSession(IInterceptor interceptor);
        void BuildSchema();
    }

}
