// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisclosureHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for DisclosureHelper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Print
{
    using System;
    using System.Xml;

    using Common.Logging;

    using WF.EAI.BLL.cas.Invokers;
    using WF.EAI.BLL.cas.Services;
    using WF.EAI.Data.config.sif;
    using WF.EAI.Data.sif.Services.EAI.PrintCCH;
    using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    ///     Summary description for DisclosureHelper.
    /// </summary>
    public class DisclosureHelper : IAJAXHelper
    {
        #region Static Fields


        #endregion

        #region Fields

        /// <summary>
        /// The au.
        /// </summary>
        private string AU;

        /// <summary>
        /// The activity code.
        /// </summary>
        private string ActivityCode;

        /// <summary>
        /// The app id.
        /// </summary>
        private string AppID;

        /// <summary>
        /// The user id.
        /// </summary>
        private string UserID;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The doc type.
        /// </summary>
        private string docType;

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
            return string.Empty;
        }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string sendMessage(XmlDocument msg)
        {
            try
            {
                this.AppID = msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/applicationID") != null
                                 ? msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/applicationID").InnerText
                                 : string.Empty;
                this.ActivityCode = msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/activityCode") != null
                                        ? msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/activityCode").InnerText
                                        : string.Empty;
                this.UserID = msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/printUserID") != null
                                  ? msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/printUserID").InnerText
                                  : string.Empty;
                this.AU = msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/printAU") != null
                              ? msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/printAU").InnerText
                              : string.Empty;
                this.docType = msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/docType") != null
                                   ? msg.SelectSingleNode("//WellsFargoUFEDS/msgBody/docType").InnerText
                                   : string.Empty;
                PrintCCHRes printCCHRes;
                printCCHRes = Invoker.PrintCChService(
                    this.AppID, this.ActivityCode, this.UserID, this.AU, string.Empty, this.docType, string.Empty);
                if (printCCHRes.IsOK())
                {
                    return printCCHRes.resXmlStr;
                }
            }
            catch (Exception e)
            {
                //Logger.Instance.Error(e.Message);
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