using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EAI.Entities.domain.uca.Common;
using WF.EAI.Entities.domain.uca.Core;

namespace WF.UAP.UA.UCRA.BLL.CUSP.Interface
{
   public interface IUCRASearchBo
    {
        /// <summary>
        /// The get customer details.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
       string GetCustomerDetails(AppDataHeader appHeader);
        ///// <summary>
        ///// The search cas application by phone.
        ///// </summary>
        ///// <param name="appHeader">
        ///// The app header.
        ///// </param>
        ///// <returns>
        ///// The <see cref="string"/>.
        ///// </returns>
       string SearchCASApplicationByPhone(AppDataHeader appHeader);
        ///// <summary>
        ///// The search cas application by application id.
        ///// </summary>
        ///// <param name="appHeader">
        ///// The app header.
        ///// </param>
        ///// <returns>
        ///// The <see cref="string"/>.
        ///// </returns>
       string SearchCASApplicationByAppID(AppDataHeader appHeader);
        ///// <summary>
        ///// The search cas application by fdr account number.
        ///// </summary>
        ///// <param name="appHeader">
        ///// The app header.
        ///// </param>
        ///// <returns>
        ///// The <see cref="string"/>.
        ///// </returns>
       string SearchCASApplicationByFDRAccountNumber(AppDataHeader appHeader);
       // ///// <summary>
       // ///// The search cas application by SSN CLient CAMS
       // ///// </summary>
       // ///// <param name="appHeader"></param>
       // ///// <returns></returns>
       string SearchCASApplicationBySSNClientCAMS(AppDataHeader appHeader);
       // ///// <summary>
       // ///// The search cas application by name.
       // ///// </summary>
       // ///// <param name="appHeader"></param>
       // ///// <returns></returns>
       string SearchCASApplicationByName(AppDataHeader appHeader);
       // ///// <summary>
       // ///// The search cas application.
       // ///// </summary>
       // ///// <param name="appHeader">
       // ///// The app header.
       // ///// </param>
       // ///// <returns>
       // ///// The <see cref="string"/>.
       // ///// </returns>
       string SearchCASApplication(AppDataHeader appHeader);
       // ///// <summary>
       // ///// The search party data.
       // ///// </summary>
       // ///// <param name="appHeader">
       // ///// The app header.
       // ///// </param>
       // ///// <returns>
       // ///// The <see cref="string"/>.
       // ///// </returns>
       string SearchPartyData(AppDataHeader appHeader);
       // ///// <summary>
       // ///// The search saved application.
       // ///// </summary>
       // ///// <param name="appHeader">
       // ///// The app header.
       // ///// </param>
       // ///// <returns>
       // ///// The <see cref="string"/>.
       // ///// </returns>
       string SearchSavedApplication(AppDataHeader appHeader);
       // ///// <summary>
       // ///// The search ESignature application.
       // ///// </summary>
       // ///// <param name="acctNum"></param>
       // ///// <param name="sessionId"></param>
       // ///// <returns></returns>
       WF.EAI.Entities.domain.uca.Common.AppDataHeader SearchESignature(string acctNum, string sessionId);


    }
}
