using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new Crowler();
            var url = "https://www.freelance-info.fr/missions?remote=1&page=1";

            var opportunities = scraper.GetOportunity(url);
            
            Console.WriteLine(opportunities);
        }
    }
}
