
using C1.Win.C1FlexGrid;
using Duzon.Common.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_FI_CARD_DELETE_KMB_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_FI_CARD_DELETE_KMB_SUB));
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.btnCancle = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnDelete = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.SuspendLayout();
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(12, 82);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(768, 445);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 140;
			this._flex.UseGridCalculator = true;
			// 
			// btnCancle
			// 
			this.btnCancle.BackColor = System.Drawing.Color.White;
			this.btnCancle.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancle.Location = new System.Drawing.Point(710, 56);
			this.btnCancle.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnCancle.Name = "btnCancle";
			this.btnCancle.Size = new System.Drawing.Size(70, 19);
			this.btnCancle.TabIndex = 139;
			this.btnCancle.TabStop = false;
			this.btnCancle.Text = "취소";
			this.btnCancle.UseVisualStyleBackColor = true;
			// 
			// btnDelete
			// 
			this.btnDelete.BackColor = System.Drawing.Color.White;
			this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDelete.Location = new System.Drawing.Point(634, 56);
			this.btnDelete.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(70, 19);
			this.btnDelete.TabIndex = 138;
			this.btnDelete.TabStop = false;
			this.btnDelete.Text = "삭제";
			this.btnDelete.UseVisualStyleBackColor = true;
			// 
			// P_FI_CARD_DELETE_KMB_SUB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(788, 539);
			this.Controls.Add(this._flex);
			this.Controls.Add(this.btnCancle);
			this.Controls.Add(this.btnDelete);
			this.Name = "P_FI_CARD_DELETE_KMB_SUB";
			this.Text = "P_FI_CARD_DELETE_KMB_SUB";
			this.TitleText = "법인카드 삭제";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

		private Dass.FlexGrid.FlexGrid _flex;
		private RoundedButton btnCancle;
		private RoundedButton btnDelete;

		#endregion
	}
}