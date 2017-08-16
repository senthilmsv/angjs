using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class ApplicationInformationRepository : RepositoryBase<ApplicationInfomation>, IApplicationInformationRepository
    {
        public ApplicationInformationRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
