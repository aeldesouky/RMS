using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json; 

using System.Data.SqlClient;

namespace RMS.Pages
{
    public class ToProfTaChangeGrade : PageModel
    {
        public List<GradeReportItem> GradeReportData { get; set; }

        public void OnGet()
        {
            string serializedCourseCodes = HttpContext.Session.GetString("CourseCodes");

            if (!string.IsNullOrEmpty(serializedCourseCodes))
            {
                List<string> courseCodes = JsonConvert.DeserializeObject<List<string>>(serializedCourseCodes);

                string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    GradeReportData = new List<GradeReportItem>();

                    foreach (var code in courseCodes)
                    {
                        string queryStr = "SELECT StudentID, CourseCode, Grade FROM GradeReport WHERE CourseCode = @ID";
                        SqlCommand com = new SqlCommand(queryStr, con);
                        com.Parameters.AddWithValue("@ID", code);

                        SqlDataReader read = com.ExecuteReader();

                        while (read.Read())
                        {
                            GradeReportData.Add(new GradeReportItem
                            {
                                StudentID = read["StudentID"].ToString(),
                                CourseCode = read["CourseCode"].ToString(),
                                Grade = read["Grade"].ToString()
                            });
                        }

                        read.Close();
                    }

                    con.Close();
                }
            }
        }
        public IActionResult OnPost(List<string> grades)
        {
            if (grades != null && grades.Count > 0)
            {
                string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    for (int i = 0; i < GradeReportData.Count; i++)
                    {
                        string updateQuery = "UPDATE GradeReport SET Grade = @Grade WHERE StudentID = @StudentID AND CourseCode = @CourseCode";

                        SqlCommand com = new SqlCommand(updateQuery, con);
                        com.Parameters.AddWithValue("@Grade", grades[i]);
                        com.Parameters.AddWithValue("@StudentID", Request.Form["studentIds"][i]);
                        com.Parameters.AddWithValue("@CourseCode", Request.Form["courseCodes"][i]);

                        com.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }

            return RedirectToPage("/ToProfTaChangeGrade"); 
        }
    }

    public class GradeReportItem
    {
        public string StudentID { get; set; }
        public string CourseCode { get; set; }
        public string Grade { get; set; }
    }
}
