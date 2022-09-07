using System;

namespace Laivanupotus2._0
{
    internal class Program
    {
        static Random rnd = new Random();
        //tehdään ruudukko
        static int[] LaivaKoot = { 2, 2, 4, 5 };
        public static int[,] Ruudukko = new int[10, 10] { { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 } };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public static void TaulukkoPaivitys()
        {    //tehdään ylä numerorivi
            Console.Write("# ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(" " + Convert.ToString(i));
            }
            Console.WriteLine();
            Console.WriteLine();
            //aletaan luomaan peliaslustaa
            int RuutuRivi = 0;

            for (int i = 0; i < 10; i++)
            {   //kirjoitetaan numero jokaisen rivin eteen
                Console.Write(Convert.ToString(i) + " ");
                for (int j = 0; j < 10; j++)
                {
                    //Tarkistetaan onko kohtaan osuttu ja onko siinä laivaa, johon on osuttu
                    if (Ruudukko[j, RuutuRivi] == 10)
                    {
                        Console.Write(" O");
                    }
                    else if (Ruudukko[j, RuutuRivi] % 2 == 0 && (Ruudukko[j, RuutuRivi] != 10))
                    {
                        Console.Write(" #");
                    }
                    else if (Ruudukko[j, RuutuRivi] % 2 != 0)
                    {
                        Console.Write(" ¤");
                    }

                }
                Console.WriteLine();
                RuutuRivi++;
            }
        }

        static void RandomLaivat()
        {
            int OsumatonLaiva = 1;

            for (int i = 0; i < 4; i++)
            {   //arvotaan luvut
                int Randomx = rnd.Next(0, 9);
                int Randomy = rnd.Next(0, 9);
                int xvaiy = rnd.Next(0, 1);
                //Tarkistetaan onko ruudukon kohta tyhja
                if ((Ruudukko[Randomx, Randomy] == 10))
                {
                    //aletaan tarkistaa että onko loppukin veneen tila tyhjaa
                    if (xvaiy == 0)
                    {
                        for (int j = 0; j < LaivaKoot[i]; j++)
                        {
                            if ((Ruudukko[Randomx, (Randomy + 1)] == 10))
                            {
                                continue;
                            }
                            else if (Ruudukko[Randomx, (Randomy + 1)] != 10 || Ruudukko[Randomx, (Randomy + 1)] == null)
                            {
                                i--;
                                break;
                            }
                        }
                    }
                    else if (xvaiy == 1)
                    {
                        for (int j = 0; j < LaivaKoot[i]; j++)
                        {
                            if ((Ruudukko[(Randomx + 1), Randomy] == 10))
                            {
                                continue;
                            }
                            else if ((Ruudukko[(Randomx + 1), Randomy] != 10) || Ruudukko[Randomx, (Randomy + 1)] == null)
                            {
                                i--;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //miinustetaan 1 jotta ohjelma alottaisi samasta kohtaa alusta
                        i--;
                    }
                }
            }
        }
    }
}
