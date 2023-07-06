namespace cz
{
    partial class P_CZ_MA_FAX_SUB
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt받는사람 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl받는사람 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt첨부파일 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl첨부파일 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn보내기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.web첨부파일뷰어 = new Duzon.Common.Controls.WebBrowserExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.web첨부파일뷰어, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(681, 584);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(675, 62);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(665, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt받는사람);
            this.bpPanelControl1.Controls.Add(this.lbl받는사람);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(659, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // txt받는사람
            // 
            this.txt받는사람.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt받는사람.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt받는사람.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt받는사람.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt받는사람.Location = new System.Drawing.Point(106, 0);
            this.txt받는사람.Name = "txt받는사람";
            this.txt받는사람.Size = new System.Drawing.Size(553, 21);
            this.txt받는사람.TabIndex = 1;
            // 
            // lbl받는사람
            // 
            this.lbl받는사람.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl받는사람.Location = new System.Drawing.Point(0, 0);
            this.lbl받는사람.Name = "lbl받는사람";
            this.lbl받는사람.Size = new System.Drawing.Size(100, 23);
            this.lbl받는사람.TabIndex = 0;
            this.lbl받는사람.Text = "받는사람";
            this.lbl받는사람.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(665, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.txt첨부파일);
            this.bpPanelControl2.Controls.Add(this.lbl첨부파일);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(659, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // txt첨부파일
            // 
            this.txt첨부파일.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt첨부파일.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt첨부파일.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt첨부파일.Location = new System.Drawing.Point(106, 0);
            this.txt첨부파일.Name = "txt첨부파일";
            this.txt첨부파일.ReadOnly = true;
            this.txt첨부파일.Size = new System.Drawing.Size(553, 21);
            this.txt첨부파일.TabIndex = 2;
            this.txt첨부파일.TabStop = false;
            // 
            // lbl첨부파일
            // 
            this.lbl첨부파일.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl첨부파일.Location = new System.Drawing.Point(0, 0);
            this.lbl첨부파일.Name = "lbl첨부파일";
            this.lbl첨부파일.Size = new System.Drawing.Size(100, 23);
            this.lbl첨부파일.TabIndex = 1;
            this.lbl첨부파일.Text = "첨부파일";
            this.lbl첨부파일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn보내기);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 71);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(675, 24);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn보내기
            // 
            this.btn보내기.BackColor = System.Drawing.Color.Transparent;
            this.btn보내기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn보내기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn보내기.Location = new System.Drawing.Point(602, 3);
            this.btn보내기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn보내기.Name = "btn보내기";
            this.btn보내기.Size = new System.Drawing.Size(70, 19);
            this.btn보내기.TabIndex = 0;
            this.btn보내기.TabStop = false;
            this.btn보내기.Text = "보내기";
            this.btn보내기.UseVisualStyleBackColor = false;
            // 
            // web첨부파일뷰어
            // 
            this.web첨부파일뷰어.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web첨부파일뷰어.Location = new System.Drawing.Point(3, 101);
            this.web첨부파일뷰어.MinimumSize = new System.Drawing.Size(20, 20);
            this.web첨부파일뷰어.Name = "web첨부파일뷰어";
            this.web첨부파일뷰어.Size = new System.Drawing.Size(675, 480);
            this.web첨부파일뷰어.TabIndex = 3;
            // 
            // P_CZ_MA_FAX_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(683, 634);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_MA_FAX_SUB";
            this.Text = "DINTEC ERP iU";
            this.TitleText = "팩스 보내기";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt받는사람;
        private Duzon.Common.Controls.LabelExt lbl받는사람;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.TextBoxExt txt첨부파일;
        private Duzon.Common.Controls.LabelExt lbl첨부파일;
        private Duzon.Common.Controls.RoundedButton btn보내기;
        private Duzon.Common.Controls.WebBrowserExt web첨부파일뷰어;
    }
}