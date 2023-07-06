namespace cz
{
    partial class P_CZ_BI_WTMCALC_MONTH
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_BI_WTMCALC_MONTH));
            this.tlayH = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp일자 = new Duzon.Common.Controls.PeriodPicker();
            this.btn비고 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.lblWeek = new Duzon.Common.Controls.LabelExt();
            this.labelExt4 = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbWorkDay = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this.tlayH.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flex)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tlayH);
            this.mDataArea.Size = new System.Drawing.Size(1090, 756);
            // 
            // tlayH
            // 
            this.tlayH.ColumnCount = 1;
            this.tlayH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlayH.Controls.Add(this.oneGrid1, 0, 0);
            this.tlayH.Controls.Add(this.panel1, 0, 1);
            this.tlayH.Controls.Add(this.flex, 0, 2);
            this.tlayH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlayH.Location = new System.Drawing.Point(0, 0);
            this.tlayH.Name = "tlayH";
            this.tlayH.RowCount = 3;
            this.tlayH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tlayH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.16931F));
            this.tlayH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.82011F));
            this.tlayH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlayH.Size = new System.Drawing.Size(1090, 756);
            this.tlayH.TabIndex = 2;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1084, 47);
            this.oneGrid1.TabIndex = 3;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.oneGridItem2);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem2.Location = new System.Drawing.Point(2, 1);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1068, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp일자);
            this.bpPanelControl2.Controls.Add(this.btn비고);
            this.bpPanelControl2.Controls.Add(this.lblWeek);
            this.bpPanelControl2.Controls.Add(this.labelExt4);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(1062, 22);
            this.bpPanelControl2.TabIndex = 18;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp일자
            // 
            this.dtp일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp일자.Location = new System.Drawing.Point(82, 1);
            this.dtp일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp일자.Name = "dtp일자";
            this.dtp일자.Size = new System.Drawing.Size(185, 21);
            this.dtp일자.TabIndex = 16;
            // 
            // btn비고
            // 
            this.btn비고.BackColor = System.Drawing.Color.White;
            this.btn비고.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn비고.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn비고.Location = new System.Drawing.Point(316, 1);
            this.btn비고.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn비고.Name = "btn비고";
            this.btn비고.Size = new System.Drawing.Size(70, 19);
            this.btn비고.TabIndex = 15;
            this.btn비고.TabStop = false;
            this.btn비고.Text = "비고";
            this.btn비고.UseVisualStyleBackColor = false;
            // 
            // lblWeek
            // 
            this.lblWeek.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblWeek.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblWeek.Location = new System.Drawing.Point(392, 0);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(670, 22);
            this.lblWeek.TabIndex = 3;
            this.lblWeek.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt4
            // 
            this.labelExt4.Location = new System.Drawing.Point(3, 0);
            this.labelExt4.Name = "labelExt4";
            this.labelExt4.Size = new System.Drawing.Size(76, 22);
            this.labelExt4.TabIndex = 3;
            this.labelExt4.Text = "조회일자";
            this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbWorkDay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 85);
            this.panel1.TabIndex = 6;
            // 
            // lbWorkDay
            // 
            this.lbWorkDay.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbWorkDay.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbWorkDay.Location = new System.Drawing.Point(835, 51);
            this.lbWorkDay.Name = "lbWorkDay";
            this.lbWorkDay.Size = new System.Drawing.Size(249, 34);
            this.lbWorkDay.TabIndex = 1;
            this.lbWorkDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "* 외근은 국내외출장, 교육 등 포함";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTitle
            // 
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("굴림", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(1084, 51);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flex
            // 
            this.flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this.flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex.AutoResize = false;
            this.flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this.flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex.EnabledHeaderCheck = true;
            this.flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.flex.Location = new System.Drawing.Point(3, 147);
            this.flex.Name = "flex";
            this.flex.Rows.Count = 1;
            this.flex.Rows.DefaultSize = 20;
            this.flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.flex.ShowSort = false;
            this.flex.Size = new System.Drawing.Size(1084, 606);
            this.flex.StyleInfo = resources.GetString("flex.StyleInfo");
            this.flex.TabIndex = 5;
            this.flex.UseGridCalculator = true;
            // 
            // P_CZ_BI_WTMCALC_MONTH
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_BI_WTMCALC_MONTH";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "근태현황보고";
            this.mDataArea.ResumeLayout(false);
            this.tlayH.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlayH;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Dass.FlexGrid.FlexGrid flex;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbWorkDay;
        private Duzon.Common.Controls.LabelExt labelExt4;
        private Duzon.Common.Controls.LabelExt lblWeek;
        private Duzon.Common.Controls.RoundedButton btn비고;
        private Duzon.Common.Controls.PeriodPicker dtp일자;
    }
}

