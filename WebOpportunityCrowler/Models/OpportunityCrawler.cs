using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; // a voir l'utilité
using System.Text.RegularExpressions;

namespace WebOpportunityCrowler
{
    public class OpportunityCrawler
    {
        public List<Opportunity> GetOpportunityListFromFreelanceInfoWebSite(string url, int tokenUrl)
        {
            List<Opportunity> opportunities = new List<Opportunity>();

            HtmlWeb web = new HtmlWeb();

            while (tokenUrl < 1) // scanne le nombre -1 (si on met 3, ça crawl sur 2 pages)
            {
                url = url + tokenUrl;

                var doc = web.Load(url);

                var nodes = doc.DocumentNode.SelectNodes("//a[starts-with(@href,'/mission/')]");
                if (nodes == null) throw new Exception("Aucun noeud ne correspond à la recherche");

                DateTime newDate = DateTime.Today;
                Console.WriteLine("Crawl du : "+newDate);

                var oldUrl = "!";

                foreach (var node in nodes)
                {
                    var opportunityUrl = "https://www.freelance-info.fr" + node.GetAttributeValue("href", "");
                    if (opportunityUrl == oldUrl) continue;
                    oldUrl = opportunityUrl;

                    var takeDetail = takeDetailOpportunity(opportunityUrl);

                    var opportunityNode = node.ParentNode?.ParentNode;
                    if (opportunityNode == null) throw new Exception("Impossible de remonter vers l'opportunité.");

                    var idIndex = opportunityUrl.LastIndexOf('-');
                    string opportunityId = opportunityUrl.Substring(idIndex, 7);
                    opportunityId = opportunityId.Substring(1, 6);

                    var opportunityStatus = opportunityNode.SelectSingleNode("div[@class='rlig_det']/span[1]")?.InnerText;
                    var opportunityStatusRead = "Lu";
                    if (opportunityStatus == opportunityStatusRead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("[ WARNING = Opportunités déjà lus ]");
                        Console.ForegroundColor = ConsoleColor.White;
                        //    break;
                    }

                    var opportunityDate = opportunityNode.SelectSingleNode("span[@class='textgrisfonce9']")?.InnerText;
                    var opportunityParsedDate = DateTime.Parse(opportunityDate);
                    int compareDate = (newDate - opportunityParsedDate).Days;
                    Console.WriteLine("Diférence de jours : "+compareDate);
                    if (compareDate > 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[ ERROR = écart de date trop important ] [ " + tokenUrl +" page(s) scané(s) ]");
                        Console.ForegroundColor = ConsoleColor.White;
                        //continue;
                    }

                    var opportunityTitle = opportunityNode.SelectSingleNode("div[@id='titre-mission']")?.InnerText;
                    var opportunityLocation = opportunityNode.SelectSingleNode("span[@class='textvert9']")?.InnerText;
                    var opportunityRate = opportunityNode.SelectSingleNode("div[@class='rlig_det']/span[2]")?.InnerText;


                    Regex regex = new Regex(@"(\s){2,}");
                    opportunityTitle = regex.Replace(opportunityTitle, "");
                    takeDetail[1] = regex.Replace(takeDetail[1], "");
                    takeDetail[0] = regex.Replace(takeDetail[0], "");


                    string[] sentences = takeDetail[1].Split(new char[] { });
                    string[] wordsArrayToMatch = {"Dev", "developpeur", "développeur"};
                    string[] opportunityTest = {};

                    foreach (string wordsToMatch in wordsArrayToMatch)
                    {
                        Console.WriteLine("index du tableau : " + wordsToMatch);

                        var sentenceQuery = from sentence in sentences
                                            where sentence.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                            select sentence;

                        foreach (var str in sentenceQuery)
                        {
                            Console.WriteLine(str);
                            Console.WriteLine(sentenceQuery);

                            int arrayIndex = Array.IndexOf(opportunityTest, opportunityId);
                            if (arrayIndex > -1) Console.Write("Ligne 114");
                            else
                            {
                                var opportunity = new Opportunity
                                {
                                    id = opportunityId,
                                    title = opportunityTitle,
                                    description = takeDetail[1],
                                    company = takeDetail[0],
                                    date = opportunityDate,
                                    location = opportunityLocation,
                                    rate = opportunityRate,
                                    url = opportunityUrl,
                                };
                                opportunityTest.Append(opportunityId);
                                opportunities.Add(opportunity);

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("[ INFO = l'opportunité suivante a été ajoutée : " + opportunityUrl + " ]");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                    }

                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine(" TITRE =  " + opportunityTitle + opportunityDate + " ID = " + opportunityId);
                    Console.WriteLine(" URL = " + opportunityUrl);
                }
                Console.WriteLine("[=====================================================================================]");
                Console.WriteLine("[--------------------------           NEXT PAGE          -----------------------------]");
                Console.WriteLine("[=====================================================================================]");
                tokenUrl = tokenUrl + 1;
            }

            Console.WriteLine("");
            Console.WriteLine("+---------------------------+");
            Console.WriteLine("|       " + tokenUrl + " pages scanés      |");
            Console.WriteLine("+---------------------------+");
            Console.WriteLine("");

            return opportunities;
        }
        public string[] takeDetailOpportunity(string Url)
        {
            Console.WriteLine("");
            Console.WriteLine("======================================START takeDetailOpportunity======================================");
            Console.WriteLine("||                                                                                                   ||");
            Console.WriteLine("||  Inner takeDetailOpportunity " + Url + " link is good");
            Console.WriteLine("||                                                                                                   ||");
            Console.WriteLine("======================================START takeDetailOpportunity======================================");
            Console.WriteLine("");

            HtmlWeb web = new HtmlWeb();

            var doc = web.Load(Url);

            var nodes = doc.DocumentNode.SelectNodes("//div[@id='left-col']");
            if (nodes == null) throw new Exception("Aucun noeud ne correspond à la recherche");

            string[] detailOpportunity = { };

            foreach (var node in nodes)
            {
                var detailNode = node.ParentNode?.ParentNode;
                if (detailNode == null) throw new Exception("Impossible de remonter vers l'opportunité.");

                var detailDateCompany = detailNode.SelectSingleNode("//div[@class='textnoir9']")?.InnerText; // Vérifier son utilitée
                var detailAll = detailNode.SelectSingleNode("//div[@ class='row']/div[@ class='col-8 left']")?.InnerText;
                var detailDescription = detailNode.SelectSingleNode("//div[@ class='textnoir9 mt-3']")?.InnerText;

                var indexCompany = detailDateCompany.LastIndexOf("par");
                var detailCompany = detailDateCompany.Substring(indexCompany);

                detailOpportunity = new string[] { detailCompany, detailDescription, detailAll };
            }
            return detailOpportunity;
        }
    }
}