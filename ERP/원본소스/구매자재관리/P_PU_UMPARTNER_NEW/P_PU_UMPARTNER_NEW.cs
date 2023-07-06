using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Util;

using System.Threading;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;
using Duzon.ERPU;
using Duzon.Windows.Print;

namespace pur
{
    /// <summary>
    /// ================================================
    /// AUTHOR      : DIGITAL\조형우
    /// CREATE DATE : 2011-01-19 오전 9:41:16
    /// 
    /// MODULE      : 구매/자재
    /// SYSTEM      : 
    /// SUBSYSTEM   : 
    /// PAGE        : 
    /// PROJECT     : 
    /// DESCRIPTION : 
    /// ================================================ 
    /// CHANGE HISTORY
    /// v1.0 : 2011-01-19 오전 9:41:16 신규 생성
    /// ================================================
    /// </summary>

    public partial class P_PU_UMPARTNER_NEW : PageBase
    {
        #region ★ 멤버필드

        private P_PU_UMPARTNER_NEW_BIZ _biz;
        private OpenFileDialog m_FileDlg = new OpenFileDialog();
        private DataTable _dt_EXCEL = null;

        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_PU_UMPARTNER_NEW()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flexH, _flexL };
            DataChanged += new EventHandler(Page_DataChanged);
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_PU_UMPARTNER_NEW_BIZ();

            InitGridH();
            InitGridL();

            _flexH.DetailGrids = new FlexGrid[] { _flexL };
            // _flexH.WhenRowChangeThenGetDetail = false;          // true이면 Header의 Row가 바뀔때마다 Line 내용을 그때 그때 가져온다는 뜻.
            btn엑셀업로드.Enabled = false;
        }

        #endregion

        #region -> InitGridH

        private void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);

            _flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("CD_PARTNER", "거래처", 80, 20, false);
            _flexH.SetCol("LN_PARTNER", "거래처명", 200, 50, false);

            _flexH.SetDummyColumn("S");

            _flexL.SettingVersion = "1.1.0.1";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexH.AfterRowChange += new RangeEventHandler(_flexH_AfterRowChange);
            _flexH.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexH.AfterEdit += new RowColEventHandler(_flexH_AfterEdit);
        }

        #endregion

        #region -> InitGridL

        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, true);

            _flexL.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);

            _flexL.SetCol("CD_ITEM", "품목코드", 100, 20, true);
            _flexL.SetCol("NM_ITEM", "품목명", 140, false);
            _flexL.SetCol("STND_ITEM", "규격", 120, false);
            _flexL.SetCol("UNIT_PO", "발주단위", 40, false);
            _flexL.SetCol("TP_PROC", "조달구분", 100, false);
            _flexL.SetCol("CLS_ITEM", "계정구분", 100, false);
            _flexL.SetCol("CD_EXCH", "환종", 80, true); //환종
            _flexL.SetCol("FG_UM", "단가유형", 80, true);  //단가유형
            _flexL.SetCol("UM_ITEM", "단가", 80, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);//단가
            _flexL.SetCol("SDT_UM", "기간시작일", 80, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);//기간시작일
            _flexL.SetCol("EDT_UM", "기간종료일", 80, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);//기간종료일
            _flexL.SetCol("DT_INSERT", "입력일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);//입력일시
            _flexL.SetCol("NM_USER", "입력자", 80, false);//입력자
            _flexL.SetCol("NO_DESIGN", "도면번호", 140, false);
            _flexL.SetCol("DT_UPDATE", "수정일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("NM_EDIT_USER", "수정자", 80, false);
            _flexL.SetCol("FG_REASON", "변경사유", 120, true);
            _flexL.SetCol("DC_RMK", "비고", 120, true);

            _flexL.SetCol("NO_LINE", "항번",false );
            _flexL.SetCol("CD_PARTNER", "거래처",false);

            ////사용자정의컬럼 추가시 코드관리에 추가하여 사용여부가 사용일 경우 각 구분코드명에 캡션명 저장한 후 업체별로 사용 하면됨... 
            DataTable dtTEXT = MA.GetCode("MA_B000142", false);

            if (dtTEXT != null && dtTEXT.Rows.Count != 0)
            {
                foreach (DataRow row in dtTEXT.Rows)
                {
                    string ColName = D.GetString(row["NAME"]) == "" ? DD("사용자정의컬럼1") : D.GetString(row["NAME"]);
                    string ColCode = D.GetString(row["CODE"]);

                    switch (ColCode)
                    {
                        case "001": //사용자정의컬럼1(CD_USERDEF1)
                            _flexL.SetCol("CD_USERDEF1", ColName, 100, true, typeof(string));
                            break;
                        default:
                            break;
                    }


                }
            }

            //_flexL.Cols["UM_ITEM"].Format = "#,##0.00";

            _flexL.SetDummyColumn("S");
            _flexL.EnterKeyAddRow = false;

            _flexL.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_PO", "TP_PROC", "CLS_ITEM", "DT_INSERT", "NM_USER");
            _flexL.VerifyAutoDelete = new string[] { "CD_ITEM" };
            _flexL.VerifyPrimaryKey = new string[] { "CD_PARTNER", "CD_ITEM", "CD_EXCH", "FG_UM", "SDT_UM", "NO_LINE" };
            _flexL.VerifyNotNull = new string[] { "CD_PARTNER", "CD_ITEM", "CD_EXCH", "FG_UM", "SDT_UM", "EDT_UM" };
            _flexL.VerifyCompare(_flexL.Cols["SDT_UM"], _flexL.Cols["EDT_UM"], OperatorEnum.LessOrEqual); _flexL.VerifyCompare(_flexL.Cols["SDT_UM"], _flexL.Cols["EDT_UM"], OperatorEnum.LessOrEqual);
         //   _flexL.VerifyCompare(_flexL.Cols["UM_ITEM"], 0, OperatorEnum.Greater);

            _flexL.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_PO" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_PO" });

            _flexL.SettingVersion = "1.1.0.1";
            _flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexL.AddRow += new System.EventHandler(OnToolBarAddButtonClicked);
            _flexL.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flexL_ValidateEdit);
            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flexL_BeforeCodeHelp);
            _flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexL_AfterCodeHelp);
            _flexL.AfterEdit += new RowColEventHandler(_flexL_AfterEdit);
            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            InitControl();
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            DataSet ds = GetComboData("N;MA_PLANT", "S;MA_B000009", "S;MA_B000010", "S;PU_C000001", "N;MA_B000005", "N;PU_C000001", "S;PU_C000034", "S;MA_B000029", "S;MA_B000003", "S;PU_C000002", "S;PU_C000049");

            cbo공장.DataSource = ds.Tables[0];
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            cbo조달구분.DataSource = ds.Tables[1];
            cbo조달구분.DisplayMember = "NAME";
            cbo조달구분.ValueMember = "CODE";

            cbo계정구분.DataSource = ds.Tables[2];
            cbo계정구분.DisplayMember = "NAME";
            cbo계정구분.ValueMember = "CODE";

            cbo단가.DataSource = ds.Tables[3];
            cbo단가.DisplayMember = "NAME";
            cbo단가.ValueMember = "CODE";

            // 환종, 단가유형
            _flexL.SetDataMap("CD_EXCH", ds.Tables[4], "CODE", "NAME");
            _flexL.SetDataMap("FG_UM", ds.Tables[5], "CODE", "NAME");

            // 조달구분, 계정구분
            _flexL.SetDataMap("TP_PROC", ds.Tables[1].Copy(), "CODE", "NAME");
            _flexL.SetDataMap("CLS_ITEM", ds.Tables[2].Copy(), "CODE", "NAME");

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WJIS" || Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL")
            {
                DataRow row = ds.Tables[6].NewRow();

                row = ds.Tables[6].NewRow();
                row["CODE"] = "998";
                row["NAME"] = DD("도면번호");
                ds.Tables[6].Rows.Add(row);
            }


            cbo품목.DataSource = ds.Tables[6];
            cbo품목.DisplayMember = "NAME";
            cbo품목.ValueMember = "CODE";

            cbo내외자구분.DataSource = ds.Tables[7];
            cbo내외자구분.DisplayMember = "NAME";
            cbo내외자구분.ValueMember = "CODE";

            cbo거래처분류.DataSource = ds.Tables[8];
            cbo거래처분류.DisplayMember = "NAME";
            cbo거래처분류.ValueMember = "CODE";

            cbo_yn_use.DataSource = ds.Tables[9];
            cbo_yn_use.DisplayMember = "NAME";
            cbo_yn_use.ValueMember = "CODE";

            cbo_YnExist.DataSource = MA.GetCodeUser(new string[] { "", "Y", "N" }, new string[] { "", "Y", "N" });
            cbo_YnExist.DisplayMember = "NAME";
            cbo_YnExist.ValueMember = "CODE";

            _flexL.SetDataMap("FG_REASON", ds.Tables[10], "CODE", "NAME"); // 변경사유

            ////사용자정의컬럼1 CD_USERDEF1
            DataTable dtTEXT = MA.GetCode("MA_B000143", false);

            if (dtTEXT != null && dtTEXT.Rows.Count != 0 && _flexL.Cols.Contains("CD_USERDEF1"))
            {
                _flexL.SetDataMap("CD_USERDEF1", dtTEXT, "CODE", "NAME"); // 단가구분
            }
        }

        #endregion

        #endregion

        #region ★ 메인버튼 클릭

        #region -> 조회버튼 클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                object[] obj = new object[] {
                    LoginInfo.CompanyCode, 
                    D.GetString(cbo공장.SelectedValue), 
                    D.GetString(cbo조달구분.SelectedValue), 
                    D.GetString(cbo계정구분.SelectedValue), 
                    D.GetString(cbo품목.SelectedValue), 
                    txt품목.Text, 
                    D.GetString(cbo내외자구분.SelectedValue), 
                    D.GetString(cbo단가.SelectedValue),
                    "",//거래처그룹  <- 애는 어디서 들어가는걸까?? 2011.SMR
                    txt거래처검색.Text,
                    dpt_period.StartDateToString,
                    dpt_period.EndDateToString,
                    D.GetString(cbo거래처분류.SelectedValue),
                    D.GetString(cbo_yn_use.SelectedValue),
                    D.GetString(cbo_YnExist.SelectedValue)
                };

                DataTable dt = _biz.Search(obj);

                _flexH.Binding = dt;
                btn엑셀업로드.Enabled = true;

                if (!_flexH.HasNormalRow)
                {
                    ShowMessage(PageResultMode.SearchNoData);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가버튼 클릭

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                if (!_flexH.HasNormalRow)
                {
                    ShowMessage("조회된 품목이 없습니다.");
                    return;
                }

                // 라인 최대항번 구하기
                decimal max_no_line = _flexL.GetMaxValue("NO_LINE");   // NO_LINE 이라는 컬럼에서 최대값 가져옴.
                max_no_line++;

                _flexL.Rows.Add();
                _flexL.Row = _flexL.Rows.Count - 1;

                _flexL["CD_PARTNER"] = _flexH.DataView[_flexH.DataIndex()]["CD_PARTNER"].ToString();

                _flexL["CD_PLANT"] = cbo공장.SelectedValue.ToString();

                _flexL["CD_ITEM"] = "";
                _flexL["NM_ITEM"] = "";
                _flexL["STND_ITEM"] = "";
                _flexL["UNIT_PO"] = "";

                _flexL["TP_PROC"] = "";
                _flexL["CD_EXCH"] = "";
                _flexL["FG_UM"] = "";
                _flexL["UM_ITEM"] = 0m;

                if (Duzon.ERPU.MF.Common.BASIC.GetMAEXC_Menu("P_PU_UMPARTNER_NEW", "PU_A00000014") == "100") // 단가기간중복체크시 사용 옵션
                    _flexL["SDT_UM"] = string.Empty;

                else
                    _flexL["SDT_UM"] = MainFrameInterface.GetStringToday;

                _flexL["EDT_UM"] = "99991231";

                _flexL["NO_LINE"] = max_no_line.ToString();
                _flexL["TP_UMMODULE"] = "001";

                _flexL["NM_USER"] = Global.MainFrame.LoginInfo.EmployeeName;
                _flexL["DT_INSERT"] = Global.MainFrame.GetStringToday;
                _flexL.AddFinished();

                _flexL.Col = _flexL.Cols.Fixed;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제버튼 클릭

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete()) return;

                if (!_flexL.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataTable dt = _flexL.GetCheckedRows("S");

                if (dt == null || dt.Rows.Count <= 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flexL.Redraw = false;


                for (int r = _flexL.Rows.Count - 1; r >= _flexL.Rows.Fixed; r--)
                {
                    if (_flexL.GetCellCheck(r, _flexL.Cols["S"].Index) == CheckEnum.Checked)
                    {
              
                        string Filter = "NO_LINE <> '" + D.GetInt(_flexL.Rows[r]["NO_LINE"]) + "' AND CD_PARTNER = '" + _flexL.Rows[r]["CD_PARTNER"].ToString() + "' AND CD_ITEM = '" + _flexL.Rows[r]["CD_ITEM"].ToString() + "' AND CD_EXCH = '" + _flexL.Rows[r]["CD_EXCH"].ToString() + "' AND FG_UM = '" + _flexL.Rows[r]["FG_UM"].ToString() + "'";
                        DataRow[] drs = _flexL.DataTable.Select(Filter, "EDT_UM", DataViewRowState.CurrentRows);

                        if (drs != null && drs.Length > 0)
                            drs[drs.Length - 1]["EDT_UM"] = "99991231";

                        _flexL.Rows.Remove(r);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexL.Redraw = true;
            }
        }

        #endregion

        #region -> 저장버튼 클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄버튼 클릭
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataRow[] row = _flexL.DataTable.Select("S = 'Y'");

                if (row == null || row.Length < 1)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                ReportHelper rptHelper = new ReportHelper("R_PU_UMPARTNER_NEW_0", "거래처별단가관리(NEW)");

                rptHelper.SetData("NM_PLANT", D.GetString(cbo공장.Text));
                rptHelper.SetData("TP_PROC", D.GetString(cbo조달구분.Text));
                rptHelper.SetData("CLS_ITEM", D.GetString(cbo계정구분.Text));
                rptHelper.SetData("FG_UM", D.GetString(cbo단가.Text));
                rptHelper.SetData("TP_PART", D.GetString(cbo내외자구분.Text));
                rptHelper.SetData("DT_FROM", D.GetString(dpt_period.StartDateToString));
                rptHelper.SetData("DT_TO", D.GetString(dpt_period.EndDateToString));
                rptHelper.SetData("CLS_PARTNER", D.GetString(cbo거래처분류.Text));
                rptHelper.SetDataRow(row);
                rptHelper.Print();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ★ 기타메소드

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            //if (!Checker.IsValid(tb_dt_f, tb_dt_t, false, "조회기간From", "조회기간To")) return false;

            return true;
        }

        #endregion

        #region -> SaveData

        protected override bool SaveData()
        {
            // MainGrids 에 설정된 모든 그리드에 무결성 검사 수행
            if (!Verify()) return false;

            DataTable dt = _flexL.GetChanges();

            if (dt == null)		// CheckM() 에서 필요없는 레코드를 삭제한 경우 null 값일 수 있다.
                return true;

            bool bSuccess = _biz.Save(dt);

            if (bSuccess)
            {
                _flexL.AcceptChanges();
                return true;
            }

            return false;
        }

        #endregion

        #endregion

        #region ★ 화면내버튼 클릭

        #region -> 적용버튼 클릭 이벤트(적용)

        private void 적용(object sender, EventArgs e)
        {
            try
            {
                if (cbo단가.Text == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, DD("단가유형"));
                    return;
                }
                if (!_flexH.HasNormalRow)
                    return;

                if (cur증감율.DecimalValue == 0)
                    return;

                int i, j, i_i;

                object obj = MainFrameInterface.LoadHelpWindow("P_PU_UM_SUB", new object[] { MainFrameInterface });
                if (((Duzon.Common.Forms.CommonDialog)obj).ShowDialog() == DialogResult.OK)
                {
                    object[] dlg = (object[])((Duzon.Common.Forms.IHelpWindow)obj).ReturnValues;

                    decimal result = 0;
                    decimal gd_um_item;

                    // txtRt_Flex의 %값 구하기
                    decimal re_in_de_num = cur증감율.DecimalValue / 100;  //증감율/100


                    string ls_fg_um = cbo단가.SelectedValue.ToString(); //단가유형

                    DataTable gdt_return = new DataTable();

                    DataRow[] rows = _flexL.DataTable.Select("S ='Y'");

                    if (rows.Length > 0)
                    {
                        for (i = 0; i < rows.Length; i++)
                            gdt_return.ImportRow(rows[i]);
                    }

                    for (i_i = 0; i_i < gdt_return.Rows.Count; i_i++)
                    {
                        DataRow[] rows_s = _flexL.DataTable.Select("CD_ITEM= '" + rows[i_i]["CD_ITEM"].ToString() + "'");

                        if (rows_s.Length > 0)
                        {
                            for (j = 0; j < rows_s.Length; j++)
                            {
                                string dg_fg_um = rows_s[j]["FG_UM"].ToString();

                                if (ls_fg_um == dg_fg_um)
                                {
                                    gd_um_item = _flexH.CDecimal(rows_s[j]["UM_ITEM"]);

                                    //단가 + (단가 * 증감율)
                                    decimal result1 = re_in_de_num * gd_um_item;
                                    decimal result2 = gd_um_item + result1;

                                    if (dlg[0].ToString() == "1") //절하(버림)
                                    {
                                        result = Math.Floor(result2);
                                    }
                                    else if (dlg[0].ToString() == "2") //반올림
                                    {
                                        result = Math.Round(result2, 0);
                                    }
                                    else if (dlg[0].ToString() == "3") //절상(올림)
                                    {
                                        result = Math.Floor(result2 + 1);
                                    }

                                    rows_s[j]["UM_ITEM"] = result;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                MsgEnd(EX);
            }
        }

        #endregion

        #region -> 엑셀 버튼 클릭 이벤트(_btn엑셀_Click)

        private void _btn엑셀_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow)
                {
                    ShowMessage("조회된 품목이 없습니다.");
                    return;
                }

                if (IsChanged())
                {
                    ShowMessage("변경된 내역이 있습니다. 저장 후 엑셀업로드 버튼을 클릭해주십시오.");
                    return;
                }
                
                Duzon.Common.Util.Excel excel = null;

                m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (m_FileDlg.ShowDialog() == DialogResult.OK)
                {
                    Application.DoEvents();

                    string FileName = m_FileDlg.FileName;
                    string NO_ITEM = string.Empty; string MULTI_ITEM = string.Empty;

                    string NO_PARTNER = string.Empty; string MULTI_PTN = string.Empty;
                    bool 검증여부 = false; bool 품목검증여부 = false; bool 거래처검증여부 = false;
                    bool 기본키적합 = false; bool 엑셀PK적합 = false;
                    bool 품목적합 = false; string 적합품목 = string.Empty; string 적합거래처품목 = string.Empty;
                    bool 거래처적합 = false; string 거래처 = string.Empty; string 거래처명 = string.Empty;
                    bool 변경사유적합 = false; bool 변경사유유무 = false;

                    //excel = new Duzon.Common.Util.Excel();
                    //_dt_EXCEL = excel.StartLoadExcel(FileName);
                    //int j = _flexL.Rows.Count - _flexL.Rows.Fixed; 
                    // 
                    excel = new Duzon.Common.Util.Excel();
                    DataTable dt_EXCEL = excel.StartLoadExcel(FileName); 
                  
                    _dt_EXCEL = dt_EXCEL.Clone();
 
                    foreach (DataRow dr in dt_EXCEL.Rows)
                    {
                        if (D.GetString(dr["CD_ITEM"]) != string.Empty && D.GetString(dr["CD_PARTNER"]) != string.Empty)
                        {
                            //continue;
                            _dt_EXCEL.ImportRow(dr); 
                        }  
                    }
                    // 

                    MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");
                    
                    IsGridebinding();
                    
                    _flexL.Redraw = false;
                    _flexL.EmptyRowFilter();

                    DataTable dtLine = new DataTable();
                    DataTable MasterItemDt = new DataTable();
                    DataTable MasterPtnDt = new DataTable();

                    int iCount = 1;

                    변경사유유무 = _dt_EXCEL.Columns.Contains("FG_REASON");

                    /* 엑셀의 품목이 많을 경우 Received Parameter가 OverFlow되는 문제를 사전에 방지하고자 일정 단위에서 쪼개서 Merge함으로 처리함 */
                     
                    foreach (DataRow drSplit in _dt_EXCEL.Rows)
                    { 
                      
                        if (D.GetString(_flexH["CD_PLANT"]) != D.GetString(drSplit["CD_PLANT"]))
                        {
                            ShowMessage("입력하신 공장이 품목공장과 같아야합니다.");
                            return;
                        } 

                        if (NO_ITEM != drSplit["CD_ITEM"].ToString())
                        {
                            NO_ITEM = drSplit["CD_ITEM"].ToString();
                            MULTI_ITEM += NO_ITEM + "|";
                        }

                        if (NO_PARTNER != drSplit["CD_PARTNER"].ToString())
                        {
                            NO_PARTNER = drSplit["CD_PARTNER"].ToString();
                            MULTI_PTN += NO_PARTNER + "|";
                        }

                        /* 로직 설명 
                         *  1. 목적 : 최대 MAX (8000) 보다 CD_ITEM의 길이가 20이 작은것을 짤라서 Char(8000) 한계 부하를 줄인다
                         *  2. 필요DataTable
                         *       dtLine : 마지막 체크때 Grid라인에 추가하기 위해 Line데이타 가져옴
                         *       MasterItemDt : Master DB에 품목이 존재하는지 체크하기 위해 MasterDB가져옴
                         *       MasterPtnDt : Master DB에 거래처 존재하는지 체크하기 위해 MasterDB가져옴
                         */

                        if (MULTI_ITEM.Length >= 7980 || _dt_EXCEL.Rows.Count == iCount)
                        {
                            DataTable dt = _biz.TotalLine(MULTI_ITEM, "001");
                            dtLine.Merge(dt, false, MissingSchemaAction.Add);

                            //DataTable dtITEM = _biz.ExcelSearch(MULTI_ITEM, "ITEM", "1000");
                            DataTable dtITEM = _biz.ExcelSearch(MULTI_ITEM, "ITEM", _dt_EXCEL.Rows[0]["CD_PLANT"].ToString());
                            MasterItemDt.Merge(dtITEM, false, MissingSchemaAction.Add);

                            DataTable dtPARTNER = _biz.ExcelSearch(MULTI_PTN, "PARTNER", "XXXX");

                            if (MULTI_PTN != "") 
                            MasterPtnDt.Merge(dtPARTNER, false, MissingSchemaAction.Add);

                            MULTI_ITEM = "";
                            MULTI_PTN = "";
                        }

                        iCount++;
                    }

                    DataTable dt엑셀 = _biz.엑셀(_dt_EXCEL);

                    DataTable dt엑셀품목검증 = dt엑셀.Clone();
                    DataTable dt엑셀거래처검증 = dt엑셀.Clone();
                    DataTable dt엑셀PK = dt엑셀.Clone();

                    DataRow NewRowItem; DataRow NewRowPtn;

                    StringBuilder 검증리스트_품목 = new StringBuilder();
                    StringBuilder 검증리스트_거래처 = new StringBuilder();
                    StringBuilder 검증리스트_기본키 = new StringBuilder();
                    StringBuilder 검증리스트_엑셀PK = new StringBuilder();
                    StringBuilder list_fg_reason = new StringBuilder();


                    string msg = "품목코드   거래처코드 거래처명        단가유형\t    시작일    종료일";

                    검증리스트_품목.AppendLine(msg);
                    검증리스트_거래처.AppendLine(msg);
                    검증리스트_기본키.AppendLine(msg);
                    검증리스트_엑셀PK.AppendLine(msg);

                    msg = "-".PadRight(80, '-');

                    검증리스트_품목.AppendLine(msg);
                    검증리스트_거래처.AppendLine(msg);
                    검증리스트_기본키.AppendLine(msg);
                    검증리스트_엑셀PK.AppendLine(msg);

                    string msg1 = "변경사유코드";
                    list_fg_reason.AppendLine(msg1);
                    msg1 = "-".PadRight(80, '-');
                    list_fg_reason.AppendLine(msg1);

                    foreach (DataRow dr in _dt_EXCEL.Rows)
                    {
                        #region -> 엑셀 Data 검증 ( 품목 체크 )

                        DataRow[] drs = MasterItemDt.Select("CD_ITEM = '" + dr["CD_ITEM"].ToString() + "'");

                        if (drs.Length > 0)
                            품목적합 = true;
                        else
                            품목적합 = false;

                        if (품목적합 == true)
                        {
                            NewRowItem = dt엑셀품목검증.NewRow();

                            NewRowItem["CD_ITEM"] = dr["CD_ITEM"].ToString();
                            NewRowItem["CD_EXCH"] = dr["CD_EXCH"].ToString();
                            NewRowItem["CD_PARTNER"] = dr["CD_PARTNER"].ToString();
                            NewRowItem["LN_PARTNER"] = "";
                            NewRowItem["UM_ITEM"] = dr["UM_ITEM"];
                            NewRowItem["FG_UM"] = dr["FG_UM"].ToString();
                            NewRowItem["SDT_UM"] = dr["SDT_UM"].ToString();
                            NewRowItem["EDT_UM"] = dr["EDT_UM"].ToString().Trim() == "" ? "99991231" : dr["EDT_UM"].ToString().Trim();
                            NewRowItem["NO_LINE"] = 0;
                            NewRowItem["TP_UMMODULE"] = "001";
                            NewRowItem["NM_USER"] = Global.MainFrame.LoginInfo.EmployeeName;
                            NewRowItem["DT_INSERT"] = Global.MainFrame.GetStringToday;
                            if (변경사유유무)
                                NewRowItem["FG_REASON"] = dr["FG_REASON"];

                            dt엑셀품목검증.Rows.Add(NewRowItem);
                            품목적합 = false;
                        }
                        else
                        {
                            string CD_ITEM = dr["CD_ITEM"].ToString().PadRight(10, ' ');
                            string CD_PARTNER = dr["CD_PARTNER"].ToString().PadRight(10, ' ');
                            string LN_PARTNER = String.Empty.PadRight(10, ' ');
                            string FG_UM = dr["FG_UM"].ToString().PadRight(8, ' ');
                            string SDT = dr["SDT_UM"].ToString().PadRight(8, ' ');
                            string EDT = dr["EDT_UM"].ToString().Trim() == "" ? "99991231" : dr["EDT_UM"].ToString().PadRight(8, ' ');

                            string msg2 = CD_ITEM + " " + CD_PARTNER + " " + LN_PARTNER + " " + FG_UM + " " + SDT + " " + EDT;

                            검증리스트_품목.AppendLine(msg2);
                            품목검증여부 = true;
                            continue;
                        }


                        #endregion

                        /* *********************************************************************************************** */

                        #region -> 엑셀 Data 검증 ( 거래처 체크 )

                        if (dr["CD_PARTNER"].ToString() == "") continue;

                        drs = dt엑셀품목검증.Select("CD_PARTNER = '" + dr["CD_PARTNER"].ToString() + "'");

                        if (drs.Length > 0)
                        {
                            DataRow[] drs거래처 = MasterPtnDt.Select("CD_PARTNER = '" + drs[0]["CD_PARTNER"].ToString() + "'");
                            if (drs거래처.Length > 0)
                            {
                                거래처명 = drs거래처[0]["LN_PARTNER"].ToString();
                                거래처적합 = true;
                            }
                        }
                        else
                            거래처적합 = false;

                        if (거래처적합)
                        {
                            NewRowPtn = dt엑셀거래처검증.NewRow();

                            NewRowPtn["CD_ITEM"] = dr["CD_ITEM"].ToString();
                            NewRowPtn["CD_EXCH"] = dr["CD_EXCH"].ToString();
                            NewRowPtn["CD_PARTNER"] = dr["CD_PARTNER"].ToString();
                            NewRowPtn["LN_PARTNER"] = 거래처명;
                            NewRowPtn["CD_PLANT"] = D.GetString(_flexH["CD_PLANT"]);

                            NewRowPtn["UM_ITEM"] = dr["UM_ITEM"].ToString();
                            NewRowPtn["FG_UM"] = dr["FG_UM"].ToString();
                            NewRowPtn["SDT_UM"] = dr["SDT_UM"].ToString();
                            NewRowPtn["EDT_UM"] = dr["EDT_UM"].ToString();
                            NewRowPtn["NO_LINE"] = dr["NO_LINE"];
                            NewRowPtn["TP_UMMODULE"] = dr["TP_UMMODULE"];
                            NewRowPtn["NM_USER"] = dr["NM_USER"];
                            NewRowPtn["DT_INSERT"] = dr["DT_INSERT"];
                            if (변경사유유무)
                                NewRowPtn["FG_REASON"] = dr["FG_REASON"];

                            dt엑셀거래처검증.Rows.Add(NewRowPtn);

                            거래처적합 = false;
                        }
                        else
                        {
                            string CD_ITEM = dr["CD_ITEM"].ToString().PadRight(10, ' ');
                            string CD_PARTNER = dr["CD_PARTNER"].ToString().PadRight(10, ' ');
                            string LN_PARTNER = String.Empty.PadRight(10, ' ');
                            string FG_UM = dr["FG_UM"].ToString().PadRight(8, ' ');
                            string SDT = dr["SDT_UM"].ToString().PadRight(10, ' ');
                            string EDT = dr["EDT_UM"].ToString().Trim() == "" ? "99991231" : dr["EDT_UM"].ToString().PadRight(10, ' ');

                            string msg2 = CD_ITEM + " " + CD_PARTNER + " " + LN_PARTNER + " " + FG_UM + " " + SDT + " " + EDT;

                            검증리스트_거래처.AppendLine(msg2);
                            거래처검증여부 = true;
                        }

                        #endregion

                        /* *********************************************************************************************** */
                    }   // 엑셀 데이타 for문 끝

                    if (품목검증여부)
                    {
                        MsgControl.CloseMsg();

                        ShowDetailMessage("엑셀 업로드하는 중에 [마스터품목]과 불일치 항목들이 존재합니다. \n " +
                        " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                    }

                    if (거래처검증여부)
                    {

                        MsgControl.CloseMsg();
                        ShowDetailMessage("엑셀 업로드하는 중에 [마스터거래처]와 불일치 항목들이 존재합니다. \n " +
                        " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_거래처.ToString());
                    }

                    #region -> 엑셀 Data 검증 ( 엑셀화일상에 품목 중복 체크 )

                    MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");

                    품목검증여부 = false; 거래처검증여부 = false;

                    for (int i = 0; i < dt엑셀거래처검증.Rows.Count; i++)
                    {
                        DataRow[] drChk = dt엑셀거래처검증.Select("CD_PARTNER = '" + dt엑셀거래처검증.Rows[i]["CD_PARTNER"].ToString() +
                                                                    "' AND CD_EXCH = '" + dt엑셀거래처검증.Rows[i]["CD_EXCH"].ToString() +
                                                                    "' AND FG_UM = '" + dt엑셀거래처검증.Rows[i]["FG_UM"].ToString() +
                                                                    "' AND CD_ITEM = '" + dt엑셀거래처검증.Rows[i]["CD_ITEM"].ToString() +
                                                                    "' AND SDT_UM = '" + dt엑셀거래처검증.Rows[i]["SDT_UM"].ToString() +
                                                                    "'", "", DataViewRowState.CurrentRows);
                        int iRowCount = 0;
                        foreach (DataRow drDel in drChk)
                        {
                            if (iRowCount > 0)
                            {
                                drDel.Delete();
                            }
                            iRowCount++;
                        }

                        DataRow dr1 = dt엑셀PK.NewRow();

                        dr1["CD_ITEM"] = drChk[0]["CD_ITEM"];
                        dr1["CD_EXCH"] = drChk[0]["CD_EXCH"];
                        dr1["CD_PARTNER"] = drChk[0]["CD_PARTNER"];
                        dr1["LN_PARTNER"] = drChk[0]["LN_PARTNER"];
                        dr1["CD_PLANT"] = drChk[0]["CD_PLANT"];
                        dr1["UM_ITEM"] = drChk[0]["UM_ITEM"];
                        dr1["FG_UM"] = drChk[0]["FG_UM"];
                        dr1["SDT_UM"] = drChk[0]["SDT_UM"];
                        dr1["EDT_UM"] = drChk[0]["EDT_UM"];
                        dr1["NO_LINE"] = drChk[0]["NO_LINE"];
                        dr1["TP_UMMODULE"] = drChk[0]["TP_UMMODULE"];
                        dr1["NM_USER"] = drChk[0]["NM_USER"];
                        dr1["DT_INSERT"] = drChk[0]["DT_INSERT"];
                        if (변경사유유무)
                            dr1["FG_REASON"] = drChk[0]["FG_REASON"];

                        dt엑셀PK.Rows.Add(dr1);
                    }

                    dt엑셀거래처검증.AcceptChanges();

                    #endregion

                    /* *********************************************************************************************** */

                    #region -> 마지막 엑셀 Data 검증 ( Flex 라인상의 기본키 중복 체크 )
                    DataTable dt품목그룹 = dt엑셀PK.DefaultView.ToTable(true, "CD_ITEM");
                    DataTable dt거래처그룹 = dt엑셀PK.DefaultView.ToTable(true, "CD_PARTNER");


                    string 멀티품목 = "";
                    string 멀티거래처 = "";
                    foreach (DataRow row in dt품목그룹.Rows)
                    {
                        멀티품목 += D.GetString(row["CD_ITEM"]) + "|";
                    }
                    foreach (DataRow row in dt거래처그룹.Rows)
                    {
                        멀티거래처 += D.GetString(row["CD_PARTNER"]) + "|";
                    }

                    DataTable dtMaxLine = _biz.GetMaxLine(멀티품목, 멀티거래처);
                    dtMaxLine.PrimaryKey = new DataColumn[] { dtMaxLine.Columns["CD_ITEM"], dtMaxLine.Columns["CD_PARTNER"], dtMaxLine.Columns["FG_UM"], dtMaxLine.Columns["CD_EXCH"] };
                    DataTable dt_Reason = GetComboData("S;PU_C000049").Tables[0];

                    int iMaxValue = 0; int MaxLine = 0; decimal MaxValue = 0; NO_ITEM = string.Empty;

                    for (int i = 0; i < dt엑셀PK.Rows.Count; i++)
                    {
                        if (dt엑셀PK.Rows[i]["CD_ITEM"].ToString().Trim() == "") continue;

                        DataRow[] existLineDr = _flexL.DataTable.Select("CD_PARTNER = '" + dt엑셀PK.Rows[i]["CD_PARTNER"].ToString() +
                                                            "' AND CD_EXCH = '" + dt엑셀PK.Rows[i]["CD_EXCH"].ToString() +
                                                            "' AND FG_UM = '" + dt엑셀PK.Rows[i]["FG_UM"].ToString() +
                                                            "' AND CD_ITEM = '" + dt엑셀PK.Rows[i]["CD_ITEM"].ToString() +
                                                            "' AND SDT_UM = '" + dt엑셀PK.Rows[i]["SDT_UM"].ToString() +
                                                            "'", "", DataViewRowState.CurrentRows);
                        if (existLineDr.Length > 0)
                            기본키적합 = false;
                        else
                            기본키적합 = true;

                        DataRow row엑셀 = dt엑셀PK.Rows[i];
                        if (기본키적합 == true)
                        {
                            //DataRow[] dr = _flexL.DataTable.Select("CD_ITEM = '" + dt엑셀PK.Rows[i]["CD_ITEM"].ToString().Trim() + "'");
                            //MaxValue = dr.Length + 1;

                            DataRow rowLine = dtMaxLine.Rows.Find(new object[] { row엑셀["CD_ITEM"], row엑셀["CD_PARTNER"], row엑셀["FG_UM"], row엑셀["CD_EXCH"] });
                            if (rowLine == null)
                            {
                                MaxValue += 1;
                            }
                            else
                            {
                                MaxValue = D.GetDecimal(rowLine["NO_LINE"]) + 1;
                                rowLine["NO_LINE"] = MaxValue;
                            }

                            DataRow lineDr = _flexL.DataTable.NewRow();

                            DataRow[] drs = MasterItemDt.Select("CD_ITEM = '" + dt엑셀PK.Rows[i]["CD_ITEM"].ToString().Trim() + "' ", "", DataViewRowState.CurrentRows);

                            lineDr["CD_ITEM"] = dt엑셀PK.Rows[i]["CD_ITEM"].ToString().Trim();
                            if (drs.Length > 0)
                            {
                                lineDr["NM_ITEM"] = drs[0]["NM_ITEM"];
                                lineDr["STND_ITEM"] = drs[0]["STND_ITEM"];
                                lineDr["UNIT_PO"] = drs[0]["UNIT_PO"];
                                lineDr["TP_PROC"] = drs[0]["TP_PROC"];
                                lineDr["CLS_ITEM"] = drs[0]["CLS_ITEM"];
                            }
                            lineDr["CD_EXCH"] = dt엑셀PK.Rows[i]["CD_EXCH"].ToString().Trim();
                            lineDr["CD_PARTNER"] = dt엑셀PK.Rows[i]["CD_PARTNER"].ToString().Trim();
                            lineDr["CD_PLANT"] = dt엑셀PK.Rows[i]["CD_PLANT"].ToString().Trim();
                            lineDr["UM_ITEM"] = dt엑셀PK.Rows[i]["UM_ITEM"].ToString().Trim();
                            lineDr["FG_UM"] = dt엑셀PK.Rows[i]["FG_UM"].ToString().Trim();
                            lineDr["SDT_UM"] = dt엑셀PK.Rows[i]["SDT_UM"].ToString().Trim();
                            lineDr["EDT_UM"] = dt엑셀PK.Rows[i]["EDT_UM"].ToString().Trim() == "" ? "99991231" : dt엑셀PK.Rows[i]["EDT_UM"].ToString().Trim();
                            lineDr["NO_LINE"] = MaxValue;
                            lineDr["TP_UMMODULE"] = "001";
                            lineDr["NM_USER"] = Global.MainFrame.LoginInfo.EmployeeName;
                            lineDr["DT_INSERT"] = Global.MainFrame.GetStringToday;
                            if (변경사유유무)
                            {
                                DataRow[] row = dt_Reason.Select("CODE = '" + D.GetString(dt엑셀PK.Rows[i]["FG_REASON"]) + "'");
                                if (row != null && row.Length > 0)
                                {
                                    lineDr["FG_REASON"] = row[0]["CODE"];
                                }
                                else
                                {
                                    변경사유적합 = true;
                                    list_fg_reason.AppendLine(D.GetString(dt엑셀PK.Rows[i]["FG_REASON"]));
                                }
                            }


                            기본키적합 = false;
                            
                            _flexL.DataTable.Rows.Add(lineDr);
                            IsDateCal(lineDr);

                            //MaxLine = 0;
                        }
                        else
                        {
                            string CD_ITEM = dt엑셀PK.Rows[i]["CD_ITEM"].ToString().PadRight(10, ' ');
                            string CD_PARTNER = dt엑셀PK.Rows[i]["CD_PARTNER"].ToString().PadRight(10, ' ');
                            string LN_PARTNER = 거래처명.ToString().PadRight(10, ' ');
                            string FG_UM = dt엑셀PK.Rows[i]["FG_UM"].ToString().PadRight(8, ' ');
                            string SDT = dt엑셀PK.Rows[i]["SDT_UM"].ToString().PadRight(10, ' ');
                            string EDT = dt엑셀PK.Rows[i]["EDT_UM"].ToString().Trim() == "" ? "99991231" : dt엑셀PK.Rows[i]["EDT_UM"].ToString().PadRight(10, ' ');

                            string msg2 = CD_ITEM + " " + CD_PARTNER + " " + LN_PARTNER + " " + FG_UM + " " + SDT + " " + EDT;

                            검증리스트_기본키.AppendLine(msg2);
                            검증여부 = true;
                        }
                    }

                    if (검증여부)
                    {
                        MsgControl.CloseMsg();
                        ShowDetailMessage("엑셀 업로드하는 중에 DB저장된 단가 데이타와 중복된 항목들이 존재합니다. \n " +
                        " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_기본키.ToString());
                    }


                    if (변경사유적합)
                    {

                        MsgControl.CloseMsg();
                        ShowDetailMessage("엑셀 업로드하는 중에 [마스터변경사유]와 불일치 항목들이 존재합니다. \n " +
                        " \n ▼ 버튼을 눌러서 목록을 확인하세요!", list_fg_reason.ToString());
                    }

                    #endregion

                    /* *********************************************************************************************** */

                    MsgControl.CloseMsg();

                    ShowMessage("엑셀 작업을 완료하였습니다.");

                    _flexL.RowFilter = "CD_PARTNER = '" + _flexH[_flexH.Row, "CD_PARTNER"].ToString() + "'";
                    _flexL.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                _flexL.Redraw = true;
            }
        }

        #endregion

        #endregion

        #region ★ 그리드 이벤트

        #region -> 그리드 행변경 이벤트(_flexH_AfterRowChange)

        void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;
                string Key = _flexH[e.NewRange.r1, "CD_PARTNER"].ToString();
                string Filter = "CD_PARTNER = '" + Key + "'";
                if (_flexH.DetailQueryNeed)
                {
                    object[] obj = new object[] {
                        LoginInfo.CompanyCode, 
                        D.GetString(cbo공장.SelectedValue), 
                        D.GetString(cbo조달구분.SelectedValue), 
                        D.GetString(cbo계정구분.SelectedValue), 
                        D.GetString(cbo품목.SelectedValue), 
                        txt품목.Text, 
                        D.GetString(cbo내외자구분.SelectedValue), 
                        D.GetString(cbo단가.SelectedValue), 
                        Key,
                        dpt_period.StartDateToString,
                        dpt_period.EndDateToString
                    };

                    dt = _biz.SearchDetail(obj);
                }
                _flexL.BindingAdd(dt, Filter);
                _flexH.DetailQueryNeed = false;
                
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 컬럼 변경 처리 이벤트(_flexL_ValidateEdit)

        private void _flexL_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {
                string OldValue = _flexL.GetData(e.Row, e.Col).ToString();
                string NewValue = _flexL.EditData;

                if (OldValue == NewValue) return;

                string 컬럼네임 = _flexL.Cols[e.Col].Name;

                switch (컬럼네임)
                {
                    case "SDT_UM":
                        if (!D.StringDate.IsValidDate(D.GetString(_flexL["SDT_UM"]), D.GetString(_flexL["EDT_UM"]), false, _flexL.Cols["SDT_UM"].Caption, _flexL.Cols["EDT_UM"].Caption))
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (Duzon.ERPU.MF.Common.BASIC.GetMAEXC_Menu("P_PU_UMPARTNER_NEW", "PU_A00000014") == "100") // 단가기간중복체크시 사용 옵션
                        {
                            if (!IsChkDate())
                            {
                                _flexL["STD_UM"] = OldValue;
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;
                    case "EDT_UM":
                        if (!D.StringDate.IsValidDate(D.GetString(_flexL["SDT_UM"]), D.GetString(_flexL["EDT_UM"]), false, _flexL.Cols["SDT_UM"].Caption, _flexL.Cols["EDT_UM"].Caption))
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (Duzon.ERPU.MF.Common.BASIC.GetMAEXC_Menu("P_PU_UMPARTNER_NEW", "PU_A00000014") == "100") // 단가기간중복체크시 사용 옵션
                        {
                            if (!IsChkDate())
                            {
                                _flexL["ETD_UM"] = OldValue;
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 도움창 호출전 세팅 이벤트(_flexL_BeforeCodeHelp)

        void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                switch (e.Parameter.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB1:
                        if (Duzon.ERPU.Checker.IsEmpty(cbo공장, lbl공장.Text, true))
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P09_CD_PLANT = cbo공장.SelectedValue.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 도움창 호출후 처리 이벤트(_flexL_AfterCodeHelp)

        void _flexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                switch (e.Result.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB1:
                        foreach (DataRow dr in e.Result.Rows)
                        {
                            if (e.Result.Rows[0] != dr)     //첫번째 행이 아닐 경우만 추가를 해준다.
                                OnToolBarAddButtonClicked(null, null);

                            _flex["CD_ITEM"] = dr["CD_ITEM"];
                            _flex["NM_ITEM"] = dr["NM_ITEM"];
                            _flex["STND_ITEM"] = dr["STND_ITEM"];
                            _flex["UNIT_PO"] = dr["UNIT_PO"];
                            _flex["TP_PROC"] = dr["TP_PROC"];
                            _flex["CLS_ITEM"] = dr["CLS_ITEM"];
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 수정후 처리 이벤트(_flexL_AfterEdit)
        void _flexL_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;
                if (_flexL.Cols[e.Col].Name == "S") return;

                IsDateCal(_flexL.GetDataRow(_flexL.Row)); 
            }
            catch (Exception ex)
            {

                MsgEnd(ex);
            }
        } 
        #endregion

        #region -> _flex_CheckHeaderClick
        //하단그리드체크박스의헤더를마우스오른쪽클릭했을때발생하는이벤트(OnCheckBoxHeaderClick)
        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단그리드Header Click 이벤트

                        //데이타테이블의체크값을변경해주어도화면에보이는값을변경시키지못하므로화면의값도같이변경시켜줌
                        _flexH.Row = 1; // 맨처음row에자동위치하도록해야틀어지지않는다.

                        if (!_flexH.HasNormalRow || !_flexH.HasNormalRow) return;


                        for (int h = 0; h < _flexH.Rows.Count - 1; h++)
                        {
                            _flexH.Row = h + 1; //Row가순차적으로변경되면서RowChange() 이벤트를자동실행하게유도한다.

                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                            {
                                if (_flexL.RowState(i) == DataRowState.Deleted) continue;

                                _flexL[i, "S"] = D.GetString(_flexH["S"]);
                            }
                        }


                        break;

                    case "_flexL":  //하단그리드Header Click 이벤트

                        if (!_flexL.HasNormalRow) return;

                        _flexH["S"] = D.GetString(_flexL["S"]);

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion 

        #region -> _flexH_AfterEdit

        void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_flexH[_flexH.Row, "S"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #endregion

        #region ★ 기타 이벤트

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (_flexH.HasNormalRow)
                    ToolBarDeleteButtonEnabled = _flexL.HasNormalRow;
                else
                    ToolBarDeleteButtonEnabled = false;


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Verify Override

        protected override bool Verify()
        {
            if (!base.Verify())
                return false;

            DataTable dt = _flexL.GetChanges();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                        continue;

                    string strCD_ITEM = dr["CD_ITEM"].ToString();
                    string strCD_PARTNER = dr["CD_PARTNER"].ToString();
                    string strCD_EXCH = dr["CD_EXCH"].ToString();
                    string strFG_UM = dr["FG_UM"].ToString();
                    string strSDT_UM = dr["SDT_UM"].ToString();

                    string strCondition = "CD_ITEM = '" + strCD_ITEM +
                            "' AND CD_PARTNER = '" + strCD_PARTNER +
                            "' AND CD_EXCH = '" + strCD_EXCH +
                            "' AND FG_UM = '" + strFG_UM +
                            "' AND (SDT_UM = '" + strSDT_UM + "')";

                    DataRow[] drArr = _flexL.DataTable.Select(strCondition, "", DataViewRowState.CurrentRows);

                    if (drArr.Length > 1)
                    {
                        ShowMessage("품목(" + strCD_ITEM + "), 거래처(" + strCD_PARTNER + "), 환종, 단가유형이 날짜가 겹칩니다.");
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region -> IsGridebinding
        private void IsGridebinding()
        {
            if (!_flexH.HasNormalRow) return;

            string multiCdPartner = string.Empty;

            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
            {
                if (!_flexH.DetailQueryNeedByRow(i)) continue;

                multiCdPartner += D.GetString(_flexH[i, "CD_PARTNER"]) + "|";
                _flexH.SetDetailQueryNeedByRow(i, false);
            }

            List<string> list = new List<string>();
            list.Add(MA.Login.회사코드);
            list.Add(string.Empty);
            list.Add(D.GetString(cbo공장.SelectedValue));
            list.Add(D.GetString(cbo조달구분.SelectedValue));
            list.Add(D.GetString(cbo계정구분.SelectedValue));
            list.Add(D.GetString(cbo품목.SelectedValue));
            list.Add(txt품목.Text);
            list.Add(D.GetString(cbo내외자구분.SelectedValue));
            list.Add(D.GetString(cbo단가.SelectedValue));
            list.Add(dpt_period.StartDateToString);
            list.Add(dpt_period.EndDateToString);

            DataTable dt = _biz.SearchMulti(multiCdPartner, list.ToArray());

            string Filter = "CD_PARTNER = '" + D.GetString(_flexH["CD_PARTNER"]) + "'";

            _flexL.BindingAdd(dt, Filter);
        } 
        #endregion
        
        #region -> IsChkDate
        private bool IsChkDate()
        {
            DataRow[] drs = _flexL.DataTable.Select("NO_LINE <> '" + D.GetInt(_flexL["NO_LINE"]) + "' AND  CD_PARTNER = '" + _flexL["CD_PARTNER"].ToString() + "' AND CD_ITEM = '" + _flexL["CD_ITEM"].ToString() + "' AND CD_EXCH = '" + _flexL["CD_EXCH"].ToString() + "' AND FG_UM = '" + _flexL["FG_UM"].ToString() + "' AND EDT_UM >= '" + _flexL["SDT_UM"].ToString() + "' AND SDT_UM <= '" + _flexL["EDT_UM"].ToString() + "'", "", DataViewRowState.CurrentRows);
            if (drs.Length != 0)
            {
                ShowMessage("기간이 겹치는건이 있습니다.");
                return false;
            }
            return true;
        } 
        #endregion

        #region -> IsDateCal
        private void IsDateCal(DataRow dr)
        {
            if (D.GetString(dr["SDT_UM"]) == string.Empty || D.GetString(dr["EDT_UM"]) == string.Empty) return;

            DataRow[] drs = _flexL.DataTable.Select("NO_LINE <> '" + D.GetInt(dr["NO_LINE"]) + "' AND CD_PARTNER = '" + dr["CD_PARTNER"].ToString() + "' AND CD_ITEM = '" + dr["CD_ITEM"].ToString() + "' AND CD_EXCH = '" + dr["CD_EXCH"].ToString() + "' AND FG_UM = '" + dr["FG_UM"].ToString() + "'", "", DataViewRowState.CurrentRows);

            if (drs.Length == 0) return;

            DataTable dt = new DataTable();
            dt.Columns.Add("NO_LINE", typeof(decimal));
            dt.Columns.Add("DAY", typeof(decimal));

            foreach (DataRow row in drs)
            {
                DataRow newRow = dt.NewRow();
                TimeSpan ts = DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", null) - DateTime.ParseExact(row["SDT_UM"].ToString(), "yyyyMMdd", null);
                newRow["NO_LINE"] = row["NO_LINE"];
                newRow["DAY"] = Math.Abs(ts.Days);
                dt.Rows.Add(newRow);
            }
            
            DataRow[] drsMin = dt.Select("DAY = MIN(DAY)", "", DataViewRowState.CurrentRows);
            DataRow[] drs2 = _flexL.DataTable.Select("CD_PARTNER = '" + dr["CD_PARTNER"].ToString() + "' AND CD_ITEM = '" + dr["CD_ITEM"].ToString() + "' AND CD_EXCH = '" + dr["CD_EXCH"].ToString() + "' AND FG_UM = '" + dr["FG_UM"].ToString() + "' AND NO_LINE = '" + D.GetInt(drsMin[0]["NO_LINE"]) + "'", "", DataViewRowState.CurrentRows);
          
            if (DateTime.Compare(DateTime.ParseExact(drs2[0]["SDT_UM"].ToString(), "yyyyMMdd", null), DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", null).AddDays(-1)) < 0 || DateTime.Compare(DateTime.ParseExact(drs2[0]["SDT_UM"].ToString(), "yyyyMMdd", null), DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", null).AddDays(-1)) < 0)
                drs2[0]["EDT_UM"] = String.Format(@"{0:yyyyMMdd}", DateTime.ParseExact(dr["SDT_UM"].ToString(), "yyyyMMdd", null).AddDays(-1));
        } 
        #endregion
       
        #endregion
    }
}