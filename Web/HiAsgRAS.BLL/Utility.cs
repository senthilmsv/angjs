using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HiAsgRAS.ViewModel;
using HiAsgRAS.BLL;

namespace HiAsgRAS.BLL
{
    public class Utility
    {

        #region Get System Information
        public static SystemInformationModel GetSystemInformation(string serverName, SystemInformationModel objSysInfo)
        {
            try
            {   
                //SystemInformation objSysInfo = new SystemInformation();
                GetComputerSystem(serverName, objSysInfo);
                GetOperatingSystem(serverName, objSysInfo);
                GetLogicalDisk(serverName, objSysInfo);
                GetIPAddress(serverName, objSysInfo);
                GetProcessor(serverName, objSysInfo);                
            }
            catch (Exception ex)
            {
                objSysInfo.ErrorInfo = ex.Message.ToString();
                objSysInfo.Ex = ex;
            }
            return objSysInfo;
        }

        private static void GetComputerSystem(string serverName, SystemInformationModel objSysInfo)
        {
            
            try
            {
                string sRAM = string.Empty;
                string strNameSpace = @"\\";

                if (serverName != "")
                    strNameSpace += serverName;
                else
                    strNameSpace += ".";

                strNameSpace += @"\root\cimv2";

                //ManagementObjectSearcher searcher =
                //    new ManagementObjectSearcher(strNameSpace,
                //    "SELECT * FROM Win32_ComputerSystem");

                ManagementScope oMs = GetMSConnetction(strNameSpace);

                ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(oMs, wql);
                ManagementObjectCollection results = searcher.Get();

                foreach (ManagementObject result in results)
                {
                    objSysInfo.ComputerName = result["Name"].ToString();
                    objSysInfo.SystemModel = result["Model"].ToString();
                 //   objSysInfo.Domain = result["Domain"].ToString();
                    objSysInfo.TotalRAM = ConvertKiloBytes2Text(Convert.ToInt64(result["TotalPhysicalMemory"].ToString())); 
                }
            }
            catch (ManagementException ex)
            {
                // exception handling
                throw ex;
            }
        }

        private static ManagementScope GetMSConnetction(string strNameSpace)
        {
            ConnectionOptions oConn = new ConnectionOptions();
            oConn.Impersonation = ImpersonationLevel.Impersonate;
            
            //these should be passed in as well, if to change from machine to machine
            oConn.Username = Common.ApplicationConstants.UserName();
            oConn.Password = Common.ApplicationConstants.Password();
            ManagementScope oMs;
            try
            {
                oMs = new ManagementScope(strNameSpace, oConn);
                oMs.Connect();
                if (oMs.IsConnected)
                {
                    oMs = new ManagementScope(strNameSpace, oConn);
                }
            }
            catch (Exception)
            {                
                oConn = new ConnectionOptions();
                oConn.Impersonation = ImpersonationLevel.Impersonate;

                oMs = new ManagementScope(strNameSpace, oConn);
                oMs.Connect();
                if (oMs.IsConnected)
                {
                    oMs = new ManagementScope(strNameSpace, oConn);
                }
            }
            return oMs;
        }

        private static void GetIPAddress(string serverName, SystemInformationModel objSysInfo)
        {
            IPAddress[] ipaddress = Dns.GetHostAddresses(serverName);
            foreach (IPAddress ipaddr in ipaddress)
            {
                objSysInfo.IPAddress = ipaddr.ToString();
            }
        }

        private static void GetOperatingSystem(string serverName, SystemInformationModel objSysInfo)
        {
            StringBuilder availableMemmory = new StringBuilder();
            //ConnectionOptions oConn = new ConnectionOptions();
            //oConn.Impersonation = ImpersonationLevel.Impersonate;
            //oConn.Authentication = AuthenticationLevel.Default;
            //oConn.EnablePrivileges = true;

            //oConn.Username = "";
            //oConn.Password = "";
            string strNameSpace = @"\\";

            if (serverName != "")
                strNameSpace += serverName;
            else
                strNameSpace += ".";

            strNameSpace += @"\root\cimv2";

            ManagementScope oMs = GetMSConnetction(strNameSpace);

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(oMs, wql);
            ManagementObjectCollection results = searcher.Get();
            
            foreach (ManagementObject result in results)
            {
                objSysInfo.AvblRAM = ConvertKiloBytes2Text(Convert.ToInt64(result["FreePhysicalMemory"].ToString()));

                var str = result["LastBootupTime"] as string;
                var LastBootUpDate = ManagementDateTimeConverter.ToDateTime(str);
                str = result["InstallDate"] as string;
                var installDateString = ManagementDateTimeConverter.ToDateTime(str);

                objSysInfo.LastBootTime = LastBootUpDate;
                objSysInfo.LastBootTimeText = LastBootUpDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                //objSysInfo.OriginalInstallDate = installDateString;
                objSysInfo.OsName = result["Caption"].ToString() + ", " + result["OSArchitecture"].ToString();
                objSysInfo.OsVersion = result["Version"].ToString();
            }
        }

        private static void GetLogicalDisk(string serverName, SystemInformationModel objSysInfo)
        {
            try
            {
                //ConnectionOptions oConn = new ConnectionOptions();
                //oConn.Impersonation = ImpersonationLevel.Impersonate;
                //oConn.Authentication = AuthenticationLevel.Default;
                //oConn.Username = "";
                //oConn.Password = "";
                string strNameSpace = @"\\";

                if (serverName != "")
                    strNameSpace += serverName;
                else
                    strNameSpace += ".";

                strNameSpace += @"\root\cimv2";

                ManagementScope oMs = GetMSConnetction(strNameSpace);
                //ManagementScope oMs = new ManagementScope(strNameSpace, oConn);

                //get Fixed disk state

                ObjectQuery oQuery = new ObjectQuery("select FreeSpace,Size,Name from Win32_LogicalDisk where DriveType=3");

                //Execute the query
                ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);

                //Get the results
                ManagementObjectCollection oReturnCollection = oSearcher.Get();

                //loop through found drives and write out info
                double D_Freespace = 0;
                double D_Totalspace = 0;
                long l_Totalspace = 0, l_Freespace = 0;
                string TotalHDD = string.Empty, AvblHDD = string.Empty, curDrvTotal = "", curDrvAvbl = "";
                string HDDSpaceText = string.Empty;

                foreach (ManagementObject oReturn in oReturnCollection)
                {

                    // Total Size in bytes
                    string strTotalspace = oReturn["Size"].ToString();
                    D_Totalspace = System.Convert.ToDouble(strTotalspace);
                    l_Totalspace = System.Convert.ToInt64(strTotalspace);

                    //Total HDD Space
                    //C:\110GB, E:\279GB, F:\279GB
                    //Convert into GB
                    //HDDSpaceText = ConvertBytes2Text(D_Totalspace);                    

                    HDDSpaceText = ByteSize.SizeSuffix(l_Totalspace, 0);                    
                    curDrvTotal = string.Format(@"{0}\{1}", oReturn["Name"].ToString(), HDDSpaceText);
                    if (TotalHDD != string.Empty)
                    {
                        TotalHDD = TotalHDD + "," + curDrvTotal;
                    }
                    else
                    {
                        TotalHDD = curDrvTotal;
                    }

                    // Available HDD Space in bytes
                    string strFreespace = oReturn["FreeSpace"].ToString();
                    D_Freespace = System.Convert.ToDouble(strFreespace);
                    l_Freespace = System.Convert.ToInt64(strFreespace);

                    //C:\110GB, E:\279GB, F:\279GB
                    //HDDSpaceText = ConvertBytes2Text(D_Freespace);                    
                    HDDSpaceText = ByteSize.SizeSuffix(l_Freespace, 0);                    
                    curDrvAvbl = string.Format(@"{0}\{1}", oReturn["Name"].ToString(), HDDSpaceText);

                    if (AvblHDD != string.Empty)
                    {
                        AvblHDD = AvblHDD + "," + curDrvAvbl;
                    }
                    else
                    {
                        AvblHDD = curDrvAvbl;
                    }
                }

                objSysInfo.TotalHDD = TotalHDD;
                objSysInfo.AvblHDD = AvblHDD;
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void GetProcessor(string serverName, SystemInformationModel objSysInfo)
        {
            StringBuilder availableMemmory = new StringBuilder();
            //ConnectionOptions oConn = new ConnectionOptions();
            //oConn.Impersonation = ImpersonationLevel.Impersonate;
            //oConn.Authentication = AuthenticationLevel.Default;

            //oConn.Username = "";
            //oConn.Password = "";
            string strNameSpace = @"\\";

            if (serverName != "")
                strNameSpace += serverName;
            else
                strNameSpace += ".";

            strNameSpace += @"\root\cimv2";

            //ManagementScope oMs = new ManagementScope(strNameSpace, oConn);
            ManagementScope oMs = GetMSConnetction(strNameSpace);

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(oMs, wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                objSysInfo.Processor = result["Name"].ToString();
                objSysInfo.TotalCores = Convert.ToInt32(result["NumberOfCores"].ToString());
            }
        }

        #region Bytes to Text
        /// <summary>
        /// HDD Returns bytes
        /// </summary>
        /// <param name="D_Totalspace"></param>
        /// <returns></returns>
        private static string ConvertBytes2Text(double D_Totalspace)
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

        /// <summary>
        /// Free Physical Memory, Avaiable Memory return in KiloBytes
        /// </summary>
        /// <param name="D_Totalspace"></param>
        /// <returns></returns>
        private static string ConvertKiloBytes2Text(double D_Totalspace)
        {
            /*
             *  FreePhysicalMemory
             *  Data type: uint64
             *  Access type: Read-only
             *  Qualifiers: Units ("kilobytes")
             * */

            double dCalFreeSpace = 0;
            string curTotalHDDSpace = string.Empty;
            dCalFreeSpace = System.Math.Round(D_Totalspace / (1024 * 1024 * 1024));
            if (dCalFreeSpace > 0)
            {
                return dCalFreeSpace + " GB";
            }
            else
            {
                //Convert into MB
                dCalFreeSpace = System.Math.Round(D_Totalspace / (1024 * 1024));
                if (dCalFreeSpace > 0)
                {
                    return dCalFreeSpace + " MB";
                }
            }
            return D_Totalspace + " KB";
        }

        #endregion



        #endregion

        





        
    }
}
