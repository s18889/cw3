using System;
using System.Collections.Generic;
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

        [HttpGet("{id}")]
        public IActionResult GetStudentId(int id)
        {
            if(id == 0)
            {
                return Ok("Kowalski");
            }else if(id == 1)
            {
                return Ok("Nowak");
            }else
            {
                return NotFound("nie znaleziono studenta");
            }

        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {

            return Ok(_dbService.GetStudents());

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