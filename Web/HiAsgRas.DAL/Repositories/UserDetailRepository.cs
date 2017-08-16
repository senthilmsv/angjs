using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class UserDetailRepository : RepositoryBase<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public bool checkUserLogin()
        {
            return true;
        }
        public IEnumerable<UserDetailModel> GetUserList()
        {
            var db = RepositoryContext.ObjectContext;
            var userList = (from rec in db.Set<UserDetail>()
                           where rec.IsDeleted == false 
                            select new UserDetailModel
                              {
                                  Id = rec.Id,
                                  UserName = rec.UserName,
                                  UserType = rec.UserType,
                                  NUID = rec.NUID,
                                  EmailID = rec.EmailID,
                                  IsActive = rec.IsActive
                                  
                              }).ToList();

            return userList;
        }
        public UserDetail GetUserDetails(int id)
        {

            var db = RepositoryContext.ObjectContext;
            var userDetail = (from s in db.Set<UserDetail>()
                             where s.Id == id 
                             select s).FirstOrDefault();


            return userDetail;
        }
        public bool CheckDuplicateUserNUID(UserDetailModel userDetailModel)
        {
            var recs = GetAll(x => (x.NUID.Trim().ToUpper() == userDetailModel.NUID.Trim().ToUpper())).ToList();
            if (userDetailModel.Id > 0)
            {
               recs = GetAll(x => (x.NUID.Trim().ToUpper() == userDetailModel.NUID.Trim().ToUpper()) &&
                                x.Id != userDetailModel.Id).ToList();

            }
        
            return recs.Count() > 0 ? true : false;
        }
        public bool CheckDuplicateUserEmail(UserDetailModel userDetailModel)
        {
            var recs = GetAll(x => (x.EmailID.Trim().ToUpper() == userDetailModel.EmailID.Trim().ToUpper())).ToList();
            if (userDetailModel.Id > 0)
            {
                recs = GetAll(x => (x.EmailID.Trim().ToUpper() == userDetailModel.EmailID.Trim().ToUpper()) &&
                                 x.Id != userDetailModel.Id).ToList();

            }

            return recs.Count() > 0 ? true : false;
        }
        public int UpdateIsActive(int Id, string status)
        {
            int recUpdated;

            var db = RepositoryContext.ObjectContext;
            var userEntity = (from s in db.Set<UserDetail>()
                              where s.Id == Id
                              select s).FirstOrDefault();
            userEntity.IsActive = status.ToUpper() == "NO" ? "Yes" : "No"; 
            db.Set<UserDetail>().Attach(userEntity);
            db.Entry(userEntity).Property(x => x.IsActive).IsModified = true;
            recUpdated = db.SaveChanges();
            return recUpdated;
        }
    }
}
