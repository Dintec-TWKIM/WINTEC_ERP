namespace pur
{
    partial class P_PU_LOT_SUB_R
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_LOT_SUB_R));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_close = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_confirm = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_Seq = new Duzon.Common.Controls.RoundedButton(this.components);
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.roundedButton2 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.roundedButton1 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._btn엑셀 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_출고LOT = new Duzon.Common.Controls.RoundedButton(this.components);
            this.pnl빈공간 = new System.Windows.Forms.Panel();
            this.btn_프로젝트적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.txt_Serial = new Duzon.Common.Controls.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this._flexM);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 38);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(797, 231);
            this.panel4.TabIndex = 131;
            // 
            // _flexM
            // 
            this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM.AutoResize = false;
            this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM.EnabledHeaderCheck = true;
            this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM.Location = new System.Drawing.Point(0, 0);
            this._flexM.Name = "_flexM";
            this._flexM.Rows.Count = 1;
            this._flexM.Rows.DefaultSize = 18;
            this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM.ShowSort = false;
            this._flexM.Size = new System.Drawing.Size(797, 231);
            this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
            this._flexM.TabIndex = 131;
            this._flexM.UseGridCalculator = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this._flexD);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 310);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(797, 266);
            this.panel5.TabIndex = 132;
            // 
            // _flexD
            // 
            this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD.AutoResize = false;
            this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD.EnabledHeaderCheck = true;
            this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD.Location = new System.Drawing.Point(0, 0);
            this._flexD.Name = "_flexD";
            this._flexD.Rows.Count = 1;
            this._flexD.Rows.DefaultSize = 18;
            this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD.ShowSort = false;
            this._flexD.Size = new System.Drawing.Size(797, 266);
            this._flexD.StyleInfo = resources.GetString("_flexD.StyleInfo");
            this._flexD.TabIndex = 132;
            this._flexD.UseGridCalculator = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_close);
            this.flowLayoutPanel1.Controls.Add(this.btn_confirm);
            this.flowLayoutPanel1.Controls.Add(this.btn_Seq);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(797, 29);
            this.flowLayoutPanel1.TabIndex = 133;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.White;
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Location = new System.Drawing.Point(734, 3);
            this.btn_close.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(60, 19);
            this.btn_close.TabIndex = 133;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "종료";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.종료_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.White;
            this.btn_confirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirm.Location = new System.Drawing.Point(668, 3);
            this.btn_confirm.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(60, 19);
            this.btn_confirm.TabIndex = 134;
            this.btn_confirm.TabStop = false;
            this.btn_confirm.Text = "확인";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.확인_Click);
            // 
            // btn_Seq
            // 
            this.btn_Seq.BackColor = System.Drawing.Color.White;
            this.btn_Seq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Seq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Seq.Location = new System.Drawing.Point(592, 3);
            this.btn_Seq.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Seq.Name = "btn_Seq";
            this.btn_Seq.Size = new System.Drawing.Size(70, 19);
            this.btn_Seq.TabIndex = 135;
            this.btn_Seq.TabStop = false;
            this.btn_Seq.Text = "자동채번";
            this.btn_Seq.UseVisualStyleBackColor = false;
            this.btn_Seq.Visible = false;
            this.btn_Seq.Click += new System.EventHandler(this.btn_Seq_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.roundedButton2);
            this.flowLayoutPanel2.Controls.Add(this.roundedButton1);
            this.flowLayoutPanel2.Controls.Add(this._btn엑셀);
            this.flowLayoutPanel2.Controls.Add(this.btn_출고LOT);
            this.flowLayoutPanel2.Controls.Add(this.pnl빈공간);
            this.flowLayoutPanel2.Controls.Add(this.btn_프로젝트적용);
            this.flowLayoutPanel2.Controls.Add(this.txt_Serial);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 275);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(797, 29);
            this.flowLayoutPanel2.TabIndex = 134;
            // 
            // roundedButton2
            // 
            this.roundedButton2.BackColor = System.Drawing.Color.White;
            this.roundedButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton2.Location = new System.Drawing.Point(734, 3);
            this.roundedButton2.MaximumSize = new System.Drawing.Size(0, 19);
            this.roundedButton2.Name = "roundedButton2";
            this.roundedButton2.Size = new System.Drawing.Size(60, 19);
            this.roundedButton2.TabIndex = 133;
            this.roundedButton2.TabStop = false;
            this.roundedButton2.Text = "건별삭제";
            this.roundedButton2.UseVisualStyleBackColor = false;
            this.roundedButton2.Click += new System.EventHandler(this.삭제_Click);
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.White;
            this.roundedButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.Location = new System.Drawing.Point(668, 3);
            this.roundedButton1.MaximumSize = new System.Drawing.Size(0, 19);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(60, 19);
            this.roundedButton1.TabIndex = 134;
            this.roundedButton1.TabStop = false;
            this.roundedButton1.Text = "추가";
            this.roundedButton1.UseVisualStyleBackColor = false;
            this.roundedButton1.Click += new System.EventHandler(this.추가_Click);
            // 
            // _btn엑셀
            // 
            this._btn엑셀.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btn엑셀.BackColor = System.Drawing.Color.White;
            this._btn엑셀.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btn엑셀.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btn엑셀.Location = new System.Drawing.Point(574, 3);
            this._btn엑셀.MaximumSize = new System.Drawing.Size(0, 19);
            this._btn엑셀.Name = "_btn엑셀";
            this._btn엑셀.Size = new System.Drawing.Size(88, 19);
            this._btn엑셀.TabIndex = 135;
            this._btn엑셀.TabStop = false;
            this._btn엑셀.Tag = "엑셀업로드";
            this._btn엑셀.Text = "엑셀업로드";
            this._btn엑셀.UseVisualStyleBackColor = true;
            this._btn엑셀.Click += new System.EventHandler(this._btn엑셀_Click);
            // 
            // btn_출고LOT
            // 
            this.btn_출고LOT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_출고LOT.BackColor = System.Drawing.Color.White;
            this.btn_출고LOT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_출고LOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_출고LOT.Location = new System.Drawing.Point(480, 3);
            this.btn_출고LOT.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_출고LOT.Name = "btn_출고LOT";
            this.btn_출고LOT.Size = new System.Drawing.Size(88, 19);
            this.btn_출고LOT.TabIndex = 136;
            this.btn_출고LOT.TabStop = false;
            this.btn_출고LOT.Tag = "";
            this.btn_출고LOT.Text = "출고LOT내역";
            this.btn_출고LOT.UseVisualStyleBackColor = true;
            this.btn_출고LOT.Click += new System.EventHandler(this.btn_출고LOT_Click);
            // 
            // pnl빈공간
            // 
            this.pnl빈공간.ForeColor = System.Drawing.Color.White;
            this.pnl빈공간.Location = new System.Drawing.Point(276, 3);
            this.pnl빈공간.Name = "pnl빈공간";
            this.pnl빈공간.Size = new System.Drawing.Size(198, 21);
            this.pnl빈공간.TabIndex = 216;
            // 
            // btn_프로젝트적용
            // 
            this.btn_프로젝트적용.BackColor = System.Drawing.Color.White;
            this.btn_프로젝트적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_프로젝트적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_프로젝트적용.Location = new System.Drawing.Point(228, 3);
            this.btn_프로젝트적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_프로젝트적용.Name = "btn_프로젝트적용";
            this.btn_프로젝트적용.Size = new System.Drawing.Size(42, 19);
            this.btn_프로젝트적용.TabIndex = 215;
            this.btn_프로젝트적용.TabStop = false;
            this.btn_프로젝트적용.Text = "적용";
            this.btn_프로젝트적용.UseVisualStyleBackColor = false;
            this.btn_프로젝트적용.Click += new System.EventHandler(this.btn적용_Click);
            // 
            // txt_Serial
            // 
            this.txt_Serial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt_Serial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Serial.Location = new System.Drawing.Point(4, 3);
            this.txt_Serial.MaxLength = 50;
            this.txt_Serial.Name = "txt_Serial";
            this.txt_Serial.SelectedAllEnabled = false;
            this.txt_Serial.Size = new System.Drawing.Size(218, 21);
            this.txt_Serial.TabIndex = 151;
            this.txt_Serial.Tag = "";
            this.txt_Serial.UseKeyEnter = false;
            this.txt_Serial.UseKeyF3 = false;
            // 
            // P_PU_LOT_SUB_R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_close;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(804, 626);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_PU_LOT_SUB_R";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleText = "LOT도움창(입고)";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton roundedButton1;
        private Duzon.Common.Controls.RoundedButton roundedButton2;
        private Duzon.Common.Controls.RoundedButton btn_confirm;
        private Duzon.Common.Controls.RoundedButton btn_close;
        private System.Windows.Forms.Panel panel4;
        private Dass.FlexGrid.FlexGrid _flexM;
        private System.Windows.Forms.Panel panel5;
        private Dass.FlexGrid.FlexGrid _flexD;
        private Duzon.Common.Controls.RoundedButton _btn엑셀;
        private Duzon.Common.Controls.RoundedButton btn_출고LOT;
        private Duzon.Common.Controls.TextBoxExt txt_Serial;
        private Duzon.Common.Controls.RoundedButton btn_프로젝트적용;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel pnl빈공간;
        private Duzon.Common.Controls.RoundedButton btn_Seq;
    }
}
