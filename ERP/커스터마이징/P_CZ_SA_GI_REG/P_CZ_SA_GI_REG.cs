using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Windows.Print;

namespace cz
{
	public partial class P_CZ_SA_GI_REG : PageBase
	{
		FreeBinding header = new FreeBinding();
		string NO_GIR_LINK = "";

		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_GIR
		{
			get { return txt협조전번호.Text; }
			set { txt협조전번호.Text = value; }
		}

		public string STA_GIR { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_GI_REG()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();
		}

		public P_CZ_SA_GI_REG(string NO_GIR)
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();

			NO_GIR_LINK = NO_GIR;
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
			Util.SetCON_ReadOnly(pnl작성일자, true);
			Util.SetCON_ReadOnly(pnl작성자, true);
			Util.SetCON_ReadOnly(pnl매출처, true);
			Util.SetCON_ReadOnly(pnl호선번호, true);
			Util.SetCON_ReadOnly(pnl선명, true);
			Util.SetCON_ReadOnly(pnl업무구분, true);
			Util.SetCON_ReadOnly(pnl상세구분, true);
			Util.SetCON_ReadOnly(pnl납품처, true);
			Util.SetCON_ReadOnly(pnl작업시작일, true);
			Util.SetCON_ReadOnly(pnl완료예정일, true);
			Util.SetCON_ReadOnly(pnl상태, true);
			Util.SetCON_ReadOnly(pnl비고, true);
			Util.SetCON_ReadOnly(pnl사유, true);

			MainGrids = new FlexGrid[] { flexL };
		}

		private void InitGrid()
		{			
			// ==================================================================================================== L
			flexL.BeginSetting(1, 1, false);

			flexL.SetCol("NO_GIR"			, "협조전번호"	, false);
			flexL.SetCol("NO_SO"			, "파일번호"		, 90);
			flexL.SetCol("SEQ_SO"			, "수주항번"		, false);
			flexL.SetCol("NO_DSP"			, "순번"			, 45);
			flexL.SetCol("NM_SUBJECT"		, "주제"			, false);
			flexL.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			flexL.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 230);			
			flexL.SetCol("CD_ITEM"			, "재고코드"		, 100);
			//flexL.SetCol("NM_UNIT_MM"		, "단위"			, 45);
			flexL.SetCol("QT_SO"			, "수주수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			flexL.SetCol("QT_GI"			, "출고수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			flexL.SetCol("QT_GIR"			, "의뢰수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			flexL.SetCol("CD_LOCATION"		, "로케이션"		, 70);			
			flexL.SetCol("NO_IO"			, "출고번호"		, false);
			flexL.SetCol("NO_IOLINE"		, "출고항번"		, false);
			flexL.SetCol("EDITED"			, "수정여부"		, false);

			flexL.Cols["NO_DSP"].Format = "####.##";
			flexL.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			//flexL.Cols["NM_UNIT_MM"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["CD_LOCATION"].TextAlign = TextAlignEnum.CenterCenter;

			flexL.KeyActionEnter = KeyActionEnum.MoveDown;
			flexL.SettingVersion = "15.12.26.01";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);			
		}

		private void InitEvent()
		{
			txt협조전번호.KeyDown += new KeyEventHandler(txt협조전번호_KeyDown);
		}
		
		protected override void InitPaint()
		{
			DataTable dtH = DBMgr.GetDataTable("SP_CZ_SA_GIH_REG_SELECT", new object[] { "", "" });
			header.SetBinding(dtH, oneH);
			header.ClearAndNewRow();

			if (NO_GIR_LINK != "")
			{
				NO_GIR = NO_GIR_LINK;
				OnToolBarSearchButtonClicked(null, null);
			}
		}

		private void Clear()
		{
			header.ClearAndNewRow();
			Util.Clear(txt출고번호);
			Util.Clear(dtp출고일자);
			Util.Clear(ctx담당자);
			Util.Clear(txt출고비고);
			Util.Clear(flexL);
		}

		#endregion

		#region ==================================================================================================== Event

		private void txt협조전번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				if (txt협조전번호.Text.Trim() == "") ShowMessage("협조전번호를 입력하세요");
				else OnToolBarSearchButtonClicked(null, null);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dtH = DBMgr.GetDataTable("SP_CZ_SA_GIH_REG_SELECT", new object[] { CD_COMPANY, NO_GIR });
			DataTable dtL = DBMgr.GetDataTable("SP_CZ_SA_GIL_REG_SELECT", new object[] { CD_COMPANY, NO_GIR });
			if (dtH.Rows.Count == 0) { ShowMessage(공통메세지.선택된자료가없습니다); return; }
			
			header.SetDataTable(dtH);
			flexL.Binding = dtL;
			STA_GIR = dtH.Rows[0]["STA_GIR"].ToString();

			// 출고 기본값 설정
			if (STA_GIR == "R")
			{
				txt출고번호.Text = dtH.Rows[0]["NO_GIR"].ToString().Replace("WO", "DN");
				dtp출고일자.Text = Util.GetToday();
				ctx담당자.CodeValue = Global.MainFrame.LoginInfo.UserID;
				ctx담당자.CodeName = Global.MainFrame.LoginInfo.UserName;

				flexL[flexL.Rows.Fixed, "EDITED"] = "Y";
			}
			else
			{
				txt출고번호.Text = dtH.Rows[0]["NO_IO"].ToString();
				dtp출고일자.Text = dtH.Rows[0]["DT_IO"].ToString();
				ctx담당자.CodeValue = dtH.Rows[0]["NO_EMP_IO"].ToString();
				ctx담당자.CodeName = dtH.Rows[0]["NM_EMP_IO"].ToString();
				txt출고비고.Text = dtH.Rows[0]["DC_RMK_IO"].ToString();
			}
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (!MsgAndSave(PageActionMode.Search)) return;
			Clear();
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (!BeforeSave()) return;
			if (!MsgAndSave(PageActionMode.Save)) return;

			ToolBarSaveButtonEnabled = false;
			ShowMessage(PageResultMode.SaveGood);
			OnToolBarSearchButtonClicked(null, null);
		}

		protected override bool BeforeSave()
		{
			if (STA_GIR != "R") { ShowMessage("확정상태만 출고할 수 있습니다."); return false; }

			return true;
		}

		protected override bool SaveData()
		{
			MsgControl.ShowMsg("잠시만 기다려주세요.");

            try
            {
				DBMgr dbm = new DBMgr(DBConn.iU);
				dbm.Procedure = "PX_CZ_SA_GI_REG";
				dbm.AddParameter("CD_COMPANY" , CD_COMPANY);
				dbm.AddParameter("NO_GIR"	  , NO_GIR);
				dbm.AddParameter("DT_IO"	  , dtp출고일자.Text);
				dbm.AddParameter("NO_EMP"	  , ctx담당자.CodeValue);
				dbm.AddParameter("DC_RMK"	  , txt출고비고.Text);
				dbm.AddParameter("ID_USER"	  , Global.MainFrame.LoginInfo.UserID);
				dbm.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
				return false;
			}

			MsgControl.CloseMsg();
			flexL.AcceptChanges();
			return true;
		}

		#endregion

		#region ==================================================================================================== Delete



		#endregion
	}
}

