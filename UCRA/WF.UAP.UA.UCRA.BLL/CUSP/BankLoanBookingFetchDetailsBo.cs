using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;

using WF.EAI.Entities.domain.cusp.UI;

namespace WF.EAI.BLL.CUSP
{
    using WF.EAI.Entities.domain.cusp.Search;
    using WF.UAP.UASF.CrossCutting.Logging;
    using WF.UAP.UDB.Domain.ORM.dal;
    using WF.UAP.UDB.Repository.Transform.dal.UPortal.CUSP;

    public class BankLoanBookingFetchDetailsBo
    {
        /// <summary>
        /// The search.
        /// </summary>
        /// <param name="criteria">
        /// The criteria.
        /// </param>
        /// <returns>
        /// The <see cref="SearchApplicationResultEntityCollection"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public GetBankLoanBookingUsersListEntityCollection GetBankLoanBookingUsersList()
        {
            try
            {
                using (var db = ContextClass.CreateCUSPObjectContext())
                {
                    GetBankLoanBookingUsersListEntityCollection getBlbUsersList;
                    ObjectResult<GetBankLoanBookingUsersList_Result> result = db.GetBankLoanBookingUsersList();
                    getBlbUsersList = new GetBankLoanBookingUsersListEntityCollection();
                    getBlbUsersList.getBankLoanBookingUsersListEntityList = new List<GetBankLoanBookingUsersListEntity>();
                    var enumBlbUsers = result.AsEnumerable();
                    foreach (GetBankLoanBookingUsersList_Result userDetails in enumBlbUsers)
                    {
                        var temp = new GetBankLoanBookingUsersListEntity();
                        temp.User_Id = userDetails.UserId;
                        temp.User_Name = userDetails.UserName;
                        getBlbUsersList.getBankLoanBookingUsersListEntityList.Add(temp);
                    }

                    //Logger.Instance.Info(
                        //"No. of users returned for accessing Bank Loan Booking = " + getBlbUsersList.getBankLoanBookingUsersListEntityList.Count);
                    return getBlbUsersList;
                }
            }
            catch (Exception ex)
            {
                //Logger.Instance.Error("Bank Loan Work Flow Utility: ", ex);
                throw new Exception();
            }
        }
    }
}
