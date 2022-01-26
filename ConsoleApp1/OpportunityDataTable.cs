using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
	public class OpportunityDataTable
	{
		public DataTable CreateOpportunityDataTable()
		{
			var datatable = new DataTable();

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_id",
				DataType = typeof(Guid),
				AllowDBNull = false
			});

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_title",
				DataType = typeof(string),
				AllowDBNull = false
			});

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_date",
				DataType = typeof(string),
				AllowDBNull = false
			});

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_url",
				DataType = typeof(string),
				AllowDBNull = false
			});

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_description",
				DataType = typeof(string),
				AllowDBNull = false
			});

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_location",
				DataType = typeof(string),
				AllowDBNull = false
			});

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_company",
				DataType = typeof(string),
				AllowDBNull = false
			});

			datatable.Columns.Add(new DataColumn()
			{
				ColumnName = "opportunity_rate",
				DataType = typeof(string),
				AllowDBNull = false
			});

			return datatable;
		}
		public void AddNewOpportunitiesRows(DataTable datatable, List<Opportunity> opportunities)
		{

			//Opportunity[] opportunities = {};
			//opportunities.CopyTo(opportunity);
			foreach (var opportunity in opportunities)
			{
				var row = datatable.NewRow();

				row["opportunity_id"] = Guid.NewGuid();
				row["opportunity_title"] = opportunity.title;
				row["opportunity_date"] = opportunity.date;
				row["opportunity_url"] = opportunity.url;
				row["opportunity_description"] = opportunity.description;
				row["opportunity_location"] = opportunity.location;
				row["opportunity_company"] = opportunity.company;
				row["opportunity_rate"] = opportunity.rate;

				Console.WriteLine(opportunity);
				datatable.Rows.Add(row);
			}
			// J'ai trouvé cette maniere de faire sur la doc Microsoft
			// https://docs.microsoft.com/fr-fr/dotnet/framework/data/adonet/dataset-datatable-dataview/adding-data-to-a-datatable
			// datatable.Rows.Add(opportunity); 
			
			datatable.AcceptChanges();
			Console.WriteLine(datatable.Rows);
		}
		public void FindOpportunityData()
        {
			var datatable = CreateOpportunityDataTable();
			//AddOpportunityRow(datatable);
        }

	}
}
