using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Project_Management_Tool.Pages.Users
{
    public class EditModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=userInfo;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.phone = reader.GetString(3);
                                userInfo.task = reader.GetString(4);
                                userInfo.admin_perms = reader.GetString(5);
                            }
                            else
                            {
                                Console.WriteLine("No data found for the specified ID. " + id);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                errorMessage = ex.Message; 
                Console.WriteLine("ERROR " + errorMessage);
            }
        }

        public void OnPost()
        {
            userInfo.id = Request.Form["id"];
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.task = Request.Form["task"];
            userInfo.admin_perms = Request.Form["admin_perms"];

            if (userInfo.name.Length == 0 || userInfo.email.Length == 0 || userInfo.phone.Length == 0 || userInfo.task.Length == 0 || userInfo.admin_perms.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=userInfo;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE users " +
                        "SET name=@name, email=@email, phone=@phone, task=@task, admin_perms=@admin_perms " +
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@task", userInfo.task);
                        command.Parameters.AddWithValue("@admin_perms", userInfo.admin_perms);
                        command.Parameters.AddWithValue("@id", userInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Users/Index");
        }
    }
}
