using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class RenewalRepository : RepositoryBase<RenewalDetail>, IRenewalRepository
    {
        public RenewalRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }
        public void UpdateRenewalRecord(RenewalViewModel renewalDetailModel)
        {
            //Sp Call
            using (var context = new HiDashEntities())
            {
                context.UpdateRenewalDetail(renewalDetailModel.IsRenewalRequired,renewalDetailModel.UniqueId);
            }
        }
    }
}
