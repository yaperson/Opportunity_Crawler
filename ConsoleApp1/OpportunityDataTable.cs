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
				ColumnName = "opportunity_details",
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
		public void AddOpportunityRow(DataTable datatable, List<Opportunity> opportunities)
		{

			var row = datatable.NewRow();

			Opportunity[] opportunity = {};

			opportunities.CopyTo(opportunity);

			row["opportunity_id"] = Guid.NewGuid();
			row["opportunity_title"] = opportunity[1];
			row["opportunity_date"] = opportunity[2];
			row["opportunity_url"] = opportunity[3];
			row["opportunity_description"] = opportunity[4];
			row["opportunity_details"] = opportunity[5];
			row["opportunity_company"] = opportunity[6];
			row["opportunity_rate"] = opportunity[7];
			

			datatable.Rows.Add(row);

			// J'ai trouvé cette maniere de faire sur la doc Microsoft
			// https://docs.microsoft.com/fr-fr/dotnet/framework/data/adonet/dataset-datatable-dataview/adding-data-to-a-datatable
			// datatable.Rows.Add(opportunity); 
			
			datatable.AcceptChanges();
		}
		public void FindOpportunityData()
        {
			var datatable = CreateOpportunityDataTable();
			//AddOpportunityRow(datatable);
        }

	}
}
