using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RMS.Pages;

public class RegistrarEdit : PageModel
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
            string queryString = "SELECT Name FROM Registrar WHERE ID = @ID";

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
    
    public void OnPost()
    {
        string conString = @"Data Source=DESKTOP-R0BEJSG;Initial Catalog=RMS_DB;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);
        Console.WriteLine("connected");
        string querystring = "UPDATE Registrar SET ID=@ID, Name=@Name WHERE ID = @ID";
        
        SqlCommand cmd1 = new SqlCommand(querystring, con);
        cmd1.Parameters.AddWithValue("@ID", ID);
        cmd1.Parameters.AddWithValue("@Name", Name);
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