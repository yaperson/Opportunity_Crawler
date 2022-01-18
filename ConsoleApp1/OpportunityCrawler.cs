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
                    compareWord(takeDetail[1]);


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
                        detailDescription = takeDetail[1],
                    };
                    opportunities.Add(opportunity);


                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine(" TITRE =  " + opportunityTitle + opportunityLocation + " " + opportunityDate + " DUREE / TARIFS = " + opportunityTarifs + " ID = " + opportunityId);
                    Console.WriteLine(" DESCRIPTION = " + opportunityDescription);
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

                var detailDate = detailNode.SelectSingleNode("//div[@class='textnoir9']")?.InnerText;
                var detailAll = detailNode.SelectSingleNode("//div[@ class='row']/div[@ class='col-8 left']")?.InnerText;
                var indexTeletravail = detailAll.LastIndexOf('%');
                var detailTeletravail = detailAll.Substring(indexTeletravail, 3);

                var detailDescription = detailNode.SelectSingleNode("//div[@ class='textnoir9 mt-3']")?.InnerText;

                detailOpportunity = new string[] { detailDate, detailDescription, detailTeletravail, detailAll };
            }


            return detailOpportunity;
        }
        public void compareWord (string detailAll)
        {
            Console.WriteLine("...bip boup bip...");
            Console.WriteLine("//     analyse du contenu de l'annonce      //");


            string[] sentences = detailAll.Split(new char[] { '.', '?', '!' });

            // Define the search terms. This list could also be dynamically populated at run time.  
            string[] wordsToMatch = { "développeur", "client" };
            
            // Find sentences that contain all the terms in the wordsToMatch array.  
            // Note that the number of terms to match is not specified at compile time.  
            var sentenceQuery = from sentence in sentences
                                let w = sentence.Split(new char[] {'.', '?', '!', ' ', ';', ':', ',' },
                                                        StringSplitOptions.RemoveEmptyEntries)
                                where w.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                select sentence;

            // Execute the query. Note that you can explicitly type  
            // the iteration variable here even though sentenceQuery  
            // was implicitly typed.
            foreach (var str in sentenceQuery)
            {
                Console.WriteLine(str);
                Console.WriteLine(sentenceQuery);
            }
            Console.WriteLine("DING ! c'est cuit !!");
        }
    }
}