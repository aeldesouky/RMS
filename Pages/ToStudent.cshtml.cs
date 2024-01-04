using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
namespace RMS.Pages;


public class ToStudent : PageModel
{
    public int ReceivedValue { get; set; }
    public string Name { get; set; }
    public string StoredUserId { get; private set; }
    public void OnGet()
    {
        StoredUserId = HttpContext.Session.GetString("UserID");
        ReceivedValue = int.Parse(StoredUserId);
        RetrieveStudentDetails(); 
    }

    public void RetrieveStudentDetails()
    {
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        
        using (SqlConnection con = new SqlConnection(conString))
        {
            string queryString = "SELECT Name FROM Student WHERE ID = @ID";
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

    public IActionResult OnPost()
    {
        return RedirectToPage("/ToStudentInfo");
    }
    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Index");
    }
}
