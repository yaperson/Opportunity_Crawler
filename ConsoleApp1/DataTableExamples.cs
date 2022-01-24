using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class DataTableExamples
    {
        public DataTable CreateDataTableFromScratch()
        {
            // tout se trouve dans System.Data.
            var datatable = new DataTable();

            // Création des colonnes
            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "id",
                DataType = typeof(Guid), // Je préfère mettre des guid (cherche sur internet)
                AllowDBNull = false
            });

            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "firstname",
                DataType = typeof(string),
                AllowDBNull = false
            });

            datatable.Columns.Add(new DataColumn()
            {
                ColumnName = "lastname",
                DataType = typeof(string),
                AllowDBNull = false
            });

            // Il est possible d'enregistrer 
            // datatable.WriteXml("chemin"); = juste les données
            // datatable.WriteXmlSchema("chemin"); = les données + schéma (le schéma étant ce qu'il y a ci-dessus)
            // dans le cas actuel on a unn schéma mais pas encore de données.
            // On peut recharger les données avec les méthodes ReadXml et ReadXmlSchema

            return datatable;
        }

        public void AddDataToDataTable(DataTable datatable)
        {
            var row1 = datatable.NewRow();
            row1["id"] = Guid.NewGuid();
            row1["firstname"] = "Pierre";
            row1["lastname"] = "Affeux";
            datatable.Rows.Add(row1);

            var row2 = datatable.NewRow();
            row2["id"] = Guid.NewGuid();
            row2["firstname"] = "Pierre";
            row2["lastname"] = "Affeux";
            datatable.Rows.Add(row2);

            // Maitenant on pourrait enregistrer notre datatable
            // pour sauvegarder les données
        }

        public void FindSomethingInDataTable()
        {
            var datatable = CreateDataTableFromScratch();
            AddDataToDataTable(datatable);

            var contactRow = (from row in datatable.Rows.Cast<DataRow>()
                              where row.Field<string>("firstname") == "Pierre"
                              select row);

            // Tu as aussi tout ce qu'il faut pour supprimer.
            // Attention lors de tes recherches sur Internet.
            // DataTable peut être utilisée comme ci-dessus, mais
            // aussi pour travailler avec des BDD. Les principes sont
            // les mêmes mais c'est la BDD qui remplie le schéma et les données
            // donc attention à ne pas te laisser détourner par des exemples
            // de code non appropriés.
        }
    }
}
