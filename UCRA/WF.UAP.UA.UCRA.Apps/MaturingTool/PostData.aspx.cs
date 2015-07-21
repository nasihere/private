// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostData.aspx.cs" company="">
//   
// </copyright>
// <summary>
//   The post data.
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
    using System.Web.UI;

    using WF.UAP.UA.UCRA.Apps.Helper;
    using WF.EAI.Entities.domain.MaturingTools;
    #endregion

    /// <summary>
    /// The post data.
    /// </summary>
    public partial class PostData : Page
    {
        /// <summary>
        /// The root json.
        /// </summary>
        public string rootJSON = string.Empty;

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
            this.rootJSON = this.Request.Form["rootJSON"];
            this.mtJsonData = this.SaveMaturingToolsData(this.rootJSON);
            this.Response.Expires = -1;
            this.Response.ContentType = "application/json; charset=utf-8";
            this.Response.Write(this.mtJsonData);
            this.Response.End();
        }

        /// <summary>
        /// The save maturing tools data.
        /// </summary>
        /// <param name="rootJSON">
        /// The root json.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object SaveMaturingToolsData(string rootJSON)
        {
            var data = JsonMapper<MaturingToolsEntity>.TextToObject(rootJSON);

            var req1 = new HttpRequestMessage(
                HttpMethod.Post, 
                ConfigurationManager.AppSettings["MaturingToolWebApiUrl"] + "/SubmitMaturingToolsData/")
                           {
                               Content =
                                   new ObjectContent
                                   <
                                   MaturingToolsEntity
                                   >(
                                   data, 
                                   new JsonMediaTypeFormatter
                                   ())
                           };

            using (var client1 = new HttpClient())
            {
                HttpResponseMessage response = client1.SendAsync(req1).Result;
                if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var responseMessage = response.Content.ReadAsStringAsync().Result;
                    return responseMessage;
                }
            }

            return string.Empty;
        }
    }
}