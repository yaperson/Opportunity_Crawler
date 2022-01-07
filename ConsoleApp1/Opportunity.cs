using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp1
{
    public class Opportunity
    {
        public string Opportunity_title { get; set; }
        public string Opportunity_description { get; set; }
        public string Opportunity_link { get; set; }
        public string Opportunity_date { get; set; }
        public string Opportunity_location { get; set; }

        // TODO
        // lien pour acceder a l'offre ** MAJ : Le lien est dans le titre, donc autant prendre le lien directement dedans
        // Lieu / Durée / Tarif / Télétravail
        // descriptif complet
        // profil recheché
        //-----------------
        // j'ai remarqué que chaque anonce a déja son id indiqué dans la bare du navigateur 
        // exemple : dev-fullstack-java-angular-1657124
        // l'annonce d'apres : chef-de-projets-senior-moe-industrialisation-1657123
        // Nous avons donc [Nom-annonce]-[id]
    }
}
