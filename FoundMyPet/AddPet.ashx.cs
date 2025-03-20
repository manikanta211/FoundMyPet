using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace FoundMyPet
{
    /// <summary>
    /// Summary description for AddPet
    /// </summary>
    public class AddPet : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            if (context.Request.HttpMethod == "POST")
            {
                var petName = context.Request.Form["petName"];
                var contactDetails = context.Request.Form["contactDetails"];
                var lostLocation = context.Request.Form["lostLocation"];
                var petImage = context.Request.Files["petImage"];
             
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                string fileExt = Path.GetExtension(petImage.FileName).ToLower();

                if (Array.IndexOf(allowedExtensions, fileExt) < 0)
                {
                    context.Response.Write(JsonConvert.SerializeObject(new { success = false, message = "Invalid file type! Only JPG, PNG, and GIF are allowed." }));
                    return;
                }
                
                if (petImage.ContentLength > 5 * 1024 * 1024)
                {
                    context.Response.Write(JsonConvert.SerializeObject(new { success = false, message = "File size must be less than 5MB!" }));
                    return;
                }
             
                string safeFileName = Guid.NewGuid().ToString() + fileExt;
                string filePath = "/Assets/img/" + safeFileName;
                petImage.SaveAs(context.Server.MapPath(filePath));

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Pets (pet_name, pet_photo, contact_details, lost_location) OUTPUT INSERTED.pet_id VALUES (@name, @photo, @contact, @location)", con);
                    cmd.Parameters.AddWithValue("@name", petName);
                    cmd.Parameters.AddWithValue("@photo", filePath);
                    cmd.Parameters.AddWithValue("@contact", contactDetails);
                    cmd.Parameters.AddWithValue("@location", lostLocation);

                    int newPetId = (int)cmd.ExecuteScalar();

                    context.Response.Write(JsonConvert.SerializeObject(new
                    {
                        success = true,
                        pet_id = newPetId,
                        pet_name = petName,
                        pet_photo = filePath,
                        contact_details = contactDetails,
                        lost_location = lostLocation
                    }));
                }
            }
            else if (context.Request.HttpMethod == "GET")
            {
                int page = int.Parse(context.Request.QueryString["page"] ?? "1");
                int pageSize = 5;
                int offset = (page - 1) * pageSize;

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string query = @"
        SELECT pet_id, pet_name, pet_photo, contact_details, lost_location 
        FROM Pets 
        WHERE is_lost = 1 
        ORDER BY created_at DESC 
        OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@offset", offset);
                        cmd.Parameters.AddWithValue("@pageSize", pageSize);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var pets = new System.Collections.Generic.List<object>();

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

                            context.Response.Write(JsonConvert.SerializeObject(pets));
                        }
                    }
                }

            }
        }
        public bool IsReusable => false;
    }
}