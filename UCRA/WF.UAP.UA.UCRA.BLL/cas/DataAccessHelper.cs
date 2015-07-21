// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccessHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The data access helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using WF.EAI.Entities.domain.cusp;
using WF.EAI.Entities.domain.cusp.UI;

namespace WF.EAI.BLL.cas
{
    using System.Collections.Generic;

    using WF.EAI.Entities.domain.cas;
    using WF.UAP.UDB.Repository.Transform.dal.UCRA.CAS;
    using WF.UAP.UDB.Repository.Transform.dal.UPortal.CUSP;

    /// <summary>
    /// The data access helper.
    /// </summary>
    public class DataAccessHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The cb d_ get eligible products.
        /// </summary>
        /// <param name="Product">
        /// The product.
        /// </param>
        /// <param name="lOB">
        /// The l ob.
        /// </param>
        /// <param name="ProductCode">
        /// The product code.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public static Dictionary<string, string> CBD_GetEligibleProducts(string Product, string lOB, string ProductCode)
        {
            return CBDDataHelper.CBD_GetEligibleProducts(Product, lOB, ProductCode);
        }

        /// <summary>
        /// The get authorized ssn list.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetAuthorizedSSNList(string userId)
        {
            return HMCHelper.GetAuthorizedSSNList(userId);
        }

        /// <summary>
        /// The get credit card.
        /// </summary>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <returns>
        /// The <see cref="CreditCardData"/>.
        /// </returns>
        public static CreditCardData getCreditCard(string appId, string channel)
        {
            CreditCardHelper creditCardHelper = new CreditCardHelper();
            return creditCardHelper.getCreditCard(appId, channel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static List<CasAppDataCsFollowUp> GetCSFollowUpAppData(string tabName, string userID, string TeamID, string userRole)
        {
            var csFollowUpHelper = new CASApplicationDataCsFollowUpHelper();
            return csFollowUpHelper.GetApplicationDataCS(tabName, userID, TeamID, userRole);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sAppSearch"></param>
        /// <param name="sRoleFlag"></param>
        /// <param name="sAppStatus"></param>
        /// <returns></returns>
        public static List<SearchBNFApplicationEntity> SearchBNFApplications(string sUserID, string sAppSearch, string sRoleFlag, string sAppStatus)
        {
            CASApplicationDataCsFollowUpHelper csFollowUpHelper = new CASApplicationDataCsFollowUpHelper();
            return csFollowUpHelper.SearchBNFApplications(sUserID, sAppSearch, sRoleFlag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<SearchBNFApplicationEntity> GetNextApp()
        { 
            CASApplicationDataCsFollowUpHelper csFollowUpHelper = new CASApplicationDataCsFollowUpHelper();
            return csFollowUpHelper.GetNextApplication();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<SearchAutoAssistApplicationEntity> GetNextAppforAutoAssist()
        { 
            CASApplicationDataCsFollowUpHelper csFollowUpHelper = new CASApplicationDataCsFollowUpHelper();
            return csFollowUpHelper.GetNextAppforAutoAssist();
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sAppSearch"></param>
        /// <param name="sRoleFlag"></param>
        /// <param name="sAppStatus"></param>
        /// <returns></returns>
        public static List<SearchAutoAssistApplicationEntity> SearchAutoAssistApplications(string sUserID, string sAppSearch, string sRoleFlag, string sAppStatus)
        {
            CASApplicationDataCsFollowUpHelper csFollowUpHelper = new CASApplicationDataCsFollowUpHelper();
            return csFollowUpHelper.SearchAutoAssistApplications(sUserID, sAppSearch, sRoleFlag);
        }
            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static List<GetBankLoanUserAppEntity> GetBankLoanUserAppDetails(string sUserID, string sRouteStatus)
        {
            CASApplicationDataCsFollowUpHelper csFollowUpHelper = new CASApplicationDataCsFollowUpHelper();
            return csFollowUpHelper.GetBankLoanUserAppDetails(sUserID, sRouteStatus);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserRole"></param>
        /// <param name="teamID"></param>
        /// <param name="centerID"></param>
        /// <returns></returns>
        public static List<UsrInfoByCriteria> GetUsrInfoByCriteria(string UserID, string UserRole, string teamID, string centerID)
        {
            var cuspUserDa = new CUSPUserDA();

            return cuspUserDa.GetUsrInfoByCriteria(UserID, UserRole, teamID, centerID);

        }

        #endregion
    }
}