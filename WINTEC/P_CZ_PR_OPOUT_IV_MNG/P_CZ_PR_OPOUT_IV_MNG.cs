using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_OPOUT_IV_MNG : PageBase
	{
        private DataTable _dtReject = new DataTable();
        private string 지급관리통제설정 = "N";
        private bool bGridrowChanging = false;
        private P_CZ_PR_OPOUT_IV_MNG_BIZ _biz = new P_CZ_PR_OPOUT_IV_MNG_BIZ();
        private string s_CD_PLANT = string.Empty;
        private string s_DT_IV = string.Empty;
        private string s_NO_EMP = string.Empty;
        private string s_NM_KOR = string.Empty;
        private string s_YN_DOCU = string.Empty;
        private bool b_CallPage = false;
        private string _NO_MODULE = "";
        private string _NO_MDOCU = "";
        public P_CZ_PR_OPOUT_IV_MNG()
		{
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_PR_OPOUT_IV_MNG( string CD_PLANT, string DT_IV, string NO_EMP, string NM_KOR, string YN_DOCU)
        {
            try
            {
                this.InitializeComponent();

                this.s_CD_PLANT = CD_PLANT;
                this.s_DT_IV = DT_IV;
                this.s_NO_EMP = NO_EMP;
                this.s_NM_KOR = NM_KOR;
                this.s_YN_DOCU = YN_DOCU;
                this.b_CallPage = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_PR_OPOUT_IV_MNG(string NO_MODULE, string NO_MDOCU)
        {
            this.InitializeComponent();

            this._NO_MODULE = NO_MODULE;
            this._NO_MDOCU = NO_MDOCU;
        }

        public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
        {
            object[] args = e.Args;
            this._NO_MODULE = D.GetString(args[0]);
            this._NO_MDOCU = D.GetString(args[1]);
            this.InitPaint();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitMaExc();
            this.InitGrid();
            this.InitEvent();
        }

		private void InitMaExc()
        {
            this.지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");
            if (!(this.지급관리통제설정 == string.Empty) && !(this.지급관리통제설정 == "000"))
                return;
            this.지급관리통제설정 = "N";
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexM, this._flexD };
            this._flexM.DetailGrids = new FlexGrid[] { this._flexD };

            #region _flexM

            this._flexM.BeginSetting(1, 1, false);
            this._flexM.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM.SetCol("NO_IV", "계산서번호", 100, false);
            this._flexM.SetCol("DT_IV", "처리일자", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("CD_PLANT", "공장코드", false);
            this._flexM.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexM.SetCol("LN_PARTNER", "거래처명", 140, false);
            this._flexM.SetCol("CD_BIZAREA_TAX", "부가세사업장", 100, false);
            this._flexM.SetCol("NM_BIZAREA_TAX", "부가세사업장명", 150, false);
            this._flexM.SetCol("NM_FG_TAX", "과세구분", 100, false);
            this._flexM.SetCol("AM_CLS", "공급가액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_HAP", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("CD_EXCH", "환종코드", false);
            this._flexM.SetCol("NM_EXCH", "환종명", 100, false);
            this._flexM.SetCol("RT_EXCH", "환율", false);
            this._flexM.SetCol("FG_TAX", "과세구분", false);
            this._flexM.SetCol("FG_TPPURCHASE", "매입유형코드", false);
            this._flexM.SetCol("NM_TP", "매입유형", 100, false);
            this._flexM.SetCol("CD_CC", "C/C코드", false);
            this._flexM.SetCol("NM_CC", "C/C", 100, false);
            this._flexM.SetCol("DC_RMK", "비고", 100, 100, false);
            this._flexM.SetCol("FG_PAYBILL", "지급조건", 80, false, typeof(string));
            this._flexM.SetCol("DT_PAY_PREARRANGED", "지급예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("DT_DUE", "만기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("YN_DOCU", "전표처리여부", 90, false);
            this._flexM.SetCol("NO_DOCU", "전표번호", 100, false);
            this._flexM.SetCol("CD_PC", "회계단위코드", false);
            this._flexM.SetCol("NM_PC", "회계단위", false);
            this._flexM.VerifyNotNull = new string[] { "CD_PARTNER" };
            this._flexM.SettingVersion = "1.0.0.1";
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM.SetDummyColumn("CHK");
            this._flexM.Cols["YN_DOCU"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.BeforeRowChange += new RangeEventHandler(this._flexM_BeforeRowChange);
            this._flexM.AfterRowChange += new RangeEventHandler(this.Grid_AfterRowChange);
            this._flexM.CheckHeaderClick += new EventHandler(this.Grid_CheckHeaderClick);
            this._flexM.DoubleClick += new EventHandler(this._flex_DoubleClick);

            #endregion

            #region _flexD

            this._flexD.BeginSetting(1, 1, false);
            this._flexD.SetCol("NO_IV", "계산서번호", false);
            this._flexD.SetCol("NO_LINE", "항번", false);
            this._flexD.SetCol("CD_PLANT", "공장코드", false);
            this._flexD.SetCol("CD_PARTNER", "거래처코드", false);
            this._flexD.SetCol("NO_WORK", "실적번호", 100, false);
            this._flexD.SetCol("NO_WO", "작업지시번호", false);
            this._flexD.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("UM_MATL", "도급자재비", 90, 19, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("UM_SOUL", "임가공비", 90, 19, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("UM_EXCLS", "외화단가", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("UM_CLS", "단가", 90, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexD.SetCol("AM_EXCLS", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("AM_CLS", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("AM_HAP", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD.SetCol("NO_POLINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("CD_OP", "OP", false);
            this._flexD.SetCol("CD_WC", "작업장코드", false);
            this._flexD.SetCol("NM_WC", "작업장", false);
            this._flexD.SetCol("CD_WCOP", "공정코드", false);
            this._flexD.SetCol("NM_OP", "공정", false);
            this._flexD.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexD.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_WORK_CHCOEF", "실적수량(변환)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_WORK_BAD_CHCOEF", "불량수량(변환)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("CD_PJT", "프로젝트코드", 100);
            this._flexD.SetCol("NM_PROJECT", "프로젝트명", 100);
            if (Config.MA_ENV.프로젝트사용)
                this._flexD.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            this._flexD.SetCol("FG_TPPURCHASE", "매입유형코드", false);
            this._flexD.SetCol("NM_TP", "매입유형", 100, false);
            this._flexD.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexD.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            this._flexD.SetCol("MAT_ITEM", "재질", false);
            this._flexD.SetCol("NM_MAKER", "Maker", false);
            this._flexD.SetCol("BARCODE", "BARCODE", false);
            this._flexD.SetCol("NO_MODEL", "모델번호", false);
            if (MA.ServerKey(false, "JIGLS"))
                this._flexD.SetCol("TXT_USERDEF1_WO", "규격변경", 80, false);
            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD.SetExceptSumCol("NO_POLINE");
            this._flexD.Cols["NO_POLINE"].TextAlign = TextAlignEnum.CenterCenter;

            #endregion

		}

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn미결전표처리.Click += new EventHandler(this.btn미결전표처리_Click);
            this.btn미결전표처리취소.Click += new EventHandler(this.btn미결전표처리취소_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.InitControl();
            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.bpP_Date.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            if (this.b_CallPage)
            {
                this.cbo공장.SelectedValue = this.s_CD_PLANT;
                this.dtp처리일자.StartDateToString = this.s_DT_IV;
                this.dtp처리일자.EndDateToString = this.s_DT_IV;
                this.ctx담당자.CodeValue = this.s_NO_EMP;
                this.ctx담당자.CodeName = this.s_NM_KOR;
                this.cbo전표처리여부.SelectedValue = this.s_YN_DOCU;
                this.OnToolBarSearchButtonClicked(null, null);
            }
            else
            {
                this.dtp처리일자.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
                this.dtp처리일자.EndDateToString = this.MainFrameInterface.GetStringToday;
            }
            if (string.IsNullOrEmpty(this._NO_MODULE))
                return;
            DataTable dataTable = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                  string.Empty,
                                                                  string.Empty,
                                                                  string.Empty,
                                                                  string.Empty,
                                                                  string.Empty,
                                                                  string.Empty,
                                                                  string.Empty,
                                                                  Global.SystemLanguage.MultiLanguageLpoint,
                                                                  this._NO_MDOCU });
            if (dataTable.Rows.Count > 0)
            {
                this.cbo공장.SelectedValue = D.GetString(dataTable.Rows[0]["CD_PLANT"]);
                this.dtp처리일자.StartDateToString = D.GetString(dataTable.Rows[0]["DT_IV"]);
                this.dtp처리일자.EndDateToString = D.GetString(dataTable.Rows[0]["DT_IV"]);
                this.ctx담당자.SetCode(D.GetString(dataTable.Rows[0]["NO_EMP"]), D.GetString(dataTable.Rows[0]["NM_KOR"]));
                this.ctx외주처.SetCode(D.GetString(dataTable.Rows[0]["CD_PARTNER"]), D.GetString(dataTable.Rows[0]["LN_PARTNER"]));
                if (D.GetString(dataTable.Rows[0]["YN_DOCU"]) != string.Empty)
                    this.cbo전표처리여부.SelectedValue = D.GetString(dataTable.Rows[0]["YN_DOCU"]);
                this._flexM.Binding = dataTable;
            }
        }

        private void InitControl()
        {
            DataSet comboData = this.GetComboData("NC;MA_PLANT", "S;MA_B000057", "N;PU_C000044");
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;
            this.cbo전표처리여부.DataSource = comboData.Tables[1];
            this.cbo전표처리여부.DisplayMember = "NAME";
            this.cbo전표처리여부.ValueMember = "CODE";
            if (this.지급관리통제설정 == "N")
            {
                this._flexM.SetDataMap("FG_PAYBILL", comboData.Tables[2], "CODE", "NAME");
            }
            else
            {
                DataTable payList = ComFunc.GetPayList();
                if (payList != null)
                    this._flexM.SetDataMap("FG_PAYBILL", payList, "CODE", "NAME");
            }
            if (!MA.ServerKey(false, "NOVAREX"))
                return;
            this.btn삭제.Visible = false;
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
                this.ToolBarPrintButtonEnabled = true;
                this.btn미결전표처리.Enabled = this._flexM.HasNormalRow;
                this.btn미결전표처리취소.Enabled = this._flexM.HasNormalRow;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Check(string Gubun)
        {
            Hashtable HList = new Hashtable();
            if ("SEARCH" == Gubun)
            {
                HList.Add(this.cbo공장, this.lbl공장);
            }
            else
            {
                LabelExt labelExt = new LabelExt();
                labelExt.Text = this.ctx담당자.Text;
                HList.Add(this.cbo공장, this.lbl공장);
                HList.Add(this.ctx담당자, labelExt);
                HList.Add(this.dtp처리일자.StartDateToString, this.lbl처리일자);
                HList.Add(this.dtp처리일자.EndDateToString, this.lbl처리일자);
            }
            return Checker.IsValid(this.dtp처리일자, true, this.lbl처리일자.Text) && ComFunc.NullCheck(HList);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch() || !this.Check("SEARCH"))
                    return;
                this._flexM.Binding = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.dtp처리일자.StartDateToString,
                                                                      this.dtp처리일자.EndDateToString,
                                                                      this.ctx담당자.CodeValue,
                                                                      this.ctx외주처.CodeValue,
                                                                      this.cbo전표처리여부.SelectedValue.ToString(),
                                                                      this.LoginInfo.EmployeeNo,
                                                                      Global.SystemLanguage.MultiLanguageLpoint });
                if (!this._flexM.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flexM.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'", "");
                if (dataRowArray.Length == 0)
                    return;
                foreach (DataRow dataRow in dataRowArray)
                {
                    if (dataRow["YN_DOCU"].ToString() == "Y")
                    {
                        this.ShowMessage("체크된 건중에 전표처리가 된 건이 있습니다. 삭제할 수 없습니다.");
                        return;
                    }
                }
                foreach (DataRow dataRow1 in dataRowArray)
                {
                    string[] strArray = new string[] { "CD_PLANT = '",
                                                       dataRow1["CD_PLANT"].ToString(),
                                                       "' AND NO_IV = '",
                                                       dataRow1["NO_IV"].ToString(),
                                                       "' " };
                    foreach (DataRow dataRow2 in this._flexD.DataTable.Select(string.Concat(strArray), ""))
                        dataRow2.Delete();
                    dataRow1.Delete();
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
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;
                this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow || !this.BeforePrint())
                    return;
                ReportHelper rptHelper = new ReportHelper("R_PR_OPOUT_IV_MNG_0", "공정외주매입관리");
                rptHelper.가로출력();
                rptHelper.SetData("공장코드", this.cbo공장.SelectedValue.ToString());
                rptHelper.SetData("공장명", this.cbo공장.Text.Substring(0, this.cbo공장.Text.IndexOf("(")));
                rptHelper.SetData("처리일자시작", this.dtp처리일자.StartDateToString);
                rptHelper.SetData("처리일자끝", this.dtp처리일자.EndDateToString);
                rptHelper.SetData("담당자", this.ctx담당자.CodeValue);
                rptHelper.SetData("외주처", this.ctx외주처.CodeValue);
                rptHelper.SetData("전표처리여부", this.cbo전표처리여부.SelectedValue.ToString());
                if (!this.PrintGridSetting(ref this._flexM, ref this._flexD, "NO_IV", "NO_IV", ref rptHelper))
                    return;
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool PrintGridSetting( ref FlexGrid _flexM, ref FlexGrid _flexD, string strFilterCondition, string strSearchCondition, ref ReportHelper rptHelper)
        {
            string strFilter1 = "";
            if (strSearchCondition == "")
                strSearchCondition = strFilterCondition;
            if (strFilterCondition != "")
            {
                if (!_flexM.HasNormalRow)
                    return false;
                DataRow[] dataRowArray = _flexM.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray.Length == 0)
                    return false;
                string strFilter2 = strFilterCondition + " IN (";
                for (int index = 0; index < dataRowArray.Length; ++index)
                {
                    string str = strFilter2 + " '" + dataRowArray[index][strSearchCondition].ToString() + "'";
                    strFilter2 = dataRowArray.Length - 1 == index ? str + ") " : str + ", ";
                }
                DataTable printTable = this.GetPrintTable(ref _flexD, strFilter2);
                if (printTable == null)
                    return false;
                this.CaptionMapping(new FlexGrid[] { _flexM, _flexD }, ref printTable);
                rptHelper.SetDataTable(printTable);
            }
            else
            {
                DataTable printTable = this.GetPrintTable(ref _flexM, strFilter1);
                if (printTable == null)
                    return false;
                this.CaptionMapping(new FlexGrid[] { _flexM, _flexD }, ref printTable);
                rptHelper.SetDataTable(printTable);
            }
            return true;
        }

        private DataTable GetPrintTable(ref FlexGrid _flex, string strFilter)
        {
            DataRow[] dataRowArray = !_flex.DataTable.Columns.Contains("CHK") ? _flex.DataTable.Select(strFilter, _flex.DataTable.Columns[0].ColumnName) : _flex.DataTable.Select("CHK = 'Y'", _flex.DataTable.Columns[0].ColumnName);
            if (dataRowArray.Length == 0)
                return null;
            DataTable printTable = _flex.DataTable.Clone();
            foreach (DataRow row in dataRowArray)
                printTable.ImportRow(row);
            return printTable;
        }

        private void CaptionMapping(FlexGrid[] _flexArr, ref DataTable dt)
        {
            foreach (DataColumn column in dt.Columns)
            {
                foreach (FlexGrid flexGrid in _flexArr)
                {
                    if (flexGrid.Cols.Contains(column.ColumnName))
                        column.Caption = flexGrid.Cols[column.ColumnName].Caption;
                }
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify())
                return false;
            DataTable changes1 = this._flexM.GetChanges();
            DataTable changes2 = this._flexD.GetChanges();
            if (changes1 == null && changes2 == null)
                return true;
            if (!this._biz.Save(changes1, changes2))
                return false;
            this._flexM.AcceptChanges();
            this._flexD.AcceptChanges();
            return true;
        }

        private void btn미결전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexM.DataTable.Select("CHK = 'Y' ", "", DataViewRowState.CurrentRows);
                    DataRow[] dataRowArray2 = this._flexM.DataTable.Select("CHK = 'Y' AND YN_DOCU = 'N' ", "", DataViewRowState.CurrentRows);
                    if (dataRowArray1.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else if (dataRowArray1.Length != dataRowArray2.Length)
                    {
                        this.ShowMessage("체크된 건 중에 전표처리가 된 건(전표처리여부 = 'Y')이 있습니다.");
                    }
                    else
                    {
                        foreach (DataRow dataRow in dataRowArray2)
                        {
                            if (this._flexD.DataTable.Select("CD_PLANT = '" + dataRow["CD_PLANT"].ToString() + "' AND NO_IV = '" + dataRow["NO_IV"].ToString() + "'", "", DataViewRowState.CurrentRows).Length == 0)
                            {
                                this.ShowMessage("체크된 건중 계산서번호(@)의 실적이 없습니다. 실적없이 전표처리를 할 수 없습니다.", dataRow["NO_IV"].ToString());
                                return;
                            }
                        }
                        DataTable dt = this._flexM.DataTable.Clone();
                        foreach (DataRow dataRow in dataRowArray2)
                            dt.Rows.Add(dataRow.ItemArray);
                        if (this._biz.TransferDocu(dt))
                        {
                            this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                            this.OnToolBarSearchButtonClicked(null, null);
                        }
                        else
                        {
                            this.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn미결전표처리취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexM.DataTable.Select("CHK = 'Y' ", "", DataViewRowState.CurrentRows);
                    DataRow[] dataRowArray2 = this._flexM.DataTable.Select("CHK = 'Y' AND YN_DOCU = 'Y' ", "", DataViewRowState.CurrentRows);
                    if (dataRowArray1.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else if (dataRowArray1.Length != dataRowArray2.Length)
                    {
                        this.ShowMessage("체크된 건 중에 전표처리가 되지 않은 건(전표처리여부 = 'N')이 있습니다.");
                    }
                    else
                    {
                        DataTable dt = this._flexM.DataTable.Clone();
                        foreach (DataRow dataRow in dataRowArray2)
                            dt.Rows.Add(dataRow.ItemArray);
                        if (this._biz.TransferDocuCancel(dt))
                        {
                            this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                            this.OnToolBarSearchButtonClicked(null, null);
                        }
                        else
                        {
                            this.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                this._flexD.Rows.Remove(this._flexD.Row);
                if (this._flexM.HasNormalRow)
                {
                    object obj1 = this._flexD.DataTable.Compute("SUM(AM_CLS)", "");
                    object obj2 = this._flexD.DataTable.Compute("SUM(AM_VAT)", "");
                    object obj3 = this._flexD.DataTable.Compute("SUM(AM_HAP)", "");
                    decimal result1 = 0M;
                    decimal result2 = 0M;
                    decimal result3 = 0M;
                    decimal.TryParse(obj1.ToString(), out result1);
                    decimal.TryParse(obj2.ToString(), out result2);
                    decimal.TryParse(obj3.ToString(), out result3);
                    this._flexM["AM_CLS"] = result1;
                    this._flexM["AM_VAT"] = result2;
                    this._flexM["AM_HAP"] = result3;
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

        private void Grid_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.ToolBarSearchButtonEnabled = false;
                this.bGridrowChanging = false;
                DataTable dt = (DataTable)null;
                string filter = "CD_PLANT = '" + this._flexM["CD_PLANT"].ToString() + "' AND NO_IV = '" + this._flexM["NO_IV"].ToString() + "' ";
                string str = this._flexM["NO_IV"].ToString();
                if (this._flexM.DetailQueryNeed)
                    dt = this._biz.SearchDetail(new object[] { this.LoginInfo.CompanyCode,
                                                               this.cbo공장.SelectedValue.ToString(),
                                                               this.dtp처리일자.StartDateToString,
                                                               this.dtp처리일자.EndDateToString,
                                                               this.ctx담당자.CodeValue,
                                                               this.ctx외주처.CodeValue,
                                                               this.cbo전표처리여부.SelectedValue.ToString(),
                                                               str,
                                                               this.LoginInfo.EmployeeNo,
                                                               Global.SystemLanguage.MultiLanguageLpoint });
                this._flexM.DetailGrids[0].BindingAdd(dt, filter);
                this._flexM.DetailQueryNeed = false;
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

        private void Grid_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow)
                    return;
                this._flexM.Redraw = false;
                this._flexD.Redraw = false;
                int row = this._flexM.Row;
                this._flexM.Row = this._flexM.Rows.Fixed;
                for (int index = 0; index < this._flexM.DataView.Count; ++index)
                {
                    this._flexM.Row = this._flexM.Rows.Fixed + index;
                    if (this._flexM["CHK"].ToString() == "Y")
                        this._flexM.SetCellCheck(this._flexM.Row, this._flexM.Cols["CHK"].Index, CheckEnum.Checked);
                    else
                        this._flexM.SetCellCheck(this._flexM.Row, this._flexM.Cols["CHK"].Index, CheckEnum.Unchecked);
                }
                this._flexM.Row = row;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexM.Redraw = true;
                this._flexD.Redraw = true;
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = (FlexGrid)sender;
                if (!flexGrid.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    switch (flexGrid.Cols[flexGrid.Col].Name)
                    {
                        case "NO_DOCU":
                            if (D.GetString(flexGrid["NO_DOCU"]) != "")
                            {
                                object[] args = new object[] { D.GetString(flexGrid["NO_DOCU"]),
                                                               "1",
                                                               D.GetString(flexGrid["CD_PC"]),
                                                               Global.MainFrame.LoginInfo.CompanyCode };
                                this.CallOtherPageMethod("P_FI_DOCU", MA.PageName("P_FI_DOCU") + "(" + this.PageName + ")", "P_FI_DOCU", this.Grant, args);
                                break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
