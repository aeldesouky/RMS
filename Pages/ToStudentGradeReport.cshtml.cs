using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RMS.Pages;
[BindProperties(SupportsGet = true)]
public class ToStudentGradeReport : PageModel
{
    public DataTable GradeReport { get; private set; }
    public string RepNum { get; set; }
    public int StudentID { get; set; }
    public string StoredUserId { get; private set; }


    public void OnGet()
    {
        StoredUserId = HttpContext.Session.GetString("UserID");
        StudentID = int.Parse(StoredUserId);
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
}