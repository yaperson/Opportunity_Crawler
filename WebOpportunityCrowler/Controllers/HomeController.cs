using System;
using System.Web.Mvc;

namespace WebOpportunityCrowler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "opportunities web crawler";

            var datatable = new OpportunityDataTable();
            var table = datatable.CreateOpportunityDataTable();

            var crawlDate = DateTime.MinValue;

            if (DateTime.Now != crawlDate) 
            {
                dayCrawl(table, datatable);
                crawlDate = DateTime.Today;
            }
            

            ViewBag.data = datatable.getOpportunityData(table);

            return View();
        }

        private void dayCrawl(System.Data.DataTable table, OpportunityDataTable datatable)
        {
            var opportunityCrowler = new OpportunityCrawler();

            string url = string.Format("https://www.freelance-info.fr/missions?remote=1&page=");
            int tokenUrl = 1;
            var opportunities = opportunityCrowler.GetOpportunityListFromFreelanceInfoWebSite(url, tokenUrl);

            datatable.AddNewOpportunitiesRows(table, opportunities);
        }
    }
}
    