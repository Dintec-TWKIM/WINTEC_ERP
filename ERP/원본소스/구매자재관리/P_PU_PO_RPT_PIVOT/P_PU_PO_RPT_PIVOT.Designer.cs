namespace pur
{
    partial class P_PU_PO_RPT_PIVOT
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_PO_RPT_PIVOT));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._pivot = new Duzon.BizOn.Windows.PivotGrid.PivotGrid();
            this.lbl_CD_PLANT = new Duzon.Common.Controls.LabelExt();
            this.lbl_FG_POST = new Duzon.Common.Controls.LabelExt();
            this.cbo_CD_PLANT = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_FG_POST = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_DT_SELECT = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl_CD_PURGRP = new Duzon.Common.Controls.LabelExt();
            this.bp_CD_PURGRP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl_MA_EMP = new Duzon.Common.Controls.LabelExt();
            this.bp_MA_EMP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bp_CD_ITEM = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl_CD_ITEM = new Duzon.Common.Controls.LabelExt();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.dps_date = new Duzon.Common.Controls.PeriodPicker();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._pivot, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // _pivot
            // 
            this._pivot.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pivot.LocalLanguage = Duzon.BizOn.Windows.PivotGrid.LocalLanguage.KOR;
            this._pivot.Location = new System.Drawing.Point(3, 77);
            this._pivot.Name = "_pivot";
            this._pivot.Size = new System.Drawing.Size(821, 499);
            this._pivot.TabIndex = 1;
            this._pivot.Text = "pivotGrid1";
            // 
            // lbl_CD_PLANT
            // 
            this.lbl_CD_PLANT.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CD_PLANT.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_CD_PLANT.ForeColor = System.Drawing.Color.Black;
            this.lbl_CD_PLANT.Location = new System.Drawing.Point(0, 2);
            this.lbl_CD_PLANT.Name = "lbl_CD_PLANT";
            this.lbl_CD_PLANT.Resizeble = true;
            this.lbl_CD_PLANT.Size = new System.Drawing.Size(80, 16);
            this.lbl_CD_PLANT.TabIndex = 0;
            this.lbl_CD_PLANT.Text = "공장";
            this.lbl_CD_PLANT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_FG_POST
            // 
            this.lbl_FG_POST.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FG_POST.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_FG_POST.ForeColor = System.Drawing.Color.Black;
            this.lbl_FG_POST.Location = new System.Drawing.Point(0, 2);
            this.lbl_FG_POST.Name = "lbl_FG_POST";
            this.lbl_FG_POST.Resizeble = true;
            this.lbl_FG_POST.Size = new System.Drawing.Size(80, 16);
            this.lbl_FG_POST.TabIndex = 10;
            this.lbl_FG_POST.Text = "발주상태";
            this.lbl_FG_POST.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo_CD_PLANT
            // 
            this.cbo_CD_PLANT.AutoDropDown = true;
            this.cbo_CD_PLANT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo_CD_PLANT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CD_PLANT.ItemHeight = 12;
            this.cbo_CD_PLANT.Location = new System.Drawing.Point(81, 1);
            this.cbo_CD_PLANT.Name = "cbo_CD_PLANT";
            this.cbo_CD_PLANT.ShowCheckBox = false;
            this.cbo_CD_PLANT.Size = new System.Drawing.Size(186, 20);
            this.cbo_CD_PLANT.TabIndex = 1;
            this.cbo_CD_PLANT.UseKeyEnter = false;
            this.cbo_CD_PLANT.UseKeyF3 = false;
            // 
            // cbo_FG_POST
            // 
            this.cbo_FG_POST.AutoDropDown = true;
            this.cbo_FG_POST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_FG_POST.ItemHeight = 12;
            this.cbo_FG_POST.Location = new System.Drawing.Point(81, 1);
            this.cbo_FG_POST.Name = "cbo_FG_POST";
            this.cbo_FG_POST.ShowCheckBox = false;
            this.cbo_FG_POST.Size = new System.Drawing.Size(186, 20);
            this.cbo_FG_POST.TabIndex = 11;
            this.cbo_FG_POST.UseKeyEnter = false;
            this.cbo_FG_POST.UseKeyF3 = false;
            // 
            // cbo_DT_SELECT
            // 
            this.cbo_DT_SELECT.AutoDropDown = true;
            this.cbo_DT_SELECT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo_DT_SELECT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_DT_SELECT.FormattingEnabled = true;
            this.cbo_DT_SELECT.ItemHeight = 12;
            this.cbo_DT_SELECT.Location = new System.Drawing.Point(0, 2);
            this.cbo_DT_SELECT.Name = "cbo_DT_SELECT";
            this.cbo_DT_SELECT.ShowCheckBox = false;
            this.cbo_DT_SELECT.Size = new System.Drawing.Size(80, 20);
            this.cbo_DT_SELECT.TabIndex = 2;
            this.cbo_DT_SELECT.UseKeyEnter = true;
            this.cbo_DT_SELECT.UseKeyF3 = true;
            // 
            // lbl_CD_PURGRP
            // 
            this.lbl_CD_PURGRP.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CD_PURGRP.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_CD_PURGRP.ForeColor = System.Drawing.Color.Black;
            this.lbl_CD_PURGRP.Location = new System.Drawing.Point(0, 2);
            this.lbl_CD_PURGRP.Name = "lbl_CD_PURGRP";
            this.lbl_CD_PURGRP.Resizeble = true;
            this.lbl_CD_PURGRP.Size = new System.Drawing.Size(80, 16);
            this.lbl_CD_PURGRP.TabIndex = 8;
            this.lbl_CD_PURGRP.Text = "구매그룹";
            this.lbl_CD_PURGRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bp_CD_PURGRP
            // 
            this.bp_CD_PURGRP.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_CD_PURGRP.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CD_PURGRP.ButtonImage")));
            this.bp_CD_PURGRP.ChildMode = "";
            this.bp_CD_PURGRP.CodeName = "";
            this.bp_CD_PURGRP.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CD_PURGRP.CodeValue = "";
            this.bp_CD_PURGRP.ComboCheck = true;
            this.bp_CD_PURGRP.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB;
            this.bp_CD_PURGRP.IsCodeValueToUpper = true;
            this.bp_CD_PURGRP.ItemBackColor = System.Drawing.Color.Empty;
            this.bp_CD_PURGRP.Location = new System.Drawing.Point(81, 1);
            this.bp_CD_PURGRP.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CD_PURGRP.Name = "bp_CD_PURGRP";
            this.bp_CD_PURGRP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CD_PURGRP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp_CD_PURGRP.SearchCode = true;
            this.bp_CD_PURGRP.SelectCount = 0;
            this.bp_CD_PURGRP.SetDefaultValue = false;
            this.bp_CD_PURGRP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CD_PURGRP.Size = new System.Drawing.Size(186, 21);
            this.bp_CD_PURGRP.TabIndex = 9;
            this.bp_CD_PURGRP.TabStop = false;
            // 
            // lbl_MA_EMP
            // 
            this.lbl_MA_EMP.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MA_EMP.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_MA_EMP.ForeColor = System.Drawing.Color.Black;
            this.lbl_MA_EMP.Location = new System.Drawing.Point(0, 2);
            this.lbl_MA_EMP.Name = "lbl_MA_EMP";
            this.lbl_MA_EMP.Resizeble = true;
            this.lbl_MA_EMP.Size = new System.Drawing.Size(80, 16);
            this.lbl_MA_EMP.TabIndex = 12;
            this.lbl_MA_EMP.Text = "발주담당자";
            this.lbl_MA_EMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bp_MA_EMP
            // 
            this.bp_MA_EMP.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_MA_EMP.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_MA_EMP.ButtonImage")));
            this.bp_MA_EMP.ChildMode = "";
            this.bp_MA_EMP.CodeName = null;
            this.bp_MA_EMP.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_MA_EMP.CodeValue = null;
            this.bp_MA_EMP.ComboCheck = true;
            this.bp_MA_EMP.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bp_MA_EMP.IsCodeValueToUpper = true;
            this.bp_MA_EMP.ItemBackColor = System.Drawing.Color.White;
            this.bp_MA_EMP.Location = new System.Drawing.Point(81, 1);
            this.bp_MA_EMP.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_MA_EMP.Name = "bp_MA_EMP";
            this.bp_MA_EMP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_MA_EMP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp_MA_EMP.SearchCode = true;
            this.bp_MA_EMP.SelectCount = 0;
            this.bp_MA_EMP.SetDefaultValue = false;
            this.bp_MA_EMP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_MA_EMP.Size = new System.Drawing.Size(186, 21);
            this.bp_MA_EMP.TabIndex = 13;
            this.bp_MA_EMP.TabStop = false;
            // 
            // bp_CD_ITEM
            // 
            this.bp_CD_ITEM.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_CD_ITEM.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CD_ITEM.ButtonImage")));
            this.bp_CD_ITEM.ChildMode = "";
            this.bp_CD_ITEM.CodeName = "";
            this.bp_CD_ITEM.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CD_ITEM.CodeValue = "";
            this.bp_CD_ITEM.ComboCheck = true;
            this.bp_CD_ITEM.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.bp_CD_ITEM.IsCodeValueToUpper = true;
            this.bp_CD_ITEM.ItemBackColor = System.Drawing.Color.Empty;
            this.bp_CD_ITEM.Location = new System.Drawing.Point(81, 1);
            this.bp_CD_ITEM.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CD_ITEM.Name = "bp_CD_ITEM";
            this.bp_CD_ITEM.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CD_ITEM.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp_CD_ITEM.SearchCode = true;
            this.bp_CD_ITEM.SelectCount = 0;
            this.bp_CD_ITEM.SetDefaultValue = false;
            this.bp_CD_ITEM.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CD_ITEM.Size = new System.Drawing.Size(186, 21);
            this.bp_CD_ITEM.TabIndex = 17;
            this.bp_CD_ITEM.TabStop = false;
            // 
            // lbl_CD_ITEM
            // 
            this.lbl_CD_ITEM.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CD_ITEM.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_CD_ITEM.ForeColor = System.Drawing.Color.Black;
            this.lbl_CD_ITEM.Location = new System.Drawing.Point(0, 2);
            this.lbl_CD_ITEM.Name = "lbl_CD_ITEM";
            this.lbl_CD_ITEM.Resizeble = true;
            this.lbl_CD_ITEM.Size = new System.Drawing.Size(80, 16);
            this.lbl_CD_ITEM.TabIndex = 16;
            this.lbl_CD_ITEM.Text = "품목";
            this.lbl_CD_ITEM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 68);
            this.oneGrid1.TabIndex = 263;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.lbl_CD_PLANT);
            this.bpPanelControl1.Controls.Add(this.cbo_CD_PLANT);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.lbl_FG_POST);
            this.bpPanelControl2.Controls.Add(this.cbo_FG_POST);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.dps_date);
            this.bpPanelControl3.Controls.Add(this.cbo_DT_SELECT);
            this.bpPanelControl3.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl3.TabIndex = 1;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.lbl_CD_PURGRP);
            this.bpPanelControl4.Controls.Add(this.bp_CD_PURGRP);
            this.bpPanelControl4.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl4.TabIndex = 2;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.bp_CD_ITEM);
            this.bpPanelControl5.Controls.Add(this.lbl_CD_ITEM);
            this.bpPanelControl5.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl5.TabIndex = 1;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.bp_MA_EMP);
            this.bpPanelControl6.Controls.Add(this.lbl_MA_EMP);
            this.bpPanelControl6.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl6.TabIndex = 2;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // dps_date
            // 
            this.dps_date.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dps_date.IsNecessaryCondition = true;
            this.dps_date.Location = new System.Drawing.Point(81, 1);
            this.dps_date.Mask = "####/##/##";
            this.dps_date.MaximumSize = new System.Drawing.Size(185, 21);
            this.dps_date.MinimumSize = new System.Drawing.Size(185, 21);
            this.dps_date.Name = "dps_date";
            this.dps_date.Size = new System.Drawing.Size(185, 21);
            this.dps_date.TabIndex = 3;
            // 
            // P_PU_PO_RPT_PIVOT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_PU_PO_RPT_PIVOT";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.BizOn.Windows.PivotGrid.PivotGrid _pivot;
        private Duzon.Common.Controls.LabelExt lbl_CD_ITEM;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CD_ITEM;
        private Duzon.Common.BpControls.BpCodeTextBox bp_MA_EMP;
        private Duzon.Common.Controls.LabelExt lbl_MA_EMP;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CD_PURGRP;
        private Duzon.Common.Controls.LabelExt lbl_CD_PURGRP;
        private Duzon.Common.Controls.DropDownComboBox cbo_DT_SELECT;
        private Duzon.Common.Controls.DropDownComboBox cbo_FG_POST;
        private Duzon.Common.Controls.DropDownComboBox cbo_CD_PLANT;
        private Duzon.Common.Controls.LabelExt lbl_FG_POST;
        private Duzon.Common.Controls.LabelExt lbl_CD_PLANT;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.PeriodPicker dps_date;


    }
}
