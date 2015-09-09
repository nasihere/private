using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Xml.Linq;
using WF.EAI.Model;
using WF.EAI.Utils.smtp;
using xxxxProjec.....UAPortal..Authentication;
using xxxxProjec.....UAPortal..Helpers;
using xxxxProjec....SF.CrossCutting.Logging;
using WF.UAP.UDB.Repository.Domain.Entities.UAPortal;
using WF.UAP.UDB.Repository.Domain.Entities.UAPortal.Account.Response;

namespace xxxxProjec.....UAPortal..BO
{
    /// <summary>
    /// UAPortalAccountBO
    /// </summary>
    public class UAPortalAccountBO
    {
        /// <summary>
        /// GetAcccountModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AccountModel</returns>
        public static AccountResponseModel GetAcccountModel(AccountModel model)
        {
            var acountResponseModel = new AccountResponseModel {ErrorMessages = null };
            try
            {
                var modulePrivileges = UaPortalDbHelper.GetUserAccountInformation(model.UserId, true);
                acountResponseModel.AccountModel = new AccountModel
                            {
                                ModulePrivileges = modulePrivileges,
                                IsAdEntId = true,
                                UserId = model.UserId,
                                FirstName = modulePrivileges.Select(i => i.FirstName).FirstOrDefault(),
                                LastName = modulePrivileges.Select(i => i.LastName).FirstOrDefault()
                            };
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("UAPortalAccountBO Error:" + ex);
                SetErrorMessage(acountResponseModel, ex.Message);
            }
            return acountResponseModel;
        }

        /// <summary>
        ///  Registers a new user and sends an email notificatoin on success
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AccountResponseModel RegisterAccountModel(AccountModel model)
        {
            var acountResponseModel = new AccountResponseModel { AccountModel = model, ErrorMessages = null };
            try
            {
                //Step 1: Validate UserID against Active Directory. If success then register.
                var activeDirectoryAuth = new ActiveDirectoryAuth();
                bool isSuccess = activeDirectoryAuth.ValidateUser(model.UserId, model.Password);

                if (!isSuccess)
                {
                    SetErrorMessage(acountResponseModel, "Invalid Credentials - Error while registering an account.");
                }
                else
                {
                    //Step 2: Register New User - It adds records in to database
                    if (!RegisterAccount(model))
                    {
                        SetErrorMessage(acountResponseModel, "Error while registering an account.");
                    }
                    else
                    {
                        //Step 3: If Registration is success then send an email notification
                        SendRegistrationEmail(model);
                    }

                }


            }
            catch (Exception ex)
            {
                Logger.Instance.Error("UAPortalAccountBO - RegisterAccountModel - Error:" + ex);
                SetErrorMessage(acountResponseModel, ex.Message);
            }

            return acountResponseModel;
        }

        private static bool RegisterAccount(AccountModel model)
        {
            try
            {
                var userId = model.UserId;
                var email = model.UserEmail;
                var isEdEntId = model.IsAdEntId;
                var firstname = model.FirstName;
                var lastname = model.LastName;

                return UaPortalDbHelper.RegisterUserAccountInformation(userId, isEdEntId, email, firstname, lastname);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error in RegisterAccount::" + ex);
                throw;
            }
        }

        private static void SendRegistrationEmail(AccountModel model)
        {
            try
            {
                var webapiurl = WebConfigurationManager.AppSettings["UAPortalWebApiUrl"];
                var smtpServer = WebConfigurationManager.AppSettings["SMTPServerUrl"];

                var fromName = model.FirstName + ", " + model.LastName;
                var recipients = model.ManagersEmail;

                var guid = UaPortalDbHelper.GetUserGuid(model.UserId, model.IsAdEntId);

                var roleIds = string.Empty;
                if (model.UserRoles != null)
                {
                    roleIds = model.UserRoles.Aggregate(roleIds, (current, role) => current + role.Id + ",");
                    roleIds = roleIds.Substring(0, roleIds.Length - 1);
                }
                var userName = model.LastName + ", " + model.FirstName + " ( " + model.UserId + " ) ";

                var message = "Hello Manager, <br><br>New User <b>" + userName + "</b> has been registered and requests your approval to proceed further. <br> Please use this link <a href='" + webapiurl + "/Account/Approve/guid=" + guid + "/roleids=" + roleIds + "'>Approve</a> in order to approve.<br><br> Thanks, <br> System Administrator.";
                var subject = "New User Registration Approval";
                var fromEmail = model.UserEmail;
                if (!String.IsNullOrEmpty(recipients))
                {
                    var sendMail = new SendMail();
                    sendMail.SendEmail(smtpServer, fromEmail, fromName, recipients, null, subject, message, null);
                }
                else
                {
                    Logger.Instance.Error("Managers Email is NULL in AccountModel from UI");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error in SendRegistrationEmail::" + ex);
                throw;
            }
        }

        /// <summary>
        /// Fetches the Active Non-Admin Roles 
        /// </summary>
        /// <returns></returns>
        public static List<NameValue> GetRoles()
        {
            var rolesList = new List<NameValue>();
            try
            {
                rolesList = UaPortalDbHelper.GetRoles();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("UAPortalAccountBO - GetRoles - Error::" + ex);
            }

            return rolesList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SaveAccountRoles(AccountModel model)
        {
            var isSuccess = true;
            try
            {
                //Save account roles
                isSuccess = SaveRoles(model);

            }
            catch (Exception ex)
            {
                Logger.Instance.Error("UAPortalAccountBO - RegisterAccountModel - Error:" + ex);
                isSuccess = false;
            }

            return isSuccess;
        }

        private static bool SaveRoles(AccountModel model)
        {
            var userId = model.UserId;
            var isEdEntId = model.IsAdEntId;
            // Sample Roles XML Format
            //<Roles><Role Id="1"></Role><Role Id="2"></Role></Roles>
            var roles = string.Empty;
            if (model.UserRoles != null)
            {
                var xmlElements = new XElement("Roles",
                    model.UserRoles.Select(i => new XElement("Role", new XAttribute("Id", i.Id))));
                roles = Convert.ToString(xmlElements);
            }
            return UaPortalDbHelper.SaveUserAccountRoles(userId, isEdEntId, roles);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SaveAccount(AccountModel model)
        {
            var isSuccess = true;
            try
            {
                //Update user profile
                isSuccess = UpdateProfile(model);

            }
            catch (Exception ex)
            {
                Logger.Instance.Error("UAPortalAccountBO - SaveAccount - Error:" + ex);
                isSuccess = false;
            }

            return isSuccess;
        }

        private static bool UpdateProfile(AccountModel model)
        {
            var userId = model.UserId;
            var isEdEntId = model.IsAdEntId;
            var email = model.UserEmail;
            // Sample Roles XML Format
            //<Roles><Role Id="1"></Role><Role Id="2"></Role></Roles>
            var roles = string.Empty;
            if (model.UserRoles != null)
            {
                var xmlElements = new XElement("Roles",
                    model.UserRoles.Select(i => new XElement("Role", new XAttribute("Id", i.Id))));
                roles = Convert.ToString(xmlElements);
            }
            return UaPortalDbHelper.UpdateUserProfile(userId, isEdEntId, roles, email);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="roleids"></param>
        /// <returns></returns>
        public static string ApproveAccountModel(string guid, string roleids)
        {
            var isSuccess = false;
            try
            {
                //Step 1: Enable User - Update IsDisable field to 0 in database
                isSuccess = EnableUser(guid);

                //Step 2: If user is enabled then send an email notification to App Admin
                if (isSuccess)
                {
                    SendApprovalEmailToAdmin(guid, roleids);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("UAPortalAccountBO - RegisterAccountModel - Error:" + ex);
            }


            return isSuccess ? "Approved" : "Not Approved";
        }

        private static bool EnableUser(string guid)
        {
            return UaPortalDbHelper.EnableUserProfile(guid);
        }

        private static void SendApprovalEmailToAdmin(string guid, string roleswithComma)
        {
            try
            {

                var smtpServer = WebConfigurationManager.AppSettings["SMTPServerUrl"];
                var adminUser = UaPortalDbHelper.GetAdminProfiles().FirstOrDefault();
                var allNonAdminroles = UaPortalDbHelper.GetRoles();
                var userDetails = UaPortalDbHelper.GetUserInfoByGuid(guid).FirstOrDefault();


                if (userDetails == null) return;

                if (adminUser == null) return;

                var recipients = adminUser.UserEmail;
                if (recipients == null) return;

                var fromName = "UA Portal System Administrator";

                var roleLines = "<br><b>Roles : </b><br>";
                if (allNonAdminroles != null)
                {
                    var inputRoles = roleswithComma.Split(',').ToList();
                    roleLines =
                        (from role in allNonAdminroles
                         from inputRole in inputRoles
                         where Convert.ToString(role.Id) == inputRole
                         select role).Aggregate(roleLines,
                                (current, role) => current + role.Name + "<br>");
                }
                var userName = userDetails.LastName + ", " + userDetails.FirstName + " ( " +
                               userDetails.UserId + " ) ";

                var message = "Hello Admin, <br><br>New User <b>" + userName +
                              "</b> has been approved. Please enable necessary following requested roles. <br> " +
                              roleLines + "<br><br> Thanks, <br> System Administrator.";
                const string subject = "UA Portal User Approvals";
                const string fromEmail = "sysadmin@wellsfargo.com";

                var sendMail = new SendMail();
                sendMail.SendEmail(smtpServer, fromEmail, fromName, recipients, null, subject, message, null);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error in SendApprovalEmailToAdmin::" + ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<AccountModel> GetUsersList(AccountModel model)
        {
            var usersList = new List<AccountModel>();
            try
            {
                usersList = UaPortalDbHelper.GetUsersList(model.FirstName, model.LastName, model.UserId, model.IsAdEntId);
                foreach (var user in usersList)
                {
                    user.UserRoles = UaPortalDbHelper.GetUserRoles(user.UserId, model.IsAdEntId);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("UAPortalAccountBO Error:" + ex);
            }
            return usersList;
        }



        private static void SetErrorMessage(AccountResponseModel acountResponseModel, string errorMessage)
        {
            if (acountResponseModel.ErrorMessages != null)
            {
                acountResponseModel.ErrorMessages.Add(
                    new ErrorMessage()
                    {
                        Message = errorMessage,
                        Code = "FAILED"
                    }
                    );
            }
            else
            {
                acountResponseModel.ErrorMessages = new List<ErrorMessage>();
                acountResponseModel.ErrorMessages.Add(
                    new ErrorMessage()
                    {
                        Message = errorMessage,
                        Code = "FAILED"
                    }
                    );
            }
        }
    }
}
