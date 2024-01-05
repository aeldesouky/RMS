using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RMS.Pages;

public class Schedule : PageModel
{
    public string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
    public DataTable ScheduleTable { get; private set; }

    [BindProperty(SupportsGet = true)]
    public string GP { get; set; }

    [BindProperty(SupportsGet = true)]
    public string CourseCode { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Day { get; set; }

    [BindProperty(SupportsGet = true)]
    public string RoomName { get; set; }

    public void OnGet()
    {
        SqlConnection con = new SqlConnection(conString);
        string queryString = "SELECT GP, CourseCode, RoomName, Day, LecTime FROM Schedule WHERE (GP = @GP OR @GP IS NULL) AND (CourseCode = @CourseCode OR @CourseCode IS NULL) AND (Day = @Day OR @Day IS NULL) AND (RoomName = @RoomName OR @RoomName IS NULL)";

        SqlCommand command = new SqlCommand(queryString, con);
        command.Parameters.AddWithValue("@GP", (object)GP ?? DBNull.Value);
        command.Parameters.AddWithValue("@CourseCode", (object)CourseCode ?? DBNull.Value);
        command.Parameters.AddWithValue("@Day", (object)Day ?? DBNull.Value);
        command.Parameters.AddWithValue("@RoomName", (object)RoomName ?? DBNull.Value);
        con.Open();
        SqlDataReader reader = command.ExecuteReader();
        ScheduleTable = new DataTable();
        ScheduleTable.Load(reader);
    }

    public IActionResult OnPost()
    {
        return RedirectToPage("/Schedule", new { GP = GP });
    }

    
}