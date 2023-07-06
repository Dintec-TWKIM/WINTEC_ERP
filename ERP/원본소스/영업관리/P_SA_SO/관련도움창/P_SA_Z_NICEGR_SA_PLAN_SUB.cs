using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace sale
{
    public partial class P_SA_Z_NICEGR_SA_PLAN_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region ▷ 생성자 & 초기화
        P_SA_Z_NICEGR_SA_PLAN_SUB_BIZ _biz = new P_SA_Z_NICEGR_SA_PLAN_SUB_BIZ();

        public string _거래처 = string.Empty;
        public string _거래처명 = string.Empty;
        public string _환종 = string.Empty;
        public string _환종명 = string.Empty;
        public DataTable _dt = null;


        public P_SA_Z_NICEGR_SA_PLAN_SUB(string CD_PARTNER, string LN_PARTNER, string CD_EXCH, string NM_EXCH)
        {
            InitializeComponent();

            _거래처 = CD_PARTNER;
            _거래처명 = LN_PARTNER;
            _환종 = CD_EXCH;
            _환종명 = NM_EXCH;
        }

        #region -> InitLoad
        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            InitEvent();
        }
        #endregion

        #region -> InitEvent
        private void InitEvent()
        {
            btn조회.Click += new EventHandler(btn조회_Click);
            btn확인.Click += new EventHandler(btn확인_Click);
            btn닫기.Click += new EventHandler(btn닫기_Click);
            bpWeekNo.QueryBefore += new BpQueryHandler(Control_QueryBefore);
        }
        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            SetControl str = new SetControl();
            str.SetCombobox(cbo환종, MA.GetCode("MA_B000005"));

            bp거래처.SetCode(_거래처, _거래처명);
            cbo환종.SelectedValue = _환종;
            cbo환종.Text = _환종명;

            dtp년도.Text = Global.MainFrame.GetStringYearMonth;

            _flex.SetDataMap("FG_SALE", MA.GetCodeUser(new string[] { "100", "200" }, new string[] { "국내", "해외" }, true), "CODE", "NAME");
            _flex.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005", true), "CODE", "NAME");
        }
        #endregion

        #region -> InitGrid
        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flex.SetCol("FG_SALE", "구분", 80, false);
            _flex.SetCol("LAST_PARTNER", "최종거래처", 100, false);
            _flex.SetCol("NM_LAST_PARTNER", "최종거래처명", 120, false);
            _flex.SetCol("CD_MODEL", "모델", 100, false);
            _flex.SetCol("CD_ITEM", "품목코드", 100, false);
            _flex.SetCol("NM_ITEM", "품목명", 120, false);
            _flex.SetCol("CD_PARTNER", "거래처", 100, false);
            _flex.SetCol("LN_PARTNER", "거래처명", 120, false);
            _flex.SetCol("CD_EXCH", "환종", 80, false);
            _flex.SetCol("QT_PLAN", "수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("UM_PLAN", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex.SetCol("AM_PLAN", "금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("QT_SO", "반영수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("NO_WEEK", "차수", 80, false);
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex.SetExceptSumCol("UM_PLAN");

        }
        #endregion
        
        #endregion

        #region ▷ 화면 내 버튼
        #region -> btn조회_Click
        void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk년월) return;

                object[] obj = { MA.Login.회사코드,
                                 Global.MainFrame.LoginInfo.CdPlant,
                                 dtp년도.Text,
                                 D.GetNull(bpWeekNo.CodeValue),
                                 D.GetNull(txt차수.Text),
                                 _거래처,
                                 _환종

                               };

                DataTable dt = _biz.Search(obj);
                _flex.Binding = dt;

                if (!_flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn확인_Click
        void btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn닫기_Click
        void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion 
        #endregion

        #region ▷ 도움창 이벤트
        #region -> Control_QueryBefore
        void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                BpCodeTextBox bp_Control = sender as BpCodeTextBox;

                switch (e.HelpID)
                {
                    case HelpID.P_USER:
                        bp_Control.UserParams = "Week No 도움창;H_SA_Z_NICEGR_WEEK_NO_SUB;";
                        break;

                    case HelpID.P_SA_TPSO_SUB:
                        e.HelpParam.P61_CODE1 = "N";
                        e.HelpParam.P62_CODE2 = "Y";
                        break;
                }
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion 
        #endregion

        #region ▷ 기타 메서드
        #region -> GetData
        public DataRow[] GetData
        {
            get
            {
                return _flex.DataTable.Select("S ='Y'", "", DataViewRowState.CurrentRows);
            }
        }
        #endregion 
        #endregion

        #region ▷ 속성
        #region -> 속성
        bool Chk년월 { get { return !Checker.IsEmpty(dtp년도, lbl년도.Text); } }

        #endregion 
        #endregion
    }
}
