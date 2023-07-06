using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;


using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;


namespace sale
{
	// **************************************
	// 작   성   자 : 이금주
	// 재 작  성 일 : 2003-10-01
	// 모   듈   명 : 영업
	// 시 스  템 명 : 영업관리
	// 서브시스템명 : 반품의뢰관리
	// 페 이 지  명 : 출고반품의뢰등록
	// 프로젝트  명 : P_SA_GIRR_REG
	// **************************************
	public class P_SA_GIRR_REG_BAK :  Duzon.Common.Forms.PageBase
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

		// Panel
		private Duzon.Common.Controls.PanelExt m_pnlGirL;
        private Duzon.Common.Controls.PanelExt panel1;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.PanelExt panel10;

        // Label
		private Duzon.Common.Controls.LabelExt  m_lblNoGir;
		private Duzon.Common.Controls.LabelExt  m_lblCdPartner;
		private Duzon.Common.Controls.LabelExt  m_lblDtGir;
        private Duzon.Common.Controls.LabelExt m_lblTpGi;
		private Duzon.Common.Controls.LabelExt  m_lblPlantGir;
        private Duzon.Common.Controls.LabelExt m_lblEmpGir;

		// RoundedButton
		private Duzon.Common.Controls.RoundedButton m_btnApplySo;
		private Duzon.Common.Controls.RoundedButton m_btnDelete;

		// DropDownComboBox
		private Duzon.Common.Controls.DropDownComboBox m_cboTpBusi;
		private Duzon.Common.Controls.DropDownComboBox m_cboLcCode;
        private Duzon.Common.Controls.DropDownComboBox m_cboPlantGir;

		// TextBox
		private Duzon.Common.Controls.TextBoxExt m_txtNoGir;
		private Duzon.Common.Controls.TextBoxExt m_txtDcRmk;

		// IContainer
		private System.ComponentModel.IContainer components;
		
		/********************************************/
		private DataTable g_dtGirH;
		private DataTable g_dtGirL;
		private Duzon.Common.Controls.DatePicker m_mskDtGir;
	
		private int Seq_Gir;
		//20040401 적용을 받기 위한 테이블
		DataTable gdt_ReqDataREQ = new DataTable();

		private string acd_dept,anm_dept, no_seq = "";
		
		#endregion

		#region -> 멤버필드(주요)

		/// <summary>
		/// 그리드 선언
		/// </summary>
		private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.BpControls.BpCodeTextBox bpCdPartner;
		private Duzon.Common.BpControls.BpCodeTextBox bpNoEmp;
        private TableLayoutPanel tableLayoutPanel1;
        private LabelExt m_lblDcRmk;
        private Duzon.Common.BpControls.BpCodeTextBox bpTpGi;
        private DropDownComboBox m_cboFgGirq;
        private Panel panel2;
        private LabelExt labelExt1;

		/// <summary>
		/// Load여부 변수(Paint Event에서 사용)
		/// </summary>
		private bool _isPainted = false;

		#endregion

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자

        public P_SA_GIRR_REG_BAK()
		{
			// 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
			InitializeComponent();

			InitializeDefaultDataTable("Y");

			this.Load += new System.EventHandler(Page_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Page_Paint); 

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_GIRR_REG_BAK));
            this.m_lblNoGir = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.bpTpGi = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpNoEmp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpCdPartner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_mskDtGir = new Duzon.Common.Controls.DatePicker();
            this.m_cboTpBusi = new Duzon.Common.Controls.DropDownComboBox();
            this.m_cboLcCode = new Duzon.Common.Controls.DropDownComboBox();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.m_lblCdPartner = new Duzon.Common.Controls.LabelExt();
            this.m_lblEmpGir = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.m_lblDcRmk = new Duzon.Common.Controls.LabelExt();
            this.m_lblDtGir = new Duzon.Common.Controls.LabelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_lblPlantGir = new Duzon.Common.Controls.LabelExt();
            this.m_lblTpGi = new Duzon.Common.Controls.LabelExt();
            this.m_txtNoGir = new Duzon.Common.Controls.TextBoxExt();
            this.m_cboPlantGir = new Duzon.Common.Controls.DropDownComboBox();
            this.m_txtDcRmk = new Duzon.Common.Controls.TextBoxExt();
            this.m_btnApplySo = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_pnlGirL = new Duzon.Common.Controls.PanelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_btnDelete = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_cboFgGirq = new Duzon.Common.Controls.DropDownComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskDtGir)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.m_pnlGirL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblNoGir
            // 
            this.m_lblNoGir.Location = new System.Drawing.Point(3, 5);
            this.m_lblNoGir.Name = "m_lblNoGir";
            this.m_lblNoGir.Resizeble = true;
            this.m_lblNoGir.Size = new System.Drawing.Size(75, 18);
            this.m_lblNoGir.TabIndex = 0;
            this.m_lblNoGir.Tag = "NO_GIR";
            this.m_lblNoGir.Text = "의뢰번호";
            this.m_lblNoGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.bpTpGi);
            this.panel1.Controls.Add(this.bpNoEmp);
            this.panel1.Controls.Add(this.bpCdPartner);
            this.panel1.Controls.Add(this.m_mskDtGir);
            this.panel1.Controls.Add(this.m_cboTpBusi);
            this.panel1.Controls.Add(this.m_cboLcCode);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.m_txtNoGir);
            this.panel1.Controls.Add(this.m_cboPlantGir);
            this.panel1.Controls.Add(this.m_txtDcRmk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 80);
            this.panel1.TabIndex = 0;
            // 
            // bpTpGi
            // 
            this.bpTpGi.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpTpGi.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpTpGi.ButtonImage")));
            this.bpTpGi.ChildMode = "";
            this.bpTpGi.CodeName = "";
            this.bpTpGi.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpTpGi.CodeValue = "";
            this.bpTpGi.ComboCheck = true;
            this.bpTpGi.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB;
            this.bpTpGi.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpTpGi.Location = new System.Drawing.Point(366, 28);
            this.bpTpGi.Name = "bpTpGi";
            this.bpTpGi.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpTpGi.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpTpGi.SearchCode = true;
            this.bpTpGi.SelectCount = 0;
            this.bpTpGi.SetDefaultValue = false;
            this.bpTpGi.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpTpGi.Size = new System.Drawing.Size(161, 21);
            this.bpTpGi.TabIndex = 31;
            this.bpTpGi.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpTpGi.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpNoEmp
            // 
            this.bpNoEmp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpNoEmp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNoEmp.ButtonImage")));
            this.bpNoEmp.ChildMode = "";
            this.bpNoEmp.CodeName = "";
            this.bpNoEmp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNoEmp.CodeValue = "";
            this.bpNoEmp.ComboCheck = true;
            this.bpNoEmp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpNoEmp.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpNoEmp.Location = new System.Drawing.Point(616, 30);
            this.bpNoEmp.Name = "bpNoEmp";
            this.bpNoEmp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNoEmp.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNoEmp.SearchCode = true;
            this.bpNoEmp.SelectCount = 0;
            this.bpNoEmp.SetDefaultValue = false;
            this.bpNoEmp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNoEmp.Size = new System.Drawing.Size(161, 21);
            this.bpNoEmp.TabIndex = 5;
            this.bpNoEmp.Text = "bpCodeTextBox3";
            this.bpNoEmp.Click += new System.EventHandler(this.bpNoEmp_Click);
            this.bpNoEmp.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Contorl_QueryBefore);
            this.bpNoEmp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpCdPartner
            // 
            this.bpCdPartner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpCdPartner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpCdPartner.ButtonImage")));
            this.bpCdPartner.ChildMode = "";
            this.bpCdPartner.CodeName = "";
            this.bpCdPartner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpCdPartner.CodeValue = "";
            this.bpCdPartner.ComboCheck = true;
            this.bpCdPartner.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpCdPartner.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpCdPartner.Location = new System.Drawing.Point(616, 3);
            this.bpCdPartner.Name = "bpCdPartner";
            this.bpCdPartner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpCdPartner.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpCdPartner.SearchCode = true;
            this.bpCdPartner.SelectCount = 0;
            this.bpCdPartner.SetDefaultValue = false;
            this.bpCdPartner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpCdPartner.Size = new System.Drawing.Size(161, 21);
            this.bpCdPartner.TabIndex = 2;
            this.bpCdPartner.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Contorl_QueryBefore);
            this.bpCdPartner.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // m_mskDtGir
            // 
            this.m_mskDtGir.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskDtGir.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskDtGir.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskDtGir.Location = new System.Drawing.Point(366, 3);
            this.m_mskDtGir.Mask = "####/##/##";
            this.m_mskDtGir.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskDtGir.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskDtGir.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskDtGir.Modified = false;
            this.m_mskDtGir.Name = "m_mskDtGir";
            this.m_mskDtGir.PaddingCharacter = '_';
            this.m_mskDtGir.PassivePromptCharacter = '_';
            this.m_mskDtGir.PromptCharacter = '_';
            this.m_mskDtGir.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskDtGir.ShowToDay = true;
            this.m_mskDtGir.ShowTodayCircle = true;
            this.m_mskDtGir.ShowUpDown = false;
            this.m_mskDtGir.Size = new System.Drawing.Size(86, 21);
            this.m_mskDtGir.SunDayColor = System.Drawing.Color.Red;
            this.m_mskDtGir.TabIndex = 1;
            this.m_mskDtGir.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskDtGir.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskDtGir.ToDayColor = System.Drawing.Color.Red;
            this.m_mskDtGir.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskDtGir.UseKeyF3 = false;
            this.m_mskDtGir.Value = new System.DateTime(((long)(0)));
            this.m_mskDtGir.Validated += new System.EventHandler(this.m_mskDtGir_Validated);
            // 
            // m_cboTpBusi
            // 
            this.m_cboTpBusi.AutoDropDown = true;
            this.m_cboTpBusi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboTpBusi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboTpBusi.Location = new System.Drawing.Point(84, 57);
            this.m_cboTpBusi.Name = "m_cboTpBusi";
            this.m_cboTpBusi.ShowCheckBox = false;
            this.m_cboTpBusi.Size = new System.Drawing.Size(122, 20);
            this.m_cboTpBusi.TabIndex = 6;
            this.m_cboTpBusi.UseKeyEnter = false;
            this.m_cboTpBusi.UseKeyF3 = false;
            this.m_cboTpBusi.SelectionChangeCommitted += new System.EventHandler(this.OnComboBoxSelectionChangeCommitted);
            this.m_cboTpBusi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // m_cboLcCode
            // 
            this.m_cboLcCode.AutoDropDown = true;
            this.m_cboLcCode.BackColor = System.Drawing.Color.White;
            this.m_cboLcCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboLcCode.Location = new System.Drawing.Point(207, 57);
            this.m_cboLcCode.Name = "m_cboLcCode";
            this.m_cboLcCode.ShowCheckBox = false;
            this.m_cboLcCode.Size = new System.Drawing.Size(72, 20);
            this.m_cboLcCode.TabIndex = 7;
            this.m_cboLcCode.UseKeyEnter = false;
            this.m_cboLcCode.UseKeyF3 = false;
            this.m_cboLcCode.SelectionChangeCommitted += new System.EventHandler(this.m_cboLcCode_SelectionChangeCommitted);
            this.m_cboLcCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel10.BackgroundImage")));
            this.panel10.Location = new System.Drawing.Point(5, 52);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(777, 1);
            this.panel10.TabIndex = 30;
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Location = new System.Drawing.Point(5, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(777, 1);
            this.panel9.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel8.Controls.Add(this.m_lblCdPartner);
            this.panel8.Controls.Add(this.m_lblEmpGir);
            this.panel8.Location = new System.Drawing.Point(533, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(80, 52);
            this.panel8.TabIndex = 2;
            // 
            // m_lblCdPartner
            // 
            this.m_lblCdPartner.Location = new System.Drawing.Point(3, 5);
            this.m_lblCdPartner.Name = "m_lblCdPartner";
            this.m_lblCdPartner.Resizeble = true;
            this.m_lblCdPartner.Size = new System.Drawing.Size(75, 18);
            this.m_lblCdPartner.TabIndex = 0;
            this.m_lblCdPartner.Tag = "CD_PARTNER";
            this.m_lblCdPartner.Text = "거래처";
            this.m_lblCdPartner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblEmpGir
            // 
            this.m_lblEmpGir.Location = new System.Drawing.Point(3, 30);
            this.m_lblEmpGir.Name = "m_lblEmpGir";
            this.m_lblEmpGir.Resizeble = true;
            this.m_lblEmpGir.Size = new System.Drawing.Size(75, 18);
            this.m_lblEmpGir.TabIndex = 0;
            this.m_lblEmpGir.Tag = "EMP_GIR";
            this.m_lblEmpGir.Text = "담당자";
            this.m_lblEmpGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.labelExt1);
            this.panel7.Controls.Add(this.m_lblDcRmk);
            this.panel7.Controls.Add(this.m_lblDtGir);
            this.panel7.Location = new System.Drawing.Point(281, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(80, 76);
            this.panel7.TabIndex = 0;
            // 
            // m_lblDcRmk
            // 
            this.m_lblDcRmk.Location = new System.Drawing.Point(3, 55);
            this.m_lblDcRmk.Name = "m_lblDcRmk";
            this.m_lblDcRmk.Resizeble = true;
            this.m_lblDcRmk.Size = new System.Drawing.Size(75, 18);
            this.m_lblDcRmk.TabIndex = 1;
            this.m_lblDcRmk.Tag = "DC_RMK";
            this.m_lblDcRmk.Text = "비고";
            this.m_lblDcRmk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDtGir
            // 
            this.m_lblDtGir.Location = new System.Drawing.Point(3, 5);
            this.m_lblDtGir.Name = "m_lblDtGir";
            this.m_lblDtGir.Resizeble = true;
            this.m_lblDtGir.Size = new System.Drawing.Size(75, 18);
            this.m_lblDtGir.TabIndex = 0;
            this.m_lblDtGir.Tag = "DT_GIR";
            this.m_lblDtGir.Text = "의뢰일자";
            this.m_lblDtGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.m_lblNoGir);
            this.panel6.Controls.Add(this.m_lblPlantGir);
            this.panel6.Controls.Add(this.m_lblTpGi);
            this.panel6.Location = new System.Drawing.Point(1, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(80, 76);
            this.panel6.TabIndex = 0;
            // 
            // m_lblPlantGir
            // 
            this.m_lblPlantGir.Location = new System.Drawing.Point(3, 30);
            this.m_lblPlantGir.Name = "m_lblPlantGir";
            this.m_lblPlantGir.Resizeble = true;
            this.m_lblPlantGir.Size = new System.Drawing.Size(75, 18);
            this.m_lblPlantGir.TabIndex = 0;
            this.m_lblPlantGir.Tag = "CD_PLANT";
            this.m_lblPlantGir.Text = "공장";
            this.m_lblPlantGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblTpGi
            // 
            this.m_lblTpGi.Location = new System.Drawing.Point(3, 57);
            this.m_lblTpGi.Name = "m_lblTpGi";
            this.m_lblTpGi.Resizeble = true;
            this.m_lblTpGi.Size = new System.Drawing.Size(75, 18);
            this.m_lblTpGi.TabIndex = 0;
            this.m_lblTpGi.Tag = "TP_BUSI";
            this.m_lblTpGi.Text = "거래구분";
            this.m_lblTpGi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtNoGir
            // 
            this.m_txtNoGir.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtNoGir.Location = new System.Drawing.Point(85, 3);
            this.m_txtNoGir.Name = "m_txtNoGir";
            this.m_txtNoGir.ReadOnly = true;
            this.m_txtNoGir.SelectedAllEnabled = false;
            this.m_txtNoGir.Size = new System.Drawing.Size(100, 21);
            this.m_txtNoGir.TabIndex = 0;
            this.m_txtNoGir.TabStop = false;
            this.m_txtNoGir.UseKeyEnter = false;
            this.m_txtNoGir.UseKeyF3 = false;
            // 
            // m_cboPlantGir
            // 
            this.m_cboPlantGir.AutoDropDown = true;
            this.m_cboPlantGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboPlantGir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPlantGir.Location = new System.Drawing.Point(85, 30);
            this.m_cboPlantGir.Name = "m_cboPlantGir";
            this.m_cboPlantGir.ShowCheckBox = false;
            this.m_cboPlantGir.Size = new System.Drawing.Size(194, 20);
            this.m_cboPlantGir.TabIndex = 3;
            this.m_cboPlantGir.UseKeyEnter = false;
            this.m_cboPlantGir.UseKeyF3 = false;
            this.m_cboPlantGir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // m_txtDcRmk
            // 
            this.m_txtDcRmk.Location = new System.Drawing.Point(366, 55);
            this.m_txtDcRmk.Name = "m_txtDcRmk";
            this.m_txtDcRmk.SelectedAllEnabled = false;
            this.m_txtDcRmk.Size = new System.Drawing.Size(411, 21);
            this.m_txtDcRmk.TabIndex = 9;
            this.m_txtDcRmk.UseKeyEnter = false;
            this.m_txtDcRmk.UseKeyF3 = false;
            this.m_txtDcRmk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // m_btnApplySo
            // 
            this.m_btnApplySo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnApplySo.BackColor = System.Drawing.Color.White;
            this.m_btnApplySo.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnApplySo.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnApplySo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnApplySo.Location = new System.Drawing.Point(710, 3);
            this.m_btnApplySo.Name = "m_btnApplySo";
            this.m_btnApplySo.Size = new System.Drawing.Size(80, 24);
            this.m_btnApplySo.TabIndex = 0;
            this.m_btnApplySo.TabStop = false;
            this.m_btnApplySo.Text = "요청적용";
            this.m_btnApplySo.UseVisualStyleBackColor = false;
            this.m_btnApplySo.Click += new System.EventHandler(this.OnSubOpen);
            // 
            // m_pnlGirL
            // 
            this.m_pnlGirL.Controls.Add(this._flex);
            this.m_pnlGirL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGirL.Location = new System.Drawing.Point(3, 149);
            this.m_pnlGirL.Name = "m_pnlGirL";
            this.m_pnlGirL.Size = new System.Drawing.Size(787, 409);
            this.m_pnlGirL.TabIndex = 11;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.IsDataChanged = false;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(0, 0);
            this._flex.Name = "_flex";
            this._flex.RowFilter = "";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(787, 409);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete.BackColor = System.Drawing.Color.White;
            this.m_btnDelete.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnDelete.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnDelete.Enabled = false;
            this.m_btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDelete.Location = new System.Drawing.Point(718, 0);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(65, 24);
            this.m_btnDelete.TabIndex = 0;
            this.m_btnDelete.TabStop = false;
            this.m_btnDelete.Text = "삭제";
            this.m_btnDelete.UseVisualStyleBackColor = false;
            this.m_btnDelete.Click += new System.EventHandler(this.m_btnDelete_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_btnApplySo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlGirL, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // m_cboFgGirq
            // 
            this.m_cboFgGirq.AutoDropDown = true;
            this.m_cboFgGirq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboFgGirq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFgGirq.Location = new System.Drawing.Point(483, 4);
            this.m_cboFgGirq.Name = "m_cboFgGirq";
            this.m_cboFgGirq.ShowCheckBox = false;
            this.m_cboFgGirq.Size = new System.Drawing.Size(166, 20);
            this.m_cboFgGirq.TabIndex = 8;
            this.m_cboFgGirq.UseKeyEnter = false;
            this.m_cboFgGirq.UseKeyF3 = false;
            this.m_cboFgGirq.Visible = false;
            this.m_cboFgGirq.SelectionChangeCommitted += new System.EventHandler(this.OnComboBoxSelectionChangeCommitted);
            this.m_cboFgGirq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_btnDelete);
            this.panel2.Controls.Add(this.m_cboFgGirq);
            this.panel2.Location = new System.Drawing.Point(3, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(787, 24);
            this.panel2.TabIndex = 32;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(3, 29);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(75, 18);
            this.labelExt1.TabIndex = 4;
            this.labelExt1.Tag = "TP_GI";
            this.labelExt1.Text = "출하형태";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_SA_GIRR_REG
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_SA_GIRR_REG";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskDtGir)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.m_pnlGirL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region -> 소멸자
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

		#region -> InitializeDefaultDataTable

		/// <summary>
		/// 데이터 테이블을 초기화 한다.
		/// </summary>
		private void InitializeDefaultDataTable(string ps_isLine)
		{
			//=================================================================//
			//=============== 출고의뢰 HEAD(SA_GIRH)테이블 ====================//
			//=================================================================//
			this.g_dtGirH = new DataTable();

			this.g_dtGirH.Columns.Add(new DataColumn("NO_GIR"));    // 납품의뢰번호
			this.g_dtGirH.Columns.Add(new DataColumn("CD_COMPANY"));// 회사코드
			this.g_dtGirH.Columns.Add(new DataColumn("DT_GIR"));    // 의뢰일자
			this.g_dtGirH.Columns.Add(new DataColumn("NO_EMP"));    // 출하의뢰자
			this.g_dtGirH.Columns.Add(new DataColumn("TP_BUSI"));   // 거래구분
			this.g_dtGirH.Columns.Add(new DataColumn("CD_PLANT"));  // 출하의뢰공장
			//this.g_dtGirH.Columns.Add(new DataColumn("CD_SL"));     // 출하창고
            this.g_dtGirH.Columns.Add(new DataColumn("TP_GI"));     // 출하형태
			this.g_dtGirH.Columns.Add(new DataColumn("CD_PARTNER"));// 거래처
			this.g_dtGirH.Columns.Add(new DataColumn("GI_PARTNER"));// 납품처
			this.g_dtGirH.Columns.Add(new DataColumn("STA_GIR"));   // 의뢰상태
			this.g_dtGirH.Columns.Add(new DataColumn("YN_RETURN")); // 반품유무
			this.g_dtGirH.Columns.Add(new DataColumn("DC_RMK"));    // 비고
			this.g_dtGirH.Columns.Add(new DataColumn("DTS_INSERT"));// 등록자
			this.g_dtGirH.Columns.Add(new DataColumn("ID_INSERT")); // 등록일
			this.g_dtGirH.Columns.Add(new DataColumn("DTS_UPDATE"));// 수정자
			this.g_dtGirH.Columns.Add(new DataColumn("ID_UPDATE")); // 수정일자

			DataRow ldr_new = this.g_dtGirH.NewRow();
			this.g_dtGirH.Rows.Add(ldr_new);
			//==================================================================//

			if(ps_isLine == "Y")
			{
				//===================================================================================//
				//=========================== 출고의로LINE(SA_GIRL) =================================//
				//===================================================================================//
				this.g_dtGirL = new DataTable();

				this.g_dtGirL.Columns.Add(new DataColumn("CHK"));               // 0.선택(S)
				this.g_dtGirL.Columns.Add(new DataColumn("NO_GIR"));            // 1.고객납품의뢰번호
				this.g_dtGirL.Columns.Add(new DataColumn("CD_COMPANY"));        // 2.회사코드
				this.g_dtGirL.Columns.Add(new DataColumn("SEQ_GIR"));           // 3.의뢰항번
				this.g_dtGirL.Columns.Add(new DataColumn("SO_PARTNER"));        // 4.거래처
				this.g_dtGirL.Columns.Add(new DataColumn("GI_PARTNER"));        // 5.납품처
				this.g_dtGirL.Columns.Add(new DataColumn("GI_PARTNERNM"));      // 5.납품처명
				this.g_dtGirL.Columns.Add(new DataColumn("CD_PARTNER"));        // 6.매출처
				this.g_dtGirL.Columns.Add(new DataColumn("BILL_PARTNER"));      // 7.수금처
				this.g_dtGirL.Columns.Add(new DataColumn("CD_ITEM"));           // 8.품목코드
				this.g_dtGirL.Columns.Add(new DataColumn("NM_ITEM"));           // 9.품목명
				this.g_dtGirL.Columns.Add(new DataColumn("TP_ITEM"));           // 10.품목유형
				this.g_dtGirL.Columns.Add(new DataColumn("UNIT"));              // 11.수주단위(출하의뢰)
				this.g_dtGirL.Columns.Add(new DataColumn("DT_DUEDATE"));        // 12.납기일
				this.g_dtGirL.Columns.Add(new DataColumn("DT_REQGI"));          // 13.출하예정일
				this.g_dtGirL.Columns.Add(new DataColumn("YN_INSPECT"));        // 14.검사유무
				this.g_dtGirL.Columns.Add(new DataColumn("QT_GIR"));            // 15.수배의뢰량
				this.g_dtGirL.Columns.Add(new DataColumn("UM"));                // 16.단가
				this.g_dtGirL.Columns.Add(new DataColumn("AM_GIR"));            // 17.의뢰금액
				this.g_dtGirL.Columns.Add(new DataColumn("QT_REMAIN"));         // 18.오더잔량
				this.g_dtGirL.Columns.Add(new DataColumn("CD_EXCH"));           // 19.화페단위
				this.g_dtGirL.Columns.Add(new DataColumn("RT_EXCH"));           // 20.환율
				this.g_dtGirL.Columns.Add(new DataColumn("AM_GIRAMT"));         // 21.의뢰금액(원화)
				this.g_dtGirL.Columns.Add(new DataColumn("NO_PROJECT"));        // 22.프로젝트코드
				this.g_dtGirL.Columns.Add(new DataColumn("CD_SALEGRP"));        // 23.영업그룹
				this.g_dtGirL.Columns.Add(new DataColumn("NO_EMP"));            // 24.사번
				this.g_dtGirL.Columns.Add(new DataColumn("STA_GIR"));           // 25.처리상태
				this.g_dtGirL.Columns.Add(new DataColumn("STA_GIRNM"));         // 26.처리상태명
				this.g_dtGirL.Columns.Add(new DataColumn("NO_SO"));             // 27.수주번호
				this.g_dtGirL.Columns.Add(new DataColumn("SEQ_SO"));            // 28.수주항번
				this.g_dtGirL.Columns.Add(new DataColumn("QT_GI"));             // 29.출하량
				this.g_dtGirL.Columns.Add(new DataColumn("TP_GI"));             // 30.출하형태
				this.g_dtGirL.Columns.Add(new DataColumn("TP_IV"));             // 31.매출형태
				this.g_dtGirL.Columns.Add(new DataColumn("TP_BUSI"));           // 32.매출형태
				this.g_dtGirL.Columns.Add(new DataColumn("CONF"));              // 33.확정유무
				this.g_dtGirL.Columns.Add(new DataColumn("GIR"));               // 34.의뢰유무
				this.g_dtGirL.Columns.Add(new DataColumn("GI"));                // 35.출고유무
				this.g_dtGirL.Columns.Add(new DataColumn("IV"));                // 36.매출유부
				this.g_dtGirL.Columns.Add(new DataColumn("TRADE"));             // 37.수출유무
				this.g_dtGirL.Columns.Add(new DataColumn("CM"));                // 38.위탁유무
				this.g_dtGirL.Columns.Add(new DataColumn("RET"));               // 39.반품유무
				this.g_dtGirL.Columns.Add(new DataColumn("SUBCONT"));           // 40.외주유무
				this.g_dtGirL.Columns.Add(new DataColumn("QT_INSPECT"));        // 41.검사의뢰수량
				this.g_dtGirL.Columns.Add(new DataColumn("QT_PASS"));           // 42.합격수
				this.g_dtGirL.Columns.Add(new DataColumn("QT_REJECT"));         // 43.불합격
				this.g_dtGirL.Columns.Add(new DataColumn("YN_DECISION"));       // 44.판정여부
				this.g_dtGirL.Columns.Add(new DataColumn("TP_VAT"));            // 45.VAT구분
				this.g_dtGirL.Columns.Add(new DataColumn("RT_VAT"));            // 46.부가세율
				this.g_dtGirL.Columns.Add(new DataColumn("QT_GIR_IM"));         // 47.출하의뢰량
				this.g_dtGirL.Columns.Add(new DataColumn("QT_IM"));             // 48.재고수량
				this.g_dtGirL.Columns.Add(new DataColumn("FG_TAXP"));           // 49.
				this.g_dtGirL.Columns.Add(new DataColumn("AM_VAT"));            // 50.부가세
				this.g_dtGirL.Columns.Add(new DataColumn("CD_SL"));             // 51.창고
				this.g_dtGirL.Columns.Add(new DataColumn("NM_SL"));             // 52.창고명
				this.g_dtGirL.Columns.Add(new DataColumn("NO_IO_MGMT"));        // 53.관련수불번호
				this.g_dtGirL.Columns.Add(new DataColumn("NO_IOLINE_MGMT"));    // 54.관련수불항번
				this.g_dtGirL.Columns.Add(new DataColumn("NO_SOLINE_MGMT"));    // 55.관련수주번호
				this.g_dtGirL.Columns.Add(new DataColumn("NO_SO_MGMT"));        // 56.관련수주항번

				this.g_dtGirL.Columns.Add(new DataColumn("QT_SO"));             // 57.QT_SO
				this.g_dtGirL.Columns.Add(new DataColumn("NO_LC"));             // 58.L/C번호
				this.g_dtGirL.Columns.Add(new DataColumn("SEQ_LC"));            // 59.L/C항번
				this.g_dtGirL.Columns.Add(new DataColumn("QT_LC"));             // 60.L/C수량
				this.g_dtGirL.Columns.Add(new DataColumn("STND_ITEM"));         // 61.규격
				this.g_dtGirL.Columns.Add(new DataColumn("UNIT_IM"));           // 62.재고단위(수주LINE)
				this.g_dtGirL.Columns.Add(new DataColumn("AM_EXGIR"));

				this.g_dtGirL.Columns.Add(new DataColumn("ID_INSERT"));         // 63.등록자
				this.g_dtGirL.Columns.Add(new DataColumn("DTS_INSERT"));        // 64.등록일
				this.g_dtGirL.Columns.Add(new DataColumn("ID_UPDATE"));         // 65.수정자
				this.g_dtGirL.Columns.Add(new DataColumn("DTS_UPDATE"));        // 66.수정일자

				this.g_dtGirL.Columns.Add(new DataColumn("NM_SO"));				// 66.수정일자
				this.g_dtGirL.Columns.Add(new DataColumn("AUTO_TYPE")); 
				this.g_dtGirL.Columns.Add(new DataColumn("DC_RMK")); 
           
				this.g_dtGirL.Columns.Add(new DataColumn("CD_PLANT")); 

				DataRow ldrL_new = this.g_dtGirL.NewRow();
			}
		}

		#endregion

		#region -> Page_Load

		/// <summary>
		/// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
		/// </summary>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				this.Enabled   = false;

				// 페이지를 로드하는 중입니다.
				this.ShowStatusBarMessage(1);
				this.SetProgressBarValue(100, 10);			
			
				// 그리드 컨트롤을 초기화 한다.
				InitGrid();
				this.SetProgressBarValue(100, 30);

				InitControl();
				this.SetProgressBarValue(100, 70);

				Application.DoEvents();			
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
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
					case "CHK":			// S
						temp = temp + " + " + "S";
						break;
					case "CD_ITEM":		// 품번
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_ITEM");
						break;
					case "NM_ITEM":		// 품명
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NM_ITEM");
						break;
					case "STND_ITEM":	// 규격
						temp = temp + " + " + this.GetDataDictionaryItem("SA","SPEC_ITEM");
						break;
					case "UNIT":		// 단위
						temp = temp + " + " + this.GetDataDictionaryItem("SA","UNIT");
						break;
					case "DT_DUEDATE":	// 납기요청일
						temp = temp + " + " + this.GetDataDictionaryItem("SA","DT_DUEDATE");
						break;
					case "DT_REQGI":	// 출하예정일
						temp = temp + " + " + this.GetDataDictionaryItem("SA","DT_REQGI");
						break;
					case "YN_INSPECT":	// 검사유무
						temp = temp + " + " + this.GetDataDictionaryItem("SA","YN_INSPECT");
						break;
					case "NM_SL":		// 창고명
						temp = temp + " + " + this.GetDataDictionaryItem("SA","SL_GIR");
						break;
					case "CD_SL":		// 창고
						temp = temp + " + " + this.GetDataDictionaryItem("SA","SL_GIR");
						break;
					case "QT_GIR":		// 수배의뢰량
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_GIR");
						break;
					case "CD_EXCH":		// 환종
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_EXCH");
						break;
					case "UM":			// 환종단가
						temp = temp + " + " + this.GetDataDictionaryItem("SA","PRICE");
						break;
					case "AM_GIR":		// 금액
						temp = temp + " + " + this.GetDataDictionaryItem("SA","AMT");
						break;
					case "AM_GIRAMT":	// 원화금액
						temp = temp + " + " + this.GetDataDictionaryItem("SA","AM_WON");
						break;
					case "AM_VAT":		// 부가세
						temp = temp + " + " + this.GetDataDictionaryItem("SA","AM_VAT");
						break;
					case "UNIT_IM":		// 재고단위
						temp = temp + " + " + this.GetDataDictionaryItem("SA","UNIT_IM");
						break;
					case "QT_IM":		// 재고수량
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_IM");
						break;
					case "STA_GIR":		// 수주상태
						temp = temp + " + " + this.GetDataDictionaryItem("SA","TP_SO");
						break;
					case "GI_PARTNERNM":// 납품처
						temp = temp + " + " + this.GetDataDictionaryItem("SA","GI_PARTNER");
						break;
					case "NO_PROJECT":	// 프로젝트
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NO_PROJECT");
						break;
					case "NO_IO_MGMT":	// 관련수불번호
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NO_IO_MGMT");
						break;
					case "NO_SO_MGMT":		// 관련수주번호
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NO_SO");
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

		#region -> InitGrid

		private void InitGrid()
		{
			Application.DoEvents();
			
			_flex.Redraw = false;

			_flex.Rows.Count = 1;
			_flex.Rows.Fixed = 1;
			_flex.Cols.Count = 25;
			_flex.Cols.Fixed = 1;
			_flex.Rows.DefaultSize = 20;
			
			_flex.Cols[0].Width = 50;

			// 1.S
			_flex.Cols[1].Name = "CHK";
			_flex.Cols[1].DataType = typeof(string);
			_flex.Cols[1].Width = 40;
			_flex.Cols[1].Format = "Y;N";
			_flex.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			
			// 2.품번
			_flex.Cols[2].Name = "CD_ITEM";
			_flex.Cols[2].DataType = typeof(string);
			_flex.Cols[2].Width = 90;
			_flex.SetColMaxLength("CD_ITEM",20);
			_flex.Cols[2].AllowEditing = false;

			// 3.품명
			_flex.Cols[3].Name = "NM_ITEM";
			_flex.Cols[3].DataType = typeof(string);
			_flex.Cols[3].Width = 100;
			_flex.Cols[3].AllowEditing = false;

			// 4.규격
			_flex.Cols[4].Name = "STND_ITEM";
			_flex.Cols[4].DataType = typeof(string);
			_flex.Cols[4].Width = 60;
			_flex.Cols[4].AllowEditing = false;

			// 5.단위
			_flex.Cols[5].Name = "UNIT";
			_flex.Cols[5].DataType = typeof(string);
			_flex.Cols[5].Width = 45;
			_flex.Cols[5].AllowEditing = false;
			_flex.SetColMaxLength("UNIT",3);

			// 6.납기요청일
			_flex.Cols[6].Name = "DT_DUEDATE";
			_flex.Cols[6].DataType = typeof(string);
			_flex.Cols[6].Width = 80;
			_flex.Cols[6].EditMask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			_flex.Cols[6].Format = _flex.Cols[6].EditMask;
			_flex.Cols[6].AllowEditing = false;
			_flex.SetStringFormatCol("DT_DUEDATE");
			_flex.SetNoMaskSaveCol("DT_DUEDATE");

			// 7.출하예정일
			_flex.Cols[7].Name = "DT_REQGI";
			_flex.Cols[7].DataType = typeof(string);
			_flex.Cols[7].Width = 80;
			_flex.Cols[7].EditMask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			_flex.Cols[7].Format = _flex.Cols[6].EditMask;
			_flex.Cols[7].AllowEditing = false;
			_flex.SetStringFormatCol("DT_REQGI");
			_flex.SetNoMaskSaveCol("DT_REQGI");

			// 8.검사유무
			_flex.Cols[8].Name = "YN_INSPECT";
			_flex.Cols[8].DataType = typeof(string);
			_flex.Cols[8].Width = 70;
			_flex.Cols[8].Format = "Y;N";
			_flex.Cols[8].AllowEditing = false;
			_flex.Cols[8].ImageAlign = ImageAlignEnum.CenterCenter;

			// 9.창고명
			_flex.Cols[9].Name = "NM_SL";
			_flex.Cols[9].DataType = typeof(string);
			_flex.Cols[9].Width = 80;
			_flex.Cols[9].AllowEditing = false;
			_flex.Cols[9].ImageAlign = ImageAlignEnum.Hide;

			// 10.창고
			_flex.Cols[10].Name = "CD_SL";
			_flex.Cols[10].DataType = typeof(string);
			_flex.Cols[10].Width = 0;
			_flex.Cols[10].AllowEditing = false;

            _flex.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, "CD_SL", "NM_SL");
            _flex.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_ITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "UNIT", "STND_ITEM" });

			// 11.수배의뢰량
			_flex.Cols[11].Name = "QT_GIR";
			_flex.Cols[11].DataType = typeof(decimal);
			_flex.Cols[11].Width = 90;
			this.SetFormat(_flex.Cols[11], DataDictionaryTypes.SA, FormatTpType.QUANTITY, FormatFgType.INSERT);

			// 12.환종
			_flex.Cols[12].Name = "CD_EXCH";
			_flex.Cols[12].DataType = typeof(string);
			_flex.Cols[12].Width = 90;
			_flex.Cols[12].AllowEditing = false;

			// 13.환종단가
			_flex.Cols[13].Name = "UM";
			_flex.Cols[13].DataType = typeof(decimal);
			_flex.Cols[13].Width = 90;
			this.SetFormat(_flex.Cols[13], DataDictionaryTypes.SA, FormatTpType.UNIT_COST, FormatFgType.INSERT);

			// 14.금액
			_flex.Cols[14].Name = "AM_GIR";
			_flex.Cols[14].DataType = typeof(decimal);
			_flex.Cols[14].Width = 90;
			this.SetFormat(_flex.Cols[14], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);

			// 15.원화금액
			_flex.Cols[15].Name = "AM_GIRAMT";
			_flex.Cols[15].DataType = typeof(decimal);
			_flex.Cols[15].Width = 90;
			this.SetFormat(_flex.Cols[15], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);

			// 16.부가세
			_flex.Cols[16].Name = "AM_VAT";
			_flex.Cols[16].DataType = typeof(decimal);
			_flex.Cols[16].Width = 100;
			this.SetFormat(_flex.Cols[16], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);

			// 17.재고단위
			_flex.Cols[17].Name = "UNIT_IM";
			_flex.Cols[17].DataType = typeof(string);
			_flex.Cols[17].Width = 90;
			_flex.Cols[17].AllowEditing = false;

			// 18.재고수량
			_flex.Cols[18].Name = "QT_IM";
			_flex.Cols[18].DataType = typeof(decimal);
			_flex.Cols[18].Width = 90;
			this.SetFormat(_flex.Cols[18], DataDictionaryTypes.SA, FormatTpType.QUANTITY, FormatFgType.INSERT);

			// 19.수주상태
			_flex.Cols[19].Name = "STA_GIR";
			_flex.Cols[19].DataType = typeof(string);
			_flex.Cols[19].Width = 90;

			// 20.납품처
			_flex.Cols[20].Name = "GI_PARTNERNM";
			_flex.Cols[20].DataType = typeof(string);
			_flex.Cols[20].Width = 90;
			_flex.Cols[20].AllowEditing = false;

			// 21.프로젝트
			_flex.Cols[21].Name = "NO_PROJECT";
			_flex.Cols[21].DataType = typeof(string);
			_flex.Cols[21].Width = 90;
			_flex.Cols[21].AllowEditing = false;

			// 22.관련수불번호
			_flex.Cols[22].Name = "NO_IO_MGMT";
			_flex.Cols[22].DataType = typeof(string);
			_flex.Cols[22].Width = 110;
			_flex.Cols[22].AllowEditing = false;

			// 23.관련수주번호
			_flex.Cols[23].Name = "NO_SO_MGMT";
			_flex.Cols[23].DataType = typeof(string);
			_flex.Cols[23].Width = 110;
			_flex.Cols[23].AllowEditing = false;

			// 24.창고명
			_flex.Cols[24].Name = "NM_SL";
			_flex.Cols[24].DataType = typeof(string);
			_flex.Cols[24].Width = 80;
			_flex.Cols[24].Visible = false;
			_flex.Cols[24].AllowEditing = false;
			_flex.Cols[24].ImageAlign = ImageAlignEnum.Hide;

			_flex.AllowSorting = AllowSortingEnum.MultiColumn;
			_flex.NewRowEditable = false;
			_flex.EnterKeyAddRow = false;

			_flex.SumPosition = SumPositionEnum.Top;
			_flex.GridStyle = GridStyleEnum.Green;

			_flex.SetExceptSumCol("QT_GIR");
			_flex.SetExceptSumCol("UM");
			_flex.SetExceptSumCol("QT_IM");

			this.SetUserGrid(_flex);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flex.Cols.Count-1; i++)
				_flex[0, i] = GetDDItem(_flex.Cols[i].Name);

			_flex.Redraw = true;

			// 그리드 이벤트 선언
			_flex.ValidateEdit		+= new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flex_ValidateEdit);
			_flex.AfterDataRefresh	+= new System.ComponentModel.ListChangedEventHandler(_flex_AfterDataRefresh);
			
		}

 

		#endregion

		#region -> InitControl

		private void InitControl()
		{
			try
			{
				m_mskDtGir.Text	= this.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);

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
										((Duzon.Common.Controls.LabelExt)ctrls).Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)((Duzon.Common.Controls.LabelExt)ctrls).Tag);
								}
							}
						}
					}
				}
				m_btnApplySo.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, "APPLY_RET");

				DataSet g_dsCombo = this.GetComboData("N;PU_C000016","N;MA_PLANT","N;TR_IM00006", "N;PU_C000004","N;PU_C000027");

				// 거래구분
				m_cboTpBusi.DataSource		= new DataView(g_dsCombo.Tables[0], "CODE IN ('001', '002', '003')","CODE", DataViewRowState.CurrentRows);
				m_cboTpBusi.DisplayMember	= "NAME";
				m_cboTpBusi.ValueMember		= "CODE";

				//공장
				m_cboPlantGir.DataSource	= g_dsCombo.Tables[1];
				m_cboPlantGir.DisplayMember = "NAME";
				m_cboPlantGir.ValueMember	= "CODE";

				m_cboLcCode.DataSource		= g_dsCombo.Tables[2];
				m_cboLcCode.DisplayMember	= "NAME";
				m_cboLcCode.ValueMember		= "CODE";

				//의뢰구분
				m_cboFgGirq.DataSource		= new DataView(g_dsCombo.Tables[4], "CODE IN ('Y', 'C')", "CODE", DataViewRowState.CurrentRows);
				m_cboFgGirq.DisplayMember	= "NAME";
				m_cboFgGirq.ValueMember		= "CODE";
				m_cboFgGirq.SelectedValue	= "Y";

				//환종
				this._flex.SetDataMap("CD_EXCH",g_dsCombo.Tables[3], "CODE","NAME");

				string ls_date = MainFrameInterface.GetStringToday;
			
				g_dtGirH.Rows[0]["CD_COMPANY"]	= MainFrameInterface.LoginInfo.CompanyCode;
				g_dtGirH.Rows[0]["TP_BUSI"]		= m_cboTpBusi.SelectedValue;
				g_dtGirH.Rows[0]["CD_PLANT"]	= m_cboPlantGir.SelectedValue;
				g_dtGirH.Rows[0]["DT_GIR"]		= m_mskDtGir.Text = ls_date;
				g_dtGirH.Rows[0]["NO_EMP"]		= bpNoEmp.CodeValue = LoginInfo.EmployeeNo;
				g_dtGirH.Rows[0]["STA_GIR"]		= "O";
			
				bpNoEmp.CodeValue = LoginInfo.EmployeeNo;
				bpNoEmp.CodeName  = LoginInfo.EmployeeName;
				//			bpNoEmp.IsConfirmed = true;
				anm_dept = LoginInfo.DeptName;
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
				
                //this.m_lblTitle.Visible = true;
                //this.m_lblTitle.Text	= this.GetDataDictionaryItem(DataDictionaryTypes.SA, "P_SA_GIRR_REG");
                //this.m_lblTitle.Show();
                //SetProgressBarValue(100, 100);
                //Application.DoEvents();
			
				this.Seq_Gir = 0;
				this.m_mskDtGir.Text = this.MainFrameInterface.GetStringToday;
				this.bpNoEmp.CodeValue = this.LoginInfo.EmployeeNo;
				this.bpNoEmp.CodeName  = this.LoginInfo.EmployeeName;
				this.Enabled = true; //페이지 전체 활성

				this.m_mskDtGir.Focus();
				this.m_mskDtGir.Select();

			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{				
				this.SetToolBarButtonState(true,true, true, false,false);				
			}
		}

		#endregion

		#endregion

		#region ♣ 저장관련

		#region -> IsChanged

		private bool IsChanged(string gubun)
		{
			try
			{
				if(gubun == null)
					return _flex.IsDataChanged;

				return false;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region -> MsgAndSave

		private bool MsgAndSave(bool displayDialog, bool isExit)
		{		
			if(!IsChanged(null)) return true;
			
			bool isSaved = false;

			if(!displayDialog)								// 저장 버튼을 클릭한 경우이므로 다이알로그는 필요없음
			{
				if(IsChanged(null)) isSaved = Save();
				
				return isSaved;
			}

			DialogResult result;

			if(isExit)
			{
				result = this.ShowMessage("QY3_002");	
				if(result == DialogResult.No)
					return true;
				if(result == DialogResult.Cancel)
					return false;
			}
			else
			{
				result = this.ShowMessage("QY2_001");	
				if(result == DialogResult.No)
					return true;
			}

			Application.DoEvents();		// 대화상자 즉시 사라지게

			// "예"를 선택한 경우
			if(IsChanged(null)) isSaved = Save();

			return isSaved;
		}

		#endregion

		#region -> Check

		private bool Check()
		{			
			// 의뢰일자..
			if(m_mskDtGir.Text == "" && m_mskDtGir.Text == string.Empty)
			{
				this.ShowMessage("WK1_004", this.m_lblDtGir.Text);	
				m_mskDtGir.Focus();
				return false;
			}
			// 거래처..
			if(bpCdPartner.CodeValue == "" && bpCdPartner.CodeName == string.Empty)
			{
				this.ShowMessage("WK1_004", this.m_lblCdPartner.Text);	
				bpCdPartner.Focus();
				return false;
			}
			// 공장
			if(m_cboPlantGir.SelectedValue.ToString() == "" && m_cboPlantGir.SelectedValue.ToString() == string.Empty)
			{
				this.ShowMessage("WK1_004", this.m_lblPlantGir.Text);	
				m_cboPlantGir.Focus();
				return false;
			}
			// 담당자..
			if((bpNoEmp.CodeValue == "" && bpNoEmp.CodeName == string.Empty) && (bpNoEmp.CodeName == "" && bpNoEmp.CodeName == string.Empty))
			{
				this.ShowMessage("WK1_004", this.m_lblEmpGir.Text);	
				bpNoEmp.Focus();
				return false;
			}
			// 거래구분
			if(m_cboTpBusi.SelectedValue.ToString() == "" && m_cboTpBusi.SelectedValue.ToString() == string.Empty)
			{
				this.ShowMessage("WK1_004", this.m_lblTpGi.Text);	
				m_cboTpBusi.Focus();
				return false;
			}
            // 출하형태
            if (bpTpGi.CodeValue == "")
            {
                this.ShowMessage("WK1_004", this.labelExt1.Text);
                bpTpGi.Focus();
                return false;
            }
            //// 의뢰구분
            //if(m_cboFgGirq.SelectedValue.ToString() == "" && m_cboFgGirq.SelectedValue.ToString() == string.Empty)
            //{
            //    this.ShowMessage("WK1_004", this.m_lblFgGirq.Text);	
            //    m_cboFgGirq.Focus();
            //    return false;
            //}
			return true;
		}

		#endregion

		#region -> Save

		private bool Save()
		{
			try
			{
			
				if(!Check())		// 널값체크 및 중복값체크 등에서 문제가 발생한 경우
					return false;

				m_pnlGirL.Focus();

				//20040528
			
				if(_flex.RowState(_flex.Row).ToString() =="Added")
				{
					// 의뢰번호 채번
					no_seq = (string)this.GetSeq(LoginInfo.CompanyCode, "SA", "03", m_mskDtGir.Text.Substring(0,6).Trim());

					// SA_SOL 추가
					for(int li_i = 0; li_i < g_dtGirH.Rows.Count; li_i++)
					{				
						g_dtGirH.Rows[0]["NO_GIR"] = no_seq;	
						g_dtGirH.Rows[0]["DC_RMK"]  = m_txtDcRmk.Text;
					}

			
					// SA_SOL 추가
					for(int li_i = 0; li_i < _flex.DataView.Count; li_i++)
					{
						_flex.DataView[li_i].BeginEdit();
						_flex.DataView[li_i]["NO_GIR"] = no_seq;				
						_flex.DataView[li_i].EndEdit();
					}
				}


				DataTable dtH = g_dtGirH.GetChanges();
				DataTable dtL = _flex.DataTable.GetChanges();

				if(dtL == null && dtH == null)		// CheckM() 에서 필요없는 레코드를 삭제한 경우 null 값일 수 있다.
					return true;		

				string ls_date = MainFrameInterface.GetStringDetailToday;

				_flex.SetUpdateDate(dtL, this.LoginInfo.CompanyCode, this.MainFrameInterface.GetStringDetailToday);
			
				SpInfoCollection sic = new SpInfoCollection();

                if (dtH != null && dtH.Rows != null && dtH.Rows.Count > 0)
                {
                    SpInfo siM = new SpInfo();
                    siM.DataValue = dtH;
                    siM.SpNameInsert = "SP_SA_GIRH_SO_INSERT";
                    siM.SpNameUpdate = "SP_SA_GIRH_SO_UPDATE";

                    siM.SpParamsInsert = new String[] {	"NO_GIR","CD_COMPANY","DT_GIR","NO_EMP","TP_BUSI","CD_PLANT","TP_GI","CD_PARTNER","GI_PARTNER","STA_GIR","YN_RETURN","DC_RMK",
													  "DTS_INSERT_R","ID_INSERT_R"};
                    siM.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT_R", this.MainFrameInterface.GetStringDetailToday);
                    siM.SpParamsValues.Add(ActionState.Insert, "ID_INSERT_R", this.LoginInfo.UserID);

                    siM.SpParamsUpdate = new String[] {"NO_GIR","CD_COMPANY","DT_GIR","NO_EMP","TP_BUSI","CD_PLANT","TP_GI","CD_PARTNER","GI_PARTNER","STA_GIR","YN_RETURN","DC_RMK",
													  "DTS_UPDATE_R","ID_UPDATE_R"};
                    siM.SpParamsValues.Add(ActionState.Update, "DTS_UPDATE_R", this.MainFrameInterface.GetStringDetailToday);
                    siM.SpParamsValues.Add(ActionState.Update, "ID_UPDATE_R", this.LoginInfo.UserID);

                    sic.Add(siM);
                }
                if (dtL != null && dtL.Rows != null && dtL.Rows.Count > 0)
                {
                    SpInfo siD = new SpInfo();
                    siD.DataValue = dtL;				
                    siD.SpNameInsert = "SP_SA_GIRL_SO_INSERT";
                    siD.SpNameUpdate = "SP_SA_GIRL_SO_UPDATE";

                    siD.SpParamsInsert = new String[] {"NO_GIR","CD_COMPANY","SEQ_GIR","SO_PARTNER","GI_PARTNER",	
													  "CD_PARTNER","BILL_PARTNER","CD_ITEM","TP_ITEM","UNIT",
													  "DT_DUEDATE","DT_REQGI","YN_INSPECT","QT_GIR","UM",
													  "AM_GIR","CD_EXCH","RT_EXCH","AM_GIRAMT","NO_PROJECT",
													  "CD_SALEGRP","NO_EMP","STA_GIR","NO_SO","SEQ_SO",
													  "NO_LC","SEQ_LC","QT_GI","TP_GI","TP_IV",
													  "TP_BUSI","CONF","GIR","GI","IV",
													  "TRADE","CM","RET","SUBCONT","QT_INSPECT",
													  "QT_PASS","QT_REJECT","YN_DECISION","TP_VAT","RT_VAT",
													  "QT_GIR_IM","FG_TAXP","AM_VAT","CD_SL","QT_IM",
													  "NO_IO_MGMT","NO_IOLINE_MGMT","NO_SO_MGMT","NO_SOLINE_MGMT","AUTO_TYPE",
													  "DTS_INSERT_R","ID_INSERT_R","CD_PLANT"};//"QT_GI_IM",
                    siD.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT_R", this.MainFrameInterface.GetStringDetailToday);
                    siD.SpParamsValues.Add(ActionState.Insert, "ID_INSERT_R", this.LoginInfo.UserID);

                    siD.SpParamsUpdate = new String[] {"NO_GIR","CD_COMPANY","SEQ_GIR","SO_PARTNER","GI_PARTNER",	
													  "CD_PARTNER","BILL_PARTNER","CD_ITEM","TP_ITEM","UNIT",
													  "DT_DUEDATE","DT_REQGI","YN_INSPECT","QT_GIR","UM",
													  "AM_GIR","CD_EXCH","RT_EXCH","AM_GIRAMT","NO_PROJECT",
													  "CD_SALEGRP","NO_EMP","STA_GIR","NO_SO","SEQ_SO",
													  "NO_LC","SEQ_LC","QT_GI","TP_GI","TP_IV",
													  "TP_BUSI","CONF","GIR","GI","IV",
													  "TRADE","CM","RET","SUBCONT","QT_INSPECT",
													  "QT_PASS","QT_REJECT","YN_DECISION","TP_VAT","RT_VAT",
													  "QT_GIR_IM","FG_TAXP","AM_VAT","CD_SL","QT_IM", 
													  "NO_IO_MGMT","NO_IOLINE_MGMT","NO_SO_MGMT","NO_SOLINE_MGMT","AUTO_TYPE",
													  "DTS_UPDATE_R","ID_UPDATE_R", "CD_PLANT"};
                    siD.SpParamsValues.Add(ActionState.Update, "DTS_UPDATE_R", this.MainFrameInterface.GetStringDetailToday);
                    siD.SpParamsValues.Add(ActionState.Update, "ID_UPDATE_R", this.LoginInfo.UserID);

                    sic.Add(siD);
                }

                if (sic.Count <= 0)
                    return false;

				ResultData[] rs = (ResultData[])this.Save(sic);

                if ((rs.Length == 2 && rs[0].Result && rs[1].Result) || (rs.Length == 1 && rs[0].Result))
				{							
					this.m_txtNoGir.Text = no_seq.ToString();
					g_dtGirH.AcceptChanges();
					_flex.AcceptChanges();
				
					this.m_btnDelete.Enabled = false;
				
					return true;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return false;
		}

		#endregion

		#endregion

		#region ♣ 메인버튼 이벤트 / 메소드

		#region -> DoContinue

		private bool DoContinue()
		{
			if(_flex.Editor != null)
			{
				return _flex.FinishEditing(false);
			}
			
			return true;
		}

		#endregion

		#region -> 조회조건체크

		private bool SearchCondition()
		{
			return true;
		}

		#endregion

		#region -> 조회버튼클릭

		// 브라우저의 조회 버턴이 클릭될때 처리 부분
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if(!DoContinue())
					return;

				//this.m_lblTitle.Focus();

				if(!MsgAndSave(true,false))
					return;

				object dlg = this.LoadHelpWindow("P_SA_GIR_SCH_SUB", new object[] {this.MainFrameInterface, 
																					  m_cboPlantGir.SelectedValue.ToString(), 
																					  "", //bpNm_Sl.CodeName, 
																					  "", //bpNm_Sl.CodeValue, 
																					  m_cboFgGirq.SelectedValue.ToString()});
				if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
				{
					DataRowView ldrH = (DataRowView)(((IHelpWindow)dlg).ReturnValues)[0];

					FillGirH(ldrH);	

					if(_flex.DataTable != null)
					{
						_flex.DataTable.Rows.Clear();
					
						Application.DoEvents();
						Thread.Sleep(50);
					}
							
					this.ShowStatusBarMessage(2);
					this.SetProgressBarValue(100, 10);	
					Application.DoEvents();

					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
				
					si.SpNameSelect = "SP_SA_GIRR_SELECT_L";
					si.SpParamsSelect = new Object[]{LoginInfo.CompanyCode, 
													 g_dtGirH.Rows[0]["NO_GIR"].ToString() };

					object ret = this.FillDataTable(si);
					ResultData result = (ResultData)this.FillDataTable(si);
					DataTable dt= (DataTable)result.DataValue; 

					// Detail 바인딩
					_flex.Redraw=false;
					_flex.BindingStart();
					_flex.DataSource = dt.DefaultView;//g_dsGirL.Tables[0].DefaultView;
					_flex.EmptyRowFilter();								// 처음에 아무것도 안 보이게
					_flex.BindingEnd();
					_flex.Redraw=true;

					if(!_flex.HasNormalRow)
					{
						this.SetProgressBarValue(100, 100);	
						this.ShowStatusBarMessage(0);

						// 검색된 내용이 존재하지 않습니다..
						this.ShowMessage("IK1_003");
					}
					else
					{
						this.m_btnDelete.Enabled = false;
						ToolBarDeleteButtonEnabled = true;
						m_btnApplySo.Enabled = false;

						FreeFormControlsEnabled(false);

						// 몇 건이 조회되었습니다.
						this.ShowStatusBarMessage(3, _flex.Rows.Count.ToString());
					}
				
					this.SetProgressBarValue(100, 100);	
					Application.DoEvents();
				}
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
			finally
			{				
				this.ToolBarSearchButtonEnabled = true;
				this.ToolBarAddButtonEnabled	= true;
				this.ToolBarDeleteButtonEnabled = true;				
			}
		}

		#endregion

		#region -> 추가버튼클릭

		// 브라우저의 추가 버턴이 클릭될때 처리 부분
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{		
			//this.m_lblTitle.Focus();

			try
			{
				gdt_ReqDataREQ.Clear();

				if(this.g_dtGirH.Rows.Count > 0)
				{
					SetControlState();
				}
				else
				{
					InitializeDefaultDataTable("N");
					SetControlState();
				}

				this.FreeFormControlsEnabled(true);
				
				m_cboTpBusi.SelectedValue = "001";
				OnComboBoxSelectionChangeCommitted(m_cboTpBusi, e);
				
				m_btnDelete.Enabled = true;
				ToolBarSaveButtonEnabled = true;
				m_mskDtGir.Focus();						
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> 삭제버튼클릭

		// 브라우저의 삭제 버턴이 클릭될때 처리 부분
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;

			try
			{
				m_pnlGirL.Focus();

				if(g_dtGirH.Rows[0]["NO_GIR"].ToString() == "")
					return;

				if(_flex.DataView.Count == 0)
					return;

				if(!DeleteChecked(_flex.DataView)) 
				{
					// 출하량이 존재하여 삭제할 수 없습니다.
					ShowMessage("SA_M000104", "IK1");
					return;
				}

				DialogResult ldr_result = ShowMessageBox(1003, PageName);
				if(ldr_result == DialogResult.Yes)
				{					
					//======================= SA_SOL 잔량 체크로직 ===========================//
					//DataView ldv_src = (DataView)m_grdGir.DataSource;
					DataTable ldt_updL = _flex.DataTable;
					object[] args = {ldt_updL};
				
					bool result = (bool)this.InvokeRemoteMethod("SaleShipment_NTX", "sale.CC_SA_GIR_NTX", "CC_SA_GIR_NTX.rem", "SelectSaveCheck",args);

					if(result == false)
					{
						// SA_M000110(출고의뢰수량이 수주수량을 초과할 수 없습니다.)
						ShowMessage("SA_M000110", "IK1");
						return;
					}
					//=======================================================================//

					for(int row = _flex.Rows.Count-1;row >= _flex.Rows.Fixed; row--)//(int row = _flex.DataView.Count -1; row > -1; row--)
						_flex.Rows.Remove(row);
						//_flex.RemoveItem(row);

					DataTable ldt_updL1 = _flex.GetChanges();

					_flex.Redraw=false;
					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
					si.DataValue = ldt_updL1; 								//저장할 데이터 테이블
					
					si.SpNameDelete = "SP_SA_GIRMNGH_DELETE";//"SP_SA_GIRH_DELETE";			//Delete 프로시저명

					/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/		
					si.SpParamsDelete = new string[] {"NO_GIR","CD_COMPANY"};
					
					/*브라우저의 저장메소드(Save)호출 ResultData 타입으로 리턴된다.*/	
					Duzon.Common.Util.ResultData result_sp = (Duzon.Common.Util.ResultData)this.Save(si);
					
					/*저장에 성공한경우*/
					if(result_sp.Result)
					{						
						//SetControlState();
						OnToolBarAddButtonClicked(sender, e);
						_flex.AcceptChanges();
						gdt_ReqDataREQ.Clear();

						_flex.Redraw=true;
						// 자료를 삭제하였습니다.
						this.ShowMessage("IK1_002");
					}			
				}
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
			finally
			{
				_flex.Redraw = true;					
			}
		}

		#endregion

		#region -> 저장버튼클릭

		// 브라우저의 저장 버턴이 클릭될때 처리 부분
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;			

			try
			{
				if(MsgAndSave(false, false))
				{
					this.Seq_Gir = 0;

					// 의뢰일자
					m_mskDtGir.Enabled	= true;
				
					// 거래처
					bpCdPartner.Enabled	= true;
//					m_btnCdPartner.Enabled	= true;
					
					// 출고공장
					m_cboPlantGir.Enabled = true;
					
					// 거래구분
					m_cboTpBusi.Enabled	= true;
					m_btnApplySo.Enabled = true;
					
//					this.m_btnDelete.Enabled = true;
					this.ToolBarAddButtonEnabled	= true;
					this.ToolBarDeleteButtonEnabled = true;				

					this.ShowMessage("IK1_001");
				}
				else
				{
					m_btnApplySo.Enabled = true;
				}
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}			
		}

		#endregion

		#region -> 인쇄버튼클릭

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;

			try
			{
				m_mskDtGir.Focus();

				DataTable ldt_Report = new DataTable();

				ldt_Report = _flex.DataTable;		
				
				if( ldt_Report == null || ldt_Report.Rows.Count <=0)
				{
					ShowMessage("CM_M100007");	
					return ;
				}
							
				PrintSetting(ldt_Report);
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
		}

		#endregion

		#region 출력 보조 함수 

		
		private void PrintSetting(DataTable pdt_Report)
		{
			try
			{				
				string[] code = new string[19];
				code[0] = "NO_GIR_P";
				code[1] = "NO_GIR_P1";

				code[2] = "NM_PLANT_P";
				code[3] = "NM_PLANT_P1";

				code[4] = "DT_GIR_P";
				code[5] = "DT_GIR_P1";
				
				code[6] = "FG_GIRQ_P";
				code[7] = "FG_GIRQ_P1";

				code[8] = "NM_PARTNER_P";
				code[9] = "NM_PARTNER_P1";

				code[10] = "NM_DEPT_P";
				code[11] = "NM_DEPT_P1";

				code[12] = "NM_EMP_P";			
				code[13] = "NM_EMP_P1"; 

				code[14] = "DC_RMK_P";
				code[15] = "DC_RMK_P1"; 

				code[16] = "NM_SO_P";

				code[17] = "DT_IO_P";
	
				code[18] = "R_SA_GIRMNG_101";	
				
				string[] name = new string[19];
				name[0] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "NO_GIR");
				name[1] = m_txtNoGir.Text;

				name[2] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "CD_PLANT");
				name[3] = m_cboPlantGir.Text;

				name[4] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "DT_GIR");
				name[5] = m_mskDtGir.Text;

				name[6] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "FG_GIRQ");
				name[7] = m_cboFgGirq.Text;
				
				name[8] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "CD_PARTNER");
				name[9] = bpCdPartner.CodeName;

				name[10] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "NM_DEPT");
				name[11] = anm_dept;//LoginInfo.DeptName;
			
				name[12] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "NO_EMP");
				name[13] = bpNoEmp.CodeName;

				name[14] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "DC_RMK");
				name[15] = m_txtDcRmk.Text;
				
				name[16] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "NM_SO");

				name[17] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "DT_GI");

				name[18] = this.GetDataDictionaryItem(DataDictionaryTypes.SA, "R_SA_GIRMNG_101");
			
				// 생성자에서 0은 세로, 1은 가로 출력
				Duzon.Common.Util.ReportDesigner designer = new Duzon.Common.Util.ReportDesigner(this.MainFrameInterface,0);
				
				designer.SetDataDictionary(code, name);
				
				designer.SetDataDictionary(_flex.Cols);
					
				designer.Start("R_SA_GIRMNG_1", pdt_Report);
				
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
		}
		

		
		#endregion

		#region -> 종료버튼클릭

		// 브라우저의 닫기 버턴이 클릭될때 처리 부분
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return false;

			try
			{
				if(!MsgAndSave(true,true))	// 저장이 실패하면
					return false;			// 창 닫지 않음
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
			return true;
		}

		#endregion

		#endregion

		#region ♣ 그리드 이벤트 / 메소드
		
		#region -> _flex_AfterDataRefresh

		private void _flex_AfterDataRefresh(object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			if(IsChanged(null))
				SetToolBarButtonState(true,true,true,true,true);
			else
				SetToolBarButtonState(true,true,true,false, true);

			if(!_flex.HasNormalRow)
				ToolBarDeleteButtonEnabled = false;
		}

		#endregion

		#region -> _flex_ValidateEdit
		
		private void _flex_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
		{
		
			try
			{
				switch (_flex.Col)
				{
						// 의뢰수량
					case 11 :
						object[] argu = {this.LoginInfo.CompanyCode, _flex[e.Row,"UNIT_IM"], _flex[e.Row,"UNIT"]};
						decimal qt_girim = (decimal)this.InvokeRemoteMethod("SaleOrder_NTX","sale.CC_SA_SO_NTX","CC_SA_SO_NTX.rem", "SeekUintIM", argu);

						//의뢰수량
						_flex[e.Row,"QT_GIR"] = _flex.Editor.Text;

						//출하의뢰량
						_flex[e.Row,"QT_GIR_IM"] = Convert.ToString(_flex.CDecimal(_flex.Editor.Text) * qt_girim);

						//의뢰금액(외화) = 의뢰수량 * 단가
						_flex[e.Row,"AM_GIR"] = _flex.CDecimal(_flex.Editor.Text) * _flex.CDecimal(_flex[e.Row,"UM"]);
					
						//의뢰금액(원화) = 외화금액 * 환율
						_flex[e.Row,"AM_GIRAMT"] =	_flex.CDecimal(_flex[e.Row,"AM_GIR"]) *	_flex.CDecimal(_flex[e.Row,"RT_EXCH"]);
					
						//VAT금액(원화) = 원화금액 * VAT율 / 100
						_flex[e.Row,"AM_VAT"] = System.Decimal.Truncate(_flex.CDecimal(_flex[e.Row,"AM_GIRAMT"]) * _flex.CDecimal(_flex[e.Row,"RT_VAT"]) /100);
					
						// 재고수량 = 재고수량 - 의뢰수량
						_flex[e.Row,"QT_IM"] = Convert.ToString(_flex.CDecimal(_flex.Editor.Text) * qt_girim);
					
						if(_flex[e.Row,"YN_INSPECT"].ToString() == "Y")
							_flex[e.Row,"QT_INSPECT"] = _flex.CDecimal(_flex.Editor.Text);

//						this.SetControlsBinding(_flex.DataView);
						break;

						//단가
					case 13 :
						//의뢰금액(금액) = 의뢰수량 * 단가
						_flex[e.Row,"AM_GIR"] = _flex.CDecimal(_flex[e.Row,"QT_GIR"]) * _flex.CDecimal(_flex.Editor.Text);
						
						//의뢰금액(원화금액) = 외화금액 * 환율
						_flex[e.Row,"AM_GIRAMT"] = _flex.CDecimal(_flex[e.Row,"AM_GIR"]) * _flex.CDecimal(_flex[e.Row,"RT_EXCH"]);
						
						//VAT금액(원화) = 원화금액 * VAT율 / 100
						_flex[e.Row,"AM_VAT"] = System.Decimal.Truncate(_flex.CDecimal(_flex[e.Row,"AM_GIRAMT"]) * _flex.CDecimal(_flex[e.Row,"RT_VAT"]) /100);
						
						// 금액 = 
						//ldv_src[row -1]["AM_SO"] = _flex.CDecimal(_flex.Editor.Text) * _flex.CDecimal(ldv_src[row -1]["QT_GIR"]);

//						this.SetControlsBinding(_flex.DataView);
						break;
						// 금액
					case 14 : 
						_flex[e.Row,"AM_GIR"] = _flex.Editor.Text;
						
						//의뢰금액(원화) = 외뢰금액 * 환율
						_flex[e.Row,"AM_GIRAMT"] = _flex.CDecimal(_flex.Editor.Text) * _flex.CDecimal(_flex[e.Row,"RT_EXCH"]);

						//VAT금액(원화) = 원화금액 * VAT율 / 100
						_flex[e.Row,"AM_VAT"] = System.Decimal.Truncate(_flex.CDecimal(_flex[e.Row,"AM_GIRAMT"]) * _flex.CDecimal(_flex[e.Row,"RT_VAT"]) /100);

//						this.SetControlsBinding(_flex.DataView);
						break;
						// 원화금액
					case 15 :
						_flex[e.Row,"AM_GIRAMT"] = _flex.Editor.Text;

//						this.SetControlsBinding(_flex.DataView);
						break;
						// 부가세
					case 16 :
						_flex[e.Row,"AM_VAT"] = _flex.Editor.Text;

//						this.SetControlsBinding(_flex.DataView);
						break;
						// 재고단위
					case 17 :
						_flex[e.Row,"UNIT_IM"] = _flex.Editor.Text;

//						this.SetControlsBinding(_flex.DataView);
						break;
						// 출하의뢰량
					case 18 :
						_flex[e.Row,"QT_IM"] = _flex.Editor.Text;

//						this.SetControlsBinding(_flex.DataView);
						break;
				}
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
		}

		#endregion

		#endregion

		#region ♣ 도움창 이벤트 / 메소드

		

		#region -> OnHelpShow

		/// <summary>
		/// 도움창 띄우는 함수
		/// </summary>
		private void OnHelpShow(object sender, System.EventArgs e)
		{

			object dlg = null;

			if(Duzon.Common.Forms.BasicInfo.ActiveDialog == false)
			{				
				switch(((Duzon.Common.Controls.ButtonExt)sender).Name)
				{
					
						// 의뢰일자
					case "m_btnDtGir":
						dlg = this.LoadHelpWindow("P_PR_CALENDAR", new object[] {this.MainFrameInterface, this.m_mskDtGir.Text});
						if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
						{	
							if(dlg is IHelpWindow)
							{							
								this.m_mskDtGir.Text = ((((IHelpWindow)dlg).ReturnValues)[0].ToString());
								g_dtGirH.Rows[0].BeginEdit();
								g_dtGirH.Rows[0]["DT_GIR"] = ((((IHelpWindow)dlg).ReturnValues)[0].ToString());
								g_dtGirH.Rows[0].EndEdit();
								this.m_mskDtGir.Focus();
							}
						}
						break;

					default:
						break;
				}
			}
		}

		#endregion

		#region -> OnEnterHelpShow

		/// <summary>
		/// 도움창 띄우는 함수
		/// </summary>
		private void OnEnterHelpShow(object sender, System.EventArgs e)
		{
			object dlg = null;
			switch(((Duzon.Common.Controls.ButtonExt)sender).Name)
			{
				
					// 의뢰일자
				case "m_btnDtGir":
					dlg = this.LoadHelpWindow("P_PR_CALENDAR", new object[] {this.MainFrameInterface});
					if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
					{	
						if(dlg is IHelpWindow)
						{							
							this.m_mskDtGir.Text = ((((IHelpWindow)dlg).ReturnValues)[0].ToString());
							g_dtGirH.Rows[0]["DT_GIR"] = ((((IHelpWindow)dlg).ReturnValues)[0].ToString());
							this.m_mskDtGir.Focus();
						}
					}
					break;
			
				

				default:
					break;
			}
		}

		#endregion

		#endregion
		
		#region ♣ 기타 이벤트 / 메소드

		#region -> OnComboBoxSelectionChangeCommitted

		/// <summary>
		/// 콤보박스가 바뀔때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnComboBoxSelectionChangeCommitted(object sender, System.EventArgs e)
		{
			switch(((Control)sender).Name)
			{

					//거래구분
				case "m_cboTpBusi":
					// 거래구분이 "LOCAL L/C 일때..
					if(this.m_cboTpBusi.SelectedValue.ToString() == "003")
					{
						m_cboLcCode.SelectedValue = "001";
						m_cboLcCode.Enabled  = true;

						m_btnApplySo.Enabled = false;	// 수주적용버튼
					}
						//거래구분이 "MASTER L/C 일때..
					else if(this.m_cboTpBusi.SelectedValue.ToString() == "004")
					{
						m_cboLcCode.SelectedValue = "001";
						m_cboLcCode.Enabled  = false;

						m_btnApplySo.Enabled = false;	// 수주적용버튼
					}
					else
					{
						m_cboLcCode.SelectedValue = "001";
						m_cboLcCode.Enabled = false;

						m_btnApplySo.Enabled = true;	// 수주적용버튼
					}
					//거래구분이 "LOCAL L/C & 선L/C", "MASTER L/C" 인 경우 L/C적용만 가능
					if( (m_cboTpBusi.SelectedValue.ToString() == "003") || (m_cboTpBusi.SelectedValue.ToString() == "004") )
					{
						m_btnApplySo.Enabled = false;
					}
					else
					{
						m_btnApplySo.Enabled = true;
					}
					g_dtGirH.Rows[0].BeginEdit();
					g_dtGirH.Rows[0]["TP_BUSI"] = m_cboTpBusi.SelectedValue;
					g_dtGirH.Rows[0].EndEdit();
					break;

					//출하의뢰공장
				case "m_cboPlantGir":
					g_dtGirH.Rows[0].BeginEdit();
					g_dtGirH.Rows[0]["CD_PLANT"] = m_cboPlantGir.SelectedValue;
					g_dtGirH.Rows[0].EndEdit();
					break;
					// 의뢰구분
				case "m_cboFgGirq" :
					g_dtGirH.Rows[0].BeginEdit();
					g_dtGirH.Rows[0]["YN_RETURN"] = m_cboFgGirq.SelectedValue;
					g_dtGirH.Rows[0].EndEdit();

					if(this.g_dtGirL == null)
						return;

					if(this.g_dtGirL.Rows.Count > 0)
					{
						for(int row = 0; row < this.g_dtGirL.Rows.Count; row++)
						{
							this.g_dtGirL.Rows[row].BeginEdit();
							this.g_dtGirL.Rows[row]["RET"] = m_cboFgGirq.SelectedValue;
							this.g_dtGirL.Rows[row].EndEdit();
						}
					}
					break;
			}
		}

		#endregion

		#region -> m_cboLcCode_SelectionChangeCommitted

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_cboLcCode_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			switch(this.m_cboLcCode.SelectedValue.ToString())
			{
				case "001" ://선 L/C
					m_btnApplySo.Enabled = false;	// 수주적용버튼
					break;
				case "002" ://후 L/C
					m_btnApplySo.Enabled = true;	// 수주적용버튼
					break;
			}
		}

		#endregion

		#region -> OnValidating

		private void OnValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				switch(sender.GetType().Name)
				{

						
					case "TextBox":
					case "m_txtDcRmk":
						if(m_txtDcRmk.Modified == true)
						{
							g_dtGirH.Rows[0].BeginEdit();
							g_dtGirH.Rows[0]["DC_RMK"] = ((Duzon.Common.Controls.TextBoxExt)sender).Text;
							g_dtGirH.Rows[0].EndEdit();
						}
						break;
					case "MaskedEditBox" :
						g_dtGirH.Rows[0].BeginEdit();
						g_dtGirH.Rows[0]["DT_GIR"] = ((Duzon.Common.Controls.MaskedEditBox)sender).ClipText;
						g_dtGirH.Rows[0].EndEdit();
						break;
				}
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
		}

		#endregion

		#region -> OnKeyDown

		/// <summary>
		/// 텍스트박스, 마스크에디터, 콤보박스 이벤트
		/// 도움창의 TextBox에 Enter키 입력시 코드, 명 값 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.Enter :
					if((sender.GetType().Name == "CodeTextBox") || (sender.GetType().Name == "MaskedEditBox") || (sender.GetType().Name == "DropDownComboBox"))
						SendKeys.Send("{TAB}");
					break;
				case Keys.Up :
					if((sender.GetType().Name == "CodeTextBox") || (sender.GetType().Name == "MaskedEditBox"))
						SendKeys.Send("{+TAB}");
					break;
				case Keys.Down :
					if((sender.GetType().Name == "CodeTextBox") || (sender.GetType().Name == "MaskedEditBox"))
						SendKeys.Send("{TAB}");
					break;
				
			}
		}

		#endregion

		#region -> m_btnDelete_Click

		/// <summary>
		/// 라인 단위 개별 삭제 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!DoContinue())
					return;

				// Select를 사용하여 체크된 데이터의 DataRow[] 을 얻어온다.
				DataRow[] ldr_temp = _flex.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);

				// 그리드에서 체크된 데이터만 복제한다.
				// 현재 바인딩된 원본 그리드의 스키마를 복제한다.
				DataTable ldt_Copy = _flex.DataTable.Clone();
    
				if(_flex.DataView.Count == 0)
					return;

				if(!DeleteChecked(_flex.DataView)) 
				{
					ShowMessage("SA_M000104");
					return;
				}
				else
					this.m_btnDelete.Enabled = true;

				if(ldr_temp.Length > 0)
				{
					if(_flex.DataView.Count == ldr_temp.Length)
					{
						if(g_dtGirH != null)
						{
							for(int row = 0; row < ldr_temp.Length; row ++)

								ldr_temp[row].Delete();

							this.g_dtGirH.Rows[0].Delete();

							if(this.g_dtGirH.Rows.Count == 0)
							{
								DataRow ldr_new = g_dtGirH.NewRow();
								g_dtGirH.Rows.Add(ldr_new);

								g_dtGirH.Rows[0].BeginEdit();
								g_dtGirH.Rows[0]["TP_BUSI"]		= m_cboTpBusi.SelectedValue;
								g_dtGirH.Rows[0]["CD_PARTNER"]	= bpCdPartner.CodeValue;
								g_dtGirH.Rows[0]["CD_PLANT"]	= m_cboPlantGir.SelectedValue;
								g_dtGirH.Rows[0]["DT_GIR"]		= m_mskDtGir.Text;
								g_dtGirH.Rows[0]["NO_EMP"]		= bpNoEmp.CodeValue;
								g_dtGirH.Rows[0]["YN_RETURN"]	= m_cboFgGirq.SelectedValue.ToString();
								g_dtGirH.Rows[0]["STA_GIR"]		= "O";
								g_dtGirH.Rows[0].EndEdit();
							}
						}
					}
					else
					{
						// 아래의 경우는 DataTable까지 필요한 경우 복제된 테이블에 데이터를 추가하는 부분
						// 복제된 테이블에 추가한다.
						for(int i = 0; i < ldr_temp.Length; i++)
						{
							ldr_temp[i].Delete();
						}
						this.ToolBarSaveButtonEnabled = true;
						this.m_btnApplySo.Enabled = true;
					}
				}
				else
				{
					//선택된 사원이 없습니다.사원을 선택해주세요.
					this.ShowMessage("SA_M000093","IK1");
					this.ToolBarSaveButtonEnabled = false;
				}
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> OnSubOpen(요청적용, 출고적용)

		private void OnSubOpen(object sender, System.EventArgs e)
		{
			string ls_filter = "";
			object dlg = null;

			if(!Check())		// 널값체크 및 중복값체크 등에서 문제가 발생한 경우
				return;

			try
			{
				switch((((RoundedButton)sender)).Name)
				{
						//요청적용
					case "m_btnApplySo":	
						if(_flex.DataView != null)
						{
							if(_flex.Rows.Count > 2)
							{
								if(_flex.Rows.Count == 3)
								{
									ls_filter = " AND A.NO_SO NOT IN(" + "'" + _flex[_flex.Row,"NO_SO"].ToString() + "')";
								}
								else
								{
									string values = null;
//									for(int i = 1; i < _flex.Rows.Count -1; i++)
//									{
//										values += "'" + _flex[i,"NO_SO"].ToString() + "'" + ",";
//									}
//									values += "'" + _flex[_flex.Row,"NO_SO"].ToString() + "'";
									for (int i=0; i < _flex.DataTable.Rows.Count; i++)
									{
										values += " '" + _flex.DataTable.Rows[i]["NO_SO"].ToString().Trim() + "' ";
										if (i != _flex.DataTable.Rows.Count-1)
											values += ", ";
									}
									
									ls_filter = " AND A.NO_SO NOT IN(" + values + ")";
								}
							}
						}

						// 수주적용보조 화면에 넘겨주는 Parameter : MainFrameInterface, 거래구분코드값,
						// 거래구분명, 출하공장 코드값, 출하공장명, 거래처 코드값, 거래처명, 반품유무
						dlg = this.LoadHelpWindow("P_SA_SO_SUB", new object[] {this.MainFrameInterface, 
																				m_cboTpBusi.SelectedValue.ToString(), 
																				m_cboTpBusi.Text, 
																				m_cboPlantGir.SelectedValue.ToString(), 
																				m_cboPlantGir.Text, 
																				bpCdPartner.CodeValue, 
																				bpCdPartner.CodeName, 
																				"", //bpNm_Sl.CodeValue, 
																				"", //bpNm_Sl.CodeName,
																				m_cboFgGirq.SelectedValue.ToString(), 
																				ls_filter,gdt_ReqDataREQ});

						if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
						{
							if(dlg is IHelpWindow)
							{
								DataRow[] ldrL = (DataRow[])(((IHelpWindow)dlg).ReturnValues)[0];

								if(ldrL != null && ldrL.Length > 0)
								{
									this.FreeFormControlsEnabled(false);

									this.ToolBarAddButtonEnabled	= true;
									this.ToolBarSaveButtonEnabled	= true;
								}

								FillGirL(ldrL,"SO");								
							}
						}
						break;
						//출고적용
					case "m_rudCholGo":	
						if(_flex.DataView != null)
						{
							if(_flex.Rows.Count > 1)
							{
								if(_flex.Rows.Count == 2)
								{
									ls_filter = " AND NO_IO NOT IN(" + "'" + _flex[1,"NO_SO_MGMT"].ToString() + "')";
								}
								else
								{
									string values = null;
									for(int i = 1; i < _flex.Rows.Count -1; i++)
									{
										values += "'" + _flex[i,"NO_SO_MGMT"].ToString() + "'" + ",";
									}

									values += "'" + _flex[_flex.Rows.Count,"NO_SO_MGMT"].ToString() + "'";
									ls_filter = " AND NO_IO NOT IN(" + values + ")";
								}
							}
						}

						string[] argument = new string[8];
						//						argument[0] = this.m_cdtxtCdPatner.CodeValue;				// 거래처코드
						//						argument[1] = this.m_cdtxtCdPatner.CodeName;				// 거래처명
						//						argument[2] = this.m_dzcboCdPlant.SelectedValue.ToString();	// 공장코드
						//						argument[3] = this.m_dzcboCdPlant.Text;						// 공장명
						//						argument[4] = this.m_dzcboTpVat.SelectedValue.ToString();	// 과세구분코드
						//						argument[5] = this.m_dzcboTpVat.Text;						// 과세구분명
						//						argument[6] = this.m_FgTrans;								// 거래구분코드
						//						argument[7] = ls_filter;									// 출고적용된 데이터
						dlg = this.LoadHelpWindow("P_SA_GIRGI_SUB", new object[] {this.MainFrameInterface, argument});
						if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
						{
							if(dlg is IHelpWindow)
							{
								DataRow[] ldrL = (DataRow[])(((IHelpWindow)dlg).ReturnValues)[0];

								if(ldrL != null && ldrL.Length > 0)
								{
									this.FreeFormControlsEnabled(false);

									this.ToolBarSaveButtonEnabled = true;
									this.ToolBarAddButtonEnabled  = true;
									this.ToolBarDeleteButtonEnabled = true;
								}
								FillGirL(ldrL,"CH");
							}
						}
						break;
				}
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
		}

		#endregion


		#region -> SetControlState

		private void SetControlState()
		{
			try
			{			
				_flex.DataTable.Clear();
				_flex.DataTable.AcceptChanges();
				g_dtGirH.Clear();
			}
			catch
			{}
			
			m_txtNoGir.Text			= "";
			//bpNm_Sl.CodeName		= "";
			//bpNm_Sl.CodeValue		= "";
//			bpNm_Sl.IsConfirmed		= false;
			bpCdPartner.CodeName	= "";
			bpCdPartner.CodeValue	= "";
//			bpCdPartner.IsConfirmed= false;

            bpTpGi.CodeValue = "";
            bpTpGi.CodeName = "";

			m_txtDcRmk.Text			= "";
			m_mskDtGir.Text = MainFrameInterface.GetStringToday;

			DataRow ldr_new = g_dtGirH.NewRow();
			g_dtGirH.Rows.Add(ldr_new);

			g_dtGirH.Rows[0].BeginEdit();
			g_dtGirH.Rows[0]["CD_COMPANY"]	= LoginInfo.CompanyCode;
			g_dtGirH.Rows[0]["TP_BUSI"]		= m_cboTpBusi.SelectedValue;
			g_dtGirH.Rows[0]["CD_PARTNER"]	= bpCdPartner.CodeValue;
			g_dtGirH.Rows[0]["CD_PLANT"]	= m_cboPlantGir.SelectedValue;
			g_dtGirH.Rows[0]["DT_GIR"]		= m_mskDtGir.Text;
			g_dtGirH.Rows[0]["NO_EMP"]		= bpNoEmp.CodeValue;
			g_dtGirH.Rows[0]["STA_GIR"]		= "O";
			g_dtGirH.Rows[0].EndEdit();
		}

		#endregion

		#region -> FreeFormControlsEnabled

		private void FreeFormControlsEnabled(bool values)
		{
			try
			{
				//m_txtNoGir.Enabled		= values;// 의뢰번호
				m_mskDtGir.Enabled		= values;// 의뢰일자
				//bpCdPartner.Enabled	= values;// 거래처
                if (values)
                    bpCdPartner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;// 거래처
                else
                    bpCdPartner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.TotalReadOnly;// 거래처
//				m_btnCdPartner.Enabled	= values;// 거래처 도움창버튼
				m_cboPlantGir.Enabled	= values;// 출고공장
				m_cboTpBusi.Enabled		= values;// 거래구분
				m_cboLcCode.Enabled		= values;// L/C
                if (values)
                    bpTpGi.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
                else
                    bpTpGi.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.TotalReadOnly;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region -> DeleteChecked

		/// <summary>
		/// 출고량이 0보다 큰면 삭제를 하지 못한다..
		/// </summary>
		/// <returns></returns>
		private bool DeleteChecked()
		{
			if(_flex.DataSource == null)
				return true;

			DataView ldv_check = _flex.DataView;

			//출고량(SA_GIRL.QT_GI> 0)
			if(ldv_check != null && ldv_check.Count > 0)
			{
				for(int row = 0; row < ldv_check.Count; row++)
				{
					if(Convert.ToDecimal(ldv_check[row]["QT_GI"]) > 0)
						return false;
				}
			}
			return true;
		}

		#endregion

		#region -> FillGirH

		private void FillGirH(DataRowView pdr_Header)
		{
			if(pdr_Header == null || pdr_Header.DataView.Count == 0)
			{
				this.g_dtGirH.AcceptChanges();
				return;
			}

			try
			{
				// 의뢰번호
				m_txtNoGir.Text				= pdr_Header["NO_GIR"].ToString();
				// 의뢰일자
				m_mskDtGir.Text				= pdr_Header["DT_GIR"].ToString();
				// 출하의뢰자
				bpNoEmp.CodeName			= pdr_Header["NM_KOR"].ToString();
				bpNoEmp.CodeValue			= pdr_Header["NO_EMP"].ToString();
				// 거래구분
				m_cboTpBusi.SelectedValue	= pdr_Header["TP_BUSI"].ToString();
				// 출하공장
				m_cboPlantGir.SelectedValue = pdr_Header["CD_PLANT"].ToString();
				// 창고
				//bpNm_Sl.CodeName			= pdr_Header["NM_SL"].ToString();
				//bpNm_Sl.CodeValue			= pdr_Header["CD_SL"].ToString();
				// 비고
				m_txtDcRmk.Text				= pdr_Header["DC_RMK"].ToString(); 
				// 거래처
				bpCdPartner.CodeValue		= pdr_Header["CD_PARTNER"].ToString();
				bpCdPartner.CodeName		= pdr_Header["LN_PARTNER"].ToString();

                m_cboFgGirq.SelectedValue   = pdr_Header["YN_RETURN"].ToString();

                bpTpGi.CodeValue            = pdr_Header["TP_GI"].ToString();
                bpTpGi.CodeName             = pdr_Header["NM_TP_GI"].ToString();
			
//				bpNoEmp.IsConfirmed	= true;
//				bpNm_Sl.IsConfirmed		= true;
//				bpCdPartner.IsConfirmed= true;

				if(g_dtGirH.Rows.Count == 0)
				{
					g_dtGirH = new DataTable();
			
					g_dtGirH.Columns.Add(new DataColumn("NO_GIR"));		// 납품의뢰번호
					g_dtGirH.Columns.Add(new DataColumn("CD_COMPANY"));	// 회사코드
					g_dtGirH.Columns.Add(new DataColumn("DT_GIR"));		// 의뢰일자
					g_dtGirH.Columns.Add(new DataColumn("NO_EMP"));		// 출하의뢰자
					g_dtGirH.Columns.Add(new DataColumn("TP_BUSI"));	// 거래구분
					g_dtGirH.Columns.Add(new DataColumn("CD_PLANT"));	// 출하의뢰공장
					//g_dtGirH.Columns.Add(new DataColumn("CD_SL"));		// 출하창고
                    g_dtGirH.Columns.Add(new DataColumn("TP_GI"));		// 출하형태
					g_dtGirH.Columns.Add(new DataColumn("CD_PARTNER"));	// 거래처
					g_dtGirH.Columns.Add(new DataColumn("GI_PARTNER"));	// 납품처
					g_dtGirH.Columns.Add(new DataColumn("STA_GIR"));	// 처리상태
					g_dtGirH.Columns.Add(new DataColumn("YN_RETURN"));	// 반품유무
					g_dtGirH.Columns.Add(new DataColumn("DC_RMK"));		// 비고
					g_dtGirH.Columns.Add(new DataColumn("DTS_INSERT"));	// 등록자
					g_dtGirH.Columns.Add(new DataColumn("ID_INSERT"));	// 등록일
					g_dtGirH.Columns.Add(new DataColumn("DTS_UPDATE"));	// 수정자
					g_dtGirH.Columns.Add(new DataColumn("ID_UPDATE"));	// 수정일자

					DataRow ldr_new = g_dtGirH.NewRow();
					g_dtGirH.Rows.Add(ldr_new);
				}

				g_dtGirH.Rows[0]["NO_GIR"]		= pdr_Header["NO_GIR"].ToString();
				g_dtGirH.Rows[0]["CD_COMPANY"]	= this.LoginInfo.CompanyCode;
				g_dtGirH.Rows[0]["DT_GIR"]		= pdr_Header["DT_GIR"].ToString();
				g_dtGirH.Rows[0]["NO_EMP"]		= pdr_Header["NO_EMP"].ToString();
				g_dtGirH.Rows[0]["TP_BUSI"]		= pdr_Header["TP_BUSI"].ToString();
				g_dtGirH.Rows[0]["CD_PLANT"]	= pdr_Header["CD_PLANT"].ToString();
                //g_dtGirH.Rows[0]["CD_SL"]       = pdr_Header["CD_SL"].ToString();
				g_dtGirH.Rows[0]["TP_GI"]		= pdr_Header["TP_GI"].ToString();
				g_dtGirH.Rows[0]["CD_PARTNER"]	= pdr_Header["CD_PARTNER"].ToString();
				g_dtGirH.Rows[0]["GI_PARTNER"]	= pdr_Header["GI_PARTNER"].ToString();
				g_dtGirH.Rows[0]["STA_GIR"]		= pdr_Header["STA_GIR"].ToString();
			
				if(m_cboFgGirq.SelectedValue.ToString().Trim() == "")
					g_dtGirH.Rows[0]["YN_RETURN"]	= "C";
				else
					g_dtGirH.Rows[0]["YN_RETURN"]	= m_cboFgGirq.SelectedValue.ToString();

				g_dtGirH.Rows[0]["DC_RMK"]		= pdr_Header["DC_RMK"].ToString();
				g_dtGirH.Rows[0]["DTS_INSERT"]	= pdr_Header["DTS_INSERT"].ToString();
				g_dtGirH.Rows[0]["ID_INSERT"]	= pdr_Header["ID_INSERT"].ToString();
				g_dtGirH.Rows[0]["DTS_UPDATE"]	= pdr_Header["DTS_UPDATE"].ToString();
				g_dtGirH.Rows[0]["ID_UPDATE"]	= pdr_Header["ID_UPDATE"].ToString();	

				g_dtGirH.AcceptChanges();
				this.ToolBarSaveButtonEnabled = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region -> FillGirL

		/// <summary>
		/// 수주, LC적용 버튼 누를때 Row생성
		/// </summary>
		/// <param name="pdr_Line"></param>
		/// <param name="ps_sender"></param>
		private void FillGirL(DataRow[] pdr_Line, string ps_sender)
		{
			if(pdr_Line == null || pdr_Line.Length == 0)
				return;

			if(!DoContinue())
				return;

			DataTable myTable = new DataTable();

			switch(ps_sender)
			{
					//수주 적용
				case "SO":		
					if(m_cboFgGirq.SelectedValue.ToString().Trim() == "")
					{
						if(g_dtGirH != null)
							g_dtGirH.Rows[0]["YN_RETURN"]	= "C";							
					}
					else
					{
                        if (g_dtGirH != null)
                        {
                            g_dtGirH.Rows[0]["YN_RETURN"] = m_cboFgGirq.SelectedValue.ToString().Trim();
                            g_dtGirH.Rows[0]["TP_GI"] = pdr_Line[0]["TP_GI"].ToString();
                        }	
					}

					if(_flex.DataSource != null)
						_flex.RemoveViewAll();
					else
					{
						_flex.Redraw = false;
						_flex.BindingStart();
						_flex.DataSource = g_dtGirL;
						_flex.BindingEnd();
						_flex.Redraw = true;
					}

					if( gdt_ReqDataREQ.Rows.Count <= 0)
					{
						gdt_ReqDataREQ = g_dtGirL.Clone();
					}

					_flex.Redraw = false;

					DataRow row_1;

					for(int row = 0; row < pdr_Line.Length; row++)
					{	
						row_1 =  pdr_Line[row];
						gdt_ReqDataREQ.ImportRow(row_1);						

						_flex.Rows.Add();						
						_flex.Row = _flex.Rows.Count - 1;	

						_flex[_flex.Row, "CD_COMPANY"]  = pdr_Line[row]["CD_COMPANY"];          //회사코드
						_flex[_flex.Row, "SEQ_GIR"]     = this.Seq_Gir = this.Seq_Gir + 1;      //의뢰항번
						_flex[_flex.Row, "SO_PARTNER"]  = pdr_Line[row]["SO_PARTNER"];          //수주처
						_flex[_flex.Row, "GI_PARTNER"]  = pdr_Line[row]["GI_PARTNER"];          //납품처
						_flex[_flex.Row, "GI_PARTNERNM"]  = pdr_Line[row]["GI_PARTNERNM"];          //납품처
						_flex[_flex.Row, "CD_PARTNER"]  = pdr_Line[row]["CD_PARTNER"];          //매출처
						_flex[_flex.Row, "BILL_PARTNER"]= pdr_Line[row]["BILL_PARTNER"];        //수금처
						_flex[_flex.Row, "CD_ITEM"]     = pdr_Line[row]["CD_ITEM"];             //품목코드
						_flex[_flex.Row, "NM_ITEM"]     = pdr_Line[row]["NM_ITEM"];             //품목코드명
						_flex[_flex.Row, "TP_ITEM"]     = pdr_Line[row]["TP_ITEM"];             //품목유형
						_flex[_flex.Row, "UNIT"]        = pdr_Line[row]["UNIT_SO"];             //수주단위
						_flex[_flex.Row, "UNIT_IM"]     = pdr_Line[row]["UNIT_IM"];             //재고단위
						_flex[_flex.Row, "DT_DUEDATE"]  = pdr_Line[row]["DT_DUEDATE"];          //납기일

						if(pdr_Line[row]["DT_REQGI"].ToString() =="00000000")
						{
							_flex[_flex.Row, "DT_REQGI"] = "";
						}
						else
						{
						
							_flex[_flex.Row, "DT_REQGI"]    = pdr_Line[row]["DT_REQGI"];            //출하예정일
						}
						_flex[_flex.Row, "YN_INSPECT"]  = pdr_Line[row]["YN_INSPECT"];          //검사유무
						_flex[_flex.Row, "QT_GIR_IM"]   = pdr_Line[row]["QT_GIR1"];             //수배의뢰량
						_flex[_flex.Row, "QT_IM"]       = pdr_Line[row]["QT_IM1"];              //재고수량
						_flex[_flex.Row, "QT_GIR"]      = pdr_Line[row]["QT_GIR1"];             //수배의뢰량
						_flex[_flex.Row, "AM_GIR"]      = pdr_Line[row]["AM_EXGIR1"];           //의뢰금액
						_flex[_flex.Row, "AM_GIRAMT"]   = pdr_Line[row]["AM_GIR1"];             //의뢰원화금액
						_flex[_flex.Row, "UM"]          = pdr_Line[row]["UM_SO"];               //단가
						_flex[_flex.Row, "QT_SO"]       = pdr_Line[row]["QT_SO"];               //의뢰번호
						_flex[_flex.Row, "CD_EXCH"]     = pdr_Line[row]["CD_EXCH"];             //환종
						_flex[_flex.Row, "RT_EXCH"]     = pdr_Line[row]["RT_EXCH"];             //환율
						_flex[_flex.Row, "NO_PROJECT"]  = pdr_Line[row]["NO_PROJECT"];          //프로젝트코드
						_flex[_flex.Row, "CD_SALEGRP"]  = pdr_Line[row]["CD_SALEGRP"];          //영업그룹
						_flex[_flex.Row, "NO_EMP"]      = pdr_Line[row]["NO_EMP"];              //담당자
						_flex[_flex.Row, "STA_GIR"]     = "O";                                  //출고상태
						//_flex[_flex.Row, "STA_GIRNM"]   = "의뢰";                               //의뢰
						_flex[_flex.Row, "NO_SO"]       = pdr_Line[row]["NO_SO"];               //수주번호
						_flex[_flex.Row, "SEQ_SO"]      = pdr_Line[row]["SEQ_SO"];              //수주항번
						_flex[_flex.Row, "NO_LC"]       = "";                                   //LC번호
						_flex[_flex.Row, "SEQ_LC"]      = 0;                                    //LC항번
						_flex[_flex.Row, "QT_GI"]       = 0;                                    //수배출하량
						_flex[_flex.Row, "TP_GI"]       = pdr_Line[row]["TP_GI"];               //출하형태
						_flex[_flex.Row, "TP_IV"]       = pdr_Line[row]["TP_IV"];               //매출형태
						_flex[_flex.Row, "TP_BUSI"]     = pdr_Line[row]["TP_BUSI"];             //거래구분
						_flex[_flex.Row, "CONF"]        = pdr_Line[row]["CONF"];                //자동승인
						_flex[_flex.Row, "GIR"]         = pdr_Line[row]["GIR"];                 //의뢰유무
						_flex[_flex.Row, "GI"]          = pdr_Line[row]["GI"];                  //출고유무
						_flex[_flex.Row, "IV"]          = pdr_Line[row]["IV"];                  //마감유무
						_flex[_flex.Row, "TRADE"]       = pdr_Line[row]["TRADE"];               //무역유무
						_flex[_flex.Row, "CM"]          = pdr_Line[row]["CM"];                  //위탁유무
						_flex[_flex.Row, "RET"]         = pdr_Line[row]["RET"];                 //반품유무
						_flex[_flex.Row, "SUBCONT"]     = pdr_Line[row]["SUBCONT"];             //외주유무

						if(pdr_Line[row]["YN_INSPECT"].ToString() == "Y")
							_flex[_flex.Row, "QT_INSPECT"] = pdr_Line[row]["QT_GIR"];       //체크시에 의뢰수량 QT_GIR
						else
							_flex[_flex.Row, "QT_INSPECT"] = 0;

						_flex[_flex.Row, "QT_PASS"]             = 0;                                    //합격량
						_flex[_flex.Row, "QT_REJECT"]           = 0;                                    //불합격량
						_flex[_flex.Row, "YN_DECISION"]         = "N";                                  //판정여부
						_flex[_flex.Row, "TP_VAT"]              = pdr_Line[row]["TP_VAT"];              //과세구분
						_flex[_flex.Row, "RT_VAT"]              = pdr_Line[row]["RT_VAT"];              // codedtl에 CD_FLAG1컬럼사용(부가세율)
						_flex[_flex.Row, "AM_VAT"]              = System.Decimal.Truncate(Convert.ToDecimal(pdr_Line[row]["AM_GIR1"]) * Convert.ToDecimal(pdr_Line[row]["RT_VAT"]) / 100);//부가세
						_flex[_flex.Row, "CD_SL"]               = pdr_Line[row]["CD_SL"];               //창고
						_flex[_flex.Row, "NM_SL"]               = pdr_Line[row]["NM_SL"];               //창고명
						_flex[_flex.Row, "STND_ITEM"]           = "";                                   //규격
						_flex[_flex.Row, "FG_TAXP"]             = pdr_Line[row]["FG_TAXP"];             //계산처리
						_flex[_flex.Row, "AM_EXGIR"]            = pdr_Line[row]["AM_EXGIR"];            //계산처리
						_flex[_flex.Row, "NO_IO_MGMT"]          = pdr_Line[row]["NO_IO_MGMT"];          //관련수불번호
						_flex[_flex.Row, "NO_IOLINE_MGMT"]      = pdr_Line[row]["NO_IOLINE_MGMT"];      //관련수불항번
						_flex[_flex.Row, "NO_SOLINE_MGMT"]      = pdr_Line[row]["NO_SOLINE_MGMT"];      //관련수주번호
						_flex[_flex.Row, "NO_SO_MGMT"]          = pdr_Line[row]["NO_SO_MGMT"];          //계산처리
						_flex[_flex.Row, "DTS_INSERT"]          = "";                                   //등록일
						_flex[_flex.Row, "ID_INSERT"]           = "";                                   //등록자
						_flex[_flex.Row, "DTS_UPDATE"]          = "";                                   //수정일
						_flex[_flex.Row, "ID_UPDATE"]           = "";                                   //수정자

						_flex[_flex.Row, "NM_SO"]				= pdr_Line[row]["NM_SO"];  						
						_flex[_flex.Row, "DC_RMK"]				= pdr_Line[row]["DC_RMK"]; 
						_flex[_flex.Row, "AUTO_TYPE"]			= "N";

						_flex[_flex.Row, "CD_PLANT"] = pdr_Line[row]["CD_PLANT"];
						_flex[_flex.Row, "STND_ITEM"] = pdr_Line[row]["STND_ITEM"];

						_flex.AddFinished();
					}
					_flex.Col = _flex.Cols.Fixed;
					_flex.Focus();
					
					m_txtDcRmk.Text	= _flex[_flex.Row, "DC_RMK"].ToString();
//					m_mskDtGir.Text = _flex[_flex.Row, "DT_DUEDATE"].ToString();
//					bpNoEmp.CodeValue =  _flex[_flex.Row, "NO_EMP"].ToString();
//					bpNoEmp.CodeName =  _flex[_flex.Row, "NM_KOR"].ToString();

//					MessageBox.Show(_flex[_flex.Row, "DC_RMK"].ToString());

					_flex.Redraw = true;

					break;

					//출고적용
				case "CH":
					if(_flex.DataSource != null)
						_flex.RemoveViewAll();
					else
					{
						_flex.Redraw = false;
						_flex.BindingStart();
						_flex.DataSource = g_dtGirL;
						_flex.BindingEnd();
						_flex.Redraw = true;
					}

					for( int row = 0 ; row < pdr_Line.Length ; row++)
					{
						_flex.Rows.Add();						
						_flex.Row = _flex.Rows.Count - 1;	

						_flex[_flex.Row, "CHK"]         = pdr_Line[row]["CHOOSE"];      // S
						_flex[_flex.Row, "CD_COMPANY"]  = pdr_Line[row]["CD_COMPANY"];  // 회사코드
						_flex[_flex.Row, "CD_ITEM"]     = pdr_Line[row]["CD_ITEM"];     // 품목코드
						_flex[_flex.Row, "NM_ITEM"]     = pdr_Line[row]["NM_ITEM"];     // 품목코드명
						_flex[_flex.Row, "STND_ITEM"]   = pdr_Line[row]["STND_ITEM"];   // 규격
						_flex[_flex.Row, "UNIT_IM"]     = pdr_Line[row]["UNIT_IM"];     // 재고단위
						_flex[_flex.Row, "QT_GIR"]      = pdr_Line[row]["QT"];          // 수량
						_flex[_flex.Row, "UM"]          = pdr_Line[row]["UM_EX"];       // 단가
						_flex[_flex.Row, "AM_GIR"]      = pdr_Line[row]["AM_GIR"];      // 금액
						_flex[_flex.Row, "AM_GIRAMT"]   = pdr_Line[row]["AM_GIRAMT"];   // 원화금액
						_flex[_flex.Row, "AM_VAT"]      = pdr_Line[row]["VAT"];         // 부가세
						_flex[_flex.Row, "CD_SL"]       = pdr_Line[row]["CD_SL"];       // 창고
						_flex[_flex.Row, "NM_SL"]       = pdr_Line[row]["NM_SL"];       // 창고명
						_flex[_flex.Row, "DT_DUEDATE"]  = pdr_Line[row]["DT_IO"];       // 출고일
						_flex[_flex.Row, "NO_SO_MGMT"]  = pdr_Line[row]["NO_IO"];       // 수불번호
						_flex[_flex.Row, "NO_IO_MGMT"] =  pdr_Line[row]["NO_PSO_MGMT"]; // 관련수발주번호
						_flex[_flex.Row, "NO_PROJECT"]  = pdr_Line[row]["CD_PJT"];      // PROJECT

						//=============== hidden 데이터=================================//

						_flex[_flex.Row, "CD_PARTNER"]  = pdr_Line[row]["CD_PARTNER"];  // 거래처코드
						_flex[_flex.Row, "CD_SALEGRP"]  = pdr_Line[row]["CD_GROUP"];    // 영업그룹
						_flex[_flex.Row, "NO_EMP"]      = pdr_Line[row]["NO_EMP"];      // 담당자
						_flex[_flex.Row, "NO_GIR"]      = pdr_Line[row]["NO_ISURCV"];   // 의뢰번호
						_flex[_flex.Row, "TP_VAT"]      = pdr_Line[row]["FG_TAX"];      // 과세구분
						_flex[_flex.Row, "DTS_INSERT"]  = "";
						_flex[_flex.Row, "ID_INSERT"]   = "";
						_flex[_flex.Row, "DTS_UPDATE"]  = "";
						_flex[_flex.Row, "ID_UPDATE"]   = "";
						_flex[_flex.Row, "CD_PLANT"] = pdr_Line[row]["CD_PLANT"];	
						_flex.AddFinished();
					}
					_flex.Col = _flex.Cols.Fixed;
					_flex.Focus();
					break;
			}
			this.m_btnDelete.Enabled = true;
		}

		#endregion

		#region -> DeleteChecked

		/// <summary>
		/// 출고량이 0보다 큰면 삭제를 하지 못한다..
		/// </summary>
		/// <returns></returns>
		private bool DeleteChecked(DataView ldv_check)
		{
			//출고량(SA_GIRL.QT_GI> 0)
			if(ldv_check != null && ldv_check.Count > 0)
			{
				for(int row = 0; row < ldv_check.Count; row++)
				{
					if(Convert.ToDecimal(ldv_check[row]["QT_GI"]) > 0)
						return false;
				}
			}
			return true;
		}

		#endregion

		#region -> SetControlsBinding

		private void SetControlsBinding(DataView ldv_table)
		{
			try
			{
				decimal Qtsum  = 0;
				decimal Amsum  = 0;
				decimal Wonsum = 0;

				if(ldv_table != null && ldv_table.Count > 0)
				{
					for(int row = 0; row < ldv_table.Count; row++)
					{
						Qtsum  += Convert.ToDecimal(ldv_table[row]["QT_GIR"]);		// 수량
						Amsum  += Convert.ToDecimal(ldv_table[row]["AM_GIR"]);		// 금액
						Wonsum += Convert.ToDecimal(ldv_table[row]["AM_GIRAMT"]);	// 원화금액
					}
//					this.m_txtQt.DecimalValue	= Qtsum;	// 수량
//					this.m_txtAm.DecimalValue	= Amsum;	// 금액
//					this.m_txtAmWon.DecimalValue= Wonsum;	// 원화금액
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region -> qt_Check

		private bool qt_Check()
		{
			// ====================== SA_SOL 잔량 체크로직 ===========================//
			object[] args = {_flex.DataTable.GetChanges()};
			bool rs = (bool)this.InvokeRemoteMethod("SaleShipment_NTX", "sale.CC_SA_GIR_NTX", "CC_SA_GIR_NTX.rem", "SelectSaveCheck",args);

			if(rs == false)
			{
				MessageBoxEx.Show("SA_SOL에 잔량을 초과 하였습니다.",this.PageName, MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return false;
			}
			//=======================================================================//
			
			return true;
		}

		#endregion	

		#region >> 날짜 관련 이벤트
		
		private void m_mskDtGir_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_mskDtGir.IsValidated)
			{
				ShowMessage("WK1_003", m_lblDtGir.Text);
				this.m_mskDtGir.Focus();
			}

		}
		#endregion

		#endregion

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "041|042|";
                    break;
            }
        }

		private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			if(e.DialogResult == DialogResult.Cancel)
				return;

			switch(e.ControlName)
			{
				case "bpCdPartner": // 거래처
					bpCdPartner.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
					bpCdPartner.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();

					g_dtGirH.Rows[0].BeginEdit();
					g_dtGirH.Rows[0]["CD_PARTNER"] = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
					g_dtGirH.Rows[0]["GI_PARTNER"] = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
					g_dtGirH.Rows[0].EndEdit();
					break;
                //case "bpNm_Sl":		// 창고									
                //    bpNm_Sl.CodeName = e.HelpReturn.Rows[0]["NM_SL"].ToString();
                //    bpNm_Sl.CodeValue = e.HelpReturn.Rows[0]["CD_SL"].ToString();

                //    try
                //    {
                //        if(_flex.Rows.Count > 1)
                //        {
                //            for(int i = 0 ; i < _flex.DataView.Count ;i++)
                //            {
                //                _flex.DataView[i].BeginEdit();
                //                _flex.DataView[i]["CD_SL"] = e.HelpReturn.Rows[0]["CD_SL"].ToString();
                //                _flex.DataView[i]["NM_SL"] = e.HelpReturn.Rows[0]["NM_SL"].ToString();
                //                _flex.DataView[i].EndEdit();
                //            }
                //        }
                //    }
                //    catch
                //    {}
                //    //g_dtGirH.Rows[0].BeginEdit();
                //    //g_dtGirH.Rows[0]["CD_SL"] = e.HelpReturn.Rows[0]["CD_SL"].ToString();
                //    //g_dtGirH.Rows[0].EndEdit();
                //    break;
                case "bpTpGi":          //출하형태
                    bpTpGi.CodeValue = e.CodeValue;
                    bpTpGi.CodeName = e.CodeName;
                    g_dtGirH.Rows[0]["TP_GI"] = e.CodeValue;
                    if (e.HelpReturn.Rows[0]["FG_IO"].ToString() == "041")
                        m_cboFgGirq.SelectedValue = "Y";
                    else if (e.HelpReturn.Rows[0]["FG_IO"].ToString() == "042")
                        m_cboFgGirq.SelectedValue = "C";
                    break;
				case "bpNoEmp":		// 담당자				
					bpNoEmp.CodeValue =  e.HelpReturn.Rows[0]["NO_EMP"].ToString();
					bpNoEmp.CodeName =  e.HelpReturn.Rows[0]["NM_KOR"].ToString();
					g_dtGirH.Rows[0].BeginEdit();
					g_dtGirH.Rows[0]["NO_EMP"] = e.HelpReturn.Rows[0]["NO_EMP"].ToString();
					g_dtGirH.Rows[0].EndEdit();

					try
					{
						acd_dept = e.HelpReturn.Rows[0]["CD_DEPT"].ToString();
						anm_dept = e.HelpReturn.Rows[0]["NM_DEPT"].ToString();
					}
					catch
					{}
					break;	
			}
		}

		private void Contorl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
		
			switch(e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
					e.HelpParam.P09_CD_PLANT = m_cboPlantGir.SelectedValue.ToString();
					
					break;	
			}
		}

        private void bpNoEmp_Click(object sender, EventArgs e)
        {

        }
	}
}
