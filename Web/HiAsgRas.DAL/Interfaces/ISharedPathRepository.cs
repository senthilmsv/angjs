using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.DAL.EntityModels;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface ISharedPathRepository : IRepository<SharedPath>
    {
        List<GetSharedNWPathDetails_Result> GetSharedNWPathDetails();
        GetSharedNWPathDetails_Result GetSharedNWPathDetailsById(int Id);
    }
}
