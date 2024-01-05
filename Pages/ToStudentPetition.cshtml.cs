using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
namespace RMS.Pages;

[BindProperties(SupportsGet = true)]

public class ToStudentPetition : PageModel
{
    public string courseCode { set; get; }
    public List<string> CourseCodes { get; set; }
    public int GivenID { get;  set; }
    public string StoredUserId { get; private set; }

    public int petNumber { get; set; }
    public DataTable PetitionTable { get; private set; }


    public void OnGet()
    {
        CourseCodes = new List<string>();
        StoredUserId = HttpContext.Session.GetString("UserID");
        GivenID = int.Parse(StoredUserId);

        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        string GetCourseCode = "SELECT CourseCode FROM Course";
        string queryString = "SELECT PetNum, StudentID, CourseCode, RegistrarID, Stat FROM Petition WHERE StudentID = @ID";

        using (SqlConnection con = new SqlConnection(conString))
        {
            SqlCommand GetCourseCodeCommand = new SqlCommand(GetCourseCode, con);
            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@ID", GivenID); 
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
                SqlDataReader reader = command.ExecuteReader();
                PetitionTable = new DataTable();
                PetitionTable.Load(reader);
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
        StoredUserId = HttpContext.Session.GetString("UserID");
        GivenID = int.Parse(StoredUserId);
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        string maxserial = "SELECT MAX(PetNum) AS MaxPetNum FROM Petition";
        string PetitionQuery = "INSERT INTO Petition (PetNum, StudentID, CourseCode, RegistrarID, Stat) VALUES (@PetNumber, @StoredUserId, @Code, null, 'Pending')";

        using (SqlConnection con = new SqlConnection(conString))
        {
            SqlCommand maxserialCommand = new SqlCommand(maxserial, con);
            SqlCommand Petition = new SqlCommand(PetitionQuery, con);
            Petition.Parameters.AddWithValue("@StoredUserId", GivenID);
            Petition.Parameters.AddWithValue("@Code", courseCode);

            try
            {
                con.Open();
                SqlDataReader maxserialReader = maxserialCommand.ExecuteReader();
                while (maxserialReader.Read())
                {
                    petNumber = maxserialReader.IsDBNull(maxserialReader.GetOrdinal("MaxPetNum")) ? 1 : maxserialReader.GetInt32(maxserialReader.GetOrdinal("MaxPetNum")) + 1;
                }
                con.Close();

                Petition.Parameters.AddWithValue("@PetNumber", petNumber);

                con.Open();
                Petition.ExecuteNonQuery();
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

            return RedirectToPage("/ToStudentPetition");
        }
    }

}