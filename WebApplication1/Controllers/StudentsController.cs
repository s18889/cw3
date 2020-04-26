using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DAL;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        public IConfiguration Configuration { get; set;}



        //[HttpGet]
        //public String GetStudent()
        //{
        //    return "Ania, Ela, Adam";
        //}

        private readonly IDBService _dbService;

        public StudentsController(IConfiguration con, IDBService dbService)
        {
            _dbService = dbService;
            Configuration = con;
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
        //[Route("api/students")]
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
        //7.1
        [HttpPost]
        public IActionResult login(LoginRequestDto request)
        {
            String[] cr = { request.Login, request.Haslo };
            if (!_dbService.checkPas(cr))
            {
                return BadRequest("blendne haslo lub login");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,request.Haslo),
                new Claim(ClaimTypes.Name, request.Login),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            var tokenref = Guid.NewGuid();
            _dbService.putKay(tokenref.ToString());
            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = tokenref
            });
        }

        [HttpPost("refresh")]
        public IActionResult refresh(LoginRequestDtorefresh request)
        {

            if (!_dbService.testKay(request.Kay))
            {
                return BadRequest("niepoprawny klucz");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,request.Haslo),
                new Claim(ClaimTypes.Name, request.Login),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            var tokenref = Guid.NewGuid();
            _dbService.putKay(tokenref.ToString());
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = tokenref
            });

        }

        [HttpGet]
        [Authorize]
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

 


        /*
                [HttpPost]
                public IActionResult CreateStudent(Student s)
                {
                    s.IndexNumber = $"s{new Random().Next(1, 20000)}";

                    return Ok(s);

                }
        */
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("Ąktualizacja dokończona");
        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }

        
    }


}