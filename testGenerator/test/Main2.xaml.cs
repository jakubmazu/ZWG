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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace test
{
    /// <summary>
    /// Interaction logic for Main2.xaml
    /// </summary>
    public partial class Main2 : Window
    {
        dataBaseConnector connector = new dataBaseConnector();
        answersChecker checker = new answersChecker();
        DirectoryInfo di = Directory.CreateDirectory("..\\..\\..\\Files");
        DirectoryInfo di2 = Directory.CreateDirectory("..\\..\\..\\Tests");

        public Main2()
        {
            InitializeComponent();
            /*try
            {
                if (connector.createNewDBConnection("ORCL", "SYSTEM", "Pentaxk2s"))
                {
                    TextPolBaza.Content = "Połączono";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+ Environment.NewLine+ "Problem with Database connection");
            }*/
        }

        private void buttonBazaPytan_Click(object sender, RoutedEventArgs e)
        {
            //[Ania]
            //tutaj wybierany jest plik z bazą pytań

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xlsx";

            openFileDialog1.Filter = "Text files (*.xlsx)|*.xlsx"; //format pliku
            openFileDialog1.ShowDialog();
            string strfilename = openFileDialog1.InitialDirectory + openFileDialog1.FileName;

            // DO MARCINA - zwrocę Ci sciezke do pliku, potrzebuje liczby pytan i liczby odp, czyli w sumie trzeba przekleic to co jest na dole
            string lokalizacjaExcelPytania = "d:\\Marcin\\Szkoła\\Polibuda\\[INF mgr] I rok I semestr (2018 lato)\\Zastosowania inform. w gospod\\P - Zastosowania inform. w gospod\\Repo - wspólne\\testGenerator\\test\\bin\\Debug\\zestawPytań2.xlsx";
            List<Pytanie> wszystkiePytania = new List<Pytanie>();
            wszystkiePytania = ReadFromExcelv2.Read(lokalizacjaExcelPytania);               // wczytuje wszystkie pytania
            int liczbaWszystkichOdpowiedzi = wszystkiePytania[0].listaOdpowiedzi.Count();   // dla Ani - liczba wszystkich odpowiedzi
            int liczbaWszystkichPytan = wszystkiePytania.Count();                           // dla Ani - liczba wszystkich pytań

            maxPytan.Content = liczbaWszystkichPytan.ToString();
            maxOdp.Content = liczbaWszystkichOdpowiedzi.ToString();


            TextLiczbaPytan.IsEnabled = true;
            TextLiczbaOdp.IsEnabled = true;
            sliderOdp.IsEnabled = true;
            sliderPyt.IsEnabled = true;
            buttonGenerujTest.IsEnabled = true;
        }

        private void buttonGenerujTest_Click(object sender, RoutedEventArgs e)
        {
            //[Ania]
            //tutaj bedzie ttylko generowany teest

            //boole do losowych
            bool losowyUkladPytan = (LosPyt.IsChecked).Value;
            bool losowyUkladOdp = (LosOdp.IsChecked).Value;

            //int id =  connector.writeNewTestInfo(TextNazwaTestu.Text, Int32.Parse(TextLiczbaPytan.Text), Int32.Parse(TextLiczbaOdp.Text));
            int id = 5;                   // do testow, to będzie wczytywane potem z bazy od Kuby

            int liczbaPytan = Int32.Parse(TextLiczbaPytan.Text);        // liczba wybranych pytań do wygenerowania
            int liczbaOdpowiedzi = Int32.Parse(TextLiczbaOdp.Text);     // liczba wybranych odpowiedzi do wygenerowania
            
            string nazwaTestu = TextNazwaTestu.Text;
            
            string lokalizacjaExcelPytania = "d:\\Marcin\\Szkoła\\Polibuda\\[INF mgr] I rok I semestr (2018 lato)\\Zastosowania inform. w gospod\\P - Zastosowania inform. w gospod\\Repo - wspólne\\testGenerator\\test\\bin\\Debug\\zestawPytań2.xlsx";
            
            // lista obiektow Pytanie, wczytanie do niej pytan z Excela i wyswietlenie wszystkich pytan
            ////////////////////////////////////////////////////////////////////////////////////
            ///  DLA ANI /////
            ////////////////////////////////////////////////////////////////////////////////////
            //string lokalizacjaExcelPytania = "NIC";                       // Tutaj Ania uzupełni
            List <Pytanie> wszystkiePytania = new List<Pytanie>();          //Marcina zostawić
            wszystkiePytania = ReadFromExcelv2.Read(lokalizacjaExcelPytania); //tego Marcin potrzebuje
            int liczbaWszystkichOdpowiedzi = wszystkiePytania[0].listaOdpowiedzi.Count();          
            int liczbaWszystkichPytan = wszystkiePytania.Count();          // to dla Ani
            ////////////////////////////////////////////////////////////////////////////////////
            
            if ((losowyUkladPytan) || (losowyUkladOdp))         // pomieszaj ewentualnie pytania i/lub odpowiedzi
            {
                wszystkiePytania = Pomieszaj.Losowo(wszystkiePytania, losowyUkladPytan, losowyUkladOdp);      // pomieszanie wszystkich pytan i odpowiedzi
            }
            
            ExportToPdfv2.GenerateTest(wszystkiePytania, id, nazwaTestu, liczbaPytan);      // generowanie pliku *.pdf z pytaniami do testu (wczytanymi z Excela)

            //do testow stworzono idTestu I nemeTest, potem "idTestu" bedzie zwracane z bazy a uzytkownik bedzie podawal "nameTest"
            // generowanie klucza odpowiedzi do pliku *.csv, zeby potem ten plik wczytac do bazy danych
            GenerateCSV.Generate(wszystkiePytania, id, nazwaTestu);
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            (new Logowanie()).Show();
            Close();

        }

        private void buttonSprawdz_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            double[] Tab;
            Tab = new double[6];
            Tab[0] = Int32.Parse(Prog55.Text);
            Tab[1] = Int32.Parse(Prog5.Text);
            Tab[2] = Int32.Parse(Prog45.Text);
            Tab[3] = Int32.Parse(Prog4.Text);
            Tab[4] = Int32.Parse(Prog35.Text);
            Tab[5] = Int32.Parse(Prog3.Text);

            try
            {
                try
                {
                    checker.readAnswers("Answers_" + TextIdTestuPob.Text + ".csv");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + Environment.NewLine + "Not found: answers for this test");
                    check = false;
                }
                int answerNumb = connector.returnAnswersNumber(Int32.Parse(TextIdTestuPob.Text));

                try
                {
                    checker.readKey("Key_" + TextIdTestuPob.Text + ".csv", answerNumb);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + Environment.NewLine + "Not found: key for this test");
                    check = false;
                }

                checker.checkTest(TextIdTestuPob.Text, Tab);

                if (check)
                {
                    MessageBox.Show("Test checked");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + Environment.NewLine + "Problem with test checking");
            }
        }


        private void buttonPobierzTesty_Click(object sender, RoutedEventArgs e)
        {
            connector.readAnswers(Int32.Parse(TextIdTestuPob.Text));
            buttonSprawdz.IsEnabled = true;

            try
            {
                connector.readAnswers(Int32.Parse(TextIdTestuPob.Text));
                MessageBox.Show("Answers downloaded from database");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + Environment.NewLine + "Problem with downloading answers");
            }
        }

        private void Viewbox_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void DodajKonto_Click(object sender, RoutedEventArgs e)
        {
            (new NowyProfil()).Show();


        }
    }
}
