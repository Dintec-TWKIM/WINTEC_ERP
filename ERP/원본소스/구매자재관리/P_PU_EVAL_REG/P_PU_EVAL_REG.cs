using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Util;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;


namespace pur
{
    public partial class P_PU_EVAL_REG : Duzon.Common.Forms.PageBase
    {

        private P_PU_EVAL_REG_BIZ _biz;
        private string _cddept;

        #region ♣ 초기화

        public P_PU_EVAL_REG()
        {
            try
            {
                InitializeComponent();
                this.MainGrids = new FlexGrid[] { _flex };

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_PU_EVAL_REG_BIZ(this.MainFrameInterface);
            InitDD(_tlayMain);
            InitGrid();
            
        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flex.SetCol("CD_PJT", "프로젝트", 80);
                _flex.SetCol("NM_PROJECT", "프로젝트명", 100);

                _flex.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                _flex.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                _flex.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));

                if (Config.MA_ENV.YN_UNIT == "Y")
                {
                    _flex.SetCol("SEQ_PROJECT", "프로젝트항번", 80);



                }
            }


            _flex.SetCol("CD_ITEM", "품목", 120);
            _flex.SetCol("NM_ITEM", "품목명", 150);
            _flex.SetCol("STND_ITEM", "규격", 80);
            _flex.SetCol("UNIT_IM", "재고단위", 80);
            _flex.SetCol("NM_CLSITEM", "계정구분", 80);
            _flex.SetCol("QT_BAS", "기초수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("UM_BAS", "기초단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            _flex.SetCol("AM_BAS", "기초금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("QT_RCV", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("AM_RCV", "입고금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("QT_ISU_SUB", "대체수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("AM_ISU_SUB", "대체금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("QT_ISU", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("AM_ISU", "출고금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("QT_INV", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("UM_INV", "재고단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            _flex.SetCol("AM_INV", "재고금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            
            _flex.SetCol("NM_CLS_L", "대분류", false);
            _flex.SetCol("NM_CLS_M", "중분류", false);
            _flex.SetCol("NM_CLS_S", "소분류", false);

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }



        protected override void InitPaint()
        {
            base.InitPaint();

            dTP_From.Value = new System.DateTime(System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(0, 4)), System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(4, 2)), 1);
            dTP_To.Value = new System.DateTime(System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(0, 4)), System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(4, 2)), 1);
            tb_DT_TODAY.Text = this.MainFrameInterface.GetStringToday;
            tb_NO_EMP.CodeValue = this.LoginInfo.EmployeeNo;
            tb_NO_EMP.CodeName = this.LoginInfo.EmployeeName;
            _cddept = this.LoginInfo.DeptCode;


            DataSet ds = this.GetComboData("NC;MA_PLANT");

            // 공장	
            cbo_CD_PLANT.DataSource = ds.Tables[0];
            cbo_CD_PLANT.DisplayMember = "NAME";
            cbo_CD_PLANT.ValueMember = "CODE";

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl5.IsNecessaryCondition = true;
            bpPanelControl6.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            if (Global.MainFrame.ServerKeyCommon.Contains("DAEYONG") ) btn_send.Visible = true;
            
        }

        #endregion

        #region ♣ 메인버튼 이벤트

        #region -> 조회버튼클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                pur.P_PU_EVAL_SUB m_dlg = new pur.P_PU_EVAL_SUB(this.MainFrameInterface);
                if (m_dlg.ShowDialog(this) == DialogResult.OK)
                {
                    object[] args = new object[3];
                    args[0] = m_dlg.m_SelecedRow["YM_STANDARD"].ToString();
                    args[1] = m_dlg.m_SelecedRow["CD_PLANT"].ToString();
                    args[2] = MainFrameInterface.LoginInfo.CompanyCode;

                    DataTable ldt_data = _biz.Search(args);


                    if (ldt_data == null || ldt_data.Rows.Count <= 0)
                    {
                        this.ShowMessage("IK1_003");
                        return;
                    }

                    _flex.Binding = ldt_data;
                    SubTotalDisplay(_flex);

                    cbo_CD_PLANT.SelectedValue = m_dlg.m_SelecedRow["CD_PLANT"].ToString();
                    tb_NO_EMP.CodeValue = m_dlg.m_SelecedRow["NO_EMP"].ToString();
                    tb_NO_EMP.CodeName = m_dlg.m_SelecedRow["NM_KOR"].ToString();

                    dTP_From.Value = new System.DateTime(System.Int32.Parse(m_dlg.m_SelecedRow["YM_FSTANDARD"].ToString().Substring(0, 4)),
                        System.Int32.Parse(m_dlg.m_SelecedRow["YM_FSTANDARD"].ToString().Substring(4, 2)), 1);
                    dTP_To.Value = new System.DateTime(System.Int32.Parse(m_dlg.m_SelecedRow["YM_STANDARD"].ToString().Substring(0, 4)),
                        System.Int32.Parse(m_dlg.m_SelecedRow["YM_STANDARD"].ToString().Substring(4, 2)), 1);

                    tb_DT_TODAY.Text = m_dlg.m_SelecedRow["DT_INPUT"].ToString();

                    this.ToolBarDeleteButtonEnabled = true;
                    EnabledControl(false);

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가버튼클릭

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                SetInitialControlData();
                cbo_CD_PLANT.Focus();
                _flex.DataTable.Clear();

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제버튼클릭

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_biz.평가기간체크(D.GetString(cbo_CD_PLANT.SelectedValue), (dTP_From.Value.Year.ToString("0000") + dTP_From.Value.Month.ToString("00")), dTP_To.Value.Year.ToString("0000") + dTP_To.Value.Month.ToString("00"),"DELETE"))
                {
                    return;
                } 

                DialogResult result = this.ShowMessage("QY2_003");
                if (result == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    object[] m_obj = new object[6];
                    m_obj[0] = this.LoginInfo.CompanyCode;
                    m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
                    m_obj[2] = dTP_To.Value.Year.ToString("0000") + dTP_To.Value.Month.ToString("00");
                    m_obj[3] = dTP_From.Value.Year.ToString("0000") + dTP_From.Value.Month.ToString("00");
                    m_obj[4] = this.LoginInfo.UserID;
                    m_obj[5] = this.MainFrameInterface.GetStringDetailToday;

                    ResultData ret = _biz.Delete(m_obj);

                    if (ret.Result)
                    {
                        _flex.DataTable.Clear();
                        SetInitialControlData();
                        this.ShowMessage("IK1_002");
                    }
                    else
                    {
                        this.ShowMessage("EK1_002");
                        return;
                    }

                }
            }

            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }


        #endregion	


        #endregion

        #region ♣ 기타 함수


        #region -> 재고평가 버턴 클릭
        private void btn_INV_EVAL_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!FieldCheck_Head())
                {
                    return;
                } 

                DialogResult resultOK = this.ShowMessage("PU_M000066", "QY2");


                if (!_biz.평가기간체크(D.GetString(cbo_CD_PLANT.SelectedValue), (dTP_From.Value.Year.ToString("0000") + dTP_From.Value.Month.ToString("00")), dTP_To.Value.Year.ToString("0000") + dTP_To.Value.Month.ToString("00"), "SEARCH"))
                {
                    return;
                } 

                
                if (resultOK == DialogResult.Yes)
                {
                    object[] m_obj = new object[12];
                    m_obj[0] = this.LoginInfo.CompanyCode;
                    m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
                    m_obj[2] = dTP_From.Value.Year.ToString("0000") + dTP_From.Value.Month.ToString("00");
                    m_obj[3] = dTP_To.Value.Year.ToString("0000") + dTP_To.Value.Month.ToString("00");
                    m_obj[4] = dTP_From.Value.Year.ToString("0000");
                    m_obj[5] = _cddept;
                    m_obj[6] = tb_NO_EMP.CodeValue.ToString();
                    m_obj[7] = tb_DT_TODAY.MaskEditBox.ClipText;
                    m_obj[8] = this.LoginInfo.UserID;
                    m_obj[9] = this.MainFrameInterface.GetStringDetailToday;
                    m_obj[10] = this.LoginInfo.UserID;
                    m_obj[11] = this.MainFrameInterface.GetStringDetailToday;
                    //ResultData ret = (ResultData)this.ExecSp("SP_PU_AMINV_PROCESS_TOTAL", m_obj);

                    ResultData ret = null;

                    if (Global.MainFrame.ServerKeyCommon == "WONIK")
                        ret = _biz.Eval_WONIK(m_obj);
                    else
                        ret = _biz.Eval(m_obj);
                    


                    object[] args = new object[3];
                    args[0] = dTP_To.Value.Year.ToString("0000") + dTP_To.Value.Month.ToString("00");
                    args[1] = cbo_CD_PLANT.SelectedValue.ToString();
                    args[2] = this.LoginInfo.CompanyCode;


                    //Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                    //si.SpNameSelect = "SP_PU_EVAL_SELECT";
                    //si.SpParamsSelect = args;
                    //ResultData result = (ResultData)this.FillDataTable(si);

                    //DataTable ldt_result = (DataTable)result.DataValue;

                    DataTable ldt_result = _biz.Search(args);


                    _flex.Binding = new DataView(ldt_result);

                    //_flex.Redraw = false;
                    //_flex.BindingStart();
                    //_flex.DataSource = new DataView(ldt_result);
                    //_flex.BindingEnd();
                    //_flex.Redraw = true;

                    SubTotalDisplay(_flex);


                    this.ShowMessage("PU_M000068");
                    EnabledControl(false);
                    this.ToolBarDeleteButtonEnabled = true;

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region -> SubTotalDisplay
        private void SubTotalDisplay(Dass.FlexGrid.FlexGrid flex)
        {
            try
            {
                flex.SubtotalPosition = SubtotalPositionEnum.BelowData;

                CellStyle s = flex.Styles[CellStyleEnum.Subtotal0];
                s.BackColor = Color.FromArgb(234, 234, 234);
                s.ForeColor = Color.Black;
                s.Font = new Font(flex.Font, FontStyle.Bold);

                flex.Subtotal(AggregateEnum.Clear);//MA, GRAND
                flex.Subtotal(AggregateEnum.Sum, 0, -1, flex.Cols["AM_BAS"].Index);
                flex.Subtotal(AggregateEnum.Sum, 0, -1, flex.Cols["AM_RCV"].Index);
                flex.Subtotal(AggregateEnum.Sum, 0, -1, flex.Cols["AM_ISU_SUB"].Index);
                flex.Subtotal(AggregateEnum.Sum, 0, -1, flex.Cols["AM_ISU"].Index);
                flex.Subtotal(AggregateEnum.Sum, 0, -1, flex.Cols["AM_INV"].Index);

            }
            catch
            {
            }
        }
        #endregion	

        #region -> SetInitialControlData
        private void SetInitialControlData()
        {
            try
            {
                EnabledControl(true);
                dTP_From.Value = new System.DateTime(System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(0, 4)),
                    System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(4, 2)), 1);
                dTP_To.Value = new System.DateTime(System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(0, 4)),
                    System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(4, 2)), 1);
                tb_DT_TODAY.Text = this.MainFrameInterface.GetStringToday;
                tb_NO_EMP.CodeValue = this.LoginInfo.EmployeeNo;
                tb_NO_EMP.CodeName = this.LoginInfo.EmployeeName;
                _cddept = this.LoginInfo.DeptCode;

                this.ToolBarAddButtonEnabled = true;
                this.ToolBarDeleteButtonEnabled = false;
                this.ToolBarSaveButtonEnabled = false;
                this.ToolBarSearchButtonEnabled = true;

            }
            catch
            {
            }
        }

        #endregion

        #region -> EnabledControl
        private void EnabledControl(bool isEnabled)
        {
            try
            {
                cbo_CD_PLANT.Enabled = isEnabled;
                tb_NO_EMP.Enabled = isEnabled;
                dTP_From.Enabled = isEnabled;
                dTP_To.Enabled = isEnabled;
                btn_INV_EVAL.Enabled = isEnabled;
            }
            catch
            {
            }
        }


        #endregion

        #region -> FieldCheck_Head

        private bool FieldCheck_Head()
        {
            try
            {
                if (cbo_CD_PLANT.SelectedValue.ToString() == "")
                {
                    cbo_CD_PLANT.Focus();
                    this.ShowMessage("WK1_004", lb_NM_PLANT.Text);
                    return false;
                }

                if (tb_NO_EMP.CodeValue.ToString() == "")
                {
                    tb_NO_EMP.Focus();
                    this.ShowMessage("WK1_004", lb_NO_EMP.Text);
                    return false;
                }

            }

            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return true;
        }

        #endregion

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                bool rtn = _biz.Send(D.GetString(cbo_CD_PLANT.SelectedValue), dTP_To.Value.Year.ToString("0000") + dTP_To.Value.Month.ToString("00"));
                if (rtn)
                    ShowMessage(PageResultMode.SaveGood);
                else
                    ShowMessage(PageResultMode.SaveFail);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion


    }
}
