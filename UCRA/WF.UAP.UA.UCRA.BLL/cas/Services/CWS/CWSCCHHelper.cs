// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CWSCCHHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The cwscch helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;
    using System.Web;

    using WF.EAI.Data.sif;
    using WF.EAI.Data.sif.Services.CWS.CCHToACAPS;

    /// <summary>
    /// The cwscch helper.
    /// </summary>
    [Serializable]
    public class CWSCCHHelper
    {
        #region Fields

        /// <summary>
        /// The session_id.
        /// </summary>
        private string session_id = string.Empty;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The begin inquiry.
        /// </summary>
        /// <param name="ApplicationId">
        /// The application id.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="UserSalesId">
        /// The user sales id.
        /// </param>
        /// <param name="UserAU">
        /// The user au.
        /// </param>
        /// <param name="ActivityCode">
        /// The activity code.
        /// </param>
        /// <param name="Comments">
        /// The comments.
        /// </param>
        /// <param name="ErrMsg">
        /// The err msg.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool BeginInquiry(
            string ApplicationId, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            string ActivityCode, 
            string Comments, 
            ref string ErrMsg)
        {
            if (UserSalesId.Trim().Length == 0)
            {
                UserSalesId = "12345";
            }

            return this.acapsCWSCCH(ApplicationId, ActivityCode, UserId, UserSalesId, UserAU, Comments, ref ErrMsg);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The acaps cwscch.
        /// </summary>
        /// <param name="ApplicationId">
        /// The application id.
        /// </param>
        /// <param name="ActivityCode">
        /// The activity code.
        /// </param>
        /// <param name="UserId">
        /// The user id.
        /// </param>
        /// <param name="UserSalesId">
        /// The user sales id.
        /// </param>
        /// <param name="UserAU">
        /// The user au.
        /// </param>
        /// <param name="Comments">
        /// The comments.
        /// </param>
        /// <param name="ErrMsg">
        /// The err msg.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool acapsCWSCCH(
            string ApplicationId, 
            string ActivityCode, 
            string UserId, 
            string UserSalesId, 
            string UserAU, 
            string Comments, 
            ref string ErrMsg)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            // acaps
            EAIService eaiService = EAIService.getService("CCHTOACAPS", "CAS", this.session_id);
            CCHToACAPSReq cwsCCHReq = new CCHToACAPSReq(
                ApplicationId, ActivityCode, UserId, UserSalesId, UserAU, Comments);
            CCHToACAPSRes cwsCCHRes = (CCHToACAPSRes)eaiService.Execute(cwsCCHReq);

            // Modified the code according to new SIF Call
            // if (cwsCCHRes != null && cwsCCHRes.success)
            if (cwsCCHRes != null && cwsCCHRes.IsOK())
            {
                return true;
            }
            else
            {
                ErrMsg = cwsCCHRes != null && cwsCCHRes.ex != null ? cwsCCHRes.ex.Message : string.Empty;
            }

            return false;
        }

        #endregion
    }
}