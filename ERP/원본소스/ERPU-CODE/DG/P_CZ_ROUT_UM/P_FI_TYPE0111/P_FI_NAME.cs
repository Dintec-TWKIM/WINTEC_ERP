using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon;
using Duzon.ERPU;
using Duzon.ERPU.FI;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace account
{
    /// <summary>
    /// ================================================
    /// AUTHOR      : KwakYoungJin-PC\KwakYoungJin
    /// CREATE DATE : 2011-09-14 오후 3:52:41
    /// 
    /// MODULE      : 회계관리
    /// SYSTEM      : 
    /// SUBSYSTEM   : 
    /// PAGE        : 
    /// PROJECT     : 
    /// DESCRIPTION : 참고화면 - 신용카드부서등록(P_FI_CARD_DEPT)
    /// ================================================ 
    /// CHANGE HISTORY
    /// v1.0 : 2011-09-14 오후 3:52:41 신규 생성
    /// ================================================
    /// </summary>

    public partial class P_FI_TYPE0111 : PageBase
    {
        P_FI_TYPE011_BIZ _biz;

        public P_FI_TYPE0111()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flexTab1M, _flexTab1D, _flexTab2M, _flexTab2D };
        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_FI_TYPE011_BIZ();
            InitGrid();
            InitEvent();
        }

        void InitGrid()
        {

        }

        private void InitEvent()
        {
            _tab.Selecting += new TabControlCancelEventHandler(Tab_Selecting);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            dtp01.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtp01.Text = Global.MainFrame.GetStringToday;
        }

        #endregion

        #region ♪ 메인 버튼     ♬

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch() || !조회조건체크) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                DataSet ds = _biz.Search();
                _flexTab1M.Binding = ds.Tables[1];

                if (!_flexTab1M.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
                if (!_flexTab1M.HasNormalRow) return;

                _flexTab1M.Rows.Add();
                _flexTab1M.Row = _flexTab1M.Rows.Count - 1;
                _flexTab1M[""] = D.GetString(_flexTab1D[""]);
                _flexTab1M.Col = _flexTab1M.Cols.Fixed;
                _flexTab1M.AddFinished();
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
                if (!_flexTab1M.HasNormalRow) return;

                DataRow[] drs = _flexTab1M.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drs == null || drs.Length == 0)
                    _flexTab1M.Rows.Remove(_flexTab1M.Row);
                else
                {
                    foreach (DataRow row in drs)
                        row.Delete();
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
                if (!Verify()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
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
            if (!base.SaveData()) return false;

            //DataTable dt = _flex.GetChanges();

            //if (dt == null || dt.Rows.Count == 0) return false;

            //if (!_biz.Save(dt)) return false;

            //_flex.AcceptChanges();
            return true;
        }

        #endregion

        #region ♪ 그리드 이벤트 ♬

        void Flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                //DataTable dt = null;
                //string Key = D.GetString(_flexH[""]);
                //string Filter = "컬럼 = '" + Key + "'";
                //string 조회조건 = D.GetString(cbo04.SelectedValue);

                //if (_flexH.DetailQueryNeed)
                //    dt = _biz.SearchDetail(Key, 조회조건);

                //_flexL.BindingAdd(dt, Filter);
                //_flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        void Flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                // 도움창띄울컬럼이아니면 RETURN
                //if (_flex.Cols[e.Col].Name != SELCOL) return;

                // 처리한 로직 넣어줌.
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 기타 이벤트   ♬

        void Tab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                switch (_tab.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 기타 메소드   ♬

        #endregion

        #region ♪ 속성          ♬

        bool 조회조건체크 { get { return !Checker.IsEmpty(dtpFROM.Text, DD("")); } }

        #endregion
    }
}