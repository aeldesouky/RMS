using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;

public class TaRa : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ID { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }

    
    public void OnGet()
    {
        if (!string.IsNullOrEmpty(ID))
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT Name FROM TaRa WHERE ID = @ID";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@ID", ID);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
                
            if (reader.Read())
            {
                Name = reader.GetString(0);
            }
        }
    }
    
    public IActionResult OnPostDelete(string ID)
    {
        if (!string.IsNullOrEmpty(ID))
        {
            string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "DELETE FROM TaRa WHERE ID = @ID";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@ID", ID);

            con.Open();
            command.ExecuteNonQuery();
        }
        Console.WriteLine("empty ID");
        return RedirectToPage("/Index");
    }
}