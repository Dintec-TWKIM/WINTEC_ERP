namespace cs
{
    partial class CurrencyScheduler
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
            this.grd환율정보 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn환율정보가져오기 = new System.Windows.Forms.Button();
            this.chk자동종료 = new System.Windows.Forms.CheckBox();
            this.chk파일저장 = new System.Windows.Forms.CheckBox();
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
            this.lbl회차 = new System.Windows.Forms.Label();
            this.txt회차 = new System.Windows.Forms.TextBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.txt로그 = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.grd환율정보)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grd환율정보
            // 
            this.grd환율정보.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd환율정보.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd환율정보.Location = new System.Drawing.Point(3, 153);
            this.grd환율정보.Name = "grd환율정보";
            this.grd환율정보.RowTemplate.Height = 23;
            this.grd환율정보.Size = new System.Drawing.Size(632, 220);
            this.grd환율정보.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grd환율정보, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt로그, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 226F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(638, 506);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn환율정보가져오기);
            this.flowLayoutPanel1.Controls.Add(this.chk자동종료);
            this.flowLayoutPanel1.Controls.Add(this.chk파일저장);
            this.flowLayoutPanel1.Controls.Add(this.chk타이머);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 117);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(632, 30);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn환율정보가져오기
            // 
            this.btn환율정보가져오기.Location = new System.Drawing.Point(497, 3);
            this.btn환율정보가져오기.Name = "btn환율정보가져오기";
            this.btn환율정보가져오기.Size = new System.Drawing.Size(132, 23);
            this.btn환율정보가져오기.TabIndex = 0;
            this.btn환율정보가져오기.Text = "환율정보가져오기";
            this.btn환율정보가져오기.UseVisualStyleBackColor = true;
            // 
            // chk자동종료
            // 
            this.chk자동종료.AutoSize = true;
            this.chk자동종료.Checked = true;
            this.chk자동종료.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk자동종료.Location = new System.Drawing.Point(419, 3);
            this.chk자동종료.Name = "chk자동종료";
            this.chk자동종료.Size = new System.Drawing.Size(72, 16);
            this.chk자동종료.TabIndex = 3;
            this.chk자동종료.Text = "자동종료";
            this.chk자동종료.UseVisualStyleBackColor = true;
            // 
            // chk파일저장
            // 
            this.chk파일저장.AutoSize = true;
            this.chk파일저장.Checked = true;
            this.chk파일저장.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk파일저장.Location = new System.Drawing.Point(341, 3);
            this.chk파일저장.Name = "chk파일저장";
            this.chk파일저장.Size = new System.Drawing.Size(72, 16);
            this.chk파일저장.TabIndex = 1;
            this.chk파일저장.Text = "파일저장";
            this.chk파일저장.UseVisualStyleBackColor = true;
            // 
            // chk타이머
            // 
            this.chk타이머.AutoSize = true;
            this.chk타이머.Checked = true;
            this.chk타이머.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk타이머.Location = new System.Drawing.Point(275, 3);
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
            this.tableLayoutPanel2.Controls.Add(this.lbl회차, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txt회차, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblDebug, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnFileOpen, 3, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(632, 108);
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
            this.lbl사용자.Size = new System.Drawing.Size(94, 26);
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
            this.lbl패스워드.Size = new System.Drawing.Size(113, 26);
            this.lbl패스워드.TabIndex = 4;
            this.lbl패스워드.Text = "패스워드";
            this.lbl패스워드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl회차
            // 
            this.lbl회차.AutoSize = true;
            this.lbl회차.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl회차.Location = new System.Drawing.Point(3, 79);
            this.lbl회차.Name = "lbl회차";
            this.lbl회차.Size = new System.Drawing.Size(94, 29);
            this.lbl회차.TabIndex = 12;
            this.lbl회차.Text = "회차";
            this.lbl회차.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt회차
            // 
            this.txt회차.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt회차.Location = new System.Drawing.Point(103, 82);
            this.txt회차.Name = "txt회차";
            this.txt회차.Size = new System.Drawing.Size(181, 21);
            this.txt회차.TabIndex = 13;
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDebug.Location = new System.Drawing.Point(290, 79);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(113, 29);
            this.lblDebug.TabIndex = 14;
            this.lblDebug.Text = "Debug";
            this.lblDebug.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Location = new System.Drawing.Point(409, 82);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(75, 23);
            this.btnFileOpen.TabIndex = 15;
            this.btnFileOpen.Text = "파일열기";
            this.btnFileOpen.UseVisualStyleBackColor = true;
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
            // CurrencyScheduler
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(638, 506);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CurrencyScheduler";
            this.Text = "환율정보";
            ((System.ComponentModel.ISupportInitialize)(this.grd환율정보)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grd환율정보;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn환율정보가져오기;
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
        private System.Windows.Forms.CheckBox chk파일저장;
        private System.Windows.Forms.CheckBox chk타이머;
        private System.Windows.Forms.CheckBox chk자동종료;
        private System.Windows.Forms.Label lbl회차;
        private System.Windows.Forms.TextBox txt회차;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.Button btnFileOpen;
    }
}