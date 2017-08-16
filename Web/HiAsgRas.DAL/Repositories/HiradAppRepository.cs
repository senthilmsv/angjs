using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System.Collections.Generic;
using System.Linq;


namespace HiAsgRAS.DAL.Repositories
{
    public class HiradAppRepository : RepositoryBase<HiradApp>, IHiradAppRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();
        public HiradAppRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }
        public List<HiradAppListSearch_Result> searchHiradAppList_Procedure(HiradAppListSearch_Result objEntity)
        {
            var result = dbEntity.HiradAppListSearch(objEntity.Application, objEntity.APPServerName, objEntity.Vendor, objEntity.ApplicationLayer,objEntity.WebsiteURL,objEntity.ABCID,objEntity.SATName,objEntity.ApplicationRenewalDate,objEntity.ApplicationLiveDate);
            return result.ToList();
        }

        public GetClientAppDetailsById_Result GetAppDetails(int id)
        {

            //var db = RepositoryContext.ObjectContext;
            //var appDetail = (from s in db.Set<HiradApp>()
            //                    where s.Id == id
            //                    select s).FirstOrDefault();

            var result = dbEntity.GetClientAppDetailsById(id).FirstOrDefault();
            return result;            
        }

        public bool CheckDuplicateApp(HiradAppModel hiradAppModel)
        {
            var recs = GetAll(x => (x.Application.Trim().ToUpper() == hiradAppModel.Application.Trim().ToUpper() && x.IsDeleted == false)).ToList();

            if (hiradAppModel.Id > 0)
            {
                recs = GetAll(x => (x.Application.Trim().ToUpper() == hiradAppModel.Application.Trim().ToUpper() &&
                                  x.Id != hiradAppModel.Id && x.IsDeleted == false)).ToList();
            }
            return recs.Count() > 0 ? true : false;
        }

        public bool CheckDuplicateLayer(HiradAppModel hiradAppModel)
        {
            var recs = GetAll(x => (x.ApplicationLayer.Trim().ToUpper() == hiradAppModel.ApplicationLayer.Trim().ToUpper() && x.Layer5Location.Trim().ToUpper() == hiradAppModel.Layer5Location.Trim().ToUpper())).ToList();

            if (hiradAppModel.Id > 0)
            {
                recs = GetAll(x => (x.ApplicationLayer.Trim().ToUpper() == hiradAppModel.ApplicationLayer.Trim().ToUpper() && x.Layer5Location.Trim().ToUpper() == hiradAppModel.Layer5Location.Trim().ToUpper() &&
                                  x.Id != hiradAppModel.Id)).ToList();
            }
            return recs.Count() > 0 ? true : false;
        }

        //public long InsertRequest(RequestDetailViewModel m)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        var result = context.InsertRequest(m.UserId, m.ManagerId, m.FoldersSelected, m.EnvironmentsSelected, m.CreatedBy, m.Status, m.ReasonForAccess).FirstOrDefault();
        //        long retResult = 0;
        //        long.TryParse(result.ToString(), out retResult);
        //        return retResult;
        //    }
        //}

        //public void UpdateRequest(ApproveViewModel a)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        context.UpdateRequest(a.UserId, a.ManagerId,a.RequestId,a.CreatedBy,a.Status,a.Comments);
        //    }
        //}

      

        //public void UpdateWorkOrder(ApproveViewModel a)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        context.UpdateWorkOrder(a.UserId,a.ManagerId,a.RequestId,a.CreatedBy,a.Status,a.Comments,a.WorkOrderNumber);
        //    }
        //}

        //public void UpdateWorkOrderWithStatus(ApproveViewModel a)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        context.UpdateWorkOrderWithStatus(a.UserId, a.ManagerId, a.RequestId, a.CreatedBy, a.Status, a.Comments, a.WorkOrderNumber);
        //    }
        //}

        //public List<ApproveViewModel> GetTransactionsById(long RequestId)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        var results = (from a in context.RequestDetails
        //                       where a.RequestId == RequestId
        //                       select new ApproveViewModel
        //                       {
        //                           CreatedDate = a.CreatedDate,
        //                           Status = a.Status,                                   
        //                           Comments = a.Comments
        //                       }).ToList();
        //        return results;
        //    }
        //}


        //public List<GetRequestDetailsByStatus_Result> GetRequestDetails(int status)
        //{
        //    //short? statusValue = Convert.ToInt16(status);
        //    //using (var context = new InfoViewAccessEntities())
        //    //{
        //    //    var requests = context.GetRequestDetailsByStatus(statusValue).ToList();

        //    //    return requests;
        //    //}
        //    return new List<GetRequestDetailsByStatus_Result>();
        //}

        //public IEnumerable<GetRequestDetailsByStatus_Result> GetRequestDetailsByStatus(GetAllRequestsViewModel viewModel)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //       // viewModel.FromDate, viewModel.ToDate
        //        short? statusValue = Convert.ToInt16(viewModel.Status);
        //        var requests = context.GetRequestDetailsByStatus(statusValue,viewModel.FromDate,viewModel.ToDate).ToList();
        //        return requests;
        //    }
        //}

        //public IEnumerable<GetRequestDetailsByWorkOrder_Result> GetRequestDetailsByWorkOrder(GetAllRequestsViewModel viewModel)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        // viewModel.FromDate, viewModel.ToDate
        //       // short? statusValue = Convert.ToInt16(viewModel.Status);
        //        var requests = context.GetRequestDetailsByWorkOrder(viewModel.WorkOrderNumber).ToList();
        //        return requests;
        //    }
        //}

        //public IEnumerable<GetRequestDetailsByUser_Result> GetRequestDetailsByUser(GetAllRequestsViewModel viewModel)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    { 
        //        var requests = context.GetRequestDetailsByUser(viewModel.UsersSelected).ToList();
        //        return requests;
                
        //    }
        //}

        //public IEnumerable<GetRequestDetailsByManager_Result> GetRequestDetailsByManager(GetAllRequestsViewModel viewModel)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        short? statusValue = Convert.ToInt16(viewModel.Status);
        //        var requests = context.GetRequestDetailsByManager(statusValue, viewModel.FromDate, viewModel.ToDate,viewModel.ManagersSelected).ToList();
        //        return requests;
        //    }
        //}

        //public IEnumerable<RequestDetailViewModel> GetComments(long RequestId)
        //{
        //    throw new NotImplementedException();
        //}

        //public GetRequestInformationById_Result GetRequestInformationById(long RequestId)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        return context.GetRequestInformationById(RequestId).FirstOrDefault();             
        //    }
        //}

        //public void DeleteRequest(long RequestId)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        context.DeleteRequest(RequestId);
        //    }
        //}





        //public List<GetAllRequestsPendingByManager_Result> GetAllRequestsPendingByManager(long ManagerId)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        return context.GetAllRequestsPendingByManager(ManagerId).ToList();
        //    }
        //}

        //public List<GetAllRequestsByAdmin_Result> GetAllRequestsByAdmin()
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        return context.GetAllRequestsByAdmin().ToList();
        //    }
        //}

        //public List<GetAllRequestCreatedByUser_Result> GetAllRequestCreatedByUser(long UserId)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        return context.GetAllRequestCreatedByUser(UserId).ToList();
        //    }
        //}

        //public List<GetDashboardDetails_Result> GetDashboardDetails(long UserId)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        return context.GetDashboardDetails(UserId).ToList();
        //    }
        //}

        //public void ModifyRequest(RequestDetailViewModel m)
        //{
        //    using (var context = new InfoViewAccessEntities())
        //    {
        //        var result = context.ModifyRequest(m.RequestId, 
        //            m.RequestorDetails.UserId,
        //            m.RequestorDetails.EMailId,
        //            m.RequestorDetails.PrimaryPhoneNumber,
        //            m.RequestorDetails.SecondaryPhoneNumber,                
        //            m.ManagerId,                      
        //            m.FoldersSelected, 
        //            m.EnvironmentsSelected, 
        //            m.ModifiedBy, m.Status, m.ReasonForAccess);
        //    }
        //}
    }
}