using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class SharedPathRepository : RepositoryBase<SharedPath>, ISharedPathRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();
        public SharedPathRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }
        public List<GetSharedNWPathDetails_Result> GetSharedNWPathDetails()
        {
            var result = dbEntity.GetSharedNWPathDetails(null);
            return result.ToList();
        }

        public GetSharedNWPathDetails_Result GetSharedNWPathDetailsById(int Id)
        {
            var result = dbEntity.GetSharedNWPathDetails(Id).FirstOrDefault(); ;
            return result;
        }
    }
}
