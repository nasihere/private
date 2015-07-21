// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreditDefenseDisclosureHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The credit defense disclosure helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.EAI.BLL.cas.Services
{
    using System;
    using System.Text;
    using System.Web;
    using System.Xml;

    //using WellsFargo.EAI.SIF.ServiceProxy.COM.WellsFargo.Service.Provider.RWS.Product;

    using WF.EAI.Data.sif;

    using WellsFargo.EAI.SIF.ServiceProxy.COM.WellsFargo.Service.Provider.RWS.Form2008;

    using WF.EAI.Data.sif.Services.RWS.GetForm;
    using WF.EAI.Entities.constants;
    using WF.EAI.Utils.xml;
    using WF.EAI.Entities.domain.MaturingTools;

    /// <summary>
    /// The credit defense disclosure helper.
    /// </summary>
    public class CreditDefenseDisclosureHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get pdf.
        /// </summary>
        /// <param name="pdfKey">
        /// The pdf key.
        /// </param>
        /// <param name="AU">
        /// The au.
        /// </param>
        /// <param name="customerNbr">
        /// The customer nbr.
        /// </param>
        /// <param name="loc">
        /// The loc.
        /// </param>
        /// <param name="initiatorId">
        /// The initiator id.
        /// </param>
        /// <param name="officer">
        /// The officer.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public byte[] GetPDF(
            string pdfKey, string AU, string customerNbr, string loc, string initiatorId, string officer)
        {
            byte[] pdfBytes = null;
            string xmlValue = string.Empty;
            string sessionId = string.Empty;
            XmlDocument resDoc = null;

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                sessionId = HttpContext.Current.Session.SessionID;
            }

            RWSContext_Type rwsContext = null;
            rwsContext = new RWSContext_Type();

            AU = AU != null ? AU : "0005426";
            loc = loc != null ? loc : "12345";
            officer = officer != null ? officer : "12345";
            rwsContext.branchNbr = AU;
            rwsContext.initiatorId = initiatorId;
            rwsContext.locationAUNumber = loc;
            rwsContext.locationCompanyNumber = "300"; // TODO
            rwsContext.primaryOfficer = officer;

            // Service Call
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.GetForm.ToString(), Constants.ApplicationNameEnum.CAS.ToString(), sessionId);
            GetFormReq getFormReq = new GetFormReq(
                AU, 
                customerNbr, 
                initiatorId, 
                rwsContext, 
                pdfKey, 
                true, 
                true, 
                true, 
                Constants.ServiceNameEnum.GetForm.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString());

            resDoc = eaiService.GetResponseXml(getFormReq);

            EaiXmlNode createFormXmlUtils = new EaiXmlNode(resDoc);
            xmlValue = createFormXmlUtils.getXmlValue("//pdf");

            if (xmlValue.Length > 1)
            {
                pdfBytes = Convert.FromBase64String(xmlValue);
            }
            else
            {
                StringBuilder faultReasonText = new StringBuilder();
                foreach (
                    XmlNode xmlNode in createFormXmlUtils.getNodes("/Envelope/Body/getFormResponse/WFFaultList/WFFault")
                    )
                {
                    faultReasonText.Append(xmlNode.SelectSingleNode("faultReasonText").InnerText);
                }

                if (faultReasonText.Length > 1)
                {
                    throw new Exception(faultReasonText.ToString());
                }
            }

            return pdfBytes;
        }

        #endregion
    }
}