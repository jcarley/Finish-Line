using System;
using NHibernate;

namespace FinishLine.Library.Repository.NHibernate
{
    public class NHibernateTransactionBoundary : ITransactionBoundary
    {
        private readonly ISessionSource _sessionSource;
        private bool _isInitialized;
        private ISession _session;
        private ITransaction _transaction;

        public NHibernateTransactionBoundary(ISessionSource sessionSource)
        {
            _sessionSource = sessionSource;
        }

        public ISession Session
        {
            get
            {
                EnsureInitialized();
                return _session;
            }
        }

        public bool IsDisposed { get; private set; }

        public void Start()
        {
            _session.FlushMode = FlushMode.Commit;
            _transaction = _session.BeginTransaction();
            _isInitialized = true;
        }

        public void Commit()
        {
            ShouldNotBeDisposed();
            EnsureInitialized();
            _transaction.Commit();
        }

        public void Rollback()
        {
            ShouldNotBeDisposed();
            EnsureInitialized();
            _transaction.Rollback();
            _transaction = _session.BeginTransaction();
        }

        public void Dispose()
        {
            IsDisposed = true;

            if (_transaction != null)
                _transaction.Dispose();

            if (_session != null)
                _session.Dispose();
        }

        private void ShouldNotBeDisposed()
        {
            if (!IsDisposed)
                return;

            throw new ObjectDisposedException("NHibernateTransactionBoundary");
        }

        private void EnsureInitialized()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException(
                    "An attempt was made to access the database session outside of a transaction. Please make sure all access is made within an initialized transaction boundary.");
            }
        }
    }
}
