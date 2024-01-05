using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RMS.Pages;

public class Petition : PageModel
{
    public DataTable PetitionTable { get; private set; }
    
    [BindProperty(SupportsGet = true)]
    public string PetNum { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string StudentID { get; set; }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(PetNum))
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT PetNum, StudentID, CourseCode, RegistrarID, Stat FROM Petition WHERE PetNum = @PetNum";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@PetNum", PetNum);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            PetitionTable = new DataTable();
            PetitionTable.Load(reader);
            
        }
        else
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT PetNum, StudentID, CourseCode, RegistrarID, Stat FROM Petition";

            SqlCommand command = new SqlCommand(queryString, con);

            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            PetitionTable = new DataTable();
            PetitionTable.Load(reader);
            
        }
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("/Petition", new { PetNum = PetNum });
    }
}