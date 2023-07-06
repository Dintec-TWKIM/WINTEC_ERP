
using C1.Win.C1FlexGrid;
using Duzon.Common.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_FI_Z_AVLFNC_PARTNER_CLOSE
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(P_FI_Z_AVLFNC_PARTNER_CLOSE));
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn닫기 = new RoundedButton(this.components);
            ((ISupportInitialize)this.closeButton).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this._flex.BeginInit();
            this.SuspendLayout();
            this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Controls.Add((Control)this._flex, 0, 0);
            this.tableLayoutPanel1.Location = new Point(3, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Size = new Size(688, 346);
            this.tableLayoutPanel1.TabIndex = 99;
            this._flex.AllowFreezing = AllowFreezingEnum.Both;
            this._flex.AllowResizing = AllowResizingEnum.Both;
            this._flex.AllowSorting = AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = DockStyle.Fill;
            this._flex.DrawMode = DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.Font = new Font("굴림체", 9f);
            this._flex.KeyActionEnter = KeyActionEnum.MoveAcross;
            this._flex.Location = new Point(3, 3);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = SelectionModeEnum.Row;
            this._flex.ShowButtons = ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new Size(682, 340);
            this._flex.StyleInfo = componentResourceManager.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            this._flex.UseGridCalculator = true;
            this.btn닫기.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btn닫기.BackColor = Color.White;
            this.btn닫기.Cursor = Cursors.Hand;
            this.btn닫기.FlatStyle = FlatStyle.Flat;
            this.btn닫기.Location = new Point(604, 23);
            this.btn닫기.MaximumSize = new Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new Size(86, 19);
            this.btn닫기.TabIndex = 100;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = false;
            this.AutoScaleMode = AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new Size(694, 399);
            this.Controls.Add((Control)this.btn닫기);
            this.Controls.Add((Control)this.tableLayoutPanel1);
            this.Name = "P_FI_Z_AVLFNC_PARTNER_CLOSE";
            this.Text = "더존 ERPiU";
            this.TitleText = "휴폐업정보 도움창";
            ((ISupportInitialize)this.closeButton).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this._flex.EndInit();
            this.ResumeLayout(false);
        }

		private TableLayoutPanel tableLayoutPanel1;
		private Dass.FlexGrid.FlexGrid _flex;
		private RoundedButton btn닫기;

		#endregion
	}
}