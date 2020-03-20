using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class MockDbService : IDBService
    {

        private static IEnumerable<Student> _studetns;

        static MockDbService()
        {
            _studetns = new List<Student>
            {
                new Student { FirstName = "Adam", LastName = "Kowalski", IdStudent = 1 , IndexNumber = "s1"},
                new Student { FirstName = "Zuzia", LastName = "Magda", IdStudent = 2 , IndexNumber = "s2"},
                new Student { FirstName = "Janusz", LastName = "Pawlak", IdStudent = 3 , IndexNumber = "s3"},
                new Student { FirstName = "Michał", LastName = "Zieliński", IdStudent = 4 , IndexNumber = "s18889"},
            };

        }

        public IEnumerable<Student> GetStudents()
        {
            return _studetns;
        }
    }
}
