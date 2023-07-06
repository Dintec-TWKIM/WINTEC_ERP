using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Linq;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Net;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace cz
{
	public partial class P_CZ_PU_PO_TOOL_RPT : PageBase
	{
		public P_CZ_PU_PO_TOOL_RPT()
		{
			InitializeComponent();
			StartUp.Certify(this);
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			
		}

		protected override void InitPaint()
		{
			
		}
		
		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			// ********** 헤드
			grd헤드.BeginSetting(1, 1, false);
			   
			grd헤드.SetCol("NO_TOOL"		, "도구번호"	, 100);
			grd헤드.SetCol("DT_TOOL"		, "날짜"		, 100	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd헤드.SetCol("NM_EMP"		, "담당자"	, 100);
			grd헤드.SetCol("NM_MODEL"	, "엔진모델"	, 150);
			grd헤드.SetCol("NO_PO"		, "발주번호"	, 150);
			grd헤드.SetCol("NO_REF"		, "견적번호"	, 150);
			grd헤드.SetCol("CD_STATUS"	, "진행상황"	, 180);
			grd헤드.SetCol("DC_RMK"		, "비고"		, 500);
			   
			grd헤드.Cols["NO_TOOL"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.SetDataMap("CD_STATUS", GetDb.Code("CZ_PU00005"), "CODE", "NAME");

			grd헤드.SetDefault("20.07.08.04", SumPositionEnum.None);
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			grd헤드.DoubleClick += Grd헤드_DoubleClick;
		}

		private void Grd헤드_DoubleClick(object sender, EventArgs e)
		{
			if (!grd헤드.HasNormalRow || grd헤드.MouseCol <= 0)
				return;

			// 헤더클릭
			if (grd헤드.MouseRow < grd헤드.Rows.Fixed)
				return;

			// ***** 견적화면 열기			
			string pageId = "P_CZ_PU_PO_TOOL_REG";
			string pageName = "재고발주도구";
			
			if (IsExistPage(pageId, false))
				UnLoadPage(pageId, false);

			LoadPageFrom(pageId, pageName, Grant, new object[] { grd헤드["NO_TOOL"] });
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			// 조회시작			
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_PU_PO_TOOLH", LoginInfo.CompanyCode);
			grd헤드.DataBind(dtHead);
		}
		
		#endregion
	}
}
