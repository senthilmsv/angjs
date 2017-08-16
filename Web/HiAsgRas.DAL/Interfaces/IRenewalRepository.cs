
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IRenewalRepository : IRepository<RenewalDetail>
    {
        void UpdateRenewalRecord(RenewalViewModel renewalDetailModel);
    }
}
