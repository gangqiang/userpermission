using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPermission.ApiService
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CRM.Business.ActionResult ar = CRM.Business.BaseBusiness.AccountLogin("guy", "111111", "117");
        }
    }
}