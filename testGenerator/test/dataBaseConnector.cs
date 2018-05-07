using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using System.Data;

namespace test
{
    class dataBaseConnector
    {
        private bool isDBConnected;
        private string loginDB;
        private string passwordDB;
        private string dataSourceDB;
        private OracleConnection connection;

        public dataBaseConnector()
        {
            isDBConnected = false;
        }

        public int returnAnswersNumber(int id)
        {
            int answersNumber;

            OracleCommand readId = new OracleCommand("SELECT LICZBAODPOWIEDZI FROM INFO_TESTU WHERE ID="+id, connection);
            OracleDataReader drInfo = readId.ExecuteReader();
            drInfo.Read();

            answersNumber = drInfo.GetInt32(0);

            return answersNumber;
        }

        public int writeNewTestInfo(string name, int questionsNumber, int answersNumber)
        {
            int newId;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string sqlCommand = "INSERT INTO INFO_TESTU (nazwaTestu, liczbaPytan, liczbaOdpowiedzi) VALUES ('" + name + "', " + questionsNumber + ", " + answersNumber + ")";
            OracleCommand writeNewTestInfo = new OracleCommand(sqlCommand, connection);
            OracleDataReader dataWriter = writeNewTestInfo.ExecuteReader();
            dataWriter.Close();

            OracleCommand readId = new OracleCommand("SELECT MAX(id) FROM INFO_TESTU", connection);
            OracleDataReader drInfo = readId.ExecuteReader();
            drInfo.Read();

            newId = drInfo.GetInt32(0);

            return newId;
        }

        public bool createNewDBConnection(string name, string login, string pass)
        {
            string constr = "Data Source=" + name + "; User Id=" + login + "; Password=" + pass + ";";
            //string constr = "Data Source=//192.168.43.155:1522/xe; User ID=hr; Password=mikulec32;"; //Not working
            connection = new OracleConnection(constr);
            
            connection.ConnectionString = constr;
            connection.Open();
            Console.WriteLine("chyba polaczono");
            if (connection.State != ConnectionState.Open)
            {
                 connection.Close();
                 connection.Open();
            }

            if (connection.State != ConnectionState.Open)
            {
                 return false;
            }
            else
            {
                 return true;
            }
            
        }

        public void closeDBConnection()
        {
            connection.Close();
            connection.Dispose();
        }

        public int readAnswers(int id_test)
        {
            int counter = 0;
            OracleCommand readInfo = new OracleCommand("SELECT LICZBAPYTAN FROM INFO_TESTU WHERE ID=" + id_test, connection);
            OracleDataReader drInfo = readInfo.ExecuteReader();
            drInfo.Read();
            int answersNumber = drInfo.GetInt32(0);


            StreamWriter writer = new StreamWriter("..\\..\\..\\Files\\Answers_" + id_test + ".csv");

            writer.Write("idTestu;testName;studentId;");

            for(int i=0; i < answersNumber; i++)
            {
                if (i < answersNumber-1)
                {
                    writer.Write("Quest" + i + ";");
                }
                else if (i==answersNumber-1)
                {
                    writer.WriteLine("Quest" + i);
                }

            }

            OracleCommand readResults = new OracleCommand("SELECT * FROM SPRAWDZIAN WHERE IDTESTU=" + id_test, connection);
            OracleDataReader dr = readResults.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    writer.Write(dr["IDTESTU"].ToString());
                    writer.Write(";");
                   // Console.WriteLine(dr["IDTESTU"].ToString());
                    writer.Write(dr["GRUPATESTU"].ToString());
                    writer.Write(";");
                    writer.Write(dr["INDEKSSTUDENTA"].ToString());
                    writer.Write(";");

                    for (int i = 1; i < (answersNumber + 1); i++)
                    {
                        if (i == answersNumber)
                        {
                            writer.WriteLine(dr[("PYT" + i)].ToString());
                        }
                        else
                        {
                            writer.Write(dr[("PYT" + i)].ToString());
                            writer.Write(";");
                        }
                    }
                    counter++;
                }
                writer.Close();
            }
            return counter;
        }

        public void setIsDBConnected(bool bol)
        {
            isDBConnected = bol;
        }

        public void setLoginDB(string log)
        {
            loginDB = log;
        }

        public void setPasswordDB(string pas)
        {
            passwordDB = pas;
        }

        public void setDataSourceDB(string nam)
        {
            dataSourceDB = nam;
        }

        public bool getIsDBConnected()
        {
            return isDBConnected;
        }

        public string getLoginDB()
        {
            return loginDB;
        }

        public string getPasswordDB()
        {
            return passwordDB;
        }

        public string getDataSourceDB()
        {
            return dataSourceDB;
        }
    }
}
