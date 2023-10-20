using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nerxeas.DataAccess.Repository.IRepository;

namespace Nerxeas.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // For example, _db.Categories == dbSet

            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
            // With this I can populate the fields with EFCore.
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            // 1. Here we create a Queryable item, so we can apply queries to this object
            //    (In this case, the dbSet (for example, _db.Categories <<)
            IQueryable<T> query = dbSet;

            // 2. Then, we apply the filters to the query and assign them back to the Queryable item.
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                // Usar ',' en vez de crear una char array funcionaría perfectamente, 
                // pero con la array es dinámico y escalable por si quiero añadir más separaciones.
                // Por ahora, tanto ',' como new char[] funcionan exactamente igual.
                {
                    query = query.Include(includeProp);
                }
            }

            // 3. Then return the Entity, because Get should return an TEntity (in this example, a Category)
            return query.FirstOrDefault();
        }

        // We need to recieve the includeProperties as a comma separated value, like Category,CoverType ...
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    // Usar ',' en vez de crear una char array funcionaría perfectamente, 
                    // pero con la array es dinámico y escalable por si quiero añadir más separaciones.
                    // Por ahora, tanto ',' como new char[] funcionan exactamente igual.
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
