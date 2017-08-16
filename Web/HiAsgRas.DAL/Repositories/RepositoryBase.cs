using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HiAsgRAS.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
         where T : class
    {

        #region Constructor
        public RepositoryBase()
            : this(new BaseRepositoryContext())
        {
        }

        public RepositoryBase(IRepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext ?? new BaseRepositoryContext();
            _objectSet = repositoryContext.GetObjectSet<T>();
        }
        #endregion

        #region Properties
        private DbSet<T> _objectSet;
        public DbSet<T> ObjectSet
        {
            get
            {
                return _objectSet;
            }
        }

        private IRepositoryContext _repositoryContext;
        public IRepositoryContext RepositoryContext
        {
            get
            {
                return _repositoryContext;
            }
        }
        #endregion

        #region IRepository Members

        public void Add(T entity)
        {            
            this.ObjectSet.Add(entity);
        }
        //public long Add(T entity)
        //{
        //    this.ObjectSet.Add(entity);
        //    this.SaveChanges();
        //    var tblName = ((System.Reflection.MemberInfo)(entity.GetType())).Name;
        //    var identityQuery = "SELECT IDENT_CURRENT ('dbo." + tblName + "')";
        //    var identityValue = _repositoryContext.ObjectContext.Database.SqlQuery<decimal>(identityQuery).Single();

        //    return Convert.ToInt64(identityValue);
        //}

        public void Update(T entity)
        {
            var entry = this.RepositoryContext.ObjectContext.Entry<T>(entity);

            // Retreive the Id through reflection
            string keyName = "Id";
            string entityName = ObjectContext.GetObjectType(entry.Entity.GetType()).Name;
            keyName = GetPrimaryKeyName(entry, entityName, keyName);
            var pkey = entry.Entity.GetType().GetProperty(keyName).GetValue(entity, null);
            //var isDeleteFlag = entry.Entity.GetType().GetProperty("DeleteFlag").GetValue(entity, null);

            if (entry.State == EntityState.Detached)
            {
                var set = this.RepositoryContext.ObjectContext.Set<T>();
                T attachedEntity = set.Find(pkey);  // access the key
                if (attachedEntity != null)
                {
                    var attachedEntry = this.RepositoryContext.ObjectContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                    //if (isDeleteFlag.ToString() == "Y")
                    //{
                    //    entry.State = EntityState.Deleted;
                    //}
                }
                else
                {
                    entry.State = EntityState.Modified; // attach the entity
                }
            }
        }

        public void Update(T entity, params Expression<Func<T, object>>[] properties)
        {
            this.ObjectSet.Attach(entity);
            DbEntityEntry<T> entry = this.RepositoryContext.ObjectContext.Entry(entity);
            foreach (var selector in properties)
            {
                entry.Property(selector).IsModified = true;
            }
            entry.State = EntityState.Modified;
            this.RepositoryContext.ObjectContext.Entry(entity).State = EntityState.Modified;
        }

        private static string GetPrimaryKeyName(DbEntityEntry entry, string entityName, string keyName)
        {
            // Get primary key value (If you have more than one key column, this will need to be adjusted)
            var keyNames = entry.Entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).ToList();

            string findKeyName = string.Empty;
            if (keyNames == null || keyNames.Count == 0)
            {
                var keyProperties = entry.Entity.GetType().GetProperties().ToList();
                if (keyProperties != null && keyProperties.Count > 0)
                {
                    findKeyName = "Id";
                    if (keyProperties.Where(p => p.Name == findKeyName).Count() > 0)
                    {
                        keyName = findKeyName;
                    }
                    else if (entityName.Contains("UserDetail"))
                    {
                        keyName = "UserId";
                    }
                   
                    else
                    {
                        findKeyName = entityName + "Id";
                        if (keyProperties.Where(p => p.Name == findKeyName).Count() > 0)
                        {
                            keyName = findKeyName;
                        }

                    }
                }
                else
                {
                    keyName = "Id";
                }
            }
            else
            {
                keyName = keyNames[0].Name;
            }
            return keyName;
        }

        public void Delete(T entity)
        {
            this.ObjectSet.Remove(entity);
        }

        public void DeleteById(long Id)
        {
            T entity = GetById(Id);
            this.ObjectSet.Remove(entity);
            //this.SaveChanges();
        }

        #region GetAll
        public IList<T> GetAll()
        {
            return this.ObjectSet.ToList<T>();
        }

        public IList<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            //return this.ObjectSet.Where(predicate).ToList<T>();
            IQueryable<T> query = GetQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }


        public IList<T> GetAll(Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null)
        {
            //IQueryable<T> query = this.ObjectSet;
            IQueryable<T> query = GetQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
                return query.ToList();
            }

            return query.ToList();
        }
        public IList<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = GetQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (string includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }
        #endregion

        #region Get
        public T GetById(long Id)
        {
            if (Id == 0)
            {
                throw new ApplicationException("please enter valid id.");
            }
            return this.ObjectSet.Find(Id);
        }

        public T GetByIntId(int Id)
        {
            if (Id == 0)
            {
                throw new ApplicationException("please enter valid id.");
            }
            return this.ObjectSet.Find(Id);
        }

        public T Find(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            if (predicate == null)
            {
                throw new ApplicationException("please enter valid condition.");
            }

            IQueryable<T> query = GetQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (string includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault<T>();
        }
        #endregion

        public IQueryable<T> GetQueryable()
        {
            return this.ObjectSet.AsQueryable<T>();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.ObjectSet.Any(predicate);
        }
        #endregion

        #region Save Changes
        public int SaveChanges()
        {
            AuditData();
            return this.RepositoryContext.SaveChanges();
        }

        private void AuditData()
        {
            string strAuditLog = string.Empty;
            if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.AuditLog] != null)
            {
                strAuditLog = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.AuditLog].ToString();
            }
            if (strAuditLog.ToLower() == ApplicationConstants.AppKeyValues.On)
            {
                List<Audit> auditLogs = new List<Audit>();
                auditLogs = GetAuditLogData();
                foreach (Audit a in auditLogs)
                {
                    AddLog(a);
                }
            }
        }
        #endregion

        #region Terminate
        public void Terminate()
        {
            this.RepositoryContext.Terminate();
        }
        #endregion

        #region Logging
        public void AddLog(Audit audit)
        {
            this.RepositoryContext.ObjectContext.Entry(audit).State = EntityState.Added;
        }

        #region Get Audit Log Data

        public List<Audit> GetAuditLogData()
        {
            Audit auditLog = new Audit();
            List<Audit> AuditLogs = new List<Audit>();
            var changeTrack = this.RepositoryContext.ObjectContext.ChangeTracker.Entries().Where(
                p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified);
            foreach (var entry in changeTrack)
            {
                if (entry.Entity != null)
                {
                    string entityName = string.Empty;
                    string state = string.Empty;

                    entityName = ObjectContext.GetObjectType(entry.Entity.GetType()).Name;

                    string strAuditTrack = string.Empty;
                    if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.AuditTrack] != null)
                    {
                        strAuditTrack = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.AuditTrack].ToString();
                    }

                    if (strAuditTrack != string.Empty && strAuditTrack.Contains(entityName))
                    {
                        state = entry.State.ToString();
                        auditLog = new Audit()
                        {
                            TableName = entityName,
                            State = state,
                            CreatedDate = DateTime.Now
                        };

                        string keyName = string.Empty; //"Id"
                        keyName = GetPrimaryKeyName(entry, entityName, keyName);

                        if (keyName != string.Empty)
                        {
                            switch (entry.State)
                            {
                                case EntityState.Modified:
                                    SetLogModifiedEntity(auditLog, AuditLogs, entry, keyName);
                                    break;
                                case EntityState.Added:
                                    SetLogAddedEntity(auditLog, AuditLogs, entry);
                                    break;
                                case EntityState.Deleted:
                                    SetLogDeletedEntity(auditLog, AuditLogs, entry, keyName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            return AuditLogs;
        }

        private static void SetLogAddedEntity(Audit auditLog, List<Audit> AuditLogs, DbEntityEntry entry)
        {
            string oldData = string.Empty, newData = string.Empty;
            StringBuilder sbNewData = new StringBuilder();

            //CreatedBy Is Log data Created By, there is no Modified by, each and every transaction will be created as new row in Audit Log table
            //FIXIT
            // auditLog.CreatedBy = NullHandler.ConvertToIntNullable(entry.CurrentValues.GetValue<object>("CreatedBy").ToString());
            foreach (string prop in entry.CurrentValues.PropertyNames)
            {
                object currentValue = entry.CurrentValues[prop];
                if (currentValue != null)
                {
                    newData = string.Format("{0}={1} || ", prop, currentValue.ToString());
                    sbNewData.Append(newData);
                }
            }
            auditLog.NewData = sbNewData.ToString();
            auditLog.OldData = string.Empty;
            AuditLogs.Add(auditLog);
        }

        private static void SetLogModifiedEntity(Audit auditLog, List<Audit> AuditLogs, DbEntityEntry entry, string keyName)
        {
            string oldData = string.Empty, newData = string.Empty;
            StringBuilder sbOldData = new StringBuilder();
            StringBuilder sbNewData = new StringBuilder();

            auditLog.RecordID = entry.OriginalValues.GetValue<object>(keyName).ToString();
            //CreatedBy Is Log data Created By, there is no Modified by, each and every transaction will be created as new row in Audit Log table
            //FIXIT
            //auditLog.CreatedBy = NullHandler.ConvertToIntNullable(entry.CurrentValues.GetValue<object>("ModifiedBy").ToString());

            foreach (string prop in entry.OriginalValues.PropertyNames)
            {
                object currentValue = entry.CurrentValues[prop];
                object oldValue = entry.OriginalValues[prop];
                if (oldValue != null && currentValue != null && !currentValue.Equals(oldValue))
                {
                    oldData = string.Format("{0}={1} || ", prop, oldValue.ToString());
                    sbOldData.Append(oldData);
                    if (currentValue != null)
                    {
                        newData = string.Format("{0}={1} || ", prop, currentValue.ToString());
                        sbNewData.Append(newData);
                    }
                }
            }

            auditLog.OldData = sbOldData.ToString();
            auditLog.NewData = sbNewData.ToString();
            AuditLogs.Add(auditLog);
        }

        private static void SetLogDeletedEntity(Audit auditLog, List<Audit> AuditLogs, DbEntityEntry entry, string keyName)
        {
            string oldData = string.Empty, newData = string.Empty;
            StringBuilder sbOldData = new StringBuilder();

            auditLog.RecordID = entry.OriginalValues.GetValue<object>(keyName).ToString();
            //CreatedBy Is Log data Created By, there is no Modified by, each and every transaction will be created as new row in Audit Log table
            //FIXIT
            //auditLog.CreatedBy = NullHandler.ConvertToIntNullable(entry.CurrentValues.GetValue<object>("ModifiedBy").ToString());

            foreach (string prop in entry.OriginalValues.PropertyNames)
            {
                object oldValue = entry.OriginalValues[prop];
                if (oldValue != null)
                {
                    oldData = string.Format("{0}={1} || ", prop, oldValue.ToString());
                    sbOldData.Append(oldData);
                }
            }
            auditLog.OldData = sbOldData.ToString();
            auditLog.NewData = string.Empty;
            AuditLogs.Add(auditLog);
        }
        #endregion

        #endregion




    }
}
