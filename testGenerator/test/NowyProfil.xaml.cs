using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;

namespace test
{
    /// <summary>
    /// Interaction logic for NowyProfil.xaml
    /// </summary>
    public partial class NowyProfil : Window
    {
        public NowyProfil()
        {
            InitializeComponent();
                          
        }


        private void buttonStworzKonto_Click(object sender, RoutedEventArgs e)
        {


            

            bool zajety = false;

            Profile profile = new Profile();

            for (int i = 0; i < profile.Users.Count(); i++)
            {
                if (profile.Users[i] == TextNLogin.Text)
                {
                    zajety = true;
                    break;
                }
            }

            

            /*
            for(int i =0; i<profile.User.Length;i++)
            {
                if ( profile.User [i] == TextNLogin.Text)
                {
                    zajety = true;
                }
            }*/

            if (TextNLogin.Text != "" && NewPassword.Password == NewPassword2.Password && NewPassword.Password != "" && zajety == false)
            {
                // tworzenie nowego profilu ---------------------    TO DO    ------------

                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();

                // lokalizacja pliku z baza pytan
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:\studia\2_Informatyka\2_1\Zastosowanie inf w gospodarce\projekt\Nowy folder\ZWG\testGenerator\logs.xlsx");

                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;

                xlRange.Cells[rowCount + 1, 1] = TextNLogin.Text;
                xlRange.Cells[rowCount + 1, 2] = NewPassword.Password;

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

                Close();
            }
            else if(zajety== true)
                KomNP.Content = "Nazwa uzytkownika jest już zajęta";
            else if(TextNLogin.Text == "")
                KomNP.Content = "Podaj nazwę użytkownika";
            else if (NewPassword.Password == NewPassword2.Password && NewPassword.Password == "")
                KomNP.Content = "Wprowadź hasło";
            else if (NewPassword.Password != NewPassword2.Password)
                KomNP.Content = "Hasła są różne";

        }
    }
}
