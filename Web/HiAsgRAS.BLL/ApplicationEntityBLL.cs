using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiAsgRAS.BLL
{
    public class ApplicationEntityBLL : IApplicationEntityBLL
    {
        private IApplicationEntityRepository _IApplicationEntityRepository;
        public ApplicationEntityBLL(IApplicationEntityRepository IApplicationEntityRepository)
        {
            _IApplicationEntityRepository = IApplicationEntityRepository;
        }

        public ViewModel.ApplicationEntityModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(ViewModel.ApplicationEntityModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(ViewModel.ApplicationEntityModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(ViewModel.ApplicationEntityModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<ViewModel.ApplicationEntityModel> GetAll()
        {            

            return MappingHelper.MappingHelper.MapApplicationEntitiesListToModels(
                    _IApplicationEntityRepository.GetAll().Where(
                    x => x.IsDeleted.Equals(false)).ToList()).ToList();
        }

        public IQueryable<ViewModel.ApplicationEntityModel> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
