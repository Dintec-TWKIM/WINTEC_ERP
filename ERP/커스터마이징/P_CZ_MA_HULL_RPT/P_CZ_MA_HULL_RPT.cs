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
using Dintec;
using Duzon.Common.BpControls;

namespace cz
{
	public partial class P_CZ_MA_HULL_RPT : PageBase
	{
		P_CZ_MA_HULL_RPT_BIZ _biz;

		public P_CZ_MA_HULL_RPT()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this._biz = new P_CZ_MA_HULL_RPT_BIZ();

			this.InitGrid();
            this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.cbo국가.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000020");
			this.cbo국가.ValueMember = "CODE";
			this.cbo국가.DisplayMember = "NAME";

			this.cbo조회기간.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002", "003" }, new string[] { "견적일자", "수주일자", "선박납기일자", "수통일자" });
			this.cbo조회기간.ValueMember = "CODE";
			this.cbo조회기간.DisplayMember = "NAME";
            this.cbo조회기간.SelectedValue = "001";

			this.dtp조회기간.StartDate = this.MainFrameInterface.GetDateTimeToday().AddYears(-1);
			this.dtp조회기간.EndDate = this.MainFrameInterface.GetDateTimeToday();
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flexH };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			#region Header
			this._flexH.BeginSetting(3, 1, false);

			this._flexH.SetCol("NO_IMO", "IMO번호", 60);
			this._flexH.SetCol("NO_HULL", "호선번호", 60);
			this._flexH.SetCol("NM_VESSEL", "호선명", 150);
			this._flexH.SetCol("NM_SHIP_OWNER", "선주", 150);
			this._flexH.SetCol("NM_PARTNER", "운항선사", 150);
			this._flexH.SetCol("NM_NATION", "국가", 80);
			this._flexH.SetCol("DC_SHIPBUILDER", "조선소", 150);
			this._flexH.SetCol("DT_SHIP_DLV", "선박납기일자", 60, false, typeof(string), FormatTpType.YEAR_MONTH);
			this._flexH.SetCol("NM_TP_SHIP", "호선유형", 150);
			this._flexH.SetCol("YN_NEWSHIP", "특수선여부", 60, true, CheckTypeEnum.Y_N);
			this._flexH.SetCol("QT_DEAD_WEIGHT", "DeadWeight", 60, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("ME1_MAKER", "제조사", 80);
			this._flexH.SetCol("ME1_MODEL", "모델", 80);
			this._flexH.SetCol("GE1_MAKER", "제조사", 80);
			this._flexH.SetCol("GE1_MODEL", "모델", 80);
			this._flexH.SetCol("GE2_MAKER", "제조사", 80);
			this._flexH.SetCol("GE2_MODEL", "모델", 80);
			this._flexH.SetCol("TC1_MAKER", "제조사", 80);
			this._flexH.SetCol("TC1_MODEL", "모델", 80);
			this._flexH.SetCol("TC2_MAKER", "제조사", 80);
			this._flexH.SetCol("TC2_MODEL", "모델", 80);
			this._flexH.SetCol("TC3_MAKER", "제조사", 80);
			this._flexH.SetCol("TC3_MODEL", "모델", 80);
            
            this._flexH.SetCol("QT_QTN", "건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("AM_EX_QTN_TOTAL", "외화총액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_QTN_TOTAL", "총액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_QTN_ME", "메인엔진", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_QTN_AE", "발전기\n(힘센엔진포함)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_QTN_EQ", "조선기자재", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_QTN_GS", "선용품", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_QTN_ETC", "기타", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetCol("QT_SO", "건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("AM_EX_SO_TOTAL", "외화총액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SO_TOTAL", "총액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_SO_ME", "메인엔진", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_SO_AE", "발전기\n(힘센엔진포함)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_SO_EQ", "조선기자재", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_SO_GS", "선용품", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_SO_ETC", "기타", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			
            this._flexH.SetCol("DC_RMK", "호선비고", 150);
			this._flexH.SetCol("DC_RMK_RPT", "비고", 150, true);

			this._flexH[0, "ME1_MAKER"] = "대형엔진";
			this._flexH[0, "ME1_MODEL"] = "대형엔진";

			this._flexH[0, "GE1_MAKER"] = "중형엔진1";
			this._flexH[0, "GE1_MODEL"] = "중형엔진1";
			this._flexH[0, "GE2_MAKER"] = "중형엔진2";
			this._flexH[0, "GE2_MODEL"] = "중형엔진2";

			this._flexH[0, "TC1_MAKER"] = "터보차저1";
			this._flexH[0, "TC1_MODEL"] = "터보차저1";
			this._flexH[0, "TC2_MAKER"] = "터보차저2";
			this._flexH[0, "TC2_MODEL"] = "터보차저2";
			this._flexH[0, "TC3_MAKER"] = "터보차저3";
			this._flexH[0, "TC3_MODEL"] = "터보차저3";

            this._flexH[0, "QT_QTN"] = "견적";
            this._flexH[0, "AM_EX_QTN_TOTAL"] = "견적";
            this._flexH[0, "AM_QTN_TOTAL"] = "견적";
            this._flexH[0, "AM_QTN_ME"] = "견적";
            this._flexH[0, "AM_QTN_AE"] = "견적";
            this._flexH[0, "AM_QTN_EQ"] = "견적";
            this._flexH[0, "AM_QTN_GS"] = "견적";
            this._flexH[0, "AM_QTN_ETC"] = "견적";

            this._flexH[0, "QT_SO"] = "수주";
            this._flexH[0, "AM_EX_SO_TOTAL"] = "수주";
            this._flexH[0, "AM_SO_TOTAL"] = "수주";
			this._flexH[0, "AM_SO_ME"] = "수주";
			this._flexH[0, "AM_SO_AE"] = "수주";
			this._flexH[0, "AM_SO_EQ"] = "수주";
			this._flexH[0, "AM_SO_GS"] = "수주";
			this._flexH[0, "AM_SO_ETC"] = "수주";

			this._flexH.ExtendLastCol = true;

			this._flexH.SettingVersion = "0.0.0.2";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetExceptSumCol("QT_DEAD_WEIGHT");
			#endregion

			#region Line
			this._flexL.BeginSetting(1, 1, false);

			this._flexL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexL.SetCol("NO_SO", "수주번호", 80);
			this._flexL.SetCol("NM_ITEMGRP", "품목유형", 80);
			this._flexL.SetCol("NO_DSP", "순번", 60);
			this._flexL.SetCol("CD_ITEM", "품목코드", 80);
			this._flexL.SetCol("NM_ITEM", "품목명", 80);
			this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 80);
			this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 80);
			this._flexL.SetCol("UNIT_SO", "단위", 60);
			this._flexL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("NM_EXCH", "통화명", 80);
			this._flexL.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexL.SetCol("UM_EX_P", "매입단가(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("UM_KR_P", "매입단가(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_EX_P", "매입금액(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_KR_P", "매입금액(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("UM_EX_S", "매출단가(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("UM_KR_S", "매출단가(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_EX_S", "매출금액(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_KR_S", "매출금액(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flexL.SettingVersion = "0.0.0.1";
			this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexL.SetExceptSumCol("RT_EXCH", "UM_EX_P", "UM_KR_P", "UM_EX_S", "UM_KR_S");
			#endregion
		}

		private void InitEvent()
		{
            this.bpc파일유형.QueryBefore += new BpQueryHandler(this.bpc파일유형_QueryBefore);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.MouseDoubleClick += new MouseEventHandler(this._flexH_MouseDoubleClick);
		}

		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string key, filter;

			try
			{
				key = D.GetString(this._flexH["NO_IMO"]);
				filter = "NO_IMO ='" + key + "'";

				if (this._flexH.DetailQueryNeed)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   key,
															   this.cbo조회기간.SelectedValue.ToString(),
															   this.dtp조회기간.StartDateToString,
															   this.dtp조회기간.EndDateToString,
                                                               this.bpc파일유형.QueryWhereIn_Pipe,
															   this.ctx매입처.CodeValue,
															   this.ctx매출처.CodeValue });
				}
				
				this._flexL.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexH_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			string pageId, pageName;

			try
			{
				if (this._flexH.HasNormalRow == false) return;
				if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;
				if (this._flexH.ColSel != this._flexH.Cols["NO_IMO"].Index) return;
				if (string.IsNullOrEmpty(D.GetString(this._flexH["NO_IMO"]))) return;

				pageId = "P_CZ_SA_INQ_RPT";
				pageName = "첨부파일등록현황";

				if (Global.MainFrame.IsExistPage(pageId, false))
					Global.MainFrame.UnLoadPage(pageId, false);

				Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, new object[] { D.GetString(this._flexH["NO_IMO"]),
																						   D.GetString(this._flexH["NO_HULL"]),
																						   D.GetString(this._flexH["NM_VESSEL"]) });
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	  D.GetString(this.cbo조회기간.SelectedValue),
																	  this.dtp조회기간.StartDateToString,
																	  this.dtp조회기간.EndDateToString,
																	  this.ctx운항선사.CodeValue,
																	  this.txtIMO번호.Text,
																	  this.txt호선번호.Text,
																	  this.txt호선명.Text,
																	  D.GetString(this.cbo국가.SelectedValue),
																	  this.txt조선소.Text,
																	  (this.chk신조선여부.Checked == true ? "Y" : "N"),
                                                                      (this.chk견적금액보기.Checked == true ? "Y" : "N"),
                                                                      this.bpc파일유형.QueryWhereIn_Pipe,
																	  this.ctx매입처.CodeValue,
																	  this.ctx매출처.CodeValue });

				if (!_flexH.HasNormalRow)
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
					ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;
			if (this._flexH.IsDataChanged == false) return false;

			if (!this._biz.Save(this._flexH.GetChanges())) return false;

			this._flexH.AcceptChanges();

			return true;
		}

        private void bpc파일유형_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = "CZ_SA00023";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
	}
}
