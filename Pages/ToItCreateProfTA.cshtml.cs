using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
namespace RMS.Pages;
[BindProperties(SupportsGet = true)]

public class ToItCreateProfTA : PageModel
{
    public string Name { get; set; }
    public int ID { get; set; }

    public string Password { get; set; }


    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        string profInsertQuery ="INSERT INTO Faculty (ID, Name) VALUES ( @ID,@Name)";
        string loginInsertQuery = "INSERT INTO Login (ID, Password,Type) VALUES (@ID, @Password,'Prof')";

        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                SqlCommand studentCommand = new SqlCommand(profInsertQuery, con, transaction);
                studentCommand.Parameters.AddWithValue("@ID", ID);
                studentCommand.Parameters.AddWithValue("@Name", Name);


                studentCommand.ExecuteNonQuery();

                SqlCommand loginCommand = new SqlCommand(loginInsertQuery, con, transaction);
                loginCommand.Parameters.AddWithValue("@ID", ID);
                loginCommand.Parameters.AddWithValue("@Password", Password);

                loginCommand.ExecuteNonQuery();

                transaction.Commit();
                return RedirectToPage("/ToItCreateProfTA");
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
