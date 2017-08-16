using System.Data.Entity;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IRepositoryContext
    {

        DbSet<T> GetObjectSet<T>() where T : class;

        DbContext ObjectContext { get; }

        /// <summary>
        /// Save all changes to all repositories
        /// </summary>
        /// <returns>Integer with number of objects affected</returns>
        int SaveChanges();

        /// <summary>
        /// Terminates the current repository context
        /// </summary>
        void Terminate();
    }
}
