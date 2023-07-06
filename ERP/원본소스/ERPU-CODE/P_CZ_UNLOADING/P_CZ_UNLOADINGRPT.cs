using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Controls;
using Duzon.Common.BpControls;



using Duzon.BizOn.Erpu.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace cz
{
    /// <summary>
    /// ================================================
    /// AUTHOR      : 
    /// CREATE DATE : 2013-07-14 오후 3:53:30
    /// 
    /// MODULE      : 커스터마이징 생산관리
    /// SYSTEM      : 
    /// SUBSYSTEM   : 
    /// PAGE        : 
    /// PROJECT     : 
    /// DESCRIPTION : 참고화면 - 
    /// ================================================ 
    /// CHANGE HISTORY
    /// v1.0 : 
    /// ================================================
    /// </summary>

    public partial class P_CZ_UNLOADINGRPT : PageBase
    {
        P_CZ_UNLOADINGRPT_BIZ _biz;
        //OpenFileDialog _FileDlg = new OpenFileDialog();
        //DataTable _dt_EXCEL = null;
        //bool 저장유무 = false;

        public P_CZ_UNLOADINGRPT()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flexL, _flexL2 };
            _flexH.DetailGrids = new FlexGrid[] { _flexL, _flexL2 };
            DataChanged += new EventHandler(Page_DataChanged);
        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_UNLOADINGRPT_BIZ();
            InitGrid();
            InitEvent();



            btn01.Visible = false;
            btn02.Visible = false;

        }



        private void InitGrid()
        {
            // 상단 그리드
            _flexH.BeginSetting(1, 1, true);
            this._flexH.SetCol("NO_BL", "B/L번호", 80, false, typeof(string));
            this._flexH.SetCol("DT_SHIPPING", "선적일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_LOADING", "선적지", 100, false, typeof(string));
            this._flexH.SetCol("ARRIVER", "도착지", 100, false, typeof(string));
            this._flexH.SetCol("LN_PARTNER", "거래처", 140);
            this._flexH.SetCol("REMARK", "비고", 100, false, typeof(string));
            this._flexH.SetCol("NM_EXCH", "통화", 40, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_EX", "원화금액", 80);
            this._flexH.SetCol("NM_VESSEL", "VESSEL", 80, false, typeof(string));
            this._flexH.SetCol("CD_PLANT", "CD_PLANT", false);
            this._flexH.SetCol("CD_ITEM", "CD_ITEM", false);


            //_flexH.SetDummyColumn("S");

            _flexH.AfterRowChange += new RangeEventHandler(FlexH_AfterRowChange);

            _flexH.SettingVersion = "1.0.0.1";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);


            // 중단 그리드
            _flexL2.BeginSetting(2, 1, false);
           
            _flexL2.SetCol("TM_LOADING", "선적항", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexL2.SetCol("TM_UNLOADING", "하역항", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexL2.SetCol("RT_CONTRACT", "계약 RATE", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexL2.SetCol("UM_CONTRACT", "계약 단가", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexL2.SetCol("AM_CONTRACT", "계약 금액", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            _flexL2.SetCol("UM_DEM", "Dem. 단가", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexL2.SetCol("AM_DEM", "Dem. 금액", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            _flexL2.SetCol("YN_DEM", "발생여부", 100, true, CheckTypeEnum.Y_N);

            _flexL2[0, "TM_LOADING"] = "소요시간";
            _flexL2[0, "TM_UNLOADING"] = "소요시간";

            _flexL2[0, "RT_CONTRACT"] = "계약";
            _flexL2[0, "UM_CONTRACT"] = "계약";
            _flexL2[0, "AM_CONTRACT"] = "계약";

            _flexL2[0, "UM_DEM"] = "Demmurage";
            _flexL2[0, "AM_DEM"] = "Demmurage";
            _flexL2[0, "YN_DEM"] = "Demmurage";

            _flexL2.SetHeaderMerge();

            _flexL2.VerifyAutoDelete = new string[] { "NM_VESSEL" };
            _flexL2.VerifyPrimaryKey = new string[] { "NM_VESSEL" };
            _flexL2.VerifyNotNull = new string[] { "NM_VESSEL" };

           
            _flexL2.SettingVersion = "1.0.0.1";
            _flexL2.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);



            // 하단 그리드
            _flexL.BeginSetting(2, 1, false);
       
            //_flexL.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flexL.SetCol("DT_UNLOADING", "하역일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("CD_SL", "창고", 100, true, typeof(string));
            _flexL.SetCol("CD_ITEM", "CD_ITEM", false);

            _flexL.SetCol("UNLOADING_HARBOR", "하역항", false);

            _flexL.SetCol("WT_LOADING", "본선량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("LOSS_LOADING", "감모량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("RT_LOADING", "BALANCE", 80, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            _flexL.SetCol("WT_UNLOADING", "본선량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("LOSS_UNLOADING", "감모량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("RT_UNLOADING", "BALANCE", 80, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            _flexL.SetCol("WT_REAL", "하역량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("LOSS_REAL", "감모량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("RT_REAL", "BALANCE", 80, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);


            _flexL.SetCol("VALUE_EL1", "산가", 80, true, typeof(string));
            _flexL.SetCol("VALUE_EL2", "M&I", 80, true, typeof(string));
            _flexL.SetCol("VALUE_EL3", "요오드가", 80, true, typeof(string));
            _flexL.SetCol("VALUE_EL4", "MP", 80, true, typeof(string));

            _flexL.SetCol("VALUE_EL5", "칼라", 80, true, typeof(string));
            _flexL.SetCol("VALUE_EL6", "인지질", 80, true, typeof(string));


            //_flexL[0, "UNLOADING_HARBOR"] = "하역항";

            _flexL[0, "WT_LOADING"] = "선적항";
            _flexL[0, "LOSS_LOADING"] = "선적항";
            _flexL[0, "RT_LOADING"] = "선적항";

            _flexL[0, "WT_UNLOADING"] = "하역항";
            _flexL[0, "LOSS_UNLOADING"] = "하역항";
            _flexL[0, "RT_UNLOADING"] = "하역항";

            _flexL[0, "WT_REAL"] = "결과";
            _flexL[0, "LOSS_REAL"] = "결과";
            _flexL[0, "RT_REAL"] = "결과";
            _flexL.SetHeaderMerge();



            //_flexL.SetDummyColumn("S");
            //_flexL.EnterKeyAddRow = false;

            _flexL.SetExceptEditCol("");
            _flexL.VerifyAutoDelete = new string[] { "NO_BL" };
            _flexL.VerifyPrimaryKey = new string[] { "NO_BL" };
            _flexL.VerifyNotNull = new string[] { "NO_BL","DT_UNLOADING","CD_SL" };
            //_flexL.VerifyCompare(_flexL.Cols[""], _flexL.Cols[""], OperatorEnum.LessOrEqual);
            //_flexL.VerifyCompare(_flexL.Cols[""], _flexL.Cols[""], OperatorEnum.LessOrEqual);
            //_flexL.VerifyCompare(_flexL.Cols[""], 0, OperatorEnum.Greater);

            // 도움창 예제
            _flexL.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, "CD_SL","NM_SL");

            _flexL.AddRow += new EventHandler(OnToolBarAddButtonClicked);
            _flexL.ValidateEdit += new ValidateEditEventHandler(FlexL_ValidateEdit);
            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(FlexL_BeforeCodeHelp);

            //_flexL.BeforeRowChange += new RangeEventHandler(FlexL_BeforeRowChange);

            _flexL.SettingVersion = "1.0.0.1";
            _flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            //BpContorol 사전 파라미터를 설정하기 위해 BpControl_QueryBefore 이벤트를 등록
            //this.ctx작업장.QueryBefore += new BpQueryHandler(this.BpControl_QueryBefore);
           // this.ctx작업장.QueryAfter += new BpQueryHandler(this.BpControl_QueryAfter);


            btn02.Click += new EventHandler(BtnApply_Click);
            btn01.Click += new EventHandler(BtnExcel_Click);

            btn추가.Click += new EventHandler(BtnAdd_Click);
            btn삭제.Click += new EventHandler(BtnDel_Click);

        }

        protected override void InitPaint()
        {
            base.InitPaint();

            //SetControl str = new SetControl();
            //str.SetCombobox(cbo등록여부, MF.GetCode("CZ_LHE0001", true)); ;  // ModuleHelper에 정의된 코드가 있으면.

            //str.SetCombobox(cbo공장, MF.GetCode("SA_B000050", true));           // ModuleHelper에 정의된 코드가 없으면..
            ////str.SetCombobox(cbo02, MF.GetCode("PU_C000016", true));


            //해당날짜(시작일)
            this.dtp시작일.Mask = base.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp시작일.Text = base.MainFrameInterface.GetStringFirstDayInMonth;
            

            //해당날짜(종료일)
            this.dtp종료일.Mask = base.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp종료일.Text = base.MainFrameInterface.GetStringToday;
        }

        //컨트롤의 적용전 쿼리 ...주로 선행조건
        void BpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (D.GetString(e.ControlName))
                {
                    case "ctx작업장":
                        //e.HelpParam.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                        //e.HelpParam.P65_CODE5 = D.GetString(cbo공장.SelectedValue);
                        //e.HelpParam.P20_CD_WC = D.GetString(ctx작업장.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        //컨트롤의 값의 변경에 따른 후처리
        void BpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {

                switch (((Control)sender).Name)
                {
                    case "ctx작업장":
                        //string str_data = string.Empty;

                        //decimal num2;
                        //DataRow tPSO = BASIC.GetTPSO(e.CodeValue);
                        //this.cbo과세구분.SelectedValue = tPSO["TP_VAT"];
                        //this.cur부가세율.DecimalValue = (num2 = D.GetDecimal(tPSO["RT_VAT"]));
                        ////this._flexH.["RT_VAT", num2);
                        //// this._flexH.["TP_BUSI", tPSO["TP_BUSI"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }



        #endregion

        #region ♪ 메인 버튼     ♬

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;
                string 회사 = Global.MainFrame.LoginInfo.CompanyCode;
                string 시작일 = D.GetString(this.dtp시작일.Text);
                string 종료일 = D.GetString(this.dtp종료일.Text);

                string 거래처 = D.GetString(this.ctx거래처.CodeValue);

                object[] obj = new object[] { 회사, 시작일, 종료일, 거래처 };

                _flexH.Binding = _biz.Search(obj);


                if (!_flexH.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                if (!_flexH.HasNormalRow)
                {
                    ShowMessage("조회된 B/L이 없습니다.");
                    return;
                }


                if (_flexL.Rows.Count == 2)
                {
                    _flexL.Rows.Add();
                    _flexL.Row = _flexL.Rows.Count - 1;

                    _flexL["NO_BL"] = D.GetString(_flexH["NO_BL"]);
                    _flexL["CD_ITEM"] = D.GetString(_flexH["CD_ITEM"]);


                    _flexL.AddFinished();


                    _flexL.Col = _flexL.Cols.Fixed;
                }
                
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete()) return;

                //DataTable dt = _flexL.GetCheckedRows("S");

                //if (dt == null || dt.Rows.Count <= 0)
                //{
                //    ShowMessage(공통메세지.선택된자료가없습니다);
                //    return;
                //}

                _flexL.Redraw = false;

                //for (int r = _flexL.Rows.Count - 1; r >= _flexL.Rows.Fixed; r--)
                //{
                //    if (_flexL.GetCellCheck(r, _flexL.Cols["S"].Index) == CheckEnum.Checked)
                //        _flexL.Rows.Remove(r);
                //}

                _flexL.Rows.Remove(_flexL.Row);
                _flexL.Redraw = true;
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

        #region ♪ 화면 내 버튼  ♬

        void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cbo등록여부.Text == "")
                //{
                //    ShowMessage(공통메세지._은는필수입력항목입니다, "단가유형");
                //    return;
                //}

                if (!_flexH.HasNormalRow) return;

                //if (txt02.DecimalValue == 0) return;

                //int i, j, i_i;

                object obj = this.MainFrameInterface.LoadHelpWindow("P_PU_UM_SUB", new object[] { this.MainFrameInterface });



                //if (((Duzon.Common.Forms.CommonDialog)obj).ShowDialog() == DialogResult.OK)
                //{
                //    object[] dlg = (object[])((Duzon.Common.Forms.IHelpWindow)obj).ReturnValues;

                //    decimal result = 0;
                //    decimal gd_um_item;

                //    // txtRt_Flex의 %값 구하기
                //    decimal re_in_de_num = txt02.DecimalValue / 100;  //증감율/100


                //    string ls_fg_um = cbo04.SelectedValue.ToString(); //단가유형

                //    DataTable gdt_return = new DataTable();

                //    DataRow[] rows = _flexH.DataTable.Select("S ='Y'");

                //    if (rows.Length > 0)
                //    {
                //        for (i = 0; i < rows.Length; i++)
                //            gdt_return.ImportRow(rows[i]);
                //    }

                //    for (i_i = 0; i_i < gdt_return.Rows.Count; i_i++)
                //    {
                //        DataRow[] rows_s = _flexL.DataTable.Select("CD_ITEM= '" + rows[i_i]["CD_ITEM"].ToString() + "'");

                //        if (rows_s.Length > 0)
                //        {
                //            for (j = 0; j < rows_s.Length; j++)
                //            {
                //                string dg_fg_um = rows_s[j]["FG_UM"].ToString();

                //                if (ls_fg_um == dg_fg_um)
                //                {
                //                    gd_um_item = _flexH.CDecimal(rows_s[j]["UM_ITEM"]);

                //                    //단가 + (단가 * 증감율)
                //                    decimal result1 = re_in_de_num * gd_um_item;
                //                    decimal result2 = gd_um_item + result1;

                //                    if (dlg[0].ToString() == "1") //절하(버림)
                //                        result = Math.Floor(result2);
                //                    else if (dlg[0].ToString() == "2") //반올림
                //                        result = Math.Round(result2, 0);
                //                    else if (dlg[0].ToString() == "3") //절상(올림)
                //                        result = Math.Floor(result2 + 1);

                //                    rows_s[j]["UM_ITEM"] = result;
                //                }
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception EX)
            {
                MsgEnd(EX);
            }
        }
        void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string nm_vessel = D.GetString(_flexH["NM_VESSEL"]);

                if (!_flexH.HasNormalRow) return;
                if ((_flexL2.Rows.Count == 2) && (D.GetString(_flexH["NM_VESSEL"]) != ""))
                {
                    _flexL2.Rows.Add();
                    _flexL2.Row = _flexL2.Rows.Count - 1;

                    _flexL2["NM_VESSEL"] = D.GetString(_flexH["NM_VESSEL"]);
                    _flexL2.AddFinished();
                    _flexL2.Col = _flexL2.Cols.Fixed;
                }

  

            }
            catch (Exception EX)
            {
                MsgEnd(EX);
            }
        }

        void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {

                if (!_flexH.HasNormalRow) return;
                if (_flexL2.Rows.Count == 3)
                {
                    _flexL2.Rows.Remove(_flexL2.Row);
                    _flexL2.Redraw = true;
                }

            }
            catch (Exception EX)
            {
                MsgEnd(EX);
            }
        }
        private void BtnExcel_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region ♪ 저장 관련     ♬

        protected override bool SaveData()
        {
            // MainGrids 에 설정된 모든 그리드에 무결성 검사 수행
            if (!((base.SaveData() && this.Verify())))
            {
                return false;
            }
            this._biz.Save(this._flexL.GetChanges(), this._flexL2.GetChanges());

            this._flexL.AcceptChanges();
            this._flexL2.AcceptChanges();
            return true;
        }

        #endregion

        #region ♪ 그리드 이벤트 ♬

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                // 해당페이지에 속한 이벤트로서 해당페이지에 바인딩된 데이터가 변경되면 발생한다.
                // 주로 버튼들의 활성화 여부를 제어한다.
                
                //DataTable dtL2;
                //dtL2 = _flexL2.GetChanges();

                if (_flexL2.IsDataChanged)
                {
                    base.ToolBarSaveButtonEnabled = true;
                    
                    //_flexL2.IsDataChanged = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        //도움창 적용 전 그리드 변경
        private void FlexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID == HelpID.P_MA_SL_SUB)
                {

                    if (Duzon.ERPU.MF.Common.Common.IsEmpty(this._flexH, "CD_PLANT", "공장"))
                    {
                        //this._flexD.set_Col(this._flexD.get_Cols().get_Item("CD_PLANT").get_Index());


                        e.Cancel = true;
                    }


                    //else if (Duzon.ERPU.MF.Common.Common.IsEmpty(this._flexH, "CD_EXCH", "환종"))
                    //{
                    //    this.cbo환종.Focus();
                    //    e.Cancel = true;
                    //}
                    else
                    {
                        e.Parameter.P09_CD_PLANT = D.GetString(this._flexH["CD_PLANT"]);
                    }
                }
            }
            catch (Exception exception)
            {
                base.MsgEnd(exception);
            }
        }

        void FlexH_AfterRowChange(object sender, RangeEventArgs e)
        {

            try
            {
                DataTable dt = null;
                DataTable dt2 = null;
                //string Key = D.GetString(_flexH["NO_SO"]);
                //string Filter = "NO_SO = '" + Key + "'";
                //string 조회조건 = D.GetString(cbo04.SelectedValue);
                //String Key = D.GetString(_flexH["NO_SO"]);

                //string 공장 = D.GetString(cbo공장.SelectedValue);
                string NO_BL = D.GetString(_flexH["NO_BL"]);

                string NM_VESSEL = D.GetString(_flexH["NM_VESSEL"]);

                string Filter = "NO_BL = '" + NO_BL + "'";

                string Filter2 = "NM_VESSEL = '" + NM_VESSEL + "'";

                //string Filter = "";

                if (_flexH.DetailQueryNeed)
                {
                    object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_BL };//, this.dtp시작일.Text, this.dtp종료일.Text };
                    dt = _biz.SearchDetail(objArray);

                   
                }


                _flexL2.Binding = null;

                object[] objArray2 = new object[] { Global.MainFrame.LoginInfo.CompanyCode, NM_VESSEL };//, this.dtp시작일.Text, this.dtp종료일.Text };
                dt2 = _biz.SearchDetail2(objArray2);
               

                _flexL.BindingAdd(dt, Filter);
                _flexL2.BindingAdd(dt2, Filter2);
                //_flexL.Binding = dt;



                _flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        void FlexL_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {

                string oldValue = D.GetString(_flexL.GetData(e.Row, e.Col));//수정 전에 입력되어있던 값
                string newValue = _flexL.EditData;//수정한 값

                if (oldValue == newValue) return;

                switch (_flexL.Cols[e.Col].Name)
                {
                    case "QT_MAN":
                        _flexL[e.Row, "MH_TOTAL"] = this._flexL.CDecimal(newValue) * D.GetDecimal(_flexL.GetData(e.Row, "TM_LABOR"));
                        break;

                    case "TM_LABOR":
                        _flexL[e.Row, "MH_TOTAL"] = this._flexL.CDecimal(newValue) * D.GetDecimal(_flexL.GetData(e.Row, "QT_MAN"));
                        break;

                }



                //string OldValue = D.GetString(_flexL.GetData(e.Row, e.Col));    // 수정되기 전에 입력되어있던 데이터
                //string NewValue = _flexL.EditData;  // 수정한 데이터

                //if (OldValue == NewValue) return;

                //string colName = _flexL.Cols[e.Col].Name;
                //switch (colName)
                //{
                //    case "QT_MAN":
                //        _flexL[e.Row, "MH_TOTAL"] = D.GetDecimal(_flexL.GetData(e.Row, "QT_MAN")) * D.GetDecimal(_flexL.GetData(e.Row, "TM_LABOR"));


                //        break;
                //    case "TM_LABOR":
                //        //_flexL[e.Row, "MH_TOTAL"] = D.GetDecimal(_flexL.GetData(e.Row, "QT_MAN")) * D.GetDecimal(_flexL.GetData(e.Row, "TM_LABOR"));

                //        break;


                //}






            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

    
        private void FlexL_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                Dass.FlexGrid.FlexGrid grid = sender as Dass.FlexGrid.FlexGrid;
                if ((grid != null) && (e.NewRange.r1 == grid.Rows.Fixed))
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exception)
            {
                base.MsgEnd(exception);
            }
        }

 

 



        #endregion

        #region ♪ 기타 이벤트   ♬

        #endregion

        #region ♪ 기타 메서드   ♬

        #endregion

        #region ♪ 속성          ♬

        #endregion
    }
}