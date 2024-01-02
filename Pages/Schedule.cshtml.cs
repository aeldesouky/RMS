using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RMS.Pages;

public class Schedule : PageModel
{
    public DataTable ScheduleTable { get; private set; }
    
    [BindProperty(SupportsGet = true)]
    public string GP { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string RoomName { get; set; }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(GP))
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT GP, CourseCode, RoomName, Day, LecTime FROM Schedule WHERE GP = @GP";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@GP", GP);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            ScheduleTable = new DataTable();
            ScheduleTable.Load(reader);
            
        }
        else if (!string.IsNullOrEmpty(RoomName))
        {
            string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT GP, CourseCode, RoomName, Day, LecTime FROM Schedule WHERE RoomName = @RoomName";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@RoomName", RoomName);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            ScheduleTable = new DataTable();
            ScheduleTable.Load(reader);
        }
        else
        {
            string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT GP, CourseCode, RoomName, Day, LecTime FROM Schedule";

            SqlCommand command = new SqlCommand(queryString, con);

            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            ScheduleTable = new DataTable();
            ScheduleTable.Load(reader);
        }
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("/Schedule", new { GP = GP });
    }
}