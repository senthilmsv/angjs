using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiAsgRAS.BLL
{
    public class ApplicationInfomationBLL : IApplicationInfomationBLL
    {
         private IApplicationInformationRepository _IApplicationInfoRepository;
         public ApplicationInfomationBLL(IApplicationInformationRepository IApplicationInfoRepository)
        {
            _IApplicationInfoRepository = IApplicationInfoRepository;
        }

         public ViewModel.ApplicationInformationModel GetById(long Id)
         {
             return MappingHelper.MappingHelper.MapApplicationInfoEntityToViewModel(
                     _IApplicationInfoRepository.GetAll().Where(x => x.Id == Id).FirstOrDefault());
         }
         public void Add(ViewModel.ApplicationInformationModel viewModel)
        {
            throw new NotImplementedException();
        }

         public void Delete(ViewModel.ApplicationInformationModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(ViewModel.ApplicationInformationModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<ViewModel.ApplicationInformationModel> GetAll()
        {

            return MappingHelper.MappingHelper.MapApplicationInfoListToModels(
                   _IApplicationInfoRepository.GetAll().ToList()).ToList();
        }

        public IQueryable<ViewModel.ApplicationInformationModel> GetQueryable()
        {
            throw new NotImplementedException();
        }
        public int AddApplicationInformation(ViewModel.ApplicationInformationModel appInfoModel)
        {
            int appInfoId = appInfoModel.Id;

            var appInfoEntity = MappingHelper.MappingHelper.MapApplicationInfoModelToEntity(appInfoModel);

            if (appInfoEntity.Id > 0)
            {
                //Update existing server details
                _IApplicationInfoRepository.Update(appInfoEntity);
            }
            else
            {
                _IApplicationInfoRepository.Add(appInfoEntity);

            }

            _IApplicationInfoRepository.SaveChanges();

            return appInfoId;
        }
    }
}
