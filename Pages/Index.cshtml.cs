using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RMS.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
    
    public IActionResult OnPostStudentLogin(int userId, string password)
    {
        bool isvalid = CheckStudentCredentials(userId, password);
        
        if (isvalid)
        {
            HttpContext.Session.SetString("UserID", userId.ToString());
            return RedirectToPage("/ToStudent"); 
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }


    public IActionResult OnPostAdminLogin(int userId, string password)
    {
        bool isvalid = CheckAdminCredentials(userId, password);
        
        if (isvalid)
        {
            HttpContext.Session.SetString("UserID", userId.ToString());
            return RedirectToPage("/adminPage");
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }
    public IActionResult OnPostITLogin(int userId, string password)
    {
        bool isvalid = CheckITCredentials(userId, password);
        
        if (isvalid)
        {
            HttpContext.Session.SetString("UserID", userId.ToString());
            return RedirectToPage("/ToIt");
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }
    public IActionResult OnPostProfLogin(int userId, string password)
    {
        bool isvalid = CheckProfCredentials(userId, password);
        
        if (isvalid)
        {
            HttpContext.Session.SetString("UserID", userId.ToString());
            return RedirectToPage("/ToProfTa");
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }
    private bool CheckProfCredentials(int userId, string password)
    {
        string ConString = "Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        using (SqlConnection con = new SqlConnection(ConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) FROM Login WHERE ID = @userId AND Password = @password AND Type = 'Prof'";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@password", password);

            int count = (int)command.ExecuteScalar();
            return count > 0; 
        }
    }
    private bool CheckITCredentials(int userId, string password)
    {
        string ConString = "Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        using (SqlConnection con = new SqlConnection(ConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) FROM Login WHERE ID = @userId AND Password = @password AND Type = 'IT'";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@password", password);

            int count = (int)command.ExecuteScalar();
            return count > 0; 
        }
    }
    private bool CheckStudentCredentials(int userId, string password)
    {
        string ConString = "Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        using (SqlConnection con = new SqlConnection(ConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) FROM Login WHERE ID = @userId AND Password = @password AND Type = 'Student'";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@password", password);

            int count = (int)command.ExecuteScalar();
            return count > 0; 
        }
    }
    
    
    private bool CheckAdminCredentials(int userId, string password)
    {
        string ConString = "Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        using (SqlConnection con = new SqlConnection(ConString))
        {
            con.Open();
            string query = "SELECT COUNT(*) FROM Login WHERE ID = @userId AND Password = @password AND Type = 'Admin'";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@password", password);

            int count = (int)command.ExecuteScalar();
            return count > 0; 
        }
    }
    
}