namespace cz
{
    partial class P_CZ_INTER_PRICE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_INTER_PRICE));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx3 = new Duzon.Common.Controls.PanelEx();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.dtpTo = new Duzon.Common.Controls.DatePicker();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.dtpFrom = new Duzon.Common.Controls.DatePicker();
            this.btn_추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelEx1 = new Duzon.Common.Controls.PanelEx();
            this.ctx품목정보 = new Duzon.Common.Controls.TextBoxExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._flex_H = new Dass.FlexGrid.FlexGrid(this.components);
            this._flex_L = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex_L)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelEx3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelEx3
            // 
            this.panelEx3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx3.ColorA = System.Drawing.Color.Empty;
            this.panelEx3.ColorB = System.Drawing.Color.Empty;
            this.panelEx3.Controls.Add(this.labelExt3);
            this.panelEx3.Controls.Add(this.dtpTo);
            this.panelEx3.Controls.Add(this.labelExt2);
            this.panelEx3.Controls.Add(this.dtpFrom);
            this.panelEx3.Controls.Add(this.btn_추가);
            this.panelEx3.Controls.Add(this.btn_삭제);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(3, 43);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(821, 26);
            this.panelEx3.TabIndex = 4;
            // 
            // labelExt3
            // 
            this.labelExt3.BackColor = System.Drawing.Color.White;
            this.labelExt3.Location = new System.Drawing.Point(207, 0);
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Resizeble = true;
            this.labelExt3.Size = new System.Drawing.Size(24, 23);
            this.labelExt3.TabIndex = 6;
            this.labelExt3.Text = "~";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarBackColor = System.Drawing.Color.White;
            this.dtpTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpTo.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpTo.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtpTo.Location = new System.Drawing.Point(241, 2);
            this.dtpTo.Mask = "####/##/##";
            this.dtpTo.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtpTo.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpTo.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpTo.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpTo.Modified = true;
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.NullCheck = false;
            this.dtpTo.PaddingCharacter = '_';
            this.dtpTo.PassivePromptCharacter = '_';
            this.dtpTo.PromptCharacter = '_';
            this.dtpTo.SelectedDayColor = System.Drawing.Color.White;
            this.dtpTo.ShowToDay = true;
            this.dtpTo.ShowTodayCircle = true;
            this.dtpTo.ShowUpDown = false;
            this.dtpTo.Size = new System.Drawing.Size(126, 21);
            this.dtpTo.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtpTo.TabIndex = 5;
            this.dtpTo.TitleBackColor = System.Drawing.Color.White;
            this.dtpTo.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpTo.ToDayColor = System.Drawing.Color.Red;
            this.dtpTo.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtpTo.UseKeyF3 = false;
            this.dtpTo.Value = new System.DateTime(((long)(0)));
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.White;
            this.labelExt2.Location = new System.Drawing.Point(3, 0);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(72, 23);
            this.labelExt2.TabIndex = 4;
            this.labelExt2.Text = "상세일시";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarBackColor = System.Drawing.Color.White;
            this.dtpFrom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFrom.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpFrom.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtpFrom.Location = new System.Drawing.Point(80, 2);
            this.dtpFrom.Mask = "####/##/##";
            this.dtpFrom.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtpFrom.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpFrom.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpFrom.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpFrom.Modified = true;
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.NullCheck = false;
            this.dtpFrom.PaddingCharacter = '_';
            this.dtpFrom.PassivePromptCharacter = '_';
            this.dtpFrom.PromptCharacter = '_';
            this.dtpFrom.SelectedDayColor = System.Drawing.Color.White;
            this.dtpFrom.ShowToDay = true;
            this.dtpFrom.ShowTodayCircle = true;
            this.dtpFrom.ShowUpDown = false;
            this.dtpFrom.Size = new System.Drawing.Size(117, 21);
            this.dtpFrom.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtpFrom.TabIndex = 3;
            this.dtpFrom.TitleBackColor = System.Drawing.Color.White;
            this.dtpFrom.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpFrom.ToDayColor = System.Drawing.Color.Red;
            this.dtpFrom.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtpFrom.UseKeyF3 = false;
            this.dtpFrom.Value = new System.DateTime(((long)(0)));
            // 
            // btn_추가
            // 
            this.btn_추가.BackColor = System.Drawing.Color.Transparent;
            this.btn_추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_추가.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_추가.Location = new System.Drawing.Point(679, 0);
            this.btn_추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_추가.Name = "btn_추가";
            this.btn_추가.Size = new System.Drawing.Size(70, 19);
            this.btn_추가.TabIndex = 0;
            this.btn_추가.TabStop = false;
            this.btn_추가.Text = "추가";
            this.btn_추가.UseVisualStyleBackColor = true;
            // 
            // btn_삭제
            // 
            this.btn_삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn_삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_삭제.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_삭제.Location = new System.Drawing.Point(749, 0);
            this.btn_삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_삭제.Name = "btn_삭제";
            this.btn_삭제.Size = new System.Drawing.Size(70, 19);
            this.btn_삭제.TabIndex = 2;
            this.btn_삭제.TabStop = false;
            this.btn_삭제.Text = "삭제";
            this.btn_삭제.UseVisualStyleBackColor = true;
            // 
            // panelEx1
            // 
            this.panelEx1.AutoScroll = true;
            this.panelEx1.AutoSize = true;
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx1.ColorA = System.Drawing.Color.Empty;
            this.panelEx1.ColorB = System.Drawing.Color.Empty;
            this.panelEx1.Controls.Add(this.ctx품목정보);
            this.panelEx1.Controls.Add(this.labelExt1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(821, 34);
            this.panelEx1.TabIndex = 1;
            // 
            // ctx품목정보
            // 
            this.ctx품목정보.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx품목정보.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx품목정보.Location = new System.Drawing.Point(80, 8);
            this.ctx품목정보.Name = "ctx품목정보";
            this.ctx품목정보.SelectedAllEnabled = false;
            this.ctx품목정보.Size = new System.Drawing.Size(429, 21);
            this.ctx품목정보.TabIndex = 4;
            this.ctx품목정보.UseKeyEnter = true;
            this.ctx품목정보.UseKeyF3 = true;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.White;
            this.labelExt1.Location = new System.Drawing.Point(-1, 8);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(72, 23);
            this.labelExt1.TabIndex = 1;
            this.labelExt1.Text = "분류명";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 75);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._flex_H);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._flex_L);
            this.splitContainer2.Size = new System.Drawing.Size(821, 501);
            this.splitContainer2.SplitterDistance = 225;
            this.splitContainer2.TabIndex = 3;
            // 
            // _flex_H
            // 
            this._flex_H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex_H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex_H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex_H.AutoResize = false;
            this._flex_H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex_H.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex_H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex_H.EnabledHeaderCheck = true;
            this._flex_H.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Heavy;
            this._flex_H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex_H.Location = new System.Drawing.Point(0, 0);
            this._flex_H.Name = "_flex_H";
            this._flex_H.Rows.Count = 1;
            this._flex_H.Rows.DefaultSize = 20;
            this._flex_H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex_H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex_H.ShowSort = false;
            this._flex_H.Size = new System.Drawing.Size(225, 501);
            this._flex_H.StyleInfo = resources.GetString("_flex_H.StyleInfo");
            this._flex_H.TabIndex = 0;
            this._flex_H.UseGridCalculator = true;
            // 
            // _flex_L
            // 
            this._flex_L.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex_L.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex_L.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex_L.AutoResize = false;
            this._flex_L.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this._flex_L.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex_L.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex_L.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex_L.EnabledHeaderCheck = true;
            this._flex_L.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Heavy;
            this._flex_L.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex_L.Location = new System.Drawing.Point(0, 0);
            this._flex_L.Name = "_flex_L";
            this._flex_L.Rows.Count = 1;
            this._flex_L.Rows.DefaultSize = 20;
            this._flex_L.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex_L.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex_L.ShowSort = false;
            this._flex_L.Size = new System.Drawing.Size(592, 501);
            this._flex_L.StyleInfo = resources.GetString("_flex_L.StyleInfo");
            this._flex_L.TabIndex = 0;
            this._flex_L.UseGridCalculator = true;
            // 
            // P_CZ_INTER_PRICE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "P_CZ_INTER_PRICE";
            this.TitleText = " 국제시세관리";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex_L)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelEx panelEx1;
        private Duzon.Common.Controls.TextBoxExt ctx품목정보;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Dass.FlexGrid.FlexGrid _flex_H;
        private Dass.FlexGrid.FlexGrid _flex_L;
        private Duzon.Common.Controls.PanelEx panelEx3;
        private Duzon.Common.Controls.RoundedButton btn_추가;
        private Duzon.Common.Controls.RoundedButton btn_삭제;
        private Duzon.Common.Controls.LabelExt labelExt3;
        private Duzon.Common.Controls.DatePicker dtpTo;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.DatePicker dtpFrom;
    }
}

