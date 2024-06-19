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
    public class TrainSystem
    {

        public int train_No { get; set; }
        public string TrainName { get; set; }
        public string station { get; set; }
        public TimeSpan? Schedule {  get; set; }

        public TrainSystem()
        {

        }
        public TrainSystem(TrainSystem lobjTrain)
        {
            train_No = lobjTrain.train_No;
            station = lobjTrain.station;
            Schedule = lobjTrain.Schedule;
            TrainName = lobjTrain.TrainName;
        }


        public void ReadInput()
        {
            Console.WriteLine("Enter a Train ID");
            train_No = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter A Train Name");
            TrainName = Console.ReadLine();

            Console.WriteLine("Enter A Station Name");
            station = Console.ReadLine();

            Console.WriteLine("Enter A Schdule");
            Schedule = TimeSpan.Parse(Console.ReadLine());
        }
        public bool Save()
        {

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";

            string lsQuery = "";
            using (SqlConnection lobjCon = new SqlConnection(lsConnStr))
            {

                lsQuery = "INSERT INTO Irctc (trainNo,TrainName,station,Schedule)";
                lsQuery += "VALUES(" +train_No.ToString()+ ",'"+TrainName + "' ,'" + station + "','";

                lsQuery += Schedule + "')";



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

        public List<TrainSystem> FindThroughStationName(List<TrainSystem> list)
        public List<TrainSystem> ListStation()
        {
            List<TrainSystem> lobjTrainList = new List<TrainSystem>();

            string lsConnStr = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=C#Training; Data Source=LAPTOP-LFHQRLA5\\SQLEXPRESS";
            using (SqlConnection lobjconn = new SqlConnection(lsConnStr))
            {
                string lsQuery = "SELECT trainNo,TrainName,station,Schedule FROM Irctc ";
            
                SqlCommand cmd = new SqlCommand(lsQuery, lobjconn);
                cmd.CommandType = System.Data.CommandType.Text;
                lobjconn.Open();
                using (SqlDataReader lobjSDR = cmd.ExecuteReader())
                {
                    if (lobjSDR.HasRows)
                    {
                        while (lobjSDR.Read())
                        {
                           train_No = (int)lobjSDR[0];


                            if (DBNull.Value.Equals(lobjSDR[1]))
                            {
                                TrainName = "";
                            }
                            else
                            {
                                TrainName = lobjSDR[1].ToString();
                            }

                            if (DBNull.Value.Equals(lobjSDR[2]))
                            {
                                station = "";
                            }
                            else
                            {
                                station = lobjSDR[2].ToString();
                            }



                            if (DBNull.Value.Equals(lobjSDR[3]))
                            {
                                Schedule = null;
                            }
                            else
                            {
                                Schedule = (TimeSpan)lobjSDR[3];
                            }




                            lobjTrainList.Add(new TrainSystem(this));
                        }
                    }
                }
                lobjconn.Close();
            }
            return lobjTrainList;
          
        }
      
    }
   
}
