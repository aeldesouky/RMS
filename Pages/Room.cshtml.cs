using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;

public class Room : PageModel
{
    [BindProperty(SupportsGet = true)] public string Name { get; set; }

    [BindProperty(SupportsGet = true)] public string RoomFloor { get; set; }

    [BindProperty(SupportsGet = true)] public string Number { get; set; }

    [BindProperty(SupportsGet = true)] public string Zone { get; set; }

    [BindProperty(SupportsGet = true)] public string Type { get; set; }

    [BindProperty(SupportsGet = true)] public string Capacity { get; set; }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(Name))
        {
            string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT Name, RoomFloor, Number, Zone, Type, Capacity FROM Room WHERE Name = @Name";

            SqlCommand command = new SqlCommand(queryString, con);
            command.Parameters.AddWithValue("@Name", Name);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Name = reader.GetString(0);
                RoomFloor = reader.GetString(1);
                Number = reader.GetString(2);
                Zone = reader.GetString(3);
                Type = reader.GetString(4);
                Capacity = reader.GetInt32(5).ToString();

            }
        }
    }

    public IActionResult OnPostDelete(string Name)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            Console.WriteLine("empty");
        }

        string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);
        string queryString = "DELETE FROM Room WHERE Name = @Name";

        SqlCommand command = new SqlCommand(queryString, con);
        command.Parameters.AddWithValue("@Name", Name);

        con.Open();
        command.ExecuteNonQuery();
        return RedirectToPage("/Room");
    }
}