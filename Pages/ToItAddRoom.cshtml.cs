using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;
[BindProperties(SupportsGet = true)]

public class ToItAddRoom : PageModel
{
    public string Name { get; set; }
    public string RoomFloor { get; set; }
    public string Zone { get; set; }
    public string Number { get; set; }
    public int Capacity { get; set; }

    public string Type { get; set; }


    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        string RoomInsertQuery ="INSERT INTO Room (Name,RoomFloor,Number,Zone,Type,Capacity) VALUES (@Name,@RoomFloor,@Number,@Zone,@Type,@Capacity)";

        using (SqlConnection con = new SqlConnection(conString))
        {
                SqlCommand Command = new SqlCommand(RoomInsertQuery, con);
                Command.Parameters.AddWithValue("@Name", Name);
                Command.Parameters.AddWithValue("@RoomFloor", RoomFloor.PadRight(1));
                Command.Parameters.AddWithValue("@Number", Number);
                Command.Parameters.AddWithValue("@Zone", Zone.PadRight(1));
                Command.Parameters.AddWithValue("@Type", Type);
                Command.Parameters.AddWithValue("@Capacity", Capacity);
                try
                {
                    con.Open();
                    Command.ExecuteNonQuery();
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
                return RedirectToPage("/ToItAddRoom");
        }
    }
}
