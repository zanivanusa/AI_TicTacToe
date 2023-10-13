using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_naloga2_minmax
{
    internal class MinMax
    {
        Igra igra = new Igra();

        public int minMax(string[,] polje, int globina, bool maksimiziramo)
        {
            igra.Polje = polje;
            if (globina == 0 || igra.GameEnd() != "ongoing") return igra.Tocke(polje);

            if (maksimiziramo)
            {
                var najboljsaPoteza = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrWhiteSpace(polje[i, j]))
                        {
                            polje[i, j] = igra.max_char;
                            najboljsaPoteza = Math.Max(najboljsaPoteza, minMax(polje, globina - 1, false));//maks bo sedaj false
                            polje[i, j] = "";
                        }
                    }
                }
                return najboljsaPoteza;
            }
            else//minimiziramo
            {
                var najboljsaPoteza = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrWhiteSpace(polje[i, j]))
                        {
                            polje[i, j] = igra.min_char;
                            najboljsaPoteza = Math.Min(najboljsaPoteza, minMax(polje, globina - 1, true));
                            polje[i, j] = "";
                        }
                    }
                }
                return najboljsaPoteza;
            }
        }

        /// <summary>
        /// alfa int.min beta pa int.max
        /// </summary>
        /// <param name="polje"></param>
        /// <param name="globina"></param>
        /// <param name="maksimiziramo"></param>
        /// <param name="alfa"></param>
        /// <param name="beta"></param>
        /// <returns></returns>
        public int alfaBeta(string[,] polje, int globina, bool maksimiziramo, int alfa, int beta)
        {
            igra.Polje = polje;
            if (globina == 0 || igra.GameEnd() != "ongoing") return igra.Tocke(polje);

            int maxScore = alfa;
            if (maksimiziramo)
            {
                var najboljsaPoteza = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrWhiteSpace(polje[i, j]))
                        {
                            polje[i, j] = igra.max_char;
                            najboljsaPoteza = Math.Max(najboljsaPoteza, alfaBeta(polje, globina - 1, false, alfa, beta));//maks bo sedaj false
                            polje[i, j] = "";
                            if (najboljsaPoteza > alfa) //nastavimo najboljso vrednost
                            {
                                alfa = najboljsaPoteza;
                            }
                                if(alfa >= beta)
                                {
                                    return alfa;
                                }
                            
                        }
                    }
                }
                return najboljsaPoteza;
            }
            else//minimiziramo
            {
                var najboljsaPoteza = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrWhiteSpace(polje[i, j]))
                        {
                            polje[i, j] = igra.min_char;
                            najboljsaPoteza = Math.Min(najboljsaPoteza, alfaBeta(polje, globina - 1, true, alfa, beta));
                            polje[i, j] = "";
                            if (najboljsaPoteza < beta) //nastavimo najboljso vrednost
                            {
                                beta = najboljsaPoteza;
                            }
                            if (alfa >= beta)
                            {
                                return beta;
                            }
                        }
                    }
                }
                return najboljsaPoteza;
            }
        }

    }
}
