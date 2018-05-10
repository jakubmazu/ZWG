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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// Interaction logic for Logowanie.xaml
    /// </summary>
    public partial class Logowanie : Window
    {
        public Logowanie()
        {
            InitializeComponent();
            TextLogin.Focus();
        }

        private void buttonZaloguj_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            bool correct = false;
            
            for (int i = 0; i < profile.User.Length; i++)
            {
                if (profile.User[i] == TextLogin.Text && profile.Password[i] == Password.Password)
                {
                    correct = true;
                }
            }
            if (correct == true)
            {
                Main2 nowy2 = new Main2();
                nowy2.Show();
                Application.Current.Properties["Login"] = TextLogin.Text;
                Application.Current.Properties["ConnectedStatus"] = "brak";
                nowy2.zalogowany.Text = Application.Current.Properties["Login"].ToString();
                Close();
            }
            else
                KomH.Content = "Nieprawidłowy login lub hasło.";
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonZaloguj_Click(sender, e);
            }
        }

        private void buttonPubliczny_Click(object sender, RoutedEventArgs e)
        {
            //(new Main()).Show();
           // Main nowy = new Main();
            //nowy.Show();
            Main2 nowy2 = new Main2();
            nowy2.Show();

            Application.Current.Properties["Login"] = "profil publiczny";
            Application.Current.Properties["ConnectedStatus"] = "brak";
            nowy2.zalogowany.Text = Application.Current.Properties["Login"].ToString();

            if(Application.Current.Properties["Login"].ToString() == "admin")
                nowy2.DodajKonto.IsEnabled = true;
            else
                nowy2.DodajKonto.IsEnabled = false;


            //nowy.TextNazwaTestu.Text = Application.Current.Properties["Login"].ToString();
            //nowy.TextPolBaza.Text = Application.Current.Properties["ConnectedStatus"].ToString();

            Close();
        }

    }
}
