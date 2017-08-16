using HiAsgRAS.DAL.Infrastructure;
using HiAsgRAS.DAL.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace HiAsgRAS.DAL.Repositories
{
    public class BaseRepositoryContext : IRepositoryContext
    {
        private const string OBJECT_CONTEXT_KEY = "HiAsgRAS.DAL.EntityModels.HiDashEntities";
        public DbSet<T> GetObjectSet<T>()
            where T : class
        {
            return ContextManager.GetObjectContext(OBJECT_CONTEXT_KEY).Set<T>();
        }

        /// <summary>
        /// Returns the active object context
        /// </summary>
        public DbContext ObjectContext
        {
            get
            {
                return ContextManager.GetObjectContext(OBJECT_CONTEXT_KEY);
            }
        }

        public int SaveChanges()
        {
            try
            {
                return this.ObjectContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw dbEx;  // You can also choose to handle the exception here...
            }
 
        }

        public void Terminate()
        {
            ContextManager.SetRepositoryContext(null, OBJECT_CONTEXT_KEY);
        }
    }
}
