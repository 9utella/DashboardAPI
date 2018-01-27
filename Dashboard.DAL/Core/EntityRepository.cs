using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Core
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new ()
    {
        readonly DbContext _entityContext;

        public EntityRepository(EntityContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException("_entityContext is null.");
            }
            _entityContext = context;
        }

        public void Add(T entity)
        {
            _entityContext.Set<T>().Add(entity);
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = _entityContext.Entry<T>(entity);
            entry.State = EntityState.Deleted;
        }

        public void Edit(T entity)
        {
            DbEntityEntry entry = _entityContext.Entry<T>(entity);
            entry.State = EntityState.Modified;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetSingle(Guid key)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
