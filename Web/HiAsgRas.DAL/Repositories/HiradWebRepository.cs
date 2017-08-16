using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace HiAsgRAS.DAL.Repositories
{
    public class HiradWebRepository : RepositoryBase<HiradWeb>, IHiradWebRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();

        public HiradWebRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public List<HiradWebAppListSearch_Result> searchHiradWebAppList_Procedure(HiradWebAppListSearch_Result objEntity)
        {
            var result = dbEntity.HiradWebAppListSearch(objEntity.WebFolder, objEntity.Active, objEntity.Status, objEntity.RemedyGroupName, objEntity.WebSite, objEntity.WebStat, Convert.ToString(objEntity.ABCID), objEntity.AppServer);
            return result.ToList();
        }
        public GetWebAppDetailsById_Result GetWebAppDetails(int id)
        {

            //var db = RepositoryContext.ObjectContext;
            //var appDetail = (from s in db.Set<HiradWeb>()
            //                 where s.Id == id
            //                 select s).FirstOrDefault();
            var result = dbEntity.GetWebAppDetailsById(id).FirstOrDefault();
            return result;
        }
        public IEnumerable<HiradWebModel> GetWebAll()
        {
            var db = RepositoryContext.ObjectContext;
            var webList = (from rec in db.Set<HiradWeb>()
                           where rec.IsDeleted == false &&
                           rec.StatusTypeId == 1
                           select new HiradWebModel
                           {
                               Id = rec.Id,
                               WebFolder = rec.WebFolder,
                               WebSite = rec.WebSite,
                               RemedyGroupName = rec.RemedyGroupName,
                               PrimayContact = rec.PrimayContact,
                               IsMonitor = rec.IsMonitor
                           }).ToList();

            return webList;
        }

        public bool CheckDuplicateWebApp(HiradWebModel hiradWebModel)
        {
            var recs = GetAll(x => (x.WebFolder.Trim().ToUpper() == hiradWebModel.WebFolder.Trim().ToUpper() && x.IsDeleted == false)).ToList();

            if (hiradWebModel.Id > 0)
            {
                recs = GetAll(x => (x.WebFolder.Trim().ToUpper() == hiradWebModel.WebFolder.Trim().ToUpper()) &&
                                  x.Id != hiradWebModel.Id && x.IsDeleted == false).ToList();
            }
            return recs.Count() > 0 ? true : false;
        }
        //public IEnumerable<HiradWeb> GetFolderList()
        //{
        //    var recs = GetAll(orderBy: x => x.FolderName);
        //    return recs;
        //}
        //public IEnumerable<Folder> GetAllFolderBySearch(FolderViewModel folderViewModel)
        //{
        //    Dictionary<int, Expression> operationMap = new Dictionary<int, Expression>();
        //    //Expression<Func<Folder, bool>> filter = null;
        //    //int filterCount = 0;

        //    //Name
        //    //if ((!string.IsNullOrEmpty(folderViewModel.FolderName)))
        //    //{
        //    //    var likePredicate = PredicateBuilder.False<Folder>();
        //    //    filter = likePredicate.LikeQuery(f => f.FolderName, folderViewModel.FolderName + "%");
        //    //    operationMap.Add(filterCount, filter);
        //    //    ++filterCount;


        //    //    //Generic Method to combine with All conditions by "And" Operator
        //    //    Expression<Func<Folder, bool>> combinedFilter = ExpressionBuilder.CombineWithAllFiltersByAnd<Folder>(operationMap);
        //    //    return GetAll(predicate: combinedFilter, orderBy: x => x.FolderName);
        //    //}
        //    //else
        //    {
        //        return GetAll().OrderBy(x => x.FolderName).ToList();
        //    }
        //}


        //public bool CheckDuplicateFolderName(FolderViewModel folderModel)
        //{
        //    var recs = GetAll(x => (x.FolderName.Trim().ToUpper() == folderModel.FolderName.Trim().ToUpper()) &&
        //                            x.FolderId != folderModel.FolderId).ToList();
        //    return recs.Count() > 0 ? true : false;
        //}

        
    }
}
 