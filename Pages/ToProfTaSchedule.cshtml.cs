using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json; 
using System.Data.SqlClient;
namespace RMS.Pages;

public class ToProfTaSchedule : PageModel
{
    public int ReceivedValue { get; set; }
    public string CourseCode { get; set; }
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
            string queryStr = "SELECT CourseCode FROM ProfessorsCourses WHERE ProfID = @ID";
            SqlCommand com = new SqlCommand(queryStr, con);
            com.Parameters.AddWithValue("@ID", ReceivedValue); 

            con.Open();
            SqlDataReader read = com.ExecuteReader();

            List<string> courseCodesf = new List<string>();

            while (read.Read())
            {
                courseCodesf.Add(read["CourseCode"].ToString());
            }
            string serializedCourseCodes = JsonConvert.SerializeObject(courseCodesf);

            HttpContext.Session.SetString("CourseCodes", serializedCourseCodes);
            foreach (var code in courseCodesf)
            {
                Console.WriteLine("Course Code: " + code);
            }
            //Console.WriteLine("GP"+ @CourseCode);
            read.Close();
            
            
            ScheduleTable = new DataTable();

            foreach (var code in courseCodesf)
            {
                string queryString = "SELECT GP, CourseCode, RoomName, Day, LecTime FROM Schedule WHERE CourseCode = @GP";
                SqlCommand command = new SqlCommand(queryString, con);
                command.Parameters.AddWithValue("@GP", code);

                SqlDataReader reader = command.ExecuteReader();
                DataTable tempTable = new DataTable();
                tempTable.Load(reader);

                ScheduleTable.Merge(tempTable);
            }
            
            
            
            
            

        }
        
    }
    
}