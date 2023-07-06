
namespace DX
{
	partial class WARNING
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WARNING));
			this.spc메인 = new System.Windows.Forms.SplitContainer();
			this.grd경고 = new Dass.FlexGrid.FlexGrid(this.components);
			this.spc라인 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lay라인 = new System.Windows.Forms.TableLayoutPanel();
			this.grd견적 = new Dass.FlexGrid.FlexGrid(this.components);
			this.web경고 = new System.Windows.Forms.WebBrowser();
			this.panel2 = new System.Windows.Forms.Panel();
			this.web견적 = new System.Windows.Forms.WebBrowser();
			this.btn확인 = new System.Windows.Forms.Button();
			this.btn닫기 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).BeginInit();
			this.spc메인.Panel1.SuspendLayout();
			this.spc메인.Panel2.SuspendLayout();
			this.spc메인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd경고)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spc라인)).BeginInit();
			this.spc라인.Panel1.SuspendLayout();
			this.spc라인.Panel2.SuspendLayout();
			this.spc라인.SuspendLayout();
			this.panel1.SuspendLayout();
			this.lay라인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd견적)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// spc메인
			// 
			this.spc메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc메인.Location = new System.Drawing.Point(6, 53);
			this.spc메인.Name = "spc메인";
			this.spc메인.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spc메인.Panel1
			// 
			this.spc메인.Panel1.Controls.Add(this.grd경고);
			// 
			// spc메인.Panel2
			// 
			this.spc메인.Panel2.Controls.Add(this.spc라인);
			this.spc메인.Size = new System.Drawing.Size(1482, 910);
			this.spc메인.SplitterDistance = 246;
			this.spc메인.TabIndex = 0;
			// 
			// grd경고
			// 
			this.grd경고.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd경고.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd경고.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd경고.AutoResize = false;
			this.grd경고.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd경고.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd경고.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd경고.EnabledHeaderCheck = true;
			this.grd경고.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd경고.Location = new System.Drawing.Point(0, 0);
			this.grd경고.Name = "grd경고";
			this.grd경고.Rows.Count = 1;
			this.grd경고.Rows.DefaultSize = 18;
			this.grd경고.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd경고.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd경고.ShowSort = false;
			this.grd경고.Size = new System.Drawing.Size(1482, 246);
			this.grd경고.StyleInfo = resources.GetString("grd경고.StyleInfo");
			this.grd경고.TabIndex = 13;
			this.grd경고.UseGridCalculator = true;
			// 
			// spc라인
			// 
			this.spc라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc라인.Location = new System.Drawing.Point(0, 0);
			this.spc라인.Name = "spc라인";
			// 
			// spc라인.Panel1
			// 
			this.spc라인.Panel1.Controls.Add(this.panel1);
			// 
			// spc라인.Panel2
			// 
			this.spc라인.Panel2.Controls.Add(this.panel2);
			this.spc라인.Size = new System.Drawing.Size(1482, 660);
			this.spc라인.SplitterDistance = 954;
			this.spc라인.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lay라인);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(954, 660);
			this.panel1.TabIndex = 0;
			// 
			// lay라인
			// 
			this.lay라인.ColumnCount = 1;
			this.lay라인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay라인.Controls.Add(this.grd견적, 0, 1);
			this.lay라인.Controls.Add(this.web경고, 0, 0);
			this.lay라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay라인.Location = new System.Drawing.Point(0, 0);
			this.lay라인.Margin = new System.Windows.Forms.Padding(0);
			this.lay라인.Name = "lay라인";
			this.lay라인.RowCount = 2;
			this.lay라인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay라인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay라인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.lay라인.Size = new System.Drawing.Size(952, 658);
			this.lay라인.TabIndex = 0;
			// 
			// grd견적
			// 
			this.grd견적.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd견적.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd견적.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd견적.AutoResize = false;
			this.grd견적.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			this.grd견적.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd견적.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd견적.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd견적.EnabledHeaderCheck = true;
			this.grd견적.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd견적.Location = new System.Drawing.Point(0, 429);
			this.grd견적.Margin = new System.Windows.Forms.Padding(0);
			this.grd견적.Name = "grd견적";
			this.grd견적.Rows.Count = 1;
			this.grd견적.Rows.DefaultSize = 18;
			this.grd견적.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd견적.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd견적.ShowSort = false;
			this.grd견적.Size = new System.Drawing.Size(952, 229);
			this.grd견적.StyleInfo = resources.GetString("grd견적.StyleInfo");
			this.grd견적.TabIndex = 14;
			this.grd견적.UseGridCalculator = true;
			// 
			// web경고
			// 
			this.web경고.Dock = System.Windows.Forms.DockStyle.Top;
			this.web경고.Location = new System.Drawing.Point(0, 0);
			this.web경고.Margin = new System.Windows.Forms.Padding(0);
			this.web경고.MinimumSize = new System.Drawing.Size(20, 20);
			this.web경고.Name = "web경고";
			this.web경고.ScrollBarsEnabled = false;
			this.web경고.Size = new System.Drawing.Size(952, 429);
			this.web경고.TabIndex = 3;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.web견적);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(524, 660);
			this.panel2.TabIndex = 0;
			// 
			// web견적
			// 
			this.web견적.Dock = System.Windows.Forms.DockStyle.Fill;
			this.web견적.Location = new System.Drawing.Point(0, 0);
			this.web견적.Margin = new System.Windows.Forms.Padding(0);
			this.web견적.MinimumSize = new System.Drawing.Size(20, 20);
			this.web견적.Name = "web견적";
			this.web견적.ScrollBarsEnabled = false;
			this.web견적.Size = new System.Drawing.Size(522, 658);
			this.web견적.TabIndex = 4;
			// 
			// btn확인
			// 
			this.btn확인.Location = new System.Drawing.Point(1322, 8);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(80, 33);
			this.btn확인.TabIndex = 1;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = true;
			// 
			// btn닫기
			// 
			this.btn닫기.Location = new System.Drawing.Point(1408, 8);
			this.btn닫기.Name = "btn닫기";
			this.btn닫기.Size = new System.Drawing.Size(80, 33);
			this.btn닫기.TabIndex = 2;
			this.btn닫기.Text = "닫기";
			this.btn닫기.UseVisualStyleBackColor = true;
			// 
			// WARNING
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(1494, 969);
			this.Controls.Add(this.btn닫기);
			this.Controls.Add(this.btn확인);
			this.Controls.Add(this.spc메인);
			this.Name = "WARNING";
			this.Padding = new System.Windows.Forms.Padding(6, 53, 6, 6);
			this.Text = "WARNING";
			this.TitleText = "경고마스터";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.spc메인.Panel1.ResumeLayout(false);
			this.spc메인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).EndInit();
			this.spc메인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd경고)).EndInit();
			this.spc라인.Panel1.ResumeLayout(false);
			this.spc라인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc라인)).EndInit();
			this.spc라인.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.lay라인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd견적)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spc메인;
		private Dass.FlexGrid.FlexGrid grd경고;
		private System.Windows.Forms.SplitContainer spc라인;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel lay라인;
		private Dass.FlexGrid.FlexGrid grd견적;
		private System.Windows.Forms.WebBrowser web경고;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.WebBrowser web견적;
		private System.Windows.Forms.Button btn확인;
		private System.Windows.Forms.Button btn닫기;
	}
}