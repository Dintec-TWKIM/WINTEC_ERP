namespace cz
{
    partial class P_CZ_MA_FILE_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_FILE_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.btn변경사항저장 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn선택다운로드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn선택삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn파일추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt서버파일경로 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl서버파일경로 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.panelExt2.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelExt2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 235F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(606, 351);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.labelExt1);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 284);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(600, 32);
            this.panelExt1.TabIndex = 3;
            // 
            // labelExt1
            // 
            this.labelExt1.AutoSize = true;
            this.labelExt1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelExt1.Location = new System.Drawing.Point(9, 10);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(248, 12);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Text = "※ 개별파일 더블클릭 시 실행시킵니다.";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelExt2
            // 
            this.panelExt2.Controls.Add(this.btn변경사항저장);
            this.panelExt2.Controls.Add(this.btn선택다운로드);
            this.panelExt2.Controls.Add(this.btn닫기);
            this.panelExt2.Controls.Add(this.btn선택삭제);
            this.panelExt2.Controls.Add(this.btn파일추가);
            this.panelExt2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt2.Location = new System.Drawing.Point(3, 322);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(600, 26);
            this.panelExt2.TabIndex = 4;
            // 
            // btn변경사항저장
            // 
            this.btn변경사항저장.BackColor = System.Drawing.Color.Transparent;
            this.btn변경사항저장.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn변경사항저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn변경사항저장.Location = new System.Drawing.Point(312, 3);
            this.btn변경사항저장.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn변경사항저장.Name = "btn변경사항저장";
            this.btn변경사항저장.Size = new System.Drawing.Size(94, 19);
            this.btn변경사항저장.TabIndex = 4;
            this.btn변경사항저장.TabStop = false;
            this.btn변경사항저장.Text = "변경사항저장";
            this.btn변경사항저장.UseVisualStyleBackColor = false;
            // 
            // btn선택다운로드
            // 
            this.btn선택다운로드.BackColor = System.Drawing.Color.Transparent;
            this.btn선택다운로드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn선택다운로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn선택다운로드.Location = new System.Drawing.Point(412, 3);
            this.btn선택다운로드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn선택다운로드.Name = "btn선택다운로드";
            this.btn선택다운로드.Size = new System.Drawing.Size(94, 19);
            this.btn선택다운로드.TabIndex = 3;
            this.btn선택다운로드.TabStop = false;
            this.btn선택다운로드.Text = "선택다운로드";
            this.btn선택다운로드.UseVisualStyleBackColor = false;
            // 
            // btn닫기
            // 
            this.btn닫기.BackColor = System.Drawing.Color.Transparent;
            this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn닫기.Location = new System.Drawing.Point(512, 3);
            this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new System.Drawing.Size(85, 19);
            this.btn닫기.TabIndex = 2;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = false;
            // 
            // btn선택삭제
            // 
            this.btn선택삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn선택삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn선택삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn선택삭제.Location = new System.Drawing.Point(94, 3);
            this.btn선택삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn선택삭제.Name = "btn선택삭제";
            this.btn선택삭제.Size = new System.Drawing.Size(85, 19);
            this.btn선택삭제.TabIndex = 1;
            this.btn선택삭제.TabStop = false;
            this.btn선택삭제.Text = "선택삭제";
            this.btn선택삭제.UseVisualStyleBackColor = false;
            // 
            // btn파일추가
            // 
            this.btn파일추가.BackColor = System.Drawing.Color.Transparent;
            this.btn파일추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn파일추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn파일추가.Location = new System.Drawing.Point(3, 3);
            this.btn파일추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn파일추가.Name = "btn파일추가";
            this.btn파일추가.Size = new System.Drawing.Size(85, 19);
            this.btn파일추가.TabIndex = 0;
            this.btn파일추가.TabStop = false;
            this.btn파일추가.Text = "파일추가";
            this.btn파일추가.UseVisualStyleBackColor = false;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(600, 40);
            this.oneGrid1.TabIndex = 5;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(590, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt서버파일경로);
            this.bpPanelControl1.Controls.Add(this.lbl서버파일경로);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(585, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // txt서버파일경로
            // 
            this.txt서버파일경로.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt서버파일경로.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt서버파일경로.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt서버파일경로.Location = new System.Drawing.Point(106, 0);
            this.txt서버파일경로.Name = "txt서버파일경로";
            this.txt서버파일경로.ReadOnly = true;
            this.txt서버파일경로.Size = new System.Drawing.Size(479, 21);
            this.txt서버파일경로.TabIndex = 1;
            this.txt서버파일경로.TabStop = false;
            // 
            // lbl서버파일경로
            // 
            this.lbl서버파일경로.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl서버파일경로.Location = new System.Drawing.Point(0, 0);
            this.lbl서버파일경로.Name = "lbl서버파일경로";
            this.lbl서버파일경로.Size = new System.Drawing.Size(100, 23);
            this.lbl서버파일경로.TabIndex = 0;
            this.lbl서버파일경로.Text = "서버파일경로";
            this.lbl서버파일경로.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flexInfo
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 49);
            this._flex.Name = "flexInfo";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(600, 229);
            this._flex.StyleInfo = resources.GetString("flexInfo.StyleInfo");
            this._flex.TabIndex = 6;
            this._flex.UseGridCalculator = true;
            // 
            // P_CZ_MA_FILE_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(606, 398);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_MA_FILE_SUB";
            this.Text = "DINTEC ERP iU";
            this.TitleText = "첨부파일 업/다운로드";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.panelExt1.PerformLayout();
            this.panelExt2.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.Controls.RoundedButton btn변경사항저장;
        private Duzon.Common.Controls.RoundedButton btn선택다운로드;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn선택삭제;
        private Duzon.Common.Controls.RoundedButton btn파일추가;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt서버파일경로;
        private Duzon.Common.Controls.LabelExt lbl서버파일경로;
        private Dass.FlexGrid.FlexGrid _flex;
    }
}