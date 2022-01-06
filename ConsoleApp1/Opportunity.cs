using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Opportunity
    {
        public string Oportunity_title { get; set; }
        public string Opportunity_description { get; set; }
        public DateTime Opportunity_date { get; set; }

        // TODO
        // lien pour acceder a l'offre
        // Lieu / Durée / Tarif / Télétravail
        // descriptif complet
        // profil recheché
        //-----------------
        // j'ai remarqué que chaque anonce a déja son id indiqué dans la bare du navigateur 
        // exemple : dev-fullstack-java-angular-1657124
        // l'annonce d'apres : chef-de-projets-senior-moe-industrialisation-1657123
        // Nous avons donc [Nom-annonce]-[id]
}
