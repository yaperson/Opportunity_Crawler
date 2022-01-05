using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new FastWebScraper();
            var url = "https://yanis-projet.alwaysdata.net/veille.html";

            var cards = scraper.GetCards(url);

            Console.WriteLine(cards);
        }
    }
}
