using s18889a.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace s18889a.DAL
{
    public class DatabaseServise : IDataBaseService
    {
        public String test()
        {
            return "alamakota";
        }

        string IDataBaseService.test()
        {
            throw new NotImplementedException();
        }

        object IDataBaseService.z11(int id)
        {

            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {



                String data = "";
                com.Connection = con;
                com.CommandText = $"select Project.Name, Task.name, TaskType.name, Task.Deadline  from Project  inner join  Task on Project.IdProject = Task.IdProject join TaskType on Task.IdTaskType = TaskType.IdTaskType where Project.IdProject = {id} order by Task.Deadline desc;";
                con.Open();



                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    data += dr[0].ToString();
                    data += " " + dr[1].ToString();
                    data += " " + dr[2].ToString();
                    data += " " + dr[3].ToString();
                }
                else
                {
                    throw new Exception();
                }

                while(dr.Read())
                {
                    data += "\n";
                    data += dr[0].ToString();
                    data += " " + dr[1].ToString();
                    data += " " + dr[2].ToString();
                    data += " " + dr[3].ToString();
                }

                return data;
            }
        }

        string IDataBaseService.z2(addSTH putSth)
        {
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.Connection = con;
                var trans = con.BeginTransaction();
                com.CommandText = $"select * from TaskType where TaskType.IdTaskType ={putSth.TaskType.IdTaskType}";
                com.Transaction = trans;
                con.Open();

                // idStudies 

                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    com.CommandText = $"INSERT INTO TaskType VALUES({putSth.TaskType.IdTaskType} ,{putSth.TaskType.Name}) ";
                    var nq = com.ExecuteNonQuery();

                }

                com.CommandText = $"insert into Task values (select max(IdTask)+1 from Task,{putSth.Name},{putSth.Description},{putSth.Deadline},{null},{putSth.TaskType.IdTaskType},{putSth.IdAssignedTo},{putSth.IdCreator})"; //nie mam czasu

                trans.Commit();
                return "";
            }
        }
    }
}
