using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
namespace RMS.Pages;

public class ToStudentschedule : PageModel
{
    public int ReceivedValue { get; set; }
    public string GP { get; set; }
    public string StoredUserId { get; private set; }
    
    public DataTable ScheduleTable { get; private set; }
    [BindProperty(SupportsGet = true)]
    public string RoomName { get; set; }
    
    public void OnGet()
    {
        StoredUserId = HttpContext.Session.GetString("UserID");
        ReceivedValue = int.Parse(StoredUserId);
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        
        using (SqlConnection con = new SqlConnection(conString))
        {
            string queryStr = "SELECT GP FROM Student WHERE ID = @ID";
            SqlCommand com = new SqlCommand(queryStr, con);
            com.Parameters.AddWithValue("@ID", ReceivedValue); 

            con.Open();
            SqlDataReader read = com.ExecuteReader();

            if (read.Read())
            {
                GP = read["GP"].ToString();
            }
            Console.WriteLine("GP"+ @GP);
            read.Close();
            
            
            string queryString = "SELECT GP, CourseCode, RoomName, Day, LecTime FROM Schedule WHERE GP = @GP";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@GP", GP);
            SqlDataReader reader = command.ExecuteReader();
            ScheduleTable = new DataTable();
            ScheduleTable.Load(reader);
            
            
            
            
            

        }
        
    }
}