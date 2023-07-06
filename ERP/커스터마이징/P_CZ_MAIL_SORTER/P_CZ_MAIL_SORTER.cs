using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using DzHelpFormLib;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace cz
{
	public partial class P_CZ_MAIL_SORTER : PageBase
	{
		P_CZ_MAIL_REG mailreg = new P_CZ_MAIL_REG();
		FreeBinding Header = new FreeBinding();

		#region ==================== 서버 파일 저장
		// 구조체 선언
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct NETRESOURCE
		{
			public uint dwScope;
			public uint dwType;
			public uint dwDisplayType;
			public uint dwUsage;
			public string lpLocalName;
			public string lpRemoteName;
			public string lpComment;
			public string lpProvider;
		}

		// API 함수 선언
		[DllImport("mpr.dll", CharSet = CharSet.Auto)]
		public static extern int WNetUseConnection(
					IntPtr hwndOwner,
					[MarshalAs(UnmanagedType.Struct)] ref NETRESOURCE lpNetResource,
					string lpPassword,
					string lpUserID,
					uint dwFlags,
					StringBuilder lpAccessName,
					ref int lpBufferSize,
					out uint lpResult);

		// API 함수 선언 (공유해제)
		[DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2", CharSet = CharSet.Auto)]
		public static extern int WNetCancelConnection2A(string lpName, int dwFlags, int fForce);
		#endregion ==================== 서버 파일 저장

		#region ==================== 변수


		public int itemCount = 0;

		public string NO_MAIL = string.Empty;

		public string CD_COMPANY = string.Empty;
		public string CATEGORY = string.Empty;
		public string CODE = string.Empty;
		public string MAIL_FROM = string.Empty;
		public string MAIL_TO = string.Empty;
		public string MAIL_CC = string.Empty;
		public string MAIL_BCC = string.Empty;
		public string FOLDER_R = string.Empty;
		public string FOLDER_M = string.Empty;
		public string DOMAIN = string.Empty;
		public string NM_KOR = string.Empty;
		public string NO_EMP = string.Empty;
		public string NM_PARTER = string.Empty;
		public string CD_PARTNER = string.Empty;
		public string SUBJECT_KEY = string.Empty;
		public string SUBJECT_DEL = string.Empty;
		public string BODY_KEY = string.Empty;
		public string BODY_DEL = string.Empty;
		public string USE_YN = string.Empty;
		public string RECEIVE_MAIL = string.Empty;
		public string ETC2 = string.Empty;
		public string DC_RMK = string.Empty;
		public string ID_INSERT = string.Empty;
		public string DTS_INSERT = string.Empty;
		public string ID_UPDATE = string.Empty;
		public string DTS_UPDATE = string.Empty;


		public string NO_EMAIL = string.Empty;

		public string DELIVERYDT = string.Empty;

		public string MAIL_SUBJECT = string.Empty;
		public string MAIL_BODY = string.Empty;


		public string MAIL_CD_COMPANY = string.Empty;
		public string MAIL_CATEGORY = string.Empty;
		public string MAIL_CODE = string.Empty;
		public string MAIL_MAIL_FROM = string.Empty;
		public string MAIL_MAIL_CC = string.Empty;
		public string MAIL_MAIL_BCC = string.Empty;
		public string MAIL_FOLDER_R = string.Empty;
		public string MAIL_FOLDER_M = string.Empty;
		public string MAIL_DOMAIN = string.Empty;
		public string MAIL_NM_KOR = string.Empty;
		public string MAIL_NO_EMP = string.Empty;
		public string MAIL_NM_PARTER = string.Empty;
		public string MAIL_CD_PARTNER = string.Empty;
		public string MAIL_SUBJECT_KEY = string.Empty;
		public string MAIL_SUBJECT_DEL = string.Empty;
		public string MAIL_BODY_KEY = string.Empty;
		public string MAIL_BODY_DEL = string.Empty;
		public string MAIL_USE_YN = string.Empty;
		public string MAIL_RECEIVE_MAIL = string.Empty;
		public string MAIL_ETC2 = string.Empty;
		public string MAIL_DC_RMK = string.Empty;
		public string MAIL_ID_INSERT = string.Empty;
		public string MAIL_DTS_INSERT = string.Empty;
		public string MAIL_ID_UPDATE = string.Empty;
		public string MAIL_DTS_UPDATE = string.Empty;

		public string PartnerGroupCode
		{
			get
			{
				return cbo거래처그룹.GetValue();
			}
			set
			{
				cbo거래처그룹.SetValue(value);
				Header.CurrentRow["NM_REGION"] = cbo거래처그룹.GetValue();
			}
		}

		#endregion ==================== 변수

		#region ==================== 설정
		public P_CZ_MAIL_SORTER()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			splitContainer1.SplitterDistance = splitContainer1.Width - 800;

		}

		protected override void InitLoad()
		{
			base.InitLoad();


			


			this.InitGrid();
			this.InitEvent();

			


			cbx구분.Items.Add("");
			cbx구분.Items.Add("INQ");
			cbx구분.Items.Add("BACKUP");
			cbx구분.Items.Add("SEND_ETC");
			cbx구분.Items.Add("SEND_VSHIP");
			cbx구분.Items.Add("SEND_VSHIPS_INVOICE");
			cbx구분.Items.Add("SEND_WOOYANG");
			cbx구분.Items.Add("UPLOAD_OMCEAST");
			cbx구분.Items.Add("UPLOAD_QT");
			cbx구분.Items.Add("UPLOAD_ETC");
			cbx구분.Items.Add("UPLOAD_WOOYANG");



			cbx접수형식.Items.Add("");
			cbx접수형식.Items.Add("EWS");
			cbx접수형식.Items.Add("MAIL");
			cbx접수형식.Items.Add("SEAPROC");
			cbx접수형식.Items.Add("ShipServ");

			cbx사용여부.Items.Add("");
			cbx사용여부.Items.Add("Y");
			cbx사용여부.Items.Add("N");

			cbo사용여부.Items.Add("");
			cbo사용여부.Items.Add("Y");
			cbo사용여부.Items.Add("N");

			cbx자동입력.Items.Add("");
			cbx자동입력.Items.Add("Y");
			cbx자동입력.Items.Add("N");


			cbo자동입력.Items.Add("");
			cbo자동입력.Items.Add("Y");
			cbo자동입력.Items.Add("N");


			cbx수신팀메일.DataSource = GetDb.MailCodeGet();
			cbx수신팀메일.ValueMember = "CODE";
			cbx수신팀메일.DisplayMember = "NAME";

			cbo거래처그룹.DataBind(GetDb.Code("MA_B000065"), true);

			//cbo수신팀메일.DataSource = GetDb.MailCodeGet();
			//cbo수신팀메일.ValueMember = "CODE";
			//cbo수신팀메일.DisplayMember = "NAME";


			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			

			ctx회사.CodeValue = CD_COMPANY;
			ctx회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;
		}


		private void InitGrid()
		{
			DataTable dtYN = new DataTable();
			dtYN.Columns.Add("CODE");
			dtYN.Columns.Add("NAME");
			dtYN.Rows.Add("Y", DD("사용"));
			dtYN.Rows.Add("N", DD("미사용"));


			DataTable dtContain = new DataTable();
			dtContain.Columns.Add("CODE");
			dtContain.Columns.Add("NAME");
			dtContain.Rows.Add("Y", DD("포함검색"));
			dtContain.Rows.Add("N", DD("앞글자검색"));

			#region 전체
			this.MainGrids = new FlexGrid[] { this.flexGrid };

			this.flexGrid.BeginSetting(2, 1, true);

			this.flexGrid.SetCol("NO_MAIL", "코드", 80);
			this.flexGrid.SetCol("CD_COMPANY", "회사", 80);
			this.flexGrid.SetCol("CODE", "구분", 80);
			this.flexGrid.SetCol("CD_PARTNER", "코드", false);
			this.flexGrid.SetCol("NM_PARTNER", "이름", 150);
			this.flexGrid.SetCol("NM_REGION", "그룹", 150);


			this.flexGrid.SetCol("MAIL_FROM1", "발신자", 200);
			
			this.flexGrid.SetCol("CD_TEAM", "코드", false);
			this.flexGrid.SetCol("NM_TEAM", "메일", 200);

			this.flexGrid.SetCol("NM_KOR", "담당자", 100);
			this.flexGrid.SetCol("RECEIVE_MAIL", "메일", 100);
			this.flexGrid.SetCol("NO_EMP", "담당자", false);
			this.flexGrid.SetCol("YN_USE", "사용여부", 70);
			this.flexGrid.SetCol("YN_INQINSERT", "사용여부", 100);
			this.flexGrid.SetCol("NM_SALES", "영업담당", 100);
			this.flexGrid.SetCol("NM_ENGINE", "엔진담당", 100);
			this.flexGrid.SetCol("ID_SALES", "영업담당자", false);
			this.flexGrid.SetCol("ID_ENGINE", "엔진담당자", false);
			this.flexGrid.SetCol("YN_MAIL", "메일", false);
			


			this.flexGrid.SetCol("SUBJECT_CONTAIN1", "포함1", 300);
			this.flexGrid.SetCol("SUBJECT_CONTAIN2", "포함2", 300);
			this.flexGrid.SetCol("SUBJECT_CONTAIN3", "포함3", 300);
			this.flexGrid.SetCol("SUBJECT_CONTAIN4", "포함4", 300);

			this.flexGrid.SetCol("SUBJECT_DELETE1", "제외1", 300);
			this.flexGrid.SetCol("SUBJECT_DELETE2", "제외2", 300);
			this.flexGrid.SetCol("SUBJECT_DELETE3", "제외3", 300);
			this.flexGrid.SetCol("SUBJECT_DELETE4", "제외4", 300);

			this.flexGrid.SetCol("BODY_CONTAIN1", "포함1", 300);
			this.flexGrid.SetCol("BODY_CONTAIN2", "포함2", 300);
			this.flexGrid.SetCol("BODY_CONTAIN3", "포함3", 300);
			this.flexGrid.SetCol("BODY_CONTAIN4", "포함4", 300);

			this.flexGrid.SetCol("BODY_DELETE1", "제외1", 300);
			this.flexGrid.SetCol("BODY_DELETE2", "제외2", 300);
			this.flexGrid.SetCol("BODY_DELETE3", "제외3", 300);
			this.flexGrid.SetCol("BODY_DELETE4", "제외4", 300);


			
			this.flexGrid.SetCol("DC_RMK", "비고", 50);

			this.flexGrid.SetCol("ID_INSERT", "등록자", false);
			this.flexGrid.SetCol("DTS_INSERT", "등록일자", false);
			this.flexGrid.SetCol("ID_UPDATE", "수정자", false);
			this.flexGrid.SetCol("DTS_UPDATE", "수정일자", false);

			flexGrid.Cols["CODE"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["CD_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NO_MAIL"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NM_REGION"].TextAlign = TextAlignEnum.CenterCenter;


			//flexGrid.Cols["CD_TEAM"].TextAlign = TextAlignEnum.CenterCenter;
			//flexGrid.Cols["NM_TEAM"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NM_SALES"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NM_ENGINE"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["YN_USE"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["YN_INQINSERT"].TextAlign = TextAlignEnum.CenterCenter;

			flexGrid[0, flexGrid.Cols["MAIL_FROM1"].Index] = "발신자";

			flexGrid[0, flexGrid.Cols["SUBJECT_CONTAIN1"].Index] = "제목";
			flexGrid[0, flexGrid.Cols["SUBJECT_CONTAIN2"].Index] = "제목";
			flexGrid[0, flexGrid.Cols["SUBJECT_CONTAIN3"].Index] = "제목";
			flexGrid[0, flexGrid.Cols["SUBJECT_CONTAIN4"].Index] = "제목";
			flexGrid[0, flexGrid.Cols["SUBJECT_DELETE1"].Index] = "제목";
			flexGrid[0, flexGrid.Cols["SUBJECT_DELETE2"].Index] = "제목";
			flexGrid[0, flexGrid.Cols["SUBJECT_DELETE3"].Index] = "제목";
			flexGrid[0, flexGrid.Cols["SUBJECT_DELETE4"].Index] = "제목";

			flexGrid[0, flexGrid.Cols["BODY_CONTAIN1"].Index] = "본문";
			flexGrid[0, flexGrid.Cols["BODY_CONTAIN2"].Index] = "본문";
			flexGrid[0, flexGrid.Cols["BODY_CONTAIN3"].Index] = "본문";
			flexGrid[0, flexGrid.Cols["BODY_CONTAIN4"].Index] = "본문";
			flexGrid[0, flexGrid.Cols["BODY_DELETE1"].Index] = "본문";
			flexGrid[0, flexGrid.Cols["BODY_DELETE2"].Index] = "본문";
			flexGrid[0, flexGrid.Cols["BODY_DELETE3"].Index] = "본문";
			flexGrid[0, flexGrid.Cols["BODY_DELETE4"].Index] = "본문";

			flexGrid[0, flexGrid.Cols["CD_TEAM"].Index] = "수신팀";
			flexGrid[0, flexGrid.Cols["NM_TEAM"].Index] = "수신팀";


			flexGrid[0, flexGrid.Cols["NM_KOR"].Index] = "수신메일";
			flexGrid[0, flexGrid.Cols["RECEIVE_MAIL"].Index] = "수신메일";

			flexGrid[0, flexGrid.Cols["CD_PARTNER"].Index] = "거래처";
			flexGrid[0, flexGrid.Cols["NM_PARTNER"].Index] = "거래처";
			flexGrid[0, flexGrid.Cols["NM_REGION"].Index] = "거래처";

			flexGrid[0, flexGrid.Cols["YN_INQINSERT"].Index] = "INQ자동등록";
			flexGrid[0, flexGrid.Cols["NM_SALES"].Index] = "INQ자동등록";
			flexGrid[0, flexGrid.Cols["NM_ENGINE"].Index] = "INQ자동등록";
			flexGrid[0, flexGrid.Cols["ID_SALES"].Index] = "INQ자동등록";
			flexGrid[0, flexGrid.Cols["ID_ENGINE"].Index] = "INQ자동등록";


			//this.flexGrid.SetDataMap("CD_TEAM", GetDb.MailCodeGet(), "CODE", "NAME");
			this.flexGrid.SetDataMap("YN_USE", dtYN, "CODE", "NAME");
			this.flexGrid.SetDataMap("YN_INQINSERT", dtYN, "CODE", "NAME");
			this.flexGrid.SetDataMap("NM_REGION", GetDb.Code("MA_B000065"), "CODE", "NAME");


			this.flexGrid.SetCodeHelpCol("NM_KOR", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR", "NO_EMP" }, new string[] { "NM_KOR", "NO_EMP" });
			this.flexGrid.SetCodeHelpCol("NM_SALES", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_SALES", "ID_SALES" }, new string[] { "NM_KOR", "NO_EMP" });
			this.flexGrid.SetCodeHelpCol("NM_ENGINE", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_ENGINE","ID_ENGINE" }, new string[] { "NM_KOR", "NO_EMP" });
			this.flexGrid.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER" }, new string[] { "CD_PARTNER" });

			this.flexGrid.ExtendLastCol = true;


			flexGrid.SetOneGridBinding(new object[] { }, oneGridMID, oneGridSent);


			this.flexGrid.SettingVersion = "20.22.12.27";
			this.flexGrid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion 전체


			//this.flexGrid.SetDataMap("YN_CONTAIN", dtContain, "CODE", "NAME");
		}

		private void InitEvent()
		{
			cbm분류팀메일.QueryBefore += Cbm분류팀메일_QueryBefore;
			cbm분류팀메일.QueryAfter += Cbm분류팀메일_QueryAfter;
			btnSimul.Click += BtnSimul_Click;
			flexGrid.AfterRowChange += FlexGrid_AfterRowChange;
		}


		private void FlexGrid_AfterRowChange(object sender, RangeEventArgs e)
		{
			cbm분류팀메일
				.Clear();

			cbm분류팀메일.AddItem2(flexGrid["CD_TEAM"].ToString(), flexGrid["NM_TEAM"].ToString());
		}

		private void Cbm분류팀메일_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.P00_CHILD_MODE = "분류팀메일";

			e.HelpParam.P61_CODE1 = @"
	CD_SYSDEF	AS CODE
,	NM_SYSDEF	AS NAME";

			e.HelpParam.P62_CODE2 = @"
CZ_MA_CODEDTL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + "K100" + @"'
	AND CD_FIELD = 'CZ_MA00002'
	AND YN_USE = 'Y'
ORDER BY CD_SYSDEF";

			string a = e.HelpParam.ToString();
		}

		private void Cbm분류팀메일_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			flexGrid["CD_TEAM"] = cbm분류팀메일.QueryWhereIn_Pipe;
			flexGrid["NM_TEAM"] = cbm분류팀메일.QueryWhereIn_PipeDisplayMember;
		}


		//private void Cbo수신팀메일_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if (cbo수신팀메일.Text.Equals("딘텍:service"))
		//		lblreceiveMail.Text = "service@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("딘텍:dongjin"))
		//		lblreceiveMail.Text = "dongjin@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업1팀"))
		//		lblreceiveMail.Text = "dintec.sales1@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업2팀"))
		//		lblreceiveMail.Text = "dintec.sales2@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업3팀"))
		//		lblreceiveMail.Text = "dintec.sales3@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업5팀"))
		//		lblreceiveMail.Text = "offshore@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업6팀"))
		//		lblreceiveMail.Text = "sb@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업7팀"))
		//		lblreceiveMail.Text = "db@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업물류1팀"))
		//		lblreceiveMail.Text = "dintec.log@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("영업물류2팀"))
		//		lblreceiveMail.Text = "log2@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("두베코:service"))
		//		lblreceiveMail.Text = "service@dubheco.com";
		//	else if (cbo수신팀메일.Text.Equals("전체 팀 메일"))
		//		lblreceiveMail.Text = "-";
		//	else if (cbo수신팀메일.Text.Equals("매입업로드:DINTEC"))
		//		lblreceiveMail.Text = "upload@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("매입업로드:DUBHECO"))
		//		lblreceiveMail.Text = "upload@dubheco.com";
		//	else if (cbo수신팀메일.Text.Equals("관련도면업로드:DINTEC"))
		//		lblreceiveMail.Text = "drawing@dintec.co.kr";
		//	else if (cbo수신팀메일.Text.Equals("관련도면업로드:DUBHECO"))
		//		lblreceiveMail.Text = "drawing@dubheco.com";
		//	//else if (cbo수신팀메일.Text.Equals("INVOICE(딘텍)"))
		//	//	lblreceiveMail.Text = "invoice@dintec.co.kr";
		//	//else if (cbo수신팀메일.Text.Equals("INVOICE(두베코)"))
		//	//	lblreceiveMail.Text = "invoice@dubheco.com";
		//}

		#endregion 설정

		#region ==================== 시뮬레이션
		private void BtnSimul_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();

			string nameStr = string.Empty;
			string codeStr = string.Empty;
			string mailStr = string.Empty;
			string nameCodeStr = string.Empty;
			string companyStr = string.Empty;

			OpenFileDialog fileDlg = new OpenFileDialog();
			fileDlg.Filter = Global.MainFrame.DD("메일") + "|*.msg;*.eml";

			if (fileDlg.ShowDialog() != DialogResult.OK) return;

			dt = mailreg.MailSort_Test(fileDlg.FileName);

			if (dt.Rows.Count != 0)
			{
				for (int c = 0; c < dt.Rows.Count; c++)
				{
					companyStr = dt.Rows[c]["CD_COMPANY"].ToString();
					nameCodeStr = nameCodeStr + "/" + dt.Rows[c]["NO_EMP"].ToString();
					nameStr = dt.Rows[c]["NM_KOR"].ToString();
					mailStr = mailStr + "/" + dt.Rows[c]["RECEIVE_MAIL"].ToString();
					codeStr = codeStr + "/" + dt.Rows[c]["NO_MAIL"].ToString();

					if (nameCodeStr.StartsWith("/"))
						nameCodeStr = nameCodeStr.Substring(1, nameCodeStr.Length - 1).ToString();

					if (mailStr.StartsWith("/"))
						mailStr = mailStr.Substring(1, mailStr.Length - 1).ToString();

					if (codeStr.StartsWith("/"))
						codeStr = codeStr.Substring(1, codeStr.Length - 1).ToString();
				}

				ShowMessage("회사: " + companyStr + "\r\n담당자: " + nameStr + "/" + nameCodeStr + "\r\n분류코드: " + codeStr + "\r\n설정메일: " + mailStr);

			}
			else if (dt.Rows.Count == 0)
			{
				ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
		}
		#endregion ==================== 시뮬레이션

		#region ==================== 조회
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			try
			{
				string cd_team_search = cbx수신팀메일.GetValue().ToString();

				//if (cd_team_search == "NON")
				//	cd_team_search = "";

				cd_team_search = "";

				DataTable dt = null;

				dt = DBHelper.GetDataTable("P_CZ_MAIL_SORTER_S", new object[] { CD_COMPANY, tbx제목키워드.Text, tbx제목제외.Text, tbx본문키워드.Text,
					tbx본문제외.Text, ctx매출처.CodeValue, tbx발신자.Text, cbx접수형식.Text, cbx구분.Text, cbx사용여부.Text, ctx회사.CodeValue, ctx담당자.CodeValue, tbx수신자.Text, cd_team_search,
				cbx자동입력.Text});

				flexGrid.Redraw = false;

				flexGrid.Binding = dt;

				flexGrid.Redraw = true;

				if (!flexGrid.HasNormalRow)
				{
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion 조회

		#region ==================== 삭제
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flexGrid.HasNormalRow) return;
				this.flexGrid.Rows.Remove(flexGrid.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion 삭제

		#region ==================== 추가
		protected override bool BeforeAdd()
		{
			return base.BeforeAdd();
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			try
			{
				DataTable dt = flexGrid.DataTable;

				flexGrid.Rows.Add();
				flexGrid.Row = flexGrid.Rows.Count - 1;
				flexGrid["CD_COMPANY"] = CD_COMPANY;
				flexGrid["YN_USE"] = "Y";
				flexGrid["CATEGORY"] = "MAIL";
				flexGrid["YN_CLOUDOC"] = "N";
				flexGrid["YN_INQINSERT"] = "N";
				flexGrid["YN_MAIL"] = "Y";
				flexGrid["CD_TEAM"] = "";
				flexGrid["NM_TEAM"] = "";
				flexGrid["YN_CONTAIN"] = "Y";

				if (!string.IsNullOrEmpty(cbx구분.Text))
					flexGrid["CODE"] = cbx구분.Text;
				else
					flexGrid["CODE"] = "SEND_ETC";

				flexGrid.AddFinished();

				flexGrid.AllowEditing = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion 추가

		#region ==================== 저장
		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;

			return true;
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeSave()) return;

				if (this.MsgAndSave(PageActionMode.Save))
					this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !this.Verify()) return false;

			if (this.flexGrid.IsDataChanged == false) return false;

			if (flexGrid.HasNormalRow)
			{
				if (string.IsNullOrEmpty(this.flexGrid["MAIL_FROM1"].ToString()) && this.flexGrid["CODE"].ToString().Equals("SEND_ETC"))
				{
					ShowMessage("발신자는 필수 입니다.\r\n메일 계정이 아닌 도메인만 입력 가능\r\nex) @dintec.co.kr");
					return false;
				}
				else if (string.IsNullOrEmpty(this.flexGrid["MAIL_FROM1"].ToString()) && this.flexGrid["CODE"].ToString().Equals("INQ"))
				{
					ShowMessage("발신자는 필수 입니다.\r\n메일 계정이 아닌 도메인만 입력 가능\r\nex) @dintec.co.kr");
					return false;
				}
				else if (string.IsNullOrEmpty(this.flexGrid["MAIL_FROM1"].ToString()) && string.IsNullOrEmpty(this.flexGrid["CODE"].ToString()))
				{
					ShowMessage("발신자는 필수 입니다.\r\n메일 계정이 아닌 도메인만 입력 가능\r\nex) @dintec.co.kr");
					return false;
				}
				else if (this.flexGrid["CODE"].ToString().Equals("SEND_ETC") && this.flexGrid["NO_EMP"].ToString().Equals("SYSADMIN"))
				{
					ShowMessage("시스템관리자는 수신담당자로 설정 할 수 없습니다.");
					return false;
				}
				else if (this.flexGrid["CODE"].ToString().Equals("INQ") && this.flexGrid["NO_EMP"].ToString().Equals("SYSADMIN"))
				{
					ShowMessage("시스템관리자는 수신담당자로 설정 할 수 없습니다.");
					return false;
				}
				


				if (string.IsNullOrEmpty(this.flexGrid["NO_EMP"].ToString()) && string.IsNullOrEmpty(this.flexGrid["RECEIVE_MAIL"].ToString()))
				{
					ShowMessage("수신담당자 또는 수신메일을 설정 해주세요.");
					return false;
				}
				else if (string.IsNullOrEmpty(this.flexGrid["NO_EMP"].ToString()))
				{
					ShowMessage("수신담당자는 필수 입니다. \r\n우선순위\r\n1.수신메일\r\n2.수신담당자\r\n수신메일이 설정 되어있어도 수신담당자는 필수 입니다.");
					return false;
				}


				if(CD_COMPANY.Equals("K100") && this.flexGrid["RECEIVE_MAIL"].ToString().Contains("@dubheco.com"))
				{
					if (!Global.MainFrame.LoginInfo.UserID.Equals("S-458"))
					{
						ShowMessage("수신메일로 등록 가능 회사가 아닙니다.");
						return false;
					}
				}
				else if (CD_COMPANY.Equals("K200") && this.flexGrid["RECEIVE_MAIL"].ToString().Contains("@dintec.co.kr"))
				{
					ShowMessage("수신메일로 등록 가능 회사가 아닙니다.");
					return false;
				}


				if(CD_COMPANY.Equals("K100") && this.flexGrid["NM_TEAM"].ToString().Contains("dubheco.com"))
				{
					if (!Global.MainFrame.LoginInfo.UserID.Equals("S-458"))
					{
						ShowMessage("분류팀메일을 다시 확인해주세요. \r\n(다른 회사 사용 금지)");
						return false;
					}
				}
				else if (CD_COMPANY.Equals("K200") && this.flexGrid["NM_TEAM"].ToString().Contains("dintec.co.kr"))
				{
					ShowMessage("분류팀메일을 다시 확인해주세요. \r\n(다른 회사 사용 금지)");
					return false;
				}


				if (string.IsNullOrEmpty(flexGrid["NM_TEAM"].ToString()))
				{
					ShowMessage("분류팀메일을 선택해주세요.");
					return false;
				}


				if (this.flexGrid["RECEIVE_MAIL"].ToString().Contains("service@dintec.co.kr") 
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("dongjin@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("dintec.sales1@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("dintec.sales2@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("dintec.sales3@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("dintec.sales8@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("offshore@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("sb@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("db@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("dintec.log@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("log2@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("service@dubheco.com")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("upload@dubheco.co,")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("upload@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("drawing@dintec.co.kr")
					|| this.flexGrid["RECEIVE_MAIL"].ToString().Contains("drawing@dintec.co.kr"))
				{
					ShowMessage("팀 메일은 수신메일로 등록 할 수 없습니다.");
					return false;
				}
			}


			DataTable dt = this.flexGrid.GetChanges();
			string xml = Util.GetTO_Xml(dt);
			DBMgr.ExecuteNonQuery("SP_CZ_MAIL_SORTER_XML", new object[] { xml });
			//DBMgr.ExecuteNonQuery("SP_CZ_MAIL_SORTER_XML_TEST", new object[] { xml });

			flexGrid.AcceptChanges();

			OnToolBarSearchButtonClicked(null, null);

			return true;
		}
		#endregion 저장
	}
}

