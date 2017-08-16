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
    public class HiradServerBLL : IHiradServerBLL
    {

        private IHiradServerRepository _IHiradServerRepository;

        public HiradServerBLL(IHiradServerRepository hiradServerRepository)
        {
            _IHiradServerRepository = hiradServerRepository;
        }

        public IEnumerable<HiradServerModel> GetServerAll()
        {
            var recs = _IHiradServerRepository.GetServerAll();
            return recs;
        }
        public bool CheckDuplicateSystemName(HiradServerModel hiradServerModel)
        {
            return _IHiradServerRepository.CheckDuplicateSystemName(hiradServerModel);
        }


        public int UpdateServer(HiradServerModel HiradServerViewModel)
        {
            int ServerId = HiradServerViewModel.Id;

            var serverEntity = MappingHelper.MappingHelper.MapSeverViewModelToEntity(HiradServerViewModel);

            if (serverEntity.Id > 0)
            {
                //Update existing server details
                _IHiradServerRepository.Update(serverEntity);
            }
            else
            {
                _IHiradServerRepository.Add(serverEntity);

            }

            _IHiradServerRepository.SaveChanges();

            return ServerId;
        }


        public HiradServerModel GetServerDetails(int id)
        {
            var serverEnity = _IHiradServerRepository.GetSeverDetails(id);
            return MappingHelper.MappingHelper.MapSeverEntityToViewModel(serverEnity);
        }
        public void DeleteServer(int Id, string modified)
        {
            //_IHiradServerRepository.DeleteById(id);
            //_IHiradServerRepository.SaveChanges();

            var entity = _IHiradServerRepository.GetById(Id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedBy = modified;
                entity.ModifiedDate = DateTime.Now;
                _IHiradServerRepository.Update(entity);
                _IHiradServerRepository.SaveChanges();
            }
        }

        public List<HiradServerModel> searchHiradServerList_procedure(HiradServerModel hiradModel)
        {
            HiradServerListSearch_Result objEntity = new HiradServerListSearch_Result();
            objEntity.SystemName = hiradModel.SystemName;
            objEntity.Processor = hiradModel.Processor;
            objEntity.Location = hiradModel.Location;
            objEntity.IPAddress = hiradModel.IPAddress;
            objEntity.ABCId = hiradModel.ABCId;
            objEntity.CostCenter = hiradModel.CostCenter;
            objEntity.SerialNumber = hiradModel.SerialNumber;
            objEntity.Platform = hiradModel.Platform;
            objEntity.TotalCores = hiradModel.TotalCores;
            objEntity.CostCenter = hiradModel.CostCenter;
            objEntity.Model = hiradModel.Model;
            objEntity.Storage = hiradModel.Storage;
            objEntity.SupportStaff = hiradModel.SupportStaff;
            objEntity.TSMInstalled = hiradModel.TSMInstalled;
            objEntity.RAM = hiradModel.RAM;
            objEntity.HDDConfiguration = hiradModel.HDDConfiguration;
            objEntity.AssetTag = hiradModel.AssetTag;
            objEntity.Comments = hiradModel.Comments;

            var result = (from c in _IHiradServerRepository.searchHiradServerList_Procedure(objEntity)
                          select new HiradServerModel
                          {
                              Id = c.Id,
                              SystemName = c.SystemName ?? string.Empty,
                              Location = c.Location ?? string.Empty,
                              SerialNumber = c.SerialNumber ?? string.Empty,
                              AssetTag = c.AssetTag ?? string.Empty,
                              ABCId = c.ABCId ?? string.Empty,
                              CostCenter = c.CostCenter ?? string.Empty,
                              SupportStaff = c.SupportStaff ?? string.Empty,
                              Model = c.Model ?? string.Empty,
                              Platform = c.Platform ?? string.Empty, //OsName
                              BuildVersion = c.BuildVersion ?? string.Empty,
                              IPAddress = c.IPAddress ?? string.Empty,
                              Processor = c.Processor ?? string.Empty,
                              TotalCores = Convert.ToDouble(c.TotalCores),
                              Storage = c.Storage ?? string.Empty,
                              HDDConfiguration = c.HDDConfiguration ?? string.Empty,
                              RAM = c.RAM ?? string.Empty,
                              TSMInstalled = c.TSMInstalled ?? string.Empty,
                              Comments = c.Comments ?? string.Empty,
                              LastBootTime = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(c.LastBootTime),
                              //  Domain = c.Domain ?? null,
                              IsMonitor = c.IsMonitor ?? false,
                              IsDeleted = c.IsDeleted,
                              StatusTypeId = c.StatusTypeId,
                              StatusTypeChangedOn = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(c.StatusTypeChangedOn),
                              NewServerId = c.NewServerId,
                              StatusText = c.StatusText ?? string.Empty,
                              ApplicationUse = c.ApplicationUse ?? string.Empty
                          }).ToList();
            return result;
        }

        public List<AppsListModel> GetAllAppsByServerId(int ServerId)
        {
            var appList = _IHiradServerRepository.GetAllAppsByServerId(ServerId);
            return appList;
        }

        #region InterfaceImplementation
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


        #endregion InterfaceImplementation


        public IEnumerable<HiradServerModel> GetAllMonitoringServers()
        {
            var recs = _IHiradServerRepository.GetAllMonitoringServers();
            return recs;
        }


        public SystemInformationModel GetSystemInformation(string systemName)
        {
            SystemInformationModel objSysInfo = new SystemInformationModel();
            return Utility.GetSystemInformation(systemName, objSysInfo);
        }

        public bool UpdateMonitorInfo(string Ids, string source)
        {
            return _IHiradServerRepository.UpdateMonitorInfo(Ids, source);
        }

        public void UpdateAllSystemInformation()
        {
            var lstMonitoringServers = GetAllMonitoringServers();
            //var lstMonitoringServers = GetServerAll();

            SystemInformationModel objSysInfo = new SystemInformationModel();
            if (lstMonitoringServers != null && lstMonitoringServers.Any())
            {
                foreach (var objServer in lstMonitoringServers)
                {
                    objSysInfo = GetSystemInformation(objServer.SystemName.Trim());
                    if (objSysInfo != null && string.IsNullOrEmpty(objSysInfo.ErrorInfo))
                    {
                        //Update System Information

                        var entity = _IHiradServerRepository.GetById(objServer.Id);
                        if (entity != null)
                        {
                            entity.IPAddress = objSysInfo.IPAddress;
                            entity.Platform = objSysInfo.OsName;
                            entity.BuildVersion = objSysInfo.OsVersion;
                            entity.LastBootTime = objSysInfo.LastBootTime;
                            entity.Model = objSysInfo.SystemModel;
                            entity.Processor = objSysInfo.Processor;
                            entity.TotalCores = objSysInfo.TotalCores;
                            entity.RAM = objSysInfo.TotalRAM;
                            //  entity.Domain = objSysInfo.Domain;
                            entity.HDDConfiguration = objSysInfo.TotalHDD;
                            entity.IsMonitor = true;

                            _IHiradServerRepository.Update(entity);
                            _IHiradServerRepository.SaveChanges();
                        }
                    }
                    else
                    {
                        var entity = _IHiradServerRepository.GetById(objServer.Id);
                        if (entity != null)
                        {
                            entity.IsMonitor = false;

                            _IHiradServerRepository.Update(entity);
                            _IHiradServerRepository.SaveChanges();
                        }
                    }
                }
            }
        }



        public void UpdateServerStatus(int id, int statusTypeId)
        {
            var entity = _IHiradServerRepository.GetById(id);

            if (entity != null)
            {
                entity.StatusTypeId = statusTypeId;
                entity.StatusTypeChangedOn = DateTime.Now;
                _IHiradServerRepository.Update(entity);
                _IHiradServerRepository.SaveChanges();
            }
        }
    }
}
