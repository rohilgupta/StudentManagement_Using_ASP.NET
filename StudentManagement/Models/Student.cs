using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
namespace StudentManagement.Models
{
    public class Student
    {
        [Key]
        public int StudentNo { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Branch")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Please Enter Section")]
        public int Section { get; set; }

        [Required(ErrorMessage = "Please Enter Valid EmailId")]
        public string EmailId { get; set; }

        public static void Insert(Student obj)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Tbl_Student values( @Name ,@Branch,@Section,@EmailId)";
                cmd.Parameters.AddWithValue("Name", obj.Name);
                cmd.Parameters.AddWithValue("Branch", obj.Branch);
                cmd.Parameters.AddWithValue("Section", obj.Section);
                cmd.Parameters.AddWithValue("EmailId", obj.EmailId);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
        }
        public static Student GetSingleStudent(int id)
        {
            Student s = null;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("StudentNo", id);
                cmd.CommandText = "select * from Tbl_Student where StudentNo = @StudentNo";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Student std = new Student();
                    std.StudentNo = reader.GetInt32("StudentNo");
                    std.Name = reader.GetString("Name");
                    std.Branch = reader.GetString("Branch");
                    std.Section = reader.GetInt32("Section");
                    std.EmailId = reader.GetString("EmailId");
                    s = std;
                    cmd.ExecuteScalar();

                }
                else
                {
                    Console.WriteLine("not found");
                }
                reader.Close();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
            return s;
        }
        public static List<Student> GetAllStudent()
        {
            List<Student> stdlist = new List<Student>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Tbl_Student ";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student std = new Student();
                    std.StudentNo = reader.GetInt32("StudentNo");
                    std.Name = reader.GetString("Name");
                    std.Branch = reader.GetString("Branch");
                    std.Section = reader.GetInt32("Section");
                    std.EmailId = reader.GetString("EmailId");
                    stdlist.Add(std);
                }
                reader.Close();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
            return stdlist;

        }
       
        public static void Update(int id,Student obj)
        {
          
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Tbl_Student set Name = @Name,Branch = @Branch,Section = @Section,EmailId = @EmailId where StudentNo = @StudentNo";
                cmd.Parameters.AddWithValue("@StudentNo",id );

                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Branch", obj.Branch);
                cmd.Parameters.AddWithValue("@Section", obj.Section);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { cn.Close(); }
        }
    }
}
