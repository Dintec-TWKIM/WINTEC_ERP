//********************************************************************
// 작   성   자 : 유이열 
// 작   성   일 : 2002-08-13
// 수   정   일 : 2006-09-18 
// 모   듈   명 : 무역
// 시 스  템 명 : 무역관리
// 서브시스템명 : 기준정보
// 페 이 지  명 : 무역환경설정
// 프로젝트  명 : P_TR_CONTROL_TMP1
//********************************************************************
using System;
using System.Data;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;

using System.Diagnostics;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace trade
{

    public delegate void VoidBoolHandler(bool enabled);

    public partial class P_TR_TO_IN : Duzon.Common.Forms.PageBase
    {
        // *******************************************************************************
        // 작   성   자 : 오성영
        // 재 작  성 일 : 2006-09-25 / 2007-05-10
        // 모   듈   명 : 구매
        // 시 스  템 명 : 수입관리
        // 서브시스템명 : 수입관리
        // 페 이 지  명 : 통관등록
        // 프로젝트  명 : P_TR_TO_IN
        // 변 경  내 역 : 
        // 2009.12.10 면장환율을 지정하면 무조건 지정환율로 따라간다.
        //--------------------------------------------------------------------------
        // 2010.02.27 가격조건 BL적용시 자동적용-SMR(이형준)
        //--------------------------------------------------------------------------
        // 2010.03.29 BL적용시 BL쪽 데이터 그대로 적용 
        //            이후 금액,환율조정은 통관 HEADER쪽에서 수정하면 고쳐진다.
        //--------------------------------------------------------------------------
        // 2011.08.29 1. 분할통관 되도록 수정함.
        //            2. 시스템통제설정 값에 따라서 처리되도록 수정함.
        //--------------------------------------------------------------------------
        // 2011.11.17 PIMS : D20111107058,  통관일(DT_CUSTOMS) 추가
        // 2013.07.25 윤성우 - ONE GRID 수정(입력 전용)
        // *******************************************************************************

        #region ★ 멤버필드
                                                                        
        private P_TR_TO_IN_BIZ _biz  = new P_TR_TO_IN_BIZ();
        private FreeBinding _header = new FreeBinding();

        //private string m_CdCC = null, m_NmCC = null;

        // 수정, 삭제 못하는 여부
        private string m_DeleteCheck = "N";
        //private bool bEXIST_NO_TO = true;  // 내역 등록에 NO_BL 가 있는지 여부를 알기 위해 선언           
        DataTable _dt = new DataTable();   // BL적용시 발생하며 라인 동시 저장시에 쓰임...
        //private string m_PrevRtExch = null;

        private bool 헤더변경여부 = true;
        
        //private string _전용설정 = "000";   //2011-08-29, 최승애, 시스템통제설정(무역모듈 - 통관등록-분할통관사용, 기본값 000) 

        String _첨부CHK설정 = "000";            //2011-09-23, 최승애 추가


        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_TR_TO_IN()
        {
            try
            {
                InitializeComponent();

                this.MainGrids = new FlexGrid[] {_flex};

                _header.JobModeChanged += new FreeBindingEventHandler( _header_JobModeChanged );
                _header.ControlValueChanged += new FreeBindingEventHandler( _header_ControlValueChanged );
            }
            catch ( Exception ex )
            {
                this.MsgEnd( ex );
            }
       }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            

            ///////////////////////////////////////////////////////////////////////////////////////////

            //_전용설정 = Duzon.ERPU.MF.ComFunc.전용코드("수입통관등록-분할통관사용");        //2011-08-29, 최승애 추가함.

            //if (D.GetString(_전용설정) == string.Empty)
            //    _전용설정 = "000";

            if (분할통관사용 == "100")
            {
                _flex.Visible = true;
                btn삭제.Visible = true;
                oneGrid1.Dock = DockStyle.Top;
            }
            else
            {
                _flex.Visible = false;
                btn삭제.Visible = false;
                oneGrid1.Dock = DockStyle.Top;
            }

            InitGrid();
        }

        #endregion


        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);

            _flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flex.SetCol("NO_BL", "BL번호", 120, false);
            _flex.SetCol("CD_ITEM", "품목코드", 90, false);
            _flex.SetCol("NM_ITEM", "품목명", 150, false);
            _flex.SetCol("STND_ITEM", "규격", 80, false);
            _flex.SetCol("CD_UNIT_MM", "단위", 80, false);
            _flex.SetCol("CD_SL", "S/L코드", 80, false);
            _flex.SetCol("NM_SL", "S/L명", 120, false);
            _flex.SetCol("DT_DELIVERY", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("QT_TO_MM", "수량", 100,  true, typeof(Decimal), FormatTpType.QUANTITY);
            _flex.SetCol("UM_EX_PO", "단가", 100, false, typeof(Decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex.SetCol("AM_EX", "금액", 120, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("AM", "원화금액", 120, false, typeof(Decimal), FormatTpType.MONEY);
            _flex.SetCol("NM_PLANT", "공장명", 120, false);
            _flex.SetCol("NM_PJT", "프로젝트명", 120, false);
            _flex.SetCol("RT_CUSTOMS", "관세율", 120, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("RT_SPECT", "특소세율", 120, false, typeof(Decimal), FormatTpType.MONEY);

            if (BASIC.GetMAEXC("리베이트사용여부") == "100" && (BASIC.GetMAEXC("수입프로젝트형사용여부") != "100"))
            {
                _flex.SetCol("UM_REBATE", "리베이트단가", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("AM_REBATE_EX", "리베이트금액", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex.SetCol("AM_REBATE", "리베이트원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            }

            DataTable dtTEXT = MA.GetCode("TR_IM00030", false);
            if (dtTEXT != null && dtTEXT.Rows.Count != 0)
            {
                foreach (DataRow row in dtTEXT.Rows)
                {
                    string ColCaption = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                    string ColName = D.GetString(row["NAME"]);
                    if (ColName.Contains("NUM"))
                        _flex.SetCol(ColName, ColCaption, 70, true, typeof(decimal), FormatTpType.QUANTITY);
                }
            }

            // _flex.SetCodeHelpCol( "CD_PJT", HelpID.P_SA_PJT_SUB, ShowHelpEnum.Always, new string [] { "CD_PJT" }, new string [] { "NO_PROJECT" } );
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            if (BASIC.GetMAEXC("리베이트사용여부") == "100" && (BASIC.GetMAEXC("수입프로젝트형사용여부") != "100"))
                _flex.SetExceptSumCol("UM_EX_PO", "RT_CUSTOMS", "RT_SPECT", "UM_REBATE");
            else
                _flex.SetExceptSumCol("UM_EX_PO", "RT_CUSTOMS", "RT_SPECT");
            _flex.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);

            //    _flex.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler( _flex_AfterRowColChange );
        }

        #endregion


        #region -> InitPaint

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                TOMainGetGubunCD();

                DataSet dt = _biz.Search("");
                _header.SetBinding(dt.Tables[0], oneGrid1);
                _header.ClearAndNewRow();
                _dt = dt.Tables[1];
                _flex.Binding = dt.Tables[1];

                _header.CurrentRow["FG_LC"] = Settings1.Default.FG_LC;
                _header.CurrentRow["CD_PARTNER"] = Settings1.Default.CD_PARTNER;
                _header.CurrentRow["LN_PARTNER"] = Settings1.Default.LN_PARTNER;
                _header.CurrentRow["CD_PURGRP"] = Settings1.Default.CD_PURGRP;
                _header.CurrentRow["NM_PURGRP"] = Settings1.Default.NM_PURGRP;
                _header.CurrentRow["CD_EXCH"] = Settings1.Default.CD_EXCH;
                _header.CurrentRow["COND_PRICE"] = Settings1.Default.COND_PRICE;

                콘트롤초기화();

                oneGrid1.UseCustomLayout = true;
                bpPanelControl1.IsNecessaryCondition = true;
                bpPanelControl2.IsNecessaryCondition = true;
                bpPanelControl3.IsNecessaryCondition = true;
                bpPanelControl4.IsNecessaryCondition = true;
                bpPanelControl10.IsNecessaryCondition = true;
                bpPanelControl11.IsNecessaryCondition = true;
                bpPanelControl12.IsNecessaryCondition = true;
                bpPanelControl13.IsNecessaryCondition = true;
                bpPanelControl16.IsNecessaryCondition = true;
                bpPanelControl19.IsNecessaryCondition = true;
                bpPanelControl21.IsNecessaryCondition = true;
                bpPanelControl22.IsNecessaryCondition = true;
                bpPanelControl23.IsNecessaryCondition = true;
                bpPanelControl25.IsNecessaryCondition = true;

                oneGrid1.IsSearchControl = false; //2013.07.25 - 윤성우 - ONE GRID 수정(입력 전용)

                oneGrid1.InitCustomLayout();


            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }

        #endregion

        /// <summary>
        /// 각 구분코드 Setting
        /// </summary>
        private void TOMainGetGubunCD()
        {

            DataSet lds_Combo = this.GetComboData( "N;TR_IM00005", "N;MA_B000005", "N;TR_IM00002" );


            DataTable dt = lds_Combo.Tables[0].Clone();
            //Local L/C는 제외한다
            DataRow[] drs = lds_Combo.Tables[0].Select("CODE NOT IN ('003')");

            foreach (DataRow dr in drs)
                dt.ImportRow(dr);

            //거래구분코드 ComboBox에 Add
            m_comFgTrans.DataSource = dt;
            m_comFgTrans.ValueMember = "CODE";
            m_comFgTrans.DisplayMember = "NAME";

            //통화구분 ComboBox에 Add
            m_comCdCurrency.DataSource = lds_Combo.Tables [1];
            m_comCdCurrency.ValueMember = "CODE";
            m_comCdCurrency.DisplayMember = "NAME";

            //가격조건 ComboBox에 Add
            m_comCondPrice.DataSource = lds_Combo.Tables [2];
            m_comCondPrice.ValueMember = "CODE";
            m_comCondPrice.DisplayMember = "NAME";

            m_comFgTrans.SelectedIndex = -1;
            m_comCdCurrency.SelectedIndex = -1;
            m_comCondPrice.SelectedIndex = -1;

            if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                m_txtRateLicense.Enabled = false;


            // 2011-06-07, 최승애 추가함./////////////////////
            m_txtAmtEx.Mask = this.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.FOREIGN_MONEY, FormatFgType.INSERT);
            m_txtKorAmt.Mask = this.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.MONEY, FormatFgType.INSERT);
            /////////////////////////////////////////////////


            // 2011-09-23, 첨부기능 추가함. 최승애(포티스  정기현과장님 요청)
            _첨부CHK설정 = Duzon.ERPU.MF.ComFunc.전용코드("송장등록-첨부파일");
            
            if (D.GetString(_첨부CHK설정) == string.Empty)
                _첨부CHK설정 = "000";

            if (_첨부CHK설정 == "100")
                btn첨부파일.Visible = true;


            


        }

        #endregion

        #region ★ 주요버튼 /// 브라우저의 버튼이 눌려졌을때 처리할 이벤트 모음 ///

        /// <summary>
        /// 버튼조회
        /// </summary>
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
               // string 참조여부 = "002";   // 전체 도움창을 보여준다.  
                P_TR_TO_NO dlg = new P_TR_TO_NO();

                if ( dlg.ShowDialog() == DialogResult.OK )
                {
                    DataSet ds = _biz.Search(dlg.통관번호);            // SP_TR_BL_NO_SELECT_VIEW 호출 - 통관된 BL은 보여주지 않는다.
                    DataTable dt = ds.Tables[0];
                    _dt = ds.Tables[1];                                //2009.11.05  BL LINE DATA TABLE 추가
                    _header.SetDataTable(dt);                           /*  JobModeChanged 이벤트가 발생됨 */
                    _flex.Binding = ds.Tables[1];




                    //if ( dt.Rows [0] ["EXIST_NO_TO"].Equals( "Y" ) )  /*  만일 [SP_TR_TO_NO_SELECT] 상에서 TR_TO_IML.NO_LC[EXIST_NO_TO]값(내역등록 데이타) 존재[Y]할 경우 비활성화 시켜준다.  2007.04.23 By Young  */
                    //    bEXIST_NO_TO = false;
                    //else
                    //    bEXIST_NO_TO = true;

                    //m_CdCC = dlg.CC코드;
                    //m_NmCC = dlg.CC명;

                    if ( dt.Rows[0]["RCVL"].ToString() != "" )    // 구매입고의뢰가 되었다면
                    {
                        ToolBarDeleteButtonEnabled = false;
                        ToolBarSaveButtonEnabled = false;
                        m_DeleteCheck = "Y";
                    }
                    else
                    {
                        ToolBarDeleteButtonEnabled = true;
                        ToolBarSaveButtonEnabled = true;
                        m_DeleteCheck = "N";
                    }

                    SetControlEnable( !EXIST_NO_TO );
                    //_header.AcceptChanges();
                    //_dt.AcceptChanges();
                    //_flex.AcceptChanges();

                }

            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if ( !BeforeAdd() )
                    return;

                Debug.Assert( _header.CurrentRow != null );       // 혹시나 해서 한번 더 확인

                _header.ClearAndNewRow();

                _header.CurrentRow["FG_LC"] = Settings1.Default.FG_LC;
                if (!Global.MainFrame.ServerKeyCommon.Contains("WJIS"))
                {
                    _header.CurrentRow["CD_PARTNER"] = Settings1.Default.CD_PARTNER;
                    _header.CurrentRow["LN_PARTNER"] = Settings1.Default.LN_PARTNER;
                }
                _header.CurrentRow["CD_PURGRP"] = Settings1.Default.CD_PURGRP;
                _header.CurrentRow["NM_PURGRP"] = Settings1.Default.NM_PURGRP;
                _header.CurrentRow["CD_EXCH"] = Settings1.Default.CD_EXCH;
                _header.CurrentRow["COND_PRICE"] = Settings1.Default.COND_PRICE;
                콘트롤초기화();


                _flex.DataTable.Rows.Clear();
                _flex.AcceptChanges();


                //SetControlEnable( true );
          
            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }

        #region -> 삭제버튼 클릭

        protected override bool BeforeDelete()
        {
            if ( !base.BeforeDelete() )
                return false;


            if ( ShowMessage( 공통메세지.자료를삭제하시겠습니까 ) == DialogResult.Yes )
            {
                if ( _header.CurrentRow ["NO_COST"].ToString() != "" && _header.CurrentRow ["NO_COST"].ToString() != String.Empty )
                {
                    ShowMessage( "부대비용이 등록되어 있어 삭제할 수 없습니다." );
                    return false;
                }

                if ( m_txtAmtEx.DecimalValue != 0 )
                {
                    //if ( MessageBoxEx.Show( "내역이 등록되어있습니다. 모두 삭제하시겠습니까 ?", this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) != DialogResult.Yes )
                    if (ShowMessage("내역이 등록되어있습니다. 모두 삭제하시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return false;
                }
                if ( IsExistPage( "P_TR_TO_LINE", false ) )
                    UnLoadPage( "P_TR_TO_LINE", false );
            }
            else
                return false;

            //if ( ShowMessage( 공통메세지.자료를삭제하시겠습니까 ) != DialogResult.Yes )
            //    return false;

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if ( !BeforeDelete() )
                    return;

                if ( _biz.Delete( m_txtNoTo.Text ) )
                {
                    ShowMessage( 공통메세지.자료가정상적으로삭제되었습니다 );
                    OnToolBarAddButtonClicked( sender, e );
                }
            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }


        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if ( !BeforeExit() )
                    return false;

                if ( IsExistPage( "P_TR_TO_LINE", false ) )
                {
                    //if ( MessageBoxEx.Show( "내역등록창을 먼저 닫아야 합니다. 창을 닫으시겠습니까 ?", this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) != DialogResult.Yes )
                    if (ShowMessage("내역등록창을 먼저 닫아야 합니다. 창을 닫으시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return false;
                    UnLoadPage( "P_TR_TO_LINE", false );
                }

                Settings1.Default.FG_LC = _header.CurrentRow["FG_LC"].ToString();
                Settings1.Default.CD_PARTNER = _header.CurrentRow["CD_PARTNER"].ToString();
                Settings1.Default.LN_PARTNER = _header.CurrentRow["LN_PARTNER"].ToString();
                Settings1.Default.CD_PURGRP = _header.CurrentRow["CD_PURGRP"].ToString();
                Settings1.Default.NM_PURGRP = _header.CurrentRow["NM_PURGRP"].ToString();
                Settings1.Default.CD_EXCH = _header.CurrentRow["CD_EXCH"].ToString();
                Settings1.Default.COND_PRICE = _header.CurrentRow["COND_PRICE"].ToString();
                //꼭 저장을 해줘야 한다.
                Settings1.Default.Save();


            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
            return true;
        }

        #endregion


        protected override bool IsChanged()
        {
            if ( base.IsChanged() )
                return true;

            DataTable dt = _header.GetChanges();

            if ( dt != null && ( dt.Rows [0] ["NO_TO"].ToString() != "" || dt.Rows [0] ["NO_TO"].ToString() != string.Empty ) )
            {
                return true;      // 변경된 자료가 있을 경우
            }

            return false;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if ( BeforeSave() == false )
                    return;

                if ( MsgAndSave( PageActionMode.Save ) )
                {
                    this.ShowMessage( 공통메세지.자료가정상적으로저장되었습니다 );
                }
            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }

        #endregion

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData())
                    return false;


                String 통관번호 = m_txtNoTo.Text;


                if (분할통관사용 == "000")    //기본설정시 처리
                {

                    DataTable dt = _header.GetChanges();


                    if (dt == null)
                        return true;


                    Boolean b입고적용여부 = (dt.Rows[0]["RCVL"].ToString() != string.Empty) ? true : false;

                    if (b입고적용여부)
                    {
                        //ShowMessageKor("입고적용 건에 대해서는 통관내용을 수정 및 삭제할 수 없습니다.");
                        ShowMessage("입고적용 건에 대해서는 통관내용을 수정 및 삭제할 수 없습니다.");
                        return false;
                    }
                    else
                    {
                        //String 통관번호 = m_txtNoTo.Text;

                        bool bSuccess = _biz.Save(dt, _dt, 통관번호);
                        if (!bSuccess)
                            return false;


                    }

                }
                else
                {

                    //분할통관 설정 사용시 처리

                    if (_flex.HasNormalRow)
                    {
                        foreach (DataRow dr in _flex.DataTable.Rows)
                        {
                            if (dr.RowState == DataRowState.Deleted) continue;

                            if (D.GetString(dr["NO_TO"]) == "") dr["NO_TO"] = 통관번호;
                        }
                    }


                    DataTable dt = _header.GetChanges();
                    DataTable dtL = _flex.GetChanges();


                    if (dt == null && dtL == null)
                        return true;


                    Boolean b입고적용여부 = true;
                    decimal iCnt = 0;


                    if (dt != null)
                    {
                        b입고적용여부 = (dt.Rows[0]["RCVL"].ToString() != string.Empty) ? true : false;
                    }


                    if (dtL != null)
                    {
                        foreach (DataRow dr in dtL.Rows)
                        {
                            if (dr.RowState == DataRowState.Deleted) continue;

                            b입고적용여부 = (D.GetString(dr["RCVL"]) != string.Empty) ? true : false;

                            if (b입고적용여부) iCnt++;
                        }
                    }



                    if ((dtL != null && iCnt > 0) || (dt != null && b입고적용여부))
                    //if (b입고적용여부)
                    {
                        //ShowMessageKor("입고적용 건에 대해서는 통관내용을 수정 및 삭제할 수 없습니다.");
                        ShowMessage("입고적용 건에 대해서는 통관내용을 수정 및 삭제할 수 없습니다.");
                        return false;
                    }
                    else
                    {                        
                        bool bSuccess = _biz.Save(dt, dtL, 통관번호);

                        if (!bSuccess)
                             return false;

                    }
                }


                _dt.AcceptChanges();
                _header.AcceptChanges();
               
                return true;



                ////if (_flex.HasNormalRow)
                ////{
                ////    foreach (DataRow dr in _flex.DataTable.Rows)
                ////    {
                ////        if (dr.RowState == DataRowState.Deleted) continue;

                ////        if (D.GetString(dr["NO_TO"]) == "") dr["NO_TO"] = 통관번호;
                ////    }
                ////}


                ////DataTable dt = _header.GetChanges();                
                ////DataTable dtL = _flex.GetChanges();
                

                ////if (dt == null && dtL== null)
                ////    return true;


                ////Boolean b입고적용여부 = true;
                ////decimal iCnt = 0;


                ////if (dt != null)
                ////{
                ////    b입고적용여부 = (dt.Rows[0]["RCVL"].ToString() != string.Empty) ? true : false;
                ////}


                ////if (dtL != null)
                ////{
                ////    foreach (DataRow dr in dtL.Rows)
                ////    {
                ////        if (dr.RowState == DataRowState.Deleted) continue;

                ////        b입고적용여부 = (D.GetString(dr["RCVL"]) != string.Empty) ? true : false;

                ////        if (b입고적용여부) iCnt++;
                ////    }
                ////}
                
                

                ////if ((dtL != null && iCnt > 0) ||  (dt != null && b입고적용여부))
                //////if (b입고적용여부)
                ////{
                ////    //ShowMessageKor("입고적용 건에 대해서는 통관내용을 수정 및 삭제할 수 없습니다.");
                ////    ShowMessage("입고적용 건에 대해서는 통관내용을 수정 및 삭제할 수 없습니다.");
                ////    return false;
                ////}
                ////else
                ////{

                ////    //String 통관번호 = m_txtNoTo.Text;


                ////    //if (_flex.HasNormalRow)
                ////    //{   
                ////    //    foreach (DataRow dr in _flex.DataTable.Rows)
                ////    //    {
                ////    //        if (dr.RowState == DataRowState.Deleted) continue;

                ////    //        if (D.GetString(dr["NO_TO"]) == "")  dr["NO_TO"] = 통관번호;                            
                ////    //    }
                ////    //}

                ////    bool bSuccess = false;

                ////     if (_전용설정 == "000")
                ////     {                        
                ////        bSuccess = _biz.Save(dt, _dt, 통관번호);
                        
                ////        if (!bSuccess)
                ////            return false;

                ////        _header.AcceptChanges();
                ////        _dt.AcceptChanges();
                ////     }
                ////     else
                ////     {
                ////        bSuccess = _biz.Save(dt, dtL, 통관번호);

                ////        if (!bSuccess)
                ////            return false;

                ////        _header.AcceptChanges();                  
                ////        _flex.AcceptChanges();
                ////     }
                  
                ////    return true;                
                ////}


            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return false;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if ( BeforePrint() == false )
                    return;
            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }

        #region ▶ 내역등록, 부대비용등록 버튼
        /// <summary>
        /// 내역등록 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void m_btnConfirmItem_Click(object sender, System.EventArgs e)
        {
            if ( _header == null )
                return;

            try
            {

                if (!부대비용창존재여부) return;
                //if ( IsExistPage( "P_TR_TO_LINE", false ) )
                //     UnLoadPage(  "P_TR_TO_LINE", false );

                object [] args = new object [11];
                args [0] = _header.CurrentRow ["NO_TO"].ToString(); 
                args [1] = _header.CurrentRow ["CD_PARTNER"].ToString();
                args [2] = _header.CurrentRow ["LN_PARTNER"].ToString();
                args [3] = _header.CurrentRow ["NO_EMP"].ToString();
                args [4] = _header.CurrentRow ["NM_KOR"].ToString();
                args [5] = _header.CurrentRow ["NO_SCBL"].ToString();
                args [6] = _header.CurrentRow ["RT_EXCH"].ToString();     // 면장환율 넘김
                args [7] = m_DeleteCheck;
                args[8] = new VoidHandler<bool>(SetControlEnable);
                args [9] = _header;
                args [10] = new VoidParamsHandler<Object>( 내역등록 );

                this.LoadPageFrom( "P_TR_TO_LINE", DD( "통관내역등록" ), Grant, args );
            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }

        /***********************************************************************************************
         Method 명 : 내역등록
         통관도움창 역할을 하는 SP를 가져와서 Binding -> 내역등록의 금액 가져옴
        ***********************************************************************************************/

        private void 내역등록(params Object [] chk)
        {
            try
            {
                return;
                //_header.CurrentRow["AM_EX"] = chk[0].ToString();
                //m_txtAmtEx.DecimalValue = Convert.ToDecimal(chk[0].ToString());

                //_header.CurrentRow["AM"] = chk[1].ToString();
                //m_txtKorAmt.DecimalValue = Convert.ToDecimal(chk[1].ToString());


                //bEXIST_NO_TO = (Convert.ToDecimal(chk[0]) != 0) ? false : true;
                //_dt = _biz.Search_Line(_header.CurrentRow["NO_TO"].ToString());
                //SetControlEnable(bEXIST_NO_TO);

                //헤더변경여부 = false;
                //SaveData();
         
            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }


        /// <summary>
        /// 부대비용등록 클릭 이벤트
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void m_btnImportCost_Click(object sender, System.EventArgs e)
        {
            if (!Check())
                return;
            if (!내역등록창존재여부) return;
            //if ( IsExistPage( "P_TR_IMCOST", false ) )
            //     UnLoadPage(  "P_TR_IMCOST", false );

            object [] args = new object [18];
            args [0] = "TO";
            args [1] = m_txtNoTo.Text;
            args [2] = "0";
            args [3] = _header.CurrentRow ["NO_SCLC"].ToString();
            args [4] = m_tbBLRefer.Text;
            args [5] = m_txtCdTrans.CodeValue;
            args [6] = m_txtCdTrans.CodeName;
            args [7] = m_txtToDate.Text;
            args [8] = _header.CurrentRow ["NO_COST"].ToString();
            args [9] = D.GetString(m_comCdCurrency.SelectedValue);
            args [10] = m_txtAmtEx.Text; // m_txtAmtLicense.Text; 면장 금액 삭제
            args [11] = D.GetString(_header.CurrentRow["CD_CC"]); ;
            args [12] = D.GetString(_header.CurrentRow["NM_CC"]); ;
            args [13] = _header.CurrentRow ["AM"].ToString();
            args [14] = new VoidParamsHandler<string>(부대비용);   // 이벤트 대리자를 통해 no_cost값 전달
            args[15] = MainFrameInterface.GetStringToday;
            args[16] = m_txtNmEmp.CodeValue;
            args[17] = m_txtNmEmp.CodeName;

            LoadPageFrom( "P_TR_IMCOST", DD( "부대비용등록(수입통관)" ), Grant, args );

        }

        #endregion


        private void 부대비용(params string [] chk)
        {
            //if (chk[0].ToString() != "부대비용Null")
            //{
            //    _header.CurrentRow["NO_COST"] = chk[0].ToString();
            //    ToolBarDeleteButtonEnabled = false;
            //}
            //else
            //    _header.CurrentRow["NO_COST"] = null;

            //헤더변경여부 = false;
           // SaveData();

            _header.CurrentRow["NO_COST"] = D.GetString(chk[0]);
            _header.AcceptChanges();
        }

        #region ▶ 각종 버튼 이벤트
        /// <summary>
        /// B/L번호 도움창
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void m_btnNoBl_Search(object sender, SearchEventArgs e)
        {
  
            string 통관참조여부 = "001";
            string 넘기는화면 = "P_TR_TO_IN";
            string 거래처 = m_txtCdTrans.CodeValue;
           // string 전용설정 = _전용설정;
            decimal iLine = 0;

            string 참조BL번호 = string.Empty;
            string[] BL번호 = null;

            P_TR_BL_NO dlg = new P_TR_BL_NO(통관참조여부, 넘기는화면, 거래처, 분할통관사용);
            //Decimal rt_exch = 0;

            if ( dlg.ShowDialog() == DialogResult.OK )
            {
                DataRow dr0 = dlg.ReturnRow;
                

                if ( dr0 == null )
                    return;
                else
                    ToolBarSaveButtonEnabled = true;


                if (분할통관사용 == "000")
                {

                    _header.CurrentRow["NO_SCBL"] = dlg.BL번호;
                    //    _header.CurrentRow ["CD_COMPANY"] = dlg.회사코드;
                    _header.CurrentRow["CD_PARTNER"] = dlg.거래처코드;
                    _header.CurrentRow["LN_PARTNER"] = dlg.거래처명;
                    _header.CurrentRow["CD_PURGRP"] = dlg.거래처그룹코드;
                    _header.CurrentRow["NM_PURGRP"] = dlg.거래처그룹명;
                    _header.CurrentRow["FG_LC"] = dlg.LC구분;
                    _header.CurrentRow["CD_BANK"] = dlg.은행코드;
                    _header.CurrentRow["NM_BANK"] = dlg.은행명;
                    //_header.CurrentRow["AM_EX"] = dlg.환율금액;
                    //_header.CurrentRow["AM"] = dlg.원화금액;
                    _header.CurrentRow["COND_PRICE"] = dlg.조건금액;
                    _header.CurrentRow["NO_EMP"] = dlg.담당자코드;
                    _header.CurrentRow["NM_KOR"] = dlg.담당자명;
                    _header.CurrentRow["REMARK"] = dlg.비고;
                    _header.CurrentRow["NO_SCLC"] = dlg.LC참조번호;
                    _header.CurrentRow["COND_PRICE"] = dlg.조건금액;

                    //_header.CurrentRow["CD_EXCH"] = dlg.환율코드;
                    //_header.CurrentRow["RT_EXCH"] = dlg.환율;
                    // 환종이 서로 다르면 적용받는 환종을 따른다.
                    // 그때 환율적용받는 환율을 따른다.
                    //rt_exch = Convert.ToDecimal(_header.CurrentRow["RT_EXCH"].ToString());
                    //if ((dlg.환율코드.ToString() == _header.CurrentRow["CD_EXCH"].ToString()) &&
                    //    (rt_exch != 1 && rt_exch != 0))
                    //{
                    //    rt_exch = Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]);
                    //}
                    //else
                    //{
                    //    _header.CurrentRow["CD_EXCH"] = dlg.환율코드;
                    //    _header.CurrentRow["RT_EXCH"] = dlg.환율;
                    //    rt_exch = Convert.ToDecimal(dlg.환율);
                    //}

                    //_header.CurrentRow["RT_EXCH"] = dlg_P_TR_LC_NO.GetResultTable.Rows[0]["RT_EXCH"];
                    //_header.CurrentRow["AM_EX"] = dlg_P_TR_LC_NO.GetResultTable.Rows[0]["AM_EX"];

                    // 2010.03.29 적용받을때는 무조건 적용을 받는쪽 환율,환종,금액을 따라간다. -smr(이형준/김형석)
                    _header.CurrentRow["CD_EXCH"] = dlg.환율코드;
                    _header.CurrentRow["RT_EXCH"] = dlg.환율;

                    _header.CurrentRow["AM_EX"] = D.GetDecimal(dlg.환율금액);
                    //m_txtAmtEx.Text = _header.CurrentRow["AM_EX"].ToString()); 2011-11-17, 최승애 수정함.(한국화스너)
                    m_txtAmtEx.DecimalValue = Unit.외화금액(DataDictionaryTypes.TR, D.GetDecimal(_header.CurrentRow["AM_EX"].ToString()));

                    _header.CurrentRow["AM"] = D.GetDecimal(dlg.원화금액);    //Decimal.Truncate(rt_exch * d_am_ex_sum);
                    //m_txtKorAmt.Text = _header.CurrentRow["AM"].ToString();   2011-11-17, 최승애 수정함.(한국화스너)
                    m_txtKorAmt.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_header.CurrentRow["AM"].ToString()));
                    
                    /////////////////////////////////////////////////////////////////////////////////////////////////



                    //CC코드/명칭
                    //m_CdCC = dlg.CC코드;
                    //m_NmCC = dlg.CC명;
                  

                    if (m_txtNoTo.Text != null)
                        _header.CurrentRow["NO_TO"] = m_txtNoTo.Text;

                    //기타값들...
                    _header.CurrentRow["YN_DISTRIBU"] = "N";								//정산여부
                    _header.CurrentRow["WEIGHT"] = 0;										//순중량
                    _header.CurrentRow["TOT_WEIGHT"] = 0;									//총중량

                    //참조시 비활성 컬럼 처리
                    m_txtCdTrans.ReadOnly = ReadOnly.TotalReadOnly;
                    m_txtOpenBank.ReadOnly = ReadOnly.TotalReadOnly;
                    m_comFgTrans.Enabled = false;
                    m_txtGroupRcv.Focus();

                }
                else
                {
                    참조BL번호 = dlg.Multi참조BL번호;
                    BL번호 = 참조BL번호.Split(new char[] { '|' });

                    //2011-09-05, 최승애 추가
                    if (BL번호.Length == 2)
                    {
                        m_tbBLRefer.Text = BL번호[0].ToString();         //참조BL번호.Replace("|", ",").ToString();
                        _header.CurrentRow["NO_SCBL"] = BL번호[0].ToString();
                    }
                    else
                    {
                        m_tbBLRefer.Text = 참조BL번호.Replace("|", ",").ToString();
                        _header.CurrentRow["NO_SCBL"] = 참조BL번호.Replace("|", ",").ToString();
                    }
                    
                    //m_tbBLRefer.Text = 참조BL번호.Replace("|", ",").ToString();
                    //_header.CurrentRow["NO_SCBL"] = 참조BL번호.Replace("|", ",").ToString();


                    //    _header.CurrentRow ["CD_COMPANY"] = dlg.회사코드;
                    _header.CurrentRow["CD_PARTNER"] = dlg.거래처코드;
                    _header.CurrentRow["LN_PARTNER"] = dlg.거래처명;
                    _header.CurrentRow["CD_PURGRP"] = dlg.거래처그룹코드;
                    _header.CurrentRow["NM_PURGRP"] = dlg.거래처그룹명;
                    _header.CurrentRow["FG_LC"] = dlg.LC구분;
                    _header.CurrentRow["CD_BANK"] = dlg.은행코드;
                    _header.CurrentRow["NM_BANK"] = dlg.은행명;
                    //_header.CurrentRow["AM_EX"] = dlg.환율금액;
                    //_header.CurrentRow["AM"] = dlg.원화금액;
                    _header.CurrentRow["COND_PRICE"] = dlg.조건금액;
                    _header.CurrentRow["NO_EMP"] = dlg.담당자코드;
                    _header.CurrentRow["NM_KOR"] = dlg.담당자명;
                    _header.CurrentRow["REMARK"] = dlg.비고;
                    _header.CurrentRow["NO_SCLC"] = dlg.LC참조번호;
                    _header.CurrentRow["COND_PRICE"] = dlg.조건금액;

                    //_header.CurrentRow["CD_EXCH"] = dlg.환율코드;
                    //_header.CurrentRow["RT_EXCH"] = dlg.환율;
                    // 환종이 서로 다르면 적용받는 환종을 따른다.
                    // 그때 환율적용받는 환율을 따른다.
                    //rt_exch = Convert.ToDecimal(_header.CurrentRow["RT_EXCH"].ToString());
                    //if ((dlg.환율코드.ToString() == _header.CurrentRow["CD_EXCH"].ToString()) &&
                    //    (rt_exch != 1 && rt_exch != 0))
                    //{
                    //    rt_exch = Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]);
                    //}
                    //else
                    //{
                    //    _header.CurrentRow["CD_EXCH"] = dlg.환율코드;
                    //    _header.CurrentRow["RT_EXCH"] = dlg.환율;
                    //    rt_exch = Convert.ToDecimal(dlg.환율);
                    //}

                    //_header.CurrentRow["RT_EXCH"] = dlg_P_TR_LC_NO.GetResultTable.Rows[0]["RT_EXCH"];
                    //_header.CurrentRow["AM_EX"] = dlg_P_TR_LC_NO.GetResultTable.Rows[0]["AM_EX"];

                    // 2010.03.29 적용받을때는 무조건 적용을 받는쪽 환율,환종,금액을 따라간다. -smr(이형준/김형석)
                    _header.CurrentRow["CD_EXCH"] = dlg.환율코드;
                    _header.CurrentRow["RT_EXCH"] = dlg.환율;

                    _header.CurrentRow["AM_EX"] = D.GetDecimal(dlg.환율금액);
                    //m_txtAmtEx.Text = _header.CurrentRow["AM_EX"].ToString();  2011-11-17, 최승애 (한국화스너)건으로 주석처리하고 아래라인으로 수정함.
                    m_txtAmtEx.DecimalValue = Unit.외화금액(DataDictionaryTypes.TR, D.GetDecimal(_header.CurrentRow["AM_EX"]));

                    _header.CurrentRow["AM"] = D.GetDecimal(dlg.원화금액);    //Decimal.Truncate(rt_exch * d_am_ex_sum);
                    //m_txtKorAmt.Text = _header.CurrentRow["AM"].ToString();    2011-11-17, 최승애 (한국화스너)건으로 주석처리하고 아래라인으로 수정함.
                    m_txtKorAmt.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_header.CurrentRow["AM"]));

                    /////////////////////////////////////////////////////////////////////////////////////////////////



                    //CC코드/명칭
                    //m_CdCC = dlg.CC코드;
                    //m_NmCC = dlg.CC명;

                    if (m_txtNoTo.Text != null)
                        _header.CurrentRow["NO_TO"] = m_txtNoTo.Text;

                    //기타값들...
                    _header.CurrentRow["YN_DISTRIBU"] = "N";								//정산여부
                    _header.CurrentRow["WEIGHT"] = 0;										//순중량
                    _header.CurrentRow["TOT_WEIGHT"] = 0;									//총중량

                    //참조시 비활성 컬럼 처리
                    m_txtCdTrans.ReadOnly = ReadOnly.TotalReadOnly;
                    m_txtOpenBank.ReadOnly = ReadOnly.TotalReadOnly;
                    m_comFgTrans.Enabled = false;
                    m_txtGroupRcv.Focus();


                }


                DataTable dt_bl = null;

                if (분할통관사용 == "000")
                    dt_bl = _biz.SearchLine(_header.CurrentRow["NO_SCBL"].ToString());     // SP_TR_LC_AFTER_SELECT_SAVE
                else
                    dt_bl = _biz.SearchLine2(참조BL번호);     // SP_TR_LC_AFTER_SELECT_SAVE


                _dt.Clear();
                if (dt_bl.Rows.Count > 0)
                {
                    _header.CurrentRow["CD_CC"] = dt_bl.Rows[0]["CD_CC"];
                    _header.CurrentRow["NM_CC"] = dt_bl.Rows[0]["NM_CC"];
                    if (분할통관사용 == "100")
                    {
                        foreach (DataRow dr in dt_bl.Rows)
                        {

                            _flex.Rows.Add();
                            _flex.Row = _flex.Rows.Count - 1;

                            _flex[_flex.Row, "S"] = "N";

                            iLine += 1;									//항번증가

                            _flex[_flex.Row, "NO_LINE"] = iLine;
                            _flex[_flex.Row, "NO_BL"] = dr["NO_BL"];
                            _flex[_flex.Row, "NO_BLLINE"] = dr["NO_BLLINE"];
                            _flex[_flex.Row, "CD_ITEM"] = dr["CD_ITEM"];
                            _flex[_flex.Row, "QT_BL"] = dr["QT_BL"];
                            _flex[_flex.Row, "CD_UNIT_MM"] = dr["CD_UNIT_MM"];

                            _flex[_flex.Row, "QT_QT_MM"] = dr["QT_BL_MM"];
                            _flex[_flex.Row, "QT_BL_MM"] = dr["QT_BL_MM"];
                            _flex[_flex.Row, "UM_EX_PO"] = dr["UM_EX_PO"];
                            _flex[_flex.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.TR, D.GetDecimal(dr["UM_EX"]));
                            _flex[_flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.TR, D.GetDecimal(dr["AM_EX"]));
                            _flex[_flex.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.TR, D.GetDecimal(dr["UM"]));
                            _flex[_flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(dr["AM"]));

                            _flex[_flex.Row, "CD_PJT"] = dr["CD_PJT"];
                            _flex[_flex.Row, "SEQ_PROJECT"] = dr["SEQ_PROJECT"];
                            _flex[_flex.Row, "YN_PURCHASE"] = dr["YN_PURCHASE"];
                            _flex[_flex.Row, "FG_TPPURCHASE"] = dr["FG_TPPURCHASE"];
                            _flex[_flex.Row, "CD_QTIOTP"] = dr["CD_QTIOTP"];
                            _flex[_flex.Row, "YN_AUTORCV"] = dr["YN_AUTORCV"];
                            _flex[_flex.Row, "CD_PLANT"] = dr["CD_PLANT"];
                            _flex[_flex.Row, "CD_SL"] = dr["CD_SL"];
                            _flex[_flex.Row, "NM_SL"] = dr["NM_SL"];
                            _flex[_flex.Row, "DT_DELIVERY"] = dr["DT_DELIVERY"];
                            _flex[_flex.Row, "CD_COMPANY"] = dr["CD_COMPANY"];
                            _flex[_flex.Row, "NM_ITEM"] = dr["NM_ITEM"];
                            _flex[_flex.Row, "STND_ITEM"] = dr["STND_ITEM"];
                            _flex[_flex.Row, "NM_PLANT"] = dr["NM_PLANT"];
                            _flex[_flex.Row, "NM_PJT"] = dr["NM_PJT"];
                            _flex[_flex.Row, "RATE_EXCHG"] = dr["RATE_EXCHG"];
                            _flex[_flex.Row, "RT_EXCH"] = D.GetDecimal(m_txtRateLicense.DecimalValue);    //통관환율로

                            _flex[_flex.Row, "CD_EXCH"] = dr["CD_EXCH"];
                            _flex[_flex.Row, "AM_BL"] = dr["AM_BL"];
                            _flex[_flex.Row, "RT_CUSTOMS"] = dr["RT_CUSTOMS"];
                            _flex[_flex.Row, "RT_SPECT"] = dr["RT_SPECT"];
                            _flex[_flex.Row, "QT_TO_MM"] = dr["QT_TO_MM"];
                            _flex[_flex.Row, "QT_TO"] = dr["QT_TO"];
                            _flex[_flex.Row, "RCVL"] = dr["RCVL"];
                            _flex[_flex.Row, "BL_RT_EXCH"] = dr["BL_RT_EXCH"];      //2011-09-07, 최승애, BL환율 추가함.
                            _flex[_flex.Row, "AM_BL_TOT"] = dr["AM_BL_TOT"];      //2011-09-14, 최승애 추가
                            _flex[_flex.Row, "AM_TO"] = dr["AM_TO"];    //2011-09-14, 최승애 추가
                            _flex[_flex.Row, "QT_JAN_BL_MM"] = dr["QT_JAN_BL_MM"];    //2011-09-14, 최승애 추가
                            _flex[_flex.Row, "CD_CC"] = dr["CD_CC"]; 
                            _flex[_flex.Row, "NM_CC"] = dr["NM_CC"];

                            // 리베이트

                            if (dr.Table.Columns.Contains("UM_REBATE"))
                                _flex[_flex.Row, "UM_REBATE"] = dr["UM_REBATE"];

                            if (dr.Table.Columns.Contains("AM_REBATE_EX"))
                                _flex[_flex.Row, "AM_REBATE_EX"] = dr["AM_REBATE_EX"];

                            if (dr.Table.Columns.Contains("AM_REBATE"))
                            {
                             //   _flex[_flex.Row, "AM_REBATE"] = dr["AM_REBATE"];
                                _flex[_flex.Row, "AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_header.CurrentRow["RT_EXCH"]) * Convert.ToDecimal(_flex[_flex.Row, "AM_REBATE_EX"]));
                            }


                            _flex.AddFinished();
                            _flex.Col = _flex.Cols.Fixed;
                        }
                    }
                    else
                    {          
                        //기본설정 처리시

                        DataRow dr_new;
                        foreach (DataRow dr in dt_bl.Rows)
                        {
                            dr_new = _dt.NewRow();
                            dr_new["NO_BL"] = _header.CurrentRow["NO_SCBL"];
                            dr_new["NO_TO"] = _header.CurrentRow["NO_TO"];
                            dr_new["NO_LINE"] = dr["NO_LINE"];   
                            dr_new["CD_COMPANY"] = dr["CD_COMPANY"];   
                            dr_new["NO_BL"] = dr["NO_BL"];   
                            dr_new["NO_BLLINE"] = dr["NO_LINE"];   
                            dr_new["CD_ITEM"] = dr["CD_ITEM"];   
                            dr_new["QT_TO"] = dr["QT_BL"];   
                            dr_new["QT_REQ"] = 0;   
                            dr_new["CD_UNIT_MM"] = dr["CD_UNIT_MM"];   
                            dr_new["QT_TO_MM"] = dr["QT_BL_MM"];   
                            dr_new["QT_REQ_MM"] = 0;   
                            dr_new["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.TR, D.GetDecimal(dr["UM_EX_PO"]));
                            dr_new["UM_EX"] = Unit.외화단가(DataDictionaryTypes.TR, D.GetDecimal(dr["UM_EX"]));   
                            dr_new["AM_EX"] =  Unit.외화금액(DataDictionaryTypes.TR, D.GetDecimal(dr["AM_EX"]));   
                            dr_new["UM"] = Unit.원화단가(DataDictionaryTypes.TR , D.GetDecimal(dr["UM"]));                           
                            dr_new["CD_PJT"] = dr["CD_PJT"];
                            dr_new["SEQ_PROJECT"] = dr["SEQ_PROJECT"];
                            dr_new["YN_PURCHASE"] = dr["YN_PURCHASE"];
                            dr_new["YN_AUTORCV"] = dr["YN_AUTORCV"];   
                            dr_new["FG_TPPURCHASE"] = dr["FG_TPPURCHASE"]; 
                            dr_new["CD_QTIOTP"] = dr["CD_QTIOTP"];   
                            dr_new["RT_CUSTOMS"] = 0;   
                            dr_new["RT_SPEC"] = 0;   
                            dr_new["AM_EXREQ"] = 0;
                            dr_new["AM_REQ"] = 0;
                            dr_new["CD_PLANT"] = dr["CD_PLANT"];
                            dr_new["CD_SL"] = dr["CD_SL"];

                            //dr_new["AM"] = Decimal.Truncate(rt_exch * Convert.ToDecimal(dr["AM_EX"]));  //dr["AM"];2009.11.09 무조건 header쪽
                            dr_new["AM"] =   Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(dr["AM"]));    //다시 원점으로 header쪽에서 edit할때에 통관쪽 환율을 적용한다 2010.03.29 smr(김형석,이형준)
                            dr_new["AM_BL"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(dr["AM"]));   //2009.12.09 SMR 
                            dr_new["CD_CC"] = dr["CD_CC"];
                            dr_new["NM_CC"] = dr["NM_CC"];

                            // 리베이트
                            if (dr.Table.Columns.Contains("UM_REBATE"))
                                dr_new["UM_REBATE"] = dr["UM_REBATE"];


                            if (dr.Table.Columns.Contains("AM_REBATE_EX"))
                                dr_new["AM_REBATE_EX"] = dr["AM_REBATE_EX"];

                            if (dr.Table.Columns.Contains("AM_REBATE"))
                            {
                             //   dr_new["AM_REBATE"] = dr["AM_REBATE"];                                
                                dr_new["AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_header.CurrentRow["RT_EXCH"]) * Convert.ToDecimal(dr["AM_REBATE_EX"]));
                            }
                           // d_am_sum += Convert.ToDecimal(dr_new["AM"]);
                            //d_am_ex_sum += Convert.ToDecimal(dr_new["AM_EX"]);

                            _dt.Rows.Add(dr_new);
                        }
                  }

                }

                _flex.SumRefresh();
                콘트롤초기화();

                if (분할통관사용 == "100")  SetAmApply();           //2011-08-24, 최승애

                헤더변경여부 = true;
    
            }
        }

        #endregion

        #region ▶ BeforeSave
   
        protected override bool BeforeSave()
        {

            if ( !Check() )
                return false;

            if ( !this.Verify() )     // 그리드 체크
                return false;

            return true;
        }
        #endregion

        #region ▶ Control Event 정의(거래처, 은행, 담당자등..)
        /// <summary>
        /// 정산상태 Changed Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void m_txtStDistribut_TextChanged(object sender, System.EventArgs e)
        {
            switch ( m_txtStDistribut.Text )
            {
                case "Y":
                    m_txtStDistribut.Text = DD("정산");
                    break;
                case "N":
                    m_txtStDistribut.Text = DD("미정산");
                    break;
            }
        }

        private bool Check()
        {
            //신고번호  
            if ( m_txtNoTo.Text == "" || m_txtNoTo.Text == string.Empty )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblNoTo.Text );
                m_txtNoTo.Focus();
                return false;
            }

            //신고일
            if ( m_txtToDate.Text == "" || m_txtToDate.Text == string.Empty )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblToDate.Text );
                m_txtNoTo.Focus();
                return false;
            }

            //BL번호
            if ( m_tbBLRefer.Text == "" || m_tbBLRefer.Text == string.Empty )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblNoBl.Text );
                m_txtNoTo.Focus();
                return false;

            }

            //거래처
            if ( ( m_txtCdTrans.IsEmpty() || m_txtCdTrans.CodeValue == "" || m_txtCdTrans.CodeValue == null ) )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblCdTrans.Text );
                m_txtNoTo.Focus();
                return false;

            }

            //거래구분
            if ( m_comFgTrans.SelectedValue == DBNull.Value ||  D.GetString(m_comFgTrans.SelectedValue) == string.Empty )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblFgTrans.Text );
                m_txtNoTo.Focus();
                return false;

            }

            //구매그룹
            if ( ( m_txtGroupRcv.IsEmpty() || m_txtGroupRcv.CodeValue == "" || m_txtGroupRcv.CodeValue == null ) )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblGroupRcv.Text );
                m_txtNoTo.Focus();
                return false;

            }

            //담당자
            if ( ( m_txtNmEmp.IsEmpty() || m_txtNmEmp.CodeValue == "" || m_txtNmEmp.CodeValue == null ) )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblNmEmp.Text );
                m_txtNoTo.Focus();
                return false;

            }

            //외화금액
            //if ( m_txtAmtEx.Text == "" || m_txtAmtEx.Text == string.Empty )
            //{
            //    this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblAmtEx.Text );
            //    m_txtNoTo.Focus();
            //    return false;

            //}

            //통화
            if ( m_comCdCurrency.SelectedValue == DBNull.Value || D.GetString(m_comCdCurrency.SelectedValue) == string.Empty )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblCdCurrency.Text );
                m_txtNoTo.Focus();
                return false;

            }

            ////면장환율
            //if ( m_txtRateLicense.Text == "" || m_txtRateLicense.Text == string.Empty )
            //{
            //    this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblRateLicense.Text );
            //    m_txtNoTo.Focus();
            //    return false;


            //}



            //가격조건
            if ( m_comCondPrice.SelectedValue == DBNull.Value || D.GetString(m_comCondPrice.SelectedValue) == string.Empty )
            {
                this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblCondPrice.Text );
                m_txtNoTo.Focus();
                return false;

            }

            ////원화금액
            //if ( m_txtKorAmt.Text == "" || m_txtKorAmt.Text == string.Empty )
            //{
            //    this.ShowMessage( 공통메세지._은는필수입력항목입니다, m_lblKorAmt.Text );
            //    m_txtNoTo.Focus();
            //    return false;
            //}
            return true;
        }


        /// <summary>
        /// 통화구분에서 항목을 선택했을 경우 발생 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void m_comCdCurrency_SelectionChangeCommitted(object sender, System.EventArgs e)
        //{
        //    //통화가 "KRW"일경우는 환율은 1로 Setting한다.(Protect)
        //    _header.CurrentRow["CD_EXCH"] = m_comCdCurrency.SelectedValue.ToString();
        //    if ( m_comCdCurrency.SelectedValue.ToString() == "000" )
        //    {
        //        m_txtRateLicense.DecimalValue = 1;
        //        m_txtRateLicense.BackColor = System.Drawing.SystemColors.Control;
        //        m_txtRateLicense.Enabled = false;
        //        _header.CurrentRow["RT_EXCH"] = 1;
        //        ComputeKorAmount(1);
        //    }
        //    else
        //    {
        //        m_txtRateLicense.BackColor = System.Drawing.Color.FromArgb( ( ( System.Byte )( 226 ) ), ( ( System.Byte )( 239 ) ), ( ( System.Byte )( 243 ) ) );
        //        m_txtRateLicense.Enabled = true;
        //    }

        //}

        /// <summary>
        /// 원화금액을 계산하여 준다(환율 * 외화금액)
        /// </summary>
        private void ComputeKorAmount()
        {
            try
            {
                Decimal am_won = 0, am_sum = 0;
                if (_dt == null && _dt.Rows.Count < 1) return;
                
                foreach (DataRow dr in _dt.Rows)
                {
                    //dr["AM"] = Decimal.Truncate(Convert.ToDecimal(dr["AM_EX"]) * Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]));
                    dr["AM"] = Unit.원화금액(DataDictionaryTypes.TR,  Convert.ToDecimal(dr["AM_EX"]) * Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]));
                    dr["AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(dr["AM_REBATE_EX"]) * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));
                    am_sum += Convert.ToDecimal(dr["AM"]);
                }
                CalcAdujstAm();

                 //am_won = Decimal.Truncate(Convert.ToDecimal(_header.CurrentRow["AM_EX"]) * Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]));

                //am_won = Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(_header.CurrentRow["AM_EX"]) * Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]));

                //_header.CurrentRow["AM"] = am_won;
                //m_txtKorAmt.DecimalValue = am_won;
                             
                //if (am_sum != am_won)
                //{
                //    _dt.Rows[_dt.Rows.Count - 1]["AM"] = Convert.ToDecimal(_dt.Rows[_dt.Rows.Count - 1]["AM"]) + (am_won - am_sum);
                //}
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }
        #region -> 원미만처리(끝전금액 오류 보정)//금액보정을 위해 2009.10.21 smr
        /// <summary>
        /// 단수차이 계산한다.
        /// </summary>
        private void CalcAdujstAm()
        {
            Decimal am_kor_sum = 0, am_sum = 0, am_ex_sum = 0;
            Decimal am_rebate = 0, am_rebate_ex = 0, sum_rebate = 0;

            if (_dt == null || _dt.Rows.Count < 1) return;

            foreach (DataRow dr in _dt.Rows)
            {
                am_sum += Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(dr["AM"]));
                am_ex_sum += Unit.외화금액(DataDictionaryTypes.TR, Convert.ToDecimal(dr["AM_EX"]));
                am_rebate += D.GetDecimal(dr["AM_REBATE"]);
                am_rebate_ex += D.GetDecimal(dr["AM_REBATE_EX"]);

            }

            //am_kor_sum = Decimal.Truncate(Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]) * am_ex_sum);
            am_kor_sum = Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]) * am_ex_sum);

            sum_rebate = Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]) * am_rebate_ex);
            am_rebate = Unit.원화금액(DataDictionaryTypes.TR, am_rebate);

            if (am_kor_sum != am_sum)
            {
                Decimal am_max = D.GetDecimal(_dt.Compute("MAX(AM)", ""));
                DataRow[] drs = _dt.Select("AM = " + D.GetDecimal(am_max));
                drs[0]["AM"] = Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(drs[0]["AM"]) + (am_kor_sum - am_sum));

            }

            if (sum_rebate != am_rebate)
            {
                Decimal am_max_re = D.GetDecimal(_dt.Compute("MAX(AM_REBATE)", ""));
                DataRow[] drs_re = _dt.Select("AM_REBATE = " + D.GetDecimal(am_max_re));
                drs_re[0]["AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(drs_re[0]["AM_REBATE"]) + (sum_rebate - am_rebate));
            }

        }       
        //private void CalcAdujstAm()

        //{
        //    Decimal am = 0; 
        //    Decimal am_sum = 0;
        //    if (_dt.Rows.Count < 1) return;
        //    foreach (DataRow dr in _dt.Rows)
        //    {
        //        am += D.GetDecimal(dr["AM_EX"]);
        //        am_sum += D.GetDecimal(dr["AM"]);
        //    }

        //    //am = Decimal.Truncate(am * Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]));
        //    am = Unit.원화금액(DataDictionaryTypes.TR ,  am * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));

        //    am_sum = Unit.원화금액(DataDictionaryTypes.TR, am_sum);  

        //    if (am != am_sum)
        //    {
        //        Decimal am_max = D.GetDecimal(_flex.DataTable.Compute("MAX(AM)", ""));
        //        DataRow[] drs = _flex.DataTable.Select("AM = " + D.GetDecimal(am_max));
        //        drs[0]["AM"] = Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(drs[0]["AM"]) + (am - am_sum) );

        //        //_dt.Rows[_dt.Rows.Count - 1]["AM"] = D.GetDecimal(_dt.Rows[_dt.Rows.Count - 1]["AM"]) + (am - am_sum);                
        //    }
        //}
        #endregion

        #region -> 콘트롤초기화()

        public void 콘트롤초기화()
        {
            try
            {
                m_txtCdTrans.CodeName = _header.CurrentRow["LN_PARTNER"].ToString();
                m_txtCdTrans.CodeValue = _header.CurrentRow["CD_PARTNER"].ToString();

                m_txtOpenBank.CodeName = _header.CurrentRow["NM_BANK"].ToString();
                m_txtOpenBank.CodeValue = _header.CurrentRow["CD_BANK"].ToString();

                m_txtGroupRcv.CodeName = _header.CurrentRow["NM_PURGRP"].ToString();   // 구매그룹CD_PURGRP;NM_PURGRP
                m_txtGroupRcv.CodeValue = _header.CurrentRow["CD_PURGRP"].ToString();

                m_txtNmEmp.CodeName = _header.CurrentRow["NM_KOR"].ToString();  //담당자NO_EMP;NM_KOR
                m_txtNmEmp.CodeValue = _header.CurrentRow["NO_EMP"].ToString();

                m_txtCondCustom.CodeName = _header.CurrentRow["NM_CUSTOMS"].ToString();  //관할세관 CD_CUSTOMS;NM_CUSTOMS
                m_txtCondCustom.CodeValue = _header.CurrentRow["CD_CUSTOMS"].ToString();

                m_comFgTrans.SelectedValue = _header.CurrentRow["FG_LC"].ToString();
                m_comCdCurrency.SelectedValue = _header.CurrentRow["CD_EXCH"].ToString();

                m_tbBLRefer.Text = _header.CurrentRow["NO_SCBL"].ToString();			//L/C


                if (D.GetString(m_comCdCurrency.SelectedValue) == "000")                //통화가 "KRW"일경우는 환율은 1로 Setting한다.(Protect)
                {
                    m_txtRateLicense.BackColor = System.Drawing.SystemColors.Control;
                    m_txtRateLicense.Enabled = false;
                }
                else
                {
                    m_txtRateLicense.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(237)), ((System.Byte)(242)));
                    m_txtRateLicense.Enabled = true;
                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        m_txtRateLicense.Enabled = false;
                }

                SetExchageApply();

                m_txtRateLicense.DecimalValue = D.GetDecimal(_header.CurrentRow["RT_EXCH"]);
                m_txtKorAmt.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_header.CurrentRow["AM"]));
                m_txtAmtEx.DecimalValue = Unit.외화금액(DataDictionaryTypes.TR,  D.GetDecimal(_header.CurrentRow["AM_EX"]));
                // 2010.02.27 
                //가격조건 기본값 설정                
                m_comCondPrice.SelectedValue = _header.CurrentRow["COND_PRICE"];

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion


        private void SetControlEnable(bool isEnable)
        {
            m_comCondPrice.Enabled = isEnable; // 가격조건
            m_txtToDate.Enabled = isEnable;
            m_txtNmEmp.Enabled = isEnable;

            m_txtAmtEx.Enabled = isEnable;
            //      m_txtAmtLicense.Enabled = isEnable;     면장금액 삭제 By 2007.06.15 CCJN
            m_txtKorAmt.Enabled = isEnable;

            m_txtNoTo.Enabled = isEnable;      //TO번호
            m_comCdCurrency.Enabled = isEnable; //통화
            m_txtRateLicense.Enabled = isEnable;  //적용환율
            if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가 && isEnable == true)
                m_txtRateLicense.Enabled = false;

            m_txtGroupRcv.Enabled = isEnable;  //구매그룹
            m_tbBLRefer.Enabled = isEnable;              //BL참조


            m_txtCdTrans.Enabled = isEnable;              //참조시 비활성 컬럼 처리
            m_comFgTrans.Enabled = isEnable;
            m_txtOpenBank.Enabled = isEnable;

            //if ( isEnable )
            //{
            //        m_comCdCurrency.SelectedValue = "000";
            //        m_txtRateLicense.Enabled = false;
            //}

            if ( isEnable == true )
            {
                m_txtGroupRcv.ReadOnly = ReadOnly.None;
                m_txtCdTrans.ReadOnly = ReadOnly.None;
            }
            else
            {
                m_txtGroupRcv.ReadOnly = ReadOnly.TotalReadOnly;
                m_txtCdTrans.ReadOnly = ReadOnly.TotalReadOnly;
            }

            //m_txtCustomsDate.Enabled = isEnable;        //통관일 추가,  2011-11-17, 최승애


        }
        #endregion

        #region -> _header_JobModeChanged

        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if ( e.JobMode == JobModeEnum.추가후수정 )
                {

                   // m_txtNoTo.Enabled = true;
                    m_txtNoTo.Focus();

                    m_btnImportCost.Enabled = false;
                    m_btnConfirmItem.Enabled = false;

                    this.ToolBarAddButtonEnabled = true;
                    this.ToolBarDeleteButtonEnabled = false;

                    SetControlEnable( true );

                }
                else
                {

                //    m_txtNoTo.Enabled = false;

                    m_btnImportCost.Enabled = true;
                    m_btnConfirmItem.Enabled = true;

                    this.ToolBarAddButtonEnabled = true;
                    this.ToolBarDeleteButtonEnabled = true;

                    SetControlEnable( false );

                }
            }
            catch ( Exception ex )
            {
                this.MsgEnd( ex );
            }
        }

        #endregion

        #region -> _header_ControlValueChanged

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                //if (_header.CurrentRow["NO_COST"].ToString() != "" && _header.CurrentRow["NO_COST"].ToString() != String.Empty)
                //{
                //    ShowMessage("부대비용이 등록되어 있어 수정 할 수 없습니다");
                //    return;
                //}

                switch ( ( ( Control )sender ).Name )
                {
                    case "m_txtNoTo":
                        if ( m_txtNoTo.Text != "" && _header.JobMode == JobModeEnum.추가후수정 )
                            ToolBarSaveButtonEnabled = true;
                    

                        foreach (DataRow dr in _dt.Rows)
                        {
                            dr["NO_TO"] = m_txtNoTo.Text;
                        }
                        break;

                    case "m_comFgTrans": // LC구분
                        if ( D.GetString(m_comFgTrans.SelectedValue) == "004" )    //L/C구분이 "Master L/C"일 경우는 L/C참조 항목은 필수처리
                            m_tbBLRefer.BackColor = System.Drawing.Color.FromArgb( ( ( System.Byte )( 255 ) ), ( ( System.Byte )( 237 ) ), ( ( System.Byte )( 242 ) ) );
                        else
                            m_tbBLRefer.BackColor = System.Drawing.Color.White;
                        break;

                    case "m_comCdCurrency":  //  통화

                        if ( D.GetString(m_comCdCurrency.SelectedValue) == "000" )   //통화가 "KRW"일경우는 환율은 1로 Setting한다.(Protect)
                        {
                            //m_txtRateLicense.DecimalValue = 1;
                            m_txtRateLicense.BackColor = System.Drawing.SystemColors.Control;
                            m_txtRateLicense.Enabled = false;
                           // _header.CurrentRow ["RT_EXCH"] = 1;
                        }
                        else
                        {
                            m_txtRateLicense.BackColor = System.Drawing.Color.FromArgb( ( ( System.Byte )( 255 ) ), ( ( System.Byte )( 237 ) ), ( ( System.Byte )( 242 ) ) );
                            m_txtRateLicense.Enabled = true;
                            if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                                m_txtRateLicense.Enabled = false;
                        }

                        SetExchageApply();

                        break;
                    case "m_txtToDate":
                         SetExchageApply();                        
                        break;
                    case "m_txtRateLicense": // 면장환율

                                if (IsExistPage("P_TR_TO_LINE", false))
                                    UnLoadPage("P_TR_TO_LINE", false);

                                ComputeKorAmount();
                                //m_PrevRtExch = e.Row["RT_EXCH"].ToString();

                                _flex.SumRefresh();
                                SetAmApply();  //2011-08-24, 최승애

                        break;
              
                    default:
                        this.ToolBarSaveButtonEnabled = true;
                        break;
                }
            }
            catch ( Exception ex )
            {
                this.MsgEnd( ex );
            }
        }
        #endregion

        #region -> 환율 적용 함수
        private void SetExchageApply()
        {
            decimal ld_rate_base = 0;

            if (D.GetString(m_comCdCurrency.SelectedValue) == "000")   //통화가 "KRW"일경우는 환율은 1로 Setting한다.(Protect)
            {
                ld_rate_base = 1;
            }
            else
            {
                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                {
                    //ld_rate_base = _biz.ExchangeSearch(m_txtToDate.Text, m_comCdCurrency.SelectedValue.ToString());
                    ld_rate_base = MA.기준환율적용(m_txtToDate.Text, D.GetString(m_comCdCurrency.SelectedValue));
                }
            }

            if (ld_rate_base != m_txtRateLicense.DecimalValue )
            {
                    m_txtRateLicense.DecimalValue = ld_rate_base;
                    _header.CurrentRow["RT_EXCH"] = ld_rate_base;

                    //m_PrevRtExch = m_txtRateLicense.Text;
                    ComputeKorAmount();
            }

        }
        #endregion


        #region
        private void SetAmApply()
        {

            //===========================================================================
            // 2011-08-24, 최승애 추가함.
            //===========================================================================
            decimal 총외화금액 = 0;
            decimal 총원화금액 = 0;

            //foreach (DataRow dr in _flex.DataView.ToTable().Rows)
            //{
            //    dr["AM"] = Unit.원화금액(DataDictionaryTypes.TR, Convert.ToDecimal(dr["AM_EX"]) * Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]));  //원화금액 = 금액 * 환율


            //    총외화금액 += D.GetDecimal(dr["AM_EX"]);
            //    총원화금액 += D.GetDecimal(dr["AM"]);
            //}

            decimal 환율 = 0;

            환율 = D.GetDecimal(_header.CurrentRow["RT_EXCH"]);

            for (int i = 2; i <= _flex.Rows.Count - 1; i++)
            {
                //2011-11-10, 최승애 수정 (DataDictionaryTypes.TRE -> DataDictionaryTypes.TR)
                //_flex[i, "AM"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_flex[i, "AM_EX"]) * 환율);


                총외화금액 += D.GetDecimal(_flex[i, "AM_EX"]);
                총원화금액 += D.GetDecimal(_flex[i, "AM"]);
            }


            _header.CurrentRow["AM_EX"] = 총외화금액;
            m_txtAmtEx.DecimalValue = 총외화금액;


            _header.CurrentRow["AM"] = 총원화금액;
            m_txtKorAmt.DecimalValue = 총원화금액;

           
            //===========================================================================


        }
        #endregion


        #region -> 수량 변경시 Validate Check 해주는 이벤트(_flex_ValidateEdit)

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {   
            try
            {
                string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                string newValue = ((FlexGrid)sender).EditData;
                if (oldValue.ToUpper() == newValue.ToUpper()) return;

                
                switch (_flex.Cols[e.Col].Name)
                {
                    case "QT_TO_MM":

                        decimal ldb_qt = 0;

                        ldb_qt = System.Decimal.Parse(_flex.EditData);
                        
                        if (ldb_qt == 0)
                        {
                            ShowMessage("통관 수량은 0을 입력할 수 없습니다.");
                            ((FlexGrid)sender)["QT_TO_MM"] = ((FlexGrid)sender).GetData(e.Row, e.Col);
                            return;
                        }



                        if (ldb_qt > D.GetDecimal(_flex[_flex.Row, "QT_BL_MM"]))
                        {
                            ShowMessage("BL 수량을 초과할 수 없습니다.");
                            ((FlexGrid)sender)["QT_TO_MM"] = ((FlexGrid)sender).GetData(e.Row, e.Col);
                            return;
                        }

                        
                        _flex[_flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.TR, ldb_qt * D.GetDecimal(_flex[_flex.Row, "UM_EX_PO"]));  //금액 = 수배수량 * 수배단가
                        if (_flex[_flex.Row, "RATE_EXCHG"] == DBNull.Value)
                            _flex[_flex.Row, "RATE_EXCHG"] = 1;
                        _flex[_flex.Row, "QT_TO"] = Unit.수량(DataDictionaryTypes.TR, ldb_qt * D.GetDecimal(_flex[_flex.Row, "RATE_EXCHG"]));  //BL수량 = 수배수량 * 단위환산율
                        
                        _flex[_flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal (_flex[_flex.Row, "AM_EX"]) * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));  //원화금액 = 금액 * 환율

                        //2011-09-07, 최승애, BL금액
                        if (D.GetDecimal(_flex[_flex.Row, "QT_JAN_BL_MM"]) - D.GetDecimal(_flex[_flex.Row, "QT_TO_MM"]) == 0)
                        {
                            _flex[_flex.Row, "AM_BL"] = Unit.원화금액(DataDictionaryTypes.TR,  D.GetDecimal(_flex[_flex.Row, "AM_BL_TOT"]) - D.GetDecimal(_flex[_flex.Row, "AM_TO"]));
                        }
                        else
                        {
                            _flex[_flex.Row, "AM_BL"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_flex[_flex.Row, "AM_EX"]) * D.GetDecimal(_flex[_flex.Row, "BL_RT_EXCH"]));  //원화금액 = 금액 * 환율
                        }
                        //////////////////////////////////////////////////////////////////////////////


                        SetAmApply();  //2011-08-24, 최승애


                        ////===========================================================================
                        //// 2011-08-24, 최승애 추가함.
                        ////===========================================================================
                        //decimal 총외화금액 = 0;
                        //decimal 총원화금액 = 0;

                        //foreach (DataRow dr in _flex.DataView.ToTable().Rows)
                        //{
                        //    총외화금액 += D.GetDecimal(dr["AM_EX"]);
                        //    총원화금액 += D.GetDecimal(dr["AM"]);
                        //}

                        //m_txtAmtEx.DecimalValue = 총외화금액;
                        //m_txtKorAmt.DecimalValue = 총원화금액;

                        ////===========================================================================

                        break;                                 
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion




        #region -> 그리드 변경 시작시 체크 이벤트(Grid_StartEdit)

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {


                if (_flex.RowState() != DataRowState.Added && _flex.Cols[e.Col].Name != "S")
                    e.Cancel = true;
                
                //switch (_flexD.Cols[e.Col].Name)
                //{
                //    case "CD_ITEM":
                //        if (_flexD.RowState() != DataRowState.Added)
                //            e.Cancel = true;
                //        break;
                //}

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        private void btn삭제_Click(object sender, EventArgs e)
        {

            try{


                if (!_flex.HasNormalRow) return;


                DataRow[] dr = _flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);


                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }


                _flex.Redraw = false;

                //if (D.GetString(_flex["RCVL"]) != "")     // 확정여부가 미정일 경우                        
                //{
                //    ShowMessage("입고되지 않은 경우에만 삭제가능합니다");
                //    return;
                //}


                //_flex.Rows.Remove(_flex.Row);


                foreach (DataRow rowL in dr)
                {
                    if (D.GetString(rowL["RCVL"]) == "")     // 확정여부가 미정일 경우                        
                    {
                        rowL.Delete();
                    }
                    else
                    {
                        ShowMessage("입고되지 않은 경우에만 삭제가능합니다");
                        return;
                    }
                }


                _flex.Redraw = true;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {

            try
            {

                if (m_txtNoTo.Text == "")
                {
                    ShowMessageKor("신고번호를 먼저 등록하셔야합니다.");
                    return;
                }


                string cd_file_code = D.GetString(m_txtNoTo.Text); //파일 PK설정   공장코드_검사성적서번호
                master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("TR", Global.MainFrame.CurrentPageID, cd_file_code);


                if (Global.MainFrame.ServerKey == "FORTIS")
                {
                    m_dlg.YnUNC = true;
                    m_dlg.UNCID = "erp";
                    m_dlg.UNCPassword = "erp_disk1223";
                    m_dlg.UNCPath = @"\\192.168.4.235\homes\erp";
                }


                if (m_dlg.ShowDialog(this) == DialogResult.Cancel)
                    return;

            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }

        }


        #region ★ 속성
        #region -> 부대비용창존재여부
        private bool 부대비용창존재여부
        {
            get
            {

                if (IsExistPage("P_TR_IMCOST", false))
                {
                    if (ShowMessage("부대비용등록창을 먼저 닫아야 합니다. 창을 닫으시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return false;

                    UnLoadPage("P_TR_IMCOST", false);

                }
                return true;
            }
        }
        #endregion


        #region -> 내역등록창존재여부
        private bool 내역등록창존재여부
        {
            get
            {
                if (IsExistPage("P_TR_TO_LINE", false))
                {
                    if (ShowMessage("내역등록창을 먼저 닫아야 합니다. 창을 닫으시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return false;

                    UnLoadPage("P_TR_TO_LINE", false);

                }
                return true;
            }
        }
        #endregion  

        #region 분할통관사용
        
        private string 분할통관사용
        {
            get
            {
                return  Duzon.ERPU.MF.ComFunc.전용코드("수입통관등록-분할통관사용");

            }
        }
        #endregion  

        #region -> 내역등록 여부
        private bool EXIST_NO_TO
        {
            get
            {
                return ((_dt != null && _dt.Rows.Count > 0)  || (_flex.DataTable !=null && _flex.DataTable.Rows.Count > 0) ? true : false); 
            }
        }
        #endregion
        #endregion

     

    }
}