using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;

namespace RMS.Pages;

[BindProperties(SupportsGet = true)]
public class ToItAddCourse : PageModel
{
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    
    public int Credits { get; set; }
    public bool successMessage { get; set; }


    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        string RoomInsertQuery = "INSERT INTO Course (CourseCode, CourseName, Credits) VALUES (@CourseCode, @CourseName, @Credits)";
        

        using (SqlConnection con = new SqlConnection(conString))
        {
                SqlCommand RoomCommand = new SqlCommand(RoomInsertQuery, con);            
                RoomCommand.Parameters.AddWithValue("@CourseCode", CourseCode);
                RoomCommand.Parameters.AddWithValue("@CourseName", CourseName);
                RoomCommand.Parameters.AddWithValue("@Credits", Credits);
                try
                {
                    con.Open();
                    RoomCommand.ExecuteNonQuery();
                    Console.WriteLine("Record Inserted Successfully");
                    successMessage = true;

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
                return RedirectToPage("/ToItAddCourse");
        }
    }
}

