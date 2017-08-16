using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace HiAsgRAS.Common
{
    public static class ApplicationConstants
    {
        public struct UserType
        {
         
            public const string Admin = "A";
            public const string GeneralUser = "G";
        }
        public struct AppKey
        {
            public const string CustomUser = "CustomUser";
            public const string AuditLog = "AuditLog";
            public const string AuditTrack = "AuditTrack";
            public const string HostName = "HostName";
            public const string SiteHostURL = "SiteHostURL";
            public const string RamThreshold = "RamThreshold";
            public const string HddThreshold = "HddThreshold";
            public const string RebootReset = "RebootReset";
        }
        public struct AppKeyValues
        {
            public const string On = "on";
            public const string Off = "off";
        }
        public struct Constants
        {
            public const string LoggedInUser = "LoggedInUser";
            public const string UserName = "UserName";
            public const string UserType = "UserType";
            public const string UserNUID = "UserNUID";
            public const string Password = "Password";
        }

        public struct eMailTemplateCode
        {
            public const string SubmittedTo = "MT001";
            public const string SubmittedCc = "MT002";
            public const string ApprovedTo = "MT003";
            public const string ApprovedCc = "MT004";
            public const string RejectedTo = "MT003";
            public const string RejectedCc = "MT004";
            public const string ReopenedTo = "MT005";
            public const string ReopenedCc = "MT006";
            public const string ClosedTo = "MT007";
            public const string ClosedCc = "MT008";

            //ToDo: Rest Others
        }
      
     

        public static string GetSiteHostURL()
        {
            string hostURL = string.Empty;
            if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.SiteHostURL] != null)
            {
                hostURL = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.SiteHostURL];
            }
            return hostURL;
        }


        public static string ReplaceHostName(string emailId)
        {
            if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.HostName] != null)
            {
                string hostName = string.Empty;
                hostName = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.HostName];
                if (emailId.Contains(hostName))
                {
                    return emailId;
                }
                else
                {
                    emailId = emailId.Substring(0, emailId.IndexOf('@')) + hostName;
                    return emailId;
                }
            }
            return emailId;
        }

        public struct EntityField
        {
            public const string UserType = "UserType";
            //public const string LastName = "LastName";
            //public const string FirstName = "FirstName";
            //public const string ProvLast = "ProvLast";
            //public const string ProvFirst = "ProvFirst";
            //public const string Phone = "Phone";
            //public const string Phone2 = "Phone2";
            //public const string LastSeen = "LastSeen";
        }

        //public static string GetUserType(string userType)
        //{
        //    switch (userType)
        //    {
        //        case UserType.Requestor_R:
        //            return UserType.Requestor;
        //        case UserType.Admin_A:
        //            return UserType.Admin;
        //        case UserType.Manager_M:
        //            return UserType.Manager;
        //        default:
        //            return UserType.Requestor;
        //    }
        //}

        public static int GetRamThreshold()
        {
            int value = 0;
            if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.RamThreshold] != null)
            {
                string keyValue = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.RamThreshold].ToString();
                int.TryParse(keyValue, out value);
            }
            return value;
        }

        public static int GetHddThreshold()
        {
            int value = 0;
            if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.HddThreshold] != null)
            {
                string keyValue = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.HddThreshold].ToString();
                int.TryParse(keyValue, out value);
            }
            return value;
        }


        public static int GetRebootReset()
        {
            int value = 0;
            if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.RebootReset] != null)
            {
                string keyValue = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.RebootReset].ToString();
                int.TryParse(keyValue, out value);
            }
            return value;
        }

        public static string UserName()
        {
            string userName = string.Empty;
            if (ConfigurationManager.AppSettings[ApplicationConstants.Constants.UserName] != null)
            {
                userName = ConfigurationManager.AppSettings[ApplicationConstants.Constants.UserName].ToString();                
            }

            return userName;
        }

        public static string Password()
        {
            string password = string.Empty;
            if (ConfigurationManager.AppSettings[ApplicationConstants.Constants.Password] != null)
            {
                password = ConfigurationManager.AppSettings[ApplicationConstants.Constants.Password].ToString();
            }

            return password;
        }
    }
}
