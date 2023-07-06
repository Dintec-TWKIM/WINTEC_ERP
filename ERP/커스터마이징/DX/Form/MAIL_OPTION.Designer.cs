
namespace DX
{
	partial class MAIL_OPTION
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
			this.chkCC포함 = new Duzon.Common.Controls.CheckBoxExt();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label25 = new System.Windows.Forms.Label();
			this.panelExt21 = new Duzon.Common.Controls.PanelExt();
			this.label1 = new System.Windows.Forms.Label();
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.rdo견적담당 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo보낸사람 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo메일회신 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo상용구 = new Duzon.Common.Controls.RadioButtonExt();
			this.panelExt2 = new Duzon.Common.Controls.PanelExt();
			this.panelExt3 = new Duzon.Common.Controls.PanelExt();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lbl받는사람 = new System.Windows.Forms.Label();
			this.lbl참조 = new System.Windows.Forms.Label();
			this.btn보내기 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelExt21.SuspendLayout();
			this.panelExt1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo견적담당)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo보낸사람)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo메일회신)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo상용구)).BeginInit();
			this.panelExt2.SuspendLayout();
			this.panelExt3.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkCC포함
			// 
			this.chkCC포함.AutoSize = true;
			this.chkCC포함.Location = new System.Drawing.Point(190, 11);
			this.chkCC포함.Name = "chkCC포함";
			this.chkCC포함.Size = new System.Drawing.Size(60, 16);
			this.chkCC포함.TabIndex = 17;
			this.chkCC포함.Text = "CC포함";
			this.chkCC포함.TextDD = null;
			this.chkCC포함.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label25, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panelExt21, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.panelExt1, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.panelExt2, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.panelExt3, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 53);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(589, 143);
			this.tableLayoutPanel1.TabIndex = 3;
			this.tableLayoutPanel1.Tag = "VIEWBOX";
			// 
			// label25
			// 
			this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label25.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label25.Location = new System.Drawing.Point(1, 1);
			this.label25.Margin = new System.Windows.Forms.Padding(1);
			this.label25.Name = "label25";
			this.tableLayoutPanel1.SetRowSpan(this.label25, 3);
			this.label25.Size = new System.Drawing.Size(98, 103);
			this.label25.TabIndex = 10;
			this.label25.Text = "받는사람";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelExt21
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panelExt21, 2);
			this.panelExt21.Controls.Add(this.chkCC포함);
			this.panelExt21.Controls.Add(this.rdo견적담당);
			this.panelExt21.Controls.Add(this.rdo보낸사람);
			this.panelExt21.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelExt21.Location = new System.Drawing.Point(101, 1);
			this.panelExt21.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt21.Name = "panelExt21";
			this.panelExt21.Size = new System.Drawing.Size(465, 33);
			this.panelExt21.TabIndex = 19;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(1, 106);
			this.label1.Margin = new System.Windows.Forms.Padding(1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 36);
			this.label1.TabIndex = 20;
			this.label1.Text = "본문";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelExt1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panelExt1, 2);
			this.panelExt1.Controls.Add(this.rdo메일회신);
			this.panelExt1.Controls.Add(this.rdo상용구);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelExt1.Location = new System.Drawing.Point(101, 106);
			this.panelExt1.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(465, 36);
			this.panelExt1.TabIndex = 21;
			// 
			// rdo견적담당
			// 
			this.rdo견적담당.AutoSize = true;
			this.rdo견적담당.Checked = true;
			this.rdo견적담당.Location = new System.Drawing.Point(11, 10);
			this.rdo견적담당.Name = "rdo견적담당";
			this.rdo견적담당.Size = new System.Drawing.Size(71, 16);
			this.rdo견적담당.TabIndex = 20;
			this.rdo견적담당.TabStop = true;
			this.rdo견적담당.Tag = "Y";
			this.rdo견적담당.Text = "견적담당";
			this.rdo견적담당.TextDD = null;
			this.rdo견적담당.UseKeyEnter = true;
			this.rdo견적담당.UseVisualStyleBackColor = true;
			// 
			// rdo보낸사람
			// 
			this.rdo보낸사람.AutoSize = true;
			this.rdo보낸사람.Location = new System.Drawing.Point(104, 10);
			this.rdo보낸사람.Name = "rdo보낸사람";
			this.rdo보낸사람.Size = new System.Drawing.Size(71, 16);
			this.rdo보낸사람.TabIndex = 19;
			this.rdo보낸사람.TabStop = true;
			this.rdo보낸사람.Tag = "N";
			this.rdo보낸사람.Text = "보낸사람";
			this.rdo보낸사람.TextDD = null;
			this.rdo보낸사람.UseKeyEnter = true;
			this.rdo보낸사람.UseVisualStyleBackColor = true;
			// 
			// rdo메일회신
			// 
			this.rdo메일회신.AutoSize = true;
			this.rdo메일회신.Location = new System.Drawing.Point(104, 10);
			this.rdo메일회신.Name = "rdo메일회신";
			this.rdo메일회신.Size = new System.Drawing.Size(125, 16);
			this.rdo메일회신.TabIndex = 22;
			this.rdo메일회신.Tag = "Y";
			this.rdo메일회신.Text = "상용구 + 메일회신";
			this.rdo메일회신.TextDD = null;
			this.rdo메일회신.UseKeyEnter = true;
			this.rdo메일회신.UseVisualStyleBackColor = true;
			// 
			// rdo상용구
			// 
			this.rdo상용구.AutoSize = true;
			this.rdo상용구.Checked = true;
			this.rdo상용구.Location = new System.Drawing.Point(11, 10);
			this.rdo상용구.Name = "rdo상용구";
			this.rdo상용구.Size = new System.Drawing.Size(59, 16);
			this.rdo상용구.TabIndex = 21;
			this.rdo상용구.TabStop = true;
			this.rdo상용구.Tag = "N";
			this.rdo상용구.Text = "상용구";
			this.rdo상용구.TextDD = null;
			this.rdo상용구.UseKeyEnter = true;
			this.rdo상용구.UseVisualStyleBackColor = true;
			// 
			// panelExt2
			// 
			this.panelExt2.Controls.Add(this.lbl받는사람);
			this.panelExt2.Location = new System.Drawing.Point(181, 36);
			this.panelExt2.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt2.Name = "panelExt2";
			this.panelExt2.Size = new System.Drawing.Size(385, 33);
			this.panelExt2.TabIndex = 22;
			// 
			// panelExt3
			// 
			this.panelExt3.Controls.Add(this.lbl참조);
			this.panelExt3.Location = new System.Drawing.Point(181, 71);
			this.panelExt3.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt3.Name = "panelExt3";
			this.panelExt3.Size = new System.Drawing.Size(385, 33);
			this.panelExt3.TabIndex = 23;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.White;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(101, 36);
			this.label2.Margin = new System.Windows.Forms.Padding(1);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.label2.Size = new System.Drawing.Size(78, 33);
			this.label2.TabIndex = 24;
			this.label2.Text = "받는사람";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.White;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(101, 71);
			this.label3.Margin = new System.Windows.Forms.Padding(1);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.label3.Size = new System.Drawing.Size(78, 33);
			this.label3.TabIndex = 25;
			this.label3.Text = "참조";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl받는사람
			// 
			this.lbl받는사람.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl받는사람.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl받는사람.Location = new System.Drawing.Point(0, 0);
			this.lbl받는사람.Margin = new System.Windows.Forms.Padding(1);
			this.lbl받는사람.Name = "lbl받는사람";
			this.lbl받는사람.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.lbl받는사람.Size = new System.Drawing.Size(368, 33);
			this.lbl받는사람.TabIndex = 0;
			this.lbl받는사람.Text = "label4";
			this.lbl받는사람.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl참조
			// 
			this.lbl참조.AutoEllipsis = true;
			this.lbl참조.AutoSize = true;
			this.lbl참조.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl참조.Location = new System.Drawing.Point(2, 8);
			this.lbl참조.Margin = new System.Windows.Forms.Padding(0);
			this.lbl참조.Name = "lbl참조";
			this.lbl참조.Size = new System.Drawing.Size(43, 17);
			this.lbl참조.TabIndex = 1;
			this.lbl참조.Text = "label5";
			this.lbl참조.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btn보내기
			// 
			this.btn보내기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn보내기.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn보내기.FlatAppearance.BorderSize = 0;
			this.btn보내기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn보내기.ForeColor = System.Drawing.Color.White;
			this.btn보내기.Location = new System.Drawing.Point(518, 10);
			this.btn보내기.Name = "btn보내기";
			this.btn보내기.Size = new System.Drawing.Size(70, 26);
			this.btn보내기.TabIndex = 19;
			this.btn보내기.Text = "보내기";
			this.btn보내기.UseVisualStyleBackColor = false;
			// 
			// MAIL_OPTION
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(601, 202);
			this.Controls.Add(this.btn보내기);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "MAIL_OPTION";
			this.Padding = new System.Windows.Forms.Padding(6, 53, 6, 6);
			this.ShowInTaskbar = false;
			this.Text = "MAIL_OPTION";
			this.TitleText = "메일 보내기";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelExt21.ResumeLayout(false);
			this.panelExt21.PerformLayout();
			this.panelExt1.ResumeLayout(false);
			this.panelExt1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo견적담당)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo보낸사람)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo메일회신)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo상용구)).EndInit();
			this.panelExt2.ResumeLayout(false);
			this.panelExt3.ResumeLayout(false);
			this.panelExt3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private Duzon.Common.Controls.CheckBoxExt chkCC포함;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label25;
		private Duzon.Common.Controls.PanelExt panelExt1;
		private System.Windows.Forms.Label label1;
		private Duzon.Common.Controls.PanelExt panelExt21;
		private Duzon.Common.Controls.RadioButtonExt rdo견적담당;
		private Duzon.Common.Controls.RadioButtonExt rdo보낸사람;
		private Duzon.Common.Controls.RadioButtonExt rdo메일회신;
		private Duzon.Common.Controls.RadioButtonExt rdo상용구;
		private Duzon.Common.Controls.PanelExt panelExt3;
		private Duzon.Common.Controls.PanelExt panelExt2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lbl받는사람;
		private System.Windows.Forms.Label lbl참조;
		private System.Windows.Forms.Button btn보내기;
	}
}