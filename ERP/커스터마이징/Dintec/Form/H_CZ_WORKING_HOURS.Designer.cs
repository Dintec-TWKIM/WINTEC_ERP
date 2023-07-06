namespace Dintec
{
	partial class H_CZ_WORKING_HOURS
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_WORKING_HOURS));
			this.lbl시간 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.pnl닫기 = new System.Windows.Forms.Panel();
			this.lbl카운터 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl시간
			// 
			this.lbl시간.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl시간.ForeColor = System.Drawing.Color.Blue;
			this.lbl시간.Location = new System.Drawing.Point(12, 10);
			this.lbl시간.Name = "lbl시간";
			this.lbl시간.Size = new System.Drawing.Size(192, 21);
			this.lbl시간.TabIndex = 0;
			this.lbl시간.Text = "11월 28일 11시간 28분";
			this.lbl시간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl시간.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Lbl시간_MouseDown);
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
			// 
			// pnlBottom
			// 
			this.pnlBottom.BackColor = System.Drawing.Color.Black;
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 39);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(309, 1);
			this.pnlBottom.TabIndex = 1;
			// 
			// pnlLeft
			// 
			this.pnlLeft.BackColor = System.Drawing.Color.Black;
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(1, 39);
			this.pnlLeft.TabIndex = 2;
			// 
			// pnlTop
			// 
			this.pnlTop.BackColor = System.Drawing.Color.Black;
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(1, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(308, 1);
			this.pnlTop.TabIndex = 3;
			// 
			// pnlRight
			// 
			this.pnlRight.BackColor = System.Drawing.Color.Black;
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlRight.Location = new System.Drawing.Point(308, 1);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(1, 38);
			this.pnlRight.TabIndex = 4;
			// 
			// pnl닫기
			// 
			this.pnl닫기.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl닫기.BackgroundImage")));
			this.pnl닫기.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pnl닫기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pnl닫기.Location = new System.Drawing.Point(270, 6);
			this.pnl닫기.Name = "pnl닫기";
			this.pnl닫기.Size = new System.Drawing.Size(30, 28);
			this.pnl닫기.TabIndex = 5;
			this.pnl닫기.Click += new System.EventHandler(this.Pnl닫기_Click);
			// 
			// lbl카운터
			// 
			this.lbl카운터.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl카운터.ForeColor = System.Drawing.Color.Red;
			this.lbl카운터.Location = new System.Drawing.Point(202, 10);
			this.lbl카운터.Name = "lbl카운터";
			this.lbl카운터.Size = new System.Drawing.Size(63, 21);
			this.lbl카운터.TabIndex = 6;
			this.lbl카운터.Text = "(88:88)";
			this.lbl카운터.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// H_CZ_WORKING_HOURS
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(309, 40);
			this.Controls.Add(this.lbl카운터);
			this.Controls.Add(this.pnl닫기);
			this.Controls.Add(this.pnlRight);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.pnlLeft);
			this.Controls.Add(this.pnlBottom);
			this.Controls.Add(this.lbl시간);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "H_CZ_WORKING_HOURS";
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_WORKING_HOURS";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.H_CZ_WORKING_HOURS_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.H_CZ_WORKING_HOURS_MouseDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl시간;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlRight;
		private System.Windows.Forms.Panel pnl닫기;
		private System.Windows.Forms.Label lbl카운터;
	}
}