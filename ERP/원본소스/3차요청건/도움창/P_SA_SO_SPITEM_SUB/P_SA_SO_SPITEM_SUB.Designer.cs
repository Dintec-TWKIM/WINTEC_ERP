namespace sale
{
    partial class P_SA_SO_SPITEM_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_SO_SPITEM_SUB));
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.bp_CdSl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.btn_Apply02 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_DelRow = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_AddRow = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_ReSearch = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.ctx접수유형 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.btn_Pitem_Search = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_SPTYPE_Search = new Duzon.Common.Controls.RoundedButton(this.components);
            this.txt_SPITEM_Search = new Duzon.Common.Controls.TextBoxExt();
            this.cbo_Plant = new Duzon.Common.Controls.DropDownComboBox();
            this.bp_Pitem = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panel39 = new Duzon.Common.Controls.PanelExt();
            this.panelExt3 = new Duzon.Common.Controls.PanelExt();
            this.lbl_공장 = new Duzon.Common.Controls.LabelExt();
            this.lbl_접수유형 = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.lbl_품목검색 = new Duzon.Common.Controls.LabelExt();
            this.lbl_상품검색 = new Duzon.Common.Controls.LabelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.btn_Cancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_Apply01 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_Search = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.panelExt2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelExt3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panelExt1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(3, 89);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(780, 146);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 6;
            this._flexH.UseGridCalculator = true;
            // 
            // panelExt2
            // 
            this.panelExt2.Controls.Add(this.bp_CdSl);
            this.panelExt2.Controls.Add(this.btn_Apply02);
            this.panelExt2.Controls.Add(this.btn_DelRow);
            this.panelExt2.Controls.Add(this.btn_AddRow);
            this.panelExt2.Controls.Add(this.btn_ReSearch);
            this.panelExt2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt2.Location = new System.Drawing.Point(3, 241);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(780, 21);
            this.panelExt2.TabIndex = 5;
            // 
            // bp_CdSl
            // 
            this.bp_CdSl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bp_CdSl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bp_CdSl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CdSl.ButtonImage")));
            this.bp_CdSl.ChildMode = "";
            this.bp_CdSl.CodeName = "";
            this.bp_CdSl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CdSl.CodeValue = "";
            this.bp_CdSl.ComboCheck = true;
            this.bp_CdSl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bp_CdSl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.bp_CdSl.IsCodeValueToUpper = true;
            this.bp_CdSl.ItemBackColor = System.Drawing.SystemColors.Window;
            this.bp_CdSl.Location = new System.Drawing.Point(345, 0);
            this.bp_CdSl.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CdSl.Name = "bp_CdSl";
            this.bp_CdSl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CdSl.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp_CdSl.SearchCode = true;
            this.bp_CdSl.SelectCount = 0;
            this.bp_CdSl.SetDefaultValue = false;
            this.bp_CdSl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CdSl.Size = new System.Drawing.Size(147, 21);
            this.bp_CdSl.TabIndex = 195;
            this.bp_CdSl.TabStop = false;
            this.bp_CdSl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // btn_Apply02
            // 
            this.btn_Apply02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Apply02.BackColor = System.Drawing.Color.White;
            this.btn_Apply02.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Apply02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Apply02.Location = new System.Drawing.Point(493, 0);
            this.btn_Apply02.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Apply02.Name = "btn_Apply02";
            this.btn_Apply02.Size = new System.Drawing.Size(60, 19);
            this.btn_Apply02.TabIndex = 21;
            this.btn_Apply02.TabStop = false;
            this.btn_Apply02.Tag = "Q_QUERY";
            this.btn_Apply02.Text = "창고적용";
            this.btn_Apply02.UseVisualStyleBackColor = false;
            this.btn_Apply02.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // btn_DelRow
            // 
            this.btn_DelRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DelRow.BackColor = System.Drawing.Color.White;
            this.btn_DelRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DelRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DelRow.Location = new System.Drawing.Point(718, 0);
            this.btn_DelRow.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_DelRow.Name = "btn_DelRow";
            this.btn_DelRow.Size = new System.Drawing.Size(60, 19);
            this.btn_DelRow.TabIndex = 20;
            this.btn_DelRow.TabStop = false;
            this.btn_DelRow.Tag = "";
            this.btn_DelRow.Text = "삭제";
            this.btn_DelRow.UseVisualStyleBackColor = false;
            this.btn_DelRow.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // btn_AddRow
            // 
            this.btn_AddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddRow.BackColor = System.Drawing.Color.White;
            this.btn_AddRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddRow.Location = new System.Drawing.Point(657, 0);
            this.btn_AddRow.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_AddRow.Name = "btn_AddRow";
            this.btn_AddRow.Size = new System.Drawing.Size(60, 19);
            this.btn_AddRow.TabIndex = 19;
            this.btn_AddRow.TabStop = false;
            this.btn_AddRow.Tag = "";
            this.btn_AddRow.Text = "추가";
            this.btn_AddRow.UseVisualStyleBackColor = false;
            this.btn_AddRow.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // btn_ReSearch
            // 
            this.btn_ReSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ReSearch.BackColor = System.Drawing.Color.White;
            this.btn_ReSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ReSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ReSearch.Location = new System.Drawing.Point(596, 0);
            this.btn_ReSearch.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_ReSearch.Name = "btn_ReSearch";
            this.btn_ReSearch.Size = new System.Drawing.Size(60, 19);
            this.btn_ReSearch.TabIndex = 15;
            this.btn_ReSearch.TabStop = false;
            this.btn_ReSearch.Tag = "Q_QUERY";
            this.btn_ReSearch.Text = "재전개";
            this.btn_ReSearch.UseVisualStyleBackColor = false;
            this.btn_ReSearch.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.ctx접수유형);
            this.panel4.Controls.Add(this.btn_Pitem_Search);
            this.panel4.Controls.Add(this.btn_SPTYPE_Search);
            this.panel4.Controls.Add(this.txt_SPITEM_Search);
            this.panel4.Controls.Add(this.cbo_Plant);
            this.panel4.Controls.Add(this.bp_Pitem);
            this.panel4.Controls.Add(this.panel39);
            this.panel4.Controls.Add(this.panelExt3);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(780, 53);
            this.panel4.TabIndex = 3;
            // 
            // ctx접수유형
            // 
            this.ctx접수유형.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx접수유형.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx접수유형.ButtonImage")));
            this.ctx접수유형.ChildMode = "";
            this.ctx접수유형.CodeName = "";
            this.ctx접수유형.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx접수유형.CodeValue = "";
            this.ctx접수유형.ComboCheck = true;
            this.ctx접수유형.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx접수유형.IsCodeValueToUpper = true;
            this.ctx접수유형.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.ctx접수유형.Location = new System.Drawing.Point(93, 3);
            this.ctx접수유형.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx접수유형.Name = "ctx접수유형";
            this.ctx접수유형.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx접수유형.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx접수유형.SearchCode = true;
            this.ctx접수유형.SelectCount = 0;
            this.ctx접수유형.SetDefaultValue = false;
            this.ctx접수유형.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx접수유형.Size = new System.Drawing.Size(161, 21);
            this.ctx접수유형.TabIndex = 200;
            this.ctx접수유형.TabStop = false;
            this.ctx접수유형.Tag = "";
            this.ctx접수유형.UserCodeName = "NAME";
            this.ctx접수유형.UserCodeValue = "CODE";
            this.ctx접수유형.UserHelpID = "H_SA_HELP01";
            this.ctx접수유형.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // btn_Pitem_Search
            // 
            this.btn_Pitem_Search.BackColor = System.Drawing.Color.White;
            this.btn_Pitem_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Pitem_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Pitem_Search.Location = new System.Drawing.Point(550, 28);
            this.btn_Pitem_Search.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Pitem_Search.Name = "btn_Pitem_Search";
            this.btn_Pitem_Search.Size = new System.Drawing.Size(48, 19);
            this.btn_Pitem_Search.TabIndex = 199;
            this.btn_Pitem_Search.TabStop = false;
            this.btn_Pitem_Search.Text = "검색";
            this.btn_Pitem_Search.UseVisualStyleBackColor = false;
            this.btn_Pitem_Search.Visible = false;
            this.btn_Pitem_Search.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // btn_SPTYPE_Search
            // 
            this.btn_SPTYPE_Search.BackColor = System.Drawing.Color.White;
            this.btn_SPTYPE_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SPTYPE_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SPTYPE_Search.Location = new System.Drawing.Point(550, 3);
            this.btn_SPTYPE_Search.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_SPTYPE_Search.Name = "btn_SPTYPE_Search";
            this.btn_SPTYPE_Search.Size = new System.Drawing.Size(48, 19);
            this.btn_SPTYPE_Search.TabIndex = 198;
            this.btn_SPTYPE_Search.TabStop = false;
            this.btn_SPTYPE_Search.Text = "검색";
            this.btn_SPTYPE_Search.UseVisualStyleBackColor = false;
            this.btn_SPTYPE_Search.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // txt_SPITEM_Search
            // 
            this.txt_SPITEM_Search.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt_SPITEM_Search.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt_SPITEM_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_SPITEM_Search.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_SPITEM_Search.Location = new System.Drawing.Point(352, 4);
            this.txt_SPITEM_Search.MaxLength = 20;
            this.txt_SPITEM_Search.Name = "txt_SPITEM_Search";
            this.txt_SPITEM_Search.SelectedAllEnabled = false;
            this.txt_SPITEM_Search.Size = new System.Drawing.Size(197, 21);
            this.txt_SPITEM_Search.TabIndex = 197;
            this.txt_SPITEM_Search.Tag = "NO_SO";
            this.txt_SPITEM_Search.UseKeyEnter = false;
            this.txt_SPITEM_Search.UseKeyF3 = false;
            // 
            // cbo_Plant
            // 
            this.cbo_Plant.AutoDropDown = true;
            this.cbo_Plant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_Plant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Plant.ItemHeight = 12;
            this.cbo_Plant.Location = new System.Drawing.Point(93, 30);
            this.cbo_Plant.Name = "cbo_Plant";
            this.cbo_Plant.ShowCheckBox = false;
            this.cbo_Plant.Size = new System.Drawing.Size(161, 20);
            this.cbo_Plant.TabIndex = 196;
            this.cbo_Plant.Tag = "TP_PRICE";
            this.cbo_Plant.UseKeyEnter = false;
            this.cbo_Plant.UseKeyF3 = false;
            // 
            // bp_Pitem
            // 
            this.bp_Pitem.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bp_Pitem.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_Pitem.ButtonImage")));
            this.bp_Pitem.ChildMode = "";
            this.bp_Pitem.CodeName = "";
            this.bp_Pitem.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_Pitem.CodeValue = "";
            this.bp_Pitem.ComboCheck = true;
            this.bp_Pitem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bp_Pitem.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.bp_Pitem.IsCodeValueToUpper = true;
            this.bp_Pitem.ItemBackColor = System.Drawing.SystemColors.Window;
            this.bp_Pitem.Location = new System.Drawing.Point(352, 29);
            this.bp_Pitem.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_Pitem.Name = "bp_Pitem";
            this.bp_Pitem.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_Pitem.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp_Pitem.SearchCode = true;
            this.bp_Pitem.SelectCount = 0;
            this.bp_Pitem.SetDefaultValue = false;
            this.bp_Pitem.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_Pitem.Size = new System.Drawing.Size(198, 21);
            this.bp_Pitem.TabIndex = 194;
            this.bp_Pitem.TabStop = false;
            this.bp_Pitem.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // panel39
            // 
            this.panel39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel39.BackColor = System.Drawing.Color.Transparent;
            this.panel39.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel39.BackgroundImage")));
            this.panel39.Location = new System.Drawing.Point(0, 27);
            this.panel39.Name = "panel39";
            this.panel39.Size = new System.Drawing.Size(779, 1);
            this.panel39.TabIndex = 192;
            // 
            // panelExt3
            // 
            this.panelExt3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt3.Controls.Add(this.lbl_공장);
            this.panelExt3.Controls.Add(this.lbl_접수유형);
            this.panelExt3.Location = new System.Drawing.Point(1, 1);
            this.panelExt3.Name = "panelExt3";
            this.panelExt3.Size = new System.Drawing.Size(89, 49);
            this.panelExt3.TabIndex = 193;
            // 
            // lbl_공장
            // 
            this.lbl_공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_공장.Location = new System.Drawing.Point(5, 28);
            this.lbl_공장.Name = "lbl_공장";
            this.lbl_공장.Resizeble = true;
            this.lbl_공장.Size = new System.Drawing.Size(81, 18);
            this.lbl_공장.TabIndex = 2;
            this.lbl_공장.Tag = "";
            this.lbl_공장.Text = "공장";
            this.lbl_공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_접수유형
            // 
            this.lbl_접수유형.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_접수유형.Location = new System.Drawing.Point(5, 5);
            this.lbl_접수유형.Name = "lbl_접수유형";
            this.lbl_접수유형.Resizeble = true;
            this.lbl_접수유형.Size = new System.Drawing.Size(81, 18);
            this.lbl_접수유형.TabIndex = 1;
            this.lbl_접수유형.Tag = "";
            this.lbl_접수유형.Text = "접수유형";
            this.lbl_접수유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.lbl_품목검색);
            this.panel7.Controls.Add(this.lbl_상품검색);
            this.panel7.Location = new System.Drawing.Point(260, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(89, 49);
            this.panel7.TabIndex = 40;
            // 
            // lbl_품목검색
            // 
            this.lbl_품목검색.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_품목검색.Location = new System.Drawing.Point(3, 28);
            this.lbl_품목검색.Name = "lbl_품목검색";
            this.lbl_품목검색.Resizeble = true;
            this.lbl_품목검색.Size = new System.Drawing.Size(81, 18);
            this.lbl_품목검색.TabIndex = 2;
            this.lbl_품목검색.Tag = "";
            this.lbl_품목검색.Text = "품목검색";
            this.lbl_품목검색.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_상품검색
            // 
            this.lbl_상품검색.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_상품검색.Location = new System.Drawing.Point(3, 5);
            this.lbl_상품검색.Name = "lbl_상품검색";
            this.lbl_상품검색.Resizeble = true;
            this.lbl_상품검색.Size = new System.Drawing.Size(81, 18);
            this.lbl_상품검색.TabIndex = 1;
            this.lbl_상품검색.Tag = "";
            this.lbl_상품검색.Text = "상품검색";
            this.lbl_상품검색.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn_Cancel);
            this.panelExt1.Controls.Add(this.btn_Apply01);
            this.panelExt1.Controls.Add(this.btn_Search);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 62);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(780, 21);
            this.panelExt1.TabIndex = 4;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.BackColor = System.Drawing.Color.White;
            this.btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Location = new System.Drawing.Point(717, 0);
            this.btn_Cancel.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(60, 19);
            this.btn_Cancel.TabIndex = 20;
            this.btn_Cancel.TabStop = false;
            this.btn_Cancel.Tag = "Q_CANCEL";
            this.btn_Cancel.Text = "종료";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            // 
            // btn_Apply01
            // 
            this.btn_Apply01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Apply01.BackColor = System.Drawing.Color.White;
            this.btn_Apply01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Apply01.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Apply01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Apply01.Location = new System.Drawing.Point(656, 0);
            this.btn_Apply01.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Apply01.Name = "btn_Apply01";
            this.btn_Apply01.Size = new System.Drawing.Size(60, 19);
            this.btn_Apply01.TabIndex = 19;
            this.btn_Apply01.TabStop = false;
            this.btn_Apply01.Tag = "";
            this.btn_Apply01.Text = "적용";
            this.btn_Apply01.UseVisualStyleBackColor = false;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.BackColor = System.Drawing.Color.White;
            this.btn_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.Location = new System.Drawing.Point(595, 0);
            this.btn_Search.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(60, 19);
            this.btn_Search.TabIndex = 15;
            this.btn_Search.TabStop = false;
            this.btn_Search.Tag = "Q_QUERY";
            this.btn_Search.Text = "조회";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(3, 268);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 18;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(780, 291);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 7;
            this._flexL.UseGridCalculator = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flexL, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelExt2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._flexH, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 562);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // P_SA_SO_SPITEM_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(788, 613);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_SO_SPITEM_SUB";
            this.TitleText = "상품적용 도움창";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.panelExt2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelExt3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Dass.FlexGrid.FlexGrid _flexH;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CdSl;
        private Duzon.Common.Controls.RoundedButton btn_Apply02;
        private Duzon.Common.Controls.RoundedButton btn_DelRow;
        private Duzon.Common.Controls.RoundedButton btn_AddRow;
        private Duzon.Common.Controls.RoundedButton btn_ReSearch;
        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.Controls.RoundedButton btn_Pitem_Search;
        private Duzon.Common.Controls.RoundedButton btn_SPTYPE_Search;
        private Duzon.Common.Controls.TextBoxExt txt_SPITEM_Search;
        private Duzon.Common.Controls.DropDownComboBox cbo_Plant;
        private Duzon.Common.BpControls.BpCodeTextBox bp_Pitem;
        private Duzon.Common.Controls.PanelExt panel39;
        private Duzon.Common.Controls.PanelExt panelExt3;
        private Duzon.Common.Controls.LabelExt lbl_공장;
        private Duzon.Common.Controls.LabelExt lbl_접수유형;
        private Duzon.Common.Controls.PanelExt panel7;
        private Duzon.Common.Controls.LabelExt lbl_품목검색;
        private Duzon.Common.Controls.LabelExt lbl_상품검색;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.RoundedButton btn_Cancel;
        private Duzon.Common.Controls.RoundedButton btn_Apply01;
        private Duzon.Common.Controls.RoundedButton btn_Search;
        private Dass.FlexGrid.FlexGrid _flexL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx접수유형;

    }
}