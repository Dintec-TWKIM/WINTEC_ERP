
namespace cz
{
	partial class P_CZ_PR_POP_REG_IMAGE_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_POP_REG_IMAGE_SUB));
			this._flex작업지침서 = new Dass.FlexGrid.FlexGrid(this.components);
			this.web작업지침서 = new Duzon.Common.Controls.WebBrowserExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn미리보기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn열기 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flex작업지침서)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.imagePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _flex작업지침서
			// 
			this._flex작업지침서.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex작업지침서.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex작업지침서.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex작업지침서.AutoResize = false;
			this._flex작업지침서.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex작업지침서.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex작업지침서.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex작업지침서.EnabledHeaderCheck = true;
			this._flex작업지침서.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex작업지침서.Location = new System.Drawing.Point(0, 0);
			this._flex작업지침서.Name = "_flex작업지침서";
			this._flex작업지침서.Rows.Count = 1;
			this._flex작업지침서.Rows.DefaultSize = 18;
			this._flex작업지침서.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex작업지침서.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex작업지침서.ShowSort = false;
			this._flex작업지침서.Size = new System.Drawing.Size(785, 81);
			this._flex작업지침서.StyleInfo = resources.GetString("_flex작업지침서.StyleInfo");
			this._flex작업지침서.TabIndex = 0;
			this._flex작업지침서.UseGridCalculator = true;
			// 
			// web작업지침서
			// 
			this.web작업지침서.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.web작업지침서.Location = new System.Drawing.Point(3, 28);
			this.web작업지침서.MinimumSize = new System.Drawing.Size(20, 20);
			this.web작업지침서.Name = "web작업지침서";
			this.web작업지침서.Size = new System.Drawing.Size(779, 370);
			this.web작업지침서.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(2, 49);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flex작업지침서);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.imagePanel1);
			this.splitContainer1.Size = new System.Drawing.Size(785, 486);
			this.splitContainer1.SplitterDistance = 81;
			this.splitContainer1.TabIndex = 1;
			// 
			// imagePanel1
			// 
			this.imagePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.web작업지침서);
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(0, 0);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(785, 401);
			this.imagePanel1.TabIndex = 4;
			this.imagePanel1.TitleText = "미리보기";
			// 
			// btn미리보기
			// 
			this.btn미리보기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn미리보기.BackColor = System.Drawing.Color.Transparent;
			this.btn미리보기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn미리보기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn미리보기.Location = new System.Drawing.Point(711, 23);
			this.btn미리보기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn미리보기.Name = "btn미리보기";
			this.btn미리보기.Size = new System.Drawing.Size(70, 19);
			this.btn미리보기.TabIndex = 2;
			this.btn미리보기.TabStop = false;
			this.btn미리보기.Text = "미리보기";
			this.btn미리보기.UseVisualStyleBackColor = false;
			// 
			// btn열기
			// 
			this.btn열기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn열기.BackColor = System.Drawing.Color.Transparent;
			this.btn열기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn열기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn열기.Location = new System.Drawing.Point(635, 23);
			this.btn열기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn열기.Name = "btn열기";
			this.btn열기.Size = new System.Drawing.Size(70, 19);
			this.btn열기.TabIndex = 3;
			this.btn열기.TabStop = false;
			this.btn열기.Text = "열기";
			this.btn열기.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PR_POP_REG_IMAGE_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(789, 536);
			this.Controls.Add(this.btn열기);
			this.Controls.Add(this.btn미리보기);
			this.Controls.Add(this.splitContainer1);
			this.Name = "P_CZ_PR_POP_REG_IMAGE_SUB";
			this.Text = "ERP iU";
			this.TitleText = "지침서보기";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex작업지침서)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.imagePanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Dass.FlexGrid.FlexGrid _flex작업지침서;
		private Duzon.Common.Controls.WebBrowserExt web작업지침서;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Duzon.Common.Controls.RoundedButton btn미리보기;
		private Duzon.Common.Controls.RoundedButton btn열기;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
	}
}