namespace cz
{
    partial class H_CZ_HELP_SL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_HELP_SL));
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.txt공장 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn검색 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.flex = new Dass.FlexGridLight.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panelExt1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flex)).BeginInit();
            this.SuspendLayout();
            // 
            // panelExt1
            // 
            this.panelExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExt1.Controls.Add(this.txt공장);
            this.panelExt1.Controls.Add(this.panel1);
            this.panelExt1.Location = new System.Drawing.Point(12, 53);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(647, 28);
            this.panelExt1.TabIndex = 35;
            // 
            // txt공장
            // 
            this.txt공장.Location = new System.Drawing.Point(91, 2);
            this.txt공장.Name = "txt공장";
            this.txt공장.ReadOnly = true;
            this.txt공장.Size = new System.Drawing.Size(178, 21);
            this.txt공장.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(254)))), ((int)(((byte)(177)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(86, 28);
            this.panel1.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(51, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "공장";
            // 
            // btn취소
            // 
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(598, 88);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(61, 19);
            this.btn취소.TabIndex = 38;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = true;
            // 
            // btn확인
            // 
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(536, 88);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(61, 19);
            this.btn확인.TabIndex = 37;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = true;
            // 
            // btn검색
            // 
            this.btn검색.BackColor = System.Drawing.Color.White;
            this.btn검색.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn검색.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn검색.Location = new System.Drawing.Point(473, 88);
            this.btn검색.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn검색.Name = "btn검색";
            this.btn검색.Size = new System.Drawing.Size(61, 19);
            this.btn검색.TabIndex = 36;
            this.btn검색.TabStop = false;
            this.btn검색.Text = "검색";
            this.btn검색.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flex);
            this.panel2.Location = new System.Drawing.Point(12, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 337);
            this.panel2.TabIndex = 39;
            // 
            // flex
            // 
            this.flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this.flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex.AutoResize = false;
            this.flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this.flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.flex.Location = new System.Drawing.Point(0, 0);
            this.flex.Name = "flex";
            this.flex.Rows.Count = 1;
            this.flex.Rows.DefaultSize = 18;
            this.flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.flex.ShowSort = false;
            this.flex.Size = new System.Drawing.Size(663, 337);
            this.flex.StyleInfo = resources.GetString("flex.StyleInfo");
            this.flex.TabIndex = 30;
            // 
            // H_CZ_HELP_SL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 476);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn취소);
            this.Controls.Add(this.btn확인);
            this.Controls.Add(this.btn검색);
            this.Controls.Add(this.panelExt1);
            this.Name = "H_CZ_HELP_SL";
            this.Text = "H_CZ_HELP_SL";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.panelExt1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.PanelExt panelExt1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn검색;
        private System.Windows.Forms.Panel panel2;
        protected Dass.FlexGridLight.FlexGrid flex;
        private System.Windows.Forms.TextBox txt공장;




    }
}