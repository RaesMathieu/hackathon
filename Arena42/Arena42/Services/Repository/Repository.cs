using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Arena42.Services.Repository
{
    public class Repository<T>
        where T : class
    {
        #region Ctor
        /// <summary>
        /// Initialize the database context and the collection of all entities
        /// </summary>
        /// <param name="context">Database context</param>
        public Repository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public virtual void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            var entry = _context.Entry(entity);
            _dbset.Attach(entity);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Get an entity by its identifier
        /// </summary>
        /// <param name="id">Identifier of the entity</param>
        /// <returns>An entity</returns>
        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// Get an entity by its identifier
        /// </summary>
        /// <param name="id">Identifier of the entity</param>
        /// <returns>An entity</returns>
        public virtual T GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// Get all the entities
        /// </summary>
        /// <returns>An list of entities</returns>
        public virtual IQueryable<T> All()
        {
            return _dbset;
        }

        /// <summary>
        /// Get all the entities
        /// </summary>
        /// <returns>An list of entities</returns>
        public virtual Task<List<T>> AllAsync()
        {
            return _dbset.ToListAsync();
        }


        /// <summary>
        /// Get the entities verifying the predicate
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>An enumeration of entities</returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Any(predicate);
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Get by id an element async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Get by id an element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }


        #endregion

        #region Fields
        /// <summary>
        /// Database context
        /// </summary>
        private readonly DbContext _context;
        /// <summary>
        /// Collection of all entities in the context
        /// </summary>
        private readonly IDbSet<T> _dbset;
        #endregion
    }
}