using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Crowler
    {
        public List<Opportunity> GetOportunity(string url)
        {
            var uri = new Uri(url);
            List<Opportunity> opportunities = new List<Opportunity>();

            HtmlWeb web = new HtmlWeb();

            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("/html/body/div[2]/main/div/div/div[1]/div[7]");

            foreach (var node in nodes)
            {
                var opportunity = new Opportunity
                {
                    Opportunity_title = node.SelectSingleNode("/div[1]/div/div[1]/div[1]").InnerText,
                    Opportunity_description = node.SelectSingleNode("/div[1]/div/div[1]/div[2]").InnerText,
                    Opportunity_date = int.Parse(node.SelectSingleNode("/div[1]/div/div[1]/span[2]").InnerText),
                };
                opportunities.Add(opportunity);
            }
            //url = uri ;

            return opportunities;
        }   
    }
}
