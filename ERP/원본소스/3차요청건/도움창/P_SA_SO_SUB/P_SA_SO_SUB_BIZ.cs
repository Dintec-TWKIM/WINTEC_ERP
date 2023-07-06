using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.SA.Common;
using System.Text;

namespace sale
{
    class P_SA_SO_SUB_BIZ
    {
        DataTable dt수주적용Schema = null;

        public P_SA_SO_SUB_BIZ()
        {
            dt수주적용Schema = new DataTable();
            dt수주적용Schema.Columns.Add("NO_SO", typeof(string)); dt수주적용Schema.Columns.Add("SEQ_SO", typeof(decimal));
            T.SetDefaultValue(dt수주적용Schema);
        }

        #region 조회
        public DataTable Search(object[] obj, string strTabName)
        {
            DataTable dt = null;

            if (Global.MainFrame.ServerKeyCommon == "HANSU")
                dt = DBHelper.GetDataTable("UP_SA_SO_Z_HANSU_SUB_APPLY_S", obj);
            else dt = DBHelper.GetDataTable("UP_SA_SO_SUB_APPLY_S", obj);

            switch (strTabName)
            {
                case "NO_SO":
                    dt.DefaultView.Sort = "NO_SO ASC";
                    break;
                case "CD_ITEM":
                    dt.DefaultView.Sort = "CD_ITEM ASC, NO_SO ASC";
                    break;
            }
            dt = dt.DefaultView.ToTable();
            T.SetDefaultValue(dt);            
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = null;

            if (Global.MainFrame.ServerKeyCommon == "WINPLUS" && Global.MainFrame.CurrentPageID == "P_SA_GIR")
                dt = DBHelper.GetDataTable("UP_SA_Z_WINPLUS_SO_SUB_S", obj);
            else if (Global.MainFrame.ServerKeyCommon == "HANSU")
                dt = DBHelper.GetDataTable("UP_SA_Z_HANSU_SO_SUB_SELECT1", obj);
            else
                dt = DBHelper.GetDataTable("UP_SA_SO_SUB_SELECT1", obj);

            if (dt.Columns.Contains("NO_SO") && dt.Columns.Contains("SEQ_SO"))
            {
                dt.DefaultView.Sort = "NO_SO ASC, SEQ_SO ASC";
                dt = dt.DefaultView.ToTable();
            }
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion

        #region 단가통제 적용 여부 조회
        public string GetSaleOrgUmCheck(object[] obj)
        {
            string SelectQuery = string.Empty;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                SelectQuery = " SELECT ISNULL(SO.SO_PRICE, 'N') AS SO_PRICE " +
                                     "   FROM MA_SALEGRP SG " +
                                     "   LEFT JOIN MA_SALEORG SO ON SG.CD_COMPANY = SO.CD_COMPANY AND SG.CD_SALEORG = SO.CD_SALEORG " +
                                     "  WHERE SG.CD_COMPANY = '" + obj[0].ToString() + "' " +
                                     "    AND SG.CD_SALEGRP = '" + obj[1].ToString() + "'";
            }
            else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {
                SelectQuery = " SELECT NVL(SO.SO_PRICE, 'N') AS SO_PRICE " +
                                     "   FROM MA_SALEGRP SG " +
                                     "   LEFT JOIN MA_SALEORG SO ON SG.CD_COMPANY = SO.CD_COMPANY AND SG.CD_SALEORG = SO.CD_SALEORG " +
                                     "  WHERE SG.CD_COMPANY = '" + obj[0].ToString() + "' " +
                                     "    AND SG.CD_SALEGRP = '" + obj[1].ToString() + "'";
            }

            string so_Price = D.GetString(Global.MainFrame.ExecuteScalar(SelectQuery));

            return so_Price;
        }
        #endregion

        #region ♣ 영업환경설정 가져오기
        public DataTable search_EnvMng()
        {
            object[] obk = new object[1];
            obk[0] = Global.MainFrame.LoginInfo.CompanyCode;

            //재고단위EDIT여부(2중단위관리여부)사용여부CHK
            return DBHelper.GetDataTable("UP_SA_ENV_SELECT", obk);
        }
        #endregion

        public DataTable Get수주적용Schema { get { return dt수주적용Schema; } }

        public DataTable 수주Schema()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CD_PARTNER", typeof(string));
            dt.Columns.Add("LN_PARTNER", typeof(string));
            dt.Columns.Add("CD_ITEM", typeof(string));
            dt.Columns.Add("NM_ITEM", typeof(string));
            dt.Columns.Add("STND_ITEM", typeof(string));
            dt.Columns.Add("UNIT_IM", typeof(string));
            dt.Columns.Add("QT_SO", typeof(decimal));
            dt.Columns.Add("GI_PARTNER", typeof(string));
            dt.Columns.Add("GI_LN_PARTNER", typeof(string));
            dt.Columns.Add("CD_SL", typeof(string));
            dt.Columns.Add("DT_DUEDATE", typeof(string));
            dt.Columns.Add("DT_REQGI", typeof(string));
            return dt;
        }

        #region 거래처정보
        internal DataTable 거래처정보(DataTable dt엑셀, ref StringBuilder str, ref bool is거래처)
        {
            DataTable dt거래처 = dt엑셀.DefaultView.ToTable(true, "CD_PARTNER");
            string CD_PARTNER = Duzon.ERPU.MF.Common.Common.MultiString(dt거래처, "CD_PARTNER", "|");
            거래처.조회 partner = new 거래처.조회();
            DataTable dt = partner.거래처정보(CD_PARTNER);

            foreach (DataRow row in dt거래처.Rows)
            {
                if (dt.Rows.Find(row["CD_PARTNER"]) == null)
                {
                    str.AppendLine(D.GetString(row["CD_PARTNER"]));
                    is거래처 = false;
                }
            }
            return dt;

        }
        #endregion

        #region 품목
        internal DataTable 품목(DataTable dt엑셀, string 공장, ref StringBuilder str, ref bool is품목)
        {
            DataTable dt품목 = dt엑셀.DefaultView.ToTable(true, "CD_ITEM");
            string CD_ITEM = Duzon.ERPU.MF.Common.Common.MultiString(dt품목, "CD_ITEM", "|");
            품목관리.조회 item = new 품목관리.조회();
            DataTable dt = item.품목정보(공장, CD_ITEM);

            foreach (DataRow row in dt품목.Rows)
            {
                if (dt.Rows.Find(row["CD_ITEM"]) == null)
                {
                    str.AppendLine(D.GetString(row["CD_ITEM"]));
                    is품목 = false;
                }
            }
            return dt;
        }
        #endregion 
    }
}