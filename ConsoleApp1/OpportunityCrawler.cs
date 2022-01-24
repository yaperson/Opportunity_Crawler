﻿using HtmlAgilityPack;
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


            //------

            // MLB: J'ai supprimé le package
            //SqlCeConnection sqlConnect = new SqlCeConnection(@"Data Source=D:\Projet pro\stage\2022\stage-crawler\dbOpportunity.sdf");


            //------

            while (tokenUrl < 3) // scanne le nombre -1 (si on met 3, ça crawl sur 2 pages)
            {
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


                    var takeDetail = takeDetailOpportunity(opportunityUrl);

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
                    int compareDate = (newDate - opportunityParsedDate).Days;
                    Console.WriteLine(compareDate);
                    if (compareDate > 1) break;


                    var opportunityTitle = opportunityNode.SelectSingleNode("div[@id='titre-mission']")?.InnerText;
                    var opportunityLocation = opportunityNode.SelectSingleNode("span[@class='textvert9']")?.InnerText;
                    // var opportunityDescription = opportunityNode.SelectSingleNode("//*[@id='offre']/div/div[1]/div[2]")?.InnerText;
                    var opportunityRate = opportunityNode.SelectSingleNode("div[@class='rlig_det']/span[2]")?.InnerText;


                    string[] sentences = takeDetail[1].Split(new char[] { '.', '?', '!' });
                    string[] wordsToMatch = { " " };

                    var sentenceQuery = from sentence in sentences
                                        let w = sentence.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },
                                                                StringSplitOptions.RemoveEmptyEntries)
                                        where w.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                        select sentence;

                    foreach (var str in sentenceQuery)
                    {
                        Console.WriteLine(str);
                        Console.WriteLine(sentenceQuery);

                        var opportunity = new Opportunity // opportunityId - opportunityTitle - opportunityDate - opportunityUrl - detailDescription - detailDate - detailAll
                        {
                            id = opportunityId,
                            title = opportunityTitle,
                            description = takeDetail[1],
                            company = takeDetail[2],
                            date = opportunityDate,
                            location = opportunityLocation,
                            rate = opportunityRate,
                            url = opportunityUrl,
                            //detailDescription = takeDetail[1],
                        };
                        opportunities.Add(opportunity);
                    }


                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine(" TITRE =  " + opportunityTitle + opportunityLocation + " " + opportunityDate + " DUREE / TARIFS = " + opportunityRate + " ID = " + opportunityId);
                    // Console.WriteLine(" DESCRIPTION = " + opportunityDescription);
                    Console.WriteLine(" URL = " + opportunityUrl);
                }
                //loadNextPage(tokenUrl);
                Console.WriteLine("[=====================================================================================]");
                Console.WriteLine("[--------------------------           NEXT PAGE          -----------------------------]");
                Console.WriteLine("[=====================================================================================]");
                tokenUrl = tokenUrl + 1;
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("+---------------------------+");
            Console.WriteLine("|       " + tokenUrl + " pages scanés      |");
            Console.WriteLine("+---------------------------+");
            Console.WriteLine("");
            Console.WriteLine("");

            return opportunities;
        }
        public string[] takeDetailOpportunity(string Url)
        {
            Console.WriteLine("");
            Console.WriteLine("======================================START takeDetailOpportunity======================================");
            Console.WriteLine("||                                                                                                   ||");
            Console.WriteLine("||  Inner takeDetailOpportunity " + Url + " link is good !!");
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

                var detailDateCompany = detailNode.SelectSingleNode("//div[@class='textnoir9']")?.InnerText;
                var detailAll = detailNode.SelectSingleNode("//div[@ class='row']/div[@ class='col-8 left']")?.InnerText;
                var indexCompany = detailDateCompany.LastIndexOf("par");
                var detailCompany = detailDateCompany.Substring(indexCompany);

                var detailDescription = detailNode.SelectSingleNode("//div[@ class='textnoir9 mt-3']")?.InnerText;

                detailOpportunity = new string[] { detailCompany, detailDescription, detailAll };

                Console.WriteLine(detailAll + " " + detailDescription + " " + detailCompany); // detailDescription - detailDate - detailAll

            }


            return detailOpportunity;
        }
    }
}