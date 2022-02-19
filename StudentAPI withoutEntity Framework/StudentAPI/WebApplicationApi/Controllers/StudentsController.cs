using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationApi.Models;

namespace WebApplicationApi.Controllers
{
    public class StudentsController : ApiController
    {
        // GET: api/Students
        public IEnumerable<Student> Get()
        {

            List<Student> lstStudents = new List<Student>();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Students ";

            try
            {
                SqlDataReader dr = cmdInsert.ExecuteReader();
                while (dr.Read())
                {
                    
                    Student obj = new Student();
                    obj.StudId = (int)dr["StudId"];
                    obj.StudName = dr["StudName"].ToString();
                    obj.StudGender = dr["StudGender"].ToString();

                    lstStudents.Add(obj);

                }
                dr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            cn.Close();

            return lstStudents;
            // return new string[] { "value1", "value2" };
        }

        // GET: api/Students/5
        public Student Get(int id)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdGet = new SqlCommand();
            cmdGet.CommandType = System.Data.CommandType.Text;
            cmdGet.CommandText = "select * from Students where StudId = @id";
            cmdGet.Parameters.AddWithValue("@id",id);

            Student stud = new Student();
            try
            {
                SqlDataReader dr = cmdGet.ExecuteReader();
                dr.Read();
               
                stud.StudId = (int)dr["StudId"];
                stud.StudName = dr["StudName"].ToString();
                stud.StudGender = dr["StudGender"].ToString();
                return stud;
                dr.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            cn.Close();

            
           
        }

        // POST: api/Students
        public void Post([FromBody]Student obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "insert into Students values(@StudName,@StudGender)";
            //cmdInsert.CommandText = "update Employees set Name=@Name, Basic=@Basic, DeptNo=@DeptNo  where EmpNo=@EmpNo";
            //cmdInsert.CommandText = "delete from Employees where EmpNo=@EmpNo";


            cmdInsert.Parameters.AddWithValue("@StudName", obj.StudName);
            cmdInsert.Parameters.AddWithValue("@StudGender", obj.StudGender);
            

            try
            {
                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("no errors");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            cn.Close();
        }

        // PUT: api/Students/5
        public void Put(int id, [FromBody]Student obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            //cmdInsert.CommandText = "insert into Students values(@StudName,@StudGender)";
            cmdInsert.CommandText = "update Students set StudName=@StudName, StudGender=@StudGender  where StudId=@StudId";
            //cmdInsert.CommandText = "delete from Employees where EmpNo=@EmpNo";

            cmdInsert.Parameters.AddWithValue("@StudId", id);
            cmdInsert.Parameters.AddWithValue("@StudName", obj.StudName);
            cmdInsert.Parameters.AddWithValue("@StudGender", obj.StudGender);


            try
            {
                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("no errors");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            cn.Close();
        }

        // DELETE: api/Students/5
        public void Delete(int id)
        {
             SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            //cmdInsert.CommandText = "insert into Students values(@StudName,@StudGender)";
           // cmdInsert.CommandText = "update Students set StudName=@StudName, StudGender=@StudGender  where StudId=@StudId";
            cmdInsert.CommandText = "delete from Students where StudId=@StudId";

            cmdInsert.Parameters.AddWithValue("@StudId", id);
       
            try
            {
                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("no errors");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            cn.Close();
        }
    }
}
