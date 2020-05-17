using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class MockDbService : IDBService
    {

        private static IEnumerable<Student0> _studetns;

        static MockDbService()
        {
            _studetns = new List<Student0>
            {
                new Student0 { FirstName = "Adam", LastName = "Kowalski", IdStudent = "1" , IndexNumber = "s1"},
                new Student0 { FirstName = "Zuzia", LastName = "Magda", IdStudent = "2" , IndexNumber = "s2"},
                new Student0 { FirstName = "Janusz", LastName = "Pawlak", IdStudent = "3" , IndexNumber = "s3"},
                new Student0 { FirstName = "Michał", LastName = "Zieliński", IdStudent = "4" , IndexNumber = "s18889"},
            };

        }

        public bool checkPas(string[] credentials)
        {

            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {


                //pobranie id studiów
                String idStudies;
                com.Connection = con;
                com.CommandText = $"select IndexNumber from Student where IndexNumber = '{credentials[0]}' and Pasword = '{credentials[1]}'";
                con.Open();


                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }

                return false;
            }

        }

        public void putKay(String kay)
        {
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {


         
                com.Connection = con;
                com.CommandText = $"insert into refresh values('{kay}')";
                con.Open();


                var dr = com.ExecuteNonQuery();



            }
        }
        public bool testKay(String kay)
        {
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {


 
                com.Connection = con;
                com.CommandText = $"select * from refresh where kay = '{kay}'";
                con.Open();


                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    com.CommandText = $"delete from refresh where kay = '{kay}'";
                    com.ExecuteNonQuery();

                    return true;
                }

                return false;
            }

        }
            public IEnumerable<Student0> GetStudents()
        {
            return _studetns;
        }
    }
}
