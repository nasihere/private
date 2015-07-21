using System;

using WF.EAI.Entities.domain.cusp.UI;
using WF.EAI.Utils;

namespace WF.EAI.BLL.CUSP
{
    using WF.UAP.UASF.CrossCutting.Logging;

    public class BankLoanBookingBo
    {


        /// <summary>
        /// The search cas application.
        /// </summary>
        /// <param name="appHeader">
        /// The app header.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetBankLoanBookingUsersList()
        {
            string jsonText = string.Empty;

            GetBankLoanBookingUsersListEntityCollection BlbUserList = new GetBankLoanBookingUsersListEntityCollection();
            try
            {
               var bankLoanUsersBo = new BankLoanBookingFetchDetailsBo();
               BlbUserList = bankLoanUsersBo.GetBankLoanBookingUsersList();
               
                // Map Initial data  to entities                
                jsonText = JsonMapper<string>.ObjectToJsonText(BlbUserList);
            }
            catch (Exception ex)
            {
                ////Logger.Instance.Error("Bank Loan Work Flow Utility: ", ex);
                throw new Exception();               
            }

            return jsonText;
        }
    }
}
