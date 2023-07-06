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
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_FORWARDER_RPT : PageBase
    {
        P_CZ_SA_FORWARDER_RPT_BIZ _biz;

        public P_CZ_SA_FORWARDER_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.MainGrids = new FlexGrid[] { this._flex };
            this._biz = new P_CZ_SA_FORWARDER_RPT_BIZ(); 

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(2, 1, false);

            this._flex.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_GIR", "의뢰번호", 80);
            this._flex.SetCol("NO_SO", "수주번호", 80);
            this._flex.SetCol("NM_KOR", "담당자", 60);
            this._flex.SetCol("PORT_ARRIVER", "도착지", 60);
            this._flex.SetCol("NM_SUB_CATEGORY", "구분", 60, true);
            this._flex.SetCol("WEIGHT", "무게", 60, true, typeof(decimal));
            
            this._flex.SetCodeHelpCol("NM_SUB_CATEGORY", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_SUB_CATEGORY", "NM_SUB_CATEGORY" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.FastMode);
            this._flex.SetCodeHelpCol("NM_FORWARDER", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_FORWARDER", "NM_FORWARDER" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.FastMode);
            this._flex.SetCodeHelpCol("NM_FG_REASON", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "FG_REASON", "NM_FG_REASON" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.FastMode);

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.SetExceptSumCol("WEIGHT");
        }

        private void InitEvent()
        {
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.dtp의뢰일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
                this.dtp의뢰일자.EndDateToString = Global.MainFrame.GetStringToday;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this.동적컬럼생성();

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     Global.MainFrame.LoginInfo.Language,
                                                                     this.dtp의뢰일자.StartDateToString,
                                                                     this.dtp의뢰일자.EndDateToString,
                                                                     this.ctx담당자.CodeValue,
                                                                     this.txt의뢰번호.Text });

                if (!this._flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                else
                    this.점유율계산();
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

                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(PageResultMode.SaveGood);
                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex.IsDataChanged == false) return false;

            if (!this._biz.SaveData(this._flex.GetChanges())) return false;

            this._flex.AcceptChanges();

            return true;
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "NM_SUB_CATEGORY":
                        e.Parameter.P41_CD_FIELD1 = "CZ_SA00011";
                        break;
                    case "NM_FORWARDER":
                        e.Parameter.P41_CD_FIELD1 = "CZ_SA00015";
                        break;
                    case "NM_FG_REASON":
                        e.Parameter.P41_CD_FIELD1 = "CZ_SA00031";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "NM_SUB_CATEGORY":
                        if (e.Result.CodeValue != "001" && e.Result.CodeValue != "002")
                            e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 동적컬럼생성()
        {
            DataTable dt;
            string query, code, name;

            try
            {
                query = @"SELECT CD_SYSDEF, NM_SYSDEF 
                          FROM MA_CODEDTL WITH(NOLOCK) 
                          WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                        @"AND CD_FIELD = 'CZ_SA00015'
                          AND USE_YN = 'Y'
                          ORDER BY CD_SYSDEF";

                dt = DBHelper.GetDataTable(query);

                this._flex.BeginSetting(2, 1, false);
                this._flex.Cols.Count = 8;

                foreach (DataRow dr in dt.Rows)
                {
                    code = "AM_PRICE_" + D.GetString(dr["CD_SYSDEF"]);
                    name = D.GetString(dr["NM_SYSDEF"]);

                    this._flex.SetCol(code, name, 60, true, typeof(decimal));

                    this._flex[0, this._flex.Cols[code].Index] = "운임(견적)";
                }

                this._flex.SetCol("NM_FORWARDER", "업체", 80, true);
                this._flex.SetCol("NM_FG_REASON", "사유", 80, true);

                this._flex.AllowCache = false;
                this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 점유율계산()
        {
            decimal 총액;

            try
            {
                총액 = D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_PRICE)", string.Empty));

                if (총액 > 0)
                {
                    this.cur점유율A.DecimalValue = (D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_PRICE)", "CD_FORWARDER = '001'")) / 총액) * 100;
                    this.cur점유율B.DecimalValue = (D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_PRICE)", "CD_FORWARDER = '002'")) / 총액) * 100;
                    this.cur점유율C.DecimalValue = (D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_PRICE)", "CD_FORWARDER = '004'")) / 총액) * 100;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
