using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;



namespace test
{
    class Profile
    {
        public string[] User
        {
            get;
            private set;
        }

        public string[] Password
        {
            get;
            private set;
        }

        public Profile(string [] u, string [] p)
        {
            User = u;
            Password = p;
        }

        // dodać ścieżkę do pliku z loginami i hasłami ---------------------    TO DO    ------------
        // szyfrowanie haseł??? ---------------------    TO DO    ------------

        public List<string> Users = new List<string>();
        public List<string> Passwords = new List<string>();
       // public List<string> DataBasePassword = new List<string>();
        //public List<string> DataBaseAdress = new List<string>();

        public void Read()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();

            // lokalizacja pliku z baza pytan
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:\studia\2_Informatyka\2_1\Zastosowanie inf w gospodarce\projekt\Nowy folder\ZWG\testGenerator\logs.xlsx");

            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;

            for(int i = 1; i<=rowCount; i++)
            {
                Users.Add(xlRange.Cells[i, 1].Value2.ToString());
                Passwords.Add(xlRange.Cells[i, 2].Value2.ToString());
                //DataBasePassword.Add(xlRange.Cells[i, 3].Value2.ToString());
               // DataBaseAdress.Add(xlRange.Cells[i, 4].Value2.ToString());
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        public Profile() 
        {
            Read();


            /*FileStream fs = File.Open("logs.xlsx", FileMode.Open, FileAccess.ReadWrite);
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(fs);
            logs = reader as DataSet;   */

            /*User = new string[3];
            User[0] = "aa";
            User[1] = "bb";
            User[2] = "admin";

            Password = new string[3];
            Password[0] = "aa";
            Password[1] = "bb";
            Password[2] = "admin";*/
        }
    }
}
