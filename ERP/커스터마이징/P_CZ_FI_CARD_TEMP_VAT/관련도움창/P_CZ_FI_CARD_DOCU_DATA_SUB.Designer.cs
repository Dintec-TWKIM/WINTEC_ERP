
using C1.Win.C1FlexGrid;
using Duzon.Common.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_FI_CARD_DOCU_DATA_SUB
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(P_CZ_FI_CARD_DOCU_DATA_SUB));
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.btnOk = new RoundedButton(this.components);
            this.btnCopy = new RoundedButton(this.components);
            ((ISupportInitialize)this.closeButton).BeginInit();
            this._flex.BeginInit();
            this.SuspendLayout();
            this._flex.AllowFreezing = AllowFreezingEnum.Both;
            this._flex.AllowResizing = AllowResizingEnum.Both;
            this._flex.AllowSorting = AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.DrawMode = DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = KeyActionEnum.MoveAcross;
            this._flex.Location = new Point(7, 77);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = SelectionModeEnum.Row;
            this._flex.ShowButtons = ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new Size(311, 311);
            this._flex.StyleInfo = componentResourceManager.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 137;
            this._flex.UseGridCalculator = true;
            this.btnOk.BackColor = Color.White;
            this.btnOk.Cursor = Cursors.Hand;
            this.btnOk.DialogResult = DialogResult.Cancel;
            this.btnOk.FlatStyle = FlatStyle.Flat;
            this.btnOk.Location = new Point(249, 52);
            this.btnOk.MaximumSize = new Size(0, 19);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(70, 19);
            this.btnOk.TabIndex = 136;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "닫기";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnCopy.BackColor = Color.White;
            this.btnCopy.Cursor = Cursors.Hand;
            this.btnCopy.FlatStyle = FlatStyle.Flat;
            this.btnCopy.Location = new Point(173, 52);
            this.btnCopy.MaximumSize = new Size(0, 19);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new Size(70, 19);
            this.btnCopy.TabIndex = 135;
            this.btnCopy.TabStop = false;
            this.btnCopy.Text = "적용";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CaptionPaint = true;
            this.ClientSize = new Size(328, 396);
            this.Controls.Add((Control)this._flex);
            this.Controls.Add((Control)this.btnOk);
            this.Controls.Add((Control)this.btnCopy);
            this.Name = "P_CZ_FI_CARD_DOCU_DATA_SUB";
            this.Text = "";
            this.TitleText = "데이터확인";
            ((ISupportInitialize)this.closeButton).EndInit();
            this._flex.EndInit();
            this.ResumeLayout(false);
        }

		private Dass.FlexGrid.FlexGrid _flex;
		private RoundedButton btnOk;
		private RoundedButton btnCopy;

		#endregion
	}
}