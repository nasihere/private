// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaturingToolsHomePage.aspx.cs" company="">
//   
// </copyright>
// <summary>
//   The maturing tools home page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.UAP.UA.UCRA.Apps.MaturingTool
{
    #region

    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web;
    using System.Web.UI;
    using WF.EAI.Entities.domain.MaturingTools;
    #endregion

    /// <summary>
    /// The maturing tools home page.
    /// </summary>
    public partial class MaturingToolsHomePage : Page
    {
        /// <summary>
        /// The user type.
        /// </summary>
        public string UserType = string.Empty;

        /// <summary>
        /// The user id.
        /// </summary>
        public string UserId = string.Empty;

        /// <summary>
        /// The account num.
        /// </summary>
        public string AccountNum = string.Empty;

        /// <summary>
        /// The mlo id.
        /// </summary>
        public string MloId = string.Empty;

        /// <summary>
        /// The agent name.
        /// </summary>
        public string AgentName = string.Empty;

        /// <summary>
        /// The cusp apps maturing options web api base url.
        /// </summary>
        public string CuspAppsMaturingOptionsWebApiBaseUrl = string.Empty;

        /// <summary>
        /// The end Date for new home loan checkbox.
        /// </summary>
        public string EndDate = string.Empty;

        /// <summary>
        /// The mt json data.
        /// </summary>
        public object mtJsonData = string.Empty;

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CuspAppsMaturingOptionsWebApiBaseUrl = ConfigurationManager.AppSettings["MaturingToolWebApiUrl"];
            EndDate = ConfigurationManager.AppSettings["endDate"];

            if (HttpContext.Current.Session["userId"] != null)
            {
                UserId = HttpContext.Current.Session["userId"].ToString();
            }

            if (HttpContext.Current.Session["mloId"] != null)
            {
                MloId = HttpContext.Current.Session["mloId"].ToString();
            }
            if (HttpContext.Current.Session["agentName"] != null)
            {
                AgentName = HttpContext.Current.Session["agentName"].ToString();
            }

            //Assign userType
            if (!string.IsNullOrEmpty(MloId) && !string.IsNullOrEmpty(AgentName))
            {
                UserType = WF.EAI.Entities.domain.MaturingTools.UserType.HEMAP.ToString();
            }
            else
            {
                UserType = WF.EAI.Entities.domain.MaturingTools.UserType.OSU.ToString();
            }

            ////TODO Remove harcoded values to use session and always comments these below lines after your testing is done.
            //UserId = "U337238";
            //UserType = "HEMAP";
            //MloId = "TestMloId";
            //AgentName = "TestAgentName";
            //AccountNum = string.Empty;

            if (Request.Form.HasKeys())
            {
                mtJsonData = GetMaturingOptionsData();
                Response.Expires = -1;
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(mtJsonData);
                Response.End();
            }
        }

        /// <summary>
        /// The get maturing options data.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        protected object GetMaturingOptionsData()
        {
            var data = new MaturingToolsEntity();
            data.UserType = UserType;
            data.CreatedBy = UserId;
            data.AgentName = AgentName;
            data.MNLSRID = MloId;
            data.AccountNum = Request.Form["accountNum"];

            var req1 = new HttpRequestMessage(HttpMethod.Post,
                ConfigurationManager.AppSettings["MaturingToolWebApiUrl"] + "/GetMaturingOptionsData/")
            {
                Content = new ObjectContent<MaturingToolsEntity>(data, new JsonMediaTypeFormatter())
            };

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