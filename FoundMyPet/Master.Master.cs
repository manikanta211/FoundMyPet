using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoundMyPet
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {
            if (!IsPostBack)
            {
                string userEmail = Session["UserEmail"] as string;

                if (!string.IsNullOrEmpty(userEmail))
                {
                   
                    loginNavItem.Visible = false;
                    
                }
                else
                {
                    logout.Visible = false;
                }
            }
        }
    }
}