using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;
using System.Drawing;

namespace cz
{
    public partial class P_CZ_SA_SOSCH1_WINTEC : PageBase
    {
        #region 초기화 & 전역변수
        private P_CZ_SA_SOSCH1_WINTEC_BIZ _biz = new P_CZ_SA_SOSCH1_WINTEC_BIZ();
        private bool b수량권한 = true;
        private bool b단가권한 = true;
        private bool b금액권한 = true;
        private bool chkDate
        {
            get
            {
                return Checker.IsValid(this.dtp수주일자, true, this.cbo수주일자.Text);
            }
        }

        public P_CZ_SA_SOSCH1_WINTEC()
        {
            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
                    StartUp.Certify(this);

                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
        {
            object[] args = e.Args;

            this.InitPaint();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            DataTable dataTable = BASIC.MFG_AUTH("P_SA_SO_MNG");

            if (dataTable.Rows.Count > 0)
            {
                this.b수량권한 = !(dataTable.Rows[0]["YN_QT"].ToString() == "Y");
                this.b단가권한 = !(dataTable.Rows[0]["YN_UM"].ToString() == "Y");
                this.b금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");
            }

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex품목별, this._flex현대시리얼번호 };
            this._flex품목별.DetailGrids = new FlexGrid[] { this._flex현대시리얼번호 };

            #region 품목별
            this._flex품목별.BeginSetting(1, 1, false);

            this._flex품목별.SetCol("CD_PARTNER", "거래처코드", 100);
            this._flex품목별.SetCol("LN_PARTNER", "거래처명", 100);
            this._flex품목별.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            this._flex품목별.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목별.SetCol("CD_USERDEF3", "엔진타입", 100);
            this._flex품목별.SetCol("CD_ITEM", "품목코드", 100);
            this._flex품목별.SetCol("NM_ITEM", "품목명", 100);
            this._flex품목별.SetCol("TXT_USERDEF6", "호선번호", 100);
            this._flex품목별.SetCol("NO_SO", "수주번호", 100);
            this._flex품목별.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex품목별.SetCol("TXT_USERDEF7", "도면번호(수주)", 100);
            this._flex품목별.SetCol("DC_RMK_TEXT", "텍스트비고1", 120);

            if (this.b수량권한)
            {
                this._flex품목별.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flex품목별.SetCol("QT_GIR", "의뢰수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flex품목별.SetCol("QT_GI", "출고수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flex품목별.SetCol("QT_REMAIN", "미납수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            }

            this._flex품목별.SetCol("CD_USERDEF1", "선급검사기관1", 100);
            this._flex품목별.SetCol("CD_USERDEF2", "선급검사기관2", 100);
            this._flex품목별.SetCol("DT_EXPECT", "소요납기(최초)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목별.SetCol("DT_DUEDATE", "관리납기", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목별.SetCol("DT_REQGI", "수정소요납기(최종)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목별.SetCol("DT_IO", "납품일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목별.SetCol("DT_DELAY", "납기지연일수", 100);
            this._flex품목별.SetCol("STA_SO", "진행상태", 100);
            this._flex품목별.SetCol("TXT_USERDEF5", "납품장소", 100);
            this._flex품목별.SetCol("DC_RMK1", "비고", 100);
            this._flex품목별.SetCol("DTS_INSERT", "입력일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목별.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            if (this.b단가권한)
                this._flex품목별.SetCol("UM_SO", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            if (this.b금액권한)
            {
                this._flex품목별.SetCol("AM_SO", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex품목별.SetCol("AM_WONAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                this._flex품목별.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
                this._flex품목별.SetCol("AM_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            }

            this._flex품목별.SetCol("NO_LC", "L/C번호", 100);
            this._flex품목별.SetCol("TXT_USERDEF3", "자재번호", 100);
            this._flex품목별.SetCol("YN_EBOM", "EBOM여부", 60, false, CheckTypeEnum.Y_N);
            this._flex품목별.SetCol("YN_BOM", "BOM여부", 60, false, CheckTypeEnum.Y_N);

            this._flex품목별.SetDataMap("CD_USERDEF3", MA.GetCode("CZ_WIN0003", true), "CODE", "NAME"); // 엔진타입
            this._flex품목별.SetDataMap("CD_USERDEF1", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관1
            this._flex품목별.SetDataMap("CD_USERDEF2", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관2

            this._flex품목별.SettingVersion = "0.0.0.1";
            this._flex품목별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex품목별.Styles.Add("납기일자변경").ForeColor = Color.Red;
            this._flex품목별.Styles.Add("납기일자변경").BackColor = Color.White;
            this._flex품목별.Styles.Add("납기일자동일").ForeColor = Color.Black;
            this._flex품목별.Styles.Add("납기일자동일").BackColor = Color.White;
            #endregion

            #region 시리얼
            this._flex현대시리얼번호.BeginSetting(1, 1, false);

            this._flex현대시리얼번호.SetCol("NO_SERIAL", "시리얼", 100);

            this._flex현대시리얼번호.SettingVersion = "0.0.0.1";
            this._flex현대시리얼번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();

            this.dtp수주일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.dtp수주일자.EndDateToString = Global.MainFrame.GetStringToday;

            DataSet comboData = this.GetComboData("N;MA_PLANT", "S;SA_B000016");

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            if (comboData.Tables[0] != null && comboData.Tables[0].Rows.Count > 0)
                this.cbo공장.SelectedIndex = 0;

            this.cbo수주상태.DataSource = comboData.Tables[1];
            this.cbo수주상태.DisplayMember = "NAME";
            this.cbo수주상태.ValueMember = "CODE";

            DataTable dataTable = comboData.Tables[0].Clone();
            DataRow row1 = dataTable.NewRow();
            row1["CODE"] = "SO";
            row1["NAME"] = this.DD("수주일자");
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["CODE"] = "DU";
            row2["NAME"] = this.DD("납기일자");
            dataTable.Rows.Add(row2);
            DataRow row3 = dataTable.NewRow();
            row3["CODE"] = "GI";
            row3["NAME"] = this.DD("납품일자");
            dataTable.Rows.Add(row3);
            DataRow row4 = dataTable.NewRow();
            row4["CODE"] = "IP";
            row4["NAME"] = this.DD("입력일자");
            dataTable.Rows.Add(row4);

            this.cbo수주일자.DataSource = dataTable;
            this.cbo수주일자.DisplayMember = "NAME";
            this.cbo수주일자.ValueMember = "CODE";

            this._flex품목별.SetDataMap("STA_SO", comboData.Tables[1], "CODE", "NAME");

            string currentGrantMenu = Global.MainFrame.CurrentGrantMenu;
            if (currentGrantMenu == "B" || currentGrantMenu == "S" || (currentGrantMenu == "C" || currentGrantMenu == "D") || currentGrantMenu == "E")
                this.cbo공장.Enabled = false;
        }

        private void InitEvent()
        {
            this.ctx품목from.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx품목to.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx품목from.QueryAfter += new BpQueryHandler(this.ctx품목from_QueryAfter);

            this._flex품목별.OwnerDrawCell += _flex품목별_OwnerDrawCell;

            this._flex품목별.AfterRowChange += new RangeEventHandler(this._flex품목별_AfterRowChange);
        }

        private void _flex품목별_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (!this._flex품목별.HasNormalRow) return;
                if (e.Row < this._flex품목별.Rows.Fixed || e.Col < this._flex품목별.Cols.Fixed) return;

                CellStyle cellStyle = this._flex품목별.Rows[e.Row].Style;


                if (D.GetString(this._flex품목별[e.Row, "DT_DUEDATE"]) != string.Empty &&
                    D.GetString(this._flex품목별[e.Row, "DT_REQGI"]) != string.Empty &&
                    D.GetString(this._flex품목별[e.Row, "DT_DUEDATE"]) != D.GetString(this._flex품목별[e.Row, "DT_REQGI"]))
                {
                    if (cellStyle == null || cellStyle.Name != "납기일자변경")
                        this._flex품목별.Rows[e.Row].Style = this._flex품목별.Styles["납기일자변경"];
                }
                else
                {
                    if (cellStyle == null || cellStyle.Name != "납기일자동일")
                        this._flex품목별.Rows[e.Row].Style = this._flex품목별.Styles["납기일자동일"];
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;
            object[] obj;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch() || !this.chkDate)
                    return;

                obj = new object[] { this.LoginInfo.CompanyCode,
                                     D.GetString(this.cbo공장.SelectedValue),
                                     this.dtp수주일자.StartDateToString,
                                     this.dtp수주일자.EndDateToString,
                                     this.ctx거래처.CodeValue,
                                     D.GetString(this.cbo수주상태.SelectedValue),
                                     this.txt수주번호.Text,
                                     D.GetString(this.ctx품목from.CodeValue),
                                     D.GetString(this.ctx품목to.CodeValue),
                                     this.txt호선번호.Text,
                                     this.txt거래처po번호.Text,
                                     this.txtLC번호.Text };

                dt = this._biz.Search(this.cbo수주일자.SelectedValue.ToString(),obj);

                this._flex품목별.Binding = dt;

                if (!this._flex품목별.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex품목별_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)null;
                string key1, key2;

                key1 = this._flex품목별["NO_SO"].ToString();
                key2 = this._flex품목별["SEQ_SO"].ToString();
                string filter = "NO_SO = '" + key1 + "'AND SEQ_SO = '" + key2 + "'";

                if (this._flex품목별.DetailQueryNeed)
                    dt = this._biz.SearchSerial(new object[] { this.LoginInfo.CompanyCode,
                                                               key1,
                                                               key2});

                this._flex현대시리얼번호.BindingAdd(dt, filter);

                #region MAN시리얼번호
                DataSet ds = this._biz.SearchTrust(new object[] { this.LoginInfo.CompanyCode,
                                                                  key1,
                                                                  key2,
                                                                  this._flex품목별["CD_ITEM"].ToString() });

                if (ds != null && ds.Tables.Count == 2)
                {
                    this._flexMAN시리얼번호.BeginSetting(2, 1, false);
                    this._flexMAN시리얼번호.Cols.Count = 1;

                    this._flexMAN시리얼번호.SetCol("IDX", "순번", 100);

                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        this._flexMAN시리얼번호.SetCol(dr["CD_ITEM"].ToString(), "시리얼", 100);
                        this._flexMAN시리얼번호[0, this._flexMAN시리얼번호.Cols[dr["CD_ITEM"].ToString()].Index] = dr["NM_ITEM"].ToString();

                        this._flexMAN시리얼번호.SetCol(dr["CD_ITEM"].ToString() + "-1", "ID번호", 100);
                        this._flexMAN시리얼번호[0, this._flexMAN시리얼번호.Cols[dr["CD_ITEM"].ToString() + "-1"].Index] = dr["NM_ITEM"].ToString();
                    }

                    this._flexMAN시리얼번호.AllowCache = false;
                    this._flexMAN시리얼번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                    this._flexMAN시리얼번호.Binding = ds.Tables[0];
                }
                else
                {
                    this._flexMAN시리얼번호.BeginSetting(2, 1, false);
                    this._flexMAN시리얼번호.Cols.Count = 1;

                    this._flexMAN시리얼번호.SetCol("IDX", "순번", 100, false);

                    this._flexMAN시리얼번호.AllowCache = false;
                    this._flexMAN시리얼번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                    this._flexMAN시리얼번호.Binding = null;
                }
                #endregion
            }
			catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void ctx품목from_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (!(this.ctx품목to.CodeValue == ""))
                    return;

                this.ctx품목to.CodeValue = this.ctx품목from.CodeValue;
                this.ctx품목to.CodeName = this.ctx품목from.CodeName;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case HelpID.P_MA_PITEM_SUB:
                    e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                    break;
                case HelpID.P_SA_TPSO_SUB1:
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                    break;
            }
        }
        #endregion
    }
}
