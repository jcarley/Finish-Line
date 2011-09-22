using System;
using System.Linq;
using System.Linq.Expressions;

namespace FinishLine.Library.Infrastructure.Domain
{
    public interface IRepository
    {
        TEntity Find<TEntity>(Guid id)
            where TEntity : IAggregateRoot;

        IQueryable<TEntity> Query<TEntity>()
            where TEntity : IAggregateRoot;

        IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> where)
            where TEntity : IAggregateRoot;

        // here for future use.
        //TEntity FindBy<TEntity, U>(Expression<Func<TEntity, U>> expression, U search)
        //    where TEntity : DomainEntity;

        TEntity FindBy<TEntity>(Expression<Func<TEntity, bool>> where)
            where TEntity : IAggregateRoot;

        void Delete<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot;

        void Update<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot;

        void Insert<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot;

        void RejectChanges<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot;

        TEntity[] GetAll<TEntity>()
            where TEntity : IAggregateRoot;
    }
}
