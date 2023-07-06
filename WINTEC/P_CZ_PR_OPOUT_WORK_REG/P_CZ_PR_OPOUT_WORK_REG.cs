using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.PR;
using Duzon.Windows.Print;
using prd;
using pur;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace cz
{
    public partial class P_CZ_PR_OPOUT_WORK_REG : PageBase
    {
        #region 초기화 & 전역변수
        private P_CZ_PR_OPOUT_WORK_REG_BIZ _biz = new P_CZ_PR_OPOUT_WORK_REG_BIZ();
        private string strErrCol;
        private string YN_PR_MNG_LOT;
        private DataTable dtWork;
        private DataRow[] drs;
        private DataTable _dtReject;
        private DataTable _dtLotItem;
        private DataTable _dtSERL;
        private Hashtable htDetailQueryCollection;
        private DataTable LOT관리항목DT;
        private P_CZ_PR_OPOUT_WORK_REG.stDetailQuery st;
        private DataTable _dtMatl;
        private DataTable _dtLotMatl;
        private DataTable _dtInsp;
        private bool bSaveCalled = false;
        private bool bGridrowChanging = false;
        private bool bDetailGridrowChanging = true;
        private string sChcoef_YN = string.Empty;
        private string Use_NextOP_sub;
        private bool b실적수량초과여부 = false;
        private string s조회기준 = BASIC.GetMAEXC_Menu(nameof(P_CZ_PR_OPOUT_WORK_REG), "PR_A00000001");
        private string s최종공정LOT필수여부 = BASIC.GetMAEXC("작업실적_최종공정LOT_필수여부");

        public P_CZ_PR_OPOUT_WORK_REG()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.splitContainer1.SplitterDistance = 1824;

            DataSet comboData = this.GetComboData("NC;MA_PLANT");
            new SetControl().SetCombobox(this.cbo공장, comboData.Tables[0]);
            if (comboData != null)
            {
                if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                    this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
                else if (this.cbo공장.Items.Count > 0)
                    this.cbo공장.SelectedIndex = 0;
            }
            this._flexM.SetDataMap("FG_WO", MA.GetCode("PR_0000007"), "CODE", "NAME"); // 작업지시구분
            this._flexM.SetDataMap("ST_WO", MA.GetCode("PR_0000006"), "CODE", "NAME"); // 작업상태
            this._flexM.SetDataMap("ST_OP", MA.GetCode("PR_0000009"), "CODE", "NAME"); // 공정상태
            this._flexD.SetDataMap("ST_OP", MA.GetCode("PR_0000009"), "CODE", "NAME"); // 공정상태

            this.sChcoef_YN = ComFunc.전용코드("공정외주임가공단가 단위 변환 사용");
            if (this.sChcoef_YN == string.Empty)
                this.sChcoef_YN = "000";
            else if (this.sChcoef_YN == "001")
            {
                this._flexD.SetDataMap("UNIT_IM", MA.GetCode("MA_B000004", true), "CODE", "NAME"); // 단위
                this._flexD.SetDataMap("UNIT_CH", MA.GetCode("MA_B000004", true), "CODE", "NAME"); // 단위
            }
            this._flexD.SetDataMap("CD_USERDEF1", MA.GetCode("PR_0000094", true), "CODE", "NAME"); // 작업실적등록사용자정의1(CD_USERDEF1)
            this._flexD.SetDataMap("CD_USERDEF2", MA.GetCode("PR_0000095", true), "CODE", "NAME"); // 작업실적등록사용자정의2(CD_USERDEF2)
            this._flexD.SetDataMap("CD_USERDEF3", MA.GetCode("PR_0000096", true), "CODE", "NAME"); // 작업실적등록사용자정의3(CD_USERDEF3)
            this.dtp발주기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
            this.dtp발주기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            this.dtp작업기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
            this.dtp작업기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            this.chk재작업잔량여부.Checked = Settings1.Default.재작업잔량여부;

            if (Pr_Global.bMfg_AuthH_YN)
            {
                DataSet dataSet = this._biz.Search_AUTH(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       string.Empty,
                                                                       Global.MainFrame.LoginInfo.EmployeeNo,
                                                                       this.cbo공장.SelectedValue.ToString(),
                                                                       Global.SystemLanguage.MultiLanguageLpoint });

                if (dataSet != null && dataSet.Tables[7].Rows.Count != 0)
                {
                    foreach (DataRow row in dataSet.Tables[7].Rows)
                    {
                        if (!(D.GetString(row["FG_USE"]) == "") && !(D.GetString(row["FG_USE"]) == "N"))
                            this.bpc오더형태.AddItem2(D.GetString(row["CD_AUTH"]), D.GetString(row["NM_AUTH"]));
                    }
                }
                if (dataSet != null && dataSet.Tables[6].Rows.Count != 0)
                {
                    foreach (DataRow row in dataSet.Tables[6].Rows)
                    {
                        if (!(D.GetString(row["FG_USE"]) == "") && !(D.GetString(row["FG_USE"]) == "N") && (!(D.GetString(row["TP_WC"]) == "") && !(D.GetString(row["TP_WC"]) != "002")))
                            this.bpc작업장.AddItem2(D.GetString(row["CD_AUTH"]), D.GetString(row["NM_AUTH"]));
                    }
                }
            }

            if (!MA.ServerKey(false, "DIIN"))
                return;
            this._flexM.KeyPress += new KeyPressEventHandler(this._flex_Z_DIIN_KeyPress);
            this._flexD.KeyPress += new KeyPressEventHandler(this._flex_Z_DIIN_KeyPress);

            DataTable code1 = MA.GetCode("PR_0000098");
            for (int index = 1; index <= code1.Rows.Count; ++index)
            {
                string str1 = "CD_USERDEF" + D.GetString(index);
                if (code1.Rows.Count < index)
                    this._flexM.Cols[str1].Visible = false;
                else if (!(D.GetString(code1.Rows[index - 1]["CD_FLAG2"]) == "N"))
                {
                    string str2 = D.GetString(code1.Rows[index - 1]["NAME"]);
                    this._flexM.Cols[str1].Caption = str2;
                    this._flexM.Cols[str1].Visible = true;
                    DataTable code2 = MA.GetCode("PR_0000" + D.GetString(index + 98).PadLeft(3, '0'), true);
                    if (code2.Rows.Count > 1)
                        this._flexM.SetDataMap(str1, code2.Copy(), "CODE", "NAME");
                }
            }
            DataTable code3 = MA.GetCode("PR_0000104");
            for (int index = 1; index <= code3.Rows.Count; ++index)
            {
                string columnName = "NUM_USERDEF" + D.GetString(index);
                if (code3.Rows.Count < index)
                    this._flexM.Cols[columnName].Visible = false;
                else if (!(D.GetString(code3.Rows[index - 1]["CD_FLAG2"]) == "N"))
                {
                    string str = D.GetString(code3.Rows[index - 1]["NAME"]);
                    this._flexM.Cols[columnName].Caption = str;
                    this._flexM.Cols[columnName].Visible = true;
                }
            }
            DataTable code4 = MA.GetCode("MA_B000147");
            for (int index = 1; index <= code4.Rows.Count; ++index)
            {
                string str = D.GetString(code4.Rows[index - 1]["NAME"]);
                this._flexD.Cols["CD_USERDEF" + D.GetString(index)].Caption = str;
                this._flexD.Cols["CD_USERDEF" + D.GetString(index)].Visible = true;
            }
            DataTable code5 = MA.GetCode("MA_B000149");
            for (int index = 1; index <= code5.Rows.Count; ++index)
            {
                string str = D.GetString(code5.Rows[index - 1]["NAME"]);
                this._flexD.Cols["TXT_USERDEF" + D.GetString(index)].Caption = str;
                this._flexD.Cols["TXT_USERDEF" + D.GetString(index)].Visible = true;
            }
            DataTable code6 = MA.GetCode("MA_B000150");
            for (int index = 1; index <= 10; ++index)
            {
                string columnName = "NUM_USERDEF" + D.GetString(index);
                if (code6.Rows.Count < index)
                {
                    this._flexD.Cols[columnName].Visible = false;
                }
                else
                {
                    string str = D.GetString(code6.Rows[index - 1]["NAME"]);
                    this._flexD.Cols[columnName].Caption = str;
                    this._flexD.Cols[columnName].Visible = true;
                }
            }
            DataTable code7 = MA.GetCode("PR_0000107");
            for (int index = 1; index <= 3; ++index)
            {
                string columnName = "ROUT_NUM_USERDEF" + D.GetString(index);
                if (code7.Rows.Count < index)
                {
                    this._flexD.Cols[columnName].Visible = false;
                }
                else
                {
                    string str = D.GetString(code7.Rows[index - 1]["NAME"]);
                    this._flexD.Cols[columnName].Caption = str;
                    this._flexD.Cols[columnName].Visible = true;
                }
            }
            DataTable code8 = MA.GetCode("PR_0000108");
            for (int index = 1; index <= 3; ++index)
            {
                string columnName = "ROUT_TXT_USERDEF" + D.GetString(index);
                if (code8.Rows.Count < index)
                {
                    this._flexD.Cols[columnName].Visible = false;
                }
                else
                {
                    string str = D.GetString(code8.Rows[index - 1]["NAME"]);
                    this._flexD.Cols[columnName].Caption = str;
                    this._flexD.Cols[columnName].Visible = true;
                }
            }

            this.cbo공장_SelectionChangeCommitted(null, null);
            this.oneGrid1.UseCustomLayout = true;
            this._bpPnl_plant.IsNecessaryCondition = true;
            this._bpPnl_발주기간.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();

            this.chk작업지시비고적용여부.Checked = Settings1.Default.Wo_DcRmk_Apply_YN;
            if (!(this.s조회기준 == "100"))
                return;
            this.bpPnl마감여부.Visible = true;

            this._dtReject = new DataTable();
            this._dtLotItem = new DataTable();
            this._dtSERL = new DataTable();
            this._dtMatl = new DataTable();
            this._dtLotMatl = new DataTable();
            this._dtInsp = new DataTable();
            this.htDetailQueryCollection = new Hashtable();
            this.st = new P_CZ_PR_OPOUT_WORK_REG.stDetailQuery();
            this.Use_NextOP_sub = ComFunc.전용코드("작업실적등록-다음공정변경기능사용");
        }

        private void _flex_Z_DIIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid) || !flexGrid.HasNormalRow || e.KeyChar.ToString() != "\r")
                    return;
                if (flexGrid == this._flexM)
                {
                    int row = this._flexD.FindRow(D.GetString(this._flexM["CD_OP"]), this._flexD.Rows.Fixed, this._flexD.Cols["CD_OP"].Index, false, true, true);
                    if (row < 0)
                        row = this._flexD.Rows.Fixed;
                    this._flexD.Focus();
                    this._flexD.Select(row, this._flexD.Cols["QT_WORK"].Index);
                }
                else if (flexGrid == this._flexD)
                {
                    this._flexM.Select(this._flexM.Row, this._flexM.Cols["NO_PO"].Index);
                    this._flexM.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo공장.Items.Count > 0)
                {
                    object[] objArray = new object[] { this.LoginInfo.CompanyCode,
                                                       this.cbo공장.SelectedValue.ToString() };
                    this.YN_PR_MNG_LOT = this._biz.SELECT_YN_PR_MNG_LOT(objArray);
                    DataSet plantCfg = Pr_ComFunc.Get_Plant_Cfg(objArray);
                    if (plantCfg.Tables[1].Rows.Count < 1 || !(plantCfg.Tables[1].Rows[0]["YN_QT_WORK"].ToString() == "Y"))
                        return;
                    this.b실적수량초과여부 = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexM.Enabled = false;
                this._flexD.Enabled = false;
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexD, this._flexM };

            this._flexM.DetailGrids = new FlexGrid[] { this._flexD };

            #region _flexM
            this._flexM.BeginSetting(1, 1, false);

            this._flexM.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flexM.SetCol("NO_PO", "발주번호", 120, false);
            this._flexM.SetCol("NO_POLINE", "발주항번", 60, false, typeof(decimal));
            this._flexM.SetCol("DT_PO", "발주일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("DC_RMK_H", "비고", 120, false);
            this._flexM.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexM.SetCol("FG_WO", "작업지시구분", 90, false);
            this._flexM.SetCol("ST_WO", "상태", 40, false);
            this._flexM.SetCol("CD_ITEM", "품목", 100, false);
            this._flexM.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexM.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexM.SetCol("STND_ITEM", "규격", 120, false);
            this._flexM.SetCol("UNIT_IM", "단위", 40, false);
            this._flexM.SetCol("TP_ROUT", "오더형태", 100, false);
            this._flexM.SetCol("NM_TP_WO", "오더형태명", 100, false);
            this._flexM.SetCol("DT_REL", "시작일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("DT_DUE", "종료일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("DT_DLV", "납기일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("QT_ITEM", "지시수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("QT_PO", "발주수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("WEIGHT", "중량", 80, true, typeof(decimal));
            this._flexM.Cols["WEIGHT"].Format = "#,###,###.####";
            this._flexM.SetCol("YN_LOT", "LOT사용유무", 20, false, CheckTypeEnum.Y_N);
            this._flexM.SetCol("NO_LOT", "LOT번호", 100, 50, false);
            this._flexM.SetCol("YN_SERL", "SERIAL사용유무", 20, false, CheckTypeEnum.Y_N);
            this._flexM.SetCol("UM_EX", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexM.SetCol("UM", "원화단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexM.SetCol("AM_EX", "외화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexM.SetCol("AM", "원화금액", 120, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_VAT", "부가세액", 120, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("CD_SL", "입고창고코드", 100, 50, false);
            this._flexM.SetCol("NM_SL", "입고창고명", 140, 50, false);
            this._flexM.SetCol("DC_RMK", "비고", 150, false, typeof(string));
            this._flexM.SetCol("NO_PJT", "프로젝트", 100, false);
            this._flexM.SetCol("NM_PJT", "프로젝트명", 120, false);
            this._flexM.SetCol("CD_PARTNER", "거래처", 100, false);
            this._flexM.SetCol("LN_PARTNER", "거래처명", 100, false);
            this._flexM.SetCol("CD_OP", "OP", 30);
            this._flexM.SetCol("NM_OP", "공정명", 100);
            this._flexM.SetCol("CD_WC", "작업장", 50);
            this._flexM.SetCol("NM_WC", "작업장명", 100);
            this._flexM.SetCol("ST_OP", "공정상태", 35);
            if (Config.MA_ENV.프로젝트사용)
                this._flexM.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexM.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexM.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexM.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            this._flexM.SetCol("DC_RMK_WO", "작업지시비고", false);
            this._flexM.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexM.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            this._flexM.SetCol("MAT_ITEM", "재질", false);
            this._flexM.SetCol("NM_MAKER", "Maker", false);
            this._flexM.SetCol("BARCODE", "BARCODE", false);
            this._flexM.SetCol("NO_MODEL", "모델번호", false);
            this._flexM.SetCol("CD_USERDEF1", "CD_USERDEF1", false);
            this._flexM.SetCol("CD_USERDEF2", "CD_USERDEF2", false);
            this._flexM.SetCol("CD_USERDEF3", "CD_USERDEF3", false);
            this._flexM.SetCol("CD_USERDEF4", "CD_USERDEF4", false);
            this._flexM.SetCol("CD_USERDEF5", "CD_USERDEF5", false);
            this._flexM.SetCol("NUM_USERDEF1", "NUM_USERDEF1", 80, false, typeof(decimal));
            this._flexM.SetCol("NUM_USERDEF2", "NUM_USERDEF2", 80, false, typeof(decimal));
            if (MA.ServerKey(false, "JIGLS"))
                this._flexM.SetCol("TXT_USERDEF1_WO", "규격변경", 80, false);
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM.Cols["NO_POLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.Cols["CD_PLANT"].Visible = false;
            this._flexM.Cols["CD_SL"].Visible = false;
            this._flexM.Cols["NM_SL"].Visible = false;

            this._flexM.SettingVersion = "0.0.0.1";
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region _flexD
            this._flexD.BeginSetting(1, 1, false);

            this._flexD.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flexD.SetCol("CD_OP", "OP", 30);
            this._flexD.SetCol("NM_OP", "공정명", 100);
            this._flexD.SetCol("CD_WC", "작업장", 50);
            this._flexD.SetCol("NM_WC", "작업장명", 100);
            this._flexD.SetCol("ST_OP", "상태", 35);
            this._flexD.SetCol("QT_WO", "지시수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            if (this.sChcoef_YN == "001")
            {
                this._flexD.SetCol("UNIT_IM", "지시단위", 100, false);
                this._flexD.SetCol("QT_CHCOEF", "발주변환단위수량", 120, false);
                this._flexD.SetCol("QT_PO", "발주변환수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.SetCol("UNIT_CH", "발주변환단위", 100, false);
                this._flexD.SetCol("OLD_QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            this._flexD.SetCol("QT_START", "입고수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_WO_WORK", "실적수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_REMAIN", "작업잔량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_MOVE", "이동수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_REWORKREMAIN", "재작업잔량", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            if (this.sChcoef_YN == "001")
            {
                this._flexD.SetCol("QT_WORK_CHCOEF", "실적입력량(변환)", 120, true, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.SetCol("QT_WORK_BAD_CHCOEF", "불량입력량(변환)", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            }
            this._flexD.SetCol("QT_OUTPO", "발주량(공정)", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("DT_WORK", "작업일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD.SetCol("NO_EMP", "담당자코드", 80, 10, true);
            this._flexD.SetCol("NM_KOR", "담당자명", 100, false);
            this._flexD.SetCol("NO_SFT", "SFT", 40, true);
            this._flexD.SetCol("NM_SFT", "SFT명", 100, false);
            this._flexD.SetCol("CD_EQUIP", "설비코드", 60, true);
            this._flexD.SetCol("NM_EQUIP", "설비명", 100, 100, true);
            this._flexD.SetCol("QT_WORK", "실적입력량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_REJECT", "불량입력량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("NO_LOT", "LOT번호", 100, 50, true, typeof(string));
            this._flexD.SetCol("DC_RMK1", "비고1", 100, 100, true);
            this._flexD.SetCol("DC_RMK2", "비고2", 100, 100, true);
            this._flexD.SetCol("YN_SUBCON", "외주여부", 60, false);
            this._flexD.SetCol("YN_FINAL", "마지막공정여부", 120, false);
            this._flexD.SetCol("NO_PO", "발주번호", 100, true);
            this._flexD.SetCol("NO_POLINE", "발주항번", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_PO", "발주량(발주별)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_OUTRCV", "실적수량(번호별)", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_OUT_REJECT", "불량수량(번호별)", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_OUT_REWORK", "재작업수량(번호별)", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_OUT_BAD", "불량처리수량(번호별)", 140, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_GOOD", "양품실적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("DT_LIMIT", "유효일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this.LOT관리항목DT = MA.GetCode("PU_C000079");
            if (this.LOT관리항목DT.Rows.Count != 0)
            {
                foreach (DataRow row in this.LOT관리항목DT.Rows)
                {
                    int num = D.GetInt(row["CODE"]);
                    string colName = "CD_MNG" + num;
                    System.Type colType = (System.Type)null;
                    FormatTpType colFormat = FormatTpType.NONE;
                    if (MA.ServerKey(false, "SDBIO", "SKPLASMA"))
                    {
                        if (num == 1)
                        {
                            colType = typeof(string);
                            colFormat = FormatTpType.YEAR_MONTH_DAY;
                        }
                    }
                    else if (MA.ServerKey(false, "WINPLUS"))
                    {
                        if (num == 6 || num == 7)
                        {
                            colType = typeof(decimal);
                            colFormat = FormatTpType.QUANTITY;
                        }
                        else if (num == 11)
                        {
                            colType = typeof(string);
                            colFormat = FormatTpType.YEAR_MONTH_DAY;
                        }
                    }
                    if (colType != null)
                        this._flexD.SetCol(colName, D.GetString(row["NAME"]), 80, true, colType, colFormat);
                    else
                        this._flexD.SetCol(colName, D.GetString(row["NAME"]), 100, 200, true, typeof(string));
                }
            }
            this._flexD.SetCol("TXT_USERDEF1", "TXT_USERDEF1", 100, true, typeof(string));
            this._flexD.SetCol("TXT_USERDEF2", "TXT_USERDEF2", 100, true, typeof(string));
            this._flexD.SetCol("TXT_USERDEF3", "TXT_USERDEF3", 100, true, typeof(string));
            this._flexD.SetCol("CD_USERDEF1", "CD_USERDEF1", 100, true);
            this._flexD.SetCol("CD_USERDEF2", "CD_USERDEF2", 100, true);
            this._flexD.SetCol("CD_USERDEF3", "CD_USERDEF3", 100, true);
            this._flexD.SetCol("NUM_USERDEF1", "NUM_USERDEF1", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF2", "NUM_USERDEF2", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF3", "NUM_USERDEF3", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF4", "NUM_USERDEF4", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF5", "NUM_USERDEF5", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF6", "NUM_USERDEF6", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF7", "NUM_USERDEF7", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF8", "NUM_USERDEF8", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF9", "NUM_USERDEF9", 100, true, typeof(decimal));
            this._flexD.SetCol("NUM_USERDEF10", "NUM_USERDEF10", 100, true, typeof(decimal));
            this._flexD.SetCol("ROUT_NUM_USERDEF1", "ROUT_NUM_USERDEF1", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("ROUT_NUM_USERDEF2", "ROUT_NUM_USERDEF2", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("ROUT_NUM_USERDEF3", "ROUT_NUM_USERDEF3", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("ROUT_TXT_USERDEF1", "ROUT_TXT_USERDEF1", 100, true, typeof(string));
            this._flexD.SetCol("ROUT_TXT_USERDEF2", "ROUT_TXT_USERDEF2", 100, true, typeof(string));
            this._flexD.SetCol("ROUT_TXT_USERDEF3", "ROUT_TXT_USERDEF3", 100, true, typeof(string));
            this._flexD.VerifyCompare(this._flexD.Cols["QT_WORK"], this._flexD.Cols["QT_REJECT"], OperatorEnum.GreaterOrEqual);
            if (!MA.ServerKey(false, "GOPT"))
                this._flexD.VerifyCompare(this._flexD.Cols["QT_WORK"], 0, OperatorEnum.Greater);
            string[] strArray = new string[] { "DT_WORK",
                                               "NO_EMP" };
            if (MA.ServerKey(false, "KEUNDAN"))
                strArray = new string[] { "DT_WORK",
                                          "NO_EMP",
                                          "DT_LIMIT",
                                          "CD_MNG1" };
            this._flexD.VerifyNotNull = strArray;
            this._flexD.SetCodeHelpCol("NO_EMP", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP",
                "NM_KOR",
                "CD_DEPT"
            }, new string[] { "NO_EMP", "NM_KOR", "CD_DEPT" }, new string[]
            {
                "NO_EMP",
                "NM_KOR",
                "CD_DEPT"
            }, ResultMode.SlowMode);
            this._flexD.SetCodeHelpCol("NO_SFT", "H_PR_SFT_SUB", ShowHelpEnum.Always, new string[]
            {
                "NO_SFT",
                "NM_SFT"
            }, new string[] { "NO_SFT", "NM_SFT" });
            this._flexD.SetCodeHelpCol("NM_EQUIP", "H_PR_EQUIP_SUB", ShowHelpEnum.Always, new string[]
            {
                "CD_EQUIP",
                "NM_EQUIP"
            }, new string[] { "CD_EQUIP", "NM_EQUIP" });
            this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.None, SumPositionEnum.None);
            if (this.LoginInfo.MngLot != "Y")
            {
                this._flexD.Cols["NO_LOT"].Visible = false;
                if (this.LOT관리항목DT.Rows.Count != 0)
                {
                    foreach (DataRow row in this.LOT관리항목DT.Rows)
                        this._flexD.Cols["CD_MNG" + D.GetInt(row["CODE"])].Visible = false;
                }
            }
            this._flexD.Cols["CD_PLANT"].Visible = false;
            this._flexD.Cols["NM_SFT"].Visible = false;
            this._flexD.Cols["CD_EQUIP"].Visible = false;
            this._flexD.Cols["YN_SUBCON"].Visible = false;
            this._flexD.Cols["YN_FINAL"].Visible = false;
            this._flexD.Cols["NO_PO"].Visible = false;
            this._flexD.Cols["NO_POLINE"].Visible = false;
            if (this.LoginInfo.MngLot != "Y")
                this._flexD.Cols["NO_LOT"].Visible = false;

            this._flexD.SettingVersion = "0.0.0.1";
            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region _flexID
            this._flexID.BeginSetting(1, 1, false);

            this._flexID.SetCol("SEQ_WO", "순번", 45);
            this._flexID.SetCol("NO_ID", "ID번호", 100);
            this._flexID.SetCol("YN_GOOD", "양품여부", 60, true, CheckTypeEnum.Y_N);
            this._flexID.SetCol("YN_BAD", "불량여부", 60, true, CheckTypeEnum.Y_N);

            this._flexID.SettingVersion = "0.0.0.1";
            this._flexID.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.cbo공장_SelectionChangeCommitted);
            this.bpc작업장.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bpc오더형태.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bpc공정.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx작업지시번호to.CodeChanged += new EventHandler(this.ctx작업지시번호_CodeChanged);
            this.ctx작업지시번호to.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx작업품목.CodeChanged += new EventHandler(this.OnBpControl_CodeChanged);
            this.ctx작업품목.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx작업품목.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx작업지시번호from.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.btn불량내역등록.Click += new EventHandler(this.btn불량내역등록_Click);
            this.btn공정재작업처리.Click += new EventHandler(this.btn공정재작업처리_Click);
            this.btn실적이력.Click += new EventHandler(this.btn실적이력_Click);
            this.btn투입상세.Click += new EventHandler(this.btn투입상세_Click);
            this.btn공정불량처리.Click += new EventHandler(this.btn공정불량처리_Click);
            this.btn양품체크.Click += new EventHandler(this.btn양품체크_Click);
            this.btn불량체크.Click += new EventHandler(this.btn불량체크_Click);
            this._flexM.BeforeRowChange += new RangeEventHandler(this._flexM_BeforeRowChange);
            this._flexM.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);
            this._flexD.AfterRowChange += new RangeEventHandler(this._flexD_AfterRowChange);
            this._flexD.BeforeRowChange += new RangeEventHandler(this._flexD_BeforeRowChange);
            this._flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexD_BeforeCodeHelp);
            this._flexD.ValidateEdit += new ValidateEditEventHandler(this._flexD_ValidateEdit);
            this._flexD.StartEdit += new RowColEventHandler(this._flexD_StartEdit);
            this._flexD.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flexD_OwnerDrawCell);

        }
        #endregion

        #region 메인버튼 이벤트
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
            {
                if (this._flexD.Cols.Contains(this.strErrCol))
                    this._flexD.Col = this._flexD.Cols[this.strErrCol].Index;
                return false;
            }
            if (!this.공장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.cbo공장.Focus();
                return false;
            }
            if (!this.chk발주기간.Checked && !this.chk작업기간.Checked)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.chk발주기간.Text);
                this.chk발주기간.Focus();
                return false;
            }
            return (!this.chk발주기간.Checked || Checker.IsValid(this.dtp발주기간, false, this.chk발주기간.Text)) && (!this.chk작업기간.Checked || Checker.IsValid(this.dtp작업기간, false, this.chk작업기간.Text));
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                object[] objArray = new object[] { this.LoginInfo.CompanyCode,
                                                   this.cbo공장.SelectedValue.ToString(),
                                                   this.ctx작업품목.CodeValue,
                                                   this.ctx작업지시번호from.CodeValue,
                                                   this.ctx작업지시번호to.CodeValue,
                                                   this.dtp발주기간.StartDateToString,
                                                   this.dtp발주기간.EndDateToString,
                                                   this.txt발주번호시작.Text,
                                                   this.txt발주번호끝.Text,
                                                   null,
                                                   this.bpc오더형태.QueryWhereIn_Pipe,
                                                   this.bpc작업장.QueryWhereIn_Pipe,
                                                   this.LoginInfo.EmployeeNo,
                                                   this.chk발주기간.Checked ? "Y" : "N",
                                                   this.chk작업기간.Checked ? "Y" : "N",
                                                   this.dtp작업기간.StartDateToString,
                                                   this.dtp작업기간.EndDateToString,
                                                   this.chk재작업잔량여부.Checked ? "Y" : "N",
                                                   D.GetString(this.ctx거래처.CodeValue),
                                                   D.GetString(this.bpc공정.QueryWhereIn_Pipe),
                                                   this.마감구분,
                                                   Global.SystemLanguage.MultiLanguageLpoint };
                DataTable dataTable = !MA.ServerKey(false, "BKSEMS") ? (!(this.s조회기준 == "100") ? this._biz.Search(objArray) : this._biz.Search2(objArray)) : this._biz.Search_Chcoef(objArray);
                if (this._flexD.DataTable != null)
                {
                    this._flexD.DataTable.Rows.Clear();
                    this._flexD.AcceptChanges();
                }
                this._flexM.Binding = dataTable;
                this._flexD.Focus();
                this._flexD.Col = this._flexD.Cols["QT_WORK"].Index;
                if (!this._flexM.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
                else
                {
                    if (this.dtWork != null)
                    {
                        this.dtWork.Rows.Clear();
                        this.dtWork.AcceptChanges();
                    }
                    if (this._dtReject != null)
                    {
                        this._dtReject.Rows.Clear();
                        this._dtReject.AcceptChanges();
                    }
                    if (this._dtLotItem != null)
                    {
                        this._dtLotItem.Rows.Clear();
                        this._dtLotItem.AcceptChanges();
                    }
                    if (this._dtSERL != null)
                    {
                        this._dtSERL.Rows.Clear();
                        this._dtSERL.AcceptChanges();
                    }
                    if (this._dtMatl != null)
                    {
                        this._dtMatl.Rows.Clear();
                        this._dtMatl.AcceptChanges();
                    }
                    if (this._dtLotMatl != null)
                    {
                        this._dtLotMatl.Rows.Clear();
                        this._dtLotMatl.AcceptChanges();
                    }
                    if (this._dtInsp != null)
                    {
                        this._dtInsp.Rows.Clear();
                        this._dtInsp.AcceptChanges();
                    }
                    this._flexM.Enabled = true;
                    this._flexD.Enabled = true;
                    this.strErrCol = "QT_WORK";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.Verify() || !this.BeforeSave()) return;
                if (this.MsgAndSave(PageActionMode.Save))
                {
                    this.bSaveCalled = true;
                    this.ShowMessage(PageResultMode.SaveGood);
                    this.조회후해당행찾기("QT_WORK");
                    if (MA.ServerKey(false, "DIIN") && this._flexM.Rows.Count - this._flexM.Rows.Fixed > this._flexM.Row)
                    {
                        this._flexM.Select(this._flexM.Row + 1, this._flexM.Cols["NO_PO"].Index);
                        this._flexM.Focus();
                        this.조회후해당행찾기("QT_WORK");
                    }
                    this.OnToolBarSearchButtonClicked(null, null);
                }
                else
                {
                    this.ToolBarSaveButtonEnabled = true;
                    this.bSaveCalled = true;
                    int row = this._flexD.FindRow(this.drs[0]["CD_OP"].ToString(), this._flexD.Rows.Fixed, this._flexD.Cols["CD_OP"].Index, false, true, true);
                    if (row < 0)
                        row = this._flexD.Rows.Fixed;
                    this._flexD.Focus();
                    this._flexD.Select(row, this._flexD.Cols[this.strErrCol].Index);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.bSaveCalled = false;
            }
        }

        protected override bool SaveData()
        {
            if (!this.Verify())
                return false;
            string strNO_II = "";
            string str입고창고 = "";
            string seq1 = "";
            DataSet dataSet1 = new DataSet();
            DataTable dt_Manday = new DataTable();
            DataTable dt_Auto_Bad_Work = new DataTable();
            DataTable dt_Auto_Bad = new DataTable();
            DataTable dt_AutoBad_ReqH = new DataTable();
            DataTable dt_AutoBad_ReqL = new DataTable();
            if (this._flexD.HasNormalRow)
            {
                string str1 = this.dtWork.Rows[0]["DT_WORK"].ToString();
                seq1 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "05", str1.Substring(0, 6));
                if (str1 == "")
                    str1 = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
                if (seq1 == "")
                {
                    this.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                    return false;
                }
                this.dtWork.Rows[0]["DT_WORK"] = str1;
                this.dtWork.Rows[0]["NO_WORK"] = seq1;
                this.dtWork.Rows[0]["YN_REWORK"] = "N";
                if (this._dtReject != null)
                {
                    for (int index = 0; index < this._dtReject.Rows.Count; ++index)
                    {
                        this._dtReject.Rows[index]["QT_WORK"] = this.dtWork.Rows[0]["QT_WORK"];
                        this._dtReject.Rows[index]["NO_WORK"] = seq1;
                    }
                }
                if (this.Use_NextOP_sub == "100" && D.GetString(this.drs[0]["YN_FINAL"]) != "Y" && new P_PR_WORK_REG_NEXT_OP_SUB(D.GetString(this.drs[0]["CD_PLANT"]), D.GetString(this.drs[0]["NO_WO"]), D.GetDecimal(this.drs[0]["NO_LINE"]), "OPOUT").ShowDialog() != DialogResult.OK)
                    return false;
                if (this.LoginInfo.MngLot == "Y" && this._flexM["YN_LOT"].ToString() == "Y" && this.drs[0]["NO_LOT"].ToString().Trim() == "")
                {
                    DataTable dt = this.dtWork.Copy();
                    dt.Columns["DT_WORK"].ColumnName = "DT_IO";
                    dt.Columns.Add("QT_IO", typeof(decimal), "QT_WORK - QT_REJECT");
                    P_PR_LOT_ITEM_SUB pPrLotItemSub = new P_PR_LOT_ITEM_SUB(dt, this.YN_PR_MNG_LOT, "N");
                    if (pPrLotItemSub.ShowDialog() != DialogResult.OK)
                        return false;
                    if (pPrLotItemSub.dtL != null && pPrLotItemSub.dtL.Rows.Count > 0)
                        this._dtLotItem = pPrLotItemSub.dtL.Copy();
                }
                foreach (DataRow row in this.dtWork.Rows)
                {
                    row["NO_IO_202_102"] = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "06", str1.Substring(0, 6));
                    row["NO_IO_203"] = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "13", str1.Substring(0, 6));
                    row["NO_LINE_202"] = 1;
                    row["NO_LINE_102"] = 2;
                    row["NO_LINE_203"] = 1;
                    if (this._dtLotItem != null && this._dtLotItem.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in this._dtLotItem.Select(""))
                        {
                            dataRow["NO_IO_202_102"] = row["NO_IO_202_102"];
                            dataRow["NO_IO_203"] = row["NO_IO_203"];
                            dataRow["NO_LINE_202"] = row["NO_LINE_202"];
                            dataRow["NO_LINE_102"] = row["NO_LINE_102"];
                            dataRow["NO_LINE_203"] = row["NO_LINE_203"];
                            dataRow["CD_OP"] = row["CD_OP"];
                            dataRow["CD_WC"] = row["CD_WC"];
                        }
                    }
                }
                if (this._dtLotItem != null && this._dtLotItem.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in this._dtLotItem.Select("CHK = 'Y'"))
                    {
                        this.st.NO_IO = dataRow["NO_IO_202_102"].ToString();
                        this.st.NO_LINE2 = this._flexD.CInt32(dataRow["NO_IOLINE2"]);
                        this.st.NO_LOT = dataRow["NO_LOT"].ToString();
                        this.중복체크함수(ref this.st);
                        if (this._flexD.CDecimal(dataRow["NO_IOLINE2"]) != this._flexD.CDecimal(this.st.NO_LINE2))
                            dataRow["NO_IOLINE2"] = this._flexD.CDecimal(this.st.NO_LINE2);
                        this.st.NO_IO = dataRow["NO_IO_203"].ToString();
                        this.st.NO_LINE2 = this._flexD.CInt32(dataRow["NO_IOLINE2"]);
                        this.st.NO_LOT = dataRow["NO_LOT"].ToString();
                        this.중복체크함수(ref this.st);
                        if (this._flexD.CDecimal(dataRow["NO_IOLINE2"]) != this._flexD.CDecimal(this.st.NO_LINE2))
                            dataRow["NO_IOLINE2"] = this._flexD.CDecimal(this.st.NO_LINE2);
                    }
                }
                if (MA.ServerKey(false, "THV"))
                {
                    str입고창고 = this._flexM["CD_SL"].ToString();
                    if (string.IsNullOrEmpty(str입고창고))
                    {
                        this.ShowMessage("입고창고가 지정되어 있지 않습니다.");
                        return false;
                    }
                    this.dtWork.Rows[0]["FG_MOVE"] = "Y";
                    this.dtWork.Rows[0]["FG_ISU"] = "Y";
                    this.dtWork.Rows[0]["FG_CLOSE"] = "N";
                    this.dtWork.Rows[0]["CD_RSRC_LABOR"] = string.Empty;
                    this.dtWork.Rows[0]["TM_LABOR"] = 0M;
                    this.dtWork.Rows[0]["CD_RSRC_MACH"] = string.Empty;
                    this.dtWork.Rows[0]["TM_MACH"] = 0M;
                    this.dtWork.Rows[0]["QT_RSRC_LABOR"] = 0M;
                    DataSet dataSet2 = DBHelper.GetDataSet("UP_PR_Z_THV_WORK_MATL_AUTO_S", new object[] { this.LoginInfo.CompanyCode,
                                                                                                          this.dtWork.Rows[0]["CD_PLANT"].ToString(),
                                                                                                          this.dtWork.Rows[0]["NO_WO"].ToString(),
                                                                                                          this.dtWork.Rows[0]["CD_OP"].ToString() });
                    this._dtMatl = dataSet2.Tables[0];
                    this._dtLotMatl = dataSet2.Tables[1];
                    string empty = string.Empty;
                    foreach (DataRow row in this._dtMatl.Rows)
                    {
                        empty = D.GetString(row["NO_IO_MM"]);
                        strNO_II = D.GetString(row["NO_IO_201"]);
                        row["NO_IO_101"] = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "11", str1.Substring(0, 6));
                        foreach (DataRow dataRow in this._dtLotMatl.Select("NO_LINE = " + D.GetString(row["NO_LINE"])))
                        {
                            dataRow["NO_IO_201"] = row["NO_IO_201"];
                            dataRow["NO_IO_101"] = row["NO_IO_101"];
                            dataRow["NO_IO_MM"] = row["NO_IO_MM"];
                            dataRow["NO_LINE"] = row["NO_LINE"];
                        }
                    }
                    this._dtMatl.AcceptChanges();
                }
                else
                {
                    P_PR_INPUT_MATL_SUB pPrInputMatlSub = new P_PR_INPUT_MATL_SUB(this.dtWork.Rows[0], this._flexM["CD_SL"].ToString(), this._flexM["NM_SL"].ToString());
                    pPrInputMatlSub.ShowDialog();
                    if (pPrInputMatlSub.DialogResult != DialogResult.OK && pPrInputMatlSub.DialogResult != DialogResult.Ignore)
                        return false;
                    if (pPrInputMatlSub.HasMatl && pPrInputMatlSub.DialogResult == DialogResult.OK)
                    {
                        if (pPrInputMatlSub == null)
                            return false;
                        object[] returnValues = ((IHelpWindow)pPrInputMatlSub).ReturnValues;
                        DataRow dataRow1 = (DataRow)returnValues[0];
                        this.dtWork.Rows[0]["FG_MOVE"] = dataRow1["FG_MOVE"];
                        this.dtWork.Rows[0]["FG_ISU"] = dataRow1["FG_ISU"];
                        this.dtWork.Rows[0]["FG_CLOSE"] = dataRow1["FG_CLOSE"];
                        this.dtWork.Rows[0]["CD_RSRC_LABOR"] = dataRow1["CD_RSRC_LABOR"];
                        this.dtWork.Rows[0]["TM_LABOR"] = dataRow1["TM_LABOR"];
                        this.dtWork.Rows[0]["CD_RSRC_MACH"] = dataRow1["CD_RSRC_MACH"];
                        this.dtWork.Rows[0]["TM_MACH"] = dataRow1["TM_MACH"];
                        this._dtMatl = (DataTable)returnValues[1];
                        foreach (DataRow row in this._dtMatl.Rows)
                        {
                            if (row.RowState != DataRowState.Deleted)
                                row["DT_WORK"] = this.dtWork.Rows[0]["DT_WORK"];
                        }
                        DataTable dataTable1 = returnValues[2] as DataTable;
                        if (dataTable1 != null)
                        {
                            DataTable dataTable2 = dataTable1.Clone();
                            foreach (DataRow dataRow2 in dataTable1.Select("", "", DataViewRowState.CurrentRows))
                                dataTable2.Rows.Add(dataRow2.ItemArray);
                            dataTable2.Columns.Remove("DT_IO");
                            dataTable2.Columns.Add(new DataColumn("DT_IO", typeof(string))
                            {
                                DefaultValue = str1
                            });
                            this._dtLotMatl = dataTable2;
                        }
                        string str2 = "";
                        this._dtMatl.AcceptChanges();
                        foreach (DataRow row in this._dtMatl.Rows)
                            row.SetAdded();
                        if (this._dtMatl.Rows.Count > 0)
                        {
                            str2 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "17", str1.Substring(0, 6));
                            strNO_II = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "04", str1.Substring(0, 6));
                        }
                        foreach (DataRow row in this._dtMatl.Rows)
                        {
                            if (row.RowState != DataRowState.Deleted)
                            {
                                row["NO_IO_201"] = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "04", str1.Substring(0, 6));
                                if (row["YN_BF"].ToString() == "Y")
                                {
                                    row["NO_IO_101"] = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "11", str1.Substring(0, 6));
                                    row["NO_IO_MM"] = str2;
                                }
                                if (this._dtLotMatl != null && this._dtLotMatl.Rows.Count > 0)
                                {
                                    DataTable dtLotMatl = this._dtLotMatl;
                                    string filterExpression = "CD_PLANT = '" + row["CD_PLANT"].ToString() + "' AND CD_ITEM = '" + row["CD_MATL"].ToString() + "' ";
                                    foreach (DataRow dataRow2 in dtLotMatl.Select(filterExpression))
                                    {
                                        dataRow2["NO_IO_201"] = row["NO_IO_201"];
                                        dataRow2["NO_IO_101"] = row["NO_IO_101"];
                                        dataRow2["NO_IO_MM"] = row["NO_IO_MM"];
                                        dataRow2["NO_LINE"] = row["NO_LINE"];
                                    }
                                }
                            }
                        }
                    }
                    str입고창고 = pPrInputMatlSub.입고창고코드;
                }
                if (App.SystemEnv.SERIAL사용 && this._flexM["YN_SERL"].ToString() == "Y" && (this.drs[0]["YN_FINAL"].ToString() == "Y" && this.drs[0]["YN_AUTORCV"].ToString() == "Y") && Convert.ToDecimal(this.drs[0]["QT_WORK"].ToString()) - Convert.ToDecimal(this.drs[0]["QT_REJECT"].ToString()) > 0M)
                {
                    DataTable dt = this.drs[0].Table.Clone();
                    dt.Columns.Add("NO_IO", typeof(string));
                    dt.Columns.Add("NO_IOLINE", typeof(decimal));
                    dt.Columns.Add("DT_IO", typeof(string));
                    dt.Columns.Add("FG_IO", typeof(string));
                    dt.Columns.Add("CD_SL", typeof(string));
                    dt.Columns.Add("NM_SL", typeof(string));
                    dt.Columns.Add("CD_QTIOTP", typeof(string));
                    dt.Columns.Add("QT_GOOD_INV", typeof(decimal));
                    dt.Columns["NO_IO"].DefaultValue = "";
                    dt.Columns["NO_IOLINE"].DefaultValue = 0.0;
                    dt.Columns["DT_IO"].DefaultValue = this.drs[0]["DT_WORK"].ToString();
                    dt.Columns["FG_IO"].DefaultValue = "002";
                    dt.Columns["CD_SL"].DefaultValue = this._flexM["CD_SL"].ToString();
                    dt.Columns["NM_SL"].DefaultValue = this._flexM["NM_SL"].ToString();
                    dt.Columns["CD_QTIOTP"].DefaultValue = this._flexM["TP_GR"].ToString();
                    dt.Columns["QT_GOOD_INV"].DefaultValue = (Convert.ToDecimal(this.drs[0]["QT_WORK"].ToString()) - Convert.ToDecimal(this.drs[0]["QT_REJECT"].ToString()));
                    dt.Rows.Add(this.drs[0].ItemArray);
                    P_PU_SERL_SUB_R pPuSerlSubR = new P_PU_SERL_SUB_R(dt, "PR");
                    pPuSerlSubR.ShowDialog();
                    if (pPuSerlSubR.DialogResult != DialogResult.OK && pPuSerlSubR.DialogResult != DialogResult.Ignore)
                        return false;
                    if (pPuSerlSubR.DialogResult == DialogResult.OK)
                        this._dtSERL = pPuSerlSubR.dtL;
                }
                DataSet dataSet3 = this._biz.Search_PlantSetting(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                D.GetString(this._flexD["CD_PLANT"]),
                                                                                Global.SystemLanguage.MultiLanguageLpoint });
                if (dataSet3 != null && dataSet3.Tables[1].Rows.Count != 0 && D.GetString(dataSet3.Tables[1].Rows[0]["YN_LINK_HR"]) == "Y")
                {
                    P_PR_WORK_MANDAY_SUB pPrWorkMandaySub = new P_PR_WORK_MANDAY_SUB(this.dtWork.Rows[0]);
                    pPrWorkMandaySub.Set설비코드 = D.GetString(this.dtWork.Rows[0]["CD_EQUIP"]);
                    pPrWorkMandaySub.Set설비명 = D.GetString(this.dtWork.Rows[0]["NM_EQUIP"]);
                    if (pPrWorkMandaySub.ShowDialog() != DialogResult.OK)
                        return false;
                    dt_Manday = pPrWorkMandaySub.Return_Dt;
                }
                if (this._dtReject != null && this._dtReject.Rows.Count > 0)
                {
                    DataSet plantCfg = Pr_ComFunc.Get_Plant_Cfg(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                               this.dtWork.Rows[0]["CD_PLANT"].ToString() });
                    if (D.GetString(plantCfg.Tables[1].Rows[0]["YN_AUTOBAD"]) == "Y" && (D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "000" || D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "001"))
                    {
                        DataRow[] dataRowArray = this._dtReject.Select("ISNULL(CHK, 'N') = 'Y'");
                        if (dataRowArray != null && dataRowArray.Length > 0)
                        {
                            string str2 = Global.MainFrame.GetStringToday.Substring(0, 8);
                            string seq2 = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "05", str2.Substring(0, 6));
                            if (str2 == "")
                                str2 = Global.MainFrame.GetStringToday.Substring(0, 6) + "01";
                            dt_Auto_Bad = this._dtReject.Clone();
                            foreach (DataRow row in dataRowArray)
                            {
                                for (int index = 1; index <= 20; ++index)
                                {
                                    string columnName = "CD_MNG" + D.GetInt(index);
                                    row[columnName] = D.GetString(this.dtWork.Rows[0][columnName]);
                                }
                                dt_Auto_Bad.ImportRow(row);
                                dt_Auto_Bad.Rows[dt_Auto_Bad.Rows.Count - 1]["NO_WORK"] = seq2;
                            }
                            decimal num1 = D.GetDecimal(dt_Auto_Bad.Compute("SUM(QT_REJECT)", "ISNULL(CHK, 'N') = 'Y'"));
                            dt_Auto_Bad_Work = this.dtWork.Clone();
                            dt_Auto_Bad_Work.ImportRow(this.dtWork.Rows[0]);
                            dt_Auto_Bad_Work.Rows[0]["NO_IO_202_102"] = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "06", str2.Substring(0, 6));
                            dt_Auto_Bad_Work.Rows[0]["NO_IO_203"] = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "13", str2.Substring(0, 6));
                            dt_Auto_Bad_Work.Rows[0]["NO_LINE_202"] = 1;
                            dt_Auto_Bad_Work.Rows[0]["NO_LINE_102"] = 2;
                            dt_Auto_Bad_Work.Rows[0]["NO_LINE_203"] = 1;
                            dt_Auto_Bad_Work.Rows[0]["DT_WORK"] = this.dtWork.Rows[0]["DT_WORK"];
                            dt_Auto_Bad_Work.Rows[0]["NO_WORK"] = seq2;
                            dt_Auto_Bad_Work.Rows[0]["QT_WORK"] = num1;
                            dt_Auto_Bad_Work.Rows[0]["QT_MOVE"] = (D.GetDecimal(this.dtWork.Rows[0]["QT_WORK"]) - num1);
                            dt_Auto_Bad_Work.Rows[0]["QT_REJECT"] = 0;
                            dt_Auto_Bad_Work.Rows[0]["YN_REWORK"] = "N";
                            dt_Auto_Bad_Work.Rows[0]["YN_BAD_PROC"] = "Y";
                            dt_Auto_Bad_Work.Rows[0]["DC_REJECT"] = "";
                            dt_Auto_Bad_Work.Rows[0]["FG_MOVE"] = "Y";
                            dt_Auto_Bad_Work.Rows[0]["NO_PO"] = this.dtWork.Rows[0]["NO_PO"];
                            dt_Auto_Bad_Work.Rows[0]["NO_POLINE"] = this.dtWork.Rows[0]["NO_POLINE"];
                            if (D.GetString(plantCfg.Tables[1].Rows[0]["YN_AUTOBAD_REQ"]) == "Y")
                            {
                                string str3 = string.Empty;
                                string str4 = D.GetString(plantCfg.Tables[1].Rows[0]["YN_AUTOBAD_RCV"]);
                                DataSet dataSet2 = this._biz.Search_AutoBad_Req(new object[] { string.Empty,
                                                                                               string.Empty,
                                                                                               string.Empty,
                                                                                               Global.SystemLanguage.MultiLanguageLpoint });
                                dt_AutoBad_ReqH = dataSet2.Tables[0].Clone();
                                dt_AutoBad_ReqH.Columns.Add("NO_IO", typeof(string));
                                dt_AutoBad_ReqH.Columns.Add("CD_DEPT", typeof(string));
                                dt_AutoBad_ReqH.Columns.Add("YN_AUTOBAD_RCV", typeof(string));
                                dt_AutoBad_ReqL = dataSet2.Tables[1].Clone();
                                dt_AutoBad_ReqL.Columns.Add("NO_IO", typeof(string));
                                dt_AutoBad_ReqL.Columns.Add("QT_INSP", typeof(decimal));
                                dt_AutoBad_ReqL.Columns.Add("YN_AUTOBAD_RCV", typeof(string));
                                dt_AutoBad_ReqL.Columns.Add("NO_LOT", typeof(string));
                                dt_AutoBad_ReqL.Columns.Add("NO_IO_203", typeof(string));
                                dt_AutoBad_ReqL.Columns.Add("NO_LINE_203", typeof(decimal));
                                dt_AutoBad_ReqL.Columns.Add("DT_LIMIT", typeof(string));
                                for (int index = 1; index <= 20; ++index)
                                {
                                    string columnName = "CD_MNG" + D.GetInt(index);
                                    dt_AutoBad_ReqL.Columns.Add(columnName, typeof(string));
                                }
                                this.dtWork.Rows[0]["DT_WORK"].ToString();
                                string seq3 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "06", Global.MainFrame.GetStringToday.Substring(0, 8));
                                dt_AutoBad_ReqH.Rows.Add(dt_AutoBad_ReqH.NewRow());
                                dt_AutoBad_ReqH.Rows[0]["CD_PLANT"] = this.dtWork.Rows[0]["CD_PLANT"].ToString();
                                dt_AutoBad_ReqH.Rows[0]["NO_REQ"] = seq3;
                                dt_AutoBad_ReqH.Rows[0]["DT_REQ"] = this.dtWork.Rows[0]["DT_WORK"];
                                dt_AutoBad_ReqH.Rows[0]["NO_EMP"] = this.dtWork.Rows[0]["NO_EMP"];
                                dt_AutoBad_ReqH.Rows[0]["CD_DEPT"] = this.dtWork.Rows[0]["CD_DEPT"];
                                if (str4 == "Y")
                                {
                                    str3 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "18", Global.MainFrame.GetStringToday.Substring(0, 8));
                                    dt_AutoBad_ReqH.Rows[0]["NO_IO"] = str3;
                                    dt_AutoBad_ReqH.Rows[0]["YN_AUTOBAD_RCV"] = str4;
                                }
                                int num2 = 1;
                                foreach (DataRow row1 in dt_Auto_Bad.Rows)
                                {
                                    if (D.GetString(row1["CD_SL_BAD"]) == string.Empty)
                                    {
                                        Global.MainFrame.ShowMessage("자동입고처리가 체크되어있는 경우 불량창고는 필수 항목입니다.");
                                        return false;
                                    }
                                    DataRow row2 = dt_AutoBad_ReqL.NewRow();
                                    row2["CD_PLANT"] = this.dtWork.Rows[0]["CD_PLANT"];
                                    row2["NO_REQ"] = seq3;
                                    row2["NO_LINE"] = num2++;
                                    row2["CD_WC"] = this.dtWork.Rows[0]["CD_WC"];
                                    row2["CD_ITEM"] = this.dtWork.Rows[0]["CD_ITEM"];
                                    row2["DT_REQ"] = this.dtWork.Rows[0]["DT_WORK"];
                                    row2["QT_REQ"] = row1["QT_REJECT"];
                                    row2["QT_REQ_W"] = 0;
                                    row2["QT_REQ_B"] = 0;
                                    row2["QT_RCV"] = 0;
                                    row2["CD_SL"] = row1["CD_SL_BAD"];
                                    row2["NO_WO"] = row1["NO_WO"];
                                    row2["NO_WORK"] = row1["NO_WORK"];
                                    row2["TP_WB"] = "0";
                                    row2["TP_GR"] = "981";
                                    row2["NO_IO"] = str3;
                                    row2["YN_AUTOBAD_RCV"] = str4;
                                    row2["NO_EMP"] = this.dtWork.Rows[0]["NO_EMP"];
                                    row2["NO_LOT"] = row1["NO_LOT"];
                                    row2["NO_IO_203"] = dt_Auto_Bad_Work.Rows[0]["NO_IO_203"];
                                    row2["NO_LINE_203"] = dt_Auto_Bad_Work.Rows[0]["NO_LINE_203"];
                                    row2["DT_LIMIT"] = this.dtWork.Rows[0]["DT_LIMIT"];
                                    for (int index = 1; index <= 20; ++index)
                                    {
                                        string columnName = "CD_MNG" + D.GetInt(index);
                                        row2[columnName] = D.GetString(this.dtWork.Rows[0][columnName]);
                                    }
                                    dt_AutoBad_ReqL.Rows.Add(row2);
                                }
                            }
                        }
                    }
                }
            }
            string 외주열처리번호 = this.txt외주열처리번호.Text;
            _dtInsp.Columns.Add("CD_COMPANY", typeof(string));
            _dtInsp.Columns.Add("NO_INSP", typeof(string));
            _dtInsp.Columns.Add("NO_HEAT", typeof(string));
            _dtInsp.Columns.Add("USER_ID", typeof(string));
            _dtInsp.Columns.Add("NO_OPOUT_WORK", typeof(string));

            foreach (DataRow dr in _dtInsp.Rows)
            {
                if (D.GetString(dr["YN_GOOD"]) == "N" && D.GetString(dr["YN_BAD"]) == "N")
                    dr.Delete();
				else
				{
                    dr["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                    dr["NO_WO"] = this.dtWork.Rows[0]["NO_WO"].ToString();
                    dr["NO_LINE"] = this._flexD.CDecimal(this._flexD["NO_LINE"]);

                    if (D.GetString(dr["YN_GOOD"]) == "Y")
                    {
                        dr["NO_INSP"] = "995";
                    }
                    else if (D.GetString(dr["YN_BAD"]) == "Y")
                    {
                        dr["NO_INSP"] = "-1";
                    }
                    dr["NO_HEAT"] = 외주열처리번호;
                    dr["USER_ID"] = Global.MainFrame.LoginInfo.UserID;
                    dr["NO_OPOUT_WORK"] = seq1;
                }
            }

            string str발주번호 = this._flexM["NO_PO"].ToString();
            decimal d발주항번 = this._flexM.CDecimal(this._flexM["NO_POLINE"]);
            if (!this._biz.Save(this.dtWork,
                                this._dtReject,
                                this._dtLotItem,
                                this._dtMatl,
                                this._dtLotMatl,
                                this._dtSERL,
                                this.dtWork.Rows[0]["NO_LINE"].ToString(),
                                strNO_II,
                                str입고창고,
                                str발주번호,
                                d발주항번,
                                dt_Auto_Bad_Work,
                                dt_Auto_Bad,
                                dt_AutoBad_ReqH,
                                dt_AutoBad_ReqL,
                                dt_Manday,
                                this._dtInsp))
                return false;

            this._flexD.AcceptChanges();

            if (this.dtWork != null)
            {
                this.dtWork.Rows.Clear();
                this.dtWork.AcceptChanges();
            }
            if (this._dtReject != null)
            {
                this._dtReject.Rows.Clear();
                this._dtReject.AcceptChanges();
            }
            if (this._dtLotItem != null)
            {
                this._dtLotItem.Rows.Clear();
                this._dtLotItem.AcceptChanges();
            }
            if (this._dtSERL != null)
            {
                this._dtSERL.Rows.Clear();
                this._dtSERL.AcceptChanges();
            }
            if (this._dtMatl != null)
            {
                this._dtMatl.Rows.Clear();
                this._dtMatl.AcceptChanges();
            }
            if (this._dtLotMatl != null)
            {
                this._dtLotMatl.Rows.Clear();
                this._dtLotMatl.AcceptChanges();
            }
            if (this._dtInsp != null)
            {
                this._dtInsp.Rows.Clear();
                this._dtInsp.AcceptChanges();
            }
            return true;
        }

        protected override bool BeforeSave()
        {
            this.ToolBarSaveButtonEnabled = false;
            return base.BeforeSave();
        }

        protected override bool Verify()
        {
            if (!base.BeforeSave())
                return false;
            this.dtWork = this._flexD.GetChanges();
            this._dtInsp = this._flexID.GetChanges();
            if (this.dtWork == null)
                return false;
            if (this._dtInsp == null)
            {
                this.ShowMessage("ID번호를 선택하세요.");
                return false;
            }

            if (this.dtWork.Rows.Count > 1)
            {
                this.ShowMessage("변경이 행이 @개입니다. 로직상 변경된 행은 1개만 되야합니다.", this.dtWork.Rows.Count);
                return false;
            }
            this.drs = this._flexD.DataTable.Select("NO_WO = '" + this.dtWork.Rows[0]["NO_WO"].ToString() + "' AND CD_OP = '" + this.dtWork.Rows[0]["CD_OP"].ToString() + "'  AND NO_PO = '" + this.dtWork.Rows[0]["NO_PO"].ToString() + "'  AND NO_POLINE = " + this.dtWork.Rows[0]["NO_POLINE"].ToString() + " ");
            if (this.LoginInfo.MngLot == "Y" && D.GetString(this._flexM["YN_LOT"]) == "Y")
            {
                if (D.GetString(this._flexM["NO_LOT"]) == string.Empty)
                {
                    if ((this.YN_PR_MNG_LOT == "Y" || this.YN_PR_MNG_LOT == "N" && D.GetString(this.drs[0]["YN_FINAL"]) == "Y") && ((D.GetString(this.drs[0]["CD_OP_BASE"]) != string.Empty || this.s최종공정LOT필수여부 == "100") && D.GetString(this.drs[0]["NO_LOT"]) == string.Empty))
                    {
                        if (!(this.YN_PR_MNG_LOT == "N") || !(this.s최종공정LOT필수여부 != "100"))
                        {
                            this.strErrCol = "NO_LOT";
                            this.ShowMessage("@ 를 바르게 입력하세요", this.DD("LOT번호"));
                            return false;
                        }
                    }
                }
                else
                    this.dtWork.Rows[0]["NO_LOT"] = this._flexM["NO_LOT"];
            }
            decimal num1 = this._flexD.CDecimal(this.drs[0]["QT_REJECT"]);
            if (num1 > 0M)
            {
                if (this._dtReject == null || this._dtReject.Rows.Count < 1)
                {
                    this.ShowMessage(공통메세지._이가존재하지않습니다, this.DD("불량내역"));
                    if (this.ShowRejectHelp())
                        return true;
                    this._flexD.Focus();
                    return false;
                }
                if (D.GetDecimal(this._dtReject.Compute("SUM(QT_REJECT)", "")) != num1)
                {
                    this.ShowMessage("불량내역에 입력된 수량과 실적불량수량이 일치 하지 않습니다\n 입력된 불량내역은 모두 삭제됩니다.");
                    this._dtReject.Clear();
                    return false;
                }
            }
            else if (num1 == 0M && (this._dtReject != null && this._dtReject.Rows.Count > 0))
            {
                this.ShowMessage("실적불량수량이 '0'으로 수정되었습니다.\n 입력된 불량내역은 모두 삭제됩니다.");
                this._dtReject.Clear();
                return false;
            }

            decimal Gcnt = 0;
            decimal Bcnt = 0;
            for (int row = this._flexID.Rows.Fixed; row < this._flexID.Rows.Count; ++row)
            {
                if (D.GetString(this._flexID[row, "YN_GOOD"]) == "Y")
                    Gcnt++;
                else if (D.GetString(this._flexID[row, "YN_BAD"]) == "Y")
                    Bcnt++;

                if (D.GetString(this._flexID[row, "YN_GOOD"]) == "Y" && D.GetString(this._flexID[row, "YN_BAD"]) == "Y")
                {
                    this.ShowMessage("양품, 불량 모두 체크된 건이 있습니다.");
                    return false;
                }
            }
            if (Gcnt != this._flexD.CDecimal(this.drs[0]["QT_GOOD"]))
            {
                this.ShowMessage("양품실적수량과 양품체크수량이 일치 하지 않습니다.");
                return false;
            }
            if (Bcnt != this._flexD.CDecimal(this.drs[0]["QT_REJECT"]))
            {
                this.ShowMessage("불량실적수량과 불량체크수량이 일치 하지 않습니다.");
                return false;
            }

            if (this._flexM["CD_WC"].ToString() != "W510")
            {
                if (this.txt외주열처리번호.Text != null && this.txt외주열처리번호.Text != "")
                {
                    this.ShowMessage("열처리 공정이 아닙니다.\n외주열처리번호를 입력할 수 없습니다.");
                    return false;
                }
            }
            if (this._flexM["CD_WC"].ToString() == "W510")
			{
                if (this.txt외주열처리번호.Text == null || this.txt외주열처리번호.Text == "")
                {
                    this.ShowMessage("외주열처리번호를 입력하세요.");
                    return false;
                }
            }
            return base.Verify();
        }
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforePrint() || !this.MsgAndSave(PageActionMode.Print))
                    return;
                ReportHelper reportHelper = new ReportHelper("R_PR_WORK_REG_0", "작업실적전표");
                DataTable dt = this._biz.print(this.cbo공장.SelectedValue.ToString(), this._flexM["NO_WO"].ToString());
                reportHelper.SetDataTable(dt);
                reportHelper.가로출력();
                reportHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Settings1.Default.Wo_DcRmk_Apply_YN = this.chk작업지시비고적용여부.Checked;
                Settings1.Default.재작업잔량여부 = this.chk재작업잔량여부.Checked;
                Settings1.Default.Save();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return true;
        }
        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;
            if (this._flexD.Row < this._flexD.Rows.Fixed || this._flexD.Row >= this._flexD.Rows.Count || !(this._flexD.CDecimal(this._flexD["QT_WO_WORK"]) <= 0M))
                return true;
            this.ShowMessage(공통메세지._은_보다커야합니다, this._flexD.Cols["QT_WO_WORK"].Caption, "0");
            return false;
        }
        #endregion

        #region 그리드 이벤트

        private void _flexD_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid))
                    return;
                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "NO_LOT":
                        if (this._flexM["NO_LOT"].ToString().Trim().Length == 0)
                        {
                            if (this.LoginInfo.MngLot == "Y" && this.YN_PR_MNG_LOT == "Y" && flexGrid["YN_FINAL"].ToString() == "Y" && flexGrid.Rows.Count - flexGrid.Rows.Fixed != 1)
                            {
                                e.Cancel = true;
                                break;
                            }
                            if (flexGrid["YN_FINAL"].ToString() != "Y" && flexGrid["CD_OP_BASE"].ToString() == "")
                            {
                                e.Cancel = true;
                                break;
                            }
                        }
                        if (!MA.ServerKey(false, "GOPT") && this._flexD.CDecimal(this._flexD["QT_WORK"]) <= 0M)
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, flexGrid.Cols["QT_WORK"].Caption);
                            e.Cancel = true;
                            this._flexD.Select(e.Row, flexGrid.Cols["QT_WORK"].Index);
                            break;
                        }
                        break;
                    case "DT_LIMIT":
                        if (flexGrid["YN_FINAL"].ToString() == "N")
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexD_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid))
                    return;
                decimal num1 = this._flexD.CDecimal(flexGrid["QT_WORK"]);
                this._flexD.CDecimal(flexGrid["QT_REJECT"]);
                decimal num2 = this._flexD.CDecimal(flexGrid["QT_REMAIN"]);
                decimal num3 = this._flexD.CDecimal(flexGrid["QT_OUTPO"]);
                decimal num4 = this._flexD.CDecimal(flexGrid["QT_PO"]);
                decimal num5 = this._flexD.CDecimal(flexGrid["QT_OUTRCV"]);
                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "DT_WORK":
                        try
                        {
                            Convert.ToDateTime(flexGrid.Editor.Text);
                            break;
                        }
                        catch
                        {
                            e.Cancel = true;
                            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            break;
                        }
                    case "QT_WORK":
                        decimal num7 = this._flexD.CDecimal(flexGrid.EditData);
                        if (num7 > num2 && !this.b실적수량초과여부)
                        {
                            this.ShowMessage("@이  @보다 많습니다.", new object[] { flexGrid.Cols["QT_WORK"].Caption,
                                                                                    flexGrid.Cols["QT_REMAIN"].Caption });
                            e.Cancel = true;
                            break;
                        }
                        //if (num7 > num3 && this.sChcoef_YN != "100" && !this.b실적수량초과여부)
                        //{
                        //    this.ShowMessage("@이 @보다 많습니다.", new object[] { flexGrid.Cols["QT_WORK"].Caption,
                        //                                                           flexGrid.Cols["QT_OUTPO"].Caption });
                        //    e.Cancel = true;
                        //    break;
                        //}
                        if (num7 > num4 && !this.b실적수량초과여부)
                        {
                            this.ShowMessage("@이 @보다 많습니다.", new object[] { flexGrid.Cols["QT_WORK"].Caption,
                                                                                   flexGrid.Cols["QT_PO"].Caption });
                            e.Cancel = true;
                            break;
                        }
                        if (num7 > num4 - num5 && !this.b실적수량초과여부)
                        {
                            this.ShowMessage("@이 @ - @보다 많습니다.", new object[] { flexGrid.Cols["QT_WORK"].Caption,
                                                                                       flexGrid.Cols["QT_PO"].Caption,
                                                                                       flexGrid.Cols["QT_OUTRCV"].Caption });
                            e.Cancel = true;
                            break;
                        }
                        if (num7 < 0M)
                        {
                            this.ShowMessage(공통메세지._은_보다커야합니다, this.DD("실적입력량"), "0");
                            e.Cancel = true;
                            break;
                        }
                        if (D.GetDecimal(flexGrid["QT_START"]) <= 0M && this.b실적수량초과여부)
                        {
                            this.ShowMessage(공통메세지._은_보다커야합니다, flexGrid.Cols["QT_START"].Caption, "0");
                            e.Cancel = true;
                            break;
                        }
                        this._flexD[e.Row, "QT_GOOD"] = Unit.수량(DataDictionaryTypes.PR, num7 - D.GetDecimal(flexGrid[e.Row, "QT_REJECT"]));
                        break;
                    case "QT_REJECT":
                        decimal num8 = this._flexD.CDecimal(flexGrid.EditData);
                        if (num8 > num1)
                        {
                            this.ShowMessage("불량입력량이 실적수량보다 많습니다.");
                            e.Cancel = true;
                            break;
                        }
                        if (num8 < 0M)
                        {
                            this.ShowMessage(공통메세지._은_보다커야합니다, this.DD("불량입력량"), "0");
                            e.Cancel = true;
                            break;
                        }
                        this._flexD[e.Row, "QT_GOOD"] = Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(flexGrid[e.Row, "QT_WORK"]) - num8);
                        break;
                    case "QT_WORK_CHCOEF":
                        if (D.GetDecimal(flexGrid["QT_WORK_CHCOEF"]) == 0M || D.GetDecimal(flexGrid["QT_CHCOEF"]) == 0M)
                        {
                            flexGrid["QT_WORK"] = 0;
                            break;
                        }
                        flexGrid["QT_WORK"] = (D.GetDecimal(flexGrid["QT_WORK_CHCOEF"]) / D.GetDecimal(flexGrid["QT_CHCOEF"]));
                        this._flexD[e.Row, "QT_GOOD"] = Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(flexGrid[e.Row, "QT_WORK"]) - D.GetDecimal(flexGrid[e.Row, "QT_REJECT"]));
                        break;
                    case "QT_WORK_BAD_CHCOEF":
                        if (D.GetDecimal(flexGrid["QT_WORK_BAD_CHCOEF"]) == 0M || D.GetDecimal(flexGrid["QT_CHCOEF"]) == 0M)
                        {
                            flexGrid["QT_REJECT"] = 0;
                            break;
                        }
                        flexGrid["QT_REJECT"] = (D.GetDecimal(flexGrid["QT_WORK_BAD_CHCOEF"]) / D.GetDecimal(flexGrid["QT_CHCOEF"]));
                        this._flexD[e.Row, "QT_GOOD"] = Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(flexGrid[e.Row, "QT_WORK"]) - D.GetDecimal(flexGrid[e.Row, "QT_REJECT"]));
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this.bGridrowChanging)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.ToolBarSearchButtonEnabled = false;
                this.bGridrowChanging = false;
                DataTable dt = null;
                string filter = "NO_PO = '" + D.GetString(this._flexM["NO_PO"]) + "' AND NO_POLINE = " + D.GetDecimal(this._flexM["NO_POLINE"]) + " AND NO_WO = '" + D.GetString(this._flexM["NO_WO"]) + "' AND CD_OP = '" + D.GetString(this._flexM["CD_OP"]) + "' ";
                if (this.LoginInfo.MngLot == "N" || this._flexM["YN_LOT"].ToString() == "N" || this._flexM["NO_LOT"].ToString().Trim().Length > 0)
                    this._flexD.Cols["NO_LOT"].Visible = false;
                else
                    this._flexD.Cols["NO_LOT"].Visible = true;
                if (this.LoginInfo.MngLot == "N" || this._flexM["YN_LOT"].ToString() == "N")
                {
                    if (this.LOT관리항목DT.Rows.Count != 0)
                    {
                        foreach (DataRow row in this.LOT관리항목DT.Rows)
                            this._flexD.Cols["CD_MNG" + D.GetInt(row["CODE"])].Visible = false;
                    }
                }
                else if (this.LOT관리항목DT.Rows.Count != 0)
                {
                    foreach (DataRow row in this.LOT관리항목DT.Rows)
                        this._flexD.Cols["CD_MNG" + D.GetInt(row["CODE"])].Visible = true;
                }


                if (this._flexM.DetailQueryNeed)
                {
                    object[] objArray = new object[] { this.LoginInfo.CompanyCode,
                                                       this.cbo공장.SelectedValue.ToString(),
                                                       this._flexM["NO_WO"].ToString(),
                                                       this._flexM["NO_PO"].ToString(),
                                                       D.GetDecimal(this._flexM["NO_POLINE"]),
                                                       this.chk작업지시비고적용여부.Checked ? "Y" : "N",
                                                       this.LoginInfo.EmployeeNo,
                                                       Global.SystemLanguage.MultiLanguageLpoint };
                    dt = !MA.ServerKey(false, "BKSEMS") ? this._biz.SearchDetail(objArray) : this._biz.SearchDetail_Chcoef(objArray);
                }
                this._flexM.DetailGrids[0].BindingAdd(dt, filter);
                this._flexM.DetailQueryNeed = false;
                if (!this._flexM.DetailGrids[0].HasNormalRow)
                    return;
                this._flexM.DetailGrids[0].Row = this._flexM.DetailGrids[0].Rows.Fixed;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.ToolBarSearchButtonEnabled = true;
                this.bGridrowChanging = true;
            }
        }

        private void _flexD_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this.bDetailGridrowChanging)
                    e.Cancel = true;
                else
                    this.bDetailGridrowChanging = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.bDetailGridrowChanging = true;
            }
        }

        private void _flexD_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow) return;

                this._flexID.Binding = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              this._flexM["NO_WO"].ToString(),
                                                                              this._flexD["NO_LINE"].ToString(),
                                                                              this._flexM["NO_PO"].ToString(),
                                                                              this._flexM["NO_PR"].ToString() });

                if (!this._flexID.HasNormalRow)
                    return;
                if (!this.IsChanged() || this.bSaveCalled)
                    return;
                if (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까) == DialogResult.No)
                {
                    this._dtReject.Rows.Clear();
                    this._flexD.RejectChanges();
                }
                else
                {
                    this.OnToolBarSaveButtonClicked(null, null);
                    this._flexD.Col = this._flexD.Cols[this.strErrCol].Index;
                    this.strErrCol = "QT_WORK";
                }
            }
            catch (Exception ex)

            {
                this.MsgEnd(ex);
            }
        }

        private void _flexD_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID != HelpID.P_USER)
                    return;
                string str1 = "|&&|";
                string str2 = "|&|";
                if (e.Parameter.UserHelpID == "H_PR_SFT_SUB")
                {
                    e.Parameter.P41_CD_FIELD1 = "SFT";
                    e.Parameter.P09_CD_PLANT = this._flexM["CD_PLANT"].ToString();
                    e.Parameter.P65_CODE5 = this.cbo공장.Text;
                    HelpParam parameter1 = e.Parameter;
                    parameter1.UserParams = parameter1.UserParams + "USE_Y" + str2 + "Y" + str2 + "Enabled" + str1;
                    HelpParam parameter2 = e.Parameter;
                    parameter2.UserParams = parameter2.UserParams + "USE_N" + str2 + "N" + str2 + "Enabled" + str1;
                    HelpParam parameter3 = e.Parameter;
                    parameter3.UserParams = parameter3.UserParams + "USE_C" + str2 + "C" + str2 + "Enabled" + str1;
                    e.Parameter.P61_CODE1 = str1;
                    e.Parameter.P62_CODE2 = str2;
                }
                if (!(e.Parameter.UserHelpID == "H_PR_EQUIP_SUB"))
                    return;
                e.Parameter.P41_CD_FIELD1 = "EQUIP";
                e.Parameter.P09_CD_PLANT = this._flexM["CD_PLANT"].ToString();
                e.Parameter.P63_CODE3 = this._flexD["CD_WC"].ToString();
                e.Parameter.P64_CODE4 = this._flexD["CD_WCOP"].ToString();
                e.Parameter.P65_CODE5 = this.cbo공장.Text;
                e.Parameter.P61_CODE1 = str1;
                e.Parameter.P62_CODE2 = str2;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexD_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                int r1 = this._flexD.Selection.r1;
                int index = this._flexD.Cols["QT_WORK"].Index;
                if (r1 != e.Row || index != e.Col)
                    return;
                Color seaGreen = Color.SeaGreen;
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.Bounds, seaGreen, seaGreen, 1f);
                e.Graphics.FillRectangle((Brush)linearGradientBrush, e.Bounds);
                e.DrawCell(DrawCellFlags.Content);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트

        private void btn실적이력_Click(object sender, EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog)
                    return;
                if (!this._flexM.HasNormalRow || !this._flexD.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (this._flexD.Row >= this._flexD.Rows.Fixed && this._flexD.Row < this._flexD.Rows.Count)
                    {
                        if (!MA.ServerKey(false, "GOPT") && this._flexD.CDecimal(this._flexD["QT_WO_WORK"]) <= 0M)
                        {
                            this.ShowMessage(공통메세지._은_보다커야합니다, this._flexD.Cols["QT_WO_WORK"].Caption, "0");
                            return;
                        }
                    }
                    else if (this._flexD["YN_SUBCON"].ToString() == "Y" && this._flexD.CDecimal(this._flexD["QT_OUTRCV"]) > 0M)
                    {
                        this.ShowMessage(공통메세지._이가존재하지않습니다, this._flexD.Cols["QT_OUTRCV"].Caption);
                        return;
                    }
                    int iStartQty = -1;
                    int iWorkQty = -1;
                    int iQT_PO = -1;
                    string 공정외주여부 = this._flexD["YN_SUBCON"].ToString();
                    if (this._flexD["YN_FINAL"].ToString() == "N")
                    {
                        DataRow[] dataRowArray = this._flexD.DataTable.Select("CD_OP > '" + this._flexD["CD_OP"].ToString() + "' ", "CD_OP ASC");
                        iStartQty = Convert.ToInt32(dataRowArray[0]["QT_START"]);
                        iWorkQty = Convert.ToInt32(dataRowArray[0]["QT_WO_WORK"]);
                        iQT_PO = Convert.ToInt32(dataRowArray[0]["QT_OUTPO"]);
                    }
                    DataRow[] main_row = this._flexD.DataTable.Select("NO_WO = '" + this._flexM["NO_WO"].ToString() + "' AND CD_OP = '" + this._flexD["CD_OP"].ToString() + "' ", "", DataViewRowState.CurrentRows);
                    if (main_row == null || main_row.Length < 1)
                    {
                        this.ShowMessage(공통메세지._이가존재하지않습니다, this.DD("실적이력"));
                    }
                    else
                    {
                        int num2 = (int)new P_PR_WORK_HST_SUB01(main_row, this.MainFrameInterface, iStartQty, iWorkQty, iQT_PO, this.cbo공장.Text.Substring(0, this.cbo공장.Text.IndexOf("(")), this._flexM["NM_ITEM"].ToString(), this._flexM["STND_ITEM"].ToString(), this._flexM["UNIT_IM"].ToString(), 공정외주여부, "Y", this._flexD["NO_PO"].ToString(), this._flexD.CDecimal(this._flexD["NO_POLINE"])).ShowDialog();
                        this.조회후해당행찾기("QT_WORK");
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn공정재작업처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog)
                    return;
                if (!this._flexM.HasNormalRow || !this._flexD.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flexD.Row >= this._flexD.Rows.Fixed && this._flexD.Row < this._flexD.Rows.Count && this._flexD.CDecimal(this._flexD["QT_REWORKREMAIN"]) <= 0M)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this._flexD.Cols["QT_REWORKREMAIN"].Caption, "0");
                }
                else
                {
                    string[] loa_param = new string[21];
                    int row = this._flexD.Row;
                    loa_param[0] = this._flexD["CD_PLANT"].ToString();
                    loa_param[1] = this.cbo공장.Text.Substring(0, this.cbo공장.Text.IndexOf("("));
                    loa_param[2] = this._flexD["NO_WO"].ToString();
                    loa_param[3] = this._flexD["CD_WC"].ToString();
                    loa_param[4] = this._flexD["NM_WC"].ToString();
                    loa_param[5] = this._flexD["CD_OP"].ToString();
                    loa_param[6] = this._flexD["NM_OP"].ToString();
                    loa_param[7] = this._flexD["CD_ITEM"].ToString();
                    loa_param[8] = this._flexM["NM_ITEM"].ToString();
                    loa_param[9] = this._flexM["STND_ITEM"].ToString();
                    loa_param[10] = this._flexM["UNIT_IM"].ToString();
                    decimal num3 = this._flexD.CDecimal(this._flexD["QT_OUT_REJECT"]) - this._flexD.CDecimal(this._flexD["QT_OUT_REWORK"]) - this._flexD.CDecimal(this._flexD["QT_OUT_BAD"]);
                    loa_param[11] = D.GetInt(this._flexD["QT_OUT_REJECT"]).ToString();
                    string[] strArray1 = loa_param;
                    int num4 = D.GetInt(this._flexD["QT_OUT_REWORK"]);
                    string str1 = num4.ToString();
                    strArray1[12] = str1;
                    string[] strArray2 = loa_param;
                    num4 = D.GetInt(this._flexD["QT_OUT_BAD"]);
                    string str2 = num4.ToString();
                    strArray2[13] = str2;
                    string[] strArray3 = loa_param;
                    num4 = D.GetInt(this._flexD["QT_REWORKREMAIN"]);
                    string str3 = num4.ToString();
                    strArray3[14] = str3;
                    loa_param[15] = this._flexM["YN_LOT"].ToString();
                    loa_param[16] = this.YN_PR_MNG_LOT;
                    loa_param[17] = "Y";
                    loa_param[18] = "Y";
                    loa_param[19] = this._flexM["NO_PO"].ToString();
                    loa_param[20] = this._flexM["NO_POLINE"].ToString();
                    if (new P_PR_WORK_SUB(this._flexM.DataView[this._flexM.Row - 1].Row, this._flexD.DataView[this._flexD.Row - 1].Row, loa_param, this._flexD.DataView, row, this._flexD.TopRow).ShowDialog() != DialogResult.OK)
                        return;
                    this._flexD.AcceptChanges();
                    this.조회후해당행찾기("QT_WORK");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn투입상세_Click(object sender, EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog)
                    return;
                if (!this._flexM.HasNormalRow || !this._flexD.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flexD.Row >= this._flexD.Rows.Fixed && this._flexD.Row < this._flexD.Rows.Count && this._flexD.CDecimal(this._flexD["QT_WO_WORK"]) <= 0M)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this._flexD.Cols["QT_WO_WORK"].Caption, "0");
                }
                else
                {
                    object[] args = new object[] { new string[] { this.cbo공장.SelectedValue.ToString(),
                                                                  this._flexM["DT_REL"].ToString(),
                                                                  this._flexM["DT_DUE"].ToString(),
                                                                  this._flexD["CD_WC"].ToString(),
                                                                  this._flexM["CD_ITEM"].ToString(),
                                                                  this._flexM["NO_WO"].ToString(),
                                                                  this._flexD["NM_WC"].ToString(),
                                                                  this._flexM["NM_ITEM"].ToString() },
                                                                  this.MainFrameInterface };
                    if (this.IsExistPage("P_PR_II_SCH01", true))
                        this.UnLoadPage("P_PR_II_SCH01", false);
                    this.CallOtherPageMethod("P_PR_II_SCH01", MA.PageName("P_PR_II_SCH01"), this.Grant, args);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn양품체크_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexID.HasNormalRow) return;

                if (this.cur순번from.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(From)의 값", "0");
                    return;
                }

                if (this.cur순번to.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(to)의 값", "0");
                    return;
                }

                if (this.cur순번to.DecimalValue <= this.cur순번from.DecimalValue)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번to.Text, this.cur순번from.Text);
                    return;
                }

                for (int row = this._flexID.Rows.Fixed; row < this._flexID.Rows.Count; ++row)
                {
                    if (D.GetDecimal(this._flexID[row, "SEQ_WO"]) >= cur순번from.DecimalValue && D.GetDecimal(this._flexID[row, "SEQ_WO"]) <= cur순번to.DecimalValue)
                    {
                        if (D.GetString(this._flexID[row, "YN_BAD"]) == "Y")
                        {
                            this.ShowMessage("해당 순번 중 불량체크된 건이 있습니다.");
                            break;
                        }
                        else
                            this._flexID.SetCellCheck(row, this._flexID.Cols["YN_GOOD"].Index, CheckEnum.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void btn불량체크_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexID.HasNormalRow) return;

                if (this.cur순번from.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(From)의 값", "0");
                    return;
                }

                if (this.cur순번to.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(to)의 값", "0");
                    return;
                }

                if (this.cur순번to.DecimalValue <= this.cur순번from.DecimalValue)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번to.Text, this.cur순번from.Text);
                    return;
                }

                for (int row = this._flexID.Rows.Fixed; row < this._flexID.Rows.Count; ++row)
                {
                    if (D.GetDecimal(this._flexID[row, "SEQ_WO"]) >= cur순번from.DecimalValue && D.GetDecimal(this._flexID[row, "SEQ_WO"]) <= cur순번to.DecimalValue)
                    {
                        if (D.GetString(this._flexID[row, "YN_GOOD"]) == "Y")
                        {
                            this.ShowMessage("해당 순번 중 양품체크된 건이 있습니다.");
                            break;
                        }
                        this._flexID.SetCellCheck(row, this._flexID.Cols["YN_BAD"].Index, CheckEnum.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn공정불량처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog)
                    return;
                if (!this._flexM.HasNormalRow || !this._flexD.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flexD.Row >= this._flexD.Rows.Fixed && this._flexD.Row < this._flexD.Rows.Count && this._flexD.CDecimal(this._flexD["QT_REWORKREMAIN"]) <= 0M)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this._flexD.Cols["QT_REWORKREMAIN"].Caption, "0");
                }
                else
                {
                    DataRow row1 = this._flexM.DataView[this._flexM.Row - 1].Row;
                    DataRow row2 = this._flexD.DataView[this._flexD.Row - 1].Row;
                    decimal num3 = this._flexD.CDecimal(this._flexD["QT_OUT_REJECT"]) - this._flexD.CDecimal(this._flexD["QT_OUT_REWORK"]) - this._flexD.CDecimal(this._flexD["QT_OUT_BAD"]);
                    string[] strParameters = new string[21]
                    {
            this._flexD["CD_PLANT"].ToString(),
            this.cbo공장.Text.Substring(0, this.cbo공장.Text.IndexOf("(")),
            this._flexD["NO_WO"].ToString(),
            this._flexD["CD_WC"].ToString(),
            this._flexD["NM_WC"].ToString(),
            this._flexD["CD_OP"].ToString(),
            this._flexD["NM_OP"].ToString(),
            this._flexD["CD_ITEM"].ToString(),
            this._flexM["NM_ITEM"].ToString(),
            this._flexM["STND_ITEM"].ToString(),
            this._flexM["UNIT_IM"].ToString(),
            this._flexD["QT_OUT_REJECT"].ToString(),
            this._flexD["QT_OUT_REWORK"].ToString(),
            this._flexD["QT_OUT_BAD"].ToString(),
            num3.ToString(this._flexD.Cols["QT_REWORKREMAIN"].Format),
            this._flexM["YN_LOT"].ToString(),
            this.YN_PR_MNG_LOT,
            "Y",
            "Y",
            this._flexM["NO_PO"].ToString(),
            this._flexM["NO_POLINE"].ToString()
                    };
                    if (new P_PR_BADWORK_SUB(row1, row2, strParameters).ShowDialog() != DialogResult.OK)
                        return;
                    this._flexD.AcceptChanges();
                    this.조회후해당행찾기("QT_WORK");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:
                    case HelpID.P_PR_WO_REG_SUB:
                        if (!this.공장선택여부)
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                            this.cbo공장.Focus();
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        break;
                    case HelpID.P_MA_WC_SUB1:
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        break;
                    case HelpID.P_PR_WCOP_SUB1:
                        if (D.GetString(this.bpc작업장.SelectedValue) == string.Empty)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.bpc작업장.Text);
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.HelpParam.P20_CD_WC = this.bpc작업장.QueryWhereIn_Pipe;
                        break;
                    case HelpID.P_PR_TPWO_SUB1:
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx작업지시번호_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ctx작업지시번호from.SetCode(this.ctx작업지시번호to.CodeValue, this.ctx작업지시번호to.CodeName);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                this.txt규격.Text = this.txt단위.Text = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID != HelpID.P_MA_PITEM_SUB)
                    return;
                DataRow[] rows = e.HelpReturn.Rows;
                this.txt규격.Text = rows[0]["STND_ITEM"].ToString();
                this.txt단위.Text = rows[0]["UNIT_IMNM"].ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn불량내역등록_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowRejectHelp();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void 조회후해당행찾기(string strFindCol)
        {
            this._flexM["NO_WO"].ToString();
            string strFind = this._flexD["CD_OP"].ToString();
            object[] objArray = new object[] { this.LoginInfo.CompanyCode,
                                               this.cbo공장.SelectedValue.ToString(),
                                               this._flexM["NO_WO"].ToString(),
                                               this._flexM["NO_PO"].ToString(),
                                               this._flexM.CDecimal(this._flexM["NO_POLINE"]),
                                               this.chk작업지시비고적용여부.Checked ? "Y" : "N",
                                               this.LoginInfo.EmployeeNo,
                                               Global.SystemLanguage.MultiLanguageLpoint };
            DataTable dataTable = !MA.ServerKey(false, "BKSEMS") ? this._biz.SearchDetail(objArray) : this._biz.SearchDetail_Chcoef(objArray);
            this._flexM.DetailGrids[0].DataTable.PrimaryKey = new DataColumn[]
            {
                this._flexM.DetailGrids[0].DataTable.Columns["NO_WO"],
                this._flexM.DetailGrids[0].DataTable.Columns["CD_OP"],
                this._flexM.DetailGrids[0].DataTable.Columns["NO_PO"],
                this._flexM.DetailGrids[0].DataTable.Columns["NO_POLINE"]
            };
            foreach (DataRow row in dataTable.Rows)
                this._flexM.DetailGrids[0].DataTable.LoadDataRow(row.ItemArray, true);
            this._flexM.DetailGrids[0].AcceptChanges();

            if (this.dtWork != null)
            {
                this.dtWork.Rows.Clear();
                this.dtWork.AcceptChanges();
            }
            if (this._dtReject != null)
            {
                this._dtReject.Rows.Clear();
                this._dtReject.AcceptChanges();
            }
            if (this._dtLotItem != null)
            {
                this._dtLotItem.Rows.Clear();
                this._dtLotItem.AcceptChanges();
            }
            if (this._dtSERL != null)
            {
                this._dtSERL.Rows.Clear();
                this._dtSERL.AcceptChanges();
            }
            if (this._dtMatl != null)
            {
                this._dtMatl.Rows.Clear();
                this._dtMatl.AcceptChanges();
            }
            if (this._dtLotMatl != null)
            {
                this._dtLotMatl.Rows.Clear();
                this._dtLotMatl.AcceptChanges();
            }
            if (this._dtInsp != null)
            {
                this._dtInsp.Rows.Clear();
                this._dtInsp.AcceptChanges();
            }
            if (!this._flexD.HasNormalRow)
                return;
            int row1 = this._flexD.FindRow(strFind, this._flexD.Rows.Fixed, this._flexD.Cols["CD_OP"].Index, false, true, true);
            if (row1 < 0)
                row1 = this._flexD.Rows.Fixed;
            this._flexD.Focus();
            this._flexD.Select(row1, this._flexD.Cols[strFindCol].Index);
        }

        private void 중복체크함수(ref P_CZ_PR_OPOUT_WORK_REG.stDetailQuery st인자)
        {
            if (this.htDetailQueryCollection.Contains(this.st))
            {
                ++this.st.NO_LINE2;
                this.중복체크함수(ref st인자);
            }
            else
                this.htDetailQueryCollection.Add(this.st, this.st);
        }

        private bool ShowRejectHelp()
        {
            if (BasicInfo.ActiveDialog)
                return false;
            if (!this._flexM.HasNormalRow || !this._flexD.HasNormalRow)
            {
                this.ShowMessage(공통메세지.선택된자료가없습니다);
                return false;
            }
            this.dtWork = this._flexD.GetChanges();
            if (this.dtWork == null)
                return false;
            if (this.dtWork.Rows.Count > 1)
            {
                this.ShowMessage("변경이 행이 @개입니다. 로직상 변경된 행은 1개만 되야합니다.", this.dtWork.Rows.Count);
                return false;
            }
            this.drs = this._flexD.DataTable.Select("NO_WO = '" + this.dtWork.Rows[0]["NO_WO"].ToString() + "' AND CD_OP = '" + this.dtWork.Rows[0]["CD_OP"].ToString() + "'  AND NO_PO = '" + this.dtWork.Rows[0]["NO_PO"].ToString() + "'  AND NO_POLINE = " + this.dtWork.Rows[0]["NO_POLINE"].ToString() + " ");
            DataRow[] main_row = this._flexD.DataTable.Select("NO_WO = '" + this._flexM["NO_WO"].ToString() + "' AND NO_PO = '" + this._flexM["NO_PO"].ToString() + "' AND NO_POLINE = " + this._flexM["NO_POLINE"].ToString() + " ", "", DataViewRowState.CurrentRows);
            DataRow[] id_row = this._flexID.DataTable.Select("YN_BAD = 'Y'");
            decimal qt_reject = 0M;
            if (this.drs != null && this.drs.Length == 1 && (this.drs[0].RowState == DataRowState.Unchanged || this.drs[0].RowState == DataRowState.Modified))
                qt_reject = D.GetDecimal(this.drs[0]["QT_REJECT"]);
            if (qt_reject == 0M)
            {
                this.ShowMessage(공통메세지._은_보다커야합니다, this.DD("불량수량"), "0");
                return false;
            }
            if (qt_reject != id_row.Length)
			{
                this.ShowMessage("불량수량과 불량체크된 수량이 다릅니다.");
                return false;
			}
            if (this._dtReject != null && this._dtReject.Rows.Count > 0)
            {
                this.ShowMessage("불량내역이 등록되어 있습니다.");
                return false;
            }
            string strNO_SFT = this.drs[0]["NO_SFT"].ToString();
            DataSet plantCfg = Pr_ComFunc.Get_Plant_Cfg(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       D.GetString(this.dtWork.Rows[0]["CD_PLANT"]) });
            P_CZ_PR_WORK_SUB2 pPrWorkSuB02;
            if (this.LoginInfo.MngLot == "Y" && this._flexM["YN_LOT"].ToString() == "Y")
            {
                if (D.GetString(plantCfg.Tables[1].Rows[0]["YN_AUTOBAD"]) == "Y" && (D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "000" || D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "001") && D.GetString(this._flexD[this._flexD.RowSel, "NO_LOT"]) == string.Empty)
                {
                    this.ShowMessage("LOT관리 업체에서 공정불량자동처리일 경우\nLOT번호는 필수입력항목 입니다.", "EK1");
                    return false;
                }
                pPrWorkSuB02 = new P_CZ_PR_WORK_SUB2(main_row, this.MainFrameInterface, this.cbo공장.Text, qt_reject, this._flexM["NM_ITEM"].ToString(), this._flexM["STND_ITEM"].ToString(), this._flexM["UNIT_IM"].ToString(), strNO_SFT, D.GetString(this._flexD[this._flexD.RowSel, "NO_LOT"]));
            }
            else
                pPrWorkSuB02 = new P_CZ_PR_WORK_SUB2(main_row, this.MainFrameInterface, this.cbo공장.Text, qt_reject, this._flexM["NM_ITEM"].ToString(), this._flexM["STND_ITEM"].ToString(), this._flexM["UNIT_IM"].ToString(), strNO_SFT, id_row);
            if (pPrWorkSuB02.ShowDialog() != DialogResult.OK || pPrWorkSuB02 == null)
                return false;
            this._dtReject = (DataTable)((IHelpWindow)pPrWorkSuB02).ReturnValues[0];
            foreach (DataRow dr in _dtReject.Rows)
			{
                string NoId = dr["NO_ID"].ToString();
                DataRow[] drId = this._flexID.DataTable.Select("NO_ID = '" + NoId + "'");
                drId[0]["CD_REJECT"] = dr["CD_REJECT"];
                drId[0]["CD_RESOURCE"] = dr["CD_RESOURCE"];
			}
            return true;
        }

        private string 마감구분
        {
            get
            {
                string str = "";
                if (this.chk미마감.Checked && this.chk마감.Checked)
                    str = "S|R|C|";
                else if (this.chk미마감.Checked && !this.chk마감.Checked)
                    str = "S|R|";
                else if (!this.chk미마감.Checked && this.chk마감.Checked)
                    str = "C|";
                else if (!this.chk미마감.Checked && !this.chk마감.Checked)
                    str = "S|R|C|";
                return str;
            }
        }

        private bool 공장선택여부 => this.cbo공장.SelectedValue != null && !(this.cbo공장.SelectedValue.ToString() == "");

        public struct stDetailQuery
        {
            public string NO_IO;
            public string NO_LOT;
            public int NO_LINE2;
        }
    }
    #endregion
}
