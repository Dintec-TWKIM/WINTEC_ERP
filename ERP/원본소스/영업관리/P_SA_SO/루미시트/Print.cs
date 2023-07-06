using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.Windows.Print;

namespace sale
{
    internal class PrintRumiSheet
    {
        string noSo = string.Empty;
        DataTable dtH = null;
        DataTable dtL = null;
        DataTable dtSheet = null;
        DataTable dtResult = null;

        ReportHelper _rdf;
        string[] _fixedCols = null;
        string[] _headerCols = null;
        string[] _headerCaption = null;
        string[] _crossCols = null;
        int _startColIndex = 0;
        int _endColIndex = 0;
        string[] _dataCols = null;

        internal PrintRumiSheet(string NO_SO)
        {
            this.noSo = NO_SO;
        }

        internal void Print()
        {
            _rdf = new ReportHelper("R_SA_Z_FAWOO_SO_0", "수주등록(루미시트)", false);

            Search();

            dtResult = GetPrintData();

            object[] args = 동적컬럼();
           // _fixedCols = new string[] { "@DC_RMK" };  // 표의 고정 컬럼들
            //_headerCaption = 

            List<string> list = new List<string>();
            for (int i = 1; i <= dtL.Rows.Count; i++)
            {
                list.Add(D.GetString(i));
            }
            _headerCaption = list.ToArray();

            _headerCols = new string[] { "@T1", "@T2", "@T3", "@T4", "@T5", "@T6", "@T7", "@T8" };
            _crossCols = args[1] as string[];

            string filter = "NUM_USERDEF1 NOT IN (400, 500)";
            DataRow[] dr = dtL.Select(filter);

            _startColIndex = dtResult.Columns["DC_RMK"].Ordinal + 1;
            _endColIndex = _startColIndex - 1 + dr.Length;
            _dataCols = new string[] { "@D1", "@D2", "@D3", "@D4", "@D5", "@D6", "@D7", "@D8" };

            _rdf.DynamicFixedCols = _fixedCols;
            _rdf.DynamicCaptions(_headerCols, _headerCaption, _startColIndex, _endColIndex);
            _rdf.DynamicDatas(_dataCols);
            _rdf.SetDataTable(dtResult);
            _rdf.Print();
        }

        bool rdf_Printing(object sender, PrintArgs args)
        {
            if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
            {
                _rdf.Initialize();//필수
                _rdf.SetUsePrintApprove(false);

                

                _rdf.DynamicFixedCols = _fixedCols;
                _rdf.DynamicCaptions(_headerCols, _headerCaption, _startColIndex, _endColIndex);
                _rdf.DynamicDatas(_dataCols);
                _rdf.SetDataTable(dtResult);
            }
            else if (args.Action == PrintActionEnum.ON_PRINT)
            {
            }
            return true;
        }

        object[] 동적컬럼()
        {
            List<string> listTitle = new List<string>();
            List<string> listColName = new List<string>();

            for (int i = 1; i <= 8; i++)
            {
                listTitle.Add(D.GetString(i));
                listColName.Add("C" + D.GetString(i));
            }
            return new object[] { listTitle.ToArray(), listColName.ToArray() };
        }

        private DataTable GetPrintData()
        {
            dtResult = GetTableSchema(dtL.Rows.Count);            

            // Row생성
            for (int i = 1; i <= 30; i++)
            {
                DataRow newrow = dtResult.NewRow();
                newrow["NO"] = "R" + D.GetString(i);
                dtResult.Rows.Add(newrow);
            }

            // SOH 내용 채우기
            SetSOH();

            // Row 내용 채우기
            SetSOL();

            return dtResult;
        }

        void SetSOH()
        {
            DataRow rowH = dtH.Rows[0];
            string NO_SO = D.GetString(rowH["NO_SO"]);
            string DT_INSERT = D.GetString(rowH["DT_INSERT"]);
            string DT_SO = D.GetString(rowH["DT_SO"]);
            string DT_DUEDATE = D.GetString(dtL.Rows[0]["DT_DUEDATE"]);
            string NM_NATION = D.GetString(rowH["NM_NATION"]);
            string LN_PARTNER = D.GetString(rowH["LN_PARTNER"]);
            string NM_DEPT_INSERT = D.GetString(rowH["NM_DEPT_INSERT"]);
            string NM_KOR = D.GetString(rowH["NM_KOR"]);
            string NM_SALEGRP = D.GetString(rowH["NM_SALEGRP"]);
            string NM_SO = D.GetString(rowH["NM_SO"]);
            string LN_TRANSPORT = D.GetString(rowH["LN_TRANSPORT"]);
            string DC_RMK_TEXT = D.GetString(rowH["DC_RMK_TEXT"]);
            string DC_RMK = D.GetString(rowH["DC_RMK"]);


            foreach (DataRow row in dtResult.Rows)
            {
                row["NO_SO"] = 	NO_SO;		
                row["DT_INSERT"] = DT_INSERT;
                row["DT_SO"] = 	DT_SO;
                row["DT_DUEDATE"] = DT_DUEDATE;
                row["NM_NATION"] = NM_NATION;	
                row["LN_PARTNER"] = LN_PARTNER;
                row["NM_DEPT_INSERT"] = NM_DEPT_INSERT;
                row["NM_KOR"] = NM_KOR;
                row["NM_SALEGRP"] = NM_SALEGRP;
                row["NM_SO"] = NM_SO;
                row["LN_TRANSPORT"] = LN_TRANSPORT;
                row["DC_RMK_TEXT"] = DC_RMK_TEXT;
                row["DC_RMK"] = DC_RMK;
            }

            // 포장비
            string filter = "NUM_USERDEF1 = 400";
            string amt = D.GetString(rowH["NM_EXCH"]) + " " + string.Format("{0:#,###.##}", D.GetDecimal(dtL.Compute("SUM(AM_SO)", filter)));
            _rdf.SetData("@@포장비", amt);

            // 운송비
            filter = "NUM_USERDEF1 = 500";
            amt = D.GetString(rowH["NM_EXCH"]) + " " + string.Format("{0:#,###.##}", D.GetDecimal(dtL.Compute("SUM(AM_SO)", filter)));
            _rdf.SetData("@@운송비", amt);

            // 총금액
            amt = D.GetString(rowH["NM_EXCH"]) + " " + string.Format("{0:#,###.##}", D.GetDecimal(dtL.Compute("SUM(AM_SO)", "")));
            _rdf.SetData("@@총금액", amt);

            // 포장비품목명 : NUM_USERDEF1 400인것중 항번이 가장 큰 품목명
            DataRow[] dr = dtL.Select("NUM_USERDEF1 = 400", "SEQ_SO DESC", DataViewRowState.CurrentRows);
            string 포장비품목명 = "";
            if (dr != null && dr.Length > 0)
                포장비품목명 = D.GetString(dr[0]["NM_ITEM"]);
            _rdf.SetData("@@포장비품목", 포장비품목명);

            // 운송비품목명 : NUM_USERDEF1 500인것중 항번이 가장 큰 품목명
            dr = dtL.Select("NUM_USERDEF1 = 500", "SEQ_SO DESC", DataViewRowState.CurrentRows);
            string 운송비품목명 = "";
            if (dr != null && dr.Length > 0)
                운송비품목명 = D.GetString(dr[0]["NM_ITEM"]);
            _rdf.SetData("@@운송비품목", 운송비품목명);

        }

        void SetSOL()
        {
            int idx = 1;
            int idxCol = 1;
            foreach (DataRow rowL in dtL.Rows)
            {
                System.Diagnostics.Debug.WriteLine(D.GetString(rowL["NM_ITEM"]));
                if (D.GetDecimal(rowL["NUM_USERDEF1"]) == 400 || D.GetDecimal(rowL["NUM_USERDEF1"]) == 500)
                {
                    idx++;
                    continue;
                }

                // 제품사양 : 1 ~ 8
                for (int i = 1; i <= 8; i++)
                {
                    Set제품사양(i, rowL, idxCol, idx);
                }

                // LED사양 : 9 ~ 12
                decimal SEQ_SO = D.GetDecimal(rowL["SEQ_SO"]);
                string filter = "SEQ_SO = " + D.GetString(SEQ_SO) + " AND NUM_USERDEF1 = 100";
                DataRow[] dr = dtSheet.Select(filter, "", DataViewRowState.CurrentRows);
                if (dr != null && dr.Length > 0)
                {
                    for (int i = 9; i <= 12; i++)
                    {
                        SetLED사양(i, rowL, dr[0], idxCol, idx);
                    }
                }

                // 파워사양 : 13 ~ 15
                filter = "SEQ_SO = " + D.GetString(SEQ_SO) + " AND NUM_USERDEF1 = 200";
                dr = dtSheet.Select(filter, "", DataViewRowState.CurrentRows);
                if (dr != null && dr.Length > 0)
                {
                    for (int i = 13; i <= 15; i++)
                    {
                        Set파워사양_200(i, rowL, dr[0], idxCol, idx);
                    }
                }

                // 파워사양 : 16 ~ 19
                filter = "SEQ_SO = " + D.GetString(SEQ_SO) + " AND NUM_USERDEF1 = 300";
                dr = dtSheet.Select(filter, "", DataViewRowState.CurrentRows);
                if (dr != null && dr.Length > 0)
                {
                    for (int i = 16; i <= 17; i++)
                    {
                        Set파워사양_300(i, rowL, dr[0], idxCol, idx);
                    }
                }

                for (int i = 18; i <= 19; i++)
                {
                    Set파워사양(i, rowL, idxCol, idx);
                }

                for (int i = 20; i <= 27; i++)
                {
                    Set기타(i, rowL, idxCol, idx);
                }

                for (int i = 28; i <= 30; i++)
                {
                    Set금액(i, rowL, idxCol, idx);
                }

                idxCol++;
                idx++;
            }
        }

        void Set제품사양(int idx, DataRow rowL, int idxCol, int idxSOL)
        {
            DataRow row = dtResult.Rows[idx - 1];
            DataRow rowSOL = dtL.Rows[idxSOL - 1];
            string colName = "C" + D.GetString(idxCol);
            string[] argsStnd = null;

            if(idx == 2 || idx == 3 || idx == 4)
            {
                argsStnd = D.GetString(rowL["STND_ITEM"]).Split(new char[] { '*' });
            }
            switch (idx)
            {
                    // 제품사양 - 제품종류
                case 1 :
                    System.Diagnostics.Debug.WriteLine(D.GetString(rowL["NM_ITEM"]));
                    row[colName] = rowSOL["NM_ITEM"];
                    break;
                    // 제품사양 - 램프설치면
                case 2 :
                    if (argsStnd != null && argsStnd.Length >= 1)
                        row[colName] = argsStnd[0];
                    break;
                    // 제품사양 - 폭
                case 3:
                    if (argsStnd != null && argsStnd.Length >= 2)
                        row[colName] = argsStnd[1];
                    break;
                    // 제품사양 - 두께
                case 4:
                    if (argsStnd != null && argsStnd.Length >= 3)
                        row[colName] = argsStnd[2];
                    break;
                    // 제품사양 - 세부규격
                case 5 :
                    row[colName] = rowL["STND_DETAIL_ITEM"];
                    break;
                    // 제품사양 - 단/양면 기타
                case 6 :
                    row[colName] = rowL["NM_CD_USERDEF2"];                    
                    break;
                    // 제품사양 - 램프면의수
                case 7:
                    row[colName] = rowL["NM_CD_USERDEF3"];
                    break;
                    // 제품사양 - 가공형태
                case 8 :
                    row[colName] = rowL["NM_CD_USERDEF5"];
                    break;                    
            }
        }

        void SetLED사양(int idx, DataRow rowL, DataRow rowLL, int idxCol, int idxSOL)
        {
            DataRow row = dtResult.Rows[idx - 1];
            DataRow rowSOL = dtL.Rows[idxSOL - 1];
            string colName = "C" + D.GetString(idxCol);

            switch (idx)
            {
                    // LED사양 - Sheet ERP코드
                case 9 :
                    row[colName] = rowLL["CD_MATL"];
                    break;
                    // LED사양 - Sheet 규격
                case 10 :
                    row[colName] = rowLL["STND_ITEM"];
                    break;
                    // LED사양 - 비고1(SIZE)
                case 11 :
                    row[colName] = rowLL["DC_RMK_1"];
                    break;
                    // LEC사양 - 비고2(LED 총수량)
                case 12:
                    row[colName] = rowLL["DC_RMK_2"];
                    break;
            }
        }

        void Set파워사양_200(int idx, DataRow rowL, DataRow rowLL, int idxCol, int idxSOL)
        {
            DataRow row = dtResult.Rows[idx - 1];
            DataRow rowSOL = dtL.Rows[idxSOL - 1];
            string colName = "C" + D.GetString(idxCol);

            switch (idx)
            {
                    // 파워사양 - 아답터코드
                case 13 :
                    row[colName] = rowLL["CD_MATL"];
                    break;
                    // 파워사양 - 아답터규격
                case 14 :
                    row[colName] = rowLL["STND_ITEM"];
                    break;
                    // 파워사양 - 아답터총수량
                case 15 :
                    row[colName] = string.Format("{0:#,###}", D.GetDecimal(rowLL["QT_NEED"])); ;
                    break;                   
            }
        }

        void Set파워사양_300(int idx, DataRow rowL, DataRow rowLL, int idxCol, int idxSOL)
        {
            DataRow row = dtResult.Rows[idx - 1];
            DataRow rowSOL = dtL.Rows[idxSOL - 1];
            string colName = "C" + D.GetString(idxCol);

            switch (idx)
            {
                // 파워사양 - 파워선코드
                case 16:
                    row[colName] = rowLL["CD_MATL"];
                    break;
                // 파워사양 - 파워선규격
                case 17:
                    row[colName] = rowLL["STND_ITEM"];
                    break;               
            }
        }

        void Set파워사양(int idx, DataRow rowL, int idxCol, int idxSOL)
        {
            DataRow row = dtResult.Rows[idx - 1];
            DataRow rowSOL = dtL.Rows[idxSOL - 1];
            string colName = "C" + D.GetString(idxCol);

            switch (idx)
            {
                // 파워사양 - DC선형태
                case 18:
                    row[colName] = rowL["NM_CD_USERDEF6"];
                    break;
                // 파워사양 - 배선방식
                case 19:
                    row[colName] = rowL["NM_CD_USERDEF7"];
                    break;
            }
        }

        void Set기타(int idx, DataRow rowL, int idxCol, int idxSOL)
        {
            DataRow row = dtResult.Rows[idx - 1];
            DataRow rowSOL = dtL.Rows[idxSOL - 1];
            string colName = "C" + D.GetString(idxCol);

            switch (idx)
            {
                    // 기타 - 프레임종류
                case 20 :
                    row[colName] = rowL["NM_CD_USERDEF8"];
                    break;
                    // 기타 - 프레임색깔
                case 21:
                    row[colName] = rowL["NM_USERDEF1"];
                    break;
                    // 기타 - 반사지유무
                case 22:
                    row[colName] = rowL["NM_USERDEF2"];
                    break;
                    // 기타 - 병렬커넥터
                case 23:
                    row[colName] = rowL["NM_USERDEF3"];
                    break;
                    // 기타 - DC선 색깔
                case 24:
                    row[colName] = rowL["NM_USERDEF4"];
                    break;
                    // 기타 - DC선 길이
                case 25:
                    row[colName] = rowL["NM_USERDEF5"];
                    break;
                    // 기타 - 보호아크릴
                case 26:
                    row[colName] = rowL["NM_CD_USERDEF9"];
                    break;
                    // 기타 - 방수형태
                case 27:
                    row[colName] = rowL["NM_CD_USERDEF10"];
                    break;

            }
        }

        void Set금액(int idx, DataRow rowL, int idxCol, int idxSOL)
        {            
            DataRow row = dtResult.Rows[idx - 1];
            DataRow rowSOL = dtL.Rows[idxSOL - 1];
            DataRow rowSOH = dtH.Rows[0];
            string colName = "C" + D.GetString(idxCol);            

            switch (idx)
            {
                    // 금액 - 단가
                case 28 :
                    row[colName] = D.GetString(rowSOH["NM_EXCH"]) + " " + string.Format("{0:#,###.##}", D.GetDecimal(rowL["UM_SO"]));
                    break;
                    // 수량
                case 29:
                    row[colName] = string.Format("{0:#,###.##}", D.GetDecimal(rowL["QT_SO"]));
                    break;
                    // 금액
                case 30:
                    row[colName] = D.GetString(rowSOH["NM_EXCH"]) + " " + string.Format("{0:#,###.##}", D.GetDecimal(rowL["AM_SO"]));
                    break;
                   

            }
        }

        #region ★ 조회관련

        void Search()
        {
            DataSet ds = DBHelper.GetDataSet("UP_SA_Z_FAWOO_SO_S", new object[] { MA.Login.회사코드, noSo }, new string[] { "", "SEQ_SO", "SEQ_SO, SEQ_SO_LINE" });

            dtH = ds.Tables[0];
            dtL = ds.Tables[1];
            dtSheet = ds.Tables[2];
        }

        DataTable GetTableSchema(int countSol)
        {
            DataTable dt = new DataTable();
            ColAdd(dt, new string[] { "NO", "NO_SO","DT_INSERT", "DT_SO", "DT_DUEDATE" });
            ColAdd(dt, new string[] { "NM_NATION", "LN_PARTNER", "NM_DEPT_INSERT", "NM_KOR", "NM_SALEGRP" });
            ColAdd(dt, new string[] { "NM_SO", "LN_TRANSPORT", "DC_RMK_TEXT", "DC_RMK" });

            string[] args = new string[countSol];

            for (int i = 1; i <= countSol; i++)
            {
                args[i - 1] = "C" + D.GetString(i);
            }
            ColAdd(dt, args);
            return dt;
        }

        void ColAdd(DataTable dt, string[] argsColName)
        {
            foreach (string str in argsColName)
            {
                dt.Columns.Add(str, typeof(string));
            }
        }

        #endregion

    }
    
}
