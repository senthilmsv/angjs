using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;

namespace HiAsgRAS.DAL.Repositories
{
    public class LayerInfoRepository : RepositoryBase<LayerInfo>, ILayerInfoRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();

        public LayerInfoRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public IEnumerable<LayerInfoModel> GetLayerList()
        {
            var db = RepositoryContext.ObjectContext;
            var serverList = (from rec in db.Set<LayerInfo>()
                              where rec.IsDeleted == false
                              select new LayerInfoModel
                              {
                                  Id = rec.Id,
                                  AppLayerName = rec.AppLayerName,
                                  LayerLocation = rec.LayerLocation                                  
                              }).ToList();

            return serverList;
        }

        public LayerInfo GetLayerDetails(int id)
        {

            var db = RepositoryContext.ObjectContext;
            var layerDetail = (from s in db.Set<LayerInfo>()
                              where s.Id == id
                              select s).FirstOrDefault();


            return layerDetail;
        }
        public bool CheckDuplicateLayerName(LayerInfoModel layerModel)
        {
            var recs = GetAll(x => (x.AppLayerName.Trim().ToUpper() == layerModel.AppLayerName.Trim().ToUpper() && x.LayerLocation.Trim().ToUpper() == layerModel.LayerLocation.Trim().ToUpper())).ToList();
            
            if (layerModel.Id > 0)
            {
                recs = GetAll(x => (x.AppLayerName.Trim().ToUpper() == layerModel.AppLayerName.Trim().ToUpper() && x.LayerLocation.Trim().ToUpper() == layerModel.LayerLocation.Trim().ToUpper() &&
                                  x.Id != layerModel.Id)).ToList();
            }
            return recs.Count() > 0 ? true : false;
        }
        //public int UpdateIsActive(int Id, string status)
        //{
        //    int recUpdated;

        //    var db = RepositoryContext.ObjectContext;
        //    var layerEntity = (from s in db.Set<LayerInfo>()
        //                      where s.Id == Id
        //                      select s).FirstOrDefault();
        //    layerEntity.IsActive = status.ToUpper() == "NO" ? "Yes" : "No"; 
        //    db.Set<LayerInfo>().Attach(layerEntity);
        //    db.Entry(layerEntity).Property(x => x.IsActive).IsModified = true;
        //    recUpdated = db.SaveChanges();
        //    return recUpdated;
        //}

        public List<AppsListModel> GetAllAppsByLayerId(int layerId)
        {
            List<AppsListModel> lstAllAps = new List<AppsListModel>();

            var db = RepositoryContext.ObjectContext;
         
            var appsList = (from rec in db.Set<HiradApp>()
                            where rec.IsDeleted == false &&
                                  (rec.ApplicationLayerId == layerId)
                            select new AppsListModel
                            {
                                AppName = rec.Application + string.Empty,
                                RemedyGroupName = rec.RemedyGroupName + string.Empty,
                                DbName = string.Empty
                            }).ToList();

            lstAllAps.AddRange(appsList);

            return lstAllAps.ToList();
        }
    }
}
