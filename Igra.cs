using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_naloga2_minmax
{
    internal class Igra
    {
        public bool zacelAI = false;
        public string[,] Polje = new string[3, 3];//naso 3x3 polje     
        public string min_char = "O";
        public string max_char = "X";
        public bool PoljeZapolnjeno = false;
        public string GameEnd()
        {
            //preverimo ce je dosezeno zmagovalno stanje
            for (var i = 0; i < 3; i++)//preverimo vse vrstice
            {
                if (!string.IsNullOrWhiteSpace(Polje[i, 1]))
                {

                    if (Polje[i, 0] == Polje[i, 1] && Polje[i, 1] == Polje[i, 2])
                    {
                        return Polje[i, 1];
                    }
                }

            }
            for (var i = 0; i < 3; i++)//preverimo vse stolpce
            {
                if (!string.IsNullOrWhiteSpace(Polje[1, i]))
                {


                    if (Polje[0, i] == Polje[1, i] && Polje[1, i] == Polje[2, i])
                    {
                        return Polje[1, i];
                    }
                }

            }
            //in se diagonalno
            if (Polje[0, 0] == Polje[1, 1] && Polje[1, 1] == Polje[2, 2])
            {
                if (!string.IsNullOrWhiteSpace(Polje[1, 1]))
                {
                    return Polje[1, 1];
                }
            }
            if (Polje[0, 2] == Polje[1, 1] && Polje[1, 1] == Polje[2, 0])
            {
                if (!string.IsNullOrWhiteSpace(Polje[1, 1]))
                {
                    return Polje[1, 1];
                }
            }
            if (GameFull())
            {
                return "gameFull";
            }

            return "ongoing";

        }
        public bool GameFull()
        {
            int fillCounter = 0;
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!string.IsNullOrEmpty(Polje[i, j])){
                        fillCounter++;
                    }
                }
            }            
            if (fillCounter == 9)
            {               
                return true;
            }
            return false;
        }       
        public int Tocke(string[,] polje)
        {
            int vrednost = 0;
            for (var i = 0; i < 3; i++)//preverimo vse vrstice
            {
                if (polje[i, 0] == polje[i, 1] && polje[i, 1] == polje[i, 2])
                {
                    if (polje[i, 0] == max_char)
                    {
                        vrednost += 100;
                    }
                    if (polje[i, 0] == min_char)
                    {
                        vrednost -= 100;
                    }
                }

            }
            for (var i = 0; i < 3; i++)//preverimo vse stolpce
            {
                if (polje[0, i] == polje[1, i] && polje[1, i] == polje[2, i])
                {
                    if (polje[0, i] == max_char)
                    {
                        vrednost += 100;
                    }
                    if (polje[0, i] == min_char)
                    {
                        vrednost -= 100;
                    }
                }

            }
            //in se diagonalno
            if (polje[0, 0] == polje[1, 1] && polje[1, 1] == polje[2, 2])
            {
                if (polje[1, 1] == max_char)
                {
                    return 100;
                }
                if (polje[1, 1] == min_char)
                {
                    return -100;
                }
            }
            if (polje[0, 2] == polje[1, 1] && polje[1, 1] == polje[2, 0])
            {
                if (polje[1, 1] == max_char)
                {
                    vrednost += 100;
                }
                if (polje[1, 1] == min_char)
                {
                    vrednost -= 100;
                }
            }
            //preverimo po dva skup            
            for (var i = 0; i < 3; i++)//preverimo vse vrstice
            {
                if (!String.IsNullOrEmpty(Polje[i, 0]))// ce prvi ni zapolnjen sigurno nebo v tej vrstici 3
                {
                    if (polje[i, 0] == polje[i, 1] || polje[i, 1] == polje[i, 2])
                    {
                        if (polje[i, 0] == max_char)
                        {
                            vrednost += 10;
                        }
                        if (polje[i, 0] == min_char)
                        {
                            vrednost -= -10;
                        }
                    }
                }
            }
            for (var i = 0; i < 3; i++)//preverimo vse stolpce
            {
                if (!String.IsNullOrWhiteSpace(Polje[0, i]))
                {
                    if (polje[0, i] == polje[1, i] || polje[1, i] == polje[2, i])
                    {
                        if (polje[0, i] == max_char)
                        {
                            vrednost += 10;
                        }
                        if (polje[0, i] == min_char   )
                        {
                            vrednost -= 10;
                        }
                    }
                }
            }
            if (polje[0, 0] == polje[1, 1] || polje[1, 1] == polje[2, 2])
            {
                if (polje[1, 1] == max_char)
                {
                    vrednost += 10;
                }
                if (polje[1, 1] == min_char)
                {
                    vrednost -= -10;
                }
            }
            if (polje[0, 2] == polje[1, 1] || polje[1, 1] == polje[2, 0])
            {
                if (polje[1, 1] == max_char)
                {
                    vrednost += 10;
                }
                if (polje[1, 1] == min_char)
                {
                    vrednost -= -10;
                }
            }
            //preverimo za posamezne
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(polje[i, j]))
                    {
                        if(zacelAI == true)
                        {
                            if(polje[i,j] == max_char)
                            {
                                vrednost += 1;
                            }
                            else { vrednost -= -1; }
                        }
                        else
                        {
                            if (polje[i, j] == min_char)
                            {
                                vrednost -= -1;
                            }
                            else { vrednost += 1; }
                        }                       
                    }
                }
            }            
            if(GameEnd() == "X")
            {
                return 1000;
            }
            if(GameEnd() == "O")
            {
                return -1000;
            }
            if (GameEnd() == "gameFull")//igra polna
            {
                return 0;
            }


            return vrednost;
        }
        public void PosodobiPolje(int pozicijax, int pozicijay, string vrednost)
        {
            Polje[pozicijax, pozicijay] = vrednost;
        }
       
    }
}
