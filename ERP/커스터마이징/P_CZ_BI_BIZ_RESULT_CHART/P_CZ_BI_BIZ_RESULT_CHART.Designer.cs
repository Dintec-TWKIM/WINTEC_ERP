namespace cz
{
    partial class P_CZ_BI_BIZ_RESULT_CHART
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpg월별수주실적 = new System.Windows.Forms.TabPage();
            this.chart월별수주실적 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.tpg분기별수주실적 = new System.Windows.Forms.TabPage();
            this.chart분기별수주실적 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.tpg분기별매출금액 = new System.Windows.Forms.TabPage();
            this.chart분기별매출금액 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.tpg분기별세전이익 = new System.Windows.Forms.TabPage();
            this.chart분기별세전이익 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.tpg분기별수주매출세전이익 = new System.Windows.Forms.TabPage();
            this.chart통합 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo화폐단위 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl화폐단위 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp기준일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl기준일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo회사 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl회사 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.chk상세보기 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk목표보기 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk전년동월보기 = new Duzon.Common.Controls.CheckBoxExt();
            this.lbl수주실적옵션 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl전체옵션 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chk타이머사용 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk데이터그리드표시 = new Duzon.Common.Controls.CheckBoxExt();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btn손익계산서갱신 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpg월별수주실적.SuspendLayout();
            this.tpg분기별수주실적.SuspendLayout();
            this.tpg분기별매출금액.SuspendLayout();
            this.tpg분기별세전이익.SuspendLayout();
            this.tpg분기별수주매출세전이익.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(1090, 756);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpg월별수주실적);
            this.tabControl1.Controls.Add(this.tpg분기별수주실적);
            this.tabControl1.Controls.Add(this.tpg분기별매출금액);
            this.tabControl1.Controls.Add(this.tpg분기별세전이익);
            this.tabControl1.Controls.Add(this.tpg분기별수주매출세전이익);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 72);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1084, 681);
            this.tabControl1.TabIndex = 0;
            // 
            // tpg월별수주실적
            // 
            this.tpg월별수주실적.Controls.Add(this.chart월별수주실적);
            this.tpg월별수주실적.Location = new System.Drawing.Point(4, 22);
            this.tpg월별수주실적.Name = "tpg월별수주실적";
            this.tpg월별수주실적.Padding = new System.Windows.Forms.Padding(3);
            this.tpg월별수주실적.Size = new System.Drawing.Size(1076, 655);
            this.tpg월별수주실적.TabIndex = 0;
            this.tpg월별수주실적.Text = "월별수주실적";
            this.tpg월별수주실적.UseVisualStyleBackColor = true;
            // 
            // chart월별수주실적
            // 
            this.chart월별수주실적.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart월별수주실적.Location = new System.Drawing.Point(3, 3);
            this.chart월별수주실적.Name = "chart월별수주실적";
            this.chart월별수주실적.Size = new System.Drawing.Size(1070, 649);
            this.chart월별수주실적.TabIndex = 0;
            this.chart월별수주실적.Text = "uChart1";
            // 
            // tpg분기별수주실적
            // 
            this.tpg분기별수주실적.Controls.Add(this.chart분기별수주실적);
            this.tpg분기별수주실적.Location = new System.Drawing.Point(4, 22);
            this.tpg분기별수주실적.Name = "tpg분기별수주실적";
            this.tpg분기별수주실적.Size = new System.Drawing.Size(1076, 628);
            this.tpg분기별수주실적.TabIndex = 4;
            this.tpg분기별수주실적.Text = "분기별수주실적";
            this.tpg분기별수주실적.UseVisualStyleBackColor = true;
            // 
            // chart분기별수주실적
            // 
            this.chart분기별수주실적.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart분기별수주실적.Location = new System.Drawing.Point(0, 0);
            this.chart분기별수주실적.Name = "chart분기별수주실적";
            this.chart분기별수주실적.Size = new System.Drawing.Size(1076, 628);
            this.chart분기별수주실적.TabIndex = 0;
            this.chart분기별수주실적.Text = "uChart1";
            // 
            // tpg분기별매출금액
            // 
            this.tpg분기별매출금액.Controls.Add(this.chart분기별매출금액);
            this.tpg분기별매출금액.Location = new System.Drawing.Point(4, 22);
            this.tpg분기별매출금액.Name = "tpg분기별매출금액";
            this.tpg분기별매출금액.Padding = new System.Windows.Forms.Padding(3);
            this.tpg분기별매출금액.Size = new System.Drawing.Size(1076, 628);
            this.tpg분기별매출금액.TabIndex = 1;
            this.tpg분기별매출금액.Text = "분기별매출금액";
            this.tpg분기별매출금액.UseVisualStyleBackColor = true;
            // 
            // chart분기별매출금액
            // 
            this.chart분기별매출금액.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart분기별매출금액.Location = new System.Drawing.Point(3, 3);
            this.chart분기별매출금액.Name = "chart분기별매출금액";
            this.chart분기별매출금액.Size = new System.Drawing.Size(1070, 622);
            this.chart분기별매출금액.TabIndex = 0;
            this.chart분기별매출금액.Text = "uChart1";
            // 
            // tpg분기별세전이익
            // 
            this.tpg분기별세전이익.Controls.Add(this.chart분기별세전이익);
            this.tpg분기별세전이익.Location = new System.Drawing.Point(4, 22);
            this.tpg분기별세전이익.Name = "tpg분기별세전이익";
            this.tpg분기별세전이익.Size = new System.Drawing.Size(1076, 628);
            this.tpg분기별세전이익.TabIndex = 2;
            this.tpg분기별세전이익.Text = "분기별세전이익";
            this.tpg분기별세전이익.UseVisualStyleBackColor = true;
            // 
            // chart분기별세전이익
            // 
            this.chart분기별세전이익.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart분기별세전이익.Location = new System.Drawing.Point(0, 0);
            this.chart분기별세전이익.Name = "chart분기별세전이익";
            this.chart분기별세전이익.Size = new System.Drawing.Size(1076, 628);
            this.chart분기별세전이익.TabIndex = 0;
            this.chart분기별세전이익.Text = "uChart1";
            // 
            // tpg분기별수주매출세전이익
            // 
            this.tpg분기별수주매출세전이익.Controls.Add(this.chart통합);
            this.tpg분기별수주매출세전이익.Location = new System.Drawing.Point(4, 22);
            this.tpg분기별수주매출세전이익.Name = "tpg분기별수주매출세전이익";
            this.tpg분기별수주매출세전이익.Size = new System.Drawing.Size(1076, 628);
            this.tpg분기별수주매출세전이익.TabIndex = 3;
            this.tpg분기별수주매출세전이익.Text = "분기별수주매출세전이익";
            this.tpg분기별수주매출세전이익.UseVisualStyleBackColor = true;
            // 
            // chart통합
            // 
            this.chart통합.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart통합.Location = new System.Drawing.Point(0, 0);
            this.chart통합.Name = "chart통합";
            this.chart통합.Size = new System.Drawing.Size(1076, 628);
            this.chart통합.TabIndex = 0;
            this.chart통합.Text = "uChart1";
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1084, 63);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cbo화폐단위);
            this.bpPanelControl3.Controls.Add(this.lbl화폐단위);
            this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // cbo화폐단위
            // 
            this.cbo화폐단위.AutoDropDown = true;
            this.cbo화폐단위.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo화폐단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo화폐단위.FormattingEnabled = true;
            this.cbo화폐단위.ItemHeight = 12;
            this.cbo화폐단위.Location = new System.Drawing.Point(106, 0);
            this.cbo화폐단위.Name = "cbo화폐단위";
            this.cbo화폐단위.Size = new System.Drawing.Size(186, 20);
            this.cbo화폐단위.TabIndex = 1;
            // 
            // lbl화폐단위
            // 
            this.lbl화폐단위.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl화폐단위.Location = new System.Drawing.Point(0, 0);
            this.lbl화폐단위.Name = "lbl화폐단위";
            this.lbl화폐단위.Size = new System.Drawing.Size(100, 23);
            this.lbl화폐단위.TabIndex = 0;
            this.lbl화폐단위.Text = "화폐단위";
            this.lbl화폐단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp기준일자);
            this.bpPanelControl1.Controls.Add(this.lbl기준일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp기준일자
            // 
            this.dtp기준일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기준일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp기준일자.IsNecessaryCondition = true;
            this.dtp기준일자.Location = new System.Drawing.Point(107, 0);
            this.dtp기준일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp기준일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp기준일자.Name = "dtp기준일자";
            this.dtp기준일자.Size = new System.Drawing.Size(185, 21);
            this.dtp기준일자.TabIndex = 1;
            // 
            // lbl기준일자
            // 
            this.lbl기준일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl기준일자.Location = new System.Drawing.Point(0, 0);
            this.lbl기준일자.Name = "lbl기준일자";
            this.lbl기준일자.Size = new System.Drawing.Size(100, 23);
            this.lbl기준일자.TabIndex = 0;
            this.lbl기준일자.Text = "기준일자";
            this.lbl기준일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.cbo회사);
            this.bpPanelControl4.Controls.Add(this.lbl회사);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 3;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // cbo회사
            // 
            this.cbo회사.AutoDropDown = true;
            this.cbo회사.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo회사.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo회사.FormattingEnabled = true;
            this.cbo회사.ItemHeight = 12;
            this.cbo회사.Location = new System.Drawing.Point(106, 0);
            this.cbo회사.Name = "cbo회사";
            this.cbo회사.Size = new System.Drawing.Size(186, 20);
            this.cbo회사.TabIndex = 1;
            // 
            // lbl회사
            // 
            this.lbl회사.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl회사.Location = new System.Drawing.Point(0, 0);
            this.lbl회사.Name = "lbl회사";
            this.lbl회사.Size = new System.Drawing.Size(100, 23);
            this.lbl회사.TabIndex = 0;
            this.lbl회사.Text = "회사";
            this.lbl회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.flowLayoutPanel2);
            this.bpPanelControl5.Controls.Add(this.lbl수주실적옵션);
            this.bpPanelControl5.Location = new System.Drawing.Point(340, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(377, 23);
            this.bpPanelControl5.TabIndex = 2;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.chk상세보기);
            this.flowLayoutPanel2.Controls.Add(this.chk목표보기);
            this.flowLayoutPanel2.Controls.Add(this.chk전년동월보기);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(106, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(271, 23);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // chk상세보기
            // 
            this.chk상세보기.AutoSize = true;
            this.chk상세보기.Location = new System.Drawing.Point(3, 3);
            this.chk상세보기.Name = "chk상세보기";
            this.chk상세보기.Size = new System.Drawing.Size(72, 16);
            this.chk상세보기.TabIndex = 2;
            this.chk상세보기.Text = "상세보기";
            this.chk상세보기.TextDD = null;
            this.chk상세보기.UseVisualStyleBackColor = true;
            // 
            // chk목표보기
            // 
            this.chk목표보기.AutoSize = true;
            this.chk목표보기.Location = new System.Drawing.Point(81, 3);
            this.chk목표보기.Name = "chk목표보기";
            this.chk목표보기.Size = new System.Drawing.Size(72, 16);
            this.chk목표보기.TabIndex = 3;
            this.chk목표보기.Text = "목표보기";
            this.chk목표보기.TextDD = null;
            this.chk목표보기.UseVisualStyleBackColor = true;
            // 
            // chk전년동월보기
            // 
            this.chk전년동월보기.AutoSize = true;
            this.chk전년동월보기.Location = new System.Drawing.Point(159, 3);
            this.chk전년동월보기.Name = "chk전년동월보기";
            this.chk전년동월보기.Size = new System.Drawing.Size(96, 16);
            this.chk전년동월보기.TabIndex = 4;
            this.chk전년동월보기.Text = "전년동월보기";
            this.chk전년동월보기.TextDD = null;
            this.chk전년동월보기.UseVisualStyleBackColor = true;
            // 
            // lbl수주실적옵션
            // 
            this.lbl수주실적옵션.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수주실적옵션.Location = new System.Drawing.Point(0, 0);
            this.lbl수주실적옵션.Name = "lbl수주실적옵션";
            this.lbl수주실적옵션.Size = new System.Drawing.Size(100, 23);
            this.lbl수주실적옵션.TabIndex = 0;
            this.lbl수주실적옵션.Text = "수주실적옵션";
            this.lbl수주실적옵션.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.lbl전체옵션);
            this.bpPanelControl2.Controls.Add(this.flowLayoutPanel1);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(336, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // lbl전체옵션
            // 
            this.lbl전체옵션.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl전체옵션.Location = new System.Drawing.Point(0, 0);
            this.lbl전체옵션.Name = "lbl전체옵션";
            this.lbl전체옵션.Size = new System.Drawing.Size(100, 23);
            this.lbl전체옵션.TabIndex = 2;
            this.lbl전체옵션.Text = "전체옵션";
            this.lbl전체옵션.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chk타이머사용);
            this.flowLayoutPanel1.Controls.Add(this.chk데이터그리드표시);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(106, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(230, 23);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // chk타이머사용
            // 
            this.chk타이머사용.AutoSize = true;
            this.chk타이머사용.Location = new System.Drawing.Point(3, 3);
            this.chk타이머사용.Name = "chk타이머사용";
            this.chk타이머사용.Size = new System.Drawing.Size(84, 16);
            this.chk타이머사용.TabIndex = 0;
            this.chk타이머사용.Text = "타이머사용";
            this.chk타이머사용.TextDD = null;
            this.chk타이머사용.UseVisualStyleBackColor = true;
            // 
            // chk데이터그리드표시
            // 
            this.chk데이터그리드표시.AutoSize = true;
            this.chk데이터그리드표시.Location = new System.Drawing.Point(93, 3);
            this.chk데이터그리드표시.Name = "chk데이터그리드표시";
            this.chk데이터그리드표시.Size = new System.Drawing.Size(120, 16);
            this.chk데이터그리드표시.TabIndex = 1;
            this.chk데이터그리드표시.Text = "데이터그리드표시";
            this.chk데이터그리드표시.TextDD = null;
            this.chk데이터그리드표시.UseVisualStyleBackColor = true;
            // 
            // btn손익계산서갱신
            // 
            this.btn손익계산서갱신.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn손익계산서갱신.BackColor = System.Drawing.Color.Transparent;
            this.btn손익계산서갱신.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn손익계산서갱신.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn손익계산서갱신.Location = new System.Drawing.Point(981, 10);
            this.btn손익계산서갱신.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn손익계산서갱신.Name = "btn손익계산서갱신";
            this.btn손익계산서갱신.Size = new System.Drawing.Size(106, 19);
            this.btn손익계산서갱신.TabIndex = 3;
            this.btn손익계산서갱신.TabStop = false;
            this.btn손익계산서갱신.Text = "손익계산서갱신";
            this.btn손익계산서갱신.UseVisualStyleBackColor = false;
            // 
            // P_CZ_BI_BIZ_RESULT_CHART
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn손익계산서갱신);
            this.Name = "P_CZ_BI_BIZ_RESULT_CHART";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "경영실적차트";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn손익계산서갱신, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpg월별수주실적.ResumeLayout(false);
            this.tpg분기별수주실적.ResumeLayout(false);
            this.tpg분기별매출금액.ResumeLayout(false);
            this.tpg분기별세전이익.ResumeLayout(false);
            this.tpg분기별수주매출세전이익.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.bpPanelControl2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpg월별수주실적;
        private System.Windows.Forms.TabPage tpg분기별매출금액;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private System.Windows.Forms.TabPage tpg분기별세전이익;
        private System.Windows.Forms.TabPage tpg분기별수주매출세전이익;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart월별수주실적;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart분기별매출금액;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart분기별세전이익;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart통합;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp기준일자;
        private Duzon.Common.Controls.LabelExt lbl기준일자;
        private System.Windows.Forms.Timer timer;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.CheckBoxExt chk타이머사용;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DropDownComboBox cbo화폐단위;
        private Duzon.Common.Controls.LabelExt lbl화폐단위;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.CheckBoxExt chk데이터그리드표시;
        private System.Windows.Forms.TabPage tpg분기별수주실적;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart분기별수주실적;
        private Duzon.Common.Controls.RoundedButton btn손익계산서갱신;
        private Duzon.Common.Controls.CheckBoxExt chk상세보기;
        private Duzon.Common.Controls.DropDownComboBox cbo회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Duzon.Common.Controls.LabelExt lbl수주실적옵션;
        private Duzon.Common.Controls.LabelExt lbl전체옵션;
        private Duzon.Common.Controls.CheckBoxExt chk목표보기;
        private Duzon.Common.Controls.CheckBoxExt chk전년동월보기;
    }
}