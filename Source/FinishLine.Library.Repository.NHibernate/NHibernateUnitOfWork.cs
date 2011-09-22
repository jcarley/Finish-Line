using FinishLine.Library.Infrastructure.UnitOfWork;

namespace FinishLine.Library.Repository.NHibernate
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private bool _success = false;
        private ITransactionBoundary _transactionBoundary = null;

        public NHibernateUnitOfWork(ITransactionBoundary transactionBoundary)
        {
            _transactionBoundary = transactionBoundary;

            _transactionBoundary.Start();
        }

        public void Commit()
        {
            _transactionBoundary.Commit();

            _success = true;
        }

        public void Rollback()
        {
            if (_success)
                return;

            _transactionBoundary.Rollback();
        }

        public void Dispose()
        {
            Rollback();
        }
    }
}
