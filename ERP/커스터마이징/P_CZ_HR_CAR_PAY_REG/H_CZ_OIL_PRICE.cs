using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Windows.Print;

using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.BpControls;

namespace cz
{
	public partial class H_CZ_OIL_PRICE : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string YM
		{
			get
			{
				return Util.GetToday().Substring(0, 6);
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_OIL_PRICE()
		{
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();
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

		}

		private void InitGrid()
		{
			flexC.BeginSetting(1, 1, false);
				
			flexC.SetCol("YM"		, "반영월"		, 80	, false, typeof(string)	, FormatTpType.YEAR_MONTH);
			flexC.SetCol("UM_OIL"	, "유류단가"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexC.SetCol("UM_KM"	, "KM단가"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexC.SetCol("DC_RMK"	, "비고"			, 190);
				
			flexC.SetOneGridBinding(new object[] { }, one);
			flexC.VerifyNotNull = new string[] { "YM", "UM_OIL", "UM_KM" };
			
			flexC.SettingVersion = "15.11.20.02";
			flexC.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			btn조회.Click += new EventHandler(btn조회_Click);
			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);
			btn저장.Click += new EventHandler(btn저장_Click);

			cur유류단가.Leave += new EventHandler(cur유류단가_Leave);
		}
		
		protected override void InitPaint()
		{
			btn조회_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== Event

		private void cur유류단가_Leave(object sender, EventArgs e)
		{
			curKM단가.DecimalValue = Util.Ceiling(cur유류단가.DecimalValue / 6);
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn조회_Click(object sender, EventArgs e)
		{
			string query = "SELECT * FROM CZ_HR_CAR_OIL_CODE WHERE CD_COMPANY = '" + CD_COMPANY + "'";
			DataTable dt = DBMgr.GetDataTable(query);
			flexC.Binding = dt;
			if (!flexC.HasNormalRow) Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			string query = "SELECT * FROM CZ_HR_CAR_OIL_CODE WHERE CD_COMPANY = '" + CD_COMPANY + "' AND YM = '" + YM + "'";
			DataTable dt = DBMgr.GetDataTable(query);
			if (dt.Rows.Count > 0 || flexC.DataTable.Select("YM = '" + YM + "'").Length > 0) { Global.MainFrame.ShowMessage("해당월은 이미 작성하였습니다."); return; }

			flexC.Rows.Add();
			flexC.Row = flexC.Rows.Count - 1;
			flexC["YM"] = YM;
			flexC.AddFinished();
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			if (flexC.Row >= flexC.Rows.Fixed) flexC.Rows.Remove(flexC.Row);			
		}

		private void btn저장_Click(object sender, EventArgs e)
		{
			if (!Global.MainFrame.VerifyGrid(flexC)) return;

			// H 저장
			DataTable dtC = flexC.GetChanges();
			if (dtC != null)
			{
				string xmlC = Util.GetTO_Xml(dtC);
				DBMgr.ExecuteNonQuery("SP_CZ_HR_CAR_OIL_CODE_XML", new object[] { xmlC });
			}

			flexC.AcceptChanges();
			Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			this.DialogResult = DialogResult.OK;
		}

		#endregion
	}
}
