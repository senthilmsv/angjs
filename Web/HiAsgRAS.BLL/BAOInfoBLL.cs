using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.ViewModel;
using HiAsgRAS.DAL.Repositories;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.DAL.EntityModels;
namespace HiAsgRAS.BLL
{
    public class BAOInfoBLL:IBAOInfoBLL
    {
          private IBAOInfoRepository _IBAOInfoRepository;

          public BAOInfoBLL(IBAOInfoRepository iBAOInfoRepository)
        {
            _IBAOInfoRepository = iBAOInfoRepository;
        }

          public IEnumerable<BAOInfoModel> GetBAOList()
        {
            var recs = _IBAOInfoRepository.GetBAOList();
            return recs;
        }

          public List<BAOInfoModel> searchBAOList_procedure(BAOInfoModel baoInfoModel)
          {
              BAOSearch_Result objEntity = new BAOSearch_Result();
              objEntity.BAOwnerPrimary = baoInfoModel.BAOwnerPrimary;
              objEntity.BAEmailPrimary = baoInfoModel.BAEmailPrimary;
              objEntity.BAPhonePrimary = baoInfoModel.BAPhonePrimary;
              objEntity.BAODeptPrimary = baoInfoModel.BAODeptPrimary;
              objEntity.BAOwnerSecondary = baoInfoModel.BAOwnerSecondary;
              objEntity.BAEmailSecondary = baoInfoModel.BAEmailSecondary;
              objEntity.BAPhoneSecondary = baoInfoModel.BAPhoneSecondary;
              objEntity.BAODeptSecondary = baoInfoModel.BAODeptSecondary;

              var result = (from c in _IBAOInfoRepository.searchBAOSList_Procedure(objEntity)
                            where c.IsDeleted == false
                            select new BAOInfoModel
                            {
                                Id = c.Id,
                                BAOwnerPrimary = c.BAOwnerPrimary ?? string.Empty,
                                BAEmailPrimary = c.BAEmailPrimary ?? string.Empty,
                                BAPhonePrimary = c.BAPhonePrimary ?? string.Empty,
                                BAODeptPrimary = c.BAODeptPrimary ?? string.Empty,
                                BAOwnerSecondary = c.BAOwnerSecondary ?? string.Empty,
                                BAEmailSecondary = c.BAEmailSecondary ?? string.Empty,
                                BAPhoneSecondary = c.BAPhoneSecondary ?? string.Empty,
                                BAODeptSecondary = c.BAODeptSecondary ?? string.Empty
                               // IsActive = c.IsActive ?? string.Empty
                            }).ToList();

              return result;
          }
          public int UpdateBAOInfo(BAOInfoModel baoInfoModel)
          {
              int baoId = baoInfoModel.Id;

              var baoEntity = MappingHelper.MappingHelper.MapBAOModelToEntity(baoInfoModel);

              if (baoEntity.Id > 0)
              {
                  //Update existing server details
                  _IBAOInfoRepository.Update(baoEntity);
              }
              else
              {
                  _IBAOInfoRepository.Add(baoEntity);

              }

              _IBAOInfoRepository.SaveChanges();

              return baoId;
          }

          public BAOInfoModel GetBAODetails(int id)
          {
              var baoEnity = _IBAOInfoRepository.GetBAODetails(id);
              return MappingHelper.MappingHelper.MapBAOEnitityToModel(baoEnity);
          }
          public void DeleteBAO(int id, string modified)
          {
              //_IBAOInfoRepository.DeleteById(id);
              //_IBAOInfoRepository.SaveChanges();
             // _IBAOInfoRepository.UpdateIsActive(id, "No");
              var entity = _IBAOInfoRepository.GetById(id);
              if (entity != null)
              {
                  entity.IsDeleted = true;
                  entity.ModifiedBy = modified;
                  entity.ModifiedDate = DateTime.Now;
                  _IBAOInfoRepository.Update(entity);
                  _IBAOInfoRepository.SaveChanges();
              }
          }

          public bool CheckDuplicateBAO(BAOInfoModel baoInfoModel)
          {
              return _IBAOInfoRepository.CheckDuplicateBAO(baoInfoModel);
          }

          //public int UpdateIsActive(int Id, string status)
          //{
          //    return _IBAOInfoRepository.UpdateIsActive(Id, status);
          //}

          public List<AppsListModel> GetAllAppsByBAOId(int baoId)
          {
              var appList = _IBAOInfoRepository.GetAllAppsByBAOId(baoId);
              return appList;
          }

          public BAOInfoModel GetBaoInfoByApplication(int appId,string appType)
          {
              return _IBAOInfoRepository.GetBaoInfoByApplication(appId,appType);
          }

        
          #region InterfaceImplementation
          public BAOInfoModel GetById(long Id)
          {
              throw new NotImplementedException();
          }

          public void Add(BAOInfoModel viewModel)
          {
              throw new NotImplementedException();
          }

          public void Delete(BAOInfoModel viewModel)
          {
              throw new NotImplementedException();
          }

          public void DeleteById(long Id)
          {
              throw new NotImplementedException();
          }

          public void Update(BAOInfoModel viewModel)
          {
              throw new NotImplementedException();
          }

          public IList<BAOInfoModel> GetAll()
          {
              throw new NotImplementedException();
          }

          public IQueryable<BAOInfoModel> GetQueryable()
          {
              throw new NotImplementedException();
          }


          #endregion InterfaceImplementation
    }
}

 

