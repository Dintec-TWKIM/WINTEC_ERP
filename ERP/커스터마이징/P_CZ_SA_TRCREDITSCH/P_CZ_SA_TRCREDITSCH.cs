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
using Duzon.Windows.Print;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_TRCREDITSCH : PageBase
    {
        private P_CZ_SA_TRCREDITSCH_BIZ _biz = new P_CZ_SA_TRCREDITSCH_BIZ();

        private bool Chk기간
        {
            get
            {
                return !Checker.IsEmpty((Control)this.dtp조회기간, "기간");
            }
        }

        private bool Chk기준구분
        {
            get
            {
                return !Checker.IsEmpty((Control)this.dtp조회기준일, "조회기준일");
            }
        }

        public P_CZ_SA_TRCREDITSCH()
        {
            try
            {
                StartUp.Certify(this);
                this.InitializeComponent();

                this.InitGrid();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this.SetGrid(this._flex계산서번호);
            this.SetGrid(this._flex수주번호);
        }

        private void SetGrid(FlexGrid grid)
        {
            grid.BeginSetting(1, 1, false);

            grid.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            grid.SetCol("NM_DEPT", "부서", 100);
            grid.SetCol("NM_PARTNER", "거래처", 120);
            grid.SetCol("BILL_NM_PARTNER", "수금처", 120);
            grid.SetCol("NO_IV", "계산서번호", 100);
            grid.SetCol("DC_REMARK", "비고", 100);
            grid.SetCol("DT_PROCESS", "매출일자", 120, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            grid.SetCol("AM_EX_IV", "채권외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_IV", "채권원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_BAN_EX", "외화수금액(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_BAN", "원화수금액(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_MISU_EX", "외화미수금(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_MISU", "원화미수금(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_RCP_EX", "외화수금액(수금)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_RCP", "원화수금액(수금)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_RCP_MISU_EX", "외화미수금(수금)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_RCP_MISU", "원화미수금(수금)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_EX", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_EX_CHARGE", "외화비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_CHARGE", "원화비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_EX_NET", "순외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("AM_NET", "순원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            grid.SetCol("DT_RCP", "수금일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            grid.SetCol("DT_RCP_RSV", "수금예정일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            grid.SetCol("DT", "수금예정일 경과일수", 130);
            grid.SetCol("NM_EMP", "마감담당자", 120);
            grid.SetCol("CD_EXCH", "화폐단위", 80, false);
            grid.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            grid.SetCol("NO_SO", "수주번호", 100);
            grid.SetCol("CD_PJT", "프로젝트", 100);

            if (grid.Name == this._flex수주번호.Name)
                grid.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            
            grid.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            grid.SetCol("NO_IMO", "IMO번호", 100);
            grid.SetCol("NM_VESSEL", "호선명", 100);

            grid.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005", true), "CODE", "NAME");
            grid.SetDummyColumn("S");

            grid.SettingVersion = "1.0.0.1";
            grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            grid.SetExceptSumCol("DT", "RT_EXCH");

            grid.CellNoteInfo.EnabledCellNote = true;
            grid.CellNoteInfo.CategoryID = this.Name;
            grid.CellNoteInfo.DisplayColumnForDefaultNote = "NO_IV";

            grid.CellContentChanged += new CellContentEventHandler(this._flex_CellContentChanged);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo조회기간.DataSource = MA.GetCodeUser(new string[] { "000", "001" }, new string[] { "매출일자", "수주일자" }, false);
            this.cbo조회기간.DisplayMember = "NAME";
            this.cbo조회기간.ValueMember = "CODE";
            this.cbo조회기간.SelectedValue = "000";

            this.dtp조회기간.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.dtp조회기간.EndDateToString = Global.MainFrame.GetStringToday;
            this.dtp조회기준일.Text = Global.MainFrame.GetStringToday;

            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            FlexGrid grid;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch() || (!this.Chk기간 || !this.Chk기준구분)) return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (this.cbo조회기간.SelectedValue.ToString() != "000")
                    {
                        this.ShowMessage("계산서번호별 Tab은 매출일자 조회만 지원합니다.");
                        return;
                    }

                    grid = this._flex계산서번호;
                }
                else
                    grid = this._flex수주번호;

                DataTable dataTable = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.tabControl1.SelectedIndex,
                                                                      this.cbo조회기간.SelectedValue.ToString(),
                                                                      this.dtp조회기간.StartDateToString,
                                                                      this.dtp조회기간.EndDateToString,
                                                                      this.bpc매출처.QueryWhereIn_Pipe,
                                                                      this.dtp조회기준일.Text,
                                                                      this.ctx마감담당자.CodeValue,
                                                                      this.ctx수금처.CodeValue,
                                                                      (this.chk미수금0표시.Checked ? "Y" : "N"),
                                                                      this.txt계산서번호.Text });

                grid.Binding = dataTable;

                if (!grid.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                this._biz.SaveContent(e.ContentType, e.CommandType, D.GetString(this._flex계산서번호[e.Row, "NO_IV"]), e.SettingValue, D.GetString(this._flex계산서번호[e.Row, "GUBUN"]));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
