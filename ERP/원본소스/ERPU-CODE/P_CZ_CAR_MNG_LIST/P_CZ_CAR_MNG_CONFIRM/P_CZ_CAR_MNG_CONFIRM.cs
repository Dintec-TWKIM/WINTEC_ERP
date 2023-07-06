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

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace cz
{
    /// <summary>
    /// ================================================
    /// AUTHOR      : 
    /// CREATE DATE : 
    /// 
    /// MODULE      : 구매관리
    /// SYSTEM      : 
    /// SUBSYSTEM   : 
    /// PAGE        : 
    /// PROJECT     : 
    /// DESCRIPTION : 
    /// ================================================ 
    /// CHANGE HISTORY
    /// v1.0 :
    /// ================================================
    /// </summary>

    public partial class P_CZ_CAR_MNG_CONFIRM : PageBase
    {
        P_CZ_CAR_MNG_CONFIRM_BIZ _biz;

        public P_CZ_CAR_MNG_CONFIRM()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
            DataChanged += new EventHandler(Page_DataChanged);

        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_CAR_MNG_CONFIRM_BIZ();

            InitGrid();
            InitEvent();

            MainGrids = new FlexGrid[] { _flex };


        }

        private void InitGrid()
        {
            
            _flex.BeginSetting(1, 1, false);

            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CD_COMPANY", "회사", 100, true);
            _flex.SetCol("CD_SL", "창고", 100, true);
            _flex.SetCol("NM_SL", "창고명", 100, false);
            _flex.SetCol("NM_PLANT", "공장명", 100, true);
            _flex.SetCol("CONFIRM_QTY", "확정량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("DT_GIR", "납품일자", 100, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CAR_NO", "차량번호", 150, false);
            _flex.SetCol("CD_ITEM", "품번", 100, false);
            _flex.SetCol("NM_ITEM", "품명", 250, false);
            _flex.SetCol("REAL_LOADING_PLACE", "상차지", 250, false);
            _flex.SetCol("ALIGHT_PLACE", "하차지", 250, false);
            _flex.SetCol("SHIPPER", "화주코드", 60, false);
            _flex.SetCol("LN_PARTNER", "화주", 150, false);
            _flex.SetCol("NO_GIR", "의뢰번호", 100, false);
            _flex.SetCol("NO_SO", "수주번호", false);
            _flex.SetCol("NO_IO", "출하번호", false);
            _flex.SetCol("DEPTCODE", "부서번호", false);
            _flex.SetCol("DT_GI", "출하일자", false);
            _flex.SetCol("NM_EMP", "담당자", false);
            _flex.SetCol("TP_BUSI", "거래구분", false);
            _flex.SetCol("DC_RMK", "비고", false);
            _flex.SetCol("DC_RMK2", "비고2", false);
            _flex.SetCol("DC_RMK3", "비고3", false);
            _flex.SetCol("CURRENT_COMPANY", "현재 회사", false);
            _flex.SetCol("NO_IOLINE", "수주항번", false);
            _flex.SetCol("SEQ_SO", "라인항번",false);
            _flex.SetCol("CAR_SEQ", "순번", false);
            _flex.SetCol("LINE", "라인", false);
            

            //수정대상000000000
            _flex.SetCol("REQ_PRICE", "청구단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("REQ_UNIT_PRICE", "청구 대당단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("PROVIDE_PRICE", "지급단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT_PRICE", "지급 대당단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("ESTIMATE_PRICE", "계근비", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FLAG", "처리상황", 70, false);

            _flex.SetCol("FREIGHT_CHARGE_A", "운임A", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FREIGHT_CHARGE_B", "운임B", 100, 8, true, typeof(decimal), FormatTpType.MONEY);

            
            _flex.SetCol("LOADING_QTY", "상차량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("ALIGHT_QTY", "하차량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("CD_PLANT", "공장", 100, 8, true, typeof(string));


            _flex.Cols["NM_PLANT"].TextAlign = TextAlignEnum.CenterCenter;



            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            _flex.SetExceptEditCol("");
            _flex.VerifyAutoDelete = new string[] { "S" };
            _flex.VerifyPrimaryKey = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };
            _flex.VerifyNotNull = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };


            this._flex.SetDataMap("FLAG", MA.GetCode("CZ_Z000012"), "CODE", "NAME");

            // 도움창 ( 도움창이 작동할 GRID 컬럼, 도움창명, 도움창유형, 리턴 받을 GRID 컬럼, 도움창상 받아올 매핑 컬럼)
            //_flex.SetCodeHelpCol("NM_PLANT", HelpID.P_MA_PLANT_SUB, ShowHelpEnum.Always, new string[] { "CD_PLANT", "NM_PLANT" }, new string[] { "CD_PLANT", "NM_PLANT" });
            _flex.SetCodeHelpCol("CD_SL", "H_CZ_HELP_SL", ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });

            //_flex.AddRow += new EventHandler(OnToolBarAddButtonClicked);

            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);

            _flex.ValidateEdit += new ValidateEditEventHandler(Flex_ValidateEdit);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Flex_BeforeCodeHelp);

            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);


        }

        private void InitEvent()
        {
            // 컨트롤 이벤트
            //btn_출하.Click += new EventHandler(btn_출하_Click);
            //btn_취소.Click += new EventHandler(btn_취소_Click);

        }

        protected override void InitPaint()
        {
            base.InitPaint();

            //dtp월.Mask = dtpTO.Mask = GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);

            dtpFROM.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtpTO.Text = Global.MainFrame.GetStringToday;
            dtpOut.Text = Global.MainFrame.GetStringToday;
            bpc담당자.CodeValue = Global.MainFrame.LoginInfo.UserID;

            //SetControl str = new SetControl();
            //str.SetCombobox(cbo01, MF.GetCode(MF.코드.생산.공정경로번호, true));  // ModuleHelper에 정의된 코드가 있으면..
            //str.SetCombobox(cbo공장, MF.GetCode("MA_PLANT", true));           // ModuleHelper에 정의된 코드가 없으면..
        }

        #endregion

        #region ♪ 메인 버튼     ♬

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false; // || !DropDownComboBoxCheck || !DatePickerCheck) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                string 회사 = Global.MainFrame.LoginInfo.CompanyCode;
                //string 거래처 = D.GetString(bpc화주.Text);
                
                string 시작일 = D.GetString(dtpFROM.Text);
                string 종료일 = D.GetString(dtpTO.Text);
                string 화주 = D.GetString(txt화주.Text);
                string 담당자 = D.GetString(bpc담당자.CodeValue);
                string 공장명 = D.GetString(bpc공장명.CodeValue);
                string 품목 = "";
                string 시작일2 = D.GetString(dtpFROM2.Text);
                string 종료일2 = D.GetString(dtpTO2.Text);

                object[] obj = new object[] { 시작일, 종료일, 화주, 담당자, 공장명, 품목, 시작일2, 종료일2 };



                _flex.Binding = _biz.Search(obj);


                //SSInfo defaultSS = new SSInfo();
                //defaultSS.VisibleColumns = new string[] { };
                //defaultSS.GroupColumns = new string[] { };
                //defaultSS.TotalColumns = new string[] { };
                //defaultSS.AccColumns = new string[] { };
                //defaultSS.CanGrandTotal = true;

                //_flex.ExecuteSubTotal(defaultSS);

                if (!_flex.HasNormalRow)
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
                //if (!BeforeAdd()) return;

                //if (!_flexH.HasNormalRow)
                //{
                //    ShowMessage("조회된 수주내역이 없습니다.");
                //    return;
                //}


                //// 라인 최대항번 구하기
                //decimal maxNoLine = _flexL.GetMaxValue("SQ_ACT");   // SQ_ACT 이라는 컬럼에서 최대값 가져옴
                //maxNoLine++;

                //_flex.Rows.Add();
                //_flex.Row = _flex.Rows.Count - 1;


                ////_flex["CD_PLANT"] = cbo공장.SelectedValue.ToString();
                ////_flex["YYYYMM"] = dtp월.Text;

                //_flex.AddFinished();


                //_flex.Col = _flex.Cols.Fixed;
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

                _flex.Redraw = false;

                //for (int r = _flexL.Rows.Count - 1; r >= _flexL.Rows.Fixed; r--)
                //{
                //    if (_flexL.GetCellCheck(r, _flexL.Cols["S"].Index) == CheckEnum.Checked)
                //        _flexL.Rows.Remove(r);
                //}

                _flex.Rows.Remove(_flex.Row);
                _flex.Redraw = true;
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
                

                if (MsgAndSave(PageActionMode.Save))
                    //ShowMessage(PageResultMode.SaveGood);
                    ShowMessage("자료가 정상처리되었습니다");

                string 시작일 = D.GetString(dtpFROM.Text);
                string 종료일 = D.GetString(dtpTO.Text);
                string 화주 = D.GetString(txt화주.Text);
                string 담당자 = D.GetString(bpc담당자.CodeValue);
                string 공장명 = D.GetString(bpc공장명.CodeValue);
                string 품목 = "";
                string 시작일2 = D.GetString(dtpFROM2.Text);
                string 종료일2 = D.GetString(dtpTO2.Text);

                object[] obj = new object[] { 시작일, 종료일, 화주, 담당자, 공장명, 품목, 시작일2, 종료일2 };
                _biz.Search(obj);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 화면 내 버튼  ♬

        //void btn_출하_Click(object sender, EventArgs e)
        //{
        //    DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

        //    if (dr == null || dr.Length == 0)
        //    {
        //        ShowMessage(공통메세지.선택된자료가없습니다);
        //        return;
        //    }

        //    foreach (DataRow row in dr)
        //    {

        //        string company = base.LoginInfo.CompanyCode;

        //        _flex["NO_SO"] = (string)base.GetSeq(company, "SA", "02", Global.MainFrame.GetStringYearMonth);
        //        _flex["NO_IO"] = (string)base.GetSeq(company, "SA", "07", Global.MainFrame.GetStringYearMonth);
        //        _flex["DEPTCODE"] = Global.MainFrame.LoginInfo.DeptCode;
        //        _flex["DT_GI"] = dtpOut.Text;
        //        _flex["NM_EMP"] = bpc담당자.Text;
        //        _flex["TP_BUSI"] = "001";
        //        _flex["CURRENT_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;

        //        DataTable dt = _flex.GetChanges();
        //        _biz.Save(dt);
        //        _flex.AcceptChanges();
        //    }
        //}


        #endregion

        #region ♪ 저장 관련     ♬

        protected override bool SaveData()
        {

            if (!base.SaveData() || !Verify()) return false;

            DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

            if (dr == null || dr.Length == 0)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return false;
            }

            foreach (DataRow row in dr)
            {

                if (_flex["CD_SL"] == "")
                {
                    ShowMessage("창고는 필수입력항목입니다");
                    return false;
                }

                string company = base.LoginInfo.CompanyCode;

                _flex["NO_SO"] = (string)base.GetSeq(company, "SA", "02", Global.MainFrame.GetStringYearMonth);
                _flex["NO_IO"] = (string)base.GetSeq(company, "SA", "07", Global.MainFrame.GetStringYearMonth);
                _flex["DEPTCODE"] = Global.MainFrame.LoginInfo.DeptCode;
                _flex["DT_GI"] = dtpOut.Text;
                _flex["NM_EMP"] = bpc담당자.CodeValue;
                _flex["TP_BUSI"] = "001";
                _flex["CURRENT_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                

                DataTable dt = _flex.GetChanges();
                _biz.Save(dt);
                _flex.AcceptChanges();

                if (MsgAndSave(PageActionMode.Save))
                    //ShowMessage(PageResultMode.SaveGood);
                    ShowMessage("자료가 정상처리되었습니다");

                

            }

            return false;
        }

        #endregion


        #region ♪ 기타 메서드   ♬
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            //try
            //{
            //    if (e.StartEditCancel)
            //    {
            //        //e.QueryCancel = true;

            //        return;
            //    }

            //    //운송비 코드
            //    e.Parameter.P41_CD_FIELD1 = "CZ_SHIN_04";

            //}
            //catch (Exception ex)
            //{
            //    this.MsgEnd(ex);
            //}

            try
            {
                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_SL":
                        
                        string CD_COMPANY = D.GetString(_flex["CD_COMPANY"]);
                        string CD_PLANT = D.GetString(_flex["CD_PLANT"]);
                        string CD_SL = D.GetString(_flex["CD_SL"]);
                        string NM_SL = string.Empty;

                        e.Parameter.UserParams = "창고;H_CZ_SL;" + CD_COMPANY + ";" +CD_PLANT + ";" + CD_SL;
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }




        }
        #endregion



        #region ♪ 그리드 이벤트 ♬
        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                // 해당페이지에 속한 이벤트로서 해당페이지에 바인딩된 데이터가 변경되면 발생한다.
                // 주로 버튼들의 활성화 여부를 제어한다.
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        void Flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                string newValue = ((FlexGrid)sender).EditData;

                if (oldValue.ToUpper() == newValue.ToUpper()) return;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "UM_MATL":  // 재료비
                        this._flex["STDUM_ITEM"] = D.GetDecimal(newValue) + D.GetDecimal(this._flex["UM_PR"]) + D.GetDecimal(this._flex["UM_SU"]);
                        break;
                    case "UM_PR":    // 가공비
                        this._flex["STDUM_ITEM"] = D.GetDecimal(newValue) + D.GetDecimal(this._flex["UM_MATL"]) + D.GetDecimal(this._flex["UM_SU"]);
                        break;
                    case "UM_SU":    // 외주비
                        this._flex["STDUM_ITEM"] = D.GetDecimal(newValue) + D.GetDecimal(this._flex["UM_PR"]) + D.GetDecimal(this._flex["UM_MATL"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        //도움창 적용 전 그리드 변경
        private void Flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID == HelpID.P_MA_PITEM_SUB)
                {

                    if (Duzon.ERPU.MF.Common.Common.IsEmpty(this._flex, "CD_PLANT", "공장"))
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
                        e.Parameter.P09_CD_PLANT = D.GetString(this._flex["CD_PLANT"]);
                    }
                }
            }
            catch (Exception exception)
            {
                base.MsgEnd(exception);
            }
        }



        #endregion

        #region ♪ 기타 이벤트   ♬

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                //bpctb02.CodeValue = bpctb02.CodeName = "";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                // 도움창 HelpID 에 따라..
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB: // 예제
                        // 체크할 거 체크해준다.
                        //e.HelpParam.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                        //e.HelpParam.ResultMode = ResultMode.SlowMode;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_WC_SUB:
                        //bpctb02.CodeValue = bpctb02.CodeName = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 기타 메서드   ♬

        #endregion

        #region ♪ 속성          ♬

        //bool DropDownComboBoxCheck { get { return !Checker.IsEmpty(cbo공장, DD("")); } }
        //bool DatePickerCheck { get { return Checker.IsValid(dtp월, true, DD("")); } }

        #endregion

    }
}