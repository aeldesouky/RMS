using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;

namespace RMS.Pages;

[BindProperties(SupportsGet = true)]

public class ToItCreateStudent : PageModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Major { get; set; }
    public int PhoneNo { get; set; }
    public int ID { get; set; }

    public string Password { get; set; }


    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        string studentInsertQuery =
            "INSERT INTO Student (ID, Name, Address, PhoneNo, Major, cGPA, GP) VALUES (@ID, @Name, @Address, @PhoneNo, @Major, 0, 0)";
        string loginInsertQuery = "INSERT INTO Login (ID, Password,Type) VALUES (@ID, @Password,'Student')";
        

        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                SqlCommand studentCommand = new SqlCommand(studentInsertQuery, con, transaction);
                studentCommand.Parameters.AddWithValue("@ID", ID);
                studentCommand.Parameters.AddWithValue("@Name", Name);
                studentCommand.Parameters.AddWithValue("@Address", Address);
                studentCommand.Parameters.AddWithValue("@PhoneNo", PhoneNo);
                studentCommand.Parameters.AddWithValue("@Major", Major);

                studentCommand.ExecuteNonQuery();

                SqlCommand loginCommand = new SqlCommand(loginInsertQuery, con, transaction);
                loginCommand.Parameters.AddWithValue("@ID", ID);
                loginCommand.Parameters.AddWithValue("@Password", Password);

                loginCommand.ExecuteNonQuery();

                transaction.Commit();
                return RedirectToPage("/ToItCreateStudent");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.ToString());
                return Page();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
