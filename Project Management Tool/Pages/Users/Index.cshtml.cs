using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Project_Management_Tool.Pages.Users
{
    public class IndexModel : PageModel
    {

        [HttpPost]
        public IActionResult UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            Console.WriteLine("JELLLOOO");
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=userInfo;Integrated Security=True";
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE users SET done = @isChecked WHERE id = @userId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@isChecked", request.IsChecked);
                        command.Parameters.AddWithValue("@userId", request.UserId);
                        command.ExecuteNonQuery();
                    }
                }

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.ToString() });
            }
        }

        public List<UserInfo> listUsers = new List<UserInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=userInfo;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT  * FROM users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                UserInfo userInfo = new UserInfo();
                                userInfo.done = reader.GetBoolean(0);
                                userInfo.id = "" + reader.GetInt32(1);
                                userInfo.name = reader.GetString(2);
                                userInfo.email = reader.GetString(3);
                                userInfo.phone = reader.GetString(4);
                                userInfo.task = reader.GetString(5);
                                userInfo.admin_perms = reader.GetString(6);
                                userInfo.created_at = reader.GetDateTime(7).ToString();

                                listUsers.Add(userInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class UpdateStatusRequest
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("isChecked")]
        public bool IsChecked { get; set; }
    }

    public class UserInfo
    {
        public bool done;
        public String id;
        public String name;
        public String email;
        public String phone;
        public String task;
        public String admin_perms;
        public String created_at;
    }
}