using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebOpportunityCrowler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var opportunityCrowler = new OpportunityCrawler();

            string url = string.Format("https://www.freelance-info.fr/missions?remote=1&page=");
            int tokenUrl = 1;
            var opportunities = opportunityCrowler.GetOpportunityListFromFreelanceInfoWebSite(url, tokenUrl);

            var datatable = new OpportunityDataTable();
            var table = datatable.CreateOpportunityDataTable();
            datatable.AddNewOpportunitiesRows(table, opportunities);

            return View();
        }
    }
}
    