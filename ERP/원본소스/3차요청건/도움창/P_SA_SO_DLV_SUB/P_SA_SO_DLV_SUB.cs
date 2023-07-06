using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    public partial class P_SA_SO_DLV_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region �� ������ & ���� ����

        P_SA_SO_DLV_SUB_BIZ _biz = new P_SA_SO_DLV_SUB_BIZ();

        private DataSet ds = null;
        private DataTable ReturnDt = null;
        private string partner = "";
        private string str�׹��÷� = "";
        private string _Str����������ڵ�1 = string.Empty; 
        private bool _YnModify = false;
        private string _NO_SO = ""; 
        public P_SA_SO_DLV_SUB()
        {
            try
            {
                InitializeComponent();
                InitGrid();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt)
        {
            try
            {
                InitializeComponent();

                InitGrid();

                _flex.Binding = ReturnDt;

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt, string[] str) : this(ReturnDt, str, "") { }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt, string[] str, string str�׹��÷�)
        {
            try
            {
                InitializeComponent();

                this.str�׹��÷� = str�׹��÷�;

                InitGrid();

                partner = str[0];
                txt_CD_ZIP.Text = str[1];
                txt_ADDR1.Text = str[2];
                txt_ADDR2.Text = str[3];
                txt_NO_TEL_D1.Text = str[4];
                txt_NM_CUST_DLV.Text = str[5];
                txt_NO_TEL_D2.Text = str[6];

                _flex.Binding = ReturnDt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt, string[] str, string str�׹��÷�, bool YnModify, string NO_SO)
        {
            try
            {
                InitializeComponent();

                this.str�׹��÷� = str�׹��÷�;
                _YnModify = YnModify;
                _NO_SO = NO_SO;
                InitGrid();

                partner = str[0];
                txt_CD_ZIP.Text = str[1];
                txt_ADDR1.Text = str[2];
                txt_ADDR2.Text = str[3];
                txt_NO_TEL_D1.Text = str[4];
                txt_NM_CUST_DLV.Text = str[5];
                txt_NO_TEL_D2.Text = str[6];

                if (_YnModify)
                {
                    _flex.Binding = _biz.��ȸ(_NO_SO);
                }
                else _flex.Binding = ReturnDt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region �� �ʱ�ȭ �̺�Ʈ / �޼ҵ�

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);
            _flex.SetCol(str�׹��÷� != "" ? str�׹��÷� : "SEQ_SO", "�׹�", 40, false);
            _flex.SetCol("CD_ITEM", "ǰ���ڵ�", 100, false, typeof(string));
            _flex.SetCol("NM_ITEM", "ǰ���", 100, false);
            _flex.SetCol("STND_ITEM", "�԰�", 70, false);
            _flex.SetCol("UNIT_SO", "����", 80, 3, false);
            _flex.SetCol("QT_SO", "����", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("NM_CUST_DLV", "������", 100, 30, true);
            _flex.SetCol("NO_TEL_D1", "��ȭ", 100, 20, true);
            _flex.SetCol("NO_TEL_D2", "�̵���ȭ", 100, 20, true);
            _flex.SetCol("CD_ZIP", "�����ȣ", 90, 6, true, typeof(string));
            _flex.SetCol("ADDR1", "�ּ�1", 150, 300, true);
            _flex.SetCol("ADDR2", "�ּ�2", 150, 200, true);
            _flex.SetCol("TP_DLV", "��۹��", 100, 3, true);
            _flex.SetCol("TP_DLV_DUE", "��ǰ���", 100, 4, true);
            _flex.SetCol("DC_REQ", "���", 100, true);
            _flex.SetCol("NO_ORDER", "�ֹ���ȣ", false);
            _flex.SetCol("NM_CUST", "�ֹ���", false);
            _flex.SetCol("NO_TEL1", "�ֹ�����ȭ��ȣ", false);
            _flex.SetCol("NO_TEL2", "�ֹ����̵���ȭ��ȣ", false);
            _flex.SetCol("DLV_TXT_USERDEF1", "����������ؽ�Ʈ1", 100);
            _flex.SetCol("DLV_CD_USERDEF1", "����������ڵ�1", 100);

            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);

            _flex.SetExceptEditCol(str�׹��÷� != "" ? str�׹��÷� : "SEQ_SO", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "QT_SO");
            
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SIMMONS")
                _flex.VerifyNotNull = new string[] { "NM_CUST_DLV", "CD_ZIP", "ADDR1" };
            else
                _flex.VerifyNotNull = new string[] { "NM_CUST_DLV", "CD_ZIP", "ADDR1", "TP_DLV" };

            _flex.SettingVersion = "1.0.0.2";

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flex.Cols["DLV_TXT_USERDEF1"].Visible = false;
            _flex.Cols["DLV_CD_USERDEF1"].Visible = false;

            _flex.LoadUserCache("P_SA_SO_DLV_SUB__flex");

            //�׸��忡�� ����� ���� ����â ���� �ɲ�~
            //_flex.HelpClick += new EventHandler(_flex_HelpClick);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
        }
        #endregion

        #region -> InitPaint : ������ �ʱ�ȭ
        //PageBase �� �θ��Լ� �̿� : ���� �ö�� �� ������ �ö�� ��Ʈ�ѵ��� �ʱ�ȭ �����ִ� ������ �Ѵ�.
        //Combo Box ���ý� �ɼ� => N:�������, S:�����߰�, SC:����&��ȣ�߰�, NC:��ȣ�߰�
        //���� �ڵ尪���� DB ���� ���س��� �Ķ���Ϳ� ��ġ��������Ѵ�
        protected override void InitPaint()
        {
            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl6.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl8.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            // 0: ��۹��(EC_0000002)
            ds = Global.MainFrame.GetComboData("S;EC_0000002", "S;SA_B000056");
            cbo_TP_DLV.DataSource = ds.Tables[0];
            cbo_TP_DLV.DisplayMember = "NAME";
            cbo_TP_DLV.ValueMember = "CODE";

            _flex.SetDataMap("TP_DLV", ds.Tables[0], "CODE", "NAME");
            _flex.SetDataMap("TP_DLV_DUE", ds.Tables[1], "CODE", "NAME");

            //str�׹��÷� �� ������ ����upload�� ����Ҽ��� ����.
            if (str�׹��÷� == "")
            {
                _flex.Cols[str�׹��÷� != "" ? str�׹��÷� : "SEQ_SO"].Visible = false;
                _flex.Cols[str�׹��÷� != "" ? str�׹��÷� : "SEQ_SO"].Width = 0;
                btn�������ε�.Visible = false;
            }

            //��������� �÷� ĸ�� ����
            ColsSetting("DLV_TXT_USERDEF", "SA_B000117", 1, 1);

            ��������Ǽ���();

           if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SIMMONS")
               cbo����������ڵ�1.SelectedValue = _Str����������ڵ�1;
        }

        private void ��������Ǽ���()
        {
            DataTable dtUserDefine = MA.GetCode("SA_B000118");
            DataRow[] drDLV_CD_USERDEF1 = dtUserDefine.Select("CD_FLAG1 = 'DLV_CD_USERDEF1'");

            if (drDLV_CD_USERDEF1 == null || drDLV_CD_USERDEF1.Length == 0)
                _flex.Cols["DLV_CD_USERDEF1"].Visible = false;
            else
            {
                _flex.Cols["DLV_CD_USERDEF1"].Caption = D.GetString(drDLV_CD_USERDEF1[0]["NAME"]);
                _flex.Cols["DLV_CD_USERDEF1"].Visible = true;
                lbl����������ڵ�1.Visible = cbo����������ڵ�1.Visible = true;
                lbl����������ڵ�1.Text = D.GetString(drDLV_CD_USERDEF1[0]["NAME"]);

                //�����������Ǽ���(�޺��ڽ���Ʈ��)
                SetControl str = new SetControl();
                DataTable dtCode1 = MA.GetCode("SA_B000119", true);
                DataTable dtCodeDtl1 = dtCode1.Clone();
                foreach (DataRow row in dtCode1.Select("CD_FLAG1 = 'DLV_CD_USERDEF1'"))
                    dtCodeDtl1.ImportRow(row);
                str.SetCombobox(cbo����������ڵ�1, dtCodeDtl1);
                _flex.SetDataMap("DLV_CD_USERDEF1", dtCodeDtl1, "CODE", "NAME");
            }
        }
        #endregion

        #endregion

        #region �� �׸��� �̺�Ʈ / �޼ҵ�

        #region -> �׸��� HelpClick �̺�Ʈ(_flex_BeforeCodeHelp)

        //private void _flex_HelpClick(object sender, EventArgs e)
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (_flex.Cols[_flex.Col].Name == "CD_ZIP")
                {
                    object dlg = Global.MainFrame.LoadHelpWindow("P_MA_POST", new object[] { Global.MainFrame, "" });

                    if (!_flex.HasNormalRow) return;

                    if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                    {
                        if (dlg is IHelpWindow)
                        {
                            object[] Return = (object[])((IHelpWindow)dlg).ReturnValues;
                            string cd_zip = Return[0].ToString() + Return[1].ToString();
                            string addr1 = Return[2].ToString();
                            string addr2 = Return[3].ToString();

                            _flex["CD_ZIP"] = cd_zip;
                            _flex["ADDR1"] = addr1;
                            _flex["ADDR2"] = addr2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_YnModify)
                {
                    if (D.GetString(_flex["STA_SO1"]) == "C")
                    {
                        Global.MainFrame.ShowMessage("���ְ� ���� Ȥ�� ���� �̹Ƿ� ���� �� �� �����ϴ�.");
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region �� ȭ�� �� ��ưŬ�� �̺�Ʈ

        #region -> �˻� ��ư Ŭ�� �̺�Ʈ(btn_Search_Click)

        private void btn_Search_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = null;

                object[] obj = new object[8];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //ȸ��
                obj[1] = D.GetString(partner);                                   //�ŷ�ó�ڵ�
                obj[2] = txt_NM_CUST_DLV.Text;
                obj[3] = txt_CD_ZIP.Text.Replace("-", "").Replace("_", "");
                obj[4] = txt_ADDR1.Text;
                obj[5] = txt_ADDR2.Text;
                obj[6] = txt_NO_TEL_D1.Text;
                obj[7] = txt_NO_TEL_D2.Text;

                dt = _biz.GetPartnerSearch(obj);

                if (dt != null && dt.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(����޼���.���ǿ��ش��ϴ³����̾����ϴ�);
                }
                else if (dt != null && dt.Rows.Count == 1)
                {
                    txt_NM_CUST_DLV.Text = dt.Rows[0]["NM_CUST_DLV"].ToString();
                    txt_CD_ZIP.Text = dt.Rows[0]["CD_ZIP"].ToString();
                    txt_ADDR1.Text = dt.Rows[0]["ADDR1"].ToString();
                    txt_ADDR2.Text = dt.Rows[0]["ADDR2"].ToString();
                    txt_NO_TEL_D1.Text = dt.Rows[0]["NO_TEL_D1"].ToString();
                    txt_NO_TEL_D2.Text = dt.Rows[0]["NO_TEL_D2"].ToString();
                    cbo_TP_DLV.SelectedValue = dt.Rows[0]["TP_DLV"].ToString();
                }
                else if (dt != null && dt.Rows.Count > 1)
                {
                    P_SA_SO_DLV_SUB_SELECT dlg = new P_SA_SO_DLV_SUB_SELECT(dt);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        txt_NM_CUST_DLV.Text = dlg.returnParams[0];
                        txt_CD_ZIP.Text = dlg.returnParams[1];
                        txt_ADDR1.Text = dlg.returnParams[2];
                        txt_ADDR2.Text = dlg.returnParams[3];
                        txt_NO_TEL_D1.Text = dlg.returnParams[4];
                        txt_NO_TEL_D2.Text = dlg.returnParams[5];
                        cbo_TP_DLV.SelectedValue = dlg.returnParams[6];
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> �ϰ����� ��ưŬ�� �̺�Ʈ(btn_Apply_Click)

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flex.DataTable == null || _flex.DataTable.Rows.Count == 0) return;
                
                if (_YnModify)
                {
                    DataRow[] row = _flex.DataTable.Select("STA_SO1 = 'C'");
                    if (row.Length > 0)
                    {
                        Global.MainFrame.ShowMessage("���ְ� ���� Ȥ�� ���� ���� �����Ͽ� ���� �� �� �����ϴ�.");
                        return;
                    }
                }

                foreach (DataRow dr in _flex.DataTable.Rows)
                {
                    dr["NM_CUST_DLV"] = txt_NM_CUST_DLV.Text;
                    dr["NO_TEL_D1"] = txt_NO_TEL_D1.Text;
                    dr["NO_TEL_D2"] = txt_NO_TEL_D2.Text;
                    dr["CD_ZIP"] = txt_CD_ZIP.Text.Replace("-", "");
                    dr["ADDR1"] = txt_ADDR1.Text;
                    dr["ADDR2"] = txt_ADDR2.Text;
                    dr["TP_DLV"] = cbo_TP_DLV.SelectedValue == null ? "" : cbo_TP_DLV.SelectedValue.ToString();
                    dr["DC_REQ"] = txt_DC_REQ.Text;
                    dr["DLV_CD_USERDEF1"] = D.GetString(cbo����������ڵ�1.SelectedValue);
                }

                Global.MainFrame.ShowMessage("�ϰ����� �Ǿ����ϴ�");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> Ȯ�ι�ư Ŭ�� �̺�Ʈ(btn_Ok_Click)

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (_YnModify)
                {
                    if (!_flex.HasNormalRow) return;

                    DataTable dt = _flex.GetChanges();

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        Global.MainFrame.ShowMessage("����� ������ �����ϴ�.");
                        return;
                    }

                    if (!_flex.Verify()) return;

                    bool result = _biz.Save(dt);

                    if (!result) return;

                    _flex.AcceptChanges();

                    Global.MainFrame.ShowMessage("������ �Ϸ�Ǿ����ϴ�.");

                }
                else
                {
                    int cnt = 0;

                    foreach (DataRow dr in _flex.DataTable.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SIMMONS")
                        {
                            if (dr["NM_CUST_DLV"].ToString() == "" || dr["CD_ZIP"].ToString() == "" || dr["ADDR1"].ToString() == "")
                                cnt++;
                        }
                        else
                        {
                            if (dr["NM_CUST_DLV"].ToString() == "" || dr["CD_ZIP"].ToString() == "" || dr["ADDR1"].ToString() == "" || dr["TP_DLV"].ToString() == "")
                                cnt++;
                        }
                    }

                    if (cnt != 0)
                    {
                        Global.MainFrame.ShowMessage("��������� �ʼ��Է��׸��Դϴ�. " + Environment.NewLine + " ������� �Է��� Ȯ���ϼ���.");
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn�������ε�_Click

        private void btn�������ε�_Click(object sender, EventArgs e)
        {
            try
            {
                if (_YnModify)
                {
                    DataRow[] row = _flex.DataTable.Select("STA_SO1 = 'C'");
                    if (row.Length > 0)
                        Global.MainFrame.ShowMessage("���ְ� ���� Ȥ�� ���� ���� �����Ͽ� ���ε� �� �� �����ϴ�.");
                    return;
                }
                openFileDialogUploadExcel.Filter = "���� ���� (*.xls)|*.xls";

                if (openFileDialogUploadExcel.ShowDialog() == DialogResult.OK)
                {
                    Duzon.Common.Util.Excel excel = null;

                    excel = new Duzon.Common.Util.Excel();

                    DataTable dt = excel.StartLoadExcel(openFileDialogUploadExcel.FileName, 0, 2);

                    StringBuilder sbErrorList = new StringBuilder();

                    #region -> �������� ������ �÷��� �ִ��� üũ�Ѵ�.

                    if (!dt.Columns.Contains("SEQ_SO"))
                    {
                        sbErrorList.AppendLine("�÷��� [SEQ_SO] �� ������ �������� �ʽ��ϴ�. �÷��� [SEQ_SO]�� �׹��� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("NM_CUST_DLV"))
                    {
                        sbErrorList.AppendLine("�÷��� [NM_CUST_DLV] �� ������ �������� �ʽ��ϴ�. �÷��� [NM_CUST_DLV]�� �������� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("NO_TEL_D1"))
                    {
                        sbErrorList.AppendLine("�÷��� [NO_TEL_D1] �� ������ �������� �ʽ��ϴ�. �÷��� [NO_TEL_D1]�� ��ȭ�� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("NO_TEL_D2"))
                    {
                        sbErrorList.AppendLine("�÷��� [NO_TEL_D2] �� ������ �������� �ʽ��ϴ�. �÷��� [NO_TEL_D2]�� �̵���ȭ�� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("CD_ZIP"))
                    {
                        sbErrorList.AppendLine("�÷��� [CD_ZIP] �� ������ �������� �ʽ��ϴ�. �÷��� [CD_ZIP]�� �����ȣ�� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("ADDR1"))
                    {
                        sbErrorList.AppendLine("�÷��� [ADDR1] �� ������ �������� �ʽ��ϴ�. �÷��� [ADDR1]�� �ּ�1�� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("ADDR2"))
                    {
                        sbErrorList.AppendLine("�÷��� [ADDR2] �� ������ �������� �ʽ��ϴ�. �÷��� [ADDR2]�� �ּ�2�� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("TP_DLV"))
                    {
                        sbErrorList.AppendLine("�÷��� [TP_DLV] �� ������ �������� �ʽ��ϴ�. �÷��� [TP_DLV]�� ��۹���� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("TP_DLV_DUE"))
                    {
                        sbErrorList.AppendLine("�÷��� [TP_DLV_DUE] �� ������ �������� �ʽ��ϴ�. �÷��� [TP_DLV_DUE]�� ��ǰ����� �ǹ��մϴ�.");
                    }

                    if (!dt.Columns.Contains("DC_REQ"))
                    {
                        sbErrorList.AppendLine("�÷��� [DC_REQ] �� ������ �������� �ʽ��ϴ�. �÷��� [DC_REQ]�� ��� �ǹ��մϴ�.");
                    }

                    if (sbErrorList.Length > 0)
                    {
                        Global.MainFrame.ShowDetailMessage("�������� �ʰų� �߸��� �÷� ����Դϴ�.", sbErrorList.ToString());
                        return;
                    }

                    #endregion

                    #region -> EXCEL UPLOAD �� DataTable�� ���ο� DataTable �� �ִ´�.(��Ȯ�� �÷�Ÿ���� �����ϱ� ���ؼ�)

                    //���� ���̺��� �����.(�� ������ ��Ȯ�� �÷�Ÿ���� �����ϱ� ���ؼ�)
                    DataTable dtExcel = new DataTable();
                    dtExcel.Columns.Add("SEQ_SO", typeof(decimal));
                    dtExcel.Columns.Add("NM_CUST_DLV", typeof(string));
                    dtExcel.Columns.Add("NO_TEL_D1", typeof(string));
                    dtExcel.Columns.Add("NO_TEL_D2", typeof(string));
                    dtExcel.Columns.Add("CD_ZIP", typeof(string));
                    dtExcel.Columns.Add("ADDR1", typeof(string));
                    dtExcel.Columns.Add("ADDR2", typeof(string));
                    dtExcel.Columns.Add("TP_DLV", typeof(string));
                    dtExcel.Columns.Add("TP_DLV_DUE", typeof(string));
                    dtExcel.Columns.Add("DC_REQ", typeof(string));

                    //���� ���� ���̺� �������� ���� ���Ժ��� ������ �ִ´�.
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drExcel = dtExcel.NewRow();
                        drExcel["SEQ_SO"] = _flex.CDecimal(dr["SEQ_SO"]);
                        drExcel["NM_CUST_DLV"] = dr["NM_CUST_DLV"].ToString();
                        drExcel["NO_TEL_D1"] = dr["NO_TEL_D1"].ToString();
                        drExcel["NO_TEL_D2"] = dr["NO_TEL_D2"].ToString();
                        drExcel["CD_ZIP"] = dr["CD_ZIP"].ToString();
                        drExcel["ADDR1"] = dr["ADDR1"].ToString();
                        drExcel["ADDR2"] = dr["ADDR2"].ToString();
                        drExcel["TP_DLV"] = dr["TP_DLV"].ToString();
                        drExcel["TP_DLV_DUE"] = dr["TP_DLV_DUE"].ToString();
                        drExcel["DC_REQ"] = dr["DC_REQ"].ToString();
                        dtExcel.Rows.Add(drExcel);
                    }

                    #endregion

                    #region -> ��۹���� ��ǰ����� �߸��� �κ��� ã�Ƴ���.

                    DataTable dt��۹�� = ds.Tables[0].Copy();
                    DataTable dt��ǰ��� = ds.Tables[1].Copy();

                    dt��۹��.PrimaryKey = new DataColumn[] { dt��۹��.Columns["CODE"] };
                    dt��ǰ���.PrimaryKey = new DataColumn[] { dt��ǰ���.Columns["CODE"] };

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        DataRow dr��۹�� = dt��۹��.Rows.Find(dr["TP_DLV"].ToString());
                        if (dr��۹�� == null)
                            sbErrorList.AppendLine("�÷��� [TP_DLV] �� �߸��� �ڵ尪�� �����ϴ�. �÷��� [TP_DLV]�� ��۹���� �ǹ��մϴ�. �߸��� �ڵ尪�� '" + dr["TP_DLV"].ToString() + "' �Դϴ�.");

                        DataRow dr��ǰ��� = dt��ǰ���.Rows.Find(dr["TP_DLV_DUE"].ToString());
                        if (dr��ǰ��� == null)
                            sbErrorList.AppendLine("�÷��� [TP_DLV_DUE] �� �߸��� �ڵ尪�� �����ϴ�. �÷��� [TP_DLV_DUE]�� ��ǰ����� �ǹ��մϴ�. �߸��� �ڵ尪�� '" + dr["TP_DLV_DUE"].ToString() + "' �Դϴ�.");
                    }

                    if (sbErrorList.Length > 0)
                    {
                        Global.MainFrame.ShowDetailMessage("��۹�� �Ǵ� ��ǰ����� �ڵ尡 �߸��� ����Դϴ�.", sbErrorList.ToString());
                        return;
                    }

                    #endregion

                    #region -> ��ġ�� �����ʴ� �׹��� �ִ� �κ��� ã�Ƴ���.

                    DataTable dtFelx = _flex.DataTable.Copy();

                    dtFelx.PrimaryKey = new DataColumn[] { dtFelx.Columns[str�׹��÷�] };

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        DataRow drSEQ = dtFelx.Rows.Find(dr["SEQ_SO"]);
                        if (drSEQ == null)
                            sbErrorList.AppendLine("�÷��� [SEQ_SO] �� �������� �ʴ� �׹��� �ֽ��ϴ�. �÷��� [SEQ_SO]�� �׹��� �ǹ��մϴ�. �߸��� �׹��� '" + dr["SEQ_SO"].ToString() + "' �Դϴ�.");
                    }

                    if (sbErrorList.Length > 0)
                    {
                        Global.MainFrame.ShowDetailMessage("�׸��忡 ��ġ�� ���� �ʴ� �׹��� ����ִ� ����Դϴ�.", sbErrorList.ToString());
                        return;
                    }

                    #endregion

                    DataColumn[] dcPrimary = _flex.DataTable.PrimaryKey;        //primary key �ӽ�����

                    _flex.DataTable.PrimaryKey = new DataColumn[] { _flex.DataTable.Columns[str�׹��÷�] };

                    _flex.Redraw = false;

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        DataRow drSEQ = _flex.DataTable.Rows.Find(dr["SEQ_SO"]);
                        drSEQ["NM_CUST_DLV"] = dr["NM_CUST_DLV"].ToString();
                        drSEQ["NO_TEL_D1"] = dr["NO_TEL_D1"].ToString();
                        drSEQ["NO_TEL_D2"] = dr["NO_TEL_D2"].ToString();
                        drSEQ["CD_ZIP"] = dr["CD_ZIP"].ToString();
                        drSEQ["ADDR1"] = dr["ADDR1"].ToString();
                        drSEQ["ADDR2"] = dr["ADDR2"].ToString();
                        drSEQ["TP_DLV"] = dr["TP_DLV"].ToString();
                        if (drSEQ.Table.Columns.Contains("TP_DLV_DUE"))
                            drSEQ["TP_DLV_DUE"] = dr["TP_DLV_DUE"].ToString();
                        if (drSEQ.Table.Columns.Contains("DC_REQ"))
                            drSEQ["DC_REQ"] = dr["DC_REQ"].ToString();
                    }

                    _flex.DataTable.PrimaryKey = dcPrimary;     //primary key ���󺹱�
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region -> �����ȣ ����â(btn_Zip_Click)

        private void btn_Zip_Click(object sender, EventArgs e)
        {
            try
            {
                object dlg = Global.MainFrame.LoadHelpWindow("P_MA_POST", new object[] { Global.MainFrame, "" });

                if (_flex.DataTable == null || _flex.DataTable.Rows.Count == 0) return;

                if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                {
                    if (dlg is IHelpWindow)
                    {
                        object[] Return = (object[])((IHelpWindow)dlg).ReturnValues;
                        string cd_zip = Return[0].ToString() + Return[1].ToString();
                        string addr1 = Return[2].ToString();
                        string addr2 = Return[3].ToString();

                        txt_CD_ZIP.Text = cd_zip.Replace("-", "");
                        txt_ADDR1.Text = addr1;
                        txt_ADDR2.Text = addr2;
                        txt_ADDR2.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region �� ��Ÿ �̺�Ʈ / �޼ҵ�
        #region -> ��������� �÷� ĸ�� ����

        private void ColsSetting(string colName, string cdField, int startIdx, int endIdx)
        {
            for (int i = startIdx; i <= endIdx; i++)
            {
                _flex.Cols[colName + D.GetString(i)].Visible = false;
            }
            DataTable dt = MA.GetCode(cdField);
            for (int i = startIdx; (i <= dt.Rows.Count && i <= endIdx); i++)
            {
                string Name = D.GetString(dt.Rows[i - 1]["NAME"]);
                _flex.Cols[colName + D.GetString(i)].Caption = Name;
                _flex.Cols[colName + D.GetString(i)].Visible = true;
            }
        }
        #endregion

        #region -> ErrorCheck
        public bool ErrorCheck()
        {
            return true;
        }
        #endregion

        #endregion

        #region �� ����� �������� �Ӽ���
        public DataTable ReturnTable
        {
            get
            {
                ReturnDt = _flex.DataTable;
                return ReturnDt;
            }
        } 
        #endregion  

        #region �� OnClosed
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _flex.SaveUserCache("P_SA_SO_DLV_SUB__flex");
        }

        #endregion

        #region �� �Ӽ�
        public bool YnModify { set { _YnModify = value; } }
        public string NO_SO { set { _NO_SO = value; } }
        
        public string Str����������ڵ�1 { set { _Str����������ڵ�1 = value; } } 
        #endregion
    }
}
