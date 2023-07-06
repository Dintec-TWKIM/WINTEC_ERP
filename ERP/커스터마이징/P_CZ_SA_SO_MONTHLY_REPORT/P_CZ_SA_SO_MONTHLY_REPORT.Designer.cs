
namespace cz
{
	partial class P_CZ_SA_SO_MONTHLY_REPORT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_SO_MONTHLY_REPORT));
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn다운로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this._flex);
			this.mDataArea.Size = new System.Drawing.Size(977, 606);
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(0, 0);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(977, 606);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// btn다운로드
			// 
			this.btn다운로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn다운로드.BackColor = System.Drawing.Color.Transparent;
			this.btn다운로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn다운로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn다운로드.Location = new System.Drawing.Point(893, 10);
			this.btn다운로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn다운로드.Name = "btn다운로드";
			this.btn다운로드.Size = new System.Drawing.Size(78, 19);
			this.btn다운로드.TabIndex = 3;
			this.btn다운로드.TabStop = false;
			this.btn다운로드.Text = "다운로드";
			this.btn다운로드.UseVisualStyleBackColor = false;
			// 
			// P_CZ_SA_SO_MONTHLY_REPORT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn다운로드);
			this.Name = "P_CZ_SA_SO_MONTHLY_REPORT";
			this.Size = new System.Drawing.Size(977, 646);
			this.TitleText = "P_CZ_SA_SO_MONTHLY_REPORT";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn다운로드, 0);
			this.mDataArea.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Dass.FlexGrid.FlexGrid _flex;
		private Duzon.Common.Controls.RoundedButton btn다운로드;
	}
}