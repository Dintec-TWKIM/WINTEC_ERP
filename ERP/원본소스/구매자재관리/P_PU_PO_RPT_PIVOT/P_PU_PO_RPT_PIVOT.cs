using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraPivotGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace pur
{
    public partial class P_PU_PO_RPT_PIVOT : PageBase
    {
        #region ▷ 멤버필드

        private P_PU_PO_RPT_PIVOT_BIZ _biz = new P_PU_PO_RPT_PIVOT_BIZ();

        #endregion

        #region ▷ 초기화

        #region -> 생성자

        public P_PU_PO_RPT_PIVOT()
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

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            InitPivot();
            InitEvent();
        }

        #endregion

        #region -> InitPivot

        private void InitPivot()
        {
            _pivot.SetStart();

            _pivot.AddPivotField("CD_PARTNER", "거래처코드", 120, true, PivotArea.FilterArea);
            _pivot.AddPivotField("CD_PURGRP", "구매그룹코드", 100, true, PivotArea.FilterArea);
            _pivot.AddPivotField("NM_PURGRP", "구매그룹명", 120, true, PivotArea.FilterArea);
            _pivot.AddPivotField("CD_TPPO", "발주형태코드", 100, true, PivotArea.FilterArea);
            _pivot.AddPivotField("NM_TRANS", "거래구분", 80, true, PivotArea.FilterArea);
            _pivot.AddPivotField("NM_PURCHASE", "매입형태", 100, true, PivotArea.FilterArea);
            _pivot.AddPivotField("NM_PLANT", "공장", 120, true, PivotArea.FilterArea);
            _pivot.AddPivotField("CD_CC", "C/C코드", 120, true, PivotArea.FilterArea);
            _pivot.AddPivotField("NM_CC", "C/C명", 120, true, PivotArea.FilterArea);
            _pivot.AddPivotField("STND_ITEM", "규격", 120, true, PivotArea.FilterArea);
            _pivot.AddPivotField("UNIT_IM", "단위", 60, true, PivotArea.FilterArea);
            _pivot.AddPivotField("NM_ITEMGRP", "품목군", 100, true, PivotArea.FilterArea);
            _pivot.AddPivotField("DT_LIMIT", "납기일자", 80, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT), PivotArea.FilterArea);
            _pivot.AddPivotField("AM_EX", "금액", 100, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.FOREIGN_MONEY, FormatFgType.INSERT), PivotArea.FilterArea);
            _pivot.AddPivotField("VAT", "부가세", 100, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT), PivotArea.FilterArea);
            _pivot.AddPivotField("CD_PJT", "프로젝트코드", 140, true, PivotArea.FilterArea);
            _pivot.AddPivotField("NM_PJT", "프로젝트명", 140, true, PivotArea.FilterArea);
            _pivot.AddPivotField("GI_CD_PARTNER", "납품처코드", 120, true, PivotArea.FilterArea);
            _pivot.AddPivotField("GI_LN_PARTNER", "납품처명", 120, true, PivotArea.FilterArea);

            _pivot.AddPivotField("NO_PO", "발주번호", 120, true, PivotArea.RowArea);
            _pivot.AddPivotField("DT_PO", "발주일자", 80, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT), PivotArea.RowArea);
            _pivot.AddPivotField("NM_TPPO", "발주형태명", 150, true, PivotArea.RowArea);
            _pivot.AddPivotField("NM_KOR", "담당자", 80, true, PivotArea.RowArea);
            _pivot.AddPivotField("LN_PARTNER", "거래처명", 120, true, PivotArea.RowArea);
            _pivot.AddPivotField("NM_EXCH", "환종", 60, true, PivotArea.RowArea);
            _pivot.AddPivotField("CD_ITEM", "품번", 120, true, PivotArea.RowArea);
            _pivot.AddPivotField("NM_ITEM", "품목명", 150, true, PivotArea.RowArea);
            _pivot.AddPivotField("NM_POST", "발주상태", 80, true, PivotArea.RowArea);

            _pivot.AddPivotField("QT_PO", "발주량", 80, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.QUANTITY, FormatFgType.INSERT), PivotArea.DataArea);
            _pivot.AddPivotField("UM_EX", "단가", 80, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.FOREIGN_UNIT_COST, FormatFgType.INSERT), PivotArea.DataArea);
            _pivot.AddPivotField("AM", "원화금액", 100, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT), PivotArea.DataArea);
            _pivot.AddPivotField("AM_TOTAL", "총금액", 100, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT), PivotArea.DataArea);

            DataTable dtNUM = MA.GetCode("PU_C000093", false);
            if (dtNUM != null && dtNUM.Rows.Count != 0)
            {
                foreach (DataRow row in dtNUM.Rows)
                {
                    string ColName = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                    _pivot.AddPivotField(D.GetString(row["NAME"]), ColName, 80, true, Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.FOREIGN_UNIT_COST, FormatFgType.INSERT), PivotArea.DataArea);
                }
            }
            _pivot.PivotGridControl.OptionsView.ColumnTotalsLocation = DevExpress.XtraPivotGrid.PivotTotalsLocation.Far;
            _pivot.SetEnd();
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            // 공장, 발주상태, 날짜선택 콤보박스
            DataSet ds_Combo = GetComboData("N;MA_PLANT", "S;PU_C000009", "N;PU_C000013");

            cbo_CD_PLANT.DataSource = ds_Combo.Tables[0];
            cbo_CD_PLANT.DisplayMember = "NAME";
            cbo_CD_PLANT.ValueMember = "CODE";

            cbo_FG_POST.DataSource = ds_Combo.Tables[1];
            cbo_FG_POST.DisplayMember = "NAME";
            cbo_FG_POST.ValueMember = "CODE";

            cbo_DT_SELECT.DataSource = ds_Combo.Tables[2];
            cbo_DT_SELECT.DisplayMember = "NAME";
            cbo_DT_SELECT.ValueMember = "CODE";

            // 날짜컨트롤
            //dtp_FROM.Text = Global.MainFrame.GetStringFirstDayInMonth;
            //dtp_TO.Text = Global.MainFrame.GetStringToday;
            dps_date.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;//2013.08.08 날짜컨트롤 수정
            dps_date.EndDateToString = Global.MainFrame.GetStringToday;//2013.08.08 날짜컨트롤 수정
            oneGrid1.UseCustomLayout = true;
            oneGrid1.InitCustomLayout();
        }

        #endregion

        #region -> InitEvent

        private void InitEvent()
        {
            bp_CD_ITEM.QueryBefore += new BpQueryHandler(Control_QueryBefore);
        }

        #endregion

        #endregion

        #region ▷ 메인버튼 클릭

        protected override bool BeforeSearch()
        {
            if (!Chk_CD_PLANT)
                return false;

            if (!datecheck)
                return false;
            return true;
        }


        #region -> 조회버튼 클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch())
                    return;

                object[] obj = new object[]
                {
                    Global.MainFrame.LoginInfo.CompanyCode,
                    D.GetString(cbo_CD_PLANT.SelectedValue),
                    D.GetString(cbo_DT_SELECT.SelectedValue),
                    //dtp_FROM.Text,
                    //dtp_TO.Text,
                    dps_date.StartDateToString, //2013.08.08 날짜컨트롤 수정
                    dps_date.EndDateToString,   //2013.08.08 날짜컨트롤 수정
                    bp_CD_PURGRP.CodeValue,
                    D.GetString(cbo_FG_POST.SelectedValue),
                    bp_CD_ITEM.CodeValue,
                    bp_MA_EMP.CodeValue
                };

                DataTable dt = _biz.Search(obj);

                _pivot.DataSource = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

        #region ▷ 컨트롤 이벤트

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.ControlName)
                {
                    case "bp_CD_ITEM":
                        e.HelpParam.P09_CD_PLANT = D.GetString(cbo_CD_PLANT.SelectedValue);
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

        #region ▷ 기타 속성

        bool Chk_CD_PLANT { get { return !Checker.IsEmpty(cbo_CD_PLANT, DD("공장")); } }
        //bool Chk_DT_FROM { get { return !Checker.IsEmpty(dtp_FROM, DD("기간From")); } }
        //bool Chk_DT_TO { get { return !Checker.IsEmpty(dtp_TO, DD("기간To")); } }
        bool datecheck { get { return Checker.IsValid(dps_date, true, "일자"); } } //2013.08.08 날짜컨트롤 수정

        #endregion
    }
}