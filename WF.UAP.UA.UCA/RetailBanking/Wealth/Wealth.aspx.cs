using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using WF.EAI.Model.DTO.UCA;
using WF.EAI.Model.ViewModels.UCA;
using System.Globalization;
using WF.EAI.Web.UCA.RetailBanking.Wealth.Util;
using ErrorMessage = WF.EAI.Model.ErrorMessage;
using WF.UAP.UDB.Repository.Domain.Entities;

namespace WF.UAP.UA.UCA.RetailBanking.Wealth
{
    public partial class Wealth : System.Web.UI.Page
    {
        public string accessToken = string.Empty;
        public string JsonData = string.Empty;
        public string SessionIDData = string.Empty;
        public string ErrorData = string.Empty;
        public string CallIsFromSCSPrevious = string.Empty;
        public string ucaRouterLoadTimeB = string.Empty;
        public string ucaRouterLoadTimeA = string.Empty;
        public string timeB = string.Empty;
        public string timeA = string.Empty;
        public string isWmgInitialData = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            timeB = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff");

            //accessToken = Request.Form["access_token"];
            //SessionIDData = Request.Form["SessionID"];
            //CallIsFromSCSPrevious = Request.Form["FromSCSPrevious"];
            //ucaRouterLoadTimeB = Request.Form["loadTimeB"];
            //ucaRouterLoadTimeA = Request.Form["loadTimeA"];
            //JsonData = getWealthAppData(accessToken);

            if (Request.QueryString["isNewApp"] == null)
            {
                isWmgInitialData = "N";
            }

            if (Request.QueryString["isNewApp"] == "Y")
            {
                isWmgInitialData = "Y";
                SessionIDData = Request.QueryString["SessionID"];
                ucaRouterLoadTimeB = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture);
                ucaRouterLoadTimeA = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture);
            }
            else
            {
                accessToken = Request.Form["access_token"];
                SessionIDData = Request.Form["SessionID"];
                CallIsFromSCSPrevious = Request.Form["FromSCSPrevious"];
                ucaRouterLoadTimeB = Request.Form["loadTimeB"];
                ucaRouterLoadTimeA = Request.Form["loadTimeA"];
                JsonData = getWealthAppData(accessToken);
            }

            timeA = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff");
        }

        private string getWealthAppData(string tokenId)
        {
            var dto = new RequestDto<UCARequestHeader>()
            {
                RequestHeader = new UCARequestHeader()
                {
                    SessionID = SessionIDData
                }
            };
            var resModel = new UCAModel
            {
                ErrorMessages = new List<ErrorMessage>()
            };

            var serviceHost = ConfigurationManager.AppSettings["DataServiceHost"];
            var apiName = ConfigurationManager.AppSettings["DataServiceApi"];
            var errorCode = 0;
            //Configure Http Request 
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, serviceHost + apiName);
            httpRequest.Content = new ObjectContent<RequestDto<UCARequestHeader>>(dto, new JsonMediaTypeFormatter());
            //httpRequest.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + tokenId);

            try
            {
                //Configure Http Client
                using (var requestClient = new HttpClient())
                {
                    HttpResponseMessage response = requestClient.SendAsync(httpRequest).Result;
                    string serializedJsonData = string.Empty;

                    if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        var result = JsonMapper<ResponseDto<UCAModel, DataModel>>.TextToObject(responseContent);

                        
                        //Before reading the Data, check if Error exists
                        if (result.ViewModel.ErrorMessages.Count == 0)
                            serializedJsonData = JsonConvert.SerializeObject(result);
                        else
                        {
                            ErrorData = JsonConvert.SerializeObject(result.ViewModel.ErrorMessages);
                        }
                        return serializedJsonData;
                    }
                    else
                    {
                        errorCode = (int)response.StatusCode;
                        throw new Exception(" Error Phrase: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                if (resModel.ErrorMessages != null)
                    resModel.ErrorMessages.Add(new ErrorMessage()
                    {
                        Message = ex.Message.Replace("'", ""),
                        Code = errorCode.ToString()
                    });

                ErrorData = JsonConvert.SerializeObject(resModel.ErrorMessages);
            }
            return string.Empty;
        }


    }
}