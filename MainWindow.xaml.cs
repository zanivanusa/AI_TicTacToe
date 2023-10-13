using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI_naloga2_minmax
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        Igra _Igra = new Igra();
        MinMax minmax = new MinMax();
        int stZmagClovek = 0;
        int stZmagRacunalnik = 0;
        int stIzenacenj = 0;
        int _globina = 2;
        public int[] najboljsaPotezaMax(string[,] polje)//vedno da samo naslednjo
        {
            int[] konec = new int[2];
            var najboljsaPoteza = int.MinValue;
            int vrstica = -1;//rabimo ker komaj na koncu naredimo potezo
            int stolpec = -8; //shranimo si -1 da vemo da smo spremenili, 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrWhiteSpace(polje[i, j]))
                    {
                        polje[i, j] = _Igra.max_char;
                        int vrednost = minmax.minMax(polje, _globina, false); // preverimo trenutno najboljso potezo
                        polje[i, j] = "";
                        if (vrednost > najboljsaPoteza)
                        {
                            vrstica = i;
                            stolpec = j;
                            najboljsaPoteza = vrednost;
                        }
                    }
                }
            }
            konec[0] = vrstica;
            konec[1] = stolpec;
            return konec;
        }
        public int[] najboljsaPotezaMin(string[,] polje)//vedno da samo naslednjo
        {
            int[] konec = new int[2];
            var najboljsaPoteza = int.MaxValue;
            int vrstica = -2;//rabimo ker komaj na koncu naredimo potezo
            int stolpec = -1; //shranimo si -1 da vemo da smo spremenili, 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrWhiteSpace(polje[i, j]))
                    {
                        polje[i, j] = _Igra.min_char;
                        int vrednost = minmax.minMax(polje, _globina, true); //tu ne pripise vrednosti pri globini 9
                        polje[i, j] = "";
                        if (vrednost < najboljsaPoteza)
                        {
                            vrstica = i;
                            stolpec = j;
                            najboljsaPoteza = vrednost;
                        }
                    }
                }
            }
            konec[0] = vrstica;
            konec[1] = stolpec;
            return konec;
        }
        public int[] najboljsaPotezaMinAlfa(string[,] polje)//vedno da samo naslednjo
        {
            int[] konec = new int[2];
            var najboljsaPoteza = int.MaxValue;
            int vrstica = -2;//rabimo ker komaj na koncu naredimo potezo
            int stolpec = -1; //shranimo si -1 da vemo da smo spremenili, 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrWhiteSpace(polje[i, j]))
                    {
                        polje[i, j] = _Igra.min_char;
                        int vrednost = minmax.alfaBeta(polje, _globina, true, int.MinValue, int.MaxValue); //tu ne pripise vrednosti pri globini 9
                        polje[i, j] = "";
                        if (vrednost < najboljsaPoteza)
                        {
                            vrstica = i;
                            stolpec = j;
                            najboljsaPoteza = vrednost;
                        }
                    }
                }
            }
            konec[0] = vrstica;
            konec[1] = stolpec;
            return konec;
        }


        //pri zacne ai se po nasem vnosi ne updajta vrstica in stolpec ko se klice ta funkcija
        public int[] najboljsaPotezaMaxAlfa(string[,] polje)//vedno da samo naslednjo
        {
            int[] konec = new int[2];
            var najboljsaPoteza = int.MinValue;
            int vrstica = -1;//rabimo ker komaj na koncu naredimo potezo
            int stolpec = -1; //shranimo si -1 da vemo da smo spremenili, 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrWhiteSpace(polje[i, j]))
                    {
                        polje[i, j] = _Igra.max_char;
                        int vrednost = minmax.alfaBeta(polje, _globina, false, int.MaxValue, int.MinValue); // preverimo trenutno najboljso potezo
                        polje[i, j] = "";
                        if (vrednost > najboljsaPoteza)
                        {
                            vrstica = i;
                            stolpec = j;
                            najboljsaPoteza = vrednost;
                        }
                    }
                }
            }
            konec[0] = vrstica;
            konec[1] = stolpec;
            return konec;
        }




        public void disableButtons()
        {
            foreach (var buton in buttoni.Children)
            {
                if (((Button)buton).Content == string.Empty)
                {
                    ((Button)buton).IsEnabled = false;
                }
            }
        }
        private void Button_ZacneAI(object sender, RoutedEventArgs e)
        {
            _Igra.zacelAI = true;
            foreach (var polje in gumbi.Children)//disablamo gumb za ai zacetek
            {
                if (((Button)polje).Name == "AI")
                {
                    ((Button)polje).IsEnabled = false;
                    break;
                }
            }

            int[] temp = najboljsaPotezaMax(_Igra.Polje);
            _Igra.PosodobiPolje(temp[0], temp[1], _Igra.max_char);
            foreach (var polje in buttoni.Children)//verjetno obstaja boljsa funkcija za to
            {
                if (((Button)polje).Tag.ToString() == $"{temp[0]},{temp[1]}")
                {
                    ((Button)polje).Content = _Igra.max_char;
                    //PlayerClick(((Button)polje), e);
                    break;
                }
            }


        }
        private void Button_PonovnaIgra(object sender, RoutedEventArgs e)
        {
            _Igra.zacelAI = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _Igra.Polje[i, j] = "";
                }
            }

            foreach (var polje in buttoni.Children)
            {
                if (polje is Button)
                {
                    ((Button)polje).Content = String.Empty;
                    ((Button)polje).IsEnabled = true;
                }
            }
            foreach (var polje in gumbi.Children)//verjetno obstaja boljsa funkcija za to
            {
                if (((Button)polje).Name == "AI")
                {
                    ((Button)polje).IsEnabled = true;
                    break;
                }
            }
            zaslonZmage.Visibility = Visibility.Hidden;
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = (sender as ListBox).SelectedItem as ListBoxItem;
            var nekej = lbi.Content.ToString();
            var nekej2 = nekej.Substring(10, 1);
            _globina = int.Parse(nekej2);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int x = ((Button)sender).Tag.ToString()[0];
            int y = ((Button)sender).Tag.ToString()[2];
            if (!string.IsNullOrWhiteSpace(_Igra.Polje[x - 48, y - 48]))
            {//ascii
                return;
            }
            if (!_Igra.zacelAI)
            {
                ((Button)sender).Content = _Igra.max_char;
                _Igra.PosodobiPolje(x - 48, y - 48, _Igra.max_char);

                //preverimo ce je konec
                if (_Igra.GameEnd() == "X")
                {
                    stZmagClovek++;
                    igralec.Text = stZmagClovek.ToString();
                    disableButtons();
                    return;
                }
                if (_Igra.GameEnd() == "gameFull")
                {
                    stIzenacenj++;
                    izenaceno.Text = stIzenacenj.ToString();
                    disableButtons();
                    return;
                }

                int[] temp = najboljsaPotezaMin(_Igra.Polje);
                _Igra.PosodobiPolje(temp[0], temp[1], _Igra.min_char);
                foreach (var polje in buttoni.Children)//verjetno obstaja boljsa funkcija za to
                {
                    if (((Button)polje).Tag.ToString() == $"{temp[0]},{temp[1]}")
                    {
                        ((Button)polje).Content = _Igra.min_char;
                        break;
                    }
                }
                if (_Igra.GameEnd() == "O")
                {
                    stZmagRacunalnik++;
                    racunalnik.Text = stZmagRacunalnik.ToString();
                    disableButtons();
                    return;
                }
                if (_Igra.GameEnd() == "gameFull")
                {
                    stIzenacenj++;
                    izenaceno.Text = stIzenacenj.ToString();
                    disableButtons();//nepotrebno
                    return;
                }
            }
            else
            {
                ((Button)sender).Content = _Igra.min_char;
                _Igra.PosodobiPolje(x - 48, y - 48, _Igra.min_char);

                //preverimo ce je konec
                if (_Igra.GameEnd() == "O")
                {
                    stZmagClovek++;
                    igralec.Text = stZmagClovek.ToString();
                    disableButtons();
                    return;
                }
                if (_Igra.GameEnd() == "gameFull")
                {
                    stIzenacenj++;
                    izenaceno.Text = stIzenacenj.ToString();
                    disableButtons();//nepotrebno
                    return;
                }
                int[] temp = najboljsaPotezaMax(_Igra.Polje);
                _Igra.PosodobiPolje(temp[0], temp[1], _Igra.max_char);
                foreach (var polje in buttoni.Children)//verjetno obstaja boljsa funkcija za to
                {
                    if (((Button)polje).Tag.ToString() == $"{temp[0]},{temp[1]}")
                    {
                        ((Button)polje).Content = _Igra.max_char;
                        break;
                    }
                }

                if (_Igra.GameEnd() == "X")
                {
                    stZmagRacunalnik++;
                    racunalnik.Text = stZmagRacunalnik.ToString();
                    disableButtons();
                    return;
                }
                if (_Igra.GameEnd() == "gameFull")
                {
                    stIzenacenj++;
                    izenaceno.Text = stIzenacenj.ToString();
                    disableButtons();//nepotrebno
                    return;
                }
            }
            return;
        }

    }

}
