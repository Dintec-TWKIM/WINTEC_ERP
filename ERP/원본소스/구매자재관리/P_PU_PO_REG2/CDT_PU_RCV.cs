using System;
using System.Data;
using Duzon.Common.Util;

namespace pur
{
	/// <summary>
	/// DT_PU_RCV에 대한 요약 설명입니다.
	/// </summary>
	public class CDT_PU_RCV
	{
		public DataTable DT_PURCV = new DataTable();
		DataColumn ColumnType;

		public CDT_PU_RCV()
		{
			//0
			this.ColumnType = new DataColumn("NO_RCV", typeof(string), null, System.Data.MappingType.Element);
			DT_PURCV.Columns.Add(this.ColumnType);
			//1
			this.ColumnType = new DataColumn("NO_LINE", typeof(System.Decimal), null, System.Data.MappingType.Element);
			DT_PURCV.Columns.Add(this.ColumnType);
			//2
            //this.ColumnType = new DataColumn("CD_COMPANY", typeof(string), null, System.Data.MappingType.Element);
            //DT_PURCV.Columns.Add(this.ColumnType);
			//3
			this.ColumnType = new DataColumn("NO_PO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//4
			this.ColumnType = new DataColumn("NO_POLINE", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//5
			this.ColumnType = new DataColumn("CD_PURGRP", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//6
			this.ColumnType = new DataColumn("DT_LIMIT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//7
			this.ColumnType = new DataColumn("DT_GRLAST", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//8
			this.ColumnType = new DataColumn("CD_ITEM", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//9
			this.ColumnType = new DataColumn("QT_REQ", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//10
			this.ColumnType = new DataColumn("YN_INSP", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//11	
			this.ColumnType = new DataColumn("QT_PASS", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//12
			this.ColumnType = new DataColumn("QT_REJECTION", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//13
			this.ColumnType = new DataColumn("QT_GR",typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//14
			this.ColumnType = new DataColumn("CD_UNIT_MM", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//15
			this.ColumnType = new DataColumn("QT_REQ_MM",typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//16
			this.ColumnType = new DataColumn("QT_GR_MM",typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//17
			this.ColumnType = new DataColumn("CD_EXCH", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//18
			this.ColumnType = new DataColumn("RT_EXCH", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//19
			this.ColumnType = new DataColumn("UM_EX_PO", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//20
			this.ColumnType = new DataColumn("UM_EX", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//21
			this.ColumnType = new DataColumn("AM_EX", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//22
			this.ColumnType = new DataColumn("UM", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//23
			this.ColumnType = new DataColumn("AM", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//24
			this.ColumnType = new DataColumn("VAT", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//25
			this.ColumnType = new DataColumn("VAT_CLS", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//26
			this.ColumnType = new DataColumn("RT_CUSTOMS", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//27
			this.ColumnType = new DataColumn("CD_PJT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//28
			this.ColumnType = new DataColumn("YN_PURCHASE", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="Y";
			DT_PURCV.Columns.Add(this.ColumnType);
			//29
			this.ColumnType = new DataColumn("YN_RETURN", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//30
			this.ColumnType = new DataColumn("FG_TPPURCHASE", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//31
			this.ColumnType = new DataColumn("FG_RCV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//32
			this.ColumnType = new DataColumn("FG_TRANS", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//33
			this.ColumnType = new DataColumn("FG_TAX", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//34
			this.ColumnType = new DataColumn("FG_TAXP", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//35
			this.ColumnType = new DataColumn("YN_AUTORCV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//36
			this.ColumnType = new DataColumn("YN_REQ", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//37
			this.ColumnType = new DataColumn("CD_SL", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//38
			this.ColumnType = new DataColumn("NO_LC", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//39
			this.ColumnType = new DataColumn("NO_LCLINE", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//40
			this.ColumnType = new DataColumn("RT_SPEC", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//41
			this.ColumnType = new DataColumn("NO_EMP", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//42
			this.ColumnType = new DataColumn("NO_IO_MGMT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//43
			this.ColumnType = new DataColumn("NO_IOLINE_MGMT",typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//44
			this.ColumnType = new DataColumn("NO_PO_MGMT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//45
			this.ColumnType = new DataColumn("NO_POLINE_MGMT", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			//46
			this.ColumnType = new DataColumn("NO_TO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			//47
			this.ColumnType = new DataColumn("NO_TO_LINE", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//48
			this.ColumnType = new DataColumn("AM_EXRCV", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//49
			this.ColumnType = new DataColumn("AM_RCV",typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//50
			this.ColumnType = new DataColumn("AM_EXREQ", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			//51
			this.ColumnType = new DataColumn("AM_REQ", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);


			//가상필드
			this.ColumnType = new DataColumn("DT_IO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("FG_TPIO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("YN_PURSALE", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("FG_IO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_GOOD_REQ", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_REQ_RCV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_REQ_INV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("NO_IO", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_PLANT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_BIZAREA", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_PARTNER", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("YN_AM", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("NO_IOLINE", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_SECTION", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_BIN", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("NO_ISURCV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("NO_ISURCVLINE", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("NO_PSO_MGMT", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("NO_PSOLINE_MGMT", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
			
			this.ColumnType = new DataColumn("FG_PS", typeof(string), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_QTIOTP", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_IO", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_RETURN", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_TRANS_INV", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_INSP_INV", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_REJECT_INV", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_GOOD_INV", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_CLS", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("AM_CLS", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("UM_STOCK", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("UM_EVAL", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("AM_DISTRIBU", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("AM_CUSTOMS", typeof(System.Decimal), null,System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_IMSEAL", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_EXLC", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("GI_PARTNER", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_GROUP", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("CD_BIZAREA_RCV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			
			this.ColumnType = new DataColumn("CD_PLANT_RCV", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			
			this.ColumnType = new DataColumn("CD_SL_REF", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			
			this.ColumnType = new DataColumn("CD_SECTION_REF", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			
			this.ColumnType = new DataColumn("CD_BIN_REF", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);
			
			this.ColumnType = new DataColumn("BILL_PARTNER", typeof(string), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue ="";
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("QT_UNIT_MM", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			this.ColumnType = new DataColumn("UM_EX_PSO", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);

			//20031120
			this.ColumnType = new DataColumn("CD_FLAG1", typeof(System.Decimal), null, System.Data.MappingType.Element);
			this.ColumnType.DefaultValue =0;
			DT_PURCV.Columns.Add(this.ColumnType);
           
            this.ColumnType = new DataColumn("DT_REQ", typeof(System.String), null, System.Data.MappingType.Element);
            this.ColumnType.DefaultValue = "";
            DT_PURCV.Columns.Add(this.ColumnType);


		}
	}
}
