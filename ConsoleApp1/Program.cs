using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var opportunityCrowler = new OpportunityCrawler(); // J'ai changer le nom de la classe au cas ou il y ai un autre crawler (comme ProfileCrawler par exemple)

            string url = string.Format("https://www.freelance-info.fr/missions?remote=1&page=");
            int tockenUrl = 1;
            var opportunities = opportunityCrowler.GetOpportunity(url, tockenUrl);
            
            Console.WriteLine(opportunities);
        }
    }
}
