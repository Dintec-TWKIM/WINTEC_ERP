namespace trade
{
    partial class CodeHelpNO_PO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeHelpNO_PO));
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.txt검색 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_titlePanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panelExt1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // panelExt1
            // 
            this.panelExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExt1.Controls.Add(this.txt검색);
            this.panelExt1.Controls.Add(this.panel5);
            this.panelExt1.Location = new System.Drawing.Point(9, 46);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(274, 30);
            this.panelExt1.TabIndex = 43;
            // 
            // txt검색
            // 
            this.txt검색.Location = new System.Drawing.Point(91, 4);
            this.txt검색.Name = "txt검색";
            this.txt검색.Size = new System.Drawing.Size(178, 21);
            this.txt검색.TabIndex = 16;
            this.txt검색.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt검색_KeyDown);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(254)))), ((int)(((byte)(177)))));
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(-1, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(86, 45);
            this.panel5.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(54, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "검색";
            // 
            // btn취소
            // 
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(222, 78);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 22);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(61, 22);
            this.btn취소.TabIndex = 42;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = true;
            this.btn취소.Click += new System.EventHandler(this.btn취소_Click);
            // 
            // btn확인
            // 
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(160, 78);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 22);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(61, 22);
            this.btn확인.TabIndex = 41;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = true;
            this.btn확인.Click += new System.EventHandler(this.btn확인_Click);
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(7, 102);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(279, 278);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 44;
            // 
            // m_titlePanel
            // 
            this.m_titlePanel.BackColor = System.Drawing.Color.White;
            this.m_titlePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_titlePanel.BackgroundImage")));
            this.m_titlePanel.ForeColor = System.Drawing.Color.Black;
            this.m_titlePanel.Location = new System.Drawing.Point(0, 0);
            this.m_titlePanel.Name = "m_titlePanel";
            this.m_titlePanel.Size = new System.Drawing.Size(389, 40);
            this.m_titlePanel.TabIndex = 24;
            // 
            // CodeHelpNO_PO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 386);
            this.Controls.Add(this._flex);
            this.Controls.Add(this.panelExt1);
            this.Controls.Add(this.btn취소);
            this.Controls.Add(this.btn확인);
            this.Controls.Add(this.m_titlePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CodeHelpNO_PO";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.panelExt1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.PanelExt panelExt1;
        private System.Windows.Forms.TextBox txt검색;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.Panel m_titlePanel;
    }
}