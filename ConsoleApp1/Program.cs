using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var scraper = new Crowler();

            // j'essaye une autre maniere pour passer l'url car vraisemblablement elle est transmise 
            // mais une erreur survient dans Crowler.cs Ligne 21 au niveau de Load(url)

            //var url = "https://www.freelance-info.fr/missions?remote=1&page=1";
            string url = string.Format("https://www.freelance-info.fr/missions?remote=1&page=1");
            var opportunities = scraper.GetOportunity(url);
            
            Console.WriteLine(opportunities);
        }
    }
}
