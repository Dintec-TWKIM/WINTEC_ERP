
using Duzon.BizOn.Erpu.Resource;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_FI_CARD_EPDOCU_SUB
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_CARD_EPDOCU_SUB));
			this.pnlDocu = new Duzon.Common.Controls.PanelExt();
			this.bpctx예산단위 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.bpctx사업계획 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.rdo일괄 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo건별 = new Duzon.Common.Controls.RadioButtonExt();
			this.bpctx코스트센터 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.bpctx상대회계계정 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.bpctx상대예산계정 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.bpctx회계계정 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.bpctx예산계정 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.txt문서제목 = new Duzon.Common.Controls.TextBoxExt();
			this.panelExt7 = new Duzon.Common.Controls.PanelExt();
			this.panelExt6 = new Duzon.Common.Controls.PanelExt();
			this.dtp회계일자 = new Duzon.Common.Controls.DatePicker();
			this.panelExt16 = new Duzon.Common.Controls.PanelExt();
			this._lbl전표처리 = new Duzon.Common.Controls.LabelExt();
			this.cbo결의구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpctx회계단위 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.bpctx작성부서 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.bpctx작성사원 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.panelExt15 = new Duzon.Common.Controls.PanelExt();
			this.panelExt14 = new Duzon.Common.Controls.PanelExt();
			this.panelExt13 = new Duzon.Common.Controls.PanelExt();
			this.panelExt12 = new Duzon.Common.Controls.PanelExt();
			this.panelExt11 = new Duzon.Common.Controls.PanelExt();
			this.panelExt10 = new Duzon.Common.Controls.PanelExt();
			this.panelExt9 = new Duzon.Common.Controls.PanelExt();
			this.panelExt8 = new Duzon.Common.Controls.PanelExt();
			this.panelExt5 = new Duzon.Common.Controls.PanelExt();
			this.panel5 = new Duzon.Common.Controls.PanelExt();
			this.panelExt4 = new Duzon.Common.Controls.PanelExt();
			this.lbl분개라인처리 = new Duzon.Common.Controls.LabelExt();
			this.lbl코스트센터 = new Duzon.Common.Controls.LabelExt();
			this.lbl상대회계계정 = new Duzon.Common.Controls.LabelExt();
			this.lbl상대예산계정 = new Duzon.Common.Controls.LabelExt();
			this.lbl회계계정 = new Duzon.Common.Controls.LabelExt();
			this.lbl예산계정 = new Duzon.Common.Controls.LabelExt();
			this.lbl사업계획 = new Duzon.Common.Controls.LabelExt();
			this.lbl예산단위 = new Duzon.Common.Controls.LabelExt();
			this.lbl문서제목 = new Duzon.Common.Controls.LabelExt();
			this.lbl결의구분 = new Duzon.Common.Controls.LabelExt();
			this.lbl작성사원 = new Duzon.Common.Controls.LabelExt();
			this.lbl작성부서 = new Duzon.Common.Controls.LabelExt();
			this.lbl회계단위 = new Duzon.Common.Controls.LabelExt();
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.btnOK = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnEnd = new Duzon.Common.Controls.RoundedButton(this.components);
			this.panelExt2 = new Duzon.Common.Controls.PanelExt();
			this.lbl전표처리 = new Duzon.Common.Controls.LabelExt();
			this.m_titlePanel = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.pnlDocu.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo일괄)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo건별)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtp회계일자)).BeginInit();
			this.panelExt16.SuspendLayout();
			this.panelExt4.SuspendLayout();
			this.panelExt1.SuspendLayout();
			this.panelExt2.SuspendLayout();
			this.m_titlePanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlDocu
			// 
			this.pnlDocu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDocu.Controls.Add(this.bpctx예산단위);
			this.pnlDocu.Controls.Add(this.bpctx사업계획);
			this.pnlDocu.Controls.Add(this.panel2);
			this.pnlDocu.Controls.Add(this.bpctx코스트센터);
			this.pnlDocu.Controls.Add(this.bpctx상대회계계정);
			this.pnlDocu.Controls.Add(this.bpctx상대예산계정);
			this.pnlDocu.Controls.Add(this.bpctx회계계정);
			this.pnlDocu.Controls.Add(this.bpctx예산계정);
			this.pnlDocu.Controls.Add(this.txt문서제목);
			this.pnlDocu.Controls.Add(this.panelExt7);
			this.pnlDocu.Controls.Add(this.panelExt6);
			this.pnlDocu.Controls.Add(this.dtp회계일자);
			this.pnlDocu.Controls.Add(this.panelExt16);
			this.pnlDocu.Controls.Add(this.cbo결의구분);
			this.pnlDocu.Controls.Add(this.bpctx회계단위);
			this.pnlDocu.Controls.Add(this.bpctx작성부서);
			this.pnlDocu.Controls.Add(this.bpctx작성사원);
			this.pnlDocu.Controls.Add(this.panelExt15);
			this.pnlDocu.Controls.Add(this.panelExt14);
			this.pnlDocu.Controls.Add(this.panelExt13);
			this.pnlDocu.Controls.Add(this.panelExt12);
			this.pnlDocu.Controls.Add(this.panelExt11);
			this.pnlDocu.Controls.Add(this.panelExt10);
			this.pnlDocu.Controls.Add(this.panelExt9);
			this.pnlDocu.Controls.Add(this.panelExt8);
			this.pnlDocu.Controls.Add(this.panelExt5);
			this.pnlDocu.Controls.Add(this.panel5);
			this.pnlDocu.Controls.Add(this.panelExt4);
			this.pnlDocu.Location = new System.Drawing.Point(10, 73);
			this.pnlDocu.Name = "pnlDocu";
			this.pnlDocu.Size = new System.Drawing.Size(384, 341);
			this.pnlDocu.TabIndex = 24;
			// 
			// bpctx예산단위
			// 
			this.bpctx예산단위.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx예산단위.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_BGCODE_DEPT_SUB;
			this.bpctx예산단위.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx예산단위.Location = new System.Drawing.Point(99, 133);
			this.bpctx예산단위.Name = "bpctx예산단위";
			this.bpctx예산단위.Size = new System.Drawing.Size(280, 21);
			this.bpctx예산단위.TabIndex = 6;
			this.bpctx예산단위.TabStop = false;
			this.bpctx예산단위.Text = "bpCodeNTextBox1";
			// 
			// bpctx사업계획
			// 
			this.bpctx사업계획.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx사업계획.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_BIZPLAN2_SUB;
			this.bpctx사업계획.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx사업계획.Location = new System.Drawing.Point(99, 159);
			this.bpctx사업계획.Name = "bpctx사업계획";
			this.bpctx사업계획.Size = new System.Drawing.Size(280, 21);
			this.bpctx사업계획.TabIndex = 7;
			this.bpctx사업계획.TabStop = false;
			this.bpctx사업계획.Text = "bpCodeNTextBox1";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.rdo일괄);
			this.panel2.Controls.Add(this.rdo건별);
			this.panel2.Location = new System.Drawing.Point(99, 314);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(280, 24);
			this.panel2.TabIndex = 145;
			// 
			// rdo일괄
			// 
			this.rdo일괄.Checked = true;
			this.rdo일괄.Location = new System.Drawing.Point(150, 2);
			this.rdo일괄.Name = "rdo일괄";
			this.rdo일괄.Size = new System.Drawing.Size(105, 20);
			this.rdo일괄.TabIndex = 14;
			this.rdo일괄.TabStop = true;
			this.rdo일괄.Text = "일괄처리";
			this.rdo일괄.TextDD = null;
			this.rdo일괄.UseKeyEnter = true;
			this.rdo일괄.UseVisualStyleBackColor = true;
			// 
			// rdo건별
			// 
			this.rdo건별.Location = new System.Drawing.Point(3, 2);
			this.rdo건별.Name = "rdo건별";
			this.rdo건별.Size = new System.Drawing.Size(105, 20);
			this.rdo건별.TabIndex = 13;
			this.rdo건별.TabStop = true;
			this.rdo건별.Text = "건별처리";
			this.rdo건별.TextDD = null;
			this.rdo건별.UseKeyEnter = true;
			this.rdo건별.UseVisualStyleBackColor = true;
			// 
			// bpctx코스트센터
			// 
			this.bpctx코스트센터.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx코스트센터.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB;
			this.bpctx코스트센터.Location = new System.Drawing.Point(99, 289);
			this.bpctx코스트센터.Name = "bpctx코스트센터";
			this.bpctx코스트센터.Size = new System.Drawing.Size(280, 21);
			this.bpctx코스트센터.TabIndex = 12;
			this.bpctx코스트센터.TabStop = false;
			this.bpctx코스트센터.Text = "bpCodeNTextBox1";
			// 
			// bpctx상대회계계정
			// 
			this.bpctx상대회계계정.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx상대회계계정.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_ACCTCODE_NB_SUB;
			this.bpctx상대회계계정.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx상대회계계정.Location = new System.Drawing.Point(99, 263);
			this.bpctx상대회계계정.Name = "bpctx상대회계계정";
			this.bpctx상대회계계정.Size = new System.Drawing.Size(280, 21);
			this.bpctx상대회계계정.TabIndex = 11;
			this.bpctx상대회계계정.TabStop = false;
			this.bpctx상대회계계정.Text = "bpCodeNTextBox1";
			// 
			// bpctx상대예산계정
			// 
			this.bpctx상대예산계정.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx상대예산계정.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_BGACCT_NB_SUB;
			this.bpctx상대예산계정.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx상대예산계정.Location = new System.Drawing.Point(99, 237);
			this.bpctx상대예산계정.Name = "bpctx상대예산계정";
			this.bpctx상대예산계정.Size = new System.Drawing.Size(280, 21);
			this.bpctx상대예산계정.TabIndex = 10;
			this.bpctx상대예산계정.TabStop = false;
			this.bpctx상대예산계정.Text = "bpCodeNTextBox1";
			// 
			// bpctx회계계정
			// 
			this.bpctx회계계정.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx회계계정.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_ACCTCODE_NB_SUB;
			this.bpctx회계계정.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx회계계정.Location = new System.Drawing.Point(99, 211);
			this.bpctx회계계정.Name = "bpctx회계계정";
			this.bpctx회계계정.Size = new System.Drawing.Size(280, 21);
			this.bpctx회계계정.TabIndex = 9;
			this.bpctx회계계정.TabStop = false;
			this.bpctx회계계정.Text = "bpCodeNTextBox1";
			// 
			// bpctx예산계정
			// 
			this.bpctx예산계정.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx예산계정.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_BGACCT_NB_SUB;
			this.bpctx예산계정.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx예산계정.Location = new System.Drawing.Point(99, 185);
			this.bpctx예산계정.Name = "bpctx예산계정";
			this.bpctx예산계정.Size = new System.Drawing.Size(280, 21);
			this.bpctx예산계정.TabIndex = 8;
			this.bpctx예산계정.TabStop = false;
			this.bpctx예산계정.Text = "bpCodeNTextBox1";
			// 
			// txt문서제목
			// 
			this.txt문서제목.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt문서제목.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt문서제목.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.txt문서제목.Location = new System.Drawing.Point(99, 107);
			this.txt문서제목.Name = "txt문서제목";
			this.txt문서제목.Size = new System.Drawing.Size(280, 21);
			this.txt문서제목.TabIndex = 5;
			// 
			// panelExt7
			// 
			this.panelExt7.BackColor = System.Drawing.Color.Transparent;
			this.panelExt7.Location = new System.Drawing.Point(6, 104);
			this.panelExt7.Name = "panelExt7";
			this.panelExt7.Size = new System.Drawing.Size(375, 1);
			this.panelExt7.TabIndex = 14;
			// 
			// panelExt6
			// 
			this.panelExt6.BackColor = System.Drawing.Color.Transparent;
			this.panelExt6.Location = new System.Drawing.Point(6, 78);
			this.panelExt6.Name = "panelExt6";
			this.panelExt6.Size = new System.Drawing.Size(375, 1);
			this.panelExt6.TabIndex = 13;
			// 
			// dtp회계일자
			// 
			this.dtp회계일자.BackColor = System.Drawing.Color.White;
			this.dtp회계일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp회계일자.Location = new System.Drawing.Point(288, 81);
			this.dtp회계일자.Mask = "####/##/##";
			this.dtp회계일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.dtp회계일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp회계일자.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp회계일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp회계일자.Name = "dtp회계일자";
			this.dtp회계일자.Size = new System.Drawing.Size(91, 21);
			this.dtp회계일자.TabIndex = 4;
			this.dtp회계일자.Value = new System.DateTime(((long)(0)));
			// 
			// panelExt16
			// 
			this.panelExt16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.panelExt16.Controls.Add(this._lbl전표처리);
			this.panelExt16.Location = new System.Drawing.Point(210, 78);
			this.panelExt16.Name = "panelExt16";
			this.panelExt16.Size = new System.Drawing.Size(75, 27);
			this.panelExt16.TabIndex = 137;
			// 
			// _lbl전표처리
			// 
			this._lbl전표처리.BackColor = System.Drawing.Color.Transparent;
			this._lbl전표처리.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lbl전표처리.Location = new System.Drawing.Point(3, 5);
			this._lbl전표처리.Name = "_lbl전표처리";
			this._lbl전표처리.Size = new System.Drawing.Size(70, 18);
			this._lbl전표처리.TabIndex = 0;
			this._lbl전표처리.Tag = "";
			this._lbl전표처리.Text = "회계일자";
			this._lbl전표처리.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo결의구분
			// 
			this.cbo결의구분.AutoDropDown = true;
			this.cbo결의구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo결의구분.ItemHeight = 12;
			this.cbo결의구분.Location = new System.Drawing.Point(99, 81);
			this.cbo결의구분.Name = "cbo결의구분";
			this.cbo결의구분.Size = new System.Drawing.Size(105, 20);
			this.cbo결의구분.TabIndex = 3;
			// 
			// bpctx회계단위
			// 
			this.bpctx회계단위.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx회계단위.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PC_SUB;
			this.bpctx회계단위.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx회계단위.Location = new System.Drawing.Point(99, 3);
			this.bpctx회계단위.Name = "bpctx회계단위";
			this.bpctx회계단위.SetDefaultValue = true;
			this.bpctx회계단위.Size = new System.Drawing.Size(280, 21);
			this.bpctx회계단위.TabIndex = 0;
			this.bpctx회계단위.TabStop = false;
			this.bpctx회계단위.Text = "bpCodeNTextBox1";
			// 
			// bpctx작성부서
			// 
			this.bpctx작성부서.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx작성부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB;
			this.bpctx작성부서.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx작성부서.Location = new System.Drawing.Point(99, 29);
			this.bpctx작성부서.Name = "bpctx작성부서";
			this.bpctx작성부서.SetDefaultValue = true;
			this.bpctx작성부서.Size = new System.Drawing.Size(280, 21);
			this.bpctx작성부서.TabIndex = 1;
			this.bpctx작성부서.TabStop = false;
			this.bpctx작성부서.Text = "bpCodeNTextBox1";
			// 
			// bpctx작성사원
			// 
			this.bpctx작성사원.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx작성사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.bpctx작성사원.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx작성사원.Location = new System.Drawing.Point(99, 55);
			this.bpctx작성사원.Name = "bpctx작성사원";
			this.bpctx작성사원.SetDefaultValue = true;
			this.bpctx작성사원.Size = new System.Drawing.Size(280, 21);
			this.bpctx작성사원.TabIndex = 2;
			this.bpctx작성사원.TabStop = false;
			this.bpctx작성사원.Text = "bpCodeNTextBox1";
			// 
			// panelExt15
			// 
			this.panelExt15.BackColor = System.Drawing.Color.Transparent;
			this.panelExt15.Location = new System.Drawing.Point(6, 312);
			this.panelExt15.Name = "panelExt15";
			this.panelExt15.Size = new System.Drawing.Size(375, 1);
			this.panelExt15.TabIndex = 132;
			// 
			// panelExt14
			// 
			this.panelExt14.BackColor = System.Drawing.Color.Transparent;
			this.panelExt14.Location = new System.Drawing.Point(6, 286);
			this.panelExt14.Name = "panelExt14";
			this.panelExt14.Size = new System.Drawing.Size(375, 1);
			this.panelExt14.TabIndex = 21;
			// 
			// panelExt13
			// 
			this.panelExt13.BackColor = System.Drawing.Color.Transparent;
			this.panelExt13.Location = new System.Drawing.Point(6, 260);
			this.panelExt13.Name = "panelExt13";
			this.panelExt13.Size = new System.Drawing.Size(375, 1);
			this.panelExt13.TabIndex = 20;
			// 
			// panelExt12
			// 
			this.panelExt12.BackColor = System.Drawing.Color.Transparent;
			this.panelExt12.Location = new System.Drawing.Point(6, 234);
			this.panelExt12.Name = "panelExt12";
			this.panelExt12.Size = new System.Drawing.Size(375, 1);
			this.panelExt12.TabIndex = 19;
			// 
			// panelExt11
			// 
			this.panelExt11.BackColor = System.Drawing.Color.Transparent;
			this.panelExt11.Location = new System.Drawing.Point(6, 208);
			this.panelExt11.Name = "panelExt11";
			this.panelExt11.Size = new System.Drawing.Size(375, 1);
			this.panelExt11.TabIndex = 18;
			// 
			// panelExt10
			// 
			this.panelExt10.BackColor = System.Drawing.Color.Transparent;
			this.panelExt10.Location = new System.Drawing.Point(6, 182);
			this.panelExt10.Name = "panelExt10";
			this.panelExt10.Size = new System.Drawing.Size(375, 1);
			this.panelExt10.TabIndex = 17;
			// 
			// panelExt9
			// 
			this.panelExt9.BackColor = System.Drawing.Color.Transparent;
			this.panelExt9.Location = new System.Drawing.Point(6, 156);
			this.panelExt9.Name = "panelExt9";
			this.panelExt9.Size = new System.Drawing.Size(375, 1);
			this.panelExt9.TabIndex = 16;
			// 
			// panelExt8
			// 
			this.panelExt8.BackColor = System.Drawing.Color.Transparent;
			this.panelExt8.Location = new System.Drawing.Point(6, 130);
			this.panelExt8.Name = "panelExt8";
			this.panelExt8.Size = new System.Drawing.Size(375, 1);
			this.panelExt8.TabIndex = 15;
			// 
			// panelExt5
			// 
			this.panelExt5.BackColor = System.Drawing.Color.Transparent;
			this.panelExt5.Location = new System.Drawing.Point(6, 52);
			this.panelExt5.Name = "panelExt5";
			this.panelExt5.Size = new System.Drawing.Size(375, 1);
			this.panelExt5.TabIndex = 12;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.Transparent;
			this.panel5.Location = new System.Drawing.Point(6, 26);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(375, 1);
			this.panel5.TabIndex = 2;
			// 
			// panelExt4
			// 
			this.panelExt4.BackColor = System.Drawing.Color.MintCream;
			this.panelExt4.Controls.Add(this.lbl분개라인처리);
			this.panelExt4.Controls.Add(this.lbl코스트센터);
			this.panelExt4.Controls.Add(this.lbl상대회계계정);
			this.panelExt4.Controls.Add(this.lbl상대예산계정);
			this.panelExt4.Controls.Add(this.lbl회계계정);
			this.panelExt4.Controls.Add(this.lbl예산계정);
			this.panelExt4.Controls.Add(this.lbl사업계획);
			this.panelExt4.Controls.Add(this.lbl예산단위);
			this.panelExt4.Controls.Add(this.lbl문서제목);
			this.panelExt4.Controls.Add(this.lbl결의구분);
			this.panelExt4.Controls.Add(this.lbl작성사원);
			this.panelExt4.Controls.Add(this.lbl작성부서);
			this.panelExt4.Controls.Add(this.lbl회계단위);
			this.panelExt4.Location = new System.Drawing.Point(1, 1);
			this.panelExt4.Name = "panelExt4";
			this.panelExt4.Size = new System.Drawing.Size(95, 337);
			this.panelExt4.TabIndex = 0;
			// 
			// lbl분개라인처리
			// 
			this.lbl분개라인처리.Font = new System.Drawing.Font("굴림", 9F);
			this.lbl분개라인처리.Location = new System.Drawing.Point(3, 316);
			this.lbl분개라인처리.Name = "lbl분개라인처리";
			this.lbl분개라인처리.Size = new System.Drawing.Size(90, 18);
			this.lbl분개라인처리.TabIndex = 18;
			this.lbl분개라인처리.Text = "분개라인처리";
			this.lbl분개라인처리.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl코스트센터
			// 
			this.lbl코스트센터.Location = new System.Drawing.Point(3, 290);
			this.lbl코스트센터.Name = "lbl코스트센터";
			this.lbl코스트센터.Size = new System.Drawing.Size(90, 18);
			this.lbl코스트센터.TabIndex = 17;
			this.lbl코스트센터.Text = "코스트센터";
			this.lbl코스트센터.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl상대회계계정
			// 
			this.lbl상대회계계정.Location = new System.Drawing.Point(3, 264);
			this.lbl상대회계계정.Name = "lbl상대회계계정";
			this.lbl상대회계계정.Size = new System.Drawing.Size(90, 18);
			this.lbl상대회계계정.TabIndex = 16;
			this.lbl상대회계계정.Text = "상대회계계정";
			this.lbl상대회계계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl상대예산계정
			// 
			this.lbl상대예산계정.Location = new System.Drawing.Point(3, 238);
			this.lbl상대예산계정.Name = "lbl상대예산계정";
			this.lbl상대예산계정.Size = new System.Drawing.Size(90, 18);
			this.lbl상대예산계정.TabIndex = 15;
			this.lbl상대예산계정.Text = "상대예산계정";
			this.lbl상대예산계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl회계계정
			// 
			this.lbl회계계정.Location = new System.Drawing.Point(3, 212);
			this.lbl회계계정.Name = "lbl회계계정";
			this.lbl회계계정.Size = new System.Drawing.Size(90, 18);
			this.lbl회계계정.TabIndex = 14;
			this.lbl회계계정.Text = "회계계정";
			this.lbl회계계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl예산계정
			// 
			this.lbl예산계정.Location = new System.Drawing.Point(3, 186);
			this.lbl예산계정.Name = "lbl예산계정";
			this.lbl예산계정.Size = new System.Drawing.Size(90, 18);
			this.lbl예산계정.TabIndex = 13;
			this.lbl예산계정.Text = "예산계정";
			this.lbl예산계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl사업계획
			// 
			this.lbl사업계획.Location = new System.Drawing.Point(3, 160);
			this.lbl사업계획.Name = "lbl사업계획";
			this.lbl사업계획.Size = new System.Drawing.Size(90, 18);
			this.lbl사업계획.TabIndex = 12;
			this.lbl사업계획.Text = "사업계획";
			this.lbl사업계획.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl예산단위
			// 
			this.lbl예산단위.Location = new System.Drawing.Point(3, 134);
			this.lbl예산단위.Name = "lbl예산단위";
			this.lbl예산단위.Size = new System.Drawing.Size(90, 18);
			this.lbl예산단위.TabIndex = 11;
			this.lbl예산단위.Text = "예산단위";
			this.lbl예산단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl문서제목
			// 
			this.lbl문서제목.Location = new System.Drawing.Point(3, 108);
			this.lbl문서제목.Name = "lbl문서제목";
			this.lbl문서제목.Size = new System.Drawing.Size(90, 18);
			this.lbl문서제목.TabIndex = 10;
			this.lbl문서제목.Text = "문서제목";
			this.lbl문서제목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl결의구분
			// 
			this.lbl결의구분.Location = new System.Drawing.Point(3, 82);
			this.lbl결의구분.Name = "lbl결의구분";
			this.lbl결의구분.Size = new System.Drawing.Size(90, 18);
			this.lbl결의구분.TabIndex = 9;
			this.lbl결의구분.Text = "결의구분";
			this.lbl결의구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl작성사원
			// 
			this.lbl작성사원.Location = new System.Drawing.Point(3, 56);
			this.lbl작성사원.Name = "lbl작성사원";
			this.lbl작성사원.Size = new System.Drawing.Size(90, 18);
			this.lbl작성사원.TabIndex = 8;
			this.lbl작성사원.Text = "작성사원";
			this.lbl작성사원.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl작성부서
			// 
			this.lbl작성부서.Location = new System.Drawing.Point(3, 30);
			this.lbl작성부서.Name = "lbl작성부서";
			this.lbl작성부서.Size = new System.Drawing.Size(90, 18);
			this.lbl작성부서.TabIndex = 7;
			this.lbl작성부서.Text = "작성부서";
			this.lbl작성부서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl회계단위
			// 
			this.lbl회계단위.Location = new System.Drawing.Point(3, 4);
			this.lbl회계단위.Name = "lbl회계단위";
			this.lbl회계단위.Size = new System.Drawing.Size(90, 18);
			this.lbl회계단위.TabIndex = 6;
			this.lbl회계단위.Text = "회계단위";
			this.lbl회계단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelExt1
			// 
			this.panelExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelExt1.Controls.Add(this.btnOK);
			this.panelExt1.Controls.Add(this.btnEnd);
			this.panelExt1.Controls.Add(this.panelExt2);
			this.panelExt1.Location = new System.Drawing.Point(10, 41);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(384, 27);
			this.panelExt1.TabIndex = 23;
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.White;
			this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Location = new System.Drawing.Point(250, 2);
			this.btnOK.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(70, 19);
			this.btnOK.TabIndex = 35;
			this.btnOK.TabStop = false;
			this.btnOK.Text = "전표처리";
			this.btnOK.UseVisualStyleBackColor = false;
			// 
			// btnEnd
			// 
			this.btnEnd.BackColor = System.Drawing.Color.White;
			this.btnEnd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEnd.Location = new System.Drawing.Point(321, 2);
			this.btnEnd.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnEnd.Name = "btnEnd";
			this.btnEnd.Size = new System.Drawing.Size(60, 19);
			this.btnEnd.TabIndex = 34;
			this.btnEnd.TabStop = false;
			this.btnEnd.Text = "종료";
			this.btnEnd.UseVisualStyleBackColor = false;
			// 
			// panelExt2
			// 
			this.panelExt2.Controls.Add(this.lbl전표처리);
			this.panelExt2.Location = new System.Drawing.Point(1, 1);
			this.panelExt2.Name = "panelExt2";
			this.panelExt2.Size = new System.Drawing.Size(245, 23);
			this.panelExt2.TabIndex = 31;
			// 
			// lbl전표처리
			// 
			this.lbl전표처리.BackColor = System.Drawing.Color.Transparent;
			this.lbl전표처리.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl전표처리.Location = new System.Drawing.Point(20, 4);
			this.lbl전표처리.Name = "lbl전표처리";
			this.lbl전표처리.Size = new System.Drawing.Size(100, 16);
			this.lbl전표처리.TabIndex = 0;
			this.lbl전표처리.Text = "전표처리";
			this.lbl전표처리.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_titlePanel
			// 
			this.m_titlePanel.BackColor = System.Drawing.Color.White;
			this.m_titlePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_titlePanel.BackgroundImage")));
			this.m_titlePanel.Controls.Add(this.panel3);
			this.m_titlePanel.Controls.Add(this.panel1);
			this.m_titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_titlePanel.ForeColor = System.Drawing.Color.Black;
			this.m_titlePanel.Location = new System.Drawing.Point(0, 0);
			this.m_titlePanel.Name = "m_titlePanel";
			this.m_titlePanel.Size = new System.Drawing.Size(404, 33);
			this.m_titlePanel.TabIndex = 22;
			// 
			// panel3
			// 
			this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
			this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel3.Location = new System.Drawing.Point(305, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(99, 33);
			this.panel3.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.lblTitle);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(210, 33);
			this.panel1.TabIndex = 2;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(44)))), ((int)(((byte)(80)))));
			this.lblTitle.Location = new System.Drawing.Point(35, 10);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(71, 15);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "전표처리";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// P_CZ_FI_CARD_EPDOCU_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(404, 425);
			this.Controls.Add(this.pnlDocu);
			this.Controls.Add(this.panelExt1);
			this.Controls.Add(this.m_titlePanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "P_CZ_FI_CARD_EPDOCU_SUB";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ERP iU";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.pnlDocu.ResumeLayout(false);
			this.pnlDocu.PerformLayout();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rdo일괄)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo건별)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtp회계일자)).EndInit();
			this.panelExt16.ResumeLayout(false);
			this.panelExt4.ResumeLayout(false);
			this.panelExt1.ResumeLayout(false);
			this.panelExt2.ResumeLayout(false);
			this.m_titlePanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        private PanelExt pnlDocu;
        private PanelExt panelExt14;
        private PanelExt panelExt13;
        private PanelExt panelExt12;
        private PanelExt panelExt11;
        private PanelExt panelExt10;
        private PanelExt panelExt9;
        private PanelExt panelExt8;
        private PanelExt panelExt7;
        private PanelExt panelExt6;
        private PanelExt panelExt5;
        private PanelExt panel5;
        private PanelExt panelExt4;
        private LabelExt lbl코스트센터;
        private LabelExt lbl상대회계계정;
        private LabelExt lbl상대예산계정;
        private LabelExt lbl회계계정;
        private LabelExt lbl예산계정;
        private LabelExt lbl사업계획;
        private LabelExt lbl예산단위;
        private LabelExt lbl문서제목;
        private LabelExt lbl결의구분;
        private LabelExt lbl작성사원;
        private LabelExt lbl작성부서;
        private LabelExt lbl회계단위;
        private PanelExt panelExt1;
        private RoundedButton btnOK;
        private RoundedButton btnEnd;
        private PanelExt panelExt2;
        private LabelExt lbl전표처리;
        private Panel m_titlePanel;
        private Panel panel3;
        private Panel panel1;
        private Label lblTitle;
        private PanelExt panelExt15;
        private LabelExt lbl분개라인처리;
        private BpCodeNTextBox bpctx회계단위;
        private BpCodeNTextBox bpctx작성부서;
        private BpCodeNTextBox bpctx작성사원;
        private DropDownComboBox cbo결의구분;
        private PanelExt panelExt16;
        private LabelExt _lbl전표처리;
        private DatePicker dtp회계일자;
        private TextBoxExt txt문서제목;
        private BpCodeNTextBox bpctx상대회계계정;
        private BpCodeNTextBox bpctx상대예산계정;
        private BpCodeNTextBox bpctx회계계정;
        private BpCodeNTextBox bpctx예산계정;
        private BpCodeNTextBox bpctx코스트센터;
        private Panel panel2;
        private RadioButtonExt rdo일괄;
        private RadioButtonExt rdo건별;
        private BpCodeNTextBox bpctx예산단위;
        private BpCodeNTextBox bpctx사업계획;

        #endregion
    }
}