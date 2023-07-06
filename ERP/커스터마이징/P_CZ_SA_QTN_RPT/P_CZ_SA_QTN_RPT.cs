using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;

namespace cz
{
	public partial class P_CZ_SA_QTN_RPT : PageBase
	{
		DataTable dtCond = new DataTable();

		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_QTN_RPT()
		{
			StartUp.Certify(this);
			InitializeComponent();
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			dtp작성일자.StartDateToString = Util.GetToday(-90);
			dtp작성일자.EndDateToString = Util.GetToday();

			SetControl str = new SetControl();
			str.SetCombobox(cbo거래처그룹, MA.GetCode("MA_B000065", true));
			str.SetCombobox(cbo진행상태, MA.GetCode("SA_B000023", true));

			dtCond.Columns.Add("CD_COMPANY");
			dtCond.Columns.Add("NO_FILE");
			dtCond.Columns.Add("DT_INQ_F");
			dtCond.Columns.Add("DT_INQ_T");
			dtCond.Columns.Add("NO_EMP");
			dtCond.Columns.Add("CD_PARTNER");
			dtCond.Columns.Add("CD_PARTNER_GRP");
			dtCond.Columns.Add("CD_SALEGRP");
			dtCond.Columns.Add("NO_IMO");
			dtCond.Columns.Add("NO_REF");
			dtCond.Columns.Add("CD_SUPPLIER");
			dtCond.Columns.Add("STA_QTN");
			dtCond.Columns.Add("MODE");
		}

		private void InitGrid()
		{
			InitGrid_NO();

			InitGrid_COMMON_H(flexH_PTN_S);
			InitGrid_COMMON_L(flexL_PTN_S);

			InitGrid_COMMON_H(flexH_PTN_P);
			InitGrid_COMMON_L(flexL_PTN_P);

			InitGrid_COMMON_H(flexH_EMP);
			InitGrid_COMMON_L(flexL_EMP);

			InitGrid_COMMON_H(flexH_STA);
			InitGrid_COMMON_L(flexL_STA);
		}

		private void InitGrid_NO()
		{
			flexH_NO.BeginSetting(1, 1, false);
				
			flexH_NO.SetCol("NO_FILE"			, "파일번호"		, 100);
			flexH_NO.SetCol("LN_PARTNER"		, "매출처"		, 150);
			flexH_NO.SetCol("NM_VESSEL"			, "선명"			, 150);
			flexH_NO.SetCol("NO_REF"			, "문의번호"		, 110);
			flexH_NO.SetCol("ITEM_GRP"			, "품목군"		, 150);
			flexH_NO.SetCol("ITEM_CNT"			, "아이템 수"		, 50);
			flexH_NO.SetCol("NM_EMP"			, "담당자"		, 70);
			flexH_NO.SetCol("NM_PARTNER_GRP"	, "거래처그룹"	, 90);
			flexH_NO.SetCol("NM_NATION"			, "국가"			, 90);
			flexH_NO.SetCol("DT_INQ"			, "접수일자"		, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH_NO.SetCol("DT_QTN"			, "견적일자"		, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH_NO.SetCol("DT_CLOSE"			, "종결일자"		, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH_NO.SetCol("NM_EXCH"			, "통화명"		, 90);
			//flexH_NO.SetCol("AM_EX"				, "견적금액"		, 110);
			//flexH_NO.SetCol("AM_KR"				, "원화금액"		, 110);
			flexH_NO.SetCol("AM_EX_P"			, "매입금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexH_NO.SetCol("AM_KR_P"			, "매입금액(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexH_NO.SetCol("AM_EX_S"			, "매출금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexH_NO.SetCol("AM_KR_S"			, "매출금액(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexH_NO.SetCol("RT_MARGIN"			, "이윤율"		, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexH_NO.SetCol("AM_EX_MARGIN"		, "이윤"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexH_NO.SetCol("AM_KR_MARGIN"		, "이윤(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);

			 
			flexH_NO.SetCol("STA_QTN"			, "진행상태"		, 80);
			flexH_NO.SetCol("SUPPLIER"			, "문의처"		, 200);
			flexH_NO.SetCol("SUPPLIER"			, "매입처"		, 200);

			flexH_NO.Cols["NM_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			flexH_NO.Cols["STA_QTN"].TextAlign = TextAlignEnum.CenterCenter;
			flexH_NO.SetDataMap("STA_QTN", MA.GetCode("SA_B000023"), "CODE", "NAME");
				
			flexH_NO.SettingVersion = "16.02.23.03";
			flexH_NO.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ==================================================================================================== L
			InitGrid_COMMON_L(flexL_NO);
			flexL_NO.Cols["NO_FILE"].Visible = false;
		}

		private void InitGrid_COMMON_H(FlexGrid flexH)
		{
			flexH.BeginSetting(1, 1, false);

			if (flexH == flexH_PTN_S)
			{
				flexH_PTN_S.SetCol("CD_PARTNER"	, "거래처코드"	, false);
				flexH_PTN_S.SetCol("LN_PARTNER"	, "매출처"		, 150);
			}
			else if (flexH == flexH_PTN_P)
			{
				flexH_PTN_P.SetCol("CD_SUPPLIER", "거래처코드"	, false);
				flexH_PTN_P.SetCol("LN_SUPPLIER", "매입처"		, 150);
			}
			else if (flexH == flexH_EMP)
			{
				flexH_EMP.SetCol("NO_EMP"		, "사번"			, false);
				flexH_EMP.SetCol("NM_KOR"		, "담당자"		, 80);
				flexH_EMP.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
			}
			else if (flexH == flexH_STA)
			{
				flexH_STA.SetCol("STA_QTN"		, "진행상태"		, 80);
				flexH_STA.Cols["STA_QTN"].TextAlign = TextAlignEnum.CenterCenter;
				flexH_STA.SetDataMap("STA_QTN", MA.GetCode("SA_B000023"), "CODE", "NAME");
			}

			flexH.SetCol("CNT_FILE"	, "견적건수"		, 60	, false, typeof(decimal), FormatTpType.MONEY);
			flexH.SetCol("CNT_ITEM"	, "아이템수"		, 60	, false, typeof(decimal), FormatTpType.MONEY);
			flexH.SetCol("AM_KR_S"	, "견적금액"		, 110	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			flexH.SettingVersion = "15.06.19.07";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
		}

		private void InitGrid_COMMON_L(FlexGrid flexL)
		{
			flexL.BeginSetting(1, 1, false);

			flexL.SetCol("NO_FILE"			, "파일번호"		, 100);			
			flexL.SetCol("CD_PARTNER"		, "매출처코드"	, false);
			flexL.SetCol("LN_PARTNER"		, "매출처"		, 150);
			flexL.SetCol("NM_VESSEL"		, "선명"			, 150);
			flexL.SetCol("NO_LINE"			, "고유번호"		, false);
			flexL.SetCol("NO_DSP"			, "순번"			, 45);
			flexL.SetCol("NM_SUBJECT"		, "주제"			, false);
			flexL.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			flexL.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 230);
			flexL.SetCol("NM_UNIT"			, "단위"			, 50);
			flexL.SetCol("QT"				, "수량"			, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			flexL.SetCol("UM_EX_P"			, "구매단가"		, false);
			flexL.SetCol("AM_EX_P"			, "구매금액"		, false);
			flexL.SetCol("UM_KR_P"			, "구매단가(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_P"			, "구매금액(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("RT_PROFIT"		, "이윤(%)"		, 60	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("RT_DC"			, "DC(%)"		, 60	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_EX_S"			, "판매단가"		, 110	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_S"			, "판매금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_KR_S"			, "판매단가(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_S"			, "판매금액(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);			
			flexL.SetCol("RT_MARGIN"		, "최종(%)"		, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("LT"				, "납기"			, 50	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("CD_SUPPLIER"		, "매입처코드"	, false);
			flexL.SetCol("LN_SUPPLIER"		, "매입처"		, 150);
			flexL.SetCol("STA_QTN"			, "진행상태"		, 80);

			flexL.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NM_UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["STA_QTN"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.SetDataMap("STA_QTN", MA.GetCode("SA_B000023"), "CODE", "NAME");
			

			flexL.SettingVersion = "15.09.18.02";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flexL.SetExceptSumCol("RT_PROFIT", "RT_DC", "RT_MARGIN", "LT");
		}

		private void InitEvent()
		{
			flexH_NO.AfterRowChange += new RangeEventHandler(flex_AfterRowChange);
			flexH_PTN_S.AfterRowChange += new RangeEventHandler(flex_AfterRowChange);
			flexH_PTN_P.AfterRowChange += new RangeEventHandler(flex_AfterRowChange);
			flexH_EMP.AfterRowChange += new RangeEventHandler(flex_AfterRowChange);
			flexH_STA.AfterRowChange += new RangeEventHandler(flex_AfterRowChange);
		}

		protected override void InitPaint()
		{
			spl거래처별.SplitterDistance = 292;
			spl매입처별.SplitterDistance = 292;
			spl담당자별.SplitterDistance = 243;
			spl견적상태별.SplitterDistance = 243;
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			FlexGrid flex = (FlexGrid)sender;
			
			if (flex == flexH_NO) flexL_NO.Binding = SearchL("NO_FILE", flex["NO_FILE"]);			
			if (flex == flexH_PTN_S) flexL_PTN_S.Binding = SearchL("CD_PARTNER", flex["CD_PARTNER"]);
			if (flex == flexH_PTN_P) flexL_PTN_P.Binding = SearchL("CD_SUPPLIER", flex["CD_SUPPLIER"]);			
			if (flex == flexH_EMP) flexL_EMP.Binding = SearchL("NO_EMP", flex["NO_EMP"]);
			if (flex == flexH_STA) flexL_STA.Binding = SearchL("STA_QTN", flex["STA_QTN"]);			
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			FlexGrid flexH = (FlexGrid)tab.SelectedTab.Controls[0].Controls[0].Controls[0];
			FlexGrid flexL = (FlexGrid)tab.SelectedTab.Controls[0].Controls[1].Controls[0];
			if (flexL.DataTable != null) { flexL.DataTable.Rows.Clear(); flexL.AcceptChanges(); }

			DataTable dtH = SearchH();
			flexH.Binding = dtH;						
		}

		private DataTable SearchH()
		{
			DBParameters dbp = GetSearchCond();
			return DBMgr.GetDataTable("SP_CZ_SA_QTNH_RPT_SELECT", dbp);
		}

		private DataTable SearchL(string keyName, object keyValue)
		{
			DBParameters dbp = GetSearchCond();
			dbp[keyName] = keyValue;
			return DBMgr.GetDataTable("SP_CZ_SA_QTNL_RPT_SELECT", dbp);
		}

		private DBParameters GetSearchCond()
		{
			DBParameters dbp = new DBParameters();
			dbp.Add("MODE"				, tab.SelectedTab.Tag);
			dbp.Add("CD_COMPANY"		, CD_COMPANY);	
			dbp.Add("NO_FILE"			, txt파일번호.Text);
			dbp.Add("DT_INQ_F"			, dtp작성일자.StartDateToString);
			dbp.Add("DT_INQ_T"			, dtp작성일자.EndDateToString);
			dbp.Add("NO_EMP"			, ctx담당자.CodeValue);
			dbp.Add("CD_PARTNER"		, ctx매출처.CodeValue);
			dbp.Add("CD_PARTNER_GRP"	, cbo거래처그룹.SelectedValue);
			dbp.Add("CD_SALEGRP"		, ctx영업그룹.CodeValue);
			dbp.Add("NO_IMO"			, ctx호선번호.CodeValue);
			dbp.Add("NO_REF"			, txt문의번호.Text);
			dbp.Add("CD_SUPPLIER"		, ctx매입처.CodeValue);
			dbp.Add("STA_QTN"			, cbo진행상태.SelectedValue);

			return dbp;
		}

		#endregion
	}
}


