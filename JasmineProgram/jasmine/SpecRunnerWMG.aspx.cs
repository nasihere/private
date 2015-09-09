using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WF.EAI.Web.UCA.RetailBanking.Wealth.Views.TestNDebug.jasmine
{
    public partial class SpecRunnerWMG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["json"] = Request.Form["json"];

        }
    }
}