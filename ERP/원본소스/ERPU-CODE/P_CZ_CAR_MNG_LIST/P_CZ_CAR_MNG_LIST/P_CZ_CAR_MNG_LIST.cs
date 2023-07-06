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

    public partial class P_CZ_CAR_MNG_LIST : PageBase
    {
        P_CZ_PARTNER_INTRT_BIZ _biz;

        public P_CZ_CAR_MNG_LIST()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
            DataChanged += new EventHandler(Page_DataChanged);

        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_PARTNER_INTRT_BIZ();

            InitGrid();
            InitEvent();

            MainGrids = new FlexGrid[] { _flex };


        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            //_flex.SetCol("CONFIRM_YN", "S", 20, true, CheckTypeEnum.Y_N);
            _flex.SetCol("FLAG", "처리상황", 50, false);
            _flex.SetCol("DT_GIR", "납품일자", 100, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CAR_NO", "차량번호", 50, false);
            _flex.SetCol("CD_ITEM", "품번", 150, false);
            _flex.SetCol("NM_ITEM", "품명", 250, false);
            _flex.SetCol("REAL_LOADING_PLACE", "상차지", 250, false);
            _flex.SetCol("ALIGHT_PLACE", "하차지", 250, false);
            _flex.SetCol("SHIPPER", "화주코드", 60, false);
            _flex.SetCol("LN_PARTNER", "화주", 150, false);

            //수정대상000000000
            
            _flex.SetCol("REQ_PRICE", "청구단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("REQ_UNIT_PRICE", "청구 대당단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("PROVIDE_PRICE", "지급단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT_PRICE", "지급 대당단가", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("ESTIMATE_PRICE", "계근비", 100, 8, true, typeof(decimal), FormatTpType.MONEY);


            _flex.SetCol("FREIGHT_CHARGE_A", "운임A", 100, 8, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FREIGHT_CHARGE_B", "운임B", 100, 8, true, typeof(decimal), FormatTpType.MONEY);

            _flex.SetCol("CONFIRM_QTY", "확정량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("LOADING_QTY", "상차량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("ALIGHT_QTY", "하차량", 100, 8, true, typeof(decimal), FormatTpType.QUANTITY);



            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            _flex.SetExceptEditCol("");
            _flex.VerifyAutoDelete = new string[] { "CONFIRM_YN" };
            _flex.VerifyPrimaryKey = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };
            _flex.VerifyNotNull = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };


            this._flex.SetDataMap("FLAG", MA.GetCode("CZ_Z000012"), "CODE", "NAME");

            // 도움창 예제
            _flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

            //_flex.AddRow += new EventHandler(OnToolBarAddButtonClicked);
            _flex.ValidateEdit += new ValidateEditEventHandler(Flex_ValidateEdit);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Flex_BeforeCodeHelp);

            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);


        }

        private void InitEvent()
        {
            // 컨트롤 이벤트
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            //dtp월.Mask = dtpTO.Mask = GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);

            dtpFROM.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtpTO.Text = Global.MainFrame.GetStringToday;

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
                string 차량번호 = D.GetString(txt차량번호.Text);
                string 품목 = D.GetString(bpc품목.Text);
                string 시작일2 = D.GetString(dtpFROM2.Text);
                string 종료일2 = D.GetString(dtpTO2.Text);
                string cd_company = Global.MainFrame.LoginInfo.CompanyCode;

                object[] obj = new object[] { 시작일, 종료일, 화주, 담당자, 차량번호, 품목, 시작일2, 종료일2, cd_company };



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

        #endregion

        #region ♪ 저장 관련     ♬


        protected override bool SaveData()
        {
            // MainGrids 에 설정된 모든 그리드에 무결성 검사 수행
            if (Verify() == false) return false;

            DataTable dt = _flex.GetChanges();
            if (dt == null) return true;

            if (_biz.Save(dt))
            {
                _flex.AcceptChanges();
                return true;
            }

            return false;
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

        private void btn검색_Click(object sender, EventArgs e)
        {
            P_CZ_PARTNER_INTRT_SUB helpDlg = new P_CZ_PARTNER_INTRT_SUB();

            if (helpDlg.ShowDialog(this) != DialogResult.OK) return;
        }
    }
}