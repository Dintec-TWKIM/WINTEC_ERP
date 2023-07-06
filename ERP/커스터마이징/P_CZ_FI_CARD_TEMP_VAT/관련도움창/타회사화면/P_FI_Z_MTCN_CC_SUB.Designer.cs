
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_FI_Z_MTCN_CC_SUB
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
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.btn취소 = new RoundedButton(this.components);
            this.btn확인 = new RoundedButton(this.components);
            this.oneGrid1 = new OneGrid();
            this.oneGridItem1 = new OneGridItem();
            this.bpPanelControl1 = new BpPanelControl();
            this.bpt계정 = new BpCodeTextBox();
            this.labelExt1 = new LabelExt();
            this.oneGridItem2 = new OneGridItem();
            this.bpPanelControl2 = new BpPanelControl();
            this.bpt코스트센터 = new BpCodeTextBox();
            this.labelExt2 = new LabelExt();
            ((ISupportInitialize)this.closeButton).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.SuspendLayout();
            this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add((Control)this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add((Control)this.oneGrid1, 0, 1);
            this.tableLayoutPanel1.Location = new Point(1, 51);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new Size(362, 99);
            this.tableLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.flowLayoutPanel1.Controls.Add((Control)this.btn취소);
            this.flowLayoutPanel1.Controls.Add((Control)this.btn확인);
            this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(356, 25);
            this.flowLayoutPanel1.TabIndex = 1;
            this.btn취소.BackColor = Color.Transparent;
            this.btn취소.Cursor = Cursors.Hand;
            this.btn취소.FlatStyle = FlatStyle.Flat;
            this.btn취소.Location = new Point(283, 3);
            this.btn취소.MaximumSize = new Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new Size(70, 19);
            this.btn취소.TabIndex = 0;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            this.btn확인.BackColor = Color.Transparent;
            this.btn확인.Cursor = Cursors.Hand;
            this.btn확인.FlatStyle = FlatStyle.Flat;
            this.btn확인.Location = new Point(207, 3);
            this.btn확인.MaximumSize = new Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new Size(70, 19);
            this.btn확인.TabIndex = 1;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = false;
            this.oneGrid1.Dock = DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new OneGridItem[2]
            {
        this.oneGridItem1,
        this.oneGridItem2
            });
            this.oneGrid1.Location = new Point(3, 34);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new Size(356, 62);
            this.oneGrid1.TabIndex = 2;
            this.oneGridItem1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.oneGridItem1.Controls.Add((Control)this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new Size(346, 23);
            this.oneGridItem1.SizeMode = SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            this.bpPanelControl1.Controls.Add((Control)this.bpt계정);
            this.bpPanelControl1.Controls.Add((Control)this.labelExt1);
            this.bpPanelControl1.Location = new Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new Size(342, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            this.bpt계정.HelpID = HelpID.P_FI_ACCTCODE_SUB;
            this.bpt계정.Location = new Point(157, 1);
            this.bpt계정.Name = "bpt계정";
            this.bpt계정.Size = new Size(185, 21);
            this.bpt계정.TabIndex = 1;
            this.bpt계정.TabStop = false;
            this.bpt계정.Text = "bpCodeTextBox1";
            this.labelExt1.Location = new Point(0, 3);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new Size(156, 16);
            this.labelExt1.TabIndex = 0;
            this.labelExt1.Text = "복사할계정";
            this.labelExt1.TextAlign = ContentAlignment.MiddleRight;
            this.oneGridItem2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.oneGridItem2.Controls.Add((Control)this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new Size(346, 23);
            this.oneGridItem2.SizeMode = SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            this.bpPanelControl2.Controls.Add((Control)this.bpt코스트센터);
            this.bpPanelControl2.Controls.Add((Control)this.labelExt2);
            this.bpPanelControl2.Location = new Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new Size(342, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl2";
            this.bpt코스트센터.HelpID = HelpID.P_MA_CC_SUB;
            this.bpt코스트센터.Location = new Point(157, 1);
            this.bpt코스트센터.Name = "bpt코스트센터";
            this.bpt코스트센터.Size = new Size(185, 21);
            this.bpt코스트센터.TabIndex = 2;
            this.bpt코스트센터.TabStop = false;
            this.bpt코스트센터.Text = "bpCodeTextBox2";
            this.labelExt2.Location = new Point(0, 3);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Size = new Size(156, 16);
            this.labelExt2.TabIndex = 1;
            this.labelExt2.Text = "코스트센터";
            this.labelExt2.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CaptionPaint = true;
            this.ClientSize = new Size(367, 152);
            this.Controls.Add((Control)this.tableLayoutPanel1);
            this.Name = "P_FI_Z_MTCN_CC_SUB";
            this.Text = "더존 ERP-iU";
            this.TitleText = "일괄복사";
            ((ISupportInitialize)this.closeButton).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

		private TableLayoutPanel tableLayoutPanel1;
		private FlowLayoutPanel flowLayoutPanel1;
		private RoundedButton btn취소;
		private RoundedButton btn확인;
		private OneGrid oneGrid1;
		private OneGridItem oneGridItem1;
		private BpPanelControl bpPanelControl1;
		private LabelExt labelExt1;
		private OneGridItem oneGridItem2;
		private BpPanelControl bpPanelControl2;
		private LabelExt labelExt2;
		private BpCodeTextBox bpt계정;
		private BpCodeTextBox bpt코스트센터;

		#endregion
	}
}