using System;
using System.Data;
using System.Linq;

namespace ConsoleApp1
{

	public class OpportunityDataTable
	{
		public DataTable createOpportunityDataTable()
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
	}

}
