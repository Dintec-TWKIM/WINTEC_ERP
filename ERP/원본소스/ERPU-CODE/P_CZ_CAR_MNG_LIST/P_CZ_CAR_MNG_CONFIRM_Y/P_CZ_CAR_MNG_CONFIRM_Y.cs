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

    public partial class P_CZ_CAR_MNG_CONFIRM_Y : PageBase
    {
        P_CZ_CAR_MNG_CONFIRM_Y_BIZ _biz;

        public P_CZ_CAR_MNG_CONFIRM_Y()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
            DataChanged += new EventHandler(Page_DataChanged);

        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_CAR_MNG_CONFIRM_Y_BIZ();

            InitGrid();
            InitEvent();

            MainGrids = new FlexGrid[] { _flex };


        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);

            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            //_flex.SetCol("DT_GI", "수불번호", 100, false);
            _flex.SetCol("NO_IO", "출하번호", 100, false);
            _flex.SetCol("DT_IO", "출하일자", 100, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_COMPANY", "회사", 100, true);
            _flex.SetCol("CD_SL", "창고", 100, true);
            _flex.SetCol("NM_SL", "창고명", 100, false);
            _flex.SetCol("NM_PLANT", "공장명", 100, true);
            _flex.SetCol("CD_CC", "CC", 100, 8, true, typeof(string));
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
            _flex.SetCol("REQ_PRICE", "청구단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("REQ_UNIT_PRICE", "청구 대당단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("PROVIDE_PRICE", "지급단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT_PRICE", "지급 대당단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("ESTIMATE_PRICE", "계근비", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FREIGHT_CHARGE_A", "운임A", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FREIGHT_CHARGE_B", "운임B", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("LOADING_QTY", "상차량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("ALIGHT_QTY", "하차량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("CD_PLANT", "공장", 100, 8, true, typeof(string));
            _flex.SetCol("FLAG", "처리상황", false);
            _flex.SetCol("CD_BIZAREA", "사업장", false);
            _flex.SetCol("NO_BIZAREA", "사업장코드", false);
            _flex.SetCol("VAT_TAX", "부가세", false);
            _flex.SetCol("DT_PROCESS", "처리일시", false);
            _flex.SetCol("CD_DEPT", "부서", false);
            _flex.SetCol("NO_EMP", "담당자", false);
            _flex.SetCol("SUM_EX", "총합", false);
            _flex.SetCol("DC_RMK", "비고", false);
            _flex.SetCol("CD_DOCU", "전표번호", false);
            _flex.SetCol("NO_IOLINE", "출하번호라인", false);
            _flex.SetCol("CD_ITEM", "품목코드", false);
            _flex.SetCol("CD_PURGRP", "구매그룹", false);
            _flex.SetCol("QT_RCV_CLS", "수량", false);
            _flex.SetCol("AM_CLS", "금액", false);
            _flex.SetCol("NO_LINE", "금액", false);

            _flex.Cols["CONFIRM_QTY"].Format = "0.00";
            _flex.Cols["CONFIRM_QTY"].TextAlign = TextAlignEnum.RightCenter;

            _flex.Cols["REQ_PRICE"].Format = "0.00";
            _flex.Cols["REQ_PRICE"].TextAlign = TextAlignEnum.RightCenter;


            _flex.Cols["NM_PLANT"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            _flex.SetExceptEditCol("");
            _flex.VerifyAutoDelete = new string[] { "S" };
            _flex.VerifyPrimaryKey = new string[] {"CAR_SEQ" };
            _flex.VerifyNotNull = new string[] { "CAR_SEQ","CD_CC" };


            this._flex.SetDataMap("FLAG", MA.GetCode("CZ_Z000012"), "CODE", "NAME");

            // 도움창 ( 도움창이 작동할 GRID 컬럼, 도움창명, 도움창유형, 리턴 받을 GRID 컬럼, 도움창상 받아올 매핑 컬럼)
            _flex.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "CD_CC" }, new string[] { "CD_CC", "CD_CC" });
            //_flex.SetCodeHelpCol("CD_SL", "H_CZ_HELP_SL", ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });

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
            btn_처리.Click += new EventHandler(btn_처리_Click);

        }

        protected override void InitPaint()
        {
            base.InitPaint();

            dtpFROM.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtpTO.Text = Global.MainFrame.GetStringToday;
            bpC작성자.CodeName = Global.MainFrame.LoginInfo.UserID;
            dtpOut매출일자.Text = Global.MainFrame.GetStringToday;
            bpc사업장.CodeValue = Global.MainFrame.LoginInfo.BizAreaCode;
            bpc부가세사업장.CodeName = Global.MainFrame.LoginInfo.BizAreaName;
        }

        #endregion

        #region ♪ 메인 버튼    

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false; // || !DropDownComboBoxCheck || !DatePickerCheck) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {

                this.seachGrid();
                if (!_flex.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public void seachGrid()
        {
            if (!BeforeSearch()) return;

            string 회사 = Global.MainFrame.LoginInfo.CompanyCode;
            //string 거래처 = D.GetString(bpc화주.Text);

            string 시작일 = D.GetString(dtpFROM.Text);
            string 종료일 = D.GetString(dtpTO.Text);
            string 화주 = D.GetString(txt화주.Text);
            string 공장명 = D.GetString(bpc부가세사업장.CodeValue);

            object[] obj = new object[] { 시작일, 종료일, 화주, 공장명 };



            _flex.Binding = _biz.Search(obj);
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
            if (!base.SaveData() || !Verify()) return;

            DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

            if (dr == null || dr.Length == 0)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return;
            }

            int i = 0;

            foreach (DataRow row in dr)
            {

                if (bpc사업장.CodeValue == "")
                {
                    ShowMessage("사업자코드는 필수입니다.");
                    return;
                }

                if (dtpOut매출일자.Text == "")
                {
                    ShowMessage("매출일자는 필수입니다.");
                    return;
                }



                string company = base.LoginInfo.CompanyCode;

                string noIv = (string)base.GetSeq(company, "FI", "01", Global.MainFrame.GetStringYearMonth);



                _flex["NO_IV"] = noIv;
                _flex["NO_IOLINE"] = i + 1;
                _flex["CD_BIZAREA"] = bpc사업장.CodeValue;
                _flex["NO_BIZAREA"] = bpc사업장.CodeName;
                _flex["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode;
                _flex["DT_PROCESS"] = dtpOut매출일자.Text;
                _flex["NO_EMP"] = bpC작성자.CodeName;
                _flex["SUM_EX"] = 0;
                //_flex["VAT_TAX"] = 0;
                _flex["DT_PROCESS"] = dtpOut매출일자.Text;
                //_flex["QT_RCV_CLS"] = _flex["CONFIRM_QTY"];
                //_flex["AM_CLS"] = _flex["REQ_PRICE"];
                _flex["CD_DOCU"] = (string)base.GetSeq(company, "FI", "01", Global.MainFrame.GetStringYearMonth);



                DataTable dt = _flex.GetChanges();
                _biz.Save(dt);
                _flex.AcceptChanges();

                i++;

            }
            if (MsgAndSave(PageActionMode.Save))
                //ShowMessage(PageResultMode.SaveGood);
                ShowMessage("자료가 정상처리되었습니다");
            this.seachGrid();
        }

        #endregion

        #region ♪ 화면 내 버튼  ♬

        void btn_처리_Click(object sender, EventArgs e)
        {
            if (!base.SaveData() || !Verify()) return;

            DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

            if (dr == null || dr.Length == 0)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return;
            }

            int i= 0;

            foreach (DataRow row in dr)
            {

                if (bpc사업장.CodeValue == "")
                {
                    ShowMessage("사업자코드는 필수입니다.");
                    return;
                }

                if (dtpOut매출일자.Text == "")
                {
                    ShowMessage("매출일자는 필수입니다.");
                    return;
                }



                string company = base.LoginInfo.CompanyCode;

                string noIv = (string)base.GetSeq(company, "FI", "01", Global.MainFrame.GetStringYearMonth);


                
                _flex["NO_IV"] = noIv;
                _flex["NO_IOLINE"] = i + 1;
                _flex["CD_BIZAREA"] = bpc사업장.CodeValue;
                _flex["NO_BIZAREA"] = bpc사업장.CodeName;
                _flex["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode;
                _flex["DT_PROCESS"] = dtpOut매출일자.Text;
                _flex["NO_EMP"] = bpC작성자.CodeName;
                _flex["SUM_EX"] = 0;
                //_flex["VAT_TAX"] = 0;
                _flex["DT_PROCESS"] = dtpOut매출일자.Text;
                //_flex["QT_RCV_CLS"] = _flex["CONFIRM_QTY"];
                //_flex["AM_CLS"] = _flex["REQ_PRICE"];
                _flex["CD_DOCU"] = (string)base.GetSeq(company, "FI", "01", Global.MainFrame.GetStringYearMonth);

                
                
                DataTable dt = _flex.GetChanges();
                _biz.Save(dt);
                _flex.AcceptChanges();

                i++;

            }
            if (MsgAndSave(PageActionMode.Save))
                //ShowMessage(PageResultMode.SaveGood);
                ShowMessage("자료가 정상처리되었습니다");
                this.seachGrid();
        }


        #endregion

        #region ♪ 저장 관련     ♬

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