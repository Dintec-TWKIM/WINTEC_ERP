
using Duzon.BizOn.Erpu.Resource;
using Duzon.Common.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_FI_CARD_DOCU_SUB
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
            this.components = (IContainer)new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(P_CZ_FI_CARD_DOCU_SUB));
            this.m_titlePanel = new Panel();
            this.panel3 = new Panel();
            this.panel1 = new Panel();
            this.lblTitle = new Label();
            this.panelExt2 = new PanelExt();
            this.lbl전표처리 = new LabelExt();
            this.btn_건별 = new RoundedButton(this.components);
            this.btn_일괄 = new RoundedButton(this.components);
            ((ISupportInitialize)this.closeButton).BeginInit();
            this.m_titlePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelExt2.SuspendLayout();
            this.SuspendLayout();
            this.m_titlePanel.BackColor = Color.White;
            this.m_titlePanel.BackgroundImage = (Image)HelpFormResource.Pattern;
            this.m_titlePanel.Controls.Add((Control)this.panel3);
            this.m_titlePanel.Controls.Add((Control)this.panel1);
            this.m_titlePanel.Dock = DockStyle.Top;
            this.m_titlePanel.ForeColor = Color.Black;
            this.m_titlePanel.Location = new Point(0, 0);
            this.m_titlePanel.Name = "m_titlePanel";
            this.m_titlePanel.Size = new Size(267, 33);
            this.m_titlePanel.TabIndex = 23;
            this.panel3.BackgroundImage = (Image)HelpFormResource.Right;
            this.panel3.Dock = DockStyle.Right;
            this.panel3.Location = new Point(168, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(99, 33);
            this.panel3.TabIndex = 3;
            this.panel1.BackgroundImage = (Image)HelpFormResource.Left;
            this.panel1.Controls.Add((Control)this.lblTitle);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(210, 33);
            this.panel1.TabIndex = 2;
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("굴림", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)129);
            this.lblTitle.ForeColor = Color.FromArgb(23, 44, 80);
            this.lblTitle.Location = new Point(35, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(71, 15);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "전표처리";
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.panelExt2.Controls.Add((Control)this.lbl전표처리);
            this.panelExt2.Location = new Point(12, 41);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new Size(245, 23);
            this.panelExt2.TabIndex = 32;
            this.lbl전표처리.BackColor = Color.Transparent;
            this.lbl전표처리.Font = new Font("굴림체", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)129);
            this.lbl전표처리.Location = new Point(20, 4);
            this.lbl전표처리.Name = "lbl전표처리";
            this.lbl전표처리.Resizeble = true;
            this.lbl전표처리.Size = new Size(194, 16);
            this.lbl전표처리.TabIndex = 0;
            this.lbl전표처리.Text = "전표처리 방식을 선택해주세요";
            this.lbl전표처리.TextAlign = ContentAlignment.MiddleLeft;
            this.btn_건별.BackColor = Color.White;
            this.btn_건별.Cursor = Cursors.Hand;
            this.btn_건별.FlatStyle = FlatStyle.Flat;
            this.btn_건별.Location = new Point(12, 71);
            this.btn_건별.MaximumSize = new Size(0, 19);
            this.btn_건별.Name = "btn_건별";
            this.btn_건별.Size = new Size(107, 19);
            this.btn_건별.TabIndex = 36;
            this.btn_건별.TabStop = false;
            this.btn_건별.Text = "건별 전표처리";
            this.btn_건별.UseVisualStyleBackColor = false;
            this.btn_일괄.BackColor = Color.White;
            this.btn_일괄.Cursor = Cursors.Hand;
            this.btn_일괄.FlatStyle = FlatStyle.Flat;
            this.btn_일괄.Location = new Point(150, 71);
            this.btn_일괄.MaximumSize = new Size(0, 19);
            this.btn_일괄.Name = "btn_일괄";
            this.btn_일괄.Size = new Size(107, 19);
            this.btn_일괄.TabIndex = 37;
            this.btn_일괄.TabStop = false;
            this.btn_일괄.Text = "일괄 전표처리";
            this.btn_일괄.UseVisualStyleBackColor = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(267, 97);
            this.Controls.Add((Control)this.btn_일괄);
            this.Controls.Add((Control)this.btn_건별);
            this.Controls.Add((Control)this.panelExt2);
            this.Controls.Add((Control)this.m_titlePanel);
            this.Name = "P_CZ_FI_CARD_DOCU_SUB";
            this.Text = "P_CZ_FI_CARD_DOCU_SUB";
            ((ISupportInitialize)this.closeButton).EndInit();
            this.m_titlePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelExt2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

		private Panel m_titlePanel;
		private Panel panel3;
		private Panel panel1;
		private Label lblTitle;
		private PanelExt panelExt2;
		private LabelExt lbl전표처리;
		private RoundedButton btn_건별;
		private RoundedButton btn_일괄;

		#endregion
	}
}