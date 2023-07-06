//********************************************************************
// 작   성   자 : 장은경
// 작   성   일 : 2010.06.29
// 수   정   자 : 
// 수   정   일 : 
// 모   듈   명 : 영업
// 시 스  템 명 : 견적관리
// 서브시스템명 : 
// 페 이 지  명 : 견적등록
// 프로젝트  명 : 
//********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.EMail;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;
using Duzon.Windows.Print;
using System.Collections;
using Duzon.ERPU.OLD;

namespace sale
{
    public partial class P_SA_ESTMT_REG_NEW : PageBase
    {
        #region ♣ 생성자 & 변수선언

        P_SA_ESTMT_REG_BIZ _biz = new P_SA_ESTMT_REG_BIZ();
        public P_SA_ESTMT_REG_NEW()
        {
            InitializeComponent();
            MainGrids = new FlexGrid[] { _flexH, _flexD };
            _flexH.DetailGrids = new FlexGrid[] { _flexD };
            DataChanged += new EventHandler(Page_DataChanged);
        }

        #endregion

        #region ♣ 초기화

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            switch (서버키)
            {
                case "INNOWL":

                    pnl공통5.Visible = true;

                    cbo코드_사용자정의_1.Tag = "";
                    cbo코드_사용자정의_2.Tag = "";

                    cbo기준환종.Tag = "CD_USERDEF1";
                    cbo할인구분.Tag = "CD_USERDEF2";

                    lbl비고1.Text = DD("통합견적등록비고1");
                    lbl비고2.Text = DD("통합견적등록비고2");
                    lbl비고3.Text = DD("통합견적등록비고3");
                    lbl비고4.Text = DD("통합견적등록비고4");
                    lbl비고5.Text = DD("통합견적등록비고5");
                    lbl비고6.Text = DD("통합견적등록비고6");
                    lbl비고7.Text = DD("통합견적등록비고7");
                    break;
            }

            InitGrid();
            InitEvent();
        }

        #endregion

        #region -> InitGrid

        void InitGrid()
        {

            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("DT_EST", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("NO_EST", "견적번호", 100);
            _flexH.SetCol("NO_EST_NM", "견적명", 140);
            _flexH.SetCol("NO_HST", "차수", 40, false, typeof(decimal), FormatTpType.QUANTITY); _flexH.Cols["NO_HST"].Format = "###";
            _flexH.SetCol("STA_EST", "확정여부", 80);
            _flexH.SetCol("NO_SO", "적용수주번호", 100);
            //_flexH.SetBinding(new PanelExt[] { pnl공통1, pnl공통2, pnl공통3, pnl공통4, pnl공통5, pnl해외, pnl비고, pnl기타 }, null);
            _flexH.SetOneGridBinding(new object[] { }, new Duzon.Erpiu.ComponentModel.IUParentControl[] { pnl공통1, pnl공통2, pnl공통3, pnl공통4, pnl공통5, pnl해외, pnl비고, pnl기타 });

            _flexH.BeforeRowChange += new RangeEventHandler(_flexH_BeforeRowChange);
            _flexH.AfterRowChange += new RangeEventHandler(_flexH_AfterRowChange);
            _flexH.SetDummyColumn("S", "TP_BUSI");

            _flexH.VerifyPrimaryKey = new string[] { "NO_EST", "NO_HST" };
            _flexH.SettingVersion = "1.0.0.3";
            _flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexD.BeginSetting(1, 1, true);
            _flexD.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flexD.SetCol("NO_EST", "견적번호", false);
            _flexD.SetCol("NO_HST", "차수", false);
            _flexD.SetCol("SEQ_EST", "견적차수", false);
            _flexD.SetCol("CD_PLANT", "공장코드", 80, false);
            _flexD.SetCol("NM_PLANT", "공장명", 100, false);
            _flexD.SetCol("CD_ITEM", "품목코드", 100, true);
            _flexD.SetCol("NM_ITEM", "품목명", 120, false);
            _flexD.SetCol("STND_ITEM", "규격", 65, false);
            _flexD.SetCol("STND_DETAIL_ITEM", "세부규격", 120, false);
            _flexD.SetCol("UNIT_SO", "단위", 65, false);
            _flexD.SetCol("DT_DUEDATE", "납기일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexD.SetCol("QT_EST", "수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("UM_STD", "견적기준단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexD.SetCol("RT_DC", "할인율(%)", 80, true, typeof(decimal)); _flexD.Cols["RT_DC"].Format = "###.##";
            _flexD.SetCol("UM_EST", "견적단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexD.SetCol("AM_EST", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexD.SetCol("AM_K_EST", "견적원화금액", 100, true, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("AM_VAT", "견적부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("AM_TOT", "총액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("GRP_ITEM", "품목군코드", false);
            _flexD.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_PO", "기발주량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            if (_biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
            {
                _flexD.SetCol("UM_INV", "재고단가", 100, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
                _flexD.SetCol("AM_PROFIT", "예상이익", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            }

            _flexD.SetCol("NM_USERDEF1", "사용자정의1", 120, true); _flexD.Cols["NM_USERDEF1"].Visible = false;
            _flexD.SetCol("NM_USERDEF2", "사용자정의2", 100, true); _flexD.Cols["NM_USERDEF2"].Visible = false;
            _flexD.SetCol("NM_USERDEF3", "사용자정의3", 100, true); _flexD.Cols["NM_USERDEF3"].Visible = false;

            _flexD.SetCol("NUM_USERDEF1", "사용자정의4", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexD.Cols["NUM_USERDEF1"].Visible = false;
            _flexD.SetCol("NUM_USERDEF2", "사용자정의5", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexD.Cols["NUM_USERDEF2"].Visible = false;

            _flexD.SetExceptEditCol("NM_PLANT", "NO_EST", "NO_HST", "SEQ_EST", "NM_ITEM", "STND_ITEM", "UNIT_SO", "GRP_ITEM");
            _flexD.SetCodeHelpCol("CD_PLANT", Duzon.Common.Forms.Help.HelpID.P_MA_PLANT_SUB, ShowHelpEnum.Always, new string[] { "CD_PLANT", "NM_PLANT" }, new string[] { "CD_PLANT", "NM_PLANT" }, new string[] { "CD_PLANT", "NM_PLANT", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "QT_EST", "UM_STD", "RT_DC", "UM_EST", "AM_EST", "AM_K_EST", "AM_VAT", "AM_TOT", "GRP_ITEM" }, Duzon.Common.Forms.Help.ResultMode.FastMode);
            _flexD.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "QT_EST", "UM_STD", "RT_DC", "UM_EST", "AM_EST", "AM_K_EST", "AM_VAT", "AM_TOT", "GRP_ITEM" }, Duzon.Common.Forms.Help.ResultMode.FastMode);

            _flexD.VerifyPrimaryKey = new string[] { "NO_EST", "NO_HST", "SEQ_EST" };
            _flexD.VerifyNotNull = new string[] { "CD_ITEM" };
            _flexD.VerifyCompare(_flexD.Cols["QT_EST"], 0, OperatorEnum.Greater);
            _flexD.SetDummyColumn("S");
            _flexD.StartEdit += new RowColEventHandler(_flexD_StartEdit);
            _flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flexD_BeforeCodeHelp);
            _flexD.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexD_AfterCodeHelp);
            _flexD.ValidateEdit += new ValidateEditEventHandler(_flexD_ValidateEdit);
            _flexD.HelpClick += new EventHandler(_flexD_HelpClick);

            _flexD.SettingVersion = "1.0.0.7";
            _flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexD.SetExceptSumCol("UM_STD", "RT_DC", "UM_EST");
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            pnl공통1.UseCustomLayout = true;
            pnl공통1.IsSearchControl = false;   //입력 전용
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl5.IsNecessaryCondition = true;
            bpPanelControl6.IsNecessaryCondition = true;
            bpPanelControl11.IsNecessaryCondition = true;
            bpPanelControl12.IsNecessaryCondition = true;
            bpPanelControl14.IsNecessaryCondition = true;
            pnl공통1.InitCustomLayout();

            pnl공통2.UseCustomLayout = true;
            pnl공통2.IsSearchControl = false;   //입력 전용
            bpPanelControl15.IsNecessaryCondition = true;
            bpPanelControl16.IsNecessaryCondition = true;
            bpPanelControl17.IsNecessaryCondition = true;
            bpPanelControl18.IsNecessaryCondition = true;
            pnl공통2.InitCustomLayout();

            pnl공통3.UseCustomLayout = true;
            pnl공통3.IsSearchControl = false;   //입력 전용
            bpPanelControl21.IsNecessaryCondition = true;
            pnl공통3.InitCustomLayout();

            pnl공통4.UseCustomLayout = true;
            pnl공통4.IsSearchControl = false;   //입력 전용
            pnl공통4.InitCustomLayout();

            pnl공통5.UseCustomLayout = true;
            pnl공통5.IsSearchControl = false;   //입력 전용
            pnl공통5.InitCustomLayout();

            pnl해외.UseCustomLayout = true;
            pnl해외.IsSearchControl = false;   //입력 전용
            pnl해외.InitCustomLayout();

            oneGrid3.UseCustomLayout = true;
            oneGrid3.IsSearchControl = false;   //입력 전용
            oneGrid3.InitCustomLayout();

            pnl기타.UseCustomLayout = true;
            pnl기타.IsSearchControl = false;   //입력 전용
            pnl기타.InitCustomLayout();

            if (BASIC.GetMAEXC("통합견적등록-메일기능사용") == "001") btn메일전송.Visible = true;

            //dtp견적일자F.Mask = dtp견적일자T.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtp견적일자.Mask = dtp유효일자국내.Mask = dtp납기일자.Mask = dtp확정일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtp유효일자해외.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);

            //dtp견적일자F.ToDayDate = dtp견적일자T.ToDayDate = Global.MainFrame.GetDateTimeToday();
            dtp견적일자.ToDayDate = dtp유효일자국내.ToDayDate = Global.MainFrame.GetDateTimeToday();
            dtp유효일자해외.ToDayDate = Global.MainFrame.GetDateTimeToday();

            //dtp견적일자F.Text = Global.MainFrame.GetStringFirstDayInMonth;
            //dtp견적일자T.Text = Global.MainFrame.GetStringToday;
            periodPicker1.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            periodPicker1.EndDateToString = Global.MainFrame.GetStringToday;

            SetControl str = new SetControl();
            str.SetCombobox(cbo상태, MF.GetCode(MF.코드.영업.견적관리.확정여부, true));
            str.SetCombobox(cbo환종, MA.GetCode("MA_B000005"));
            str.SetCombobox(cbo과세구분, MA.GetCode("MA_B000040"));
            str.SetCombobox(cbo부가세포함, MA.GetCode("YESNO"));
            str.SetCombobox(cbo결제방법, MA.GetCode("SA_B000002", true));
            str.SetCombobox(cbo확정여부, MF.GetCode(MF.코드.영업.견적관리.확정여부));
            str.SetCombobox(cbo결제형태, MA.GetCode("TR_IM00004", true));
            str.SetCombobox(cbo포장형태, MA.GetCode("TR_IM00011", true));
            str.SetCombobox(cbo운송방법, MA.GetCode("TR_IM00008", true));
            str.SetCombobox(cbo운송형태, MA.GetCode("TR_IM00009", true));
            str.SetCombobox(cbo원산지, MA.GetCode("MA_B000020", true));
            str.SetCombobox(cbo가격조건, MA.GetCode("TR_IM00002", true));

            str.SetCombobox(cbo코드_사용자정의_1, MA.GetCode("SA_B000090", true));
            str.SetCombobox(cbo코드_사용자정의_2, MA.GetCode("SA_B000091", true));
            str.SetCombobox(cbo코드_사용자정의_3, MA.GetCode("SA_B000092", true));
            str.SetCombobox(cbo코드_사용자정의_4, MA.GetCode("SA_B000093", true));
            str.SetCombobox(cbo코드_사용자정의_5, MA.GetCode("SA_B000094", true));
            str.SetCombobox(cbo코드_사용자정의_6, MA.GetCode("SA_B000095", true));

            if (서버키 == "INNOWL")
            {
                str.SetCombobox(cbo기준환종, MA.GetCodeUser(new string[] { "000", "001" }, new string[] { "원화", "외화" }));

                DataTable dt_temp = _biz.할인율코드();

                str.SetCombobox(cbo할인구분, dt_temp);
            }
            _flexH.SetDataMap("STA_EST", MF.GetCode(MF.코드.영업.견적관리.확정여부, true), "CODE", "NAME");

            사용자정의셋팅();

            Page_DataChanged(null, null);

            //switch (서버키)
            //{
            //    case "DZSQL":   //84번 개발서버
            //    case "LUXCO":   //럭스코
            //    case "WONIK":
            //    case "CIS":
            //        btn전자결재.Visible = true;
            //        break;
            //    default:
            //        break;
            //}
        }

        #endregion

        #region -> InitEvent

        void InitEvent()
        {
            btn차수추가.Click += new EventHandler(btn차수추가_Click);
            btn차수삭제.Click += new EventHandler(btn차수삭제_Click);
            btn추가.Click += new EventHandler(btn추가_Click);
            btn삭제.Click += new EventHandler(btn삭제_Click);
            btn확정.Click += new EventHandler(btn확정_Click);
            btn확정취소.Click += new EventHandler(btn확정취소_Click);
            btn할인율적용.Click += new EventHandler(btn할인율적용_Click);
            btn복사.Click += new EventHandler(btn복사_Click);
            btn엑셀업로드.Click += new EventHandler(btn엑셀업로드_Click);
            ctx수주형태.QueryBefore += new BpQueryHandler(BpControl_QueryBefore);
            ctx수주형태.QueryAfter += new BpQueryHandler(BpControl_QueryAfter);
            ctx수주형태.CodeChanged += new EventHandler(BpControl_CodeChanged);

            ctx담당자.QueryAfter += new BpQueryHandler(BpControl_QueryAfter);

            ctx거래처.QueryAfter += new BpQueryHandler(BpControl_QueryAfter);
            ctx거래처.CodeChanged += new EventHandler(BpControl_CodeChanged);

            ctx영업그룹.QueryAfter += new BpQueryHandler(BpControl_QueryAfter);
            ctx영업그룹.CodeChanged += new EventHandler(BpControl_CodeChanged);

            //ctx프로젝트.QueryBefore += new BpQueryHandler(BpControl_CodeChanged);

            cbo환종.SelectionChangeCommitted += new EventHandler(cbo환종_SelectionChangeCommitted);
            cbo과세구분.SelectionChangeCommitted += new EventHandler(cbo과세구분_SelectionChangeCommitted);

            tab.Selecting += new TabControlCancelEventHandler(tab_Selecting);
        }

        #endregion

        #endregion

        #region ♣ 메인버튼이벤트

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch() || !Chk견적일자) return false;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                List<string> list = new List<string>();
                list.Add(MA.Login.회사코드);
                list.Add(ctx거래처조회.CodeValue);
                list.Add(ctx영업그룹조회.CodeValue);
                list.Add(ctx담당자조회.CodeValue);
                list.Add(ctx견적번호.CodeValue);
                //list.Add(dtp견적일자F.Text);
                //list.Add(dtp견적일자T.Text);
                list.Add(periodPicker1.StartDateToString);
                list.Add(periodPicker1.EndDateToString);
                list.Add(D.GetString(cbo상태.SelectedValue));

                // 견적현황 프로시져와 동일
                _flexH.Binding = _biz.Search(list.ToArray());

                if (!_flexH.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd()) return false;

            if (_flexH.HasNormalRow && D.GetString(_flexH["NO_EST"]) == string.Empty) return false;

            if (!Verify_Grid(_flexH) || !VerifyH() || !Verify품목()) return false;
            return true;
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;
                ToolBarAddButtonEnabled = false;
                Application.DoEvents();

                string 견적번호 = (string)this.GetSeq(LoginInfo.CompanyCode, "SA", "01", Global.MainFrame.GetStringYearMonth);

                if (견적번호 == string.Empty) return;

                _flexH.Rows.Add();
                _flexH.Row = _flexH.Rows.Count - 1;
                _flexH["NO_EST"] = 견적번호;
                _flexH["NO_HST"] = 1;
                if (!ctx거래처조회.IsEmpty())
                {
                    _flexH["CD_PARTNER"] = ctx거래처조회.CodeValue;
                    _flexH["LN_PARTNER"] = ctx거래처조회.CodeName;
                }
                if (!ctx영업그룹조회.IsEmpty())
                {
                    _flexH["CD_SALEGRP"] = ctx영업그룹조회.CodeValue;
                    _flexH["NM_SALEGRP"] = ctx영업그룹조회.CodeName;
                }
                if (!ctx담당자조회.IsEmpty())
                {
                    _flexH["NO_EMP"] = ctx담당자조회.CodeValue;
                    _flexH["NM_KOR"] = ctx담당자조회.CodeName;
                    _flexH["CD_BIZAREA"] = Global.MainFrame.LoginInfo.BizAreaCode;
                    _flexH["NM_BIZAREA"] = Global.MainFrame.LoginInfo.BizAreaName;
                }

                _flexH.AddFinished();
                _flexH.Col = _flexH.Cols.Fixed;
                _flexH.Focus();
                txt견적번호.Focus();
                //txt견적명.Focus();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                ToolBarAddButtonEnabled = true;
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete() || !MsgAndSave(PageActionMode.Search)) return false;
            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !BeforeDelete()) return;

                string 견적번호 = D.GetString(_flexH["NO_EST"]);
                if (_flexH.GetDataRow(_flexH.Row).RowState != DataRowState.Added)
                {
                    //DataTable dt견적 = _flexH.DataView.ToTable(true, "NO_EST", "STA_EST", "NO_SO");
                    DataRow[] drs = _flexH.DataTable.Select("NO_EST = '" + 견적번호 + "'");

                    bool is수주적용 = false;
                    foreach (DataRow row견적 in drs)
                    {
                        if (D.GetString(row견적["NO_SO"]) != string.Empty)
                        {
                            is수주적용 = true;
                            break;
                        }
                    }

                    if (is수주적용)
                    {
                        ShowMessage("수주적용된 내용은 삭제하실 수 없습니다.");
                        return;
                    }

                    if (ShowMessage("견적번호 [" + 견적번호 + "] 자료를 삭제하시겠습니까?", "QY2") != DialogResult.Yes) return;

                    MsgControl.ShowMsg("자료를 삭제 중 입니다.\n잠시만 기다려 주십시요.");
                    _biz.Delete(견적번호);
                    MsgControl.CloseMsg();
                }
                else
                {
                    if (ShowMessage("견적번호 [" + 견적번호 + "] 자료를 삭제하시겠습니까?", "QY2") != DialogResult.Yes) return;
                }

                ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                _flexH.AcceptChanges();
                _flexD.AcceptChanges();
                OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !MsgAndSave(PageActionMode.Search)) return;

                if (!_flexD.HasNormalRow)
                {
                    ShowMessage("품목에 대한 내용이 존재하지 않습니다.");
                    return;
                }

                DataRow[] drs = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                string multi_Est = string.Empty;

                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                {
                    if (_flexH[i, "S"].ToString() == "Y")
                        multi_Est += _flexH[i, "NO_EST"].ToString() + "$" + D.GetString(_flexH["NO_HST"]) + "|";
                }

                DataSet ds = _biz.SearchPrint(multi_Est);
                DataTable dt결과 = ds.Tables[0].Copy();

                dt결과.Columns.Add("SUM_AM_K_EST", typeof(decimal));
                dt결과.Columns.Add("SUM_AM_TOT", typeof(decimal));

                decimal sumAmKEst = D.GetDecimal(dt결과.Compute("SUM(AM_K_EST)", ""));
                decimal sumAmTot = D.GetDecimal(dt결과.Compute("SUM(AM_TOT)", ""));

                if (서버키 == "WONIK") //원익전용
                {
                    DataTable dt_temp = WONIK_Print(dt결과);
                    dt결과 = null;
                    dt결과 = dt_temp.Copy();

                }
                else
                {
                    foreach (DataRow row in dt결과.Rows)
                    {
                        row["SUM_AM_K_EST"] = sumAmKEst;
                        row["SUM_AM_TOT"] = sumAmTot;
                    }
                }

                dt결과.AcceptChanges();

                ReportHelper rptHelper = new ReportHelper("R_SA_ESTMT_REG_0", "견적서");
                //rptHelper.SetDataTable("견적", dt결과);  //DRF출력물
                rptHelper.SetDataTable(dt결과, 1);
                rptHelper.SetDataTable(dt결과, 2);
                rptHelper.SetDataTable(dt결과, 3);
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private DataTable WONIK_Print(DataTable dt결과)
        {
            DataTable dt = dt결과.Clone();

            dt.Columns.Add("CUSTOMS_FEE_P", typeof(string));
            dt.Columns.Add("CUSTOMS_FEE_WON", typeof(decimal));
            dt.Columns.Add("COST_PRICE", typeof(decimal));
            dt.Columns.Add("SALES_MARGIN_WON", typeof(decimal));
            dt.Columns.Add("SALES_MARGIN_P", typeof(string));
            dt.Columns.Add("VAT", typeof(decimal));

            dt.Columns.Add("SUM_CONTRACT_PRICE", typeof(decimal));
            dt.Columns.Add("SUM_TOTAL_OCV", typeof(decimal));
            dt.Columns.Add("SUM_TOTAL_AMOUNT", typeof(decimal));
            dt.Columns.Add("SUM_SALES_MARGIN", typeof(decimal));
            dt.Columns.Add("SUM_SALES_MARGIN_RATE", typeof(string));
            dt.Columns.Add("SUM_TOTAL_COST_PRICE", typeof(decimal));

            dt.Columns["NM_USERDEF1"].DataType = System.Type.GetType("System.Decimal");
            dt.Columns["NM_USERDEF2"].DataType = System.Type.GetType("System.Decimal");

            for (int i = 0; i < dt결과.Rows.Count; i++)
            {
                if (D.GetString(dt결과.Rows[i]["NM_USERDEF1"]) == string.Empty)
                {
                    dt결과.Rows[i]["NM_USERDEF1"] = 0;
                }

                if (D.GetString(dt결과.Rows[i]["NM_USERDEF2"]) == string.Empty)
                {
                    dt결과.Rows[i]["NM_USERDEF2"] = 0;
                }
            }

            dt결과.AcceptChanges();

            for (int i = 0; i < dt결과.Rows.Count; i++)
            {
                dt.ImportRow(dt결과.Rows[i]);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dr["SUM_AM_K_EST"] = D.GetDecimal(dt.Compute("SUM(AM_K_EST)", "NO_EST = '" + D.GetString(dr["NO_EST"]) + "'"));
                dr["SUM_AM_TOT"] = D.GetDecimal(dt.Compute("SUM(AM_TOT)", "NO_EST = '" + D.GetString(dr["NO_EST"]) + "'"));

                dr["CUSTOMS_FEE_P"] = D.GetDecimal(dr["NM_USERDEF1"]);
                dr["CUSTOMS_FEE_WON"] = (D.GetDecimal(dr["AM_K_EST"]) * D.GetDecimal(dr["NM_USERDEF1"]) / 100);

                dr["COST_PRICE"] = D.GetDecimal(dr["AM_K_EST"]) + D.GetDecimal(dr["CUSTOMS_FEE_WON"]);
                dr["SALES_MARGIN_WON"] = D.GetDecimal(dr["NM_USERDEF2"]) - (D.GetDecimal(dr["AM_K_EST"]) + D.GetDecimal(dr["CUSTOMS_FEE_WON"]));
                if (D.GetDecimal(dr["NM_USERDEF2"]) == 0)
                {
                    dr["SALES_MARGIN_P"] = 0;
                }
                else
                {
                    double sales_margin_p = Convert.ToDouble(D.GetDecimal(dr["SALES_MARGIN_WON"]) / D.GetDecimal(dr["NM_USERDEF2"])) * 10000;
                    dr["SALES_MARGIN_P"] = Math.Truncate(sales_margin_p) / 100;

                    //dr["SALES_MARGIN_P"] =  ((D.GetDecimal(dr["SALES_MARGIN_WON"]) / D.GetDecimal(dr["NM_USERDEF2"])) * 100).ToString("N2");
                }

                dr["SUM_CONTRACT_PRICE"] = D.GetDecimal(dt.Compute("SUM(NM_USERDEF2)", "NO_EST = '" + D.GetString(dr["NO_EST"]) + "'"));
                dr["VAT"] = D.GetDecimal(dt.Compute("SUM(NM_USERDEF2)", "NO_EST = '" + D.GetString(dr["NO_EST"]) + "'")) * 10 / 100;
                dr["SUM_TOTAL_AMOUNT"] = D.GetDecimal(dr["SUM_CONTRACT_PRICE"]) + D.GetDecimal(dr["VAT"]);
                dr["SUM_TOTAL_OCV"] = D.GetDecimal(dr["NUM_USERDEF1"]) + D.GetDecimal(dr["NUM_USERDEF2"]) + D.GetDecimal(dr["NUM_USERDEF3"]) + D.GetDecimal(dr["NUM_USERDEF4"]) + D.GetDecimal(dr["NUM_USERDEF5"]) + D.GetDecimal(dr["NUM_USERDEF6"]) + D.GetDecimal(dr["TEXT_USERDEF3"]);

            }

            dt.AcceptChanges();

            foreach (DataRow dr_row in dt.Rows)
            {
                dr_row["CUSTOMS_FEE_P"] = D.GetString(dr_row["CUSTOMS_FEE_P"]) + "%";
                dr_row["SALES_MARGIN_P"] = D.GetString(dr_row["SALES_MARGIN_P"]) + "%";
                dr_row["SUM_TOTAL_COST_PRICE"] = D.GetDecimal(dt.Compute("SUM(COST_PRICE)", "NO_EST = '" + D.GetString(dr_row["NO_EST"]) + "'")) + D.GetDecimal(dr_row["SUM_TOTAL_OCV"]);
                dr_row["SUM_SALES_MARGIN"] = D.GetDecimal(dr_row["SUM_CONTRACT_PRICE"]) - D.GetDecimal(dr_row["SUM_TOTAL_COST_PRICE"]);

                if (D.GetDecimal(dr_row["SUM_CONTRACT_PRICE"]) == 0)
                {
                    dr_row["SUM_SALES_MARGIN_RATE"] = 0;
                }
                else
                {
                    double SUM_SALES_MARGIN_RATE = Convert.ToDouble(D.GetDecimal(dr_row["SUM_SALES_MARGIN"]) / D.GetDecimal(dr_row["SUM_CONTRACT_PRICE"])) * 10000;
                    dr_row["SUM_SALES_MARGIN_RATE"] = (Math.Truncate(SUM_SALES_MARGIN_RATE) / 100).ToString() + "%";

                    //dr_row["SUM_SALES_MARGIN_RATE"] = ((D.GetDecimal(dr_row["SUM_SALES_MARGIN"]) / D.GetDecimal(dr_row["SUM_CONTRACT_PRICE"])) * 100).ToString("N2");
                }
            }

            return dt;

        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("중복"))
                {
                    ShowMessage("중복된 값이 있습니다.");
                }
                else
                {
                    MsgEnd(ex);
                }
            }
        }

        #endregion

        #region ♣ 화면내버튼이벤트

        #region -> btn차수추가_Click

        void btn차수추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow) return;

                decimal MaxSeq = GetMax차수(D.GetString(_flexH["NO_EST"]));

                if (MaxSeq > D.GetDecimal(_flexH["NO_HST"]))
                {
                    ShowMessage("최대차수가 아닙니다.");
                    return;
                }

                if (D.GetString(_flexH["STA_EST"]) == "1")
                {
                    ShowMessage("이미 확정된 견적입니다.");
                    return;
                }

                MaxSeq++;

                DataRow row견적 = _flexH.GetDataRow(_flexH.Row);
                DataRow[] dr품목 = _flexD.DataTable.Select(_flexD.RowFilter);

                DataTable dt품목 = _flexD.DataTable.Clone();

                // 품목복사
                _flexD.Redraw = false;
                foreach (DataRow row품목 in dr품목)
                {
                    dt품목.ImportRow(row품목);
                    dt품목.Rows[dt품목.Rows.Count - 1]["NO_HST"] = MaxSeq;
                }
                dt품목.AcceptChanges();

                foreach (DataRow row품목 in dt품목.Rows)
                {
                    row품목.SetAdded();
                    DataRow newrow = _flexD.DataTable.NewRow();
                    _flexD.DataTable.ImportRow(row품목);
                }
                _flexD.Redraw = true;

                _flexH.Rows.Add();
                _flexH.Row = _flexH.Rows.Count - 1;

                foreach (DataColumn col in _flexH.DataTable.Columns)
                {
                    _flexH[col.ColumnName] = row견적[col.ColumnName];
                }

                _flexH["NO_EST"] = row견적["NO_EST"];
                _flexH["NO_HST"] = MaxSeq;
                _flexH.AddFinished();
                _flexH.Col = _flexH.Cols.Fixed;
                _flexH.Focus();

                txt견적명.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn차수삭제_Click

        void btn차수삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow) return;

                if (D.GetString(_flexH["STA_EST"]) == "1")
                {
                    ShowMessage("확정된 내용은 삭제하실 수 없습니다.");
                    return;
                }
                if (!Common.IsEmpty(_flexH, "NO_SO"))
                {
                    ShowMessage("수주적용된 내용은 삭제하실 수 없습니다.");
                    return;
                }

                string 견적번호 = D.GetString(_flexD["NO_EST"]);
                decimal Max차수 = GetMax차수(견적번호);

                if (Max차수 > D.GetDecimal(_flexH["NO_HST"]))
                {
                    ShowMessage("최대차수만 삭제 가능합니다.");
                    return;
                }

                _flexD.RemoveViewAll();
                _flexH.Rows.Remove(_flexH.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn추가_Click

        void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !VerifyH()) return;

                if (D.GetString(_flexH["STA_EST"]) == "1")
                {
                    ShowMessage("이미 확정된 견적입니다.");
                    return;
                }

                if (!lay공통.Enabled) return;

                _flexH.AddFinished();  // 추가

                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;
                _flexD["NO_EST"] = _flexH["NO_EST"];
                _flexD["NO_HST"] = _flexH["NO_HST"];
                _flexD["SEQ_EST"] = D.GetDecimal(_flexD.GetMaxValue("SEQ_EST")) + 1;

                switch (서버키)
                {
                    case "PNE":         //PNE솔루션
                    case "FORTIS":      //포티스
                    case "WJIS":        //우진산전
                    case "DAEYONG":     //대용산업
                    case "BIOLAND":     //바이오랜드
                        _flexD["CD_ITEM"] = _flexH["NO_EST"];
                        break;

                    case "LGDISP":      //LGD 아바코
                        if (MA.Login.회사코드 == "4000")
                            _flexD["CD_ITEM"] = _flexH["NO_EST"];
                        break;

                    case "INNOWL":
                        _flexD["NM_USERDEF1"] = cbo기준환종.SelectedValue;
                        _flexD["NM_USERDEF2"] = cbo할인구분.SelectedValue;
                        break;

                    default:
                        break;
                }

                _flexD.AddFinished();
                _flexD.Col = _flexD.Cols["CD_ITEM"].Index;
                if (_flexD.Rows.Count == 3)
                {
                    _flexD[_flexD.Row, "DT_DUEDATE"] = dtp납기일자.Text;
                }

                else if (_flexD.Rows.Count > 3)
                {
                    _flexD[_flexD.Row, "DT_DUEDATE"] = _flexD[_flexD.Row - 1, "DT_DUEDATE"];
                    _flexD.Row = _flexD.Rows.Count - 1;
                }

                _flexD.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn삭제_Click

        void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexD.HasNormalRow) return;

                if (D.GetString(_flexH["STA_EST"]) == "1")
                {
                    ShowMessage("이미 확정된 견적입니다.");
                    return;
                }
                if (!lay공통.Enabled) return;
                _flexD.Rows.Remove(_flexD.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn확정_Click

        void btn확정_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !MsgAndSave(PageActionMode.Search)) return;

                if (D.GetString(_flexH["STA_EST"]) == "1")
                {
                    ShowMessage("이미 확정된 견적입니다.");
                    return;
                }

                string 견적번호 = D.GetString(_flexH["NO_EST"]);
                decimal Max차수 = GetMax차수(견적번호);

                if (!Chk미등록품목) return;

                if (Max차수 > D.GetDecimal(_flexH["NO_HST"]))
                {
                    ShowMessage("최대차수만 확정 가능합니다.");
                    return;
                }

                if (Common.IsEmpty(_flexH, "CD_PARTNER", "거래처"))
                {
                    return;
                }

                MsgControl.ShowMsg("확정 작업 중 입니다.잠시만 기다려 주십시오.");
                _biz.확정(견적번호, Max차수);
                MsgControl.CloseMsg();

                ShowMessage(공통메세지._작업을완료하였습니다, DD("확정"));

                _flexH.AcceptChanges();
                _flexD.AcceptChanges();
                OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        #endregion

        #region -> btn확정취소_Click

        void btn확정취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !MsgAndSave(PageActionMode.Search)) return;

                if (D.GetString(_flexH["STA_EST"]) == "0")
                {
                    ShowMessage("확정되지않은 견적입니다.");
                    return;
                }

                if (D.GetString(_flexH["NO_SO"]) != string.Empty)
                {
                    ShowMessage("수주적용된 견적은 취소하실 수 없습니다.");
                    return;
                }

                string 견적번호 = D.GetString(_flexH["NO_EST"]);
                decimal Max차수 = GetMax차수(견적번호);

                if (Max차수 > D.GetDecimal(_flexH["NO_HST"]))
                {
                    ShowMessage("최대차수만 확정최소 가능합니다.");
                    return;
                }

                MsgControl.ShowMsg("확정취소 작업 중 입니다.잠시만 기다려주십시요.");
                _biz.확정취소(견적번호, Max차수);
                MsgControl.CloseMsg();

                ShowMessage(공통메세지._작업을완료하였습니다, DD("확정취소"));

                _flexH.AcceptChanges();
                _flexD.AcceptChanges();
                OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        #endregion

        #region -> btn할인율적용_Click

        void btn할인율적용_Click(object sender, EventArgs e)
        {
            try
            {
                그리드전체금액계산(cur할인율.DecimalValue);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn파일업로드_Click

        private void btn파일업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow) return;

                decimal MaxSeq = GetMax차수(D.GetString(_flexH["NO_EST"]));

                if (MaxSeq == D.GetDecimal(_flexH["NO_HST"]))
                {
                    string cd_file_code = D.GetString(_flexD["NO_EST"]) + "_" + D.GetString(_flexD["NO_HST"]); //파일 PK설정   공장코드_검사성적서번호
                    master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("SA", Global.MainFrame.CurrentPageID, cd_file_code);

                    if (D.GetString(_flexH["STA_EST"]) == "1") // 확정
                        m_dlg.UseGrant = false;
                    else
                        m_dlg.UseGrant = true;

                    if (m_dlg.ShowDialog(this) == DialogResult.Cancel) return;
                }
                else
                {
                    string cd_file_code = D.GetString(_flexD["NO_EST"]) + "_" + D.GetString(_flexD["NO_HST"]); //파일 PK설정   공장코드_검사성적서번호
                    master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("SA", Global.MainFrame.CurrentPageID, cd_file_code);
                    m_dlg.UseGrant = false;
                    if (m_dlg.ShowDialog(this) == DialogResult.Cancel) return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn메일전송_Click

        private void btn메일전송_Click(object sender, EventArgs e)
        {
            if (!_flexH.HasNormalRow || !MsgAndSave(PageActionMode.Search)) return;

            if (!_flexD.HasNormalRow)
            {
                ShowMessage("품목에 대한 내용이 존재하지 않습니다.");
                return;
            }

            string multi_Est = string.Empty;

            DataRow[] drs = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

            if (drs == null || drs.Length == 0)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return;
            }

            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
            {
                if (_flexH[i, "S"].ToString() == "Y")
                    multi_Est += _flexH[i, "NO_EST"].ToString() + "$" + D.GetString(_flexH["NO_HST"]) + "|";
            }

            DataSet ds = _biz.SearchPrint(multi_Est);

            ReportHelper rptHelper = new ReportHelper("R_SA_ESTMT_REG_0", "통합견적등록");

            Dictionary<string, string> dic = new Dictionary<string, string>();

            string pkcol_name = "NO_EST";

            P_MF_EMAIL_NOMULTI mail = new P_MF_EMAIL_NOMULTI("R_SA_ESTMT_REG_0", new ReportHelper[] { rptHelper }, dic, "통합견적등록", pkcol_name, ds.Tables[0]);
            mail.ShowDialog();
        }

        #endregion

        #region -> btn복사_Click
        void btn복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow) return;

                DataRow row견적 = _flexH.GetDataRow(_flexH.Row);
                DataRow[] dr품목 = _flexD.DataTable.Select(_flexD.RowFilter);

                DataTable dt품목 = _flexD.DataTable.Clone();

                string 견적번호 = (string)this.GetSeq(LoginInfo.CompanyCode, "SA", "01", Global.MainFrame.GetStringYearMonth);

                // 품목복사
                _flexD.Redraw = false;
                foreach (DataRow row품목 in dr품목)
                {
                    dt품목.ImportRow(row품목);
                    dt품목.Rows[dt품목.Rows.Count - 1]["NO_EST"] = 견적번호;
                    dt품목.Rows[dt품목.Rows.Count - 1]["NO_HST"] = 1;
                }
                dt품목.AcceptChanges();

                foreach (DataRow row품목 in dt품목.Rows)
                {
                    row품목.SetAdded();
                    DataRow newrow = _flexD.DataTable.NewRow();
                    _flexD.DataTable.ImportRow(row품목);
                }
                _flexD.Redraw = true;

                _flexH.Rows.Add();
                _flexH.Row = _flexH.Rows.Count - 1;

                foreach (DataColumn col in _flexH.DataTable.Columns)
                {
                    _flexH[col.ColumnName] = row견적[col.ColumnName];
                }

                _flexH["NO_EST"] = 견적번호;
                _flexH["NO_HST"] = 1;
                _flexH["DT_EST"] = Global.MainFrame.GetStringToday;
                _flexH.AddFinished();
                _flexH.Col = _flexH.Cols.Fixed;
                _flexH.Focus();

                txt견적명.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> btn엑셀업로드_Click

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !VerifyH()) return;

                if (D.GetString(_flexH["STA_EST"]) == "1")
                {
                    ShowMessage("이미 확정된 견적입니다.");
                    return;
                }

                if (!lay공통.Enabled) return;

                _flexH.AddFinished();  // 추가

                m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (m_FileDlg.ShowDialog() == DialogResult.OK)
                {
                    Application.DoEvents();

                    MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");

                    _flexD.Redraw = false;

                    string FileName = m_FileDlg.FileName;

                    Excel excel = new Excel();
                    DataTable dtExcel = excel.StartLoadExcel(FileName);

                    // PK컬럼의 존재유무
                    string[] argsPk = new string[] { "CD_PLANT", "CD_ITEM" };
                    string[] argsPkNm = new string[] { DD("공장코드"), DD("품목코드") };

                    for (int i = 0; i < argsPk.Length; i++)
                    {
                        if (!dtExcel.Columns.Contains(argsPk[i]))
                        {
                            ShowMessage("필수항목이 존재하지 않습니다. -> " + argsPk[i] + " : " + argsPkNm[i]);
                            return;
                        }
                    }

                    // 데이터 읽으면서 해당하는 값 셋팅
                    foreach (DataRow row in dtExcel.Rows)
                    {
                        _flexD.Rows.Add();
                        _flexD.Row = _flexD.Rows.Count - 1;
                        _flexD["NO_EST"] = _flexH["NO_EST"];
                        _flexD["NO_HST"] = _flexH["NO_HST"];
                        _flexD["SEQ_EST"] = D.GetDecimal(_flexD.GetMaxValue("SEQ_EST")) + 1;

                        _flexD["CD_PLANT"] = row["CD_PLANT"];
                        _flexD["NM_PLANT"] = "";
                        _flexD["CD_ITEM"] = row["CD_ITEM"];
                        _flexD["DT_DUEDATE"] = row["DT_DUEDATE"];
                        _flexD["QT_EST"] = row["QT_EST"];
                        if (dtExcel.Columns.Contains("UM_STD"))
                        {
                            _flexD["UM_STD"] = row["UM_STD"];
                            그리드라인금액계산();
                        }
                        _flexD.AddFinished();

                        _flexD.Focus();
                    }

                    _flexD.SumRefresh();

                    MsgControl.CloseMsg();

                    ShowMessage("엑셀 작업을 완료하였습니다.");
                    if (_flexD.HasNormalRow) ToolBarSaveButtonEnabled = true;
                    _flexD.Redraw = true;
                }
                MsgControl.CloseMsg();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexD.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        #endregion

        #endregion

        #region ♣ 저장관련

        protected override bool Verify()
        {
            if (!base.Verify() || !VerifyH()) return false;
            return true;
        }

        bool VerifyH()
        {
            if (!_flexH.HasNormalRow || _flexH.GetDataRow(_flexH.Row).RowState == DataRowState.Unchanged) return true;

            if (Checker.IsEmpty(txt견적번호, "견적번호"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(cur차수, "차수"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(txt견적명, "견적명"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (!Checker.IsValid(dtp견적일자, true, "견적일자"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(ctx영업그룹, "영업그룹"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(ctx담당자, "담당자"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(ctx사업장, "사업장"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(ctx수주형태, "수주형태"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(cbo환종, "환종"))
            {
                TabChange(tpg공통);
                return false;
            }

            if (D.GetString(cbo환종.SelectedValue) == "000" && cur환율.DecimalValue != 1)
            {
                _flexH["RT_EXCH"] = cur환율.DecimalValue = 1;
            }
            else
            {
                if (Checker.IsEmpty(cur환율, "환율"))
                {
                    TabChange(tpg공통);
                    return false;
                }
            }
            if (Checker.IsEmpty(cbo과세구분, "과세구분"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(cbo부가세포함, "부가세포함"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (!Checker.IsValid(dtp유효일자국내, true, lbl유효일자국내.Text))
            {
                TabChange(tpg공통);
                return false;
            }
            if (Checker.IsEmpty(cbo확정여부, "확정여부"))
            {
                TabChange(tpg공통);
                return false;
            }
            if (D.GetString(cbo확정여부.SelectedValue) == "1" && D.GetString(dtp확정일자.Text) == string.Empty)
            {
                ShowMessage("확정되었지만 확정일자가 존재하지 않습니다.");
                cbo확정여부.Focus();
                TabChange(tpg공통);
                return false;
            }

            if (서버키 == "INNOWL")
            {
                if (Checker.IsEmpty(cbo기준환종, "기준환종"))
                {
                    TabChange(tpg공통);
                    return false;
                }

                if (Checker.IsEmpty(cbo할인구분, "할인구분"))
                {
                    TabChange(tpg공통);
                    return false;
                }
            }

            return true;
        }

        bool Verify품목()
        {
            if (!_flexH.HasNormalRow) return true;
            if (_flexH.GetDataRow(_flexH.Row).RowState == DataRowState.Detached) return true;

            if (!_flexD.HasNormalRow)
            {
                ShowMessage("품목에 대한 내용이 존재하지 않습니다.");
                return false;
            }
            return true;
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !Verify() || !Verify품목()) return false;

            _biz.Save(_flexH.GetChanges(), _flexD.GetChanges());

            _flexH.AcceptChanges();
            _flexD.AcceptChanges();
            return true;
        }

        #endregion

        #region ♣ 그리드이벤트

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                ToolBarDeleteButtonEnabled = ToolBarPrintButtonEnabled = btn추가.Enabled = _flexH.HasNormalRow;
                btn삭제.Enabled = btn할인율적용.Enabled = _flexD.HasNormalRow;

                ControlEnabled();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexH_BeforeRowChange

        void _flexH_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!Verify_Grid(_flexH) || !VerifyH() || !Verify품목())
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexH_AfterRowChange

        void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                string 견적번호 = D.GetString(_flexH["NO_EST"]);
                decimal 차수 = D.GetDecimal(_flexH["NO_HST"]);
                string Filter = "NO_EST = '" + 견적번호 + "' AND NO_HST = " + D.GetString(차수);
                DataTable dt = null;

                if (_flexH.DetailQueryNeed)
                {
                    dt = _biz.DetailSearch(견적번호, 차수);
                }
                _flexD.BindingAdd(dt, Filter);

                ControlEnabled();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexD_StartEdit

        void _flexD_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_flexH.HasNormalRow)
                {
                    if (D.GetString(_flexH["STA_EST"]) == "1")
                    {
                        ShowMessage("이미 확정된 견적입니다.");
                        e.Cancel = true;
                        return;
                    }
                    if (!lay공통.Enabled)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                string ColName = _flexD.Cols[e.Col].Name;

                switch (ColName)
                {
                    case "UM_EST":
                    case "AM_K_EST":
                    case "AM_VAT":
                    case "AM_TOT":
                        e.Cancel = true;
                        return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexD_ValidateEdit

        void _flexD_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {

                string oldValue = D.GetString(_flexD.GetData(e.Row, e.Col));
                string newValue = _flexD.EditData;

                if (oldValue == newValue) return;

                string colName = _flexD.Cols[e.Col].Name;

                switch (colName)
                {
                    case "QT_EST":      // 수량
                        _flexD["QT_EST"] = D.GetDecimal(newValue);
                        break;

                    case "RT_DC":       // 할인율
                        _flexD["RT_DC"] = D.GetDecimal(newValue);
                        break;

                    case "UM_STD":      // 견적기준가
                        _flexD["UM_STD"] = D.GetDecimal(newValue);
                        break;

                    case "AM_EST":      // 견적금액
                        if (서버키 == "KFL" && D.GetDecimal(_flexD["RT_DC"]) != 0m) //한국화스너
                            _flexD["UM_EST"] = decimal.Ceiling(D.GetDecimal(_flexD["QT_EST"]) == 0m ? 0m : D.GetDecimal(newValue) / D.GetDecimal(_flexD["QT_EST"]));
                        else
                            _flexD["UM_EST"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexD["QT_EST"]) == 0m ? 0m : D.GetDecimal(newValue) / D.GetDecimal(_flexD["QT_EST"]));

                        _flexD["UM_STD"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexD["QT_EST"]) == 0m ? 0m : (D.GetDecimal(newValue) / (1m - (0.01m * D.GetDecimal(_flexD["RT_DC"])))) / D.GetDecimal(_flexD["QT_EST"]));
                        _flexD["AM_K_EST"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newValue) * cur환율.DecimalValue);
                        _flexD["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_K_EST"]) * (cur부가세율.DecimalValue / 100));
                        _flexD["AM_TOT"] = D.GetDecimal(_flexD["AM_K_EST"]) + D.GetDecimal(_flexD["AM_VAT"]);

                        if (_biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
                        {
                            수주관리.Calc 수주관리계산 = new 수주관리.Calc();
                            _flexD["AM_PROFIT"] = 수주관리계산.예상이익계산(D.GetDecimal(_flexD["QT_EST"]), D.GetDecimal(_flexD["UM_INV"]), D.GetDecimal(_flexD["AM_K_EST"]));
                        }
                        break;
                }

                switch (colName)
                {
                    case "QT_EST":      // 수량
                    case "RT_DC":       // 할인율
                    case "UM_STD":      // 견적기준가
                        if (서버키 == "INNOWL")
                            INNOWL라인금액계산();
                        else
                            그리드라인금액계산();
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexD_BeforeCodeHelp

        void _flexD_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (e.Parameter.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB1:
                        if (Common.IsEmpty(_flexD, "CD_PLANT", "공장"))
                        {
                            _flexD.Col = _flexD.Cols["CD_PLANT"].Index;
                            e.Cancel = true;
                            return;
                        }

                        if (Common.IsEmpty(_flexH, "CD_EXCH", "환종"))
                        {
                            cbo환종.Focus();
                            e.Cancel = true;
                            return;
                        }

                        e.Parameter.P09_CD_PLANT = D.GetString(_flexD["CD_PLANT"]);
                        break;
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexD_AfterCodeHelp

        void _flexD_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                //bool b할인율적용 = false;

                switch (e.Result.HelpID)
                {
                    case HelpID.P_MA_PLANT_SUB:
                        if (e.OriginValue == e.Result.CodeValue) return;

                        _flexD["CD_PLANT"] = e.Result.CodeValue;
                        if (서버키 == "BIOLAND")  //바이오랜드 이동길부장 요청
                        {
                            if (D.GetString(_flexH["NO_EST"]) != D.GetString(_flexD["CD_ITEM"]))
                                _flexD["CD_ITEM"] = _flexD["NM_ITEM"] = _flexD["STND_ITEM"] = "";
                        }
                        else
                            _flexD["CD_ITEM"] = _flexD["NM_ITEM"] = _flexD["STND_ITEM"] = "";
                        _flexD["GRP_ITEM"] = "";
                        _flexD["UNIT_SO"] = "";
                        _flexD["QT_EST"] = 0;

                        //b할인율적용 = Duzon.ERPU.MF.ComFunc.전용코드("수주등록-할인율적용") == "001" ? true : false;
                        if (서버키 == "KFL" || 서버키 == "RECAM" || 서버키 == "LEECHEM")
                        {
                            decimal dUM_STD = 0m;

                            if (_flexH["TP_SALEPRICE"].ToString() == "002") //유형별
                            {
                                dUM_STD = 견적기준가셋팅_수주등록_유형별할인율적용(_flexD["CD_ITEM"].ToString(), _flexH["CD_EXCH"].ToString(), _flexH["DT_EST"].ToString());
                            }
                            else if (_flexH["TP_SALEPRICE"].ToString() == "003") //거래처별
                            {
                                dUM_STD = 견적기준가셋팅_수주등록_거래처별할인율적용(_flexD["CD_ITEM"].ToString(), _flexH["CD_PARTNER"].ToString(), _flexH["CD_EXCH"].ToString(), _flexH["DT_EST"].ToString());
                            }

                            _flexD["UM_STD"] = dUM_STD;
                            _flexD["RT_DC"] = 할인율적용(_flexD["CD_PLANT"].ToString(), _flexD["GRP_ITEM"].ToString(), _flexH["CD_PARTNER_GRP"].ToString());
                        }
                        else
                        {
                            견적기준가셋팅();
                        }

                        그리드라인금액계산();
                        _flexD.Col = _flexD.Cols["CD_ITEM"].Index;
                        break;
                    case HelpID.P_MA_PITEM_SUB1:
                        DataTable dt예상이익2 = null;
                        if (D.GetString(_flexD["CD_PLANT"]) != string.Empty && _biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
                        {
                            dt예상이익2 = _biz.예상이익(D.GetString(_flexD["CD_PLANT"]), dtp견적일자.Text, e.Result.Rows);
                        }

                        string multiItem = Common.MultiString(e.Result.DataTable, "CD_ITEM", "|");
                        DataTable dtQtInv = _biz.현재고(LoginInfo.CompanyCode, D.GetString(_flexD["CD_PLANT"]), multiItem);
                        dtQtInv.PrimaryKey = new DataColumn[] { dtQtInv.Columns["CD_PLANT"], dtQtInv.Columns["CD_ITEM"] };

                        DataTable dtQtPo = _biz.기발주수량(LoginInfo.CompanyCode, D.GetString(_flexD["CD_PLANT"]), multiItem);
                        dtQtPo.PrimaryKey = new DataColumn[] { dtQtPo.Columns["CD_PLANT"], dtQtPo.Columns["CD_ITEM"] };



                        foreach (DataRow dr in e.Result.Rows)
                        {
                            if (e.Result.Rows[0] != dr)
                                btn추가_Click(null, null);

                            _flexD["CD_ITEM"] = dr["CD_ITEM"];
                            _flexD["NM_ITEM"] = dr["NM_ITEM"];
                            _flexD["STND_ITEM"] = dr["STND_ITEM"];
                            _flexD["GRP_ITEM"] = dr["GRP_ITEM"];
                            _flexD["UNIT_SO"] = dr["UNIT_SO"];

                            if (서버키 == "FORTIS") continue;

                            _flexD["QT_EST"] = 0;

                            //b할인율적용 = Duzon.ERPU.MF.ComFunc.전용코드("수주등록-할인율적용") == "001" ? true : false;
                            if (서버키 == "KFL" || 서버키 == "RECAM" || 서버키 == "LEECHEM")
                            {
                                decimal dUM_STD = 0m;

                                if (_flexH["TP_SALEPRICE"].ToString() == "002") //유형별
                                {
                                    dUM_STD = 견적기준가셋팅_수주등록_유형별할인율적용(_flexD["CD_ITEM"].ToString(), _flexH["CD_EXCH"].ToString(), _flexH["DT_EST"].ToString());
                                }
                                else if (_flexH["TP_SALEPRICE"].ToString() == "003") //거래처별
                                {
                                    dUM_STD = 견적기준가셋팅_수주등록_거래처별할인율적용(_flexD["CD_ITEM"].ToString(), _flexH["CD_PARTNER"].ToString(), _flexH["CD_EXCH"].ToString(), _flexH["DT_EST"].ToString());
                                }

                                _flexD["UM_STD"] = dUM_STD;
                                _flexD["RT_DC"] = 할인율적용(_flexD["CD_PLANT"].ToString(), dr["GRP_ITEM"].ToString(), _flexH["CD_PARTNER_GRP"].ToString());
                            }
                            else
                            {
                                견적기준가셋팅();
                            }

                            if (dt예상이익2 != null && dt예상이익2.Rows.Count > 0)
                            {
                                DataRow row예상이익 = dt예상이익2.Rows.Find(dr["CD_ITEM"]);
                                _flexD["UM_INV"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row예상이익 == null ? 0M : row예상이익["UM_INV"]));
                            }

                            그리드라인금액계산();

                            //현재고
                            DataRow rowQtInv = dtQtInv.Rows.Find(new object[] { D.GetString(_flexD["CD_PLANT"]), D.GetString(_flexD["CD_ITEM"]) });

                            if (rowQtInv == null)
                                _flexD["QT_INV"] = 0m;
                            else
                                _flexD["QT_INV"] = D.GetDecimal(rowQtInv["QT_INV"]);
                            //기발주수량
                            DataRow rowQtPo = dtQtPo.Rows.Find(new object[] { D.GetString(_flexD["CD_PLANT"]), D.GetString(_flexD["CD_ITEM"]) });

                            if (rowQtPo == null)
                                _flexD["QT_PO"] = 0m;
                            else
                                _flexD["QT_PO"] = D.GetDecimal(rowQtPo["QT_PO"]);


                        }
                        _flexD.Col = _flexD.Cols["QT_EST"].Index;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexD_HelpClick
        void _flexD_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!_flexD.HasNormalRow) return;

                if (D.GetString(_flexH["STA_EST"]) == "1") return;

                switch (_flexD.Cols[_flexD.Col].Name)
                {
                    case "UM_STD":
                        P_SA_UM_HISTORY_SUB dlg = new P_SA_UM_HISTORY_SUB(ctx거래처.CodeValue, ctx거래처.CodeName, ctx수주형태.CodeValue, ctx수주형태.CodeName, D.GetString(_flexD["CD_PLANT"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(_flexD["NM_ITEM"]), D.GetString(cbo환종.SelectedValue));
                        dlg.SetPageId = PageID;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            _flexD["UM_STD"] = Unit.외화단가(DataDictionaryTypes.SA, dlg.단가);
                            _flexD["UM_EST"] = Unit.외화단가(DataDictionaryTypes.SA, dlg.단가 - (dlg.단가 * D.GetDecimal(_flexD["RT_DC"])) / 100);
                            _flexD["AM_EST"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["QT_EST"]) * D.GetDecimal(_flexD["UM_EST"]));
                            _flexD["AM_K_EST"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_EST"]) * cur환율.DecimalValue);
                            _flexD["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_K_EST"]) * (cur부가세율.DecimalValue / 100));
                            _flexD["AM_TOT"] = D.GetDecimal(_flexD["AM_K_EST"]) + D.GetDecimal(_flexD["AM_VAT"]);
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

        #endregion

        #region ♣ 도움창 이벤트

        #region -> 도움창 Clear 이벤트(BpControl_CodeChanged)

        void BpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                BpCodeTextBox bp = sender as BpCodeTextBox;
                if (bp == null) return;

                switch (bp.Name)
                {
                    case "ctx수주형태":
                        _flexH["TP_VAT"] = cbo과세구분.SelectedValue = "";
                        _flexH["RT_VAT"] = cur부가세율.DecimalValue = 0;
                        _flexH["TP_BUSI"] = "";
                        거래구분에따른셋팅();
                        break;
                    case "ctx거래처":
                        _flexH["CD_PARTNER_GRP"] = "";

                        if (_flexD.HasNormalRow)
                        {
                            bool b할인율적용 = Duzon.ERPU.MF.ComFunc.전용코드("수주등록-할인율적용") == "001" ? true : false;
                            if (b할인율적용)
                            {
                                _flexD.Redraw = false;
                                try
                                {
                                    foreach (DataRow dr in _flexD.DataTable.Rows)
                                    {
                                        if (dr.RowState == DataRowState.Deleted) continue;

                                        dr["RT_DC"] = 0m;
                                    }
                                }
                                finally
                                {
                                    _flexD.Redraw = true;
                                }
                                그리드전체금액계산(0m);
                            }
                        }
                        break;
                    case "ctx영업그룹":
                        _flexH["TP_SALEPRICE"] = "";

                        if (_flexD.HasNormalRow)
                        {
                            bool b할인율적용 = Duzon.ERPU.MF.ComFunc.전용코드("수주등록-할인율적용") == "001" ? true : false;

                            if (b할인율적용)
                            {
                                _flexD.Redraw = false;
                                try
                                {
                                    foreach (DataRow dr in _flexD.DataTable.Rows)
                                    {
                                        if (dr.RowState == DataRowState.Deleted) continue;

                                        dr["UM_STD"] = 0m;
                                        그리드라인금액계산();
                                    }
                                }
                                finally
                                {
                                    _flexD.Redraw = true;
                                }
                            }
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

        #region -> BpControl_QueryBefore

        void BpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow)
                {
                    e.QueryCancel = true;
                    return;
                }
                switch (e.HelpID)
                {
                    case HelpID.P_SA_TPSO_SUB:
                        e.HelpParam.P61_CODE1 = "N";        // 반품형태
                        e.HelpParam.P62_CODE2 = "Y";        // 사용유무
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> BpControl_QueryAfter

        void BpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_SA_TPSO_SUB:
                        DataRow row수주형태 = BASIC.GetTPSO(e.CodeValue);
                        _flexH["TP_VAT"] = cbo과세구분.SelectedValue = row수주형태["TP_VAT"];
                        _flexH["RT_VAT"] = cur부가세율.DecimalValue = D.GetDecimal(row수주형태["RT_VAT"]);
                        _flexH["TP_BUSI"] = row수주형태["TP_BUSI"];
                        거래구분에따른셋팅();
                        break;
                    case HelpID.P_MA_EMP_SUB:
                        _flexH["CD_BIZAREA"] = e.HelpReturn.Rows[0]["CD_BIZAREA"];
                        _flexH["NM_BIZAREA"] = e.HelpReturn.Rows[0]["NM_BIZAREA"];
                        ctx사업장.SetCode(D.GetString(_flexH["CD_BIZAREA"]), D.GetString(_flexH["NM_BIZAREA"]));
                        break;
                    case HelpID.P_MA_PARTNER_SUB:
                        _flexH["CD_PARTNER_GRP"] = e.HelpReturn.Rows[0]["CD_PARTNER_GRP"];

                        if (_flexD.HasNormalRow)
                        {
                            bool b할인율적용 = Duzon.ERPU.MF.ComFunc.전용코드("수주등록-할인율적용") == "001" ? true : false;
                            if (b할인율적용)
                            {
                                foreach (DataRow dr in _flexD.DataTable.Rows)
                                {
                                    if (dr.RowState == DataRowState.Deleted) continue;

                                    dr["RT_DC"] = 할인율적용(dr["CD_PLANT"].ToString(), dr["GRP_ITEM"].ToString(), _flexH["CD_PARTNER_GRP"].ToString());
                                    그리드라인금액계산();
                                }
                            }
                        }
                        break;
                    case HelpID.P_MA_SALEGRP_SUB:
                        _flexH["TP_SALEPRICE"] = e.HelpReturn.Rows[0]["TP_SALEPRICE"].ToString();

                        if (_flexD.HasNormalRow)
                        {
                            bool b할인율적용 = Duzon.ERPU.MF.ComFunc.전용코드("수주등록-할인율적용") == "001" ? true : false;

                            if (b할인율적용)
                            {
                                foreach (DataRow dr in _flexD.DataTable.Rows)
                                {
                                    if (dr.RowState == DataRowState.Deleted) continue;

                                    decimal dUM_STD = 0m;

                                    if (_flexH["TP_SALEPRICE"].ToString() == "002") //유형별
                                    {
                                        dUM_STD = 견적기준가셋팅_수주등록_유형별할인율적용(_flexD["CD_ITEM"].ToString(), _flexH["CD_EXCH"].ToString(), _flexH["DT_EST"].ToString());
                                    }
                                    else if (_flexH["TP_SALEPRICE"].ToString() == "003") //거래처별
                                    {
                                        dUM_STD = 견적기준가셋팅_수주등록_거래처별할인율적용(_flexD["CD_ITEM"].ToString(), _flexH["CD_PARTNER"].ToString(), _flexH["CD_EXCH"].ToString(), _flexH["DT_EST"].ToString());
                                    }

                                    _flexD["UM_STD"] = dUM_STD;

                                    그리드라인금액계산();
                                }
                            }
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

        #endregion

        #region ♣ Control 이벤트

        #region -> cbo환종_SelectionChangeCommitted

        void cbo환종_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!cbo환종.Modified || !_flexH.HasNormalRow) return;

                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    _flexH["RT_EXCH"] = cur환율.DecimalValue = MA.기준환율적용(dtp견적일자.Text, D.GetString(cbo환종.SelectedValue));


                // 원화일경우 환율은 1로 셋팅해 준다
                if (D.GetString(cbo환종.SelectedValue) == "000")
                {
                    _flexH["RT_EXCH"] = cur환율.DecimalValue = 1;
                    cur환율.Enabled = false;
                }
                else
                {
                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        cur환율.Enabled = false;
                    else
                        cur환율.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> cbo과세구분_SelectionChangeCommitted

        void cbo과세구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!cbo과세구분.Modified || !_flexH.HasNormalRow) return;

                decimal 부가세율 = BASIC.GetTPVAT(D.GetString(cbo과세구분.SelectedValue));
                _flexH["RT_VAT"] = cur부가세율.DecimalValue = 부가세율;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> TabChange

        void TabChange(TabPage tpg)
        {
            if (tab.SelectedTab != tpg)
                tab.SelectedTab = tpg;
        }

        #endregion

        #region -> tab_Selecting

        void tab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow) return;
                if (e.TabPage == tpg해외)
                {
                    if (Checker.IsEmpty(ctx수주형태, "수주형태"))
                    {
                        TabChange(tpg공통);
                        e.Cancel = true;
                        return;
                    }

                    string 거래구분 = D.GetString(_flexH["TP_BUSI"]);
                    if (거래구분 == string.Empty)
                    {
                        ShowMessage("수주형태에 해당하는 거래구분이 존재하지 않습니다.");
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 기타메소드

        #region -> 견적기준가셋팅

        void 견적기준가셋팅()
        {
            //string 거래구분 = D.GetString(_flexH["TP_BUSI"]);
            //if ((거래구분 == "003" || 거래구분 == "004")) return;

            string 공장 = D.GetString(_flexD["CD_PLANT"]);
            string 품목 = D.GetString(_flexD["CD_ITEM"]);
            string 환종 = string.Empty;

            if (서버키 == "INNOWL")
            {
                if (D.GetString(cbo기준환종.SelectedValue) == "000")
                {
                    환종 = "000";
                }
                else
                {
                    환종 = D.GetString(_flexH["CD_EXCH"]);
                }
            }
            else
            {
                환종 = D.GetString(_flexH["CD_EXCH"]);
            }

            decimal 기준가 = 0;

            _biz.견적기준가(공장, 품목, 환종, out 기준가);
            _flexD["UM_STD"] = Unit.외화단가(DataDictionaryTypes.SA, 기준가);
        }

        #endregion

        #region -> 견적기준가셋팅_수주등록_유형별할인율적용

        decimal 견적기준가셋팅_수주등록_유형별할인율적용(string str품목, string str환종, string str견적일자)
        {
            object[] obj = new object[] { LoginInfo.CompanyCode, 
                "002",      //TP_UMMODULE
                str품목, 
                "001",      //단가유형
                str환종, 
                str견적일자
            };

            decimal d견적기준가 = Unit.외화단가(DataDictionaryTypes.SA, _biz.견적기준가조회_수주등록_유형별할인율적용(obj));
            return d견적기준가;
        }

        #endregion

        #region -> 견적기준가셋팅_수주등록_거래처별할인율적용

        decimal 견적기준가셋팅_수주등록_거래처별할인율적용(string str품목, string str거래처, string str환종, string str견적일자)
        {
            object[] obj = new object[] { LoginInfo.CompanyCode, 
                "002",      //TP_UMMODULE
                str품목, 
                str거래처, 
                "001",      //단가유형
                str환종, 
                str견적일자
            };

            decimal d견적기준가 = Unit.외화단가(DataDictionaryTypes.SA, _biz.견적기준가조회_수주등록_거래처별할인율적용(obj));
            return d견적기준가;
        }

        #endregion

        #region -> GetMax차수

        decimal GetMax차수(string 견적번호)
        {
            decimal MaxSeq = D.GetDecimal(_flexH.DataTable.Compute("MAX(NO_HST)", "NO_EST = '" + 견적번호 + "'"));
            return MaxSeq;
        }

        #endregion

        #region -> ControlEnabled

        void ControlEnabled()
        {
            bool isEnabled = false;

            if (_flexH.HasNormalRow)
            {
                if (D.GetString(cbo환종.SelectedValue) == "000")
                    cur환율.Enabled = false;
                else
                {
                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        cur환율.Enabled = false;
                    else
                        cur환율.Enabled = true;
                }

                if (_flexH.GetDataRow(_flexH.Row).RowState == DataRowState.Added)
                    isEnabled = true;

                ctx수주형태.Enabled = cbo과세구분.Enabled = isEnabled;

                decimal MaxSeq = GetMax차수(D.GetString(_flexH["NO_EST"]));
                if (MaxSeq == D.GetDecimal(_flexH["NO_HST"]) && D.GetString(_flexH["STA_EST"]) == "0")
                    isEnabled = true;
            }
            lay공통.Enabled = lay해외.Enabled = lay비고.Enabled = lay기타.Enabled = isEnabled;

            // 왜 있는지 모르겠음. 메인 추가버튼 클릭시 활성화 안되는 문제가 생김
            //if (_flexD.HasNormalRow) pnl공통2.Enabled = pnl공통5.Enabled = false;
        }

        #endregion

        #region -> 할인율적용

        decimal 할인율적용(string str공장, string str품목군, string str거래처그룹)
        {
            object[] obj = new object[] { LoginInfo.CompanyCode, 
                str공장, 
                str거래처그룹, 
                str품목군
            };

            decimal d할인율 = _biz.할인율조회(obj);
            return d할인율;
        }

        #endregion

        #region -> 그리드라인금액계산

        void 그리드라인금액계산()
        {
            decimal 견적기준단가 = D.GetDecimal(_flexD["UM_STD"]);
            decimal 할인율 = D.GetDecimal(_flexD["RT_DC"]);
            decimal 견적단가 = 0m;

            if (서버키 == "KFL" && 할인율 != 0m) //한국화스너
                견적단가 = decimal.Ceiling(견적기준단가 - (견적기준단가 * 할인율 * 0.01M));
            else
                견적단가 = Unit.외화단가(DataDictionaryTypes.SA, 견적기준단가 - (견적기준단가 * 할인율 * 0.01M));

            _flexD["UM_EST"] = 견적단가;

            decimal 수량 = D.GetDecimal(_flexD["QT_EST"]);

            string 부가세구분 = D.GetString(cbo과세구분.SelectedValue);
            bool 부가세포함 = D.GetString(cbo부가세포함.SelectedValue) == "Y" ? true : false;
            decimal 부가세율 = cur부가세율.DecimalValue;
            decimal 환율 = cur환율.DecimalValue;
            decimal 견적금액, 원화금액, 부가세;


            Calc.GetAmt(수량, 견적단가, 환율, 부가세구분, 부가세율, 모듈.SALE, 부가세포함, out 견적금액, out 원화금액, out 부가세);

            _flexD["AM_EST"] = 견적금액;
            _flexD["AM_K_EST"] = 원화금액;
            _flexD["AM_VAT"] = 부가세;
            _flexD["AM_TOT"] = 원화금액 + 부가세;

            if (_biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
            {
                수주관리.Calc 수주관리계산 = new 수주관리.Calc();
                _flexD["AM_PROFIT"] = 수주관리계산.예상이익계산(D.GetDecimal(_flexD["QT_EST"]), D.GetDecimal(_flexD["UM_INV"]), D.GetDecimal(_flexD["AM_K_EST"]));
            }
        }

        #region -> INNOWL라인금액계산
        private void INNOWL라인금액계산()
        {
            try
            {
                DataTable dt_discount = _biz.할인율(D.GetString(_flexD["CD_PLANT"]), D.GetDecimal(_flexD["QT_EST"]), dtp견적일자.Text, D.GetString(cbo할인구분.SelectedValue));

                if (dt_discount.Rows.Count != 0)

                    _flexD["NUM_USERDEF2"] = D.GetDecimal(dt_discount.Rows[0]["RT_DISCOUNT"]);
                else
                    _flexD["NUM_USERDEF2"] = 0;

                decimal 견적기준단가 = D.GetDecimal(_flexD["UM_STD"]);
                decimal 할인율1 = D.GetDecimal(_flexD["NUM_USERDEF2"]);
                decimal 할인율2 = D.GetDecimal(_flexD["RT_DC"]);
                decimal 견적단가 = 0m;
                decimal 환율 = cur환율.DecimalValue;

                견적단가 = Unit.외화단가(DataDictionaryTypes.SA, 견적기준단가 - (견적기준단가 * (할인율1 + 할인율2) * 0.01M));

                if (D.GetString(cbo기준환종.SelectedValue) == "000")
                {
                    _flexD["NUM_USERDEF1"] = Unit.원화단가(DataDictionaryTypes.SA, 견적단가);
                    _flexD["AM_K_EST"] = Unit.원화금액(DataDictionaryTypes.SA, 견적단가 * D.GetDecimal(_flexD["QT_EST"]));
                    _flexD["AM_EST"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_K_EST"]) / 환율);
                    _flexD["UM_EST"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_EST"]) / D.GetDecimal(_flexD["QT_EST"]));
                    _flexD["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_K_EST"]) * (cur부가세율.DecimalValue / 100));
                    _flexD["AM_TOT"] = D.GetDecimal(_flexD["AM_K_EST"]) + D.GetDecimal(_flexD["AM_VAT"]);
                }

                else
                {
                    _flexD["UM_EST"] = Unit.외화단가(DataDictionaryTypes.SA, 견적단가);
                    _flexD["AM_EST"] = Unit.외화금액(DataDictionaryTypes.SA, 견적단가 * D.GetDecimal(_flexD["QT_EST"]));
                    _flexD["AM_K_EST"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_EST"]) * 환율);
                    _flexD["NUM_USERDEF1"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_K_EST"]) / D.GetDecimal(_flexD["QT_EST"]));
                    _flexD["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexD["AM_K_EST"]) * (cur부가세율.DecimalValue / 100));
                    _flexD["AM_TOT"] = D.GetDecimal(_flexD["AM_K_EST"]) + D.GetDecimal(_flexD["AM_VAT"]);

                }


            }
            catch (Exception ex)
            {

                MsgEnd(ex);
            }

        }
        #endregion

        #endregion

        #region -> 그리드전체금액계산

        void 그리드전체금액계산(decimal d할인율)
        {
            decimal 할인율 = d할인율;

            for (int i = 0; i < _flexD.DataTable.Rows.Count; i++)
            {
                DataRow dr = _flexD.DataTable.Rows[i];

                if (D.GetString(dr["S"]) != "Y") continue;

                if (dr.RowState == DataRowState.Deleted) continue;

                decimal 견적기준단가 = D.GetDecimal(dr["UM_STD"]);
                decimal 견적단가 = 0m;

                if (서버키 == "KFL" && 할인율 != 0m) //한국화스너
                    견적단가 = decimal.Ceiling(견적기준단가 - (견적기준단가 * 할인율 * 0.01M));
                else
                    견적단가 = Unit.외화단가(DataDictionaryTypes.SA, 견적기준단가 - (견적기준단가 * 할인율 * 0.01M));

                dr["UM_EST"] = 견적단가;
                dr["RT_DC"] = 할인율;

                decimal 수량 = D.GetDecimal(dr["QT_EST"]);

                string 부가세구분 = D.GetString(cbo과세구분.SelectedValue);
                bool 부가세포함 = D.GetString(cbo부가세포함.SelectedValue) == "Y" ? true : false;
                decimal 부가세율 = cur부가세율.DecimalValue;
                decimal 환율 = cur환율.DecimalValue;
                decimal 견적금액, 원화금액, 부가세;

                Calc.GetAmt(수량, 견적단가, 환율, 부가세구분, 부가세율, 모듈.SALE, 부가세포함, out 견적금액, out 원화금액, out 부가세);

                dr["AM_EST"] = 견적금액;
                dr["AM_K_EST"] = 원화금액;
                dr["AM_VAT"] = 부가세;
                dr["AM_TOT"] = 원화금액 + 부가세;

                if (_biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
                {
                    수주관리.Calc 수주관리계산 = new 수주관리.Calc();
                    dr["AM_PROFIT"] = 수주관리계산.예상이익계산(D.GetDecimal(dr["QT_EST"]), D.GetDecimal(dr["UM_INV"]), D.GetDecimal(dr["AM_K_EST"]));
                }
            }

            _flexD.SumRefresh();
        }

        #endregion

        #region -> 거래구분에따른셋팅

        void 거래구분에따른셋팅()
        {
            bool isEnabled = false;

            if (_flexH.HasNormalRow)
            {
                string 거래구분 = D.GetString(_flexH["TP_BUSI"]);
                if (is해외(거래구분))
                    isEnabled = true;
            }

            pnl해외.Enabled = isEnabled;
        }

        #endregion

        #region -> is해외

        bool is해외(string 거래구분)
        {
            if (거래구분 != "001") return true;
            return false;
        }

        #endregion

        void 사용자정의셋팅()
        {
            for (int i = 1; i <= 3; i++)
            {
                _flexD.Cols["NM_USERDEF" + D.GetString(i)].Visible = false;
            }

            DataTable dt명칭 = MA.GetCode("SA_B000077");
            for (int i = 1; i <= dt명칭.Rows.Count; i++)
            {
                string Name = D.GetString(dt명칭.Rows[i - 1]["NAME"]);
                _flexD.Cols["NM_USERDEF" + D.GetString(i)].Caption = Name;
                _flexD.Cols["NM_USERDEF" + D.GetString(i)].Visible = true;
            }

            DataTable dtDATE = MA.GetCode("SA_B000086");
            for (int i = 1; i <= dtDATE.Rows.Count; i++)
            {
                string Name = D.GetString(dtDATE.Rows[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        lbl날짜_사용자정의_1.Text = Name;
                        lbl날짜_사용자정의_1.Visible = dtp날짜_사용자정의_1.Visible = true;
                        break;
                    case 2:
                        lbl날짜_사용자정의_2.Text = Name;
                        lbl날짜_사용자정의_2.Visible = dtp날짜_사용자정의_2.Visible = true;
                        break;
                }
            }

            DataTable dtTEXT = MA.GetCode("SA_B000087");
            for (int i = 1; i <= dtTEXT.Rows.Count; i++)
            {
                string Name = D.GetString(dtTEXT.Rows[i - 1]["NAME"]);

                switch (i)
                {
                    case 1:
                        lbl텍스트_사용자정의_1.Text = Name;
                        lbl텍스트_사용자정의_1.Visible = txt텍스트_사용자정의_1.Visible = true;
                        break;
                    case 2:
                        lbl텍스트_사용자정의_2.Text = Name;
                        lbl텍스트_사용자정의_2.Visible = txt텍스트_사용자정의_2.Visible = true;
                        break;
                    case 3:
                        lbl텍스트_사용자정의_3.Text = Name;
                        lbl텍스트_사용자정의_3.Visible = txt텍스트_사용자정의_3.Visible = true;
                        break;
                    case 4:
                        lbl텍스트_사용자정의_4.Text = Name;
                        lbl텍스트_사용자정의_4.Visible = txt텍스트_사용자정의_4.Visible = true;
                        break;
                    case 5:
                        lbl텍스트_사용자정의_5.Text = Name;
                        lbl텍스트_사용자정의_5.Visible = txt텍스트_사용자정의_5.Visible = true;
                        break;
                    case 6:
                        lbl텍스트_사용자정의_6.Text = Name;
                        lbl텍스트_사용자정의_6.Visible = txt텍스트_사용자정의_6.Visible = true;
                        break;
                }
            }

            DataTable dtCODE = MA.GetCode("SA_B000088");
            for (int i = 1; i <= dtCODE.Rows.Count; i++)
            {
                string Name = D.GetString(dtCODE.Rows[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        lbl코드_사용자정의_1.Text = Name;
                        lbl코드_사용자정의_1.Visible = cbo코드_사용자정의_1.Visible = true;
                        break;
                    case 2:
                        lbl코드_사용자정의_2.Text = Name;
                        lbl코드_사용자정의_2.Visible = cbo코드_사용자정의_2.Visible = true;
                        break;
                    case 3:
                        lbl코드_사용자정의_3.Text = Name;
                        lbl코드_사용자정의_3.Visible = cbo코드_사용자정의_3.Visible = true;
                        break;
                    case 4:
                        lbl코드_사용자정의_4.Text = Name;
                        lbl코드_사용자정의_4.Visible = cbo코드_사용자정의_4.Visible = true;
                        break;
                    case 5:
                        lbl코드_사용자정의_5.Text = Name;
                        lbl코드_사용자정의_5.Visible = cbo코드_사용자정의_5.Visible = true;
                        break;
                    case 6:
                        lbl코드_사용자정의_6.Text = Name;
                        lbl코드_사용자정의_6.Visible = cbo코드_사용자정의_6.Visible = true;
                        break;
                }
            }

            DataTable dtNUMBER = MA.GetCode("SA_B000089");
            for (int i = 1; i <= dtNUMBER.Rows.Count; i++)
            {
                string Name = D.GetString(dtNUMBER.Rows[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        lbl숫자_사용자정의_1.Text = Name;
                        lbl숫자_사용자정의_1.Visible = cur숫자_사용자정의_1.Visible = true;
                        break;
                    case 2:
                        lbl숫자_사용자정의_2.Text = Name;
                        lbl숫자_사용자정의_2.Visible = cur숫자_사용자정의_2.Visible = true;
                        break;
                    case 3:
                        lbl숫자_사용자정의_3.Text = Name;
                        lbl숫자_사용자정의_3.Visible = cur숫자_사용자정의_3.Visible = true;
                        break;
                    case 4:
                        lbl숫자_사용자정의_4.Text = Name;
                        lbl숫자_사용자정의_4.Visible = cur숫자_사용자정의_4.Visible = true;
                        break;
                    case 5:
                        lbl숫자_사용자정의_5.Text = Name;
                        lbl숫자_사용자정의_5.Visible = cur숫자_사용자정의_5.Visible = true;
                        break;
                    case 6:
                        lbl숫자_사용자정의_6.Text = Name;
                        lbl숫자_사용자정의_6.Visible = cur숫자_사용자정의_6.Visible = true;
                        break;
                }
            }


            DataTable dtL_NUMBER = MA.GetCode("SA_B000113");
            for (int i = 1; i <= dtL_NUMBER.Rows.Count; i++)
            {
                string Name = D.GetString(dtL_NUMBER.Rows[i - 1]["NAME"]);
                _flexD.Cols["NUM_USERDEF" + D.GetString(i)].Caption = Name;
                _flexD.Cols["NUM_USERDEF" + D.GetString(i)].Visible = true;
            }


            if (dtDATE.Rows.Count == 0 && dtTEXT.Rows.Count == 0 && dtCODE.Rows.Count == 0 && dtNUMBER.Rows.Count == 0)
            {
                tab.TabPages.Remove(tpg기타);
            }
        }

        #endregion

        #region ♣ 속성

        //bool Chk견적일자 { get { return Checker.IsValid(dtp견적일자F, dtp견적일자T, true, "견적일자From", "견적일자To"); } }
        bool Chk견적일자 { get { return Checker.IsValid(periodPicker1, true, "견적일자"); } }
        bool Chk미등록품목
        {
            get
            {
                if (서버키 == "PNE" || 서버키 == "FORTIS" || 서버키 == "LGDISP" || 서버키 == "WJIS" || 서버키 == "DAEYONG" || 서버키 == "BIOLAND")
                {
                    DataRow[] drs = _flexD.DataView.ToTable().Select("NM_ITEM IS NULL OR NM_ITEM = ''");

                    if (drs != null && drs.Length != 0)
                    {
                        ShowMessage("미등록 품목이 존재하여 확정 할 수 없습니다.");
                        return false;
                    }
                }

                return true;
            }
        }
        string 서버키 { get { return Global.MainFrame.ServerKeyCommon.ToUpper(); } }

        #endregion

        #region ♣ 전자결재
        #region -> btn전자결재_Click
        private void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !_flexD.HasNormalRow) return;

                if (IsChanged())
                {
                    ShowMessage("저장후 결재버튼을 클릭하세요.");
                    return;
                }

                switch (서버키)
                {
                    case "LUXCO":
                        LUXCO_GW gw = new LUXCO_GW();
                        if (gw.전자결재(_flexH.GetDataRow(_flexH.Row), _flexD.DataView.ToTable()))
                            ShowMessage("전자결재가 완료되었습니다.");
                        break;

                    case "WONIK":
                        WONIK_GW gw_wonik = new WONIK_GW();
                        if (gw_wonik.전자결재(_flexH.GetDataRow(_flexH.Row), _flexD.DataView.ToTable()))
                            ShowMessage("전자결재가 완료되었습니다.");
                        break;

                    case "CIS":
                        if (D.GetDecimal(_flexH["STA_EST"]) != 1)
                        {
                            ShowMessage("확정건에 대해서 전자결재가 진행됩니다.");
                            return;
                        }

                        CIS_GW gw_cis = new CIS_GW();
                        if (gw_cis.전자결재(_flexH.GetDataRow(_flexH.Row), _flexD.DataView.ToTable()))
                            ShowMessage("전자결재가 완료되었습니다.");
                        break;

                    default:
                        NP_GW gw_np = new NP_GW();
                        if (gw_np.전자결재(_flexH.GetDataRow(_flexH.Row), _flexD.DataView.ToTable()))
                            ShowMessage("전자결재가 완료되었습니다.");
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
    }
}