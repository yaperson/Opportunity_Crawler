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


			DataSet opportunitiesDataTable = new DataSet();
			opportunitiesDataTable.DataSetName = "opportunitiesDataTable";
			opportunitiesDataTable.Tables.Add(datatable);

			string routeDataTable = "D:/Projet pro/stage/2022/ASP-NET-TEST/WebOpportunityCrowler/WebOpportunityCrowler/App_Data/opportunitiesDataTable.xml";
			
			opportunitiesDataTable.ReadXml(routeDataTable);
			//opportunitiesDataTable.WriteXml(routeDataTable);
			
		}
		public object getOpportunityData(DataTable table)
        {

			/*
			var contactRow = from row in table.Rows.Cast<DataRow>()
							 where row.Field<string>("opportunity_title") != ""
							 select row;
			*/
			//---

			List<Opportunity> opportunities = new List<Opportunity>();

			string expression = "opportunity_title is not null";
			
			string sortOrder = "opportunity_title ASC";
			
			DataRow[] foundRows;

			foundRows = table.Select(expression, sortOrder);
			
			string label = "all rows";

			Console.WriteLine("\n{0}", label);
			if (foundRows.Length <= 0)
			{
				Console.WriteLine("no rows found");
			}
			object row2 = null;
			foreach (DataRow row in foundRows)
			{
				foreach (DataColumn column in row.Table.Columns)
				{
					Console.Write("\table {0}", row[column]);
					// rowArray.Append(row[column]);
				}
				Console.WriteLine();
				row2 = row.ItemArray;
			}
			return row2;
		}
	}
}