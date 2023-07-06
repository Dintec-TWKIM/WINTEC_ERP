
namespace cz
{
    partial class P_CZ_PR_OPOUT_REG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_OPOUT_REG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flexID = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx작업지시번호 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp요청기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl요청기간 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn일괄해제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn일괄선택 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn해제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn선택 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.curID번호To = new Duzon.Common.Controls.CurrencyTextBox();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.curID번호From = new Duzon.Common.Controls.CurrencyTextBox();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexID)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호To)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호From)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(918, 756);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flexID, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(918, 756);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _flexID
            // 
            this._flexID.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexID.AllowHeaderClickSelected = false;
            this._flexID.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexID.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexID.AutoResize = false;
            this._flexID.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexID.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexID.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexID.EnabledHeaderCheck = true;
            this._flexID.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexID.Location = new System.Drawing.Point(3, 420);
            this._flexID.Name = "_flexID";
            this._flexID.Rows.Count = 1;
            this._flexID.Rows.DefaultSize = 18;
            this._flexID.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexID.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexID.ShowSort = false;
            this._flexID.Size = new System.Drawing.Size(912, 333);
            this._flexID.StyleInfo = resources.GetString("_flexID.StyleInfo");
            this._flexID.TabIndex = 9;
            this._flexID.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(912, 39);
            this.oneGrid1.TabIndex = 0;
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
            this.oneGridItem1.Size = new System.Drawing.Size(902, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx작업지시번호);
            this.bpPanelControl3.Controls.Add(this.lbl작업지시번호);
            this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // ctx작업지시번호
            // 
            this.ctx작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx작업지시번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WO_REG_SUB;
            this.ctx작업지시번호.Location = new System.Drawing.Point(107, 0);
            this.ctx작업지시번호.Name = "ctx작업지시번호";
            this.ctx작업지시번호.Size = new System.Drawing.Size(185, 21);
            this.ctx작업지시번호.TabIndex = 2;
            this.ctx작업지시번호.TabStop = false;
            this.ctx작업지시번호.Text = "bpCodeTextBox1";
            // 
            // lbl작업지시번호
            // 
            this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
            this.lbl작업지시번호.Name = "lbl작업지시번호";
            this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
            this.lbl작업지시번호.TabIndex = 1;
            this.lbl작업지시번호.Text = "작업지시번호";
            this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp요청기간);
            this.bpPanelControl2.Controls.Add(this.lbl요청기간);
            this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp요청기간
            // 
            this.dtp요청기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp요청기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp요청기간.Location = new System.Drawing.Point(107, 0);
            this.dtp요청기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp요청기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp요청기간.Name = "dtp요청기간";
            this.dtp요청기간.Size = new System.Drawing.Size(185, 21);
            this.dtp요청기간.TabIndex = 2;
            // 
            // lbl요청기간
            // 
            this.lbl요청기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl요청기간.Location = new System.Drawing.Point(0, 0);
            this.lbl요청기간.Name = "lbl요청기간";
            this.lbl요청기간.Size = new System.Drawing.Size(100, 23);
            this.lbl요청기간.TabIndex = 1;
            this.lbl요청기간.Text = "요청기간";
            this.lbl요청기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cbo공장);
            this.bpPanelControl1.Controls.Add(this.lbl공장);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = true;
            this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.FormattingEnabled = true;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(107, 0);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.Size = new System.Drawing.Size(185, 20);
            this.cbo공장.TabIndex = 1;
            // 
            // lbl공장
            // 
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 0;
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this._flex.Location = new System.Drawing.Point(3, 48);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(912, 333);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 2;
            this._flex.UseGridCalculator = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn일괄해제);
            this.flowLayoutPanel1.Controls.Add(this.btn일괄선택);
            this.flowLayoutPanel1.Controls.Add(this.btn해제);
            this.flowLayoutPanel1.Controls.Add(this.btn선택);
            this.flowLayoutPanel1.Controls.Add(this.curID번호To);
            this.flowLayoutPanel1.Controls.Add(this.labelExt1);
            this.flowLayoutPanel1.Controls.Add(this.curID번호From);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 387);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(912, 27);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btn일괄해제
            // 
            this.btn일괄해제.BackColor = System.Drawing.Color.Transparent;
            this.btn일괄해제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn일괄해제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn일괄해제.Location = new System.Drawing.Point(839, 3);
            this.btn일괄해제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn일괄해제.Name = "btn일괄해제";
            this.btn일괄해제.Size = new System.Drawing.Size(70, 19);
            this.btn일괄해제.TabIndex = 7;
            this.btn일괄해제.TabStop = false;
            this.btn일괄해제.Text = "일괄해제";
            this.btn일괄해제.UseVisualStyleBackColor = false;
            // 
            // btn일괄선택
            // 
            this.btn일괄선택.BackColor = System.Drawing.Color.Transparent;
            this.btn일괄선택.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn일괄선택.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn일괄선택.Location = new System.Drawing.Point(763, 3);
            this.btn일괄선택.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn일괄선택.Name = "btn일괄선택";
            this.btn일괄선택.Size = new System.Drawing.Size(70, 19);
            this.btn일괄선택.TabIndex = 0;
            this.btn일괄선택.TabStop = false;
            this.btn일괄선택.Text = "일괄선택";
            this.btn일괄선택.UseVisualStyleBackColor = false;
            // 
            // btn해제
            // 
            this.btn해제.BackColor = System.Drawing.Color.Transparent;
            this.btn해제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn해제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn해제.Location = new System.Drawing.Point(702, 3);
            this.btn해제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn해제.Name = "btn해제";
            this.btn해제.Size = new System.Drawing.Size(55, 19);
            this.btn해제.TabIndex = 8;
            this.btn해제.TabStop = false;
            this.btn해제.Text = "해제";
            this.btn해제.UseVisualStyleBackColor = false;
            // 
            // btn선택
            // 
            this.btn선택.BackColor = System.Drawing.Color.Transparent;
            this.btn선택.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn선택.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn선택.Location = new System.Drawing.Point(641, 3);
            this.btn선택.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn선택.Name = "btn선택";
            this.btn선택.Size = new System.Drawing.Size(55, 19);
            this.btn선택.TabIndex = 4;
            this.btn선택.TabStop = false;
            this.btn선택.Text = "선택";
            this.btn선택.UseVisualStyleBackColor = false;
            // 
            // curID번호To
            // 
            this.curID번호To.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.curID번호To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curID번호To.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.curID번호To.ForeColor = System.Drawing.SystemColors.ControlText;
            this.curID번호To.Location = new System.Drawing.Point(535, 3);
            this.curID번호To.Name = "curID번호To";
            this.curID번호To.NullString = "0";
            this.curID번호To.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.curID번호To.Size = new System.Drawing.Size(100, 21);
            this.curID번호To.TabIndex = 6;
            this.curID번호To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(519, 0);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(10, 23);
            this.labelExt1.TabIndex = 3;
            this.labelExt1.Text = "~";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // curID번호From
            // 
            this.curID번호From.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.curID번호From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curID번호From.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.curID번호From.ForeColor = System.Drawing.SystemColors.ControlText;
            this.curID번호From.Location = new System.Drawing.Point(413, 3);
            this.curID번호From.Name = "curID번호From";
            this.curID번호From.NullString = "0";
            this.curID번호From.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.curID번호From.Size = new System.Drawing.Size(100, 21);
            this.curID번호From.TabIndex = 5;
            this.curID번호From.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // P_CZ_PR_OPOUT_REG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "P_CZ_PR_OPOUT_REG";
            this.Size = new System.Drawing.Size(918, 796);
            this.TitleText = "P_CZ_PR_OPOUT_REG";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexID)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호To)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호From)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl작업지시번호;
        private Duzon.Common.Controls.LabelExt lbl요청기간;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.BpControls.BpCodeTextBox ctx작업지시번호;
        private Duzon.Common.Controls.PeriodPicker dtp요청기간;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flexID;
        private Duzon.Common.Controls.RoundedButton btn선택;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.RoundedButton btn일괄선택;
        private Duzon.Common.Controls.CurrencyTextBox curID번호From;
        private Duzon.Common.Controls.CurrencyTextBox curID번호To;
        private Duzon.Common.Controls.RoundedButton btn일괄해제;
        private Duzon.Common.Controls.RoundedButton btn해제;
    }
}