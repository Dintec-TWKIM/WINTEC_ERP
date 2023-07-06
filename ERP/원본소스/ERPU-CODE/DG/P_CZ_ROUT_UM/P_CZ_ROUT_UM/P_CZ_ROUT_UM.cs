using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.ERPU;
//using Duzon.ERPU.MF;
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
    /// CREATE DATE : 2014-02-14 
    /// 
    /// MODULE      : 커스터마이징
    /// SYSTEM      : 
    /// SUBSYSTEM   : 
    /// PAGE        : 
    /// PROJECT     : 
    /// DESCRIPTION : 구간별 단가정보관리 
    /// ================================================ 
    /// CHANGE HISTORY
    /// v1.0 : 2014-02-14
    /// ================================================
    /// </summary>

    public partial class P_CZ_ROUT_UM : PageBase
    {
        P_CZ_ROUT_UM_BIZ _biz;

        public P_CZ_ROUT_UM()
        {
            InitializeComponent();
            MainGrids = new FlexGrid[] { _flex };
        }

        #region ♪ 초기화        ♬

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_ROUT_UM_BIZ();

            InitGrid();
            InitEvent();
        }

        private void InitGrid()
        {

            _flex.BeginSetting(1, 1, false);
            _flex.SetCol("S", "S", 20, true, CheckTypeEnum.Y_N);
            //_flex.SetCol("CAR_NO", "차량번호", 100, 20, false);
            //_flex.SetCol("NM_CAR", "차량정보", 100, 20, false);
            _flex.SetCol("NM_SHIPPER", "화주", 150, 8, true, typeof(string));
            _flex.SetCol("NM_REAL_LOADING_PLACE", "실상차지", 100, 8, true, typeof(string));
            _flex.SetCol("NM_LOADING_PLACE", "상차지", 100, 8, true, typeof(string));
            _flex.SetCol("NM_ALIGHT_PLACE", "하차지", 100, 8, true, typeof(string));

            _flex.SetCol("REQ_PRICE", "청구단가", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("REQ_UNIT_PRICE", "청구 대당단가", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("PROVIDE_PRICE", "지급단가", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT_PRICE", "지급 대당단가", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("ESTIMATE_PRICE", "계근비", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FREIGHT_CHARGE_A", "운임A", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FREIGHT_CHARGE_B", "운임B", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("STANDARD_PLACE", "상하차기준", 90, 17, true, typeof(string));

            _flex.SetCol("SHIPPER", "화주", 100, 8, true, typeof(string));
            _flex.SetCol("REAL_LOADING_PLACE", "실상차지", 100, 8, true, typeof(string));
            _flex.SetCol("LOADING_PLACE", "상차지", 100, 8, true, typeof(string));
            _flex.SetCol("ALIGHT_PLACE", "하차지", 100, 8, true, typeof(string));

            _flex.EnterKeyAddRow = false;

            //_flex.SetExceptEditCol("");
            //_flex.VerifyAutoDelete = new string[] {"CAR_NO", "SHIPPER" ,"LOADING_PLACE", "ALIGHT_PLACE"};
            //_flex.VerifyPrimaryKey = new string[] {  "CAR_NO", "SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE" };
            _flex.VerifyPrimaryKey = new string[] { "IDX","SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE" };
            //_flex.VerifyNotNull = new string[] { "CAR_NO", "SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE" };
            _flex.VerifyNotNull = new string[] { "SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "STANDARD_PLACE" };


            // 도움창 ( 도움창이 작동할 GRID 컬럼, 도움창명, 도움창유형, 리턴 받을 GRID 컬럼, 도움창상 받아올 매핑 컬럼)
            //_flex.SetCodeHelpCol("CAR_NO", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "CAR_NO", "NM_CAR" }, new string[] { "CAR_NO", "NM_CAR" });
            _flex.SetCodeHelpCol("NM_SHIPPER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "SHIPPER", "NM_SHIPPER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            //_flex.SetCodeHelpCol("NM_REAL_LOADING_PLACE", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "REAL_LOADING_PLACE", "NM_REAL_LOADING_PLACE" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            //_flex.SetCodeHelpCol("NM_LOADING_PLACE", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "LOADING_PLACE", "NM_LOADING_PLACE" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            //_flex.SetCodeHelpCol("NM_ALIGHT_PLACE", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "ALIGHT_PLACE", "NM_ALIGHT_PLACE" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

            _flex.SetCodeHelpCol("NM_REAL_LOADING_PLACE", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "REAL_LOADING_PLACE", "NM_REAL_LOADING_PLACE" }, new string[] { "PLACE_CODE", "PLACE_NAME" });
            _flex.SetCodeHelpCol("NM_LOADING_PLACE", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "LOADING_PLACE", "NM_LOADING_PLACE" }, new string[] { "PLACE_CODE", "PLACE_NAME" });
            _flex.SetCodeHelpCol("NM_ALIGHT_PLACE", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "ALIGHT_PLACE", "NM_ALIGHT_PLACE" }, new string[] { "PLACE_CODE", "PLACE_NAME" });


            _flex.SetDataMap("STANDARD_PLACE", MA.GetCode("CZ_Z000001"), "CODE", "NAME");



            _flex.AddRow += new EventHandler(OnToolBarAddButtonClicked);
            //_flex.ValidateEdit += new ValidateEditEventHandler(FlexL_ValidateEdit);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);


            //_flex.Cols.Frozen = 3;

        }

        private void InitEvent()
        {
            // 컨트롤 이벤트

        }

        protected override void InitPaint()
        {
            base.InitPaint();

            //dtpFROM.Mask = dtpTO.Mask = GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            //dtpFROM.Text = Global.MainFrame.GetStringFirstDayInMonth;
           // dtpTO.Text = Global.MainFrame.GetStringToday;

           // SetControl str = new SetControl();
            //str.SetCombobox(cbo01, MF.GetCode(MF.코드.생산.공정경로번호, true));  // ModuleHelper에 정의된 코드가 있으면..
            //str.SetCombobox(cbo01, MF.GetCode("MA_PLANT", true));           // ModuleHelper에 정의된 코드가 없으면..
        }

        #endregion

        #region ♪ 메인 버튼     ♬

        protected override bool BeforeSearch()
        {
            //if (!base.BeforeSearch() || !DropDownComboBoxCheck || !DatePickerCheck) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;


                object[] obj = new object[] { LoginInfo.CompanyCode, ctx화주.CodeValue, ctx상차지.Text, ctx하차지.Text, ctx실상차지.Text };

                DataTable dt = _biz.Search(obj);

                _flex.Binding = dt;

                //_flex.UnBinding = dt;

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
                if (!BeforeAdd()) return;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;
                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;

                _flex[_flex.Row, "CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;

                //_flex[_flex.Row, "CD_COMPANY"] = "1000";
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
            if (!base.SaveData() || !Verify()) return false;

            DataTable dt = _flex.GetChanges();

            _biz.Save(dt);
            _flex.AcceptChanges();
            return true;
        }

        #endregion

        #region ♪ 그리드 이벤트 ♬

        #endregion

        #region ♪ 기타 이벤트   ♬

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                ctx하차지.Text = ctx하차지.Text = "";
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
                        //e.HelpParam.P09_CD_PLANT = D.GetString(cbo01.SelectedValue);
                        //e.HelpParam.ResultMode = ResultMode.SlowMode;
                        break;

                    default:
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
                        ctx하차지.Text = ctx하차지.Text = "";
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
                    case "CAR_NO":
                        string CD_VEHICLE = D.GetString(_flex["CAR_NO"]);
                        string NM_VEHICLE = string.Empty;
                        e.Parameter.UserParams = "차량도움창;H_CAR_EQUIP_SUB;" + CD_VEHICLE + ";" + NM_VEHICLE;
                        break;

                    //case "NM_SHIPPER":
                    //    //string NAME = D.GetString(_flex["NM_SHIPPER"]);
                    //    //string NM_MAN = string.Empty;
                    //    e.Parameter.UserParams = "H_CZ_DELIVERY_MAN_SUB;"; // +CD_MAN + ";" + NM_MAN;
                    //    break;

                    case "NM_REAL_LOADING_PLACE":
                        string PRICE = string.Empty;
                        e.Parameter.UserParams = "실상차지도움창;H_CZ_CAR_REAL_PRICE;" + PRICE;
                        break;

                    case "NM_LOADING_PLACE":
                        string PRICE2 = string.Empty;
                        e.Parameter.UserParams = "상차지도움창;H_CZ_CAR_REAL_PRICE;" + PRICE2;
                        break;

                    case "NM_ALIGHT_PLACE":
                        string PRICE3 = string.Empty;
                        e.Parameter.UserParams = "하차지도움창;H_CZ_CAR_REAL_PRICE;" + PRICE3;
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

        private void ctx상차지_TextChanged(object sender, EventArgs e)
        {

        }

        #region ♪ 속성          ♬

        //bool DropDownComboBoxCheck { get { return !Checker.IsEmpty(cbo01, DD("")); } }
        //bool DatePickerCheck { get { return Checker.IsValid(dtpFROM, dtpTO, true, DD(""), DD("")); } }

        #endregion
    }
}