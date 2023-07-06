namespace cz
{
    partial class P_CZ_CAR_EMERGY_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_CAR_EMERGY_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelEx1 = new Duzon.Common.Controls.PanelEx();
            this.ctx차량명 = new Duzon.Common.Controls.TextBoxExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.ctx차량정보 = new Duzon.Common.Controls.TextBoxExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(666, 516);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this._flex.Location = new System.Drawing.Point(3, 74);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(660, 439);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            this._flex.UseGridCalculator = true;
            // 
            // panelEx1
            // 
            this.panelEx1.ColorA = System.Drawing.Color.Empty;
            this.panelEx1.ColorB = System.Drawing.Color.Empty;
            this.panelEx1.Controls.Add(this.ctx차량명);
            this.panelEx1.Controls.Add(this.labelExt2);
            this.panelEx1.Controls.Add(this.ctx차량정보);
            this.panelEx1.Controls.Add(this.labelExt1);
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(660, 35);
            this.panelEx1.TabIndex = 1;
            // 
            // ctx차량명
            // 
            this.ctx차량명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx차량명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx차량명.Location = new System.Drawing.Point(419, 6);
            this.ctx차량명.Name = "ctx차량명";
            this.ctx차량명.SelectedAllEnabled = false;
            this.ctx차량명.Size = new System.Drawing.Size(170, 21);
            this.ctx차량명.TabIndex = 7;
            this.ctx차량명.UseKeyEnter = true;
            this.ctx차량명.UseKeyF3 = true;
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelExt2.Location = new System.Drawing.Point(302, 5);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(100, 23);
            this.labelExt2.TabIndex = 6;
            this.labelExt2.Text = "차량명";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctx차량정보
            // 
            this.ctx차량정보.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx차량정보.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx차량정보.Location = new System.Drawing.Point(113, 5);
            this.ctx차량정보.Name = "ctx차량정보";
            this.ctx차량정보.SelectedAllEnabled = false;
            this.ctx차량정보.Size = new System.Drawing.Size(170, 21);
            this.ctx차량정보.TabIndex = 5;
            this.ctx차량정보.UseKeyEnter = true;
            this.ctx차량정보.UseKeyF3 = true;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelExt1.Location = new System.Drawing.Point(0, 4);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(100, 23);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Text = "차량정보";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn조회);
            this.panelExt1.Controls.Add(this.btn적용);
            this.panelExt1.Location = new System.Drawing.Point(3, 44);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(660, 24);
            this.panelExt1.TabIndex = 2;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(520, 0);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 1;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = true;
            // 
            // btn적용
            // 
            this.btn적용.BackColor = System.Drawing.Color.Transparent;
            this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn적용.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn적용.Location = new System.Drawing.Point(590, 0);
            this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn적용.Name = "btn적용";
            this.btn적용.Size = new System.Drawing.Size(70, 19);
            this.btn적용.TabIndex = 0;
            this.btn적용.TabStop = false;
            this.btn적용.Text = "적용";
            this.btn적용.UseVisualStyleBackColor = true;
            // 
            // P_CZ_CAR_EMERGY_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_CAR_EMERGY_SUB";
            this.Text = "차량정보";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelExt1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.PanelEx panelEx1;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.TextBoxExt ctx차량정보;
        private Duzon.Common.Controls.TextBoxExt ctx차량명;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.RoundedButton btn적용;
    }
}