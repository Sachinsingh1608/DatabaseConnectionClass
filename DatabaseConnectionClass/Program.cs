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
            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using(SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                if(emp_id == 0)
                {
                    lsQuery = "INSERT INTO EmployeeNew (first_name,last_name,birth_date,sex,age,address" +
                        ",salary,dept_id)";
                    lsQuery += "VALUES('" + first_name + "','" + last_name + "' ,'" + ((DateTime)birth_date).ToString("dd-MMM-yyyy") + "','";

                    lsQuery += sex + "'," + age.ToString() + ",'" + address + "'," + salary.ToString() + "," + dept_id.ToString() + ")";
            
                        
                }

                else
                {
                    lsQuery = "UPDATE EmployeeNew SET    first_name = '" + first_name + "', first_name = '"+first_name + "',last_name='"+last_name + "',birth_date='"+((DateTime)birth_date).ToString("dd-MMM-yyyy")+ "','";
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

        public void ReadInput()
        {
            Console.WriteLine("First Name");
            first_name = Console.ReadLine();

            Console.WriteLine("Last Name");
            last_name = Console.ReadLine();

            Console.WriteLine("DOB");
            birth_date=DateTime.Parse(Console.ReadLine());

            Console.WriteLine("SEX");
            sex = Console.ReadLine();

            TimeSpan lobjTimeSpan = DateTime.Now.Subtract((DateTime)birth_date);


            age = lobjTimeSpan.TotalDays / 365.0;
            Console.WriteLine("Address");
            address = Console.ReadLine();

            Console.WriteLine("Salary");
            salary = float.Parse(Console.ReadLine());

            Console.WriteLine("Dept_id");
            dept_id = int.Parse(Console.ReadLine());    
        }
        public void DisplayEmployee()
        {
            Console.WriteLine($"name {first_name} {last_name}");
            Console.WriteLine($"Date of Birth: {birth_date}");
            Console.WriteLine($"Sex: {sex}");
            Console.WriteLine($"address: {address}");
            Console.WriteLine($"salary: {salary}");
            Console.WriteLine($"dept_id: {dept_id}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee lobjEmp = new Employee();
            bool lbExit = false;
            while(!lbExit)
            {
                Console.WriteLine("Enter Exit 0");
                Console.WriteLine("Enter Add Employee 1");
                Console.WriteLine("Enter Find Employee 2");
                Console.WriteLine("Enter Delete Employee 3");
              string lsChoice = Console.ReadLine();
                switch (lsChoice)
                {
                    case "0":
                        lbExit = true; 
                        break;
                    case "1":
                        lobjEmp.ReadInput();
                        lobjEmp.Save();
                        break;
                }
            }
        }
    }
}
