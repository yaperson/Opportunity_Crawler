using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Crowler
    {
        public List<Opportunity> GetOportunity(string url)
        {
            var uri = new Uri(url);
            
            //var servicePoint = System.Net.ServicePointManager.FindServicePoint(uri);
            //servicePoint.ConnectionLimit = 40;
            
            List<Opportunity> opportunities = new List<Opportunity>();

            HtmlWeb web = new HtmlWeb();

            var doc = web.Load(url);

            // var nodes = doc.DocumentNode.SelectNodes("/html/body/div[2]/main/div/div/div[1]/div[7]");
            // MLB : Voici ce que je te propose à la place. Si tu effectue des recherches sur XPath tu verra
            // qu'il est possible d'être très précis dans ce que tu recherches dans le DOM.
            var nodes = doc.DocumentNode.SelectNodes("//a[starts-with(@href,'/mission/')]");
            if (nodes == null) throw new Exception("Aucun noeud ne correspond à la recherche");

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

            foreach (var node in nodes)
            {
                var opportunityUrl = node.GetAttributeValue("href", "");

                var oportunityNode = node.ParentNode?.ParentNode;
                if (oportunityNode == null) throw new Exception("Impossible de remonter vers l'opportunité.");
                
                var opportunityLocation = oportunityNode.SelectSingleNode("span[@class='textvert9']")?.InnerText;
                var opportunityDate = oportunityNode.SelectSingleNode("span[@class='textgrisfonce9']")?.InnerText;

                // Je te laisse chercher pour isoler les 

                var opportunity = new Opportunity
                {
                    Opportunity_title = node.SelectSingleNode("/div[1]/div/div[1]/div[1]").InnerText,
                    Opportunity_description = node.SelectSingleNode("/div[1]/div/div[1]/div[2]").InnerText,
                    //Opportunity_date = int.Parse(node.SelectSingleNode("/div[1]/div/div[1]/span[2]").InnerText),
                };
                opportunities.Add(opportunity);
            }
            //url = uri ;
            return opportunities;
        }   
    }
}
