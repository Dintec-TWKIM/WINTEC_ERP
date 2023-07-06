using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_MA_ITEMGP : PageBase
    {
        private string m_CDITEMGrp;
        private string m_NMITEMGrp;
        private string m_Rmk;
        private bool m_IsAdding = false;
        private bool m_IsMoving = true;
        private IU_Language L = new IU_Language();
        private string MLangCol = string.Empty;
        public P_CZ_MA_ITEMGP()
        {
            this.InitializeComponent();
            this.Z_ChangedComponent();
            this.Load += new EventHandler(this.Page_Load);
            this.Paint += new PaintEventHandler(this.Page_Paint);
            this.DataChanged += new EventHandler(this.Page_DataChanged);
        }
        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvnet();
        }

        private void InitEvnet()
        {
            this.txt품목그룹CD.Leave += new EventHandler(this.OnTextBoxLeave);
            this.txt품목그룹명.TextChanged += new EventHandler(this.GlobalzationControl_TextChanged);
            this.txt품목그룹명.Search += new EventHandler(this.Control_Search);
            this.txt영문그룹명.Leave += new EventHandler(this.OnTextBoxLeave);
            this.txt상위품목그룹.Leave += new EventHandler(this.OnTextBoxLeave);
            this.cbo최상위그룹유무.SelectionChangeCommitted += new EventHandler(this.OnComboBox_SelectionChangeCommitted);
            this.cbo공통여부.SelectionChangeCommitted += new EventHandler(this.OnComboBox_SelectionChangeCommitted);
            this.cbo계획bom여부.SelectionChangeCommitted += new EventHandler(this.OnComboBox_SelectionChangeCommitted);
            this.txt비고.Leave += new EventHandler(this.OnTextBoxLeave);
            this.cbo사용여부.SelectionChangeCommitted += new EventHandler(this.OnComboBox_SelectionChangeCommitted);
            this.cbo부자재.SelectionChangeCommitted += new EventHandler(this.OnComboBox_SelectionChangeCommitted);
            this.cur인당월투입시간.Leave += new EventHandler(this.OnCurrencyTextBoxLeave);
            this.cur생산능률.Leave += new EventHandler(this.OnCurrencyTextBoxLeave);
            this.cur인당월도급비.Leave += new EventHandler(this.OnCurrencyTextBoxLeave);
            this.cur단가비율.Leave += new EventHandler(this.OnCurrencyTextBoxLeave);
            this.treeView_Menu.BeforeSelect += new TreeViewCancelEventHandler(this.m_TreeView_BeforeSelect);
            this.treeView_Menu.AfterSelect += new TreeViewEventHandler(this.m_TreeView_AfterSelect);
            this.treeView_Menu.Leave += new EventHandler(this.m_TreeView_Leave);
            this.ctx사용자정의도움창1.CodeChanged += new EventHandler(this._bpUserdef1_CodeChanged);
        }

        public void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.MLangCol = this.L.GetLanguageTag("NM_ITEMGRP");
                this.txt품목그룹명.Tag = this.MLangCol;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarPrintButtonEnabled = false;
                if (!this.IsChanged())
                    return;
                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_Paint(object sender, PaintEventArgs e)
        {
            this.Paint -= new PaintEventHandler(this.Page_Paint);
            try
            {
                this.InitControl();
                this.SetButton();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitControl()
        {
            if (this.cbo사용여부.DataSource != null)
                return;
            DataSet comboData = this.GetComboData("N;YESNO");
            this.cbo최상위그룹유무.DataSource = comboData.Tables[0];
            this.cbo최상위그룹유무.DisplayMember = "CODE";
            this.cbo최상위그룹유무.ValueMember = "CODE";
            this.cbo공통여부.DataSource = comboData.Tables[0].Copy();
            this.cbo공통여부.DisplayMember = "CODE";
            this.cbo공통여부.ValueMember = "CODE";
            this.cbo계획bom여부.DataSource = comboData.Tables[0].Copy();
            this.cbo계획bom여부.DisplayMember = "CODE";
            this.cbo계획bom여부.ValueMember = "CODE";
            this.cbo사용여부.DataSource = comboData.Tables[0].Copy();
            this.cbo사용여부.DisplayMember = "CODE";
            this.cbo사용여부.ValueMember = "CODE";
            this.cbo부자재.DataSource = comboData.Tables[0].Copy();
            this.cbo부자재.DisplayMember = "CODE";
            this.cbo부자재.ValueMember = "CODE";
        }

        private void SetButton()
        {
            if (this.CanSearch)
                this.ToolBarSearchButtonEnabled = true;
            else
                this.ToolBarSearchButtonEnabled = false;
            if (this.CanAdd)
                this.ToolBarAddButtonEnabled = true;
            else
                this.ToolBarAddButtonEnabled = false;
            if (this.CanDelete)
                this.ToolBarDeleteButtonEnabled = true;
            else
                this.ToolBarDeleteButtonEnabled = false;
            if (this.CanSave)
                this.ToolBarSaveButtonEnabled = true;
            else
                this.ToolBarSaveButtonEnabled = false;
            if (this.CanPrint)
                this.ToolBarPrintButtonEnabled = false;
            else
                this.ToolBarPrintButtonEnabled = false;
            this.SetToolBarButtonState(true, false, false, false, false);
        }

        private void TreeViewSetting()
        {
            this.treeView_Menu.Nodes.Clear();
            TreeNode p_parentnode = new TreeNode();
            p_parentnode.ImageIndex = 0;
            p_parentnode.SelectedImageIndex = 1;
            p_parentnode.Text = this.PageName;
            p_parentnode.Tag = "";
            this.treeView_Menu.Nodes.Add(this.MakingChildNode(this._dtHead, p_parentnode.Tag.ToString(), p_parentnode));
            this.treeView_Menu.ExpandAll();
            this.treeView_Menu.SelectedNode = this.treeView_Menu.Nodes[0];
            this.treeView_Menu.Focus();
        }

        private TreeNode MakingChildNode(DataTable pdt_table, string ps_handle, TreeNode p_parentnode)
        {
            foreach (DataRow row in (InternalDataCollectionBase)pdt_table.Rows)
            {
                if (row["HD_ITEMGRP"].ToString() == ps_handle)
                    p_parentnode.Nodes.Add(this.MakingChildNode(pdt_table, row["CD_ITEMGRP"].ToString(), new TreeNode()
                    {
                        ImageIndex = 0,
                        SelectedImageIndex = 1,
                        Text = row[this.L.GetLanguageTag("NM_ITEMGRP")].ToString(),
                        Tag = row["CD_ITEMGRP"].ToString()
                    }));
            }
            return p_parentnode;
        }

        private void RemoveNode(TreeNode p_node)
        {
            foreach (TreeNode node in p_node.Nodes)
            {
                this._dtHead.Rows.Find(node.Tag.ToString()).Delete();
                this.RemoveNode(node);
            }
        }

        private void m_TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node == this.treeView_Menu.Nodes[0])
                {
                    this.txt품목그룹CD.Text = this.PageName;
                    this.txt품목그룹명.Text = "";
                    this.txt영문그룹명.Text = "";
                    this.txt상위품목그룹.Text = "";
                    this.txt상위품목그룹sub.Text = "";
                    this.cbo계획bom여부.SelectedIndex = 0;
                    this.cbo최상위그룹유무.SelectedIndex = 1;
                    this.cbo공통여부.SelectedIndex = 1;
                    this.cbo사용여부.SelectedIndex = 0;
                    this.cbo부자재.SelectedIndex = 1;
                    this.txt비고.Text = "";
                    this.cur인당월투입시간.DecimalValue = 0M;
                    this.cur생산능률.DecimalValue = 0M;
                    this.cur인당월도급비.DecimalValue = 0M;
                    this.ctx사용자정의도움창1.CodeValue = "";
                    this.ctx사용자정의도움창1.CodeName = "";
                    this.cur단가비율.DecimalValue = 0M;
                    this.ToolBarAddButtonEnabled = true;
                    this.ToolBarDeleteButtonEnabled = true;
                }
                else
                {
                    DataRow dataRow = e.Node.Tag != null ? this._dtHead.Rows.Find(e.Node.Tag.ToString()) : this._dtHead.Rows.Find("");
                    this.txt품목그룹CD.Text = dataRow["CD_ITEMGRP"].ToString();
                    this.txt품목그룹명.Text = dataRow[this.L.GetLanguageTag("NM_ITEMGRP")].ToString();
                    this.txt상위품목그룹.Text = dataRow["HD_ITEMGRP"].ToString();
                    this.txt상위품목그룹sub.Text = dataRow["HD_ITEMGRPNM"].ToString();
                    this.cbo계획bom여부.SelectedValue = dataRow["YN_PLANBOM"].ToString();
                    this.cbo최상위그룹유무.SelectedValue = dataRow["YN_TERM"].ToString();
                    this.cbo공통여부.SelectedValue = dataRow["YN_COMMON"].ToString();
                    this.cbo사용여부.SelectedValue = dataRow["USE_YN"].ToString();
                    this.cbo부자재.SelectedValue = dataRow["TXT_USERDEF2"].ToString();
                    this.txt비고.Text = dataRow["DC_RMK"].ToString();
                    this.txt영문그룹명.Text = dataRow["EN_ITEMGRP"].ToString();
                    this.cur인당월투입시간.DecimalValue = D.GetDecimal(dataRow["TM_PEOPLEPERMONTH"]);
                    this.cur생산능률.DecimalValue = D.GetDecimal(dataRow["RT_PRODUCT"]);
                    this.cur인당월도급비.DecimalValue = D.GetDecimal(dataRow["AM_CONTRACTPERMONTH"]);
                    this.ctx사용자정의도움창1.CodeValue = dataRow["CD_USERDEF1"].ToString();
                    this.ctx사용자정의도움창1.CodeName = dataRow["NM_USERDEF1"].ToString();
                    this.cur단가비율.DecimalValue = D.GetDecimal(dataRow["NUM_USERDEF1"]);
                    this.m_CDITEMGrp = dataRow["CD_ITEMGRP"].ToString();
                    this.m_NMITEMGrp = dataRow["NM_ITEMGRP"].ToString();
                    this.m_Rmk = dataRow["DC_RMK"].ToString();
                    this.modifyFg(this.treeView_Menu.SelectedNode);
                    this.ToolBarAddButtonEnabled = true;
                    this.ToolBarDeleteButtonEnabled = true;
                    if (e.Node.Parent == this.treeView_Menu.Nodes[0])
                        this.cbo최상위그룹유무.Enabled = false;
                    else
                        this.cbo최상위그룹유무.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void modifyFg(TreeNode p_node)
        {
            if (this.treeView_Menu.SelectedNode.LastNode == null)
            {
                this.cbo최상위그룹유무.SelectedValue = "Y";
            }
            else
            {
                this.cbo최상위그룹유무.SelectedValue = "N";
                DataRow dataRow = this._dtHead.Rows.Find(p_node.Tag.ToString());
                if (dataRow != null && (this.cbo최상위그룹유무.SelectedValue == null || this.cbo최상위그룹유무.SelectedValue.ToString() == "Y"))
                    dataRow["YN_TERM"] = "N";
            }
        }

        private void m_TreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Action.ToString() == "Unknown" || e.Node.Tag == null || this.m_IsAdding)
                    return;
                this.treeView_Menu.Focus();
                if (!this.m_IsMoving)
                {
                    this.m_IsMoving = true;
                    e.Cancel = true;
                }
                else if (string.IsNullOrEmpty(this.txt품목그룹CD.Text.ToString()))
                {
                    this.ShowMessage(this.MainFrameInterface.GetMessageDictionaryItem("MA_M000015").Replace("@", this.lbl품목그룹CD.Text));
                    e.Cancel = true;
                }
                else
                {
                    DataRow dataRow = this.ReturnRow(e.Node);
                    if (dataRow == null)
                        this.txt품목그룹CD.Enabled = false;
                    else if (dataRow.RowState == DataRowState.Added)
                        this.txt품목그룹CD.Enabled = true;
                    else
                        this.txt품목그룹CD.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private DataRow ReturnRow(TreeNode treeNode) => treeNode.Tag == null ? null : this._dtHead.Rows.Find(treeNode.Tag.ToString()) ?? null;

        private void m_TreeView_Leave(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = this.treeView_Menu.SelectedNode;
                if (selectedNode == null)
                    return;
                if (selectedNode == this.treeView_Menu.Nodes[0])
                {
                    this._curRow = null;
                }
                else
                {
                    this._curRow = selectedNode.Tag != null ? this._dtHead.Rows.Find(selectedNode.Tag.ToString()) : this._dtHead.Rows.Find("");
                    this._curNode = selectedNode;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.MsgAndSave(true, false))
                    return;
                this.txt품목그룹명.Modified = false;
                ResultData resultData = (ResultData)this.FillDataTable(new SpInfo()
                {
                    SpNameSelect = "UP_CZ_MA_ITEMGP_SELECT",
                    SpParamsSelect = new object[] { this.LoginInfo.CompanyCode,
                                                    Global.SystemLanguage.MultiLanguageLpoint }
                });
                this._dtHead = null;
                this._dtHead = (DataTable)resultData.DataValue;
                this.TreeViewSetting();
                this._dtHead.Columns["CD_ITEMGRP"].DefaultValue = "";
                this._dtHead.Columns["NM_ITEMGRP"].DefaultValue = "New TreeNode";
                this._dtHead.Columns[this.L.GetLanguageTag("NM_ITEMGRP")].DefaultValue = "New TreeNode";
                this._dtHead.Columns["HD_ITEMGRPNM"].DefaultValue = "";
                this._dtHead.Columns["HD_ITEMGRP"].DefaultValue = "";
                this._dtHead.Columns["YN_TERM"].DefaultValue = "N";
                this._dtHead.Columns["YN_COMMON"].DefaultValue = "N";
                this._dtHead.Columns["YN_PLANBOM"].DefaultValue = "N";
                this._dtHead.Columns["DC_RMK"].DefaultValue = "";
                this._dtHead.Columns["USE_YN"].DefaultValue = "Y";
                this._dtHead.Columns["EN_ITEMGRP"].DefaultValue = "";
                this._dtHead.Columns["TM_PEOPLEPERMONTH"].DefaultValue = 0;
                this._dtHead.Columns["RT_PRODUCT"].DefaultValue = 0;
                this._dtHead.Columns["AM_CONTRACTPERMONTH"].DefaultValue = 0;
                this._dtHead.Columns["CD_USERDEF1"].DefaultValue = "";
                this._dtHead.Columns["NM_USERDEF1"].DefaultValue = "";
                this._dtHead.Columns["NUM_USERDEF1"].DefaultValue = 0;
                this._dtHead.Columns["TXT_USERDEF2"].DefaultValue = "N";
                this._dtHead.PrimaryKey = new DataColumn[] { this._dtHead.Columns["CD_ITEMGRP"] };
                this.txt품목그룹CD.Text = this.PageName;
                this.txt품목그룹명.Text = "";
                this.txt영문그룹명.Text = "";
                this.txt상위품목그룹.Text = "";
                this.txt품목그룹명.Text = "";
                this.cbo사용여부.SelectedIndex = 0;
                this.cbo계획bom여부.SelectedIndex = 0;
                this.cbo최상위그룹유무.SelectedIndex = 0;
                this.cbo공통여부.SelectedIndex = 0;
                this.cbo부자재.SelectedIndex = 1;
                this.txt비고.Text = "";
                this.cur인당월도급비.DecimalValue = 0M;
                this.cur생산능률.DecimalValue = 0M;
                this.cur인당월투입시간.DecimalValue = 0M;
                this.ctx사용자정의도움창1.CodeValue = "";
                this.ctx사용자정의도움창1.CodeName = "";
                this.txt품목그룹CD.Enabled = false;
                this.cur단가비율.DecimalValue = 0M;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt품목그룹CD.Text.ToString()))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl품목그룹CD.Text);
                }
                else
                {
                    this.m_IsAdding = true;
                    TreeNode selectedNode = this.treeView_Menu.SelectedNode;
                    DataRow row = this._dtHead.NewRow();
                    row["HD_ITEMGRP"] = selectedNode.Tag.ToString();
                    row["YN_TERM"] = "Y";
                    this._dtHead.Rows.Add(row);
                    TreeNode node = new TreeNode();
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;
                    node.Tag = null;
                    node.Text = row[this.L.GetLanguageTag("NM_ITEMGRP")].ToString();
                    selectedNode.Nodes.Add(node);
                    DataRow dataRow = this._dtHead.Rows.Find(selectedNode.Tag.ToString());
                    if (selectedNode.Tag.ToString() != "")
                        dataRow["YN_TERM"] = "N";
                    this.treeView_Menu.SelectedNode = node;
                    this._curNode = this.treeView_Menu.SelectedNode;
                    this._curRow = row;
                    this.ToolBarSaveButtonEnabled = true;
                    this.txt품목그룹CD.Focus();
                    this.m_IsAdding = false;
                    this.txt품목그룹CD.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = this.treeView_Menu.SelectedNode;
                if (selectedNode.LastNode != null)
                {
                    this.ShowMessage("하위 노드가 존재합니다. 해당 노드를 삭제할 수 없습니다.");
                }
                else if (selectedNode == this.treeView_Menu.Nodes[0])
                {
                    this.ShowMessage("최상위 노드는 삭제할 수 없습니다.");
                }
                else
                {
                    this.RemoveNode(selectedNode);
                    selectedNode.Remove();
                    if (selectedNode.Tag != null)
                    {
                        this._dtHead.Rows.Find(selectedNode.Tag.ToString())?.Delete();
                        this.ToolBarSaveButtonEnabled = true;
                    }
                    else
                    {
                        this._dtHead.Rows.Find("")?.Delete();
                        this.ToolBarSaveButtonEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.MsgAndSave(true, true))
                    return false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.MsgAndSave(false, false))
                    return;
                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool IsChanged(string gubun)
        {
            if (this._dtHead == null)
                return false;
            this.treeView_Menu.Focus();
            return this._dtHead.GetChanges() != null;
        }

        private bool MsgAndSave(bool displayDialog, bool isExit)
        {
            if (!this.IsChanged(null))
                return true;
            bool flag = false;
            if (!displayDialog)
            {
                if (this.IsChanged(null))
                    flag = this.Save();
                return flag;
            }
            if (isExit)
            {
                switch (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까, "QY3"))
                {
                    case DialogResult.Cancel:
                        return false;
                    case DialogResult.No:
                        return true;
                }
            }
            else if (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까, "QY2") == DialogResult.No)
                return true;
            Application.DoEvents();
            if (this.IsChanged(null))
                flag = this.Save();
            return flag;
        }

        private string GetDDItem(params string[] colName)
        {
            string str = "";
            return str == "" ? "" : str.Substring(3, str.Length - 3);
        }

        private bool Check()
        {
            if (!string.IsNullOrEmpty(this.txt품목그룹CD.Text))
                return true;
            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl품목그룹CD.Text);
            this.txt품목그룹CD.Focus();
            return false;
        }

        private bool Save()
        {
            DataTable changes = this._dtHead.GetChanges();
            if (!this.Check())
                return false;
            if (changes == null || changes.Rows.Count == 0)
                return true;
            if (!((ResultData)this.Save(new SpInfo()
            {
                DataValue = changes,
                SpNameInsert = "UP_CZ_MA_ITEMGP_INSERT",
                SpNameUpdate = "UP_CZ_MA_ITEMGP_UPDATE",
                SpNameDelete = "UP_MA_ITEMGP_DELETE",
                SpParamsInsert = new string[] { "CD_ITEMGRP",
                                                "CD_COMPANY",
                                                "NM_ITEMGRP",
                                                "HD_ITEMGRP",
                                                "YN_TERM",
                                                "DC_RMK",
                                                "YN_PLANBOM",
                                                "USE_YN",
                                                "ID_INSERT",
                                                "EN_ITEMGRP",
                                                "YN_COMMON",
                                                "TM_PEOPLEPERMONTH",
                                                "RT_PRODUCT",
                                                "AM_CONTRACTPERMONTH",
                                                "NM_ITEMGRP_L1",
                                                "NM_ITEMGRP_L2",
                                                "NM_ITEMGRP_L3",
                                                "NM_ITEMGRP_L4",
                                                "NM_ITEMGRP_L5",
                                                "CD_USERDEF1",
                                                "NUM_USERDEF1",
                                                "TXT_USERDEF2" },
                SpParamsUpdate = new string[] { "CD_ITEMGRP",
                                                "CD_COMPANY",
                                                "NM_ITEMGRP",
                                                "HD_ITEMGRP",
                                                "YN_TERM",
                                                "DC_RMK",
                                                "YN_PLANBOM",
                                                "USE_YN",
                                                "ID_UPDATE",
                                                "EN_ITEMGRP",
                                                "YN_COMMON",
                                                "TM_PEOPLEPERMONTH",
                                                "RT_PRODUCT",
                                                "AM_CONTRACTPERMONTH",
                                                "NM_ITEMGRP_L1",
                                                "NM_ITEMGRP_L2",
                                                "NM_ITEMGRP_L3",
                                                "NM_ITEMGRP_L4",
                                                "NM_ITEMGRP_L5",
                                                "CD_USERDEF1",
                                                "NUM_USERDEF1",
                                                "TXT_USERDEF2" },
                SpParamsDelete = new string[] { "CD_ITEMGRP",
                                                "CD_COMPANY" },
                SpParamsValues = { { ActionState.Insert, "CD_COMPANY", this.LoginInfo.CompanyCode },
                                   { ActionState.Update, "CD_COMPANY", this.LoginInfo.CompanyCode },
                                   { ActionState.Delete, "CD_COMPANY", this.LoginInfo.CompanyCode },
                                   { ActionState.Insert, "ID_INSERT",  this.LoginInfo.EmployeeNo  },
                                   { ActionState.Update, "ID_UPDATE",  this.LoginInfo.EmployeeNo  } }
            })).Result)
                return false;
            this._dtHead.AcceptChanges();
            this.ToolBarSaveButtonEnabled = false;
            this.txt품목그룹CD.Enabled = false;
            return true;
        }

        private void OnComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (sender is ComboBox && this._curRow != null)
                {
                    ComboBox comboBox = (ComboBox)sender;
                    if (comboBox.Name == "cbo계획bom여부")
                        this._curRow["YN_PLANBOM"] = comboBox.SelectedValue;
                    else if (comboBox.Name == "cbo최상위그룹유무")
                        this._curRow["YN_TERM"] = comboBox.SelectedValue;
                    else if (comboBox.Name == "cbo사용여부")
                        this._curRow["USE_YN"] = comboBox.SelectedValue;
                    else if (comboBox.Name == "cbo공통여부")
                        this._curRow["YN_COMMON"] = comboBox.SelectedValue;
                    else if (comboBox.Name == "cbo부자재")
                        this._curRow["TXT_USERDEF2"] = comboBox.SelectedValue;
                    this.ToolBarSaveButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnTextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender is TextBox && this._curRow != null)
                {
                    TextBox textBox = (TextBox)sender;
                    if (textBox.Modified)
                    {
                        if (textBox.Name == "txt품목그룹CD")
                        {
                            if (this._curRow["CD_ITEMGRP"].ToString() != textBox.Text)
                            {
                                if (this._dtHead.Rows.Find(textBox.Text) == null)
                                {
                                    this._curRow["CD_ITEMGRP"] = textBox.Text;
                                    this._curNode.Tag = textBox.Text;
                                }
                                else
                                {
                                    this.ShowMessage(공통메세지._의값이중복되었습니다, this.lbl품목그룹CD.Text);
                                    this.treeView_Menu.SelectedNode = this._curNode;
                                    this.m_IsMoving = false;
                                    textBox.Text = this._curRow["CD_ITEMGRP"].ToString();
                                    textBox.Focus();
                                }
                            }
                        }
                        else if (textBox.Name == "txt품목그룹명")
                        {
                            if (this._curRow["EN_ITEMGRP"].ToString() != textBox.Text)
                                this._curRow["EN_ITEMGRP"] = textBox.Text;
                        }
                        else if (textBox.Name == "txt상위품목그룹")
                        {
                            if (this._curRow["HD_ITEMGRP"].ToString() != textBox.Text)
                                this._curRow["HD_ITEMGRP"] = textBox.Text;
                        }
                        else if (textBox.Name == "txt비고" && this._curRow["DC_RMK"].ToString() != textBox.Text)
                            this._curRow["DC_RMK"] = textBox.Text;
                        this.ToolBarSaveButtonEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _bpUserdef1_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = this.treeView_Menu.SelectedNode;
                if (selectedNode == null)
                    return;
                if (selectedNode == this.treeView_Menu.Nodes[0])
                {
                    this._curRow = null;
                }
                else
                {
                    this._curRow = selectedNode.Tag != null ? this._dtHead.Rows.Find(selectedNode.Tag.ToString()) : this._dtHead.Rows.Find("");
                    this._curNode = selectedNode;
                }
                if (this._curRow == null)
                    return;
                BpCodeTextBox bpCodeTextBox = (BpCodeTextBox)sender;
                if (this._curRow["CD_USERDEF1"].ToString() != bpCodeTextBox.CodeValue)
                {
                    this._curRow["CD_USERDEF1"] = bpCodeTextBox.CodeValue;
                    this._curRow["NM_USERDEF1"] = bpCodeTextBox.CodeName;
                    this.ToolBarSaveButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnCurrencyTextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender is CurrencyTextBox && this._curRow != null)
                {
                    CurrencyTextBox currencyTextBox = (CurrencyTextBox)sender;
                    if (currencyTextBox.Modified)
                    {
                        if (currencyTextBox.Name == "cur인당월투입시간")
                        {
                            if (this._curRow["TM_PEOPLEPERMONTH"].ToString() != currencyTextBox.Text)
                                this._curRow["TM_PEOPLEPERMONTH"] = currencyTextBox.DecimalValue;
                        }
                        else if (currencyTextBox.Name == "cur생산능률")
                        {
                            if (this._curRow["RT_PRODUCT"].ToString() != currencyTextBox.Text)
                                this._curRow["RT_PRODUCT"] = currencyTextBox.DecimalValue;
                        }
                        else if (currencyTextBox.Name == "cur인당월도급비")
                        {
                            if (this._curRow["AM_CONTRACTPERMONTH"].ToString() != currencyTextBox.Text)
                                this._curRow["AM_CONTRACTPERMONTH"] = currencyTextBox.DecimalValue;
                        }
                        else if (currencyTextBox.Name == "cur단가비율" && this._curRow["NUM_USERDEF1"].ToString() != currencyTextBox.Text)
                            this._curRow["NUM_USERDEF1"] = currencyTextBox.DecimalValue;
                        this.ToolBarSaveButtonEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void GlobalzationControl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((GlobalzationSearchBox)sender).Modified)
                    return;
                if (this._curRow["CD_ITEMGRP"].ToString() != "")
                {
                    TreeNode selectedNode = this.treeView_Menu.SelectedNode;
                    if (selectedNode == this.treeView_Menu.TopNode)
                        return;
                    this._curNode = selectedNode;
                    this._curRow = this.ReturnRow(selectedNode);
                    if (this._curRow == null)
                        return;
                }
                if (this.txt품목그룹명.Text != this._curNode.Text)
                {
                    this._curRow[this.L.GetLanguageTag("NM_ITEMGRP")] = this.txt품목그룹명.Text;
                    this._curNode.Text = this.txt품목그룹명.Text;
                }
                this.SaveEnabled();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_Search(object sender, EventArgs e)
        {
            try
            {
                this.m_TreeView_Leave(null, null);
                string languageName = this.L.GetLanguageName(this._dtHead.DefaultView, "CD_ITEMGRP ='" + this.treeView_Menu.SelectedNode.Tag.ToString() + "'", "NM_ITEMGRP");
                if (!this.L.getChanged)
                    return;
                this.txt품목그룹명.Modified = true;
                this.txt품목그룹명.Text = languageName;
                this.SaveEnabled();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SaveEnabled()
        {
            if (this.ToolBarSaveButtonEnabled)
                return;
            this.ToolBarSaveButtonEnabled = true;
        }

        private void Z_ChangedComponent()
        {
            try
            {
                if (Global.MainFrame.ServerKeyCommon == "SIMMONS" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
                {
                    this.oneGrid1.ItemCollection.RemoveAt(12);
                    this.oneGrid1.ItemCollection.RemoveAt(10);
                    this.oneGrid1.ItemCollection.RemoveAt(9);
                    this.oneGrid1.ItemCollection.RemoveAt(8);
                    this.oneGrid1.ItemCollection.Add(this.oneGridItem12);
                    this.lbl사용자정의도움창1.Text = "거래처";
                    this.ctx사용자정의도움창1.HelpID = HelpID.P_MA_PARTNER_SUB;
                    this.ctx사용자정의도움창1.Tag = "LN_PARTNER,NM_PARTNER";
                }
                else if (Global.MainFrame.ServerKeyCommon.ToUpper() == "GALAXIA")
                {
                    this.oneGrid1.ItemCollection.RemoveAt(12);
                    this.oneGrid1.ItemCollection.RemoveAt(11);
                }
                else if (Global.MainFrame.ServerKeyCommon.ToUpper() == "UBCARE")
                {
                    this.oneGrid1.ItemCollection.RemoveAt(12);
                    this.oneGrid1.ItemCollection.RemoveAt(11);
                    this.oneGrid1.ItemCollection.RemoveAt(10);
                    this.oneGrid1.ItemCollection.RemoveAt(8);
                }
                else if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ONEGENE" || Global.MainFrame.ServerKeyCommon.ToUpper() == "ATC")
                {
                    this.oneGrid1.ItemCollection.RemoveAt(11);
                    this.oneGrid1.ItemCollection.RemoveAt(10);
                    this.oneGrid1.ItemCollection.RemoveAt(9);
                    this.oneGrid1.ItemCollection.RemoveAt(8);
                    if (!(Global.MainFrame.ServerKeyCommon.ToUpper() == "ATC"))
                        return;
                    this.lbl단가비율.Text = "기준할인율";
                }
                else
                {
                    this.oneGrid1.ItemCollection.RemoveAt(12);
                    this.oneGrid1.ItemCollection.RemoveAt(11);
                    this.oneGrid1.ItemCollection.RemoveAt(10);
                    this.oneGrid1.ItemCollection.RemoveAt(9);
                    this.oneGrid1.ItemCollection.RemoveAt(8);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

    }
}
