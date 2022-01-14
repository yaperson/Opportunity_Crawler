using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp1
{
    public class Opportunity
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string detailDescription { get; set; }
        public string url { get; set; }
        public string date { get; set; }
        public string location { get; set; }
        public string tarifs { get; set; }

        //-----------------
        // j'ai remarqué que chaque anonce a déja son id indiqué dans la bare du navigateur 
        // exemple : dev-fullstack-java-angular-1657124
        // l'annonce d'apres : chef-de-projets-senior-moe-industrialisation-1657123
        // Nous avons donc [Nom-annonce]-[id]
    }
}
