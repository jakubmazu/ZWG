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


            // bla bla bla - komantarz do usunięcia

            //int id =  connector.writeNewTestInfo(TextNazwaTestu.Text, Int32.Parse(TextLiczbaPytan.Text), Int32.Parse(TextLiczbaOdp.Text));
            int id = 5;                   // do testow

            int liczbaPytan = Int32.Parse(TextLiczbaPytan.Text);
            //int liczbaPytan = 7;        // do testow
            int liczbaOdpowiedzi = Int32.Parse(TextLiczbaOdp.Text);
            //int liczbaOdpowiedzi = 5;   // do testow

            string nazwaTestu = TextNazwaTestu.Text;
            
            //string lokalizacjaExcelPytania = ... //pobrać a okienka aplikacji
            string lokalizacjaExcelPytania = "d:\\Marcin\\Szkoła\\Polibuda\\[INF mgr] I rok I semestr (2018 lato)\\Zastosowania inform. w gospod\\P - Zastosowania inform. w gospod\\Repo - wspólne\\testGenerator\\test\\bin\\Debug\\zestawPytań2.xlsx";
            //string lokalizacjaExcelPytania = "d:\\zestawPytań2";    //KUBA TU WPISZ SWOJĄ LOKALIZAJĘ PLIKU, PAMIĘTAJ O PODWÓJNYM \\ (SLESZU) PRZY PODAWANIU FOLDERÓW

            //bool checkbox // wybór ilości poprawnych odp - true tylko 1 poprawna

            // lista obiektow Pytanie, wczytanie do niej pytan z Excela i wyswietlenie wszystkich pytan
            ////////////////////////////////////////////////////////////////////////////////////
            ///  DLA ANI /////
            ////////////////////////////////////////////////////////////////////////////////////
            //string lokalizacjaExcelPytania = "NIC";                       // Tutaj Ania uzupełni
            List <Pytanie> wszystkiePytania = new List<Pytanie>();          //Marcina zostawić
            wszystkiePytania = ReadFromExcelv2.Read(lokalizacjaExcelPytania); //tego Marcin potrzebuje
            int liczbaWszystkichOdpowiedzi = wszystkiePytania[0].listaOdpowiedzi.Count();                       // TO JEST ŹLE, TAK NIE MOZE BYĆ, NIE MAM POMYSŁU JAK TO ZEMIENIĆ (MARCIN)              
            int liczbaWszystkichPytan = wszystkiePytania.Count();          // to dla Ani

            ////////////////////////////////////////////////////////////////////////////////////

            //////////////// NIEPOTRZEBNE JUZ ///////////////////////////////////////
            //int mniejszaIloscPytan = 0;                         //potrzebne do tego gdy uzytkownik poda ze chce test z wieksza liczba pytan niz ja posiada
            //if (liczbaPytan > wszystkiePytania.Count())
            //{
            //    mniejszaIloscPytan = wszystkiePytania.Count();  // jezeli uzytkownik chce wiecej pytan niz posiada to robimy tyle ile posiada
            //}
            //else mniejszaIloscPytan = liczbaPytan;              // jezeli uzytkownik chce mniej pytan niz posiada w zbiorze to robimy tyle ile on chce


            //losowanie bez powtorzen zeby pytania byly w losowej kolejnosci
            int[] liczbyBezPowtorzen = new int[liczbaWszystkichPytan];
            liczbyBezPowtorzen = LosowanieBezPowtorzen.Losowanie(wszystkiePytania.Count(), liczbaWszystkichPytan);     // ze wszystki pytan losujemy mniejsza liczbe pytan (ktora podal uzytkownik, chyba ze jest ich mniej to losujemy wszystko)

            // nowa lista wszystkich pytan w losowej kolejnosci pytania i odpowiedzi
            // ZROBIC IF'A BOOL CZY MAJA BYC PYTANIA W LOSOWEJ KOLEJNOSCI I OSOBNO CZY ODPOWIEDZI W LOSOWEJ, JESLI TAK TO POMIESZAC PYTANIA
            wszystkiePytania = Pomieszaj.Losowo(wszystkiePytania);      // pomieszanie wszystkich pytan i odpowiedzi

            // generowanie pliku *.pdf z pytaniami do testu (wczytanymi z Excela)
            ExportToPdfv2.GenerateTest(wszystkiePytania, id, nazwaTestu, liczbaPytan);       // TO BEDZIE DZIALAC JAK TO OPRACUJEMY
            
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
            Tab[0] = 95;
            Tab[1] = 90;
            Tab[2] = 80;
            Tab[3] = 70;
            Tab[4] = 60;
            Tab[5] = 50;

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
<<<<<<< HEAD
            connector.readAnswers(Int32.Parse(TextIdTestuPob.Text));
            buttonSprawdz.IsEnabled = true;
=======
            try
            {
                connector.readAnswers(Int32.Parse(TextIdTestuPob.Text));
                MessageBox.Show("Answers downloaded from database");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + Environment.NewLine + "Problem with downloading answers");
            }
>>>>>>> 1180242635973e4c49ce0be8bc99c5e82d055413
        }

        private void Viewbox_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

       
    }
}
