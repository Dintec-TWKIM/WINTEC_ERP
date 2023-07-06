using System.Data;

namespace cz
{
    public class CDT_PU_RCVH
    {
        public DataTable DT_PURCVH = new DataTable();
        private DataColumn ColumnType;

        public CDT_PU_RCVH()
        {
            this.ColumnType = new DataColumn("NO_RCV", typeof(string), null, MappingType.Element);
            this.DT_PURCVH.Columns.Add(this.ColumnType);

            this.ColumnType = new DataColumn("CD_PLANT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);

            this.ColumnType = new DataColumn("CD_PARTNER", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("DT_REQ", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_EMP", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_TRANS", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);

            this.ColumnType = new DataColumn("FG_PROCESS", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_EXCH", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_SL", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_RETURN", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_AM", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("DC_RMK", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_AUTORCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("NO_IO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("DT_IO", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("GI_PARTNER", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("CD_DEPT", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_LC", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = "False";
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_SUBCON", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = "N";
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("YN_Local_LC", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
            
            this.ColumnType = new DataColumn("FG_RCV", typeof(string), null, MappingType.Element);
            this.ColumnType.DefaultValue = string.Empty;
            this.DT_PURCVH.Columns.Add(this.ColumnType);
        }
    }
}