// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSessionEntity.cs" company="">
//   
// </copyright>
// <summary>
//   The app session entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Web;
using WF.EAI.Entities.domain.cusp.Core;

namespace WF.EAI.Web.Controls.uiUtils.uuw
{
    using WF.EAI.Entities.domain.c2c.Common;

    /// <summary>
    /// The app session entity.
    /// </summary>
    [Serializable]
    public class AppSessionData
    {
        private static readonly string AppIdKey = "AppId";
        private static readonly string LangIndicatorKey = "LangIndicator";
        private static readonly string ScraDisplayedKey = "ScraDisplayed";
        private static readonly string acapsMsgKey = "AcapsMessage";
        private static readonly string acapsMsgsKey = "AcapsMessages";  

        public static string AppId;

        //public static CUSPAppDataHeader CuspAppHeader = SessionData.GetCUSPAppDataHeader(AppId);
        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="appId">
        /// The app id.
        /// </param>
        public  AppSessionData this[string appId]
        {
            get
            {
                if (HttpContext.Current.Session[appId] == null)
                {
                    HttpContext.Current.Session[appId] = HttpContext.Current.Session[AppIdKey];
                }

                return (AppSessionData)HttpContext.Current.Session[appId];
            }

            set
            {
                HttpContext.Current.Session[appId] = value;
            }
        }



        /// <summary>
        /// Gets or sets AppDataHeader.
        /// </summary>
        public static AppDataHeader AppDataHeader { get; set; }

        public static AcapsErrorMessage AcapsMessage
        {
            get
            {
                var fieldKey = string.Concat(AppId, acapsMsgKey);
                return (AcapsErrorMessage)HttpContext.Current.Session[fieldKey];
            }
            set
            {
                var fieldKey = string.Concat(AppId, acapsMsgKey);
                HttpContext.Current.Session[fieldKey] = value;
            }
        }

        public static List<AcapsErrorMessage> AcapsMessages
        {
            get
            {
                var fieldKey = string.Concat(AppId, acapsMsgsKey);
                return (List<AcapsErrorMessage>)HttpContext.Current.Session[fieldKey];
            }
            set
            {
                var fieldKey = string.Concat(AppId, acapsMsgsKey);
                HttpContext.Current.Session[fieldKey] = value;
            }  
        }

        public static string PaidInFullSessValue
        {
            get
            {
                var fieldKey = string.Concat(AppId, "PaidInFullSessionValue");
                return Convert.ToString(HttpContext.Current.Session[fieldKey]);
            }
            set
            {
                var fieldKey = string.Concat(AppId, "PaidInFullSessionValue");
                HttpContext.Current.Session[fieldKey] = value;
            }
        }

        public static string ChangedCbrValue
        {
            get
            {
                var fieldKey = string.Concat(AppId, "ChangedCbrValue");
                return Convert.ToString(HttpContext.Current.Session[fieldKey]);
            }
            set
            {
                var fieldKey = string.Concat(AppId, "ChangedCbrValue");
                HttpContext.Current.Session[fieldKey] = value;
            }
        }

        public static string ApplIncomeReduced
        {
            get
            {
                var fieldKey = string.Concat(AppId, "ApplIncomeReduced");
                return Convert.ToString(HttpContext.Current.Session[fieldKey]);
            }
            set
            {
                var fieldKey = string.Concat(AppId, "ApplIncomeReduced");
                HttpContext.Current.Session[fieldKey] = value;
            }
        }


        public static string ShowPtiFlag
        {
            get
            {
                var fieldKey = string.Concat(AppId, "ShowPtiFlag");
                return Convert.ToString(HttpContext.Current.Session[fieldKey]);
            }
            set
            {
                var fieldKey = string.Concat(AppId, "ShowPtiFlag");
                HttpContext.Current.Session[fieldKey] = value;
            }
        }

        public static string BkgDecisionStatus
        {
            get
            {
                var fieldKeyBDS = string.Concat(AppId, "BkgDecisionStatus");
                return Convert.ToString(HttpContext.Current.Session[fieldKeyBDS]);
            }
            set
            {
                var fieldKeyBDS = string.Concat(AppId, "BkgDecisionStatus");
                HttpContext.Current.Session[fieldKeyBDS] = value;
            }
        }


        public static void RemoveAppSessionValues()
        {
            //string appId = AppDataHeader.ApplicationId;
            //HttpContext.Current.Session.Remove(string.Concat(AppId, "ApplIncomeReduced"));
            HttpContext.Current.Session.Remove(string.Concat(AppId, "ChangedCbrValue"));
            HttpContext.Current.Session.Remove(string.Concat(AppId, "PaidInFullSessValue"));
            //HttpContext.Current.Session.Remove(string.Concat(AppId, "ShowPtiFlag"));

        }


        public static string LangIndicator
        {
            get
            {
                var fieldKey = string.Concat(AppId, LangIndicatorKey);
                return Convert.ToString(HttpContext.Current.Session[fieldKey]);
            }
            set
            {
                var fieldKey = string.Concat(AppId, LangIndicatorKey);
                HttpContext.Current.Session[fieldKey] = value;
            }
        }

        public static string ScraDisplayed
        {
            get { return Convert.ToString(HttpContext.Current.Session[ScraDisplayedKey]); }
            set { HttpContext.Current.Session[ScraDisplayedKey] = value; }
        }

    }
}
