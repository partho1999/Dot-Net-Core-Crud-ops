using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace MyStore.Pages.NewFolder
{
    public class editModel : PageModel
    {
        public ClientInfo cilentinfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql,connection)) 
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cilentinfo.id = "" + reader.GetInt32(0);
                                cilentinfo.name = reader.GetString(1);
                                cilentinfo.email = reader.GetString(2);
                                cilentinfo.phone = reader.GetString(3);
                                cilentinfo.address = reader.GetString(4);
                                
                            }
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            cilentinfo.id= Request.Form["id"];
            cilentinfo.name = Request.Form["name"];
            cilentinfo.email = Request.Form["email"];
            cilentinfo.phone = Request.Form["phone"];
            cilentinfo.address = Request.Form["address"];

            if (cilentinfo.name.Length == 0 || cilentinfo.email.Length == 0 || cilentinfo.phone.Length == 0 || cilentinfo.address.Length == 0)
            {
                errorMessage = "All fields are requered!";
                return;
            }

            try 
            {

                String connectionString = "Data Source=DESKTOP-IU7EVDU;Initial Catalog=mystore;Integrated Security=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address" +
                                 " WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", cilentinfo.name);
                        command.Parameters.AddWithValue("@email", cilentinfo.email);
                        command.Parameters.AddWithValue("@phone", cilentinfo.phone);
                        command.Parameters.AddWithValue("@address", cilentinfo.address);
                        command.Parameters.AddWithValue("@id", cilentinfo.id);

                        command.ExecuteNonQuery();

                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/NewFolder");
        }   
    }
}
