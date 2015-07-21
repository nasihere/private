using System.Collections.Generic;

using WF.EAI.Model.ViewModels.CUSPSearch;
using WF.EAI.Entities.domain.cusp.Search;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Model.DTO;
using WF.EAI.Entities.domain.cusp.Core;
using WF.EAI.Entities.domain.uca.Common;
using WF.UAP.UASF.App.Host.WebApi.Contract;

namespace WF.EAI.BLL.BO.CUSPSearch.Helper
{
    public class SearchHelper
    {
        public static SearchApplicationResultsModel GetModelObject(SearchApplicationResultEntityCollection searchAppResultEntityCollection)
        {
            SearchApplicationResultsModel searchApplicationResultsModel = new SearchApplicationResultsModel();
            searchApplicationResultsModel.SearchApplicationResultModels = new List<SearchApplicationResultModel>();
                        

            foreach (SearchApplicationResultEntity searchAppResEntity in searchAppResultEntityCollection.SearchApplicationResultEntitys)
            {
                SearchApplicationResultModel searchAppResultModel = new SearchApplicationResultModel()
                {
                     App_ID                            =searchAppResEntity.App_ID                   ,
                     Co_App_First_Name                 =searchAppResEntity.Co_App_First_Name        ,
                     Co_App_Last_Name                  =searchAppResEntity.Co_App_Last_Name         ,
                     Co_App_SSN                        =searchAppResEntity.Co_App_SSN               ,
                     Date                              =searchAppResEntity.Date                     ,
                     Dealer_Name                       =searchAppResEntity.Dealer_Name              ,
                     First_Name                        =searchAppResEntity.First_Name               ,
                     Last_Name                         =searchAppResEntity.Last_Name                ,                                                       
                     Q_UnderW_Assign                   =searchAppResEntity.Q_UnderW_Assign          ,                                                       
                     Queue_Status                      =searchAppResEntity.Queue_Status             ,                                                       
                     SSN                               =searchAppResEntity.SSN                      ,                                                       
                     Source                            =searchAppResEntity.Source                   ,                                                       
                     Status                            =searchAppResEntity.Status                   ,                                                       
                     Shaw_Account_Number               =searchAppResEntity.Shaw_Account_Number      ,                                                       
                     Owning_AU_Name                    =searchAppResEntity.Owning_AU_Name           ,                                                       
                     Closing_Branch_AU                 =searchAppResEntity.Closing_Branch_AU        ,                                                       
                     Originating_Branch_AU             =searchAppResEntity.Originating_Branch_AU    ,                                                       
                     Zip_code                          =searchAppResEntity.Zip_code                 ,                                                       
                     State_of_Residence                =searchAppResEntity.State_of_Residence       ,                                                       
                     Loan_ID                           =searchAppResEntity.Loan_ID                  ,
                     Middle_Initial                    =searchAppResEntity.Middle_Initial           ,
                     Client                            =searchAppResEntity.Client                   ,
                     Merchant                          =searchAppResEntity.Merchant                 ,
                     Product                           =searchAppResEntity.Product                  ,
                     Amount                            =searchAppResEntity.Amount                   ,
                     Address                           =searchAppResEntity.Address                  ,
                     Phone                             =searchAppResEntity.Phone                    ,
                     Phone_Type                        =searchAppResEntity.Phone_Type               ,
                     FullAddress                       =searchAppResEntity.FullAddress
                };
                searchApplicationResultsModel.SearchApplicationResultModels.Add(searchAppResultModel);
            }
            return searchApplicationResultsModel;
        }

        public static AppDataHeader GetModelObject(RequestDto<CUSPRequestHeader> request)
        {
            AppDataHeader appDataHeader = new AppDataHeader()
            {
                ServiceUri = request.RequestHeader.ServiceUri,
                CurrentTab = request.RequestHeader.ViewName.ToString(),
                ApplicationId = request.RequestHeader.ApplicationId

            };
            return appDataHeader;
        }

        public static AppDataHeader GetModelObject(RequestDto<SearchRequestHeader> request)
        {
            AppDataHeader appDataHeader = new AppDataHeader()
            {
                ServiceUri = request.RequestHeader.ServiceUri,
                CurrentTab = request.RequestHeader.ViewName.ToString(),
                ApplicationId = request.RequestHeader.ApplicationId

            };
            return appDataHeader;
        }

        public static CUSPAppDataHeader GetModelObject(CUSPRequestHeader request)
        {
            CUSPAppDataHeader cuspAppDataHeader = new CUSPAppDataHeader()
            {
                AcapsFunction = request.AcapsFunction,
                CurrentLocation = request.CurrentLocation
            };
            return cuspAppDataHeader;
        }

        public static UserModel GetModelObject(WF.EAI.Entities.domain.c2c.User user)
        {
            UserModel userModel = new UserModel()
            {
                 LoginUseridValue = user.LoginUseridValue,
                 LoginPasswordValue = user.LoginPasswordValue,
                 Role = user.Role
            };
            return userModel;
        }
    }
}
 