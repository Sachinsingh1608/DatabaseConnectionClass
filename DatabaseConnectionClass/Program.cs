using System;
using System.Collections.Generic;
using System.Data;
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

        
    
        public void DeleteEmployeeRecord(ref List<Employee> lobjEmpList)
        {
            

            foreach (Employee lobjTempemp in lobjEmpList)
            {
                emp_id = lobjTempemp.emp_id;
            }

                string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                string lsQuery = "DELETE FROM EmployeeNew WHERE emp_id = " + emp_id;
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
    


        public void UpdateEmplpoyeeDetail(ref List<Employee> lobjEmpList)
        {

            foreach (Employee lobjTempemp in lobjEmpList)
            {
                Console.WriteLine("Old First Name :- {0}", lobjTempemp.first_name);
                
                Console.WriteLine("Enter A new First Name");
                string lsFirst_Name = Console.ReadLine();
                if (lsFirst_Name.Length != 0)
                {
                    first_name = lsFirst_Name;

                }

                Console.WriteLine("Old Last name :- {0}", lobjTempemp.last_name);
                Console.WriteLine("Enter A new last Name");
                string lsLast_name = Console.ReadLine();
                if (lsLast_name.Length != 0)
                {
                    last_name = lsLast_name;

                }

                Console.WriteLine("Old Birth-Date :- {0}", lobjTempemp.birth_date);

                Console.WriteLine("Enter A new Birth Date ");
                string lstempdate = Console.ReadLine();
                
              
                if (lstempdate.Length != 0)
                {
                    DateTime lsBirth_date = DateTime.Parse(lstempdate);
                    birth_date = lsBirth_date;

                }


                Console.WriteLine("Old Gender Type :- {0}", lobjTempemp.sex);
                Console.WriteLine("Enter A new Gender");
                string lsSex = (Console.ReadLine());
                if (lsSex.Length != 0)
                {
                    sex = lsSex;

                }

                TimeSpan lobjTimeSpan = DateTime.Now.Subtract((DateTime)birth_date);


                age = lobjTimeSpan.TotalDays / 365.0;

                Console.WriteLine("Old Address  :- {0}", lobjTempemp.address);
                Console.WriteLine("Enter A new Addressa");
                string lsAddress = (Console.ReadLine());
                if (lsAddress.Length != 0)
                {
                   address= lsAddress;

                }


                Console.WriteLine("Old Salary  :- {0}", lobjTempemp.salary);
                Console.WriteLine("Enter A new salary");
                string  lfSalary = (Console.ReadLine());
                if (lfSalary.Length != 0)
                {
                   salary = float.Parse(lfSalary);

                }

                Console.WriteLine("Old Dept _ id  :- {0}", lobjTempemp.dept_id);
                Console.WriteLine("Enter A new DeptID");
                string lsDept_id = (Console.ReadLine());
                if (lsDept_id.Length != 0)
                {
                    dept_id = int.Parse(lsDept_id);

                }




                
                //Console.ReadKey();
            }

            //Console.WriteLine(lobjEmpList.first_name);
            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                lsQuery = "UPDATE EmployeeNew SET  first_name = '" + first_name + "',last_name='" + last_name + "',birth_date='" + ((DateTime)birth_date).ToString("dd-MMM-yyyy") + "',";
                lsQuery += "sex='" + sex + "',age=" + age.ToString() + ",address ='" + address.ToString() +"'" + ",salary=" + salary.ToString() + ",dept_id=" + dept_id.ToString() ;
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
            birth_date = lobj.birth_date;
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
            string[] lobjTempDate = birth_date.Value.ToString().Split(' ');
            string lstempBirth_Date = lobjTempDate[0];
            Console.WriteLine($"Emp_d {emp_id} |Name {first_name} {last_name}|Date of Birth: {lstempBirth_Date} | Sex: {sex} | address: {address} |" +
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

        public List<Employee> Find(string inEmpId)
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
               
                    lsQuery = "INSERT INTO EmployeeNew (first_name,last_name,birth_date,sex,age,address" +
                        ",salary,dept_id)";
                    lsQuery += "VALUES('" + first_name + "','" + last_name + "' ,'" + ((DateTime)birth_date).ToString("dd-MMM-yyyy") + "','";

                    lsQuery += sex + "'," + age.ToString() + ",'" + address + "'," + salary.ToString() + "," + dept_id.ToString() + ")";
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

        public bool CheckValidName(string inname)
        {
            for(int lncnt  = 0; lncnt < inname.Length; lncnt++)
            {
                if ((inname[lncnt] >= 'a' && inname[lncnt] <= 'z') || (inname[lncnt] >= 'A' && inname[lncnt] <= 'Z') || inname[lncnt] == ' ')
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
            }while(!CheckValidName(first_name));


            do {
                Console.WriteLine("Last Name");
                last_name = Console.ReadLine();
            }while(!CheckValidName(last_name));



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

           // bool lbExit = false;
            //while(!lbExit)
            //{
            //    Console.Clear();
            //    Console.WriteLine("Enter 0 Exit ");
            //    Console.WriteLine("Enter 1 Add Employee ");
            //    Console.WriteLine("Enter 2 list Employee ");
            //    Console.WriteLine("Enter 3 Find Employee ");
            //    Console.WriteLine("Enter 4 Update Employee");
            //    Console.WriteLine("Enter 5 Delete Employee Record ");

            //    string lsChoice = Console.ReadLine();
            //    switch (lsChoice)
            //    {

            //        // --------------Exit -------------------
            //        case "0":
            //            lbExit = true; 
            //            break;

            //        // --------------Add Employee -------------------
            //        case "1":

            //            lobjEmp.ReadInput();
            //            lobjEmp.Save();
            //            Console.WriteLine("Save Successfully!");
            //            Console.ReadKey();
            //            break;
            //         // ------------------ List All Employee ----------------

            //        case "2":

            //            lobjEmpList = lobjEmp.List();
            //            foreach( Employee lobjTempemp in lobjEmpList)
            //            {
            //                lobjTempemp.show();
            //            }
            //            Console.ReadKey();
            //           break;

            //            //----------- Find Employee ---------------

            //        case "3":
            //            Console.WriteLine("Enter Employee ID");
            //            string lsEmpId = Console.ReadLine();
            //            lobjEmpList = lobjEmp.Find(lsEmpId);
            //            if (lobjEmpList.Count == 0)
            //                Console.WriteLine("Wrong Employee Id");
            //            else
            //            {
            //                foreach (Employee lobjTempemp in lobjEmpList)
            //                {
            //                    lobjTempemp.show();
            //                }
            //            }
            //            Console.ReadKey();
            //            break;
            //         // ------------ Update Employee Details -----------------

            //        case "4":
            //            Console.WriteLine("Enter Employee ID");
            //            string lsEmpIdUp = Console.ReadLine();
            //            lobjEmpList = lobjEmp.Find(lsEmpIdUp);
            //            if (lobjEmpList.Count == 0)
            //                Console.WriteLine("Wrong Employee Id");

            //            else
            //            {
            //                lobjEmp.UpdateEmplpoyeeDetail(ref lobjEmpList);
            //                Console.WriteLine("Update Successfully");
            //            }
            //            Console.ReadKey();
            //            break;
            //            // --------------- Delete Employee Record ----------------

            //        case "5":
            //            Console.WriteLine("Enter Employee ID");
            //            string lsEmpIdDe = Console.ReadLine();
            //            lobjEmpList = lobjEmp.Find(lsEmpIdDe);
            //            if (lobjEmpList.Count == 0)
            //                Console.WriteLine("Wrong Employee Id");
            //            else
            //            {
            //              lobjEmp.DeleteEmployeeRecord(ref lobjEmpList);
            //            }
            //            Console.WriteLine("Delete Successfully");
            //            Console.ReadKey();
            //            break;


            //    }
            //}

            bool lbExit = false;
            List<TrainSystem> lobjTrainList = new List<TrainSystem>();
            List<Station> lobjStationList = new List<Station>();
            List<Schedule> lobjScheduleList = new List<Schedule>();
            TrainSystem lobjTrainSys = new TrainSystem();
            Station lobjStation = new Station();
            Schedule lobjSchedule = new Schedule();

            while (!lbExit) 
            {
                Console.Clear();
                Console.WriteLine("Enter 1 Add");
                Console.WriteLine("Enter 2 List ");
                Console.WriteLine("Enter 3 Update");
                Console.WriteLine("Enter 4 Delete");
                string lsChoice = Console.ReadLine();

                switch (lsChoice)
                {
                    case "1":
                        Console.WriteLine("Enter 1 Add For Train");
                        Console.WriteLine("Enter 2 Add For Station ");
                        Console.WriteLine("Enter 3 Add For Schedule");
                        string lschoiceAdd = Console.ReadLine();
                        switch (lschoiceAdd)
                        {
                            case "1":
                                lobjTrainSys.ReadInput();
                                if(lobjTrainSys.NewSave() == true)
                                    Console.WriteLine("Add Successfully");
                                else
                                    Console.WriteLine(" Not Add Successfully");
                                Console.ReadKey();
                                break;
                            case "2":
                                lobjStation.ReadInput();
                                if(lobjStation.Save() == true)

                                Console.WriteLine("Add Successfully");
                                else
                                    Console.WriteLine(" Not Add Successfully");
                                Console.ReadKey();
                                break;
                            case"3":
                                lobjSchedule.ReadInput();
                                if(lobjSchedule.Save() == true)
                                    Console.WriteLine("Add Successfully");
                                else
                                    Console.WriteLine(" Not Add Successfully");
                                Console.ReadKey();
                                break;

                        }
                        break;
                    case "2":
                        Console.WriteLine("Enter 1 List For Trains");
                        Console.WriteLine("Enter 2 List For Stations ");
                        Console.WriteLine("Enetr 3 List Schedule");
                        string lschoiceList = Console.ReadLine();
                        switch (lschoiceList)
                        {
                            case "1":
                                lobjTrainList=lobjTrainSys.ListOfTrain();
                                foreach(TrainSystem lobjtrainList  in lobjTrainList)
                                {
                                    lobjtrainList.Show();
                                }
                                Console.ReadKey();
                            
                        
                                break;
                            case "2":
                                lobjStationList = lobjStation.ListOfStation();
                                foreach (Station lobjstation in lobjStationList)
                                {
                                    lobjstation.Show();
                                }
                                Console.ReadKey();
                                break;
                            case "3":

                                lobjScheduleList = lobjSchedule.ListOfSchedule();
                                foreach (Schedule lobjschedule in lobjScheduleList)
                                {
                                    lobjSchedule.Show();
                                }
                                break;
                              

                        }
                        break;

                    case "3":

                        Console.WriteLine("Enter 1 Update For Trains");
                        Console.WriteLine("Enter 2 Upadate For Stations ");
                        Console.WriteLine("Enter 2 Update For Schedule ");
                        string lschoiceFind = Console.ReadLine();
                        switch (lschoiceFind)
                        {
                            case "1":
                                Console.WriteLine("Enter a Train Number");
                                string lsTrainNo = Console.ReadLine();
                                lobjTrainList = lobjTrainSys.FindTrain(lsTrainNo);
                                lobjTrainSys.UpadateTrainDetails(ref lobjTrainList);
                                Console.WriteLine(" Update Successfully");
                                Console.ReadKey();
                                break;
                            case "2":
                                Console.WriteLine("Enter a Station Code");
                                string lsStnCode = Console.ReadLine();

                                lobjStationList = lobjStation.FindStation(lsStnCode);
                                lobjStation.UpdateStationRecord(ref lobjStationList);
                                Console.WriteLine(" Update Successfully");
                                Console.ReadKey();
                                break;
                            case "3":
                                Console.WriteLine("Enter a Stn Code");
                                string lsStnCode1 = Console.ReadLine();
                                lobjScheduleList = lobjSchedule.FindSchedule(lsStnCode1);
                                lobjSchedule.UpdateScheduleRecord(ref lobjScheduleList);
                                Console.WriteLine(" Update Successfully");
                                Console.ReadKey();
                                break;

                        }

                        break;

                    case "4":

                        Console.WriteLine("Enter 1 Delete For Trains");
                        Console.WriteLine("Enter 2 Delete For Stations ");
                        Console.WriteLine("Enter 3 Delete For Schedule ");
                        string lschoiceDel = Console.ReadLine();
                        switch (lschoiceDel)
                        {
                            case "1":
                                Console.WriteLine("Enter a Train Number");
                                string lsTrainNo = Console.ReadLine();
                                lobjTrainList = lobjTrainSys.FindTrain(lsTrainNo);
                                lobjTrainSys.DeleteTrainRecord(ref lobjTrainList);
                                Console.WriteLine(" Delete  Successfully");
                                Console.ReadKey();
                                break;

                            case "2":

                                Console.WriteLine("Enter a Station Code");
                                string lsStnCode = Console.ReadLine();

                                lobjStationList = lobjStation.FindStation(lsStnCode);
                                lobjStation.DeleteStationRecord(ref lobjStationList);
                                Console.WriteLine(" Delete  Successfully");
                                Console.ReadKey();
                                break;
                            case "3":
                                Console.WriteLine("Enter a Station Code");
                                string lsStnCode1 = Console.ReadLine();

                                lobjScheduleList = lobjSchedule.FindSchedule(lsStnCode1);
                                lobjSchedule.DeleteScheduleRecord(ref lobjScheduleList);
                                Console.WriteLine(" Delete  Successfully");
                                Console.ReadKey();
                                break;

                        }

                        break;

                }
            }


        }
    }
}
