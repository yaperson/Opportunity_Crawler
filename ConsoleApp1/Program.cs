using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var scraper = new OpportunityCrawler(); // J'ai changer le nom de la classe au cas ou il y ai un autre crawler (comme ProfileCrawler par exemple)

            string url = string.Format("https://www.freelance-info.fr/missions?remote=1&page=1");
            var opportunities = scraper.GetOpportunity(url);
            
            Console.WriteLine(opportunities);
        }
    }
}
