using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages
{
    [BindProperties(SupportsGet = true)]
    public class ToItAddProfCourse : PageModel
    {
        public string courseCode { set; get; }
        public int profID { set; get; }
        public List<string> CourseCodes { get; set; }
        public List<string> ProfNames { get; set; }

        public List<int> profIDs { get; set; }

        public void OnGet()
        {
            CourseCodes = new List<string>();
            ProfNames = new List<string>();

            profIDs = new List<int>();
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            string GetCourseCode = "SELECT CourseCode FROM Course";
            string GetprofIDs = "SELECT ID,Name FROM Faculty";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand GetCourseCodeCommand = new SqlCommand(GetCourseCode, con);
                SqlCommand GetprofIDsCommand = new SqlCommand(GetprofIDs, con);

                try
                {
                    con.Open();
                    
                    SqlDataReader GetprofIDsReader = GetprofIDsCommand.ExecuteReader();
                    while (GetprofIDsReader.Read())
                    {
                        profIDs.Add(GetprofIDsReader.GetInt32(0));
                        ProfNames.Add(GetprofIDsReader["Name"].ToString());
                    }
                    Console.WriteLine("profIDs fetched successfully");
                    con.Close();
                    con.Open();
                    SqlDataReader courseCodeReader = GetCourseCodeCommand.ExecuteReader();
                    while (courseCodeReader.Read())
                    {
                        CourseCodes.Add(courseCodeReader["CourseCode"].ToString());
                    }
                    Console.WriteLine("Course codes fetched successfully");

                   
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
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            string ProfCoursetQuery = "INSERT INTO ProfessorsCourses (ProfID, CourseCode) VALUES (@ID, @Code)";
        

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand ProfCourse = new SqlCommand(ProfCoursetQuery, con);            
                ProfCourse.Parameters.AddWithValue("@Code", courseCode);
                ProfCourse.Parameters.AddWithValue("@ID", profID);
                try
                {
                    con.Open();
                    ProfCourse.ExecuteNonQuery();
                    Console.WriteLine("Record Inserted Successfully");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return Page();
                }
                finally
                {
                    con.Close();
                }
                return RedirectToPage("ToItAddProfCourse");
            }
        }
    }
}
