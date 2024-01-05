using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RMS.Pages.Shared;

public class GReport : PageModel
{
    public DataTable GradeReport { get; private set; }
    
    [BindProperty(SupportsGet = true)]
    public string RepNum { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string StudentID { get; set; }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(RepNum))
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT RepNum, StudentID, CourseCode, Grade FROM GradeReport WHERE RepNum = @RepNum";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@RepNum", RepNum);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            GradeReport = new DataTable();
            GradeReport.Load(reader);
            
        }
        else if (!string.IsNullOrEmpty(StudentID))
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT RepNum, StudentID, CourseCode, Grade FROM GradeReport WHERE StudentID = @StudentID";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@StudentID", StudentID);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            GradeReport = new DataTable();
            GradeReport.Load(reader);
        }
        else
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT RepNum, StudentID, CourseCode, Grade FROM GradeReport";

            SqlCommand command = new SqlCommand(queryString, con);

            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            GradeReport = new DataTable();
            GradeReport.Load(reader);
        }
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("/Report", new { RepNum = RepNum });
    }
}