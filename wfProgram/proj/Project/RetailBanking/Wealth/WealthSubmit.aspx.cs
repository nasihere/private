using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using WF.EAI.Model.DTO.UCA;
using WF.EAI.Model.ViewModels.UCA;
using WF.EAI.Web.UCA.RetailBanking.Wealth.Util;
using WF.UAP.UDB.Repository.Domain.Entities;

namespace xxxxProjec.....UCA.RetailBanking.Wealth
{
    public partial class WealthSubmit : System.Web.UI.Page
    {
        public string tokenId = string.Empty;
        public string SessionID = string.Empty;

        public string rootJSON = string.Empty;
        public object wmgJsonData = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionID = Request.Form["SessionID"];
            rootJSON = Request.Form["rootJSON"];
            tokenId = Request.Form["access_token"];
            wmgJsonData = setWealthApp(SessionID, rootJSON, tokenId);
            Response.Expires = -1;
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(wmgJsonData);
            //Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private object setWealthApp(string SessionID, string rootJSON, string accessToken)
        {
            var model = new UCAModel();
            var result = JsonMapper<ResponseDto<UCAModel, DataModel>>.TextToObject(rootJSON);
            model.ucaViewModel = result.ViewModel.ucaViewModel;

           // model.ucaViewModel.applicationInfo.AUM = "008";
            var dto1 = new RequestDto<UCARequestHeader>()
            {
                RequestHeader = new UCARequestHeader()
                {
                    SessionID = SessionID,
                    ucaViewModel = model.ucaViewModel
                }

            };
            var serviceHost = ConfigurationManager.AppSettings["DataServiceHost"];
            var apiName = ConfigurationManager.AppSettings["DataServiceApiSubmit"];

            var req1 = new HttpRequestMessage(HttpMethod.Post, serviceHost + apiName)
            {
                Content = new ObjectContent<RequestDto<UCARequestHeader>>(dto1, new JsonMediaTypeFormatter())
            };
            req1.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + tokenId);
            using (var client1 = new HttpClient())
            {

                HttpResponseMessage response = client1.SendAsync(req1).Result;
                if (response.Content != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseMessage = response.Content.ReadAsStringAsync().Result;
                    return responseMessage;
                }
            }
            return "";
          
        }
    }
}