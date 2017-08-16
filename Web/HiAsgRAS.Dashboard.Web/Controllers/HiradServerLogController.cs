using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Dashboard.Web.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradServerLogController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradServerBLL _hiradServerBLL = null;
        IHiradServerLogBLL _hiradServerLogBLL = null;

        public struct ChartType
        {
            public const string WeekDayName = "WeekDayName";
            public const string AllServersByDates = "AllServersByDates";
            public const string SelectedServerByDates = "SelectedServerByDate";
            public const string LastRunAllServers = "LastRunAllServers";
            public const string ServerByDate = "ServerByDate";
        }


        public HiradServerLogController(IUserDetailBLL usersBLL,
            IHiradServerBLL hiradServerBLL,
            IHiradServerLogBLL hiServerLogBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradServerBLL = hiradServerBLL;
            _hiradServerLogBLL = hiServerLogBLL;
        }

        //Partial View to Load popup Chart 
        public ActionResult ViewAllServerLogChart()
        {
            return PartialView();
        }

        //Partial View to Load popup data
        public ActionResult ViewServerLog(string serverName, int? serverId)
        {
            var recs = _hiradServerLogBLL.GetAllLogsByServer(serverName, serverId);
            return PartialView(recs);
        }

        public JsonResult GetOverAllServerStatus()
        {
            DashboardTopRowModel objModel = new DashboardTopRowModel();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllServerLogStatusByLastRun();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                objModel.uptime = Math.Round(lstLastRunStatus.Average(l => l.StatusPercent), 2);
                objModel.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(lstLastRunStatus[0].LoggedAt.Value);
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllServerLogStatusByLastRun()
        {
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllServerLogStatusByLastRun();
            return Json(lstLastRunStatus, JsonRequestBehavior.AllowGet);
        }

        #region Chart Log Status
        public JsonResult GetChartData(int? serverId, System.DateTime? fromDate,
                                        System.DateTime? toDate, string chartName,
                                        System.DateTime? loggedAt)
        {
            DailyStatusChartModel recs = new DailyStatusChartModel();
            List<HiradServerLogStatusModel> lstLogStatus = new List<HiradServerLogStatusModel>();
            switch (chartName)
            {
                case ChartType.WeekDayName:
                    lstLogStatus = _hiradServerLogBLL.GetAllServerLogStatus(serverId, fromDate, toDate);
                    recs = GetChartLogStatusCountByWeekDayName(lstLogStatus);
                    break;
                case ChartType.AllServersByDates:
                    lstLogStatus = _hiradServerLogBLL.GetAllServerLogStatus(serverId, fromDate, toDate);
                    recs = GetChartLogStatusCountByAllServer(lstLogStatus);
                    break;
                case ChartType.SelectedServerByDates:
                    lstLogStatus = _hiradServerLogBLL.GetAllServerLogStatus(serverId, fromDate, toDate);
                    recs = GetChartLogStatusCountByAllServer(lstLogStatus);
                    break;
                case ChartType.LastRunAllServers:
                    //Need to Show all the Servers Last Run Status                     
                    List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllServerLogStatusByLastRun();
                    var recsLastRun = GetChartByLastRun(lstLastRunStatus);
                    return Json(recsLastRun, JsonRequestBehavior.AllowGet);

                case ChartType.ServerByDate:
                    //Need to Show the All the log status based on the date
                    List<GetServerLogStatusByDateModel> logStatusByDate = _hiradServerLogBLL.GetServerLogStatusByDate(DateTime.Now);
                    var reclogStatusByDate = GetChartLogStatusByDate(logStatusByDate, serverId);
                    return Json(reclogStatusByDate, JsonRequestBehavior.AllowGet);
            }
            return Json(recs, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Prepare Charts
        private DailyStatusChartModel GetChartLogStatusCountByWeekDayName(List<HiradServerLogStatusModel> lstLogStatus)
        {
            DailyStatusChartModel objDailyChart = new DailyStatusChartModel()
            {
                labels = new List<string>(),
                series = new List<List<ChartDataModel>>()
            };

            List<ChartDataModel> lstServerData = new List<ChartDataModel>();

            if (lstLogStatus != null && lstLogStatus.Any())
            {
                //Distinct Label Week Day Names
                objDailyChart.labels = (from log in lstLogStatus select log.DayName).Distinct().ToList();

                //Select the Distinct Server Names to prepare chart data
                var serversList = (from l in lstLogStatus select l.SystemName).Distinct().ToList();
                foreach (var serverName in serversList)
                {
                    //Loop through each Server
                    lstServerData = new List<ChartDataModel>();
                    var serverLogList = lstLogStatus.FindAll(l => l.SystemName.Contains(serverName));
                    foreach (var wkdayName in objDailyChart.labels)
                    {
                        var objServerLog = serverLogList.Find(l => l.DayName.Contains(wkdayName));
                        if (objServerLog == null)
                        {
                            //Add Default value
                            lstServerData.Add(new ChartDataModel() { meta = serverName, value = 1 });
                        }
                        else
                        {
                            //Map the value into series array
                            lstServerData.Add(new ChartDataModel()
                            {
                                meta = serverName,
                                value = objServerLog.Server_Uptime.HasValue
                                ? objServerLog.Server_Uptime.Value : 1
                            });
                        }
                    }
                    //Add the each server log into series
                    objDailyChart.series.Add(lstServerData);
                }
                //Trim the Week day name into 3 chars
                objDailyChart.labels = (from log in lstLogStatus select log.DayName.Substring(0, 3)).Distinct().ToList();

            }

            return objDailyChart;
        }

        private DailyStatusChartModel GetChartLogStatusCountByAllServer(List<HiradServerLogStatusModel> lstLogStatus)
        {
            DailyStatusChartModel objDailyChart = new DailyStatusChartModel()
            {
                labels = new List<string>(),
                series = new List<List<ChartDataModel>>()
            };

            List<ChartDataModel> lstServerData = new List<ChartDataModel>();

            if (lstLogStatus != null && lstLogStatus.Any())
            {
                //Distinct Label Week Day Names
                objDailyChart.labels = (from log in lstLogStatus select log.LoggedOn.Value + "").Distinct().ToList();

                //Select the Distinct Server Names to prepare chart data
                var serversList = (from l in lstLogStatus select l.SystemName).Distinct().ToList();
                foreach (var serverName in serversList)
                {
                    //Loop through each Server
                    lstServerData = new List<ChartDataModel>();
                    var serverLogList = lstLogStatus.FindAll(l => l.SystemName.Contains(serverName));
                    foreach (var wkdayName in objDailyChart.labels)
                    {
                        var objServerLog = serverLogList.Find(l => l.LoggedOn.Value.ToString().Contains(wkdayName));
                        if (objServerLog == null)
                        {
                            //Add Default value
                            lstServerData.Add(new ChartDataModel() { meta = serverName, value = 1 });
                        }
                        else
                        {
                            //Map the value into series array
                            lstServerData.Add(new ChartDataModel()
                            {
                                meta = serverName,
                                value = objServerLog.Server_Uptime.HasValue
                                ? objServerLog.Server_Uptime.Value : 1
                            });
                        }
                    }
                    //Add the each server log into series
                    objDailyChart.series.Add(lstServerData);
                }
                //Trim the Week day name into 3 chars
                objDailyChart.labels = (from log in lstLogStatus select log.LoggedOn.Value.ToString("MM/dd")).Distinct().ToList();

            }

            return objDailyChart;
        }

        private ChartByOneDimension GetChartByLastRun(List<LogStatusByLastRunModel> lstLastRunStatus)
        {
            ChartByOneDimension objDailyChart = new ChartByOneDimension()
            {
                labels = new List<string>(),
                series = new List<ChartDataModel>()
            };
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                objDailyChart.labels = (from log in lstLastRunStatus select log.SystemName.ToString()).ToList();
                objDailyChart.uptime = Math.Round(lstLastRunStatus.Average(l => l.StatusPercent),2);
                //Select the Percentage data to Prepare Chart
                foreach (var objServer in lstLastRunStatus)
                {
                    //Add the each server log into series
                    objDailyChart.series.Add(new ChartDataModel() { meta = objServer.SystemName, value = objServer.StatusPercent });

                    objDailyChart.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(objServer.LoggedAt.Value);
                }
            }

            return objDailyChart;
        }

        private DailyStatusChartModel GetChartLogStatusByDate(List<GetServerLogStatusByDateModel> logStatusByDate,
                                                int? serverId)
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

                objDailyChart.uptime = Math.Round(lstServerLogs.Average(l => l.StatusPercent),2);
                objDailyChart.labels = (from log in lstServerLogs select log.MonitoredTime.ToString()).ToList();

                ////Skip the Label to display by 2
                //if (objDailyChart.labels.Count() > 2)
                //{
                //    for (int ivlLoop = 1; ivlLoop < objDailyChart.labels.Count() - 1; ivlLoop += 2)
                //    {
                //        objDailyChart.labels[ivlLoop] = string.Empty;
                //    }
                //}


                //Loop through each Server
                foreach (var objServer in lstServerLogs)
                {
                    //Add the each server log into series
                    lstServerData.Add(new ChartDataModel()
                    {
                        meta = objServer.SystemName + " " + objServer.MonitoredTime,
                        value = objServer.StatusPercent
                    });

                    objDailyChart.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(objServer.LoggedAt.Value);
                }
                //Add the each server log into series
                objDailyChart.series.Add(lstServerData);
            }

            return objDailyChart;
        }
        #endregion

        #region R and D
        public DailyStatusChartModel GetChartDataModel()
        {
            //var recs = GetChartDataModel();
            //var recs = new
            //{
            //    labels = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
            //    //series = new[] {
            //    //                new [] {12, 17, 70, 17, 23, 18, 68},
            //    //                new [] {32, 27, 80, 37, 83, 28, 98}
            //    //                }
            //    series = new[] 
            //    {
            //        new [] {
            //            new { meta = "HiMoaFact041", value = 99 }, 
            //            new { meta = "HiMoaFact041", value = 10 },
            //            new { meta = "HiMoaFact041", value = 95 },
            //            new { meta = "HiMoaFact041", value = 80 },
            //            new { meta = "HiMoaFact041", value = 99 },
            //            new { meta = "HiMoaFact041", value = 16 },
            //            new { meta = "HiMoaFact041", value = 39 }
            //        },
            //        new [] {
            //            new { meta = "HiMoaFact042", value = 89 }, 
            //            new { meta = "HiMoaFact042", value = 20 },
            //            new { meta = "HiMoaFact042", value = 85 },
            //            new { meta = "HiMoaFact042", value = 90 },
            //            new { meta = "HiMoaFact042", value = 79 },
            //            new { meta = "HiMoaFact042", value = 46 },
            //            new { meta = "HiMoaFact042", value = 79 }
            //        }
            //    }
            //};

            DailyStatusChartModel objDailyChart = new DailyStatusChartModel()
            {
                labels = new List<string>(),
                series = new List<List<ChartDataModel>>()
            };

            objDailyChart.labels.Add("Mon");
            objDailyChart.labels.Add("Tue");
            objDailyChart.labels.Add("Wed");
            objDailyChart.labels.Add("Thu");
            objDailyChart.labels.Add("Fri");
            objDailyChart.labels.Add("Sat");
            objDailyChart.labels.Add("Sun");

            List<ChartDataModel> lstServerData = new List<ChartDataModel>();
            lstServerData.Add(new ChartDataModel() { meta = "HiMoaFact041", value = 1 });
            lstServerData.Add(new ChartDataModel() { meta = "HiMoaFact041", value = 1 });
            lstServerData.Add(new ChartDataModel() { meta = "HiMoaFact041", value = 1 });
            lstServerData.Add(new ChartDataModel() { meta = "HiMoaFact041", value = 80 });
            lstServerData.Add(new ChartDataModel() { meta = "HiMoaFact041", value = 99 });
            lstServerData.Add(new ChartDataModel() { meta = "HiMoaFact041", value = 16 });
            lstServerData.Add(new ChartDataModel() { meta = "HiMoaFact041", value = 39 });
            objDailyChart.series.Add(lstServerData);


            lstServerData = new List<ChartDataModel>();
            lstServerData.Add(new ChartDataModel() { meta = "hidscfact057", value = 99 });
            lstServerData.Add(new ChartDataModel() { meta = "hidscfact057", value = 100 });
            lstServerData.Add(new ChartDataModel() { meta = "hidscfact057", value = 85 });
            lstServerData.Add(new ChartDataModel() { meta = "hidscfact057", value = 90 });
            lstServerData.Add(new ChartDataModel() { meta = "hidscfact057", value = 79 });
            lstServerData.Add(new ChartDataModel() { meta = "hidscfact057", value = 100 });
            lstServerData.Add(new ChartDataModel() { meta = "hidscfact057", value = 39 });
            objDailyChart.series.Add(lstServerData);

            return objDailyChart;
        }

        #endregion
    }
}