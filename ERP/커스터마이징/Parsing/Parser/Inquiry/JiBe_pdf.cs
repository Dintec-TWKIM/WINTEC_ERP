using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class JiBe_pdf
	{
        string vessel;
        string reference;
        string partner;
        string imoNumber;
        string contact;
        DataTable dtItem;

        string fileName;
        UnitConverter uc;


        #region ==================================================================================================== Property

        public string Vessel
        {
            get
            {
                return vessel;
            }
        }

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public string ImoNumber
        {
            get
            {
                return imoNumber;
            }
        }

        public string Partner
        {
            get
            {
                return partner;
            }
        }

        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        #endregion ==================================================================================================== Constructor



        public JiBe_pdf(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자
            imoNumber = "";
            contact = "";

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품목명
            dtItem.Columns.Add("UNIT");         // 단위
            dtItem.Columns.Add("QT");           // 수량
            dtItem.Columns.Add("UNIQ");         // 선사
            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            int idx_s = -1;
            int idx_e = -1;
            string text = PdfReader.ToText(fileName);
            string refNo = string.Empty;

            if(text.Contains("Requisition Number"))
			{
                idx_s = text.IndexOf("Requisition Number");
                idx_e = text.IndexOf("Quotation Code");

                if(idx_s != -1 && idx_e != -1)
				{
                    refNo = text.Substring(idx_s, idx_e).Replace("Requisition Number", "").Replace(":","").Replace("­", "-").Trim();
				}
			}

            SqlConnection sqlConn;
            sqlConn = new SqlConnection("server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE");

            string query = string.Empty;
            query = "SELECT * FROM CZ_SA_QTN_PREREG WHERE NO_REF = '"+refNo+"'";
            DataTable dt = DBMgr.GetDataTable(query);
            sqlConn.Close();


            if(dt.Rows.Count > 0)
			{
                imoNumber = dt.Rows[0]["NO_IMO"].ToString();
                contact = dt.Rows[0]["CONTACT"].ToString();
                reference = refNo;
                vessel = dt.Rows[0]["NM_VESSEL"].ToString();


                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    //ITEM ADD START
                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = (r+1).ToString();
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = dt.Rows[r]["NM_ITEM_PARTNER"].ToString();
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(dt.Rows[r]["UNIT"].ToString());
                    dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = dt.Rows[r]["QT"].ToString();
                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + dt.Rows[r]["NM_SUBJECT"].ToString();
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = dt.Rows[r]["CD_ITEM_PARTNER"].ToString();
                }
            }

        }
    }
}
