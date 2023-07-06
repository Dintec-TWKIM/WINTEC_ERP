namespace cz
{
    partial class P_CZ_SA_GIR_COUNT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GIR_COUNT));
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn제출취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn제출 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
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
            this._flex.Location = new System.Drawing.Point(3, 37);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(518, 497);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            this._flex.UseGridCalculator = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn제출취소);
            this.flowLayoutPanel1.Controls.Add(this.btn제출);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(518, 28);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btn제출취소
            // 
            this.btn제출취소.BackColor = System.Drawing.Color.Transparent;
            this.btn제출취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn제출취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn제출취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn제출취소.Location = new System.Drawing.Point(445, 3);
            this.btn제출취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn제출취소.Name = "btn제출취소";
            this.btn제출취소.Size = new System.Drawing.Size(70, 19);
            this.btn제출취소.TabIndex = 1;
            this.btn제출취소.TabStop = false;
            this.btn제출취소.Text = "제출취소";
            this.btn제출취소.UseVisualStyleBackColor = false;
            // 
            // btn제출
            // 
            this.btn제출.BackColor = System.Drawing.Color.Transparent;
            this.btn제출.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn제출.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn제출.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn제출.Location = new System.Drawing.Point(369, 3);
            this.btn제출.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn제출.Name = "btn제출";
            this.btn제출.Size = new System.Drawing.Size(70, 19);
            this.btn제출.TabIndex = 0;
            this.btn제출.TabStop = false;
            this.btn제출.Text = "제출";
            this.btn제출.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 49);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(524, 537);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // P_CZ_SA_GIR_COUNT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(527, 588);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "P_CZ_SA_GIR_COUNT";
            this.Text = "P_CZ_SA_GIR_COUNT";
            this.TitleText = "협조전진행현황";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn제출취소;
        private Duzon.Common.Controls.RoundedButton btn제출;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}