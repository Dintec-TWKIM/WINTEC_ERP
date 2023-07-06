using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Forms.Help;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using Duzon.Windows.Print;

namespace cz
{
    public partial class P_CZ_SA_PTRPRICE : PageBase
    {
        private OpenFileDialog m_FileDlg = new OpenFileDialog();
        private DataTable _dt_EXCEL = null;
        private DataTable dt복사 = null;
        private P_CZ_SA_PTRPRICE_BIZ _biz;

        public P_CZ_SA_PTRPRICE()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_PTRPRICE_BIZ();
            
            this.InitGrid();
            this.initEvent();
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("CD_PARTNER", "거래처코드", 80, false);
            this._flexH.SetCol("LN_PARTNER", "거래처명", 150, false);
            this._flexH.SetCol("CD_PARTNER_GRP", "거래처그룹", 80, false);
            this._flexH.SetCol("NM_PARTNER_GRP", "거래처그룹명", 150, false);
            this._flexH.SetCol("USE_YN", "사용유무", 70, false);
            this._flexH.SetCol("CD_CON", "휴폐업구분", 70, false);
            this._flexH.SetCol("CLS_PARTNER", "거래처분류", 80, false);

            this._flexH.SetDummyColumn("S");
            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, true);

            this._flexL.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100, 20, true);
            this._flexL.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexL.SetCol("STND_ITEM", "규격", 120, false);
            this._flexL.SetCol("UNIT_PO", "발주단위", 40, false);
            this._flexL.SetCol("UNIT_IM", "재고단위", 40, false);
            this._flexL.SetCol("UNIT_SO", "수주단위", 100, false);
            this._flexL.SetCol("TP_PROC", "조달구분", 100, false);
            this._flexL.SetCol("CLS_ITEM", "계정구분", 100, false);
            this._flexL.SetCol("CD_EXCH", "환종", 80, true);
            this._flexL.SetCol("FG_UM", "단가유형", 80, true);
            this._flexL.SetCol("UM_ITEM", "단가", 80, 17, true, typeof(Decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("SDT_UM", "기간시작일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("EDT_UM", "기간종료일", 80, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_INSERT", "입력일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NM_USER", "입력자", 80, false);
            this._flexL.SetCol("PITEM_NUM_USERDEF1", "공장품목_사용자정의숫자1", 100, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("PITEM_NUM_USERDEF2", "공장품목_사용자정의숫자2", 100, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NUM_USERDEF1", "사용자정의숫자1", 100, true, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NUM_USERDEF2", "사용자정의숫자2", 100, true, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("DC_RMK", "비고", 120, true);

            this._flexL.SetDummyColumn("S");
            this._flexL.EnterKeyAddRow = false;
            this._flexL.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_PO", "TP_PROC", "CLS_ITEM", "DT_INSERT", "NM_USER", "UNIT_IM", "UNIT_SO", "PITEM_NUM_USERDEF1", "PITEM_NUM_USERDEF2");
            this._flexL.Cols["UNIT_PO"].Visible = false;
            this._flexL.VerifyAutoDelete = new string[] { "CD_ITEM" };
            this._flexL.VerifyPrimaryKey = new string[] { "CD_ITEM",
                                                          "CD_PARTNER",
                                                          "FG_UM",
                                                          "CD_EXCH",
                                                          "SDT_UM",
                                                          "TP_UMMODULE",
                                                          "CD_PLANT" };
            this._flexL.VerifyNotNull = new string[] { "CD_PARTNER",
                                                       "CD_ITEM",
                                                       "CD_EXCH",
                                                       "FG_UM",
                                                       "SDT_UM",
                                                       "EDT_UM" };
            this._flexL.VerifyCompare(this._flexL.Cols["SDT_UM"], this._flexL.Cols["EDT_UM"], OperatorEnum.LessOrEqual);
            this._flexL.VerifyCompare(this._flexL.Cols["SDT_UM"], this._flexL.Cols["EDT_UM"], OperatorEnum.LessOrEqual);
            this._flexL.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                              "NM_ITEM",
                                                                                                              "STND_ITEM",
                                                                                                              "UNIT_PO",
                                                                                                              "UNIT_IM",
                                                                                                              "UNIT_SO" }, new string[] { "CD_ITEM",
                                                                                                                                          "NM_ITEM",
                                                                                                                                          "STND_ITEM",
                                                                                                                                          "UNIT_PO",
                                                                                                                                          "UNIT_IM",
                                                                                                                                          "UNIT_SO" });
            this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void initEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.btn적용.Click += new EventHandler(this.적용);
            this.btn엑셀업로드.Click += new EventHandler(this._btn엑셀_Click);
            this.btn일괄반영.Click += new EventHandler(this.btn일괄반영_Click);
            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.cbo공장_SelectionChangeCommitted);
            this.btn복사.Click += new EventHandler(this.btn복사_Click);
            this.btn붙여넣기.Click += new EventHandler(this.btn붙여넣기_Click);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexL.AddRow += new EventHandler(this.OnToolBarAddButtonClicked);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexL_BeforeCodeHelp);
            this._flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flexL_AfterCodeHelp);
            this._flexL.AfterEdit += new RowColEventHandler(this._flexL_AfterEdit);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.InitControl();
            this.사용자정의셋팅();
        }

        private void InitControl()
        {
            DataTable dt1;
            DataTable dt2;

            if (BASIC.GetMAENV("YN_MFG_AUTH") == "Y" && Global.MainFrame.ServerKeyCommon.ToUpper().Contains("LNPCOS"))
            {
                dt1 = this._biz.SearchMfgAuth();
                dt2 = this._biz.SearchMfgAuth();
                DataRow row = dt2.NewRow();
                row["CODE"] = "";
                row["NAME"] = "";
                dt2.Rows.InsertAt(row, 0);
                dt2.AcceptChanges();
            }
            else
            {
                dt1 = MA.GetCode("SA_B000021");
                dt2 = MA.GetCode("SA_B000021", true);
            }

            DataSet comboData = this.GetComboData("N;MA_PLANT", "S;MA_B000009", "S;MA_B000010", "N;MA_B000005", "S;PU_C000034", "S;MA_B000029", "S;MA_B000003");
            
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            this.cbo조달구분.DataSource = comboData.Tables[1];
            this.cbo조달구분.DisplayMember = "NAME";
            this.cbo조달구분.ValueMember = "CODE";
            
            this.cbo계정구분.DataSource = comboData.Tables[2];
            this.cbo계정구분.DisplayMember = "NAME";
            this.cbo계정구분.ValueMember = "CODE";
            
            this._flexL.SetDataMap("CD_EXCH", comboData.Tables[3], "CODE", "NAME");
            this._flexL.SetDataMap("FG_UM", dt1, "CODE", "NAME");
            this._flexL.SetDataMap("TP_PROC", comboData.Tables[1].Copy(), "CODE", "NAME");
            this._flexL.SetDataMap("CLS_ITEM", comboData.Tables[2].Copy(), "CODE", "NAME");
            
            this.cbo품목.DataSource = comboData.Tables[4];
            this.cbo품목.DisplayMember = "NAME";
            this.cbo품목.ValueMember = "CODE";
            
            this.cbo내외자구분.DataSource = comboData.Tables[5];
            this.cbo내외자구분.DisplayMember = "NAME";
            this.cbo내외자구분.ValueMember = "CODE";
            
            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo거래처그룹, MA.GetCode("MA_B000065", true));
            setControl.SetCombobox(this.cbo단가, dt2);
            
            this._flexH.SetDataMap("USE_YN", MA.GetCode("MA_B000057"), "CODE", "NAME");
            this._flexH.SetDataMap("CD_CON", MA.GetCode("MA_B000073"), "CODE", "NAME");
            this._flexL.SetDataMap("UNIT_SO", MF.GetCode(MF.코드.MASTER.단위), "CODE", "NAME");
            this._flexL.SetDataMap("UNIT_IM", MF.GetCode(MF.코드.MASTER.단위), "CODE", "NAME");
            
            this.cbo거래처분류.DataSource = comboData.Tables[6];
            this.cbo거래처분류.DisplayMember = "NAME";
            this.cbo거래처분류.ValueMember = "CODE";
            
            this.ExcelButtonEnabled();
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void 사용자정의셋팅()
        {
            DataTable code = MA.GetCode("MA_B000093");

            for (int index = 1; index <= code.Rows.Count; ++index)
            {
                string @string = D.GetString(code.Rows[index - 1]["NAME"]);

                if (index <= 2)
                    this._flexL.Cols["NUM_USERDEF" + D.GetString(index)].Caption = @string;
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify()) return false;

            DataTable changes = this._flexL.GetChanges();
            
            if (changes == null) return true;
            
            if (!this._biz.Save(changes)) return false;
            
            this._flexL.AcceptChanges();
            
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.cbo조달구분.SelectedValue.ToString(),
                                                                      this.cbo계정구분.SelectedValue.ToString(),
                                                                      this.cbo품목.SelectedValue.ToString(),
                                                                      this.txt품목.Text,
                                                                      this.cbo내외자구분.SelectedValue.ToString(),
                                                                      D.GetString(this.cbo단가.SelectedValue),
                                                                      D.GetString(this.cbo거래처그룹.SelectedValue),
                                                                      this.txt거래처.Text,
                                                                      this.bpc거래처.CodeValue,
                                                                      D.GetString(this.cbo거래처분류.SelectedValue),
                                                                      (this.rbo_yes.Checked ? "Y" : "N") });
                
                this.ExcelButtonEnabled();

                if (!this._flexH.HasNormalRow)
                    this.ShowMessage(PageResultMode.SearchNoData);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;

                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage("조회된 품목이 없습니다.");
                }
                else
                {
                    Decimal num2 = (this._flexL.GetMaxValue("NO_LINE") + 1);
                    this._flexL.Rows.Add();
                    this._flexL.Row = this._flexL.Rows.Count - 1;
                    this._flexL["CD_PARTNER"] = this._flexH.DataView[this._flexH.DataIndex()]["CD_PARTNER"].ToString();
                    this._flexL["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                    this._flexL["CD_ITEM"] = "";
                    this._flexL["NM_ITEM"] = "";
                    this._flexL["STND_ITEM"] = "";
                    this._flexL["UNIT_PO"] = "";
                    this._flexL["TP_PROC"] = "";
                    this._flexL["CD_EXCH"] = "";
                    this._flexL["FG_UM"] = "";
                    this._flexL["UM_ITEM"] = new Decimal(0);
                    this._flexL["SDT_UM"] = !(Global.MainFrame.ServerKeyCommon.ToUpper() == "DONGAH") && !(Global.MainFrame.ServerKeyCommon.ToUpper() == "SAMWON") && (!(Global.MainFrame.ServerKeyCommon.ToUpper() == "ADDTEC") && !(Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL")) && !(Global.MainFrame.ServerKeyCommon.ToUpper() == "SQL_") ? this.MainFrameInterface.GetStringToday : "";
                    this._flexL["EDT_UM"] = "99991231";
                    this._flexL["NO_LINE"] = num2.ToString();
                    this._flexL["TP_UMMODULE"] = "002";
                    this._flexL["NM_USER"] = Global.MainFrame.LoginInfo.EmployeeName;
                    this._flexL["DT_INSERT"] = Global.MainFrame.GetStringToday;
                    this._flexL.AddFinished();
                    this._flexL.Col = this._flexL.Cols.Fixed;
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
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                if (!this._flexL.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    DataTable checkedRows = this._flexL.GetCheckedRows("S");

                    if (checkedRows == null || checkedRows.Rows.Count <= 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                    }
                    else
                    {
                        this._flexL.Redraw = false;

                        for (int index = this._flexL.Rows.Count - 1; index >= this._flexL.Rows.Fixed; --index)
                        {
                            if (this._flexL.GetCellCheck(index, this._flexL.Cols["S"].Index) == CheckEnum.Checked)
                                this._flexL.Rows.Remove(index);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

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
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flexH.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    string str = string.Empty;
                    
                    foreach (DataRow dataRow in dataRowArray)
                        str = str + D.GetString(dataRow["CD_PARTNER"]) + "|";
                    
                    DataTable dt = this._biz.Print(new List<string>() { MA.Login.회사코드,
                                                                        D.GetString(this.cbo공장.SelectedValue),
                                                                        D.GetString(this.cbo조달구분.SelectedValue),
                                                                        D.GetString(this.cbo계정구분.SelectedValue),
                                                                        D.GetString(this.cbo품목.SelectedValue),
                                                                        this.txt품목.Text,
                                                                        D.GetString(this.cbo내외자구분.SelectedValue),
                                                                        D.GetString(this.cbo단가.SelectedValue),
                                                                        D.GetString(this.cbo거래처그룹.SelectedValue),
                                                                        str }.ToArray());

                    ReportHelper reportHelper = new ReportHelper("R_SA_PTRPRICE_NEW_0", "거래처별 단가관리(NEW)");
                    reportHelper.SetData("공장", this.cbo공장.Text);
                    reportHelper.SetData("조달구분", this.cbo조달구분.Text);
                    reportHelper.SetData("계정구분", this.cbo계정구분.Text);
                    reportHelper.SetData("품목조회구분", this.cbo품목.Text);
                    reportHelper.SetData("품목조회내용", this.txt품목.Text);
                    reportHelper.SetData("단가유형", this.cbo단가.Text);
                    reportHelper.SetData("증감율", this.cur증감율.Text);
                    reportHelper.SetData("내외자구분", this.cbo내외자구분.Text);
                    reportHelper.SetData("거래처코드", this.bpc거래처.CodeValue);
                    reportHelper.SetData("거래처명", this.bpc거래처.CodeName);
                    reportHelper.SetData("거래처검색", this.txt거래처.Text);
                    reportHelper.SetData("거래처그룹", this.cbo거래처그룹.Text);
                    reportHelper.SetData("거래처분류", this.cbo거래처분류.Text);
                    reportHelper.SetDataTable(dt);
                    reportHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 적용(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo단가.Text == "")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("단가유형") });
                }
                else
                {
                    if (!this._flexH.HasNormalRow || this.cur증감율.DecimalValue == 0)
                        return;

                    object obj = this.MainFrameInterface.LoadHelpWindow("P_PU_UM_SUB", new object[] { this.MainFrameInterface });
                    
                    if (((Form)obj).ShowDialog() == DialogResult.OK)
                    {
                        object[] returnValues = ((IHelpWindow)obj).ReturnValues;
                        Decimal val = 0;
                        Decimal num2 = this.cur증감율.DecimalValue / 100;
                        string str1 = this.cbo단가.SelectedValue.ToString();
                        DataTable dataTable = new DataTable();
                        DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S ='Y'");

                        if (dataRowArray1.Length > 0)
                        {
                            for (int index = 0; index < dataRowArray1.Length; ++index)
                                dataTable.ImportRow(dataRowArray1[index]);
                        }

                        for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
                        {
                            DataRow[] dataRowArray2 = this._flexL.DataTable.Select("CD_ITEM= '" + dataRowArray1[index1]["CD_ITEM"].ToString() + "'");
                            
                            if (dataRowArray2.Length > 0)
                            {
                                for (int index2 = 0; index2 < dataRowArray2.Length; ++index2)
                                {
                                    string str2 = dataRowArray2[index2]["FG_UM"].ToString();
                                    
                                    if (str1 == str2)
                                    {
                                        Decimal num3 = this._flexH.CDecimal(dataRowArray2[index2]["UM_ITEM"]);
                                        Decimal num4 = num2 * num3;
                                        Decimal d = num3 + num4;
                                        
                                        if (returnValues[0].ToString() == "1")
                                            val = Math.Floor(d);
                                        else if (returnValues[0].ToString() == "2")
                                            val = Math.Round(d, 0);
                                        else if (returnValues[0].ToString() == "3")
                                            val = Math.Floor(d++);
                                        
                                        dataRowArray2[index2]["UM_ITEM"] = Unit.외화단가(DataDictionaryTypes.SA, val);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _btn엑셀_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage("조회된 품목이 없습니다.");
                }
                else
                {
                    this.m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";
                    if (this.m_FileDlg.ShowDialog() == DialogResult.OK)
                    {
                        Application.DoEvents();
                        string fileName = this.m_FileDlg.FileName;
                        string str1 = string.Empty;
                        string str2 = string.Empty;
                        string str3 = string.Empty;
                        string 멀티품목코드 = string.Empty;
                        bool flag1 = false;
                        bool flag2 = false;
                        bool flag3 = false;
                        string str4 = string.Empty;
                        string str5 = string.Empty;
                        bool flag6 = false;
                        string str6 = string.Empty;
                        string str7 = string.Empty;
                        this._dt_EXCEL = new Excel().StartLoadExcel(fileName);
                        int num2 = this._flexL.Rows.Count - this._flexL.Rows.Fixed;
                        MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");
                        this._flexL.Redraw = false;
                        DataTable dataTable1 = new DataTable();
                        DataTable dataTable2 = new DataTable();
                        DataTable dataTable3 = new DataTable();
                        int num3 = 1;
                        string @string = D.GetString(this._flexH["CD_PLANT"]);

                        foreach (DataRow dataRow in this._dt_EXCEL.Rows)
                        {
                            if (D.GetString(dataRow["CD_PLANT"]) != @string)
                            {
                                this.ShowMessage("현재 조회된 공장과 엑셀업로드 하려는 공장이 다릅니다.");
                                return;
                            }

                            if (str1 != dataRow["CD_ITEM"].ToString())
                            {
                                str1 = dataRow["CD_ITEM"].ToString();
                                str2 = str2 + str1 + "|";
                            }

                            if (str3 != dataRow["CD_PARTNER"].ToString())
                            {
                                str3 = dataRow["CD_PARTNER"].ToString();
                                멀티품목코드 = 멀티품목코드 + str3.ToString() + "|";
                            }

                            if (str2.Length >= 7980 || this._dt_EXCEL.Rows.Count == num3)
                            {
                                DataTable table1 = this._biz.TotalLine(str2, "002");
                                dataTable1.Merge(table1, false, MissingSchemaAction.Add);
                                DataTable table2 = this._biz.ExcelSearch(str2, "ITEM", this._dt_EXCEL.Rows[0]["CD_PLANT"].ToString());
                                dataTable2.Merge(table2, false, MissingSchemaAction.Add);
                                DataTable table3 = this._biz.ExcelSearch(멀티품목코드, "PARTNER", "XXXX");
                                if (table3 != null && table3.Rows.Count > 0)
                                    dataTable3.Merge(table3, false, MissingSchemaAction.Add);
                                str2 = "";
                                멀티품목코드 = "";
                            }

                            dataTable2.AcceptChanges();
                            ++num3;
                        }

                        DataTable dataTable4 = this._biz.엑셀(this._dt_EXCEL);
                        DataTable dataTable5 = dataTable4.Clone();
                        DataTable dataTable6 = dataTable4.Clone();
                        DataTable dataTable7 = dataTable4.Clone();
                        StringBuilder stringBuilder1 = new StringBuilder();
                        StringBuilder stringBuilder2 = new StringBuilder();
                        StringBuilder stringBuilder3 = new StringBuilder();
                        StringBuilder stringBuilder4 = new StringBuilder();
                        string str8 = this.DD("품목코드") + "   " + this.DD("거래처코드") + " " + this.DD("거래처명") + "        " + this.DD("단가유형") + "\t    " + this.DD("시작일") + "    " + this.DD("종료일");
                        stringBuilder1.AppendLine(str8);
                        stringBuilder2.AppendLine(str8);
                        stringBuilder3.AppendLine(str8);
                        stringBuilder4.AppendLine(str8);
                        string str9 = "-".PadRight(80, '-');
                        stringBuilder1.AppendLine(str9);
                        stringBuilder2.AppendLine(str9);
                        stringBuilder3.AppendLine(str9);
                        stringBuilder4.AppendLine(str9);

                        foreach (DataRow dataRow in this._dt_EXCEL.Rows)
                        {
                            if (dataTable2.Select("CD_ITEM = '" + dataRow["CD_ITEM"].ToString() + "'").Length > 0)
                            {
                                DataRow row1 = dataTable5.NewRow();
                                row1["CD_ITEM"] = dataRow["CD_ITEM"].ToString();
                                row1["CD_EXCH"] = dataRow["CD_EXCH"].ToString();
                                row1["CD_PARTNER"] = dataRow["CD_PARTNER"].ToString();
                                row1["LN_PARTNER"] = "";
                                row1["CD_PLANT"] = dataRow["CD_PLANT"];
                                row1["UM_ITEM"] = dataRow["UM_ITEM"];
                                row1["FG_UM"] = dataRow["FG_UM"].ToString();
                                row1["SDT_UM"] = dataRow["SDT_UM"].ToString();
                                row1["EDT_UM"] = dataRow["EDT_UM"].ToString().Trim() == "" ? "99991231" : dataRow["EDT_UM"].ToString().Trim();
                                row1["NO_LINE"] = 0;
                                row1["TP_UMMODULE"] = "002";
                                row1["NM_USER"] = Global.MainFrame.LoginInfo.EmployeeName;
                                row1["DT_INSERT"] = Global.MainFrame.GetStringToday;
                                dataTable5.Rows.Add(row1);

                                if (!(dataRow["CD_PARTNER"].ToString() == ""))
                                {
                                    DataRow[] dataRowArray1 = dataTable5.Select("CD_PARTNER = '" + dataRow["CD_PARTNER"].ToString() + "'");
                                    
                                    if (dataRowArray1.Length > 0)
                                    {
                                        DataRow[] dataRowArray2 = dataTable3.Select("CD_PARTNER = '" + dataRowArray1[0]["CD_PARTNER"].ToString() + "'");
                                        
                                        if (dataRowArray2.Length > 0)
                                        {
                                            str7 = dataRowArray2[0]["LN_PARTNER"].ToString();
                                            flag6 = true;
                                        }
                                    }
                                    else
                                        flag6 = false;
                                    
                                    if (flag6)
                                    {
                                        DataRow row2 = dataTable6.NewRow();
                                        row2["CD_ITEM"] = dataRow["CD_ITEM"].ToString();
                                        row2["CD_EXCH"] = dataRow["CD_EXCH"].ToString();
                                        row2["CD_PARTNER"] = dataRow["CD_PARTNER"].ToString();
                                        row2["LN_PARTNER"] = str7;
                                        row2["CD_PLANT"] = dataRow["CD_PLANT"];
                                        row2["UM_ITEM"] = dataRow["UM_ITEM"].ToString();
                                        row2["FG_UM"] = dataRow["FG_UM"].ToString();
                                        row2["SDT_UM"] = dataRow["SDT_UM"].ToString();
                                        row2["EDT_UM"] = dataRow["EDT_UM"].ToString();
                                        row2["NO_LINE"] = dataRow["NO_LINE"];
                                        row2["TP_UMMODULE"] = dataRow["TP_UMMODULE"];
                                        row2["NM_USER"] = dataRow["NM_USER"];
                                        row2["DT_INSERT"] = dataRow["DT_INSERT"];
                                        dataTable6.Rows.Add(row2);
                                        flag6 = false;
                                    }
                                    else
                                    {
                                        string str10 = dataRow["CD_ITEM"].ToString().PadRight(10, ' ') + " " + dataRow["CD_PARTNER"].ToString().PadRight(10, ' ') + " " + string.Empty.PadRight(10, ' ') + " " + dataRow["FG_UM"].ToString().PadRight(8, ' ') + " " + dataRow["SDT_UM"].ToString().PadRight(10, ' ') + " " + (dataRow["EDT_UM"].ToString().Trim() == "" ? "99991231" : dataRow["EDT_UM"].ToString().PadRight(10, ' '));
                                        stringBuilder2.AppendLine(str10);
                                        flag3 = true;
                                    }
                                }
                            }
                            else
                            {
                                string str10 = dataRow["CD_ITEM"].ToString().PadRight(10, ' ') + " " + dataRow["CD_PARTNER"].ToString().PadRight(10, ' ') + " " + string.Empty.PadRight(10, ' ') + " " + dataRow["FG_UM"].ToString().PadRight(8, ' ') + " " + dataRow["SDT_UM"].ToString().PadRight(8, ' ') + " " + (dataRow["EDT_UM"].ToString().Trim() == "" ? "99991231" : dataRow["EDT_UM"].ToString().PadRight(8, ' '));
                                stringBuilder1.AppendLine(str10);
                                flag2 = true;
                            }
                        }

                        if (flag2)
                        {
                            MsgControl.CloseMsg();
                            this.ShowDetailMessage("엑셀 업로드하는 중에 [마스터품목]과 불일치 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder1.ToString());
                        }

                        if (flag3)
                        {
                            MsgControl.CloseMsg();
                            this.ShowDetailMessage("엑셀 업로드하는 중에 [마스터거래처]와 불일치 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder2.ToString());
                        }

                        MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");
                        
                        for (int index = 0; index < dataTable6.Rows.Count; ++index)
                        {
                            DataRow[] dataRowArray = dataTable6.Select("CD_PARTNER = '" + dataTable6.Rows[index]["CD_PARTNER"].ToString() + "' AND CD_EXCH = '" + dataTable6.Rows[index]["CD_EXCH"].ToString() + "' AND FG_UM = '" + dataTable6.Rows[index]["FG_UM"].ToString() + "' AND CD_ITEM = '" + dataTable6.Rows[index]["CD_ITEM"].ToString() + "' AND SDT_UM = '" + dataTable6.Rows[index]["SDT_UM"].ToString() + "'", "", DataViewRowState.CurrentRows);
                            int num4 = 0;
                            
                            foreach (DataRow dataRow in dataRowArray)
                            {
                                if (num4 > 0)
                                    dataRow.Delete();

                                ++num4;
                            }
                            
                            DataRow row = dataTable7.NewRow();
                            row["CD_ITEM"] = dataRowArray[0]["CD_ITEM"];
                            row["CD_EXCH"] = dataRowArray[0]["CD_EXCH"];
                            row["CD_PARTNER"] = dataRowArray[0]["CD_PARTNER"];
                            row["LN_PARTNER"] = dataRowArray[0]["LN_PARTNER"];
                            row["CD_PLANT"] = dataRowArray[0]["CD_PLANT"];
                            row["UM_ITEM"] = dataRowArray[0]["UM_ITEM"];
                            row["FG_UM"] = dataRowArray[0]["FG_UM"];
                            row["SDT_UM"] = dataRowArray[0]["SDT_UM"];
                            row["EDT_UM"] = dataRowArray[0]["EDT_UM"];
                            row["NO_LINE"] = dataRowArray[0]["NO_LINE"];
                            row["TP_UMMODULE"] = dataRowArray[0]["TP_UMMODULE"];
                            row["NM_USER"] = dataRowArray[0]["NM_USER"];
                            row["DT_INSERT"] = dataRowArray[0]["DT_INSERT"];
                            dataTable7.Rows.Add(row);
                        }

                        dataTable6.AcceptChanges();
                        DataTable dataTable8 = dataTable7.DefaultView.ToTable(1 != 0, "CD_ITEM");
                        DataTable dataTable9 = dataTable7.DefaultView.ToTable(1 != 0, "CD_PARTNER");
                        string 품목 = "";
                        string 거래처 = "";
                        
                        foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable8.Rows)
                            품목 = 품목 + D.GetString(dataRow["CD_ITEM"]) + "|";
                        
                        foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable9.Rows)
                            거래처 = 거래처 + D.GetString(dataRow["CD_PARTNER"]) + "|";
                        
                        DataTable maxLine = this._biz.GetMaxLine(품목, 거래처);
                        maxLine.PrimaryKey = new DataColumn[] { maxLine.Columns["CD_ITEM"],
                                                                maxLine.Columns["CD_PARTNER"],
                                                                maxLine.Columns["FG_UM"],
                                                                maxLine.Columns["CD_EXCH"] };

                        string str11 = string.Empty;
                        
                        for (int index = 0; index < dataTable7.Rows.Count; ++index)
                        {
                            if (!(dataTable7.Rows[index]["CD_ITEM"].ToString().Trim() == ""))
                            {
                                bool flag7 = this._flexL.DataTable.Select("CD_PARTNER = '" + dataTable7.Rows[index]["CD_PARTNER"].ToString() + "' AND CD_EXCH = '" + dataTable7.Rows[index]["CD_EXCH"].ToString() + "' AND FG_UM = '" + dataTable7.Rows[index]["FG_UM"].ToString() + "' AND CD_ITEM = '" + dataTable7.Rows[index]["CD_ITEM"].ToString() + "' AND SDT_UM = '" + dataTable7.Rows[index]["SDT_UM"].ToString() + "'", "", DataViewRowState.CurrentRows).Length <= 0;
                                DataRow dataRow1 = dataTable7.Rows[index];
                                if (flag7)
                                {
                                    DataRow dataRow2 = maxLine.Rows.Find(new object[] { dataRow1["CD_ITEM"],
                                                                                        dataRow1["CD_PARTNER"],
                                                                                        dataRow1["FG_UM"],
                                                                                        dataRow1["CD_EXCH"] });

                                    Decimal num4 = dataRow2 != null ? (D.GetDecimal(dataRow2["NO_LINE"]) + 1) : 1;
                                    DataRow dataRow3 = this._flexL.DataTable.NewRow();
                                    DataRow[] dataRowArray = dataTable2.Select("CD_ITEM = '" + dataTable7.Rows[index]["CD_ITEM"].ToString().Trim() + "' ", "", DataViewRowState.CurrentRows);
                                    dataRow3["CD_ITEM"] = dataTable7.Rows[index]["CD_ITEM"].ToString().Trim();
                                    
                                    if (dataRowArray.Length > 0)
                                    {
                                        dataRow3["NM_ITEM"] = dataRowArray[0]["NM_ITEM"];
                                        dataRow3["STND_ITEM"] = dataRowArray[0]["STND_ITEM"];
                                        dataRow3["UNIT_PO"] = dataRowArray[0]["UNIT_PO"];
                                        dataRow3["TP_PROC"] = dataRowArray[0]["TP_PROC"];
                                        dataRow3["CLS_ITEM"] = dataRowArray[0]["CLS_ITEM"];
                                    }

                                    dataRow3["CD_EXCH"] = dataTable7.Rows[index]["CD_EXCH"].ToString().Trim();
                                    dataRow3["CD_PARTNER"] = dataTable7.Rows[index]["CD_PARTNER"].ToString().Trim();
                                    dataRow3["CD_PLANT"] = dataTable7.Rows[index]["CD_PLANT"].ToString().Trim();
                                    dataRow3["UM_ITEM"] = dataTable7.Rows[index]["UM_ITEM"].ToString().Trim();
                                    dataRow3["FG_UM"] = dataTable7.Rows[index]["FG_UM"].ToString().Trim();
                                    dataRow3["SDT_UM"] = dataTable7.Rows[index]["SDT_UM"].ToString().Trim();
                                    dataRow3["EDT_UM"] = dataTable7.Rows[index]["EDT_UM"].ToString().Trim() == "" ? "99991231" : dataTable7.Rows[index]["EDT_UM"].ToString().Trim();
                                    dataRow3["NO_LINE"] = num4;
                                    dataRow3["TP_UMMODULE"] = "002";
                                    dataRow3["NM_USER"] = Global.MainFrame.LoginInfo.EmployeeName;
                                    dataRow3["DT_INSERT"] = Global.MainFrame.GetStringToday;
                                    
                                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DONGAH" || Global.MainFrame.ServerKeyCommon.ToUpper() == "SAMWON" || (Global.MainFrame.ServerKeyCommon.ToUpper() == "ADDTEC" || Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL") || Global.MainFrame.ServerKeyCommon.ToUpper() == "SQL_")
                                        this.IsDateCal(dataRow3, "N");
                                    
                                    this._flexL.DataTable.Rows.Add(dataRow3);
                                }
                                else
                                {
                                    string str10 = dataTable7.Rows[index]["CD_ITEM"].ToString().PadRight(10, ' ') + " " + dataTable7.Rows[index]["CD_PARTNER"].ToString().PadRight(10, ' ') + " " + str7.ToString().PadRight(10, ' ') + " " + dataTable7.Rows[index]["FG_UM"].ToString().PadRight(8, ' ') + " " + dataTable7.Rows[index]["SDT_UM"].ToString().PadRight(10, ' ') + " " + (dataTable7.Rows[index]["EDT_UM"].ToString().Trim() == "" ? "99991231" : dataTable7.Rows[index]["EDT_UM"].ToString().PadRight(10, ' '));
                                    stringBuilder3.AppendLine(str10);
                                    flag1 = true;
                                }
                            }
                        }

                        if (flag1)
                        {
                            MsgControl.CloseMsg();
                            this.ShowDetailMessage("엑셀 업로드하는 중에 DB저장된 단가 데이타와 중복된 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder3.ToString());
                        }

                        MsgControl.CloseMsg();
                        this.ShowMessage("엑셀 작업을 완료하였습니다.");
                        this._flexL.RowFilter = "CD_PARTNER = '" + D.GetString(this._flexH[this._flexH.Row, "CD_PARTNER"]) + "' AND CD_PLANT = '" + D.GetString(this._flexH[this._flexH.Row, "CD_PLANT"]) + "'";
                        this._flexL.Redraw = true;
                        this._flexL.Row = this._flexL.Rows.Count - 1;
                        this._flexL.Col = this._flexL.Cols.Fixed;
                        this._flexL.Focus();
                        this._flexL.IsDataChanged = true;
                        this.Page_DataChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flexL.Redraw = true;
            }
        }

        private void btn일괄반영_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow || !this.Verify() || this.ShowMessage("선택된 거래처의 이미 저장된 중복 데이타는 삭제됩니다.\n계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                string sPipe = string.Empty;
                DataTable dt = this._flexL.DataTable.Clone();
                DataTable checkedRows = this._flexH.GetCheckedRows("S");
                
                if (checkedRows == null || checkedRows.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.DD("적용할 거래처") });
                }
                else
                {
                    foreach (DataRow dataRow in checkedRows.Rows)
                        sPipe = sPipe + D.GetString(dataRow["CD_PARTNER"]) + "|";

                    DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y' AND CD_PARTNER = '" + D.GetString(this._flexH["CD_PARTNER"]) + "'");
                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else
                    {
                        foreach (DataRow row in dataRowArray)
                            dt.ImportRow(row);
                        
                        foreach (string MULTI_CD_PARTNER in D.StringConvert.GetPipes(sPipe, 150))
                            this._biz.Save_AddRows(dt, MULTI_CD_PARTNER);
                        
                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("일괄반영") });
                        this._flexL.AcceptChanges();
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected void btn복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flexL.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this.dt복사 = null;
                    this.dt복사 = this._flexL.DataTable.Clone();
                    
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        DataRow row = this.dt복사.NewRow();
                        
                        foreach (DataColumn dataColumn in this.dt복사.Columns)
                            row[dataColumn.ColumnName] = dataRow[dataColumn.ColumnName];
                        
                        this.dt복사.Rows.Add(row);
                    }

                    for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                        this._flexL[@fixed, "S"] = "N";
                    
                    this.ShowMessage("복사되었습니다.");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected void btn붙여넣기_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                if (this.dt복사 == null || this.dt복사.Rows.Count == 0)
                {
                    this.ShowMessage("복사 할 데이터가 없습니다.");
                }
                else
                {
                    foreach (DataRow dataRow in this.dt복사.Rows)
                    {
                        this._flexL.Rows.Add();
                        this._flexL.Row = this._flexL.Rows.Count - this._flexL.Rows.Fixed;

                        foreach (DataColumn dataColumn in this.dt복사.Columns)
                            this._flexL[dataColumn.ColumnName] = dataRow[dataColumn.ColumnName];

                        this._flexL["S"] = "N";
                        this._flexL["CD_PARTNER"] = this._flexH["CD_PARTNER"];
                        this._flexL["CD_PLANT"] = this._flexH["CD_PLANT"];
                        this._flexL["NO_LINE"] = (this._flexL.GetMaxValue("NO_LINE") + 1);
                        
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DONGAH" || Global.MainFrame.ServerKeyCommon.ToUpper() == "SAMWON" || (Global.MainFrame.ServerKeyCommon.ToUpper() == "ADDTEC" || Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL") || Global.MainFrame.ServerKeyCommon.ToUpper() == "SQL_")
                        {
                            this._flexL["SDT_UM"] = "";
                            this._flexL["EDT_UM"] = "99991231";
                        }
                    }

                    this._flexL.AddFinished();
                    this.ShowMessage("작업이 완료되었습니다.");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;
                string string1 = D.GetString(this._flexH[e.NewRange.r1, "CD_PARTNER"]);
                string string2 = D.GetString(this._flexH[e.NewRange.r1, "CD_PLANT"]);
                string filter = "CD_PARTNER = '" + string1 + "' AND CD_PLANT = '" + string2 + "'";
                
                if (this._flexH.DetailQueryNeed)
                    dt = this._biz.SearchDetail(new object[] { this.LoginInfo.CompanyCode,
                                                               this.cbo공장.SelectedValue.ToString(),
                                                               this.cbo조달구분.SelectedValue.ToString(),
                                                               this.cbo계정구분.SelectedValue.ToString(),
                                                               this.cbo품목.SelectedValue.ToString(),
                                                               this.txt품목.Text,
                                                               this.cbo내외자구분.SelectedValue.ToString(),
                                                               D.GetString(this.cbo단가.SelectedValue),
                                                               D.GetString(this.cbo거래처그룹.SelectedValue),
                                                               string1 });

                this._flexL.BindingAdd(dt, filter);
                this._flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (this._flexL.GetData(e.Row, e.Col).ToString() == this._flexL.EditData)
                    return;

                switch (this._flexL.Cols[e.Col].Name)
                {
                    case "SDT_UM":
                    case "EDT_UM":
                        if (!D.StringDate.IsValidDate(D.GetString(this._flexL["SDT_UM"]), D.GetString(this._flexL["EDT_UM"]), false, this._flexL.Cols["SDT_UM"].Caption, this._flexL.Cols["EDT_UM"].Caption))
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

        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid) || e.Parameter.HelpID != HelpID.P_MA_PITEM_SUB1)
                    return;

                if (Checker.IsEmpty((Control)this.cbo공장, this.lbl공장.Text, true))
                    e.Cancel = true;
                else
                    e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;

                if (flexGrid == null || e.Result.HelpID != HelpID.P_MA_PITEM_SUB1)
                    return;
                
                foreach (DataRow dataRow in e.Result.Rows)
                {
                    if (e.Result.Rows[0] != dataRow)
                        this.OnToolBarAddButtonClicked(null, null);

                    flexGrid["CD_ITEM"] = dataRow["CD_ITEM"];
                    flexGrid["NM_ITEM"] = dataRow["NM_ITEM"];
                    flexGrid["STND_ITEM"] = dataRow["STND_ITEM"];
                    flexGrid["UNIT_PO"] = dataRow["UNIT_PO"];
                    flexGrid["TP_PROC"] = dataRow["TP_PROC"];
                    flexGrid["CLS_ITEM"] = dataRow["CLS_ITEM"];
                    flexGrid["UNIT_IM"] = dataRow["UNIT_IM"];
                    flexGrid["UNIT_SO"] = dataRow["UNIT_SO"];
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexL_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow || !(Global.MainFrame.ServerKeyCommon.ToUpper() == "DONGAH") && !(Global.MainFrame.ServerKeyCommon.ToUpper() == "SAMWON") && (!(Global.MainFrame.ServerKeyCommon.ToUpper() == "ADDTEC") && !(Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL")) && !(Global.MainFrame.ServerKeyCommon.ToUpper() == "SQL_"))
                    return;

                this.IsDateCal(this._flexL.GetDataRow(this._flexL.Row), "N");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.IsChanged())
                    this.ToolBarSaveButtonEnabled = true;

                if (this._flexH.HasNormalRow)
                    this.ToolBarDeleteButtonEnabled = this._flexL.HasNormalRow;
                else
                    this.ToolBarDeleteButtonEnabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool Verify()
        {
            if (!base.Verify())
                return false;

            DataTable changes = this._flexL.GetChanges();
            
            if (changes != null && changes.Rows.Count > 0)
            {
                foreach (DataRow dataRow in changes.Rows)
                {
                    if (dataRow.RowState != DataRowState.Deleted)
                    {
                        string str1 = dataRow["CD_ITEM"].ToString();
                        string str2 = dataRow["CD_PARTNER"].ToString();
                        string str3 = dataRow["CD_EXCH"].ToString();
                        string str4 = dataRow["FG_UM"].ToString();
                        string str5 = dataRow["SDT_UM"].ToString();
                        if (this._flexL.DataTable.Select("CD_ITEM = '" + str1 + "' AND CD_PARTNER = '" + str2 + "' AND CD_EXCH = '" + str3 + "' AND FG_UM = '" + str4 + "' AND (SDT_UM = '" + str5 + "')", "", DataViewRowState.CurrentRows).Length > 1)
                        {
                            this.ShowMessage("품목(@), 거래처(@), 환종, 단가유형이 날짜가 겹칩니다.", new object[] { str1, str2 });
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ExcelButtonEnabled();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void IsDateCal(DataRow dr, string YN_DELETE)
        {
            if (D.GetString(dr["SDT_UM"]) == string.Empty || D.GetString(dr["EDT_UM"]) == string.Empty || D.GetString(dr["EDT_UM"]) != "99991231")
                return;

            DataRow[] dataRowArray1 = this._flexL.DataTable.Select("NO_LINE <> '" + D.GetInt(dr["NO_LINE"]) + "' AND CD_PARTNER = '" + dr["CD_PARTNER"].ToString() + "' AND CD_ITEM = '" + dr["CD_ITEM"].ToString() + "' AND CD_EXCH = '" + dr["CD_EXCH"].ToString() + "' AND FG_UM = '" + dr["FG_UM"].ToString() + "'", "", DataViewRowState.CurrentRows);
            if (dataRowArray1.Length == 0)
                return;
            
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NO_LINE", typeof(Decimal));
            dataTable.Columns.Add("DAY", typeof(Decimal));
            
            foreach (DataRow dataRow in dataRowArray1)
            {
                if (!(dataRow["SDT_UM"].ToString() == string.Empty) && (!(YN_DELETE == "Y") || !(D.GetDecimal(dr["SDT_UM"]) < D.GetDecimal(dataRow["SDT_UM"])) || !(D.GetString(dr["EDT_UM"]) == "99991231")))
                {
                    DataRow row = dataTable.NewRow();
                    TimeSpan timeSpan = DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", (IFormatProvider)null) - DateTime.ParseExact(dataRow["SDT_UM"].ToString(), "yyyyMMdd", (IFormatProvider)null);
                    row["NO_LINE"] = dataRow["NO_LINE"];
                    row["DAY"] = Math.Abs(timeSpan.Days);
                    dataTable.Rows.Add(row);
                }
            }

            if (dataTable.Rows.Count < 1)
                return;
            
            DataRow[] dataRowArray2 = dataTable.Select("DAY = MIN(DAY)", "", DataViewRowState.CurrentRows);
            DataRow[] dataRowArray3 = this._flexL.DataTable.Select("CD_PARTNER = '" + dr["CD_PARTNER"].ToString() + "' AND CD_ITEM = '" + dr["CD_ITEM"].ToString() + "' AND CD_EXCH = '" + dr["CD_EXCH"].ToString() + "' AND FG_UM = '" + dr["FG_UM"].ToString() + "' AND NO_LINE = '" + D.GetString(dataRowArray2[0]["NO_LINE"]) + "'", "", DataViewRowState.CurrentRows);
            
            if (YN_DELETE == "Y")
            {
                dataRowArray3[0]["EDT_UM"] = "99991231";
            }
            else
            {
                DateTime t1_1 = DateTime.ParseExact(dataRowArray3[0]["SDT_UM"].ToString(), "yyyyMMdd", null);
                DateTime dateTime = DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", null);
                DateTime t2_1 = dateTime.AddDays(-1.0);
                int num;
                
                if (DateTime.Compare(t1_1, t2_1) >= 0)
                {
                    DateTime t1_2 = DateTime.ParseExact(dataRowArray3[0]["SDT_UM"].ToString(), "yyyyMMdd", null);
                    dateTime = DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", null);
                    DateTime t2_2 = dateTime.AddDays(-1.0);
                    num = DateTime.Compare(t1_2, t2_2) >= 0 ? 1 : 0;
                }
                else
                    num = 0;

                if (num == 0)
                {
                    DataRow dataRow = dataRowArray3[0];
                    string index = "EDT_UM";
                    string format = "{0:yyyyMMdd}";
                    dateTime = DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", null);
                    string str = string.Format(format, dateTime.AddDays(-1));
                    dataRow[index] = str;
                }
            }
        }

        private void ExcelButtonEnabled()
        {
            if (!this._flexH.HasNormalRow)
                this.btn엑셀업로드.Enabled = false;
            else if (D.GetString(this.cbo공장.SelectedValue) != D.GetString(this._flexH["CD_PLANT"]))
                this.btn엑셀업로드.Enabled = false;
            else
                this.btn엑셀업로드.Enabled = true;
        }
    }
}
