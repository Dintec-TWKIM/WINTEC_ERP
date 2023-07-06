using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using DintecExt;
using Duzon.Common.Util;
using System.Net;
using System.IO;
using System.Reflection;
using Duzon.ERPU.Utils;


namespace cz
{
	public partial class P_CZ_SA_QTN_PREREG : PageBase
	{
		#region ===================================================================================================== Property

		public string FileNumber
		{
			get; set;
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_QTN_PREREG()
		{
			StartUp.Certify(this);
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
			//// 콤보박스
			//DataSet ds = GetDb.Code("CD_FILE", "CZ_MA00005");
			//cbo파일번호.DataBind(ds.Tables[0], true);
			//cbo파일유형.DataBind(ds.Tables[1], true);

			//DataTable dt = new DataTable();
			//dt.Columns.Add("CODE");
			//dt.Columns.Add("NAME");

			//dt.Rows.Add("NO_REF"		, "문의번호");
			//dt.Rows.Add("NO_PO_PARTNER"	, "주문번호");
			//dt.Rows.Add("NO_IV"			, "계산서번호");

			//cbo키워드.DataBind(dt, false);
			//cbo키워드.SelectedIndex = 0;

			//// 나머지
			//tbx포커스.Left = -1000;

			//dtp등록일자.StartDateToString = Util.GetToday(-30);
			//dtp등록일자.EndDateToString = Util.GetToday();

			ctx회사코드.CodeValue = LoginInfo.CompanyCode;
			ctx회사코드.CodeName = LoginInfo.CompanyName;

			fgd헤드.DetailGrids = new FlexGrid[] { fgd라인 };
			//MainGrids = new FlexGrid[] { grd라인 };

			//splitContainer1.SplitterDistance = 930;

			//if (LoginInfo.UserID != "S-343")
			//{
			//	Grant.CanDelete = false;
			//	Grant.CanPrint = false;
			//}
		}

		private void InitGrid()
		{
			// ---------------------------------------------------------------------------------------------------- Head
			fgd헤드.BeginSetting(1, 1, false);

			fgd헤드.SetCol("CD_COMPANY"		, "회사명"		, false);
			fgd헤드.SetCol("NO_PREREG"		, "사전등록번호"	, 80);
			fgd헤드.SetCol("NO_FILE"			, "파일번호"		, 80);
			fgd헤드.SetCol("NO_TEST"			, "처리번호"		, 80);
			fgd헤드.SetCol("DTS_INSERT"		, "등록일"		, 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			fgd헤드.SetCol("LN_PARTNER"		, "매출처"		, 150);
			fgd헤드.SetCol("NO_IMO"			, "IMO번호"		, 70);
			fgd헤드.SetCol("NM_VESSEL"		, "선명"			, 250);
			fgd헤드.SetCol("NO_REF"			, "문의번호"		, 200);			
			fgd헤드.SetCol("NM_SUBJECT"		, "주제"			, 400);
			fgd헤드.SetCol("MAIL_FROM"		, "발신자"		, 150);

			fgd헤드.Cols["CD_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
			fgd헤드.Cols["NO_PREREG"].TextAlign = TextAlignEnum.CenterCenter;
			fgd헤드.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			fgd헤드.Cols["NO_IMO"].TextAlign = TextAlignEnum.CenterCenter;
			fgd헤드.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

			fgd헤드.SetDataMap("CD_COMPANY", GetDb.Company(), "CD_COMPANY", "NM_COMPANY");
			
			fgd헤드.SetDefault("20.01.30.04", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- Head
			fgd라인.BeginSetting(1, 1, false);
			
			fgd라인.SetCol("CD_COMPANY"		, "회사명"			, false);
			fgd라인.SetCol("NO_PREREG"		, "사전등록번호"		, false);
			fgd라인.SetCol("NO_LINE"			, "순번"				, 40);		// 항번 = 순번			
			fgd라인.SetCol("NM_SUBJECT"		, "주제"				, 400	, false);
			fgd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"			, 100	, false);
			fgd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"			, 300	, false);
			fgd라인.SetCol("CD_UNIQ_PARTNER"	, "선사코드"			, 100	, false);						
			fgd라인.SetCol("UNIT"			, "단위"				, 50);
			fgd라인.SetCol("QT"				, "수량"				, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);
			fgd라인.SetCol("CD_VENDOR"		, "매입처코드"		, false);
			fgd라인.SetCol("SN_VENDOR"		, "매입처(확정)"		, 200);
			fgd라인.SetCol("CD_VENDOR_EZ1"	, "매입처코드"		, false);
			fgd라인.SetCol("SN_VENDOR_EZ1"	, "매입처(전처리1)"	, 200);
			fgd라인.SetCol("CD_VENDOR_EZ2"	, "매입처코드"		, false);
			fgd라인.SetCol("SN_VENDOR_EZ2"	, "매입처(전처리2)"	, 200);
			fgd라인.SetCol("CD_VENDOR_AI"	, "매입처코드"		, false);
			fgd라인.SetCol("SN_VENDOR_AI"	, "매입처(학습)"		, 200);

			fgd라인.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;

			fgd라인.SetDefault("20.01.30.02", SumPositionEnum.None);
		}

		protected override void InitPaint()
		{
			tbx사전등록번호.Focus();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			//btn영업정보등록.Click += Btn영업정보등록_Click;
			//btn첨부파일등록.Click += Btn첨부파일등록_Click;
			//btn발송용저장.Click += Btn발송용저장_Click;

			//tbx파일번호.KeyDown += new KeyEventHandler(tbx검색_KeyDown);
			//tbx사전등록번호.KeyDown += new KeyEventHandler(tbx검색_KeyDown);
			//cbm확장자.QueryBefore += Cbm확장자_QueryBefore;

			//fgd헤드.AfterRowChange += new RangeEventHandler(fgd헤드_AfterRowChange);
			fgd헤드.AfterRowChange += Fgd헤드_AfterRowChange;
			//fgd헤드.DoubleClick += new EventHandler(fgd헤드_DoubleClick);
			//grd라인.DoubleClick += new EventHandler(grd라인_DoubleClick);

			btn견적등록.Click += Btn견적등록_Click;
		}

		

		private void Btn견적등록_Click(object sender, EventArgs e)
		{
			OpenFileDialog f = new OpenFileDialog
			{
				Filter = "Inquiry|*.msg"
			};

			if (f.ShowDialog() != DialogResult.OK)
				return;

			Preregister p = new Preregister();
			bool boResult = p.Inquiry((string)fgd헤드["CD_COMPANY"], (string)fgd헤드["NO_PREREG"], f.FileName);
			
			//PreregisterInquiry((string)fgd헤드["CD_COMPANY"], (string)fgd헤드["NO_PREREG"], f.FileName);
			if (boResult)
				ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			//if (ctx회사코드.CodeValue == "")
			//{
			//	ShowMessage(공통메세지._은는필수입력항목입니다, DD("회사코드"));
			//	return;
			//}

			DebugMode debug = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;
			Util.ShowProgress(DD("조회중입니다."));
			tbx사전등록번호.Text = EngHanConverter.KorToEng(tbx사전등록번호.Text);
			//tbx포커스.Focus();

			//// 키워드 파라메타
			//string keyPar = "@" + cbo키워드.GetValue();
			
			DBMgr dbm = new DBMgr();
			dbm.DebugMode = debug;
			dbm.Procedure = "PS_CZ_SA_QTN_PREREG_H";
			dbm.AddParameter("@CD_COMPANY"	, ctx회사코드.CodeValue);
			dbm.AddParameter("@NO_PREREG"	, tbx사전등록번호.Text);
			//dbm.AddParameter("@CD_FILE"		, cbo파일번호.GetValue());
			//dbm.AddParameter("@DT_F"		, dtp등록일자.StartDateToString);
			//dbm.AddParameter("@DT_T"		, dtp등록일자.EndDateToString);

			//dbm.AddParameter("@NO_EMP_SALE"	, ctx영업담당자.CodeValue);
			//dbm.AddParameter("@NO_EMP_TYPE"	, ctx입력담당자.CodeValue);
			//dbm.AddParameter("@CD_PARTNER"	, cbm매출처.QueryWhereIn_Pipe);
			//dbm.AddParameter("@NO_IMO"		, cbm호선.QueryWhereIn_Pipe);			
			//dbm.AddParameter("@CD_VENDOR"	, cbm매입처.QueryWhereIn_Pipe);

			//dbm.AddParameter("@TP_STEP"		, cbo파일유형.GetValue());
			//dbm.AddParameter(keyPar			, tbx키워드.Text);
			//dbm.AddParameter("@CD_EXTENSION", cbm확장자.QueryWhereIn_Pipe);
			//dbm.AddParameter("@YN_INCLUDED"	, chk첨부.Checked ? "Y" : "");
			//dbm.AddParameter("@YN_LIMIT"	, chk제한.Checked ? "Y" : "");

			DataTable dt = dbm.GetDataTable();
			fgd헤드.Binding = dt;

			Util.CloseProgress();
		}

		private void Fgd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			string companyCode = fgd헤드["CD_COMPANY"].ToString();
			string preregNumber = fgd헤드["NO_PREREG"].ToString();
			DataTable dt = null;

			if (fgd헤드.DetailQueryNeed)
			{
				DBMgr dbm = new DBMgr
				{
					DebugMode = DebugMode.Print
				,	Procedure = "PS_CZ_SA_QTN_PREREG_L"
				};
				dbm.AddParameter("@CD_COMPANY"	, companyCode);
				dbm.AddParameter("@NO_PREREG"	, preregNumber);
				
				//dbm.AddParameter("@CD_VENDOR"	, chk매입처.Checked ? cbm매입처.QueryWhereIn_Pipe : "");
				//dbm.AddParameter("@TP_STEP"		, cbo파일유형.GetValue());
				//dbm.AddParameter("@CD_EXTENSION", cbm확장자.QueryWhereIn_Pipe);
				//dbm.AddParameter("@YN_INCLUDED"	, chk첨부.Checked ? "Y" : "");
				//dbm.AddParameter("@YN_RECENT"	, chk최신.Checked ? "Y" : "");
				dt = dbm.GetDataTable();
			}
	
			fgd라인.BindingAdd(dt, "CD_COMPANY = '" + companyCode + "' AND NO_PREREG = '" + preregNumber + "'");
		}

		#endregion
	}
}
