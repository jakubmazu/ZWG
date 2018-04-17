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

         

            for(int i =0; i<profile.User.Length;i++)
            {
                if ( profile.User [i] == TextNLogin.Text)
                {
                    zajety = true;
                }
            }

            if (TextNLogin.Text != "" && NewPassword.Password == NewPassword2.Password && NewPassword.Password != "" && zajety == false)
            {
                // tworzenie nowego profilu ---------------------    TO DO    ------------

                (new Main2()).Show();

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
