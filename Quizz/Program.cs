using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quizz
{
    class Program
    {
        // testi kommentti
            //tiedoston luku: randomoi kymmenen riviä kysymyksiä
            //tallenna randomoidut kysymykset List<List<string>> tyyppiin
            //looppaa ulomman listan läpi (kokonaiset kysymysrivit)
                //splittaa kysymysrivi puolipisteen kanssa-->palauttaa string[]
                //looppaa sisemmän listan läpi (kysymysrivi jaettuna kysymykseen, vaihtoehtoihin ja vastaukseen
                //printtaa kysymys (indexOf(0)
                //printtaa vaihtoehdot (indexOf(1-3))
                //pyydä input
                //if input==vastaus
                    //score++

            //printtaa kokonaispisteet

        public static List<string> kysymyslista = new List<string>(); //tässä listassa on 10 tiedostosta randomoitua kysymystä vaihtoehtoineen ja vastauksineen
        public static string[] randomoidutKysmykset;
        public static List<string[]> splitattujenLista = new List<string[]>(); //tässä listassa on määrätty määrä kysymyksiä joita käytetään pelaamiseen
        public static int playerScore=0;
        public static string nimi;
        static void Main(string[] args)
        {
            RandomoiKysymykset();
            SplittaaKysymyksenElementit();
            Pelaa();
            EndGame();

        }

        private static void Pelaa()
        {
            Console.WriteLine("Tervetuloa pelaamaan lätkävisaa!");
            Console.WriteLine("Mikä on nimesi?");
            nimi = Console.ReadLine();
            int kysymysCounter = 0; //tähän päivitetään kysyttyjen kysymysten määrä
            do
            {
                foreach (string[] kysymys in splitattujenLista)
                {
                    Console.Clear();
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine(kysymys[i]);
                    }
                    Console.WriteLine("Anna vastaus");
                    string vastaus = Console.ReadLine();

                    if (vastaus.ToString().Trim().ToLower() == kysymys[4].ToString().Trim() .ToLower())
                    {
                        Console.WriteLine("Oikea vastaus!");
                        playerScore++;
                    }
                    else
                    {
                        Console.WriteLine("Väärä vastaus. Oikea vastaus oli: " + kysymys[4].ToString() + ".") ;
                    }
                    Console.WriteLine("Paina enter jatkaaksesi.");
                    Console.ReadLine();
                    Console.Clear();

                kysymysCounter++;
                    
                }
            } while (kysymysCounter < splitattujenLista.Count);
        }

        private static void SplittaaKysymyksenElementit()
        {
            for (int i = 0; i < kysymyslista.Count; i++)
            {
                string[] splitattu = kysymyslista.ElementAt(i).Split(';');
                splitattujenLista.Add(splitattu);
            }
        }

        private static void RandomoiKysymykset()
        {
            string[] kysymysrivit = File.ReadAllLines("lätkävisa.txt");

            Random rand = new Random();
            randomoidutKysmykset = kysymysrivit.OrderBy(x => rand.Next()).ToArray();


            for (int i = 0; i < 10; i++)
            {
                kysymyslista.Add(randomoidutKysmykset[i]);
            }

        }
        private static void EndGame()       
        {
            string palaute = "";
            if (playerScore  == 10)
            { palaute = "Paita kattoon! Olet Wayne Gretzky!"; }
            if (playerScore < 10 && playerScore>5)
            { palaute = "Ny rillataan! Olet Timo Jutila!"; }
            if (playerScore < 5 && playerScore > 0)
            { palaute = "Kaksi minuuttia, pelin viivyttäminen! Olet Jarkko Ruutu!"; }
            if (playerScore == 0)
            { palaute = "Parempi siirtyä muihin bisneksiin. Olet Ville Leino..."; }
            Console.WriteLine($"Peli loppui.");
            Console.WriteLine($"{nimi}, tuloksesi oli {playerScore}.");
            Console.WriteLine(palaute);
        }
    }
}
