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
using System.Windows.Forms;
using Duzon.Common.Forms.Help;

namespace cz
{
	public partial class P_CZ_EZLINK_VENDOR_REG : PageBase
	{
		string _companyCode;

		#region ==================================================================================================== Constructor

		public P_CZ_EZLINK_VENDOR_REG()
		{
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			_companyCode = LoginInfo.CompanyCode;

			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			pnl매입처.Editable(false);

			dtp접수일자.StartDateToString = Util.GetToday(-30);
			dtp접수일자.EndDateToString = Util.GetToday();

			dtp접수일자2.StartDateToString = Util.GetToday(-365);
			dtp접수일자2.EndDateToString = Util.GetToday();

			MainGrids = new FlexGrid[] { grd헤드, grd메인키, grd제외키, grd벤더키, grd모델키, grd입찰 };
			grd헤드.DetailGrids = new FlexGrid[] { grd메인키, grd제외키, grd벤더키, grd모델키, grd입찰 };
		}

		private void InitGrid()
		{			
			// ---------------------------------------------------------------------------------------------------- Head
			grd헤드.BeginSetting(1, 1, false);

			grd헤드.SetCol("SEQ_PARTNER"	, "항번"		, false);
			grd헤드.SetCol("NO_PRIORITY"	, "순위"		, 40	, true);
			grd헤드.SetCol("CD_PARTNER"	, "코드"		, 70	, true);			
			grd헤드.SetCol("LN_PARTNER"	, "매입처명"	, 300);
			grd헤드.SetCol("DC_RMK"		, "비고"		, 300);

			grd헤드.Cols["NO_PRIORITY"].Format = "####.##";
			grd헤드.Cols["NO_PRIORITY"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			
			grd헤드.VerifyPrimaryKey = new string[] { "NO_PRIORITY", "CD_PARTNER" };
			grd헤드.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
			grd헤드.SetOneGridBinding(new object[] { }, one매입처, one기타);
			grd헤드.SetBindningRadioButton(new RadioButtonExt[] { rdo사용, rdo미사용 }, new string[] { "Y", "N" });

			grd헤드.SetDefault("19.11.29.01", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 시뮬1
			grd시뮬.BeginSetting(1, 1, false);
			   
			grd시뮬.SetCol("NO_FILE"			, "파일번호"		, 90);
			grd시뮬.SetCol("LN_PARTNER"		, "매출처"		, false);
			grd시뮬.SetCol("NM_VESSEL"		, "선명"			, false);
			grd시뮬.SetCol("NM_EMP"			, "담당자"		, 70);
			grd시뮬.SetCol("NO_LINE"			, "항번"			, false);
			grd시뮬.SetCol("NO_DSP"			, "순번"			, 40);
			grd시뮬.SetCol("NM_SUBJECT"		, "주제"			, 200);
			grd시뮬.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd시뮬.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grd시뮬.SetCol("LN_VENDOR_ALL"	, "매입처(전체)"	, 200);
			grd시뮬.SetCol("LN_VENDOR_SEL"	, "매입처(선택)"	, 200);
			grd시뮬.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd시뮬.SetCol("UM_KR_P"			, "원화단가"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);
			grd시뮬.SetCol("YN_OK"			, "*"			, 40);
			
			grd시뮬.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			grd시뮬.Cols["NO_DSP"].Format = "####.##";
			grd시뮬.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd시뮬.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			   
			grd시뮬.SetDefault("19.11.22.01", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 시뮬2
			grd시뮬2.BeginSetting(1, 1, false);
			   
			grd시뮬2.SetCol("NO_FILE"			, "파일번호"		, 90);
			grd시뮬2.SetCol("LN_PARTNER"		, "매출처"		, false);
			grd시뮬2.SetCol("NM_VESSEL"		, "선명"			, false);
			grd시뮬2.SetCol("NM_EMP"			, "담당자"		, 70);
			grd시뮬2.SetCol("NO_LINE"			, "항번"			, false);
			grd시뮬2.SetCol("NO_DSP"			, "순번"			, 40);
			grd시뮬2.SetCol("NM_SUBJECT"		, "주제"			, 200);
			grd시뮬2.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd시뮬2.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grd시뮬2.SetCol("LN_VENDOR_ALL"	, "매입처(전체)"	, 200);
			grd시뮬2.SetCol("LN_VENDOR_SEL"	, "매입처(선택)"	, 200);
			grd시뮬2.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd시뮬2.SetCol("UM_KR_P"			, "원화단가"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);
			grd시뮬2.SetCol("YN_OK"			, "*"			, 40);
			
			grd시뮬2.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			grd시뮬2.Cols["NO_DSP"].Format = "####.##";
			grd시뮬2.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd시뮬2.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			   
			grd시뮬2.SetDefault("19.11.22.01", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 테스트
			grd테스트.BeginSetting(1, 1, false);
			
			grd테스트.SetCol("NO_FILE"			, "파일번호"		, 90);
			grd테스트.SetCol("LN_PARTNER"		, "매출처"		, false);
			grd테스트.SetCol("NM_VESSEL"			, "선명"			, false);
			grd테스트.SetCol("NM_EMP"			, "담당자"		, false);
			grd테스트.SetCol("NO_LINE"			, "항번"			, false);
			grd테스트.SetCol("NO_DSP"			, "순번"			, 40);
			grd테스트.SetCol("NM_SUBJECT"		, "주제"			, 200);
			grd테스트.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd테스트.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grd테스트.SetCol("LN_VENDOR_ALL"		, "매입처(전체)"	, 200);
			grd테스트.SetCol("LN_VENDOR_SEL"		, "매입처(선택)"	, 200);
			grd테스트.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd테스트.SetCol("UM_KR_P"			, "원화단가"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);
			grd테스트.SetCol("LN_MATCH_STEP1"	, "1단계(확정)"	, 200);
			grd테스트.SetCol("LN_MATCH_STEP1_1"	, "1-1단계"		, false);
			grd테스트.SetCol("LN_MATCH_STEP1_2"	, "1-1단계"		, false);
			grd테스트.SetCol("LN_MATCH_STEP1_3"	, "1-1단계"		, false);
			grd테스트.SetCol("LN_MATCH_STEP2"	, "2단계(보류)"	, 200);
			
			grd테스트.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			grd테스트.Cols["NO_DSP"].Format = "####.##";
			grd테스트.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd테스트.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			
			grd테스트.SetDefault("19.11.28.04", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 메인 키워드
			grd메인키.BeginSetting(1, 1, false);

			grd메인키.SetCol("CD_COMPANY"	, "회사코드"		, false);
			grd메인키.SetCol("CD_PARTNER"	, "매입처"		, false);
			grd메인키.SetCol("SEQ_PARTNER"	, "매입처순번"	, false);
			grd메인키.SetCol("SEQ"			, "순번"			, false);
			grd메인키.SetCol("EZKEY1"		, "포함"			, 180	, true);
			grd메인키.SetCol("EZKEY2"		, "포함"			, 180	, true);
			grd메인키.SetCol("EZKEY3"		, "포함"			, 180	, true);
			grd메인키.SetCol("EZKEY4"		, "포함"			, 180	, true);
			
			grd메인키.SetDefault("19.11.22.01", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 제외 키워드
			grd제외키.BeginSetting(1, 1, false);

			grd제외키.SetCol("CD_COMPANY"	, "회사코드"		, false);
			grd제외키.SetCol("CD_PARTNER"	, "매입처"		, false);
			grd제외키.SetCol("SEQ_PARTNER"	, "매입처순번"	, false);
			grd제외키.SetCol("SEQ"			, "순번"			, false);
			grd제외키.SetCol("EZKEY_EX"		, "제외"			, 180	, true);

			grd제외키.SetDefault("19.11.22.01", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 벤더 키워드
			grd벤더키.BeginSetting(1, 1, false);

			grd벤더키.SetCol("CD_COMPANY"	, "회사코드"		, false);
			grd벤더키.SetCol("CD_PARTNER"	, "매입처"		, false);
			grd벤더키.SetCol("SEQ_PARTNER"	, "매입처순번"	, false);
			grd벤더키.SetCol("SEQ"			, "순번"			, false);
			grd벤더키.SetCol("EZKEY1"		, "벤더"			, 180	, true);

			grd벤더키.SetDefault("19.11.22.01", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 모델 키워드
			grd모델키.BeginSetting(1, 1, false);

			grd모델키.SetCol("CD_COMPANY"	, "회사코드"		, false);
			grd모델키.SetCol("CD_PARTNER"	, "매입처"		, false);
			grd모델키.SetCol("SEQ_PARTNER"	, "매입처순번"	, false);
			grd모델키.SetCol("SEQ"			, "순번"			, false);
			grd모델키.SetCol("EZKEY1"		, "모델"			, 180	, true);
			grd모델키.SetCol("EZKEY2"		, "제품군"		, 180	, true);
			grd모델키.SetCol("EZKEY3"		, "제품군"		, 180	, true);
			grd모델키.SetCol("EZKEY4"		, "제품군"		, 180	, true);

			grd모델키.SetDefault("19.11.22.01", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 관련 매입처
			grd입찰.BeginSetting(1, 1, false);
			
			grd입찰.SetCol("CD_COMPANY"		, "회사코드"		, false);
			grd입찰.SetCol("CD_PARTNER"		, "매입처"		, false);
			grd입찰.SetCol("SEQ_PARTNER"		, "매입처순번"	, false);
			grd입찰.SetCol("CD_PARTNER_BID"	, "코드"			, 70	, true);
			grd입찰.SetCol("LN_PARTNER_BID"	, "매입처명"		, 300);
			grd입찰.SetCol("DC_RMK"			, "비고"			, 180	, true);

			grd입찰.Cols["CD_PARTNER_BID"].TextAlign = TextAlignEnum.CenterCenter;

			grd입찰.VerifyPrimaryKey = new string[] { "CD_PARTNER_BID" };
			grd입찰.SetCodeHelpCol("CD_PARTNER_BID", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER_BID", "LN_PARTNER_BID" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

			grd입찰.SetDefault("20.01.01.01", SumPositionEnum.None);
		}

		
		protected override void InitPaint()
		{
			grd메인키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd제외키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd벤더키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd모델키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;


			spc메인.SplitterDistance = spc메인.Width - 1050;
			spc메인키.SplitterDistance = spc메인키.Width - 250;   // 35 + 170 + 16
			spc서브키.SplitterDistance = 250;			
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn트레이닝.Click += Btn트레이닝_Click;
			btn테스트.Click += Btn테스트_Click;

			btn메인키추가.Click += Btn키워드추가_Click;
			btn메인키삭제.Click += Btn키워드삭제_Click;

			btn제외키추가.Click += Btn키워드추가_Click;
			btn제외키삭제.Click += Btn키워드삭제_Click;

			btn벤더키추가.Click += Btn키워드추가_Click;
			btn벤더키삭제.Click += Btn키워드삭제_Click;

			btn모델키추가.Click += Btn키워드추가_Click;
			btn모델키삭제.Click += Btn키워드삭제_Click;

			btn입찰추가.Click += Btn키워드추가_Click;
			btn입찰삭제.Click += Btn키워드삭제_Click;

			grd헤드.AfterRowChange += grd헤드_AfterRowChange;
			grd헤드.DoubleClick += grd헤드_DoubleClick;

		}

		private void Btn키워드추가_Click(object sender, EventArgs e)
		{
			if (grd헤드["SEQ_PARTNER"] == DBNull.Value)
			{
				ShowMessage("추가중인 매입처는 키워드 추가/삭제를 할 수 없습니다.\n저장 및 조회후 시도 바랍니다");
				return;
			}

			string name = "grd" + ((RoundedButton)sender).Name.Replace("btn", "").Replace("추가", "").Replace("삭제", "");
			FlexGrid flexGrid = (FlexGrid)Controls.Find(name, true)[0];

			flexGrid.Rows.Add();
			flexGrid.Row = flexGrid.Rows.Count - 1;
			flexGrid["CD_COMPANY"] = _companyCode;
			flexGrid["CD_PARTNER"] = grd헤드["CD_PARTNER"];
			flexGrid["SEQ_PARTNER"] = grd헤드["SEQ_PARTNER"];

			if (flexGrid.Cols.Contains("SEQ"))
				flexGrid["SEQ"] = (int)flexGrid.Aggregate(AggregateEnum.Max, "SEQ") + 1;

			flexGrid.AddFinished();
		}

		private void Btn키워드삭제_Click(object sender, EventArgs e)
		{
			string name = "grd" + ((RoundedButton)sender).Name.Replace("btn", "").Replace("추가", "").Replace("삭제", "");
			FlexGrid flexGrid = (FlexGrid)Controls.Find(name, true)[0];

			if (flexGrid.HasNormalRow)
				flexGrid.Rows.Remove(flexGrid.Row);
		}

		//private void Btn매입처추가_Click(object sender, EventArgs e)
		//{
		//	if (grd헤드["SEQ_PARTNER"] == DBNull.Value)
		//	{
		//		ShowMessage("추가중인 매입처는 키워드 추가/삭제를 할 수 없습니다.\n저장 및 조회후 시도 바랍니다");
		//		return;
		//	}


		//}

		//private void Btn매입처삭제_Click(object sender, EventArgs e)
		//{

		//}


		private void grd헤드_DoubleClick(object sender, EventArgs e)
		{
			// 헤더클릭
			//if (grd라인.MouseRow < grd라인.Rows.Fixed)
			//{
			//	SetGridStyle();
			//	return;
			//}

			//string colName = grd라인.Cols[grd라인.Col].Name;

			//if (grd라인.AllowEditing && grd라인.Cols[grd라인.Col].Name == "YN_QLINK")
			//{
			//	DataTable dt = grd라인.DataTable.Select("NO_LINE = " + grd라인["NO_LINE"]).CopyToDataTable();
				

			//	if (f.ShowDialog() == DialogResult.OK)
			//	{
			//		grd라인["CD_ITEM"] = f.List["CD_ITEM"];
			//		grd라인["UCODE"] = f.List["UCODE"];
			//		grd라인["YN_QLINK"] = "C";

			//		grd라인.SetCellStyle(grd라인.Row, grd라인.Cols["YN_QLINK"].Index, "QLINK_Y");
			//	}
			//	//}
		}

		//private void _flexL_StartEdit(object sender, RowColEventArgs e)
		//{
		//	try
		//	{
		//		if (this._flexL.Cols[e.Col].Name == "CD_LOCATION")
		//		{
		//			if (D.GetString(this._flexL["CD_LOCATION"]) != string.Empty && this._flexL.RowState() != DataRowState.Added)
		//				e.Cancel = true;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		this.MsgEnd(ex);
		//	}
		//}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DBMgr dbm = new DBMgr();
			dbm.Procedure = "PS_CZ_MA_EZLINK_VENDOR_REG";
			dbm.AddParameter("@CD_COMPANY", _companyCode);

			DataTable dt = dbm.GetDataTable();
			grd헤드.Binding = dt;
		}

		private void Btn트레이닝_Click(object sender, EventArgs e)
		{
			if (cbm매입처.QueryWhereIn_Pipe == "")
			{
				if (ShowMessage("매입처를 지정하지 않을 경우 많은 시간이 소요됩니다.\n진행하시겠습니까?", "QY2") != DialogResult.Yes)
					return;
			}

			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;
			//MsgControl.ShowMsg(DD("조회중입니다."));
			Util.ShowProgress(DD("조회중입니다."));

			// 키워드에 의해 나온 결과
			DBMgr dbm = new DBMgr
			{
				DebugMode = debugMode
			,	Procedure = "PS_CZ_MA_EZLINK_VENDOR_REG_SIMULATION"
			};
			dbm.AddParameter("@CD_COMPANY"	, _companyCode);
			dbm.AddParameter("@CD_PARTNER"	, grd헤드["CD_PARTNER"]);
			dbm.AddParameter("@SEQ_PARTNER"	, grd헤드["SEQ_PARTNER"]);
			dbm.AddParameter("@DT_F"		, dtp접수일자.StartDateToString);
			dbm.AddParameter("@DT_T"		, dtp접수일자.EndDateToString);
			dbm.AddParameter("@DT_CHK"		, chk접수일자.Checked ? "Y" : "");
			dbm.AddParameter("@CD_VENDOR"	, cbm매입처.QueryWhereIn_Pipe);
			dbm.AddParameter("@CD_TRAINING"	, rdo메인키.Checked ? "MAIN" : "SUB");

			DataTable dt = dbm.GetDataTable();

			// OK 여부
			foreach (DataRow row in dt.Rows)
			{
				if (row["LN_VENDOR_ALL"].ToString().IndexOf((string)grd헤드["LN_PARTNER"]) < 0)
					row["YN_OK"] = "X";
			}

			grd시뮬.Binding = dt;

			// 해당 매입처이지만 키워드로 검색 안되는 리스트 (원인 분석용)
			if (cbm매입처.QueryWhereIn_Pipe != "")
			{
				DBMgr dbmChk = new DBMgr
				{
					DebugMode = debugMode
				,	Procedure = "PS_CZ_MA_EZLINK_VENDOR_REG_SIMULATION_CHK"
				};
				dbmChk.AddParameter("@CD_COMPANY"	, _companyCode);
				dbmChk.AddParameter("@XML"			, GetTo.Xml(dt, "", "CD_COMPANY", "NO_FILE", "NO_LINE"));
				dbmChk.AddParameter("@DT_F"			, dtp접수일자.StartDateToString);
				dbmChk.AddParameter("@DT_T"			, dtp접수일자.EndDateToString);
				dbmChk.AddParameter("@DT_CHK"		, chk접수일자.Checked ? "Y" : "");
				dbmChk.AddParameter("@CD_VENDOR"	, cbm매입처.QueryWhereIn_Pipe);

				DataTable dtChk = dbmChk.GetDataTable();
				grd시뮬2.Binding = dtChk;
			}

			Util.CloseProgress();
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;
			Util.ShowProgress(DD("조회중입니다."));

			//if (tbx파일번호.Text == "")
			//	return;
			DBMgr dbm = new DBMgr
			{
				DebugMode = debugMode
			,	Procedure = "PS_CZ_MA_EZLINK_VENDOR_REG_TEST"
			};
			dbm.AddParameter("@CD_COMPANY"	, _companyCode);
			dbm.AddParameter("@DT_F"		, dtp접수일자2.StartDateToString);
			dbm.AddParameter("@DT_T"		, dtp접수일자2.EndDateToString);
			dbm.AddParameter("@DT_CHK"		, chk접수일자2.Checked ? "Y" : "");
			dbm.AddParameter("@CD_VENDOR"	, cbm매입처2.QueryWhereIn_Pipe);
			dbm.AddParameter("@NO_FILE"		, tbx파일번호2.Text);

			DataTable dt = dbm.GetDataTable();
			grd테스트.Binding = dt;

			Util.CloseProgress();
		}

		private void grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			if (grd헤드["CD_PARTNER"].ToString() == "")
				return;

			//grd라인.Redraw = false;
			string partnerCode = (string)grd헤드["CD_PARTNER"];
			int partnerSeq = (int)grd헤드["SEQ_PARTNER"];
			//string fileNumber = grd헤드["NO_FILE"].ToString();

			string filter = "CD_PARTNER = '" + partnerCode + "' AND SEQ_PARTNER = " + partnerSeq;
			DataTable dt = null;

			if (grd헤드.DetailQueryNeed)
			{
				DataSet ds = DBMgr.GetDataSet("PS_CZ_MA_EZLINK_VENDOR_REG_EZKEYS", _companyCode, partnerCode, partnerSeq);
				grd메인키.BindingAdd(ds.Tables[0], filter);
				grd제외키.BindingAdd(ds.Tables[1], filter);
				grd벤더키.BindingAdd(ds.Tables[2], filter);
				grd모델키.BindingAdd(ds.Tables[3], filter);
				grd입찰.BindingAdd(ds.Tables[4], filter);
			}
			else
			{				
				grd메인키.BindingAdd(null, filter);
				grd제외키.BindingAdd(null, filter);
				grd벤더키.BindingAdd(null, filter);
				grd모델키.BindingAdd(null, filter);
				grd입찰.BindingAdd(null, filter);
			}

			grd메인키.BindingAdd(dt, "");			
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{			
			// 추가
			grd헤드.Rows.Add();
			grd헤드.Row = grd헤드.Rows.Count - 1;		
			grd헤드.AddFinished();
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;


			if (!base.Verify())
				return;

			//// 저장
			DBMgr dbm = new DBMgr
			{
				DebugMode = debugMode
			,	Procedure = "PX_CZ_MA_EZLINK_VENDOR_REG"
			};
			dbm.AddParameter("@XML"				, GetTo.Xml(grd헤드.GetChanges()));
			dbm.AddParameter("@XML_MAINKEY"		, GetTo.Xml(grd메인키.GetChanges()));
			dbm.AddParameter("@XML_MAINKEY_EX"	, GetTo.Xml(grd제외키.GetChanges()));
			dbm.AddParameter("@XML_SUBKEY1"		, GetTo.Xml(grd벤더키.GetChanges()));
			dbm.AddParameter("@XML_SUBKEY2"		, GetTo.Xml(grd모델키.GetChanges()));
			dbm.AddParameter("@XML_BID"			, GetTo.Xml(grd입찰.GetChanges()));
			dbm.ExecuteNonQuery();

			
			//grdList.AcceptChanges();
			grd헤드.AcceptChanges();
			grd메인키.AcceptChanges();
			grd제외키.AcceptChanges();
			grd벤더키.AcceptChanges();
			grd모델키.AcceptChanges();
			grd입찰.AcceptChanges();
			ShowMessage(PageResultMode.SaveGood);
		}

		#endregion

		#region ==================================================================================================== 삭제

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			//if (!grdList.HasNormalRow) return;
			//grdList.Rows.Remove(grdList.Row);
		}

		#endregion
	}
}
