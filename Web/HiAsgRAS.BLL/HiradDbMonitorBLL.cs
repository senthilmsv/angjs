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
    public class HiradDbMonitorBLL : IHiradDbMonitorBLL
    {
        private IHiradDbMonitorRepository _IHiradDbMonitorRepository;
        private IHiradDbMonitorLogRepository _IHiradDbMonitorLogRepository;

        public HiradDbMonitorBLL(IHiradDbMonitorRepository hiradDbMonitorRepository, IHiradDbMonitorLogRepository hiradDbMonitorLogRepository)
        {
            _IHiradDbMonitorRepository = hiradDbMonitorRepository;
            _IHiradDbMonitorLogRepository=hiradDbMonitorLogRepository;
        }
        public IEnumerable<HiradDbMonitorModel> GetDBServerAll()
        {
            var recs = _IHiradDbMonitorRepository.GetDBServerAll();
            return recs;
        }

        public List<HiradDbMonitorModel> HiradDbList_procedure()
        {
            HiradDbList_Result objEntity = new HiradDbList_Result();

            var result = (from c in _IHiradDbMonitorRepository.HiradDbList_Procedure()
                          where c.IsDeleted == false
                          select new HiradDbMonitorModel
                          {
                              Id = c.Id,
                              Application = c.Application ?? string.Empty,
                              DBServer = c.DBServer ?? string.Empty,
                              DbName = c.DbName ?? string.Empty,
                              StatusText = c.StatusText ?? string.Empty,
                              SupportStaff = c.SupportStaff ?? string.Empty
                          }).ToList();

            return result;
        }

        public bool CheckDuplicateDatabase(HiradDbMonitorModel hiradDbMonitorModel)
        {
            return _IHiradDbMonitorRepository.CheckDuplicateDatabase(hiradDbMonitorModel);
        }

        public int UpdateDb(HiradDbMonitorModel hiradDbMonitorModel)
        {
            int AppId = hiradDbMonitorModel.Id;

            var appEntity = MappingHelper.MappingHelper.MapDbMonitorModelToEntity(hiradDbMonitorModel);

            if (appEntity.Id > 0)
            {
                //Update existing server details
                _IHiradDbMonitorRepository.Update(appEntity);
            }
            else
            {
                _IHiradDbMonitorRepository.Add(appEntity);

            }

            _IHiradDbMonitorRepository.SaveChanges();

            return AppId;
        }

        public HiradDbMonitorModel GetDbDetails(int id)
        {
            GetDBDetailsById_Result result = new GetDBDetailsById_Result();

            result = _IHiradDbMonitorRepository.GetDbDetails(id);
            
            return new HiradDbMonitorModel
            {
                Id = result.Id,
                Application = result.Application ?? string.Empty,
                DbName = result.DbName ?? string.Empty,
                DBServer = result.DBServer ?? string.Empty,
                DbServerId = result.DbServerId,
                StatusTypeId = result.StatusTypeId,
                StatusTypeChangedOn =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(result.StatusTypeChangedOn),
                IsMonitor = result.IsMonitor,
                ModifiedBy = result.ModifiedBy ?? string.Empty,
                ModifiedDate =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(result.ModifiedDate),
                CreatedBy = result.CreatedBy ?? string.Empty,
                CreatedDate =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(result.CreatedDate),
                SupportStaff = result.SupportStaff ?? string.Empty,
                DBServerName = result.SystemName ?? string.Empty

            };
        }
        public void DeleteDb(int id, string modified)
        {
           
            var entity = _IHiradDbMonitorRepository.GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
              //  entity.ModifiedBy = modified;
              //  entity.ModifiedDate = DateTime.Now;
                _IHiradDbMonitorRepository.Update(entity);
                _IHiradDbMonitorRepository.SaveChanges();
            }
        }

        public List<DbMonitorLogStatusByLastRunModel> GetAllDBLogStatusByLastRun()
        {
            List<DbMonitorLogStatusByLastRunModel> lstLastRun = new List<DbMonitorLogStatusByLastRunModel>();
            List<GetAllDBLogStatusByLastRun_Result> recs =
                _IHiradDbMonitorLogRepository.GetAllDBLogStatusByLastRun();
            if (recs != null)
            {
                DbMonitorLogStatusByLastRunModel objModel = null;
                foreach (var objRow in recs)
                {
                    objModel = new DbMonitorLogStatusByLastRunModel()
                    {
                        DbMonitorId = objRow.DbMonitorId,
                        Application = objRow.Application,
                        DbName = objRow.DbName,
                        DBServerName = objRow.DBServerName,                      
                        Status = objRow.Status,
                        ErrorDescription = objRow.ErrorDescription,
                        MonitoredAt = objRow.MonitoredAt.Value.ToString("MM/dd/yyyy hh:mm tt"),
                        LoggedAt = objRow.LoggedAt
                    };
                    lstLastRun.Add(objModel);
                }
            }

            return lstLastRun;
        }

        public HiradDbMonitorModel GetAllDBMonitorLogsByDB(int id)
        {
            HiradDbMonitorModel objDbMonitorModel = GetDbDetails(id);
            if (objDbMonitorModel != null)
            {
                //Log Rows
                var lstEntityRows = _IHiradDbMonitorLogRepository.GetAll(x => x.DbMonitorId == id);
                objDbMonitorModel.HiradDbMonitorLogs = new List<HiradDbMonitorLogModel>();

                HiradDbMonitorLogModel objModel = new HiradDbMonitorLogModel();
                foreach (var objRow in lstEntityRows)
                {
                    objModel = new HiradDbMonitorLogModel()
                    {
                        Id = objRow.Id,
                        DbMonitorId = objRow.DbMonitorId,
                        Status = objRow.Status,
                        ErrorDescription = objRow.ErrorDescription,
                        MonitoredAt = objRow.MonitoredAt.Value.ToString("MM/dd/yyyy hh:mm tt"),
                        LoggedAt = objRow.LoggedAt
                    };
                    objDbMonitorModel.HiradDbMonitorLogs.Add(objModel);
                }
            }
            return objDbMonitorModel;
        }

        #region InterfaceImplementation
        public HiradDbMonitorModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(HiradDbMonitorModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(HiradDbMonitorModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(HiradDbMonitorModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<HiradDbMonitorModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<HiradDbMonitorModel> GetQueryable()
        {
            throw new NotImplementedException();
        }


        #endregion InterfaceImplementation

    }
}
