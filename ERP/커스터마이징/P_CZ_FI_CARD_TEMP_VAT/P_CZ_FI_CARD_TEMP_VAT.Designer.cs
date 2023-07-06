using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_FI_CARD_TEMP_VAT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_CARD_TEMP_VAT));
			this.cbo카드사명 = new Duzon.Common.Controls.DropDownComboBox();
			this.cbo전표승인여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl카드사명 = new Duzon.Common.Controls.LabelExt();
			this.cbo승인구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.cbo부가세구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.ctx회계단위 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpc카드번호 = new Duzon.Common.BpControls.BpComboBox();
			this.bpc작성부서 = new Duzon.Common.BpControls.BpComboBox();
			this.cbo전표처리 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl전표승인여부 = new Duzon.Common.Controls.LabelExt();
			this.lbl승인번호 = new Duzon.Common.Controls.LabelExt();
			this.lbl카드번호 = new Duzon.Common.Controls.LabelExt();
			this.lbl청구년월 = new Duzon.Common.Controls.LabelExt();
			this.lbl승인일자 = new Duzon.Common.Controls.LabelExt();
			this.lbl회계단위 = new Duzon.Common.Controls.LabelExt();
			this.lbl전표처리 = new Duzon.Common.Controls.LabelExt();
			this.lbl부가세구분 = new Duzon.Common.Controls.LabelExt();
			this.lbl작성부서 = new Duzon.Common.Controls.LabelExt();
			this.btn전표처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn부가세미처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn부가세처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표처리미적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표처리적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn데이터확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn일괄복사 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn거래처등록 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전자결재 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPnl_DT = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp승인일자 = new Duzon.Common.Controls.PeriodPicker();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
			this.bppnl청구년월 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp청구년월 = new Duzon.Common.Controls.PeriodPicker();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bppnl그룹웨어처리 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo그룹웨어처리 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl그룹웨어처리 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bppnlCustCombo = new Duzon.Common.BpControls.BpPanelControl();
			this.cboCust = new Duzon.Common.Controls.DropDownComboBox();
			this.lblCustCombo = new Duzon.Common.Controls.LabelExt();
			this.bppnl비용계정 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc비용계정 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl비용계정 = new Duzon.Common.Controls.LabelExt();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem6 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx증빙 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl증빙 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.rdo여 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo부 = new Duzon.Common.Controls.RadioButtonExt();
			this.lbl봉사료전표처리 = new Duzon.Common.Controls.LabelExt();
			this.btn업종적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn데이터삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn결의서 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn카드전표마감 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전용4자 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표번호매핑 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnVAT제외계정등록 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전용버튼6자 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전용버튼8자 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전용버튼10자 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.roundedButton1 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnVAT제외계정 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPnl_DT.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl11.SuspendLayout();
			this.bpPanelControl10.SuspendLayout();
			this.bppnl청구년월.SuspendLayout();
			this.oneGridItem4.SuspendLayout();
			this.bppnl그룹웨어처리.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			this.oneGridItem5.SuspendLayout();
			this.bppnlCustCombo.SuspendLayout();
			this.bppnl비용계정.SuspendLayout();
			this.oneGridItem6.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo여)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo부)).BeginInit();
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1620, 773);
			// 
			// cbo카드사명
			// 
			this.cbo카드사명.AutoDropDown = true;
			this.cbo카드사명.BackColor = System.Drawing.Color.White;
			this.cbo카드사명.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo카드사명.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo카드사명.ItemHeight = 12;
			this.cbo카드사명.Location = new System.Drawing.Point(106, 0);
			this.cbo카드사명.Name = "cbo카드사명";
			this.cbo카드사명.Size = new System.Drawing.Size(186, 20);
			this.cbo카드사명.TabIndex = 130;
			// 
			// cbo전표승인여부
			// 
			this.cbo전표승인여부.AutoDropDown = true;
			this.cbo전표승인여부.BackColor = System.Drawing.Color.White;
			this.cbo전표승인여부.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo전표승인여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo전표승인여부.ItemHeight = 12;
			this.cbo전표승인여부.Location = new System.Drawing.Point(106, 0);
			this.cbo전표승인여부.Name = "cbo전표승인여부";
			this.cbo전표승인여부.Size = new System.Drawing.Size(186, 20);
			this.cbo전표승인여부.TabIndex = 129;
			// 
			// lbl카드사명
			// 
			this.lbl카드사명.BackColor = System.Drawing.Color.Transparent;
			this.lbl카드사명.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl카드사명.Location = new System.Drawing.Point(0, 0);
			this.lbl카드사명.Name = "lbl카드사명";
			this.lbl카드사명.Size = new System.Drawing.Size(100, 23);
			this.lbl카드사명.TabIndex = 0;
			this.lbl카드사명.Text = "카드사명";
			this.lbl카드사명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo승인구분
			// 
			this.cbo승인구분.AutoDropDown = true;
			this.cbo승인구분.BackColor = System.Drawing.Color.White;
			this.cbo승인구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo승인구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo승인구분.ItemHeight = 12;
			this.cbo승인구분.Location = new System.Drawing.Point(106, 0);
			this.cbo승인구분.Name = "cbo승인구분";
			this.cbo승인구분.Size = new System.Drawing.Size(186, 20);
			this.cbo승인구분.TabIndex = 125;
			// 
			// cbo부가세구분
			// 
			this.cbo부가세구분.AutoDropDown = true;
			this.cbo부가세구분.BackColor = System.Drawing.Color.White;
			this.cbo부가세구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo부가세구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo부가세구분.ItemHeight = 12;
			this.cbo부가세구분.Location = new System.Drawing.Point(106, 0);
			this.cbo부가세구분.Name = "cbo부가세구분";
			this.cbo부가세구분.Size = new System.Drawing.Size(186, 20);
			this.cbo부가세구분.TabIndex = 5;
			// 
			// ctx회계단위
			// 
			this.ctx회계단위.BackColor = System.Drawing.Color.White;
			this.ctx회계단위.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx회계단위.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PC_SUB;
			this.ctx회계단위.Location = new System.Drawing.Point(106, 0);
			this.ctx회계단위.Name = "ctx회계단위";
			this.ctx회계단위.SetDefaultValue = true;
			this.ctx회계단위.Size = new System.Drawing.Size(186, 21);
			this.ctx회계단위.TabIndex = 0;
			this.ctx회계단위.TabStop = false;
			this.ctx회계단위.Text = "bpCodeTextBox1";
			// 
			// bpc카드번호
			// 
			this.bpc카드번호.ComboCheck = false;
			this.bpc카드번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc카드번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc카드번호.ItemBackColor = System.Drawing.Color.White;
			this.bpc카드번호.Location = new System.Drawing.Point(106, 0);
			this.bpc카드번호.Name = "bpc카드번호";
			this.bpc카드번호.Size = new System.Drawing.Size(186, 21);
			this.bpc카드번호.TabIndex = 2;
			this.bpc카드번호.TabStop = false;
			this.bpc카드번호.UserCodeName = "NM_CARD";
			this.bpc카드번호.UserCodeValue = "ACCT_NO";
			this.bpc카드번호.UserHelpID = "H_FI_CARD_DEPT";
			// 
			// bpc작성부서
			// 
			this.bpc작성부서.ComboCheck = false;
			this.bpc작성부서.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc작성부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB1;
			this.bpc작성부서.ItemBackColor = System.Drawing.Color.White;
			this.bpc작성부서.Location = new System.Drawing.Point(106, 0);
			this.bpc작성부서.Name = "bpc작성부서";
			this.bpc작성부서.SetDefaultValue = true;
			this.bpc작성부서.Size = new System.Drawing.Size(186, 21);
			this.bpc작성부서.TabIndex = 1;
			this.bpc작성부서.TabStop = false;
			// 
			// cbo전표처리
			// 
			this.cbo전표처리.AutoDropDown = true;
			this.cbo전표처리.BackColor = System.Drawing.Color.White;
			this.cbo전표처리.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo전표처리.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo전표처리.ItemHeight = 12;
			this.cbo전표처리.Location = new System.Drawing.Point(106, 0);
			this.cbo전표처리.Name = "cbo전표처리";
			this.cbo전표처리.Size = new System.Drawing.Size(186, 20);
			this.cbo전표처리.TabIndex = 6;
			// 
			// lbl전표승인여부
			// 
			this.lbl전표승인여부.BackColor = System.Drawing.Color.Transparent;
			this.lbl전표승인여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl전표승인여부.Location = new System.Drawing.Point(0, 0);
			this.lbl전표승인여부.Name = "lbl전표승인여부";
			this.lbl전표승인여부.Size = new System.Drawing.Size(100, 23);
			this.lbl전표승인여부.TabIndex = 3;
			this.lbl전표승인여부.Text = "전표승인여부";
			this.lbl전표승인여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl승인번호
			// 
			this.lbl승인번호.BackColor = System.Drawing.Color.Transparent;
			this.lbl승인번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl승인번호.Location = new System.Drawing.Point(0, 0);
			this.lbl승인번호.Name = "lbl승인번호";
			this.lbl승인번호.Size = new System.Drawing.Size(100, 23);
			this.lbl승인번호.TabIndex = 2;
			this.lbl승인번호.Text = "승인구분";
			this.lbl승인번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl카드번호
			// 
			this.lbl카드번호.BackColor = System.Drawing.Color.Transparent;
			this.lbl카드번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl카드번호.Location = new System.Drawing.Point(0, 0);
			this.lbl카드번호.Name = "lbl카드번호";
			this.lbl카드번호.Size = new System.Drawing.Size(100, 23);
			this.lbl카드번호.TabIndex = 0;
			this.lbl카드번호.Text = "카드번호";
			this.lbl카드번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl청구년월
			// 
			this.lbl청구년월.BackColor = System.Drawing.Color.Transparent;
			this.lbl청구년월.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl청구년월.Location = new System.Drawing.Point(0, 0);
			this.lbl청구년월.Name = "lbl청구년월";
			this.lbl청구년월.Size = new System.Drawing.Size(100, 23);
			this.lbl청구년월.TabIndex = 2;
			this.lbl청구년월.Text = "청구년월";
			this.lbl청구년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl승인일자
			// 
			this.lbl승인일자.BackColor = System.Drawing.Color.Transparent;
			this.lbl승인일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl승인일자.Location = new System.Drawing.Point(0, 0);
			this.lbl승인일자.Name = "lbl승인일자";
			this.lbl승인일자.Size = new System.Drawing.Size(100, 23);
			this.lbl승인일자.TabIndex = 1;
			this.lbl승인일자.Text = "승인일자";
			this.lbl승인일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl회계단위
			// 
			this.lbl회계단위.BackColor = System.Drawing.Color.Transparent;
			this.lbl회계단위.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl회계단위.Location = new System.Drawing.Point(0, 0);
			this.lbl회계단위.Name = "lbl회계단위";
			this.lbl회계단위.Size = new System.Drawing.Size(100, 23);
			this.lbl회계단위.TabIndex = 0;
			this.lbl회계단위.Text = "회계단위";
			this.lbl회계단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl전표처리
			// 
			this.lbl전표처리.BackColor = System.Drawing.Color.Transparent;
			this.lbl전표처리.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl전표처리.Location = new System.Drawing.Point(0, 0);
			this.lbl전표처리.Name = "lbl전표처리";
			this.lbl전표처리.Size = new System.Drawing.Size(100, 23);
			this.lbl전표처리.TabIndex = 3;
			this.lbl전표처리.Text = "전표처리";
			this.lbl전표처리.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl부가세구분
			// 
			this.lbl부가세구분.BackColor = System.Drawing.Color.Transparent;
			this.lbl부가세구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl부가세구분.Location = new System.Drawing.Point(0, 0);
			this.lbl부가세구분.Name = "lbl부가세구분";
			this.lbl부가세구분.Size = new System.Drawing.Size(100, 23);
			this.lbl부가세구분.TabIndex = 1;
			this.lbl부가세구분.Text = "부가세구분";
			this.lbl부가세구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl작성부서
			// 
			this.lbl작성부서.BackColor = System.Drawing.Color.Transparent;
			this.lbl작성부서.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작성부서.Location = new System.Drawing.Point(0, 0);
			this.lbl작성부서.Name = "lbl작성부서";
			this.lbl작성부서.Size = new System.Drawing.Size(100, 23);
			this.lbl작성부서.TabIndex = 0;
			this.lbl작성부서.Text = "작성부서";
			this.lbl작성부서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn전표처리
			// 
			this.btn전표처리.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전표처리.BackColor = System.Drawing.Color.White;
			this.btn전표처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표처리.Location = new System.Drawing.Point(1416, 3);
			this.btn전표처리.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전표처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표처리.Name = "btn전표처리";
			this.btn전표처리.Size = new System.Drawing.Size(64, 19);
			this.btn전표처리.TabIndex = 151;
			this.btn전표처리.TabStop = false;
			this.btn전표처리.Text = "전표처리";
			this.btn전표처리.UseVisualStyleBackColor = false;
			// 
			// btn전표취소
			// 
			this.btn전표취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전표취소.BackColor = System.Drawing.Color.White;
			this.btn전표취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표취소.Location = new System.Drawing.Point(1483, 3);
			this.btn전표취소.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전표취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표취소.Name = "btn전표취소";
			this.btn전표취소.Size = new System.Drawing.Size(64, 19);
			this.btn전표취소.TabIndex = 150;
			this.btn전표취소.TabStop = false;
			this.btn전표취소.Text = "전표취소";
			this.btn전표취소.UseVisualStyleBackColor = false;
			// 
			// btn전표조회
			// 
			this.btn전표조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전표조회.BackColor = System.Drawing.Color.White;
			this.btn전표조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표조회.Location = new System.Drawing.Point(1550, 3);
			this.btn전표조회.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전표조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표조회.Name = "btn전표조회";
			this.btn전표조회.Size = new System.Drawing.Size(64, 19);
			this.btn전표조회.TabIndex = 149;
			this.btn전표조회.TabStop = false;
			this.btn전표조회.Text = "전표조회";
			this.btn전표조회.UseVisualStyleBackColor = false;
			this.btn전표조회.Visible = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1620, 773);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 217);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(1614, 553);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 4;
			this._flex.UseGridCalculator = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn전표조회);
			this.flowLayoutPanel1.Controls.Add(this.btn전표취소);
			this.flowLayoutPanel1.Controls.Add(this.btn전표처리);
			this.flowLayoutPanel1.Controls.Add(this.btn부가세미처리);
			this.flowLayoutPanel1.Controls.Add(this.btn부가세처리);
			this.flowLayoutPanel1.Controls.Add(this.btn전표처리미적용);
			this.flowLayoutPanel1.Controls.Add(this.btn전표처리적용);
			this.flowLayoutPanel1.Controls.Add(this.btn데이터확인);
			this.flowLayoutPanel1.Controls.Add(this.btn일괄복사);
			this.flowLayoutPanel1.Controls.Add(this.btn거래처등록);
			this.flowLayoutPanel1.Controls.Add(this.btn전자결재);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1614, 25);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btn부가세미처리
			// 
			this.btn부가세미처리.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn부가세미처리.BackColor = System.Drawing.Color.White;
			this.btn부가세미처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn부가세미처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn부가세미처리.Location = new System.Drawing.Point(1324, 3);
			this.btn부가세미처리.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn부가세미처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn부가세미처리.Name = "btn부가세미처리";
			this.btn부가세미처리.Size = new System.Drawing.Size(89, 19);
			this.btn부가세미처리.TabIndex = 153;
			this.btn부가세미처리.TabStop = false;
			this.btn부가세미처리.Text = "부가세미처리";
			this.btn부가세미처리.UseVisualStyleBackColor = false;
			// 
			// btn부가세처리
			// 
			this.btn부가세처리.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn부가세처리.BackColor = System.Drawing.Color.White;
			this.btn부가세처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn부가세처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn부가세처리.Location = new System.Drawing.Point(1243, 3);
			this.btn부가세처리.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn부가세처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn부가세처리.Name = "btn부가세처리";
			this.btn부가세처리.Size = new System.Drawing.Size(78, 19);
			this.btn부가세처리.TabIndex = 152;
			this.btn부가세처리.TabStop = false;
			this.btn부가세처리.Text = "부가세처리";
			this.btn부가세처리.UseVisualStyleBackColor = false;
			// 
			// btn전표처리미적용
			// 
			this.btn전표처리미적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전표처리미적용.BackColor = System.Drawing.Color.White;
			this.btn전표처리미적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표처리미적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표처리미적용.Location = new System.Drawing.Point(1128, 3);
			this.btn전표처리미적용.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전표처리미적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표처리미적용.Name = "btn전표처리미적용";
			this.btn전표처리미적용.Size = new System.Drawing.Size(112, 19);
			this.btn전표처리미적용.TabIndex = 154;
			this.btn전표처리미적용.TabStop = false;
			this.btn전표처리미적용.Text = "전표미처리적용";
			this.btn전표처리미적용.UseVisualStyleBackColor = false;
			// 
			// btn전표처리적용
			// 
			this.btn전표처리적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전표처리적용.BackColor = System.Drawing.Color.White;
			this.btn전표처리적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표처리적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표처리적용.Location = new System.Drawing.Point(1036, 3);
			this.btn전표처리적용.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전표처리적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표처리적용.Name = "btn전표처리적용";
			this.btn전표처리적용.Size = new System.Drawing.Size(89, 19);
			this.btn전표처리적용.TabIndex = 155;
			this.btn전표처리적용.TabStop = false;
			this.btn전표처리적용.Text = "전표처리적용";
			this.btn전표처리적용.UseVisualStyleBackColor = false;
			// 
			// btn데이터확인
			// 
			this.btn데이터확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn데이터확인.BackColor = System.Drawing.Color.White;
			this.btn데이터확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn데이터확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn데이터확인.Location = new System.Drawing.Point(955, 3);
			this.btn데이터확인.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn데이터확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn데이터확인.Name = "btn데이터확인";
			this.btn데이터확인.Size = new System.Drawing.Size(78, 19);
			this.btn데이터확인.TabIndex = 156;
			this.btn데이터확인.TabStop = false;
			this.btn데이터확인.Text = "데이터확인";
			this.btn데이터확인.UseVisualStyleBackColor = false;
			// 
			// btn일괄복사
			// 
			this.btn일괄복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn일괄복사.BackColor = System.Drawing.Color.Transparent;
			this.btn일괄복사.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn일괄복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn일괄복사.Location = new System.Drawing.Point(888, 3);
			this.btn일괄복사.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn일괄복사.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn일괄복사.Name = "btn일괄복사";
			this.btn일괄복사.Size = new System.Drawing.Size(64, 19);
			this.btn일괄복사.TabIndex = 166;
			this.btn일괄복사.TabStop = false;
			this.btn일괄복사.Text = "일괄복사";
			this.btn일괄복사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn일괄복사.UseVisualStyleBackColor = true;
			this.btn일괄복사.Visible = false;
			// 
			// btn거래처등록
			// 
			this.btn거래처등록.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn거래처등록.BackColor = System.Drawing.Color.Transparent;
			this.btn거래처등록.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn거래처등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn거래처등록.Location = new System.Drawing.Point(807, 3);
			this.btn거래처등록.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn거래처등록.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn거래처등록.Name = "btn거래처등록";
			this.btn거래처등록.Size = new System.Drawing.Size(78, 19);
			this.btn거래처등록.TabIndex = 167;
			this.btn거래처등록.TabStop = false;
			this.btn거래처등록.Text = "거래처등록";
			this.btn거래처등록.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn거래처등록.UseVisualStyleBackColor = true;
			this.btn거래처등록.Visible = false;
			// 
			// btn전자결재
			// 
			this.btn전자결재.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전자결재.BackColor = System.Drawing.Color.Transparent;
			this.btn전자결재.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전자결재.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전자결재.Location = new System.Drawing.Point(726, 3);
			this.btn전자결재.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전자결재.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전자결재.Name = "btn전자결재";
			this.btn전자결재.Size = new System.Drawing.Size(78, 19);
			this.btn전자결재.TabIndex = 168;
			this.btn전자결재.TabStop = false;
			this.btn전자결재.Text = "전자결재";
			this.btn전자결재.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn전자결재.UseVisualStyleBackColor = true;
			// 
			// oneGrid
			// 
			this.oneGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3,
            this.oneGridItem4,
            this.oneGridItem5});
			this.oneGrid.Location = new System.Drawing.Point(3, 34);
			this.oneGrid.Name = "oneGrid";
			this.oneGrid.Size = new System.Drawing.Size(1614, 131);
			this.oneGrid.TabIndex = 2;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1604, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl카드번호);
			this.bpPanelControl3.Controls.Add(this.bpc카드번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.lbl작성부서);
			this.bpPanelControl2.Controls.Add(this.bpc작성부서);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl회계단위);
			this.bpPanelControl1.Controls.Add(this.ctx회계단위);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl7);
			this.oneGridItem2.Controls.Add(this.bpPanelControl6);
			this.oneGridItem2.Controls.Add(this.bpPnl_DT);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1604, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.lbl승인번호);
			this.bpPanelControl7.Controls.Add(this.cbo승인구분);
			this.bpPanelControl7.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl7.TabIndex = 3;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.lbl부가세구분);
			this.bpPanelControl6.Controls.Add(this.cbo부가세구분);
			this.bpPanelControl6.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl6.TabIndex = 2;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// bpPnl_DT
			// 
			this.bpPnl_DT.Controls.Add(this.dtp승인일자);
			this.bpPnl_DT.Controls.Add(this.lbl승인일자);
			this.bpPnl_DT.Location = new System.Drawing.Point(2, 1);
			this.bpPnl_DT.Name = "bpPnl_DT";
			this.bpPnl_DT.Size = new System.Drawing.Size(292, 23);
			this.bpPnl_DT.TabIndex = 0;
			this.bpPnl_DT.Text = "bpPanelControl4";
			// 
			// dtp승인일자
			// 
			this.dtp승인일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp승인일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp승인일자.IsNecessaryCondition = true;
			this.dtp승인일자.Location = new System.Drawing.Point(107, 0);
			this.dtp승인일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp승인일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp승인일자.Name = "dtp승인일자";
			this.dtp승인일자.Size = new System.Drawing.Size(185, 21);
			this.dtp승인일자.TabIndex = 5;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl11);
			this.oneGridItem3.Controls.Add(this.bpPanelControl10);
			this.oneGridItem3.Controls.Add(this.bppnl청구년월);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1604, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl11
			// 
			this.bpPanelControl11.Controls.Add(this.lbl전표승인여부);
			this.bpPanelControl11.Controls.Add(this.cbo전표승인여부);
			this.bpPanelControl11.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl11.Name = "bpPanelControl11";
			this.bpPanelControl11.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl11.TabIndex = 4;
			this.bpPanelControl11.Text = "bpPanelControl11";
			// 
			// bpPanelControl10
			// 
			this.bpPanelControl10.Controls.Add(this.lbl전표처리);
			this.bpPanelControl10.Controls.Add(this.cbo전표처리);
			this.bpPanelControl10.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl10.Name = "bpPanelControl10";
			this.bpPanelControl10.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl10.TabIndex = 3;
			this.bpPanelControl10.Text = "bpPanelControl10";
			// 
			// bppnl청구년월
			// 
			this.bppnl청구년월.Controls.Add(this.dtp청구년월);
			this.bppnl청구년월.Controls.Add(this.lbl청구년월);
			this.bppnl청구년월.Location = new System.Drawing.Point(2, 1);
			this.bppnl청구년월.Name = "bppnl청구년월";
			this.bppnl청구년월.Size = new System.Drawing.Size(292, 23);
			this.bppnl청구년월.TabIndex = 2;
			this.bppnl청구년월.Text = "bpPanelControl9";
			// 
			// dtp청구년월
			// 
			this.dtp청구년월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp청구년월.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp청구년월.Location = new System.Drawing.Point(107, 0);
			this.dtp청구년월.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp청구년월.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp청구년월.Name = "dtp청구년월";
			this.dtp청구년월.Size = new System.Drawing.Size(185, 21);
			this.dtp청구년월.TabIndex = 6;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.Controls.Add(this.bppnl그룹웨어처리);
			this.oneGridItem4.Controls.Add(this.bpPanelControl12);
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(1604, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 3;
			// 
			// bppnl그룹웨어처리
			// 
			this.bppnl그룹웨어처리.Controls.Add(this.cbo그룹웨어처리);
			this.bppnl그룹웨어처리.Controls.Add(this.lbl그룹웨어처리);
			this.bppnl그룹웨어처리.Location = new System.Drawing.Point(296, 1);
			this.bppnl그룹웨어처리.Name = "bppnl그룹웨어처리";
			this.bppnl그룹웨어처리.Size = new System.Drawing.Size(292, 23);
			this.bppnl그룹웨어처리.TabIndex = 3;
			this.bppnl그룹웨어처리.Text = "bpPanelControl13";
			// 
			// cbo그룹웨어처리
			// 
			this.cbo그룹웨어처리.AutoDropDown = true;
			this.cbo그룹웨어처리.BackColor = System.Drawing.Color.White;
			this.cbo그룹웨어처리.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo그룹웨어처리.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo그룹웨어처리.ItemHeight = 12;
			this.cbo그룹웨어처리.Location = new System.Drawing.Point(106, 0);
			this.cbo그룹웨어처리.Name = "cbo그룹웨어처리";
			this.cbo그룹웨어처리.Size = new System.Drawing.Size(186, 20);
			this.cbo그룹웨어처리.TabIndex = 130;
			// 
			// lbl그룹웨어처리
			// 
			this.lbl그룹웨어처리.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl그룹웨어처리.Location = new System.Drawing.Point(0, 0);
			this.lbl그룹웨어처리.Name = "lbl그룹웨어처리";
			this.lbl그룹웨어처리.Size = new System.Drawing.Size(100, 23);
			this.lbl그룹웨어처리.TabIndex = 0;
			this.lbl그룹웨어처리.Text = "그룹웨어처리";
			this.lbl그룹웨어처리.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.lbl카드사명);
			this.bpPanelControl12.Controls.Add(this.cbo카드사명);
			this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl12.TabIndex = 2;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// oneGridItem5
			// 
			this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem5.Controls.Add(this.bppnlCustCombo);
			this.oneGridItem5.Controls.Add(this.bppnl비용계정);
			this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem5.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem5.Name = "oneGridItem5";
			this.oneGridItem5.Size = new System.Drawing.Size(1604, 23);
			this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem5.TabIndex = 4;
			// 
			// bppnlCustCombo
			// 
			this.bppnlCustCombo.Controls.Add(this.cboCust);
			this.bppnlCustCombo.Controls.Add(this.lblCustCombo);
			this.bppnlCustCombo.Location = new System.Drawing.Point(296, 1);
			this.bppnlCustCombo.Name = "bppnlCustCombo";
			this.bppnlCustCombo.Size = new System.Drawing.Size(292, 23);
			this.bppnlCustCombo.TabIndex = 5;
			this.bppnlCustCombo.Text = "bpPanelControl14";
			this.bppnlCustCombo.Visible = false;
			// 
			// cboCust
			// 
			this.cboCust.AutoDropDown = true;
			this.cboCust.BackColor = System.Drawing.Color.White;
			this.cboCust.Dock = System.Windows.Forms.DockStyle.Right;
			this.cboCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCust.ItemHeight = 12;
			this.cboCust.Location = new System.Drawing.Point(106, 0);
			this.cboCust.Name = "cboCust";
			this.cboCust.Size = new System.Drawing.Size(186, 20);
			this.cboCust.TabIndex = 132;
			// 
			// lblCustCombo
			// 
			this.lblCustCombo.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblCustCombo.Location = new System.Drawing.Point(0, 0);
			this.lblCustCombo.Name = "lblCustCombo";
			this.lblCustCombo.Size = new System.Drawing.Size(100, 23);
			this.lblCustCombo.TabIndex = 131;
			this.lblCustCombo.Text = "lblCustCombo";
			this.lblCustCombo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bppnl비용계정
			// 
			this.bppnl비용계정.Controls.Add(this.bpc비용계정);
			this.bppnl비용계정.Controls.Add(this.lbl비용계정);
			this.bppnl비용계정.Location = new System.Drawing.Point(2, 1);
			this.bppnl비용계정.Name = "bppnl비용계정";
			this.bppnl비용계정.Size = new System.Drawing.Size(292, 23);
			this.bppnl비용계정.TabIndex = 4;
			this.bppnl비용계정.Text = "bpPanelControl4";
			this.bppnl비용계정.Visible = false;
			// 
			// bpc비용계정
			// 
			this.bpc비용계정.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc비용계정.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_ACCTCODE_SUB1;
			this.bpc비용계정.Location = new System.Drawing.Point(106, 0);
			this.bpc비용계정.Name = "bpc비용계정";
			this.bpc비용계정.Size = new System.Drawing.Size(186, 21);
			this.bpc비용계정.TabIndex = 5;
			this.bpc비용계정.TabStop = false;
			this.bpc비용계정.Text = "bpComboBox1";
			// 
			// lbl비용계정
			// 
			this.lbl비용계정.BackColor = System.Drawing.Color.Transparent;
			this.lbl비용계정.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl비용계정.Location = new System.Drawing.Point(0, 0);
			this.lbl비용계정.Name = "lbl비용계정";
			this.lbl비용계정.Size = new System.Drawing.Size(100, 23);
			this.lbl비용계정.TabIndex = 4;
			this.lbl비용계정.Text = "비용계정";
			this.lbl비용계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem6});
			this.oneGrid1.Location = new System.Drawing.Point(3, 171);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1614, 40);
			this.oneGrid1.TabIndex = 5;
			// 
			// oneGridItem6
			// 
			this.oneGridItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem6.Controls.Add(this.bpPanelControl5);
			this.oneGridItem6.Controls.Add(this.bpPanelControl4);
			this.oneGridItem6.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem6.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem6.Name = "oneGridItem6";
			this.oneGridItem6.Size = new System.Drawing.Size(1604, 23);
			this.oneGridItem6.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem6.TabIndex = 0;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.ctx증빙);
			this.bpPanelControl5.Controls.Add(this.lbl증빙);
			this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl5.TabIndex = 1;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// ctx증빙
			// 
			this.ctx증빙.BackColor = System.Drawing.Color.White;
			this.ctx증빙.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx증빙.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx증빙.Location = new System.Drawing.Point(106, 0);
			this.ctx증빙.Name = "ctx증빙";
			this.ctx증빙.SetDefaultValue = true;
			this.ctx증빙.Size = new System.Drawing.Size(186, 21);
			this.ctx증빙.TabIndex = 149;
			this.ctx증빙.TabStop = false;
			this.ctx증빙.Text = "bpCodeTextBox1";
			// 
			// lbl증빙
			// 
			this.lbl증빙.BackColor = System.Drawing.Color.Transparent;
			this.lbl증빙.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl증빙.Location = new System.Drawing.Point(0, 0);
			this.lbl증빙.Name = "lbl증빙";
			this.lbl증빙.Size = new System.Drawing.Size(100, 23);
			this.lbl증빙.TabIndex = 148;
			this.lbl증빙.Text = "증빙";
			this.lbl증빙.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.flowLayoutPanel3);
			this.bpPanelControl4.Controls.Add(this.lbl봉사료전표처리);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.rdo여);
			this.flowLayoutPanel3.Controls.Add(this.rdo부);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(107, 0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(185, 23);
			this.flowLayoutPanel3.TabIndex = 148;
			// 
			// rdo여
			// 
			this.rdo여.AutoSize = true;
			this.rdo여.Location = new System.Drawing.Point(3, 3);
			this.rdo여.Name = "rdo여";
			this.rdo여.Size = new System.Drawing.Size(35, 16);
			this.rdo여.TabIndex = 13;
			this.rdo여.TabStop = true;
			this.rdo여.Text = "여";
			this.rdo여.TextDD = null;
			this.rdo여.UseKeyEnter = true;
			this.rdo여.UseVisualStyleBackColor = true;
			// 
			// rdo부
			// 
			this.rdo부.AutoSize = true;
			this.rdo부.Checked = true;
			this.rdo부.Location = new System.Drawing.Point(44, 3);
			this.rdo부.Name = "rdo부";
			this.rdo부.Size = new System.Drawing.Size(35, 16);
			this.rdo부.TabIndex = 14;
			this.rdo부.TabStop = true;
			this.rdo부.Text = "부";
			this.rdo부.TextDD = null;
			this.rdo부.UseKeyEnter = true;
			this.rdo부.UseVisualStyleBackColor = true;
			// 
			// lbl봉사료전표처리
			// 
			this.lbl봉사료전표처리.BackColor = System.Drawing.Color.Transparent;
			this.lbl봉사료전표처리.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl봉사료전표처리.Location = new System.Drawing.Point(0, 0);
			this.lbl봉사료전표처리.Name = "lbl봉사료전표처리";
			this.lbl봉사료전표처리.Size = new System.Drawing.Size(100, 23);
			this.lbl봉사료전표처리.TabIndex = 147;
			this.lbl봉사료전표처리.Text = "봉사료전표처리";
			this.lbl봉사료전표처리.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn업종적용
			// 
			this.btn업종적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn업종적용.BackColor = System.Drawing.Color.White;
			this.btn업종적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn업종적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn업종적용.Location = new System.Drawing.Point(803, 3);
			this.btn업종적용.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn업종적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn업종적용.Name = "btn업종적용";
			this.btn업종적용.Size = new System.Drawing.Size(64, 19);
			this.btn업종적용.TabIndex = 157;
			this.btn업종적용.TabStop = false;
			this.btn업종적용.Text = "업종적용";
			this.btn업종적용.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn업종적용.UseVisualStyleBackColor = false;
			this.btn업종적용.Visible = false;
			// 
			// btn데이터삭제
			// 
			this.btn데이터삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn데이터삭제.BackColor = System.Drawing.Color.White;
			this.btn데이터삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn데이터삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn데이터삭제.Location = new System.Drawing.Point(406, 3);
			this.btn데이터삭제.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn데이터삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn데이터삭제.Name = "btn데이터삭제";
			this.btn데이터삭제.Size = new System.Drawing.Size(78, 19);
			this.btn데이터삭제.TabIndex = 158;
			this.btn데이터삭제.TabStop = false;
			this.btn데이터삭제.Text = "데이터삭제";
			this.btn데이터삭제.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn데이터삭제.UseVisualStyleBackColor = false;
			this.btn데이터삭제.Visible = false;
			// 
			// btn결의서
			// 
			this.btn결의서.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn결의서.BackColor = System.Drawing.Color.Transparent;
			this.btn결의서.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn결의서.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn결의서.Location = new System.Drawing.Point(738, 3);
			this.btn결의서.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn결의서.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn결의서.Name = "btn결의서";
			this.btn결의서.Size = new System.Drawing.Size(62, 19);
			this.btn결의서.TabIndex = 159;
			this.btn결의서.TabStop = false;
			this.btn결의서.Text = "결의서";
			this.btn결의서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn결의서.UseVisualStyleBackColor = true;
			this.btn결의서.Visible = false;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel2.Controls.Add(this.btn업종적용);
			this.flowLayoutPanel2.Controls.Add(this.btn결의서);
			this.flowLayoutPanel2.Controls.Add(this.btn카드전표마감);
			this.flowLayoutPanel2.Controls.Add(this.btn전용4자);
			this.flowLayoutPanel2.Controls.Add(this.btn전표번호매핑);
			this.flowLayoutPanel2.Controls.Add(this.btn데이터삭제);
			this.flowLayoutPanel2.Controls.Add(this.btnVAT제외계정등록);
			this.flowLayoutPanel2.Controls.Add(this.btn전용버튼6자);
			this.flowLayoutPanel2.Controls.Add(this.btn전용버튼8자);
			this.flowLayoutPanel2.Controls.Add(this.btn전용버튼10자);
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(747, 10);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(867, 23);
			this.flowLayoutPanel2.TabIndex = 160;
			// 
			// btn카드전표마감
			// 
			this.btn카드전표마감.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn카드전표마감.BackColor = System.Drawing.Color.Transparent;
			this.btn카드전표마감.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn카드전표마감.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn카드전표마감.Location = new System.Drawing.Point(646, 3);
			this.btn카드전표마감.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn카드전표마감.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn카드전표마감.Name = "btn카드전표마감";
			this.btn카드전표마감.Size = new System.Drawing.Size(89, 19);
			this.btn카드전표마감.TabIndex = 161;
			this.btn카드전표마감.TabStop = false;
			this.btn카드전표마감.Text = "카드전표마감";
			this.btn카드전표마감.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn카드전표마감.UseVisualStyleBackColor = true;
			this.btn카드전표마감.Visible = false;
			// 
			// btn전용4자
			// 
			this.btn전용4자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전용4자.BackColor = System.Drawing.Color.Transparent;
			this.btn전용4자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전용4자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전용4자.Location = new System.Drawing.Point(579, 3);
			this.btn전용4자.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전용4자.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전용4자.Name = "btn전용4자";
			this.btn전용4자.Size = new System.Drawing.Size(64, 19);
			this.btn전용4자.TabIndex = 163;
			this.btn전용4자.TabStop = false;
			this.btn전용4자.Text = "전용4자";
			this.btn전용4자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn전용4자.UseVisualStyleBackColor = true;
			this.btn전용4자.Visible = false;
			// 
			// btn전표번호매핑
			// 
			this.btn전표번호매핑.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전표번호매핑.BackColor = System.Drawing.Color.Transparent;
			this.btn전표번호매핑.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표번호매핑.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표번호매핑.Location = new System.Drawing.Point(487, 3);
			this.btn전표번호매핑.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전표번호매핑.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표번호매핑.Name = "btn전표번호매핑";
			this.btn전표번호매핑.Size = new System.Drawing.Size(89, 19);
			this.btn전표번호매핑.TabIndex = 162;
			this.btn전표번호매핑.TabStop = false;
			this.btn전표번호매핑.Text = "전표번호매핑";
			this.btn전표번호매핑.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn전표번호매핑.UseVisualStyleBackColor = true;
			this.btn전표번호매핑.Visible = false;
			// 
			// btnVAT제외계정등록
			// 
			this.btnVAT제외계정등록.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnVAT제외계정등록.BackColor = System.Drawing.Color.Transparent;
			this.btnVAT제외계정등록.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnVAT제외계정등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVAT제외계정등록.Location = new System.Drawing.Point(283, 3);
			this.btnVAT제외계정등록.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btnVAT제외계정등록.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnVAT제외계정등록.Name = "btnVAT제외계정등록";
			this.btnVAT제외계정등록.Size = new System.Drawing.Size(120, 19);
			this.btnVAT제외계정등록.TabIndex = 160;
			this.btnVAT제외계정등록.TabStop = false;
			this.btnVAT제외계정등록.Text = "VAT제외계정등록";
			this.btnVAT제외계정등록.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnVAT제외계정등록.UseVisualStyleBackColor = true;
			this.btnVAT제외계정등록.Visible = false;
			// 
			// btn전용버튼6자
			// 
			this.btn전용버튼6자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전용버튼6자.BackColor = System.Drawing.Color.Transparent;
			this.btn전용버튼6자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전용버튼6자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전용버튼6자.Location = new System.Drawing.Point(191, 3);
			this.btn전용버튼6자.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전용버튼6자.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전용버튼6자.Name = "btn전용버튼6자";
			this.btn전용버튼6자.Size = new System.Drawing.Size(89, 19);
			this.btn전용버튼6자.TabIndex = 164;
			this.btn전용버튼6자.TabStop = false;
			this.btn전용버튼6자.Text = "전용버튼6자";
			this.btn전용버튼6자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn전용버튼6자.UseVisualStyleBackColor = true;
			this.btn전용버튼6자.Visible = false;
			// 
			// btn전용버튼8자
			// 
			this.btn전용버튼8자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전용버튼8자.BackColor = System.Drawing.Color.Transparent;
			this.btn전용버튼8자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전용버튼8자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전용버튼8자.Location = new System.Drawing.Point(99, 3);
			this.btn전용버튼8자.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전용버튼8자.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전용버튼8자.Name = "btn전용버튼8자";
			this.btn전용버튼8자.Size = new System.Drawing.Size(89, 19);
			this.btn전용버튼8자.TabIndex = 165;
			this.btn전용버튼8자.TabStop = false;
			this.btn전용버튼8자.Text = "전용버튼8자";
			this.btn전용버튼8자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn전용버튼8자.UseVisualStyleBackColor = true;
			this.btn전용버튼8자.Visible = false;
			// 
			// btn전용버튼10자
			// 
			this.btn전용버튼10자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전용버튼10자.BackColor = System.Drawing.Color.Transparent;
			this.btn전용버튼10자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전용버튼10자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전용버튼10자.Location = new System.Drawing.Point(6, 3);
			this.btn전용버튼10자.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn전용버튼10자.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전용버튼10자.Name = "btn전용버튼10자";
			this.btn전용버튼10자.Size = new System.Drawing.Size(90, 19);
			this.btn전용버튼10자.TabIndex = 166;
			this.btn전용버튼10자.TabStop = false;
			this.btn전용버튼10자.Text = "전용버튼10자";
			this.btn전용버튼10자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn전용버튼10자.UseVisualStyleBackColor = true;
			this.btn전용버튼10자.Visible = false;
			// 
			// roundedButton1
			// 
			this.roundedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.roundedButton1.BackColor = System.Drawing.Color.Transparent;
			this.roundedButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.roundedButton1.Location = new System.Drawing.Point(91, 3);
			this.roundedButton1.MaximumSize = new System.Drawing.Size(0, 19);
			this.roundedButton1.Name = "roundedButton1";
			this.roundedButton1.Size = new System.Drawing.Size(104, 19);
			this.roundedButton1.TabIndex = 160;
			this.roundedButton1.TabStop = false;
			this.roundedButton1.Text = "VAT제외계정등록";
			this.roundedButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.roundedButton1.UseVisualStyleBackColor = true;
			this.roundedButton1.Visible = false;
			// 
			// btnVAT제외계정
			// 
			this.btnVAT제외계정.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnVAT제외계정.BackColor = System.Drawing.Color.Transparent;
			this.btnVAT제외계정.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnVAT제외계정.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVAT제외계정.Location = new System.Drawing.Point(91, 3);
			this.btnVAT제외계정.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnVAT제외계정.Name = "btnVAT제외계정";
			this.btnVAT제외계정.Size = new System.Drawing.Size(104, 19);
			this.btnVAT제외계정.TabIndex = 160;
			this.btnVAT제외계정.TabStop = false;
			this.btnVAT제외계정.Text = "VAT제외계정등록";
			this.btnVAT제외계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnVAT제외계정.UseVisualStyleBackColor = true;
			this.btnVAT제외계정.Visible = false;
			// 
			// P_CZ_FI_CARD_TEMP_VAT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel2);
			this.Name = "P_CZ_FI_CARD_TEMP_VAT";
			this.Size = new System.Drawing.Size(1620, 813);
			this.TitleText = "법인카드승인내역";
			this.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPnl_DT.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl11.ResumeLayout(false);
			this.bpPanelControl10.ResumeLayout(false);
			this.bppnl청구년월.ResumeLayout(false);
			this.oneGridItem4.ResumeLayout(false);
			this.bppnl그룹웨어처리.ResumeLayout(false);
			this.bpPanelControl12.ResumeLayout(false);
			this.oneGridItem5.ResumeLayout(false);
			this.bppnlCustCombo.ResumeLayout(false);
			this.bppnl비용계정.ResumeLayout(false);
			this.oneGridItem6.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo여)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo부)).EndInit();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        private BpCodeTextBox ctx회계단위;
        private BpComboBox bpc카드번호;
        private BpComboBox bpc작성부서;
        private DropDownComboBox cbo전표처리;
        private LabelExt lbl청구년월;
        private LabelExt lbl카드번호;
        private LabelExt lbl승인일자;
        private LabelExt lbl회계단위;
        private LabelExt lbl부가세구분;
        private LabelExt lbl작성부서;
        private DropDownComboBox cbo부가세구분;
        private TableLayoutPanel tableLayoutPanel1;
        private RoundedButton btn전표처리;
        private RoundedButton btn전표취소;
        private RoundedButton btn전표조회;
        private RoundedButton btn부가세처리;
        private RoundedButton btn부가세미처리;
        private DropDownComboBox cbo승인구분;
        private LabelExt lbl승인번호;
        private RoundedButton btn전표처리미적용;
        private RoundedButton btn전표처리적용;
        private LabelExt lbl전표처리;
        private LabelExt lbl전표승인여부;
        private DropDownComboBox cbo전표승인여부;
        private LabelExt lbl카드사명;
        private DropDownComboBox cbo카드사명;
        private RoundedButton btn데이터확인;
        private OneGrid oneGrid;
        private OneGridItem oneGridItem1;
        private BpPanelControl bpPanelControl3;
        private BpPanelControl bpPanelControl2;
        private BpPanelControl bpPanelControl1;
        private OneGridItem oneGridItem2;
        private BpPanelControl bpPanelControl7;
        private BpPanelControl bpPanelControl6;
        private BpPanelControl bpPnl_DT;
        private OneGridItem oneGridItem3;
        private BpPanelControl bpPanelControl11;
        private BpPanelControl bpPanelControl10;
        private BpPanelControl bppnl청구년월;
        private OneGridItem oneGridItem4;
        private BpPanelControl bppnl그룹웨어처리;
        private BpPanelControl bpPanelControl12;
        private PeriodPicker dtp승인일자;
        private PeriodPicker dtp청구년월;
        private FlowLayoutPanel flowLayoutPanel1;
        private RoundedButton btn업종적용;
        private RoundedButton btn데이터삭제;
        private RoundedButton btn결의서;
        private FlowLayoutPanel flowLayoutPanel2;
        private BpPanelControl bppnl비용계정;
        private LabelExt lbl비용계정;
        private BpComboBox bpc비용계정;
        private OneGridItem oneGridItem5;
        private DropDownComboBox cbo그룹웨어처리;
        private LabelExt lbl그룹웨어처리;
        private BpPanelControl bppnlCustCombo;
        private DropDownComboBox cboCust;
        private LabelExt lblCustCombo;
        private RoundedButton btnVAT제외계정등록;
        private RoundedButton roundedButton1;
        private RoundedButton btnVAT제외계정;
        private RoundedButton btn카드전표마감;
        private RoundedButton btn전표번호매핑;
        private RoundedButton btn전용4자;
        private Dass.FlexGrid.FlexGrid _flex;
        private OneGrid oneGrid1;
        private OneGridItem oneGridItem6;
        private BpPanelControl bpPanelControl5;
        private BpPanelControl bpPanelControl4;
        private RadioButtonExt rdo부;
        private RadioButtonExt rdo여;
        private LabelExt lbl봉사료전표처리;
        private BpCodeTextBox ctx증빙;
        private LabelExt lbl증빙;
        private RoundedButton btn전용버튼6자;
        private RoundedButton btn전용버튼8자;
        private RoundedButton btn일괄복사;
        private RoundedButton btn전용버튼10자;
        private RoundedButton btn거래처등록;

		#endregion

		private FlowLayoutPanel flowLayoutPanel3;
		private RoundedButton btn전자결재;
	}
}