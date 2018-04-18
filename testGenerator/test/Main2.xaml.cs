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

namespace test
{
    /// <summary>
    /// Interaction logic for Main2.xaml
    /// </summary>
    public partial class Main2 : Window
    {
        dataBaseConnector connector = new dataBaseConnector();
        answersChecker checker = new answersChecker();

        public Main2()
        {
            InitializeComponent();
        }

        private void buttonPolBaza_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonGenerujTest_Click(object sender, RoutedEventArgs e)
        {
            //int id =  connector.writeNewTestInfo(TextNazwaTestu.Text, Int32.Parse(TextLiczbaPytan.Text), Int32.Parse(TextLiczbaOdp.Text));
            int id = 5;

            // zadne zmiany - tylko do testu - usunac ten komantarz
            int liczbaPytan = Int32.Parse(TextLiczbaPytan.Text);
            int liczbaOdpowiedzi = Int32.Parse(TextLiczbaOdp.Text);

            string nazwaTestu = TextNazwaTestu.Text;
            string tytulTestu = TextTytulTestu.Text;

            //bool checkbox // wybór ilości poprawnych odp - true tylko 1 poprawna

            // lista obiektow Pytanie, wczytanie do niej pytan z Excela i wyswietlenie wszystkich pytan
            List<Pytanie> wszystkiePytania = new List<Pytanie>();
            wszystkiePytania = ReadFromExcel.Read();



            // tu stworzyc nowa liste z mniejsza liczba pytania z listy wszystkich pytan

            // generowanie pliku *.pdf z pytaniami do testu (wczytanymi z Excela)
            ExportToPdf.GenerateTest(wszystkiePytania, id, TextTytulTestu.Text);       // TO BEDZIE DZIALAC JAK TO OPRACUJEMY
            
            //do testow stworzono idTestu I nemeTest, potem "idTestu" bedzie zwracane z bazy a uzytkownik bedzie podawal "nameTest"
            // generowanie klucza odpowiedzi do pliku *.csv, zeby potem ten plik wczytac do bazy danych
            GenerateCSV.Generate(wszystkiePytania, id, TextNazwaTestu.Text);
            
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
            checker.readAnswers("Answers_" + TextIdTestuPob.Text + ".csv");
            int answerNumb = connector.returnAnswersNumber(Int32.Parse(TextIdTestuPob.Text));
            checker.readKey("Key_" +TextIdTestuPob.Text + ".csv", answerNumb);
            checker.checkTest(TextIdTestuPob.Text);
        }

        private void buttonBPolacz_Click(object sender, RoutedEventArgs e)
        {
            bool check = connector.createNewDBConnection(BSource.Text, TextBLogin.Text, BPassword.Password);
            if (check == true) TextPolBaza.Content = "Połączono";
        }

        private void buttonPobierzTesty_Click(object sender, RoutedEventArgs e)
        {
            connector.readAnswers(Int32.Parse(TextIdTestuPob.Text));
        }
    }
}
