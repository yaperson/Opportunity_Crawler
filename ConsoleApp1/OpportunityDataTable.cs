using System;
using System.Data;
using System.Linq;

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
				DataType = typeof(DateTime),
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
		public void AddOpportunityRow(DataTable datatable, OpportunityCrawler opportunity)
		{

			var row = datatable.NewRow();
			row["opportunity_id"] = Guid.NewGuid();
			row["opportunity_title"] = opportunity;
			row["opportunity_date"] = opportunity;
			row["opportunity_url"] = opportunity;
			row["opportunity_description"] = opportunity;
			row["opportunity_details"] = opportunity;
			row["opportunity_company"] = opportunity;
			row["opportunity_rate"] = opportunity;

			datatable.Rows.Add(row);

			// J'ai trouvé cette maniere de faire sur la doc Microsoft
			// https://docs.microsoft.com/fr-fr/dotnet/framework/data/adonet/dataset-datatable-dataview/adding-data-to-a-datatable
			datatable.Rows.Add(new OpportunityCrawler()); 
			
			datatable.AcceptChanges();
		}
		public void FindOpportunityData()
        {
			var datatable = CreateOpportunityDataTable();
			//AddOpportunityRow(datatable);
        }

	}
}
