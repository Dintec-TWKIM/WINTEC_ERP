namespace sale
{
    partial class P_SA_SO_DLV_SUB
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_SO_DLV_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.lbl사용자정의코드1 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의코드1 = new Duzon.Common.Controls.DropDownComboBox();
			this.panelExt4 = new Duzon.Common.Controls.PanelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.panelExt5 = new Duzon.Common.Controls.PanelExt();
			this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn_Cancel = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn_Search = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn_Ok = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn_Apply = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_배송방법 = new Duzon.Common.Controls.LabelExt();
			this.cbo_TP_DLV = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_이동전화 = new Duzon.Common.Controls.LabelExt();
			this.txt_NO_TEL_D2 = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_전화 = new Duzon.Common.Controls.LabelExt();
			this.txt_NO_TEL_D1 = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_수취인 = new Duzon.Common.Controls.LabelExt();
			this.txt_NM_CUST_DLV = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_주소2 = new Duzon.Common.Controls.LabelExt();
			this.txt_ADDR2 = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_주소1 = new Duzon.Common.Controls.LabelExt();
			this.txt_ADDR1 = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_우편번호 = new Duzon.Common.Controls.LabelExt();
			this.txt_CD_ZIP = new Duzon.Common.Controls.MaskedEditBox();
			this.btn_Zip = new Duzon.Common.Controls.ButtonExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_비고 = new Duzon.Common.Controls.LabelExt();
			this.txt_DC_REQ = new Duzon.Common.Controls.TextBoxExt();
			this.openFileDialogUploadExcel = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelExt1.SuspendLayout();
			this.panelExt4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.panelExt5.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txt_CD_ZIP)).BeginInit();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panelExt4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.panelExt5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 49);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 562);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// panelExt1
			// 
			this.panelExt1.Controls.Add(this.lbl사용자정의코드1);
			this.panelExt1.Controls.Add(this.cbo사용자정의코드1);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt1.Location = new System.Drawing.Point(3, 94);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(780, 26);
			this.panelExt1.TabIndex = 4;
			// 
			// lbl사용자정의코드1
			// 
			this.lbl사용자정의코드1.Location = new System.Drawing.Point(3, 2);
			this.lbl사용자정의코드1.Name = "lbl사용자정의코드1";
			this.lbl사용자정의코드1.Size = new System.Drawing.Size(95, 18);
			this.lbl사용자정의코드1.TabIndex = 2;
			this.lbl사용자정의코드1.Tag = "";
			this.lbl사용자정의코드1.Text = "사용자정의코드1";
			this.lbl사용자정의코드1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의코드1.Visible = false;
			// 
			// cbo사용자정의코드1
			// 
			this.cbo사용자정의코드1.AutoDropDown = true;
			this.cbo사용자정의코드1.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의코드1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의코드1.ItemHeight = 12;
			this.cbo사용자정의코드1.Location = new System.Drawing.Point(102, 2);
			this.cbo사용자정의코드1.Name = "cbo사용자정의코드1";
			this.cbo사용자정의코드1.Size = new System.Drawing.Size(131, 20);
			this.cbo사용자정의코드1.TabIndex = 222;
			this.cbo사용자정의코드1.Tag = "CD_PLANT";
			this.cbo사용자정의코드1.Visible = false;
			// 
			// panelExt4
			// 
			this.panelExt4.Controls.Add(this._flex);
			this.panelExt4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt4.Location = new System.Drawing.Point(3, 155);
			this.panelExt4.Name = "panelExt4";
			this.panelExt4.Size = new System.Drawing.Size(780, 404);
			this.panelExt4.TabIndex = 5;
			// 
			// _flex
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
			this._flex.Location = new System.Drawing.Point(0, 0);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(780, 404);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// panelExt5
			// 
			this.panelExt5.Controls.Add(this.btn엑셀업로드);
			this.panelExt5.Controls.Add(this.btn_Cancel);
			this.panelExt5.Controls.Add(this.btn_Search);
			this.panelExt5.Controls.Add(this.btn_Ok);
			this.panelExt5.Controls.Add(this.btn_Apply);
			this.panelExt5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt5.Location = new System.Drawing.Point(3, 126);
			this.panelExt5.Name = "panelExt5";
			this.panelExt5.Size = new System.Drawing.Size(780, 23);
			this.panelExt5.TabIndex = 6;
			// 
			// btn엑셀업로드
			// 
			this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn엑셀업로드.BackColor = System.Drawing.Color.White;
			this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀업로드.Location = new System.Drawing.Point(452, 2);
			this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀업로드.Name = "btn엑셀업로드";
			this.btn엑셀업로드.Size = new System.Drawing.Size(80, 19);
			this.btn엑셀업로드.TabIndex = 22;
			this.btn엑셀업로드.TabStop = false;
			this.btn엑셀업로드.Tag = "";
			this.btn엑셀업로드.Text = "엑셀업로드";
			this.btn엑셀업로드.UseVisualStyleBackColor = false;
			this.btn엑셀업로드.Click += new System.EventHandler(this.btn엑셀업로드_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.BackColor = System.Drawing.Color.White;
			this.btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Cancel.Location = new System.Drawing.Point(720, 2);
			this.btn_Cancel.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(60, 19);
			this.btn_Cancel.TabIndex = 20;
			this.btn_Cancel.TabStop = false;
			this.btn_Cancel.Tag = "Q_CANCEL";
			this.btn_Cancel.Text = "취소";
			this.btn_Cancel.UseVisualStyleBackColor = false;
			// 
			// btn_Search
			// 
			this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Search.BackColor = System.Drawing.Color.White;
			this.btn_Search.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Search.Location = new System.Drawing.Point(534, 2);
			this.btn_Search.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn_Search.Name = "btn_Search";
			this.btn_Search.Size = new System.Drawing.Size(60, 19);
			this.btn_Search.TabIndex = 21;
			this.btn_Search.TabStop = false;
			this.btn_Search.Tag = "";
			this.btn_Search.Text = "검색";
			this.btn_Search.UseVisualStyleBackColor = false;
			this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
			// 
			// btn_Ok
			// 
			this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Ok.BackColor = System.Drawing.Color.White;
			this.btn_Ok.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Ok.Location = new System.Drawing.Point(658, 2);
			this.btn_Ok.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn_Ok.Name = "btn_Ok";
			this.btn_Ok.Size = new System.Drawing.Size(60, 19);
			this.btn_Ok.TabIndex = 15;
			this.btn_Ok.TabStop = false;
			this.btn_Ok.Tag = "Q_QUERY";
			this.btn_Ok.Text = "확인";
			this.btn_Ok.UseVisualStyleBackColor = false;
			this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
			// 
			// btn_Apply
			// 
			this.btn_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Apply.BackColor = System.Drawing.Color.White;
			this.btn_Apply.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Apply.Location = new System.Drawing.Point(596, 2);
			this.btn_Apply.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn_Apply.Name = "btn_Apply";
			this.btn_Apply.Size = new System.Drawing.Size(60, 19);
			this.btn_Apply.TabIndex = 19;
			this.btn_Apply.TabStop = false;
			this.btn_Apply.Tag = "";
			this.btn_Apply.Text = "일괄적용";
			this.btn_Apply.UseVisualStyleBackColor = false;
			this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(780, 85);
			this.oneGrid1.TabIndex = 7;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(770, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.lbl_배송방법);
			this.bpPanelControl4.Controls.Add(this.cbo_TP_DLV);
			this.bpPanelControl4.Location = new System.Drawing.Point(575, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(189, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// lbl_배송방법
			// 
			this.lbl_배송방법.Location = new System.Drawing.Point(0, 3);
			this.lbl_배송방법.Name = "lbl_배송방법";
			this.lbl_배송방법.Size = new System.Drawing.Size(80, 16);
			this.lbl_배송방법.TabIndex = 0;
			this.lbl_배송방법.Tag = "";
			this.lbl_배송방법.Text = "배송방법";
			this.lbl_배송방법.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo_TP_DLV
			// 
			this.cbo_TP_DLV.AutoDropDown = true;
			this.cbo_TP_DLV.BackColor = System.Drawing.Color.White;
			this.cbo_TP_DLV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo_TP_DLV.ItemHeight = 12;
			this.cbo_TP_DLV.Location = new System.Drawing.Point(81, 1);
			this.cbo_TP_DLV.Name = "cbo_TP_DLV";
			this.cbo_TP_DLV.Size = new System.Drawing.Size(105, 20);
			this.cbo_TP_DLV.TabIndex = 218;
			this.cbo_TP_DLV.Tag = "CD_PLANT";
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl_이동전화);
			this.bpPanelControl3.Controls.Add(this.txt_NO_TEL_D2);
			this.bpPanelControl3.Location = new System.Drawing.Point(384, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(189, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl_이동전화
			// 
			this.lbl_이동전화.Location = new System.Drawing.Point(0, 3);
			this.lbl_이동전화.Name = "lbl_이동전화";
			this.lbl_이동전화.Size = new System.Drawing.Size(80, 16);
			this.lbl_이동전화.TabIndex = 0;
			this.lbl_이동전화.Tag = "";
			this.lbl_이동전화.Text = "이동전화";
			this.lbl_이동전화.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_NO_TEL_D2
			// 
			this.txt_NO_TEL_D2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_NO_TEL_D2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_NO_TEL_D2.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.txt_NO_TEL_D2.Location = new System.Drawing.Point(81, 1);
			this.txt_NO_TEL_D2.MaxLength = 100;
			this.txt_NO_TEL_D2.Name = "txt_NO_TEL_D2";
			this.txt_NO_TEL_D2.Size = new System.Drawing.Size(106, 21);
			this.txt_NO_TEL_D2.TabIndex = 209;
			this.txt_NO_TEL_D2.Tag = "NO_PROJECT";
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.lbl_전화);
			this.bpPanelControl2.Controls.Add(this.txt_NO_TEL_D1);
			this.bpPanelControl2.Location = new System.Drawing.Point(193, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(189, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// lbl_전화
			// 
			this.lbl_전화.BackColor = System.Drawing.Color.White;
			this.lbl_전화.Location = new System.Drawing.Point(0, 3);
			this.lbl_전화.Name = "lbl_전화";
			this.lbl_전화.Size = new System.Drawing.Size(80, 16);
			this.lbl_전화.TabIndex = 1;
			this.lbl_전화.Tag = "";
			this.lbl_전화.Text = "전화";
			this.lbl_전화.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_NO_TEL_D1
			// 
			this.txt_NO_TEL_D1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_NO_TEL_D1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_NO_TEL_D1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.txt_NO_TEL_D1.Location = new System.Drawing.Point(81, 1);
			this.txt_NO_TEL_D1.MaxLength = 100;
			this.txt_NO_TEL_D1.Name = "txt_NO_TEL_D1";
			this.txt_NO_TEL_D1.Size = new System.Drawing.Size(106, 21);
			this.txt_NO_TEL_D1.TabIndex = 208;
			this.txt_NO_TEL_D1.Tag = "NO_PROJECT";
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl_수취인);
			this.bpPanelControl1.Controls.Add(this.txt_NM_CUST_DLV);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(189, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// lbl_수취인
			// 
			this.lbl_수취인.BackColor = System.Drawing.Color.White;
			this.lbl_수취인.Location = new System.Drawing.Point(0, 3);
			this.lbl_수취인.Name = "lbl_수취인";
			this.lbl_수취인.Size = new System.Drawing.Size(80, 16);
			this.lbl_수취인.TabIndex = 7;
			this.lbl_수취인.Tag = "";
			this.lbl_수취인.Text = "수취인";
			this.lbl_수취인.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_NM_CUST_DLV
			// 
			this.txt_NM_CUST_DLV.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_NM_CUST_DLV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_NM_CUST_DLV.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.txt_NM_CUST_DLV.Location = new System.Drawing.Point(81, 1);
			this.txt_NM_CUST_DLV.MaxLength = 13;
			this.txt_NM_CUST_DLV.Name = "txt_NM_CUST_DLV";
			this.txt_NM_CUST_DLV.Size = new System.Drawing.Size(92, 21);
			this.txt_NM_CUST_DLV.TabIndex = 207;
			this.txt_NM_CUST_DLV.Tag = "NO_PROJECT";
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl6);
			this.oneGridItem2.Controls.Add(this.bpPanelControl7);
			this.oneGridItem2.Controls.Add(this.bpPanelControl8);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(770, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.lbl_주소2);
			this.bpPanelControl6.Controls.Add(this.txt_ADDR2);
			this.bpPanelControl6.Location = new System.Drawing.Point(479, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(285, 23);
			this.bpPanelControl6.TabIndex = 6;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// lbl_주소2
			// 
			this.lbl_주소2.Location = new System.Drawing.Point(0, 3);
			this.lbl_주소2.Name = "lbl_주소2";
			this.lbl_주소2.Size = new System.Drawing.Size(80, 16);
			this.lbl_주소2.TabIndex = 1;
			this.lbl_주소2.Tag = "";
			this.lbl_주소2.Text = "주소2";
			this.lbl_주소2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_ADDR2
			// 
			this.txt_ADDR2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_ADDR2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_ADDR2.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.txt_ADDR2.Location = new System.Drawing.Point(81, 1);
			this.txt_ADDR2.MaxLength = 100;
			this.txt_ADDR2.Name = "txt_ADDR2";
			this.txt_ADDR2.Size = new System.Drawing.Size(201, 21);
			this.txt_ADDR2.TabIndex = 213;
			this.txt_ADDR2.Tag = "NO_PROJECT";
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.lbl_주소1);
			this.bpPanelControl7.Controls.Add(this.txt_ADDR1);
			this.bpPanelControl7.Location = new System.Drawing.Point(193, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(284, 23);
			this.bpPanelControl7.TabIndex = 5;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// lbl_주소1
			// 
			this.lbl_주소1.BackColor = System.Drawing.Color.White;
			this.lbl_주소1.Location = new System.Drawing.Point(0, 3);
			this.lbl_주소1.Name = "lbl_주소1";
			this.lbl_주소1.Size = new System.Drawing.Size(80, 16);
			this.lbl_주소1.TabIndex = 2;
			this.lbl_주소1.Tag = "";
			this.lbl_주소1.Text = "주소1";
			this.lbl_주소1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_ADDR1
			// 
			this.txt_ADDR1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_ADDR1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_ADDR1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.txt_ADDR1.Location = new System.Drawing.Point(81, 1);
			this.txt_ADDR1.MaxLength = 100;
			this.txt_ADDR1.Name = "txt_ADDR1";
			this.txt_ADDR1.Size = new System.Drawing.Size(201, 21);
			this.txt_ADDR1.TabIndex = 212;
			this.txt_ADDR1.Tag = "NO_PROJECT";
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.lbl_우편번호);
			this.bpPanelControl8.Controls.Add(this.txt_CD_ZIP);
			this.bpPanelControl8.Controls.Add(this.btn_Zip);
			this.bpPanelControl8.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(189, 23);
			this.bpPanelControl8.TabIndex = 4;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// lbl_우편번호
			// 
			this.lbl_우편번호.BackColor = System.Drawing.Color.White;
			this.lbl_우편번호.Location = new System.Drawing.Point(0, 3);
			this.lbl_우편번호.Name = "lbl_우편번호";
			this.lbl_우편번호.Size = new System.Drawing.Size(80, 16);
			this.lbl_우편번호.TabIndex = 8;
			this.lbl_우편번호.Tag = "";
			this.lbl_우편번호.Text = "우편번호";
			this.lbl_우편번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_CD_ZIP
			// 
			this.txt_CD_ZIP.AccessibleDescription = "MaskedEdit TextBox";
			this.txt_CD_ZIP.AccessibleName = "MaskedEditBox";
			this.txt_CD_ZIP.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txt_CD_ZIP.BackColor = System.Drawing.Color.White;
			this.txt_CD_ZIP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_CD_ZIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_CD_ZIP.Culture = new System.Globalization.CultureInfo("ko-KR");
			this.txt_CD_ZIP.Location = new System.Drawing.Point(81, 1);
			this.txt_CD_ZIP.Mask = "";
			this.txt_CD_ZIP.MaxLength = 5;
			this.txt_CD_ZIP.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.txt_CD_ZIP.Name = "txt_CD_ZIP";
			this.txt_CD_ZIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txt_CD_ZIP.Size = new System.Drawing.Size(60, 21);
			this.txt_CD_ZIP.TabIndex = 215;
			this.txt_CD_ZIP.Tag = "CD_ZIP";
			// 
			// btn_Zip
			// 
			this.btn_Zip.BackColor = System.Drawing.SystemColors.Control;
			this.btn_Zip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Zip.Image = ((System.Drawing.Image)(resources.GetObject("btn_Zip.Image")));
			this.btn_Zip.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btn_Zip.Location = new System.Drawing.Point(144, 0);
			this.btn_Zip.Name = "btn_Zip";
			this.btn_Zip.Size = new System.Drawing.Size(30, 21);
			this.btn_Zip.TabIndex = 216;
			this.btn_Zip.TabStop = false;
			this.btn_Zip.UseVisualStyleBackColor = false;
			this.btn_Zip.Click += new System.EventHandler(this.btn_Zip_Click);
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl12);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(770, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.lbl_비고);
			this.bpPanelControl12.Controls.Add(this.txt_DC_REQ);
			this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(764, 23);
			this.bpPanelControl12.TabIndex = 4;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// lbl_비고
			// 
			this.lbl_비고.BackColor = System.Drawing.Color.White;
			this.lbl_비고.Location = new System.Drawing.Point(0, 3);
			this.lbl_비고.Name = "lbl_비고";
			this.lbl_비고.Size = new System.Drawing.Size(80, 16);
			this.lbl_비고.TabIndex = 9;
			this.lbl_비고.Tag = "";
			this.lbl_비고.Text = "비고";
			this.lbl_비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_DC_REQ
			// 
			this.txt_DC_REQ.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_DC_REQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_DC_REQ.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.txt_DC_REQ.Location = new System.Drawing.Point(81, 1);
			this.txt_DC_REQ.MaxLength = 100;
			this.txt_DC_REQ.Name = "txt_DC_REQ";
			this.txt_DC_REQ.Size = new System.Drawing.Size(678, 21);
			this.txt_DC_REQ.TabIndex = 221;
			this.txt_DC_REQ.Tag = "NO_PROJECT";
			// 
			// openFileDialogUploadExcel
			// 
			this.openFileDialogUploadExcel.FileName = "openFileDialog1";
			// 
			// P_SA_SO_DLV_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(788, 613);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_SA_SO_DLV_SUB";
			this.TitleText = "배송지 주소 도움창";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelExt1.ResumeLayout(false);
			this.panelExt4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.panelExt5.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl6.PerformLayout();
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl7.PerformLayout();
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl8.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txt_CD_ZIP)).EndInit();
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl12.ResumeLayout(false);
			this.bpPanelControl12.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.LabelExt lbl_수취인;
        private Duzon.Common.Controls.LabelExt lbl_이동전화;
        private Duzon.Common.Controls.LabelExt lbl_전화;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.RoundedButton btn_Cancel;
        private Duzon.Common.Controls.RoundedButton btn_Apply;
        private Duzon.Common.Controls.RoundedButton btn_Ok;
        private Duzon.Common.Controls.TextBoxExt txt_ADDR2;
        private Duzon.Common.Controls.TextBoxExt txt_ADDR1;
        private Duzon.Common.Controls.TextBoxExt txt_NO_TEL_D2;
        private Duzon.Common.Controls.TextBoxExt txt_NO_TEL_D1;
        private Duzon.Common.Controls.TextBoxExt txt_NM_CUST_DLV;
        private Duzon.Common.Controls.LabelExt lbl_우편번호;
        private Duzon.Common.Controls.MaskedEditBox txt_CD_ZIP;
        private Duzon.Common.Controls.ButtonExt btn_Zip;
        private Duzon.Common.Controls.LabelExt lbl_배송방법;
        private Duzon.Common.Controls.LabelExt lbl_주소2;
        private Duzon.Common.Controls.LabelExt lbl_주소1;
        private Duzon.Common.Controls.DropDownComboBox cbo_TP_DLV;
        private Duzon.Common.Controls.TextBoxExt txt_DC_REQ;
        private Duzon.Common.Controls.LabelExt lbl_비고;
        private Duzon.Common.Controls.RoundedButton btn_Search;
        private Duzon.Common.Controls.RoundedButton btn엑셀업로드;
        private System.Windows.Forms.OpenFileDialog openFileDialogUploadExcel;
        private Duzon.Common.Controls.DropDownComboBox cbo사용자정의코드1;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.Controls.LabelExt lbl사용자정의코드1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl12;
    }
}