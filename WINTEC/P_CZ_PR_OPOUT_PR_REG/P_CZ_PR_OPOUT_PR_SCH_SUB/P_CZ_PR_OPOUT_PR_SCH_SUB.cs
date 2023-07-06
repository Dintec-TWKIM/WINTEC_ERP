using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_OPOUT_PR_SCH_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_PR_OPOUT_PR_SCH_SUB_BIZ _biz = new P_CZ_PR_OPOUT_PR_SCH_SUB_BIZ();
        public P_CZ_PR_OPOUT_PR_SCH_SUB()
        {
            InitializeComponent();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp요청일자.StartDateToString = Global.MainFrame.GetStringToday.Substring(0, 6) + "01";
            this.dtp요청일자.EndDateToString = Global.MainFrame.GetStringToday;
            this.ctx공장.CodeValue = Global.MainFrame.LoginInfo.CdPlant;
            this.ctx공장.CodeName = Global.MainFrame.LoginInfo.NmPlant;
        }

        private void InitEvent()
        {
            this._flexM.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);

            this.btn검색.Click += new EventHandler(this.btn검색_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        private void InitGrid()
        {
            this._flexM.DetailGrids = new FlexGrid[] { this._flexD };
            #region _flexM
            this._flexM.BeginSetting(1, 1, false);

            this._flexM.SetCol("NO_PR", "요청번호", 100);
            this._flexM.SetCol("DT_PR", "요청일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("NM_KOR", "요청자", 100);

            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region _flexD
            this._flexD.BeginSetting(1, 1, false);

            this._flexD.SetCol("NO_WO", "작업지시번호", 100);
            this._flexD.SetCol("NM_TP_WO", "오더형태", 100, false);
            this._flexD.SetCol("CD_OP", "OP.", 100);
            this._flexD.SetCol("CD_WC", "작업장", 60, false);
            this._flexD.SetCol("NM_WC", "작업장명", 120, false);
            this._flexD.SetCol("CD_WCOP", "공정", 80, false);
            this._flexD.SetCol("NM_OP", "공정명", 120, false);
            this._flexD.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flexD.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexD.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD.SetCol("QT_PR", "요청수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;
                string str1 = D.GetString(this._flexM[e.NewRange.r1, "NO_PR"]);
                string str2 = "NO_PR = '" + str1 + "'";
                if (this._flexM.DetailQueryNeed)
                    dt= this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                              Global.MainFrame.LoginInfo.CdPlant,
                                                              this._flexM["NO_PR"].ToString() });
                this._flexD.BindingAdd(dt, str2);
                this._flexM.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn검색_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexM.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.ctx공장.CodeValue,
                                                                      this.dtp요청일자.StartDateToString,
                                                                      this.dtp요청일자.EndDateToString });

                if (!this._flexM.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnClosed(e);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public string[] returnParams
        {
            get
            {
                string[] returnParams = new string[2];
                returnParams[0] = D.GetString(this._flexM["NO_PR"]);

                return returnParams;
            }
        }
    }
}
