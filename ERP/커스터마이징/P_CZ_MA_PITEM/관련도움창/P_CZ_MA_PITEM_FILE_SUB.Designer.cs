namespace cz
{
    partial class P_CZ_MA_PITEM_FILE_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_PITEM_FILE_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._flex파일 = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn일괄업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn불러오기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn초기화 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex파일)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._flex파일, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 500);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _flex파일
			// 
			this._flex파일.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex파일.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex파일.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex파일.AutoResize = false;
			this._flex파일.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex파일.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex파일.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex파일.EnabledHeaderCheck = true;
			this._flex파일.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex파일.Location = new System.Drawing.Point(3, 35);
			this._flex파일.Name = "_flex파일";
			this._flex파일.Rows.Count = 1;
			this._flex파일.Rows.DefaultSize = 18;
			this._flex파일.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex파일.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex파일.ShowSort = false;
			this._flex파일.Size = new System.Drawing.Size(762, 462);
			this._flex파일.StyleInfo = resources.GetString("_flex파일.StyleInfo");
			this._flex파일.TabIndex = 1;
			this._flex파일.UseGridCalculator = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn일괄업로드);
			this.flowLayoutPanel1.Controls.Add(this.btn불러오기);
			this.flowLayoutPanel1.Controls.Add(this.btn초기화);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(762, 26);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btn일괄업로드
			// 
			this.btn일괄업로드.BackColor = System.Drawing.Color.Transparent;
			this.btn일괄업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn일괄업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn일괄업로드.Location = new System.Drawing.Point(689, 3);
			this.btn일괄업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn일괄업로드.Name = "btn일괄업로드";
			this.btn일괄업로드.Size = new System.Drawing.Size(70, 19);
			this.btn일괄업로드.TabIndex = 1;
			this.btn일괄업로드.TabStop = false;
			this.btn일괄업로드.Text = "일괄업로드";
			this.btn일괄업로드.UseVisualStyleBackColor = false;
			// 
			// btn불러오기
			// 
			this.btn불러오기.BackColor = System.Drawing.Color.Transparent;
			this.btn불러오기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn불러오기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn불러오기.Location = new System.Drawing.Point(613, 3);
			this.btn불러오기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn불러오기.Name = "btn불러오기";
			this.btn불러오기.Size = new System.Drawing.Size(70, 19);
			this.btn불러오기.TabIndex = 0;
			this.btn불러오기.TabStop = false;
			this.btn불러오기.Text = "불러오기";
			this.btn불러오기.UseVisualStyleBackColor = false;
			// 
			// btn초기화
			// 
			this.btn초기화.BackColor = System.Drawing.Color.Transparent;
			this.btn초기화.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn초기화.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn초기화.Location = new System.Drawing.Point(537, 3);
			this.btn초기화.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn초기화.Name = "btn초기화";
			this.btn초기화.Size = new System.Drawing.Size(70, 19);
			this.btn초기화.TabIndex = 2;
			this.btn초기화.TabStop = false;
			this.btn초기화.Text = "초기화";
			this.btn초기화.UseVisualStyleBackColor = false;
			// 
			// P_CZ_MA_PITEM_FILE_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(770, 549);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_MA_PITEM_FILE_SUB";
			this.Text = "ERP iU";
			this.TitleText = "일괄업로드";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex파일)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn불러오기;
        private Duzon.Common.Controls.RoundedButton btn일괄업로드;
        private Dass.FlexGrid.FlexGrid _flex파일;
        private Duzon.Common.Controls.RoundedButton btn초기화;
    }
}