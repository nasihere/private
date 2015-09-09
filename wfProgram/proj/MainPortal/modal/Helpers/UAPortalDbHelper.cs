using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WF.EAI.Data.sif.ServiceProxy.com.wellsfargo.service.provider.EDM;
using WF.EAI.Model;
using xxxxProjec....SF.CrossCutting.ConfigMgmt.config.Global;
using xxxxProjec....SF.CrossCutting.Logging;
using xxxxProjec....SF.Utils.Patterns.Repository;
using xxxxProjec....SF.Utils.Patterns.Repository.Core;
using WF.UAP.UDB.Repository.Domain.Entities.UAPortal;

namespace xxxxProjec.....UAPortal..Helpers
{
    public class UaPortalDbHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isAdEntId"></param>
        /// <returns></returns>
        public static List<ModulePrivilegeModel> GetUserAccountInformation(string userId, bool isAdEntId)
        {
            var model = new List<ModulePrivilegeModel>();
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {

                    var dataAttr = new DataAttributes()
                    {
                        CommandText = "sso.upAccountGet",
                        ApplyTransaction = false,
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                        ConnectionString =
                            eFlowConfig.Instance.GetConnectionStringByName(
                                eFlowConfig.EAIDatabaseName.),
                        DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "userId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = userId
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.Boolean,
                                                                Name = "isAdEntId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = isAdEntId
                                                            }
                                                        }
                    };

                    var repository = new Repository<ModulePrivilegeModel>(dataAttr);
                    model = repository.GetData();
                }
                else
                {
                    Logger.Instance.Error("UaPortalDbHelper - GetUserAccountInformation UserID is NULL OR Blank");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - GetUserAccountInformation Failed:" + userId +
                                    "::Exception:" + ex);
                throw;
            }

            return model;
        }

        /// <summary>
        /// Registers User Account Information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isAdEntId"></param>
        /// <param name="roles"></param>
        /// <param name="email"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        public static bool RegisterUserAccountInformation(string userId, bool isAdEntId, string email, string firstname, string lastname)
        {
            var result = true;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {

                    var dataAttr = new DataAttributes()
                    {
                        CommandText = "sso.upAccountIns",
                        ApplyTransaction = false,
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                        ConnectionString =
                            eFlowConfig.Instance.GetConnectionStringByName(
                                eFlowConfig.EAIDatabaseName.),
                        DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "userId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = userId
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.Boolean,
                                                                Name = "isAdEntId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = isAdEntId
                                                            },
                                                             new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "email",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = email
                                                            },
                                                             new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "firstname",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = firstname
                                                            },
                                                             new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "lastname",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = lastname
                                                            }
                                                        }
                    };

                    var repository = new Repository<ModulePrivilegeModel>(dataAttr);
                    repository.SetData();
                }
                else
                {
                    Logger.Instance.Error("UaPortalDbHelper - RegisterUserAccountInformation UserID is NULL OR Blank");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - RegisterUserAccountInformation Failed:" + userId +
                                    "::Exception:" + ex);
                result = false;
                throw;
            }

            return result;
        }

        /// <summary>
        ///  Get Roles fetches - Active Non-Admin Roles
        /// </summary>
        /// <returns></returns>
        public static List<NameValue> GetRoles()
        {
            var rolesList = new List<NameValue>();
            try
            {

                var dataAttr = new DataAttributes()
                {
                    CommandText = "[Sso].[GetRoles]",
                    ApplyTransaction = false,
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                    ConnectionString =
                        eFlowConfig.Instance.GetConnectionStringByName(
                            eFlowConfig.EAIDatabaseName.),
                };

                var repository = new Repository<NameValue>(dataAttr);
                rolesList = repository.GetData();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - GetRoles Failed ::Exception:" + ex);
            }

            return rolesList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isAdEntId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool SaveUserAccountRoles(string userId, bool isAdEntId, string roles)
        {
            var result = true;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {

                    var dataAttr = new DataAttributes()
                    {
                        CommandText = "sso.upAccountRoleMerge",
                        ApplyTransaction = false,
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                        ConnectionString =
                            eFlowConfig.Instance.GetConnectionStringByName(
                                eFlowConfig.EAIDatabaseName.),
                        DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "userId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = userId
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.Boolean,
                                                                Name = "isAdEntId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = isAdEntId
                                                            },
                                                             new DataParameter()
                                                            {
                                                                DbType = DbType.Xml,
                                                                Name = "roles",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = roles
                                                            }
                                                        }
                    };

                    var repository = new Repository<ModulePrivilegeModel>(dataAttr);
                    repository.SetData();
                }
                else
                {
                    Logger.Instance.Error("UaPortalDbHelper - SaveUserAccountRoles UserID is NULL OR Blank");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - SaveUserAccountRoles Failed:" + userId +
                                    "::Exception:" + ex);
                result = false;
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isAdEntId"></param>
        /// <param name="roles"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool UpdateUserProfile(string userId, bool isAdEntId, string roles, string email)
        {
            var result = true;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {

                    var dataAttr = new DataAttributes()
                    {
                        CommandText = "sso.AccountProfileUpd",
                        ApplyTransaction = false,
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                        ConnectionString =
                            eFlowConfig.Instance.GetConnectionStringByName(
                                eFlowConfig.EAIDatabaseName.),
                        DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "userId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = userId
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.Boolean,
                                                                Name = "isAdEntId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = isAdEntId
                                                            },
                                                             new DataParameter()
                                                            {
                                                                DbType = DbType.Xml,
                                                                Name = "roles",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = roles
                                                            },
                                                             new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "email",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = email
                                                            }
                                                        }
                    };

                    var repository = new Repository<ModulePrivilegeModel>(dataAttr);
                    repository.SetData();
                }
                else
                {
                    Logger.Instance.Error("UaPortalDbHelper - UpdateUserProfile UserID is NULL OR Blank");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - UpdateUserProfile Failed:" + userId +
                                    "::Exception:" + ex);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isAdEntId"></param>
        /// <returns></returns>
        public static string GetUserGuid(string userId, bool isAdEntId)
        {
            var result = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {

                    var dataAttr = new DataAttributes()
                    {
                        CommandText = "sso.upGetUserGuid",
                        ApplyTransaction = false,
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                        ConnectionString =
                            eFlowConfig.Instance.GetConnectionStringByName(
                                eFlowConfig.EAIDatabaseName.),
                        DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "userId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = userId
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.Boolean,
                                                                Name = "isAdEntId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = isAdEntId
                                                            }
                                                        }
                    };

                    var repository = new Repository<String>(dataAttr);
                    result = repository.GetData();
                }
                else
                {
                    Logger.Instance.Error("UaPortalDbHelper - GetUserGuid UserID is NULL OR Blank");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - GetUserGuid Failed:" + userId +
                                    "::Exception:" + ex);
            }

            return (result != null) ? result.FirstOrDefault() : string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool EnableUserProfile(string guid)
        {
            var result = true;
            try
            {
                if (!string.IsNullOrEmpty(guid))
                {

                    var dataAttr = new DataAttributes()
                    {
                        CommandText = "sso.upAccountApprove",
                        ApplyTransaction = false,
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                        ConnectionString =
                            eFlowConfig.Instance.GetConnectionStringByName(
                                eFlowConfig.EAIDatabaseName.),
                        DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "guidstr",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = guid
                                                            }
                                                        }
                    };

                    var repository = new Repository<ModulePrivilegeModel>(dataAttr);
                    repository.SetData();
                }
                else
                {
                    Logger.Instance.Error("UaPortalDbHelper - EnableUserProfile GUID is NULL OR Blank");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - EnableUserProfile Failed ::Exception:" + ex);
                result = false;
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<AccountModel> GetAdminProfiles()
        {
            var usersList = new List<AccountModel>();
            try
            {

                var dataAttr = new DataAttributes()
                {
                    CommandText = "[Sso].[upGetAdminProfiles]",
                    ApplyTransaction = false,
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                    ConnectionString =
                        eFlowConfig.Instance.GetConnectionStringByName(
                            eFlowConfig.EAIDatabaseName.),
                };

                var repository = new Repository<AccountModel>(dataAttr);
                usersList = repository.GetData();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - GetAdminProfiles Failed ::Exception:" + ex);
            }

            return usersList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<AccountModel> GetUserInfoByGuid(string guid)
        {
            var usersList = new List<AccountModel>();
            try
            {

                var dataAttr = new DataAttributes()
                {
                    CommandText = "[Sso].[upGetUserInfoByGuid]",
                    ApplyTransaction = false,
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                    ConnectionString =
                           eFlowConfig.Instance.GetConnectionStringByName(
                               eFlowConfig.EAIDatabaseName.),
                    DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "guid",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = guid
                                                            }
                                                        }
                };

                var repository = new Repository<AccountModel>(dataAttr);
                usersList = repository.GetData();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - GetUserInfoByGuid Failed ::Exception:" + ex);
            }

            return usersList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isAdEntId"></param>
        /// <returns></returns>
        public static List<NameValue> GetUserRoles(string userId, bool isAdEntId)
        {
            var model = new List<NameValue>();
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {

                    var dataAttr = new DataAttributes()
                    {
                        CommandText = "sso.upGetUserRoles",
                        ApplyTransaction = false,
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                        ConnectionString =
                            eFlowConfig.Instance.GetConnectionStringByName(
                                eFlowConfig.EAIDatabaseName.),
                        DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "userId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = userId
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.Boolean,
                                                                Name = "isAdEntId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = isAdEntId
                                                            }
                                                        }
                    };

                    var repository = new Repository<NameValue>(dataAttr);
                    model = repository.GetData();
                }
                else
                {
                    Logger.Instance.Error("UaPortalDbHelper - GetUserRoles UserID is NULL OR Blank");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - GetUserRoles Failed:" + userId +
                                    "::Exception:" + ex);
            }

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userId"></param>
        /// <param name="isAdEntId"></param>
        /// <returns></returns>
        public static List<AccountModel> GetUsersList(string firstName, string lastName, string userId, bool isAdEntId)
        {
            var model = new List<AccountModel>();

            try
            {
                var dataAttr = new DataAttributes()
                      {
                          CommandText = "sso.upGetUsersList",
                          ApplyTransaction = false,
                          CommandTimeout = 120,
                          CommandType = CommandType.StoredProcedure,
                          ConnectionString =
                              eFlowConfig.Instance.GetConnectionStringByName(
                                  eFlowConfig.EAIDatabaseName.),
                          DataParameters = new[]
                                                        {
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "firstname",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = string.IsNullOrEmpty(firstName)?"": firstName
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "lastname ",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = string.IsNullOrEmpty(lastName)?"":lastName
                                                            },
                                                             new DataParameter()
                                                            {
                                                                DbType = DbType.String,
                                                                Name = "userid ",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = string.IsNullOrEmpty(userId)?"":userId
                                                            },
                                                            new DataParameter()
                                                            {
                                                                DbType = DbType.Boolean,
                                                                Name = "isAdEntId",
                                                                ParamDirection = ParameterDirection.Input,
                                                                Value = isAdEntId
                                                            }
                                                        }
                      };

                var repository = new Repository<AccountModel>(dataAttr);
                model = repository.GetData();

            }
            catch (Exception ex)
            {
                Logger.Instance.Error(
                                    "UaPortalDbHelper - GetUsersList Failed:" + userId +
                                    "::Exception:" + ex);
            }

            return model;
        }
        
    }
}
