using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
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

        public void UpdateEmplpoyeeDetail(ref List<Employee> lobjEmpList)
        {
            foreach (Employee lobjTempemp in lobjEmpList)
            {
                Console.WriteLine("Old First Name :- {0}", first_name);
                Console.WriteLine("Enter A new First Name");
                string lsFirst_Name = Console.ReadLine();
                if (lsFirst_Name.Length != 0)
                {
                    lobjTempemp.first_name = lsFirst_Name;
                  
                }
                Console.WriteLine(lobjEmpList[0].first_name);
                Console.ReadKey();
            }
            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                lsQuery = "UPDATE EmployeeNew SET  first_name = '" + lobjEmpList[0].first_name;
                //lsQuery += "sex='" + lobjEmpList[3].sex + "',age=" + lobjEmpList[4].age.ToString() + ",address ='" + lobjEmpList[5].address + ",salary=" + lobjEmpList[6].salary.ToString() + ",dept_id='" + lobjEmpList[7].dept_id.ToString() + ")";
                lsQuery += " WHERE  emp_id = " + emp_id.ToString();
                SqlCommand cmd = new SqlCommand(lsQuery, lobjCon);
                cmd.CommandType = System.Data.CommandType.Text;
                try
                {
                    lobjCon.Open();
                    cmd.ExecuteNonQuery();
                    lobjCon.Close();
                }
                catch (SqlException Ex)
                {
                    Console.WriteLine(Ex.Message);

                }
            }

            }
        public Employee() { }
        public Employee(Employee lobj) {

            emp_id = lobj.emp_id;
            first_name = lobj.first_name;
            last_name = lobj.last_name;
            age = lobj.age;
            sex = lobj.sex;
            age = lobj.age;
            address = lobj.address;
            salary = lobj.salary;
            dept_id = lobj.dept_id;
        }
        public void Add()
        {

        }
        public void show()
        {
            Console.WriteLine($"name {first_name} {last_name}|Date of Birth: {birth_date} | Sex: {sex} | address: {address} |" +
                $" salary: {salary} |  dept_id: {dept_id}");
           
        }
        public List<Employee> List()
        {
            List<Employee> lobjEmpList = new List<Employee>();
         
            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
                string lsQuery = "SELECT emp_id,first_name,last_name,birth_date,sex,age,address,salary,dept_id FROM EmployeeNew";
                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using(SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                            emp_id = (int)lobjSDR[0];


                            if (DBNull.Value.Equals(lobjSDR[1]))
                                {
                                first_name = "";
                                }
                            else
                            {
                                first_name = lobjSDR[1].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[2]))
                            {
                                last_name = "";
                            }
                            else
                            {
                               last_name = lobjSDR[2].ToString();
                            }



                            if (DBNull.Value.Equals(lobjSDR[3]))
                            {
                                birth_date = null;
                            }
                            else
                            {
                                birth_date = (DateTime)lobjSDR[3];
                            }


                            if (DBNull.Value.Equals(lobjSDR[4]))
                            {
                                sex = "";
                            }
                            else
                            {
                                sex = lobjSDR[4].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[5]))
                            {
                                age = null;
                            }
                            else
                            {
                                age = (double)lobjSDR[5];
                            }

                            if (DBNull.Value.Equals(lobjSDR[6]))
                            {
                                address = "";
                            }
                            else
                            {
                                address = lobjSDR[6].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[7])){
                                salary = null;
                            }
                            else
                            {
                                salary = (double)lobjSDR[7];
                            }

                            dept_id = (int)lobjSDR[8];


                            lobjEmpList.Add(new Employee(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjEmpList;
        }

        public List<Employee> find(string inEmpId)
        {

            List<Employee> lobjEmpList = new List<Employee>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
                string lsQuery = "SELECT emp_id,first_name,last_name,birth_date,sex,age,address,salary,dept_id FROM EmployeeNew ";
                lsQuery +="WHERE emp_id =" + inEmpId;
                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                            emp_id = (int)lobjSDR[0];


                            if (DBNull.Value.Equals(lobjSDR[1]))
                            {
                                first_name = "";
                            }
                            else
                            {
                                first_name = lobjSDR[1].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[2]))
                            {
                                last_name = "";
                            }
                            else
                            {
                                last_name = lobjSDR[2].ToString();
                            }



                            if (DBNull.Value.Equals(lobjSDR[3]))
                            {
                                birth_date = null;
                            }
                            else
                            {
                                birth_date = (DateTime)lobjSDR[3];
                            }


                            if (DBNull.Value.Equals(lobjSDR[4]))
                            {
                                sex = "";
                            }
                            else
                            {
                                sex = lobjSDR[4].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[5]))
                            {
                                age = null;
                            }
                            else
                            {
                                age = (double)lobjSDR[5];
                            }

                            if (DBNull.Value.Equals(lobjSDR[6]))
                            {
                                address = "";
                            }
                            else
                            {
                                address = lobjSDR[6].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[7]))
                            {
                                salary = null;
                            }
                            else
                            {
                                salary = (double)lobjSDR[7];
                            }

                            dept_id = (int)lobjSDR[8];


                            lobjEmpList.Add(new Employee(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjEmpList;
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
        public bool checkValidName(string inname)
        {
            for(int lncnt  = 0; lncnt < inname.Length; lncnt++)
            {
                if ((inname[lncnt] >= 'a' && inname[lncnt] <= 'z') || (inname[lncnt] >= 'A' && inname[lncnt] <= 'Z'))
                    continue;
                else
                {
                    Console.WriteLine("---------Not Valid Name---------");
                    return false;
                }

            }
            return true;
        }
        public void ReadInput()
        {
            do {
                Console.WriteLine("First Name");
                first_name = Console.ReadLine();
            }while(!checkValidName(first_name));


            do {
                Console.WriteLine("Last Name");
                last_name = Console.ReadLine();
            }while(!checkValidName(last_name));



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
            List<Employee> lobjEmpList = new List<Employee>();

            bool lbExit = false;
            while(!lbExit)
            {
                Console.WriteLine("Enter Exit 0");
                Console.WriteLine("Enter 1 Add Employee ");
                Console.WriteLine("Enter 2 list Employee ");
                Console.WriteLine("Enter 3 Find Employee ");
                Console.WriteLine("Enter 4 Update Employee");
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
                     case "2":
                       
                        lobjEmpList = lobjEmp.List();
                        foreach( Employee lobjTempemp in lobjEmpList)
                        {
                            lobjTempemp.show();
                        }
                       break;

                    case "3":
                        Console.WriteLine("Enter Employee ID");
                        string lsEmpId = Console.ReadLine();
                        lobjEmpList = lobjEmp.find(lsEmpId);
                        if (lobjEmpList.Count == 0)
                            Console.WriteLine("Wrong Employee Id");
                        else
                        {
                            foreach (Employee lobjTempemp in lobjEmpList)
                            {
                                lobjTempemp.show();
                            }
                        }
                        break;
                    case "4":
                        Console.WriteLine("Enter Employee ID");
                        string lsEmpIdUp = Console.ReadLine();
                        lobjEmpList = lobjEmp.find(lsEmpIdUp);
                        if (lobjEmpList.Count == 0)
                            Console.WriteLine("Wrong Employee Id");

                        else
                        {
                            lobjEmp.UpdateEmplpoyeeDetail(ref lobjEmpList);
                        }
                        break;


                }
            }
        }
    }
}
