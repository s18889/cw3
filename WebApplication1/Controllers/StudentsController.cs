using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //[HttpGet]
        //public String GetStudent()
        //{
        //    return "Ania, Ela, Adam";
        //}

        private readonly IDBService _dbService;

        public StudentsController(IDBService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("/z3/{id}")]
        public IActionResult GetStudentId(int id)
        {
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = $"select * from Student where Student.IndexNumber={id}";
                
                con.Open();
                var dr = com.ExecuteReader();

                dr.Read();
                string idd = dr["IdEnrollment"].ToString();
                dr.Close();
                com.CommandText = $"select * from Enrollment where Enrollment.IdEnrollment={idd}";
                dr = com.ExecuteReader();
                var st = new List<string>();
                //while (dr.Read())
                //{
                //    if (dr["LastName"] == DBNull.Value)
                //    {

                //    }
                //    Console.WriteLine(dr["LastName"].ToString());
                //    st.Add(dr["LastName"].ToString());

                //}
                dr.Read();
                return Ok(dr["IdEnrollment"].ToString());
                dr.Close();
            }

        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {

            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student";

                con.Open();
                var dr = com.ExecuteReader();
                var st = new List<string>();
                while(dr.Read())
                {
                    if(dr["LastName"] == DBNull.Value )
                    {

                    }
                    Console.WriteLine(dr["LastName"].ToString());
                    st.Add(dr["LastName"].ToString());
                    
                }
                    return Ok(st);
            }

                
            
            

        }

        [HttpPost]
        public IActionResult CreateStudent(Student s)
        {
            s.IndexNumber = $"s{new Random().Next(1, 20000)}";

            return Ok(s);

        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("Ąktualizacja dokończona");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }
    }
}