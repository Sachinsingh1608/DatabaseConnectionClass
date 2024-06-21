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
