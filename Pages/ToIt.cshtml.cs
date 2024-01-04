using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace RMS.Pages;

public class ToIt : PageModel
{
    public int ReceivedValue { get; set; }
    public string Name { get; set; }
    public string StoredUserId { get; private set; }
    public void OnGet()
    {
        StoredUserId = HttpContext.Session.GetString("UserID");
        ReceivedValue = int.Parse(StoredUserId);
        RetrieveUserDetails(); 
    }
    public void RetrieveUserDetails()
    {
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        
        using (SqlConnection con = new SqlConnection(conString))
        {
            string queryString = "SELECT Name FROM Faculty WHERE ID = @ID";
            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@ID", ReceivedValue); 

            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Name = reader["Name"].ToString();
            }

            reader.Close();
        }
    }

    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Index");
    }
    
}