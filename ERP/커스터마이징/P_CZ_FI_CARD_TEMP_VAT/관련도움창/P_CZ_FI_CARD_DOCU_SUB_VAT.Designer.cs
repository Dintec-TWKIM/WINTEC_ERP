
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_FI_CARD_DOCU_SUB_VAT
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
			this.btn_cancle = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn_process = new Duzon.Common.Controls.RoundedButton(this.components);
			this.pnlDocu = new Duzon.Common.Controls.PanelExt();
			this.panel2 = new System.Windows.Forms.Panel();
			this.rdo일괄 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo건별 = new Duzon.Common.Controls.RadioButtonExt();
			this.bpctx부가세계정 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.panel5 = new Duzon.Common.Controls.PanelExt();
			this.panelExt4 = new Duzon.Common.Controls.PanelExt();
			this.lbl작성부서 = new Duzon.Common.Controls.LabelExt();
			this.lbl회계단위 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.pnlDocu.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo일괄)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo건별)).BeginInit();
			this.panelExt4.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_cancle
			// 
			this.btn_cancle.BackColor = System.Drawing.Color.White;
			this.btn_cancle.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn_cancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_cancle.Location = new System.Drawing.Point(313, 51);
			this.btn_cancle.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn_cancle.Name = "btn_cancle";
			this.btn_cancle.Size = new System.Drawing.Size(77, 19);
			this.btn_cancle.TabIndex = 39;
			this.btn_cancle.TabStop = false;
			this.btn_cancle.Text = "전표취소";
			this.btn_cancle.UseVisualStyleBackColor = false;
			// 
			// btn_process
			// 
			this.btn_process.BackColor = System.Drawing.Color.White;
			this.btn_process.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn_process.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_process.Location = new System.Drawing.Point(230, 51);
			this.btn_process.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn_process.Name = "btn_process";
			this.btn_process.Size = new System.Drawing.Size(77, 19);
			this.btn_process.TabIndex = 40;
			this.btn_process.TabStop = false;
			this.btn_process.Text = "전표처리";
			this.btn_process.UseVisualStyleBackColor = false;
			// 
			// pnlDocu
			// 
			this.pnlDocu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDocu.Controls.Add(this.panel2);
			this.pnlDocu.Controls.Add(this.bpctx부가세계정);
			this.pnlDocu.Controls.Add(this.panel5);
			this.pnlDocu.Controls.Add(this.panelExt4);
			this.pnlDocu.Location = new System.Drawing.Point(6, 73);
			this.pnlDocu.Name = "pnlDocu";
			this.pnlDocu.Size = new System.Drawing.Size(384, 55);
			this.pnlDocu.TabIndex = 41;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.rdo일괄);
			this.panel2.Controls.Add(this.rdo건별);
			this.panel2.Location = new System.Drawing.Point(99, 26);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(280, 24);
			this.panel2.TabIndex = 145;
			// 
			// rdo일괄
			// 
			this.rdo일괄.Checked = true;
			this.rdo일괄.Location = new System.Drawing.Point(150, 2);
			this.rdo일괄.Name = "rdo일괄";
			this.rdo일괄.Size = new System.Drawing.Size(105, 20);
			this.rdo일괄.TabIndex = 14;
			this.rdo일괄.TabStop = true;
			this.rdo일괄.Text = "일괄처리";
			this.rdo일괄.TextDD = null;
			this.rdo일괄.UseKeyEnter = true;
			this.rdo일괄.UseVisualStyleBackColor = true;
			// 
			// rdo건별
			// 
			this.rdo건별.Location = new System.Drawing.Point(3, 2);
			this.rdo건별.Name = "rdo건별";
			this.rdo건별.Size = new System.Drawing.Size(105, 20);
			this.rdo건별.TabIndex = 13;
			this.rdo건별.TabStop = true;
			this.rdo건별.Text = "건별처리";
			this.rdo건별.TextDD = null;
			this.rdo건별.UseKeyEnter = true;
			this.rdo건별.UseVisualStyleBackColor = true;
			// 
			// bpctx부가세계정
			// 
			this.bpctx부가세계정.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
			this.bpctx부가세계정.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_ACCTCODE_SUB;
			this.bpctx부가세계정.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.bpctx부가세계정.Location = new System.Drawing.Point(99, 2);
			this.bpctx부가세계정.Name = "bpctx부가세계정";
			this.bpctx부가세계정.SetDefaultValue = true;
			this.bpctx부가세계정.Size = new System.Drawing.Size(280, 21);
			this.bpctx부가세계정.TabIndex = 0;
			this.bpctx부가세계정.TabStop = false;
			this.bpctx부가세계정.Text = "bpCodeNTextBox1";
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.Transparent;
			this.panel5.Location = new System.Drawing.Point(6, 26);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(375, 1);
			this.panel5.TabIndex = 2;
			// 
			// panelExt4
			// 
			this.panelExt4.BackColor = System.Drawing.Color.MintCream;
			this.panelExt4.Controls.Add(this.lbl작성부서);
			this.panelExt4.Controls.Add(this.lbl회계단위);
			this.panelExt4.Location = new System.Drawing.Point(1, 1);
			this.panelExt4.Name = "panelExt4";
			this.panelExt4.Size = new System.Drawing.Size(95, 51);
			this.panelExt4.TabIndex = 0;
			// 
			// lbl작성부서
			// 
			this.lbl작성부서.Location = new System.Drawing.Point(3, 28);
			this.lbl작성부서.Name = "lbl작성부서";
			this.lbl작성부서.Size = new System.Drawing.Size(90, 18);
			this.lbl작성부서.TabIndex = 7;
			this.lbl작성부서.Text = "처리방식";
			this.lbl작성부서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl회계단위
			// 
			this.lbl회계단위.Location = new System.Drawing.Point(3, 4);
			this.lbl회계단위.Name = "lbl회계단위";
			this.lbl회계단위.Size = new System.Drawing.Size(90, 18);
			this.lbl회계단위.TabIndex = 6;
			this.lbl회계단위.Text = "부가세처리계정";
			this.lbl회계단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_FI_CARD_DOCU_SUB_VAT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(397, 133);
			this.Controls.Add(this.pnlDocu);
			this.Controls.Add(this.btn_process);
			this.Controls.Add(this.btn_cancle);
			this.Name = "P_CZ_FI_CARD_DOCU_SUB_VAT";
			this.Text = "P_CZ_FI_CARD_DOCU_SUB_VAT";
			this.TitleText = "전표처리옵션";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.pnlDocu.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rdo일괄)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo건별)).EndInit();
			this.panelExt4.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		private RoundedButton btn_cancle;
		private RoundedButton btn_process;
		private PanelExt pnlDocu;
		private Panel panel2;
		private RadioButtonExt rdo일괄;
		private RadioButtonExt rdo건별;
		private PanelExt panel5;
		private PanelExt panelExt4;
		private LabelExt lbl작성부서;
		private LabelExt lbl회계단위;
		private BpCodeNTextBox bpctx부가세계정;

		#endregion
	}
}