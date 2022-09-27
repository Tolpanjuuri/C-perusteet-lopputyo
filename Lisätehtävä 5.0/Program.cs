using System;
using System.Xml.XPath;

namespace Lisätehtävä_5._0
{

    internal class Program
    { 
        static Random rnd = new Random();
        //tehdään ruudukko
        static int[] LaivaKoot = { 2, 2, 4, 5 };
        public static int[,] Ruudukko = new int[10, 10] {
        { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
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
            StarttiViestit();
            RandomLaivat();

            while (true)
            {
                TaulukkoPaivitys();
                KysyPelaajalta();
                if (TilanneTarkistus() == 1)
                {
                    Console.WriteLine("Onneksi olkoon voitit!");
                    break;
                }
            }

        }

        static void StarttiViestit()
        {
            Console.WriteLine("Hei, tervetuloa pelaamaan laivanupotusta!");
            Console.WriteLine("Säännöt lyhyesti:");
            Console.WriteLine("Peli arvoo laivat satunnaisiin paikkoihin laudalla");
            Console.WriteLine("Sinun pitää arvata laivojen paikat");
            Console.WriteLine("0 = Tutkimaton, # = Osuttu, ¤ = Ohi\n");
        }

        static int TilanneTarkistus()
        {
            int jatka = 1;
            //tarkistetaan ruutu kerrallaan onko siinä osumaton laiva
            foreach (int ruutu in Ruudukko)
                if (ruutu == 0 || ruutu == 1 || ruutu == 2)
                    jatka = 1;
                else if (ruutu == 3)
                {
                    jatka = 0;
                    break;
                }

            return jatka;
        }


        static void Piirto(int numero, int ruutuRivi)
        {
            //Tarkistetaan onko kohtaan osuttu ja onko siinä laivaa, johon on osuttu
            if (Ruudukko[numero, ruutuRivi] == 10)
            {
                Console.Write(" O");
            }
            else if (Ruudukko[numero, ruutuRivi] == 1)
            {
                Console.Write(" #");
            }
            else if (Ruudukko[numero, ruutuRivi] == 2)
            {
                Console.Write(" ¤");
            }
            else if (Ruudukko[numero, ruutuRivi] == 3)
            {
                Console.Write(" 0");
            }
        }

        public static void TaulukkoPaivitys()
        {    //tehdään ylä numero rivi
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
            {   //Tehdään sivu numero rivi
                Console.Write(Convert.ToString(i) + " ");

                //piirretään taulukko
                for (int j = 0; j < 10; j++)
                {
                    Piirto(j,RuutuRivi);
                }
                Console.WriteLine();
                RuutuRivi++;
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static void JoAmmuttu(int xCor, int yCor)
        {
            Console.WriteLine("Olet jo ampunut tahan yrita uudelleen\n\n");
            while (true)
            {   //yritetään uudestaan kunnes tulee ruutu johon ei ole kertaakaan ammuttu
                if (Ruudukko[xCor, yCor] == 2)
                {
                    Console.WriteLine("Anna x Koordinaatti: ");
                    xCor = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Anna y koordinaatti: ");
                    yCor = Convert.ToInt32(Console.ReadLine());
                }
                else
                {
                    break;
                }
            }
        }

        static void Osuttu(int xCor, int yCor)
        {
            Console.WriteLine("Osuit!");
            Console.WriteLine();
            Console.WriteLine();
            Ruudukko[xCor, yCor] = 1;
        }

        static void Ohi(int xCor, int yCor)
        {
            Console.WriteLine("Ohi!");
            Console.WriteLine();
            Console.WriteLine();
            Ruudukko[xCor, yCor] = 2;
        }

        static void KysyPelaajalta()
        {   //kysytään koordinaatit
            Console.WriteLine("Anna y Koordinaatti: ");
            int xCor = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Anna x koordinaatti: ");
            int yCor = Convert.ToInt32(Console.ReadLine());


            if (Ruudukko[xCor,yCor] == 2 || Ruudukko[xCor,yCor] == 1)
            {   //jos on jo kerran ammuttu samaan paikkaan
                    JoAmmuttu(xCor, yCor);
                
            }
            //jos osut
            else if (Ruudukko[xCor,yCor] == 3)
            {
                Osuttu(xCor, yCor);
            }   //jos et osu
            else if (Ruudukko[xCor, yCor] == 10)
            {
                Ohi(xCor, yCor);
            }

        }

        static bool PystyLaiva(int onnistunut,int laivaX, int laivaY, int osumatonLaiva, int numero)
        {
            for (int j = 1; j < LaivaKoot[numero]; j++)
            {   //tarkistetaan onko ruutua olemassa siinä sijainnissa
                if (laivaY + j < Ruudukko.GetLength(1))
                {
                    //tarkistetaan onko ruussa jo laivaa
                    if ((Ruudukko[laivaX, (laivaY + j)] == 10))
                    {
                        onnistunut = 1;
                        continue;

                    }
                }
                else
                {
                    return false;


                }
            }
            //piirretään laiva
            if (onnistunut == 1)
            {
                for (int j = 0; j < LaivaKoot[numero]; j++)
                {
                    Ruudukko[laivaX, (laivaY + j)] = osumatonLaiva;
                }
                return true;
            }
            return false;
        }

        static bool VaakaLaiva(int onnistunut, int laivaX, int laivaY, int OsumatonLaiva, int numero)
        {
            for (int j = 1; j < LaivaKoot[numero]+1; j++)
            {   //tarkistetaan onko ruutua olemassa siinä sijainnissa
                if (laivaX + j < Ruudukko.GetLength(1))
                {   //tarkistetaan onko ruudussa jo laivaa
                    if ((Ruudukko[(laivaX + j), laivaY] == 10))
                    {
                        onnistunut = 1;
                        continue;
                    }
                }
                else
                {
                    return false;

                }
            }
            //piirretään laiva
            if (onnistunut == 1)
            {
                for (int j = 0; j < LaivaKoot[numero]; j++)
                {
                    Ruudukko[(laivaX + j), laivaY] = OsumatonLaiva;
                    
                }
                return true;

            }
            return false;
        }

        static void RandomLaivat()
        {
            int OsumatonLaiva = 3;
            int Onnistunut = 0;

            for (int i = 0; i < 4;)
            {   //arvotaan Koordinaatit
                int laivaX = rnd.Next(1, 10);
                int laivaY = rnd.Next(0, 9);
                //Arvotaan kuumin päin laiva on
                int laivaSuunta = rnd.Next(1, 3);

                //Varmistetaan ettei mene ali
                if (i < 0)
                {
                    i = 0;
                }
                Onnistunut = 0;
                //Tarkistetaan onko ruudukon kohta tyhja
                if ((Ruudukko[laivaX, laivaY] == 10))
                {
                    //aletaan tarkistaa että onko loppukin veneen tila tyhjaa
                    //tarkistetaan kumpaan suuntaa vene osottaa
                    if (laivaSuunta == 1)
                    {   //tarkistetaan kuinka iso alus on
                       if (PystyLaiva(Onnistunut,laivaX,laivaY,OsumatonLaiva,i))
                        {
                            i++;
                        }
                    }
                    //tarkistetaan kumpaan suuntaa vene osottaa
                    else if (laivaSuunta == 2)
                    {   //tarkistetaan kuinka iso alus on
                        if (VaakaLaiva(Onnistunut, laivaX, laivaY, OsumatonLaiva, i))
                        {
                            i++;
                        }
                    }
                }
            }
    
        }
    }
}

