using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.ViewModel;
using HiAsgRAS.DAL.Repositories;
using HiAsgRAS.DAL.Interfaces;
namespace HiAsgRAS.BLL
{
    public class UserDetailBLL : IUserDetailBLL
    {
        private IUserDetailRepository _IUsersRepository;

        public UserDetailBLL(IUserDetailRepository UsersRepository)
        {
            _IUsersRepository = UsersRepository;
        }

        public bool checkUseLogin()
        {
            return true;
        }

        public UserDetailModel GetUserByNUID(string Nuid)
        {
            return MappingHelper.MappingHelper.MapUserEntityToModel(_IUsersRepository.Find(u => u.NUID.Equals(Nuid) && (u.IsActive.ToUpper() == "Y" || u.IsActive.ToUpper() == "YES")));
        }

        public IEnumerable<UserDetailModel> GetUserList()
        {
            var recs = _IUsersRepository.GetUserList();
            return recs;
        }

        public int UpdateUserDetail(UserDetailModel userDetailModel)
        {
            int userId = userDetailModel.Id;

            var userEntity = MappingHelper.MappingHelper.MapUserModelToEntity(userDetailModel);

            if (userEntity.Id > 0)
            {
                //Update existing server details
                _IUsersRepository.Update(userEntity);
            }
            else
            {
                _IUsersRepository.Add(userEntity);

            }

            _IUsersRepository.SaveChanges();

            return userId;
        }

        public UserDetailModel GetUserDetails(int id)
        {
            var userEnity = _IUsersRepository.GetUserDetails(id);
            return MappingHelper.MappingHelper.MapUserEntityToModel(userEnity);
        }
        public void DeleteUser(int id, string modified)
        {
            //_IUsersRepository.DeleteById(id);
            //_IUsersRepository.SaveChanges();
          //  _IUsersRepository.UpdateIsActive(id, "No");
            var entity = _IUsersRepository.GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedBy = modified;
                entity.ModifiedDate = DateTime.Now;
                _IUsersRepository.Update(entity);
                _IUsersRepository.SaveChanges();
            }
        }

        public bool CheckDuplicateUserNUID(UserDetailModel userDetailModel)
        {
            return _IUsersRepository.CheckDuplicateUserNUID(userDetailModel);
        }
        public bool CheckDuplicateUserEmail(UserDetailModel userDetailModel)
        {
            return _IUsersRepository.CheckDuplicateUserEmail(userDetailModel);
        }
        public int UpdateIsActive(int Id, string status)
        {
            return _IUsersRepository.UpdateIsActive(Id, status);
        }
        #region InterfaceImplementation
        public UserDetailModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(UserDetailModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserDetailModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserDetailModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<UserDetailModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserDetailModel> GetQueryable()
        {
            throw new NotImplementedException();
        }
      

        #endregion InterfaceImplementation
    }
}
