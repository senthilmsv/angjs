using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class StatusTypeRepository : RepositoryBase<StatusType>, IStatusTypeRepository
    {
        public StatusTypeRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }
    }
}
