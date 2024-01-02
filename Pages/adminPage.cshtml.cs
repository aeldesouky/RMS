using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RMS.Pages;

public class adminPage : PageModel{

public DataTable Student { get; private set; }
    
[BindProperty(SupportsGet = true)]
public string ID { get; set; }

public void OnGet()
{
    string conString = @"Data Source=Abdullah; Initial Catalog=RMS_DB;Integrated Security=True";
    SqlConnection con = new SqlConnection(conString);
    string queryString = "SELECT ID, Name, Address, PhoneNo, Major, cGPA, GP FROM Student";

    SqlCommand command = new SqlCommand(queryString, con);

    con.Open();
    SqlDataReader reader = command.ExecuteReader();
    Student = new DataTable();
    Student.Load(reader);
}
public IActionResult OnPost()
{
    return RedirectToPage("/Student", new { ID = ID });
}
}