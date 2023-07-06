using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;
using Dintec;
using Duzon.ERPU.Grant;
using System.Diagnostics;

namespace cz
{
    public partial class P_CZ_SA_ITEM_UPDATE : PageBase
    {
        P_CZ_SA_ITEM_UPDATE_BIZ _biz = new P_CZ_SA_ITEM_UPDATE_BIZ();

        public P_CZ_SA_ITEM_UPDATE()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_FILE", "파일번호", 100);
            this._flex.SetCol("NM_PARTNER", "매출처", 100);
            this._flex.SetCol("NO_IMO", "IMO번호", false);
            this._flex.SetCol("YN_PARTNER_ITEM", "기자재등록여부", 40);
            this._flex.SetCol("NO_HULL", "호선번호", 80);
            this._flex.SetCol("NM_VESSEL", "호선명", 100);
			this._flex.SetCol("DT_SHIP_DLV", "선박납기일자", 60, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flex.SetCol("DC_SHIPBUILDER", "조선소", false);
            this._flex.SetCol("NM_SUPPLIER", "매입처", 100);
            this._flex.SetCol("NO_LINE", "항번", 50);
            this._flex.SetCol("NO_DSP", "순번", 50);
            this._flex.SetCol("NM_SUBJECT", "주제", 150);
            this._flex.SetCol("CD_ITEM_PARTNER", "품목코드", 150);
            this._flex.SetCol("NM_ITEM_PARTNER", "품목명", 150);
            this._flex.SetCol("NM_CLS_ITEM", "계정구분", 100);
            this._flex.SetCol("QT_QTN", "수량", 40, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("UM_KR_P", "매입단가(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("LT", "LT", false);
            this._flex.SetCol("CD_ITEM_BEFORE", "재고코드(전)", 100);
            this._flex.SetCol("CD_ITEM_AFTER", "재고코드(후)", 100, true);
            this._flex.SetCol("CD_ITEM_SUB_BEFORE", "재고코드2(전)", 100);
            this._flex.SetCol("CD_ITEM_SUB_AFTER", "재고코드2(후)", 100, true);
            this._flex.SetCol("WARNING", "경고마스터", false);
            this._flex.SetCol("DC_RMK_QTN", "매입처상단비고", 100);
            this._flex.SetCol("DC_RMK", "매입처라인비고", 100);
            this._flex.SetCol("NM_UPDATE", "수정자", 80);
            this._flex.SetCol("DTS_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";
            this._flex.SetDummyColumn("S");

            this._flex.SetCodeHelpCol("CD_ITEM_AFTER", HelpID.P_MA_ITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM_AFTER" }, new string[] { "CD_ITEM" });

            this._flex.SetDefault("1.0.0.1", SumPositionEnum.Top);

            this._flex.SetMalgunGothic();

            this._flex.Styles.Add("미입력").BackColor = Color.White;
            this._flex.Styles.Add("입력").BackColor = Color.Yellow;

            #region 조건
            this._flex조건.BeginSetting(1, 1, false);

            this._flex조건.SetCol("NM_COLUMN", "컬럼", 100, true);
            this._flex조건.SetCol("YN_NOT_LIKE", "제외여부", 40, true, CheckTypeEnum.Y_N);
            this._flex조건.SetCol("DC_CONDITION", "조건", 120, true);

            this._flex조건.SetDataMap("NM_COLUMN", MA.GetCodeUser(new string[] { "NM_SUBJECT1", "CD_ITEM_PARTNER1", "NM_ITEM_PARTNER1", "CD_ITEM1", "NO_FILE1", "WARNING", "CD_SPEC1" }, new string[] { "주제", "품목코드", "품목명", "재고코드", "파일번호", "경고마스터", "U코드" }), "CODE", "NAME");

            this._flex조건.ExtendLastCol = true;

            this._flex조건.SettingVersion = "1.0.0.0";
            this._flex조건.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex조건.SetAlternateRow();
            this._flex조건.SetMalgunGothic();
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp견적일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp견적일자.EndDateToString = Global.MainFrame.GetStringToday;

            this.splitContainer1.SplitterDistance = 996;

            this.btn조건조기화_Click(null, null);

            UGrant ugrant = new UGrant();

            if (ugrant.GrantButton(Global.MainFrame.CurrentPageID, "EDIT"))
            {
                this.bpPanelControl1.Visible = true;
                this.chk재고코드갱신.Visible = true;
                this.btn재고코드적용.Visible = true;
                this._flex.Cols["S"].Visible = true;
                this._flex.Cols["CD_ITEM_BEFORE"].Caption = "재고코드(전)";
                this._flex.Cols["CD_ITEM_AFTER"].Visible = true;
                this._flex.Cols["CD_ITEM_AFTER"].AllowEditing = true;

                this._flex.Cols["CD_ITEM_SUB_BEFORE"].Caption = "재고코드2(전)";
                this._flex.Cols["CD_ITEM_SUB_AFTER"].Visible = true;
                this._flex.Cols["CD_ITEM_SUB_AFTER"].AllowEditing = true;

                this.bpPanelControl7.Visible = true;
                this._flex.Cols["YN_PARTNER_ITEM"].Visible = true;
            }
            else
            {
                this.bpPanelControl1.Visible = false;
                this.chk재고코드갱신.Visible = false;
                this.btn재고코드적용.Visible = false;
                this._flex.Cols["S"].Visible = false;
                this._flex.Cols["CD_ITEM_BEFORE"].Caption = "재고코드";
                this._flex.Cols["CD_ITEM_AFTER"].Visible = false;
                this._flex.Cols["CD_ITEM_AFTER"].AllowEditing = false;

                this._flex.Cols["CD_ITEM_SUB_BEFORE"].Visible = false;
                this._flex.Cols["CD_ITEM_SUB_BEFORE"].AllowEditing = false;
                this._flex.Cols["CD_ITEM_SUB_AFTER"].Visible = false;
                this._flex.Cols["CD_ITEM_SUB_AFTER"].AllowEditing = false;

                this.bpPanelControl7.Visible = false;
                this._flex.Cols["YN_PARTNER_ITEM"].Visible = false;
            }
        }

        private void InitEvent()
        {
            this.btn조건조기화.Click += new EventHandler(this.btn조건조기화_Click);

            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this.ctx재고코드.QueryBefore += new BpQueryHandler(this.ctx재고코드_QueryBefore);

            this.btn재고코드적용.Click += new EventHandler(this.btn재고코드적용_Click);

            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);

            this.chk자동줄맞춤.CheckedChanged += new EventHandler(this.chk자동줄맞춤_CheckedChanged);

            this._flex.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            string 쿼리;
            DataTable dt;
            Stopwatch timer;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;
                if (this._flex조건.DataTable.Select("ISNULL(DC_CONDITION, '') <> ''").Length == 0)
                {
                    this.ShowMessage("조회조건이 지정되지 않았습니다.");
                    return;
                }

                dt = null;

                int 기간 = Math.Abs((this.dtp견적일자.StartDate - this.dtp견적일자.EndDate).Days);

                string startDate, endDate;
                DataTable tmpDataTable;

                for (int i = 0; i <= 기간 / 365; i++)
                {
                    startDate = this.dtp견적일자.StartDate.AddDays(i * 365).ToString("yyyyMMdd");

                    if (기간 >= (i + 1) * 365)
                        endDate = this.dtp견적일자.StartDate.AddDays(((i + 1) * 365) - 1).ToString("yyyyMMdd");
                    else
                        endDate = this.dtp견적일자.EndDateToString;

                    쿼리 = "EXEC SP_EXECUTESQL N'" + this.쿼리생성(startDate, endDate).Replace("'", "''") + "'";
                    Debug.WriteLine(쿼리);

                    timer = new Stopwatch();

                    timer.Start();
                    tmpDataTable = DBMgr.GetDataTable(쿼리);
                    timer.Stop();

                    Debug.WriteLine("데이터조회 : " + timer.Elapsed.ToString());

                    if (i == 0)
                        dt = tmpDataTable;
                    else
                        dt.Merge(tmpDataTable);
                }

                this._flex.Binding = dt;

                if (this.chk자동줄맞춤.Checked)
                {
                    this._flex.AllowResizing = AllowResizingEnum.BothUniform;
                    this._flex.AutoSizeRows();
                }
                
                if (!_flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex.IsDataChanged == false) return false;

            if (!_biz.Save(this._flex.GetChanges())) return false;

            this._flex.AcceptChanges();

            return true;
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                if (e.Row < this._flex.Rows.Fixed || e.Col < this._flex.Cols.Fixed)
                    return;

                if (this._flex.Rows[e.Row]["CD_ITEM_BEFORE"] == null &&
                    this._flex.Rows[e.Row]["CD_ITEM_AFTER"] == null) return;

                CellStyle cellStyle = this._flex.Rows[e.Row].Style;

                if (cellStyle == null)
                {
                    if (this._flex.Rows[e.Row]["CD_ITEM_BEFORE"].ToString() == this._flex.Rows[e.Row]["CD_ITEM_AFTER"].ToString())
                        this._flex.Rows[e.Row].Style = this._flex.Styles["미입력"];
                    else
                        this._flex.Rows[e.Row].Style = this._flex.Styles["입력"];
                }
                else if (cellStyle.Name == "미입력")
                {
                    if (this._flex.Rows[e.Row]["CD_ITEM_BEFORE"].ToString() != this._flex.Rows[e.Row]["CD_ITEM_AFTER"].ToString())
                        this._flex.Rows[e.Row].Style = this._flex.Styles["입력"];
                }
                else if (cellStyle.Name == "입력")
                {
                    if (this._flex.Rows[e.Row]["CD_ITEM_BEFORE"].ToString() == this._flex.Rows[e.Row]["CD_ITEM_AFTER"].ToString())
                        this._flex.Rows[e.Row].Style = this._flex.Styles["미입력"];
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P42_CD_FIELD2 = "009";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx재고코드_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P42_CD_FIELD2 = "009";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn조건조기화_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("NM_COLUMN");
                dt.Columns.Add("YN_NOT_LIKE");
                dt.Columns.Add("DC_CONDITION");

                this._flex조건.Binding = dt;

                this.조건추가("WARNING", false, string.Empty);
                this.조건추가("WARNING", true, string.Empty);

                this.조건추가("NM_SUBJECT1", false, string.Empty);
                this.조건추가("NM_SUBJECT1", false, string.Empty);
                this.조건추가("NM_SUBJECT1", false, string.Empty);

                this.조건추가("NM_SUBJECT1", true, string.Empty);
                this.조건추가("NM_SUBJECT1", true, string.Empty);
                this.조건추가("NM_SUBJECT1", true, string.Empty);

                this.조건추가("CD_ITEM_PARTNER1", false, string.Empty);
                this.조건추가("CD_ITEM_PARTNER1", false, string.Empty);
                this.조건추가("CD_ITEM_PARTNER1", false, string.Empty);

                this.조건추가("CD_ITEM_PARTNER1", true, string.Empty);
                this.조건추가("CD_ITEM_PARTNER1", true, string.Empty);
                this.조건추가("CD_ITEM_PARTNER1", true, string.Empty);

                this.조건추가("NM_ITEM_PARTNER1", false, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", false, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", false, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", false, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", false, string.Empty);

                this.조건추가("NM_ITEM_PARTNER1", true, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", true, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", true, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", true, string.Empty);
                this.조건추가("NM_ITEM_PARTNER1", true, string.Empty);

                this.조건추가("CD_ITEM1", false, string.Empty);
                this.조건추가("CD_ITEM1", true, string.Empty);

                this.조건추가("NO_FILE1", false, string.Empty);
                this.조건추가("NO_FILE1", true, string.Empty);

                this.조건추가("CD_SPEC1", false, string.Empty);
                this.조건추가("CD_SPEC1", true, string.Empty);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn재고코드적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.DataTable.Select("S = 'Y'").Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in this._flex.DataTable.Select("S = 'Y'"))
                    {
                        if (this.chk재고코드갱신.Checked)
                            dr["CD_ITEM_AFTER"] = this.ctx재고코드.CodeValue;
                        else if (!this.chk재고코드갱신.Checked && (string.IsNullOrEmpty(dr["CD_ITEM_BEFORE"].ToString()) || dr["CLS_ITEM"].ToString() == "005"))
                            dr["CD_ITEM_AFTER"] = this.ctx재고코드.CodeValue;
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn재고코드적용.Text);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this.조건추가("NM_SUBJECT", false, string.Empty);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex조건.HasNormalRow) return;

                this._flex조건.GetDataRow(this._flex조건.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 조건추가(string 컬럼, bool 제외여부, string 조건)
        {
            DataRow dr;

            try
            {
                dr = this._flex조건.DataTable.NewRow();

                dr["NM_COLUMN"] = 컬럼;
                dr["YN_NOT_LIKE"] = (제외여부 == true ? "Y" : "N");
                dr["DC_CONDITION"] = 조건;

                this._flex조건.DataTable.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private string 쿼리생성(string 견적일자시작, string 견적일자종료)
        {
			string query = string.Empty,
				   where = string.Empty;

            try
            {
                where += Environment.NewLine + "AND QH.DTS_INSERT BETWEEN '" + 견적일자시작 + "000000" + "' AND '" + 견적일자종료 + "999999" + "'";

                if (!string.IsNullOrEmpty(this.bpc매출처.QueryWhereIn_Pipe))
                    where += Environment.NewLine + "AND QH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('" + this.bpc매출처.QueryWhereIn_Pipe + "'))";

                if (!string.IsNullOrEmpty(this.bpc호선.QueryWhereIn_Pipe))
                    where += Environment.NewLine + "AND QH.NO_IMO IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('" + this.bpc호선.QueryWhereIn_Pipe + "'))";

                if (!string.IsNullOrEmpty(this.bpc영업그룹.QueryWhereIn_Pipe))
                    where += Environment.NewLine + "AND QH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('" + this.bpc영업그룹.QueryWhereIn_Pipe + "'))";

                if (!string.IsNullOrEmpty(this.bpc매입처.QueryWhereIn_Pipe))
                    where += Environment.NewLine + "AND QL.CD_SUPPLIER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('" + this.bpc매입처.QueryWhereIn_Pipe + "'))";

                foreach (DataRow dr in this._flex조건.DataTable.Select("ISNULL(DC_CONDITION, '') <> ''"))
                {
                    if (dr["NM_COLUMN"].ToString() == "WARNING")
                    {
                        this.경고마스터조건생성(ref where, dr["YN_NOT_LIKE"].ToString(), dr["DC_CONDITION"].ToString());
                    }
                    else
                    {
                        if (dr["YN_NOT_LIKE"].ToString() == "Y")
                            where += Environment.NewLine + "AND (QL." + dr["NM_COLUMN"].ToString() + " IS NULL OR " + "QL." + dr["NM_COLUMN"].ToString() + " NOT LIKE '%" + dr["DC_CONDITION"].ToString().Replace("'", "''") + "%')";
                        else
                            where += Environment.NewLine + "AND QL." + dr["NM_COLUMN"].ToString() + " LIKE '%" + dr["DC_CONDITION"].ToString().Replace("'", "''") + "%'";
                    }
                }

                query = @"SELECT 'N' AS S,
						         QH.CD_COMPANY,
						         QH.NO_FILE,
						         QH.NO_IMO,
						         (SELECT LN_PARTNER 
						  		  FROM MA_PARTNER MP WITH(NOLOCK)
						  		  WHERE MP.CD_COMPANY = QH.CD_COMPANY
						  		  AND MP.CD_PARTNER = QH.CD_PARTNER) AS NM_PARTNER,
						         MH.NO_HULL,
						         MH.NM_VESSEL,
						  	     MH.DT_SHIP_DLV,
                                 MH.DC_SHIPBUILDER,
						         (SELECT LN_PARTNER 
						  		  FROM MA_PARTNER MP WITH(NOLOCK)
						  		  WHERE MP.CD_COMPANY = QL.CD_COMPANY
						  		  AND MP.CD_PARTNER = QL.CD_SUPPLIER) AS NM_SUPPLIER, 
						         QL.NO_LINE,
						         QL.NO_DSP, 
						         QL.NM_SUBJECT, 
						         QL.CD_ITEM_PARTNER, 
						         QL.NM_ITEM_PARTNER,
						         MI.CLS_ITEM,
						         (SELECT NM_SYSDEF 
						  	      FROM MA_CODEDTL MC WITH(NOLOCK)
						  		  WHERE MC.CD_COMPANY = MI.CD_COMPANY 
						  		  AND MC.CD_FIELD = 'MA_B000010' 
						  		  AND MC.CD_SYSDEF = MI.CLS_ITEM) AS NM_CLS_ITEM,
						         QL.QT_QTN,
						         QL.UM_KR_P,
						         QL.LT,
						         ISNULL(QL.CD_ITEM, '') AS CD_ITEM_BEFORE,
						         ISNULL(QL.CD_ITEM, '') AS CD_ITEM_AFTER,
                                 ISNULL(QL.CD_ITEM_SUB, '') AS CD_ITEM_SUB_BEFORE,
						         ISNULL(QL.CD_ITEM_SUB, '') AS CD_ITEM_SUB_AFTER,
						         QL.WARNING,
						         (SELECT NM_USER 
						  		  FROM MA_USER MU WITH(NOLOCK)
						  		  WHERE MU.CD_COMPANY = QL.CD_COMPANY 
						  		  AND MU.ID_USER = QL.ID_UPDATE) AS NM_UPDATE,
						         QL.DTS_UPDATE,
                                 (CASE WHEN EXISTS (SELECT 1 
                                                    FROM CZ_MA_HULL_VENDOR_ITEM VI
                                                    WHERE VI.NO_IMO = QH.NO_IMO
                                                    AND VI.CD_VENDOR = '" + (this.ctx기자재매입처.CodeValue == null ? string.Empty : this.ctx기자재매입처.CodeValue.ToString()) + @"') THEN 'Y' ELSE 'N' END) AS YN_PARTNER_ITEM,
                                 (SELECT QH.DC_RMK_QTN 
                                  FROM CZ_PU_QTNH QH WITH(NOLOCK)
                                  WHERE QH.CD_COMPANY = QL.CD_COMPANY
                                  AND QH.NO_FILE = QL.NO_FILE
                                  AND QH.CD_PARTNER = QL.CD_SUPPLIER) AS DC_RMK_QTN,
                                 (SELECT QL1.DC_RMK 
                                  FROM CZ_PU_QTNL QL1 WITH(NOLOCK)
                                  WHERE QL1.CD_COMPANY = QL.CD_COMPANY
                                  AND QL1.NO_FILE = QL.NO_FILE
                                  AND QL1.NO_LINE = QL.NO_LINE
                                  AND QL1.CD_PARTNER = QL.CD_SUPPLIER) AS DC_RMK
						  FROM CZ_SA_QTNH QH WITH(NOLOCK)
						  JOIN (SELECT QL.CD_COMPANY,
						  			   QL.NO_FILE,
						  			   QL.NO_LINE,
						  			   QL.NO_DSP,
						  			   QL.NM_SUBJECT,
						  			   QL.CD_ITEM_PARTNER,
						  			   QL.NM_ITEM_PARTNER,
						  			   QL.CD_ITEM,
                                       QL.CD_ITEM_SUB,
						  			   QL.CD_SUPPLIER,
						  			   QL.QT_QTN,
						  			   QL.UM_KR_P,
						  			   QL.LT,
						  			   (' ' + NM_SUBJECT + ' ') AS NM_SUBJECT1,
									   (' ' + CD_ITEM_PARTNER + ' ') AS CD_ITEM_PARTNER1,
									   (' ' + NM_ITEM_PARTNER + ' ') AS NM_ITEM_PARTNER1,
									   (' ' + CD_ITEM + ' ') AS CD_ITEM1,
									   (' ' + NO_FILE + ' ') AS NO_FILE1,
                                       (' ' + CD_SPEC + ' ') AS CD_SPEC1,
									   (' ' + ISNULL(NM_SUBJECT, '') + ' ' + ISNULL(CD_ITEM_PARTNER, '') + ' ' + ISNULL(NM_ITEM_PARTNER, '') + ' ' + ISNULL(CD_ITEM, '') + ' ' + ISNULL(CD_SPEC, '') + ' ') AS WARNING,
						  			   QL.ID_UPDATE,
						  			   QL.DTS_UPDATE
						  	    FROM CZ_SA_QTNL QL WITH(NOLOCK, INDEX(PK_CZ_SA_QTNL))) QL 
						  ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
						  LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = QH.NO_IMO
						  LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = QL.CD_COMPANY AND MI.CD_ITEM = QL.CD_ITEM
						  WHERE QH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + where;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return query;
        }

        private void 경고마스터조건생성(ref string 쿼리, string 제외여부, string 조건)
        {
            List<string> tempList;
            string[] temp;
            string 조건1;
            
            try
            {
                tempList = new List<string>();

                foreach (string 조건2 in 조건.Split('['))
                {
                    tempList.Add("[" + 조건2.Split(']')[0].ToString() + "]");
                }

                foreach (string temp1 in tempList.Distinct())
                {
                    조건 = 조건.Replace(temp1, temp1.Replace(",", "!@#"));
                }

                temp = 조건.Split(',');

                for (int index = 0; index < temp.Length; index++)
                {
                    temp[index] = temp[index].Replace("!@#", ",");
                }

                if (제외여부 == "Y")
                {
                    foreach (string str in temp)
                    {
                        조건1 = str.Trim().Replace("'", "''");
                        쿼리 += Environment.NewLine + @"AND QL.WARNING NOT LIKE '%" + 조건1 + "%'";
                    }
                }
                else
                {
                    쿼리 += Environment.NewLine + @"AND (";

                    for (int index = 0; index < temp.Length; index++)
                    {
                        조건1 = temp[index].Trim().Replace("'", "''");

                        if (index == 0)
                            쿼리 += "(";    
                        else
                            쿼리 += Environment.NewLine + "OR (";

                        bool 첫번째여부 = true;

                        foreach (string 조건2 in 조건1.Split(' ').Where(x => !string.IsNullOrEmpty(x.Trim())))
                        {
                            if (첫번째여부 == true)
                            {
                                쿼리 += "QL.WARNING LIKE '%" + 조건2 + "%'";
                                첫번째여부 = false;
                            }
                            else
                                쿼리 += Environment.NewLine + "AND QL.WARNING LIKE '%" + 조건2 + "%'";
                        }

                        쿼리 += ")";
                    }

                    쿼리 += ")";
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void chk자동줄맞춤_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk자동줄맞춤.Checked)
                {
                    this._flex.AllowResizing = AllowResizingEnum.BothUniform;
                    this._flex.AutoSizeRows();
                }
                else
                    this._flex.AllowResizing = AllowResizingEnum.Both;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}
