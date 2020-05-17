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
        s18889Context _linqDb;

        public StudentsController(IConfiguration con, IDBService dbService,s18889Context linqDb)
        {
            _dbService = dbService;
            Configuration = con;
            _linqDb = linqDb;
            //_linqDb = new s18889Context();
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


        [HttpPost("add")]
        public IActionResult addStudentTo(Student st)
        {


            _linqDb.Student.Add(st);
            _linqDb.SaveChanges();
            return Ok(_linqDb.Student.ToList());

        }

        [HttpPost("enrollments")]
        public IActionResult EnrollmentsController(AddStudent st)
        {
    

            if (st.GetType().GetProperties().All(p => p.GetValue(st) != null)) return BadRequest("brak pola");    //sprawdzenie czy którzyś element nie jest null

            try //sprawdzanie formatu daty
            {
                DateTime.Parse(st.BirthDate);
            }
            catch (FormatException e)
            {
                return BadRequest("zły format daty");
            }

            if (!_linqDb.Studies.Any(e => e.Name.Equals(st.Studies))) return BadRequest("nieprawidlowa nazwa kierunku");

            int idStu = _linqDb.Studies.Where(e => e.Name.Equals(st.Studies)).Select(e => e.IdStudy).First();//wybieram id studiów
            int idenrol;
            if (!_linqDb.Enrollment.Any(e => e.IdStudy.Equals(idStu))) //jeśli nie ma takiego enrolment dodajemy go
            {
                idenrol = _linqDb.Enrollment.Max(e => e.IdEnrollment) + 1;
                _linqDb.Enrollment.Add(new Enrollment
                {
                    IdEnrollment = idenrol,
                    IdStudy = idStu,
                    Semester = 1,
                    StartDate = DateTime.Now

                }) ;
               
            }
            else //jak jest to go znajdujemy
            {
                idenrol = _linqDb.Enrollment.Where(e => e.IdStudy.Equals(idStu)).Select(e => e.IdEnrollment).First();
            }

            //sprawdzam unikalność indexu

            if (_linqDb.Student.Any(e => e.IndexNumber.Equals(st.IndexNumber))) return BadRequest("podany index nie jest unikalny");

            //dodaję studenta

            

            _linqDb.Student.Add(new Student
            {
                BirthDate = DateTime.Parse(st.BirthDate),
                FirstName = st.FirstName,
                LastName = st.LastName,
                IdEnrollment = idenrol,
                Pasword = "12345", //musiałem dodać z ręki bo w pierwotnym zadaniu nie było chasła
                IndexNumber = st.IndexNumber

            });
            _linqDb.SaveChanges();
            return Ok(_linqDb.Student.ToList());

        }

        [HttpPost("enrollments/Promotions")]
        public IActionResult EnrollmentPromotion(enrolmentUp eu)
        {
            if (!_linqDb.Studies.Any(e => e.Name.Equals(eu.Studies))) return BadRequest("nieprawidlowa nazwa kierunku");
            //pobranie id studies
            int idStu = _linqDb.Studies.Where(e => e.IdStudy.Equals(eu.Studies)).Select(e => e.IdStudy).First();
            //sprawdzanie czy jest poprawny enrolment
            if (!_linqDb.Enrollment.Any(e => (e.IdStudy.Equals(idStu) && e.Semester.Equals(eu.Semester)))) return BadRequest("brak enrolment");
            int idEnrolL = _linqDb.Enrollment.Where(e => e.IdStudy.Equals(idStu) && e.Semester.Equals(eu.Semester)).Select(e => e.IdEnrollment).First(); //niższy semestr

            //czy nie istnieje rok starsze enrolment
            int idEnrolH;
            if (!_linqDb.Enrollment.Any(e => (e.IdStudy.Equals(idStu) && e.Semester.Equals(eu.Semester + 1))))
            {//jeśli nie istnieje tworzymy

                //nowe id enrolment
                idEnrolH = _linqDb.Enrollment.Max(e => e.IdEnrollment) + 1;
                //tworzę i dodaję
                _linqDb.Enrollment.Add(new Enrollment
                {
                    Semester= eu.Semester + 1,
                    StartDate=DateTime.Now,
                    IdStudy=idStu,
                    IdStudyNavigation = null, //???? nie wiem
                    IdEnrollment = idEnrolH
                });

            }
            else
            {
                idEnrolH = _linqDb.Enrollment.Where(e => (e.IdStudy.Equals(idStu) && e.Semester.Equals(eu.Semester + 1))).Select(e => e.IdEnrollment).First();
            }
            //wyszukuje studentów
            var list = _linqDb.Student.Where(e => e.IdEnrollment.Equals(idEnrolL));
            //aktualizuję ich enrolment
            foreach(Student s in list)
            {
                s.IdEnrollment = idEnrolH;
            }

            _linqDb.SaveChanges();

            return Ok();
        }

        [HttpDelete("remove")]
        public IActionResult removeStudentfrom(Student st)
        {

            var s = _linqDb.Student.Where(e=> e.IndexNumber.Equals(st.IndexNumber)).First();
            _linqDb.Student.Remove(s);
            _linqDb.SaveChanges();
            return Ok(_linqDb.Student.ToList());

        }

        [HttpGet("list")]
        public IActionResult GetStudentlist(String id)
        {
            return Ok( _linqDb.Student.ToList());

        }
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