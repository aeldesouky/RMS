using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;
[BindProperties(SupportsGet = true)]

public class ScheduleAdd : PageModel
{
    public string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
    public string courseCode { set; get; }

    public List<string> CourseCodes { get; set; }
    public string GP { get; set; }
    
     public string CourseCode { get; set; }
     public List<string> RoomNames { get; set; }
     public string RoomName { get; set; }

     public string Day { get; set; }

     public string LecTime { get; set; }

    public void OnGet()
    {
        CourseCodes = new List<string>();
        RoomNames = new List<string>();
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        string GetCourseCode = "SELECT CourseCode FROM Course";
        string RoomName = "SELECT Name FROM Room";

        using (SqlConnection con = new SqlConnection(conString))
        {
            SqlCommand GetCourseCodeCommand = new SqlCommand(GetCourseCode, con);
            SqlCommand GetRoomNameCommand = new SqlCommand(RoomName, con);

            try
            {
                con.Open();
                SqlDataReader courseCodeReader = GetCourseCodeCommand.ExecuteReader();
                while (courseCodeReader.Read())
                {
                    CourseCodes.Add(courseCodeReader["CourseCode"].ToString());
                }
                Console.WriteLine("Course codes fetched successfully");
                con.Close();
                con.Open();
                SqlDataReader GetRoomNameReader = GetRoomNameCommand.ExecuteReader();
                while (GetRoomNameReader.Read())
                {
                    RoomNames.Add(GetRoomNameReader["Name"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
    
    public IActionResult OnPost()
    {
        SqlConnection con = new SqlConnection(conString);

        con.Open();

        SqlCommand selectCommand = new SqlCommand("SELECT TOP 1 Entry FROM Schedule ORDER BY Entry DESC", con);

        object result = selectCommand.ExecuteScalar();
        int lastEntry = (result != null) ? Convert.ToInt32(result) : 0;

        int newEntry = lastEntry + 1;

        string querystring =
            "INSERT INTO Schedule (Entry, GP, CourseCode, RoomName, Day, LecTime) VALUES (@Entry, @GP, @CourseCode, @RoomName, @Day, @LecTime)";

        SqlCommand cmd1 = new SqlCommand(querystring, con);
        cmd1.Parameters.AddWithValue("@Entry", newEntry);
        cmd1.Parameters.AddWithValue("@GP", GP);
        cmd1.Parameters.AddWithValue("@CourseCode", CourseCode);
        cmd1.Parameters.AddWithValue("@RoomName", RoomName);
        cmd1.Parameters.AddWithValue("@Day", Day);
        cmd1.Parameters.AddWithValue("@LecTime", LecTime);

        try
        {
            cmd1.ExecuteNonQuery();
            Console.WriteLine("Record Updated Successfully");
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            con.Close();
        }

        return RedirectToPage("/Schedule");
    }
}
    