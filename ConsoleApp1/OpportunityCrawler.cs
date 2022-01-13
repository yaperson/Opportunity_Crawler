using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class OpportunityCrawler
    {
        public List<Opportunity> GetOpportunity(string url, int tokenUrl)
        {
            List<Opportunity> opportunities = new List<Opportunity>();

            HtmlWeb web = new HtmlWeb();

            url = url + tokenUrl;

            var doc = web.Load(url);

            var nodes = doc.DocumentNode.SelectNodes("//a[starts-with(@href,'/mission/')]");
            if (nodes == null) throw new Exception("Aucun noeud ne correspond à la recherche");

            DateTime newDate = DateTime.Today;
            Console.WriteLine(newDate);

            var oldUrl = "!";

            foreach (var node in nodes)
            {
                var opportunityUrl = "https://www.freelance-info.fr" + node.GetAttributeValue("href", "");
                if (opportunityUrl == oldUrl) continue;
                oldUrl = opportunityUrl;

                takeDetailOpportunity(opportunityUrl);

                var opportunityNode = node.ParentNode?.ParentNode;
                if (opportunityNode == null) throw new Exception("Impossible de remonter vers l'opportunité.");

                var idIndex = opportunityUrl.LastIndexOf('-');
                string opportunityId = opportunityUrl.Substring(idIndex, 7);
                opportunityId = opportunityId.Substring(1, 6);

                var opportunityStatus = opportunityNode.SelectSingleNode("div[@class='rlig_det']/span[1]")?.InnerText;
                var opportunityStatusRead = "Lu";
                if (opportunityStatus == opportunityStatusRead) break;

                var opportunityDate = opportunityNode.SelectSingleNode("span[@class='textgrisfonce9']")?.InnerText;
                var opportunityParsedDate = DateTime.Parse(opportunityDate);
                int compareDate = (opportunityParsedDate - newDate).Days;
                if (compareDate > 1) break;
                
                
                var opportunityTitle = opportunityNode.SelectSingleNode("div[@id='titre-mission']")?.InnerText;
                var opportunityLocation = opportunityNode.SelectSingleNode("span[@class='textvert9']")?.InnerText;
                var opportunityDescription = opportunityNode.SelectSingleNode("//*[@id='offre']/div/div[1]/div[2]")?.InnerText;
                var opportunityTarifs = opportunityNode.SelectSingleNode("div[@class='rlig_det']/span[2]")?.InnerText;


                    var opportunity = new Opportunity
                    {
                        id = opportunityId,
                        title = opportunityTitle,
                        description = opportunityDescription,
                        date = opportunityDate,
                        location = opportunityLocation,
                        tarifs = opportunityTarifs,
                        url = opportunityUrl,
                    };
                    opportunities.Add(opportunity);


                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine(" TITRE =  " + opportunityTitle + opportunityLocation + " " + opportunityDate + " DUREE / TARIFS = " + opportunityTarifs + " ID = " + opportunityId);
                    Console.WriteLine(" DESCRIPTION = " + opportunityDescription);
                    Console.WriteLine(" URL = " + opportunityUrl);
            }

            // Petite sécuritée car ça m'ai arrivé d'oublier de stopper le programme...
            if (tokenUrl < 20) loadNextPage(tokenUrl);
            else
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("+---------------------------+");
                Console.WriteLine("|       "+ tokenUrl + " pages scanés      |");
                Console.WriteLine("+---------------------------+");
                Console.WriteLine("");
                Console.WriteLine("");

            }

            return opportunities;
        } 
        public void takeDetailOpportunity(string Url)
        {
            Console.WriteLine("");
            Console.WriteLine("======================================START takeDetailOpportunity======================================");
            Console.WriteLine("||                                                                                                   ||");
            Console.WriteLine("||  Inner takeDetailOpportunity "+Url+ " link is good !!");
            Console.WriteLine("||                                                                                                   ||");
            Console.WriteLine("======================================START takeDetailOpportunity======================================");
            Console.WriteLine("");
            
            HtmlWeb web = new HtmlWeb();

            var doc = web.Load(Url);

            var nodes = doc.DocumentNode.SelectNodes("//div[@id='left-col']");
            if (nodes == null) throw new Exception("Aucun noeud ne correspond à la recherche");

            foreach (var node in nodes)
            {
                var detailNode = node.ParentNode?.ParentNode;
                if (detailNode == null) throw new Exception("Impossible de remonter vers l'opportunité.");

                var detailDate = detailNode.SelectSingleNode("//div[@class='textnoir9']")?.InnerText;
                var detailAll = detailNode.SelectSingleNode("//div[@ class='row']/div[@ class='col-8 left']")?.InnerText;
                var indexTeletravail = detailAll.LastIndexOf('%');
                var detailTeletravail = detailAll.Substring(indexTeletravail, 3);

                var detailDescription = detailNode.SelectSingleNode("//div[@ class='textnoir9 mt-3']")?.InnerText;

                Console.WriteLine(detailDate + " " + detailTeletravail + " " + detailDescription);

            }

        }
        public void compareWord(string opportunityDescription, bool wordScan)
        {
            if (String.Compare(opportunityDescription, "client") == -1)
            {
                Console.WriteLine();
                wordScan = true;
            }
        }
        public void loadNextPage(int tokenUrl)
        {
            Console.WriteLine("[=====================================================================================]");
            Console.WriteLine("[--------------------------           NEXT PAGE          -----------------------------]");
            Console.WriteLine("[=====================================================================================]");
            tokenUrl = tokenUrl + 1;
            string url = "https://www.freelance-info.fr/missions?remote=1&page=";
            GetOpportunity(url, tokenUrl);
        }
    }
}