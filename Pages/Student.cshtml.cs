using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;

public class Student : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ID { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Address { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string Major { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string cGPA { get; set; }

    [BindProperty(SupportsGet = true)]
    public string PhoneNo { get; set; }
    
    public void OnGet()
    {
        if (!string.IsNullOrEmpty(ID))
        {
            string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT Name, Address, Major, cGPA, PhoneNo FROM Student WHERE ID = @ID";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@ID", ID);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
                
            if (reader.Read())
            {
                Name = reader.GetString(0);
                Address = reader.GetString(1);
                Major = reader.GetString(2);
                cGPA = reader.GetDecimal(3).ToString();
                PhoneNo = reader.GetString(4);
            }
        }
    }
    
    public IActionResult OnPostDelete(string ID)
    {
        Console.WriteLine("HERE" + ID);
        if (!string.IsNullOrEmpty(ID))
        {
            Console.WriteLine("empty");
        }
        string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);
        string queryString = "DELETE FROM Student WHERE ID = @ID";

        SqlCommand command = new SqlCommand(queryString, con);
        command.Parameters.AddWithValue("@ID", ID);

        con.Open();
        command.ExecuteNonQuery();
        return RedirectToPage("/Index");
    }
}