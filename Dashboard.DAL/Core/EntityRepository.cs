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
            IQueryable<T> query = _entityContext.Set<T>();
            foreach(var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query;
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
            return _entityContext.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _entityContext.Set<T>();
        }

        public T GetSingle(Guid key)
        {
            return _entityContext.Set<T>().Find(key);
        }

        public void Save()
        {
            _entityContext.SaveChanges();
        }
    }
}
