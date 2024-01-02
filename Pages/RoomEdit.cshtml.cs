using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;

public class RoomEdit : PageModel
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
    
    public void OnPost()
    {
        string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);
        Console.WriteLine("connected");
        string querystring = "UPDATE Room SET Name=@Name, RoomFloor=@RoomFloor, Number=@Number, Zone=@Zone, Type=@Type, Capacity=@Capacity WHERE Name = @Name";
        
        SqlCommand cmd1 = new SqlCommand(querystring, con);
        cmd1.Parameters.AddWithValue("@Name", Name);
        cmd1.Parameters.AddWithValue("@RoomFloor", RoomFloor);
        cmd1.Parameters.AddWithValue("@Number", Number);
        cmd1.Parameters.AddWithValue("@Zone", Zone);
        cmd1.Parameters.AddWithValue("@Type", Type);
        cmd1.Parameters.AddWithValue("@Capacity", Capacity);
        try
        {
            con.Open();
            cmd1.ExecuteNonQuery();
            Console.WriteLine("Record Updated Successfully");
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            con.Close();
        }
    }
}