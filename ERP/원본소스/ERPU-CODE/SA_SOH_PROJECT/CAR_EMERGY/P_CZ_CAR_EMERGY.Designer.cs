namespace cz
{
    partial class P_CZ_CAR_EMERGY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_CAR_EMERGY));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx1 = new Duzon.Common.Controls.PanelEx();
            this.txt성명 = new Duzon.Common.Controls.TextBoxExt();
            this.ctx차량정보 = new Duzon.Common.Controls.TextBoxExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelEx2 = new Duzon.Common.Controls.PanelEx();
            this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.panelEx2.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx1.ColorA = System.Drawing.Color.Empty;
            this.panelEx1.ColorB = System.Drawing.Color.Empty;
            this.panelEx1.Controls.Add(this.txt성명);
            this.panelEx1.Controls.Add(this.ctx차량정보);
            this.panelEx1.Controls.Add(this.labelExt2);
            this.panelEx1.Controls.Add(this.labelExt1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(821, 40);
            this.panelEx1.TabIndex = 0;
            // 
            // txt성명
            // 
            this.txt성명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt성명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt성명.Location = new System.Drawing.Point(452, 11);
            this.txt성명.Name = "txt성명";
            this.txt성명.SelectedAllEnabled = false;
            this.txt성명.Size = new System.Drawing.Size(212, 21);
            this.txt성명.TabIndex = 11;
            this.txt성명.UseKeyEnter = true;
            this.txt성명.UseKeyF3 = true;
            // 
            // ctx차량정보
            // 
            this.ctx차량정보.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx차량정보.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx차량정보.Location = new System.Drawing.Point(89, 11);
            this.ctx차량정보.Name = "ctx차량정보";
            this.ctx차량정보.SelectedAllEnabled = false;
            this.ctx차량정보.Size = new System.Drawing.Size(212, 21);
            this.ctx차량정보.TabIndex = 4;
            this.ctx차량정보.UseKeyEnter = true;
            this.ctx차량정보.UseKeyF3 = true;
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.White;
            this.labelExt2.Location = new System.Drawing.Point(384, 10);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(59, 23);
            this.labelExt2.TabIndex = 2;
            this.labelExt2.Text = "성명";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.White;
            this.labelExt1.Location = new System.Drawing.Point(6, 10);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(85, 23);
            this.labelExt1.TabIndex = 1;
            this.labelExt1.Text = "차량정보";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this._flex.Location = new System.Drawing.Point(3, 77);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 499);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 2;
            this._flex.UseGridCalculator = true;
            // 
            // panelEx2
            // 
            this.panelEx2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx2.ColorA = System.Drawing.Color.Empty;
            this.panelEx2.ColorB = System.Drawing.Color.Empty;
            this.panelEx2.Controls.Add(this.btn추가);
            this.panelEx2.Controls.Add(this.btn삭제);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 49);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(821, 22);
            this.panelEx2.TabIndex = 3;
            // 
            // btn추가
            // 
            this.btn추가.BackColor = System.Drawing.Color.Transparent;
            this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn추가.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn추가.Location = new System.Drawing.Point(679, 0);
            this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn추가.Name = "btn추가";
            this.btn추가.Size = new System.Drawing.Size(70, 19);
            this.btn추가.TabIndex = 3;
            this.btn추가.TabStop = false;
            this.btn추가.Text = "추가";
            this.btn추가.UseVisualStyleBackColor = true;
            // 
            // btn삭제
            // 
            this.btn삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(749, 0);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(70, 19);
            this.btn삭제.TabIndex = 2;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = true;
            // 
            // P_CZ_CAR_EMERGY
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_CAR_EMERGY";
            this.TitleText = "비상연락";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelEx panelEx1;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.PanelEx panelEx2;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Common.Controls.TextBoxExt ctx차량정보;
        private Duzon.Common.Controls.TextBoxExt txt성명;

    }
}