using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;

public class ScheduleEdit : PageModel
{
    public string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
    
    [BindProperty(SupportsGet = true)]
    public string GP { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string CourseCode { get; set; }

    [BindProperty(SupportsGet = true)]
    public string RoomName { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string Day { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string LecTime { get; set; }
    
    public void OnGet()
    {
        if (!string.IsNullOrEmpty(GP))
        {
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT GP, CourseCode, RoomName, Day, LecTime FROM Schedule WHERE GP = @GP";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@GP", GP);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
                
            if (reader.Read())
            {
                GP = reader.GetInt32(0).ToString();
                CourseCode = reader.GetString(1);
                RoomName = reader.GetString(2);
                Day = reader.GetString(3);
                LecTime = reader.GetTimeSpan(4).ToString();
            }
        }
    }
    
    public void OnPost()
    {
        SqlConnection con = new SqlConnection(conString);
        Console.WriteLine("connected");
        string querystring = "UPDATE Schedule SET GP=@GP, CourseCode=@CourseCode, RoomName=@RoomName, Day=@Day, LecTime=@LecTime WHERE GP = @GP";
        
        SqlCommand cmd1 = new SqlCommand(querystring, con);
        cmd1.Parameters.AddWithValue("@GP", GP);
        cmd1.Parameters.AddWithValue("@CourseCode", CourseCode);
        cmd1.Parameters.AddWithValue("@RoomName", RoomName);
        cmd1.Parameters.AddWithValue("@Day", Day);
        cmd1.Parameters.AddWithValue("@LecTime", LecTime);
        try
        {
            con.Open();
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
    }
    
}