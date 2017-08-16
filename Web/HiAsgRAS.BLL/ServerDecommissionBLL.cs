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
    public class ServerDecommissionBLL : IServerDecommissionBLL
    {
         private  IServerDecommissionRepository _IServerDecommissionRepository;

         public ServerDecommissionBLL(IServerDecommissionRepository serverDecommissionRepository)
        {
            _IServerDecommissionRepository = serverDecommissionRepository;
        }

        public IEnumerable<HiradServerModel> GetAllServers()
        {
            var recs = _IServerDecommissionRepository.GetAllServers();
            return recs;
        }

        public IEnumerable<HiradAppModel> GetAllClientAppsList(int Id)
        {
            var recs = _IServerDecommissionRepository.GetAllClientAppsList(Id);
            return recs;
        }

        public IEnumerable<HiradWebModel> GetAllWebList(int Id)
        {
            var recs = _IServerDecommissionRepository.GetAllWebList(Id);
            return recs;
        }
        public IEnumerable<HiradDbMonitorModel> GetDBServerList(int Id)
        {
            var recs = _IServerDecommissionRepository.GetDBServerList(Id);
            return recs;
        }

        public bool SaveServerInfo(string Ids, string source, int newServerId, int oldServerId)
        {
            return _IServerDecommissionRepository.SaveServerInfo(Ids, source, newServerId, oldServerId);
        }


        public HiradServerModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(HiradServerModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(HiradServerModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(HiradServerModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<HiradServerModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<HiradServerModel> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
