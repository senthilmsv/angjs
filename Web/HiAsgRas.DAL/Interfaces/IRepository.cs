using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using HiAsgRAS.DAL.EntityModels;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get a selected based on the condition
        /// </summary>
        /// <param name="id">Primary key ID</param>
        T Find(Expression<Func<T, bool>> predicate, string includeProperties = "");

        T GetById(long Id);

        T GetByIntId(int Id);

        /// <summary> 
        /// Add entity to the repository 
        /// </summary> 
        /// <param name="entity">the entity to add</param> 
        /// <returns>The added entity</returns> 
        void Add(T entity);

        /// <summary> 
        /// Mark entity to be deleted within the repository 
        /// </summary> 
        /// <param name="entity">The entity to delete</param> 
        void Delete(T entity);

        /// <summary> 
        /// Mark selected Id to be deleted within the repository 
        /// </summary> 
        /// <param name="entity">The entity to delete</param> 
        void DeleteById(long Id);

        /// <summary> 
        /// Updates entity within the the repository 
        /// </summary> 
        /// <param name="entity">the entity to update</param> 
        /// <returns>The updates entity</returns> 
        void Update(T entity);

        void Update(T entity, params Expression<Func<T, object>>[] properties);

        /// <summary> 
        /// Load the entities using a linq expression filter
        /// </summary> 
        /// <typeparam name="E">the entity type to load</typeparam> 
        /// <param name="where">where condition</param> 
        /// <returns>the loaded entity</returns> 
        IList<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Load the entities using a linq expression filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IList<T> GetAll(Expression<Func<T, bool>> predicate = null,
                       Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
                       string includeProperties = "");

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary> 
        /// Query entities from the repository that match the linq expression selection criteria
        /// </summary> 
        /// <typeparam name="E">the entity type to load</typeparam> 
        /// <param name="where">where condition</param> 
        /// <returns>the loaded entity</returns> 
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Determines if there are any entities matching the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>True if a match was found</returns>
        bool Any(Expression<Func<T, bool>> predicate);


        /// <summary>
        /// Save all changes to all repositories
        /// </summary>
        /// <returns>Integer with number of objects affected</returns>
        int SaveChanges();

        /// <summary>
        /// Terminates the current repository context
        /// </summary>
        void Terminate();

       // void AddLog(Audit audit);
    }
}
