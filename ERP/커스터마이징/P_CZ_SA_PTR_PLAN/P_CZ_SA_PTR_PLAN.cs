using System;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_PTR_PLAN : PageBase
    {
        #region 생성자 & 전역변수
        private P_CZ_SA_PTR_PLAN_BIZ _biz;

        private enum 선택탭
        {
            CC,
            매출처그룹,
            영업그룹,
            매출처
        }

        private FlexGrid 선택그리드
        {
            get
            {
                switch ((선택탭)this.tabControlExt1.SelectedIndex)
                {
                    case 선택탭.CC:
                        return this._flexCC;
                    case 선택탭.매출처그룹:
                        return this._flex매출처그룹;
                    case 선택탭.영업그룹:
                        return this._flex영업그룹;
                    case 선택탭.매출처:
                        return this._flex매출처;
                    default:
                        return null;
                }
            }
        }

        public P_CZ_SA_PTR_PLAN()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_CZ_SA_PTR_PLAN_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexCC, this._flex매출처그룹, this._flex영업그룹, this._flex매출처 };

            this.InitGrid(this._flexCC);
            this.InitGrid(this._flex매출처그룹);
            this.InitGrid(this._flex영업그룹);
            this.InitGrid(this._flex매출처);
        }

        private void InitGrid(FlexGrid grid)
        {
            grid.BeginSetting(1, 1, false);

            if (grid.Name == this._flexCC.Name)
            {
                grid.SetCol("CD_KEY", "CC", 100, true);
                grid.SetCol("NM_KEY", "CC명", 100, false);

                this.SetCommonColumn(grid);

                grid.SettingVersion = "0.0.0.1";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                grid.VerifyNotNull = new string[] { "CD_KEY" };
                grid.VerifyPrimaryKey = new string[] { "CD_KEY" };

                grid.SetCodeHelpCol("CD_KEY", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, "CD_KEY", "NM_KEY");
            }
            else if (grid.Name == this._flex매출처그룹.Name)
            {
                grid.SetCol("CD_KEY", "매출처그룹", 100, true);
                grid.SetCol("NM_KEY", "매출처그룹명", 100, false);

                this.SetCommonColumn(grid);

                grid.SettingVersion = "0.0.0.1";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                grid.VerifyNotNull = new string[] { "CD_KEY" };
                grid.VerifyPrimaryKey = new string[] { "CD_KEY" };

                grid.SetCodeHelpCol("CD_KEY", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, "CD_KEY", "NM_KEY");
            }
            else if (grid.Name == this._flex영업그룹.Name)
            {
                grid.SetCol("CD_KEY", "영업그룹", 100, true);
                grid.SetCol("NM_KEY", "영업그룹명", 100, false);

                this.SetCommonColumn(grid);

                grid.SettingVersion = "0.0.0.1";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                grid.VerifyNotNull = new string[] { "CD_KEY" };
                grid.VerifyPrimaryKey = new string[] { "CD_KEY" };

                grid.SetCodeHelpCol("CD_KEY", HelpID.P_MA_SALEGRP_SUB, ShowHelpEnum.Always, "CD_KEY", "NM_KEY");
            }
            else if (grid.Name == this._flex매출처.Name)
            {
                grid.SetCol("CD_KEY", "매출처", 100, true);
                grid.SetCol("NM_KEY", "매출처명", 100, false);

                this.SetCommonColumn(grid);

                grid.SettingVersion = "0.0.0.1";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                grid.VerifyNotNull = new string[] { "CD_KEY" };
                grid.VerifyPrimaryKey = new string[] { "CD_KEY" };

                grid.SetCodeHelpCol("CD_KEY", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, "CD_KEY", "NM_KEY");
            }
        }

        private void SetCommonColumn(FlexGrid grid)
        {
            try
            {
                grid.SetCol("AM_TOTWON", "연간목표금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_JAN", "1월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_FEB", "2월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_MAR", "3월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_APR", "4월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_MAY", "5월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_JUN", "6월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_JUL", "7월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_AUG", "8월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_SEP", "9월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_OCT", "10월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_NOV", "11월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                grid.SetCol("AM_TOT_DEC", "12월목표", 110, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this._flexCC.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex매출처그룹.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex영업그룹.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex매출처.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);

            this._flex매출처그룹.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex매출처.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp계획년도.Text = Global.MainFrame.GetDateTimeToday().Year.ToString();
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            FlexGrid grid;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;
                grid = this.선택그리드;
                if (grid == null) return;

                grid.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               Global.MainFrame.LoginInfo.Language,
                                                               this.dtp계획년도.Text,
                                                               this.tabControlExt1.SelectedIndex });

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

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            FlexGrid grid;

            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                grid = this.선택그리드;

                if (grid == null) return;
                if (!BeforeAdd()) return;

                grid.Rows.Add();
                grid.Row = grid.Rows.Count - 1;

                grid["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                grid["YY_PLAN"] = this.dtp계획년도.Text;
                grid["TP_PLAN"] = this.tabControlExt1.SelectedIndex;

                grid.AddFinished();
                grid.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            FlexGrid grid;

            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                grid = this.선택그리드;
                grid.Rows.Remove(grid.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            FlexGrid grid;

            try
            {
                if (!base.SaveData() || !base.Verify()) return false;
                grid = this.선택그리드;
                if (grid.IsDataChanged == false) return false;

                if (!this._biz.Save(grid.GetChanges()))
                    return false;

                grid.AcceptChanges();

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }
        #endregion

        #region 그리드 이벤트
        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = this.선택그리드;

                grid["AM_TOTWON"] = (D.GetDecimal(grid["AM_TOT_JAN"])
                                  + D.GetDecimal(grid["AM_TOT_FEB"])
                                  + D.GetDecimal(grid["AM_TOT_MAR"])
                                  + D.GetDecimal(grid["AM_TOT_APR"])
                                  + D.GetDecimal(grid["AM_TOT_MAY"])
                                  + D.GetDecimal(grid["AM_TOT_JUN"])
                                  + D.GetDecimal(grid["AM_TOT_JUL"])
                                  + D.GetDecimal(grid["AM_TOT_AUG"])
                                  + D.GetDecimal(grid["AM_TOT_SEP"])
                                  + D.GetDecimal(grid["AM_TOT_OCT"])
                                  + D.GetDecimal(grid["AM_TOT_NOV"])
                                  + D.GetDecimal(grid["AM_TOT_DEC"]));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID == HelpID.P_MA_CODE_SUB)
                {
                    e.Parameter.P41_CD_FIELD1 = "MA_B000065";
                }
                else
                {
                    e.Parameter.P61_CODE1 = "100";
                    e.Parameter.P11_ID_MENU = "82";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion
    }
}
