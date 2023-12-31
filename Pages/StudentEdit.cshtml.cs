﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace RMS.Pages;

public class Edit : PageModel
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
    
    [BindProperty(SupportsGet = true)]
    public string GP { get; set; }
    
    public void OnGet()
    {
        if (!string.IsNullOrEmpty(ID))
        {
            string conString = @"Data Source = Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            string queryString = "SELECT Name, Address, Major, cGPA, PhoneNo, GP FROM Student WHERE ID = @ID";

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
                GP = reader.GetInt32(5).ToString();
            }
        }
    }
    
    public void OnPost()
    {
        string conString = @"Data Source=Abdullah;Initial Catalog=RMS_DB;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);
        Console.WriteLine("connected");
        string querystring = "UPDATE Student SET ID=@ID, Name=@Name, Address = @Address, Major = @Major, cGPA = @cGPA, PhoneNo = @PhoneNo, GP = @GP WHERE ID = @ID";
        
        SqlCommand cmd1 = new SqlCommand(querystring, con);
        cmd1.Parameters.AddWithValue("@ID", ID);
        cmd1.Parameters.AddWithValue("@Name", Name);
        cmd1.Parameters.AddWithValue("@Address", Address);
        cmd1.Parameters.AddWithValue("@Major", Major);
        cmd1.Parameters.AddWithValue("@cGPA", cGPA);
        cmd1.Parameters.AddWithValue("@PhoneNo", PhoneNo);
        cmd1.Parameters.AddWithValue("@GP", GP);
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