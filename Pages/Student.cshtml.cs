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
    public string PhoneNo { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string Major { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string cGPA { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string GP { get; set; }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(ID))
        {
            string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT ID, Name, Address, PhoneNo, Major, cGPA, GP FROM Student WHERE ID = @ID";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@ID", ID);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
                
            if (reader.Read())
            {
                Name = reader.GetString(1);
                Address = reader.GetString(2);
                PhoneNo = reader.GetString(3);
                Major = reader.GetString(4);
                cGPA = reader.GetDecimal(5).ToString();
                GP = reader.GetInt32(6).ToString();
                
            }
        }
    }
    
    public IActionResult OnPostDelete(string ID)
    {
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