using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace cz
{
    partial class P_CZ_HR_WBARCODE
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_WBARCODE));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl = new Duzon.Common.Controls.TabControlExt();
			this.tpg리더기설정 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panel29 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.lbl근태코드연결퇴근 = new Duzon.Common.Controls.LabelExt();
			this.txt근태코드연결복귀 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl근태코드연결출근 = new Duzon.Common.Controls.LabelExt();
			this.lbl근태코드연결외출 = new Duzon.Common.Controls.LabelExt();
			this.txt근태코드연결퇴근 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl근태코드연결복귀 = new Duzon.Common.Controls.LabelExt();
			this.txt근태코드연결외출 = new Duzon.Common.Controls.TextBoxExt();
			this.txt근태코드연결출근 = new Duzon.Common.Controls.TextBoxExt();
			this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel28 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.rdoFILE사용 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo리더기사용 = new Duzon.Common.Controls.RadioButtonExt();
			this.panel33 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.rdoText탭으로분리 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdoText일반 = new Duzon.Common.Controls.RadioButtonExt();
			this.chk근태코드존재 = new Duzon.Common.Controls.CheckBoxExt();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.panel55 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.lbl근태일자 = new Duzon.Common.Controls.LabelExt();
			this.lbl근무시간 = new Duzon.Common.Controls.LabelExt();
			this.lbl탭분리위치 = new Duzon.Common.Controls.LabelExt();
			this.lbl카드번호 = new Duzon.Common.Controls.LabelExt();
			this.lbl파일FORMAT = new Duzon.Common.Controls.LabelExt();
			this.lbl근태 = new Duzon.Common.Controls.LabelExt();
			this.lbl자릿수 = new Duzon.Common.Controls.LabelExt();
			this.lbl기기번호 = new Duzon.Common.Controls.LabelExt();
			this.lbl시작포인트 = new Duzon.Common.Controls.LabelExt();
			this.lbl출근 = new Duzon.Common.Controls.LabelExt();
			this.cur기기번호탭분리위치 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur근태탭분리위치 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur카드번호탭분리위치 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur근무시간탭분리위치 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur근태일자탭분리위치 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur기기번호자릿수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur근태자릿수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur카드번호자릿수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur근무시간자릿수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur근태일자자릿수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.txt기기번호시작포인트 = new Duzon.Common.Controls.TextBoxExt();
			this.txt근태시작포인트 = new Duzon.Common.Controls.TextBoxExt();
			this.txt카드번호시작포인트 = new Duzon.Common.Controls.TextBoxExt();
			this.txt근무시간시작포인트 = new Duzon.Common.Controls.TextBoxExt();
			this.txt근태일자시작포인트 = new Duzon.Common.Controls.TextBoxExt();
			this.imagePanel4 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
			this.panelExt15 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.msk퇴근퇴근 = new Duzon.Common.Controls.MaskedEditBox();
			this.lbl출근퇴근 = new Duzon.Common.Controls.LabelExt();
			this.msk출근출근 = new Duzon.Common.Controls.MaskedEditBox();
			this.msk출근퇴근 = new Duzon.Common.Controls.MaskedEditBox();
			this.chk시간 = new Duzon.Common.Controls.CheckBoxExt();
			this.panelExt16 = new Duzon.Common.Controls.PanelExt();
			this.panelExt17 = new Duzon.Common.Controls.PanelExt();
			this.labelExt12 = new Duzon.Common.Controls.LabelExt();
			this.imagePanel6 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.panel34 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.lblFORMAT지정 = new Duzon.Common.Controls.LabelExt();
			this.nud연결대수 = new Duzon.Common.Controls.NumericUpDownExt();
			this.lbl통신속도 = new Duzon.Common.Controls.LabelExt();
			this.cbo리더기종류 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl포트지정 = new Duzon.Common.Controls.LabelExt();
			this.lbl연결대수 = new Duzon.Common.Controls.LabelExt();
			this.cbo통신속도 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl리더기종류 = new Duzon.Common.Controls.LabelExt();
			this.cbo포트지정 = new Duzon.Common.Controls.DropDownComboBox();
			this.txtFORMAT지정1 = new Duzon.Common.Controls.TextBoxExt();
			this.txtFORMAT지정 = new Duzon.Common.Controls.TextBoxExt();
			this.imagePanel5 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tpg사원카드번호연결 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.imagePanel7 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.m_pnlEmpConnect = new Duzon.Common.Controls.PanelExt();
			this._flex사원카드번호연결 = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl재직구분 = new Duzon.Common.Controls.LabelExt();
			this.cbo재직구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl부서 = new Duzon.Common.Controls.LabelExt();
			this.bpc부서 = new Duzon.Common.BpControls.BpComboBox();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc사업장 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bppnl입사일자 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp입사일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl입사일자 = new Duzon.Common.Controls.LabelExt();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn카드정보올리기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn사번복사 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tpg데이터조회 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
			this.imagePanel8 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
			this.m_pnlDataSearch = new Duzon.Common.Controls.PanelExt();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tpg승인대기 = new System.Windows.Forms.TabPage();
			this._flex승인대기 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg승인 = new System.Windows.Forms.TabPage();
			this.tabControl3 = new System.Windows.Forms.TabControl();
			this.tpg승인일반 = new System.Windows.Forms.TabPage();
			this._flex승인일반 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg승인Pivot = new System.Windows.Forms.TabPage();
			this._flex승인Pivot = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid2 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사원 = new Duzon.Common.Controls.LabelExt();
			this.bpc사원 = new Duzon.Common.BpControls.BpComboBox();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl부서1 = new Duzon.Common.Controls.LabelExt();
			this.bpc부서1 = new Duzon.Common.BpControls.BpComboBox();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc사업장1 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl사업장1 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bppnl년월일 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp년월일 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl년월일 = new Duzon.Common.Controls.LabelExt();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnFPMS데이터읽기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn데이터읽기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnCaps데이터읽기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnSecom데이터읽기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn승인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
			this.panelExt8 = new Duzon.Common.Controls.PanelExt();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.panelExt9 = new Duzon.Common.Controls.PanelExt();
			this.panelExt10 = new Duzon.Common.Controls.PanelExt();
			this.panelExt11 = new Duzon.Common.Controls.PanelExt();
			this.textBoxExt1 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt2 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt3 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt4 = new Duzon.Common.Controls.TextBoxExt();
			this.panelExt12 = new Duzon.Common.Controls.PanelExt();
			this.panelExt13 = new Duzon.Common.Controls.PanelExt();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.labelExt6 = new Duzon.Common.Controls.LabelExt();
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel11.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tpg리더기설정.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel29.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel28.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdoFILE사용)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo리더기사용)).BeginInit();
			this.panel33.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdoText탭으로분리)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdoText일반)).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			this.panel55.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur기기번호탭분리위치)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태탭분리위치)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur카드번호탭분리위치)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근무시간탭분리위치)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태일자탭분리위치)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur기기번호자릿수)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태자릿수)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur카드번호자릿수)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근무시간자릿수)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태일자자릿수)).BeginInit();
			this.tableLayoutPanel13.SuspendLayout();
			this.panelExt15.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.msk퇴근퇴근)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.msk출근출근)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.msk출근퇴근)).BeginInit();
			this.tableLayoutPanel4.SuspendLayout();
			this.panel34.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud연결대수)).BeginInit();
			this.tpg사원카드번호연결.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			this.imagePanel7.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.m_pnlEmpConnect.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex사원카드번호연결)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bppnl입사일자.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tpg데이터조회.SuspendLayout();
			this.tableLayoutPanel10.SuspendLayout();
			this.imagePanel8.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			this.m_pnlDataSearch.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tpg승인대기.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex승인대기)).BeginInit();
			this.tpg승인.SuspendLayout();
			this.tabControl3.SuspendLayout();
			this.tpg승인일반.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex승인일반)).BeginInit();
			this.tpg승인Pivot.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex승인Pivot)).BeginInit();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.oneGridItem4.SuspendLayout();
			this.bppnl년월일.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel12.SuspendLayout();
			this.panelExt8.SuspendLayout();
			this.panelExt9.SuspendLayout();
			this.panelExt13.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel11);
			this.mDataArea.Size = new System.Drawing.Size(1265, 821);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// SaveFileDialog
			// 
			this.SaveFileDialog.FileName = "CardInfomation";
			this.SaveFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt";
			this.SaveFileDialog.FilterIndex = 2;
			this.SaveFileDialog.RestoreDirectory = true;
			// 
			// tableLayoutPanel11
			// 
			this.tableLayoutPanel11.ColumnCount = 1;
			this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel11.Controls.Add(this.tabControl, 0, 0);
			this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel11.Name = "tableLayoutPanel11";
			this.tableLayoutPanel11.RowCount = 1;
			this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel11.Size = new System.Drawing.Size(1265, 821);
			this.tableLayoutPanel11.TabIndex = 3;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tpg리더기설정);
			this.tabControl.Controls.Add(this.tpg사원카드번호연결);
			this.tabControl.Controls.Add(this.tpg데이터조회);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.ItemSize = new System.Drawing.Size(150, 20);
			this.tabControl.Location = new System.Drawing.Point(3, 3);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1259, 815);
			this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl.TabIndex = 0;
			// 
			// tpg리더기설정
			// 
			this.tpg리더기설정.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.tpg리더기설정.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tpg리더기설정.Controls.Add(this.tableLayoutPanel6);
			this.tpg리더기설정.ImageIndex = 0;
			this.tpg리더기설정.Location = new System.Drawing.Point(4, 24);
			this.tpg리더기설정.Name = "tpg리더기설정";
			this.tpg리더기설정.Size = new System.Drawing.Size(1251, 787);
			this.tpg리더기설정.TabIndex = 0;
			this.tpg리더기설정.Tag = "TAB11_WORK";
			this.tpg리더기설정.Text = "리더기 설정";
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 1;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel5, 0, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(1247, 783);
			this.tableLayoutPanel6.TabIndex = 259;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel5.ColumnCount = 2;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 1, 0);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel13, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel4, 1, 1);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(8, 8);
			this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(8);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(4);
			this.tableLayoutPanel5.RowCount = 3;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.Size = new System.Drawing.Size(1231, 767);
			this.tableLayoutPanel5.TabIndex = 258;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.panel29, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.imagePanel3, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(618, 7);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(606, 154);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// panel29
			// 
			this.panel29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.panel29.Controls.Add(this.lbl근태코드연결퇴근);
			this.panel29.Controls.Add(this.txt근태코드연결복귀);
			this.panel29.Controls.Add(this.lbl근태코드연결출근);
			this.panel29.Controls.Add(this.lbl근태코드연결외출);
			this.panel29.Controls.Add(this.txt근태코드연결퇴근);
			this.panel29.Controls.Add(this.lbl근태코드연결복귀);
			this.panel29.Controls.Add(this.txt근태코드연결외출);
			this.panel29.Controls.Add(this.txt근태코드연결출근);
			this.panel29.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel29.Location = new System.Drawing.Point(0, 28);
			this.panel29.Margin = new System.Windows.Forms.Padding(0);
			this.panel29.Name = "panel29";
			this.panel29.Size = new System.Drawing.Size(606, 126);
			this.panel29.TabIndex = 3;
			// 
			// lbl근태코드연결퇴근
			// 
			this.lbl근태코드연결퇴근.Location = new System.Drawing.Point(9, 41);
			this.lbl근태코드연결퇴근.Name = "lbl근태코드연결퇴근";
			this.lbl근태코드연결퇴근.Size = new System.Drawing.Size(150, 16);
			this.lbl근태코드연결퇴근.TabIndex = 0;
			this.lbl근태코드연결퇴근.Tag = "CLOSE";
			this.lbl근태코드연결퇴근.Text = "퇴근";
			this.lbl근태코드연결퇴근.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt근태코드연결복귀
			// 
			this.txt근태코드연결복귀.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt근태코드연결복귀.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt근태코드연결복귀.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt근태코드연결복귀.Location = new System.Drawing.Point(165, 91);
			this.txt근태코드연결복귀.Name = "txt근태코드연결복귀";
			this.txt근태코드연결복귀.Size = new System.Drawing.Size(150, 21);
			this.txt근태코드연결복귀.TabIndex = 3;
			this.txt근태코드연결복귀.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl근태코드연결출근
			// 
			this.lbl근태코드연결출근.Location = new System.Drawing.Point(9, 15);
			this.lbl근태코드연결출근.Name = "lbl근태코드연결출근";
			this.lbl근태코드연결출근.Size = new System.Drawing.Size(150, 16);
			this.lbl근태코드연결출근.TabIndex = 0;
			this.lbl근태코드연결출근.Tag = "START";
			this.lbl근태코드연결출근.Text = "출근";
			this.lbl근태코드연결출근.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl근태코드연결외출
			// 
			this.lbl근태코드연결외출.Location = new System.Drawing.Point(9, 67);
			this.lbl근태코드연결외출.Name = "lbl근태코드연결외출";
			this.lbl근태코드연결외출.Size = new System.Drawing.Size(150, 16);
			this.lbl근태코드연결외출.TabIndex = 0;
			this.lbl근태코드연결외출.Tag = "GOOUT";
			this.lbl근태코드연결외출.Text = "외출";
			this.lbl근태코드연결외출.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt근태코드연결퇴근
			// 
			this.txt근태코드연결퇴근.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt근태코드연결퇴근.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt근태코드연결퇴근.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt근태코드연결퇴근.Location = new System.Drawing.Point(165, 39);
			this.txt근태코드연결퇴근.Name = "txt근태코드연결퇴근";
			this.txt근태코드연결퇴근.Size = new System.Drawing.Size(150, 21);
			this.txt근태코드연결퇴근.TabIndex = 1;
			this.txt근태코드연결퇴근.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl근태코드연결복귀
			// 
			this.lbl근태코드연결복귀.Location = new System.Drawing.Point(9, 93);
			this.lbl근태코드연결복귀.Name = "lbl근태코드연결복귀";
			this.lbl근태코드연결복귀.Size = new System.Drawing.Size(150, 16);
			this.lbl근태코드연결복귀.TabIndex = 0;
			this.lbl근태코드연결복귀.Tag = "TXT43_WORK";
			this.lbl근태코드연결복귀.Text = "복귀";
			this.lbl근태코드연결복귀.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt근태코드연결외출
			// 
			this.txt근태코드연결외출.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt근태코드연결외출.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt근태코드연결외출.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt근태코드연결외출.Location = new System.Drawing.Point(165, 65);
			this.txt근태코드연결외출.Name = "txt근태코드연결외출";
			this.txt근태코드연결외출.Size = new System.Drawing.Size(150, 21);
			this.txt근태코드연결외출.TabIndex = 2;
			this.txt근태코드연결외출.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt근태코드연결출근
			// 
			this.txt근태코드연결출근.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt근태코드연결출근.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt근태코드연결출근.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt근태코드연결출근.Location = new System.Drawing.Point(165, 13);
			this.txt근태코드연결출근.Name = "txt근태코드연결출근";
			this.txt근태코드연결출근.Size = new System.Drawing.Size(150, 21);
			this.txt근태코드연결출근.TabIndex = 0;
			this.txt근태코드연결출근.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// imagePanel3
			// 
			this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel3.LeftImage = null;
			this.imagePanel3.Location = new System.Drawing.Point(0, 0);
			this.imagePanel3.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.imagePanel3.Name = "imagePanel3";
			this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel3.PatternImage = null;
			this.imagePanel3.RightImage = null;
			this.imagePanel3.Size = new System.Drawing.Size(603, 28);
			this.imagePanel3.TabIndex = 4;
			this.imagePanel3.TitleText = "근태 코드 연결";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.panel28, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel33, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.imagePanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.imagePanel2, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 7);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(605, 154);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// panel28
			// 
			this.panel28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.panel28.Controls.Add(this.rdoFILE사용);
			this.panel28.Controls.Add(this.rdo리더기사용);
			this.panel28.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel28.Location = new System.Drawing.Point(0, 28);
			this.panel28.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.panel28.Name = "panel28";
			this.panel28.Size = new System.Drawing.Size(605, 29);
			this.panel28.TabIndex = 0;
			// 
			// rdoFILE사용
			// 
			this.rdoFILE사용.Location = new System.Drawing.Point(196, 6);
			this.rdoFILE사용.Name = "rdoFILE사용";
			this.rdoFILE사용.Size = new System.Drawing.Size(177, 18);
			this.rdoFILE사용.TabIndex = 1;
			this.rdoFILE사용.TabStop = true;
			this.rdoFILE사용.Tag = "OPT4_WORK";
			this.rdoFILE사용.Text = "FILE 사용";
			this.rdoFILE사용.TextDD = null;
			this.rdoFILE사용.UseKeyEnter = true;
			// 
			// rdo리더기사용
			// 
			this.rdo리더기사용.Enabled = false;
			this.rdo리더기사용.Location = new System.Drawing.Point(13, 6);
			this.rdo리더기사용.Name = "rdo리더기사용";
			this.rdo리더기사용.Size = new System.Drawing.Size(177, 18);
			this.rdo리더기사용.TabIndex = 0;
			this.rdo리더기사용.TabStop = true;
			this.rdo리더기사용.Tag = "OPT3_WORK";
			this.rdo리더기사용.Text = "리더기 사용";
			this.rdo리더기사용.TextDD = null;
			this.rdo리더기사용.UseKeyEnter = true;
			// 
			// panel33
			// 
			this.panel33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.panel33.Controls.Add(this.rdoText탭으로분리);
			this.panel33.Controls.Add(this.rdoText일반);
			this.panel33.Controls.Add(this.chk근태코드존재);
			this.panel33.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel33.Location = new System.Drawing.Point(0, 90);
			this.panel33.Margin = new System.Windows.Forms.Padding(0);
			this.panel33.Name = "panel33";
			this.panel33.Size = new System.Drawing.Size(605, 64);
			this.panel33.TabIndex = 1;
			// 
			// rdoText탭으로분리
			// 
			this.rdoText탭으로분리.Enabled = false;
			this.rdoText탭으로분리.Location = new System.Drawing.Point(196, 34);
			this.rdoText탭으로분리.Name = "rdoText탭으로분리";
			this.rdoText탭으로분리.Size = new System.Drawing.Size(177, 18);
			this.rdoText탭으로분리.TabIndex = 2;
			this.rdoText탭으로분리.TabStop = true;
			this.rdoText탭으로분리.Tag = "OPT6_WORK";
			this.rdoText탭으로분리.Text = "Text (탭으로 분리)";
			this.rdoText탭으로분리.TextDD = null;
			this.rdoText탭으로분리.UseKeyEnter = true;
			// 
			// rdoText일반
			// 
			this.rdoText일반.Enabled = false;
			this.rdoText일반.Location = new System.Drawing.Point(13, 34);
			this.rdoText일반.Name = "rdoText일반";
			this.rdoText일반.Size = new System.Drawing.Size(177, 18);
			this.rdoText일반.TabIndex = 1;
			this.rdoText일반.TabStop = true;
			this.rdoText일반.Tag = "OPT5_WORK";
			this.rdoText일반.Text = "TXT (일반)";
			this.rdoText일반.TextDD = null;
			this.rdoText일반.UseKeyEnter = true;
			// 
			// chk근태코드존재
			// 
			this.chk근태코드존재.Enabled = false;
			this.chk근태코드존재.Location = new System.Drawing.Point(13, 11);
			this.chk근태코드존재.Name = "chk근태코드존재";
			this.chk근태코드존재.Size = new System.Drawing.Size(177, 18);
			this.chk근태코드존재.TabIndex = 0;
			this.chk근태코드존재.Tag = "CHK1_WORK";
			this.chk근태코드존재.Text = "근태코드존재";
			this.chk근태코드존재.TextDD = null;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(0, 0);
			this.imagePanel1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(602, 28);
			this.imagePanel1.TabIndex = 2;
			this.imagePanel1.TitleText = "리더기 설정";
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(0, 63);
			this.imagePanel2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(602, 27);
			this.imagePanel2.TabIndex = 3;
			this.imagePanel2.TitleText = "파일 양식 지정";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.Controls.Add(this.panel55, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.imagePanel4, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 167);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(605, 248);
			this.tableLayoutPanel3.TabIndex = 256;
			// 
			// panel55
			// 
			this.panel55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.panel55.Controls.Add(this.lbl근태일자);
			this.panel55.Controls.Add(this.lbl근무시간);
			this.panel55.Controls.Add(this.lbl탭분리위치);
			this.panel55.Controls.Add(this.lbl카드번호);
			this.panel55.Controls.Add(this.lbl파일FORMAT);
			this.panel55.Controls.Add(this.lbl근태);
			this.panel55.Controls.Add(this.lbl자릿수);
			this.panel55.Controls.Add(this.lbl기기번호);
			this.panel55.Controls.Add(this.lbl시작포인트);
			this.panel55.Controls.Add(this.lbl출근);
			this.panel55.Controls.Add(this.cur기기번호탭분리위치);
			this.panel55.Controls.Add(this.cur근태탭분리위치);
			this.panel55.Controls.Add(this.cur카드번호탭분리위치);
			this.panel55.Controls.Add(this.cur근무시간탭분리위치);
			this.panel55.Controls.Add(this.cur근태일자탭분리위치);
			this.panel55.Controls.Add(this.cur기기번호자릿수);
			this.panel55.Controls.Add(this.cur근태자릿수);
			this.panel55.Controls.Add(this.cur카드번호자릿수);
			this.panel55.Controls.Add(this.cur근무시간자릿수);
			this.panel55.Controls.Add(this.cur근태일자자릿수);
			this.panel55.Controls.Add(this.txt기기번호시작포인트);
			this.panel55.Controls.Add(this.txt근태시작포인트);
			this.panel55.Controls.Add(this.txt카드번호시작포인트);
			this.panel55.Controls.Add(this.txt근무시간시작포인트);
			this.panel55.Controls.Add(this.txt근태일자시작포인트);
			this.panel55.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel55.Location = new System.Drawing.Point(0, 27);
			this.panel55.Margin = new System.Windows.Forms.Padding(0);
			this.panel55.Name = "panel55";
			this.panel55.Size = new System.Drawing.Size(605, 221);
			this.panel55.TabIndex = 2;
			// 
			// lbl근태일자
			// 
			this.lbl근태일자.Location = new System.Drawing.Point(7, 69);
			this.lbl근태일자.Name = "lbl근태일자";
			this.lbl근태일자.Size = new System.Drawing.Size(108, 14);
			this.lbl근태일자.TabIndex = 0;
			this.lbl근태일자.Tag = "WORKDAY";
			this.lbl근태일자.Text = "근태일자";
			this.lbl근태일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl근무시간
			// 
			this.lbl근무시간.Location = new System.Drawing.Point(7, 95);
			this.lbl근무시간.Name = "lbl근무시간";
			this.lbl근무시간.Size = new System.Drawing.Size(108, 14);
			this.lbl근무시간.TabIndex = 0;
			this.lbl근무시간.Tag = "TXT55_WORK";
			this.lbl근무시간.Text = "근무시간";
			this.lbl근무시간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl탭분리위치
			// 
			this.lbl탭분리위치.Location = new System.Drawing.Point(347, 40);
			this.lbl탭분리위치.Name = "lbl탭분리위치";
			this.lbl탭분리위치.Size = new System.Drawing.Size(108, 14);
			this.lbl탭분리위치.TabIndex = 1;
			this.lbl탭분리위치.Tag = "TXT80_WORK";
			this.lbl탭분리위치.Text = "탭분리위치";
			this.lbl탭분리위치.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl카드번호
			// 
			this.lbl카드번호.Location = new System.Drawing.Point(7, 122);
			this.lbl카드번호.Name = "lbl카드번호";
			this.lbl카드번호.Size = new System.Drawing.Size(108, 14);
			this.lbl카드번호.TabIndex = 0;
			this.lbl카드번호.Tag = "NO_CARD";
			this.lbl카드번호.Text = "카드번호";
			this.lbl카드번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl파일FORMAT
			// 
			this.lbl파일FORMAT.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl파일FORMAT.Location = new System.Drawing.Point(106, 15);
			this.lbl파일FORMAT.Name = "lbl파일FORMAT";
			this.lbl파일FORMAT.Size = new System.Drawing.Size(244, 17);
			this.lbl파일FORMAT.TabIndex = 0;
			this.lbl파일FORMAT.Tag = "TXT51_WORK";
			this.lbl파일FORMAT.Text = "파일 FORMAT";
			this.lbl파일FORMAT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl근태
			// 
			this.lbl근태.Location = new System.Drawing.Point(7, 150);
			this.lbl근태.Name = "lbl근태";
			this.lbl근태.Size = new System.Drawing.Size(108, 14);
			this.lbl근태.TabIndex = 0;
			this.lbl근태.Tag = "TAB3_PAY";
			this.lbl근태.Text = "근태";
			this.lbl근태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl자릿수
			// 
			this.lbl자릿수.Location = new System.Drawing.Point(233, 40);
			this.lbl자릿수.Name = "lbl자릿수";
			this.lbl자릿수.Size = new System.Drawing.Size(108, 14);
			this.lbl자릿수.TabIndex = 0;
			this.lbl자릿수.Tag = "TXT53_WORK";
			this.lbl자릿수.Text = "자릿수";
			this.lbl자릿수.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl기기번호
			// 
			this.lbl기기번호.Location = new System.Drawing.Point(7, 178);
			this.lbl기기번호.Name = "lbl기기번호";
			this.lbl기기번호.Size = new System.Drawing.Size(108, 14);
			this.lbl기기번호.TabIndex = 0;
			this.lbl기기번호.Tag = "TXT58_WORK";
			this.lbl기기번호.Text = "기기번호";
			this.lbl기기번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl시작포인트
			// 
			this.lbl시작포인트.Location = new System.Drawing.Point(121, 40);
			this.lbl시작포인트.Name = "lbl시작포인트";
			this.lbl시작포인트.Size = new System.Drawing.Size(108, 14);
			this.lbl시작포인트.TabIndex = 0;
			this.lbl시작포인트.Tag = "TXT52_WORK";
			this.lbl시작포인트.Text = "시작포인트";
			this.lbl시작포인트.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl출근
			// 
			this.lbl출근.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl출근.Location = new System.Drawing.Point(7, 40);
			this.lbl출근.Name = "lbl출근";
			this.lbl출근.Size = new System.Drawing.Size(108, 14);
			this.lbl출근.TabIndex = 0;
			this.lbl출근.Tag = "TXT40_WORK";
			this.lbl출근.Text = "출   근";
			this.lbl출근.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur기기번호탭분리위치
			// 
			this.cur기기번호탭분리위치.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur기기번호탭분리위치.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur기기번호탭분리위치.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur기기번호탭분리위치.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur기기번호탭분리위치.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur기기번호탭분리위치.Location = new System.Drawing.Point(347, 175);
			this.cur기기번호탭분리위치.MaxLength = 4;
			this.cur기기번호탭분리위치.Name = "cur기기번호탭분리위치";
			this.cur기기번호탭분리위치.NullString = "0";
			this.cur기기번호탭분리위치.PositiveColor = System.Drawing.Color.Black;
			this.cur기기번호탭분리위치.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur기기번호탭분리위치.Size = new System.Drawing.Size(108, 21);
			this.cur기기번호탭분리위치.TabIndex = 14;
			this.cur기기번호탭분리위치.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur근태탭분리위치
			// 
			this.cur근태탭분리위치.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur근태탭분리위치.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur근태탭분리위치.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur근태탭분리위치.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur근태탭분리위치.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur근태탭분리위치.Location = new System.Drawing.Point(347, 147);
			this.cur근태탭분리위치.MaxLength = 4;
			this.cur근태탭분리위치.Name = "cur근태탭분리위치";
			this.cur근태탭분리위치.NullString = "0";
			this.cur근태탭분리위치.PositiveColor = System.Drawing.Color.Black;
			this.cur근태탭분리위치.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur근태탭분리위치.Size = new System.Drawing.Size(108, 21);
			this.cur근태탭분리위치.TabIndex = 11;
			this.cur근태탭분리위치.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur카드번호탭분리위치
			// 
			this.cur카드번호탭분리위치.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur카드번호탭분리위치.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur카드번호탭분리위치.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur카드번호탭분리위치.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur카드번호탭분리위치.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur카드번호탭분리위치.Location = new System.Drawing.Point(347, 119);
			this.cur카드번호탭분리위치.MaxLength = 4;
			this.cur카드번호탭분리위치.Name = "cur카드번호탭분리위치";
			this.cur카드번호탭분리위치.NullString = "0";
			this.cur카드번호탭분리위치.PositiveColor = System.Drawing.Color.Black;
			this.cur카드번호탭분리위치.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur카드번호탭분리위치.Size = new System.Drawing.Size(108, 21);
			this.cur카드번호탭분리위치.TabIndex = 8;
			this.cur카드번호탭분리위치.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur근무시간탭분리위치
			// 
			this.cur근무시간탭분리위치.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur근무시간탭분리위치.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur근무시간탭분리위치.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur근무시간탭분리위치.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur근무시간탭분리위치.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur근무시간탭분리위치.Location = new System.Drawing.Point(347, 92);
			this.cur근무시간탭분리위치.MaxLength = 4;
			this.cur근무시간탭분리위치.Name = "cur근무시간탭분리위치";
			this.cur근무시간탭분리위치.NullString = "0";
			this.cur근무시간탭분리위치.PositiveColor = System.Drawing.Color.Black;
			this.cur근무시간탭분리위치.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur근무시간탭분리위치.Size = new System.Drawing.Size(108, 21);
			this.cur근무시간탭분리위치.TabIndex = 5;
			this.cur근무시간탭분리위치.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur근태일자탭분리위치
			// 
			this.cur근태일자탭분리위치.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur근태일자탭분리위치.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur근태일자탭분리위치.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur근태일자탭분리위치.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur근태일자탭분리위치.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur근태일자탭분리위치.Location = new System.Drawing.Point(347, 66);
			this.cur근태일자탭분리위치.MaxLength = 4;
			this.cur근태일자탭분리위치.Name = "cur근태일자탭분리위치";
			this.cur근태일자탭분리위치.NullString = "0";
			this.cur근태일자탭분리위치.PositiveColor = System.Drawing.Color.Black;
			this.cur근태일자탭분리위치.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur근태일자탭분리위치.Size = new System.Drawing.Size(108, 21);
			this.cur근태일자탭분리위치.TabIndex = 2;
			this.cur근태일자탭분리위치.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur기기번호자릿수
			// 
			this.cur기기번호자릿수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur기기번호자릿수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur기기번호자릿수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur기기번호자릿수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur기기번호자릿수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur기기번호자릿수.Location = new System.Drawing.Point(235, 175);
			this.cur기기번호자릿수.MaxLength = 4;
			this.cur기기번호자릿수.Name = "cur기기번호자릿수";
			this.cur기기번호자릿수.NullString = "0";
			this.cur기기번호자릿수.PositiveColor = System.Drawing.Color.Black;
			this.cur기기번호자릿수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur기기번호자릿수.Size = new System.Drawing.Size(108, 21);
			this.cur기기번호자릿수.TabIndex = 13;
			this.cur기기번호자릿수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur근태자릿수
			// 
			this.cur근태자릿수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur근태자릿수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur근태자릿수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur근태자릿수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur근태자릿수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur근태자릿수.Location = new System.Drawing.Point(235, 147);
			this.cur근태자릿수.MaxLength = 4;
			this.cur근태자릿수.Name = "cur근태자릿수";
			this.cur근태자릿수.NullString = "0";
			this.cur근태자릿수.PositiveColor = System.Drawing.Color.Black;
			this.cur근태자릿수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur근태자릿수.Size = new System.Drawing.Size(108, 21);
			this.cur근태자릿수.TabIndex = 10;
			this.cur근태자릿수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur카드번호자릿수
			// 
			this.cur카드번호자릿수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur카드번호자릿수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur카드번호자릿수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur카드번호자릿수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur카드번호자릿수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur카드번호자릿수.Location = new System.Drawing.Point(235, 119);
			this.cur카드번호자릿수.MaxLength = 4;
			this.cur카드번호자릿수.Name = "cur카드번호자릿수";
			this.cur카드번호자릿수.NullString = "0";
			this.cur카드번호자릿수.PositiveColor = System.Drawing.Color.Black;
			this.cur카드번호자릿수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur카드번호자릿수.Size = new System.Drawing.Size(108, 21);
			this.cur카드번호자릿수.TabIndex = 7;
			this.cur카드번호자릿수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur근무시간자릿수
			// 
			this.cur근무시간자릿수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur근무시간자릿수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur근무시간자릿수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur근무시간자릿수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur근무시간자릿수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur근무시간자릿수.Location = new System.Drawing.Point(235, 92);
			this.cur근무시간자릿수.MaxLength = 4;
			this.cur근무시간자릿수.Name = "cur근무시간자릿수";
			this.cur근무시간자릿수.NullString = "0";
			this.cur근무시간자릿수.PositiveColor = System.Drawing.Color.Black;
			this.cur근무시간자릿수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur근무시간자릿수.Size = new System.Drawing.Size(108, 21);
			this.cur근무시간자릿수.TabIndex = 4;
			this.cur근무시간자릿수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur근태일자자릿수
			// 
			this.cur근태일자자릿수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur근태일자자릿수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur근태일자자릿수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur근태일자자릿수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur근태일자자릿수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur근태일자자릿수.Location = new System.Drawing.Point(235, 66);
			this.cur근태일자자릿수.MaxLength = 4;
			this.cur근태일자자릿수.Name = "cur근태일자자릿수";
			this.cur근태일자자릿수.NullString = "0";
			this.cur근태일자자릿수.PositiveColor = System.Drawing.Color.Black;
			this.cur근태일자자릿수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur근태일자자릿수.Size = new System.Drawing.Size(108, 21);
			this.cur근태일자자릿수.TabIndex = 1;
			this.cur근태일자자릿수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt기기번호시작포인트
			// 
			this.txt기기번호시작포인트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt기기번호시작포인트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt기기번호시작포인트.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt기기번호시작포인트.Location = new System.Drawing.Point(123, 175);
			this.txt기기번호시작포인트.Name = "txt기기번호시작포인트";
			this.txt기기번호시작포인트.Size = new System.Drawing.Size(108, 21);
			this.txt기기번호시작포인트.TabIndex = 12;
			this.txt기기번호시작포인트.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt근태시작포인트
			// 
			this.txt근태시작포인트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt근태시작포인트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt근태시작포인트.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt근태시작포인트.Location = new System.Drawing.Point(123, 147);
			this.txt근태시작포인트.Name = "txt근태시작포인트";
			this.txt근태시작포인트.Size = new System.Drawing.Size(108, 21);
			this.txt근태시작포인트.TabIndex = 9;
			this.txt근태시작포인트.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt카드번호시작포인트
			// 
			this.txt카드번호시작포인트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt카드번호시작포인트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt카드번호시작포인트.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt카드번호시작포인트.Location = new System.Drawing.Point(123, 119);
			this.txt카드번호시작포인트.Name = "txt카드번호시작포인트";
			this.txt카드번호시작포인트.Size = new System.Drawing.Size(108, 21);
			this.txt카드번호시작포인트.TabIndex = 6;
			this.txt카드번호시작포인트.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt근무시간시작포인트
			// 
			this.txt근무시간시작포인트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt근무시간시작포인트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt근무시간시작포인트.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt근무시간시작포인트.Location = new System.Drawing.Point(123, 92);
			this.txt근무시간시작포인트.Name = "txt근무시간시작포인트";
			this.txt근무시간시작포인트.Size = new System.Drawing.Size(108, 21);
			this.txt근무시간시작포인트.TabIndex = 3;
			this.txt근무시간시작포인트.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt근태일자시작포인트
			// 
			this.txt근태일자시작포인트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt근태일자시작포인트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt근태일자시작포인트.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt근태일자시작포인트.Location = new System.Drawing.Point(123, 66);
			this.txt근태일자시작포인트.Name = "txt근태일자시작포인트";
			this.txt근태일자시작포인트.Size = new System.Drawing.Size(108, 21);
			this.txt근태일자시작포인트.TabIndex = 0;
			this.txt근태일자시작포인트.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// imagePanel4
			// 
			this.imagePanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel4.LeftImage = null;
			this.imagePanel4.Location = new System.Drawing.Point(0, 0);
			this.imagePanel4.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.imagePanel4.Name = "imagePanel4";
			this.imagePanel4.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel4.PatternImage = null;
			this.imagePanel4.RightImage = null;
			this.imagePanel4.Size = new System.Drawing.Size(602, 27);
			this.imagePanel4.TabIndex = 3;
			this.imagePanel4.TitleText = "TEXT 형식";
			// 
			// tableLayoutPanel13
			// 
			this.tableLayoutPanel13.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel13.ColumnCount = 1;
			this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel13.Controls.Add(this.panelExt15, 0, 2);
			this.tableLayoutPanel13.Controls.Add(this.labelExt12, 0, 1);
			this.tableLayoutPanel13.Controls.Add(this.imagePanel6, 0, 0);
			this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel13.Location = new System.Drawing.Point(7, 421);
			this.tableLayoutPanel13.Name = "tableLayoutPanel13";
			this.tableLayoutPanel13.RowCount = 3;
			this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel13.Size = new System.Drawing.Size(605, 339);
			this.tableLayoutPanel13.TabIndex = 258;
			// 
			// panelExt15
			// 
			this.panelExt15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.panelExt15.Controls.Add(this.msk퇴근퇴근);
			this.panelExt15.Controls.Add(this.lbl출근퇴근);
			this.panelExt15.Controls.Add(this.msk출근출근);
			this.panelExt15.Controls.Add(this.msk출근퇴근);
			this.panelExt15.Controls.Add(this.chk시간);
			this.panelExt15.Controls.Add(this.panelExt16);
			this.panelExt15.Controls.Add(this.panelExt17);
			this.panelExt15.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelExt15.Location = new System.Drawing.Point(0, 52);
			this.panelExt15.Margin = new System.Windows.Forms.Padding(0);
			this.panelExt15.Name = "panelExt15";
			this.panelExt15.Size = new System.Drawing.Size(605, 34);
			this.panelExt15.TabIndex = 3;
			// 
			// msk퇴근퇴근
			// 
			this.msk퇴근퇴근.AccessibleDescription = "MaskedEdit TextBox";
			this.msk퇴근퇴근.AccessibleName = "MaskedEditBox";
			this.msk퇴근퇴근.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.msk퇴근퇴근.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.msk퇴근퇴근.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.msk퇴근퇴근.Culture = new System.Globalization.CultureInfo("ko-KR");
			this.msk퇴근퇴근.Font = new System.Drawing.Font("굴림체", 9F);
			this.msk퇴근퇴근.Location = new System.Drawing.Point(106, 63);
			this.msk퇴근퇴근.Mask = "##:##";
			this.msk퇴근퇴근.MaxLength = 5;
			this.msk퇴근퇴근.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.msk퇴근퇴근.Name = "msk퇴근퇴근";
			this.msk퇴근퇴근.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.msk퇴근퇴근.Size = new System.Drawing.Size(125, 21);
			this.msk퇴근퇴근.TabIndex = 29;
			this.msk퇴근퇴근.Tag = "";
			this.msk퇴근퇴근.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.msk퇴근퇴근.Visible = false;
			// 
			// lbl출근퇴근
			// 
			this.lbl출근퇴근.Location = new System.Drawing.Point(7, 9);
			this.lbl출근퇴근.Name = "lbl출근퇴근";
			this.lbl출근퇴근.Size = new System.Drawing.Size(108, 14);
			this.lbl출근퇴근.TabIndex = 0;
			this.lbl출근퇴근.Tag = "START";
			this.lbl출근퇴근.Text = "출근 - 퇴근";
			this.lbl출근퇴근.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// msk출근출근
			// 
			this.msk출근출근.AccessibleDescription = "MaskedEdit TextBox";
			this.msk출근출근.AccessibleName = "MaskedEditBox";
			this.msk출근출근.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.msk출근출근.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.msk출근출근.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.msk출근출근.Culture = new System.Globalization.CultureInfo("ko-KR");
			this.msk출근출근.Font = new System.Drawing.Font("굴림체", 9F);
			this.msk출근출근.Location = new System.Drawing.Point(106, 34);
			this.msk출근출근.Mask = "##:##";
			this.msk출근출근.MaxLength = 5;
			this.msk출근출근.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.msk출근출근.Name = "msk출근출근";
			this.msk출근출근.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.msk출근출근.Size = new System.Drawing.Size(125, 21);
			this.msk출근출근.TabIndex = 28;
			this.msk출근출근.Tag = "";
			this.msk출근출근.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.msk출근출근.Visible = false;
			// 
			// msk출근퇴근
			// 
			this.msk출근퇴근.AccessibleDescription = "MaskedEdit TextBox";
			this.msk출근퇴근.AccessibleName = "MaskedEditBox";
			this.msk출근퇴근.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.msk출근퇴근.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.msk출근퇴근.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.msk출근퇴근.Culture = new System.Globalization.CultureInfo("ko-KR");
			this.msk출근퇴근.Font = new System.Drawing.Font("굴림체", 9F);
			this.msk출근퇴근.Location = new System.Drawing.Point(123, 5);
			this.msk출근퇴근.Mask = "##:##";
			this.msk출근퇴근.MaxLength = 5;
			this.msk출근퇴근.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.msk출근퇴근.Name = "msk출근퇴근";
			this.msk출근퇴근.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.msk출근퇴근.Size = new System.Drawing.Size(108, 21);
			this.msk출근퇴근.TabIndex = 27;
			this.msk출근퇴근.Tag = "";
			this.msk출근퇴근.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// chk시간
			// 
			this.chk시간.Checked = true;
			this.chk시간.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk시간.Location = new System.Drawing.Point(6, 94);
			this.chk시간.Name = "chk시간";
			this.chk시간.Size = new System.Drawing.Size(150, 18);
			this.chk시간.TabIndex = 9;
			this.chk시간.Tag = "";
			this.chk시간.Text = "동일한 날짜는 제외";
			this.chk시간.TextDD = null;
			this.chk시간.Visible = false;
			// 
			// panelExt16
			// 
			this.panelExt16.BackColor = System.Drawing.Color.Transparent;
			this.panelExt16.Location = new System.Drawing.Point(5, 87);
			this.panelExt16.Name = "panelExt16";
			this.panelExt16.Size = new System.Drawing.Size(356, 1);
			this.panelExt16.TabIndex = 8;
			// 
			// panelExt17
			// 
			this.panelExt17.BackColor = System.Drawing.Color.Transparent;
			this.panelExt17.Location = new System.Drawing.Point(5, 58);
			this.panelExt17.Name = "panelExt17";
			this.panelExt17.Size = new System.Drawing.Size(356, 1);
			this.panelExt17.TabIndex = 7;
			// 
			// labelExt12
			// 
			this.labelExt12.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelExt12.Location = new System.Drawing.Point(3, 32);
			this.labelExt12.Name = "labelExt12";
			this.labelExt12.Size = new System.Drawing.Size(232, 14);
			this.labelExt12.TabIndex = 4;
			this.labelExt12.Tag = "TXT43_WORK";
			this.labelExt12.Text = "( 동일 날짜에는 적용되지 않습니다. )";
			this.labelExt12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// imagePanel6
			// 
			this.imagePanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel6.LeftImage = null;
			this.imagePanel6.Location = new System.Drawing.Point(0, 0);
			this.imagePanel6.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.imagePanel6.Name = "imagePanel6";
			this.imagePanel6.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel6.PatternImage = null;
			this.imagePanel6.RightImage = null;
			this.imagePanel6.Size = new System.Drawing.Size(602, 27);
			this.imagePanel6.TabIndex = 5;
			this.imagePanel6.TitleText = "BARCODE 시간 분리 설정";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.Controls.Add(this.panel34, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.imagePanel5, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(618, 167);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(606, 248);
			this.tableLayoutPanel4.TabIndex = 257;
			// 
			// panel34
			// 
			this.panel34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.panel34.Controls.Add(this.lblFORMAT지정);
			this.panel34.Controls.Add(this.nud연결대수);
			this.panel34.Controls.Add(this.lbl통신속도);
			this.panel34.Controls.Add(this.cbo리더기종류);
			this.panel34.Controls.Add(this.lbl포트지정);
			this.panel34.Controls.Add(this.lbl연결대수);
			this.panel34.Controls.Add(this.cbo통신속도);
			this.panel34.Controls.Add(this.lbl리더기종류);
			this.panel34.Controls.Add(this.cbo포트지정);
			this.panel34.Controls.Add(this.txtFORMAT지정1);
			this.panel34.Controls.Add(this.txtFORMAT지정);
			this.panel34.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel34.Location = new System.Drawing.Point(0, 27);
			this.panel34.Margin = new System.Windows.Forms.Padding(0);
			this.panel34.Name = "panel34";
			this.panel34.Size = new System.Drawing.Size(606, 221);
			this.panel34.TabIndex = 4;
			// 
			// lblFORMAT지정
			// 
			this.lblFORMAT지정.Location = new System.Drawing.Point(9, 123);
			this.lblFORMAT지정.Name = "lblFORMAT지정";
			this.lblFORMAT지정.Size = new System.Drawing.Size(150, 16);
			this.lblFORMAT지정.TabIndex = 0;
			this.lblFORMAT지정.Tag = "TXT48_WORK";
			this.lblFORMAT지정.Text = "FORMAT 지정";
			this.lblFORMAT지정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nud연결대수
			// 
			this.nud연결대수.Location = new System.Drawing.Point(165, 66);
			this.nud연결대수.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.nud연결대수.Modified = false;
			this.nud연결대수.Name = "nud연결대수";
			this.nud연결대수.Size = new System.Drawing.Size(70, 21);
			this.nud연결대수.TabIndex = 2;
			// 
			// lbl통신속도
			// 
			this.lbl통신속도.Location = new System.Drawing.Point(9, 45);
			this.lbl통신속도.Name = "lbl통신속도";
			this.lbl통신속도.Size = new System.Drawing.Size(150, 16);
			this.lbl통신속도.TabIndex = 0;
			this.lbl통신속도.Tag = "TXT46_WORK";
			this.lbl통신속도.Text = "통신속도";
			this.lbl통신속도.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo리더기종류
			// 
			this.cbo리더기종류.AutoDropDown = true;
			this.cbo리더기종류.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo리더기종류.ItemHeight = 12;
			this.cbo리더기종류.Items.AddRange(new object[] {
            "S1 Barcode Reader"});
			this.cbo리더기종류.Location = new System.Drawing.Point(165, 93);
			this.cbo리더기종류.Name = "cbo리더기종류";
			this.cbo리더기종류.Size = new System.Drawing.Size(253, 20);
			this.cbo리더기종류.TabIndex = 3;
			// 
			// lbl포트지정
			// 
			this.lbl포트지정.Location = new System.Drawing.Point(9, 18);
			this.lbl포트지정.Name = "lbl포트지정";
			this.lbl포트지정.Size = new System.Drawing.Size(150, 16);
			this.lbl포트지정.TabIndex = 0;
			this.lbl포트지정.Tag = "TXT44_WORK";
			this.lbl포트지정.Text = "포트지정";
			this.lbl포트지정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl연결대수
			// 
			this.lbl연결대수.Location = new System.Drawing.Point(9, 69);
			this.lbl연결대수.Name = "lbl연결대수";
			this.lbl연결대수.Size = new System.Drawing.Size(150, 16);
			this.lbl연결대수.TabIndex = 0;
			this.lbl연결대수.Tag = "TXT45_WORK";
			this.lbl연결대수.Text = "연결대수";
			this.lbl연결대수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo통신속도
			// 
			this.cbo통신속도.AutoDropDown = true;
			this.cbo통신속도.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo통신속도.ItemHeight = 12;
			this.cbo통신속도.Items.AddRange(new object[] {
            "9600 bps",
            "19200 bps",
            "38400 bps"});
			this.cbo통신속도.Location = new System.Drawing.Point(165, 40);
			this.cbo통신속도.Name = "cbo통신속도";
			this.cbo통신속도.Size = new System.Drawing.Size(150, 20);
			this.cbo통신속도.TabIndex = 1;
			// 
			// lbl리더기종류
			// 
			this.lbl리더기종류.Location = new System.Drawing.Point(9, 96);
			this.lbl리더기종류.Name = "lbl리더기종류";
			this.lbl리더기종류.Size = new System.Drawing.Size(150, 16);
			this.lbl리더기종류.TabIndex = 0;
			this.lbl리더기종류.Tag = "TXT47_WORK";
			this.lbl리더기종류.Text = "리더기 종류";
			this.lbl리더기종류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo포트지정
			// 
			this.cbo포트지정.AutoDropDown = true;
			this.cbo포트지정.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo포트지정.ItemHeight = 12;
			this.cbo포트지정.Items.AddRange(new object[] {
            "포트 1 (COM1)",
            "포트 2 (COM2)",
            "포트 3 (COM3)",
            "포트 4 (COM4)"});
			this.cbo포트지정.Location = new System.Drawing.Point(165, 14);
			this.cbo포트지정.Name = "cbo포트지정";
			this.cbo포트지정.Size = new System.Drawing.Size(150, 20);
			this.cbo포트지정.TabIndex = 0;
			// 
			// txtFORMAT지정1
			// 
			this.txtFORMAT지정1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFORMAT지정1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFORMAT지정1.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFORMAT지정1.Location = new System.Drawing.Point(165, 144);
			this.txtFORMAT지정1.Name = "txtFORMAT지정1";
			this.txtFORMAT지정1.Size = new System.Drawing.Size(253, 21);
			this.txtFORMAT지정1.TabIndex = 5;
			// 
			// txtFORMAT지정
			// 
			this.txtFORMAT지정.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFORMAT지정.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFORMAT지정.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFORMAT지정.Location = new System.Drawing.Point(165, 118);
			this.txtFORMAT지정.Name = "txtFORMAT지정";
			this.txtFORMAT지정.ReadOnly = true;
			this.txtFORMAT지정.Size = new System.Drawing.Size(180, 21);
			this.txtFORMAT지정.TabIndex = 4;
			this.txtFORMAT지정.TabStop = false;
			// 
			// imagePanel5
			// 
			this.imagePanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel5.LeftImage = null;
			this.imagePanel5.Location = new System.Drawing.Point(0, 0);
			this.imagePanel5.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.imagePanel5.Name = "imagePanel5";
			this.imagePanel5.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel5.PatternImage = null;
			this.imagePanel5.RightImage = null;
			this.imagePanel5.Size = new System.Drawing.Size(603, 27);
			this.imagePanel5.TabIndex = 5;
			this.imagePanel5.TitleText = "리더기 양식 지정";
			// 
			// tpg사원카드번호연결
			// 
			this.tpg사원카드번호연결.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.tpg사원카드번호연결.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tpg사원카드번호연결.Controls.Add(this.tableLayoutPanel8);
			this.tpg사원카드번호연결.ImageIndex = 1;
			this.tpg사원카드번호연결.Location = new System.Drawing.Point(4, 24);
			this.tpg사원카드번호연결.Name = "tpg사원카드번호연결";
			this.tpg사원카드번호연결.Size = new System.Drawing.Size(1251, 787);
			this.tpg사원카드번호연결.TabIndex = 1;
			this.tpg사원카드번호연결.Tag = "TAB12_WORK";
			this.tpg사원카드번호연결.Text = "사원카드번호 연결";
			this.tpg사원카드번호연결.Visible = false;
			// 
			// tableLayoutPanel8
			// 
			this.tableLayoutPanel8.ColumnCount = 1;
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel8.Controls.Add(this.imagePanel7, 0, 0);
			this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 1;
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(1247, 783);
			this.tableLayoutPanel8.TabIndex = 101;
			// 
			// imagePanel7
			// 
			this.imagePanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel7.Controls.Add(this.tableLayoutPanel7);
			this.imagePanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel7.LeftImage = null;
			this.imagePanel7.Location = new System.Drawing.Point(3, 3);
			this.imagePanel7.Name = "imagePanel7";
			this.imagePanel7.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel7.PatternImage = null;
			this.imagePanel7.RightImage = null;
			this.imagePanel7.Size = new System.Drawing.Size(1241, 777);
			this.imagePanel7.TabIndex = 256;
			this.imagePanel7.TitleText = "사원카드번호 연결";
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel7.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel7.ColumnCount = 1;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Controls.Add(this.m_pnlEmpConnect, 0, 2);
			this.tableLayoutPanel7.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 28);
			this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(8);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 3;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(1235, 746);
			this.tableLayoutPanel7.TabIndex = 100;
			// 
			// m_pnlEmpConnect
			// 
			this.m_pnlEmpConnect.Controls.Add(this._flex사원카드번호연결);
			this.m_pnlEmpConnect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_pnlEmpConnect.Location = new System.Drawing.Point(3, 105);
			this.m_pnlEmpConnect.Name = "m_pnlEmpConnect";
			this.m_pnlEmpConnect.Size = new System.Drawing.Size(1229, 638);
			this.m_pnlEmpConnect.TabIndex = 1;
			// 
			// _flex사원카드번호연결
			// 
			this._flex사원카드번호연결.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex사원카드번호연결.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex사원카드번호연결.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex사원카드번호연결.AutoResize = false;
			this._flex사원카드번호연결.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex사원카드번호연결.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex사원카드번호연결.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex사원카드번호연결.EnabledHeaderCheck = true;
			this._flex사원카드번호연결.Font = new System.Drawing.Font("굴림", 9F);
			this._flex사원카드번호연결.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex사원카드번호연결.Location = new System.Drawing.Point(0, 0);
			this._flex사원카드번호연결.Name = "_flex사원카드번호연결";
			this._flex사원카드번호연결.Rows.Count = 1;
			this._flex사원카드번호연결.Rows.DefaultSize = 20;
			this._flex사원카드번호연결.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex사원카드번호연결.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex사원카드번호연결.ShowSort = false;
			this._flex사원카드번호연결.Size = new System.Drawing.Size(1229, 638);
			this._flex사원카드번호연결.StyleInfo = resources.GetString("_flex사원카드번호연결.StyleInfo");
			this._flex사원카드번호연결.TabIndex = 0;
			this._flex사원카드번호연결.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1229, 64);
			this.oneGrid1.TabIndex = 257;
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
			this.oneGridItem1.Size = new System.Drawing.Size(1219, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl재직구분);
			this.bpPanelControl3.Controls.Add(this.cbo재직구분);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl재직구분
			// 
			this.lbl재직구분.BackColor = System.Drawing.Color.Transparent;
			this.lbl재직구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl재직구분.Location = new System.Drawing.Point(0, 0);
			this.lbl재직구분.Name = "lbl재직구분";
			this.lbl재직구분.Size = new System.Drawing.Size(100, 23);
			this.lbl재직구분.TabIndex = 0;
			this.lbl재직구분.Tag = "CD_INCOM";
			this.lbl재직구분.Text = "재직구분";
			this.lbl재직구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo재직구분
			// 
			this.cbo재직구분.AutoDropDown = true;
			this.cbo재직구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo재직구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo재직구분.ItemHeight = 12;
			this.cbo재직구분.Location = new System.Drawing.Point(106, 0);
			this.cbo재직구분.Name = "cbo재직구분";
			this.cbo재직구분.Size = new System.Drawing.Size(186, 20);
			this.cbo재직구분.TabIndex = 2;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.lbl부서);
			this.bpPanelControl2.Controls.Add(this.bpc부서);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// lbl부서
			// 
			this.lbl부서.BackColor = System.Drawing.Color.Transparent;
			this.lbl부서.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl부서.Location = new System.Drawing.Point(0, 0);
			this.lbl부서.Name = "lbl부서";
			this.lbl부서.Size = new System.Drawing.Size(100, 23);
			this.lbl부서.TabIndex = 0;
			this.lbl부서.Tag = "CD_DEPT";
			this.lbl부서.Text = "부서";
			this.lbl부서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpc부서
			// 
			this.bpc부서.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB1;
			this.bpc부서.Location = new System.Drawing.Point(106, 0);
			this.bpc부서.Name = "bpc부서";
			this.bpc부서.SetDefaultValue = true;
			this.bpc부서.Size = new System.Drawing.Size(186, 21);
			this.bpc부서.TabIndex = 1;
			this.bpc부서.TabStop = false;
			this.bpc부서.Text = "bpComboBox1";
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.bpc사업장);
			this.bpPanelControl1.Controls.Add(this.lbl사업장);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// bpc사업장
			// 
			this.bpc사업장.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB1;
			this.bpc사업장.Location = new System.Drawing.Point(106, 0);
			this.bpc사업장.Name = "bpc사업장";
			this.bpc사업장.SetDefaultValue = true;
			this.bpc사업장.Size = new System.Drawing.Size(186, 21);
			this.bpc사업장.TabIndex = 9;
			this.bpc사업장.TabStop = false;
			this.bpc사업장.Text = "bpComboBox1";
			// 
			// lbl사업장
			// 
			this.lbl사업장.BackColor = System.Drawing.Color.Transparent;
			this.lbl사업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl사업장.Location = new System.Drawing.Point(0, 0);
			this.lbl사업장.Name = "lbl사업장";
			this.lbl사업장.Size = new System.Drawing.Size(100, 23);
			this.lbl사업장.TabIndex = 0;
			this.lbl사업장.Tag = "CD_BIZAREA";
			this.lbl사업장.Text = "사업장";
			this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bppnl입사일자);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1219, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bppnl입사일자
			// 
			this.bppnl입사일자.Controls.Add(this.dtp입사일자);
			this.bppnl입사일자.Controls.Add(this.lbl입사일자);
			this.bppnl입사일자.Location = new System.Drawing.Point(2, 1);
			this.bppnl입사일자.Name = "bppnl입사일자";
			this.bppnl입사일자.Size = new System.Drawing.Size(292, 23);
			this.bppnl입사일자.TabIndex = 0;
			this.bppnl입사일자.Text = "bpPanelControl4";
			// 
			// dtp입사일자
			// 
			this.dtp입사일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp입사일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp입사일자.IsNecessaryCondition = true;
			this.dtp입사일자.Location = new System.Drawing.Point(107, 0);
			this.dtp입사일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp입사일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp입사일자.Name = "dtp입사일자";
			this.dtp입사일자.Size = new System.Drawing.Size(185, 21);
			this.dtp입사일자.TabIndex = 8;
			// 
			// lbl입사일자
			// 
			this.lbl입사일자.BackColor = System.Drawing.Color.Transparent;
			this.lbl입사일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl입사일자.Location = new System.Drawing.Point(0, 0);
			this.lbl입사일자.Name = "lbl입사일자";
			this.lbl입사일자.Size = new System.Drawing.Size(100, 23);
			this.lbl입사일자.TabIndex = 7;
			this.lbl입사일자.Tag = "DT_ENTER";
			this.lbl입사일자.Text = "입사일자";
			this.lbl입사일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn카드정보올리기);
			this.flowLayoutPanel1.Controls.Add(this.btn사번복사);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 73);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1229, 26);
			this.flowLayoutPanel1.TabIndex = 258;
			// 
			// btn카드정보올리기
			// 
			this.btn카드정보올리기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn카드정보올리기.BackColor = System.Drawing.Color.White;
			this.btn카드정보올리기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn카드정보올리기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn카드정보올리기.Location = new System.Drawing.Point(1114, 3);
			this.btn카드정보올리기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn카드정보올리기.Name = "btn카드정보올리기";
			this.btn카드정보올리기.Size = new System.Drawing.Size(112, 19);
			this.btn카드정보올리기.TabIndex = 99;
			this.btn카드정보올리기.TabStop = false;
			this.btn카드정보올리기.Tag = "BUT30_WORK";
			this.btn카드정보올리기.Text = "카드정보올리기";
			this.btn카드정보올리기.UseVisualStyleBackColor = false;
			// 
			// btn사번복사
			// 
			this.btn사번복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn사번복사.BackColor = System.Drawing.Color.White;
			this.btn사번복사.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn사번복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn사번복사.Location = new System.Drawing.Point(1044, 3);
			this.btn사번복사.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn사번복사.Name = "btn사번복사";
			this.btn사번복사.Size = new System.Drawing.Size(64, 19);
			this.btn사번복사.TabIndex = 6;
			this.btn사번복사.TabStop = false;
			this.btn사번복사.Tag = "BUT17_WORK";
			this.btn사번복사.Text = "사번복사";
			this.btn사번복사.UseVisualStyleBackColor = false;
			// 
			// tpg데이터조회
			// 
			this.tpg데이터조회.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.tpg데이터조회.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tpg데이터조회.Controls.Add(this.tableLayoutPanel10);
			this.tpg데이터조회.ImageIndex = 2;
			this.tpg데이터조회.Location = new System.Drawing.Point(4, 24);
			this.tpg데이터조회.Name = "tpg데이터조회";
			this.tpg데이터조회.Size = new System.Drawing.Size(1251, 787);
			this.tpg데이터조회.TabIndex = 2;
			this.tpg데이터조회.Tag = "TAB13_WORK";
			this.tpg데이터조회.Text = "데이터 조회";
			// 
			// tableLayoutPanel10
			// 
			this.tableLayoutPanel10.ColumnCount = 1;
			this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel10.Controls.Add(this.imagePanel8, 0, 0);
			this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel10.Name = "tableLayoutPanel10";
			this.tableLayoutPanel10.RowCount = 1;
			this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel10.Size = new System.Drawing.Size(1247, 783);
			this.tableLayoutPanel10.TabIndex = 257;
			// 
			// imagePanel8
			// 
			this.imagePanel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel8.Controls.Add(this.tableLayoutPanel9);
			this.imagePanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel8.LeftImage = null;
			this.imagePanel8.Location = new System.Drawing.Point(3, 3);
			this.imagePanel8.Name = "imagePanel8";
			this.imagePanel8.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel8.PatternImage = null;
			this.imagePanel8.RightImage = null;
			this.imagePanel8.Size = new System.Drawing.Size(1241, 777);
			this.imagePanel8.TabIndex = 256;
			this.imagePanel8.TitleText = "데이터 조회";
			// 
			// tableLayoutPanel9
			// 
			this.tableLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel9.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel9.ColumnCount = 1;
			this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel9.Controls.Add(this.m_pnlDataSearch, 0, 2);
			this.tableLayoutPanel9.Controls.Add(this.oneGrid2, 0, 0);
			this.tableLayoutPanel9.Controls.Add(this.flowLayoutPanel2, 0, 1);
			this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 27);
			this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 3;
			this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel9.Size = new System.Drawing.Size(1231, 750);
			this.tableLayoutPanel9.TabIndex = 256;
			// 
			// m_pnlDataSearch
			// 
			this.m_pnlDataSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_pnlDataSearch.Controls.Add(this.tabControl2);
			this.m_pnlDataSearch.Location = new System.Drawing.Point(3, 105);
			this.m_pnlDataSearch.Name = "m_pnlDataSearch";
			this.m_pnlDataSearch.Size = new System.Drawing.Size(1225, 642);
			this.m_pnlDataSearch.TabIndex = 1;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tpg승인대기);
			this.tabControl2.Controls.Add(this.tpg승인);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(1225, 642);
			this.tabControl2.TabIndex = 0;
			// 
			// tpg승인대기
			// 
			this.tpg승인대기.Controls.Add(this._flex승인대기);
			this.tpg승인대기.Location = new System.Drawing.Point(4, 22);
			this.tpg승인대기.Name = "tpg승인대기";
			this.tpg승인대기.Padding = new System.Windows.Forms.Padding(3);
			this.tpg승인대기.Size = new System.Drawing.Size(1217, 616);
			this.tpg승인대기.TabIndex = 0;
			this.tpg승인대기.Text = "승인대기";
			this.tpg승인대기.UseVisualStyleBackColor = true;
			// 
			// _flex승인대기
			// 
			this._flex승인대기.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex승인대기.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex승인대기.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex승인대기.AutoResize = false;
			this._flex승인대기.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex승인대기.Cursor = System.Windows.Forms.Cursors.Default;
			this._flex승인대기.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex승인대기.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex승인대기.EnabledHeaderCheck = true;
			this._flex승인대기.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex승인대기.Location = new System.Drawing.Point(3, 3);
			this._flex승인대기.Name = "_flex승인대기";
			this._flex승인대기.Rows.Count = 1;
			this._flex승인대기.Rows.DefaultSize = 20;
			this._flex승인대기.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex승인대기.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex승인대기.ShowSort = false;
			this._flex승인대기.Size = new System.Drawing.Size(1211, 610);
			this._flex승인대기.StyleInfo = resources.GetString("_flex승인대기.StyleInfo");
			this._flex승인대기.TabIndex = 0;
			this._flex승인대기.UseGridCalculator = true;
			// 
			// tpg승인
			// 
			this.tpg승인.Controls.Add(this.tabControl3);
			this.tpg승인.Location = new System.Drawing.Point(4, 22);
			this.tpg승인.Name = "tpg승인";
			this.tpg승인.Padding = new System.Windows.Forms.Padding(3);
			this.tpg승인.Size = new System.Drawing.Size(1217, 616);
			this.tpg승인.TabIndex = 1;
			this.tpg승인.Text = "승인";
			this.tpg승인.UseVisualStyleBackColor = true;
			// 
			// tabControl3
			// 
			this.tabControl3.Controls.Add(this.tpg승인일반);
			this.tabControl3.Controls.Add(this.tpg승인Pivot);
			this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl3.Location = new System.Drawing.Point(3, 3);
			this.tabControl3.Name = "tabControl3";
			this.tabControl3.SelectedIndex = 0;
			this.tabControl3.Size = new System.Drawing.Size(1211, 610);
			this.tabControl3.TabIndex = 0;
			// 
			// tpg승인일반
			// 
			this.tpg승인일반.Controls.Add(this._flex승인일반);
			this.tpg승인일반.Location = new System.Drawing.Point(4, 22);
			this.tpg승인일반.Name = "tpg승인일반";
			this.tpg승인일반.Padding = new System.Windows.Forms.Padding(3);
			this.tpg승인일반.Size = new System.Drawing.Size(1203, 584);
			this.tpg승인일반.TabIndex = 0;
			this.tpg승인일반.Text = "일반";
			this.tpg승인일반.UseVisualStyleBackColor = true;
			// 
			// _flex승인일반
			// 
			this._flex승인일반.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex승인일반.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex승인일반.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex승인일반.AutoResize = false;
			this._flex승인일반.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex승인일반.Cursor = System.Windows.Forms.Cursors.Default;
			this._flex승인일반.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex승인일반.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex승인일반.EnabledHeaderCheck = true;
			this._flex승인일반.Font = new System.Drawing.Font("굴림", 9F);
			this._flex승인일반.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex승인일반.Location = new System.Drawing.Point(3, 3);
			this._flex승인일반.Name = "_flex승인일반";
			this._flex승인일반.Rows.Count = 1;
			this._flex승인일반.Rows.DefaultSize = 20;
			this._flex승인일반.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex승인일반.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex승인일반.ShowSort = false;
			this._flex승인일반.Size = new System.Drawing.Size(1197, 578);
			this._flex승인일반.StyleInfo = resources.GetString("_flex승인일반.StyleInfo");
			this._flex승인일반.TabIndex = 0;
			this._flex승인일반.UseGridCalculator = true;
			// 
			// tpg승인Pivot
			// 
			this.tpg승인Pivot.Controls.Add(this._flex승인Pivot);
			this.tpg승인Pivot.Location = new System.Drawing.Point(4, 22);
			this.tpg승인Pivot.Name = "tpg승인Pivot";
			this.tpg승인Pivot.Padding = new System.Windows.Forms.Padding(3);
			this.tpg승인Pivot.Size = new System.Drawing.Size(1203, 584);
			this.tpg승인Pivot.TabIndex = 1;
			this.tpg승인Pivot.Text = "Pivot";
			this.tpg승인Pivot.UseVisualStyleBackColor = true;
			// 
			// _flex승인Pivot
			// 
			this._flex승인Pivot.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex승인Pivot.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex승인Pivot.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex승인Pivot.AutoResize = false;
			this._flex승인Pivot.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex승인Pivot.Cursor = System.Windows.Forms.Cursors.Default;
			this._flex승인Pivot.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex승인Pivot.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex승인Pivot.EnabledHeaderCheck = true;
			this._flex승인Pivot.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex승인Pivot.Location = new System.Drawing.Point(3, 3);
			this._flex승인Pivot.Name = "_flex승인Pivot";
			this._flex승인Pivot.Rows.Count = 1;
			this._flex승인Pivot.Rows.DefaultSize = 20;
			this._flex승인Pivot.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex승인Pivot.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex승인Pivot.ShowSort = false;
			this._flex승인Pivot.Size = new System.Drawing.Size(1197, 578);
			this._flex승인Pivot.StyleInfo = resources.GetString("_flex승인Pivot.StyleInfo");
			this._flex승인Pivot.TabIndex = 0;
			this._flex승인Pivot.UseGridCalculator = true;
			// 
			// oneGrid2
			// 
			this.oneGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid2.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem3,
            this.oneGridItem4});
			this.oneGrid2.Location = new System.Drawing.Point(3, 3);
			this.oneGrid2.Name = "oneGrid2";
			this.oneGrid2.Size = new System.Drawing.Size(1225, 62);
			this.oneGrid2.TabIndex = 257;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl6);
			this.oneGridItem3.Controls.Add(this.bpPanelControl5);
			this.oneGridItem3.Controls.Add(this.bpPanelControl4);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1215, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 0;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.lbl사원);
			this.bpPanelControl6.Controls.Add(this.bpc사원);
			this.bpPanelControl6.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl6.TabIndex = 2;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// lbl사원
			// 
			this.lbl사원.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl사원.Location = new System.Drawing.Point(0, 0);
			this.lbl사원.Name = "lbl사원";
			this.lbl사원.Size = new System.Drawing.Size(100, 23);
			this.lbl사원.TabIndex = 0;
			this.lbl사원.Tag = "CD_DEPT";
			this.lbl사원.Text = "사원";
			this.lbl사원.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpc사원
			// 
			this.bpc사원.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB1;
			this.bpc사원.Location = new System.Drawing.Point(106, 0);
			this.bpc사원.Name = "bpc사원";
			this.bpc사원.SetDefaultValue = true;
			this.bpc사원.Size = new System.Drawing.Size(186, 21);
			this.bpc사원.TabIndex = 1;
			this.bpc사원.TabStop = false;
			this.bpc사원.Text = "bpComboBox1";
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.lbl부서1);
			this.bpPanelControl5.Controls.Add(this.bpc부서1);
			this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl5.TabIndex = 1;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// lbl부서1
			// 
			this.lbl부서1.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl부서1.Location = new System.Drawing.Point(0, 0);
			this.lbl부서1.Name = "lbl부서1";
			this.lbl부서1.Size = new System.Drawing.Size(100, 23);
			this.lbl부서1.TabIndex = 0;
			this.lbl부서1.Tag = "CD_DEPT";
			this.lbl부서1.Text = "부서";
			this.lbl부서1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpc부서1
			// 
			this.bpc부서1.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc부서1.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB1;
			this.bpc부서1.Location = new System.Drawing.Point(106, 0);
			this.bpc부서1.Name = "bpc부서1";
			this.bpc부서1.SetDefaultValue = true;
			this.bpc부서1.Size = new System.Drawing.Size(186, 21);
			this.bpc부서1.TabIndex = 1;
			this.bpc부서1.TabStop = false;
			this.bpc부서1.Text = "bpComboBox1";
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.bpc사업장1);
			this.bpPanelControl4.Controls.Add(this.lbl사업장1);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// bpc사업장1
			// 
			this.bpc사업장1.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc사업장1.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB1;
			this.bpc사업장1.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.bpc사업장1.Location = new System.Drawing.Point(106, 0);
			this.bpc사업장1.Name = "bpc사업장1";
			this.bpc사업장1.SetDefaultValue = true;
			this.bpc사업장1.Size = new System.Drawing.Size(186, 21);
			this.bpc사업장1.TabIndex = 17;
			this.bpc사업장1.TabStop = false;
			this.bpc사업장1.Text = "bpComboBox1";
			// 
			// lbl사업장1
			// 
			this.lbl사업장1.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl사업장1.Location = new System.Drawing.Point(0, 0);
			this.lbl사업장1.Name = "lbl사업장1";
			this.lbl사업장1.Size = new System.Drawing.Size(100, 23);
			this.lbl사업장1.TabIndex = 0;
			this.lbl사업장1.Tag = "CD_BIZAREA";
			this.lbl사업장1.Text = "사업장";
			this.lbl사업장1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.Controls.Add(this.bppnl년월일);
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(1215, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 1;
			// 
			// bppnl년월일
			// 
			this.bppnl년월일.Controls.Add(this.dtp년월일);
			this.bppnl년월일.Controls.Add(this.lbl년월일);
			this.bppnl년월일.Location = new System.Drawing.Point(2, 1);
			this.bppnl년월일.Name = "bppnl년월일";
			this.bppnl년월일.Size = new System.Drawing.Size(292, 23);
			this.bppnl년월일.TabIndex = 2;
			this.bppnl년월일.Text = "bpPanelControl6";
			// 
			// dtp년월일
			// 
			this.dtp년월일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp년월일.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp년월일.IsNecessaryCondition = true;
			this.dtp년월일.Location = new System.Drawing.Point(107, 0);
			this.dtp년월일.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp년월일.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp년월일.Name = "dtp년월일";
			this.dtp년월일.Size = new System.Drawing.Size(185, 21);
			this.dtp년월일.TabIndex = 1;
			// 
			// lbl년월일
			// 
			this.lbl년월일.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl년월일.Location = new System.Drawing.Point(0, 0);
			this.lbl년월일.Name = "lbl년월일";
			this.lbl년월일.Size = new System.Drawing.Size(100, 23);
			this.lbl년월일.TabIndex = 0;
			this.lbl년월일.Text = "년월일";
			this.lbl년월일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btnFPMS데이터읽기);
			this.flowLayoutPanel2.Controls.Add(this.btn데이터읽기);
			this.flowLayoutPanel2.Controls.Add(this.btnCaps데이터읽기);
			this.flowLayoutPanel2.Controls.Add(this.btnSecom데이터읽기);
			this.flowLayoutPanel2.Controls.Add(this.btn승인);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 71);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(1225, 28);
			this.flowLayoutPanel2.TabIndex = 258;
			// 
			// btnFPMS데이터읽기
			// 
			this.btnFPMS데이터읽기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFPMS데이터읽기.BackColor = System.Drawing.Color.White;
			this.btnFPMS데이터읽기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnFPMS데이터읽기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFPMS데이터읽기.Location = new System.Drawing.Point(1110, 3);
			this.btnFPMS데이터읽기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnFPMS데이터읽기.Name = "btnFPMS데이터읽기";
			this.btnFPMS데이터읽기.Size = new System.Drawing.Size(112, 19);
			this.btnFPMS데이터읽기.TabIndex = 5;
			this.btnFPMS데이터읽기.TabStop = false;
			this.btnFPMS데이터읽기.Tag = "";
			this.btnFPMS데이터읽기.Text = "FPMS 데이터 읽기";
			this.btnFPMS데이터읽기.UseVisualStyleBackColor = false;
			// 
			// btn데이터읽기
			// 
			this.btn데이터읽기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn데이터읽기.BackColor = System.Drawing.Color.White;
			this.btn데이터읽기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn데이터읽기.Enabled = false;
			this.btn데이터읽기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn데이터읽기.Location = new System.Drawing.Point(1015, 3);
			this.btn데이터읽기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn데이터읽기.Name = "btn데이터읽기";
			this.btn데이터읽기.Size = new System.Drawing.Size(89, 19);
			this.btn데이터읽기.TabIndex = 4;
			this.btn데이터읽기.TabStop = false;
			this.btn데이터읽기.Tag = "BUT18_WORK";
			this.btn데이터읽기.Text = "데이터 읽기";
			this.btn데이터읽기.UseVisualStyleBackColor = false;
			// 
			// btnCaps데이터읽기
			// 
			this.btnCaps데이터읽기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCaps데이터읽기.BackColor = System.Drawing.Color.White;
			this.btnCaps데이터읽기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCaps데이터읽기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCaps데이터읽기.Location = new System.Drawing.Point(897, 3);
			this.btnCaps데이터읽기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnCaps데이터읽기.Name = "btnCaps데이터읽기";
			this.btnCaps데이터읽기.Size = new System.Drawing.Size(112, 19);
			this.btnCaps데이터읽기.TabIndex = 6;
			this.btnCaps데이터읽기.TabStop = false;
			this.btnCaps데이터읽기.Tag = "";
			this.btnCaps데이터읽기.Text = "Caps 데이터 읽기";
			this.btnCaps데이터읽기.UseVisualStyleBackColor = false;
			this.btnCaps데이터읽기.Visible = false;
			// 
			// btnSecom데이터읽기
			// 
			this.btnSecom데이터읽기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSecom데이터읽기.BackColor = System.Drawing.Color.White;
			this.btnSecom데이터읽기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSecom데이터읽기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSecom데이터읽기.Location = new System.Drawing.Point(776, 3);
			this.btnSecom데이터읽기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnSecom데이터읽기.Name = "btnSecom데이터읽기";
			this.btnSecom데이터읽기.Size = new System.Drawing.Size(115, 19);
			this.btnSecom데이터읽기.TabIndex = 7;
			this.btnSecom데이터읽기.TabStop = false;
			this.btnSecom데이터읽기.Tag = "";
			this.btnSecom데이터읽기.Text = "Secom 데이터 읽기";
			this.btnSecom데이터읽기.UseVisualStyleBackColor = false;
			this.btnSecom데이터읽기.Visible = false;
			// 
			// btn승인
			// 
			this.btn승인.BackColor = System.Drawing.Color.Transparent;
			this.btn승인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn승인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn승인.Location = new System.Drawing.Point(700, 3);
			this.btn승인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn승인.Name = "btn승인";
			this.btn승인.Size = new System.Drawing.Size(70, 19);
			this.btn승인.TabIndex = 8;
			this.btn승인.TabStop = false;
			this.btn승인.Text = "승인";
			this.btn승인.UseVisualStyleBackColor = false;
			// 
			// tableLayoutPanel12
			// 
			this.tableLayoutPanel12.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel12.ColumnCount = 1;
			this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel12.Controls.Add(this.panelExt8, 0, 0);
			this.tableLayoutPanel12.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel12.Name = "tableLayoutPanel12";
			this.tableLayoutPanel12.RowCount = 2;
			this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel12.Size = new System.Drawing.Size(200, 100);
			this.tableLayoutPanel12.TabIndex = 0;
			// 
			// panelExt8
			// 
			this.panelExt8.Controls.Add(this.labelExt2);
			this.panelExt8.Location = new System.Drawing.Point(3, 3);
			this.panelExt8.Name = "panelExt8";
			this.panelExt8.Size = new System.Drawing.Size(366, 14);
			this.panelExt8.TabIndex = 0;
			// 
			// labelExt2
			// 
			this.labelExt2.BackColor = System.Drawing.Color.Transparent;
			this.labelExt2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt2.Location = new System.Drawing.Point(20, 5);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(200, 16);
			this.labelExt2.TabIndex = 0;
			this.labelExt2.Tag = "TXT37_WORK";
			this.labelExt2.Text = "근태 코드 연결";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panelExt9
			// 
			this.panelExt9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelExt9.Controls.Add(this.panelExt10);
			this.panelExt9.Controls.Add(this.panelExt11);
			this.panelExt9.Controls.Add(this.textBoxExt1);
			this.panelExt9.Controls.Add(this.textBoxExt2);
			this.panelExt9.Controls.Add(this.textBoxExt3);
			this.panelExt9.Controls.Add(this.textBoxExt4);
			this.panelExt9.Controls.Add(this.panelExt12);
			this.panelExt9.Controls.Add(this.panelExt13);
			this.panelExt9.Location = new System.Drawing.Point(3, 32);
			this.panelExt9.Name = "panelExt9";
			this.panelExt9.Size = new System.Drawing.Size(366, 118);
			this.panelExt9.TabIndex = 3;
			// 
			// panelExt10
			// 
			this.panelExt10.BackColor = System.Drawing.Color.Transparent;
			this.panelExt10.Location = new System.Drawing.Point(5, 87);
			this.panelExt10.Name = "panelExt10";
			this.panelExt10.Size = new System.Drawing.Size(356, 1);
			this.panelExt10.TabIndex = 8;
			// 
			// panelExt11
			// 
			this.panelExt11.BackColor = System.Drawing.Color.Transparent;
			this.panelExt11.Location = new System.Drawing.Point(5, 58);
			this.panelExt11.Name = "panelExt11";
			this.panelExt11.Size = new System.Drawing.Size(356, 1);
			this.panelExt11.TabIndex = 7;
			// 
			// textBoxExt1
			// 
			this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt1.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt1.Location = new System.Drawing.Point(106, 92);
			this.textBoxExt1.Name = "textBoxExt1";
			this.textBoxExt1.Size = new System.Drawing.Size(125, 21);
			this.textBoxExt1.TabIndex = 3;
			this.textBoxExt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxExt2
			// 
			this.textBoxExt2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt2.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt2.Location = new System.Drawing.Point(106, 34);
			this.textBoxExt2.Name = "textBoxExt2";
			this.textBoxExt2.Size = new System.Drawing.Size(125, 21);
			this.textBoxExt2.TabIndex = 1;
			this.textBoxExt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxExt3
			// 
			this.textBoxExt3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt3.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt3.Location = new System.Drawing.Point(106, 63);
			this.textBoxExt3.Name = "textBoxExt3";
			this.textBoxExt3.Size = new System.Drawing.Size(125, 21);
			this.textBoxExt3.TabIndex = 2;
			this.textBoxExt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxExt4
			// 
			this.textBoxExt4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt4.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt4.Location = new System.Drawing.Point(106, 5);
			this.textBoxExt4.Name = "textBoxExt4";
			this.textBoxExt4.Size = new System.Drawing.Size(125, 21);
			this.textBoxExt4.TabIndex = 0;
			this.textBoxExt4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// panelExt12
			// 
			this.panelExt12.BackColor = System.Drawing.Color.Transparent;
			this.panelExt12.Location = new System.Drawing.Point(5, 29);
			this.panelExt12.Name = "panelExt12";
			this.panelExt12.Size = new System.Drawing.Size(356, 1);
			this.panelExt12.TabIndex = 2;
			// 
			// panelExt13
			// 
			this.panelExt13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.panelExt13.Controls.Add(this.labelExt3);
			this.panelExt13.Controls.Add(this.labelExt4);
			this.panelExt13.Controls.Add(this.labelExt5);
			this.panelExt13.Controls.Add(this.labelExt6);
			this.panelExt13.Location = new System.Drawing.Point(1, 1);
			this.panelExt13.Name = "panelExt13";
			this.panelExt13.Size = new System.Drawing.Size(100, 115);
			this.panelExt13.TabIndex = 0;
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(3, 38);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(94, 14);
			this.labelExt3.TabIndex = 0;
			this.labelExt3.Tag = "CLOSE";
			this.labelExt3.Text = "퇴근";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelExt4
			// 
			this.labelExt4.Location = new System.Drawing.Point(3, 8);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(94, 14);
			this.labelExt4.TabIndex = 0;
			this.labelExt4.Tag = "START";
			this.labelExt4.Text = "출근";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelExt5
			// 
			this.labelExt5.Location = new System.Drawing.Point(3, 65);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Size = new System.Drawing.Size(94, 14);
			this.labelExt5.TabIndex = 0;
			this.labelExt5.Tag = "GOOUT";
			this.labelExt5.Text = "외출";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelExt6
			// 
			this.labelExt6.Location = new System.Drawing.Point(3, 95);
			this.labelExt6.Name = "labelExt6";
			this.labelExt6.Size = new System.Drawing.Size(94, 14);
			this.labelExt6.TabIndex = 0;
			this.labelExt6.Tag = "TXT43_WORK";
			this.labelExt6.Text = "복귀";
			this.labelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_HR_WBARCODE
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_HR_WBARCODE";
			this.Size = new System.Drawing.Size(1265, 861);
			this.TitleText = "BARCODE등록";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel11.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tpg리더기설정.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel29.ResumeLayout(false);
			this.panel29.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel28.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rdoFILE사용)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo리더기사용)).EndInit();
			this.panel33.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rdoText탭으로분리)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdoText일반)).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.panel55.ResumeLayout(false);
			this.panel55.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur기기번호탭분리위치)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태탭분리위치)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur카드번호탭분리위치)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근무시간탭분리위치)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태일자탭분리위치)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur기기번호자릿수)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태자릿수)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur카드번호자릿수)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근무시간자릿수)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur근태일자자릿수)).EndInit();
			this.tableLayoutPanel13.ResumeLayout(false);
			this.panelExt15.ResumeLayout(false);
			this.panelExt15.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.msk퇴근퇴근)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.msk출근출근)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.msk출근퇴근)).EndInit();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.panel34.ResumeLayout(false);
			this.panel34.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud연결대수)).EndInit();
			this.tpg사원카드번호연결.ResumeLayout(false);
			this.tableLayoutPanel8.ResumeLayout(false);
			this.imagePanel7.ResumeLayout(false);
			this.tableLayoutPanel7.ResumeLayout(false);
			this.m_pnlEmpConnect.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex사원카드번호연결)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bppnl입사일자.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tpg데이터조회.ResumeLayout(false);
			this.tableLayoutPanel10.ResumeLayout(false);
			this.imagePanel8.ResumeLayout(false);
			this.tableLayoutPanel9.ResumeLayout(false);
			this.m_pnlDataSearch.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tpg승인대기.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex승인대기)).EndInit();
			this.tpg승인.ResumeLayout(false);
			this.tabControl3.ResumeLayout(false);
			this.tpg승인일반.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex승인일반)).EndInit();
			this.tpg승인Pivot.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex승인Pivot)).EndInit();
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.oneGridItem4.ResumeLayout(false);
			this.bppnl년월일.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel12.ResumeLayout(false);
			this.panelExt8.ResumeLayout(false);
			this.panelExt9.ResumeLayout(false);
			this.panelExt9.PerformLayout();
			this.panelExt13.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private BpComboBox bpc부서1;
        private LabelExt lbl부서1;
        private LabelExt lbl사업장1;
        private BpComboBox bpc부서;
        private PanelExt m_pnlEmpConnect;
        private PanelExt m_pnlDataSearch;
        private FlexibleRoundedCornerBox panel28;
        private FlexibleRoundedCornerBox panel33;
        private FlexibleRoundedCornerBox panel34;
        private FlexibleRoundedCornerBox panel55;
        private LabelExt lblFORMAT지정;
        private LabelExt lbl통신속도;
        private LabelExt lbl포트지정;
        private LabelExt lbl연결대수;
        private LabelExt lbl리더기종류;
        private LabelExt lbl재직구분;
        private LabelExt lbl부서;
        private LabelExt lbl사업장;
        private LabelExt lbl년월일;
        private LabelExt lbl출근;
        private LabelExt lbl파일FORMAT;
        private LabelExt lbl탭분리위치;
        private LabelExt lbl자릿수;
        private LabelExt lbl시작포인트;
        private LabelExt lbl근태일자;
        private LabelExt lbl근무시간;
        private LabelExt lbl카드번호;
        private LabelExt lbl근태;
        private LabelExt lbl기기번호;
        private TextBoxExt txtFORMAT지정1;
        private TextBoxExt txtFORMAT지정;
        private TextBoxExt txt기기번호시작포인트;
        private TextBoxExt txt근태시작포인트;
        private TextBoxExt txt카드번호시작포인트;
        private TextBoxExt txt근무시간시작포인트;
        private TextBoxExt txt근태일자시작포인트;
        private DropDownComboBox cbo리더기종류;
        private DropDownComboBox cbo통신속도;
        private DropDownComboBox cbo포트지정;
        private CurrencyTextBox cur기기번호자릿수;
        private CurrencyTextBox cur근태자릿수;
        private CurrencyTextBox cur카드번호자릿수;
        private CurrencyTextBox cur근무시간자릿수;
        private CurrencyTextBox cur근태일자자릿수;
        private CurrencyTextBox cur기기번호탭분리위치;
        private CurrencyTextBox cur근태탭분리위치;
        private CurrencyTextBox cur카드번호탭분리위치;
        private CurrencyTextBox cur근무시간탭분리위치;
        private CurrencyTextBox cur근태일자탭분리위치;
        private RoundedButton btn데이터읽기;
        private RadioButtonExt rdoText탭으로분리;
        private RadioButtonExt rdoText일반;
        private RadioButtonExt rdoFILE사용;
        private RadioButtonExt rdo리더기사용;
        private NumericUpDownExt nud연결대수;
        private ImageList imageList1;
        private CheckBoxExt chk근태코드존재;
        private SaveFileDialog SaveFileDialog;
        private DropDownComboBox cbo재직구분;
        private LabelExt lbl입사일자;
        private TabControlExt tabControl;
        private TabPage tpg리더기설정;
        private TabPage tpg사원카드번호연결;
        private RoundedButton btn카드정보올리기;
        private RoundedButton btn사번복사;
        private TabPage tpg데이터조회;
        private Dass.FlexGrid.FlexGrid _flex사원카드번호연결;
        private Dass.FlexGrid.FlexGrid _flex승인일반;
        private bool isChanged;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel11;
        private FlexibleRoundedCornerBox panel29;
        private TextBoxExt txt근태코드연결복귀;
        private TextBoxExt txt근태코드연결퇴근;
        private TextBoxExt txt근태코드연결외출;
        private TextBoxExt txt근태코드연결출근;
        private LabelExt lbl근태코드연결퇴근;
        private LabelExt lbl근태코드연결출근;
        private LabelExt lbl근태코드연결외출;
        private LabelExt lbl근태코드연결복귀;
        private TableLayoutPanel tableLayoutPanel12;
        private PanelExt panelExt8;
        private LabelExt labelExt2;
        private PanelExt panelExt9;
        private PanelExt panelExt10;
        private PanelExt panelExt11;
        private TextBoxExt textBoxExt1;
        private TextBoxExt textBoxExt2;
        private TextBoxExt textBoxExt3;
        private TextBoxExt textBoxExt4;
        private PanelExt panelExt12;
        private PanelExt panelExt13;
        private LabelExt labelExt3;
        private LabelExt labelExt4;
        private LabelExt labelExt5;
        private LabelExt labelExt6;
        private TableLayoutPanel tableLayoutPanel13;
        private FlexibleRoundedCornerBox panelExt15;
        private PanelExt panelExt16;
        private PanelExt panelExt17;
        private LabelExt lbl출근퇴근;
        private CheckBoxExt chk시간;
        private LabelExt labelExt12;
        private MaskedEditBox msk퇴근퇴근;
        private MaskedEditBox msk출근출근;
        private MaskedEditBox msk출근퇴근;
        private RoundedButton btnFPMS데이터읽기;
        private RoundedButton btnCaps데이터읽기;
        private ImagePanel imagePanel1;
        private ImagePanel imagePanel2;
        private ImagePanel imagePanel3;
        private ImagePanel imagePanel4;
        private ImagePanel imagePanel6;
        private ImagePanel imagePanel5;
        private ImagePanel imagePanel7;
        private ImagePanel imagePanel8;
        private BpComboBox bpc사업장;
        private RoundedButton btnSecom데이터읽기;
        private BpComboBox bpc사업장1;
        private OneGrid oneGrid1;
        private OneGridItem oneGridItem1;
        private BpPanelControl bpPanelControl3;
        private BpPanelControl bpPanelControl2;
        private BpPanelControl bpPanelControl1;
        private OneGridItem oneGridItem2;
        private BpPanelControl bppnl입사일자;
        private OneGrid oneGrid2;
        private OneGridItem oneGridItem3;
        private BpPanelControl bppnl년월일;
        private BpPanelControl bpPanelControl5;
        private BpPanelControl bpPanelControl4;
        private PeriodPicker dtp입사일자;
        private PeriodPicker dtp년월일;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private Dass.FlexGrid.FlexGrid _flex승인Pivot;
		private Dass.FlexGrid.FlexGrid _flex승인대기;
		private TabControl tabControl2;
		private TabPage tpg승인대기;
		private TabPage tpg승인;
		private TabControl tabControl3;
		private TabPage tpg승인일반;
		private TabPage tpg승인Pivot;
		private RoundedButton btn승인;
		private OneGridItem oneGridItem4;
		private BpPanelControl bpPanelControl6;
		private LabelExt lbl사원;
		private BpComboBox bpc사원;
	}
}