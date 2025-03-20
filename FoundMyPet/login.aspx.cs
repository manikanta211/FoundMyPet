using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoundMyPet
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.Cookies["UserEmail"] != null)
            {
                email.Value = Request.Cookies["UserEmail"].Value;
                rememberMe.Checked = true; 
            }
        }
        protected void Login_ServerClick(object sender, EventArgs e)
        {
            string userEmail = email.Value.Trim();

            if (!string.IsNullOrEmpty(userEmail))
            {
                Session["UserEmail"] = userEmail;
              
                if (rememberMe.Checked)
                {
                    HttpCookie userCookie = new HttpCookie("UserEmail", userEmail);
                    userCookie.Expires = DateTime.Now.AddDays(30); 
                    Response.Cookies.Add(userCookie);
                }
                else
                {
                    if (Request.Cookies["UserEmail"] != null)
                    {
                        HttpCookie userCookie = new HttpCookie("UserEmail");
                        userCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(userCookie);
                    }
                }

                Response.Redirect("MyPets.aspx"); 
            }
            else
            {
                Response.Write("<script>alert('Please enter a valid email address!');</script>");
            }
        }
    }
}