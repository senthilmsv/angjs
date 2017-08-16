using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;

namespace HiAsgRAS.BLL
{
    public class HiradServerLogBLL : IHiradServerLogBLL
    {

        private IHiradServerLogRepository _IHiradServerLogRepository;
        private IHiradServerRepository _IHiradServerRepository;

        public HiradServerLogBLL(IHiradServerLogRepository hiradServerLogRepository,
            IHiradServerRepository hiradServerRepository)
        {
            _IHiradServerLogRepository = hiradServerLogRepository;
            _IHiradServerRepository = hiradServerRepository;
        }

        public List<HiradServerLogStatusModel> GetAllServerLogStatus(
                int? serverId, System.DateTime? fromDate, System.DateTime? toDate)
        {
            List<HiradServerLogStatusModel> lstHiSrvrLogStatus = new List<ViewModel.HiradServerLogStatusModel>();
            var recs = _IHiradServerLogRepository.GetAllServerLogStatus(serverId, fromDate, toDate);
            if (recs != null)
            {
                HiradServerLogStatusModel objModel = null;
                foreach (var objRow in recs)
                {
                    objModel = new HiradServerLogStatusModel()
                    {
                        ServerId = objRow.ServerId,
                        SystemName = objRow.SystemName,
                        LoggedOn = objRow.Logged,
                        DayName = objRow.DayName,
                        Total_count = objRow.Total_count,
                        success_count = objRow.success_count,
                        Failed_count = objRow.Failed_count,
                        Server_Uptime = objRow.Server_Uptime
                    };

                    lstHiSrvrLogStatus.Add(objModel);
                }
                
            }
            
            return lstHiSrvrLogStatus;
        }

        public List<LogStatusByLastRunModel> GetAllServerLogStatusByLastRun()
        {
            List<LogStatusByLastRunModel> lstLastRun = new List<LogStatusByLastRunModel>();
            List<GetAllServerLogStatusByLastRun_Result> recs = _IHiradServerLogRepository.GetAllServerLogStatusByLastRun();
            if (recs != null )
            {
                LogStatusByLastRunModel objModel = null;
                foreach (var objRow in recs)
                {
                    objModel = new LogStatusByLastRunModel()
                    {
                        AvblHDDSpace = objRow.AvblHDDSpace,
                        AvblRAM = objRow.AvblRAM,
                        ErrorDescription = objRow.ErrorDescription,
                        LoggedAt = objRow.LoggedAt,
                        MonitoredAt = objRow.MonitoredAt.Value.ToString("MM/dd/yyyy hh:mm tt"),
                        ServerId = objRow.ServerId,
                        Status = objRow.Status,
                        StatusPercent = objRow.StatusPercent,
                        SystemName = objRow.SystemName,
                        HDDConfiguration = objRow.HDDConfiguration,
                        RAM = objRow.RAM,
                        LastBootTime = objRow.LastBootTime.HasValue ? objRow.LastBootTime : DateTime.Now 
                    };
                    lstLastRun.Add(objModel);
                }
            }

            return lstLastRun;
        }

 
        #region Interface Implementation
        public ViewModel.HiradServerLogModel GetById(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ViewModel.HiradServerLogModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ViewModel.HiradServerLogModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ViewModel.HiradServerLogModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IList<ViewModel.HiradServerLogModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public System.Linq.IQueryable<ViewModel.HiradServerLogModel> GetQueryable()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        public List<GetServerLogStatusByDateModel> GetServerLogStatusByDate(System.DateTime? LoggedAt)
        {
            List<GetServerLogStatusByDateModel> lstReturn = new List<GetServerLogStatusByDateModel>();

            var recs = _IHiradServerLogRepository.GetServerLogStatusByDate(LoggedAt);
            if (recs != null)
            {
                foreach (var objEntity in recs)
                {
                    lstReturn.Add(new GetServerLogStatusByDateModel()
                    {
                        AvblHDDSpace = objEntity.AvblHDDSpace,
                        AvblRAM = objEntity.AvblRAM,
                        ErrorDescription = objEntity.ErrorDescription,
                        LoggedAt = objEntity.LoggedAt,
                        MonitoredAt = objEntity.MonitoredAt,
                        MonitoredTime = objEntity.MonitoredTime,
                        ServerId = objEntity.ServerId,
                        Status = objEntity.Status,
                        StatusPercent = objEntity.StatusPercent,
                        SystemName = objEntity.SystemName,
                        RAM = objEntity.RAM
                    });
                }
            }

            return lstReturn;
        }


        public HiradServerModel GetAllLogsByServer(string serverName, int? serverId)
        {
            HiradServer objHiradServerEntity = new HiradServer();
            
            if (serverId.HasValue)
            {
                objHiradServerEntity = _IHiradServerRepository.GetByIntId(serverId.Value);
                
            }
            else if (!string.IsNullOrEmpty(serverName))
            {
                objHiradServerEntity = _IHiradServerRepository.Find(x => x.SystemName.Contains(serverName));
            }

            HiradServerModel objHiradServerModel = new HiradServerModel();
            if (objHiradServerEntity != null)
            {
                objHiradServerModel = MappingHelper.MappingHelper.MapSeverEntityToViewModel(objHiradServerEntity);

                //Log Rows
                var lstEntityRows = _IHiradServerLogRepository.GetAll(x => x.ServerId == objHiradServerModel.Id);
                objHiradServerModel.HiradServerLogs = MappingHelper.MappingHelper.MapHiradServerLogEntitiesToModels(lstEntityRows);

                //Apps List

                objHiradServerModel.AppsList = new List<AppsListModel>();
                var lstAppsEntities = _IHiradServerRepository.GetAllAppsByServerId(objHiradServerModel.Id);
                //objHiradServerModel.AppsList = MappingHelper.MappingHelper.MapGetAllAppsEntitiesToViewModel(lstAppsEntities);
                objHiradServerModel.AppsList = lstAppsEntities;
            }

            return objHiradServerModel;
        }

        public List<GetServerLogStatusByDateModel> GetAllRAMPercentageByDate(DateTime? LoggedAt)
        {
            decimal totalRam = 0, avblRam = 0, FREE_PERCENT = 0, USED_PERCENT = 0;
            List<GetServerLogStatusByDateModel> lstLastRun = GetServerLogStatusByDate(LoggedAt);
            
            foreach (var objRam in lstLastRun)
            {
                totalRam = 0; avblRam = 0;
                if (!string.IsNullOrEmpty(objRam.RAM))
                {
                    totalRam = ConvertRAMText2Number(objRam.RAM);
                    objRam.RAM = objRam.RAM.Contains("GB") ? objRam.RAM : objRam.RAM + " GB";
                }
                if (!string.IsNullOrEmpty(objRam.AvblRAM))
                {
                    avblRam = ConvertRAMText2Number(objRam.AvblRAM);
                }
                if (totalRam > 0 && avblRam > 0)
                {
                    FREE_PERCENT = ((100 * avblRam / totalRam));

                    USED_PERCENT = ((100 - FREE_PERCENT));


                    objRam.RAMPercentage = (int)Math.Round(USED_PERCENT, 0);
                }
                else
                {
                    objRam.RAMPercentage = 100;
                }
            }

            return lstLastRun;
        }

        public List<LogStatusByLastRunModel> GetAllRAMPercentageByLastRun()
        {
            decimal totalRam = 0, avblRam = 0, FREE_PERCENT = 0, USED_PERCENT = 0;
            List<LogStatusByLastRunModel> lstLastRun = GetAllServerLogStatusByLastRun();
            foreach (LogStatusByLastRunModel objRam in lstLastRun)
            {
                totalRam = 0; avblRam = 0;
                if (!string.IsNullOrEmpty(objRam.RAM))
                {
                    totalRam = ConvertRAMText2Number(objRam.RAM);
                    objRam.RAM = objRam.RAM.Contains("GB") ? objRam.RAM : objRam.RAM + " GB";
                }
                if (!string.IsNullOrEmpty(objRam.AvblRAM))
                {
                    avblRam = ConvertRAMText2Number(objRam.AvblRAM);
                }
                if (totalRam > 0 && avblRam > 0)
                {
                    FREE_PERCENT = ((100 * avblRam / totalRam));

                    USED_PERCENT = ((100 - FREE_PERCENT));


                    objRam.RAMPercentage = (int)Math.Round(USED_PERCENT, 0);
                }
                else
                {
                    objRam.RAMPercentage = 100;
                }
            }

            return lstLastRun;
        }

        public int ConvertRAMText2Number(string ramText)
        {
            int ram = 0;
            ramText = ramText.Replace("GB", string.Empty).Trim();

            int.TryParse(ramText, out ram);

            return ram;
        }


        public List<LogStatusByLastRunModel> GetAllHDDPercentage()
        {            
            string AvblHddSpace = string.Empty, HddSpaceConfig = string.Empty;
            List<LogStatusByLastRunModel> lstLastRun = GetAllServerLogStatusByLastRun();
            foreach (LogStatusByLastRunModel objRam in lstLastRun)
            {
                if (!string.IsNullOrEmpty(objRam.AvblHDDSpace) &&
                    (!string.IsNullOrEmpty(objRam.HDDConfiguration)))
                {
                    objRam.HddPercentage = CalculateFreeHddSpace(objRam.AvblHDDSpace, objRam.HDDConfiguration);
                }
            }

            return lstLastRun;
        }

        private decimal CalculateFreeHddSpace(string AvblHDDSpace, string HDDConfiguration)
        {
            decimal FREE_PERCENT = 0, USED_PERCENT = 0;

            int hddThershold = ApplicationConstants.GetHddThreshold();

            //Available HDD
            //C:\90 GB,E:\80 GB,F:\322 GB
            string[] arrAvblHdd = AvblHDDSpace.Split(',');

            List<HddDriveInfo> lstAvblHdd = new List<HddDriveInfo>();
            HddDriveInfo objHddDriveInfo = new HddDriveInfo();
            foreach (string avblDrive in arrAvblHdd)
            {
                string[] arrDriveDet = avblDrive.Split('\\');

                objHddDriveInfo = new HddDriveInfo();
                objHddDriveInfo.Drive = arrDriveDet[0];
                objHddDriveInfo.DriveWithSpace = arrDriveDet[1];
                objHddDriveInfo.SizeInBytes = ConvertText2Bytes(arrDriveDet[1]);

                lstAvblHdd.Add(objHddDriveInfo);
            }

            //Hdd Configuration C:\136GB, E:\279GB, F:\558GB
            string[] arrHddConfig = HDDConfiguration.Split(',');
            List<HddDriveInfo> lstHddConfig = new List<HddDriveInfo>();
            foreach (string drive in arrHddConfig)
            {
                string[] arrDriveDet = drive.Split('\\');

                objHddDriveInfo = new HddDriveInfo();
                objHddDriveInfo.Drive = arrDriveDet[0];
                objHddDriveInfo.DriveWithSpace = arrDriveDet[1];
                objHddDriveInfo.SizeInBytes = ConvertText2Bytes(arrDriveDet[1]);

                lstHddConfig.Add(objHddDriveInfo);
            }

            //Check the Thershold
            decimal totalPercent = 0;

            foreach (HddDriveInfo objAvblDrive in lstAvblHdd)
            {
                HddDriveInfo objMatch = lstHddConfig.Find(d => d.Drive.Contains(objAvblDrive.Drive));
                if (objMatch != null)
                {
                    if (objMatch.SizeInBytes > 0)
                    {
                        FREE_PERCENT = System.Math.Truncate((100 * objAvblDrive.SizeInBytes / objMatch.SizeInBytes));

                        USED_PERCENT = ((100 - FREE_PERCENT));

                        objAvblDrive.UsedPercent = USED_PERCENT;
                        if (USED_PERCENT > hddThershold)
                        {
                            return USED_PERCENT;
                        }
                        totalPercent += objAvblDrive.UsedPercent;
                    }
                }
            }


            return System.Math.Truncate(totalPercent / lstAvblHdd.Count);
        }

        public string ConvertBytes2Text(double D_Totalspace)
        {

            double dCalFreeSpace = 0;
            string curTotalHDDSpace = string.Empty;
            dCalFreeSpace = System.Math.Truncate(D_Totalspace / (1024 * 1024 * 1024));
            if (dCalFreeSpace > 0)
            {
                return dCalFreeSpace + " GB";
            }
            else
            {
                //Convert into MB
                dCalFreeSpace = System.Math.Truncate(D_Totalspace / (1024 * 1024));
                if (dCalFreeSpace > 0)
                {
                    return dCalFreeSpace + " MB";
                }
            }
            return D_Totalspace + " KB";
        }

        public decimal ConvertText2Bytes(string s_Totalspace)
        {
            decimal lCalSpace = 0;
            if (s_Totalspace.Contains("TB"))
            {
                decimal KbBytes = 1024;
                decimal tbBytes = (KbBytes * KbBytes * KbBytes * KbBytes);
                s_Totalspace = s_Totalspace.Replace("TB", "").Trim();
                lCalSpace = (Convert.ToDecimal(s_Totalspace) * tbBytes);
            }
            else if (s_Totalspace.Contains("GB"))
            {
                s_Totalspace = s_Totalspace.Replace("GB", "").Trim();
                lCalSpace = ( Convert.ToDecimal(s_Totalspace) * (1024 * 1024 * 1024));
            }
            else if (s_Totalspace.Contains("MB"))
            {
                s_Totalspace = s_Totalspace.Replace("MB", "").Trim();
                lCalSpace = (Convert.ToDecimal(s_Totalspace) * (1024 * 1024));
            }
            else
            {
                s_Totalspace = s_Totalspace.Replace("KB", "").Trim();
                lCalSpace = (Convert.ToDecimal(s_Totalspace));
            }

            return lCalSpace;
        }


        public List<LogStatusByLastRunModel> GetAllRebootServers()
        {
            string AvblHddSpace = string.Empty, HddSpaceConfig = string.Empty;
            List<LogStatusByLastRunModel> lstLastStatus = GetAllServerLogStatusByLastRun();
            foreach (LogStatusByLastRunModel objStatus in lstLastStatus)
            {
                objStatus.LastBootInDays = RebootDaysCount(objStatus.LastBootTime);
            }

            return lstLastStatus;
        }

        public int RebootDaysCount(DateTime? dtLastBootTime)
        {
            int noOfDays = 0;
            DateTime nullDate = new DateTime(9999, 12, 31, 0,0,0,000);
            if (dtLastBootTime != null && dtLastBootTime.Value.Date != nullDate.Date)
            {
                noOfDays = DateTime.Now.Date.Subtract(dtLastBootTime.Value.Date).Days;
            }

            return noOfDays;
        }



    }
}
