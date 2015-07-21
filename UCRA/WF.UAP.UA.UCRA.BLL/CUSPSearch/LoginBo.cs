using Newtonsoft.Json;
using System;

using WF.EAI.BLL.BO.CUSPSearch.Helper;
using WF.EAI.Entities.constants.uca;
using WF.EAI.Entities.domain.cusp.Core;
using WF.EAI.Model;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CUSPSearch;
using WF.EAI.Utils;

namespace WF.EAI.BLL.BO.CUSPSearch
{
    using WF.EAI.BLL.CUSP;
    using WF.EAI.Model.ViewModels.CUSPSearch;
    using WF.UAP.UASF.App.Host.WebApi.Contract;

    public class LoginBo : SearchBo
    {
        public LoginBo()
        {
            ViewName = Model.Lookup.CUSPSearchViewName.Login;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">RequestDto<CUSPRequestHeader></param>
        /// <returns>ViewModel</returns>
        public UserModel Invoke(RequestDto<CUSPRequestHeader> request)
        {
            CUSPAppDataHeader appHeader = SearchHelper.GetModelObject(request.RequestHeader);
            WF.EAI.Entities.domain.c2c.User user = new WF.EAI.Entities.domain.c2c.User();
            try
            {
                if (request.RequestHeader.ServiceUri == Lookup.ServiceUri.Login.ToString())
                {
                        string  jsonText= ACAPSAppDataServiceAdapter.ProcessLogin(appHeader);                        
                        object jsonObject = JsonConvert.DeserializeObject(jsonText);

                        if (!string.IsNullOrEmpty(Convert.ToString(jsonObject)))
                        {
                            appHeader = JsonMapper<CUSPAppDataHeader>.JsonTextToAppDataHeader<CUSPAppDataHeader>(Convert.ToString(jsonObject));

                            if (appHeader.ApplicationData.Contains("WF.EAI.Entities.domain.c2c.User"))
                            {
                                user = JsonMapper<WF.EAI.Entities.domain.c2c.User>.JsonTextToAppDataHeader<WF.EAI.Entities.domain.c2c.User>(Convert.ToString(jsonObject));                                
                            }
                        }
                    return SearchHelper.GetModelObject(user);
                }
            }
            catch (Exception ex)
            {
                LogErrorInfo("Error occured in CreditAnalysisBo", ex);
                return null;
            }
            return null;
        }
    }
}
