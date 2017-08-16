using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.Dashboard.Web.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradServerRAMController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradServerLogBLL _hiradServerLogBLL = null;

        public struct ChartType
        {
            public const string AllCriticalRAM = "AllCriticalRAM";
            public const string ServerRAMByDate = "ServerRAMByDate";
        }

        public HiradServerRAMController(IUserDetailBLL usersBLL,
            IHiradServerLogBLL hiServerLogBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradServerLogBLL = hiServerLogBLL;
        }
        //
        // GET: /HiradServerRAM/
        public JsonResult GetCriticalRAMCount()
        {
            DashboardTopRowModel objModel = new DashboardTopRowModel();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRAMPercentageByLastRun();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                int thresold = ApplicationConstants.GetRamThreshold();
                objModel.RAMCount = lstLastRunStatus.FindAll(p => p.RAMPercentage > thresold).Count();
                objModel.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(lstLastRunStatus[0].LoggedAt.Value);
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCriticalRAM()
        {
            List<LogStatusByLastRunModel> lstCriticalRam = new List<LogStatusByLastRunModel>();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRAMPercentageByLastRun();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                int thresold = ApplicationConstants.GetRamThreshold();
                lstCriticalRam = lstLastRunStatus.FindAll(p => p.RAMPercentage > thresold);
            }
            return PartialView(lstCriticalRam);
        }

        public JsonResult GetAllRAMStatusByLastRun()
        {
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRAMPercentageByLastRun();
            return Json(lstLastRunStatus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRAMChartData(string chartName, int? serverId)
        {
            switch (chartName)
            {
                case ChartType.AllCriticalRAM:
                    ChartByOneDimension recs = new ChartByOneDimension();
                    recs = GetAllCriticalRAMBarChart();
                    return Json(recs, JsonRequestBehavior.AllowGet);
                case ChartType.ServerRAMByDate:
                    DailyStatusChartModel recs1 = new DailyStatusChartModel();
                    List<GetServerLogStatusByDateModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRAMPercentageByDate(DateTime.Now);
                    //recs = GetChartByAllCriticalRAM_ByDate(lstLastRunStatus, serverId);
                    recs1 = GetChartByAllCriticalRAM_old(lstLastRunStatus, serverId);
                    return Json(recs1, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        private ChartByOneDimension GetAllCriticalRAMBarChart()
        {
            ChartByOneDimension recs = new ChartByOneDimension();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRAMPercentageByLastRun();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                int thresold = ApplicationConstants.GetRamThreshold();
                var lstCriticalRam = lstLastRunStatus.FindAll(p => p.RAMPercentage > thresold);
                if (lstCriticalRam != null && lstCriticalRam.Any())
                {
                    recs = GetChartByAllCriticalRAM_BarChart(lstCriticalRam);
                }
            }

            return recs;
        }

        private ChartByOneDimension GetChartByAllCriticalRAM_BarChart(List<LogStatusByLastRunModel> lstServerLogs)
        {
            ChartByOneDimension objDailyChart = new ChartByOneDimension()
            {
                labels = new List<string>(),
                series = new List<ChartDataModel>(),
            };

            if (lstServerLogs != null && lstServerLogs.Any())
            {
                List<ChartDataModel> lstServerData = new List<ChartDataModel>();

                objDailyChart.uptime = lstServerLogs.Average(l => l.StatusPercent);
                objDailyChart.labels = (from log in lstServerLogs select log.SystemName.ToString()).Distinct().ToList();


                //Select the Distinct Server Names to prepare chart data
                var serversList = (from l in lstServerLogs select l.ServerId).Distinct().ToList();
                foreach (var distinctServer in serversList)
                {
                    lstServerData = new List<ChartDataModel>();
                    var serverLogList = lstServerLogs.FindAll(l => l.ServerId == distinctServer);
                    //Loop through each Server
                    foreach (var objServer in serverLogList)
                    {
                        //Add the each server log into series
                        objDailyChart.series.Add(new ChartDataModel()
                        {
                            meta = objServer.SystemName,
                            value = Convert.ToInt32(objServer.RAMPercentage)
                        });

                        objDailyChart.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(objServer.LoggedAt.Value);

                    }
                    //Add the each server log into series
                    // objDailyChart.series.Add(lstServerData);
                }
            }

            return objDailyChart;
        }

        private DailyStatusChartModel GetChartByAllCriticalRAM_old(
                List<GetServerLogStatusByDateModel> logStatusByDate, int? serverId)
        {
            DailyStatusChartModel objDailyChart = new DailyStatusChartModel()
            {
                labels = new List<string>(),
                series = new List<List<ChartDataModel>>()
            };

            List<GetServerLogStatusByDateModel> lstServerLogs = null;
            if (serverId.HasValue)
            {
                lstServerLogs = logStatusByDate.FindAll(l => l.ServerId == serverId.Value);
            }
            else
            {
                lstServerLogs = logStatusByDate;
            }
            if (lstServerLogs != null && lstServerLogs.Any())
            {
                List<ChartDataModel> lstServerData = new List<ChartDataModel>();

                objDailyChart.uptime = lstServerLogs.Average(l => l.StatusPercent);
                objDailyChart.labels = (from log in lstServerLogs select log.MonitoredTime.ToString()).Distinct().ToList();

                ////Skip the Label to display by 2
                //if (objDailyChart.labels.Count() > 2)
                //{
                //    for (int ivlLoop = 1; ivlLoop < objDailyChart.labels.Count() - 1; ivlLoop += 2)
                //    {
                //        objDailyChart.labels[ivlLoop] = string.Empty;
                //    }
                //}

                //Select the Distinct Server Names to prepare chart data
                var serversList = (from l in lstServerLogs select l.ServerId).Distinct().ToList();
                foreach (var distinctServer in serversList)
                {
                    lstServerData = new List<ChartDataModel>();
                    var serverLogList = lstServerLogs.FindAll(l => l.ServerId == distinctServer);
                    //Loop through each Server
                    foreach (var objServer in serverLogList)
                    {
                        //Add the each server log into series
                        lstServerData.Add(new ChartDataModel()
                        {
                            meta = objServer.SystemName + " " + objServer.MonitoredTime,
                            value = objServer.RAMPercentage
                        });

                        objDailyChart.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(objServer.LoggedAt.Value);
                    }
                    //Add the each server log into series
                    objDailyChart.series.Add(lstServerData);
                }
            }

            return objDailyChart;

        }


        private ChartByOneDimension GetChartByAllCriticalRAM_ByDate(
               List<GetServerLogStatusByDateModel> logStatusByDate, int? serverId)
        {
            ChartByOneDimension objDailyChart = new ChartByOneDimension()
            {
                labels = new List<string>(),
                series = new List<ChartDataModel>(),
            };

            List<GetServerLogStatusByDateModel> lstServerLogs = null;
            if (serverId.HasValue)
            {
                lstServerLogs = logStatusByDate.FindAll(l => l.ServerId == serverId.Value);
            }
            else
            {
                lstServerLogs = logStatusByDate;
            }
            if (lstServerLogs != null && lstServerLogs.Any())
            {
                List<ChartDataModel> lstServerData = new List<ChartDataModel>();

                objDailyChart.uptime = lstServerLogs.Average(l => l.StatusPercent);
                objDailyChart.labels = (from log in lstServerLogs select log.SystemName.ToString()).Distinct().ToList();

                //Select the Distinct Server Names to prepare chart data
                var serversList = (from l in lstServerLogs select l.ServerId).Distinct().ToList();
                foreach (var distinctServer in serversList)
                {
                    lstServerData = new List<ChartDataModel>();
                    var serverLogList = lstServerLogs.FindAll(l => l.ServerId == distinctServer);
                    //Loop through each Server
                    foreach (var objServer in serverLogList)
                    {
                        //Add the each server log into series
                        objDailyChart.series.Add(new ChartDataModel() { meta = objServer.SystemName, value = objServer.RAMPercentage });

                        objDailyChart.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(objServer.LoggedAt.Value);

                    }
                    //Add the each server log into series
                }
            }

            return objDailyChart;

        }
    }
}