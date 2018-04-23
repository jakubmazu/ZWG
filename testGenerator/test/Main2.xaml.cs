﻿using System;
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
