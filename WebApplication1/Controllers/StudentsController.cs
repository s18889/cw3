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



        //Zadanie 4.4  http://localhost:57597/api/students/1;drop%20table%20student;--
        //Zadanie 4.3 Zakomentowuje by zachować wersję sprzed zadania 4.5
        /*[HttpGet("{id}")]
        public IActionResult GetStudentWpis(String id)
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

                return Ok(dr[0].ToString()+","+dr[1] +","+ dr[2]);

            }

        }*/
        //Zadanie 4.5 
        [HttpGet("{id}")]
        public IActionResult GetStudentWpis(String id)
        {
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student where Student.IndexNumber=@id";
                com.Parameters.AddWithValue("id", id);
                con.Open();
                var dr = com.ExecuteReader();

                dr.Read();
                string idd = dr["IdEnrollment"].ToString();

                return Ok(dr[0].ToString() + "," + dr[1] + "," + dr[2]);

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