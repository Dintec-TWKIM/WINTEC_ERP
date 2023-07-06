using System;
using System.Data;

namespace cz
{
    public class CDT_PU_RCV
    {
        public DataTable DT_PURCV = new DataTable();
        private DataColumn ColumnType;

        public CDT_PU_RCV()
        {
            this.ColumnType = new DataColumn("NO_RCV", typeof(string), null, MappingType.Element);
            this.DT_PURCV.Columns.Add(this.ColumnType);

            this.ColumnType = new DataColumn("NO_LINE", typeof(decimal), null, MappingType.Element);
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_PO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_POLINE", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_PURGRP", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("DT_LIMIT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("DT_GRLAST", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_ITEM", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);

            this.ColumnType = new DataColumn("QT_REQ", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_INSP", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_PASS", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_REJECTION", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_GR", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_UNIT_MM", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_REQ_MM", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_GR_MM", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_EXCH", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("RT_EXCH", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("UM_EX_PO", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("UM_EX", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_EX", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("UM", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("VAT", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("VAT_CLS", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("RT_CUSTOMS", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_PJT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_PURCHASE", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = "Y";
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_RETURN", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_TPPURCHASE", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_RCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_TRANS", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_TAX", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_TAXP", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_AUTORCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_REQ", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_SL", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_LC", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_LCLINE", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("RT_SPEC", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_EMP", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_IO_MGMT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_IOLINE_MGMT", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_PO_MGMT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_POLINE_MGMT", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_TO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_TO_LINE", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_EXRCV", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_RCV", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_EXREQ", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_REQ", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("DT_IO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_TPIO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_PURSALE", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_IO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_GOOD_REQ", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_REQ_RCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_REQ_INV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_IO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_PLANT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_BIZAREA", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_PARTNER", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_AM", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_IOLINE", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_SECTION", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_BIN", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_ISURCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_ISURCVLINE", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_PSO_MGMT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_PSOLINE_MGMT", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_PS", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_QTIOTP", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_IO", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_RETURN", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_TRANS_INV", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_INSP_INV", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_REJECT_INV", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_GOOD_INV", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_CLS", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_CLS", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("UM_STOCK", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("UM_EVAL", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_DISTRIBU", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("AM_CUSTOMS", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_IMSEAL", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_EXLC", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("GI_PARTNER", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_GROUP", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_BIZAREA_RCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_PLANT_RCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_SL_REF", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_SECTION_REF", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_BIN_REF", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("BILL_PARTNER", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("QT_UNIT_MM", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("UM_EX_PSO", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_FLAG1", typeof(decimal), null, MappingType.Element);
            this.ColumnType.DefaultValue = 0;
            this.DT_PURCV.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("DT_REQ", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCV.Columns.Add(this.ColumnType);
        }
    }
}
