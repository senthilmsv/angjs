using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiAsgRAS.BLL
{
    public class StatusTypeBLL : IStatusTypeBLL
    {
        private IStatusTypeRepository _IStatusTypeRepository;
        public StatusTypeBLL(IStatusTypeRepository IStatusTypeRepository)
        {
            _IStatusTypeRepository = IStatusTypeRepository;
        }

        public ViewModel.StatusTypeModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(ViewModel.StatusTypeModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(ViewModel.StatusTypeModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(ViewModel.StatusTypeModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<ViewModel.StatusTypeModel> GetAll()
        {
            return MappingHelper.MappingHelper.MapStatusTypeEntitiesListToModels(
                    _IStatusTypeRepository.GetAll().Where(
                    x => x.IsDeleted.Equals(false)).ToList()).ToList();
        }

        public IQueryable<ViewModel.StatusTypeModel> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
