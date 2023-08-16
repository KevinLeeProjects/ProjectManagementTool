using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Project_Management_Tool.Pages.Users
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            
        }

        public void OnPost() 
        {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.task = Request.Form["task"];
            userInfo.admin_perms = Request.Form["admin_perms"];

            if(userInfo.name.Length == 0 || userInfo.email.Length == 0 || userInfo.phone.Length == 0 ||  userInfo.task.Length == 0 || userInfo.admin_perms.Length == 0)
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
                    String sql = "INSERT into users " +
                        "(name, email, phone, task, admin_perms) VALUES " +
                        "(@name, @email, @phone, @task, @admin_perms);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@task", userInfo.task);
                        command.Parameters.AddWithValue("@admin_perms", userInfo.admin_perms);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfo.name = "";
            userInfo.email = "";
            userInfo.phone = "";
            userInfo.task = "";
            userInfo.admin_perms = "";
            successMessage = "New user added successfully";

            Response.Redirect("/Users/Index");
        }
    }
}
