
namespace cz
{
    partial class P_CZ_PR_OPOUT_WORK_MNG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_OPOUT_WORK_MNG));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.chk작업기간 = new Duzon.Common.Controls.CheckBoxExt();
            this.dtp작업기간 = new Duzon.Common.Controls.PeriodPicker();
            this.bpP_Dt_Work = new Duzon.Common.BpControls.BpPanelControl();
            this.chk발주기간 = new Duzon.Common.Controls.CheckBoxExt();
            this.dtp발주기간 = new Duzon.Common.Controls.PeriodPicker();
            this.bpP_Plant = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx작업지시번호 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
            this.bpP_Status = new Duzon.Common.BpControls.BpPanelControl();
            this.lblStatus = new Duzon.Common.Controls.LabelExt();
            this.chk미마감 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk마감 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl외주처 = new Duzon.Common.Controls.LabelExt();
            this.ctx외주처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx작업품목 = new Duzon.Common.BpControls.BpCodeNTextBox();
            this.txt품목규격 = new Duzon.Common.Controls.TextBoxExt();
            this.txt재고단위 = new Duzon.Common.Controls.TextBoxExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpP_Dt_Work.SuspendLayout();
            this.bpP_Plant.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpP_Status.SuspendLayout();
            this.bpPanelControl9.SuspendLayout();
            this.oneGridItem4.SuspendLayout();
            this.bpPanelControl10.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            this.mDataArea.Size = new System.Drawing.Size(1060, 756);
            // 
            // _tlayMain
            // 
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this._flexD, 0, 3);
            this._tlayMain.Controls.Add(this._flexM, 0, 1);
            this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
            this._tlayMain.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 4;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tlayMain.Size = new System.Drawing.Size(1060, 756);
            this._tlayMain.TabIndex = 139;
            // 
            // _flexM
            // 
            this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM.AutoResize = false;
            this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM.EnabledHeaderCheck = true;
            this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM.Location = new System.Drawing.Point(3, 94);
            this._flexM.Name = "_flexM";
            this._flexM.Rows.Count = 1;
            this._flexM.Rows.DefaultSize = 20;
            this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM.ShowSort = false;
            this._flexM.Size = new System.Drawing.Size(1054, 313);
            this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
            this._flexM.TabIndex = 7;
            this._flexM.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem4});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1054, 85);
            this.oneGrid1.TabIndex = 132;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpP_Dt_Work);
            this.oneGridItem1.Controls.Add(this.bpP_Plant);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1044, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.chk작업기간);
            this.bpPanelControl4.Controls.Add(this.dtp작업기간);
            this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 4;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // chk작업기간
            // 
            this.chk작업기간.BackColor = System.Drawing.Color.Transparent;
            this.chk작업기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk작업기간.Location = new System.Drawing.Point(0, 0);
            this.chk작업기간.Name = "chk작업기간";
            this.chk작업기간.Size = new System.Drawing.Size(100, 23);
            this.chk작업기간.TabIndex = 254;
            this.chk작업기간.Text = "작업기간";
            this.chk작업기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk작업기간.TextDD = null;
            this.chk작업기간.UseVisualStyleBackColor = false;
            // 
            // dtp작업기간
            // 
            this.dtp작업기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp작업기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp작업기간.IsNecessaryCondition = true;
            this.dtp작업기간.Location = new System.Drawing.Point(107, 0);
            this.dtp작업기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp작업기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp작업기간.Name = "dtp작업기간";
            this.dtp작업기간.Size = new System.Drawing.Size(185, 21);
            this.dtp작업기간.TabIndex = 253;
            // 
            // bpP_Dt_Work
            // 
            this.bpP_Dt_Work.Controls.Add(this.chk발주기간);
            this.bpP_Dt_Work.Controls.Add(this.dtp발주기간);
            this.bpP_Dt_Work.Location = new System.Drawing.Point(296, 1);
            this.bpP_Dt_Work.Name = "bpP_Dt_Work";
            this.bpP_Dt_Work.Size = new System.Drawing.Size(292, 23);
            this.bpP_Dt_Work.TabIndex = 3;
            this.bpP_Dt_Work.Text = "bpPanelControl1";
            // 
            // chk발주기간
            // 
            this.chk발주기간.BackColor = System.Drawing.Color.Transparent;
            this.chk발주기간.Checked = true;
            this.chk발주기간.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk발주기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk발주기간.Location = new System.Drawing.Point(0, 0);
            this.chk발주기간.Name = "chk발주기간";
            this.chk발주기간.Size = new System.Drawing.Size(100, 23);
            this.chk발주기간.TabIndex = 254;
            this.chk발주기간.Text = "발주기간";
            this.chk발주기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk발주기간.TextDD = null;
            this.chk발주기간.UseVisualStyleBackColor = false;
            // 
            // dtp발주기간
            // 
            this.dtp발주기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp발주기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp발주기간.IsNecessaryCondition = true;
            this.dtp발주기간.Location = new System.Drawing.Point(107, 0);
            this.dtp발주기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp발주기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp발주기간.Name = "dtp발주기간";
            this.dtp발주기간.Size = new System.Drawing.Size(185, 21);
            this.dtp발주기간.TabIndex = 249;
            // 
            // bpP_Plant
            // 
            this.bpP_Plant.Controls.Add(this.lbl공장);
            this.bpP_Plant.Controls.Add(this.cbo공장);
            this.bpP_Plant.Location = new System.Drawing.Point(2, 1);
            this.bpP_Plant.Name = "bpP_Plant";
            this.bpP_Plant.Size = new System.Drawing.Size(292, 23);
            this.bpP_Plant.TabIndex = 1;
            this.bpP_Plant.Text = "bpPanelControl6";
            // 
            // lbl공장
            // 
            this.lbl공장.BackColor = System.Drawing.Color.Transparent;
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 1;
            this.lbl공장.Tag = "공장";
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = false;
            this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(107, 0);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.Size = new System.Drawing.Size(185, 20);
            this.cbo공장.TabIndex = 1;
            this.cbo공장.UseKeyF3 = false;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpP_Status);
            this.oneGridItem2.Controls.Add(this.bpPanelControl9);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1044, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.ctx작업지시번호);
            this.bpPanelControl5.Controls.Add(this.lbl작업지시번호);
            this.bpPanelControl5.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl5.TabIndex = 3;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // ctx작업지시번호
            // 
            this.ctx작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx작업지시번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WO_REG_SUB;
            this.ctx작업지시번호.Location = new System.Drawing.Point(107, 0);
            this.ctx작업지시번호.Name = "ctx작업지시번호";
            this.ctx작업지시번호.Size = new System.Drawing.Size(185, 21);
            this.ctx작업지시번호.TabIndex = 3;
            this.ctx작업지시번호.TabStop = false;
            this.ctx작업지시번호.Text = "bpCodeTextBox1";
            // 
            // lbl작업지시번호
            // 
            this.lbl작업지시번호.BackColor = System.Drawing.Color.Transparent;
            this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
            this.lbl작업지시번호.Name = "lbl작업지시번호";
            this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
            this.lbl작업지시번호.TabIndex = 2;
            this.lbl작업지시번호.Text = "작업지시번호";
            this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpP_Status
            // 
            this.bpP_Status.Controls.Add(this.lblStatus);
            this.bpP_Status.Controls.Add(this.chk미마감);
            this.bpP_Status.Controls.Add(this.chk마감);
            this.bpP_Status.Location = new System.Drawing.Point(296, 1);
            this.bpP_Status.Name = "bpP_Status";
            this.bpP_Status.Size = new System.Drawing.Size(292, 23);
            this.bpP_Status.TabIndex = 2;
            this.bpP_Status.Text = "bpPanelControl1";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 23);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Tag = "Status";
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chk미마감
            // 
            this.chk미마감.BackColor = System.Drawing.Color.Transparent;
            this.chk미마감.Checked = true;
            this.chk미마감.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk미마감.Location = new System.Drawing.Point(107, 1);
            this.chk미마감.Name = "chk미마감";
            this.chk미마감.Size = new System.Drawing.Size(60, 21);
            this.chk미마감.TabIndex = 142;
            this.chk미마감.Tag = "NON_CLOSE";
            this.chk미마감.Text = "미마감";
            this.chk미마감.TextDD = null;
            this.chk미마감.UseVisualStyleBackColor = false;
            // 
            // chk마감
            // 
            this.chk마감.BackColor = System.Drawing.Color.Transparent;
            this.chk마감.Location = new System.Drawing.Point(173, 1);
            this.chk마감.Name = "chk마감";
            this.chk마감.Size = new System.Drawing.Size(79, 21);
            this.chk마감.TabIndex = 143;
            this.chk마감.Tag = "CLOSE";
            this.chk마감.Text = "마감";
            this.chk마감.TextDD = null;
            this.chk마감.UseVisualStyleBackColor = false;
            // 
            // bpPanelControl9
            // 
            this.bpPanelControl9.Controls.Add(this.lbl외주처);
            this.bpPanelControl9.Controls.Add(this.ctx외주처);
            this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl9.Name = "bpPanelControl9";
            this.bpPanelControl9.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl9.TabIndex = 1;
            this.bpPanelControl9.Text = "bpPanelControl9";
            // 
            // lbl외주처
            // 
            this.lbl외주처.BackColor = System.Drawing.Color.Transparent;
            this.lbl외주처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl외주처.Location = new System.Drawing.Point(0, 0);
            this.lbl외주처.Name = "lbl외주처";
            this.lbl외주처.Size = new System.Drawing.Size(100, 23);
            this.lbl외주처.TabIndex = 174;
            this.lbl외주처.Tag = "공장";
            this.lbl외주처.Text = "외주처";
            this.lbl외주처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx외주처
            // 
            this.ctx외주처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx외주처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx외주처.LabelWidth = 156;
            this.ctx외주처.Location = new System.Drawing.Point(107, 0);
            this.ctx외주처.Name = "ctx외주처";
            this.ctx외주처.Size = new System.Drawing.Size(185, 21);
            this.ctx외주처.TabIndex = 173;
            this.ctx외주처.TabStop = false;
            // 
            // oneGridItem4
            // 
            this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem4.Controls.Add(this.bpPanelControl10);
            this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem4.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem4.Name = "oneGridItem4";
            this.oneGridItem4.Size = new System.Drawing.Size(1044, 23);
            this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem4.TabIndex = 2;
            // 
            // bpPanelControl10
            // 
            this.bpPanelControl10.Controls.Add(this.ctx작업품목);
            this.bpPanelControl10.Controls.Add(this.txt품목규격);
            this.bpPanelControl10.Controls.Add(this.txt재고단위);
            this.bpPanelControl10.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl10.Name = "bpPanelControl10";
            this.bpPanelControl10.Size = new System.Drawing.Size(750, 23);
            this.bpPanelControl10.TabIndex = 3;
            this.bpPanelControl10.Text = "bpPanelControl1";
            // 
            // ctx작업품목
            // 
            this.ctx작업품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx작업품목.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx작업품목.LabelVisibled = true;
            this.ctx작업품목.LabelWidth = 106;
            this.ctx작업품목.Location = new System.Drawing.Point(0, 1);
            this.ctx작업품목.Name = "ctx작업품목";
            this.ctx작업품목.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx작업품목.Size = new System.Drawing.Size(342, 21);
            this.ctx작업품목.TabIndex = 2;
            this.ctx작업품목.TabStop = false;
            this.ctx작업품목.Text = "작업품목";
            // 
            // txt품목규격
            // 
            this.txt품목규격.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt품목규격.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt품목규격.Location = new System.Drawing.Point(343, 1);
            this.txt품목규격.Name = "txt품목규격";
            this.txt품목규격.ReadOnly = true;
            this.txt품목규격.Size = new System.Drawing.Size(171, 21);
            this.txt품목규격.TabIndex = 0;
            this.txt품목규격.TabStop = false;
            // 
            // txt재고단위
            // 
            this.txt재고단위.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt재고단위.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt재고단위.Location = new System.Drawing.Point(515, 1);
            this.txt재고단위.Name = "txt재고단위";
            this.txt재고단위.ReadOnly = true;
            this.txt재고단위.Size = new System.Drawing.Size(171, 21);
            this.txt재고단위.TabIndex = 0;
            this.txt재고단위.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btn삭제);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(845, 413);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(212, 21);
            this.flowLayoutPanel1.TabIndex = 133;
            // 
            // btn삭제
            // 
            this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제.BackColor = System.Drawing.Color.White;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(150, 1);
            this.btn삭제.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(62, 19);
            this.btn삭제.TabIndex = 6;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(187, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Location = new System.Drawing.Point(380, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(187, 23);
            this.bpPanelControl3.TabIndex = 0;
            this.bpPanelControl3.Text = "bpPanelControl1";
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.Controls.Add(this.bpPanelControl2);
            this.oneGridItem3.Controls.Add(this.bpPanelControl1);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(807, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Location = new System.Drawing.Point(191, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(187, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl1";
            // 
            // _flexD
            // 
            this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD.AutoResize = false;
            this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD.EnabledHeaderCheck = true;
            this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD.Location = new System.Drawing.Point(3, 440);
            this._flexD.Name = "_flexD";
            this._flexD.Rows.Count = 1;
            this._flexD.Rows.DefaultSize = 18;
            this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD.ShowSort = false;
            this._flexD.Size = new System.Drawing.Size(1054, 313);
            this._flexD.StyleInfo = resources.GetString("_flexD.StyleInfo");
            this._flexD.TabIndex = 134;
            this._flexD.UseGridCalculator = true;
            // 
            // P_CZ_PR_OPOUT_WORK_MNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_PR_OPOUT_WORK_MNG";
            this.Size = new System.Drawing.Size(1060, 796);
            this.mDataArea.ResumeLayout(false);
            this._tlayMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpP_Dt_Work.ResumeLayout(false);
            this.bpP_Plant.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpP_Status.ResumeLayout(false);
            this.bpPanelControl9.ResumeLayout(false);
            this.oneGridItem4.ResumeLayout(false);
            this.bpPanelControl10.ResumeLayout(false);
            this.bpPanelControl10.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.BpControls.BpCodeNTextBox ctx작업품목;
        private Duzon.Common.Controls.CheckBoxExt chk마감;
        private Duzon.Common.Controls.CheckBoxExt chk미마감;
        private Duzon.Common.Controls.LabelExt lblStatus;
        private Duzon.Common.Controls.TextBoxExt txt재고단위;
        private Duzon.Common.Controls.TextBoxExt txt품목규격;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Dass.FlexGrid.FlexGrid _flexM;
        private Duzon.Common.BpControls.BpCodeTextBox ctx외주처;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpP_Dt_Work;
        private Duzon.Common.BpControls.BpPanelControl bpP_Status;
        private Duzon.Common.BpControls.BpPanelControl bpP_Plant;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl10;
        private Duzon.Common.Controls.PeriodPicker dtp발주기간;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.CheckBoxExt chk작업기간;
        private Duzon.Common.Controls.PeriodPicker dtp작업기간;
        private Duzon.Common.Controls.CheckBoxExt chk발주기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpCodeTextBox ctx작업지시번호;
        private Duzon.Common.Controls.LabelExt lbl작업지시번호;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.LabelExt lbl외주처;
        private Dass.FlexGrid.FlexGrid _flexD;
    }
}