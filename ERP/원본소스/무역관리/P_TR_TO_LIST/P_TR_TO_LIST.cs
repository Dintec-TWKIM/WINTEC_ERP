using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

using System.Threading;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.ERPU.MF.Common;

namespace trade
{
    public partial class P_TR_TO_LIST : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언

        private P_TR_TO_LIST_BIZ _biz;
        private bool YN_REBATE;


        public P_TR_TO_LIST()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        //발주코드도움 전역변수
        //string m_발주CodeValue = "";

        CodeHelpNO_PO dlg = new CodeHelpNO_PO();

        #region ♣ 초기화 이벤트 / 메소드

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_TR_TO_LIST_BIZ();
            YN_REBATE = BASIC.GetMAEXC("리베이트사용여부") == "100";


            InitGrid();
            InitEvent();
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            InitControl();

            periodPicker.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
            periodPicker.Text = MainFrameInterface.GetStringToday;


            oneGrid1.UseCustomLayout = true;
            bpPanelControl9.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            try
            {
                //1. L/C 구분
                DataSet ds = Global.MainFrame.GetComboData("S;TR_IM00005");

                // L/C 구분(TR_IM00005)
                m_comFgLc.DataSource = ds.Tables[0];
                m_comFgLc.DisplayMember = "NAME";
                m_comFgLc.ValueMember = "CODE";

                _flex.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005"), "CODE", "NAME");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            _flex.SetCol("CHK", "S", 20, true, CheckTypeEnum.Y_N);
            _flex.SetCol("NO_TO", "신고번호", 100, false);
            _flex.SetCol("NO_PO", "발주번호", 100, false);
            _flex.SetCol("NO_LINE_PO", "항번", 60, false);
            _flex.SetCol("DT_TO", "신고일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("LN_PARTNER", "거래처", 120, false);
            _flex.SetCol("CD_EXCH", "환종", 100, false);
            //_flex.SetCol("AM_EX", "금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("RT_EXCH", "기표환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            //_flex.SetCol("AM_LICENSE", "명장금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            //_flex.SetCol("BL_AM_EX", "B/L금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("NM_PURGRP", "구매그룹", 100, false);
            _flex.SetCol("NM_KOR", "담당자", 100, false);

            _flex.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            _flex.SetCol("NM_ITEM", "품목명", 140, false);
            _flex.SetCol("STND_ITEM", "규격", 120, false);
            _flex.SetCol("UNIT_IM", "단위", 40, false);
            _flex.SetCol("QT_TO", "수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("UM_EX", "단가", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex.SetCol("AM_EX", "금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("UM", "원화단가", 90, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            _flex.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("NO_BL", "B/L번호", 100, false);
            _flex.SetCol("NO_BLLINE", "B/L항번", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("NM_BANK", "개설은행", 60, false);
            _flex.SetCol("NM_FG_LC", "L/C구분", 60, false);
            _flex.SetCol("NM_PRICE", "가격조건", 60, false);
            _flex.SetCol("NM_CUSTOMS", "관할세관", 60, false);

            _flex.SetCol("NM_GRP_MFG", "제품군", 60, false);
            _flex.SetCol("NM_GRP_ITEM", "품목군", 60, false);
            _flex.SetCol("NM_CLS_L", "대분류", 60, false);
            _flex.SetCol("NM_CLS_M", "중분류", 60, false);
            _flex.SetCol("NM_CLS_S", "소분류", 60, false);
            _flex.SetCol("NO_LC", "L/C번호", 80, false);
            _flex.SetCol("CD_PJT", "프로젝트", 100, 20, false);
            _flex.SetCol("NM_PROJECT", "프로젝트명", 140, false);

            if (YN_REBATE)
            {
                _flex.SetCol("UM_REBATE", "리베이트원화단가", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("UM_REBATE_EX", "리베이트단가", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("AM_REBATE", "리베이트원화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex.SetCol("AM_REBATE_EX", "리베이트금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
                _flex.SetCol("AM_REBATE_N", "미착품", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            }

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                _flex.SetCol("CD_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                _flex.SetCol("NM_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                _flex.SetCol("STND_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            _flex.SetCol("REMARK", "비고", 150, false);

            _flex.SettingVersion = "1.0.0.3";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            _flex.SetExceptSumCol("UM_EX", "UM", "RT_EXCH", "NO_BLLINE");
        }

        #endregion

        #region -> InitEvent

        private void InitEvent()
        {
            m_txtITEM_FR.CodeChanged += new EventHandler(Control_CodeChanged);
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트 / 메소드

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;

            if (!신고기간시작등록여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, m_lblDisTermTo.Text);
                periodPicker.Focus();
                return false;
            }

            if (!신고기간끝등록여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, m_lblDisTermTo.Text);
                periodPicker.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region -> 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch() || !Chk품목)
                    return;
                //if (bpc발주.Text == "")
                //    m_발주CodeValue = "";

                object[] obj = new object[] { LoginInfo.CompanyCode, 
                    periodPicker.StartDateToString,                                //신고기간시작
                    periodPicker.EndDateToString,                                  //신고기간끝
                    m_comFgLc.SelectedValue.ToString(),             //L/C 구분
                    m_txtCdTrans.CodeValue,                         //거래처
                    m_txtGroupRcv.CodeValue,                        //구매그룹
                    m_txtITEM_FR.CodeValue,
                    m_txtITEM_TO.CodeValue,
                    txt발주번호.Text.ToUpper()

                };

                DataTable dt = _biz.search(obj);

                _flex.UnBinding = dt;

                _flex.SubtotalPosition = SubtotalPositionEnum.BelowData;
                _flex.SelectionMode = SelectionModeEnum.Row;

                CellStyle s = _flex.Styles[CellStyleEnum.Subtotal0];
                s.BackColor = Color.FromArgb(234, 234, 234);
                s.ForeColor = Color.Black;
                s.Font = new Font(_flex.Font, FontStyle.Bold);

                _flex.Subtotal(AggregateEnum.Clear);

                SSInfo defaultSS = new SSInfo();
                defaultSS.VisibleColumns = new string[] {"NO_TO", "DT_TO", "LN_PARTNER","NM_EXCH", "RT_EXCH", "NM_PURGRP","NM_KOR","CD_ITEM","NM_ITEM","STND_ITEM","UNIT_IM","QT_TO", "UM_EX", "AM_EX", "UM","AM","NO_BL","NO_BLLINE" };
                defaultSS.GroupColumns = new string[] { "NM_CLS_L", "NM_CLS_M", "NM_CLS_S" };
                defaultSS.TotalColumns = new string[] { "QT_TO", "AM_EX", "AM" };
                defaultSS.AccColumns = new string[] { "QT_TO", "AM_EX", "AM" };

                defaultSS.CanGrandTotal = true;

                _flex.ExecuteSubTotal(defaultSS);

                //_flex.ExecuteSubTotal();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 도움창 이벤트 / 메소드

        #region -> 도움창 코드변경 이벤트(OnBpControl_CodeChanged)

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                ;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 호출되기 전에 세팅해주는 이벤트(OnBpControl_QueryBefore)

        private void OnBpControl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB:
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창에서 되돌아올 때 필요한 값을 세팅하거나 Validate Check 해주는 이벤트(OnBpControl_QueryAfter)

        private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB:
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ Control 이벤트 / 메소드



        #endregion

        #region ♣ 기타 이벤트 / 메소드

        #region -> Bp_Control CodeChanged

        void Control_CodeChanged(object sender, EventArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            try
            {
                switch (bp_Control.Name)
                {
                    case "m_txtITEM_FR":
                        m_txtITEM_TO.SetCode(m_txtITEM_FR.CodeValue, m_txtITEM_FR.CodeName);
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

        #endregion

        #region ♣ 속성

        public bool 신고기간시작등록여부
        {
            get
            {
                if (periodPicker.StartDateToString == "")
                    return false;
                return true;
            }
        }

        public bool 신고기간끝등록여부
        {
            get
            {
                if (periodPicker.EndDateToString == "")
                    return false;
                return true;
            }
        }

        bool Chk품목 { get { return Checker.IsValid(m_txtITEM_FR, m_txtITEM_TO, false, DD("품목From"), DD("품목To")); } }

        #endregion

        private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB:
                    e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
                    break;
            }
        }

        //private void bpc발주btn_Click(object sender, EventArgs e)
        //{
        //    if (dlg.ShowDialog(bpc발주btn) == DialogResult.OK)
        //    {
        //        m_발주CodeValue = dlg.m_Row["NO_PO"].ToString();
        //        bpc발주.Text = dlg.m_Row["NO_PO"].ToString();
        //    }

           
        //}

        //private void bpc발주_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //    {

        //        if (dlg.Search(bpc발주.Text) == true)
        //        {
        //            m_발주CodeValue = dlg.m_Row["NO_PO"].ToString();
        //            bpc발주.Text = dlg.m_Row["NO_PO"].ToString();
        //        }
        //        else
        //        {
        //            m_발주CodeValue = "";
        //            bpc발주.Text = "";
        //        }

        //    }
        //}
    }
}
