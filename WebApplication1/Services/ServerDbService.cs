using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ServerDbService : ControllerBase, IStudentsDbService
    {

        
        
        public IActionResult StrudentEnrolment(AddStudent student)
        {

            if(student.FirstName==null || student.LastName == null || student.Studies==null || student.IndexNumber == null || student.BirthDate==null)
            {
                return BadRequest("niepoprawne dane");
            }
            
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {


                //pobranie id studiów
                String idStudies;
                com.Connection = con;
                com.CommandText = "select IdStudy from Studies where name =@name";
                com.Parameters.AddWithValue("name", student.Studies);
                con.Open();
                var trans = con.BeginTransaction();
                // idStudies 
                com.Transaction = trans;
                var dr = com.ExecuteReader();
                if(dr.Read()){
                    idStudies = dr["IdStudy"].ToString();
                }
                else
                {
                    dr.Close();
                    trans.Rollback();
                    return BadRequest("Brak takich studiów");
                }
                
                
                //znalezienie maksymalnego enrolment
                string maxIdEnrollment = "";
                com.CommandText = "select  ISNULL(max(  IdEnrollment), 0)'maxid'  from Enrollment;";
                dr.Close();
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    maxIdEnrollment = dr["maxid"].ToString();
                }
                else
                {
                    trans.Rollback();
                    return BadRequest("brak enrolment");
                }


                //dodanie noweo enrolment
                int newIdEnrolment = int.Parse(maxIdEnrollment) + 1;
                Enrolment0 en = new Enrolment0()
                {
                    IdEnrollment = newIdEnrolment,
                    Semester = 1,
                    IdStudy = int.Parse(idStudies),
                    StartDate = DateTime.Now
                };

                
                com.CommandText = $"INSERT INTO Enrollment VALUES({ newIdEnrolment}, 1, {idStudies}, GETDATE());";
                //com.Parameters.AddWithValue("name", student.Studies);
                dr.Close();
                com.ExecuteNonQuery();


                //sprawdzenie unikalnosci id studenta
                com.CommandText = $"select * from Student where IndexNumber='{student.IndexNumber}'";
                dr.Close();
                dr = com.ExecuteReader();

                if(dr.Read())
                {
                    dr.Close();
                    trans.Rollback();

                    return BadRequest("Istnieje już student o takiej s");
                }

                
                
                //dodanie nowego studenta
                com.CommandText = "INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment)" +
                    "VALUES(@IndexNumber, @FirstName, @LastName,  @BirthDate, @IdEnrollment); ";



                com.Parameters.AddWithValue("IndexNumber", student.IndexNumber);
                com.Parameters.AddWithValue("FirstName", student.FirstName);
                com.Parameters.AddWithValue("LastName", student.LastName);
                com.Parameters.AddWithValue("BirthDate", DateTime.Parse( student.BirthDate));
                com.Parameters.AddWithValue("IdEnrollment", int.Parse(maxIdEnrollment) + 1);
                dr.Close();
                com.ExecuteNonQuery();


                trans.Commit();

                return Ok(newIdEnrolment);

                //dodanie nowego student


            }



        }

        public bool StudentExists(string index)
        {

            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {


                //pobranie id studiów
                String idStudies;
                com.Connection = con;
                com.CommandText = $"select IndexNumber from Student where IndexNumber = '{index}'";
                con.Open();


                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }

                return false;
            }
        }
    }
}
