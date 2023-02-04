using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace MyStore.Pages.NewFolder
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients";
                    using (SqlCommand command= new SqlCommand(sql, connection))
                    {
                        using ( SqlDataReader reader= command.ExecuteReader())
                            while (reader.Read())
                            {
                                ClientInfo cilentinfo = new ClientInfo();
                                cilentinfo.id = "" + reader.GetInt32(0);
                                cilentinfo.name = reader.GetString(1);
                                cilentinfo.email = reader.GetString(2);
                                cilentinfo.phone = reader.GetString(3);
                                cilentinfo.address = reader.GetString(4);
                                cilentinfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(cilentinfo);


                            }
                    }
                }
            }

            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }

    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;

    }
}   
    
