namespace cz
{
    partial class P_CZ_HR_PEVALU_HUMEMP_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PEVALU_HUMEMP_SUB));
            this.btn복사 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelExt1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // btn복사
            // 
            this.btn복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn복사.BackColor = System.Drawing.Color.White;
            this.btn복사.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn복사.Location = new System.Drawing.Point(296, 3);
            this.btn복사.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn복사.Name = "btn복사";
            this.btn복사.Size = new System.Drawing.Size(65, 19);
            this.btn복사.TabIndex = 28;
            this.btn복사.TabStop = false;
            this.btn복사.Text = "복사";
            this.btn복사.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 52);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(443, 197);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn복사);
            this.panelExt1.Controls.Add(this.btn취소);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 3);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(437, 26);
            this.panelExt1.TabIndex = 3;
            // 
            // btn취소
            // 
            this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(367, 3);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(65, 19);
            this.btn취소.TabIndex = 27;
            this.btn취소.TabStop = false;
            this.btn취소.Tag = "CANCEL";
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 35);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(437, 159);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 4;
            this._flex.UseGridCalculator = true;
            // 
            // P_CZ_HR_PEVALU_HUMEMP_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(451, 253);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_HR_PEVALU_HUMEMP_SUB";
            this.TitleText = "평가자/피평가자 도움창";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        private Duzon.Common.Controls.RoundedButton btn복사;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Dass.FlexGrid.FlexGrid _flex;
        #endregion
    }
}