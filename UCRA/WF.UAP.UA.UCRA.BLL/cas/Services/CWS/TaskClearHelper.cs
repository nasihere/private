// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskClearHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for TaskClearHelper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Xml;

    using WF.EAI.BLL.cas.Invokers;
    using WF.EAI.Data.config.sif;
    using WF.EAI.Data.sif.Services.CWS.GetAlerts;

    /// <summary>
    ///     Summary description for TaskClearHelper.
    /// </summary>
    public class TaskClearHelper : IAJAXHelper
    {
        #region Fields

        /// <summary>
        /// The _task send.
        /// </summary>
        private Stack _taskSend;

        /// <summary>
        /// The session_id.
        /// </summary>
        private string session_id = string.Empty;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskClearHelper"/> class.
        /// </summary>
        public TaskClearHelper()
        {
            this._taskSend = new Stack();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The receive message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string receiveMessage(XmlDocument msg)
        {
            return null;
        }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="rDoc">
        /// The r doc.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string sendMessage(XmlDocument rDoc)
        {
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(rDoc.NameTable);
            nsMgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsMgr.AddNamespace("SOAP-ENC", "http://schemas.xmlsoap.org/soap/encoding/");
            nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nsMgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
            nsMgr.AddNamespace("m", "http://service.wellsfargo.com/provider/HCFG/ACAPS/shared/2005/");
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            string applicationId =
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:ACAPSContext/applicationId", 
                    nsMgr) != null
                    ? rDoc.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:ACAPSContext/applicationId", 
                        nsMgr).InnerText.Trim()
                    : string.Empty;
            string requestorUserID =
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:originatorId", nsMgr)
                != null
                    ? rDoc.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:originatorId", 
                        nsMgr).InnerText.Trim()
                    : string.Empty;
            string billingAU =
                rDoc.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:billingAU", nsMgr)
                != null
                    ? rDoc.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Header/m:WFContext/m:billingAU", 
                        nsMgr).InnerText.Trim()
                    : string.Empty;

            XmlNodeList nodes =
                rDoc.SelectNodes(
                    "/AJAX/Body/serviceRequest/data/SOAP-ENV:Envelope/SOAP-ENV:Body/GETALERTRequest/info/delAlertsData/alertKey", 
                    nsMgr);

            ArrayList list = new ArrayList();
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    list.Add(node.InnerText);
                }
            }

            TaskInqRes _responseNote = Invoker.GetTaskInq(
                applicationId.Trim(), 
                requestorUserID, 
                billingAU, 
                billingAU, 
                billingAU, 
                string.Empty, 
                "DEL", 
                list, 
                this.session_id);

            if (_responseNote.IsOK() && _responseNote != null)
            {
                TaskInqRes taskInqRes = Invoker.GetTaskInq(
                    applicationId.Trim(), 
                    requestorUserID, 
                    billingAU, 
                    billingAU, 
                    billingAU, 
                    string.Empty, 
                    "INQ", 
                    null, 
                    this.session_id);

                if (taskInqRes != null && taskInqRes.IsOK())
                {
                    if (taskInqRes.resXml != null && !string.IsNullOrEmpty(taskInqRes.resXml.InnerXml))
                    {
                        return taskInqRes.resXml.InnerXml.Replace("\r", string.Empty).Replace("\\\"", "\"");
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public string sendMessage(XmlDocument msg, EAISIFServiceBaseConfig config)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}