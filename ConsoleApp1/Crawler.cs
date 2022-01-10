using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Crawler
    {
        public List<Opportunity> GetOportunity(string url)
        {
            List<Opportunity> opportunities = new List<Opportunity>();

            HtmlWeb web = new HtmlWeb();

            var doc = web.Load(url);


            // var nodes = doc.DocumentNode.SelectNodes("/html/body/div[2]/main/div/div/div[1]/div[7]");
            // MLB : Voici ce que je te propose à la place. Si tu effectue des recherches sur XPath tu verra
            // qu'il est possible d'être très précis dans ce que tu recherches dans le DOM.
            var nodes = doc.DocumentNode.SelectNodes("//a[starts-with(@href,'/mission/')]");
            if (nodes == null) throw new Exception("Aucun noeud ne correspond à la recherche");

            var nextPage = doc.DocumentNode.SelectNodes("//*[@id='left - col']/nav/ul/li[2]/a");


            // Si après tu remonte de deux noeud, voici la structure HTML que l'on obtient avec toutes les infos nécessaires
            // qu'il ne reste plus qu'a targeter:
            //<div class="col-9 pb-3 pt-3">
            //    <div id="titre-mission">
            //        <a class="rtitre filter-link" href="/mission/developpeur-java-reactjs-activepivot-h-f-1657224">Développeur Java ReactJs &amp; ActivePivot (H/F)
            //        </a>
            //    </div>
            //    <span class="textvert9">Paris</span>
            //    <span class="textgrisfonce9">06/01/2022</span><br>
            //    <div class="text-justify">
            //        Contexte :Dans le cadre d'un renfort, nous recherchons un(e) Développeur Java ReactJs &amp; ActivePivot (H/F)Projet:Votre intervenez sur un projet à forte valeur ajoutée dans le secteur bancaire ; Poste et missions: L'équipe travaille sur des applications intranet en technologie Java pour la partie ...
            //        <a class="text-underline" href="/mission/developpeur-java-reactjs-activepivot-h-f-1657224">Voir plus</a>
            //    </div>
            //    <div class="rlig_det">
            //        <span>Non lu</span>
            //        <span> | 3 années | 500-550 €</span>
            //    </div>
            //</div>    
               
            
            var old = "!";

            foreach (var node in nodes) 
            {
                var opportunityUrl = "https://www.freelance-info.fr" + node.GetAttributeValue("href", "");
                if (opportunityUrl == old) continue;
                old = opportunityUrl;
                
                var oportunityNode = node.ParentNode?.ParentNode;
                if (oportunityNode == null) throw new Exception("Impossible de remonter vers l'opportunité.");

                var opportunityTitle = "";
                if (oportunityNode.SelectSingleNode("div[@id='titre-mission']") is null) {
                    opportunityTitle = "";
                } else opportunityTitle = oportunityNode.SelectSingleNode("div[@id='titre-mission']").InnerText;

                var opportunityLocation = oportunityNode.SelectSingleNode("span[@class='textvert9']")?.InnerText;
                var opportunityDate = oportunityNode.SelectSingleNode("span[@class='textgrisfonce9']")?.InnerText;
                var opportunityDescription = oportunityNode.SelectSingleNode("//*[@id='offre']/div/div[1]/div[2]")?.InnerText;

                // Je te laisse chercher pour isoler les   
                var opportunity = new Opportunity
                {
                    title = opportunityTitle,
                    description = opportunityDescription,
                    date = opportunityDate,
                    location = opportunityLocation,
                    url = opportunityUrl,
                };
                opportunities.Add(opportunity);


                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.WriteLine(node.InnerHtml);
                Console.WriteLine(opportunityLocation + " " + opportunityDate + " " + opportunityDescription + " " + opportunityTitle + " " + opportunityUrl);

            }
            return opportunities;
        } 
    }
}
