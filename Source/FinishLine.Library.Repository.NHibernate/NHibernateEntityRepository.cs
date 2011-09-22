using System;
using System.Linq;
using System.Linq.Expressions;
using FinishLine.Library.Infrastructure.Domain;
using NHibernate;
using NHibernate.Linq;

namespace FinishLine.Library.Repository.NHibernate
{
    public class NHibernateEntityRepository : IRepository
    {
        private ISession _session = null;

        public NHibernateEntityRepository(ISession session)
        {
            _session = session;
        }

        public TEntity Find<TEntity>(Guid id)
            where TEntity : IAggregateRoot
        {
            return _session.Get<TEntity>(id);
        }

        public IQueryable<TEntity> Query<TEntity>()
            where TEntity : IAggregateRoot
        {
            return _session.Query<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> where)
            where TEntity : IAggregateRoot
        {
            return _session.Query<TEntity>().Where(where);
        }

        public TEntity FindBy<TEntity>(Expression<Func<TEntity, bool>> where)
            where TEntity : IAggregateRoot
        {
            return _session.Query<TEntity>().Where(where).FirstOrDefault();
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot
        {
            _session.Delete(entity);
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot
        {
            _session.Update(entity);
        }

        public void Insert<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot
        {
            _session.Save(entity);
        }

        public void RejectChanges<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot
        {
            _session.Evict(entity);
        }

        public TEntity[] GetAll<TEntity>()
            where TEntity : IAggregateRoot
        {
            return Query<TEntity>().ToArray();
        }
    }
}
