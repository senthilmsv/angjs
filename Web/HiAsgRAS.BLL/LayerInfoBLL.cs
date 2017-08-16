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
    public class LayerInfoBLL:ILayerInfoBLL
    {
          private ILayerInfoRepository _ILayerInfoRepository;

          public LayerInfoBLL(ILayerInfoRepository layerInfoRepository)
        {
            _ILayerInfoRepository = layerInfoRepository;
        }

          public IEnumerable<LayerInfoModel> GetLayerList()
        {
            var recs = _ILayerInfoRepository.GetLayerList();
            return recs;
        }

          public int UpdateLayerInfo(LayerInfoModel layerInfoModel)
          {
              int userId = layerInfoModel.Id;

              var userEntity = MappingHelper.MappingHelper.MapLayerModelToEntity(layerInfoModel);

              if (userEntity.Id > 0)
              {
                  //Update existing server details
                  _ILayerInfoRepository.Update(userEntity);
              }
              else
              {
                  _ILayerInfoRepository.Add(userEntity);

              }

              _ILayerInfoRepository.SaveChanges();

              return userId;
          }

          public LayerInfoModel GetLayerDetails(int id)
          {
              var layerEnity = _ILayerInfoRepository.GetLayerDetails(id);
              return MappingHelper.MappingHelper.MapLayerEntityToModel(layerEnity);
          }
          public void DeleteLayer(int id, string modified)
          {
              //_ILayerInfoRepository.DeleteById(id);
              //_ILayerInfoRepository.SaveChanges();
             // _ILayerInfoRepository.UpdateIsActive(id, "No");
              var entity = _ILayerInfoRepository.GetById(id);
              if (entity != null)
              {
                  entity.IsDeleted = true;
                  entity.ModifiedBy = modified;
                  entity.ModifiedDate = DateTime.Now;
                  _ILayerInfoRepository.Update(entity);
                  _ILayerInfoRepository.SaveChanges();
              }
          }
          public bool CheckDuplicateLayerName(LayerInfoModel layerInfoModel)
          {
              return _ILayerInfoRepository.CheckDuplicateLayerName(layerInfoModel);
          }

          //public int UpdateIsActive(int Id, string status)
          //{
          //    return _ILayerInfoRepository.UpdateIsActive(Id, status);
          //}
          public List<AppsListModel> GetAllAppsByLayerId(int layerId)
          {
              var rec = _ILayerInfoRepository.GetAllAppsByLayerId(layerId);
              return rec;
          }
         
          #region InterfaceImplementation
          public LayerInfoModel GetById(long Id)
          {
              throw new NotImplementedException();
          }

          public void Add(LayerInfoModel viewModel)
          {
              throw new NotImplementedException();
          }

          public void Delete(LayerInfoModel viewModel)
          {
              throw new NotImplementedException();
          }

          public void DeleteById(long Id)
          {
              throw new NotImplementedException();
          }

          public void Update(LayerInfoModel viewModel)
          {
              throw new NotImplementedException();
          }

          public IList<LayerInfoModel> GetAll()
          {
              return MappingHelper.MappingHelper.MapLayerTypeEntitiesListToModels(
                       _ILayerInfoRepository.GetAll().Where(
                       x => x.IsDeleted.Equals(false)).ToList()).ToList();
          }

          public IQueryable<LayerInfoModel> GetQueryable()
          {
              throw new NotImplementedException();
          }


          #endregion InterfaceImplementation
    }
}
