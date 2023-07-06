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
namespace cz
{
	public partial class P_CZ_SA_INQ_QLINK : Duzon.Common.Forms.CommonDialog
	{
		string CompanyCode;
		DataTable DtItem;

		#region ===================================================================================================== Property

		public FlexGrid List
		{
			get
			{
				return grdList;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_INQ_QLINK(DataTable dataTable)
		{
			InitializeComponent();
			CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;
			DtItem = dataTable;
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
			pnl주제.Enabled(false);
			pnl품목코드.Enabled(false);
			pnl품목명.Enabled(false);
		}

		private void InitGrid()
		{			
			grdList.BeginSetting(1, 1, false);

			grdList.SetCol("CD_ITEM"	, "재고코드"	, 75);
			grdList.SetCol("NM_ITEM"	, "재고명"	, 300);
			grdList.SetCol("DC_MODEL"	, "적용모델"	, 80);
			grdList.SetCol("UCODE"		, "U코드1"	, 80);
			grdList.SetCol("UCODE2"		, "U코드2"	, 150);
			grdList.SetCol("DC_RMK1"	, "추가정보1"	, 180);
			grdList.SetCol("DC_RMK2"	, "추가정보2"	, 180);
			grdList.SetCol("DC_RMK"		, "비고"		, 180);

			grdList.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
			
			grdList.SetDefault("18.12.06.03", SumPositionEnum.None);
		}

		private void InitEvent()
		{
			btn확정.Click += new EventHandler(btn확정_Click);
			btn취소.Click += new EventHandler(btn취소_Click);

			grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
		}

		protected override void InitPaint()
		{
			tbx주제.Text = DtItem.Rows[0]["NM_SUBJECT"].ToString();
			tbx품목코드.Text = DtItem.Rows[0]["CD_ITEM_PARTNER"].ToString();
			tbx품목명.Text = DtItem.Rows[0]["NM_ITEM_PARTNER"].ToString();

			if (CompanyCode == "K200" && DtItem.Select("NO_ENGINE IS NOT NULL").Length > 0)
			{
				DtItem.Columns["NM_SUBJECT"].ColumnName = "SUBJ";
				DtItem.Columns["CD_ITEM_PARTNER"].ColumnName = "CODE";
				DtItem.Columns["NM_ITEM_PARTNER"].ColumnName = "ITEM";

				DataRow row = DtItem.Rows[0];
				DataTable dtList = DBMgr.GetDataTable("PS_CZ_SA_INQ_QLINK_HSD", true, false, CompanyCode, row["SUBJ"], row["CODE"], row["ITEM"]);
				grdList.DataBind(dtList);
			}
			else
			{
				// U코드 관련된 핵심 단어 추출
				// CORE 필드 추가
				DtItem.Columns.Add("CORE_WORD_SUBJ");
				DtItem.Columns.Add("CORE_WORD_ITEM");

				foreach (DataRow row in DtItem.Rows)
				{
					row["CORE_WORD_SUBJ"] = Util.GetDxDescriptionByCoreCode((string)row["NM_SUBJECT"]);
					row["CORE_WORD_ITEM"] = Util.GetDxDescriptionByCoreCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
				}

				// 저장
				DataTable dtList = DBMgr.GetDataTable("PS_CZ_SA_INQ_QLINK", true, false, CompanyCode, DtItem.Rows[0]["CORE_WORD_SUBJ"], DtItem.Rows[0]["CORE_WORD_ITEM"]);
				grdList.DataBind(dtList);
			}
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grdList_DoubleClick(object sender, EventArgs e)
		{
			int row = grdList.MouseRow;
			int col = grdList.MouseCol;

			// 버리는 컬럼 이벤트
			if (col <= 0)
				return;
			
			// 아이템 클릭
			if (row >= grdList.Rows.Fixed)
				btn확정_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== Save

		private void btn확정_Click(object sender, EventArgs e)
		{
			if (grdList.Row < 0)
				Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
			else
				this.DialogResult = DialogResult.OK;			
		}

		#endregion

		#region ==================================================================================================== 취소

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion
	}
}
