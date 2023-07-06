using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
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
	public partial class P_CZ_PR_WO_ROUT_EQUIP_MNG : PageBase
	{
        P_CZ_PR_WO_ROUT_EQUIP_MNG_BIZ _biz = new P_CZ_PR_WO_ROUT_EQUIP_MNG_BIZ();

        public P_CZ_PR_WO_ROUT_EQUIP_MNG()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
            #region 설비할당

            #region 공정
            this._flex공정.BeginSetting(1, 1, false);

            this._flex공정.SetCol("NM_ST_WO", "작업지시상태", 100);
            this._flex공정.SetCol("NO_WO", "작업지시번호", 100);
            this._flex공정.SetCol("NM_FG_WO", "작업지시구분", 100);
            this._flex공정.SetCol("NM_TP_ROUT", "공정경로", 100);
            this._flex공정.SetCol("CD_ITEM", "품목코드", 100);
            this._flex공정.SetCol("NM_ITEM", "품목명", 100);
            this._flex공정.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex공정.SetCol("UNIT_IM", "단위", 100);
            this._flex공정.SetCol("QT_ITEM", "지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NM_ST_OP", "공정상태", 100);
            this._flex공정.SetCol("NM_FG_WC", "W/C타입", 90);
            this._flex공정.SetCol("CD_OP", "OP", 40);
            this._flex공정.SetCol("NM_OP", "공정명", 100);
            this._flex공정.SetCol("NM_WC", "작업장명", 80);
            this._flex공정.SetCol("QT_WO", "지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("QT_START", "입고수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("QT_WORK", "작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("QT_REJECT", "불량수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("QT_REWORK", "재작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("QT_MOVE", "이동수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("QT_WIP", "대기수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("RT_WORK", "진행율(%)", 70, false, typeof(decimal), FormatTpType.MONEY);
            this._flex공정.SetCol("DT_REL", "시작일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex공정.SetCol("DT_DUE", "종료일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex공정.SetCol("CD_EQUIP", "설비코드", 100, false);
            this._flex공정.SetCol("NM_EQUIP", "설비명", 100, false);

            this._flex공정.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 설비
            this._flex설비.BeginSetting(1, 1, false);

            this._flex설비.SetCol("NM_EQUIP_GRP", "설비그룹", 100);
            this._flex설비.SetCol("CD_EQUIP", "설비코드", 100);
            this._flex설비.SetCol("NM_EQUIP", "설비명", 100);
            this._flex설비.SetCol("QT_TOTAL", "총건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비.SetCol("QT_WO", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비.SetCol("QT_REMAIN", "작업잔량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex설비.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #endregion

            #region 설비별작업현황

            #region 설비
            this._flex설비1.BeginSetting(1, 1, false);

            this._flex설비1.SetCol("NM_EQUIP_GRP", "설비그룹", 100);
            this._flex설비1.SetCol("CD_EQUIP", "설비코드", 100);
            this._flex설비1.SetCol("NM_EQUIP", "설비명", 100);
            this._flex설비1.SetCol("QT_TOTAL", "총건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비1.SetCol("QT_WO", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비1.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비1.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비1.SetCol("QT_REMAIN", "작업잔량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex설비1.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 공정
            this._flex공정1.BeginSetting(1, 1, false);

            this._flex공정1.SetCol("NM_ST_WO", "작업지시상태", 100);
            this._flex공정1.SetCol("NO_WO", "작업지시번호", 100);
            this._flex공정1.SetCol("NM_FG_WO", "작업지시구분", 100);
            this._flex공정1.SetCol("NM_TP_ROUT", "공정경로", 100);
            this._flex공정1.SetCol("CD_ITEM", "품목코드", 100);
            this._flex공정1.SetCol("NM_ITEM", "품목명", 100);
            this._flex공정1.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex공정1.SetCol("UNIT_IM", "단위", 100);
            this._flex공정1.SetCol("QT_ITEM", "지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("NM_ST_OP", "공정상태", 100);
            this._flex공정1.SetCol("NM_FG_WC", "W/C타입", 90);
            this._flex공정1.SetCol("CD_OP", "OP", 40);
            this._flex공정1.SetCol("NM_OP", "공정명", 100);
            this._flex공정1.SetCol("NM_WC", "작업장명", 80);
            this._flex공정1.SetCol("QT_WO", "지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("QT_START", "입고수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("QT_WORK", "작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("QT_REJECT", "불량수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("QT_REWORK", "재작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("QT_MOVE", "이동수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("QT_WIP", "대기수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정1.SetCol("RT_WORK", "진행율(%)", 70, false, typeof(decimal), FormatTpType.MONEY);
            this._flex공정1.SetCol("DT_REL", "시작일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex공정1.SetCol("DT_DUE", "종료일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex공정1.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #endregion
        }

		private void InitEvent()
		{
			this._flex공정.AfterRowChange += _flex공정_AfterRowChange;
			this._flex설비1.AfterRowChange += _flex설비1_AfterRowChange;

			this.ctx설비.QueryBefore += Ctx설비_QueryBefore;
			this.btn할당.Click += Btn할당_Click;
			this.btn할당취소.Click += Btn할당취소_Click;
		}

		private void Ctx설비_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
            try
            {
                e.HelpParam.P00_CHILD_MODE = "설비 도움창";
                e.HelpParam.P61_CODE1 = "WE.CD_EQUIP AS CODE, EQ.NM_EQUIP AS NAME ";
                e.HelpParam.P62_CODE2 = @"PR_WCOP_EQUIP WE
										  LEFT JOIN PR_EQUIP EQ ON WE.CD_COMPANY = EQ.CD_COMPANY AND WE.CD_PLANT = EQ.CD_PLANT AND WE.CD_EQUIP = EQ.CD_EQUIP";
                e.HelpParam.P63_CODE3 = string.Format(@"WHERE WE.CD_COMPANY = '{0}'
														AND WE.CD_PLANT = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, this.cbo공장.SelectedValue.ToString());
                e.HelpParam.P64_CODE4 = "GROUP BY WE.CD_EQUIP, EQ.NM_EQUIP";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		protected override void InitPaint()
		{
			base.InitPaint();

            this.cbo공장.DataSource = Global.MainFrame.GetComboDataCombine("NC;MA_PLANT");
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;

            this.dtp작업기간.StartDateToString = this.MainFrameInterface.GetStringToday.ToString().Substring(0, 6) + "01";
            this.dtp작업기간.EndDateToString = this.MainFrameInterface.GetStringToday;
        }

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (this.tabControlExt1.SelectedTab == this.tpg설비할당)
				{
                    this._flex공정.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             this.cbo공장.SelectedValue.ToString(),
                                                                             this.dtp작업기간.StartDateToString,
                                                                             this.dtp작업기간.EndDateToString,
                                                                             this.txt작업지시번호.Text,
                                                                             this.ctx설비.CodeValue,
                                                                             (this.chk마감제외.Checked == true ? "Y" : "N") });

                    if (!this._flex공정.HasNormalRow)
                    {
                        this.ShowMessage(PageResultMode.SearchNoData);
                    }
                }
                else if (this.tabControlExt1.SelectedTab == this.tpg설비별작업현황)
				{
                    this._flex설비1.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                               this.cbo공장.SelectedValue.ToString(),
                                                                               this.dtp작업기간.StartDateToString,
                                                                               this.dtp작업기간.EndDateToString,
                                                                               this.txt작업지시번호.Text,
                                                                               this.ctx설비.CodeValue,
                                                                               (this.chk마감제외.Checked == true ? "Y" : "N") });

                    if (!this._flex설비1.HasNormalRow)
                    {
                        this.ShowMessage(PageResultMode.SearchNoData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn할당_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex공정["CD_EQUIP"] = this._flex설비["CD_EQUIP"];

                this._biz.Save(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                              this._flex공정["NO_WO"].ToString(),
                                              this._flex공정["NO_LINE"].ToString(),
                                              this._flex공정["CD_EQUIP"].ToString(),
                                              Global.MainFrame.LoginInfo.UserID });

                this._flex공정_AfterRowChange(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn할당취소_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex공정["CD_EQUIP"] = string.Empty;

                this._biz.Save(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                              this._flex공정["NO_WO"].ToString(),
                                              this._flex공정["NO_LINE"].ToString(),
                                              this._flex공정["CD_EQUIP"].ToString(),
                                              Global.MainFrame.LoginInfo.UserID });

                this._flex공정_AfterRowChange(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex공정_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this._flex설비.Binding = this._biz.SearchEq(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           this.cbo공장.SelectedValue.ToString(),
                                                                           this._flex공정["CD_WC"].ToString(),
                                                                           this._flex공정["CD_WCOP"].ToString(),
                                                                           this.dtp작업기간.StartDateToString,
                                                                           this.dtp작업기간.EndDateToString,
                                                                           this.txt작업지시번호.Text,
                                                                           (this.chk마감제외.Checked == true ? "Y" : "N") });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex설비1_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this._flex공정1.Binding = this._biz.SearchWo(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                            this.cbo공장.SelectedValue.ToString(),
                                                                            this._flex설비1["CD_EQUIP"].ToString(),
                                                                            this.dtp작업기간.StartDateToString,
                                                                            this.dtp작업기간.EndDateToString,
                                                                            this.txt작업지시번호.Text,
                                                                            (this.chk마감제외.Checked == true ? "Y" : "N") });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
