using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var opportunityCrowler = new OpportunityCrawler(); // J'ai changer le nom de la classe au cas ou il y ai un autre crawler (comme ProfileCrawler par exemple)

            string url = string.Format("https://www.freelance-info.fr/missions?remote=1&page=");
            int tokenUrl = 1;
            var opportunities = opportunityCrowler.GetOpportunity(url, tokenUrl);

            var datatable = new OpportunityDataTable();
            var table = datatable.CreateOpportunityDataTable();
            //var instert = datatable.AddOpportunityRow(table, opportunities);


            Console.WriteLine(opportunities + " End of Program.cs - All is good");
        }
    }
}
