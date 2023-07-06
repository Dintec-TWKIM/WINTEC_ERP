namespace cz
{
	partial class H_CZ_PU_ITEM
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_PU_ITEM));
			this.tlayM = new System.Windows.Forms.TableLayoutPanel();
			this.flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tlayM.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexL)).BeginInit();
			this.SuspendLayout();
			// 
			// tlayM
			// 
			this.tlayM.ColumnCount = 1;
			this.tlayM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Controls.Add(this.flexL, 0, 1);
			this.tlayM.Controls.Add(this.oneGrid1, 0, 0);
			this.tlayM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayM.Location = new System.Drawing.Point(3, 50);
			this.tlayM.Name = "tlayM";
			this.tlayM.RowCount = 2;
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Size = new System.Drawing.Size(1173, 581);
			this.tlayM.TabIndex = 0;
			// 
			// flexL
			// 
			this.flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexL.AutoResize = false;
			this.flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexL.EnabledHeaderCheck = true;
			this.flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexL.Location = new System.Drawing.Point(3, 75);
			this.flexL.Name = "flexL";
			this.flexL.Rows.Count = 1;
			this.flexL.Rows.DefaultSize = 18;
			this.flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexL.ShowSort = false;
			this.flexL.Size = new System.Drawing.Size(1167, 503);
			this.flexL.StyleInfo = resources.GetString("flexL.StyleInfo");
			this.flexL.TabIndex = 1;
			this.flexL.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1167, 66);
			this.oneGrid1.TabIndex = 0;
			// 
			// btn확인
			// 
			this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn확인.BackColor = System.Drawing.Color.White;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(1103, 24);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(70, 19);
			this.btn확인.TabIndex = 8;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.White;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(1027, 24);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 7;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// H_CZ_PU_ITEM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(1179, 634);
			this.Controls.Add(this.btn확인);
			this.Controls.Add(this.btn조회);
			this.Controls.Add(this.tlayM);
			this.Name = "H_CZ_PU_ITEM";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_PU_ITEM";
			this.TitleText = "발주등록";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tlayM.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexL)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayM;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Dass.FlexGrid.FlexGrid flexL;
		private Duzon.Common.Controls.RoundedButton btn확인;
		private Duzon.Common.Controls.RoundedButton btn조회;
	}
}