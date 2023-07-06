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

    public partial class P_CZ_UNLOADINGRPT_RPT : PageBase
    {
        P_CZ_UNLOADINGRPT_RPT_BIZ _biz;

        public P_CZ_UNLOADINGRPT_RPT()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
            DataChanged += new EventHandler(Page_DataChanged);

        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_UNLOADINGRPT_RPT_BIZ();

            InitGrid();
            InitEvent();

            MainGrids = new FlexGrid[] { _flex };


        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            //_flex.SetCol("S", "S", 20, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CD_ITEM", "품번", 50, false, typeof(string));
            _flex.SetCol("NM_ITEM", "품명", 50, false, typeof(string));

            _flex.SetCol("DT_LOADIING", "선적항", 60, false);
            _flex.SetCol("ARRIVER", "하역항", 60, false);
            _flex.SetCol("LN_PARTNER", "업체명", 150, false);

            _flex.SetCol("QT_BL", "수량", 50, 8, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("WT_LOADING", "수량", 50, 8, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("QT_DIFF1", "감모량", 50, 8, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("RT1", "Balance(%)", 30, 8, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            _flex.SetCol("WT_UNLOADING", "수량", 50, 8, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("QT_DIFF2", "감모량", 50, 8, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("RT2", "Balance(%)", 30, 8, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            _flex.SetExceptEditCol("");
            //_flex.VerifyAutoDelete = new string[] { "CD_PARTNER", "YYYYMM", "INTEREST_RT" };
            //_flex.VerifyPrimaryKey = new string[] { "CD_PARTNER", "YYYYMM" };
            //_flex.VerifyNotNull = new string[] { "CD_PARTNER", "YYYYMM", "INTEREST_RT" };


            // 도움창 예제
            _flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

            //_flex.AddRow += new EventHandler(OnToolBarAddButtonClicked);

            //_flex.OwnerDrawCell += new OwnerDrawCellEventHandler(flex_OwnerDrawCell);

            //_flex.ValidateEdit += new ValidateEditEventHandler(Flex_ValidateEdit);
            //_flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Flex_BeforeCodeHelp);

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



        }

        #endregion

        #region ♪ 메인 버튼     ♬

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch() || !DatePickerCheck) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                string 회사 = Global.MainFrame.LoginInfo.CompanyCode;
                string 거래처 = D.GetString(bpc거래처.CodeValue);
                string 시작일 = D.GetString(dtpFROM.Text);
                string 종료일 = D.GetString(dtpTO.Text);
                string 검색 = D.GetString(txtSearch.Text);

                object[] obj = new object[] { 회사, 시작일, 종료일, 거래처, 검색 };



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
                //_flex["YYYYMM"] = dtpFROM.Text;

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
            //try
            //{
            //    if (!BeforeSave()) return;

            //    if (MsgAndSave(PageActionMode.Save))
            //        ShowMessage(PageResultMode.SaveGood);
            //}
            //catch (Exception ex)
            //{
            //    MsgEnd(ex);
            //}
        }

        #endregion

        #region ♪ 화면 내 버튼  ♬

        #endregion

        #region ♪ 저장 관련     ♬


        protected override bool SaveData()
        {
            // MainGrids 에 설정된 모든 그리드에 무결성 검사 수행
            //if (Verify() == false) return false;

            //DataTable dt = _flex.GetChanges();
            //if (dt == null) return true;

            //if (_biz.Save(dt))
            //{
            //    _flex.AcceptChanges();
            //    return true;
            //}

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
            //try
            //{
            //    // 도움창 HelpID 에 따라..
            //    switch (e.HelpID)
            //    {
            //        case HelpID.P_MA_PITEM_SUB: // 예제
            //            // 체크할 거 체크해준다.
            //            e.HelpParam.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
            //            e.HelpParam.ResultMode = ResultMode.SlowMode;
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgEnd(ex);
            //}
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

        private void flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                CellStyle s = _flex.Styles.Add("병합");
                s.Border.Direction = BorderDirEnum.Vertical;

                //if (e.Row < _flex.Rows.Fixed || e.Col < _flex.Cols.Fixed) return;//헤더는 안바뀌게

                //if (D.GetString(_flex[e.Row, "LN_PARTNER"]) == D.GetString(_flex[e.Row - 1, "LN_PARTNER"]))
                //{
                //    CellRange cr = _flex.GetCellRange(e.Row - 1, _flex.Cols["AM1"].Index);
                //    cr.Style = _flex.Styles["병합"];

                //    CellRange cr2 = _flex.GetCellRange(e.Row - 1, _flex.Cols["AM2"].Index);
                //    cr2.Style = _flex.Styles["병합"];
                //}

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 기타 메서드   ♬

        #endregion

        #region ♪ 속성          ♬


        bool DatePickerCheck { get { return Checker.IsValid(dtpFROM, true, DD("")); } }

        #endregion


    }
}