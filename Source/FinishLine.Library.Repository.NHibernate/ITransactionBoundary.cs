using System;
using NHibernate;

namespace FinishLine.Library.Repository.NHibernate
{
    public interface ITransactionBoundary : IDisposable
    {
        ISession Session { get; }
        bool IsDisposed { get; }
        void Start();
        void Commit();
        void Rollback();
    }

}