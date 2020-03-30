using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ServerDbService : ControllerBase,IStudentsDbService 
    {
        String idStudies;
        public IActionResult StrudentEnrolment(Student student)
        {

            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Studies where name =@name";
                com.Parameters.AddWithValue("name", student.Studies);
                con.Open();
                idStudies = com.ExecuteReader()["IdStudy"].ToString();
                //if(dr.Read()){ ... }
                if (false) //sprawdzenie czy istnieje
                {
                    return NotFound();
                }

            }




            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                //com.CommandText = "select * from Student where Student.IndexNumber=@id";

                com.CommandText = "INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment)"+
                    "VALUES(@IndexNumber, @FirstName, @LastName,  @BirthDate, @IdEnrollment); ";



                com.Parameters.AddWithValue("IndexNumber", student.IndexNumber);
                com.Parameters.AddWithValue("FirstName", student.FirstName);
                com.Parameters.AddWithValue("LastName", student.LastName);
                com.Parameters.AddWithValue("BirthDate", student.LastName);
                com.Parameters.AddWithValue("IdEnrollment", idStudies);

                con.Open();
                var dr = com.ExecuteReader();

                dr.Read();
                string idd = dr["IdEnrollment"].ToString();

                return Ok(dr[0].ToString() + "," + dr[1] + "," + dr[2]);

            }


            return Ok("");
            throw new NotImplementedException();
        }
    }
}
