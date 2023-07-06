using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Common.Forms.Help;

namespace sale
{
    public partial class P_SA_SO_SPITEM_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region -> ������ & ���� ����
        private P_SA_SO_SPITEM_SUB_BIZ _biz = new P_SA_SO_SPITEM_SUB_BIZ(); 
        
        //�������� DataTable
        private DataTable ReturnDt = null;
        //������ ������ �޾Ƴ��� DataTable
        private DataTable RefreshDt = null;

        private string Base_Dt = string.Empty;
        private decimal rt_Exch = 1;
        private string _�ŷ�ó = string.Empty;
        private string _�ܰ����� = string.Empty;
        private string _ȯ�� = string.Empty;
        private string _�ܰ��������� = string.Empty;
        private string so_Price = string.Empty;
        private decimal _�ΰ����� = 1;
        private string _���� = "";
        private string _����Ȱ��ȭ���� = "Y";

        public P_SA_SO_SPITEM_SUB(object[] obj)
        {
            try
            {
                InitializeComponent();

                Base_Dt = D.GetString(obj[0]);      //�������ڸ� �������� ��ǰ�� ǰ����� ���� �´�.
                rt_Exch = D.GetDecimal(obj[1]);     //ȯ���� �����ͼ� �ܰ��� �����ش�.
                _�ŷ�ó = D.GetString(obj[2]);
                _�ܰ����� = D.GetString(obj[3]);
                _ȯ�� = D.GetString(obj[4]);
                _�ܰ��������� = D.GetString(obj[5]);
                so_Price = D.GetString(obj[6]);
                _�ΰ����� = D.GetDecimal(obj[7]);
                if (obj.Length > 8)
                    _���� = D.GetString(obj[8]);
                if (obj.Length > 9)
                    _����Ȱ��ȭ���� = D.GetString(obj[9]);

                InitGrid();

                _flexH.DetailGrids = new FlexGrid[] { _flexL };
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        } 
        #endregion

        #region -> �ʱ�ȭ �̺�Ʈ / �޼ҵ�

        #region -> InitGrid : �׸��� �ʱ�ȭ
        private void InitGrid()
        { 
            #region ����â ����׸���
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flexH.SetDummyColumn("S");       // Ŭ���ϰ� �����ϰų�, ����ϸ� ���°� ���� Changed �Ӽ����� �ٲ�鼭 ���ٰ� Ȱ��ȭ �ǹ�����.
                                              // �̷� ������ ���� �����Ҷ� �޼��� ���� ������ ���� �ϱ� ���� COM�� ����� SetDummyColumn�� �����Ѵ�. 
            _flexH.SetCol("CD_SHOP", "���������ڵ�", 120, false);
            _flexH.SetCol("NM_SHOP", "����������", 120, false);
            _flexH.SetCol("CD_SPITEM", "��ǰ�ڵ�", false);
            _flexH.SetCol("NM_SPITEM", "��ǰ��", 120, false);
            _flexH.SetCol("CD_OPT", "�ɼ��ڵ�", 120, false);
            _flexH.SetCol("NM_OPT", "�ɼǸ�", 120, 100, false);
            //_flexH.SetCol("FG_VAT", "��������", 120, false);
            _flexH.SetCol("UM_SUPPLY_SUM", "���޴ܰ�", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);// ���޴ܰ�
            _flexH.SetCol("UM_SALE_SUM", "�ǸŴܰ�", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);// �ǸŴܰ�
            _flexH.SetCol("QTY", "����", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);// ����
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.ValidateEdit += new ValidateEditEventHandler(_flexH_ValidateEdit);
            _flexH.AfterRowChange +=new RangeEventHandler(_flexH_AfterRowChange);

            //����ڱ׸������ ��� : �ݵ��� EndSetting ������ �ڵ�������Ѵ�. : �ݵ��� �Ķ���ͺ����� Page�� + Grid�� ���� �Ѵ�.
            _flexH.LoadUserCache("P_SA_SO_SPITEM_SUB_flexH");
            #endregion

            #region ����â ���α׸���
            _flexL.BeginSetting(1, 1, false);
            //_flexL.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            //_flexL.SetDummyColumn("S"); // Ŭ���ϰ� �����ϰų�, ����ϸ� ���°� ���� Changed �Ӽ����� �ٲ�鼭 ���ٰ� Ȱ��ȭ �ǹ�����.
            //                              // �̷� ������ ���� �����Ҷ� �޼��� ���� ������ ���� �ϱ� ���� COM�� ����� SetDummyColumn�� �����Ѵ�. 
            _flexL.SetCol("CD_PLANT", "����", 100, false);// ����
            _flexL.SetCol("CD_ITEM", "ǰ���ڵ�", 100, true);// ǰ��
            _flexL.SetCol("NM_ITEM", "ǰ���", 100, false);// ǰ��
            _flexL.SetCol("STND_ITEM", "�԰�", 70, false);// �԰�
            _flexL.SetCol("UNIT", "����", 80, 3, false);// ����
            _flexL.SetCol("CD_SL", "â���ڵ�", 80, true);
            _flexL.SetCol("NM_SL", "â���", 120, false);
            _flexL.SetCol("QT_INV", "��������", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_SO", "����", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);// ����
            _flexL.SetCol("UM_SO", "���޴ܰ�", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);     // �ǸŴܰ�
            _flexL.SetCol("AM_SO", "���ޱݾ�", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);         // �Ǹűݾ�
            _flexL.SetCol("AM_WONAMT", "��ȭ�ݾ�", 100, 17, false, typeof(decimal), FormatTpType.MONEY);            // ��ȭ�ݾ�
            _flexL.SetCol("UMVAT_SO", "�ǸŴܰ�", 100, 17, false, typeof(decimal), FormatTpType.MONEY);              // ���޴ܰ�
            _flexL.SetCol("AMVAT_SO", "�Ǹűݾ�", 100, 17, false, typeof(decimal), FormatTpType.MONEY);              // ���ޱݾ�
            _flexL.SetCol("AM_VAT", "�ΰ���", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("RT_VAT", "�ΰ�����", 0, 0, false, typeof(decimal), FormatTpType.MONEY);

            _flexL.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_SL", "NM_SL", "UNIT_SO_FACT" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_GISL", "NM_GISL", "UNIT_SO_FACT" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flexL.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flexL.VerifyCompare(_flexL.Cols["QT_SO"], 0, OperatorEnum.Greater);
            _flexL.VerifyCompare(_flexL.Cols["UM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flexL_BeforeCodeHelp);
            _flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexL_AfterCodeHelp);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);

            //����� �ʿ���� SUM ���� && �ݵ��� EndSetting ������ �ڵ�������Ѵ�.
            _flexL.SetExceptSumCol("UM_SO", "UMVAT_SO");

            //����ڱ׸������ ��� : �ݵ��� EndSetting ������ �ڵ�������Ѵ�. : �ݵ��� �Ķ���ͺ����� Page�� + Grid�� ���� �Ѵ�.
            _flexL.LoadUserCache("P_SA_SO_SPITEM_SUB_flexL");
            #endregion
        } 
        #endregion

        #region -> InitPaint : ������ �ʱ�ȭ
        //PageBase �� �θ��Լ� �̿� : ���� �ö�� �� ������ �ö�� ��Ʈ�ѵ��� �ʱ�ȭ �����ִ� ������ �Ѵ�.
        //Combo Box ���ý� �ɼ� => N:�������, S:�����߰�, SC:����&��ȣ�߰�, NC:��ȣ�߰�
        //���� �ڵ尪���� DB ���� ���س��� �Ķ���Ϳ� ��ġ��������Ѵ�
        protected override void InitPaint()
        {
            DataSet g_dsCombo = Global.MainFrame.GetComboData("NC;MA_PLANT", "S;MA_B000040");

            //����
            cbo_Plant.DataSource = g_dsCombo.Tables[0];
            cbo_Plant.DisplayMember = "NAME";
            cbo_Plant.ValueMember = "CODE";
            _flexL.SetDataMap("CD_PLANT", g_dsCombo.Tables[0].Copy(), "CODE", "NAME");

            if (_���� != "")
            {
                DataRow[] drs = g_dsCombo.Tables[0].Select("CODE = '" + _���� + "' ");
                if (drs.Length == 1)
                {
                    cbo_Plant.SelectedValue = _����;

                    if (_����Ȱ��ȭ���� != "Y")
                        cbo_Plant.Enabled = false;
                }
            }

            //��������
            //_flexH.SetDataMap("FG_VAT", g_dsCombo.Tables[1], "CODE", "NAME");

            txt_SPITEM_Search.Text = string.Empty;
            bp_Pitem.CodeValue = string.Empty;
            bp_Pitem.CodeName = string.Empty;
            bp_CdSl.CodeValue = string.Empty;
            bp_CdSl.CodeName = string.Empty;

            //�ڵ���ȸ ����
            //object[] obj = new object[5];
            //obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            //obj[1] = cbo_SPTYPE.SelectedValue == null ? string.Empty : cbo_SPTYPE.SelectedValue.ToString();
            //obj[2] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
            //obj[3] = Base_Dt;
            //obj[4] = rt_Exch;
            //DataSet ds = _biz.Search(obj);

            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    dr["AM_SO"] = Decimal.Round(D.GetDecimal(dr["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(dr["RT_VAT"]))));
            //    dr["AM_VAT"] = D.GetDecimal(dr["AMVAT_SO"]) - D.GetDecimal(dr["AM_SO"]);
            //    dr["AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(dr["AM_SO"]) * rt_Exch);
            //    dr["UM_SO"] = D.GetDecimal(dr["AM_SO"]) / (D.GetDecimal(dr["QT_SO"]) == 0 ? 1 : D.GetDecimal(dr["QT_SO"]));
            //}

            //_flexH.Binding = ds.Tables[0];
            //_flexL.Binding = ds.Tables[1];
            //_flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'";

            //if(ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count != 0)  
            //    RefreshDt = ds.Tables[1].Copy();
        }
        #endregion

        #endregion 
 
        #region -> ��ưŬ�� EVENT 
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    #region �� ��ǰ�˻� ��ưŬ��
                    case "btn_SPTYPE_Search":

                        if (txt_SPITEM_Search.Text == string.Empty) return;
                        if (!_flexH.HasNormalRow) return;

                        //_flexH.RowSel : ���� ���õǾ� �ִ� ��, ��Ŀ���� ������ �ִ� �ο��� idx�� �����´�.
                        for (int idx = _flexH.RowSel - 1; idx < _flexH.DataTable.Rows.Count; idx++)
                        {
                            if (D.GetString(_flexH[idx + 1, "CD_SPITEM"]).Contains(txt_SPITEM_Search.Text) ||
                                D.GetString(_flexH[idx + 1, "NM_SPITEM"]).Contains(txt_SPITEM_Search.Text) ||
                                D.GetString(_flexH[idx + 1, "CD_OPT"]).Contains(txt_SPITEM_Search.Text) ||
                                D.GetString(_flexH[idx + 1, "NM_OPT"]).Contains(txt_SPITEM_Search.Text))
                            {
                                _flexH.Select(idx + 1, _flexH.Cols["CD_SPITEM"].Index);
                                _flexH.Focus();
                                break;
                            }

                            if (idx + 1 == _flexH.DataTable.Rows.Count)
                            {
                                Global.MainFrame.ShowMessage("�˻��� ���� �����ϴ�!");
                                break;
                            }
                        }

                        break;

                    #endregion

                    #region �� ǰ��˻� ��ưŬ��
                    case "btn_Pitem_Search":

                        if (bp_Pitem.CodeValue == string.Empty) return;
                        if (!_flexH.HasNormalRow) return;

                        //_flexL.RowSel : ���� ���õǾ� �ִ� ��, ��Ŀ���� ������ �ִ� �ο��� idx�� �����´�.
                        int idx_Cnt = 1;
                        foreach (DataRow dr_Item in _flexH.DataTable.Rows)
                        {
                            //����� ��Ŀ��
                            _flexH.Select(idx_Cnt, _flexH.Cols["CD_SPITEM"].Index);
                            _flexH.Focus();

                            _flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(dr_Item["CD_SPITEM"]) + "'";
                            for (int idx_Item = 0; idx_Item < _flexL.DataView.Count; idx_Item++)
                            {
                                if (D.GetString(_flexL[idx_Item + 1, "CD_ITEM"]) == bp_Pitem.CodeValue)
                                { 
                                    //���ο� ��Ŀ�� (����� �ִ� �÷��� ������� Error �� �ȳ�)
                                    _flexL.Select(idx_Item + 1, _flexH.Cols["CD_SPITEM"].Index);
                                    _flexL.Focus();
                                    return;
                                } 
                            }

                            //����� ��� ������ ���Ƽ� ������ ��� ���� �˻������� ������ Exception ó�� 
                            //����� ������ ���ڿ� ������ڿ� �����ϸ� ��� �˻� ������ ���Ҵ�.
                            if (idx_Cnt == _flexH.DataTable.Rows.Count)
                            {
                                Global.MainFrame.ShowMessage("�˻��� ���� �����ϴ�!");
                                break;
                            }
                            idx_Cnt++;

                        }

                        break;

                    #endregion

                    #region �� ��ȸ ��ưŬ��
                    case "btn_Search":

                        if (MA.ServerKey(true, new string[] { "NIKON" }))
                        {
                            if (ctx��������.IsEmpty())
                            {
                                Global.MainFrame.ShowMessage(����޼���._�����ʼ��Է��׸��Դϴ�, lbl_��������.Text);
                                return;
                            }
                        }

                        object[] obj = new object[11];
                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                        obj[1] = ctx��������.CodeValue;
                        obj[2] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
                        obj[3] = Base_Dt;
                        obj[4] = rt_Exch;
                        obj[5] = _�ܰ���������;
                        obj[6] = _�ŷ�ó;
                        obj[7] = _�ܰ�����;
                        obj[8] = _ȯ��;
                        obj[9] = _�ΰ�����;
                        obj[10] = bp_Pitem.CodeValue;

                        DataSet ds = _biz.Search(obj);

                        DataTable dtH = ds.Tables[0];
                        DataTable dtL = ds.Tables[1];

                        foreach (DataRow rowH in dtH.Rows)
                        {
                            string Filter = "ISNULL(CD_SHOP, '')   = '" + D.GetString(rowH["CD_SHOP"])   + "' AND " +
                                            "ISNULL(CD_SPITEM, '') = '" + D.GetString(rowH["CD_SPITEM"]) + "' AND " +
                                            "ISNULL(CD_OPT, '')    = '" + D.GetString(rowH["CD_OPT"])    + "'";
                            DataRow[] drs = dtL.Select(Filter);

                            decimal ���ǸŴܰ�_BASE = decimal.Truncate(D.GetDecimal(rowH["UM_SALE_SUM_1"])); //��ǰ�ڵ��Ͽ��� ��ϵ� �ǸŴܰ��� �� 
                            decimal �Ѱ��޴ܰ� = decimal.Truncate(D.GetDecimal(rowH["UM_SUPPLY_SUM"]));
                            decimal ���ǸŴܰ� = decimal.Truncate(D.GetDecimal(rowH["UM_SALE_SUM"]));
                            decimal �ǸŹ��sum = 0m, ���޹��sum = 0m, Max�ݾ� = 0m;
                            int i = 0, idx_Max�ݾ� = 0; ;

                            foreach (DataRow rowL in drs)
                            {
                                rowL["AMVAT_SO"] = ���ǸŴܰ�_BASE == 0 ? 0 : (decimal.Round(D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]) / ���ǸŴܰ�_BASE * ���ǸŴܰ�));
                                rowL["AM_SO"] = ���ǸŴܰ�_BASE == 0 ? 0 : decimal.Round(D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]) / ���ǸŴܰ�_BASE * �Ѱ��޴ܰ�);
                                rowL["UMVAT_SO"] = decimal.Round(D.GetDecimal(rowL["QT_SO"]) == 0 ? 0 : (D.GetDecimal(rowL["AMVAT_SO"]) / D.GetDecimal(rowL["QT_SO"])), 4, MidpointRounding.AwayFromZero);
                                rowL["UM_SO"] = decimal.Round(D.GetDecimal(rowL["QT_SO"]) == 0 ? 0 : (D.GetDecimal(rowL["AM_SO"]) / D.GetDecimal(rowL["QT_SO"])), 4, MidpointRounding.AwayFromZero);
                                rowL["AM_WONAMT"] = decimal.Truncate(D.GetDecimal(rowL["AM_SO"]) * rt_Exch);
                                rowL["AM_VAT"] = D.GetDecimal(rowL["AMVAT_SO"]) - D.GetDecimal(rowL["AM_WONAMT"]);
                                �ǸŹ��sum += D.GetDecimal(rowL["AMVAT_SO"]);
                                ���޹��sum += D.GetDecimal(rowL["AM_SO"]);

                                if (Max�ݾ� < D.GetDecimal(rowL["AMVAT_SO"]))
                                {
                                    Max�ݾ� = D.GetDecimal(rowL["AMVAT_SO"]);
                                    idx_Max�ݾ� = i;
                                }
                                i++;
                            }

                            //System.Diagnostics.Debug.WriteLine("idx_Max�ݾ� -> " + D.GetString(idx_Max�ݾ�) + "drs : " + D.GetString(drs.Length));

                            if (drs.Length > 0)
                            {
                                drs[idx_Max�ݾ�]["AMVAT_SO"] = D.GetDecimal(drs[idx_Max�ݾ�]["AMVAT_SO"]) + (���ǸŴܰ� - �ǸŹ��sum);
                                drs[idx_Max�ݾ�]["AM_SO"] = D.GetDecimal(drs[idx_Max�ݾ�]["AM_SO"]) + (�Ѱ��޴ܰ� - ���޹��sum);
                                drs[idx_Max�ݾ�]["AM_WONAMT"] = decimal.Truncate(D.GetDecimal(drs[idx_Max�ݾ�]["AM_SO"]) * rt_Exch);
                                drs[idx_Max�ݾ�]["AM_VAT"] = D.GetDecimal(drs[idx_Max�ݾ�]["AMVAT_SO"]) - D.GetDecimal(drs[idx_Max�ݾ�]["AM_WONAMT"]);
                            }
                            
                            /*foreach (DataRow rowL in drL)
                            {
                                RowCount++;

                                if (RowCount != drL.Length)
                                {
                                    percent = D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]) / ���ǸŴܰ�;
                                    rowL["UMVAT_SO"] = Decimal.Round(percent * D.GetDecimal(rowH["UM_SALE_SUM"]));
                                    rowL["AMVAT_SO"] = D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]);
                                    rowL["AM_SO"] = Decimal.Round(D.GetDecimal(rowL["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(rowL["RT_VAT"]))));//�Ѱ��޴ܰ� * percent;

                                    �ǸŹ��sum += D.GetDecimal(rowL["UMVAT_SO"]);
                                    ���޹��sum += D.GetDecimal(rowL["AM_SO"]);
                                }
                                else
                                {
                                    rowL["UMVAT_SO"] = D.GetDecimal(rowH["UM_SALE_SUM"]) - �ǸŹ��sum;
                                    rowL["AMVAT_SO"] = D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]);
                                    rowL["AM_SO"] = �Ѱ��޴ܰ� - ���޹��sum;
                                }

                                rowL["AM_VAT"] = D.GetDecimal(rowL["AMVAT_SO"]) - D.GetDecimal(rowL["AM_SO"]);
                                rowL["AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(rowL["AM_SO"]) * rt_Exch);
                                rowL["UM_SO"] = D.GetDecimal(rowL["AM_SO"]) / (D.GetDecimal(rowL["QT_SO"]) == 0 ? 1 : D.GetDecimal(rowL["QT_SO"]));
                            }*/
                        }

                        RefreshDt = dtL.Copy();

                        _flexH.Binding = dtH;
                        _flexL.Binding = dtL;

                        if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                        {
                            btn_Apply01.Enabled = false;
                            Global.MainFrame.ShowMessage(����޼���.���ǿ��ش��ϴ³����̾����ϴ�);
                            return;
                        }
                        else
                            btn_Apply01.Enabled = true;

                        //_flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'";
                        _flexL.RowFilter = "ISNULL(CD_SHOP, '') = '" + D.GetString(_flexH["CD_SHOP"]) + "' AND " +
                                           "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "' AND "+
                                           "ISNULL(CD_OPT, '') = '" + D.GetString(_flexH["CD_OPT"]) + "'";

                        break;
                    #endregion 

                    #region �� â������ ��ưŬ��
                    case "btn_Apply02":

                        if (bp_CdSl.CodeValue == string.Empty) return;
                        if (_flexL == null || _flexL.DataTable == null || _flexL.DataTable.Rows.Count == 0)
                        {
                            Global.MainFrame.ShowMessage("��ȸ �� ���� �Ͻñ� �ٶ��ϴ�.");
                            return;
                        }

                        //â�� �ϰ������Ѵ�.
                        _flexL.Redraw = false;

                        DataRow[] rows = _flexL.DataTable.Select("ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'");

                        foreach (DataRow row in rows)
                        {
                            row["CD_SL"] = bp_CdSl.CodeValue;
                            row["NM_SL"] = bp_CdSl.CodeName;
                        }
                        _flexL.Redraw = true;

                        Global.MainFrame.ShowMessage(����޼���._�۾����Ϸ��Ͽ����ϴ�, btn_Apply02.Text);

                        break;
                    #endregion

                    #region �� ������ ��ưŬ��
                    case "btn_ReSearch":

                        for (int idx_DelRow = _flexL.DataView.Count + 2; idx_DelRow > 2; idx_DelRow--)
                        {
                            _flexL.RemoveItem(_flexL.RowSel);
                        }

                        DataTable dt_Refresh = RefreshDt.Copy();
                        DataRow[] drs_ReFresh = dt_Refresh.Select("ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'");
                        if (drs_ReFresh == null || drs_ReFresh.Length == 0) return;
                        foreach (DataRow dr_AddRow in drs_ReFresh)
                        {
                            _flexL.DataTable.ImportRow(dr_AddRow);
                        }

                        _flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'";

                        //���ο� ��Ŀ�� (����� �ִ� �÷��� ������� Error �� �ȳ�)
                        _flexL.Select(2, _flexH.Cols["CD_SPITEM"].Index);
                        _flexL.Focus();

                        break;
                    #endregion

                    #region �� �����߰� ��ưŬ��
                    case "btn_AddRow":
                        _flexL.Rows.Add();
                        _flexL.Row = _flexL.Rows.Count - 1;
                        _flexL["CD_SHOP"] = D.GetString(_flexH["CD_SHOP"]);
                        _flexL["CD_SPITEM"] = D.GetString(_flexH["CD_SPITEM"]);
                        _flexL["CD_OPT"] = D.GetString(_flexH["CD_OPT"]);
                        _flexL["FG_VAT"] = D.GetString(_flexH["FG_VAT"]);
                        _flexL["RT_VAT"] = D.GetDecimal(_flexH["RT_VAT"]);
                        _flexL["CD_PLANT"] = cbo_Plant.SelectedValue.ToString();
                        _flexL["QT_SO"] = 0;
                        _flexL["UM_SO"] = 0;
                        _flexL["AM_SO"] = 0;
                        _flexL["UMVAT_SO"] = 0;
                        _flexL["AMVAT_SO"] = 0;
                        _flexL["AM_WONAMT"] = 0;
                        _flexL["AM_VAT"] = 0;
                        _flexL.Col = _flexL.Cols.Fixed;
                        _flexL.AddFinished();
                        _flexL.Focus();

                        break;
                    #endregion

                    #region �� ���λ��� ��ưŬ��
                    case "btn_DelRow":

                    if(_flexL.HasNormalRow)
                        _flexL.RemoveItem(_flexL.Row);

                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> Control_QueryBefore
        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB:
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    e.HelpParam.P09_CD_PLANT = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
                    break;
                case HelpID.P_USER:
                    ctx��������.UserParams = "������������â;H_EC_SPTYPE";
                    break;
            }
        }

        #endregion

        #region -> �׸��� EVENT 

        #region -> ValidateEdit Event

        #region -> _flexH_ValidateEdit
        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
            string newValue = ((FlexGrid)sender).EditData;

            if (oldValue.ToUpper() == newValue.ToUpper()) return;

            try
            {
                decimal ���ǸŴܰ�_BASE = 0m, ���ǸŴܰ� = 0m, �Ѱ��޴ܰ� = 0m;
                decimal �ǸŹ��sum = 0m, ���޹��sum = 0m, Max�ݾ� = 0m;
                int idx_Max�ݾ� = 0;

                switch (_flexH.Cols[e.Col].Name)
                {
                    case "UM_SALE_SUM":

                        ���ǸŴܰ�_BASE = decimal.Truncate(D.GetDecimal(oldValue)); //��ǰ�ڵ��Ͽ��� ��ϵ� �ǸŴܰ��� �� 
                        ���ǸŴܰ� = decimal.Truncate(D.GetDecimal(newValue) * (D.GetDecimal(_flexH[e.Row, "QTY"])));
                        _flexH[e.Row, "UM_SUPPLY_SUM"] = D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (100 / (100 + _�ΰ�����) * (D.GetDecimal(_flexH[e.Row, "QTY"])));
                        �Ѱ��޴ܰ� = decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SUPPLY_SUM"]));

                        for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            _flexL[idx, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, ���ǸŴܰ�_BASE == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / ���ǸŴܰ�_BASE * ���ǸŴܰ�)));
                            _flexL[idx, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, ���ǸŴܰ�_BASE == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / ���ǸŴܰ�_BASE * �Ѱ��޴ܰ�)));
                            _flexL[idx, "UMVAT_SO"] = Unit.��ȭ�ܰ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "AMVAT_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4, MidpointRounding.AwayFromZero)));
                            _flexL[idx, "UM_SO"] = Unit.��ȭ�ܰ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "AM_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4 , MidpointRounding.AwayFromZero)));
                            _flexL[idx, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch));
                            _flexL[idx, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_WONAMT"]));
                            �ǸŹ��sum += D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                            ���޹��sum += D.GetDecimal(_flexL[idx, "AM_SO"]);

                            if (Max�ݾ� < D.GetDecimal(_flexL[idx, "AMVAT_SO"]))
                            {
                                Max�ݾ� = D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                                idx_Max�ݾ� = idx;
                            }
                        }

                        _flexL[idx_Max�ݾ�, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AMVAT_SO"]) + (���ǸŴܰ� - �ǸŹ��sum));
                        _flexL[idx_Max�ݾ�, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_SO"]) + (�Ѱ��޴ܰ� - ���޹��sum));
                        _flexL[idx_Max�ݾ�, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_SO"]) * rt_Exch);
                        _flexL[idx_Max�ݾ�, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_WONAMT"]));

                        //---------------------------------------------------------------------------------------

                        /*for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            if (D.GetDecimal(_flexL[idx, "UMVAT_SO"]) == 0) continue;

                            _flexH[e.Row, "UM_SUPPLY_SUM"] = D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (100 / (100 + _�ΰ�����));

                            if (idx != rowCount)
                            {
                                percent = D.GetDecimal(_flexL[idx, "UMVAT_SO"]) / D.GetDecimal(oldValue);
                                _flexL[idx, "UMVAT_SO"] = Decimal.Round(percent * D.GetDecimal(newValue));
                                _flexL[idx, "AMVAT_SO"] = D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]);
                                _flexL[idx, "AM_SO"] = Decimal.Round(D.GetDecimal(_flexL[idx, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[idx, "RT_VAT"]))));

                                �ǸŹ��sum += D.GetDecimal(_flexL[idx, "UMVAT_SO"]);
                                ���޹��sum += D.GetDecimal(_flexL[idx, "AM_SO"]);
                            }
                            else
                            {
                                _flexL[idx, "UMVAT_SO"] = D.GetDecimal(newValue) - �ǸŹ��sum;
                                _flexL[idx, "AMVAT_SO"] = D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]);
                                _flexL[idx, "AM_SO"] = D.GetDecimal(_flexH["UM_SUPPLY_SUM"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) - ���޹��sum;
                            }

                            _flexL[idx, "AM_VAT"] = D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_SO"]);
                            _flexL[idx, "AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch);
                            _flexL[idx, "UM_SO"] = D.GetDecimal(_flexL[idx, "AM_SO"]) / (D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[idx, "QT_SO"]));
                        }*/
                        break;

                    case "UM_SUPPLY_SUM":

                        ���ǸŴܰ�_BASE = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (D.GetDecimal(_flexH[e.Row, "QTY"])))); //���� �Ǳ� ���� �ǸŴܰ��� �� 
                        _flexH[e.Row, "UM_SALE_SUM"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(newValue) * ((100 + _�ΰ�����) / 100));
                        ���ǸŴܰ� = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (D.GetDecimal(_flexH[e.Row, "QTY"]))));
                        �Ѱ��޴ܰ� = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(newValue) * (D.GetDecimal(_flexH[e.Row, "QTY"]))));

                        for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            _flexL[idx, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, ���ǸŴܰ�_BASE == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / ���ǸŴܰ�_BASE * ���ǸŴܰ�));
                            _flexL[idx, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, ���ǸŴܰ�_BASE == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / ���ǸŴܰ�_BASE * �Ѱ��޴ܰ�));
                            _flexL[idx, "UMVAT_SO"] = Unit.��ȭ�ܰ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "AMVAT_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4, MidpointRounding.AwayFromZero));
                            _flexL[idx, "UM_SO"] = Unit.��ȭ�ܰ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "AM_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4, MidpointRounding.AwayFromZero));
                            _flexL[idx, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch));
                            _flexL[idx, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_WONAMT"]));
                            �ǸŹ��sum += D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                            ���޹��sum += D.GetDecimal(_flexL[idx, "AM_SO"]);

                            if (Max�ݾ� < D.GetDecimal(_flexL[idx, "AMVAT_SO"]))
                            {
                                Max�ݾ� = D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                                idx_Max�ݾ� = idx;
                            }
                        }

                        _flexL[idx_Max�ݾ�, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AMVAT_SO"]) + (���ǸŴܰ� - �ǸŹ��sum));
                        _flexL[idx_Max�ݾ�, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_SO"]) + (�Ѱ��޴ܰ� - ���޹��sum));
                        _flexL[idx_Max�ݾ�, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_SO"]) * rt_Exch));
                        _flexL[idx_Max�ݾ�, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_WONAMT"]));
                        break;

                    case "QTY":

                        ���ǸŴܰ� = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * D.GetDecimal(_flexH["QTY"])));
                        �Ѱ��޴ܰ� = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SUPPLY_SUM"]) * D.GetDecimal(_flexH["QTY"])));

                        for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            _flexL[idx, "UNIT_SO_FACT"] = D.GetDecimal(_flexL[idx, "UNIT_SO_FACT"]) == 0 ? 1 : _flexL[idx, "UNIT_SO_FACT"];
                            _flexL[idx, "QT_SO"] = Unit.����(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_BASE_SO"]) * D.GetDecimal(_flexH["QTY"]));
                            _flexL[idx, "QT_IM"] = Unit.����(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_BASE_SO"]) * D.GetDecimal(_flexH["QTY"]) * D.GetDecimal(_flexL[idx, "UNIT_SO_FACT"]));

                            decimal new�Ǹűݾ� = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) * D.GetDecimal(_flexL[idx, "UMVAT_SO"]));
                            decimal new���ޱݾ� = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) * D.GetDecimal(_flexL[idx, "UM_SO"]));
                            //----------------------------------------
                            _flexL[idx, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, new�Ǹűݾ�);
                            _flexL[idx, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, new���ޱݾ�);
                            _flexL[idx, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, new�Ǹűݾ� - new���ޱݾ�);
                            _flexL[idx, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch));

                            �ǸŹ��sum += new�Ǹűݾ�;
                            ���޹��sum += new���ޱݾ�;

                            if (Max�ݾ� < D.GetDecimal(_flexL[idx, "AMVAT_SO"]))
                            {
                                Max�ݾ� = D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                                idx_Max�ݾ� = idx;
                            }
                        }

                        _flexL[idx_Max�ݾ�, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AMVAT_SO"]) + (���ǸŴܰ� - �ǸŹ��sum));
                        _flexL[idx_Max�ݾ�, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_SO"]) + (�Ѱ��޴ܰ� - ���޹��sum));
                        _flexL[idx_Max�ݾ�, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_SO"]) * rt_Exch));
                        _flexL[idx_Max�ݾ�, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max�ݾ�, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx_Max�ݾ�, "AM_WONAMT"]));
                            
                            //----------------------------------------
                            /*if (idx != rowCount)
                            {
                                _flexL[idx, "AMVAT_SO"] = new�Ǹűݾ�;
                                _flexL[idx, "AM_SO"] = new���ޱݾ�;
                                _flexL[idx, "AM_VAT"] = new�Ǹűݾ� - new���ޱݾ�;
                                _flexL[idx, "AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch);

                                �ǸŹ��sum += new�Ǹűݾ�;
                                ���޹��sum += new���ޱݾ�;
                            }
                            else
                            {
                                _flexL[idx, "AMVAT_SO"] = D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) - �ǸŹ��sum;
                                _flexL[idx, "AM_SO"] = D.GetDecimal(_flexH[e.Row, "UM_SUPPLY_SUM"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) - ���޹��sum;
                                _flexL[idx, "AM_VAT"] = D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_SO"]);
                                _flexL[idx, "AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch);
                            }
                        }*/
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        
        #endregion

        #region -> _flexL_ValidateEdit
        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (_flexL.Cols[e.Col].Name)
                {
                    case "QT_SO":

                        _flexL[e.Row, "UNIT_SO_FACT"] = D.GetDecimal(_flexL[e.Row, "UNIT_SO_FACT"]) == 0 ? 1 : _flexL[e.Row, "UNIT_SO_FACT"];
                        _flexL[e.Row, "QT_IM"] = Unit.����(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "QT_SO"]) * D.GetDecimal(_flexL[e.Row, "UNIT_SO_FACT"]));
                        _flexL[e.Row, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "UMVAT_SO"]) * D.GetDecimal(_flexL[e.Row, "QT_SO"]));
                        _flexL[e.Row, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, Decimal.Round(D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[e.Row, "RT_VAT"])))));
                        _flexL[e.Row, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flexL[e.Row, "AM_SO"]));
                        _flexL[e.Row, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[e.Row, "AM_SO"]) * rt_Exch));
                        _flexL[e.Row, "UM_SO"] = Unit.��ȭ�ܰ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AM_SO"]) / (D.GetDecimal(_flexL[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[e.Row, "QT_SO"])));

                        break;
                    case "UMVAT_SO": 
                        _flexL[e.Row, "AMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "QT_SO"]) * D.GetDecimal(_flexL[e.Row, "UMVAT_SO"]));
                        _flexL[e.Row, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, Decimal.Round(D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[e.Row, "RT_VAT"])))));
                        _flexL[e.Row, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flexL[e.Row, "AM_SO"]));
                        _flexL[e.Row, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[e.Row, "AM_SO"]) * rt_Exch));
                        _flexL[e.Row, "UM_SO"] = Unit.��ȭ�ܰ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AM_SO"]) / (D.GetDecimal(_flexL[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[e.Row, "QT_SO"])));

                        break;
                    case "AMVAT_SO": 
                        if (D.GetDecimal(_flexL[e.Row, "QT_SO"]) != 0)
                            _flexL[e.Row, "UMVAT_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) / D.GetDecimal(_flexL[e.Row, "QT_SO"]));
                        else
                            _flexL[e.Row, "UMVAT_SO"] = 0;

                        _flexL[e.Row, "AM_SO"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, Decimal.Round(D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[e.Row, "RT_VAT"])))));
                        _flexL[e.Row, "AM_VAT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flexL[e.Row, "AM_SO"]));
                        _flexL[e.Row, "AM_WONAMT"] = Unit.��ȭ�ݾ�(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[e.Row, "AM_SO"]) * rt_Exch));
                        _flexL[e.Row, "UM_SO"] = Unit.��ȭ�ܰ�(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AM_SO"]) / (D.GetDecimal(_flexL[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[e.Row, "QT_SO"])));

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> _flexH_AfterRowChange
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {  
            try
            {
                _flexL.RowFilter = "ISNULL(CD_SHOP, '') = '" + D.GetString(_flexH["CD_SHOP"]) + "' AND " +
                                   "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "' AND " +
                                   "ISNULL(CD_OPT, '') = '" + D.GetString(_flexH["CD_OPT"]) + "'";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flexL_BeforeCodeHelp
        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (_flexL.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                    case "CD_SL":
                        e.Parameter.P09_CD_PLANT = _flexL[e.Row, "CD_PLANT"].ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flexL_AfterCodeHelp
        private void _flexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;
                string ���� = string.Empty;

                switch (_flexL.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (e.Result.DialogResult == DialogResult.Cancel) return;

                        if (_flexL[e.Row, "CD_PLANT"].ToString() != string.Empty)
                            ���� = _flexL[e.Row, "CD_PLANT"].ToString();

                        _flexL.Redraw = false;
                        _flexL.SetDummyColumnAll();
                        foreach (DataRow row in helpReturn.Rows)
                        {
                            _flexL[e.Row, "CD_ITEM"] = row["CD_ITEM"];
                            _flexL[e.Row, "NM_ITEM"] = row["NM_ITEM"];
                            _flexL[e.Row, "STND_ITEM"] = row["STND_ITEM"];
                            _flexL[e.Row, "CD_SL"] = row["CD_GISL"];
                            _flexL[e.Row, "NM_SL"] = row["NM_GISL"];

                            _flexL[e.Row, "UNIT_SO"] = row["UNIT_SO"];
                            _flexL[e.Row, "UNIT_IM"] = row["UNIT_IM"];
                            _flexL[e.Row, "TP_ITEM"] = row["TP_ITEM"];

                            _flexL[e.Row, "UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];
                            _flexL[e.Row, "QT_IM"] = Unit.����(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "QT_SO"]) * D.GetDecimal(row["UNIT_SO_FACT"]));
                        }
                        _flexL.RemoveDummyColumnAll();
                        _flexL.AddFinished();
                        _flexL.Col = _flexL.Cols.Fixed;
                        _flexL.Redraw = true;
                        break; 
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> ����� �������� �Ӽ��� 
        public DataTable ReturnDataTable
        {
            get
            {
                DataRow[] drs = _flexH.DataTable.Select("S = 'Y'");

                if (drs == null || drs.Length == 0) return null;

                ReturnDt = _flexL.DataTable.Clone();
                foreach (DataRow dr in drs)
                {
                    DataRow[] drs_Line = _flexL.DataTable.Select("ISNULL(CD_SPITEM, '') = '" + D.GetString(dr["CD_SPITEM"]) + "'");

                    foreach (DataRow dr_L in drs_Line)
                    {
                        DataRow row = ReturnDt.NewRow();
                        row = dr_L;
                        ReturnDt.Rows.Add(row.ItemArray);
                    }
                }

                return ReturnDt;
            }
        } 
        #endregion

        #region -> OnClosed(ȭ���� ������)
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //����ڱ׸������ ��� : �ݵ��� �Ķ���ͺ����� Page�� + Grid�� ���� �Ѵ�.
            _flexH.SaveUserCache("P_SA_SO_SPITEM_SUB_flexH");
            _flexL.SaveUserCache("P_SA_SO_SPITEM_SUB_flexL");
        }
        #endregion
    }
}