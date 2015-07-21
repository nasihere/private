using System;

using WF.EAI.Model.Lookup;

namespace WF.EAI.BLL.BO.CUSPSearch
{
    using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    /// SearchBo
    /// </summary>
    public abstract class SearchBo
    {
        private CUSPSearchViewName _viewName = CUSPSearchViewName.None;

        private RetailerSearchViewName _retailerviewName = RetailerSearchViewName.None;
        
        /// <summary>
        /// ViewName
        /// </summary>
        public CUSPSearchViewName ViewName
        {
            get { return _viewName; }
            set { _viewName = value; }
        }

        public RetailerSearchViewName RetailerviewName
        {
            get { return _retailerviewName; }
            set { _retailerviewName = value; }
        }

        /// <summary>
        /// LogErrorInfo -- This method used for logging the exception occured in all bo level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="dataModel"></param>
        public void LogErrorInfo(string message, Exception ex)
        {
            string guid = Guid.NewGuid().ToString();
            ////Logger.Instance.Error(guid + ":" + message + ":" + ex + " at time" + DateTime.Now);
        }
    }
}
