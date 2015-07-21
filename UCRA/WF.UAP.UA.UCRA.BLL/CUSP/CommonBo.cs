using System.Collections.Generic;
using System.Linq;

namespace WF.EAI.BLL.CUSP
{
    /// <summary>
    /// Contains Business rules for CUSP
    /// </summary>
    public class CommonBo
    {

        private static readonly List<string> _approvedStatusList = new List<string>() { "AP", "AW", "DP", "55", "56", "TD", "CO", "BK", "ZP", "SR", "60" };
        private static readonly List<string> _completedDecisionList = new List<string>() { "AP", "55", "56", "CO" };

        /// <summary>
        /// checks whether decision completed based on app status 
        /// </summary>
        /// <param name="appStatus"></param>
        /// <param name="bkDecisionStatus"></param>
        /// <returns>boolean</returns>
        public static bool IsDecisionCompleted(string appStatus, string bkDecisionStatus)
        {

            var ret = _completedDecisionList.Any(s => (s == appStatus) || (s == bkDecisionStatus));

            return ret;
        }

        /// <summary>
        /// checks whether appln is approved based on app status 
        /// </summary>
        /// <param name="appStatus"></param>
        /// <param name="bkDecisionStatus"></param>
        /// <returns>boolean</returns>
        public static bool IsApproved(string appStatus, string bkDecisionStatus)
        {

            var ret = _approvedStatusList.Any(s => (s == appStatus)||(s == bkDecisionStatus))  ;

            return ret;
        }
    }
}
