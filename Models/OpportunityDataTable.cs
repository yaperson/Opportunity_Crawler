using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace WebOpportunityCrowler
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

				datatable.Rows.Add(row);
			}
			
			datatable.AcceptChanges();

			string routeDataTable = "D:/Projet pro/stage/2022/ASP-NET-TEST/WebOpportunityCrowler/WebOpportunityCrowler/App_Data/opportunitiesDataTable.xml";
			
			DataSet opportunitiesDataTable = new DataSet();

			opportunitiesDataTable.ReadXml(routeDataTable);
			opportunitiesDataTable.DataSetName = "opportunitiesDataTable";
			opportunitiesDataTable.Merge(datatable);

			//opportunitiesDataTable.WriteXml(routeDataTable);
			
		}
		public object getOpportunityData(DataTable table)
        {
			var contactRow = from row in table.Rows.Cast<DataRow>()
							 select new Opportunity
							 {
								title = row.Field<string>(1),
								date = row.Field<string>(2),
								url = row.Field<string>(3),
								description = row.Field<string>(4),
								location = row.Field<string>(5),
								company = row.Field<string>(6),
								rate = row.Field<string>(7),
							 };

			return contactRow;
		}
	}
}