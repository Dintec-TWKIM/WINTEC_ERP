using System;
using System.Data;

using Duzon.Common.Util;

namespace pur
{
	/// <summary>
	/// CDT_PU_RCVH에 대한 요약 설명입니다.
	/// </summary>
	public class CDT_PU_RCVH
	{
		public DataTable DT_PURCVH = new DataTable();
		DataColumn ColumnType;

		public CDT_PU_RCVH()
		{
			//0
			this.ColumnType = new DataColumn("NO_RCV", typeof(string), null, System.Data.MappingType.Element);
			DT_PURCVH.Columns.Add(this.ColumnType);			
			//2
            //this.ColumnType = new DataColumn("CD_COMPANY", typeof(string), null, System.Data.MappingType.Element);
            //DT_PURCVH.Columns.Add(this.ColumnType);
			//3
			this.ColumnType = new DataColumn("CD_PLANT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			//4
			this.ColumnType = new DataColumn("CD_PARTNER", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			//5
			this.ColumnType = new DataColumn("DT_REQ", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			//6
			this.ColumnType = new DataColumn("NO_EMP", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			//7
			this.ColumnType = new DataColumn("FG_TRANS", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			//8
			this.ColumnType = new DataColumn("FG_PROCESS", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			
			//10
			this.ColumnType = new DataColumn("CD_EXCH", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			
			this.ColumnType = new DataColumn("CD_SL", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);
			
			//17
			this.ColumnType = new DataColumn("YN_RETURN", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

			//17
			this.ColumnType = new DataColumn("YN_AM", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

			//17
			this.ColumnType = new DataColumn("DC_RMK", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("YN_AUTORCV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("NO_IO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

		
			this.ColumnType = new DataColumn("DT_IO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("GI_PARTNER", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_DEPT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("YN_LC", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="False";
			DT_PURCVH.Columns.Add(this.ColumnType);            
				
			this.ColumnType = new DataColumn("YN_SUBCON", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="N";
			DT_PURCVH.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("YN_Local_LC", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCVH.Columns.Add(this.ColumnType);

            this.ColumnType = new DataColumn("FG_RCV", typeof(string), null, System.Data.MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            DT_PURCVH.Columns.Add(this.ColumnType);
		}
	}
}
