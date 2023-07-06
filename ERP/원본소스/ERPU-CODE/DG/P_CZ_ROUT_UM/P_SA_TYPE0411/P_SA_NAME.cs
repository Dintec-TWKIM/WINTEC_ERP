using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.ERPU;
using Duzon.ERPU.SA;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace sale
{
    /// <summary>
    /// ================================================
    /// AUTHOR      : KwakYoungJin-PC\KwakYoungJin
    /// CREATE DATE : 2011-09-14 오후 3:50:38
    /// 
    /// MODULE      : 영업관리
    /// SYSTEM      : 
    /// SUBSYSTEM   : 
    /// PAGE        : 
    /// PROJECT     : 
    /// DESCRIPTION : 참고 화면 - 수주유형관리(P_SA_TPSO)
    /// ================================================ 
    /// CHANGE HISTORY
    /// v1.0 : 2011-09-14 오후 3:50:38 신규 생성
    /// ================================================
    /// </summary>

    public partial class P_SA_TYPE0411 : PageBase
    {
        P_SA_TYPE041_BIZ _biz;

        public P_SA_TYPE0411()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
            DataChanged += new EventHandler(Page_DataChanged);
        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_SA_TYPE041_BIZ();
            InitGrid();
        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);

            _flex.SetCol("", "", 75, 4);
            _flex.SetCol("", "", false);
            _flex.SetBinding(pnlDetail, new object[] { txt01 });

            // 라디오버튼 바인딩
            _flex.SetBindningRadioButton(new RadioButtonExt[] { rdo01, rdo02 }, new string[] { "Y", "N" });
            _flex.SetBindningRadioButton(new RadioButtonExt[] { rdo03, rdo04, rdo05 }, new string[] { "Y", "N", "C" });
            _flex.SetBindningRadioButton(new RadioButtonExt[] { rdo06, rdo07 }, new string[] { "Y", "N" });
            _flex.SetBindningRadioButton(new RadioButtonExt[] { rdo08, rdo09 }, new string[] { "Y", "N" });
            _flex.SetBindningRadioButton(new RadioButtonExt[] { rdo10, rdo11 }, new string[] { "Y", "N" });

            _flex.SetRelatedControl(txt01, new object[] { txt02 });

            _flex.VerifyPrimaryKey = new string[] { };
            _flex.VerifyNotNull = new string[] { };
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            cbo04.Enabled = Sa_Global.TpSo_AutoProcess == "N" ? false : true;   // 자동 프로세스 수정 불가
            bpctb02.Enabled = Sa_Global.TpSo_CdCc == "N" ? false : true;        // C/C설정 수정불가

            SetControl str = new SetControl();
            //str.SetCombobox(cbo01, MF.GetCode(MF.코드.생산.공정경로번호, true));  // ModuleHelper에 정의된 코드가 있으면..
            //str.SetCombobox(cbo01, MF.GetCode("SA_B000050", true));           // ModuleHelper에 정의된 코드가 없으면..
        }

        #endregion

        #region ♪ 메인 버튼     ♬

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                _flex.Binding = _biz.Search();

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
                if (!BeforeAdd()) return;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;
                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;
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
            if (!Verify()) return false; // MainGrids 에 설정된 모든 그리드에 무결성 검사 수행

            DataTable dt = _flex.GetChanges();

            if (dt == null) return true;

            _biz.Save(dt);

            _flex.AcceptChanges();
            return true;
        }

        #endregion

        #region ♪ 그리드 이벤트 ♬

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDetail.Enabled = _flex.HasNormalRow;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 기타 이벤트   ♬

        private void Cbo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "":
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

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

        #endregion
    }
}