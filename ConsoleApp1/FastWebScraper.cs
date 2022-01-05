using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class FastWebScraper
    {
        public List<Card> GetCards(string url)
        {
            var uri = new Uri(url);
            List<Card> cards = new List<Card>();

            HtmlWeb web = new HtmlWeb();

            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("/html/body/main/div[2]/div[1]");

            foreach (var node in nodes)
            {
                var card = new Card
                {
                    Card_title = node.SelectSingleNode("span[1]").InnerText,
                    Card_description = node.SelectSingleNode("p").InnerText,
                    Card_date = int.Parse(node.SelectSingleNode("span[3]").InnerText),
                };
                cards.Add(card);
            }
            //url = uri ;

            return cards;
        }   
    }
}
