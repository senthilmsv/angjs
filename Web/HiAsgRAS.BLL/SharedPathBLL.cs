using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL
{
    public class SharedPathBLL : ISharedPathBLL
    {

        private ISharedPathRepository _IRepository;

        public SharedPathBLL(ISharedPathRepository IRepository)
        {
            _IRepository = IRepository;
        }


        public int UpdateSharedPathList(SharedPathModel sharedPathModel)
        {
            int id = sharedPathModel.Id;

            var sharedPathEntity = MappingHelper.MappingHelper.MapSharedPathModelToEntity(sharedPathModel);

            if (sharedPathEntity.Id > 0)
            {
                //Update existing server details
                _IRepository.Update(sharedPathEntity);
            }
            else
            {
                _IRepository.Add(sharedPathEntity);

            }

            _IRepository.SaveChanges();

            return id;
        }

        public List<SharedPathModel> GetSharedNWPathDetails()
        {
            var result = (from c in _IRepository.GetSharedNWPathDetails()
                          select new SharedPathModel
                          {
                              Id = c.Id,
                              Name = c.Name ?? string.Empty,
                              Path = c.Path ?? string.Empty,
                              AppServerId = c.AppServerId,
                              ServerName = c.AppServerText ?? string.Empty,
                              BAOId = c.BAOId,
                              BAOwnerPrimary = c.BAOwnerPrimary ?? string.Empty,
                              BAPhonePrimary = c.BAPhonePrimary ?? string.Empty,
                              BAOwnerSecondary = c.BAOwnerSecondary ?? string.Empty,
                              BAPhoneSecondary = c.BAPhoneSecondary ?? string.Empty,
                              Comments = c.Comments ?? string.Empty
                          }).ToList();

            return result;
        }

        public SharedPathModel GetSharedNWPathDetailsById(int Id)
        {

            GetSharedNWPathDetails_Result result = new GetSharedNWPathDetails_Result();

            result = _IRepository.GetSharedNWPathDetailsById(Id);

            return new SharedPathModel
                {
                    Id = result.Id,
                    Name = result.Name ?? string.Empty,
                    Path = result.Path ?? string.Empty,
                    AppServerId = result.AppServerId,
                    ServerName = result.AppServerText ?? string.Empty,
                    BAOId = result.BAOId,
                    BAOwnerPrimary = result.BAOwnerPrimary ?? string.Empty,
                    BAPhonePrimary = result.BAPhonePrimary ?? string.Empty,
                    BAOwnerSecondary = result.BAOwnerSecondary ?? string.Empty,
                    BAPhoneSecondary = result.BAPhoneSecondary ?? string.Empty,
                    Comments = result.Comments ?? string.Empty,
                    CreatedBy = result.CreatedBy ?? string.Empty,
                    CreatedDate = result.CreatedDate
                };
        }

        public void DeleteSharedPath(int id, string modified)
        {
            var entity = _IRepository.GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedBy = modified;
                entity.ModifiedDate = DateTime.Now;
                _IRepository.Update(entity);
                _IRepository.SaveChanges();
            }
        }

        public ViewModel.SharedPathModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(ViewModel.SharedPathModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(ViewModel.SharedPathModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(ViewModel.SharedPathModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<ViewModel.SharedPathModel> GetAll()
        {
            return MappingHelper.MappingHelper.MapSharedPathEntitiesListToModels(
                    _IRepository.GetAll().Where(
                    x => x.IsDeleted.Equals(false)).ToList()).ToList();            
        }

        public IQueryable<ViewModel.SharedPathModel> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
