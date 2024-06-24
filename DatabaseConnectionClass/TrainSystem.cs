using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectionClass
{
    public class Schedule
    {
        public int TrainNo { get; set; }
        public int StnCode { get; set; }
        public TimeSpan ArrTime { get; set; }
        public TimeSpan DeptTime { get; set; }

      public Schedule()
        {

        }

        public void Show()
        {
            Console.WriteLine("TrainNo " + TrainNo + " | " + "StnCode" + StnCode + " | " + "ArrTime" + ArrTime + " | " + "DeptTime" + DeptTime);
        }
      public Schedule(Schedule lobjSchedule)
        {
            TrainNo = lobjSchedule.TrainNo;
            StnCode = lobjSchedule.StnCode;
            ArrTime = lobjSchedule.ArrTime;
            DeptTime = lobjSchedule.DeptTime;
        }
        public void DeleteScheduleRecord(ref List<Schedule> inlobjScheduleLIst)
        {


            foreach (Schedule lobjTempStationList in inlobjScheduleLIst)
            {
                StnCode = lobjTempStationList.StnCode;
            }

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                string lsQuery = "DELETE FROM schedule WHERE StnCode = " + StnCode;
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

        public void UpdateScheduleRecord( ref List<Schedule> lobjSchedule)
        {
            foreach(Schedule lobjScheduleTemp in lobjSchedule) {
                Console.WriteLine("Old Train No :" + lobjScheduleTemp.TrainNo);
                Console.WriteLine("Enter a Train Number");
                string lsTrainNum = Console.ReadLine();
                if(lsTrainNum.Length != 0)
                {
                    TrainNo = int.Parse(lsTrainNum);
                }


                Console.WriteLine("Old Arr Time :" + lobjScheduleTemp.ArrTime);
                Console.WriteLine("Enter a Arr Time");
                string lsArr = Console.ReadLine();
                if (lsArr.Length != 0)
                {
                    ArrTime = TimeSpan.Parse(lsArr);
                }


                Console.WriteLine("Old Dept Time :" + lobjScheduleTemp.DeptTime);
                Console.WriteLine("Enter a Dept Time");
                string lsdept = Console.ReadLine();
                if (lsdept.Length != 0)
                {
                    DeptTime = TimeSpan.Parse(lsdept);
                }



            }

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                lsQuery = "UPDATE Schedule SET  TrainNo=" + TrainNo + ",ArrTime='" + ArrTime +
                     "',";
                lsQuery += "DeptTime='" + DeptTime + "'"+"where StnCode = "+StnCode.ToString();

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

        public List<Schedule> FindSchedule(string inStnCode) {
            List<Schedule> lobjScheduleList = new List<Schedule>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {

                string lsQuery = "SELECT TrainNo,StnCode,ArrTime,DeptTime from Schedule where StnCode="+ inStnCode;

                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                            TrainNo = (int)(lobjSDR[0]);
                            StnCode = (int)(lobjSDR[1]);

                            //if (DBNull.Value.Equals(lobjSDR[2]))
                            //{
                            //    ArrTime = timeSpan;
                            //}


                            ArrTime = (TimeSpan)lobjSDR[2];




                            //if (DBNull.Value.Equals(lobjSDR[3]))
                            //{
                            //    DeptTime = 
                            //}

                            DeptTime = (TimeSpan)lobjSDR[3];




                            lobjScheduleList.Add(new Schedule(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjScheduleList;
        }
        public void ReadInput()
        {
            Console.WriteLine("Enter a Train Number");
            TrainNo = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a Station Code ");
            StnCode= int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a Arrival Time");
            ArrTime = TimeSpan.Parse(Console.ReadLine());

            Console.WriteLine("Enter a Dept Time");
            DeptTime= TimeSpan.Parse(Console.ReadLine());   
        }
        public List<Schedule> ListOfSchedule()
        {
            List<Schedule> lobjScheduleList = new List<Schedule>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
         
                string lsQuery = "SELECT TrainNo,StnCode,ArrTime,DeptTime from Schedule";

                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                            TrainNo = (int)(lobjSDR[0]);
                            StnCode = (int)(lobjSDR[1]);

                            //if (DBNull.Value.Equals(lobjSDR[2]))
                            //{
                            //    ArrTime = timeSpan;
                            //}
                            
                            
                                ArrTime = (TimeSpan)lobjSDR[2];
                           

                           

                            //if (DBNull.Value.Equals(lobjSDR[3]))
                            //{
                            //    DeptTime = 
                            //}
                           
                                DeptTime = (TimeSpan)lobjSDR[3];
                           



                            lobjScheduleList.Add(new Schedule(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjScheduleList;
        }

        public bool Save()
        {
            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {

                lsQuery = "INSERT INTO Schedule (TrainNo,StnCode,ArrTime,DeptTime)";
                lsQuery += "VALUES("+ TrainNo + "," + StnCode + " ,'" + ArrTime + "','" + DeptTime + "'" + ")"; ;





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
                    return false;
                }
            }
            return true;
        }
    }
    public class Station
    {

        public string StnName { get; set; }
        public int StnCode { get; set; }
        public string StnType { get; set; }

        public int NoOfPlatForm { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string StationMaster { get; set; }

        public int StationMasterMobNo { get; set; }

        public string Juntion { get; set; } 

        public Station() { }
        public Station(Station InobjStation)
        {
            StnName = InobjStation.StnName;
            StnCode = InobjStation.StnCode;
            StnType = InobjStation.StnType;
            NoOfPlatForm = InobjStation.NoOfPlatForm;
            Address = InobjStation.Address;
            State = InobjStation.State;
            City = InobjStation.City;
            StationMaster = InobjStation.StationMaster;
            StationMasterMobNo = InobjStation.StationMasterMobNo;
            Juntion = InobjStation.Juntion;
        }
        public void ReadInput()
        {
            Console.WriteLine("Enter a StnName");
            StnName = (Console.ReadLine());

            Console.WriteLine("Enter A StnCode");
            StnCode = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter A StnType ");
            StnType = Console.ReadLine();

            Console.WriteLine("Enter A NoOfPlatForm");
            NoOfPlatForm = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter A Address");
            Address = (Console.ReadLine());


            Console.WriteLine("Enter A Sate");
            State = (Console.ReadLine());

            Console.WriteLine("Enter A City");
            City = (Console.ReadLine());


            Console.WriteLine("Enter A StationMaster");
            StationMaster = (Console.ReadLine());

            Console.WriteLine("Enter A StationMasterMobNo");
            StationMasterMobNo = int.Parse(Console.ReadLine());


            Console.WriteLine("Enter A Juntion");
            Juntion = (Console.ReadLine());



        }


        public void DeleteStationRecord(ref List<Station> inIobjStation)
        {


            foreach (Station lobjTempTrain in inIobjStation)
            {
                StnCode = lobjTempTrain.StnCode;
            }

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                string lsQuery = "DELETE FROM Station WHERE StnCode = " + StnCode;
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
        public void UpdateStationRecord(ref List<Station> inIobjStation)
        {
            foreach (Station IobjStn in inIobjStation)
            {

                Console.WriteLine("Old StnName:" + IobjStn.StnName);
                Console.WriteLine("Enter New Station Name");
                string lsStnName = Console.ReadLine();
                if(lsStnName.Length != 0)
                {
                    StnName= lsStnName;
                }


               

                Console.WriteLine("Old Station Type:" + IobjStn.StnType);
                Console.WriteLine("Enter New Station Type");
                string lsStntype = Console.ReadLine();
                if (lsStntype.Length != 0)
                {
                    StnType = lsStntype;
                }


                Console.WriteLine("Old No Of Platform:" + IobjStn.NoOfPlatForm);
                Console.WriteLine("Enter New  No Of Platform");
                string lsNoPlatform = Console.ReadLine();
                if (lsNoPlatform.Length != 0)
                {
                    NoOfPlatForm = int.Parse(lsNoPlatform);
                }

                Console.WriteLine("Old Address:" + IobjStn.Address);
                Console.WriteLine("Enter New Address ");
                string lsAddress = Console.ReadLine();
                if (lsAddress.Length != 0)
                {
                    Address = lsAddress;
                }



                Console.WriteLine("Old State:" + IobjStn.State);
                Console.WriteLine("Enter New State ");
                string lsState = Console.ReadLine();
                if (lsState.Length != 0)
                {
                    State = lsState;
                }


                Console.WriteLine("Old City:" + IobjStn.City);
                Console.WriteLine("Enter New City ");
                string lsCity = Console.ReadLine();
                if (lsCity.Length != 0)
                {
                    City = lsCity;
                }

                Console.WriteLine("Old StationMaster:" + IobjStn.StationMaster);
                Console.WriteLine("Enter New StationMaster ");
                string lsStationMaster = Console.ReadLine();
                if (lsStationMaster.Length != 0)
                {
                    StationMaster = lsStationMaster;
                }


                Console.WriteLine("Old StationMasterNumber:" + IobjStn.StationMasterMobNo);
                Console.WriteLine("Enter New  StationMasterNumber");
                string lsStationMasterMobile = Console.ReadLine();
                if (lsStationMasterMobile.Length != 0)
                {
                    StationMasterMobNo = int.Parse(lsStationMasterMobile);
                }


                Console.WriteLine("Old Junstion :" + IobjStn.Juntion);
                Console.WriteLine("Enter New Junstion");
                string lsJuntion = Console.ReadLine();
                if (lsJuntion.Length != 0)
                {
                    Juntion = lsJuntion;
                }

            }


            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                lsQuery = "UPDATE Station SET  StnName='" + StnName + "',StnType='" + StnType +
                     "',";
                lsQuery += "NoOfPlatForm=" + NoOfPlatForm + ",Address='" + Address + "',State ='" + State + "'" + ",City='" + City + "',StationMaster='" + StationMaster;
                lsQuery += "', StationMasterMobNo='" + StationMasterMobNo + "'," + "Juntion='" + Juntion + "'";

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

        public List<Station> FindStation(string inStnCode)
        {
            List<Station> lobjStationList = new List<Station>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
                string lsQuery = "SELECT StnName,StnCode,StnType,NoOfPlatForm,Address,State,City,StationMaster,StationMasterMobNo,Juntion FROM Station where StnCode =  " + inStnCode;

                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {



                            if (DBNull.Value.Equals(lobjSDR[0]))
                            {
                                StnName = "";
                            }
                            else
                            {
                                StnName = lobjSDR[0].ToString();
                            }

                            StnCode = (int)lobjSDR[1];

                            if (DBNull.Value.Equals(lobjSDR[2]))
                            {
                                StnType = "";
                            }
                            else
                            {
                                StnType = lobjSDR[2].ToString();
                            }

                            NoOfPlatForm = (int)lobjSDR[3];




                            if (DBNull.Value.Equals(lobjSDR[4]))
                            {
                                Address = null;
                            }
                            else
                            {
                                Address = lobjSDR[4].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[5]))
                            {
                                State = null;
                            }
                            else
                            {
                                State = lobjSDR[5].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[6]))
                            {
                                City = null;
                            }
                            else
                            {
                                City = lobjSDR[6].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[7]))
                            {
                                StationMaster = null;
                            }
                            else
                            {
                                StationMaster = lobjSDR[7].ToString();
                            }


                            StationMasterMobNo = (int)lobjSDR[8];


                            if (DBNull.Value.Equals(lobjSDR[9]))
                            {
                                Juntion = null;
                            }
                            else
                            {
                                Juntion = lobjSDR[9].ToString();
                            }

                            lobjStationList.Add(new Station(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjStationList;
        }
        public List<Station> ListOfStation()
        {
            List<Station> lobjTrainList = new List<Station>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
                string lsQuery = "SELECT StnName,StnCode,StnType,NoOfPlatForm,Address,State,City,StationMaster,StationMasterMobNo,Juntion FROM Station ";

                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                       


                            if (DBNull.Value.Equals(lobjSDR[0]))
                            {
                                StnName = "";
                            }
                            else
                            {
                                StnName = lobjSDR[0].ToString();
                            }

                            StnCode = (int)lobjSDR[1];

                            if (DBNull.Value.Equals(lobjSDR[2]))
                            {
                                StnType = "";
                            }
                            else
                            {
                                StnType = lobjSDR[2].ToString();
                            }

                            NoOfPlatForm = (int)lobjSDR[3];




                            if (DBNull.Value.Equals(lobjSDR[4]))
                            {
                                Address = null;
                            }
                            else
                            {
                                Address = lobjSDR[4].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[5]))
                            {
                                State = null;
                            }
                            else
                            {
                                State = lobjSDR[5].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[6]))
                            {
                                City = null;
                            }
                            else
                            {
                                City = lobjSDR[6].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[7]))
                            {
                                StationMaster = null;
                            }
                            else
                            {
                                StationMaster = lobjSDR[7].ToString();
                            }


                            StationMasterMobNo = (int)lobjSDR[8];


                            if (DBNull.Value.Equals(lobjSDR[9]))
                            {
                                Juntion = null;
                            }
                            else
                            {
                                Juntion = lobjSDR[9].ToString();
                            }

                            lobjTrainList.Add(new Station(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjTrainList;
        }

        public void Show()
        {
            Console.WriteLine($" StnName : {StnName} | StnCode : {StnCode} | StnName : {StnName} | NoOfPlatForm : {NoOfPlatForm}" +
                $" | Address : {Address} | State : {State} | City : {City} | StationMaster : {StationMaster} | StationMasterMobNo : {StationMasterMobNo} | Juntion : {Juntion}");
        }
        public bool Save()
        {

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {

                lsQuery = "INSERT INTO STATION (StnName,StnCode,StnType,NoOfPlatForm,Address,State,City,StationMaster,StationMasterMobNo,Juntion)";
                lsQuery += "VALUES("+"'" + StnName + "'," + StnCode + " ,'" + StnType + "'," + NoOfPlatForm  + ",'" + Address + "','" + State + "','" + City + "','" + StationMaster + "'," + StationMasterMobNo+",'"+ Juntion+"'" + ")"; ;





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
                    return false;
                }
            }
            return true;
        }





    }
    public class TrainSystem
    {

        public int Train_No { get; set; }
        public string TrainType { get; set; }
        public string TrainName { get; set; }
     
        public string StartStn{ get; set; }
       
        public string TerminStn { get; set; }
        public int FirstAcNo { get; set; }
        public int SecondAcNo { get; set; }
        public int SleeperNo {  get; set; }
        public int General {  get; set; }

        public TrainSystem()
        {

        }
        public TrainSystem(TrainSystem lobjTrain)
        {
            Train_No = lobjTrain.Train_No;
            TrainType = lobjTrain.TrainType;
            TrainName = lobjTrain.TrainName;
            StartStn = lobjTrain.StartStn;
            TerminStn = lobjTrain.TerminStn;
            FirstAcNo = lobjTrain.FirstAcNo;
            SecondAcNo = lobjTrain.SecondAcNo;
            SleeperNo = lobjTrain.SleeperNo;
            General = lobjTrain.General;
        }
        public void Show()
        {
            Console.WriteLine($" Train_No : {Train_No} | TrainType : {TrainType} | TrainName : {TrainName} | StartStn : {StartStn}" +
                $"TerminStn : {TerminStn} | FirstAcNo : {FirstAcNo} | SecondAcNo : {SecondAcNo} | SleeperNo : {SleeperNo} | General : {General}");
        }

        public void DeleteTrainRecord(ref List<TrainSystem> inIobjTrain)
        {


            foreach (TrainSystem lobjTempTrain in inIobjTrain)
            {
                Train_No = lobjTempTrain.Train_No;
            }

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                string lsQuery = "DELETE FROM Train WHERE TrainNo = " + Train_No;
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

        public void UpadateTrainDetails(ref List<TrainSystem> inIobjTrain)
           {
             foreach (TrainSystem IobjTainList in inIobjTrain)
              {
                
                Console.Write("Old Train Type:-" + IobjTainList.TrainType);
                Console.WriteLine("Enter a New Train Type");
                string lsTrainType = Console.ReadLine();
                if (lsTrainType.Length != 0)
                {
                    TrainType = (lsTrainType);
                }

                Console.Write("Old Train Name:-" + IobjTainList.TrainName);
                Console.WriteLine("Enter a New Train Number");
                string lsTrainName = Console.ReadLine();
                if (lsTrainName.Length != 0)
                {
                    TrainName = (lsTrainName);
                }


                Console.Write("Old Starting Station:-" + IobjTainList.StartStn);
                Console.WriteLine("Enter a New Starting Station");
                string lsStartStn = Console.ReadLine();
                if (lsStartStn.Length != 0)
                {
                    StartStn = (lsStartStn);
                }


                Console.Write("Old Termination Station:-" + IobjTainList.TerminStn);
                Console.WriteLine("Enter a New Termination Station");
                string lsTerminStn = Console.ReadLine();
                if (lsTerminStn.Length != 0)
                {
                    TerminStn = (lsTerminStn);
                }


                Console.Write("Old First Ac Coach:-" + IobjTainList.FirstAcNo);
                Console.WriteLine("Enter a New First Ac Coach");
                string lsFirstAc = Console.ReadLine();
                if (lsFirstAc.Length != 0)
                {
                    FirstAcNo = int.Parse(lsFirstAc);
                }


                Console.Write("Old Second Ac Coach:-" + IobjTainList.SecondAcNo);
                Console.WriteLine("Enter a New First Ac Coach");
                string lsSecond = Console.ReadLine();
                if (lsSecond.Length != 0)
                {
                    SecondAcNo = int.Parse(lsSecond);
                }


                Console.Write("Old General:-" + IobjTainList.General);
                Console.WriteLine("Enter a New First Ac Coach");
                string lsGen = Console.ReadLine();
                if (lsGen.Length != 0)
                {
                    General = int.Parse(lsGen);
                }






               }



            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {
                lsQuery = "UPDATE TRAIN SET  TrainType='" + TrainType + "',TrainName='" + TrainName+
                     "',";
                lsQuery += "StartStn='" + StartStn + "',TerminStn='" + TerminStn + "',FirstAcNo ='" + FirstAcNo + "'" + ",SecondAcNo=" + SecondAcNo + ",General=" + General;
                lsQuery += " WHERE  TrainNo = " + Train_No;
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


        public void ReadInput()
        {
            Console.WriteLine("Enter a Train ID");
            Train_No = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter A Train Name");
            TrainName = Console.ReadLine();

            Console.WriteLine("Enter A TrainType ");
            TrainType = Console.ReadLine();

            Console.WriteLine("Enter A StartStn");
            StartStn = (Console.ReadLine());

            Console.WriteLine("Enter A TerminStn");
            TerminStn = (Console.ReadLine());


            Console.WriteLine("Enter A FirstAcNo");
            FirstAcNo = int.Parse(Console.ReadLine());


            Console.WriteLine("Enter A SecondAcNo");
            SecondAcNo = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter A SleeperNo");
            SleeperNo = int.Parse(Console.ReadLine());


            Console.WriteLine("Enter A General");
            General = int.Parse(Console.ReadLine());

           

        }



        public bool Save()
        {

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {

                lsQuery = "INSERT INTO TRAIN (TrainNo,TrainType,TrainName,StartStn,TerminStn,FirstAcNo,SecondAcNo,SleeperNo,General)";
                lsQuery += "VALUES(" + Train_No + ",'" + TrainType + "' ,'" + TrainName + "','" + StartStn + "'" + ",'" + TerminStn + "'," + FirstAcNo + "," + SecondAcNo + "," + SleeperNo + "," + General + ")"; ;





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
                    return false;
                }
            }
            return true;
        }
        public  List<TrainSystem> FindTrain(string inTrainNo)
        {
            List<TrainSystem> lobjTrainList = new List<TrainSystem>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
                string lsQuery = "SELECT TrainNo,TrainType,TrainName,StartStn,TerminStn,FirstAcNo,SecondAcNo,SleeperNo,General FROM Train Where TrainNo = "+inTrainNo;

                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                            Train_No = (int)lobjSDR[0];


                            if (DBNull.Value.Equals(lobjSDR[1]))
                            {
                                TrainType = "";
                            }
                            else
                            {
                                TrainType = lobjSDR[1].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[2]))
                            {
                                TrainName = "";
                            }
                            else
                            {
                                TrainName = lobjSDR[2].ToString();
                            }



                            if (DBNull.Value.Equals(lobjSDR[3]))
                            {
                                StartStn = null;
                            }
                            else
                            {
                                StartStn = lobjSDR[3].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[4]))
                            {
                                TerminStn = null;
                            }
                            else
                            {
                                TerminStn = lobjSDR[4].ToString();
                            }


                            FirstAcNo = (int)lobjSDR[5];

                            SecondAcNo = (int)lobjSDR[6];
                            SleeperNo = (int)lobjSDR[7];
                            General = (int)lobjSDR[8];




                            lobjTrainList.Add(new TrainSystem(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjTrainList;
        }
        
        public List<TrainSystem> ListOfTrain()
        {
            List<TrainSystem> lobjTrainList = new List<TrainSystem>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
                string lsQuery = "SELECT TrainNo,TrainType,TrainName,StartStn,TerminStn,FirstAcNo,SecondAcNo,SleeperNo,General FROM Train ";

                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                            Train_No = (int)lobjSDR[0];


                            if (DBNull.Value.Equals(lobjSDR[1]))
                            {
                                TrainType = "";
                            }
                            else
                            {
                                TrainType = lobjSDR[1].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[2]))
                            {
                                TrainName = "";
                            }
                            else
                            {
                                TrainName = lobjSDR[2].ToString();
                            }



                            if (DBNull.Value.Equals(lobjSDR[3]))
                            {
                                StartStn = null;
                            }
                            else
                            {
                                StartStn =lobjSDR[3].ToString();
                            }


                            if (DBNull.Value.Equals(lobjSDR[4]))
                            {
                                TerminStn = null;
                            }
                            else
                            {
                                TerminStn = lobjSDR[4].ToString();
                            }


                            FirstAcNo = (int)lobjSDR[5];

                            SecondAcNo = (int)lobjSDR[6];
                            SleeperNo = (int)lobjSDR[7];
                            General = (int)lobjSDR[8];




                            lobjTrainList.Add(new TrainSystem(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjTrainList;
        }
        //public List<TrainSystem> FindTrainThroughTrainNum(string InTrainNum)
        //{
        //    List<TrainSystem> lobjTrainList = new List<TrainSystem>();

        //    string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
        //    using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
        //    {
        //        string lsQuery = "SELECT trainNo,TrainName,station,Schedule FROM Irctc  Where trainNo = " + InTrainNum ;

        //        SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        lobjconn.Open();
        //        using (SqlDataReader lobjSDR = cmd.ExecuteReader())
        //        {
        //            if (lobjSDR.HasRows)
        //            {
        //                while (lobjSDR.Read())
        //                {
        //                    train_No = (int)lobjSDR[0];


        //                    if (DBNull.Value.Equals(lobjSDR[1]))
        //                    {
        //                        TrainName = "";
        //                    }
        //                    else
        //                    {
        //                        TrainName = lobjSDR[1].ToString();
        //                    }

        //                    if (DBNull.Value.Equals(lobjSDR[2]))
        //                    {
        //                        station = "";
        //                    }
        //                    else
        //                    {
        //                        station = lobjSDR[2].ToString();
        //                    }



        //                    if (DBNull.Value.Equals(lobjSDR[3]))
        //                    {
        //                        Schedule = null;
        //                    }
        //                    else
        //                    {
        //                        Schedule = (TimeSpan)lobjSDR[3];
        //                    }




        //                    lobjTrainList.Add(new TrainSystem(this));
        //                }
        //            }
        //        }
        //        lobjconn.Close();
        //    }
        //    return lobjTrainList;
        //}
        //public List<string> ListStation()
        //{

        //    List<string> lobjTrainList = new List<string>();

        //    string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
        //    using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
        //    {
        //        string lsQuery = "SELECT  station FROM Irctc GROUP BY station ";

        //        SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        lobjconn.Open();
        //        using (SqlDataReader lobjSDR = cmd.ExecuteReader())
        //        {
        //            if (lobjSDR.HasRows)
        //            {
        //                while (lobjSDR.Read())
        //                {



        //                    if (DBNull.Value.Equals(lobjSDR[0]))
        //                    {
        //                        TrainName = "";
        //                    }
        //                    else
        //                    {
        //                        TrainName = lobjSDR[0].ToString();
        //                    }





        //                    lobjTrainList.Add(TrainName);
        //                }
        //            }
        //        }
        //        lobjconn.Close();
        //    }
        //    return lobjTrainList;

        //}

    }

}
