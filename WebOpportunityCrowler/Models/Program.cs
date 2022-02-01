using System;

namespace WebOpportunityCrowler
{
    class Program
    {
        static void Main(string[] args)
        {
            var opportunityCrowler = new OpportunityCrawler();

            string url = string.Format("https://www.freelance-info.fr/missions?remote=1&page=");
            int tokenUrl = 1;
            var opportunities = opportunityCrowler.GetOpportunityListFromFreelanceInfoWebSite(url, tokenUrl);

            var datatable = new OpportunityDataTable();
            var table = datatable.CreateOpportunityDataTable();
            datatable.AddNewOpportunitiesRows(table, opportunities);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(opportunities + " End of Program.cs - All is good");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
