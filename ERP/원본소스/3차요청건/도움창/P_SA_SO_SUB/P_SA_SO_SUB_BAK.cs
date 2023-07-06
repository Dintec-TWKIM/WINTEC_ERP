//********************************************************************
// 최 초 작성자 : 손병수
// 작   성   자 : 문동주
// 재 작  성 일 : 2003-04-03
// 모   듈   명 : 영업
// 시 스  템 명 : 영업관리
// 서브시스템명 : 출하관리
// 페 이 지  명 : 납품의뢰(수주적용)
// 프로젝트  명 : P_SA_SO_SUB
//********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

// Custom NameSpace
using Duzon.Common.Controls;
using Duzon.Common.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Util;

namespace sale
{
	/// <summary>
	/// P_SA_SO_SUB에 대한 요약 설명입니다.
	/// </summary>
	public class P_SA_SO_SUB_BAK : Duzon.Common.Forms.CommonDialog, IHelpWindow
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

		private System.ComponentModel.IContainer components;

		// Panel
		private Duzon.Common.Controls.PanelExt m_pnlGridH;
		private Duzon.Common.Controls.PanelExt m_pnlGridL;
		private Duzon.Common.Controls.PanelExt panel1;
		private Duzon.Common.Controls.PanelExt panel2;
		private Duzon.Common.Controls.PanelExt panel3;
		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;		
		private Duzon.Common.Controls.PanelExt panel8;

		// Label
		private Duzon.Common.Controls.LabelExt m_lblDtSo;
		private Duzon.Common.Controls.LabelExt m_lblCdSalegrp;
		private Duzon.Common.Controls.LabelExt m_lblTitle;
		private Duzon.Common.Controls.LabelExt m_lblPlantGir;
		private Duzon.Common.Controls.LabelExt m_lblCdPartner;
		private Duzon.Common.Controls.LabelExt label1;
		private Duzon.Common.Controls.LabelExt label2;
		private Duzon.Common.Controls.LabelExt label3;
		private Duzon.Common.Controls.LabelExt label4;
		private Duzon.Common.Controls.LabelExt label5;
		private Duzon.Common.Controls.LabelExt label6;

		// RoundedButton
		private Duzon.Common.Controls.RoundedButton m_btnCancel;
		private Duzon.Common.Controls.RoundedButton m_btnApply;
		private Duzon.Common.Controls.RoundedButton m_btnQuery;

		// CodeTextBox
		private Duzon.Common.Controls.CodeTextBox m_cdeTpBusi;
		private Duzon.Common.Controls.CodeTextBox m_cdePlant;
		private Duzon.Common.Controls.CodeTextBox m_cdePartner;

		// CheckBox
		private Duzon.Common.Controls.CheckBoxExt m_chkTpBusiC;

		#endregion

		#region -> 멤버필드(주요)

		private Duzon.Common.Forms.IMainFrame m_imain;

		/// <summary>
		/// 
		/// </summary>
		private Dass.FlexGrid.FlexGrid _flexH;
		private Dass.FlexGrid.FlexGrid _flexL;

		private DataRow[] gdr_src;

		private object[] m_return = new object[1];
		private bool _isPainted  = false;

		private string str_sujonumber = null;
		private string m_chkTpBusi = "";
		private string m_YnReturn  = "";

		//private DataRow[] m_ldrSo;
		private Duzon.Common.Controls.DatePicker m_mskEnd;
		private Duzon.Common.Controls.DatePicker m_mskStart;
		/// <summary>
		/// 멀티 수주 유형
		/// </summary>
		//private string[] m_stSo;
		private Duzon.Common.BpControls.BpCodeTextBox bpGiPartner;
		private Duzon.Common.BpControls.BpComboBox bpTpSo;
		private Duzon.Common.BpControls.BpCodeTextBox bpNm_Sl;
		private Duzon.Common.BpControls.BpCodeTextBox bpSalegrp;

		// 20040401
		DataTable gdt_Selected ; 

		#endregion

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pi_main">MainFrameInterface</param>
		/// <param name="ps_tpbusi">거래구분코드값</param>
		/// <param name="ps_nmtp">거래구분명</param>
		/// <param name="ps_plant">출하공장 코드값</param>
		/// <param name="ps_nmplant">출하공장명</param>
		/// <param name="ps_partner">거래처 코드값</param>
		/// <param name="ps_nmpartner">거래처명</param>
		public P_SA_SO_SUB_BAK(Duzon.Common.Forms.IMainFrame pi_main, string ps_tpbusi, string ps_nmtp, string ps_plant, string ps_nmplant, string ps_partner, string ps_nmpartner, string ps_sicode, string ps_siname, string ps_ynreturn, string ps_sujonumber)
		{
			InitializeComponent();

			this.m_imain = pi_main;						// MainFrameInterface
			this.m_cdeTpBusi.CodeValue	= ps_tpbusi;	// 거래구분코드값
			this.m_cdeTpBusi.CodeName	= ps_nmtp;		// 거래구분명
			this.m_cdePlant.CodeValue	= ps_plant;		// 출하공장
			this.m_cdePlant.CodeName	= ps_nmplant;	// 출하공장명
			this.m_cdePartner.CodeValue	= ps_partner;	// 거래처 코드값
			this.m_cdePartner.CodeName	= ps_nmpartner;	// 거래처명
			this.bpNm_Sl.CodeName		= ps_siname;	// 창고명
			this.bpNm_Sl.CodeValue		= ps_sicode;	// 창고
			this.str_sujonumber			= ps_sujonumber;// 수주번호(필터링 부분)
			this.m_YnReturn				= ps_ynreturn;	// 반품유무

			Load += new System.EventHandler(Page_Load);
			Paint += new System.Windows.Forms.PaintEventHandler(this.Page_Paint);
		}

		
		
		
		public P_SA_SO_SUB_BAK(Duzon.Common.Forms.IMainFrame pi_main, 
										string ps_tpbusi, string ps_nmtp, string ps_plant, string ps_nmplant, 
										string ps_partner, string ps_nmpartner, 
										string ps_sicode, string ps_siname, 
										string ps_ynreturn, string ps_sujonumber,
										DataTable pdt_Selected)
		{
			InitializeComponent();

			this.m_imain = pi_main;						// MainFrameInterface
			this.m_cdeTpBusi.CodeValue	= ps_tpbusi;	// 거래구분코드값
			this.m_cdeTpBusi.CodeName	= ps_nmtp;		// 거래구분명
			this.m_cdePlant.CodeValue	= ps_plant;		// 출하공장
			this.m_cdePlant.CodeName	= ps_nmplant;	// 출하공장명
			this.m_cdePartner.CodeValue	= ps_partner;	// 거래처 코드값
			this.m_cdePartner.CodeName	= ps_nmpartner;	// 거래처명
			this.bpNm_Sl.CodeName		= ps_siname;	// 창고명
			this.bpNm_Sl.CodeValue		= ps_sicode;	// 창고
			this.str_sujonumber			= ps_sujonumber;// 수주번호(필터링 부분)
			this.m_YnReturn				= ps_ynreturn;	// 반품유무

			gdt_Selected = pdt_Selected;				// 이미 선택된 건을 빠지게 하기 위해

			Load += new System.EventHandler(Page_Load);
			Paint += new System.Windows.Forms.PaintEventHandler(this.Page_Paint);
		}


		#endregion

		#region -> Component Designer generated code
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_SO_SUB));
            this.m_lblDtSo = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdSalegrp = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdPartner = new Duzon.Common.Controls.LabelExt();
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bpSalegrp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpNm_Sl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpGiPartner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_mskEnd = new Duzon.Common.Controls.DatePicker();
            this.m_mskStart = new Duzon.Common.Controls.DatePicker();
            this.m_chkTpBusiC = new Duzon.Common.Controls.CheckBoxExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_cdePlant = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.m_cdePartner = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.label5 = new Duzon.Common.Controls.LabelExt();
            this.label4 = new Duzon.Common.Controls.LabelExt();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.label3 = new Duzon.Common.Controls.LabelExt();
            this.panel3 = new Duzon.Common.Controls.PanelExt();
            this.m_lblPlantGir = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.label2 = new Duzon.Common.Controls.LabelExt();
            this.m_cdeTpBusi = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.bpTpSo = new Duzon.Common.BpControls.BpComboBox();
            this.m_btnCancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnApply = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnQuery = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblTitle = new Duzon.Common.Controls.LabelExt();
            this.m_pnlGridH = new Duzon.Common.Controls.PanelExt();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlGridL = new Duzon.Common.Controls.PanelExt();
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.m_pnlGridH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.m_pnlGridL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("GulimChe", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.closeButton.ForeColor = System.Drawing.Color.Black;
            this.closeButton.Location = new System.Drawing.Point(778, 3);
            // 
            // m_lblDtSo
            // 
            this.m_lblDtSo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblDtSo.Location = new System.Drawing.Point(3, 6);
            this.m_lblDtSo.Name = "m_lblDtSo";
            this.m_lblDtSo.Resizeble = true;
            this.m_lblDtSo.Size = new System.Drawing.Size(70, 18);
            this.m_lblDtSo.TabIndex = 7;
            this.m_lblDtSo.Tag = "DT_SO";
            this.m_lblDtSo.Text = "수주일자";
            this.m_lblDtSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdSalegrp
            // 
            this.m_lblCdSalegrp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblCdSalegrp.Location = new System.Drawing.Point(3, 34);
            this.m_lblCdSalegrp.Name = "m_lblCdSalegrp";
            this.m_lblCdSalegrp.Resizeble = true;
            this.m_lblCdSalegrp.Size = new System.Drawing.Size(70, 18);
            this.m_lblCdSalegrp.TabIndex = 0;
            this.m_lblCdSalegrp.Tag = "CD_SALEGRP";
            this.m_lblCdSalegrp.Text = "영업그룹";
            this.m_lblCdSalegrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdPartner
            // 
            this.m_lblCdPartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblCdPartner.Location = new System.Drawing.Point(3, 31);
            this.m_lblCdPartner.Name = "m_lblCdPartner";
            this.m_lblCdPartner.Resizeble = true;
            this.m_lblCdPartner.Size = new System.Drawing.Size(70, 18);
            this.m_lblCdPartner.TabIndex = 0;
            this.m_lblCdPartner.Tag = "CD_PARTNER";
            this.m_lblCdPartner.Text = "거래처";
            this.m_lblCdPartner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 7);
            this.label6.Name = "label6";
            this.label6.Resizeble = true;
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "∼";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bpSalegrp);
            this.panel4.Controls.Add(this.bpNm_Sl);
            this.panel4.Controls.Add(this.bpGiPartner);
            this.panel4.Controls.Add(this.m_mskEnd);
            this.panel4.Controls.Add(this.m_mskStart);
            this.panel4.Controls.Add(this.m_chkTpBusiC);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.m_cdePlant);
            this.panel4.Controls.Add(this.m_cdePartner);
            this.panel4.Controls.Add(this.m_lblDtSo);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.m_cdeTpBusi);
            this.panel4.Controls.Add(this.bpTpSo);
            this.panel4.Location = new System.Drawing.Point(10, 55);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(785, 82);
            this.panel4.TabIndex = 0;
            // 
            // bpSalegrp
            // 
            this.bpSalegrp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpSalegrp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpSalegrp.ButtonImage")));
            this.bpSalegrp.ChildMode = "";
            this.bpSalegrp.CodeName = "";
            this.bpSalegrp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpSalegrp.CodeValue = "";
            this.bpSalegrp.ComboCheck = true;
            this.bpSalegrp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpSalegrp.ItemBackColor = System.Drawing.Color.White;
            this.bpSalegrp.Location = new System.Drawing.Point(373, 56);
            this.bpSalegrp.Name = "bpSalegrp";
            this.bpSalegrp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpSalegrp.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpSalegrp.SearchCode = true;
            this.bpSalegrp.SelectCount = 0;
            this.bpSalegrp.SetDefaultValue = false;
            this.bpSalegrp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpSalegrp.Size = new System.Drawing.Size(181, 21);
            this.bpSalegrp.TabIndex = 4;
            this.bpSalegrp.Text = "bpCodeTextBox1";
            this.bpSalegrp.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpSalegrp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpNm_Sl
            // 
            this.bpNm_Sl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpNm_Sl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNm_Sl.ButtonImage")));
            this.bpNm_Sl.ChildMode = "";
            this.bpNm_Sl.CodeName = "";
            this.bpNm_Sl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNm_Sl.CodeValue = "";
            this.bpNm_Sl.ComboCheck = true;
            this.bpNm_Sl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.bpNm_Sl.ItemBackColor = System.Drawing.Color.White;
            this.bpNm_Sl.Location = new System.Drawing.Point(80, 57);
            this.bpNm_Sl.Name = "bpNm_Sl";
            this.bpNm_Sl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNm_Sl.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNm_Sl.SearchCode = true;
            this.bpNm_Sl.SelectCount = 0;
            this.bpNm_Sl.SetDefaultValue = false;
            this.bpNm_Sl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNm_Sl.Size = new System.Drawing.Size(189, 21);
            this.bpNm_Sl.TabIndex = 3;
            this.bpNm_Sl.Text = "bpCodeTextBox1";
            this.bpNm_Sl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpNm_Sl.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpGiPartner
            // 
            this.bpGiPartner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpGiPartner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpGiPartner.ButtonImage")));
            this.bpGiPartner.ChildMode = "";
            this.bpGiPartner.CodeName = "";
            this.bpGiPartner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpGiPartner.CodeValue = "";
            this.bpGiPartner.ComboCheck = true;
            this.bpGiPartner.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpGiPartner.ItemBackColor = System.Drawing.Color.White;
            this.bpGiPartner.Location = new System.Drawing.Point(373, 30);
            this.bpGiPartner.Name = "bpGiPartner";
            this.bpGiPartner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpGiPartner.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpGiPartner.SearchCode = true;
            this.bpGiPartner.SelectCount = 0;
            this.bpGiPartner.SetDefaultValue = false;
            this.bpGiPartner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpGiPartner.Size = new System.Drawing.Size(181, 21);
            this.bpGiPartner.TabIndex = 2;
            this.bpGiPartner.Text = "bpCodeTextBox1";
            this.bpGiPartner.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpGiPartner.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // m_mskEnd
            // 
            this.m_mskEnd.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskEnd.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskEnd.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskEnd.Location = new System.Drawing.Point(181, 4);
            this.m_mskEnd.Mask = "####/##/##";
            this.m_mskEnd.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskEnd.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskEnd.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskEnd.Modified = false;
            this.m_mskEnd.Name = "m_mskEnd";
            this.m_mskEnd.PaddingCharacter = '_';
            this.m_mskEnd.PassivePromptCharacter = '_';
            this.m_mskEnd.PromptCharacter = '_';
            this.m_mskEnd.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskEnd.ShowToDay = true;
            this.m_mskEnd.ShowTodayCircle = true;
            this.m_mskEnd.ShowUpDown = false;
            this.m_mskEnd.Size = new System.Drawing.Size(85, 21);
            this.m_mskEnd.SunDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TabIndex = 1;
            this.m_mskEnd.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskEnd.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskEnd.ToDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskEnd.UseKeyF3 = false;
            this.m_mskEnd.Value = new System.DateTime(((long)(0)));
            this.m_mskEnd.Validated += new System.EventHandler(this.m_mskEnd_Validated);
            // 
            // m_mskStart
            // 
            this.m_mskStart.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskStart.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskStart.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskStart.Location = new System.Drawing.Point(80, 4);
            this.m_mskStart.Mask = "####/##/##";
            this.m_mskStart.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskStart.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskStart.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskStart.Modified = false;
            this.m_mskStart.Name = "m_mskStart";
            this.m_mskStart.PaddingCharacter = '_';
            this.m_mskStart.PassivePromptCharacter = '_';
            this.m_mskStart.PromptCharacter = '_';
            this.m_mskStart.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskStart.ShowToDay = true;
            this.m_mskStart.ShowTodayCircle = true;
            this.m_mskStart.ShowUpDown = false;
            this.m_mskStart.Size = new System.Drawing.Size(85, 21);
            this.m_mskStart.SunDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TabIndex = 0;
            this.m_mskStart.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskStart.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskStart.ToDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskStart.UseKeyF3 = false;
            this.m_mskStart.Value = new System.DateTime(((long)(0)));
            this.m_mskStart.Validated += new System.EventHandler(this.m_mskStart_Validated);
            // 
            // m_chkTpBusiC
            // 
            this.m_chkTpBusiC.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_chkTpBusiC.Location = new System.Drawing.Point(650, 30);
            this.m_chkTpBusiC.Name = "m_chkTpBusiC";
            this.m_chkTpBusiC.Size = new System.Drawing.Size(104, 22);
            this.m_chkTpBusiC.TabIndex = 10;
            this.m_chkTpBusiC.Text = "기타";
            this.m_chkTpBusiC.UseKeyEnter = false;
            this.m_chkTpBusiC.Click += new System.EventHandler(this.OnCheckBox_Clicked);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Location = new System.Drawing.Point(4, 53);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(775, 1);
            this.panel8.TabIndex = 102;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.Location = new System.Drawing.Point(5, 26);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(775, 1);
            this.panel6.TabIndex = 25;
            // 
            // m_cdePlant
            // 
            this.m_cdePlant.BackColor = System.Drawing.SystemColors.Control;
            this.m_cdePlant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdePlant.CodeName = "";
            this.m_cdePlant.CodeValue = "";
            this.m_cdePlant.IgnoreTextChanged = false;
            this.m_cdePlant.IsConfirmed = false;
            this.m_cdePlant.Location = new System.Drawing.Point(373, 3);
            this.m_cdePlant.Name = "m_cdePlant";
            this.m_cdePlant.ReadOnly = true;
            this.m_cdePlant.SelectedAllEnabled = false;
            this.m_cdePlant.Size = new System.Drawing.Size(182, 21);
            this.m_cdePlant.TabIndex = 8;
            this.m_cdePlant.TabStop = false;
            this.m_cdePlant.UseKeyEnter = false;
            this.m_cdePlant.UseKeyF3 = false;
            // 
            // m_cdePartner
            // 
            this.m_cdePartner.BackColor = System.Drawing.SystemColors.Control;
            this.m_cdePartner.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdePartner.CodeName = "";
            this.m_cdePartner.CodeValue = "";
            this.m_cdePartner.IgnoreTextChanged = false;
            this.m_cdePartner.IsConfirmed = false;
            this.m_cdePartner.Location = new System.Drawing.Point(80, 30);
            this.m_cdePartner.Name = "m_cdePartner";
            this.m_cdePartner.ReadOnly = true;
            this.m_cdePartner.SelectedAllEnabled = false;
            this.m_cdePartner.Size = new System.Drawing.Size(192, 21);
            this.m_cdePartner.TabIndex = 9;
            this.m_cdePartner.TabStop = false;
            this.m_cdePartner.UseKeyEnter = false;
            this.m_cdePartner.UseKeyF3 = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(558, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 78);
            this.panel2.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 57);
            this.label5.Name = "label5";
            this.label5.Resizeble = true;
            this.label5.Size = new System.Drawing.Size(81, 18);
            this.label5.TabIndex = 0;
            this.label5.Tag = "TP_SO";
            this.label5.Text = "수주유형";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 30);
            this.label4.Name = "label4";
            this.label4.Resizeble = true;
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 0;
            this.label4.Tag = "TP_BUSI";
            this.label4.Text = "거래구분기타";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Resizeble = true;
            this.label1.Size = new System.Drawing.Size(79, 18);
            this.label1.TabIndex = 0;
            this.label1.Tag = "TP_BUSI";
            this.label1.Text = "거래구분";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.m_lblCdPartner);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 78);
            this.panel1.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Resizeble = true;
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 0;
            this.label3.Tag = "SL_GIR";
            this.label3.Text = "출하창고";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.m_lblPlantGir);
            this.panel3.Location = new System.Drawing.Point(276, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(95, 26);
            this.panel3.TabIndex = 33;
            // 
            // m_lblPlantGir
            // 
            this.m_lblPlantGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblPlantGir.Location = new System.Drawing.Point(3, 6);
            this.m_lblPlantGir.Name = "m_lblPlantGir";
            this.m_lblPlantGir.Resizeble = true;
            this.m_lblPlantGir.Size = new System.Drawing.Size(88, 18);
            this.m_lblPlantGir.TabIndex = 0;
            this.m_lblPlantGir.Tag = "PLANT_GIR";
            this.m_lblPlantGir.Text = "출하공장";
            this.m_lblPlantGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.m_lblCdSalegrp);
            this.panel7.Location = new System.Drawing.Point(276, 26);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(95, 53);
            this.panel7.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Resizeble = true;
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 0;
            this.label2.Tag = "GI_PARTNER";
            this.label2.Text = "납품처";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cdeTpBusi
            // 
            this.m_cdeTpBusi.BackColor = System.Drawing.SystemColors.Control;
            this.m_cdeTpBusi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdeTpBusi.CodeName = "";
            this.m_cdeTpBusi.CodeValue = "";
            this.m_cdeTpBusi.IgnoreTextChanged = false;
            this.m_cdeTpBusi.IsConfirmed = false;
            this.m_cdeTpBusi.Location = new System.Drawing.Point(648, 3);
            this.m_cdeTpBusi.Name = "m_cdeTpBusi";
            this.m_cdeTpBusi.ReadOnly = true;
            this.m_cdeTpBusi.SelectedAllEnabled = false;
            this.m_cdeTpBusi.Size = new System.Drawing.Size(133, 21);
            this.m_cdeTpBusi.TabIndex = 8;
            this.m_cdeTpBusi.TabStop = false;
            this.m_cdeTpBusi.UseKeyEnter = false;
            this.m_cdeTpBusi.UseKeyF3 = false;
            // 
            // bpTpSo
            // 
            this.bpTpSo.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpTpSo.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpTpSo.ButtonImage")));
            this.bpTpSo.ChildMode = "";
            this.bpTpSo.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpTpSo.ComboCheck = true;
            this.bpTpSo.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB1;
            this.bpTpSo.ItemBackColor = System.Drawing.SystemColors.Window;
            this.bpTpSo.Location = new System.Drawing.Point(648, 57);
            this.bpTpSo.Name = "bpTpSo";
            this.bpTpSo.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpTpSo.SearchCode = true;
            this.bpTpSo.SelectCount = 0;
            this.bpTpSo.SelectedIndex = -1;
            this.bpTpSo.SelectedItem = null;
            this.bpTpSo.SelectedText = "";
            this.bpTpSo.SelectedValue = null;
            this.bpTpSo.SetDefaultValue = false;
            this.bpTpSo.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpTpSo.Size = new System.Drawing.Size(130, 21);
            this.bpTpSo.TabIndex = 5;
            this.bpTpSo.Text = "bpComboBox1";
            this.bpTpSo.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpTpSo.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.BackColor = System.Drawing.Color.White;
            this.m_btnCancel.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnCancel.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Location = new System.Drawing.Point(736, 152);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(60, 24);
            this.m_btnCancel.TabIndex = 13;
            this.m_btnCancel.TabStop = false;
            this.m_btnCancel.Tag = "Q_CANCEL";
            this.m_btnCancel.Text = "취소";
            this.m_btnCancel.UseVisualStyleBackColor = false;
            // 
            // m_btnApply
            // 
            this.m_btnApply.BackColor = System.Drawing.Color.White;
            this.m_btnApply.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnApply.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnApply.Location = new System.Drawing.Point(675, 152);
            this.m_btnApply.Name = "m_btnApply";
            this.m_btnApply.Size = new System.Drawing.Size(60, 24);
            this.m_btnApply.TabIndex = 11;
            this.m_btnApply.TabStop = false;
            this.m_btnApply.Tag = "Q_APPLY";
            this.m_btnApply.Text = "적용";
            this.m_btnApply.UseVisualStyleBackColor = false;
            this.m_btnApply.Click += new System.EventHandler(this.OnApply);
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.BackColor = System.Drawing.Color.White;
            this.m_btnQuery.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnQuery.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnQuery.Location = new System.Drawing.Point(614, 152);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Size = new System.Drawing.Size(60, 24);
            this.m_btnQuery.TabIndex = 10;
            this.m_btnQuery.TabStop = false;
            this.m_btnQuery.Tag = "Q_QUERY";
            this.m_btnQuery.Text = "조회";
            this.m_btnQuery.UseVisualStyleBackColor = false;
            this.m_btnQuery.Click += new System.EventHandler(this.OnSearchButtonClicked);
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.Controls.Add(this.m_lblTitle);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(805, 47);
            this.panel5.TabIndex = 0;
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.AutoSize = true;
            this.m_lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.m_lblTitle.Font = new System.Drawing.Font("GulimChe", 10F, System.Drawing.FontStyle.Bold);
            this.m_lblTitle.ForeColor = System.Drawing.Color.White;
            this.m_lblTitle.Location = new System.Drawing.Point(15, 16);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Resizeble = false;
            this.m_lblTitle.Size = new System.Drawing.Size(143, 14);
            this.m_lblTitle.TabIndex = 0;
            this.m_lblTitle.Tag = "APPLY_SO";
            this.m_lblTitle.Text = "납품의뢰(수주적용)";
            // 
            // m_pnlGridH
            // 
            this.m_pnlGridH.Controls.Add(this._flexH);
            this.m_pnlGridH.Location = new System.Drawing.Point(10, 183);
            this.m_pnlGridH.Name = "m_pnlGridH";
            this.m_pnlGridH.Size = new System.Drawing.Size(785, 220);
            this.m_pnlGridH.TabIndex = 8;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.IsDataChanged = false;
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.RowFilter = "";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(785, 220);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            // 
            // m_pnlGridL
            // 
            this.m_pnlGridL.Controls.Add(this._flexL);
            this.m_pnlGridL.Location = new System.Drawing.Point(10, 411);
            this.m_pnlGridL.Name = "m_pnlGridL";
            this.m_pnlGridL.Size = new System.Drawing.Size(785, 210);
            this.m_pnlGridL.TabIndex = 9;
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.IsDataChanged = false;
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.RowFilter = "";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 18;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(785, 210);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            // 
            // P_SA_SO_SUB
            // 
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(804, 626);
            this.Controls.Add(this.m_pnlGridL);
            this.Controls.Add(this.m_pnlGridH);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.m_btnQuery);
            this.Controls.Add(this.m_btnApply);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("GulimChe", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "P_SA_SO_SUB";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.m_pnlGridH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.m_pnlGridL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region  -> 소멸자
		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#endregion

		#endregion

		#region ♣ 초기화

		#region -> Page_Load

		/// <summary>
		/// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
		/// </summary>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				this.Enabled   = false;

				InitGridH();
				InitGridL();

				// 해당하는 언어에 맞게 Label 초기화
				InitControl();
				Application.DoEvents();			
			}
			catch(Exception ex)
			{
				m_imain.MsgEnd(ex);
			}
		}

		#endregion

		#region -> GetDDItem

		private string GetDDItem(params string[] colName)
		{
			string temp = "";
			
			for(int i = 0; i < colName.Length; i++)
			{
				switch(colName[i])		// DataView 의 컬럼이름
				{
						// ==================== header grid setting ======================== //
					case "NO_SO":		// 1.수주번호
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","NO_SO");
						break;
					case "DT_SO":		// 2.수주일자
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","DT_SO");
						break;
					case "NM_SO":		// 3.수주형태
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","TP_SO");
						break;
					case "NM_EXCH":		// 4.화폐단위
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","CD_EXCH");
						break;
					case "NM_SALEGRP":	// 5.영업그룹
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","CD_SALEGRP");
						break;
					case "NM_KOR":		// 6.담당자
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","NO_EMP");
						break;
					case "DC_RMK":		// 7.비고
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","DC_RMK");
						break;
						// ==================== line grid setting ======================== //
					case "CHK":			// 1.선택
						temp = temp + " + " + "S";
						break;
					case "CD_ITEM":		// 2.품목코드
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","CD_ITEM");
						break;
					case "NM_ITEM":		// 3.품목명
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","NM_ITEM");
						break;
					case "STA_SO":		// 4.규격
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","STA_SO");
						break;
					case "UNIT_SO":		// 5.단위
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","UNIT");
						break;
					case "DT_DUEDATE":	// 6.납기일
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","DT_DUEDTE");
						break;
					case "QT_REMAIN":	// 7.오더잔량
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","QT_REMAIN");
						break;
					case "YN_INSPECT":	// 8.검사유무
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","YN_INSPECT");
						break;
					case "QT_GIR1":		// 9.의뢰수량
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","QT_GIR");
						break;
					case "UM_SO":		// 10.단가
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","PRICE");
						break;
					case "AM_EXGIR1":	// 11.금액
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","AMT");
						break;
					case "AM_GIR1":		// 12.원화금액
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","AM_WON");
						break;
					case "CD_SL":		// 13.창고
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","SL_GIR");
						break;
					case "UNIT_IM":		// 14.재고단위
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","UNIT_IM");
						break;
					case "QT_IM1":		// 15.재고량
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","QT_IM");
						break;
					default :	
						break;
				}
			}
			
			if(temp == "")
				return "";
			else
				return temp.Substring(3,temp.Length-3);
		}

		#endregion

		#region -> InitGridH

		private void InitGridH()
		{
			Application.DoEvents();
			
			_flexH.Redraw = false;

			_flexH.Rows.Count = 1;
			_flexH.Rows.Fixed = 1;
			_flexH.Cols.Count = 8;
			_flexH.Cols.Fixed = 1;
			_flexH.Rows.DefaultSize = 20;
			
			_flexH.Cols[0].Width = 50;

			// 1.수주번호
			_flexH.Cols[1].Name = "NO_SO";
			_flexH.Cols[1].DataType = typeof(string);
			_flexH.Cols[1].Width = 140;
			_flexH.Cols[1].AllowEditing = false;

			// 2. 수주일자
			_flexH.Cols[2].Name = "DT_SO";
			_flexH.Cols[2].DataType = typeof(string);
			_flexH.Cols[2].Width = 70;
			_flexH.Cols[2].AllowEditing = false;
			_flexH.Cols[2].EditMask = this.m_imain.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			_flexH.Cols[2].Format = _flexH.Cols[2].EditMask;
			_flexH.SetStringFormatCol("DT_SO");
			_flexH.SetNoMaskSaveCol("DT_SO");
			
			// 3. 수주형태
			_flexH.Cols[3].Name = "NM_SO";
			_flexH.Cols[3].DataType = typeof(string);
			_flexH.Cols[3].Width = 100;
			_flexH.Cols[3].AllowEditing = false;

			// 4. 화폐단위
			_flexH.Cols[4].Name = "NM_EXCH";
			_flexH.Cols[4].DataType = typeof(string);
			_flexH.Cols[4].Width = 80;
			_flexH.Cols[4].AllowEditing = false;

			// 5. 영업그룹
			_flexH.Cols[5].Name = "NM_SALEGRP";
			_flexH.Cols[5].DataType = typeof(string);
			_flexH.Cols[5].Width = 80;
			_flexH.Cols[5].AllowEditing = false;


			// 6. 담당자
			_flexH.Cols[6].Name = "NM_KOR";
			_flexH.Cols[6].DataType = typeof(string);
			_flexH.Cols[6].Width = 80;
			_flexH.Cols[6].AllowEditing = false;

			// 7. 비고
			_flexH.Cols[7].Name = "DC_RMK";
			_flexH.Cols[7].DataType = typeof(string);
			_flexH.Cols[7].Width = 185;
			_flexH.Cols[7].AllowEditing = false;

			_flexH.AllowSorting = AllowSortingEnum.None;
			_flexH.NewRowEditable = true;
			_flexH.EnterKeyAddRow = true;

			_flexH.SumPosition = SumPositionEnum.None;
			_flexH.GridStyle = GridStyleEnum.Green;

			this.m_imain.SetUserGrid(_flexH);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexH.Cols.Count-1; i++)
				_flexH[0, i] = GetDDItem(_flexH.Cols[i].Name);

			_flexH.Redraw = true;

			// 그리드 이벤트 선언
			_flexH.AfterRowColChange	+= new C1.Win.C1FlexGrid.RangeEventHandler(_flexH_AfterRowColChange);
		}

		#endregion

		#region -> InitGridL

		private void InitGridL()
		{
			
			Application.DoEvents();
			
			_flexL.Redraw = false;

			_flexL.Rows.Count = 1;
			_flexL.Rows.Fixed = 1;
			_flexL.Cols.Count = 16;
			_flexL.Cols.Fixed = 1;
			_flexL.Rows.DefaultSize = 20;
			
			_flexL.Cols[0].Width = 50;

			// 1.선택
			_flexL.Cols[1].Name = "CHK";
			_flexL.Cols[1].DataType = typeof(string);
			_flexL.Cols[1].Width = 45;
			_flexL.Cols[1].Format = "Y;N";
			_flexL.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			
			// 2.품목코드
			_flexL.Cols[2].Name = "CD_ITEM";
			_flexL.Cols[2].DataType = typeof(string);
			_flexL.Cols[2].Width = 100;
			_flexL.Cols[2].AllowEditing = false;

			// 3.품목명
			_flexL.Cols[3].Name = "NM_ITEM";
			_flexL.Cols[3].DataType = typeof(string);
			_flexL.Cols[3].Width = 80;
			_flexL.Cols[3].AllowEditing = false;

			// 4.규격
			_flexL.Cols[4].Name = "STA_SO";
			_flexL.Cols[4].DataType = typeof(string);
			_flexL.Cols[4].Width = 80;
			_flexL.Cols[4].AllowEditing = false;

			// 5.단위
			_flexL.Cols[5].Name = "UNIT_SO";
			_flexL.Cols[5].DataType = typeof(string);
			_flexL.Cols[5].Width = 80;
			_flexL.Cols[5].AllowEditing = false;
	
			// 6.납기일
			_flexL.Cols[6].Name = "DT_DUEDATE";
			_flexL.Cols[6].DataType = typeof(string);
			_flexL.Cols[6].Width = 70;
			_flexL.Cols[6].AllowEditing = false;
			_flexL.Cols[6].EditMask = this.m_imain.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			_flexL.Cols[6].Format = _flexL.Cols[6].EditMask;
			_flexL.SetStringFormatCol("DT_DUEDATE");
			_flexL.SetNoMaskSaveCol("DT_DUEDATE");

			// 7.오더잔량
			_flexL.Cols[7].Name = "QT_REMAIN";
			_flexL.Cols[7].DataType = typeof(decimal);
			_flexL.Cols[7].Width = 80;
			_flexL.Cols[7].AllowEditing = false;
			this.m_imain.SetFormat(_flexL.Cols[7], DataDictionaryTypes.MA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 8.검사유무
			_flexL.Cols[8].Name = "YN_INSPECT";
			_flexL.Cols[8].DataType = typeof(string);
			_flexL.Cols[8].Width = 80;
			_flexL.Cols[8].AllowEditing = false;

			// 9.의뢰수량
			_flexL.Cols[9].Name = "QT_GIR1";
			_flexL.Cols[9].DataType = typeof(decimal);
			_flexL.Cols[9].Width = 80;
			_flexL.Cols[9].AllowEditing = false;
			this.m_imain.SetFormat(_flexL.Cols[9], DataDictionaryTypes.MA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 10.단가
			_flexL.Cols[10].Name = "UM_SO";
			_flexL.Cols[10].DataType = typeof(decimal);
			_flexL.Cols[10].Width = 80;
			_flexL.Cols[10].AllowEditing = false;
			this.m_imain.SetFormat(_flexL.Cols[10], DataDictionaryTypes.MA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 11.금액
			_flexL.Cols[11].Name = "AM_EXGIR1";
			_flexL.Cols[11].DataType = typeof(decimal);
			_flexL.Cols[11].Width = 80;
			_flexL.Cols[11].AllowEditing = false;
			this.m_imain.SetFormat(_flexL.Cols[11], DataDictionaryTypes.MA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 12.원화금액
			_flexL.Cols[12].Name = "AM_GIR1";
			_flexL.Cols[12].DataType = typeof(decimal);
			_flexL.Cols[12].Width = 80;
			_flexL.Cols[12].AllowEditing = false;
			this.m_imain.SetFormat(_flexL.Cols[12], DataDictionaryTypes.MA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 13.창고
			_flexL.Cols[13].Name = "CD_SL";
			_flexL.Cols[13].DataType = typeof(string);
			_flexL.Cols[13].Width = 80;
			_flexL.Cols[13].AllowEditing = false;

			// 14.재고단위
			_flexL.Cols[14].Name = "UNIT_IM";
			_flexL.Cols[14].DataType = typeof(string);
			_flexL.Cols[14].Width = 90;
			_flexL.Cols[14].AllowEditing = false;

			// 15.재고량
			_flexL.Cols[15].Name = "QT_IM1";
			_flexL.Cols[15].DataType = typeof(decimal);
			_flexL.Cols[15].Width = 100;
			_flexL.Cols[15].AllowEditing = false;
			this.m_imain.SetFormat(_flexL.Cols[15], DataDictionaryTypes.MA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			_flexL.AllowSorting = AllowSortingEnum.None;
			_flexL.NewRowEditable = true;
			_flexL.EnterKeyAddRow = false;
			
			_flexL.SumPosition = SumPositionEnum.None;
			_flexL.GridStyle = GridStyleEnum.Blue;

			this.m_imain.SetUserGrid(_flexL);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexL.Cols.Count-1; i++)
				_flexL[0, i] = GetDDItem(_flexL.Cols[i].Name);

			_flexL.Redraw = true;
		}

		#endregion

		#region -> InitControl

		private void InitControl()
		{
			try
			{
				//마스크 셋팅
				this.m_mskStart.Mask	= this.m_imain.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
				this.m_mskEnd.Mask		= this.m_imain.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);

				foreach(Control ctr in this.Controls)
				{
					if(ctr is Duzon.Common.Controls.PanelExt)
					{
						foreach(Control ctrl in ((Duzon.Common.Controls.PanelExt)ctr).Controls)
						{
							if(ctrl is Duzon.Common.Controls.PanelExt)
							{
								foreach(Control ctrls in ((Duzon.Common.Controls.PanelExt)ctrl).Controls)
								{
									if(ctrls is Duzon.Common.Controls.LabelExt)
										((Duzon.Common.Controls.LabelExt)ctrls).Text = m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)((Duzon.Common.Controls.LabelExt)ctrls).Tag);
								}
							}
						}
					}
				}
				m_btnQuery.Text = m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "Q_QUERY");
				m_btnApply.Text = m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "Q_APPLY");
				m_btnCancel.Text = m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "Q_CANCEL");
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion
		
		#region -> Page_Paint

		private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(this._isPainted)
				return;

			try
			{
				this._isPainted = true;
				Application.DoEvents();
				
				this.m_lblTitle.Visible = true;
				this.m_lblTitle.Text	= this.m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "P_SA_SO_SUB");
				this.m_lblTitle.Show();
				Application.DoEvents();
			
				// 수주일자 From / To
				m_mskStart.Text = m_imain.GetStringFirstDayInMonth;
				m_mskEnd.Text   = m_imain.GetStringToday;

				if(m_cdeTpBusi.CodeValue == "001" || m_cdeTpBusi.CodeValue == "003")
					this.m_chkTpBusiC.Enabled = true;
				else
					this.m_chkTpBusiC.Enabled = false;
					
				this.Enabled = true; //페이지 전체 활성
				m_mskStart.Focus();
			}
			catch(Exception ex)
			{
				m_imain.MsgEnd(ex);
			}
		}

		#endregion

		#endregion

		#region ♣ 메인버튼 이벤트 / 메소드

		#region -> 조회조건체크

		private bool SearchCondition()
		{
			//수주일자
			if((m_mskStart.Text.Trim() == "") && (m_mskEnd.Text.Trim() == ""))
			{
				//수주일자 은(는) 필수 입력입니다.
				this.m_imain.ShowMessage("WK1_004", m_lblDtSo.Text);
				m_mskStart.Focus();
				return false;
			}

			//수주일자
			if(((m_mskStart.Text.Trim() != "") && (m_mskEnd.Text.Trim() == "")) ||
				((m_mskStart.Text.Trim() == "") && (m_mskEnd.Text.Trim() != "")))
			{
				//수주일자 은(는) 필수 입력입니다.
				this.m_imain.ShowMessage("WK1_004", m_lblDtSo.Text);
				return false;
			}

			return true;
		}

		#endregion

		#region -> 조회버튼클릭

		/// <summary>
		/// 조회
		/// </summary>
		private void OnSearchButtonClicked(object sender, System.EventArgs e)
		{
			try
			{
				this.m_lblTitle.Focus();
				
				if(!SearchCondition())
					return;

				string ls_so = string.Empty;

                //// 수주유형
                //if (this.m_stSo != null)
                //{
                //    if (m_stSo.Length == 1)
                //        ls_so = " AND A.TP_SO = '" + m_stSo[0] + "' ";
                //    else
                //    {
                //        ls_so = " AND A.TP_SO IN (";
                //        for (int i = 0; i < m_stSo.Length - 1; i++)
                //        {
                //            ls_so = ls_so + " '" + m_stSo[i] + "', ";
                //        }
                //        ls_so = ls_so + " '" + m_stSo[m_stSo.Length - 1] + "')";
                //    }
                //}

                SpInfoCollection sic = new SpInfoCollection();
                SpInfo siM = new SpInfo();
                siM.SpNameSelect = "SP_SA_SO_SUB_SELECT_H";
                siM.SpParamsSelect = new object[11]{	m_imain.LoginInfo.CompanyCode,  // 회사코드
                                                        m_mskStart.Text,            // 수주일자(from)
                                                        m_mskEnd.Text,              // 수주일자(to)
                                                        m_cdeTpBusi.CodeValue,      // 거래구분
                                                        m_cdePlant.CodeValue,       // 출하공장
                                                        m_cdePartner.CodeValue,     // 거래처
                                                        bpGiPartner.CodeValue,      // 납품처
                                                        bpNm_Sl.CodeValue,          // 출하창고
                                                        bpSalegrp.CodeValue,        // 영업그룹
                                                        this.m_chkTpBusi,           // 거래구분
                                                        this.m_YnReturn };           // 반품유무
                sic.Add(siM);
                SpInfo siL = new SpInfo();
                siL.SpNameSelect = "SP_SA_SO_SUB_SELECT_L";
                siL.SpParamsSelect = new object[11]{	m_imain.LoginInfo.CompanyCode,  // 회사코드
                                                        m_mskStart.Text,            // 수주일자(from)
                                                        m_mskEnd.Text,              // 수주일자(to)
                                                        m_cdeTpBusi.CodeValue,      // 거래구분
                                                        m_cdePlant.CodeValue,       // 출하공장
                                                        m_cdePartner.CodeValue,     // 거래처
                                                        bpGiPartner.CodeValue,      // 납품처
                                                        bpNm_Sl.CodeValue,          // 출하창고
                                                        bpSalegrp.CodeValue,        // 영업그룹
                                                        this.m_chkTpBusi,           // 거래구분
                                                        this.m_YnReturn };           // 반품유무
                sic.Add(siL);
                DataSet g_dsSo = (DataSet)this.m_imain.FillDataSet(sic);


				//object[] args = { m_imain.LoginInfo.CompanyCode, argument, str_sujonumber};
				//DataSet g_dsSo = (DataSet)m_imain.InvokeRemoteMethod("SaleOrder_NTX", "sale.CC_SA_SO_NTX", "CC_SA_SO_NTX.rem", "SelectBySo", args);
				
				#region >> SP 빼기

//				SpInfoCollection sic = new SpInfoCollection();
//
//				SpInfo siM = new SpInfo();
//				siM.SpNameSelect = "SP_SA_SO_SUB_SELECT_H";
//				siM.SpParamsSelect = new Object[]{m_imain.LoginInfo.CompanyCode, 
//													 m_mskStart.Text, m_mskEnd.Text, 
//													 m_cdeTpBusi.CodeValue, 
//													 m_cdePlant.CodeValue, 
//													 m_cdePartner.CodeValue, 
//													 bpGiPartner.CodeValue, 
//													 bpNm_Sl.CodeValue, 
//													 bpSalegrp.CodeValue,
//													 m_chkTpBusi, 
//													 ls_so, 
//													 m_YnReturn,
//													 str_sujonumber};
//				sic.Add(siM);
//
//				SpInfo siD = new SpInfo();
//				siD.SpNameSelect = "SP_SA_SO_SUB_SELECT_L";
//				siD.SpParamsSelect = new Object[] {m_imain.LoginInfo.CompanyCode, "002"};
//				sic.Add(siD);
//
//				DataSet g_dsSo = (DataSet)this.m_imain.FillDataSet(sic);

				#endregion

				// 2003/04/14
				DeleteSelectedItem(g_dsSo.Tables[1]);
				g_dsSo.Tables[1].AcceptChanges();

				// Detail 바인딩
				_flexL.Redraw=false;
				_flexL.BindingStart();
				_flexL.DataSource = g_dsSo.Tables[1].DefaultView;
				_flexL.BindingEnd();
				_flexL.EmptyRowFilter();	
				_flexL.Redraw=true;

				// Master 바인딩
				_flexH.Redraw=false;
				_flexH.BindingStart();
				_flexH.DataSource = g_dsSo.Tables[0].DefaultView;	
				_flexH.BindingEnd();

				if(_flexH.HasNormalRow)		
				{
					int row = _flexH.Row;

					_flexH.Row = -1;
					_flexH.Row = row;
				}

				_flexH.Redraw = true;

				if(!_flexH.HasNormalRow)
				{
					// 검색된 내용이 존재하지 않습니다.
					m_imain.ShowMessage("IK1_003");
				}
			}
			catch(Exception ex)
			{
				m_imain.MsgEnd(ex);
			}			
		}

		#endregion

		#region -> 적용버튼클릭

		private void OnApply(object sender, System.EventArgs e)
		{
			try
			{
				if(_flexL.DataView == null || _flexL.DataView.Count == 0)
					return;

				string ls_filter;

				if(_flexH.DataView.Count == 0)
					ls_filter = "CHK= ''";
				else
					ls_filter = "CHK= 'Y'";

				gdr_src = _flexL.DataTable.Select(ls_filter,"",DataViewRowState.CurrentRows);
			
				if(gdr_src.Length == 0)
					return;

				m_return[0] = gdr_src;

				DialogResult= DialogResult.OK;
			}
			catch(Exception ex)
			{
				m_imain.MsgEnd(ex);
			}
		}

		#endregion

		private void DeleteSelectedItem(DataTable pdt_Detail)
		{
			try
			{
				if( pdt_Detail == null || pdt_Detail.Rows.Count <=0)
				{				
					return;					
				}

				if( gdt_Selected == null || gdt_Selected.Rows.Count <=0)
				{				
					return;					
				}

				DataRow row ;
				for(int i =0 ; i < gdt_Selected.Rows.Count ;i++)
				{
					row = gdt_Selected.Rows[i];

					string ls_select = "NO_SO ='"+ row["NO_SO"].ToString()+"' AND SEQ_SO ="+row["SEQ_SO"].ToString().Trim()+"";
					
					DataRow[] rows = pdt_Detail.Select(ls_select);	
					if( rows != null)
					{
						for(int j = 0 ; j < rows.Length ; j++)
						{
							rows[j].Delete();
						}
					}
				}
			}
			catch(Exception ex)
			{
				m_imain.MsgEnd(ex);
			}
		}


		#endregion

		#region ♣ 그리드 이벤트 / 메소드

		#region -> _flexH_AfterRowColChange

		private void _flexH_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			if(!_flexH.IsBindingEnd || !_flexH.HasNormalRow)
			{
				_flexL.EmptyRowFilter();
				return;
			}

			if(_flexL.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
			{
				string filter = "NO_SO = '" + _flexH[_flexH.Row,"NO_SO"].ToString() + "'";

				_flexL.DataView.RowFilter  = filter;
			}
		}
		
		#endregion

		#endregion

		#region ♣ 이벤트 / 메소드

		#region -> OnComboKeyDown

		private void OnComboKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				SendKeys.Send("{TAB}");	
		}

		#endregion
	
		#region -> OnControlKeyDown

		private void OnControlKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.Enter :
					// 도움창의 TextBox에 Enter키 입력시 코드, 명 값 조회
					if((sender.GetType().Name == "CodeTextBox") || (sender.GetType().Name == "MaskedEditBox"))
						SendKeys.Send("{TAB}");
					break;
				case Keys.Up :
					if((sender.GetType().Name == "CodeTextBox") || (sender.GetType().Name == "MaskedEditBox"))
						SendKeys.Send("{TAB}");
					break;
				case Keys.Down :
					if((sender.GetType().Name == "CodeTextBox") || (sender.GetType().Name == "MaskedEditBox"))
						SendKeys.Send("{TAB}");
					break;
				
				
			}
		}

		#endregion

		#region -> OnCheckBox_Clicked

		private void OnCheckBox_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				if(this.m_chkTpBusiC.Checked == true)
					this.m_chkTpBusi = "006";
				else
					this.m_chkTpBusi = "";
			}
			catch(Exception ex)
			{
				this.m_imain.ShowErrorMessage(ex,this.m_lblTitle.Text);
			}
		}

		#endregion

		#region -> 속성값설정 이벤트

		object[] IHelpWindow.ReturnValues
		{
			get { return m_return;	}
		}
		public DataRow[] GetDataRow
		{
			get { return gdr_src; }
		}

		#endregion

		#endregion

		#region >> 날짜 관련 함수 및 이벤트 
		
		private void m_mskStart_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_mskStart.IsValidated)
			{
				m_imain.ShowMessage("WK1_003", m_lblDtSo.Text);
				this.m_mskStart.Focus();
			}
		}

		private void m_mskEnd_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_mskEnd.IsValidated)
			{
				m_imain.ShowMessage("WK1_003", m_lblDtSo.Text);
				this.m_mskEnd.Focus();
			}
		}

		#endregion

		private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			if(e.DialogResult == DialogResult.Cancel)
				return;

			switch(e.ControlName)
			{
				case "bpGiPartner": // 거래처
					bpGiPartner.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
					bpGiPartner.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();					
					break;

				case "bpNm_Sl":		// 창고									
					bpNm_Sl.CodeName = e.HelpReturn.Rows[0]["NM_SL"].ToString();
					bpNm_Sl.CodeValue = e.HelpReturn.Rows[0]["CD_SL"].ToString();
					break;

				case "bpSalegrp" :	// 영업그룹	
					bpSalegrp.CodeValue = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
//					m_dtSoH.Rows[0]["CD_BIZAREA"] = e.HelpReturn.Rows[0]["CD_BIZAREA"].ToString();
					bpSalegrp.CodeName = e.HelpReturn.Rows[0]["NM_SALEGRP"].ToString();
					break;
					
				case "bpTpSo"://수주유형

					break;
			}
		}

		private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.MainFrame = m_imain;

			switch(e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB1:
					e.HelpParam.P61_CODE1 = "N";
					e.HelpParam.P62_CODE2 = "Y";
					break;	
				case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
					e.HelpParam.P09_CD_PLANT = m_cdePlant.CodeValue;
					
					break;	

			}
		}



	}
}
