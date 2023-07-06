namespace cz
{
    partial class VesslEtryndScheduler
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn운항정보가져오기 = new System.Windows.Forms.Button();
            this.chk로그파일저장 = new System.Windows.Forms.CheckBox();
            this.chk타이머 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.lbl회사코드 = new System.Windows.Forms.Label();
            this.txt회사코드 = new System.Windows.Forms.TextBox();
            this.lbl서버IP = new System.Windows.Forms.Label();
            this.txt서버IP = new System.Windows.Forms.TextBox();
            this.lbl포트번호 = new System.Windows.Forms.Label();
            this.lbl사용자 = new System.Windows.Forms.Label();
            this.txt사용자 = new System.Windows.Forms.TextBox();
            this.txt포트번호 = new System.Windows.Forms.TextBox();
            this.txt패스워드 = new System.Windows.Forms.TextBox();
            this.lblDB = new System.Windows.Forms.Label();
            this.lbl패스워드 = new System.Windows.Forms.Label();
            this.txt로그 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgHeader = new System.Windows.Forms.TabPage();
            this.grdHeader = new System.Windows.Forms.DataGridView();
            this.tpgLine = new System.Windows.Forms.TabPage();
            this.grdLine = new System.Windows.Forms.DataGridView();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHeader)).BeginInit();
            this.tpgLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLine)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt로그, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(638, 506);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn운항정보가져오기);
            this.flowLayoutPanel1.Controls.Add(this.chk로그파일저장);
            this.flowLayoutPanel1.Controls.Add(this.chk타이머);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 89);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(632, 28);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn운항정보가져오기
            // 
            this.btn운항정보가져오기.Location = new System.Drawing.Point(497, 3);
            this.btn운항정보가져오기.Name = "btn운항정보가져오기";
            this.btn운항정보가져오기.Size = new System.Drawing.Size(132, 23);
            this.btn운항정보가져오기.TabIndex = 0;
            this.btn운항정보가져오기.Text = "운항정보가져오기";
            this.btn운항정보가져오기.UseVisualStyleBackColor = true;
            // 
            // chk로그파일저장
            // 
            this.chk로그파일저장.AutoSize = true;
            this.chk로그파일저장.Checked = true;
            this.chk로그파일저장.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk로그파일저장.Location = new System.Drawing.Point(395, 3);
            this.chk로그파일저장.Name = "chk로그파일저장";
            this.chk로그파일저장.Size = new System.Drawing.Size(96, 16);
            this.chk로그파일저장.TabIndex = 1;
            this.chk로그파일저장.Text = "로그파일저장";
            this.chk로그파일저장.UseVisualStyleBackColor = true;
            // 
            // chk타이머
            // 
            this.chk타이머.AutoSize = true;
            this.chk타이머.Checked = true;
            this.chk타이머.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk타이머.Location = new System.Drawing.Point(329, 3);
            this.chk타이머.Name = "chk타이머";
            this.chk타이머.Size = new System.Drawing.Size(60, 16);
            this.chk타이머.TabIndex = 2;
            this.chk타이머.Text = "타이머";
            this.chk타이머.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 187F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 226F));
            this.tableLayoutPanel2.Controls.Add(this.txtDB, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl회사코드, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txt회사코드, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl서버IP, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txt서버IP, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl포트번호, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl사용자, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txt사용자, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txt포트번호, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.txt패스워드, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDB, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl패스워드, 2, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(632, 80);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(409, 3);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(220, 21);
            this.txtDB.TabIndex = 11;
            // 
            // lbl회사코드
            // 
            this.lbl회사코드.AutoSize = true;
            this.lbl회사코드.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl회사코드.Location = new System.Drawing.Point(3, 0);
            this.lbl회사코드.Name = "lbl회사코드";
            this.lbl회사코드.Size = new System.Drawing.Size(94, 26);
            this.lbl회사코드.TabIndex = 0;
            this.lbl회사코드.Text = "회사코드";
            this.lbl회사코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt회사코드
            // 
            this.txt회사코드.Location = new System.Drawing.Point(103, 3);
            this.txt회사코드.Name = "txt회사코드";
            this.txt회사코드.Size = new System.Drawing.Size(181, 21);
            this.txt회사코드.TabIndex = 1;
            // 
            // lbl서버IP
            // 
            this.lbl서버IP.AutoSize = true;
            this.lbl서버IP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl서버IP.Location = new System.Drawing.Point(3, 26);
            this.lbl서버IP.Name = "lbl서버IP";
            this.lbl서버IP.Size = new System.Drawing.Size(94, 27);
            this.lbl서버IP.TabIndex = 2;
            this.lbl서버IP.Text = "서버 IP";
            this.lbl서버IP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt서버IP
            // 
            this.txt서버IP.Location = new System.Drawing.Point(103, 29);
            this.txt서버IP.Name = "txt서버IP";
            this.txt서버IP.Size = new System.Drawing.Size(181, 21);
            this.txt서버IP.TabIndex = 3;
            // 
            // lbl포트번호
            // 
            this.lbl포트번호.AutoSize = true;
            this.lbl포트번호.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl포트번호.Location = new System.Drawing.Point(290, 26);
            this.lbl포트번호.Name = "lbl포트번호";
            this.lbl포트번호.Size = new System.Drawing.Size(113, 27);
            this.lbl포트번호.TabIndex = 6;
            this.lbl포트번호.Text = "포트번호";
            this.lbl포트번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl사용자
            // 
            this.lbl사용자.AutoSize = true;
            this.lbl사용자.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl사용자.Location = new System.Drawing.Point(3, 53);
            this.lbl사용자.Name = "lbl사용자";
            this.lbl사용자.Size = new System.Drawing.Size(94, 27);
            this.lbl사용자.TabIndex = 5;
            this.lbl사용자.Text = "사용자";
            this.lbl사용자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt사용자
            // 
            this.txt사용자.Location = new System.Drawing.Point(103, 56);
            this.txt사용자.Name = "txt사용자";
            this.txt사용자.Size = new System.Drawing.Size(181, 21);
            this.txt사용자.TabIndex = 7;
            // 
            // txt포트번호
            // 
            this.txt포트번호.Location = new System.Drawing.Point(409, 29);
            this.txt포트번호.Name = "txt포트번호";
            this.txt포트번호.Size = new System.Drawing.Size(220, 21);
            this.txt포트번호.TabIndex = 8;
            // 
            // txt패스워드
            // 
            this.txt패스워드.Dock = System.Windows.Forms.DockStyle.Left;
            this.txt패스워드.Location = new System.Drawing.Point(409, 56);
            this.txt패스워드.Name = "txt패스워드";
            this.txt패스워드.Size = new System.Drawing.Size(220, 21);
            this.txt패스워드.TabIndex = 9;
            this.txt패스워드.UseSystemPasswordChar = true;
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDB.Location = new System.Drawing.Point(290, 0);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(113, 26);
            this.lblDB.TabIndex = 10;
            this.lblDB.Text = "DB";
            this.lblDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl패스워드
            // 
            this.lbl패스워드.AutoSize = true;
            this.lbl패스워드.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl패스워드.Location = new System.Drawing.Point(290, 53);
            this.lbl패스워드.Name = "lbl패스워드";
            this.lbl패스워드.Size = new System.Drawing.Size(113, 27);
            this.lbl패스워드.TabIndex = 4;
            this.lbl패스워드.Text = "패스워드";
            this.lbl패스워드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt로그
            // 
            this.txt로그.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt로그.Location = new System.Drawing.Point(3, 379);
            this.txt로그.Multiline = true;
            this.txt로그.Name = "txt로그";
            this.txt로그.ReadOnly = true;
            this.txt로그.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt로그.Size = new System.Drawing.Size(632, 124);
            this.txt로그.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgHeader);
            this.tabControl1.Controls.Add(this.tpgLine);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 123);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 250);
            this.tabControl1.TabIndex = 5;
            // 
            // tpgHeader
            // 
            this.tpgHeader.Controls.Add(this.grdHeader);
            this.tpgHeader.Location = new System.Drawing.Point(4, 22);
            this.tpgHeader.Name = "tpgHeader";
            this.tpgHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tpgHeader.Size = new System.Drawing.Size(624, 224);
            this.tpgHeader.TabIndex = 0;
            this.tpgHeader.Text = "Header";
            this.tpgHeader.UseVisualStyleBackColor = true;
            // 
            // grdHeader
            // 
            this.grdHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHeader.Location = new System.Drawing.Point(3, 3);
            this.grdHeader.Name = "grdHeader";
            this.grdHeader.RowTemplate.Height = 23;
            this.grdHeader.Size = new System.Drawing.Size(618, 218);
            this.grdHeader.TabIndex = 0;
            // 
            // tpgLine
            // 
            this.tpgLine.Controls.Add(this.grdLine);
            this.tpgLine.Location = new System.Drawing.Point(4, 22);
            this.tpgLine.Name = "tpgLine";
            this.tpgLine.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLine.Size = new System.Drawing.Size(624, 224);
            this.tpgLine.TabIndex = 1;
            this.tpgLine.Text = "Line";
            this.tpgLine.UseVisualStyleBackColor = true;
            // 
            // grdLine
            // 
            this.grdLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLine.Location = new System.Drawing.Point(3, 3);
            this.grdLine.Name = "grdLine";
            this.grdLine.RowTemplate.Height = 23;
            this.grdLine.Size = new System.Drawing.Size(618, 218);
            this.grdLine.TabIndex = 0;
            // 
            // VesslEtryndScheduler
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(638, 506);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VesslEtryndScheduler";
            this.Text = "선박운항정보";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpgHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHeader)).EndInit();
            this.tpgLine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn운항정보가져오기;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbl회사코드;
        private System.Windows.Forms.TextBox txt회사코드;
        private System.Windows.Forms.Label lbl서버IP;
        private System.Windows.Forms.TextBox txt서버IP;
        private System.Windows.Forms.Label lbl포트번호;
        private System.Windows.Forms.Label lbl사용자;
        private System.Windows.Forms.Label lbl패스워드;
        private System.Windows.Forms.TextBox txt사용자;
        private System.Windows.Forms.TextBox txt포트번호;
        private System.Windows.Forms.TextBox txt패스워드;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox txt로그;
        private System.Windows.Forms.CheckBox chk로그파일저장;
        private System.Windows.Forms.CheckBox chk타이머;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgHeader;
        private System.Windows.Forms.TabPage tpgLine;
        private System.Windows.Forms.DataGridView grdHeader;
        private System.Windows.Forms.DataGridView grdLine;
    }
}