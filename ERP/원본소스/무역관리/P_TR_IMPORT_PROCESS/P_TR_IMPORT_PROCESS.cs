using System;
using System.Data;

using Dass.FlexGrid;
using Duzon.Common.Forms;

using C1.Win.C1FlexGrid;
using pur;
using System.Windows.Forms;
using Duzon.Common.BpControls;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace trade
{
    public partial class P_TR_IMPORT_PROCESS : Duzon.Common.Forms.PageBase
    {
        // **************************************
        // 작   성   자 : 신미란
        // 작   성   일 : 2009-07-21
        // 모   듈   명 : 무역
        // 시 스  템 명 : 수입관리
        // 서브시스템명 : 수입
        // 페 이 지  명 : 수입진행현황
        // 프로젝트  명 : P_TR_IMPORT_PROCESS
        // **************************************

        #region ★ 멤버필드

        private P_TR_IMPORT_PROCESS_BIZ _biz;
        DataSet m_dsCombo = null;
        private Dass.FlexGrid.FlexGrid _flexM1;
        private Dass.FlexGrid.FlexGrid _flexM2;
        private Dass.FlexGrid.FlexGrid _flexM3;
        private Dass.FlexGrid.FlexGrid _flexM4;
        private Dass.FlexGrid.FlexGrid _flexM5;
        private Dass.FlexGrid.FlexGrid _flexD1;
        private Dass.FlexGrid.FlexGrid _flexD2;
        private Dass.FlexGrid.FlexGrid _flexD3;
        private Dass.FlexGrid.FlexGrid _flexD4;
        private Dass.FlexGrid.FlexGrid _flexD5;


        private string CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;

        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_TR_IMPORT_PROCESS()
        {
            try
            {
                InitializeComponent();

                MainGrids = new FlexGrid[] { _flexBL };
           //     DataChanged += new EventHandler(Page_DataChanged);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_TR_IMPORT_PROCESS_BIZ();
                       
            InitGrid();
            InitEvent();
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();
            InitControl();


            oneGrid1.UseCustomLayout = true;
            bpPanelControl3.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            if (Global.MainFrame.ServerKey == "HANSU" )
            {
                label1.Text = "발주기간";
            }
        }

        #endregion

        #region -> InitGrid

        void InitGrid()
        {
            _flexBL.BeginSetting( 1, 1, false );

            _flexBL.SetCol("NM_ITEMGRP", "품목군", 120);
            _flexBL.SetCol("NO_PO", "발주번호", 80);
            _flexBL.SetCol("NO_POLINE", "항번", 50);
            _flexBL.SetCol("QT_LC", "LC수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexBL.SetCol("QT_REQ_MM", " 입고의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexBL.SetCol("NM_GRPMFG", "제품군", 80);
            _flexBL.SetCol("CD_ITEM", "품목코드", 80);
            _flexBL.SetCol("NM_ITEM", "품목명", 80);
            _flexBL.SetCol("STND_ITEM", "규격", 80);
            _flexBL.SetCol("UNIT_IM", "단위", 80);
            _flexBL.SetCol("QT_BL", "B/L수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexBL.SetCol("QT_TO", "통관수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexBL.SetCol("QT_REV_MM", "가입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexBL.SetCol("QT_REQ", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexBL.SetCol("QT_CLS", "미착정산수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexBL.SetCol("UM_AM_QT", "단가", 120, false, typeof(Decimal), FormatTpType.UNIT_COST);
            _flexBL.SetCol("AM_CLS", "금액", 120, false, typeof(Decimal), FormatTpType.MONEY);
            _flexBL.SetCol("BL_AM_EX", "미착금액(B/L)", 120, false, typeof(Decimal), FormatTpType.MONEY);
            _flexBL.SetCol("BL_AM", "미착물대(B/L-원화)", 120, false, typeof(Decimal), FormatTpType.MONEY);
            _flexBL.SetCol("NO_BL", "B/L번호", 80);
            _flexBL.SetCol("AM_EX_PO", "발주금액", 120, false, typeof(Decimal), FormatTpType.MONEY);
            _flexBL.SetCol("UM_EX_PO", "발주단가", 120, false, typeof(Decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexBL.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexBL.SetCol("LN_PARTNER", "거래처명", 80);
            _flexBL.SetCol("CD_PJT", "PROJECT", 100);
            _flexBL.SetCol("NM_PJT", "PROJECT명", 140);

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flexBL.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                _flexBL.SetCol("CD_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                _flexBL.SetCol("NM_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                _flexBL.SetCol("STND_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }

            _flexBL.SettingVersion = "1.0.0.4";
            _flexBL.EndSetting( GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top );
            _flexBL.SetExceptSumCol("UM_AM_QT");

            _flexBL.DoubleClick += new EventHandler(_flexBL_DoubleClick);

        }

        #endregion
        
        #region InitControl

        private void InitControl()
        {
            // 정산여부
            m_dsCombo = GetComboData("S;TR_IM00005", "S;MA_B000005", "SC;MA_PLANT");
            

            //L/C구분코드 ComboBox에 Add
            //L/C구분중 Local L/C는 제거
            m_dsCombo.Tables[0].Rows.RemoveAt(1);
            m_comFgLc.DataSource = m_dsCombo.Tables[0];
            m_comFgLc.ValueMember = "CODE";
            m_comFgLc.DisplayMember = "NAME";

            m_cmbCD_EXCH.DataSource = m_dsCombo.Tables[1];
            m_cmbCD_EXCH.ValueMember = "CODE";
            m_cmbCD_EXCH.DisplayMember = "NAME";


            cbo공장.DataSource = m_dsCombo.Tables[2];
            cbo공장.ValueMember = "CODE";
            cbo공장.DisplayMember = "NAME";


            //m_mskTermFrom.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            //m_mskTermFrom.ToDayDate = Global.MainFrame.GetDateTimeToday();
            periodPicker1.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;

            //m_mskTermTo.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            //m_mskTermTo.ToDayDate = Global.MainFrame.GetDateTimeToday();
            periodPicker1.EndDateToString = Global.MainFrame.GetStringToday;

            m_Date.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            m_Date.ToDayDate = Global.MainFrame.GetDateTimeToday();
            m_Date.Text = Global.MainFrame.GetStringFirstDayInMonth;

            bp프로젝트.QueryBefore += new BpQueryHandler(bp프로젝트_QueryBefore);
            //btn발주번호.Click += new EventHandler(btn발주번호_Click);

        }

       
        #endregion
        
        #region ->InitEvent

        private void InitEvent()
        {
            tb_PARTNER_Fr.CodeChanged += new EventHandler(Control_CodeChanged);

        }

        #endregion

        #endregion

        #region ♣ Control 이벤트 / 메소드

        //void btn발주번호_Click(object sender, EventArgs e)
        //{
        //    string CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
        //    string CD_PARTNER = "";
        //    string NM_PARTNER = "";
        //    string CD_PURGRP = "";
        //    string NM_PURGRP = "";

        //    P_PU_PO_SUB2 dlg = new P_PU_PO_SUB2(CD_PLANT, CD_PARTNER, NM_PARTNER, CD_PURGRP, NM_PURGRP);
        //    if (dlg.ShowDialog() != DialogResult.OK) return;

        //    txt발주번호.Text = dlg.m_NO_PO;
        //}

        #region -> Bp_Control CodeChanged

        void Control_CodeChanged(object sender, EventArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            try
            {
                switch (bp_Control.Name)
                {
                    case "tb_PARTNER_Fr":
                        tb_PARTNER_To.SetCode(tb_PARTNER_Fr.CodeValue, tb_PARTNER_Fr.CodeName);
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

        private void bp프로젝트_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_USER:
                        if (bp프로젝트.UserHelpID == "H_SA_PRJ_SUB")
                        {
                            e.HelpParam.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        #endregion

        #region ★ 메인버튼 클릭

        #region -> 조회버튼 클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch() || !Chk거래처) return;

                object[] obj = new object[13];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode.ToString();
                obj[1] = periodPicker1.StartDateToString;
                obj[2] = periodPicker1.EndDateToString;
                obj[3] = tb_PARTNER_Fr.CodeValue;
                obj[4] = tb_PARTNER_To.CodeValue;
                obj[5] = m_comFgLc.SelectedValue.ToString();
                obj[6] = tb_PURGRP.CodeValue;
                obj[7] = m_txtEmp.CodeValue.ToString();
                obj[8] = m_cmbCD_EXCH.SelectedValue.ToString();
                obj[9] = m_Date.MaskEditBox.ClipText;
                obj[10] = txt발주번호.Text.ToUpper();
                obj[11] = D.GetString(cbo공장.SelectedValue);
                obj[12] = D.GetString(bp프로젝트.CodeValue);

                DataTable dt = _biz.Search(obj);
                _flexBL.UnBinding = dt;

                SSInfo defaultSS = new SSInfo();
                defaultSS.VisibleColumns = new string[] { "NM_ITEMGRP", "NM_GRPMFG", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM" };
                defaultSS.GroupColumns = new string[] { "CD_PJT", "SEQ_PROJECT", "CD_ITEM" };
                defaultSS.TotalColumns = new string[] { "QT_BL", "QT_TO", "QT_REQ", "QT_CLS", "AM_CLS", "BL_AM_EX", "BL_AM" };
                defaultSS.AccColumns = new string[] { "QT_BL", "QT_TO", "QT_REQ", "QT_CLS", "AM_CLS", "BL_AM_EX", "BL_AM" };
                defaultSS.CanGrandTotal = true;


                _flexBL.ExecuteSubTotal(defaultSS);

                if (!_flexBL.HasNormalRow)
                {
                    ShowMessage(PageResultMode.SearchNoData);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion
  
        #endregion

        #region ♥ 그리드 이벤트

        void _flexM_DoubleClick(object sender, EventArgs e)
        {
            object[] args = new Object[]{
           _flexBL.GetDataRow(_flexBL.Row)["NO_PO"].ToString(), 0m, "발주번호"  };
            CallOtherPageMethod("P_PU_PO_REG2", "발주등록(공장)", Grant, args);

        }

        #region -> _flex_DoubleClick

        void _flexBL_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!_flexBL.HasNormalRow) return;
                object[] args = new object[3];

                switch (_flexBL.Cols[_flexBL.Col].Name)
                { 
                    case "NO_PO":
                        args[0] = _flexBL.GetDataRow(_flexBL.Row)["NO_PO"].ToString();
                        args[1] = 0m;
                        args[2] = "발주번호";

                        CallOtherPageMethod("P_PU_PO_REG2", "발주등록(공장)", Grant, args);
                        break;

                    case "NO_BL":
                        args[0] = _flexBL.GetDataRow(_flexBL.Row)["NO_BL"].ToString();
                        args[1] = 0m;
                        args[2] = "BL번호";

                        CallOtherPageMethod("P_TR_BL_IN", "B/L 도착등록", Grant, args);
                        
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

        #region ♥ 속성

        bool Chk거래처 { get { return Checker.IsValid(tb_PARTNER_Fr, tb_PARTNER_To, false, DD("거래처From"), DD("거래처To")); } }

        #endregion
    }
}
