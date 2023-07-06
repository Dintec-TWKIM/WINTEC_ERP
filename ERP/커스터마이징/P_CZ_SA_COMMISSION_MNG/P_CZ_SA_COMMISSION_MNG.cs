using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Erpiu.ComponentModel;
using Duzon.Common.Forms.Help;
using Dintec;
using Duzon.Common.Controls;
using System.Web;
using Duzon.ERPU.MF;
using Duzon.Common.BpControls;

namespace cz
{
    public partial class P_CZ_SA_COMMISSION_MNG : PageBase
    {
        #region 초기화 & 전역변수
        P_CZ_SA_COMMISSION_MNG_BIZ _biz = new P_CZ_SA_COMMISSION_MNG_BIZ();
        P_CZ_SA_COMMISSION_MNG_GW _gw = new P_CZ_SA_COMMISSION_MNG_GW();

        public P_CZ_SA_COMMISSION_MNG()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        public P_CZ_SA_COMMISSION_MNG(string 커미션번호)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.txt커미션번호S.Text = 커미션번호;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex커미션, this._flex대상자, this._flex지급내역 };
            this._flex커미션.DetailGrids = new FlexGrid[] { this._flex대상자, this._flex지급내역, this._flex수주이윤, this._flex지급금액 };

            #region 커미션
            this._flex커미션.BeginSetting(1, 1, false);

            this._flex커미션.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex커미션.SetCol("YN_ADDED", "추가지급", 60, false, CheckTypeEnum.Y_N);
            this._flex커미션.SetCol("NM_STAT", "결재상태", 80);
            this._flex커미션.SetCol("NO_COMMISSION", "커미션번호", 100);
            this._flex커미션.SetCol("DT_COMMISSION", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex커미션.SetCol("NM_USER", "등록자", 80);
            this._flex커미션.SetCol("LN_PARTNER", "거래처", 100);
            this._flex커미션.SetCol("NM_EXCH", "통화명", 80);
            this._flex커미션.SetCol("AM_COMMISSION1", "지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex커미션.SetCol("DC_COMMISSION", "내용", 120);

            this._flex커미션.SetOneGridBinding(new object[] { this.txt커미션번호 }, new IUParentControl[] { this.pnl커미션정보, this.pnl커미션내용, this.pnl대상내역 });
            this._flex커미션.SetBindningRadioButton(new RadioButtonExt[] { this.rdo비용포함, this.rdo비용제외 }, new string[] { "Y", "N" });
            this._flex커미션.SetBindningRadioButton(new RadioButtonExt[] { this.rdo추가지급, this.rdo기본지급 }, new string[] { "Y", "N" });
            this._flex커미션.SetDummyColumn("S");

            this._flex커미션.ExtendLastCol = true;

            this._flex커미션.SettingVersion = "0.0.0.1";
            this._flex커미션.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 거래처담당자
            this._flex대상자.BeginSetting(1, 1, false);

            this._flex대상자.SetCol("NM_PTR", "담당자명", 120);
            this._flex대상자.SetCol("NM_DEPT", "부서", 80);
            this._flex대상자.SetCol("NM_DUTY_RESP", "직급", 80);
            this._flex대상자.SetCol("NM_EMAIL", "이메일", 80);
            this._flex대상자.SetCol("DC_RMK", "비고", 100, true);

            this._flex대상자.SetCodeHelpCol("NM_PTR", "H_CZ_MA_PARTNERPTR_SUB", ShowHelpEnum.Always, new string[] { "SEQ", "NM_PTR", "NM_DEPT", "NM_DUTY_RESP", "NM_EMAIL" }, new string[] { "SEQ", "NM_PTR", "NM_DEPT", "NM_DUTY_RESP", "NM_EMAIL" });
            this._flex대상자.ExtendLastCol = true;
            
            this._flex대상자.SettingVersion = "0.0.0.1";
            this._flex대상자.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 지급금액
            this._flex지급금액.BeginSetting(1, 1, false);

            this._flex지급금액.SetCol("NM_EXCH", "통화명", 80);
            this._flex지급금액.SetCol("AM_TARGET", "대상금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급금액.SetCol("AM_COMMISSION", "지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급금액.SetCol("AM_COMMISSION_BEFORE", "기지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex지급금액.SettingVersion = "0.0.0.1";
            this._flex지급금액.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 대상내역
            this._flex대상내역.BeginSetting(1, 1, false);

            this._flex대상내역.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex대상내역.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex대상내역.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex대상내역.SetCol("DT_PROCESS", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex대상내역.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex대상내역.SetCol("NO_IV", "계산서번호", 100);
            this._flex대상내역.SetCol("NO_IV2", "계산서번호2", 100);
            this._flex대상내역.SetCol("NO_SO", "수주번호", 100);
            this._flex대상내역.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flex대상내역.SetCol("LN_PARTNER", "매출처", 150);
            this._flex대상내역.SetCol("NM_VESSEL", "호선", 100);
            this._flex대상내역.SetCol("NM_EXCH", "통화명", 80);
            this._flex대상내역.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex대상내역.SetCol("AM_CHARGE", "비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex대상내역.SetCol("AM_NET", "비용제외금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex대상내역.SetCol("AM_OUTSTANDING", "미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex대상내역.SetCol("AM_COMMISSION_BEFORE", "기지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex대상내역.SetCol("DC_RMK1", "커미션비고", 100);
            this._flex대상내역.SetCol("NM_ITEMGRP", "품목군", 100);

            this._flex대상내역.ExtendLastCol = true;

            this._flex대상내역.SettingVersion = "0.0.0.1";
            this._flex대상내역.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 지급내역
            this._flex지급내역.BeginSetting(1, 1, false);

            this._flex지급내역.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex지급내역.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex지급내역.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex지급내역.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex지급내역.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex지급내역.SetCol("NO_IV", "계산서번호", 100);
            this._flex지급내역.SetCol("NO_IV2", "계산서번호2", 100);
            this._flex지급내역.SetCol("NO_SO", "수주번호", 100);
            this._flex지급내역.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flex지급내역.SetCol("LN_PARTNER", "매출처", 150);
            this._flex지급내역.SetCol("NO_IMO", "IMO 번호", 80);
            this._flex지급내역.SetCol("NM_VESSEL", "호선", 100);
            this._flex지급내역.SetCol("NM_EMP", "담당자", 100);
            this._flex지급내역.SetCol("NM_EXCH", "통화명", 80);
            this._flex지급내역.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급내역.SetCol("AM_CHARGE", "비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급내역.SetCol("AM_NET", "비용제외금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급내역.SetCol("AM_OUTSTANDING", "미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급내역.SetCol("AM_COMMISSION_BEFORE", "기지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급내역.SetCol("RT_COMMISSION", "지급비율", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급내역.SetCol("AM_COMMISSION", "지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex지급내역.SetCol("DC_RMK1", "커미션비고", 100);
            this._flex지급내역.SetCol("NM_ITEMGRP", "품목군", 100);

            this._flex지급내역.SettingVersion = "0.0.0.1";
            this._flex지급내역.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex지급내역.SetExceptSumCol("RT_COMMISSION");
            #endregion

            #region 수주이윤
            this._flex수주이윤.BeginSetting(1, 1, false);

            this._flex수주이윤.SetCol("NO_SO", "수주번호", 100);
            this._flex수주이윤.SetCol("NM_EXCH", "통화명", 100);
            this._flex수주이윤.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex수주이윤.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주이윤.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주이윤.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주이윤.SetCol("AM_COMMISSION_BEFORE", "기지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주이윤.SetCol("AM_COMMISSION", "지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주이윤.SetCol("AM_PROFIT", "이윤(전)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주이윤.SetCol("AM_PROFIT1", "이윤(후)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주이윤.SetCol("RT_PROFIT", "이윤율(전)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex수주이윤.SetCol("RT_PROFIT1", "이윤율(후)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex수주이윤.SetCol("NM_ITEMGRP", "품목군", 100);

            this._flex수주이윤.SettingVersion = "0.0.0.1";
            this._flex수주이윤.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex수주이윤.SetExceptSumCol("RT_EXCH");
            #endregion
        }

        private void InitEvent()
        {
            this._flex커미션.AfterRowChange += new RangeEventHandler(this._flex커미션_AfterRowChange);
            this._flex대상자.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flex지급내역.ValidateEdit += new ValidateEditEventHandler(this._flex지급내역_ValidateEdit);

            this.dtp일자.CalendarDateChanged += new CalendarDateChangedEventHandler(this.dtp매출일자_CalendarDateChanged);

            this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);
            this.btn문서보기.Click += new EventHandler(this.btn문서보기_Click);
            this.btn대상자추가.Click += new EventHandler(this.btn대상자추가_Click);
            this.btn대상자삭제.Click += new EventHandler(this.btn대상자삭제_Click);
            this.btn대상조회.Click += new EventHandler(this.btn대상조회_Click);
            this.btn대상추가.Click += new EventHandler(this.btn대상추가_Click);
            this.btn대상삭제.Click += new EventHandler(this.btn대상삭제_Click);
            this.btn커미션율적용.Click += new EventHandler(this.btn커미션율적용_Click);
            this.btn커미션금액적용.Click += new EventHandler(this.btn커미션금액적용_Click);
            this.btn자동등록.Click += new EventHandler(this.btn자동등록_Click);
			this.btn미수금액갱신.Click += Btn미수금액갱신_Click;

            this.ctx거래처.QueryAfter += new BpQueryHandler(this.ctx거래처_QueryAfter);
        }

        private void Btn미수금액갱신_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex지급내역.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
				}
                else
				{
                    foreach (DataRow dr in dataRowArray)
					{
                        DBHelper.ExecuteNonQuery("SP_CZ_SA_COMMISSION_DT_RCP_U", new string[] { dr["CD_COMPANY"].ToString(),
                                                                                                dr["NO_IV"].ToString(),
                                                                                                dr["NO_SO"].ToString(),
                                                                                                Global.MainFrame.LoginInfo.UserID });
					}

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn미수금액갱신.Text);
				}
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.spl커미션.SplitterDistance = 983;
            this.spl커미션내역.SplitterDistance = 451;

            this.dtp등록일자S.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp등록일자S.EndDateToString = Global.MainFrame.GetStringToday;

            this.ctx등록자.CodeValue = Global.MainFrame.LoginInfo.UserID;
            this.ctx등록자.CodeName = Global.MainFrame.LoginInfo.UserName;

            this.cbo일자.DataSource = MA.GetCodeUser(new string[] { "", "001", "002", "003" }, new string[] { "", "수주일자", "매출일자", "수금일자" });
            this.cbo일자.ValueMember = "CODE";
            this.cbo일자.DisplayMember = "NAME";

            this.ControlEnable(false);

            if (!string.IsNullOrEmpty(this.txt커미션번호S.Text))
                this.OnToolBarSearchButtonClicked(null, null);
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex커미션.Binding = this._biz.SearchHeader(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                 this.dtp등록일자S.StartDateToString,
                                                                                 this.dtp등록일자S.EndDateToString,
                                                                                 this.ctx거래처S.CodeValue,
                                                                                 this.txt커미션번호S.Text,
                                                                                 this.ctx등록자.CodeValue });

                if (!this._flex커미션.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
                base.OnToolBarAddButtonClicked(sender, e);

                if (!BeforeAdd()) return;

                this._flex커미션.Rows.Add();
                this._flex커미션.Row = this._flex커미션.Rows.Count - 1;

                this._flex커미션["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex커미션["NO_COMMISSION"] = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "11", Global.MainFrame.GetStringToday.Substring(2, 2));
                this._flex커미션["RT_COMMISSION"] = 0;
                this._flex커미션["TP_DATE"] = "002";
                this._flex커미션["DT_START"] = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
                this._flex커미션["DT_END"] = Global.MainFrame.GetStringToday;
                this._flex커미션["YN_CHARGE"] = "N";
                this._flex커미션["YN_ADDED"] = "N";
                this._flex커미션["DC_COMMISSION"] = "전자결재 가이드 라인 (결재 상신 후 삭제 요망) ========================================================";
                this._flex커미션["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                this.cbo일자.SelectedValue = this._flex커미션["TP_DATE"].ToString();
                this.dtp일자.StartDateToString = this._flex커미션["DT_START"].ToString();
                this.dtp일자.EndDateToString = this._flex커미션["DT_END"].ToString();

                this._flex커미션.AddFinished();
                this._flex커미션.Col = this._flex커미션.Cols.Fixed;
                this._flex커미션.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            string 결재상태;

            try
            {
                if (this._flex커미션["ID_INSERT"].ToString() != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("본인이 작성한 건만 삭제 가능 합니다.");
                    return false;
                }

                결재상태 = D.GetString(this._flex커미션["ST_STAT"]);

                if (결재상태 == "0" || 결재상태 == "1")
                {
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(this._flex커미션["NM_STAT"]));
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return base.BeforeDelete();
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            string 커미션번호;

            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex커미션.HasNormalRow) return;

                커미션번호 = this._flex커미션["NO_COMMISSION"].ToString();
                this._flex커미션.Rows.Remove(this._flex커미션.Row);

                foreach (DataRow dr in this._flex대상자.DataTable.Select("NO_COMMISSION = '" + 커미션번호 + "'"))
                {
                    dr.Delete();
                }

                foreach (DataRow dr in this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + 커미션번호 + "'"))
                {
                    dr.Delete();
                }
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
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (this.MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex커미션.IsDataChanged == false &&
                this._flex대상자.IsDataChanged == false &&
                this._flex지급내역.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flex커미션.GetChanges(),
                                this._flex대상자.GetChanges(),
                                this._flex지급내역.GetChanges())) return false;

            this._flex커미션.AcceptChanges();
            this._flex대상자.AcceptChanges();
            this._flex지급내역.AcceptChanges();

            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (this._flex커미션.HasNormalRow == false) return;

                dataRowArray = this._flex커미션.DataTable.Select("S = 'Y'", "NO_COMMISSION DESC");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._flex커미션.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.Added).Length > 0)
                    {
                        this.ShowMessage("저장되지 않은 건이 선택되어 있습니다.");
                        return;
                    }

                    this._gw.문서보기(dataRowArray);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex커미션_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt, dt1, dt2, dt3;
            decimal 이윤, 이윤1, 수주금액;
            string key, filter;

            try
            {
                if (!this._flex커미션.HasNormalRow) return;

                this.dtp일자.StartDateToString = this._flex커미션["DT_START"].ToString();
                this.dtp일자.EndDateToString = this._flex커미션["DT_END"].ToString();

                dt = null;
                dt1 = null;
                dt2 = null;
                dt3 = null;

                key = this._flex커미션["NO_COMMISSION"].ToString();
                filter = "NO_COMMISSION = '" + key + "'";

                if (this._flex커미션["ID_INSERT"].ToString() == Global.MainFrame.LoginInfo.UserID &&
                    this._flex커미션["ST_STAT"].ToString() != "0" &&
                    this._flex커미션["ST_STAT"].ToString() != "1")
                {
                    this.ControlEnable(true);
                }
                else
                {
                    this.ControlEnable(false);

                    if (this._flex커미션["ID_INSERT"].ToString() == Global.MainFrame.LoginInfo.UserID ||
                        this._flex커미션["ST_STAT"].ToString() == "1")
                        this.btn문서보기.Enabled = true;
                }

                if (this._flex커미션.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchLine(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             key });

                    dt1 = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 key });

                    dt2 = this._biz.SearchDetail2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 key });

                    #region 대상금액
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        dt3 = ComFunc.getGridGroupBy(dt1, new string[] { "NM_EXCH" }, true);
                        dt3.Columns.Add("NO_COMMISSION");
                        dt3.Columns.Add("AM_TARGET", typeof(decimal));
                        dt3.Columns.Add("AM_COMMISSION", typeof(decimal));
                        dt3.Columns.Add("AM_COMMISSION_BEFORE", typeof(decimal));

                        foreach (DataRow dr in dt3.Rows)
                        {
                            dr["NO_COMMISSION"] = this._flex커미션["NO_COMMISSION"].ToString();
                            dr["AM_TARGET"] = dt1.Compute("SUM(AM_TARGET)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                            dr["AM_COMMISSION"] = dt1.Compute("SUM(AM_COMMISSION)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                            dr["AM_COMMISSION_BEFORE"] = dt1.Compute("SUM(AM_COMMISSION_BEFORE)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                        }
                    }
                    #endregion
                }

                this._flex대상내역.Binding = null;
                this._flex대상자.BindingAdd(dt, filter);
                this._flex지급내역.BindingAdd(dt1, filter);
                this._flex수주이윤.BindingAdd(dt2, filter);
                this._flex지급금액.BindingAdd(dt3, filter);

                if (this._flex대상자.HasNormalRow && this._flex대상자.Rows.Count > 0)
                    this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;
                else
                    this.ctx거래처.ReadOnly = ReadOnly.None;

                if (this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "'", string.Empty,DataViewRowState.CurrentRows).Length == 0)
                {
                    this.rdo비용포함.Enabled = true;
                    this.rdo비용제외.Enabled = true;
                    this.rdo기본지급.Enabled = true;
                    this.rdo추가지급.Enabled = true;

                    this.btn대상조회.Enabled = true;
                    this.btn대상추가.Enabled = true;

                    this.ctx거래처.ReadOnly = ReadOnly.None;
                }
                else
                {
                    this.rdo비용포함.Enabled = false;
                    this.rdo비용제외.Enabled = false;
                    this.rdo기본지급.Enabled = false;
                    this.rdo추가지급.Enabled = false;

                    this.btn대상조회.Enabled = false;
                    this.btn대상추가.Enabled = false;

                    this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;
                }

                #region 수주이윤
                dt2 = this._flex수주이윤.DataTable;

                이윤 = D.GetDecimal(dt2.Compute("SUM(AM_PROFIT)", filter));
                이윤1 = D.GetDecimal(dt2.Compute("SUM(AM_PROFIT1)", filter));
                수주금액 = D.GetDecimal(dt2.Compute("SUM(AM_SO)", filter));

                this._flex수주이윤[this._flex수주이윤.Rows.Fixed - 1, "RT_PROFIT"] = string.Format("{0:" + this._flex수주이윤.Cols["RT_PROFIT"].Format + "}", (수주금액 == 0 ? 0 : Decimal.Round(((이윤 / 수주금액) * 100), 2, MidpointRounding.AwayFromZero)));
                this._flex수주이윤[this._flex수주이윤.Rows.Fixed - 1, "RT_PROFIT1"] = string.Format("{0:" + this._flex수주이윤.Cols["RT_PROFIT1"].Format + "}", (수주금액 == 0 ? 0 : Decimal.Round(((이윤1 / 수주금액) * 100), 2, MidpointRounding.AwayFromZero)));
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        
        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this._flex대상자.Name)
                {
                    e.Parameter.UserParams = "거래처담당자;H_CZ_MA_PARTNERPTR_SUB";
                    e.Parameter.P35_CD_MNGD = "Y";
                    e.Parameter.P14_CD_PARTNER = this.ctx거래처.CodeValue;
                    e.Parameter.P34_CD_MNG = this.ctx거래처.CodeName;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex지급내역_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (this._flex지급내역.Cols[e.Col].Name)
                {
                    case "RT_COMMISSION":
                        if (this._flex지급내역[e.Row, "YN_CHARGE"].ToString() == "Y")
                        {
                            this._flex지급내역[e.Row, "AM_TARGET"] = D.GetDecimal(this._flex지급내역[e.Row, "AM_IV"]);
                            this._flex지급내역[e.Row, "AM_COMMISSION"] = decimal.Round(D.GetDecimal(this._flex지급내역[e.Row, "AM_IV"]) * (D.GetDecimal(this._flex지급내역[e.Row, "RT_COMMISSION"]) / 100), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            this._flex지급내역[e.Row, "AM_TARGET"] = D.GetDecimal(this._flex지급내역[e.Row, "AM_NET"]);
                            this._flex지급내역[e.Row, "AM_COMMISSION"] = decimal.Round(D.GetDecimal(this._flex지급내역[e.Row, "AM_NET"]) * (D.GetDecimal(this._flex지급내역[e.Row, "RT_COMMISSION"]) / 100), 2, MidpointRounding.AwayFromZero);
                        }
                        
                        this._flex지급내역.SumRefresh();

                        DataTable dt = ComFunc.getGridGroupBy(new DataView(this._flex지급내역.DataTable, "NO_COMMISSION = '" + this._flex지급내역[e.Row, "NO_COMMISSION"].ToString() + "'", string.Empty, DataViewRowState.CurrentRows).ToTable(), new string[] { "NM_EXCH" }, true);
                        dt.Columns.Add("NO_COMMISSION");
                        dt.Columns.Add("AM_TARGET", typeof(decimal));
                        dt.Columns.Add("AM_COMMISSION", typeof(decimal));
                        dt.Columns.Add("AM_COMMISSION_BEFORE", typeof(decimal));

                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["NO_COMMISSION"] = this._flex커미션["NO_COMMISSION"].ToString();
                            dr["AM_TARGET"] = this._flex지급내역.DataTable.Compute("SUM(AM_TARGET)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                            dr["AM_COMMISSION"] = this._flex지급내역.DataTable.Compute("SUM(AM_COMMISSION)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                            dr["AM_COMMISSION_BEFORE"] = this._flex지급내역.DataTable.Compute("SUM(AM_COMMISSION_BEFORE)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                        }

                        this._flex지급금액.Binding = dt;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void dtp매출일자_CalendarDateChanged(object sender, CalendarDateChangeEventArgs e)
        {
            try
            {
                if (e.DateType == CalendarDateType.StartDate)
                    this._flex커미션["DT_START"] = this.dtp일자.StartDateToString;
                else if (e.DateType == CalendarDateType.EndDate)
                    this._flex커미션["DT_END"] = this.dtp일자.EndDateToString;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전자결재_Click(object sender, EventArgs e)
        {
            string 전자결재번호, query;
            DataRow[] dataRowArray;

            try
            {
                if (this._flex커미션.HasNormalRow == false) return;

                dataRowArray = this._flex커미션.DataTable.Select("S = 'Y'", "NO_COMMISSION DESC");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._flex커미션.DataTable.Select("S = 'Y' AND ID_INSERT <> '" + Global.MainFrame.LoginInfo.UserID + "'", string.Empty).Length > 0)
                    {
                        this.ShowMessage("본인이 작성하지 않은 건이 선택되어 있습니다.");
                        return;
                    }

                    if (this._flex커미션.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.Added).Length > 0)
                    {
                        this.ShowMessage("저장되지 않은 건이 선택되어 있습니다.");
                        return;
                    }

                    if (this._flex커미션.DataTable.Select("S = 'Y' AND YN_ADDED = 'Y'", string.Empty).Length > 0)
                    {
                        if (Global.MainFrame.ShowMessage("추가지급 건이 선택되어 있습니다.\n" +
                                                         "결재 진행 하시겠습니까?\n" +
                                                         "추가지급 : 동일 매출건에 대해서 두번 지급해야 하는 경우에만 사용 (그 외는 기본지급으로 사용)", "QY2") != DialogResult.Yes)
                            return;
                    }

                    전자결재번호 = D.GetString(this._flex커미션.DataTable.Compute("MAX(NO_COMMISSION)", "S = 'Y'"));

                    if (this._gw.전자결재(전자결재번호, dataRowArray))
                    {
                        foreach (DataRow dr in dataRowArray)
                        {
                            query = @"UPDATE CZ_SA_COMMISSIONH
                                      SET NO_GW_DOCU = '" + 전자결재번호 + "'" + Environment.NewLine +
                                     "WHERE CD_COMPANY = '" + D.GetString(dr["CD_COMPANY"]) + "'" + Environment.NewLine +
                                     "AND NO_COMMISSION = '" + D.GetString(dr["NO_COMMISSION"]) + "'";

                            DBHelper.ExecuteScalar(query);
                        }

                        this.OnToolBarSearchButtonClicked(null, null);
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn문서보기_Click(object sender, EventArgs e)
        {
            string strURL, key;

            try
            {
                if (!this._flex커미션.HasNormalRow) return;
                if (this._flex커미션["ID_INSERT"].ToString() != Global.MainFrame.LoginInfo.UserID && this._flex커미션["ST_STAT"].ToString() != "1")
                {
                    this.ShowMessage("문서작성자가 본인이거나 결재상태가 승인 상태인 문서만 확인 가능 합니다.");
                    return;
                }

                key = (MA.Login.회사코드 + "-" + D.GetString(this._flex커미션["NO_GW_DOCU"]));

                strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                                                  + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                                                  + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                                                  + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                                                  + "&login_id=" + this._flex커미션["ID_WRITE"].ToString();

                P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(strURL);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn대상자추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex커미션.HasNormalRow) return;

                this._flex대상자.Rows.Add();
                this._flex대상자.Row = this._flex대상자.Rows.Count - 1;

                this._flex대상자["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex대상자["NO_COMMISSION"] = this._flex커미션["NO_COMMISSION"].ToString();

                this._flex대상자.AddFinished();
                this._flex대상자.Col = this._flex대상자.Cols.Fixed;
                this._flex대상자.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn대상자삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex대상자.HasNormalRow) return;

                this._flex대상자.GetDataRow(this._flex대상자.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn대상조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex커미션.HasNormalRow) return;

                if (string.IsNullOrEmpty(this.ctx거래처.CodeValue))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처.Text);
                    return;
                }

                this.rdo기본지급.Enabled = false;
                this.rdo추가지급.Enabled = false;
                this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;

                DataTable dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.cbo일자.SelectedValue.ToString(),
                                                                     this.dtp일자.StartDateToString,
                                                                     this.dtp일자.EndDateToString,
                                                                     this.ctx거래처.CodeValue,
                                                                     this._flex커미션["NO_COMMISSION"].ToString(),
                                                                     (this.rdo추가지급.Checked == true ? "Y" : "N"),
                                                                     (this.chkCOD건제외.Checked == true ? "Y" : "N") });

                this._flex대상내역.Binding = dt;

                if (!this._flex대상내역.HasNormalRow)
                {
                    this.rdo기본지급.Enabled = true;
                    this.rdo추가지급.Enabled = true;
                    this.ctx거래처.ReadOnly = ReadOnly.None;

                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn대상추가_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex대상내역.HasNormalRow) return;

                dataRowArray = this._flex대상내역.DataTable.Select(string.Format("S = 'Y' AND NO_COMMISSION = '{0}'", this._flex커미션["NO_COMMISSION"].ToString()));

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (this._flex대상내역.DataTable.Select(string.Format("S = 'Y' AND AM_OUTSTANDING > 0 AND NO_COMMISSION = '{0}'", this._flex커미션["NO_COMMISSION"].ToString())).Length > 0 &&
                    this.ShowMessage("미수금액이 있는 건이 선택되어 있습니다.\n추가 하시겠습니까 ?", "QY2") != DialogResult.Yes)
				{
                    return;
				}

                this._flex대상내역.Redraw = false;
                this._flex지급내역.Redraw = false;

                this.rdo비용포함.Enabled = false;
                this.rdo비용제외.Enabled = false;
                this.btn대상조회.Enabled = false;
                this.btn대상추가.Enabled = false;

                foreach (DataRow dr in dataRowArray)
                {
                    this._flex지급내역.Rows.Add();
                    this._flex지급내역.Row = this._flex지급내역.Rows.Count - 1;

                    this._flex지급내역["S"] = "N";
                    this._flex지급내역["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                    this._flex지급내역["NO_COMMISSION"] = dr["NO_COMMISSION"];
                    this._flex지급내역["YN_ADDED"] = dr["YN_ADDED"];
                    this._flex지급내역["YN_CHARGE"] = (this.rdo비용포함.Checked == true ? "Y" : "N");
                    this._flex지급내역["NO_IV"] = dr["NO_IV"];
                    this._flex지급내역["NO_SO"] = dr["NO_SO"];
                    this._flex지급내역["DT_QTN"] = dr["DT_QTN"];
                    this._flex지급내역["DT_SO"] = dr["DT_SO"];
                    this._flex지급내역["DT_IV"] = dr["DT_PROCESS"];
                    this._flex지급내역["DT_RCP"] = dr["DT_RCP"];
                    this._flex지급내역["NO_PO_PARTNER"] = dr["NO_PO_PARTNER"];
                    this._flex지급내역["LN_PARTNER"] = dr["LN_PARTNER"];
                    this._flex지급내역["NM_VESSEL"] = dr["NM_VESSEL"];
                    this._flex지급내역["CD_EXCH"] = dr["CD_EXCH"];
                    this._flex지급내역["NM_EXCH"] = dr["NM_EXCH"];
                    this._flex지급내역["AM_IV"] = dr["AM_IV"];
                    this._flex지급내역["AM_CHARGE"] = dr["AM_CHARGE"];
                    this._flex지급내역["AM_NET"] = dr["AM_NET"];
                    this._flex지급내역["AM_OUTSTANDING"] = dr["AM_OUTSTANDING"];
                    this._flex지급내역["AM_COMMISSION_BEFORE"] = dr["AM_COMMISSION_BEFORE"];

                    this._flex지급내역.AddFinished();
                    this._flex지급내역.Col = this._flex지급내역.Cols.Fixed;
                    this._flex지급내역.Focus();

                    dr.Delete();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex대상내역.Redraw = true;
                this._flex지급내역.Redraw = true;
            }
        }

        private void btn대상삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex지급내역.HasNormalRow) return;

                dataRowArray = this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    this._flex지급내역.Redraw = false;

                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();
                    }

                    if (this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "'", string.Empty, DataViewRowState.CurrentRows).Length == 0)
                    {
                        this.rdo비용포함.Enabled = true;
                        this.rdo비용제외.Enabled = true;
                        this.rdo기본지급.Enabled = true;
                        this.rdo추가지급.Enabled = true;

                        this.btn대상조회.Enabled = true;
                        this.btn대상추가.Enabled = true;

                        this.ctx거래처.ReadOnly = ReadOnly.None;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex지급내역.Redraw = true;
            }
        }

        private void btn커미션율적용_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex지급내역.HasNormalRow) return;
                if (this.cur커미션율.DecimalValue == 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.lbl커미션율.Text, "0");
                    return;
                }

                dataRowArray = this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr["RT_COMMISSION"] = this.cur커미션율.DecimalValue;

                        if (dr["YN_CHARGE"].ToString() == "Y")
                        {
                            dr["AM_TARGET"] = D.GetDecimal(dr["AM_IV"]);
                            dr["AM_COMMISSION"] = decimal.Round(D.GetDecimal(dr["AM_IV"]) * (D.GetDecimal(dr["RT_COMMISSION"]) / 100), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            dr["AM_TARGET"] = D.GetDecimal(dr["AM_NET"]);
                            dr["AM_COMMISSION"] = decimal.Round(D.GetDecimal(dr["AM_NET"]) * (D.GetDecimal(dr["RT_COMMISSION"]) / 100), 2, MidpointRounding.AwayFromZero);
                        }
                    }

                    this._flex지급내역.SumRefresh();

                    #region 대상금액
                    DataTable dt = ComFunc.getGridGroupBy(this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "'"), new string[] { "NM_EXCH" }, true);
                    dt.Columns.Add("NO_COMMISSION");
                    dt.Columns.Add("AM_TARGET", typeof(decimal));
                    dt.Columns.Add("AM_COMMISSION", typeof(decimal));
                    dt.Columns.Add("AM_COMMISSION_BEFORE", typeof(decimal));

                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["NO_COMMISSION"] = this._flex커미션["NO_COMMISSION"].ToString();
                        dr["AM_TARGET"] = this._flex지급내역.DataTable.Compute("SUM(AM_TARGET)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                        dr["AM_COMMISSION"] = this._flex지급내역.DataTable.Compute("SUM(AM_COMMISSION)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                        dr["AM_COMMISSION_BEFORE"] = this._flex지급내역.DataTable.Compute("SUM(AM_COMMISSION_BEFORE)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                    }

                    this._flex지급금액.Binding = dt;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn커미션금액적용_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            decimal 대상금액, 지급율, 지급금액, 차이금액, 차감금액;

            try
            {
                if (!this._flex지급내역.HasNormalRow) return;
                if (this.cur커미션금액.DecimalValue == 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.lbl커미션금액.Text, "0");
                    return;
                }

                dataRowArray = this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        if (dr["YN_CHARGE"].ToString() == "Y")
                            dr["AM_TARGET"] = D.GetDecimal(dr["AM_IV"]);
                        else
                            dr["AM_TARGET"] = D.GetDecimal(dr["AM_NET"]);
                    }

                    대상금액 = D.GetDecimal(this._flex지급내역.DataTable.Compute("SUM(AM_TARGET)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND S = 'Y'"));

                    if (this.cur커미션금액.DecimalValue > 대상금액)
                    {
                        this.ShowMessage(공통메세지._은_보다작아야합니다, this.lbl커미션금액.Text, "대상금액");
                        return;
                    }

                    지급율 = decimal.Round((this.cur커미션금액.DecimalValue / 대상금액) * 100, 2, MidpointRounding.AwayFromZero);
                    지급금액 = 0;

                    foreach (DataRow dr in dataRowArray)
                    {
                        if (지급금액 >= this.cur커미션금액.DecimalValue)
                        {
                            dr["RT_COMMISSION"] = 0;
                            dr["AM_COMMISSION"] = 0;
                        }
                        else
                        {
                            dr["RT_COMMISSION"] = 지급율;
                            dr["AM_COMMISSION"] = decimal.Round(D.GetDecimal(dr["AM_TARGET"]) * (D.GetDecimal(dr["RT_COMMISSION"]) / 100), 2, MidpointRounding.AwayFromZero);

                            지급금액 += D.GetDecimal(dr["AM_COMMISSION"]);
                        }
                    }

                    차이금액 = (this.cur커미션금액.DecimalValue - 지급금액);

                    foreach (DataRow dr in dataRowArray)
                    {
                        if (차이금액 == 0) break;

                        if (D.GetDecimal(dr["AM_COMMISSION"]) <= 차이금액)
                            차감금액 = D.GetDecimal(dr["AM_COMMISSION"]);
                        else
                        {
                            차감금액 = 차이금액;
                            dr["AM_COMMISSION"] = D.GetDecimal(dr["AM_COMMISSION"]) + 차감금액;
                        }
                        
                        dr["RT_COMMISSION"] = decimal.Round(D.GetDecimal(dr["AM_COMMISSION"]) / D.GetDecimal(dr["AM_TARGET"]) * 100, 2, MidpointRounding.AwayFromZero);

                        차이금액 -= 차감금액;
                    }

                    this._flex지급내역.SumRefresh();

                    #region 대상금액
                    DataTable dt = ComFunc.getGridGroupBy(this._flex지급내역.DataTable.Select("NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "'"), new string[] { "NM_EXCH" }, true);
                    dt.Columns.Add("NO_COMMISSION");
                    dt.Columns.Add("AM_TARGET", typeof(decimal));
                    dt.Columns.Add("AM_COMMISSION", typeof(decimal));
                    dt.Columns.Add("AM_COMMISSION_BEFORE", typeof(decimal));

                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["NO_COMMISSION"] = this._flex커미션["NO_COMMISSION"].ToString();
                        dr["AM_TARGET"] = this._flex지급내역.DataTable.Compute("SUM(AM_TARGET)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                        dr["AM_COMMISSION"] = this._flex지급내역.DataTable.Compute("SUM(AM_COMMISSION)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                        dr["AM_COMMISSION_BEFORE"] = this._flex지급내역.DataTable.Compute("SUM(AM_COMMISSION_BEFORE)", "NO_COMMISSION = '" + this._flex커미션["NO_COMMISSION"].ToString() + "' AND NM_EXCH = '" + dr["NM_EXCH"].ToString() + "'");
                    }

                    this._flex지급금액.Binding = dt;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx거래처_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {   
                decimal 커미션비율 = D.GetDecimal(DBHelper.ExecuteScalar(@"SELECT ISNULL(RT_COMMISSION, 0)
                                                                           FROM CZ_MA_PARTNER WITH(NOLOCK)
                                                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                          "AND CD_PARTNER = '" + e.CodeValue + "'"));

                this.cur커미션율.DecimalValue = 커미션비율;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn자동등록_Click(object sender, EventArgs e)
        {
            string 조회기간유형, 조회기간시작, 조회기간종료, 비용포함여부, 추가지급여부;
            DataTable dt;

            try
            {
                if (!this._flex커미션.HasNormalRow) return;
                if (this.bpc자동등록.CodeValues == null)
                {
                    this.ShowMessage("자동등록 거래처가 선택되어 있지 않습니다.");
                    return;
                }

                조회기간유형 = this._flex커미션["TP_DATE"].ToString();
                조회기간시작 = this._flex커미션["DT_START"].ToString();
                조회기간종료 = this._flex커미션["DT_END"].ToString();
                비용포함여부 = this._flex커미션["YN_CHARGE"].ToString();
                추가지급여부 = this._flex커미션["YN_ADDED"].ToString();

                foreach (string 거래처코드 in this.bpc자동등록.CodeValues)
                {
                    #region 매출데이터조회
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               조회기간유형,
                                                               조회기간시작,
                                                               조회기간종료,
                                                               거래처코드,
                                                               string.Empty,
                                                               추가지급여부 });
                    #endregion

                    this._flex커미션.Redraw = false;
                    this._flex지급내역.Redraw = false;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        #region 커미션추가
                        this._flex커미션.Rows.Add();
                        this._flex커미션.Row = this._flex커미션.Rows.Count - 1;

                        this._flex커미션["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                        this._flex커미션["NO_COMMISSION"] = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "11", Global.MainFrame.GetStringToday.Substring(2, 2));
                        this._flex커미션["CD_PARTNER"] = 거래처코드;
                        this._flex커미션["LN_PARTNER"] = this.bpc자동등록.DataTable.Select("CD_PARTNER = '" + 거래처코드 + "'")[0]["LN_PARTNER"].ToString();
                        this._flex커미션["RT_COMMISSION"] = D.GetDecimal(DBHelper.ExecuteScalar(@"SELECT ISNULL(RT_COMMISSION, 0)
                                                                                                  FROM CZ_MA_PARTNER WITH(NOLOCK)
                                                                                                  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                                                 "AND CD_PARTNER = '" + 거래처코드 + "'")); ;
                        this._flex커미션["TP_DATE"] = 조회기간유형;
                        this._flex커미션["DT_START"] = 조회기간시작;
                        this._flex커미션["DT_END"] = 조회기간종료;
                        this._flex커미션["YN_CHARGE"] = 비용포함여부;
                        this._flex커미션["YN_ADDED"] = 추가지급여부;
                        this._flex커미션["DC_COMMISSION"] = "전자결재 가이드 라인 (결재 상신 후 삭제 요망) ========================================================";
                        this._flex커미션["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                        this.cbo일자.SelectedValue = this._flex커미션["TP_DATE"].ToString();
                        this.dtp일자.StartDateToString = this._flex커미션["DT_START"].ToString();
                        this.dtp일자.EndDateToString = this._flex커미션["DT_END"].ToString();

                        this._flex커미션.AddFinished();
                        this._flex커미션.Col = this._flex커미션.Cols.Fixed;
                        this._flex커미션.Focus();
                        #endregion

                        #region 지급내역추가
                        foreach (DataRow dr in dt.Rows)
                        {
                            this._flex지급내역.Rows.Add();
                            this._flex지급내역.Row = this._flex지급내역.Rows.Count - 1;

                            this._flex지급내역["S"] = "N";
                            this._flex지급내역["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                            this._flex지급내역["NO_COMMISSION"] = this._flex커미션["NO_COMMISSION"].ToString();
                            this._flex지급내역["YN_ADDED"] = dr["YN_ADDED"];
                            this._flex지급내역["YN_CHARGE"] = 비용포함여부;
                            this._flex지급내역["NO_IV"] = dr["NO_IV"];
                            this._flex지급내역["NO_SO"] = dr["NO_SO"];
                            this._flex지급내역["DT_SO"] = dr["DT_SO"];
                            this._flex지급내역["DT_IV"] = dr["DT_PROCESS"];
                            this._flex지급내역["DT_RCP"] = dr["DT_RCP"];
                            this._flex지급내역["NO_PO_PARTNER"] = dr["NO_PO_PARTNER"];
                            this._flex지급내역["LN_PARTNER"] = dr["LN_PARTNER"];
                            this._flex지급내역["NM_VESSEL"] = dr["NM_VESSEL"];
                            this._flex지급내역["CD_EXCH"] = dr["CD_EXCH"];
                            this._flex지급내역["NM_EXCH"] = dr["NM_EXCH"];
                            this._flex지급내역["AM_IV"] = dr["AM_IV"];
                            this._flex지급내역["AM_CHARGE"] = dr["AM_CHARGE"];
                            this._flex지급내역["AM_NET"] = dr["AM_NET"];
                            this._flex지급내역["AM_OUTSTANDING"] = dr["AM_OUTSTANDING"];
                            this._flex지급내역["AM_COMMISSION_BEFORE"] = dr["AM_COMMISSION_BEFORE"];

                            this._flex지급내역["RT_COMMISSION"] = D.GetDecimal(this._flex커미션["RT_COMMISSION"]);

                            if (this._flex지급내역["YN_CHARGE"].ToString() == "Y")
                            {
                                this._flex지급내역["AM_TARGET"] = D.GetDecimal(this._flex지급내역["AM_IV"]);
                                this._flex지급내역["AM_COMMISSION"] = decimal.Round(D.GetDecimal(this._flex지급내역["AM_IV"]) * (D.GetDecimal(this._flex지급내역["RT_COMMISSION"]) / 100), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                this._flex지급내역["AM_TARGET"] = D.GetDecimal(this._flex지급내역["AM_NET"]);
                                this._flex지급내역["AM_COMMISSION"] = decimal.Round(D.GetDecimal(this._flex지급내역["AM_NET"]) * (D.GetDecimal(this._flex지급내역["RT_COMMISSION"]) / 100), 2, MidpointRounding.AwayFromZero);
                            }

                            this._flex지급내역.AddFinished();
                            this._flex지급내역.Col = this._flex지급내역.Cols.Fixed;
                            this._flex지급내역.Focus();
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex커미션.Redraw = true;
                this._flex지급내역.Redraw = true;
            }
        }
        #endregion

        #region 기타 메소드
        private void ControlEnable(bool isEnabled)
        {
            try
            {
                this.btn전자결재.Enabled = isEnabled;
                this.btn문서보기.Enabled = isEnabled;

                if (isEnabled)
                    this.ctx거래처.ReadOnly = ReadOnly.None;
                else
                    this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;

                this.btn대상자추가.Enabled = isEnabled;
                this.btn대상자삭제.Enabled = isEnabled;

                this.btn대상조회.Enabled = isEnabled;
                this.btn대상추가.Enabled = isEnabled;
                this.btn대상삭제.Enabled = isEnabled;
                this.btn커미션율적용.Enabled = isEnabled;
                this.btn커미션금액적용.Enabled = isEnabled;

                this._flex대상자.Cols["DC_RMK"].AllowEditing = isEnabled;

                this.txt내역.ReadOnly = !isEnabled;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion
    }
}
