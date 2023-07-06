namespace cz
{
    partial class P_CZ_FI_EPDOCU_MSG
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
            this._btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxExt1 = new Duzon.Common.Controls.TextBoxExt();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btn닫기
            // 
            this._btn닫기.BackColor = System.Drawing.Color.White;
            this._btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btn닫기.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btn닫기.Location = new System.Drawing.Point(168, 11);
            this._btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this._btn닫기.Name = "_btn닫기";
            this._btn닫기.Size = new System.Drawing.Size(70, 19);
            this._btn닫기.TabIndex = 113;
            this._btn닫기.TabStop = false;
            this._btn닫기.Text = "닫기";
            this._btn닫기.UseVisualStyleBackColor = true;
            this._btn닫기.Click += new System.EventHandler(this._btn닫기_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxExt1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 354F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(407, 445);
            this.tableLayoutPanel1.TabIndex = 117;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExt1.Location = new System.Drawing.Point(3, 3);
            this.textBoxExt1.Multiline = true;
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExt1.Size = new System.Drawing.Size(401, 348);
            this.textBoxExt1.TabIndex = 114;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._btn닫기);
            this.panel2.Location = new System.Drawing.Point(0, 437);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 55);
            this.panel2.TabIndex = 118;
            // 
            // P_CZ_FI_EPDOCU_MSG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(407, 475);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_CZ_FI_EPDOCU_MSG";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "오류메세지";
            this.TitleText = "오류메시지";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Duzon.Common.Controls.RoundedButton _btn닫기;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.TextBoxExt textBoxExt1;
        private System.Windows.Forms.Panel panel2;
        #endregion
    }
}