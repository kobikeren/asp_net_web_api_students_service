using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsServiceDAL
{
    public class StudentsServiceDAManager
    {
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["studentsCS"].ConnectionString);

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            SqlDataAdapter adp = new SqlDataAdapter("spGetAllStudents", cn);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            adp.Fill(dt);

            foreach (DataRow i in dt.Rows)
            {
                Student student = new Student();

                student.Id = (int)i["Id"];
                student.FirstName = i["FirstName"].ToString();
                student.LastName = i["LastName"].ToString();
                student.Gender = i["Gender"].ToString();
                student.City = i["City"].ToString();
                student.BirthYear = (int)i["BirthYear"];

                students.Add(student);
            }

            return students;
        }

        public Student GetStudentById(int id)
        {
            Student student = new Student();

            SqlDataAdapter adp = new SqlDataAdapter("spGetStudentById", cn);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterId = new SqlParameter()
            {
                ParameterName = "@Id",
                Value = id
            };
            adp.SelectCommand.Parameters.Add(parameterId);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            foreach (DataRow i in dt.Rows)
            {
                student.Id = (int)i["Id"];
                student.FirstName = i["FirstName"].ToString();
                student.LastName = i["LastName"].ToString();
                student.Gender = i["Gender"].ToString();
                student.City = i["City"].ToString();
                student.BirthYear = (int)i["BirthYear"];
            }

            return student;
        }

        public int AddStudent(Student student)
        {
            SqlCommand cmd = new SqlCommand("spAddStudent", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterFirstName = new SqlParameter()
            {
                ParameterName = "@FirstName",
                Value = student.FirstName
            };
            cmd.Parameters.Add(parameterFirstName);

            SqlParameter parameterLastName = new SqlParameter()
            {
                ParameterName = "@LastName",
                Value = student.LastName
            };
            cmd.Parameters.Add(parameterLastName);

            SqlParameter parameterGender = new SqlParameter()
            {
                ParameterName = "@Gender",
                Value = student.Gender
            };
            cmd.Parameters.Add(parameterGender);

            SqlParameter parameterCity = new SqlParameter()
            {
                ParameterName = "@City",
                Value = student.City
            };
            cmd.Parameters.Add(parameterCity);

            SqlParameter parameterBirthYear = new SqlParameter()
            {
                ParameterName = "@BirthYear",
                Value = student.BirthYear
            };
            cmd.Parameters.Add(parameterBirthYear);

            cn.Open();
            decimal decimalNewId = (decimal)cmd.ExecuteScalar();
            cn.Close();

            int intNewId = (int)decimalNewId;
            return intNewId;
        }

        public void UpdateStudent(Student student)
        {
            SqlCommand cmd = new SqlCommand("spUpdateStudent", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterId = new SqlParameter()
            {
                ParameterName = "@Id",
                Value = student.Id
            };
            cmd.Parameters.Add(parameterId);

            SqlParameter parameterFirstName = new SqlParameter()
            {
                ParameterName = "@FirstName",
                Value = student.FirstName
            };
            cmd.Parameters.Add(parameterFirstName);

            SqlParameter parameterLastName = new SqlParameter()
            {
                ParameterName = "@LastName",
                Value = student.LastName
            };
            cmd.Parameters.Add(parameterLastName);

            SqlParameter parameterGender = new SqlParameter()
            {
                ParameterName = "@Gender",
                Value = student.Gender
            };
            cmd.Parameters.Add(parameterGender);

            SqlParameter parameterCity = new SqlParameter()
            {
                ParameterName = "@City",
                Value = student.City
            };
            cmd.Parameters.Add(parameterCity);

            SqlParameter parameterBirthYear = new SqlParameter()
            {
                ParameterName = "@BirthYear",
                Value = student.BirthYear
            };
            cmd.Parameters.Add(parameterBirthYear);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public void DeleteStudent(int id)
        {
            SqlCommand cmd = new SqlCommand("spDeleteStudent", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterId = new SqlParameter()
            {
                ParameterName = "@Id",
                Value = id
            };
            cmd.Parameters.Add(parameterId);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
