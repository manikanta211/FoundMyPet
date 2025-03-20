using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoundMyPet
{
    public partial class MyPets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            string userEmail = Session["UserEmail"] as string;
            if (string.IsNullOrEmpty(userEmail))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                string query = "SELECT pet_id, pet_name, pet_photo, contact_details, lost_location FROM Pets WHERE contact_details = @userEmail AND is_lost = 1";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userEmail", userEmail);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
              
                List<object> pets = new List<object>();
                while (reader.Read())
                {
                    pets.Add(new
                    {
                        pet_id = reader["pet_id"],
                        pet_name = reader["pet_name"],
                        pet_photo = reader["pet_photo"],
                        contact_details = reader["contact_details"],
                        lost_location = reader["lost_location"]
                    });
                }
                con.Close();
                
                if (pets.Count > 0)
                {
                    RepeatInformation.DataSource = pets;
                    RepeatInformation.DataBind();
                    petsContainer.Visible = true;
                    noDataMessage.Visible = false;
                }
                else
                {
                    petsContainer.Visible = false;
                    noDataMessage.Visible = true;
                }
            }
        }

        [WebMethod]
        public static int found(string ID)
        {
            if (!int.TryParse(ID, out int petId)) 
            {
                return 0;
            }

            string query = "UPDATE Pets SET is_lost = 0 WHERE pet_id = @PetID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PetID", petId);

                    try
                    {
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0 ? 1 : 0;
                    }
                    catch (Exception ex)
                    {

                        return 0;
                    }
                }
            }
        }
    }
}
