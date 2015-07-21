using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Globalization;

namespace WF.UAP.UA.UCA
{
    public partial class UCARouter : System.Web.UI.Page
    {
        public string ucaRouterLoadTimeA = string.Empty;
        public string ucaRouterLoadTimeB = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucaRouterLoadTimeB = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture);
            GetTokenRouteToMain();
        }


        public void GetTokenRouteToMain()
        {
            //var token = GetAccessToken();
            var token = "";

            var SessionID = "";
            if (Request.QueryString["SessionID"] == null)
            {
                //SessionID = "ghoci3rsuuzf0zs5gaphuswe"; //sahan XMl
                //SessionID = "ghoci3rsuuzf0zs5gaphuswe";
                //SessionID = "lq4katkwgn15poazs5l5pfrx"; //HMDA and RECollateral applicable
                //SessionID = "jg5qkmui4wo4ly2qpaagmx2a";
                SessionID = "pgx03r0ubrf5w1ygwvuurmks";
            }
            else
            {
                SessionID = Request.QueryString["SessionID"];
            }

            var LOB = ""; 
            if (Request.Form["LOB"] == null) 
            {
                LOB = "HEQ";
            }
            else
            {
                LOB = Request.Form["LOB"];
            }

            var FromSCSPrevious = "";
            if (Request.QueryString["isPrevNavigation"] == null)
            {
                FromSCSPrevious = "N";
            }
            else
            {
                FromSCSPrevious = Request.QueryString["isPrevNavigation"];
            }

            ucaRouterLoadTimeA = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture);
            ResponseRedirect("RetailBanking/Wealth/Wealth.aspx", token, SessionID, LOB, FromSCSPrevious, ucaRouterLoadTimeB, ucaRouterLoadTimeA);
        }

        public static class Url
        {
            public static string Host = ConfigurationManager.AppSettings["TokenServiceHost"];
            public static string ApiName = ConfigurationManager.AppSettings["TokenServiceApi"];
            public static string RequestBody = ConfigurationManager.AppSettings["TokenServiceKey"];
        }

        public string GetAccessToken()
        {
            var httpContent = new StringContent(Url.RequestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using (var requestClient = new HttpClient())
            {
                var response = requestClient.PostAsync(Url.Host + Url.ApiName, httpContent).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resultContent = response.Content.ReadAsStringAsync();
                    var accessToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultContent.Result)["access_token"];
                    return accessToken;
                }
                return response.StatusCode.ToString();
            }
        }

        private void ResponseRedirect(string postbackUrl, string tokenId, string SessionID, string LOB,
            string FromSCSPrevious, string ucaB, string ucaA)
        {
            Response.Clear();

            var sb = new System.Text.StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", postbackUrl);
            sb.AppendFormat("<input type='hidden' name='access_token' value='{0}'>", tokenId);
            sb.AppendFormat("<input type='hidden' name='SessionID' value='{0}'>", SessionID);
            sb.AppendFormat("<input type='hidden' name='LOB' value='{0}'>", LOB);
            sb.AppendFormat("<input type='hidden' name='FromSCSPrevious' value='{0}'>", FromSCSPrevious);
            sb.AppendFormat("<input type='hidden' name='loadTimeB' value='{0}'>", ucaB);
            sb.AppendFormat("<input type='hidden' name='loadTimeA' value='{0}'>", ucaA);
            // Other params go here
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());
            //Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private string GetSystemName()
        {
            var systemName = System.Environment.MachineName;
            return systemName;
        }

        //class UcaWebApiClient
        //{
        //    readonly WebClient webClient = new WebClient();
        //    readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        //    public Dictionary<string, string> serviceResponse;

        //    public void GetAccessToken()
        //    {
        //        webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //        serviceResponse = jsonSerializer.Deserialize<Dictionary<string, string>>(webClient.UploadString(Url.Host + Url.ApiName, Url.RequestBody));
        //    }
        //}
    }

}