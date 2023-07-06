using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using sale;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_BILL_MNG : PageBase
    {
        P_CZ_SA_BILL_MNG_BIZ _biz = null;
        private string _수금전표처리조건전용코드 = "000";
        private string _NO_MODULE = "";
        private string _NO_MDOCU = "";
        private string ServerKey = Global.MainFrame.ServerKeyCommon.ToUpper();
        public P_CZ_SA_BILL_MNG()
        {
            InitializeComponent();
        }
        public P_CZ_SA_BILL_MNG(string NO_MODULE, string NO_MDOCU)
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
            base.InitPaint();
        }

        protected override void InitLoad()
        {
            this._biz = new P_CZ_SA_BILL_MNG_BIZ(this.MainFrameInterface);

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex수금번호H, this._flex수금번호D };

            this._flex수금번호H.BeginSetting(1, 1, false);
            this._flex수금번호H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex수금번호H.SetDummyColumn(new string[] { "S" });
            this._flex수금번호H.SetCol("NO_RCP", "수금번호", 120);
            this._flex수금번호H.SetCol("DT_RCP", "수금일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수금번호H.SetCol("CD_PARTNER_GRP", "거래처그룹", 100);
            this._flex수금번호H.SetCol("NM_PARTNER_GRP", "거래처그룹명", 120);
            this._flex수금번호H.SetCol("CD_PARTNER", "매출처코드", 100);
            this._flex수금번호H.SetCol("LN_PARTNER", "매출처명", 120);
            this._flex수금번호H.SetCol("NM_CEO", "매출처대표자명", false);
            this._flex수금번호H.SetCol("SN_PARTNER", "매출처명(약식)", 120);
            this._flex수금번호H.SetCol("BILL_PARTNER", "수금처코드", 100);
            this._flex수금번호H.SetCol("LN_BILL_PARTNER", "수금처명", 120);
            this._flex수금번호H.SetCol("BILL_NM_CEO", "수금처대표자명", 120);
            this._flex수금번호H.SetCol("AM_RCP_EX", "수금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호H.SetCol("AM_RCP", "수금액(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호H.SetCol("AM_RCP_A_EX", "선수금", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호H.SetCol("AM_RCP_A", "선수금(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호H.SetCol("AM_RCP_EX_TX", "반제액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호H.SetCol("AM_RCP_TX", "반제액(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호H.SetCol("AM_FEE_EX", "수수료", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호H.SetCol("AM_FEE", "수수료(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호H.SetCol("AM_MISC_EX", "잡이익", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호H.SetCol("AM_MISC", "잡이익(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호H.SetCol("NM_TP_BUSI", "거래구분", 80);
            this._flex수금번호H.SetCol("NM_KOR", "담당자", 80);
            this._flex수금번호H.SetCol("NM_SALEGRP", "영업그룹명", 120);
            this._flex수금번호H.SetCol("NM_SALEORG", "영업조직명", 120);
            this._flex수금번호H.SetCol("NM_EXCH", "환종", 60);
            this._flex수금번호H.SetCol("RT_EXCH", "환율", 60);
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SLFIRE" || Global.MainFrame.ServerKeyCommon.ToUpper() == "DELIF")
                this._flex수금번호H.SetCol("DC_RMK", "비고", 100, true);
            else
                this._flex수금번호H.SetCol("DC_RMK", "비고", 100, false);
            this._flex수금번호H.SetCol("FG_BILL_NAME", "수금조건", 100);
            this._flex수금번호H.SetCol("DT_RCP_PREARRANGED", "수금예정일", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수금번호H.SetCol("TP_AIS", "전표처리여부", 100);
            this._flex수금번호H.SetCol("NO_DOCU", "회계전표", 60, false);
            this._flex수금번호H.SetCol("CD_PC", "회계단위", 60, false);
            this._flex수금번호H.SetCol("ST_DOCU", "전표승인여부", 80, false);
            this._flex수금번호H.Cols["CD_PC"].Visible = false;
            this._flex수금번호H.SetCol("CD_BIZAREA", "사업장코드", 80, false);
            this._flex수금번호H.SetCol("NM_BIZAREA", "사업장명", 120, false);
            this._flex수금번호H.SetCol("NM_CD_BANK", "금융기관", 120);
            this._flex수금번호H.SetCol("GI_PARTNER", "납품처코드", 100, false);
            this._flex수금번호H.SetCol("NM_GI_PARTNER", "납품처명", 100, false);
            if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("TYPHC"))
            {
                this._flex수금번호H.SetCol("CD_TP", "수금형태코드", 100, false);
                this._flex수금번호H.SetCol("NM_TP", "수금형태명", 100, false);
                this._flex수금번호H.SetCol("FG_RCP", "수금구분코드", 100, false);
                this._flex수금번호H.SetCol("NM_FG_RCP", "수금구분명", 100, false);
            }
            if (MA.ServerKey(false, new string[] { "CIS", "CIS2" }))
            {
                this._flex수금번호H.SetCol("NO_PROJECT", "프로젝트코드", 100, false);
                this._flex수금번호H.SetCol("NM_PROJECT", "프로젝트코드명", 100, false);
            }
            this._flex수금번호H.SetCol("CD_DEPOSIT", "예적금코드", 100, false);

            this._flex수금번호H.SettingVersion = "7.0.0.0.3";
            this._flex수금번호H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex수금번호L.BeginSetting(1, 1, false);
            this._flex수금번호L.SetCol("NM_FG_RCP", "수금구분", 100);
            this._flex수금번호L.SetCol("NO_MGMT", "관리번호", 120);
            this._flex수금번호L.SetCol("NM_FG_JATA", "자/타수", 80);
            this._flex수금번호L.SetCol("AM_RCP_EX", "수금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호L.SetCol("AM_RCP", "수금액(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호L.SetCol("AM_RCP_A_EX", "선수금", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호L.SetCol("AM_RCP_A", "선수금(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            if (BASIC.GetMAEXC("수금등록-부가세사용여부(외화수금)") == "100")
            {
                this._flex수금번호L.SetCol("AM_RCP_K", "정상수금(공급가액)", 115, false, typeof(decimal), FormatTpType.MONEY);
                this._flex수금번호L.SetCol("VAT", "부가세", 115, false, typeof(decimal), FormatTpType.MONEY);
            }
            this._flex수금번호L.SetCol("NM_CD_BANK", "금융기관", 100);
            this._flex수금번호L.SetCol("DT_DUE", "만기/약정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수금번호L.SetCol("NM_ISSUE", "발행인", 100);
            this._flex수금번호L.SetCol("DC_BANK", "발행기관", 100);
            this._flex수금번호L.SetCol("DY_TURN", "회전일", 100);
            this._flex수금번호L.SetCol("DTS_INSERT", "입력일", 120);
            this._flex수금번호L.SetCol("ID_INSERT", "입력자", 80);
            this._flex수금번호L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex수금번호D.BeginSetting(1, 1, false);
            this._flex수금번호D.SetCol("NO_TX", "마감번호", 100);
            this._flex수금번호D.SetCol("DT_IV", "마감일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수금번호D.SetCol("AM_IV_EX", "대상액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호D.SetCol("AM_IV", "대상액(원화)", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호D.SetCol("RT_EXCH_IV", "마감환율", 80, false, typeof(decimal), (FormatTpType)6);
            this._flex수금번호D.SetCol("AM_PL", "환차손", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex수금번호D.SetCol("AM_RCP_TX_EX", "반제액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금번호D.SetCol("AM_RCP_TX", "반제액(원화)", 100, false, typeof(decimal), FormatTpType.MONEY);
            if (BASIC.GetMAEXC("수금등록-부가세사용여부(외화수금)") == "100")
            {
                this._flex수금번호D.SetCol("AM_IV_VAT", "부가세대상액", 100, false, typeof(decimal), FormatTpType.MONEY);
                this._flex수금번호D.SetCol("AM_RCP_VAT", "부가세반제액", 100, false, typeof(decimal), FormatTpType.MONEY);
                this._flex수금번호D.SetCol("AM_IV_K", "공급가액대상액", 120, false, typeof(decimal), FormatTpType.MONEY);
                this._flex수금번호D.SetCol("AM_RCP_K", "공급가액반제액", 120, false, typeof(decimal), FormatTpType.MONEY);
            }
            this._flex수금번호D.SetCol("DTS_INSERT", "입력일", 120);
            this._flex수금번호D.SetCol("ID_INSERT", "입력자", 80);
            if (MA.ServerKey(true, new string[] { "WGSK" }))
                this._flex수금번호D.SetCol("DT_RCP_RSV", "수금예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수금번호D.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            DataTable dataTable1 = Global.MainFrame.FillDataTable(" SELECT CD_EXC FROM MA_EXC   WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'    AND EXC_TITLE = '수금전표-처리조건' ");
            if (dataTable1.Rows.Count > 0 && dataTable1.Rows[0]["CD_EXC"] != DBNull.Value && dataTable1.Rows[0]["CD_EXC"].ToString().Trim() != string.Empty)
                this._수금전표처리조건전용코드 = dataTable1.Rows[0]["CD_EXC"].ToString().Trim();
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            this.dtp기간.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            DataSet comboData = this.GetComboData(new string[] { "S;MA_AISPOSTH;300", "S;PU_C000016", "S;SA_B000028", "S;SA_B000002" });
            this.cbo수금형태.DataSource = comboData.Tables[0];
            this.cbo수금형태.DisplayMember = "NAME";
            this.cbo수금형태.ValueMember = "CODE";
            if (BASIC.GetMAEXC_Menu("P_SA_BILL", "SA_A00000002") == "100")
                this.cbo거래구분.DataSource = new DataView(comboData.Tables[1], "CODE IN ('001', '002', '004', '005')", "CODE", DataViewRowState.CurrentRows);
            else
                this.cbo거래구분.DataSource = new DataView(comboData.Tables[1], "CODE IN ('001', '002' )", "CODE", DataViewRowState.CurrentRows);
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";
            this.cbo거래구분.SelectedValue = "";
            this.ctx담당자.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            this.ctx담당자.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
            this.cbo전표처리.DataSource = comboData.Tables[2];
            this.cbo전표처리.ValueMember = "CODE";
            this.cbo전표처리.DisplayMember = "NAME";
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("CODE", typeof(string));
            dataTable2.Columns.Add("NAME", typeof(string));
            DataRow row1 = dataTable2.NewRow();
            row1["CODE"] = "";
            row1["NAME"] = "";
            dataTable2.Rows.Add(row1);
            DataRow row2 = dataTable2.NewRow();
            row2["CODE"] = "0";
            row2["NAME"] = this.DD("미완료");
            dataTable2.Rows.Add(row2);
            DataRow row3 = dataTable2.NewRow();
            row3["CODE"] = "1";
            row3["NAME"] = this.DD("완료");
            dataTable2.Rows.Add(row3);
            this.cbo반제처리.DataSource = dataTable2;
            this.cbo반제처리.ValueMember = "CODE";
            this.cbo반제처리.DisplayMember = "NAME";
            this.cbo수금구분.DataSource = comboData.Tables[3];
            this.cbo수금구분.ValueMember = "CODE";
            this.cbo수금구분.DisplayMember = "NAME";
            this.btn통합전표처리.Visible = this.btn통합전표취소.Visible = this.Use통합전표처리;
            Debug.WriteLine("서버키 : " + Global.MainFrame.ServerKeyCommon.ToUpper());
            Debug.WriteLine("Use통합전표처리 : " + this.Use통합전표처리.ToString());
            if (MA.ServerKey(false, new string[] { "TYPHC" }))
                this.btn일괄전표처리.Visible = this.btn일괄전표취소.Visible = false;
            else if (Global.MainFrame.ServerKeyCommon.Contains("KOREAENG"))
                this.btn일괄전표처리.Visible = this.btn일괄전표취소.Visible = this.btn일괄자동반제.Visible = false;
            if (MA.ServerKey(false, new string[] { "GRST", "DZSQL" }))
            {
                this.oneGridItem5.Visible = true;
                this.oneGrid1.Height = 131;
            }
            if (string.IsNullOrEmpty(this._NO_MDOCU))
                return;
            this.Search_Jump();
        }

        private void InitEvent()
        {
            this._flex수금번호H.DoubleClick += new EventHandler(this._flex수금번호H_DoubleClick);
            this.btn반제추가.Click += new EventHandler(this.btn반제추가_Click);
            this.btn반제삭제.Click += new EventHandler(this.btn반제삭제_Click);
            this.btn일괄자동반제.Click += new EventHandler(this.btn일괄자동반제_Click);
            this.btn일괄전표취소.Click += new EventHandler(this.btn일괄전표취소_Click);
            this.btn일괄전표처리.Click += new EventHandler(this.btn일괄전표처리_Click);
            this.btn통합전표처리.Click += new EventHandler(this.btn통합전표처리_Click);
            this.btn통합전표취소.Click += new EventHandler(this.btn통합전표취소_Click);
            this._flex수금번호H.DoubleClick += new EventHandler(this._flex_DoubleClick);

            this.ctx거래처그룹.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this._flex수금번호H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk기간)
                    return;
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                if (this.cbo수금형태.SelectedValue != null)
                    empty1 = this.cbo수금형태.SelectedValue.ToString();
                if (this.cbo거래구분.SelectedValue != null)
                    empty2 = this.cbo거래구분.SelectedValue.ToString();
                object[] objArray = new object[19];
                objArray[0] = this.LoginInfo.CompanyCode;
                objArray[1] = this.dtp기간.StartDateToString;
                objArray[2] = this.dtp기간.EndDateToString;
                objArray[3] = empty1;
                objArray[4] = empty2;
                objArray[5] = this.ctx매출처.CodeValue;
                objArray[6] = this.ctx수금처.CodeValue;
                string[] strArray = new string[] { "TYPHC", "DELIF" };
                objArray[7] = !MA.ServerKey(false, strArray) ? this.ctx영업그룹.QueryWhereIn_Pipe : this.ctx영업그룹.CodeValue;
                objArray[8] = this.ctx담당자.CodeValue;
                objArray[9] = 0;
                objArray[10] = "";
                objArray[11] = "S";
                objArray[12] = this.cbo전표처리.SelectedValue;
                objArray[13] = this.cbo반제처리.SelectedValue;
                objArray[14] = this.ctx사업장.QueryWhereIn_Pipe;
                objArray[15] = this.cbo수금구분.SelectedValue;
                objArray[16] = this.ctx거래처그룹.CodeValue;
                objArray[17] = Global.SystemLanguage.MultiLanguageLpoint;
                objArray[18] = this.ctx영업조직.QueryWhereIn_Pipe;
                DataSet dataSet = this._biz.Search(objArray);
                if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
                {
                    this.btn통합전표처리.Enabled = false;
                    this.btn통합전표취소.Enabled = false;
                    this.btn일괄자동반제.Enabled = false;
                    this.btn일괄전표처리.Enabled = false;
                    this.btn일괄전표취소.Enabled = false;
                    this.btn반제추가.Enabled = false;
                    this.btn반제삭제.Enabled = false;
                    this.ShowMessage(PageResultMode.SearchNoData);
                    this._flex수금번호L.Binding = null;
                    this._flex수금번호D.Binding = null;
                    this._flexL = this._flex수금번호L;
                    this._flexD = this._flex수금번호D;
                    this._flex수금번호H.Binding = dataSet.Tables[0];
                    this._flexH = this._flex수금번호H;
                }
                else
                {
                    this.btn통합전표처리.Enabled = true;
                    this.btn통합전표취소.Enabled = true;
                    this.btn일괄자동반제.Enabled = true;
                    this.btn일괄전표처리.Enabled = true;
                    this.btn일괄전표취소.Enabled = true;
                    this.btn반제추가.Enabled = true;
                    this.btn반제삭제.Enabled = true;
                    this._flexH = this._flex수금번호H;
                    this._flex수금번호L.Binding = dataSet.Tables[1];
                    this._flex수금번호D.Binding = dataSet.Tables[2];
                    this._flexL = this._flex수금번호L;
                    this._flexD = this._flex수금번호D;
                    this._flex수금번호H.Binding = dataSet.Tables[0];
                    this._flexH = this._flex수금번호H;
                    this._flexH.DataView.Sort = "DT_RCP, NO_RCP";
                    this.ToolBarDeleteButtonEnabled = true;
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
                if (!this.MsgAndSave(PageActionMode.Save))
                    return;
                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다, new string[0]);
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
                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y' ", "", DataViewRowState.CurrentRows);
                foreach (DataRow dataRow in dataRowArray1)
                {
                    if (!this._biz.선수금체크(D.GetString(dataRow["NO_RCP"])))
                    {
                        this.ShowMessage("선수금중에선수금정리된건이있어수정이불가합니다,");
                        return;
                    }
                }
                if (dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y' AND ( TP_AIS = 'Y' )", "", DataViewRowState.CurrentRows);
                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                    {
                        this._flexH.Row = this._flexH.FindRow(dataRowArray2[0]["NO_RCP"].ToString(), 1, this._flexH.Cols["NO_RCP"].Index, true, true, true);
                        this.ShowMessage("선택된 내역중 미결전표처리건이 있습니다..");
                    }
                    else
                    {
                        if (this.ShowMessage("선택하신자료를 삭제하시겠습니까?", "QY2") == DialogResult.No)
                            return;
                        foreach (DataRow dataRow in dataRowArray1)
                        {
                            string companyCode = this.LoginInfo.CompanyCode;
                            string 수금번호 = dataRow["NO_RCP"].ToString().Trim();
                            if (!this._biz.Delete(수금번호))
                            {
                                this.ShowMessage(this.DD("수금번호") + "(@) 의 삭제처리가 정상적으로 처리되지 않았습니다.", 수금번호, "IK1");
                                this._flexH.Row = this._flexH.FindRow(수금번호, 1, (this._flexH.Cols["NO_RCP"]).Index, true, true, true);
                                return;
                            }
                        }
                        this.ShowMessage("삭제가 정상적으로 처리되었습니다");
                        base.OnToolBarSearchButtonClicked(sender, e);
                    }
                }
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
                if (!this._flex수금번호H.HasNormalRow || !this.BeforePrint())
                    return;
                DataRow[] dataRowArray = this._flex수금번호H.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    DataTable dataTable = this._flex수금번호H.DataTable.Clone();
                    foreach (DataRow row in dataRowArray)
                        dataTable.ImportRow(row);
                    if (MA.ServerKey(false, new string[] { "THV" }))
                    {
                        dataTable.DefaultView.Sort = "DT_RCP ASC, NO_DOCU ASC";
                        dataTable = dataTable.DefaultView.ToTable();
                    }
                    ReportHelper reportHelper = new ReportHelper("R_SA_BILL_MNG_0", "수금관리");
                    reportHelper.SetData("DT_RPC_FROM", D.GetString(this.dtp기간.StartDateToString));
                    reportHelper.SetData("DT_RPC_TO", D.GetString(this.dtp기간.EndDateToString));
                    reportHelper.SetDataTable(dataTable, 1);
                    reportHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            DataTable changes1 = this._flex수금번호H.GetChanges();
            DataTable changes2 = this._flex수금번호D.GetChanges();
            if (changes1 == null && changes2 == null)
                return true;
            if (!this._biz.Save(changes1, changes2))
                return false;
            this._flex수금번호H.AcceptChanges();
            this._flex수금번호D.AcceptChanges();
            return true;
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID != HelpID.P_SA_TPSO_SUB)
                    return;
                e.HelpParam.P61_CODE1 = "N";
                e.HelpParam.P62_CODE2 = "Y";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                string empty1 = string.Empty;
                string str1 = string.Empty;
                string empty2 = string.Empty;
                string empty3 = string.Empty;
                if (this.cbo수금형태.SelectedValue != null)
                    empty2 = this.cbo수금형태.SelectedValue.ToString();
                if (this.cbo거래구분.SelectedValue != null)
                    empty3 = this.cbo거래구분.SelectedValue.ToString();
                str1 = "NO_RCP = '" + this._flex수금번호H[e.NewRange.r1, "NO_RCP"].ToString() + "'";
                string str2 = "NO_RCP = '" + this._flex수금번호H[e.NewRange.r1, "NO_RCP"].ToString() + "'";
                this._flexL.RowFilter = str2;
                this._flexD.RowFilter = str2;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            if (!this._flex수금번호H.HasNormalRow || !(this._flex수금번호H.Cols[this._flex수금번호H.Col].Name == "NO_DOCU"))
                return;
            string empty = string.Empty;
            string str = !MA.ServerKey(false, new string[] { "TYPHC" }) ? D.GetString(this._flex수금번호H["CD_PC"]) : D.GetString(this._flex수금번호H.DataTable.Compute("MAX(AIS_DC_RMK)", "NO_DOCU = '" + D.GetString(this._flex수금번호H["NO_DOCU"]) + "'"));
            if (D.GetString(this._flex수금번호H["NO_DOCU"]) != "")
            {
                object[] objArray = new object[] { D.GetString(this._flex수금번호H["NO_DOCU"]),
                                                   "1",
                                                   str,
                                                   Global.MainFrame.LoginInfo.CompanyCode };
                this.CallOtherPageMethod("P_FI_DOCU", MA.PageName("P_FI_DOCU") + "(" + this.PageName + ")", "P_FI_DOCU", this.Grant, objArray);
            }
        }

        private void _flex수금번호H_DoubleClick(object sender, EventArgs e)
        {
            if (!this._flex수금번호H.HasNormalRow)
                return;
            try
            {
                if (this._flex수금번호H.Cols[this._flex수금번호H.Col].Name == "NO_RCP" && this._flex수금번호H["NO_RCP"].ToString() != "")
                {
                    if (MA.ServerKey(false, new string[] { "FDAMK" }))
                    {
                        if (this.IsExistPage("P_SA_Z_FDAMK_BILL", false))
                            this.UnLoadPage("P_SA_Z_FDAMK_BILL", false);
                        object[] objArray = new object[] { new string[] { this._flex수금번호H["NO_RCP"].ToString() } };
                        this.LoadPageFrom("P_SA_Z_FDAMK_BILL", MA.PageName("P_SA_Z_FDAMK_BILL"), this.Grant, objArray);
                    }
                    else
                    {
                        if (this.IsExistPage("P_SA_BILL", false))
                            this.UnLoadPage("P_SA_BILL", false);
                        object[] objArray = new object[] { new string[] { this._flex수금번호H["NO_RCP"].ToString() } };
                        this.LoadPageFrom("P_SA_BILL", MA.PageName("P_SA_BILL"), this.Grant, objArray);
                    }
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
                if (e.HelpID != HelpID.P_MA_CODE_SUB)
                    return;
                e.HelpParam.P41_CD_FIELD1 = "MA_B000065";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn일괄전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.DataTable.Select("S = 'Y' ", "", DataViewRowState.CurrentRows).Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else if (this.IsChanged())
                {
                    this.ShowMessage("변경된 내용이 있습니다. 저장 후 버튼을 클릭하세요.");
                }
                else if (this._수금전표처리조건전용코드 == "100" && this._flexH.DataTable.Select("S = 'Y' AND ( AM_RCP - AM_RCP_A > AM_RCP_TX )", "", DataViewRowState.CurrentRows).Length != 0)
                {
                    this.ShowMessage("(반제필수) 선택된 내역중 적용할 반제액이 총수금액과 일치하지 않는건이 있습니다.");
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y' AND ( TP_AIS = 'N' )", "", DataViewRowState.CurrentRows);
                    if (dataRowArray1.Length == 0)
                    {
                        this.ShowMessage("선택된 내역중 미결전표처리 안된건이 없습니다..");
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S ='Y' AND TP_AIS = 'N' AND  AM_RCP_EX = 0 AND AM_RCP_A_EX = 0 ");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0 && this.ShowMessage("수금액/선수금액 0인 항목이 있습니다. 당 항목은 전표처리표시를 하되 회계전표를 만들지않습니다. 계속하시겠습니까?", "IK2") == DialogResult.Cancel)
                            return;
                        foreach (DataRow dataRow in dataRowArray1)
                        {
                            string companyCode = this.LoginInfo.CompanyCode;
                            string NoRcp = dataRow["NO_RCP"].ToString().Trim();
                            if (!this._biz.미결전표처리(NoRcp))
                            {
                                this.ShowMessage(this.DD("수금번호") + "(@) 의 전표처리가 정상적으로 처리되지 않았습니다.", NoRcp, "IK1");
                                return;
                            }
                            dataRow["TP_AIS"] = "Y";
                        }
                        this.ShowMessage("전표가 처리되었습니다");
                        base.OnToolBarSearchButtonClicked(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn일괄전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y' ", "", DataViewRowState.CurrentRows);
                if (dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    if (MA.ServerKey(false, new string[1] { "DELIF" }))
                    {
                        DataRow[] dataRowArray2 = this._flex수금번호H.DataTable.Select(" S ='Y' AND NO_DOCU_GRP IS NOT NULL ");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            this.ShowMessage("선택건중 통합전표처리건이 포함되어 있습니다 (일반전표건만 선택바랍니다)");
                            return;
                        }
                    }
                    foreach (DataRow dataRow in dataRowArray1)
                    {
                        string companyCode = this.LoginInfo.CompanyCode;
                        string NoRcp = dataRow["NO_RCP"].ToString().Trim();
                        if (!this._biz.미결전표취소(NoRcp))
                        {
                            this.ShowMessage(this.DD("수금번호") + "(@) 의 전표처리가 정상적으로취소되지 않았습니다.", NoRcp, "IK1");
                            this._flexH.Row = this._flexH.FindRow(NoRcp, 1, this._flexH.Cols["NO_RCP"].Index, true, true, true);
                            return;
                        }
                        dataRow["TP_AIS"] = "N";
                    }
                    this.ShowMessage("전표가 취소되었습니다");
                    base.OnToolBarSearchButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn일괄자동반제_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray1 = !MA.ServerKey(false, new string[] { "SPK" }) ? this._flexH.DataTable.Select("S = 'Y' ", "", DataViewRowState.CurrentRows) : this._flexH.DataTable.Select("S = 'Y' ", "NO_RCP", DataViewRowState.CurrentRows);
                if (dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y' AND ( CD_EXCH <> '000' )", "", DataViewRowState.CurrentRows);
                    if (dataRowArray2.Length > 0)
                    {
                        this._flexH.Row = this._flexH.FindRow(dataRowArray2[0]["NO_RCP"].ToString(), 1, (this._flexH.Cols["NO_RCP"]).Index, true, true, true);
                        this.ShowMessage("선택된 내역중 환종이 원화가 아닌것은 자동반제대상이 될수없습니다.");
                    }
                    else
                    {
                        DataRow[] dataRowArray3 = this._flexH.DataTable.Select("S = 'Y' AND ( AM_RCP_TX <> 0 )", "", DataViewRowState.CurrentRows);
                        if (dataRowArray3.Length > 0)
                        {
                            this._flexH.Row = this._flexH.FindRow(dataRowArray3[0]["NO_RCP"].ToString(), 1, (this._flexH.Cols["NO_RCP"]).Index, true, true, true);
                            this.ShowMessage("선택된 내역중 반제내역이 이미 있는건은 자동반제대상이 아닙니다. ");
                        }
                        else
                        {
                            foreach (DataRow dataRow1 in dataRowArray1)
                            {
                                string companyCode = this.LoginInfo.CompanyCode;
                                string str1 = dataRow1["NO_RCP"].ToString().Trim();
                                dataRow1["BILL_PARTNER"].ToString().Trim();
                                this._flexD.RowFilter = "NO_RCP = '" + str1 + "'";
                                string str2 = string.Empty;
                                for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                                {
                                    if (this._flexD.RowState(index) == DataRowState.Added)
                                        str2 = str2 + this._flexD[index, "NO_TX"].ToString() + "|";
                                }
                                DataTable dataTable = !(BASIC.GetMAEXC_Menu("P_SA_BILL", "SA_A00000004") == "100") ? this._biz.자동반제(dataRow1["CD_PARTNER"].ToString().Trim(), dataRow1["BILL_PARTNER"].ToString().Trim(), dataRow1["TP_BUSI"].ToString().Trim(), "", this._flexH.CDecimal(dataRow1["AM_RCP"]), this._flexH.CDecimal(dataRow1["AM_RCP_TX"]), D.GetString(dataRow1["FG_MAP"])) : this._biz.자동반제2(dataRow1["CD_PARTNER"].ToString().Trim(), dataRow1["BILL_PARTNER"].ToString().Trim(), dataRow1["TP_BUSI"].ToString().Trim(), "", this._flexH.CDecimal(dataRow1["AM_RCP"]), this._flexH.CDecimal(dataRow1["AM_RCP_TX"]), D.GetString(dataRow1["FG_MAP"]));
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    DataRow dataRow2 = this._flexD.DataTable.NewRow();
                                    dataRow2["NO_RCP"] = str1;
                                    dataRow2["NO_TX"] = row["NO_TX"];
                                    dataRow2["DT_IV"] = row["DT_IV"];
                                    dataRow2["TP_SO"] = "001";
                                    dataRow2["AM_IV"] = row["AM_IV"];
                                    dataRow2["AM_RCP_TX"] = row["AM_RCP_TX"];
                                    dataRow2["AM_IV_EX"] = row["AM_IV"];
                                    dataRow2["RT_EXCH_IV"] = 1;
                                    dataRow2["AM_RCP_TX_EX"] = row["AM_RCP_TX"];
                                    dataRow2["AM_PL"] = 0;
                                    if (MA.ServerKey(true, new string[] { "WGSK" }) && dataTable.Columns.Contains("DT_RCP_RSV"))
                                        dataRow2["DT_RCP_RSV"] = row["DT_RCP_RSV"];
                                    this._flexD.DataTable.Rows.Add(dataRow2.ItemArray);
                                    dataRow1["AM_RCP_EX_TX"] = Unit.외화금액(DataDictionaryTypes.SA, this._flexD.CDecimal(dataRow1["AM_RCP_EX_TX"]) + this._flexD.CDecimal(row["AM_RCP_TX"]));
                                    dataRow1["AM_RCP_TX"] = Unit.외화단가(DataDictionaryTypes.SA, this._flexD.CDecimal(dataRow1["AM_RCP_TX"]) + this._flexD.CDecimal(row["AM_RCP_TX"]));
                                }
                              this._flexD.Redraw = true;
                                this._flexD.IsDataChanged = true;
                                this._flexD.SumRefresh();
                                this._flexD.Row = this._flexD.Rows.Count - 1;
                                this._flexH.Row = this._flexH.FindRow(str1, 1, (this._flexH.Cols["NO_RCP"]).Index, true, true, true);
                                if (!base.SaveData())
                                {
                                    this.ShowMessage("자동반제를 중단합니다.");
                                    return;
                                }
                            }
                            this.ShowMessage("자동반제가 완료되었습니다.");
                            this._flexD.Redraw = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 수금합계(string 수금번호)
        {
            decimal num1 = 0M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            foreach (DataRow dataRow in this._flexD.DataTable.Select("NO_RCP = '" + 수금번호 + "'"))
            {
                num2 += Convert.ToDecimal(dataRow["AM_RCP_TX"]);
                num1 += Convert.ToDecimal(dataRow["AM_RCP_TX_EX"]);
                if (this.Get_YNAmVat)
                {
                    num3 += D.GetDecimal(dataRow["AM_RCP_K"]);
                    num4 += D.GetDecimal(dataRow["AM_RCP_VAT"]);
                }
            }
            DataRow[] dataRowArray = this._flexH.DataTable.Select(" NO_RCP = '" + 수금번호 + "'");
            dataRowArray[0]["AM_RCP_EX_TX"] = Unit.외화단가(DataDictionaryTypes.SA, num1);
            dataRowArray[0]["AM_RCP_TX"] = Unit.외화단가(DataDictionaryTypes.SA, num2);
            if (!this.Get_YNAmVat)
                return;
            dataRowArray[0]["AM_RCP_VAT_TX"] = num4;
            dataRowArray[0]["AM_RCP_K_TX"] = num3;
        }

        private void btn반제추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.Row < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else if (this._flexH[this._flexH.Row, "TP_AIS"].ToString() == "Y" && this._flexH[this._flexH.Row, "TP_BUSI"].ToString() == "002")
                {
                    this.ShowMessage("구매승인서건은 전표처리후 반제를 수정할수없습니다.");
                }
                else if (this._flexH.CDecimal(this._flexH[this._flexH.Row, "AM_RCP"]) == this._flexH.CDecimal(this._flexH[this._flexH.Row, "AM_RCP_TX"]))
                {
                    this.ShowMessage("총수금액과 반제액이 동일합니다.");
                }
                else
                {
                    string companyCode = this.LoginInfo.CompanyCode;
                    string 수금번호 = this._flexH[this._flexH.Row, "NO_RCP"].ToString();
                    this._flexH[this._flexH.Row, "BILL_PARTNER"].ToString();
                    string str1 = this._flexH[this._flexH.Row, "TP_BUSI"].ToString();
                    string str2 = this._flexH[this._flexH.Row, "CD_EXCH"].ToString();
                    string MultiNoTax = string.Empty;
                    for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                    {
                        if (this._flexD.RowState(index) == DataRowState.Added)
                            MultiNoTax = MultiNoTax + this._flexD[index, "NO_TX"].ToString() + "|";
                    }
                    P_SA_BILL_SUB pSaBillSub = !Global.MainFrame.ServerKeyCommon.ToUpper().Contains("KOSMG") ? new P_SA_BILL_SUB(this.MainFrameInterface, MultiNoTax) : new P_SA_BILL_SUB(this.MainFrameInterface, MultiNoTax, D.GetString(this._flexH[this._flexH.Row, "CD_BIZAREA"]));
                    pSaBillSub._TPBusi = str1 == null ? string.Empty : str1;
                    pSaBillSub._NmTpBusi = this._flexH[this._flexH.Row, "NM_TP_BUSI"].ToString();
                    pSaBillSub._CdPartner = this._flexH[this._flexH.Row, "CD_PARTNER"].ToString();
                    pSaBillSub._NmPartner = this._flexH[this._flexH.Row, "LN_PARTNER"].ToString();
                    pSaBillSub._BillPartner = this._flexH[this._flexH.Row, "BILL_PARTNER"].ToString();
                    pSaBillSub._NmBillPartner = this._flexH[this._flexH.Row, "LN_BILL_PARTNER"].ToString();
                    pSaBillSub._NoProject = string.Empty;
                    pSaBillSub._NmProject = string.Empty;
                    pSaBillSub._AmRcp = this._flexH.CDecimal(this._flexH[this._flexH.Row, "AM_RCP"]);
                    pSaBillSub._AmRcpTx = this._flexH.CDecimal(this._flexH[this._flexH.Row, "AM_RCP_TX"]);
                    pSaBillSub._AmRcp_Ex = this._flexH.CDecimal(this._flexH[this._flexH.Row, "AM_RCP_EX"]);
                    pSaBillSub._AmRcpTx_Ex = this._flexH.CDecimal(this._flexH[this._flexH.Row, "AM_RCP_EX_TX"]);
                    if (this.Get_YNAmVat)
                    {
                        pSaBillSub._AmRcpK = D.GetDecimal(this._flexH[this._flexH.Row, "AM_RCP_K"]);
                        pSaBillSub._AmRcpK_Tx = D.GetDecimal(this._flexH[this._flexH.Row, "AM_RCP_K_TX"]);
                        pSaBillSub._AmRcpVat = D.GetDecimal(this._flexH[this._flexH.Row, "AM_RCP_VAT"]);
                        pSaBillSub._AmRcpVat_Tx = D.GetDecimal(this._flexH[this._flexH.Row, "AM_RCP_VAT_TX"]);
                    }
                    pSaBillSub._CdExch = str2 == null ? string.Empty : str2;
                    pSaBillSub._NmExch = this._flexH[this._flexH.Row, "NM_EXCH"].ToString();
                    pSaBillSub._RtExch = this._flexH.CDecimal(this._flexH[this._flexH.Row, "RT_EXCH"]);
                    pSaBillSub.Set매출구분 = D.GetString(this._flexH[this._flexH.Row, "FG_MAP"]);
                    pSaBillSub._GiPartner = this._flexH[this._flexH.Row, "GI_PARTNER"].ToString();
                    pSaBillSub._NmGiPartner = this._flexH[this._flexH.Row, "NM_GI_PARTNER"].ToString();
                    pSaBillSub._CdBizarea = this._flexH[this._flexH.Row, "CD_BIZAREA"].ToString();
                    if (pSaBillSub.ShowDialog() != DialogResult.OK)
                        return;
                    DataTable getDataTable = pSaBillSub.GetDataTable;
                    this._flexD.Redraw = false;
                    foreach (DataRow row1 in getDataTable.Rows)
                    {
                        DataRow row2 = this._flexD.DataTable.NewRow();
                        row2["NO_RCP"] = 수금번호;
                        row2["NO_TX"] = row1["NO_IV"];
                        row2["DT_IV"] = row1["DT_PROCESS"];
                        row2["TP_SO"] = "001";
                        row2["AM_IV_EX"] = row1["AM_RCP_JAN_EX"];
                        row2["RT_EXCH_IV"] = row1["RT_EXCH"];
                        row2["AM_IV"] = row1["AM_RCP_JAN"];
                        row2["AM_RCP_TX_EX"] = row1["AM_RCP_EX"];
                        row2["AM_RCP_TX"] = row1["AM_RCP"];
                        row2["AM_PL"] = row1["AM_PL"];
                        row2["AM_PL_LOSS"] = row1["AM_PL_LOSS"];
                        row2["AM_PL_PROFIT"] = row1["AM_PL_PROFIT"];
                        if (this.Get_YNAmVat)
                        {
                            row2["AM_IV_VAT"] = row1["AM_RCP_VAT_JAN"];
                            row2["AM_RCP_VAT"] = row1["AM_RCP_VAT"];
                            row2["AM_IV_K"] = row1["AM_RCP_K_JAN"];
                            row2["AM_RCP_K"] = row1["AM_RCP_K"];
                        }
                        if (MA.ServerKey(true, new string[1] { "WGSK" }))
                            row2["DT_RCP_RSV"] = row1["DT_RCP_RSV"];
                        this._flexD.DataTable.Rows.Add(row2);
                    }
                  this._flexD.Redraw = true;
                    this._flexD.IsDataChanged = true;
                    this._flexD.SumRefresh();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    this.ToolBarSaveButtonEnabled = true;
                    this.수금합계(수금번호);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn반제삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                if (this._flexH[this._flexH.Row, "TP_AIS"].ToString() == "Y" && this._flexH[this._flexH.Row, "TP_BUSI"].ToString() == "002")
                {
                    this.ShowMessage("구매승인서건은 전표처리후 반제를 수정할수없습니다.");
                }
                else
                {
                    string 수금번호 = this._flexH[this._flexH.Row, "NO_RCP"].ToString();
                    this._flexD.Rows.Remove(this._flexD.Row);
                    this.수금합계(수금번호);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn통합전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y' ", "", DataViewRowState.CurrentRows);
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y' AND TP_AIS = 'N'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray2 == null || dataRowArray2.Length == 0)
                    {
                        this.ShowMessage("선택된 내역 모두 미결전표처리 되어 있습니다.");
                    }
                    else if (!MA.ServerKey(false, new string[] { "DELIF" }) && this.dtp기간.StartDateToString != this.dtp기간.EndDateToString)
                    {
                        this.ShowMessage("통합전표처리는 기간From, To가 동일하여야 합니다.");
                        this.dtp기간.Focus();
                    }
                    else
                    {
                        string str1 = Common.MultiString(this._flexH.DataTable, "S = 'Y' AND TP_AIS = 'N'", "NO_RCP", "|");
                        string 통합멀티번호 = string.Empty;
                        if (MA.ServerKey(false, new string[] { "TYPHC" }))
                        {
                            통합멀티번호 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "SA", "C3", D.GetString(this.dtp기간.StartDateToString).Substring(0, 6));
                            DataTable dataTable = this._biz.Check_동양파일(str1);
                            if (dataTable.Rows.Count > 1)
                            {
                                this.ShowMessage("관리번호가 다른 건이 존재합니다.");
                                return;
                            }
                            if (this._biz.Check_동양파일_DOCU(D.GetString(dataTable.Rows[0]["NO_MGMT"])).Rows.Count > 0)
                            {
                                this.ShowMessage("이미 전표처리된 관리번호가 존재합니다.");
                                return;
                            }
                            string str2 = string.Empty;
                            string str3 = string.Empty;
                            foreach (DataRow dataRow in dataRowArray2)
                            {
                                if (!(D.GetString(dataRow["AIS_DC_RMK"]) == string.Empty))
                                {
                                    if (str2 == string.Empty)
                                    {
                                        str2 = dataRow["AIS_DC_RMK"].ToString().Trim();
                                        str3 = dataRow["CD_PARTNER"].ToString().Trim();
                                    }
                                    else
                                    {
                                        if (str2 != D.GetString(dataRow["AIS_DC_RMK"]))
                                        {
                                            this.ShowMessage("수금형태가 다른 건이 존재합니다.");
                                            return;
                                        }
                                        if (str3 != D.GetString(dataRow["CD_PARTNER"]))
                                        {
                                            this.ShowMessage("동일한 수금관리번호에 다른거래처가 존재합니다.");
                                            return;
                                        }
                                    }
                                }
                            }
                            if (str2 == string.Empty)
                            {
                                this.ShowMessage("수금형태를 확인하여 주시기 바랍니다.");
                                return;
                            }
                        }
                        if (MA.ServerKey(false, new string[] { "DELIF" }))
                        {
                            통합멀티번호 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "SA", "C3", D.GetString(this.MainFrameInterface.GetStringToday).Substring(0, 6));
                            DataTable gridGroupBy = ComFunc.getGridGroupBy(dataRowArray2, new string[] { "BILL_PARTNER", "CD_TP" }, true);
                            if (gridGroupBy != null && gridGroupBy.Rows.Count > 1)
                            {
                                this.ShowMessage("수금처와 수금형태가 동일한 건들만 전표처리 할수 있습니다");
                                return;
                            }
                        }
                        this._biz.통합전표처리(str1, 통합멀티번호);
                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn통합전표처리.Text });
                        base.OnToolBarSearchButtonClicked(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn통합전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y' ", "", DataViewRowState.CurrentRows);
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y' AND TP_AIS = 'Y'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray2 == null || dataRowArray2.Length == 0)
                    {
                        this.ShowMessage("선택된 내역중 미결전표처리 된건이 없습니다..");
                    }
                    else if (!MA.ServerKey(false, new string[] { "DELIF" }) && this.dtp기간.StartDateToString != this.dtp기간.EndDateToString)
                    {
                        this.ShowMessage("통합전표처리는 기간From, To가 동일하여야 합니다.");
                        this.dtp기간.Focus();
                    }
                    else
                    {
                        if (this.ShowMessage("선택된 전표번호에 대한 모든 내용이 취소됩니다." + Environment.NewLine + "계속 작업하시겠습니까?", "QY2") != DialogResult.Yes)
                            return;
                        string 멀티수금번호 = Common.MultiString(this._flexH.DataTable, "S = 'Y' AND TP_AIS = 'Y'", "NO_RCP", "|");
                        string 통합멀티번호 = string.Empty;
                        if (MA.ServerKey(false, new string[] { "TYPHC" }) || Global.MainFrame.ServerKeyCommon.Contains("DELIF"))
                            통합멀티번호 = Common.MultiString(this._flexH.DataTable, "S = 'Y' AND TP_AIS = 'Y'", "NO_DOCU_GRP", "|");
                        this._biz.통합전표취소(멀티수금번호, 통합멀티번호);
                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn통합전표취소.Text });
                        base.OnToolBarSearchButtonClicked(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Use통합전표처리 => this.ServerKey == "BON" || this.ServerKey == "ETANG" || this.ServerKey == "DZSQL" || this.ServerKey == "DOMINO" || this.ServerKey == "TYPHC" || this.ServerKey == "DELIF" || this.ServerKey == "FDWL";

        private bool Chk기간 => Checker.IsValid(this.dtp기간, true, (this.lbl기간).Text);

        public bool Get_YNAmVat => BASIC.GetMAEXC("수금등록-부가세사용여부(외화수금)") == "100" && D.GetString(this._flexH[this._flexH.Row, "CD_EXCH"]) != "000" && D.GetString(this._flexH[this._flexH.Row, "FG_MAP"]) != "E";

        private void Search_Jump()
        {
            if (string.IsNullOrEmpty(this._NO_MDOCU))
                return;
            DataSet dataSet = this._biz.Search_Jump(new object[] { this.LoginInfo.CompanyCode, 
                                                                   this._NO_MDOCU, 
                                                                   Global.SystemLanguage.MultiLanguageLpoint });
            if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
            {
                this.btn통합전표처리.Enabled = false;
                this.btn통합전표취소.Enabled = false;
                this.btn일괄자동반제.Enabled = false;
                this.btn일괄전표처리.Enabled = false;
                this.btn일괄전표취소.Enabled = false;
                this.btn반제추가.Enabled = false;
                this.btn반제삭제.Enabled = false;
                this.ShowMessage(PageResultMode.SearchNoData);
                this._flex수금번호L.Binding = null;
                this._flex수금번호D.Binding = null;
                this._flexL = this._flex수금번호L;
                this._flexD = this._flex수금번호D;
                this._flex수금번호H.Binding = dataSet.Tables[0];
                this._flexH = this._flex수금번호H;
            }
            else
            {
                this.btn통합전표처리.Enabled = true;
                this.btn통합전표취소.Enabled = true;
                this.btn일괄자동반제.Enabled = true;
                this.btn일괄전표처리.Enabled = true;
                this.btn일괄전표취소.Enabled = true;
                this.btn반제추가.Enabled = true;
                this.btn반제삭제.Enabled = true;
                this._flexH = this._flex수금번호H;
                this._flex수금번호L.Binding = dataSet.Tables[1];
                this._flex수금번호D.Binding = dataSet.Tables[2];
                this._flexL = this._flex수금번호L;
                this._flexD = this._flex수금번호D;
                this._flex수금번호H.Binding = dataSet.Tables[0];
                this._flexH = this._flex수금번호H;
                this._flexH.Cols["NO_RCP"].Sort = (SortFlags)1;
                this.ToolBarDeleteButtonEnabled = true;
            }
        }
    }
}
