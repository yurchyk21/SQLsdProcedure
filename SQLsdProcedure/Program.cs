using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLsdProcedure
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }

    class Program
    {
        static DataTable GetEmployeesData (List<Employee> list)
        {
            DataTable st = new DataTable();
            st.Columns.Add("Id");
            st.Columns.Add("Name");
            st.Columns.Add("Gender");
            foreach (var emp in list)
                st.Rows.Add(emp.Id, emp.Name, emp.Gender);
            return st;
        }
        static void Main(string[] args)
        {
            List<Employee> list = new List<Employee>()
            {
                new Employee
                {
                    Id=10,
                    Name = "Stepan",
                    Gender = "male"
                },
                new Employee
                {
                    Id=11,
                    Name = "Степан",
                    Gender = "male"
                }

            };
            string _con = "Data Source = procedure.database.windows.net; Initial Catalog = Procedures; User ID = fanya21; Password = Bike3721";
            using (SqlConnection con = new SqlConnection(_con))
            {
                SqlCommand cmd = new SqlCommand("spInsertEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramTVP = new SqlParameter()
                {
                    ParameterName = "@InputEmployees",
                    Value = GetEmployeesData(list)
                };
                cmd.Parameters.Add(paramTVP);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
