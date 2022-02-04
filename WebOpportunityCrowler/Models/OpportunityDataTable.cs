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
			opportunitiesDataTable.ReadXml(routeDataTable, XmlReadMode.ReadSchema);
			//opportunitiesDataTable.WriteXml(routeDataTable);
			
		}
		public object getOpportunityData(DataTable table)
        {

			
			var contactRow = from row in table.Rows.Cast<DataRow>()
							 select new Opportunity
							 {
								title = row.Field<string>(1),//column of index 0 = "Col1"
								date = row.Field<string>(2),//column of index 1 = "Col2"
								url = row.Field<string>(3),//column of index 5 = "Col6"
								description = row.Field<string>(4),//column of index 6 = "Col7"
								location = row.Field<string>(5),//column of index 4 = "Col3"
								company = row.Field<string>(6),//column of index 4 = "Col3"
								rate = row.Field<string>(7),//column of index 4 = "Col3"
							 };
			
			//---
			/*
			string expression = "opportunity_title is not null";
			
			string sortOrder = "opportunity_title ASC";
			
			DataRow[] foundRows;

			foundRows = table.Select(expression, sortOrder);
			
			object[] row2 = { };
			foreach (DataRow row in contactRow)
			{
				//foreach (DataColumn column in row.Table.Columns)
				//{
				//	Console.Write("\table {0}", row[column]);
				//	// rowArray.Append(row[column]);
				//}
				Console.WriteLine();
				row2 = row.ItemArray; // Retourne une ligne avec toutes ces colonnes
			}*/
			return contactRow;
		}
	}
}