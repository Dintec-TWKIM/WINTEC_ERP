/**
 * 
 * 배차관리 시스템 ver1.0 
 * 1. 그룹 : 배차관리
 * 2. 제목 : 배차관리
 * 3. 상세설명 : 배차관리 시스템
 * 4. 작성일자 : 2014-02-23
 * autor by Duzon
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace cz
{

    public partial class P_CZ_CAR_INTERVALS : PageBase
    {
        

        public P_CZ_CAR_INTERVALS()
        {
            InitializeComponent();
            this.MainGrids = new FlexGrid[] { _flexM, _flexD };
            _flexM.DetailGrids = new FlexGrid[] { _flexD };
            DataChanged += new EventHandler(Page_DataChanged);
        }

        #region ♪ 초기화        ♬

        //초기화
        P_CZ_CAR_INTERVALS_BIZ _biz = new P_CZ_CAR_INTERVALS_BIZ();

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitGridL();
            this.InitPaint();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            SetControl set = new SetControl();

            set.SetCombobox(cbo처리유형, MA.GetCode("CZ_Z000010", true));
            set.SetCombobox(cbo상태, MA.GetCode("CZ_Z000011", true));
            set.SetCombobox(cbo배차상태, MA.GetCode("CZ_Z000012", true));


            dtpFROM.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtpTO.Text = Global.MainFrame.GetStringToday;

            dtpFROM2.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtpTO2.Text = Global.MainFrame.GetStringToday;
            

        }

        private void InitGrid()
        {

            // 상단 그리드
            _flexM.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flexM.SetCol("GUBUNNM", "처리유형명", 100, false, typeof(string));
            //_flexM.SetCol("TOTAL_QT_GIR", "총 수량", 90, false, typeof(decimal));
            //_flexM.SetCol("QT_GIR", "수량", 90, false, typeof(decimal));
            _flexM.SetCol("TOTAL_QT_GIR", "총 의뢰량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexM.SetCol("CD_PARTNER", "거래처", 50, true, typeof(string));
            _flexM.SetCol("LN_PARTNER", "거래처명", 150, false, typeof(string));
            _flexM.SetCol("REAL_LOADING_PLACE", "실상차지", 100);
            _flexM.SetCol("LOADING_PLACE", "상차지", 150);
            _flexM.SetCol("ALIGHT_PLACE", "하차지", 180);

            _flexM.SetCol("SHIPPER", "화주", 80, true, typeof(string));
            _flexM.SetCol("LN_SHIPPER", "화주명", 150, false, typeof(string));
            _flexM.SetCol("REQ_PRICE", "청구단가", 80, true, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("REQ_UNIT_PRICE", "청구 대당단가", 150, true, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("PROVIDE_PRICE", "지급단가", 130, true, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("UNIT_PRICE", "대당단가", 150, true, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("STANDARD_PLACE", "상하차기준", 80, true, typeof(string));

            _flexM.SetCol("CD_ITEM", "품목코드", 80, true, typeof(string));
            _flexM.SetCol("NM_ITEM", "품목명", 130, false, typeof(string));

            _flexM.SetCol("UM", "단가", 130, false, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("AM_EX", "단가금액", 150, false, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("DT_DUEDATE", "납품일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("CD_COMPANY", "회사", 50, false, typeof(string));
            _flexM.SetCol("NM_COMPANY", "회사명", 130, false, typeof(string));
            _flexM.SetCol("ID_INSERT_NM", "입력자", 100, false, typeof(string));
            //_flexM.SetCol("DTS_INSERT", "생성일자", 100, false, typeof(string));
            _flexM.SetCol("NO_GIR", "의뢰번호", 100 ,false, typeof(string));
            _flexM.SetCol("LINE", "항차", 100);
            _flexM.SetCol("GUBUN", "구분", 50);
            _flexM.SetCol("DC_RMK", "비고(상차지)", 200);
            _flexM.SetCol("DC_RMK2", "비고1(시간)", 200);
            
            _flexM.Cols["TOTAL_QT_GIR"].Format = "0.00";
            //_flexM.Cols["QT_GIR"].Format = "0.00";
            _flexM.Cols["UM"].Format = "0.00";
            _flexM.Cols["AM_EX"].Format = "0.00";
            _flexM.Cols["REQ_PRICE"].Format = "0.00";
            _flexM.Cols["REQ_UNIT_PRICE"].Format = "0.00";
            _flexM.Cols["PROVIDE_PRICE"].Format = "0.00";
            _flexM.Cols["UNIT_PRICE"].Format = "0.00";

            _flexM.Cols["TOTAL_QT_GIR"].Style.ForeColor = Color.Red;
            _flexM.Cols["GUBUNNM"].TextAlign = TextAlignEnum.CenterCenter;
            _flexM.Cols["LN_PARTNER"].TextAlign = TextAlignEnum.LeftCenter;
            //_flexM.Cols["TOTAL_QT_GIR"].TextAlign = TextAlignEnum.RightCenter;
            //_flexM.Cols["QT_GIR"].TextAlign = TextAlignEnum.RightCenter;

            _flexM.Cols["NM_ITEM"].TextAlign = TextAlignEnum.LeftCenter;
            _flexM.Cols["UM"].TextAlign = TextAlignEnum.RightCenter;
            _flexM.Cols["AM_EX"].TextAlign = TextAlignEnum.RightCenter;
            _flexM.Cols["DT_GIR"].TextAlign = TextAlignEnum.RightCenter;
            _flexM.Cols["DT_DUEDATE"].TextAlign = TextAlignEnum.RightCenter;
            _flexM.Cols["ID_INSERT_NM"].TextAlign = TextAlignEnum.LeftCenter;

            _flexM.Cols["CD_PARTNER"].Visible = false;
            _flexM.Cols["CD_COMPANY"].Visible = false;
            _flexM.Cols["NM_COMPANY"].Visible = false;
            _flexM.Cols["CD_ITEM"].Visible = false;
            //_flexM.Cols["DTS_INSERT"].Visible = false;
            _flexM.Cols["NO_GIR"].Visible = false;
            _flexM.Cols["LINE"].Visible = false;
            _flexM.Cols["GUBUN"].Visible = false;



            _flexM.VerifyAutoDelete = new string[] { "CD_PARTNER" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flexM.VerifyNotNull = new string[] { "CD_PARTNER", "CD_ITEM" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.


            this._flexM.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            this._flexM.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_ITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
            this._flexM.SetCodeHelpCol("SHIPPER", "H_CZ_HELP_CAR_PRICE", ShowHelpEnum.Always, new string[] { "SHIPPER", "LN_SHIPPER", "REQ_PRICE", "REQ_UNIT_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE" }, new string[] { "SHIPPER", "LN_SHIPPER", "REQ_PRICE", "REQ_UNIT_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE" });
            _flexM.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);


            _flexM.SetDataMap("STANDARD_PLACE", MA.GetCode("CZ_Z000001"), "CODE", "NAME");

            _flexM.SettingVersion = "0.0.0.1";
            _flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexM.AddRow += new EventHandler(OnToolBarAddButtonClicked);

            _flexM.Cols.Frozen = 1;
        }

        private void InitGridL()
        {

            // 하단 그리드
            _flexD.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flexD.SetCol("TOTAL_QT_GIR", "남은 수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_GIR", "배차량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("CAR_NO", "차량번호", 130, false, typeof(string));
            _flexD.SetCol("NM_CAR", "차량명", 180, false, typeof(string));
            _flexD.SetCol("NO_EMP", "운전자", 80, false, typeof(string));
            _flexD.SetCol("SHIPPER", "화주", 80, false, typeof(string));
            _flexD.SetCol("LN_SHIPPER", "화주명", 150, false, typeof(string));

            _flexD.SetCol("REQ_PRICE", "청구단가", 80, true, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("REQ_UNIT_PRICE", "청구 대당단가", 150, true, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("PROVIDE_PRICE", "지급단가", 130, true, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("UNIT_PRICE", "지급 대당단가", 150, true, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("ESTIMATE_PRICE", "계근비", 80, true, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("FREIGHT_CHARGE_A", "운임1", 130, false, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("FREIGHT_CHARGE_B", "운임2", 130, false, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("ESTIMATE_QTY", "계근량", 130, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("LOADING_QTY", "상차량", 130, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("ALIGHT_QTY", "하차량", 130, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("STANDARD_PLACE", "상하차기준", 80, true, typeof(string));
            _flexD.SetCol("FLAG", "처리상황", 130, true, typeof(string));
            _flexD.SetCol("ORDER_TIME", "배차지시 시간", 130, true, typeof(string));
            _flexD.SetCol("DC_RMK", "비고", 180, true, typeof(string));
            _flexD.SetCol("INSERT_DATE", "생성일자", 100, false, typeof(string));

            _flexD.SetCol("REAL_LOADING_PLACE", "실상차지", 100, false, typeof(string));
            _flexD.SetCol("LOADING_PLACE", "상차지", 130, false, typeof(string));
            _flexD.SetCol("ALIGHT_PLACE", "하차지", 130, false, typeof(string));
            _flexD.SetCol("QT_GIR_DET", "의뢰량", 130, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("CD_COMPANY", "회사", 130, true, typeof(string));

            //_flexD.SetCol("NM_REAL_LOADING_PLACE", "실상차지명", 130, false, typeof(string));
            //_flexD.SetCol("NM_LOADING_PLACE", "상차지명", 130, false, typeof(string));
            //_flexD.SetCol("NM_ALIGHT_PLACE", "하차지명", 130, false, typeof(string));


            _flexD.Cols["TOTAL_QT_GIR"].Style.ForeColor = Color.Blue;
            _flexD.Cols["TOTAL_QT_GIR"].Format = "0.00";

            _flexD.Cols["QT_GIR"].Format = "0.00";
            _flexD.Cols["QT_GIR"].Style.ForeColor = Color.Red;
            _flexD.Cols["STANDARD_PLACE"].Style.ForeColor = Color.DarkRed;

            _flexD.Cols["ORDER_TIME"].Style.ForeColor = Color.DarkRed;

            //_flexD.Cols["REQ_PRICE"].Format = "0.00";
            //_flexD.Cols["PROVIDE_PRICE"].Format = "0.00";
            //_flexD.Cols["UNIT_PRICE"].Format = "0.00";
            //_flexD.Cols["REQ_UNIT_PRICE"].Format = "0.00";
            //_flexD.Cols["ESTIMATE_PRICE"].Format = "0.00";
            

            _flexD.Cols["ESTIMATE_QTY"].Format = "0.00";
            _flexD.Cols["ESTIMATE_QTY"].Style.ForeColor = Color.Red;
            _flexD.Cols["ESTIMATE_QTY"].Style.ForeColor = Color.DarkRed;

            _flexD.Cols["FREIGHT_CHARGE_A"].Format = "0.00";
            _flexD.Cols["FREIGHT_CHARGE_B"].Format = "0.00";

            _flexD.Cols["CAR_NO"].TextAlign = TextAlignEnum.LeftCenter;
            _flexD.Cols["NM_CAR"].TextAlign = TextAlignEnum.LeftCenter;
            _flexD.Cols["SHIPPER"].TextAlign = TextAlignEnum.LeftCenter;
            _flexD.Cols["LN_SHIPPER"].TextAlign = TextAlignEnum.LeftCenter;
            _flexD.Cols["REAL_LOADING_PLACE"].TextAlign = TextAlignEnum.LeftCenter;
            _flexD.Cols["LOADING_PLACE"].TextAlign = TextAlignEnum.LeftCenter;
            _flexD.Cols["ALIGHT_PLACE"].TextAlign = TextAlignEnum.LeftCenter;
            _flexD.Cols["REQ_PRICE"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["REQ_UNIT_PRICE"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["PROVIDE_PRICE"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["UNIT_PRICE"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["ESTIMATE_PRICE"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["FREIGHT_CHARGE_A"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["FREIGHT_CHARGE_B"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["FLAG"].TextAlign = TextAlignEnum.CenterCenter;
            _flexD.Cols["LOADING_QTY"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["ALIGHT_QTY"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["ORDER_TIME"].TextAlign = TextAlignEnum.RightCenter;
            _flexD.Cols["QT_GIR_DET"].TextAlign = TextAlignEnum.RightCenter;


            //_flexD.VerifyPrimaryKey = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.
            _flexD.VerifyAutoDelete = new string[] { "NO_GIR", "LINE","CAR_SEQ" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flexD.VerifyNotNull = new string[] { "NO_GIR", "LINE", "CAR_NO","ORDER_TIME" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.

            //팝업함수정보가 필요
            this._flexD.SetCodeHelpCol("CAR_NO", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "CAR_NO", "NM_CAR", "NO_EMP" }, new string[] { "CAR_NO", "NM_CAR", "NO_EMP" });
            //this._flexD.SetCodeHelpCol("SHIPPER", "H_CZ_HELP_CAR_PRICE", ShowHelpEnum.Always, new string[] { "SHIPPER", "LN_SHIPPER", "NM_REAL_LOADING_PLACE", "NM_LOADING_PLACE", "NM_ALIGHT_PLACE", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE" }, new string[] { "SHIPPER", "LN_SHIPPER", "NM_REAL_LOADING_PLACE", "NM_LOADING_PLACE", "NM_ALIGHT_PLACE", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE" });
            //this._flexD.SetCodeHelpCol("SHIPPER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "SHIPPER", "LN_SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE" }, new string[] { "SHIPPER", "LN_SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE" });

         

            _flexD.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flexD.SetDataMap("FLAG", MA.GetCode("CZ_Z000012"), "CODE", "NAME");
            _flexD.SetDataMap("STANDARD_PLACE", MA.GetCode("CZ_Z000001"), "CODE", "NAME");
            _flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flexD.SettingVersion = "0.0.0.1";
            _flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            //_flexD.Cols.Frozen = 1;

        }

        private void InitEvent()
        {
            //그리드이벤트
            btn_추가.Click += new EventHandler(btn_추가_Click);
            btn_삭제.Click += new EventHandler(btn_삭제_Click);

            _flexM.AfterRowChange += new RangeEventHandler(_flex_M_AfterRowChange);

        }

        #endregion

        #region ♪ 메인 버튼     ♬

        protected override bool BeforeSearch()
        {
 	        if (!base.BeforeSearch()) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                //MessageBox.Show(D.GetString(cbo상태.SelectedValue));
                _flexM.Binding = _biz.Search(D.GetString(cbo처리유형.SelectedValue), bpC상차지.Text, dtpFROM.Text, dtpTO.Text, dtpFROM2.Text, dtpTO2.Text, ctx상호.CodeValue, ctx의뢰번호.Text, D.GetString(cbo상태.SelectedValue), Global.MainFrame.LoginInfo.CompanyCode);

                if (!_flexM.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);

                //_flexM.AcceptChanges();
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

                _flexM.Rows.Add();
                _flexM.Row = _flexM.Rows.Count - _flexM.Rows.Fixed;
                _flexM["GUBUNNM"] = "자체배차";
                _flexM["GUBUN"] = "103";
                _flexM["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                _flexM["NM_COMPANY"] = Global.MainFrame.LoginInfo.CompanyName;
                _flexM["DT_GIR"] = Global.MainFrame.GetStringToday;
                _flexM["DT_DUEDATE"] = Global.MainFrame.GetStringToday;

                //_flexM.AddFinished();
                _flexM.Focus();
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

                if (D.GetString(_flexM["GUBUN"]) != "103")
                {
                    if (D.GetDecimal(_flexD["QT_GIR"]) == 0)
                    {
                        ShowMessage("배차량을 입력바랍니다.");
                        return;
                    }
                }


                if (MsgAndSave(PageActionMode.Save))
                    //ShowMessage(PageResultMode.SaveGood);
                    ShowMessage("자료가 정상처리되었습니다");
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
                if (!BeforeDelete() || !_flexM.HasNormalRow) return;

                DataRow[] dr = _flexM.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (D.GetString(_flexM["GUBUN"]) != "103")
                {
                    ShowMessage("자체배차 이외에는 삭제 불가합니다.");
                    return;
                }


                DialogResult result = ShowMessage(공통메세지.자료를삭제하시겠습니까, PageName);
                if (result == DialogResult.Yes)
                {
                    foreach (DataRow row in dr)
                    {
                        _biz.DeleteD(D.GetString(row["NO_GIR"]), D.GetString(row["LINE"]));
                    }

                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    //OnToolBarSearchButtonClicked(sender, e);

                    _flexM.Binding = _biz.Search(D.GetString(cbo처리유형.SelectedValue), bpC상차지.Text, dtpFROM.Text, dtpTO.Text, dtpFROM2.Text, dtpTO2.Text, ctx상호.CodeValue, ctx의뢰번호.Text, D.GetString(cbo상태.SelectedValue), Global.MainFrame.LoginInfo.CompanyCode);

                    if (!_flexM.HasNormalRow)
                        ShowMessage(PageResultMode.SearchNoData);
                }


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


            if (D.GetString(_flexD["STANDARD_PLACE"]) != "")
            {
                if (D.GetString(_flexD["STANDARD_PLACE"]) == "001")
                {
                    _flexD["CONFIRM_QTY"] = _flexD["LOADING_QTY"];
                }
                else
                {
                    _flexD["CONFIRM_QTY"] = _flexD["ALIGHT_QTY"];
                }
            }

            if (!base.SaveData() || !Verify()) return false;

            if (D.GetString(_flexM["GUBUN"]) != "103")
            {
                _biz.SaveD(_flexD.GetChanges());

                _flexD.AcceptChanges();
            }
            else
            {

                _biz.Save(_flexM.GetChanges(), _flexD.GetChanges());


                _flexM.AcceptChanges();
                _flexD.AcceptChanges();
            }


            _flexM.Binding = _biz.Search(D.GetString(cbo처리유형.SelectedValue), bpC상차지.Text, dtpFROM.Text, dtpTO.Text, dtpFROM2.Text, dtpTO2.Text, ctx상호.CodeValue, ctx의뢰번호.Text, D.GetString(cbo상태.SelectedValue), Global.MainFrame.LoginInfo.CompanyCode);

            if (!_flexM.HasNormalRow)
                ShowMessage(PageResultMode.SearchNoData);

            return true;
        }

        #endregion

        #region ♪ 그리드 이벤트 ♬

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {


                //if (D.GetDecimal(_flexD["QT_GIR"]) > D.GetDecimal(_flexD["QT_GIR_DET"]))
                //{
                //    ShowMessage("의뢰량이 헤더의 의뢰량을 초과합니다.");
                //    _flexD["QT_GIR"] = "";
                //    return;
                //}

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                ToolBarSaveButtonEnabled = IsChanged();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        //하단 그리드 조회
        void _flex_M_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {

                string NO_GIR = D.GetString(_flexM["NO_GIR"]);
                string LINE = D.GetString(_flexM["LINE"]);
                string GUBUN = D.GetString(_flexM["GUBUN"]);

                string Filter = "NO_GIR = '" + NO_GIR + "'";
                //string Filter = "NO_GIR = '" + NO_GIR + "'" +  "AND LINE = '" + LINE + "'";
                if (_flexM.DetailQueryNeed)

                {
                    _flexD.Redraw = false; //종 그리드의 데이터를 화면에 그리는 것을 UnVisible시킨다
                    DataTable dt = _biz.SearchL(NO_GIR, cbo배차상태.SelectedValue.ToString(), GUBUN, LINE);
                    _flexD.BindingAdd(dt, Filter);
                    _flexD.Redraw = true; //종 그리드의 데이터를 화면에 그리는 것을 UnVisible시킨다

                }

                _flexD.RowFilter = Filter;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♪ 기타 이벤트   ♬

        #endregion

        #region ♪ 기타 메소드   ♬

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {

            try
            {
                switch (_flexD.Cols[e.Col].Name)
                {

                    /*case "SHIPPER":

                        e.Parameter.UserParams = "구간별 단가 도움창;P_MA_PARTNER_SUB;";
                        break;
                    */

                    case "CAR_NO":

                        string CAR_NO = D.GetString(_flexD["CAR_NO"]);

                        e.Parameter.UserParams = "차량정보 도움창;H_CZ_CAR_INTERVALS;" + CAR_NO;
                        break;

                    default:
                        break;

                }

                switch (_flexM.Cols[e.Col].Name)
                {
                    case "SHIPPER":
                        string REAL_LOADING_PLACE = D.GetString(_flexM["REAL_LOADING_PLACE"]);
                       string LN_PARTNER = D.GetString(_flexM["LN_PARTNER"]);

                        //e.Parameter.UserParams = "구간별 단가 도움창;H_CZ_CAR_PRICE_S;" + REAL_LOADING_PLACE + ";" + LN_PARTNER;
                        //MessageBox.Show(REAL_LOADING_PLACE);
                       e.Parameter.UserParams = "구간별 단가 도움창;H_CZ_CAR_PRICE_S;" + REAL_LOADING_PLACE + ";" + LN_PARTNER;
                        break;

                    /*case "SHIPPER":

                        e.Parameter.UserParams = "구간별 단가 도움창;P_MA_PARTNER_SUB;";
                        break;
                    */

             

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #region 화면버튼


        void btn_추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow) return;

                if (D.GetString(_flexM["GUBUN"]) != "103")
                {
                    if (D.GetDecimal(_flexM["QT_GIR"]) == 0)
                    {
                        ShowMessage("헤더의 의뢰량이 0입니다. 추가 할 수 없습니다..");
                        return;
                    }
                }

                if (D.GetString(cbo상태.SelectedValue) == "1")
                {
                    ShowMessage("완결 상태에서는 추가 할 수 없습니다.");
                    return;
                }

                if (D.GetString(_flexM["SHIPPER"]) == "")
                {
                    ShowMessage("상단의 화주를 먼저 선택 바랍니다.");
                    return;
                }


                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - _flexD.Rows.Fixed;

                _flexD["NO_GIR"] = _flexM["NO_GIR"];
                _flexD["LINE"] = _flexM["LINE"];

                _flexD["FLAG"] = "100";
                _flexD["QT_GIR_DET"] = _flexM["QT_GIR"];
                _flexD["QT_GIR"] = "0";
                _flexD["TOTAL_QT_GIR"] = D.GetDecimal(_flexM["TOTAL_QT_GIR"])   - D.GetDecimal(_flexD["QT_GIR"]);
                _flexD["ORDER_TIME"] = D.GetString(_flexM["DC_RMK2"]);
                _flexD["SHIPPER"] = D.GetString(_flexM["SHIPPER"]);
                _flexD["LN_SHIPPER"] = D.GetString(_flexM["LN_SHIPPER"]);
                _flexD["REQ_PRICE"] = D.GetDecimal(_flexM["REQ_PRICE"]);
                _flexD["PROVIDE_PRICE"] = D.GetDecimal(_flexM["PROVIDE_PRICE"]);
                _flexD["CD_COMPANY"] = D.GetString(_flexM["CD_COMPANY"]);
                _flexD["REQ_UNIT_PRICE"] = D.GetDecimal(_flexM["REQ_UNIT_PRICE"]);
                _flexD["UNIT_PRICE"] = D.GetDecimal(_flexM["UNIT_PRICE"]);
                _flexD["REAL_LOADING_PLACE"] = D.GetString(_flexM["REAL_LOADING_PLACE"]);
                _flexD["LOADING_PLACE"] = D.GetString(_flexM["LOADING_PLACE"]);
                _flexD["ALIGHT_PLACE"] = D.GetString(_flexM["ALIGHT_PLACE"]);
                _flexD["STANDARD_PLACE"] = D.GetString(_flexM["STANDARD_PLACE"]);

                _flexD.Cols["REQ_UNIT_PRICE"].Format = "0.00";



                

                _flexD.AddFinished();
                _flexD.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        void btn_삭제_Click(object sender, EventArgs e)
        {

            //if (!BeforeDelete() || !_flexD.HasNormalRow) return;

            //DataRow[] dr = _flexD.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

            //if (dr == null || dr.Length == 0)
            //{
            //    ShowMessage(공통메세지.선택된자료가없습니다);
            //    return;
            //}


            //DialogResult result = ShowMessage(공통메세지.자료를삭제하시겠습니까, PageName);
            //if (result == DialogResult.Yes)
            //{
            //    foreach (DataRow row in dr)
            //    {
            //        _biz.Delete(D.GetString(row["NO_GIR"]), D.GetString(row["LINE"]), D.GetString(row["CAR_SEQ"]));
            //    }

            //    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
            //    //OnToolBarSearchButtonClicked(sender, e);

            //    _flexM.Binding = _biz.Search(D.GetString(cbo처리유형.SelectedValue), bpC상차지.Text, dtpFROM.Text, dtpTO.Text, dtpFROM2.Text, dtpTO2.Text, ctx상호.CodeValue, ctx의뢰번호.Text, D.GetString(cbo상태.SelectedValue), Global.MainFrame.LoginInfo.CompanyCode);

            //    if (!_flexM.HasNormalRow)
            //        ShowMessage(PageResultMode.SearchNoData);

            //}

            DataRow[] dr = _flexD.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

            if (dr == null || dr.Length == 0)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return;
            }

            try
            {
                if (!_flexD.HasNormalRow) return;
                _flexD.Rows.Remove(_flexD.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        private void _flexD_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region ♪ 속성          ♬

        #endregion
    }
}