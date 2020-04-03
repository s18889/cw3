using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
namespace WebApplication1.Services
{
    public class EnrolmentDBService : ControllerBase, IEnrolmentUp
    {
        public IActionResult UpEnrolment(enrolmentUp enrolment)
        {
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {


                //sprawdzenie czy jest taki enrolment

                com.Connection = con;
                com.CommandText = $"select * from Enrollment where Semester={enrolment.Semester} and IdStudy = (select IdStudy from Studies where Name = '{enrolment.Studies}')";

                con.Open();
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();

                    return NotFound();
                }

                //czyli jest więc powiększamy

                com.CommandText = $"EXEC enrolUp @semester ={enrolment.Semester}, @Name = {enrolment.Studies}";
                dr.Close();

                dr = com.ExecuteReader();
                if (dr.Read())
                {

                    Enrolment ret = new Enrolment()
                    {
                        IdEnrollment = int.Parse(dr["IdEnrollment"].ToString()),
                        Semester = int.Parse(dr["Semester"].ToString()),
                        IdStudy = int.Parse(dr["IdStudy"].ToString()),
                        StartDate = DateTime.Now

                    };
                    return Ok(ret);
                }
                else
                {
                    return NotFound("ups");
                }
                

            }
        }
    }
}
