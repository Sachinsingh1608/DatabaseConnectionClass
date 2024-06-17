using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectionClass
{
    public class Employee
    {
        public int emp_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime? birth_date { get; set; }
        public string sex { get; set; }
        public double? age { get; set; }
        public string address { get; set; }
        public double? salary { get; set; }
        public int dept_id { get; set; }
        public string Data_File { get; set; }
        public Employee() { }
        public void Add()
        {

        }
       public bool Save()
        {
            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training" +
                "Data Source = LAPTOP-LFHQRLA5\\SQLEXPRESS";
                string lsQuery = "";
            using(SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                if(emp_id == 0)
                {
                    lsQuery = "INSERT INTO EmployeeNew (first_name,last_name.birth_date,sex,age,address" +
                        ",salary,dept_id)";
                    lsQuery += "VALUES('" + first_name + "','" + last_name + "' ,'" + ((DateTime)birth_date).ToString("dd-MMM-yyyy") + "','";

                    lsQuery += sex + "'," + age.ToString() + ",'" + address + "'," + salary.ToString() + "," + dept_id.ToString() + ")";
            
                        
                }

                else
                {
                    lsQuery = "UPDATE EmployeeNew SET first_name = '"+first_name + "',last_name='"+last_name + "',birth_date='"+((DateTime)birth_date).ToString("dd-MMM-yyyy")+ "','";
                    lsQuery += "sex='" + sex + "',age=" + age.ToString() + ",address ='" + address + ",salary=" + salary.ToString() + ",dept_id='" + dept_id.ToString() + ")";
                    lsQuery += "WHERE  emp_id = " + emp_id.ToString();
                }


                SqlCommand cmd = new SqlCommand(lsQuery, lobjCon);
                    cmd.CommandType = System.Data.CommandType.Text;
                try
                {
                    lobjCon.Open();
                    cmd.ExecuteNonQuery();
                    lobjCon.Close();
                }
                catch (SqlException Ex) {
                    Console.WriteLine(Ex.Message);
                    return false;
                }
            }
            return true;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
