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

    public partial class P_CZ_CAR_MNG_CONFIRM_BILL : PageBase
    {
        P_CZ_CAR_MNG_CONFIRM_BILL_BIZ _biz;

        public P_CZ_CAR_MNG_CONFIRM_BILL()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
            DataChanged += new EventHandler(Page_DataChanged);

        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_CAR_MNG_CONFIRM_BILL_BIZ();

            InitGrid();
            InitEvent();

            MainGrids = new FlexGrid[] { _flex };


        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);

            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flex.SetCol("NO_IV", "전표번호", 100, false);
            _flex.SetCol("CD_BIZAREA", "사업장", 100, false);
            _flex.SetCol("NM_BIZAREA", "사업장명", 100, true);
            _flex.SetCol("CD_ITEM", "픔목", 100, true);
            _flex.SetCol("NM_ITEM", "품목명", 100, false);
            _flex.SetCol("NM_PLANT", "공장명", 100, true);
            _flex.SetCol("QT_RCV_CLS", "수량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("AM_CLS", "금액", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("DT_TAX", "매출등록일자", 100, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_COMPANY", "회사번호", 100, false);
            _flex.SetCol("NM_COMPANY", "회사명", 100, false);
            _flex.SetCol("CD_CC", "CC", 50, false);
            _flex.SetCol("NM_CC", "CC명", 100, false);
            _flex.SetCol("OPT_CD_SVC", "OPT_CD_SVC", 50, false);
            _flex.SetCol("OPT_ITEM_RPT_GUBUN", "OPT_ITEM_RPT_GUBUN", false);
            _flex.SetCol("OPT_ITEM_RPT_TEXT", "OPT_ITEM_RPT_TEXT", false);
            _flex.SetCol("OPT_ITEM_NM_GUBUN", "OPT_ITEM_NM_GUBUN", false);
            _flex.SetCol("OPT_SELL_DAM_GUBUN", "OPT_SELL_DAM_GUBUN", false);
            _flex.SetCol("OPT_SELL_DAM_NM", "OPT_SELL_DAM_NM", false);
            _flex.SetCol("OPT_SELL_DAM_EMAIL", "OPT_SELL_DAM_EMAIL", false);
            _flex.SetCol("OPT_SELL_DAM_MOBIL", "OPT_SELL_DAM_MOBIL", false);

            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            _flex.SetExceptEditCol("");
            _flex.VerifyAutoDelete = new string[] { "S" };
            _flex.VerifyPrimaryKey = new string[] { "NO_IV", "CD_COMPANY" };
            _flex.VerifyNotNull = new string[] { "NO_IV", "CD_COMPANY" };




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

        }

        protected override void InitPaint()
        {
            base.InitPaint();

            dtpTAXFROM.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtpTAXTO.Text = Global.MainFrame.GetStringToday;
            bpC작성자.CodeValue = Global.MainFrame.LoginInfo.UserID;
            bpc사업장.CodeValue = Global.MainFrame.LoginInfo.BizAreaCode;
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
            string company = Global.MainFrame.LoginInfo.CompanyCode;


            string 작성자 = D.GetString(bpC작성자.CodeValue);
            string 시작일 = D.GetString(dtpTAXFROM.Text);
            string 종료일 = D.GetString(dtpTAXTO.Text);
            string 사업장 = D.GetString(bpc사업장.CodeValue);
            string 품목 = D.GetString(bpc품목.CodeValue);
            string 회사 = company;

            object[] obj = new object[] { 작성자, 시작일, 종료일, 사업장, 품목, 회사};



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

                _flex.Rows.Remove(_flex.Row);
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
                if (!base.SaveData() || !Verify()) return;

                //DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

                //if (dr == null || dr.Length == 0)
                //{
                //    ShowMessage(공통메세지.선택된자료가없습니다);
                //    return;
                //}

                //foreach (DataRow row in dr)
                //{



                //    DataTable dt = _flex.GetChanges();
                //    _biz.Save(dt);
                //    _flex.AcceptChanges();

                //}

                string company = Global.MainFrame.LoginInfo.CompanyCode;
                //string noDocu = (string)base.GetSeq(company, "FI", "01", Global.MainFrame.GetStringYearMonth);

                //_flex["NO_IV"] = noDocu;
                _flex["OPT_SELL_DAM_NM"] = bpC작성자.CodeValue;

                DataTable dt = _flex.GetChanges();

                _biz.Save(dt);
                _flex.AcceptChanges();

                if (MsgAndSave(PageActionMode.Save))
                    //ShowMessage(PageResultMode.SaveGood);
                    ShowMessage("자료가 정상처리되었습니다");

                string 작성자 = D.GetString(bpC작성자.CodeValue);
                string 시작일 = D.GetString(dtpTAXFROM.Text);
                string 종료일 = D.GetString(dtpTAXTO.Text);
                string 사업장 = D.GetString(bpc사업장.CodeValue);
                string 품목 = D.GetString(bpc품목.CodeValue);
                string 회사 = company;

                object[] obj = new object[] { 작성자, 시작일, 종료일, 사업장, 품목, 회사 };
                _biz.Search(obj);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 화면 내 버튼  ♬


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