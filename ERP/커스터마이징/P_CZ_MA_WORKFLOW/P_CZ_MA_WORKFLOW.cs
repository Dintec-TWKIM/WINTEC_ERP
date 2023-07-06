using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using System.IO;
using System.Diagnostics;
using System.Net;
using Duzon.Erpiu.ComponentModel;
using System.Collections.Generic;

namespace cz
{
    public partial class P_CZ_MA_WORKFLOW : PageBase
    {
        #region 초기화
        private P_CZ_MA_WORKFLOW_BIZ _biz = new P_CZ_MA_WORKFLOW_BIZ();
        
        private 진행단계 선택단계;
        private bool isLoaded;
        private bool 수정가능여부;
        private string 선택회사코드, 견적작성필터 = "002", 계산서발행필터 = "001";
        private string 임시폴더;
        private bool _isPageLoad = false;
        private int _미출고관리일수;

        public P_CZ_MA_WORKFLOW()
        {
            StartUp.Certify(this);
            InitializeComponent();

			if (!Certify.IsLive())
			{
				this.btn장기미출고.Visible = false;
				this.btn협조전완결.Visible = false;
				this.btn납품완결.Visible = false;
				this.btn인수증등록.Visible = false;

				this.btn재고견적.Visible = false;
				this.btn인수증확인.Visible = false;

				this.oneGridItem4.Visible = false;

				this.splitContainer1.SplitterDistance = 320;
				this.splitContainer1_SplitterMoved(null, null);
			}

			this.MainGrids = new FlexGrid[] { this._고객문의등록H, this._고객문의등록L,
                                              this._매입문의등록H, this._매입문의등록L,
                                              this._문의서검토H, this._문의서검토L1, this._문의서검토L2,
                                              this._매입가등록H, this._매입가등록L1, this._매입가등록L2,
                                              this._견적작성H, this._견적작성L,
                                              this._재고견적H, this._재고견적L,
                                              this._고객문의클로징H,
                                              this._견적제출H,
                                              this._수주등록H, this._수주등록L1, this._수주등록L2,
                                              this._매입구매H, this._매입구매L1, this._매입구매L2, this._매입구매L3,
                                              this._수주확인서H, this._수주확인서L1, this._수주확인서L2,
                                              this._수발주통보서H,
                                              this._수발주통보서결재H,
                                              this._납품목록H, this._납품목록L,
                                              this._장기미출고H,
                                              this._협조전작성H, this._협조전작성L,
                                              this._협조전완결H,
                                              this._납품완결H,
                                              this._인수증등록H,
                                              this._계산서발행H, this._계산서발행L,
                                              this._클레임종결H,
											  this._인수증확인H, this._인수증확인L,
                                              this._계산서확인H };
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.임시폴더 = "temp";

            this.InitEvent();
            this.InitDD();
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.isLoaded = false;

                this._미출고관리일수 = Convert.ToInt32(DBHelper.ExecuteScalar(@"SELECT CD_FLAG1 
                                                                               FROM MA_CODEDTL
                                                                               WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                                             @"AND CD_FIELD = 'CZ_SA00035'
                                                                               AND CD_SYSDEF = '001'"));

                this.ctx회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
                this.ctx회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;
                this.선택회사코드 = this.ctx회사.CodeValue;
				
                if (this.PageID == "P_CZ_MA_WORKFLOW_MNG")
                {
                    this.수정가능여부 = true;
                    
                    this.ctx담당자.CodeValue = Global.MainFrame.LoginInfo.UserID;
                    this.ctx담당자.CodeName = Global.MainFrame.LoginInfo.UserName;

                    this.bpPanelControl2.Visible = false;

                    UGrant ugrant = new UGrant();
                    ugrant.GrantButtonVisible("P_CZ_MA_WORKFLOW_MNG", "ADMIN", this.btn관리페이지);
                }
                else
                {
                    this.수정가능여부 = false;
                    this.bpPanelControl2.Visible = true;

                    UGrant ugrant = new UGrant();
                    ugrant.GrantButtonVisible("P_CZ_MA_WORKFLOW_MNG", "ADMIN", this.btn관리페이지);
                }

                if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
				{
					this.btn재고견적.Enabled = true;
					this.btn인수증확인.Enabled = true;
				}
				else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				{
					this.btn재고견적.Enabled = false;
					this.btn인수증확인.Enabled = true;
				}
                else
				{
					this.btn재고견적.Enabled = false;
					this.btn인수증확인.Enabled = false;
				}

                this.btnRPA재실행.Visible = false;

                this.oneGrid2.UseCustomLayout = true;
                this.oneGrid2.InitCustomLayout();

                // 1분 : 60000
                this.timer.Interval = 300000; // 5분
                
                this.isLoaded = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InitDD()
        {
            this.btn영업담당.Text = Global.MainFrame.DD(this.btn영업담당.Text);
            this.btn입력지원.Text = Global.MainFrame.DD(this.btn입력지원.Text);
            this.btn구매물류.Text = Global.MainFrame.DD(this.btn구매물류.Text);
            this.btn관리.Text = Global.MainFrame.DD(this.btn관리.Text);

            this.btn고객문의등록.StepText = Global.MainFrame.DD(this.btn고객문의등록.StepText);
            this.btn매입문의등록.StepText = Global.MainFrame.DD(this.btn매입문의등록.StepText);
            this.btn문의서검토.StepText = Global.MainFrame.DD(this.btn문의서검토.StepText);
            this.btn매입가등록.StepText = Global.MainFrame.DD(this.btn매입가등록.StepText);
            this.btn견적작성.StepText = Global.MainFrame.DD(this.btn견적작성.StepText);
            this.btn재고견적.StepText = Global.MainFrame.DD(this.btn재고견적.StepText);
            this.btn견적제출.StepText = Global.MainFrame.DD(this.btn견적제출.StepText);
            this.btn고객문의클로징.StepText = Global.MainFrame.DD(this.btn고객문의클로징.StepText);
            this.btn수주등록.StepText = Global.MainFrame.DD(this.btn수주등록.StepText);
            this.btn수주확인서.StepText = Global.MainFrame.DD(this.btn수주확인서.StepText);
            this.btn매입구매.StepText = Global.MainFrame.DD(this.btn매입구매.StepText);
            this.btn수발주통보서.StepText = Global.MainFrame.DD(this.btn수발주통보서.StepText);
            this.btn수발주통보서결재.StepText = Global.MainFrame.DD(this.btn수발주통보서결재.StepText);
            this.btn납품목록.StepText = Global.MainFrame.DD(this.btn납품목록.StepText);
            this.btn장기미출고.StepText = Global.MainFrame.DD(this.btn장기미출고.StepText);
            this.btn협조전작성.StepText = Global.MainFrame.DD(this.btn협조전작성.StepText);
            this.btn협조전완결.StepText = Global.MainFrame.DD(this.btn협조전완결.StepText);
            this.btn납품완결.StepText = Global.MainFrame.DD(this.btn납품완결.StepText);
            this.btn인수증등록.StepText = Global.MainFrame.DD(this.btn인수증등록.StepText);
            this.btn계산서발행.StepText = Global.MainFrame.DD(this.btn계산서발행.StepText);
            this.btn클레임종결.StepText = Global.MainFrame.DD(this.btn클레임종결.StepText);
			this.btn인수증확인.StepText = Global.MainFrame.DD(this.btn인수증확인.StepText);
            this.btn계산서확인.StepText = Global.MainFrame.DD(this.btn계산서확인.StepText);
        }

        private void InitEvent()
        {
            this.chk자동갱신.CheckedChanged += new EventHandler(this.chk자동갱신_CheckedChanged);
            this.timer.Tick += new EventHandler(this.timer_Tick);

            this.ctx영업조직.QueryBefore += new BpQueryHandler(this.ctx영업조직_QueryBefore);
            this.bpc영업그룹.QueryBefore += new BpQueryHandler(this.ctx영업그룹_QueryBefore);
            this.ctx담당자.QueryBefore += new BpQueryHandler(this.ctx담당자_QueryBefore);
            this.ctx영업조직.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.bpc영업그룹.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx회사.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.btn관리페이지.Click += new EventHandler(this.btn관리페이지_Click);

            #region Step
            this.oneGrid2.SizeChanged += new EventHandler(this.oneGrid2_SizeChanged);
            this.splitContainer1.SplitterMoved += new SplitterEventHandler(this.splitContainer1_SplitterMoved);

            this.btn고객문의등록.Click += new EventHandler(this.button_Click);
            this.btn매입문의등록.Click += new EventHandler(this.button_Click);
            this.btn문의서검토.Click += new EventHandler(this.button_Click);
            this.btn매입가등록.Click += new EventHandler(this.button_Click);
            this.btn견적작성.Click += new EventHandler(this.button_Click);
            this.btn재고견적.Click += new EventHandler(this.button_Click);
            this.btn견적제출.Click += new EventHandler(this.button_Click);
            this.btn고객문의클로징.Click += new EventHandler(this.button_Click);
            this.btn수주등록.Click += new EventHandler(this.button_Click);
            this.btn수주확인서.Click += new EventHandler(this.button_Click);
            this.btn매입구매.Click += new EventHandler(this.button_Click);
            this.btn수발주통보서.Click += new EventHandler(this.button_Click);
            this.btn수발주통보서결재.Click += new EventHandler(this.button_Click);
            this.btn납품목록.Click += new EventHandler(this.button_Click);
            this.btn장기미출고.Click += new EventHandler(this.button_Click);
            this.btn협조전작성.Click += new EventHandler(this.button_Click);
            this.btn협조전완결.Click += new EventHandler(this.button_Click);
            this.btn납품완결.Click += new EventHandler(this.button_Click);
            this.btn인수증등록.Click += new EventHandler(this.button_Click);
            this.btn계산서발행.Click += new EventHandler(this.button_Click);
            this.btn클레임종결.Click += new EventHandler(this.button_Click);
			this.btn인수증확인.Click += new EventHandler(this.button_Click);
			this.btn계산서확인.Click += new EventHandler(this.button_Click);
            #endregion

            #region List
            this.cbo필터.SelectedValueChanged += new EventHandler(this.cbo필터_SelectedValueChanged);
            this.btn완료.Click += new EventHandler(this.btn완료_Click);
            this.btn전달.Click += new EventHandler(this.btn완료_Click);
            this.btn마감.Click += new EventHandler(this.btn완료_Click);
            #endregion

            this.btn도움말.Click += new EventHandler(this.btn도움말_Click);
            this.btn딘텍원격지원.Click += new EventHandler(this.btn딘텍원격지원_Click);
            this.btnRPA재실행.Click += new EventHandler(this.btn완료_Click);
        }

		private void Btn계산서확인_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region 메인버튼이벤트
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this.RefreshCount(this.ctx회사.CodeValue, this.ctx담당자.CodeValue, this.txt파일번호.Text, this.ctx영업조직.CodeValue, this.bpc영업그룹.QueryWhereIn_Pipe);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !base.Verify()) return false;

                return this.SaveData(null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            if (this.isLoaded == false)
            {
                this.ShowMessage("CZ_화면이 로딩 중 입니다.");
                return false;
            }
            else
            {
                this.임시파일제거();
                return base.OnToolBarExitButtonClicked(sender, e);
            }
        }
        #endregion

        #region 그리드이벤트
        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            FlexGrid flex = sender as FlexGrid;
            string 상태, 결재자1, 결재자2, 담당자;

            try
            {
                if (!flex.HasNormalRow) return;

                flex.Redraw = false;

                for (int h = flex.Rows.Fixed; h < flex.Rows.Count; ++h)
                {
                    if (flex.Name == this._수발주통보서결재H.Name)
                    {
                        상태 = D.GetString(flex.GetData(h, "YN_DONE"));
                        결재자1 = D.GetString(flex.GetData(h, "ID_FIRST_APPROVAL"));
                        결재자2 = D.GetString(flex.GetData(h, "ID_SECOND_APPROVAL"));

                        if (상태 == "미결재" && 결재자1 != Global.MainFrame.LoginInfo.UserID)
                        {
                            if (flex.GetCellCheck(h, flex.Cols["S"].Index) == CheckEnum.Checked)
                                flex.SetCellCheck(h, flex.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                        else if (상태 == "1차결재" && 결재자2 != Global.MainFrame.LoginInfo.UserID)
                        {
                            if (flex.GetCellCheck(h, flex.Cols["S"].Index) == CheckEnum.Checked)
                                flex.SetCellCheck(h, flex.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                    }
                    else if (flex.Name == this._계산서확인H.Name)
                    {
                        담당자 = D.GetString(flex.GetData(h, "NO_EMP"));

                        if (!담당자.Contains(Global.MainFrame.LoginInfo.UserID))
                        {
                            if (flex.GetCellCheck(h, flex.Cols["S"].Index) == CheckEnum.Checked)
                                flex.SetCellCheck(h, flex.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                    }
                }

                flex.Redraw = true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            string key;
            DataSet ds = null;
            FlexGrid grid;
            진행단계 선택단계;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                grid = (sender as FlexGrid);
				선택단계 = this.선택단계;

				if (선택단계 == 진행단계.인수증확인 ||
					선택단계 == 진행단계.계산서발행)
					key = D.GetString(grid["CD_FILE"]);
				else
					key = D.GetString(grid["NO_KEY"]);

                if (선택단계 == 진행단계.매입문의등록)
                {
                    if (D.GetString(grid["TP_STATE"]) == "002")
                        this.btn완료.Enabled = true;
                    else
                        this.btn완료.Enabled = false;
                }
                else if (선택단계 == 진행단계.매입가등록)
				{
                    if (grid["YN_SO"].ToString() == "Y")
                        this.btn완료.Enabled = true;
                    else
                        this.btn완료.Enabled = false;
                }
                else if (선택단계 == 진행단계.문의서검토)
                {
                    if (grid["TP_CHECK"].ToString() != "요청")
                        this.btn완료.Enabled = true;
                    else
                        this.btn완료.Enabled = false;
                }
                else if (선택단계 == 진행단계.견적작성)
                {
                    if (!string.IsNullOrEmpty(grid["DT_QUTATION"].ToString()))
                    {
                        this.btn완료.Enabled = true;
                        this.btn마감.Enabled = true;
                        this.btn전달.Enabled = true;
                    }
                    else
                    {
                        this.btn완료.Enabled = false;
                        this.btn마감.Enabled = false;
                        this.btn전달.Enabled = false;
                    }
                }
                else if (선택단계 == 진행단계.수주등록)
                {
                    if (D.GetString(grid["YN_REG"]) == "Y")
                        this.btn완료.Enabled = true;
                    else
                        this.btn완료.Enabled = false;
                }
                else
                {
                    this.btn완료.Enabled = true;
                    this.btn전달.Enabled = true;
                }

				ds = this._biz.SearchDetail(new object[] { D.GetString(grid["CD_COMPANY"]),
                                                           string.Format("{0:00}", (int)선택단계),
														   key });

                if (ds != null && ds.Tables.Count > 0)
                    grid.DetailGrids[0].Binding = ds.Tables[0];
                else
                    grid.DetailGrids[0].Binding = null;

                if (grid.DetailGrids.Length > 1)
                {
                    if (ds != null && ds.Tables.Count > 1)
                        grid.DetailGrids[1].Binding = ds.Tables[1];
                    else
                        grid.DetailGrids[1].Binding = null;
                }

                if (grid.DetailGrids.Length > 2)
                {
                    if (ds != null && ds.Tables.Count > 2)
                        grid.DetailGrids[2].Binding = ds.Tables[2];
                    else
                        grid.DetailGrids[2].Binding = null;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FlexGrid grid, headerGrid;
            string pageId, pageName, query;
            object[] obj;

            try
            {
                grid = (sender as FlexGrid);
                if (grid.HasNormalRow == false) return;
                if (grid.MouseRow < grid.Rows.Fixed) return;

                if (grid.Cols["NO_KEY"] != null && grid.ColSel == grid.Cols["NO_KEY"].Index)
                {
                    if (this.수정가능여부 == false) return;
                    if (this._isPageLoad == true) return;

                    this._isPageLoad = true;
                    
                    pageId = string.Empty;
                    pageName = string.Empty;
                    obj = new object[] { D.GetString(grid["NO_KEY"]) };

                    query = @"SELECT 1 
							  FROM MA_USER MU WITH(NOLOCK)
							  WHERE MU.CD_COMPANY = '" + this.선택회사코드 + "'" + Environment.NewLine + 
							 "AND MU.ID_USER = '" + D.GetString(grid["ID_SALES"]) + "'" + Environment.NewLine +
							 "AND MU.CD_SALEGRP IN ('010421', '010422', '010423')";

                    switch (this.선택단계)
                    {
                        case 진행단계.고객문의등록:
                        case 진행단계.매입문의등록:
                        case 진행단계.문의서검토:
                        case 진행단계.매입가등록:
                        case 진행단계.견적작성:
                        case 진행단계.재고견적:
                        case 진행단계.견적제출:
                        case 진행단계.고객문의클로징:
                        case 진행단계.수주등록:
                        case 진행단계.매입구매:
                            #region 견적등록
                            if (D.GetDecimal(DBHelper.ExecuteScalar(query)) > 0)
                            {
                                pageId = "P_CZ_SA_QTN_REG_GS";
                                pageName = Global.MainFrame.DD("견적등록(선용)");
                            }
                            else
                            {
                                pageId = "P_CZ_SA_QTN_REG";
                                pageName = Global.MainFrame.DD("견적등록");
                            }
                            #endregion
                            break;
                        case 진행단계.수주확인서:
                            #region 수주확인서
                            pageId = "P_CZ_SA_SO_REG";
                            pageName = Global.MainFrame.DD("수주등록"); ;
                            obj = new object[] { D.GetString(grid["NO_KEY"]), false };
                            #endregion
                            break;
                        case 진행단계.수발주통보서:
                        case 진행단계.수발주통보서결재:
                            #region 수발주통보서
                            pageId = "P_CZ_SA_CONTRACT_RPT";
                            pageName = Global.MainFrame.DD("수발주통보서");
                            #endregion
                            break;
                        case 진행단계.납품목록:
                        case 진행단계.협조전작성:
                            #region 물류업무협조전
                            pageId = "P_CZ_SA_GIR";
                            pageName = Global.MainFrame.DD("물류업무협조전");
                            #endregion
                            break;
                        case 진행단계.협조전완결:
                            #region 출고등록
                            pageId = "P_CZ_SA_GI_REG";
                            pageName = Global.MainFrame.DD("출고등록");
                            #endregion
                            break;
                        case 진행단계.인수증등록:
                            #region 인수증등록
                            pageId = "P_CZ_SA_GIM_REG";
                            pageName = Global.MainFrame.DD("인수증등록");
                            #endregion
                            break;
                        case 진행단계.계산서발행:
                            #region 매출등록
                            pageId = "P_CZ_SA_IV";
                            pageName = Global.MainFrame.DD("매출등록");
                            #endregion
                            break;
                        case 진행단계.클레임종결:
                            #region 클레임종결
                            pageId = "P_CZ_SA_CLAIM";
                            pageName = Global.MainFrame.DD("클레임관리");
                            #endregion
                            break;
						case 진행단계.인수증확인:
							#region 인수증확인
							pageId = "P_CZ_SA_GIM_REG";
							pageName = Global.MainFrame.DD("인수증확인");
							#endregion
							break;
					}

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);

                    this._isPageLoad = false;

                    return;
                }
				else if (grid.Cols["CD_FILE"] != null && grid.ColSel == grid.Cols["CD_FILE"].Index)
				{
					if (this.선택단계 == 진행단계.인수증확인)
					{
						P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(grid["CD_COMPANY"].ToString(), "SA", "P_CZ_SA_GIM_REG", grid["CD_FILE"].ToString(), "P_CZ_SA_GIM_REG" + "/" + grid["CD_COMPANY"].ToString() + "/" + grid["DT_GIR"].ToString().Substring(0, 4));
						dlg.ShowDialog(this);
					}
				}
				else if (grid.Cols["NM_FILE"] != null && grid.ColSel == grid.Cols["NM_FILE"].Index)
                {
					if (this.선택단계 == 진행단계.인수증확인 ||
						this.선택단계 == 진행단계.계산서발행)
					{
						if (grid.Name == this._인수증확인L.Name)
							headerGrid = this._인수증확인H;
						else if (grid.Name == this._계산서발행L.Name)
							headerGrid = this._계산서발행H;
						else
							return;

						WebClient wc = new WebClient();
						
						string 로컬경로 = Application.StartupPath + "/temp/";
						string 서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_SA_GIM_REG/" + headerGrid["CD_COMPANY"].ToString() + "/" + headerGrid["DT_GIR"].ToString().Substring(0, 4) + "/" + headerGrid["CD_FILE"].ToString() + "/";

						Directory.CreateDirectory(로컬경로);
						wc.DownloadFile(서버경로 + grid["NM_FILE"].ToString(), 로컬경로 + grid["NM_FILE"].ToString());
						Process.Start(로컬경로 + grid["NM_FILE"].ToString());
					}
					else
					{
						if (!string.IsNullOrEmpty(grid["NM_FILE_REAL"].ToString()))
							FileMgr.Download_WF(Global.MainFrame.LoginInfo.CompanyCode, D.GetString(grid["NO_KEY"]), grid["NM_FILE_REAL"].ToString(), true);
						else if (grid.Cols.Contains("YN_DB_FILE") && D.GetString(grid["YN_DB_FILE"]) == "Y")
						{
							DataTable dt = DBHelper.GetDataTable(@"SELECT FILE_DATA 
                                                               FROM CZ_SRM_QTNH_ATTACHMENT WITH(NOLOCK)
                                                               WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
																  "AND NO_FILE = '" + D.GetString(grid["NO_KEY"]) + "'" + Environment.NewLine +
																  "AND CD_PARTNER = '" + D.GetString(grid["CD_SUPPLIER"]) + "'" + Environment.NewLine +
																  "AND SEQ = '" + D.GetString(grid["NO_LINE"]) + "'");

							if (dt != null && dt.Rows.Count > 0)
							{
								string localPath = Application.StartupPath + "\\temp\\";
								string filename = FileMgr.GetUniqueFileName(localPath + grid["NM_FILE"].ToString());
								byte[] documentBinary = (byte[])dt.Rows[0]["FILE_DATA"];

								FileMgr.CreateDirectory(localPath);

								FileStream fStream = new FileStream(localPath + filename, FileMode.Create);
								fStream.Write(documentBinary, 0, documentBinary.Length);
								fStream.Close();
								fStream.Dispose();

								Process.Start(localPath + filename);
							}
						}
					}
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            string colname, oldValue;
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                colname = grid.Cols[e.Col].Name;
                oldValue = D.GetString(grid.GetData(e.Row, e.Col));

                if (grid.Name == this._수발주통보서결재H.Name)
                {
                    string 상태 = D.GetString(grid["YN_DONE"]);
                    string 결재자1 = D.GetString(grid["ID_FIRST_APPROVAL"]);
                    string 결재자2 = D.GetString(grid["ID_SECOND_APPROVAL"]);

                    if (colname == "S")
                    {
                        if (상태 == "미결재" && 결재자1 != Global.MainFrame.LoginInfo.UserID)
                        {
                            grid["S"] = D.GetString(oldValue);
                            Global.MainFrame.ShowMessage("CZ_결재 권한이 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                        else if (상태 == "1차결재" && 결재자2 != Global.MainFrame.LoginInfo.UserID)
                        {
                            grid["S"] = D.GetString(oldValue);
                            Global.MainFrame.ShowMessage("CZ_결재 권한이 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                    }
                    else if (colname == "DC_COMMENT_FIRST" && 결재자1 != Global.MainFrame.LoginInfo.UserID)
                    {
                        grid["DC_COMMENT_FIRST"] = D.GetString(oldValue);
                        Global.MainFrame.ShowMessage("CZ_결재 권한이 없습니다.");
                        e.Cancel = true;
                        return;
                    }
                    else if (colname == "DC_COMMENT_SECOND" && 결재자2 != Global.MainFrame.LoginInfo.UserID)
                    {
                        grid["DC_COMMENT_SECOND"] = D.GetString(oldValue);
                        Global.MainFrame.ShowMessage("CZ_결재 권한이 없습니다.");
                        e.Cancel = true;
                        return;
                    }
                    else if (colname == "DC_COMMENT_FIRST" && 상태 == "1차결재")
                    {
                        grid["DC_COMMENT_FIRST"] = D.GetString(oldValue);
                        Global.MainFrame.ShowMessage("1차결재 완료시 1차결재 비고 수정 불가 합니다.");
                        e.Cancel = true;
                        return;
                    }
                }
                else if (grid.Name == this._재고견적H.Name)
                {
                    if (Global.MainFrame.LoginInfo.UserID != "S-495" && 
                        Global.MainFrame.LoginInfo.UserID != "S-391")
					{
                        string 견적담당 = D.GetString(grid["ID_PUR"]);

                        if (colname == "S" && 견적담당 != Global.MainFrame.LoginInfo.UserID)
                        {
                            grid["S"] = D.GetString(oldValue);
                            Global.MainFrame.ShowMessage("견적담당자만 선택 할 수 있습니다.");
                            e.Cancel = true;
                            return;
                        }
                        else if (colname == "DC_RMK" && 견적담당 != Global.MainFrame.LoginInfo.UserID)
                        {
                            grid["DC_RMK"] = D.GetString(oldValue);
                            Global.MainFrame.ShowMessage("견적담당자만 수정 할 수 있습니다.");
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else if (grid.Name == this._계산서확인H.Name)
				{
                    if (!grid["NO_EMP"].ToString().Contains(Global.MainFrame.LoginInfo.UserID) &&
                        !grid["NO_EMP_TEAM"].ToString().Contains(Global.MainFrame.LoginInfo.UserID))
					{
                        Global.MainFrame.ShowMessage("회신 권한이 없습니다.");
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

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid grid;

            grid = ((FlexGrid)sender);

            if (grid.Name == this._견적작성H.Name)
			{
                #region 견적작성
                if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed)
                    return;

                CellStyle style = grid.Rows[e.Row].Style;

                if (style == null)
                {
                    if (D.GetString(grid[e.Row, "YN_SEND"]) == "Y")
                        grid.Rows[e.Row].Style = grid.Styles["전달"];
                    else
                        grid.Rows[e.Row].Style = grid.Styles["미전달"];
                }
                else if (style.Name == "전달" && D.GetString(grid[e.Row, "YN_SEND"]) == "N")
                {
                    grid.Rows[e.Row].Style = grid.Styles["미전달"];
                }
                else if (style.Name == "미전달" && D.GetString(grid[e.Row, "YN_SEND"]) == "Y")
                {
                    grid.Rows[e.Row].Style = grid.Styles["전달"];
                }
                #endregion
            }
            else if (grid.Name == this._견적제출H.Name)
			{
                #region 견적제출
                if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed)
                    return;

                CellStyle style = grid.Rows[e.Row].Style;

                if (style == null)
                {
                    if (D.GetString(grid[e.Row, "YN_WARNING"]) == "Y")
                        grid.Rows[e.Row].Style = grid.Styles["주의"];
                    else
                        grid.Rows[e.Row].Style = grid.Styles["일반"];
                }
                else if (style.Name == "주의" && D.GetString(grid[e.Row, "YN_WARNING"]) == "N")
                {
                    grid.Rows[e.Row].Style = grid.Styles["일반"];
                }
                else if (style.Name == "일반" && D.GetString(grid[e.Row, "YN_WARNING"]) == "Y")
                {
                    grid.Rows[e.Row].Style = grid.Styles["주의"];
                }
                #endregion
            }
            else if (grid.Name == this._재고견적H.Name)
			{
                #region 재고견적
                if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed)
                    return;

                CellStyle style = grid.Rows[e.Row].Style;

                if (style == null)
                {
                    if (grid[e.Row, "YN_SPO"].ToString() == "Y")
                        grid.Rows[e.Row].Style = grid.Styles["기획실"];
                    else
                        grid.Rows[e.Row].Style = grid.Styles["영업"];
                }
                else if (style.Name == "기획실" && grid[e.Row, "YN_SPO"].ToString() == "N")
                {
                    grid.Rows[e.Row].Style = grid.Styles["영업"];
                }
                else if (style.Name == "영업" && grid[e.Row, "YN_SPO"].ToString() == "Y")
                {
                    grid.Rows[e.Row].Style = grid.Styles["기획실"];
                }
                #endregion
            }
            else if (grid.Name == this._수발주통보서결재H.Name)
			{
                #region 수발주통보서결재
                if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed)
                    return;

                CellStyle style = grid.Rows[e.Row].Style;

                if (style == null)
                {
                    if (D.GetDecimal(grid[e.Row, "QT_MINUS"]) > 0)
                        grid.Rows[e.Row].Style = grid.Styles["마이너스"];
                    else
                        grid.Rows[e.Row].Style = grid.Styles["일반"];
                }
                else if (style.Name == "마이너스" && D.GetDecimal(grid[e.Row, "QT_MINUS"]) == 0)
                {
                    grid.Rows[e.Row].Style = grid.Styles["일반"];
                }
                else if (style.Name == "일반" && D.GetDecimal(grid[e.Row, "QT_MINUS"]) > 0)
                {
                    grid.Rows[e.Row].Style = grid.Styles["마이너스"];
                }
                #endregion
            }
            else if (grid.Name == this._매입문의등록H.Name)
			{
                #region 매입문의등록
                if (Global.MainFrame.LoginInfo.CompanyCode != "K200")
                    return;

                if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed)
                    return;

                CellStyle style = grid.Rows[e.Row].Style;
                string 비고 = grid[e.Row, "DC_RMK"].ToString().ToUpper();

                if (style == null)
                {
                    if (비고.Contains("HSD") || 비고.Contains("이비즈"))
                        grid.Rows[e.Row].Style = grid.Styles["강조"];
                    else
                        grid.Rows[e.Row].Style = grid.Styles["미강조"];
                }
                else if (style.Name == "강조" && !비고.Contains("HSD") && !비고.Contains("이비즈"))
                {
                    grid.Rows[e.Row].Style = grid.Styles["미강조"];
                }
                else if (style.Name == "미강조" && 비고.Contains("HSD") || 비고.Contains("이비즈"))
                {
                    grid.Rows[e.Row].Style = grid.Styles["강조"];
                }
                #endregion
            }
		}

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            string query, name;

            try
            {
                flexGrid = ((FlexGrid)sender);
                name = flexGrid.Cols[e.Col].Name;

                if (flexGrid.HasNormalRow == false) return;

                if (flexGrid == this._계산서발행H)
                {
                    switch (name)
                    {
                        case "YN_STATUS1":
                            query = @"UPDATE OH
                                  SET OH.YN_STATUS1 = '{2}'
                                  FROM MM_QTIOH OH
                                  WHERE OH.CD_COMPANY = '{0}'
                                  AND OH.NO_IO = '{1}'";

                            DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       this._계산서발행H["NO_KEY"].ToString(),
                                                                                       this._계산서발행H["YN_STATUS1"].ToString() }));
                            break;
                    }
                }
                else if (flexGrid == this._계산서발행L)
				{
                    switch(name)
					{
                        case "NO_REF":
                            query = @"UPDATE MF
SET MF.NO_REF = '{3}',
    MF.ID_UPDATE = '{4}',
    MF.DTS_UPDATE = GETDATE()
FROM MA_FILEINFO MF
WHERE MF.CD_COMPANY = '{0}'
AND MF.CD_MODULE = 'SA'
AND MF.ID_MENU = 'P_CZ_SA_GIM_REG'
AND MF.CD_FILE = '{1}'
AND MF.NO_SEQ = '{2}'";
                            DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       this._계산서발행L["CD_FILE"].ToString(),
                                                                                       this._계산서발행L["NO_SEQ"].ToString(),
                                                                                       this._계산서발행L["NO_REF"].ToString(),
                                                                                       Global.MainFrame.LoginInfo.UserID }));

                            break;
					}
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타이벤트
        private void chk자동갱신_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk자동갱신.Checked)
                    this.timer.Start();
                else
                    this.timer.Stop();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.RefreshCount(this.ctx회사.CodeValue, this.ctx담당자.CodeValue, this.txt파일번호.Text, this.ctx영업조직.CodeValue, this.bpc영업그룹.QueryWhereIn_Pipe);
        }

        private void ctx영업조직_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx영업그룹_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx담당자_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = this.ctx회사.CodeValue;
                e.HelpParam.P42_CD_FIELD2 = this.ctx회사.CodeName;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            if (((Control)sender).Name == this.ctx회사.Name)
                this.선택회사코드 = this.ctx회사.CodeValue;

            this.RefreshCount(this.ctx회사.CodeValue, this.ctx담당자.CodeValue, this.txt파일번호.Text, this.ctx영업조직.CodeValue, this.bpc영업그룹.QueryWhereIn_Pipe);
            this.조회결과반영(this.선택단계, this.SearchList(this.ctx회사.CodeValue, this.ctx담당자.CodeValue, this.txt파일번호.Text, this.ctx영업조직.CodeValue, this.bpc영업그룹.QueryWhereIn_Pipe).ToString());
        }

        private void oneGrid2_SizeChanged(object sender, EventArgs e)
        {
            this.ResetSize(this.splitContainer1.Panel1.Height, this.splitContainer1.Panel1.Width);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.ResetSize(this.splitContainer1.Panel1.Height, this.splitContainer1.Panel1.Width);
        }

        private void button_Click(object sender, EventArgs e)
        {
            StepButton button;

            try
            {
                button = ((StepButton)sender);

                if (button.Name == this.btn고객문의등록.Name)
                    this.선택단계 = 진행단계.고객문의등록;
                else if (button.Name == this.btn매입문의등록.Name)
                    this.선택단계 = 진행단계.매입문의등록;
                else if (button.Name == this.btn문의서검토.Name)
                    this.선택단계 = 진행단계.문의서검토;
                else if (button.Name == this.btn매입가등록.Name)
                    this.선택단계 = 진행단계.매입가등록;
                else if (button.Name == this.btn견적작성.Name)
                    this.선택단계 = 진행단계.견적작성;
                else if (button.Name == this.btn재고견적.Name)
                    this.선택단계 = 진행단계.재고견적;
                else if (button.Name == this.btn견적제출.Name)
                    this.선택단계 = 진행단계.견적제출;
                else if (button.Name == this.btn고객문의클로징.Name)
                    this.선택단계 = 진행단계.고객문의클로징;
                else if (button.Name == this.btn수주등록.Name)
                    this.선택단계 = 진행단계.수주등록;
                else if (button.Name == this.btn수주확인서.Name)
                    this.선택단계 = 진행단계.수주확인서;
                else if (button.Name == this.btn매입구매.Name)
                    this.선택단계 = 진행단계.매입구매;
                else if (button.Name == this.btn수발주통보서.Name)
                    this.선택단계 = 진행단계.수발주통보서;
                else if (button.Name == this.btn수발주통보서결재.Name)
                    this.선택단계 = 진행단계.수발주통보서결재;
                else if (button.Name == this.btn납품목록.Name)
                    this.선택단계 = 진행단계.납품목록;
                else if (button.Name == this.btn장기미출고.Name)
                    this.선택단계 = 진행단계.장기미출고;
                else if (button.Name == this.btn협조전작성.Name)
                    this.선택단계 = 진행단계.협조전작성;
                else if (button.Name == this.btn협조전완결.Name)
                    this.선택단계 = 진행단계.협조전완결;
                else if (button.Name == this.btn납품완결.Name)
                    this.선택단계 = 진행단계.납품완결;
                else if (button.Name == this.btn인수증등록.Name)
                    this.선택단계 = 진행단계.인수증등록;
                else if (button.Name == this.btn계산서발행.Name)
                    this.선택단계 = 진행단계.계산서발행;
                else if (button.Name == this.btn클레임종결.Name)
                    this.선택단계 = 진행단계.클레임종결;
                else if (button.Name == this.btn인수증확인.Name)
                    this.선택단계 = 진행단계.인수증확인;
                else if (button.Name == this.btn계산서확인.Name)
                    this.선택단계 = 진행단계.계산서확인;

				this.RefreshControl();

                this.선택단계설정();
                this.조회결과반영(this.선택단계, this.SearchList(this.ctx회사.CodeValue, this.ctx담당자.CodeValue, this.txt파일번호.Text, this.ctx영업조직.CodeValue, this.bpc영업그룹.QueryWhereIn_Pipe).ToString());
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn관리페이지_Click(object sender, EventArgs e)
        {
            P_CZ_MA_WORKFLOW_CHANGE dialog = new P_CZ_MA_WORKFLOW_CHANGE();
            dialog.Show();
        }

        private void cbo필터_SelectedValueChanged(object sender, EventArgs e)
        {
			if (this.선택단계 == 진행단계.견적작성)
			{
				if (string.IsNullOrEmpty(D.GetString(this.cbo필터.SelectedValue)))
					this._견적작성H.RowFilter = string.Empty;
				else
					this._견적작성H.RowFilter = "TP_STATE = '" + this.cbo필터.SelectedValue + "'";

				if (this.cbo필터.Visible == true)
					this.견적작성필터 = this.cbo필터.SelectedValue.ToString();
			}
            else if (this.선택단계 == 진행단계.재고견적)
			{
                if (string.IsNullOrEmpty(D.GetString(this.cbo필터.SelectedValue)))
                    this._재고견적H.RowFilter = string.Empty;
                else
                    this._재고견적H.RowFilter = "CD_SUPPLIER1 = '" + this.cbo필터.SelectedValue + "'";
            }
			else if (this.선택단계 == 진행단계.계산서발행)
			{
				switch(this.cbo필터.SelectedValue)
				{
					case "":
						this._계산서발행H.RowFilter = string.Empty;
						break;
					case "001":
						#region 미발행
						this._계산서발행H.RowFilter = "ISNULL(CD_FILE, '') <> '' AND ISNULL(DT_LOADING, '') <> '' AND ((CD_COMPANY <> 'K100' AND CD_COMPANY <> 'K200') OR CD_AREA <> '100' OR ISNULL(YN_BILL, 'N') = 'Y')";
						#endregion
						break;
					case "002":
						#region 미등록
						this._계산서발행H.RowFilter = "ISNULL(CD_FILE, '') = '' OR ISNULL(DT_LOADING, '') = '' OR ((CD_COMPANY = 'K100' OR CD_COMPANY = 'K200') AND CD_AREA = '100' AND ISNULL(YN_BILL, 'N') = 'N')";
						#endregion
						break;
				}

				if (this.cbo필터.Visible == true)
					this.계산서발행필터 = this.cbo필터.SelectedValue.ToString();
			}
        }

        private void btn완료_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            FlexGrid grid = null;
            Control control;
            진행단계 선택단계;
            string contents, query;

            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                control = ((Control)sender);
                선택단계 = this.선택단계;

                switch (선택단계)
                {
                    case 진행단계.매입문의등록:
                        grid = this._매입문의등록H;
                        break;
                    case 진행단계.매입가등록:
                        grid = this._매입가등록H;
                        break;
                    case 진행단계.문의서검토:
                        grid = this._문의서검토H;
                        break;
                    case 진행단계.견적작성:
                        grid = this._견적작성H;
                        break;
                    case 진행단계.고객문의클로징:
                        grid = this._고객문의클로징H;
                        break;
                    case 진행단계.재고견적:
                        grid = this._재고견적H;
                        break;
                    case 진행단계.견적제출:
                        grid = this._견적제출H;
                        break;
                    case 진행단계.수주등록:
                        grid = this._수주등록H;
                        break;
                    case 진행단계.매입구매:
                        grid = this._매입구매H;
                        break;
                    case 진행단계.수발주통보서결재:
                        grid = this._수발주통보서결재H;
                        break;
                    case 진행단계.납품목록:
                        grid = this._납품목록H;
                        break;
                    case 진행단계.납품완결:
                        grid = this._납품완결H;
                        break;
					case 진행단계.인수증확인:
						grid = this._인수증확인H;
						break;
					case 진행단계.계산서발행:
						grid = this._계산서발행H;
						break;
                    case 진행단계.계산서확인:
                        grid = this._계산서확인H;
                        break;
					default:
                        return;
                }

                if (grid != null)
                {
                    if (grid.IsDataChanged == true)
                    {
                        Global.MainFrame.ShowMessage("저장후 진행하시기 바랍니다.");
                        return;
                    }

                    dataRowArray = grid.DataTable.Select("S = 'Y'");

                    if (dataRowArray.Length == 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }
                    else
                    {
                        if (선택단계 == 진행단계.매입문의등록 && control.Name == this.btn완료.Name && grid.DataTable.Select("S = 'Y' AND ISNULL(TP_STATE, '') <> '002'").Length > 0)
						{
                            this.ShowMessage("등록상태가 아닌 건이 선택되어 있습니다.");
                            return;
						}
                        else if (선택단계 == 진행단계.매입가등록 && control.Name == this.btn완료.Name && grid.DataTable.Select("S = 'Y' AND YN_SO = 'N'").Length > 0)
						{
                            this.ShowMessage("수주등록 되지 않은 건이 선택되어 있습니다.");
                            return;
                        }
                        else if (선택단계 == 진행단계.문의서검토 && control.Name == this.btn완료.Name && grid.DataTable.Select("S = 'Y' AND TP_CHECK = '요청'").Length > 0)
                        {
                            this.ShowMessage("재고견적 요청 상태의 건이 선택되어 있습니다.");
                            return;
                        }
                        else if (선택단계 == 진행단계.견적작성 && (control.Name == this.btn전달.Name || control.Name == this.btn완료.Name || control.Name == this.btn마감.Name) && grid.DataTable.Select("S = 'Y' AND ISNULL(DT_QUTATION, '') = ''").Length > 0)
						{
                            this.ShowMessage("최초견적일이 없는 건이 선택되어 있습니다.");
                            return;
                        }
                        else if (선택단계 == 진행단계.견적작성 && control.Name == this.btn전달.Name && grid.DataTable.Select("S = 'Y' AND YN_SEND = 'Y'").Length > 0)
                        {
                            if (Global.MainFrame.ShowMessage("전달되었던 건이 선택되어 있습니다.\n진행하시겠습니까 ?", "QY2") == DialogResult.No)
                                return;
                        }
                        else if (선택단계 == 진행단계.수주등록 && control.Name == this.btn완료.Name && grid.DataTable.Select("S = 'Y' AND YN_REG = 'N'").Length > 0)
                        {
                            this.ShowMessage("수주등록 되지 않은 건이 선택 되어 있습니다.");
                            return;
                        }
                        else if ((선택단계 == 진행단계.매입문의등록 || 선택단계 == 진행단계.견적제출) && control.Name == this.btnRPA재실행.Name && grid.DataTable.Select("S = 'Y' AND (ISNULL(SEQ, 0) = 0 OR YN_DONE = 'Y')").Length > 0)
						{
                            this.ShowMessage("RPA 실행이 되지 않았거나 성공한 건이 선택 되어 있습니다.");
                            return;
                        }
                        else if (선택단계 == 진행단계.계산서확인 && grid.DataTable.Select("S = 'Y' AND ISNULL(DC_RMK_WF, '') = ''").Length > 0)
						{
                            this.ShowMessage("회신내용이 없는 건이 선택되어 있습니다.");
                            return;
                        }
                        else if (Global.MainFrame.LoginInfo.CompanyCode != "K200" &&
                                 선택단계 == 진행단계.수발주통보서결재 && control.Name == this.btn완료.Name && 
                                 grid.DataTable.Select("S = 'Y' AND YN_AM = 'N' AND ((YN_DONE = '미결재' AND ISNULL(DC_COMMENT_FIRST, '') = '') OR (YN_DONE = '1차결재' AND ISNULL(DC_COMMENT_SECOND, '') = ''))").Length > 0)
						{
                            this.ShowMessage("결재 비고에 내용 없는 건이 선택되어 있습니다.\n무상 공급건 결재시, 사유 확인 후 비고에 반드시 확인여부 기입해야 합니다.");
                            return;
                        }
                        else if (this.ShowMessage(선택단계.ToString() + " " + dataRowArray.Length + " 건 " + control.Text + "처리 하시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return;
                    }

                    foreach (DataRow dr in dataRowArray)
                    {
                        switch (선택단계)
                        {
                            case 진행단계.매입문의등록:
                                #region 매입문의등록
                                if (control.Name == this.btn완료.Name)
								{
                                    this.현재단계완료(((int)진행단계.고객문의등록).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.현재단계완료(((int)진행단계.매입문의등록).ToString("00"), D.GetString(dr["NO_KEY"]));

                                    this.다음단계추가(((int)진행단계.문의서검토).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty);
                                    this.다음단계추가(((int)진행단계.견적작성).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty);
                                }
                                else if (control.Name == this.btnRPA재실행.Name)
								{
                                    query = @"UPDATE A
SET A.YN_READ = 'N',
	A.DTS_READ = NULL,
	A.YN_DONE = 'N',
	A.DTS_DONE = NULL,
	A.NO_BOT_READ = NULL
FROM CZ_RPA_WORK_QUEUE A
WHERE A.CD_COMPANY = '{0}' 
AND A.SEQ = '{1}'";
                                    DBHelper.ExecuteScalar(string.Format(query, this.선택회사코드, dr["SEQ"].ToString()));
                                }
                                #endregion
                                break;
                            case 진행단계.매입가등록:
                                #region 매입가등록
                                if (control.Name == this.btn완료.Name)
                                {
                                    this.현재단계완료(((int)진행단계.매입가등록).ToString("00"), D.GetString(dr["NO_KEY"]));
                                }
                                #endregion
                                break;
                            case 진행단계.문의서검토:
                                #region 문의서검토
                                if (control.Name == this.btn완료.Name)
                                {
                                    this.현재단계완료(((int)진행단계.문의서검토).ToString("00"), D.GetString(dr["NO_KEY"]));
                                }
                                #endregion
                                break;
                            case 진행단계.견적작성:
                                #region 견적작성
                                if (control.Name == this.btn완료.Name)
                                {
                                    this.현재단계완료(((int)진행단계.매입가등록).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.현재단계완료(((int)진행단계.견적작성).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.현재단계완료(((int)진행단계.재고견적).ToString("00"), D.GetString(dr["NO_KEY"]));
                                }
                                else if (control.Name == this.btn전달.Name)
                                {
                                    this.현재단계완료(((int)진행단계.매입가등록).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.현재단계완료(((int)진행단계.견적작성).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.현재단계완료(((int)진행단계.재고견적).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.다음단계추가(((int)진행단계.견적제출).ToString("00"), D.GetString(dr["NO_KEY"]), D.GetString(dr["DC_RMK"]));
                                }
                                else if (control.Name == this.btn마감.Name)
                                {
                                    this.현재단계완료(((int)진행단계.매입가등록).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.현재단계완료(((int)진행단계.견적작성).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.현재단계완료(((int)진행단계.재고견적).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    DBHelper.ExecuteScalar("UPDATE CZ_SA_QTNH" + Environment.NewLine +
                                                           "SET YN_SYNC = 'N'" + Environment.NewLine +
                                                           "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                           "AND NO_FILE = '" + D.GetString(dr["NO_KEY"]) + "'");
                                }
                                #endregion
                                break;
                            case 진행단계.재고견적:
                                #region 재고견적
                                query = @"UPDATE WH
SET WH.DC_RMK = ISNULL(WH.DC_RMK, '') + '{2}' 
FROM CZ_MA_WORKFLOWH WH
WHERE WH.CD_COMPANY = '{0}'
AND WH.TP_STEP = '05'
AND WH.NO_KEY = '{1}'";

                                string query1 = @"UPDATE QH
		 SET QH.NO_EMP_QTN = QH.NO_EMP
		 FROM CZ_SA_QTNH QH
		 WHERE QH.CD_COMPANY = '{0}'
		 AND QH.NO_FILE = '{1}'";
                                if (control.Name == this.btn전달.Name)
								{
                                    DataTable dt1 = DBHelper.GetDataTable(string.Format(@"SELECT * 
FROM CZ_PU_QTNH QH WITH(NOLOCK)
WHERE QH.CD_COMPANY = '{0}'
AND QH.NO_FILE = '{1}'
AND CD_PARTNER = '{2}'
AND ISNULL(DT_SEND_MAIL, '') = ''", this.선택회사코드, dr["NO_KEY"].ToString(), dr["CD_SUPPLIER1"].ToString()));

                                    if (dt1 != null && dt1.Rows.Count > 0)
                                        this.ShowMessage("INQ 메일발송누락. 재확인요망");

                                    this.현재단계완료(((int)진행단계.재고견적).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    DBHelper.ExecuteScalar(string.Format(query, this.선택회사코드, dr["NO_KEY"].ToString(), "영업부 견적서 제출건"));
                                    DBHelper.ExecuteScalar(string.Format(query1, this.선택회사코드, dr["NO_KEY"].ToString()));
                                }
                                #endregion
                                break;
                            case 진행단계.고객문의클로징:
                                #region 고객문의클로징
                                this.현재단계완료(((int)진행단계.고객문의클로징).ToString("00"), D.GetString(dr["NO_KEY"]));
                                #endregion
                                break;
                            case 진행단계.견적제출:
                                #region 견적제출
                                if (control.Name == this.btn완료.Name)
                                {
                                    this.현재단계완료(((int)진행단계.견적제출).ToString("00"), D.GetString(dr["NO_KEY"]));
                                }
                                else if (control.Name == this.btnRPA재실행.Name)
								{
                                    query = @"UPDATE A
SET A.YN_READ = 'N',
	A.DTS_READ = NULL,
	A.YN_DONE = 'N',
	A.DTS_DONE = NULL,
	A.NO_BOT_READ = NULL
FROM CZ_RPA_WORK_QUEUE A
WHERE A.CD_COMPANY = '{0}' 
AND A.SEQ = '{1}'";
                                    DBHelper.ExecuteScalar(string.Format(query, this.선택회사코드, dr["SEQ"].ToString()));
                                }
                                #endregion
                                break;
                            case 진행단계.수주등록:
                                #region 수주등록
                                this.현재단계완료(((int)진행단계.수주등록).ToString("00"), D.GetString(dr["NO_KEY"]));
                                #endregion
                                break;
                            case 진행단계.매입구매:
                                #region 매입구매
                                if (DBHelper.GetDataTable(@"SELECT 1 
                                                            FROM SA_SOL SL WITH(NOLOCK)
                                                            LEFT JOIN CZ_SA_STOCK_BOOK SB WITH(NOLOCK) ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
                                                            WHERE SL.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                           "AND SL.NO_SO = '" + D.GetString(dr["NO_KEY"]) + "'" + Environment.NewLine +
                                                           "AND SL.CD_ITEM NOT LIKE 'SD%'" + Environment.NewLine +
                                                           "AND SL.QT_SO > (ISNULL(SL.QT_PO, 0) + ISNULL(SB.QT_STOCK, 0))").Rows.Count > 0)
                                {
                                    this.ShowMessage("전량 재고 사용건이거나, 발주가 완료된 경우에만 확인 처리 가능합니다.");
                                    return;
                                }
                                else
                                {
                                    this.현재단계완료(((int)진행단계.매입구매).ToString("00"), D.GetString(dr["NO_KEY"]));
                                    this.다음단계추가(((int)진행단계.수발주통보서).ToString("00"), D.GetString(dr["NO_KEY"]), D.GetString(dr["DC_RMK"]));
                                }
                                #endregion
                                break;
                            case 진행단계.수발주통보서결재:
                                #region 수발주통보서결재
                                this.현재단계완료(((int)진행단계.수발주통보서결재).ToString("00"), D.GetString(dr["NO_KEY"]));

                                if (!string.IsNullOrEmpty((D.GetString(dr["YN_DONE"]) == "미결재" ? D.GetString(dr["DC_COMMENT_FIRST"]) : D.GetString(dr["DC_COMMENT_SECOND"]))))
                                {
                                                                    contents = @"** 수발주통보서 결재 알림

- 파일번호 : {0}

- 결재상태 : {1}
- 결재자 : {2}
- 결재일자 : {3}

- 결재의견
{4}

위와 같이 수발주통보서 결재가 완료 되었습니다.

참고하시기 바랍니다.

※ 본 쪽지는 발신 전용 입니다.";

                                contents = string.Format(contents, D.GetString(dr["NO_KEY"]),
                                                                   (D.GetString(dr["YN_DONE"]) == "미결재" ? "1차결재" : "2차결재"),
                                                                   (Global.MainFrame.LoginInfo.UserName + " (" + Global.MainFrame.LoginInfo.UserID + ")"),
                                                                   Global.MainFrame.GetDateTimeToday().ToString("yyyy/MM/dd HH:mm:ss"),
                                                                   (D.GetString(dr["YN_DONE"]) == "미결재" ? D.GetString(dr["DC_COMMENT_FIRST"]) : D.GetString(dr["DC_COMMENT_SECOND"])));

                                Messenger.SendMSG(new string[] { D.GetString(dr["ID_SALES"]) }, contents);
                                }
                                #endregion
                                break;
                            case 진행단계.납품목록:
                                #region 납품목록
                                this.현재단계완료(((int)진행단계.납품목록).ToString("00"), D.GetString(dr["NO_KEY"]));
                                #endregion
                                break;
                            case 진행단계.납품완결:
                                #region 납품완결
                                DBHelper.ExecuteScalar("UPDATE CZ_SA_PACKH" + Environment.NewLine +
                                                       "SET CD_LOCATION = NULL" + Environment.NewLine +
                                                       "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                       "AND NO_GIR = '" + D.GetString(dr["NO_KEY"]) + "'" + Environment.NewLine +
                                                       "AND NO_PACK = '" + D.GetString(dr["NO_PACK"]) + "'");
                                #endregion
                                break;
							case 진행단계.인수증확인:
								#region 인수증확인
								DBHelper.ExecuteScalar("UPDATE CZ_SA_GIRH_WORK_DETAIL" + Environment.NewLine +
													  @"SET YN_BILL = 'Y',
                                                            ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
                                                           "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
													   "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
													   "AND NO_GIR = '" + D.GetString(dr["NO_KEY"]) + "'");
								#endregion
								break;
							case 진행단계.계산서발행:
								#region 계산서발행
								DBHelper.ExecuteScalar("UPDATE CZ_SA_GIRH_WORK_DETAIL" + Environment.NewLine +
													  @"SET YN_BILL = 'N',
                                                            ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
                                                           "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
                                                       "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
													   "AND NO_GIR = '" + D.GetString(dr["NO_GIR"]) + "'");
								#endregion
								break;
                            case 진행단계.계산서확인:
                                #region 계산서확인
                                DBHelper.ExecuteScalar(string.Format(@"UPDATE IC
                                                                       SET IC.YN_RETURN = 'Y',
                                                                       	   IC.DTS_RETURN = NEOE.SF_SYSDATE(GETDATE())
                                                                       FROM CZ_PU_IV_CONFIRM IC
                                                                       WHERE IC.CD_COMPANY = '{0}'
                                                                       AND IC.SEQ = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, dr["NO_KEY"].ToString()));

                                contents = @"** 세금계산서 확인요청 내용 회신

순번 : {0}

- 요청내용
{1}

- 회신내용
{2}

내용 확인 후 세금계산서확인요청관리 -> 완료처리 하시기 바랍니다.

※ 본 쪽지는 발신 전용 입니다.";

                                List<string> 수신자 = new List<string>();

                                수신자.Add(dr["ID_INSERT"].ToString());

                                if (dr["NO_EMP_TEAM"].ToString().Contains("S-495"))
                                    수신자.Add("S-495");

                                Messenger.SendMSG(수신자.ToArray(), string.Format(contents, dr["NO_KEY"].ToString(), dr["DC_RMK"].ToString(), dr["DC_RMK_WF"].ToString()));
                                #endregion
                                break;
							default:
                                break;
                        }
                    }

                    DataTable dt = ComFunc.getGridGroupBy(dataRowArray, new string[] { "CD_COMPANY", "NO_KEY" }, true);
                    dt.Columns["NO_KEY"].ColumnName = "NO_FILE";

                    DBMgr.ExecuteNonQuery("PX_CZ_SA_QTN_REG_IC_WF", dt);

                    this.조회결과반영(this.선택단계, this.SearchList(this.ctx회사.CodeValue, this.ctx담당자.CodeValue, this.txt파일번호.Text, this.ctx영업조직.CodeValue, this.bpc영업그룹.QueryWhereIn_Pipe).ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn도움말_Click(object sender, EventArgs e)
        {
            string message;

            try
            {
                switch(this.선택단계)
                {
                    case 진행단계.고객문의등록:
                        message = @"01. 고객문의등록

1. 등록 : 고객문의서 등록시 (재등록시 다시 나옴)
2. 완료
   등록된 매입처 모두에게 INQ 발송 또는
   매입문의등록에서 상태가 완료인 것에 확인

3. 파싱체크 : 고객문의서 파싱가능 여부
4. 비고 : 고객문의서 등록시 첨부파일등록에서 입력하는 비고

5. 워크플로우 담당자 : 영업담당자";
                        break;
                    case 진행단계.매입문의등록:
                        message = @"02. 매입문의등록

1. 등록 : 고객문의서 등록시 (재등록시 다시 나옴)
2. 완료
   등록된 매입처 모두에게 INQ 발송 또는
   매입문의등록에서 상태가 완료인 것에 확인

3. 상태
   매입처 한 곳이라도 등록되어 있으면 '등록'
   견적만 등록되어 있으면 '미등록'
   견적도 등록되어 있지 않으면 '공백' 
4. 파싱체크 : 고객문의서 파싱가능 여부
5. 빨간색표시조건 : 두베코로 로그인 할 경우, 비고에 '이비즈' 또는 'HSD' 문구가 들어가 있으면 빨간색으로 표시
6. 비고 : 고객문의서 등록시 첨부파일등록에서 입력하는 비고

7. 워크플로우 담당자 : 입력담당자";
                        break;
                    case 진행단계.문의서검토:
                        message = @"03. 문의서검토

1. 등록 : 매입문의 완료시
2. 완료 : 선택하고 확인 누르면, 매입처에서 SRM 전송 버튼 누르면

3. 재고견적
   요청 : 재고견적 요청 상태, 재고견적 완료가 되기 전까지 확인버튼을 누를 수 없음
   완료 : 재고견적 완료 상태, 확인 버튼을 누를 수 있음

4. 발송여부 : 견적 문의 메일발송 또는 워크전달 완료 여부 (전체 매입처 발송 완료시 'Y')

5. 워크플로우 담당자 : 영업담당자";
                        break;
                    case 진행단계.매입가등록:
                        message = @"04. 매입가등록

1. 등록 : 매입견적 파일 등록시 (재등록시 다시 나옴)
2. 완료 : 견적등록 -> 단가등록에서 저장 또는 견적작성 단계에서 완료, 매입처에서 SRM 전송 버튼 누르면, 수주등록 된 건인 경우 완료 버튼으로 완료 처리 가능

3. 자동등록체크 : SRM으로 매입견적서가 들어온 경우 체크 표시 됨
4. 매입견적서 상태
   완료 : 견적가, 파일모두 등록
   등록 : 견적가 등록 (파일등록 여부와 관계 없음)
   미등록 : 견적가, 파일모두 미등록
4. 파싱가능체크 : 매입견적서 파싱가능 여부
5. 매입견적서 재등록후 다시 나왔을 경우, 견적작성 단계에서 완료하면 사라짐
6. 매입견적서 등록/재등록 시 견적마감이 되어 있을 경우 마감 해제 됨
7. 수주여부 : 수주등록 되었는지 여부 

8. 워크플로우 담당자 : 입력담당자";
                        break;
                    case 진행단계.견적작성:
                        message = @"05. 견적작성

1. 등록 : 매입문의 완료시 or 매입견적 파일 등록시(견적마감된 경우에만) or 매입가 등록시 or 매입처에서 SRM 전송 버튼 누르면
2. 완료
   선택하고 완료, 전달, 마감 버튼 중에 하나를 누르면
   매입가등록, 재고견적 단계도 완료 처리됨
 
3. 전달 : 견적작성 단계 완료 및 견적제출 단계로 넘어감 
4. 마감 : DS 파일에 대해서 견적 정보 동기화를 시키지 않도록 함

5. 헤더 상태
   견적완료 : 견적일자 O, 매입단가 O OR 매입처견적파일 O
   견적가능 : 견적일자 X, 매입단가 O OR 매입처견적파일 O
   문의중 : 견적일자 X, 매입단가 X, 매입처견적파일 X
6. 헤더 SRM 상태
   가능 : SRM 매입견적서 적용 가능한 매입처 미완료
   완료 : SRM 매입견적서 적용 가능한 매입처 모두 완료
7. 재고견적
   요청 : 재고견적 요청 상태
   완료 : 재고견적 완료 상태
8. 전달상태 (견적제출단계에 데이터가 있는지 여부)
   전달 : 빨간색으로 표시
   미전달 : 일반색으로 표시
9. 매입문의 : 고객문의서 등록일자
10. 경과시간 : 고객문의서 등록 후 경과한 시간
11. 최초견적일 : 최초로 견적등록한 일자 (견적서 출력 여부로 판단)
12. 구매금액 : 매입견적 입력 된 금액
13. 매입문의처 : 매입처 갯수
14. 견적접수처 : 매입단가 입력된 매입처 갯수
15. 라인 상태
    완료 : 매입가 O, 매입처견적파일 O
    등록 : 매입가 O
    미등록 : 매입가 X, 매입처견적파일 X
16. 라인 SRM 상태
    가능 : SRM 매입견적서 적용 가능
    완료 : SRM 매입견적서 적용 완료

17. 버튼활성화 조건
    완료, 전달, 마감 : 최초견적일자가 있을 경우 활성화

18. 견적담당 : 재고견적 담당자

19. 워크플로우 담당자 : 영업담당자, 견적담당자";
                        break;
                    case 진행단계.견적제출:
                        message = @"06. 견적제출

1. 등록 : 견적작성에서 전달을 누르면
2. 완료 : 선택하고 확인을 누르면

3. 비고 : 견적작성 단계에서 입력하는 비고
4. 제출비고 : 쉽서브 제출시 복사 붙여넣기 하는 비고
5. 붉은색표시조건 : 통화명이 KRW, USD가 아니거나 T/T 30일 지불조건이 아닌경우 붉은색 표시 (쉽서브 제출시 주의)
6. 견적발송용 : 선사에 견적제출시 추가로 제출해야 하는 파일이 존재하는지 여부

7. 워크플로우 담당자 : 입력담당자";
                        break;
                    case 진행단계.고객문의클로징:
                        message = @"07. 고객문의클로징

1. 등록 : 견적마감 시
2. 완료 : 선택하고 확인 또는 견적마감 복구시
 
3. 상태
   견적완료 : 견적일자 O, 매입단가 O OR 매입처견적파일 O
   견적가능 : 견적일자 X, 매입단가 O OR 매입처견적파일 O
   문의중 : 견적일자 X, 매입단가 X, 매입처견적파일 X
4. 비고 : 견적비고

5. 워크플로우 담당자 : 영업담당자";
                        break;
                    case 진행단계.수주등록:
                        message = @"08. 수주등록

1. 등록 : 고객발주서 등록시 (재등록시 다시 나옴)
2. 완료 : 수주 등록시
 
3. 등록여부 : 수주등록이 되어 있는지 여부
4. 비고 : 고객발주서 등록시 첨부파일등록에서 입력하는 비고 (수정가능)
5. 수주등록 후 고객발주서가 등록되는 경우, 선택 후 완료 버튼을 누르면 됨

6. 워크플로우 담당자 : 구매담당자";
                        break;
                    case 진행단계.수주확인서:
                        message = @"09. 수주확인서

1. 등록 : 수주 등록시
2. 완료 : 수주확인서 출력시
3. 비고 : 수주확인서 단계 비고 (수정가능)

4. 워크플로우 담당자 : 구매담당자";
                        break;
                    case 진행단계.매입구매:
                        message = @"10. 매입구매

1. 등록 : 수주확인서 출력시
2. 완료 : 발주등록 후 Order Sheet 출력시, 수발주통보서 확정시

3. 전량 재고 사용건의 경우 선택 후 확인 처리해야 함
4. 전량 재고 사용시 확인 누락건이 발생해서 수발주통보서 확정시 완료처리 되도록 수정함

5. 비고 : 매입구매 단계 비고 (수정가능)

6. 워크플로우 담당자 : 구매담당자";
                        break;
                    case 진행단계.수발주통보서:
                        message = @"11. 수발주통보서

1. 등록 : 발주등록 후 Order Sheet 출력시 또는 매입구매 단계에서 완료버튼 클릭시 (전량재고 사용건)
2. 완료 : 수발주통보서 확정시
3. 완료취소 : 수발주통보서 확정취소시

4. 비고 : 수발주통보서 단계 비고 (수정가능)

4. 워크플로우 담당자 : 구매담당자";
                        break;
                    case 진행단계.수발주통보서결재:
                        message = @"12. 수발주통보서결재

1. 등록 : 수발주통보서 확정시
2. 완료 : 1차, 2차 결재 완료 시
3. 완료취소 : 수발주통보서 확정취소 또는 결재완료후 재상신시

4. 확인 : 미결재 -> 1차결재, 1차결재 -> 완료로 단계 진행 (해당 단계 결재 비고에 내용이 있을 경우 영업담당자에게 쪽지 발송)

5. 선택 : 미결재시 1차 결재자, 1차 결재시 2차 결재자만 선택 가능
6. 결재상태
   미결재 : 수발주통보서 확정 후 결재가 되지 않는 상태
   1차결재 : 1차결재 완료
7. 재상신체크 : 수발주통보서 화면에서 재상신 버튼을 누른 경우 체크 됨
8. 1차결재, 2차결재 : 구매담당자의 1차, 2차 결재자 (첨부파일등록현황 -> 영업정보등록에서 등록 가능)
9. 커미션 : 수주등록 -> 커미션
10. 수통비고 : 수주등록 -> 수통비고
11. 결재비고
    1차결재, 2차결재자만 입력 가능
    다음 결재 단계로 넘어가면 이전 단계 결재비고 수정불가
    무상공급건 결재비고 필수 입력
12. 품목 중에 하나라도 이윤이 마이너스가 있으면 빨간색으로 표시 됨

13. 워크플로우 담당자 : 구매담당자, 구매담당자의 1차, 2차 결재자";
                        break;
                    case 진행단계.납품목록:
                        message = @"13. 납품목록

1. 등록 : 수발주통보서 확정시
2. 완료 : 선택 후 확인
3. 완료취소 : 수발주통보서 확정취소시

4. 비고 : 수주등록 -> 물류비고 (수정가능)

5. 워크플로우 담당자 : 물류담당자";
                        break;
                    case 진행단계.협조전작성:
                        message = @"14. 협조전작성

1. 등록 : 수발주통보서 확정 후 전량 입고 되었지만 물류업무협조전 작성 안됨
2. 완료 : 물류업무협조전 작성

3. 무게 : 입고등록 시 입력한 무게
4. 수주비고 : 수주등록 -> 담당비고
5. 물류비고 : 수주등록 -> 물류비고

6. 워크플로우 담당자 : 수발주통보서 물류 담당자";
                        break;
                    case 진행단계.협조전완결:
                        message = @"15. 협조전완결

1. 등록 : 물류업무협조전 작성
2. 완료 : 물류업무협조전 출고
3. 조회 : 파일번호 란에 물류업무협조전 번호로 조회

4. 포장여부 : 물류업무협조전 -> Incl. Packing
5. 비고 : 물류업무협조전 -> 상세요청
 
6. 워크플로우 담당자 : 협조전담당자";
                        break;
                    case 진행단계.납품완결:
                        message = @"16. 납품완결

1. 등록 : 출고는 되었지만 실제로 창고에 남아 있는 경우, 포장위치가 있음
2. 완료 : 선택 후 확인 (포장 위치가 제거 됨)

3. 포장위치 : PDA 프로그램 -> 포장로케이션 메뉴에서 입력
3. 비고 : 포장비고
4. 택배발송 후 물류부에서 완료 처리하기로 함

5. 워크플로우 담당자 : 협조전담당자";
                        break;
                    case 진행단계.인수증등록:
                        message = @"17. 인수증등록

1. 등록 : 출고처리 되었지만 인수증이 등록되지 않았거나 선적일자가 등록되지 않은경우
2. 완료 : 인수증, 선적일자 등록
3. 조회 : 파일번호 란에 물류업무협조전 번호로 조회

3. 인수증등록여부 : 인수증이 등록 되어 있는지 여부
4. 비고 : 물류업무협조전 상세요청
5. 매출비고 : 물류업무협조전 매출비고

6. 워크플로우 담당자 : 협조전담당자";
                        break;
                    case 진행단계.계산서발행:
                        message = @"18. 계산서발행

1. 등록 : 출고처리 되었지만 매출등록 되지 않은 경우
2. 완료 : 매출등록
3. 조회 : 파일번호 란에 출고 번호로 조회

3. 반려 : 인수증확인 단계가 재등록 됨

4. 상태필터
   없음 : 리스트 필터 사용가능
   미발행 : 인수증등록 and 선적일자등록 and 인수증확인 (딘텍싱가폴, 해외선사 제외)
   미등록 : 인수증미등록 or 선적일자미등록 or 인수증미확인 (딘텍싱가폴, 해외선사 제외)

5. 출고상태
   P : 부분송품
   C : 전체송품
6. 인수증 : 인수증이 등록 되어 있으면 텍스트가 표시 됨
7. 인수증확인여부 : 인수증확인 단계에서 확인 완료된 건 체크 됨
8. 자동처리대상 : 해외 매출인보이스 자동 발행 대상 여부
 - 거래구분 : 해외
 - 지불조건 : CASH IN ADVANCE, CASH ON DELIVERY X
 - 매출처 : 인보이스 수기발행 X
 - 거래처그룹 : 국내-부품, 국내-선용, 한국(두베코) X
 - EIL 건 X
 - 국내(DB, D-), 기술서비스(TE) 건 X
9. 청구예정일(수정가능) : 물류업무협조전 -> 청구예정일
10. 비고 : 물류업무협조전 -> 상세요청
11. 매출비고(수정가능) : 물류업무협조전 -> 매출비고
12. 선적일자확인제외 : 자동매출발행시 선적일자 관계없이 자동매출처리 가능 여부

13. 워크플로우 담당자 : 전체";
                        break;
                    case 진행단계.장기미출고:
                        message = @"19. 장기미출고

1. 표시조건
   90일동안 출고 안된 것 (아이템 단위)
   수주마감 여부 : N
   수주 수량 > 출고수량
   입고 수량 > 출고수량
   가장 최근 입고일자 > 90 (미출고관리일수(CZ_SA00035) 코드로 설정가능)

2. 미출고수량 : 입고수량 - 출고수량
3. 지연일수 : 입고일자 기준 지연 일수
4. 수주비고 : 수주등록 -> 담당비고
5. 물류비고 : 수주등록 -> 물류비고
6. 미출고비고 : 장기미출고 단계 미출고비고 (CZ_MA00010)
7. 미출고상세비고 : 장기미출고 단계 미출고상세비고

8. 워크플로우 담당자 : 수주등록 단계 물류 담당자";
                        break;
                    case 진행단계.클레임종결:
                        message = @"20. 클레임종결

1. 표시조건 : 클레임 등록 건 중에서 상태가 종결 및 전자결재 승인 상태가 아닌 건

2. 경과개월 : 발행일자 기준 경과개월

3. 워크플로우 담당자 : 클레임담당자";
                        break;
                    case 진행단계.재고견적:
                        message = @"21. 재고견적

1. 등록
    견적등록 -> INQ.발송 -> 워크전달 버튼을 누르면 (CZ_DX00009 키워드 참조)

2. 완료
    견적요청 중에 견적작성단계가 완료되는 경우
    견적등록 -> 견적작성 -> 메일발송 또는 워크전달 버튼을 누르면

3. 매입처 필터 : 선택한 요청매입처 기준으로 조회
4. 영업부 : 해당 단계 완료 후 견적작성 비고 란에 '영업부 견적서 제출건'이라고 표시

5. 매입견적 : 매입견적서 파일 존재 여부
6. 매입처 : 매입처명 (매입문의일자 : 견적등록 -> INQ.발송 -> 작성일자)
7. 요청매입처 : 견적 요청한 매입처 
8. 비고
   CZ_DX00014 참조해서 자동 입력
   견적담당자만 입력 및 수정 가능
9. 노란색 표시조건 : 견적등록 -> INQ 발송 인쇄시 기획실에서 견적내야하는건 판단해서 노란색으로 표시 (영업담당자 견적내면 안됨)

10. 워크플로우 담당자 : 영업담당자, 기획실 견적담당자";
                        break;
					case 진행단계.인수증확인:
						message = @"22. 인수증확인

1. 등록 : 출고처리 후 물류부에서 인수증 등록 및 선적일자 입력 완료시 (국내선사만 해당 됨)
2. 완료 : 매출정산이 완료 되거나, 본선인수증 등록 및 선사에 청구 가능해 질 경우 선택 후 완료 버튼을 누르면
3. 재등록 : 계산서발행 단계에서 반려 버튼을 누르면

4. 출고상태
   P : 부분송품
   C : 전체송품
5. 청구예정일(수정가능) : 물류업무협조전 청구예정일 
6. 인수증 : 인수증 컬럼 더블클릭시 인수증 등록 팝업창 표시 됨
7. 매출비고(수정가능) : 물류업무협조전 매출비고

8. 워크플로우 담당자 : 수주담당자, 협조전담당자";
						break;
                    case 진행단계.계산서확인:
                        message = @"23. 계산서확인

1. 등록 : 매입정산시 담당자 확인이 필요한 사항이 있으면 정산 담당자가 등록 함 (매입등록, 자동매입정산관리, 계산서확인요청관리)
2. 회신 : 확인요청 내용 검토 및 회신내용에 내용 기재 후 저장하고 회신 버튼 누르면 정산담당자에게 내용 회신 됨 (회신한다고 완료 처리 되지 않음)
3. 완료 : 정산담당자가 회신내용 검토 후 완료처리 (계산서확인요청관리)

4. 재알람 쪽지
   확인 요청 후 24시간 경과시까지 완료처리 되지 않으면 재알람 쪽지 발송
   마감일자 1일 남거나 경과 했을 경우, 24시간 경과 관계 없이 재알람 쪽지 발송

5. 회신권한 : 확인담당자, 확인담당자의 팀장

6. 워크플로우 담당자 : 확인담당자 (해당 발주건의 담당자 또는 수기로 지정한 담당자), 확인담당자의 팀장, 정산담당자";
                        break;
                    default:
                        return;
                }

                this.ShowMessage(message);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn딘텍원격지원_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowMessage("딘텍 원격지원 페이지(http://helpu.kr/dintec) 에 접속 합니다.");

                Process.Start("msedge.exe", "http://helpu.kr/dintec");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void ResetSize(int height, int width)
        {
            this.oneGrid2.Height = height;
            this.oneGrid2.Width = width;
        }

        private void RefreshControl()
        {
            Color beforeBackColor, afterBackColor;

            try
            {
                beforeBackColor = Color.Transparent;
                afterBackColor = Color.LightCyan;

                #region 초기화
                this.btn고객문의등록.StepBackColor = beforeBackColor;
                this.btn매입문의등록.StepBackColor = beforeBackColor;
                this.btn문의서검토.StepBackColor = beforeBackColor;
                this.btn매입가등록.StepBackColor = beforeBackColor;
                this.btn견적작성.StepBackColor = beforeBackColor;
                this.btn재고견적.StepBackColor = beforeBackColor;
                this.btn견적제출.StepBackColor = beforeBackColor;
                this.btn고객문의클로징.StepBackColor = beforeBackColor;
                this.btn수주등록.StepBackColor = beforeBackColor;
                this.btn수주확인서.StepBackColor = beforeBackColor;
                this.btn매입구매.StepBackColor = beforeBackColor;
                this.btn수발주통보서.StepBackColor = beforeBackColor;
                this.btn수발주통보서결재.StepBackColor = beforeBackColor;
                this.btn납품목록.StepBackColor = beforeBackColor;
                this.btn장기미출고.StepBackColor = beforeBackColor;
                this.btn협조전작성.StepBackColor = beforeBackColor;
                this.btn협조전완결.StepBackColor = beforeBackColor;
                this.btn납품완결.StepBackColor = beforeBackColor;
                this.btn인수증등록.StepBackColor = beforeBackColor;
                this.btn계산서발행.StepBackColor = beforeBackColor;
                this.btn클레임종결.StepBackColor = beforeBackColor;
				this.btn인수증확인.StepBackColor = beforeBackColor;
                this.btn계산서확인.StepBackColor = beforeBackColor;
				#endregion

				#region 설정
				switch (this.선택단계)
                {
                    case 진행단계.고객문의등록:
                        this.btn고객문의등록.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.매입문의등록:
                        this.btn매입문의등록.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.문의서검토:
                        this.btn문의서검토.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.매입가등록:
                        this.btn매입가등록.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.견적작성:
                        this.btn견적작성.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.재고견적:
                        this.btn재고견적.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.견적제출:
                        this.btn견적제출.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.고객문의클로징:
                        this.btn고객문의클로징.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.수주등록:
                        this.btn수주등록.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.수주확인서:
                        this.btn수주확인서.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.매입구매:
                        this.btn매입구매.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.수발주통보서:
                        this.btn수발주통보서.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.수발주통보서결재:
                        this.btn수발주통보서결재.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.납품목록:
                        this.btn납품목록.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.장기미출고:
                        this.btn장기미출고.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.협조전작성:
                        this.btn협조전작성.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.협조전완결:
                        this.btn협조전완결.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.납품완결:
                        this.btn납품완결.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.인수증등록:
                        this.btn인수증등록.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.계산서발행:
                        this.btn계산서발행.StepBackColor = afterBackColor;
                        break;
                    case 진행단계.클레임종결:
                        this.btn클레임종결.StepBackColor = afterBackColor;
                        break;
					case 진행단계.인수증확인:
						this.btn인수증확인.StepBackColor = afterBackColor;
						break;
                    case 진행단계.계산서확인:
                        this.btn계산서확인.StepBackColor = afterBackColor;
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void RefreshCount(string 회사코드, string 담당자, string 파일번호, string 영업조직, string 영업그룹)
        {
            try
            {
                MsgControl.ShowMsg("[자료조회중 : 고객문의등록]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.고객문의등록, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 매입문의등록]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.매입문의등록, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 문의서검토]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.문의서검토, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 매입가등록]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.매입가등록, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 견적작성]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.견적작성, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 견적제출]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.견적제출, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 고객문의클로징]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.고객문의클로징, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 수주등록]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.수주등록, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 수주확인서]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.수주확인서, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 매입구매]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.매입구매, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 수발주통보서]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.수발주통보서, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 납품목록]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.납품목록, 파일번호, 담당자, 영업조직, 영업그룹);
                MsgControl.ShowMsg("[자료조회중 : 재고견적]\n잠시만 기다려 주세요.");
                this.표시숫자갱신(회사코드, 진행단계.재고견적, 파일번호, 담당자, 영업조직, 영업그룹);

                if (this.chk전체단계갱신.Checked == true)
                {
                    MsgControl.ShowMsg("[자료조회중 : 수발주통보서결재]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.수발주통보서결재, 파일번호, 담당자, 영업조직, 영업그룹);
                    MsgControl.ShowMsg("[자료조회중 : 협조전완결]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.협조전완결, 파일번호, 담당자, 영업조직, 영업그룹);
                    MsgControl.ShowMsg("[자료조회중 : 납품완결]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.납품완결, 파일번호, 담당자, 영업조직, 영업그룹);
                    MsgControl.ShowMsg("[자료조회중 : 인수증등록]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.인수증등록, 파일번호, 담당자, 영업조직, 영업그룹);
                    MsgControl.ShowMsg("[자료조회중 : 클레임종결]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.클레임종결, 파일번호, 담당자, 영업조직, 영업그룹);

                    MsgControl.ShowMsg("[자료조회중 : 협조전작성]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.협조전작성, 파일번호, 담당자, 영업조직, 영업그룹);
                    MsgControl.ShowMsg("[자료조회중 : 계산서발행]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.계산서발행, 파일번호, 담당자, 영업조직, 영업그룹);
					MsgControl.ShowMsg("[자료조회중 : 인수증확인]\n잠시만 기다려 주세요.");
					this.표시숫자갱신(회사코드, 진행단계.인수증확인, 파일번호, 담당자, 영업조직, 영업그룹);
                    MsgControl.ShowMsg("[자료조회중 : 계산서확인]\n잠시만 기다려 주세요.");
                    this.표시숫자갱신(회사코드, 진행단계.계산서확인, 파일번호, 담당자, 영업조직, 영업그룹);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void 조회결과반영(진행단계 반영단계, string count)
        {
            try
            {
                switch (반영단계)
                {
                    case 진행단계.고객문의등록:
                        this.btn고객문의등록.StepCount = count;
                        break;
                    case 진행단계.매입문의등록:
                        this.btn매입문의등록.StepCount = count;
                        break;
                    case 진행단계.문의서검토:
                        this.btn문의서검토.StepCount = count;
                        break;
                    case 진행단계.매입가등록:
                        this.btn매입가등록.StepCount = count;
                        break;
                    case 진행단계.견적작성:
                        this.btn견적작성.StepCount = count;
                        break;
                    case 진행단계.재고견적:
                        if (Convert.ToInt32(count) > 0)
                            this.btn재고견적.StepImage = cz.Properties.Resources.견적요청미완료;
                        else
                            this.btn재고견적.StepImage = cz.Properties.Resources.견적요청완료;
                        this.btn재고견적.StepCount = count;
                        break;
                    case 진행단계.견적제출:
                        this.btn견적제출.StepCount = count;
                        break;
                    case 진행단계.고객문의클로징:
                        this.btn고객문의클로징.StepCount = count;
                        break;
                    case 진행단계.수주등록:
                        this.btn수주등록.StepCount = count;
                        break;
                    case 진행단계.수주확인서:
                        this.btn수주확인서.StepCount = count;
                        break;
                    case 진행단계.매입구매:
                        this.btn매입구매.StepCount = count;
                        break;
                    case 진행단계.수발주통보서:
                        this.btn수발주통보서.StepCount = count;
                        break;
                    case 진행단계.수발주통보서결재:
                        this.btn수발주통보서결재.StepCount = count;
                        break;
                    case 진행단계.납품목록:
                        this.btn납품목록.StepCount = count;
                        break;
                    case 진행단계.장기미출고:
                        this.btn장기미출고.StepCount = count;
                        break;
                    case 진행단계.협조전작성:
                        this.btn협조전작성.StepCount = count;
                        break;
                    case 진행단계.협조전완결:
                        this.btn협조전완결.StepCount = count;
                        break;
                    case 진행단계.납품완결:
                        this.btn납품완결.StepCount = count;
                        break;
                    case 진행단계.인수증등록:
                        this.btn인수증등록.StepCount = count;
                        break;
                    case 진행단계.계산서발행:
                        this.btn계산서발행.StepCount = count;
                        break;
                    case 진행단계.클레임종결:
                        if (Convert.ToInt32(count) > 0)
                            this.btn클레임종결.StepImage = cz.Properties.Resources.클레임종결2;
                        else
                            this.btn클레임종결.StepImage = cz.Properties.Resources.클레임종결3;
                        this.btn클레임종결.StepCount = count;
                        break;
					case 진행단계.인수증확인:
						this.btn인수증확인.StepCount = count;
						break;
                    case 진행단계.계산서확인:
                        this.btn계산서확인.StepCount = count;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 표시숫자갱신(string 회사코드, 진행단계 단계, string 파일번호, string 담당자, string 영업조직, string 영업그룹)
        {
            DataTable dt = null;

            try
            {
                switch(단계)
                {
                    case 진행단계.고객문의등록:
                    case 진행단계.매입문의등록:
                    case 진행단계.문의서검토:
                    case 진행단계.매입가등록:
                    case 진행단계.견적작성:
                    case 진행단계.견적제출:
                    case 진행단계.고객문의클로징:
                    case 진행단계.수주등록:
                    case 진행단계.수주확인서:
                    case 진행단계.매입구매:
                    case 진행단계.수발주통보서:
                    case 진행단계.납품목록:
                    case 진행단계.재고견적:
                        dt = DBHelper.GetDataTable("SP_CZ_MA_WORKFLOW_COUNT", new object[] { 회사코드,
                                                                                             ((int)단계).ToString("00"),
                                                                                             파일번호,
                                                                                             담당자,
                                                                                             영업조직,
                                                                                             영업그룹 });
                        break;
                    case 진행단계.수발주통보서결재:
                    case 진행단계.협조전작성:
                    case 진행단계.협조전완결:
                    case 진행단계.납품완결:
                    case 진행단계.인수증등록:
                    case 진행단계.클레임종결:
					case 진행단계.인수증확인:
                    case 진행단계.계산서확인:
                        dt = DBHelper.GetDataTable("SP_CZ_MA_WORKFLOW_COUNT_" + ((int)단계).ToString("00"), new object[] { 회사코드,
                                                                                                                           담당자,
                                                                                                                           파일번호,
                                                                                                                           영업조직,
                                                                                                                           영업그룹 });
                        break;
                    case 진행단계.계산서발행:
                        dt = DBHelper.GetDataTable("SP_CZ_MA_WORKFLOW_COUNT_" + ((int)단계).ToString("00"), new object[] { 회사코드,
                                                                                                                           파일번호,
                                                                                                                           영업조직,
                                                                                                                           영업그룹 });
                        break;
                    case 진행단계.장기미출고:
                        dt = DBHelper.GetDataTable("SP_CZ_MA_WORKFLOW_COUNT_" + ((int)단계).ToString("00"), new object[] { 회사코드,
                                                                                                                           담당자,
                                                                                                                           파일번호,
                                                                                                                           영업조직,
                                                                                                                           영업그룹,
                                                                                                                           this._미출고관리일수 });
                        break;
                    default:
                        return;
                }

                if (dt != null)
                    this.조회결과반영(단계, (dt.Rows.Count > 0 ? dt.Rows[0]["CNT"].ToString() : "0"));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 선택단계설정()
        {
            try
            {
                this.pnlButtom.TitleText = this.선택단계.ToString();

                this.btn마감.Visible = false;
                this.btn완료.Visible = false;
                this.btn전달.Visible = false;
                this.btnRPA재실행.Visible = false;
                
                this.cbo필터.Visible = false;

                this.tabControl.SelectedIndex = (((int)this.선택단계) - 1);

                switch (this.선택단계)
                {
                    case 진행단계.매입문의등록:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        this.btn완료.Visible = this.수정가능여부;

                        this.btnRPA재실행.Enabled = true;
                        this.btnRPA재실행.Visible = this.수정가능여부;
                        break;
                    case 진행단계.문의서검토:
                        //if (this.LoginInfo.CompanyCode == "K100")
                        //    this.btn요청.Visible = this.수정가능여부;

                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
                    case 진행단계.매입가등록:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("완료");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
                    case 진행단계.견적작성:
                        //if (this.LoginInfo.CompanyCode == "K100")
                        //    this.btn요청.Visible = this.수정가능여부;

                        if (this.수정가능여부 == true)
                        {
                            UGrant ugrant = new UGrant();
                            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "CLOSE", this.btn마감);
                        }
                        else
                            this.btn마감.Visible = false;

                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("완료");
                        this.btn완료.Visible = this.수정가능여부;

                        this.btn전달.Enabled = true;
                        this.btn전달.Text = Global.MainFrame.DD("전달");
                        this.btn전달.Visible = this.수정가능여부;

						this.cbo필터.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_MA00015");
						this.cbo필터.ValueMember = "CODE";
						this.cbo필터.DisplayMember = "NAME";

						this.cbo필터.SelectedValue = this.견적작성필터;

						this.cbo필터.Visible = true;
                        break;
                    case 진행단계.재고견적:
                        this.btn전달.Enabled = true;
                        this.btn전달.Text = Global.MainFrame.DD("영업부");
                        this.btn전달.Visible = this.수정가능여부;

						this.cbo필터.DataSource = DBHelper.GetDataTable(string.Format(@"
SELECT '' AS CODE,
       '' AS NAME
UNION ALL
SELECT MC.CD_SYSDEF AS CODE,
	   MC.NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL MC 
WHERE MC.CD_COMPANY = '{0}'
AND MC.CD_FIELD = 'CZ_DX00009'
AND MC.YN_USE = 'Y'", Global.MainFrame.LoginInfo.CompanyCode));
						this.cbo필터.ValueMember = "CODE";
                        this.cbo필터.DisplayMember = "NAME";

                        this.cbo필터.Visible = true;
                        break;
                    case 진행단계.견적제출:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        this.btn완료.Visible = this.수정가능여부;

                        this.btnRPA재실행.Enabled = true;
                        this.btnRPA재실행.Visible = this.수정가능여부;
                        break;
                    case 진행단계.고객문의클로징:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
                    case 진행단계.수주등록:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("완료");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
                    case 진행단계.매입구매:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
                    case 진행단계.수발주통보서결재:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
                    case 진행단계.납품목록:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
                    case 진행단계.납품완결:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("확인");
                        if (this.수정가능여부 == true)
                        {
                            UGrant ugrant = new UGrant();
                            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "CONFIRM", this.btn완료);
                        }
                        else
                            this.btn완료.Visible = false;
                        break;
					case 진행단계.인수증확인:
						this.btn완료.Enabled = true;
						this.btn완료.Text = Global.MainFrame.DD("완료");
						this.btn완료.Visible = this.수정가능여부;
						break;
					case 진행단계.계산서발행:
						this.btn전달.Enabled = true;
						this.btn전달.Text = Global.MainFrame.DD("반려");
						this.btn전달.Visible = this.수정가능여부;

						this.cbo필터.DataSource = MA.GetCodeUser(new string[] { "", "001", "002" }, new string[] { "", "미발행", "미등록" });
						this.cbo필터.ValueMember = "CODE";
						this.cbo필터.DisplayMember = "NAME";

						this.cbo필터.SelectedValue = this.계산서발행필터;

						this.cbo필터.Visible = true;
						break;
                    case 진행단계.계산서확인:
                        this.btn완료.Enabled = true;
                        this.btn완료.Text = Global.MainFrame.DD("회신");
                        this.btn완료.Visible = this.수정가능여부;
                        break;
				}
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 현재단계완료(string 단계, string key)
        {
            try
            {
                DBHelper.ExecuteScalar("SP_CZ_MA_WORKFLOW_DONE", new object[] { this.선택회사코드,
                                                                                단계,
                                                                                key,
                                                                                Global.MainFrame.LoginInfo.UserID });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 다음단계추가(string 단계, string key, string 비고)
        {
            try
            {
                DBHelper.ExecuteScalar("SP_CZ_MA_WORKFLOW_NEXT_STEP", new object[] { this.선택회사코드,
                                                                                     단계,
                                                                                     key,
                                                                                     Global.MainFrame.LoginInfo.UserID,
                                                                                     비고 });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public int SearchList(string 회사, string 담당자, string 파일번호, string 영업조직, string 영업그룹)
        {
            DataTable dt;
            진행단계 선택단계;

            try
            {
                base.OnToolBarSearchButtonClicked(null, null);

                if (this.선택단계 == 진행단계.없음) return 0;

                선택단계 = this.선택단계;

                if (선택단계 == 진행단계.장기미출고)
                {
                    dt = this._biz.Search(this.선택단계, new object[] { 회사,
                                                                        담당자,
                                                                        파일번호,
                                                                        영업조직,
                                                                        영업그룹,
                                                                        this._미출고관리일수 });
                }
                else
                {
                    dt = this._biz.Search(this.선택단계, new object[] { 회사,
                                                                        담당자,
                                                                        파일번호,
                                                                        영업조직,
                                                                        영업그룹 });
                }

                switch (선택단계)
                {
                    case 진행단계.고객문의등록:
                        #region 고객문의등록
                        if (this._고객문의등록H.DataTable == null)
                        {
                            this._고객문의등록H.DetailGrids = new FlexGrid[] { this._고객문의등록L };

                            this.SetHeaderGrid(this._고객문의등록H);
                            this.SetLineGrid(this._고객문의등록L);
                        }

                        this._고객문의등록H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.매입문의등록:
                        #region 매입문의등록
                        if (this._매입문의등록H.DataTable == null)
                        {
                            this._매입문의등록H.DetailGrids = new FlexGrid[] { this._매입문의등록L };

                            this.SetHeaderGrid(this._매입문의등록H);
                            this.SetLineGrid(this._매입문의등록L);
                        }

                        this._매입문의등록H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.문의서검토:
                        #region 문의서검토
                        if (this._문의서검토H.DataTable == null)
                        {
                            this._문의서검토H.DetailGrids = new FlexGrid[] { this._문의서검토L1, this._문의서검토L2 };

                            this.SetHeaderGrid(this._문의서검토H);
                            this.SetLineGrid(this._문의서검토L1);
                            this.SetLineGrid(this._문의서검토L2);
                        }

                        this._문의서검토H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.매입가등록:
                        #region 매입가등록
                        if (this._매입가등록H.DataTable == null)
                        {
                            this._매입가등록H.DetailGrids = new FlexGrid[] { this._매입가등록L1, this._매입가등록L2 };

                            this.SetHeaderGrid(this._매입가등록H);
                            this.SetLineGrid(this._매입가등록L1);
                            this.SetLineGrid(this._매입가등록L2);
                        }

                        this._매입가등록H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.견적작성:
                        #region 견적작성
                        if (this._견적작성H.DataTable == null)
                        {
                            this._견적작성H.DetailGrids = new FlexGrid[] { this._견적작성L };

                            this.SetHeaderGrid(this._견적작성H);
                            this.SetLineGrid(this._견적작성L);
                        }

                        this._견적작성H.Binding = dt;

						if (string.IsNullOrEmpty(D.GetString(this.cbo필터.SelectedValue)))
							this._견적작성H.RowFilter = string.Empty;
						else
							this._견적작성H.RowFilter = "TP_STATE = '" + this.cbo필터.SelectedValue + "'";
						#endregion
						break;
                    case 진행단계.재고견적:
                        #region 재고견적
                        if (this._재고견적H.DataTable == null)
                        {
                            this._재고견적H.DetailGrids = new FlexGrid[] { this._재고견적L };

                            this.SetHeaderGrid(this._재고견적H);
                            this.SetLineGrid(this._재고견적L);
                        }

                        this._재고견적H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.견적제출:
                        #region 견적제출
                        if (this._견적제출H.DataTable == null)
                            this.SetHeaderGrid(this._견적제출H);

                        this._견적제출H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.고객문의클로징:
                        #region 고객문의클로징
                        if (this._고객문의클로징H.DataTable == null)
                            this.SetHeaderGrid(this._고객문의클로징H);

                        this._고객문의클로징H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.수주등록:
                        #region 수주등록
                        if (this._수주등록H.DataTable == null)
                        {
                            this._수주등록H.DetailGrids = new FlexGrid[] { this._수주등록L1, this._수주등록L2, this._수주등록L3 };

                            this.SetHeaderGrid(this._수주등록H);
                            this.SetLineGrid(this._수주등록L1);
                            this.SetLineGrid(this._수주등록L2);
                            this.SetLineGrid(this._수주등록L3);
                        }

                        this._수주등록H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.수주확인서:
                        #region 수주확인서
                        if (this._수주확인서H.DataTable == null)
                        {
                            this._수주확인서H.DetailGrids = new FlexGrid[] { this._수주확인서L1, this._수주확인서L2 };

                            this.SetHeaderGrid(this._수주확인서H);
                            this.SetLineGrid(this._수주확인서L1);
                            this.SetLineGrid(this._수주확인서L2);
                        }

                        this._수주확인서H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.매입구매:
                        #region 매입구매
                        if (this._매입구매H.DataTable == null)
                        {
                            this._매입구매H.DetailGrids = new FlexGrid[] { this._매입구매L1, this._매입구매L2, this._매입구매L3 };

                            this.SetHeaderGrid(this._매입구매H);
                            this.SetLineGrid(this._매입구매L1);
                            this.SetLineGrid(this._매입구매L2);
                            this.SetLineGrid(this._매입구매L3);
                        }

                        this._매입구매H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.수발주통보서:
                    case 진행단계.수발주통보서결재:
                        #region 수발주통보서/결재
                        FlexGrid grid;
                        decimal 원화수주금액, 수주비용, 이윤, 비용포함이윤;

                        if (선택단계 == 진행단계.수발주통보서)
                            grid = this._수발주통보서H;
                        else
                            grid = this._수발주통보서결재H;

                        if (grid.DataTable == null)
                            this.SetHeaderGrid(grid);

                        grid.Binding = dt;

                        if (grid.HasNormalRow)
                        {
                            원화수주금액 = D.GetDecimal(grid.DataTable.Compute("SUM(AM_SO)", string.Empty));
                            수주비용 = D.GetDecimal(grid.DataTable.Compute("SUM(AM_SO_CHARGE)", string.Empty));
                            이윤 = D.GetDecimal(grid.DataTable.Compute("SUM(PROFIT)", string.Empty));
                            비용포함이윤 = D.GetDecimal(grid.DataTable.Compute("SUM(PROFIT_ALL)", string.Empty));

                            grid[grid.Rows.Fixed - 1, "RT_PROFIT"] = string.Format("{0:" + grid.Cols["RT_PROFIT"].Format + "}", (원화수주금액 == 0 ? 0 : Decimal.Round(((이윤 / 원화수주금액) * 100), 2, MidpointRounding.AwayFromZero)));
                            grid[grid.Rows.Fixed - 1, "RT_PROFIT_ALL"] = string.Format("{0:" + grid.Cols["RT_PROFIT_ALL"].Format + "}", ((원화수주금액 + 수주비용) == 0 ? 0 : Decimal.Round(((비용포함이윤 / (원화수주금액 + 수주비용)) * 100), 2, MidpointRounding.AwayFromZero)));
                        }
                        #endregion
                        break;
                    case 진행단계.납품목록:
                        #region 납품목록
                        if (this._납품목록H.DataTable == null)
                        {
                            this._납품목록H.DetailGrids = new FlexGrid[] { this._납품목록L };

                            this.SetHeaderGrid(this._납품목록H);
                            this.SetLineGrid(this._납품목록L);
                        }

                        this._납품목록H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.장기미출고:
                        #region 장기미출고
                        if (this._장기미출고H.DataTable == null)
                            this.SetHeaderGrid(this._장기미출고H);

                        this._장기미출고H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.협조전작성:
                        #region 협조전작성
                        if (this._협조전작성H.DataTable == null)
                        {
                            this.SetHeaderGrid(this._협조전작성H);
                            this.SetLineGrid(this._협조전작성L);

                            this._협조전작성H.DetailGrids = new FlexGrid[] { this._협조전작성L };
                        }

                        this._협조전작성H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.협조전완결:
                        #region 협조전완결
                        if (this._협조전완결H.DataTable == null)
                            this.SetHeaderGrid(this._협조전완결H);

                        this._협조전완결H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.납품완결:
                        #region 납품완결
                        if (this._납품완결H.DataTable == null)
                            this.SetHeaderGrid(this._납품완결H);

                        this._납품완결H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.인수증등록:
                        #region 인수증등록
                        if (this._인수증등록H.DataTable == null)
                            this.SetHeaderGrid(this._인수증등록H);

                        this._인수증등록H.Binding = dt;
                        #endregion
                        break;
                    case 진행단계.계산서발행:
                        #region 계산서발행
                        if (this._계산서발행H.DataTable == null)
						{
							this._계산서발행H.DetailGrids = new FlexGrid[] { this._계산서발행L };

							this.SetHeaderGrid(this._계산서발행H);
							this.SetLineGrid(this._계산서발행L);
						}
                        
                        this._계산서발행H.Binding = dt;

						switch (this.cbo필터.SelectedValue)
						{
							case "":
								this._계산서발행H.RowFilter = string.Empty;
								break;
							case "001":
								#region 미발행
								this._계산서발행H.RowFilter = "ISNULL(CD_FILE, '') <> '' AND ISNULL(DT_LOADING, '') <> '' AND ((CD_COMPANY <> 'K100' AND CD_COMPANY <> 'K200') OR CD_AREA <> '100' OR ISNULL(YN_BILL, 'N') = 'Y')";
								#endregion
								break;
							case "002":
								#region 미등록
								this._계산서발행H.RowFilter = "ISNULL(CD_FILE, '') = '' OR ISNULL(DT_LOADING, '') = '' OR ((CD_COMPANY = 'K100' OR CD_COMPANY = 'K200') AND CD_AREA = '100' AND ISNULL(YN_BILL, 'N') = 'N')";
								#endregion
								break;
						}
						#endregion
						break;
                    case 진행단계.클레임종결:
                        #region 클레임종결
                        if (this._클레임종결H.DataTable == null)
                            this.SetHeaderGrid(this._클레임종결H);

                        this._클레임종결H.Binding = dt;
                        #endregion
                        break;
					case 진행단계.인수증확인:
						#region 인수증확인
						if (this._인수증확인H.DataTable == null)
						{
							this._인수증확인H.DetailGrids = new FlexGrid[] { this._인수증확인L };

							this.SetHeaderGrid(this._인수증확인H);
							this.SetLineGrid(this._인수증확인L);
						}
						
						this._인수증확인H.Binding = dt;
						#endregion
						break;
                    case 진행단계.계산서확인:
                        #region 계산서확인
                        if (this._계산서확인H.DataTable == null)
                            this.SetHeaderGrid(this._계산서확인H);

                        this._계산서확인H.Binding = dt;
                        #endregion
                        break;
				}

				//if (dt.Rows.Count == 0)
				//    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);

				return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return 0;
        }

        private void SetHeaderGrid(FlexGrid grid)
        {
            SumPositionEnum sumEnum;

            try
            {
                sumEnum = SumPositionEnum.None;

                if (grid.Name == this._수발주통보서H.Name || grid.Name == this._수발주통보서결재H.Name)
                    grid.BeginSetting(2, 1, false);
                else
                    grid.BeginSetting(1, 1, false);

                grid.SetCol("CD_COMPANY", "회사코드", false);

                if (grid.Name == this._고객문의등록H.Name)
                {
                    #region 고객문의등록
                    grid.SetCol("YN_PARSING", "파싱", 45);
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("DTS_INSERT", "등록일자", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_TYPIST", "입력지원", 80);

                    grid.SetCol("DC_RMK", "비고", 150, this.수정가능여부);
                    grid.ExtendLastCol = true;

                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._매입문의등록H.Name)
                {
                    #region 매입문의등록
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("YN_PARSING", "파싱", 45);
                    grid.SetCol("TP_STATE", "상태", 60);
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("DTS_INSERT", "등록일자", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_TYPIST", "입력지원", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);

                    grid.SetCol("ST_RPA", "RPA 실행상태", 100);
                    grid.SetCol("DC_RPA", "RPA 로그", 100);

                    grid.SetCol("DC_RMK", "비고", 150, false);

                    grid.ExtendLastCol = true;

                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
                    grid.SetDataMap("TP_STATE", Global.MainFrame.GetComboDataCombine("N;CZ_MA00006"), "CODE", "NAME");

                    grid.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);

                    grid.Styles.Add("미강조").ForeColor = Color.Black;
                    grid.Styles.Add("미강조").BackColor = Color.White;
                    grid.Styles.Add("강조").ForeColor = Color.Red;
                    grid.Styles.Add("강조").BackColor = Color.White;
                    #endregion
                }
                else if (grid.Name == this._문의서검토H.Name)
                {
                    #region 문의서검토
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("YN_SEND", "발송여부", 60, false, CheckTypeEnum.Y_N);

                    if (this.LoginInfo.CompanyCode == "K100")
                        grid.SetCol("TP_CHECK", "재고견적", 60);

                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 120);
                    grid.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_TYPIST", "입력지원", 80);
                    grid.SetCol("NM_ITEMGRP", "아이템유형", 100);

                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._매입가등록H.Name)
                {
                    #region 매입가등록
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("YN_SRM", "자동등록", 40, false, CheckTypeEnum.Y_N);
                    grid.SetCol("YN_SO", "수주여부", 40, false, CheckTypeEnum.Y_N);
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("DTS_INSERT", "등록일자", 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_TYPIST", "입력지원", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);
                    grid.SetCol("DT_QUTATION", "최초견적일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._견적작성H.Name)
                {
                    #region 견적작성
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("TP_STATE", "상태", 60);
                    
                    if (this.LoginInfo.CompanyCode == "K100")
                        grid.SetCol("TP_CHECK", "재고견적", 60);
                    
                    grid.SetCol("TP_SRM", "SRM", 60);
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NO_REF", "문의번호", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);
                    grid.SetCol("NM_PARTNER_SP", "싱가폴매출처", false);

                    grid.SetCol("NM_SUPPLIER", "매입처", 150);
                    
                    grid.SetCol("DT_INSERT", "매입문의", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DTS_ELAPSE", "경과시간", 100);
                    grid.SetCol("DT_QUTATION", "최초견적일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("AM_PUR_KR", "구매금액(￦)", 80, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("QT_QTN", "종수", 40, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_SUPPLIER", "매입문의처", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_QUOTED", "견적접수처", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_TYPIST", "입력지원", 80);
                    grid.SetCol("NM_EMP_QTN", "견적담당", 80);

                    grid.SetCol("DC_RMK", "비고", 150, this.수정가능여부);
                    grid.ExtendLastCol = true;

                    grid.SetDataMap("TP_STATE", Global.MainFrame.GetComboDataCombine("N;CZ_MA00015"), "CODE", "NAME");

                    grid.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);

                    grid.Styles.Add("미전달").ForeColor = Color.Black;
                    grid.Styles.Add("미전달").BackColor = Color.White;
                    grid.Styles.Add("전달").ForeColor = Color.Red;
                    grid.Styles.Add("전달").BackColor = Color.White;

                    sumEnum = SumPositionEnum.Top;
                    #endregion
                }
                else if (grid.Name == this._재고견적H.Name)
                {
                    #region 재고견적
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("TP_STATE", "상태", 60);
                    grid.SetCol("TP_SRM", "SRM", 60);
                    grid.SetCol("YN_FILE", "매입견적", 40, false, CheckTypeEnum.Y_N);
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NO_REF", "문의번호", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);
                    grid.SetCol("NM_SUPPLIER", "매입처", 150);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_TYPIST", "입력지원", 80);
                    grid.SetCol("NM_PUR", "견적담당", 80);
                    grid.SetCol("DT_INSERT", "매입문의", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DTS_ELAPSE", "경과시간", 100);
                    grid.SetCol("DTS_ELAPSE1", "경과시간1", false);
                    grid.SetCol("DT_QUTATION", "최초견적일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("AM_PUR_KR", "구매금액(￦)", 80, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("QT_QTN", "종수", 40, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_SUPPLIER", "매입문의처", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_QUOTED", "견적접수처", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("AM_PO", "매입금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_SO", "매출금액(원화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_PROFIT", "이윤(원화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("RT_PROFIT", "이윤율", 80, false, typeof(decimal), FormatTpType.RATE);
                    grid.SetCol("NM_SUPPLIER1", "요청매입처", 100);
                    grid.SetCol("DT_SEND_MAIL", "발송일(메일)", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DC_RMK", "비고", 150, this.수정가능여부);
                    
                    grid.Cols["DT_SEND_MAIL"].Format = "####/##/## ##:##:##";
                    grid.SetDataMap("TP_STATE", Global.MainFrame.GetComboDataCombine("N;CZ_MA00015"), "CODE", "NAME");
                    grid.ExtendLastCol = true;

                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    grid.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
                    grid.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);

                    grid.Styles.Add("영업").ForeColor = Color.Black;
                    grid.Styles.Add("영업").BackColor = Color.White;
                    grid.Styles.Add("기획실").ForeColor = Color.Black;
                    grid.Styles.Add("기획실").BackColor = Color.Yellow;
                    #endregion
                }
                else if (grid.Name == this._고객문의클로징H.Name)
                {
                    #region 고객문의클로징
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("TP_STATE", "상태", 60);
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);
                    grid.SetCol("NM_SUPPLIER", "매입처", 150);
                    grid.SetCol("NM_USER", "담당자", 80);
                    grid.SetCol("DT_INQ", "문의일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_CLOSE", "클로징일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("TP_CLOSE", "사유", 150, false);

                    grid.SetCol("DC_CLOSE", "비고", 150, false);
                    grid.ExtendLastCol = true;

                    grid.SetDataMap("TP_STATE", Global.MainFrame.GetComboDataCombine("N;CZ_MA00015"), "CODE", "NAME");
                    grid.SetDataMap("TP_CLOSE", Global.MainFrame.GetComboDataCombine("N;CZ_SA00022"), "CODE", "NAME");
                    
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._견적제출H.Name)
                {
                    #region 견적제출
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NO_REF", "문의번호", 80);
                    grid.SetCol("NM_FILE", "첨부파일명", 200);
                    grid.SetCol("DTS_INSERT", "전달일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_EMP_QTN", "견적담당", 80);
                    grid.SetCol("NM_TYPIST", "입력지원", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);
                    grid.SetCol("NM_EXCH", "통화명", 80);
                    grid.SetCol("NM_COND_PAY", "지불조건", 120);
                    grid.SetCol("SUBMISSION_METHOD", "제출방법", 150);
                    grid.SetCol("YN_ATTACH", "견적발송용", 40, false, CheckTypeEnum.Y_N);

                    grid.SetCol("ST_RPA", "RPA 실행상태", 100);
                    grid.SetCol("DC_RPA", "RPA 로그", 100);

                    grid.SetCol("DC_RMK", "비고", 150);
                    grid.SetCol("DC_RMK1", "제출비고", 150);

                    grid.ExtendLastCol = true;

                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);

                    grid.Styles.Add("일반").ForeColor = Color.Black;
                    grid.Styles.Add("일반").BackColor = Color.White;
                    grid.Styles.Add("주의").ForeColor = Color.Red;
                    grid.Styles.Add("주의").BackColor = Color.White;
                    #endregion
                }
                else if (grid.Name == this._수주등록H.Name || grid.Name == this._수주확인서H.Name || grid.Name == this._매입구매H.Name)
                {
                    #region 수주등록, 수주확인서, 매입구매
                    if ((grid.Name == this._수주등록H.Name || grid.Name == this._매입구매H.Name) && this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    if (grid.Name == this._수주등록H.Name)
                        grid.SetCol("YN_REG", "등록여부", 40, false, CheckTypeEnum.Y_N);

                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 120);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_PUR", "구매담당", 80);
                    grid.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

                    if (grid.Name == this._매입구매H.Name)
                    {
                        grid.SetCol("QT_SO", "수주수량", 80);
                        grid.SetCol("QT_PO", "발주수량", 80);
                    }

                    grid.SetCol("DC_RMK", "비고", 150, this.수정가능여부);
                    grid.ExtendLastCol = true;

                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._수발주통보서H.Name || grid.Name == this._수발주통보서결재H.Name)
                {
                    #region 수발주통보서, 수발주통보서결재
                    if (grid.Name == this._수발주통보서결재H.Name && this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    if (grid.Name == this._수발주통보서결재H.Name)
                    {
                        grid.SetCol("YN_DONE", "결재상태", 80);
                        grid.SetCol("YN_RE_APPROVAL", "재상신", 60, false, CheckTypeEnum.Y_N);
                        grid.SetCol("NM_SO", "수주유형", 80);
                    }
                    
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NM_FILE", "첨부파일명", 200);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);
                    grid.SetCol("DT_CONTRACT", "발행일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_PUR", "구매담당", 80);

                    if (grid.Name == this._수발주통보서결재H.Name)
                    {
                        grid.SetCol("ID_FIRST_APPROVAL", "1차결재", false);
                        grid.SetCol("ID_SECOND_APPROVAL", "2차결재", false);
                        grid.SetCol("NM_FIRST_APPROVAL", "1차결재", 80);
                        grid.SetCol("NM_SECOND_APPROVAL", "2차결재", 80);
                    }

                    grid.SetCol("AM_SO", "수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_PO", "매입금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_SO_CHARGE", "매출비용", 80, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("AM_PO_CHARGE", "매입비용", 80, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("PROFIT", "이윤", 80, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("RT_PROFIT", "이윤율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
                    grid.SetCol("PROFIT_ALL", "이윤\n(비용포함)", 80, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("RT_PROFIT_ALL", "이윤율\n(비용포함)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

                    if (grid.Name == this._수발주통보서H.Name)
                        grid.SetCol("DC_RMK", "비고", 150, this.수정가능여부);
                    else
                    {
                        grid.SetCol("DC_RMK1", "커미션", 150);
                        grid.SetCol("DC_RMK_CONTRACT", "수통비고", 150);
                        grid.SetCol("DC_RMK_LINE", "수통비고(품목)", 150);
                        grid.SetCol("DC_COMMENT_FIRST", "결재비고\n(1차결재)", 150, this.수정가능여부);
                        grid.SetCol("DC_COMMENT_SECOND", "결재비고\n(2차결재)", 150, this.수정가능여부);
                    }

                    sumEnum = SumPositionEnum.Top;

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);

                    if (grid.Name == this._수발주통보서결재H.Name)
                    {
                        grid.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
                        grid.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
                        grid.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);

                        grid.Styles.Add("일반").ForeColor = Color.Black;
                        grid.Styles.Add("일반").BackColor = Color.White;
                        grid.Styles.Add("마이너스").ForeColor = Color.Red;
                        grid.Styles.Add("마이너스").BackColor = Color.White;
                    }
                    #endregion
                }
                else if (grid.Name == this._납품목록H.Name)
                {
                    #region 납품목록
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 150);
                    grid.SetCol("NM_VESSEL", "호선", 150);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_LOG", "물류담당", 80);

                    grid.SetCol("DC_RMK", "비고", 150, this.수정가능여부);
                    grid.ExtendLastCol = true;

                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._장기미출고H.Name)
                {
                    #region 장기미출고
                    grid.SetCol("NO_KEY", "수주번호", 80);
                    grid.SetCol("LN_PARTNER", "매출처명", 120);
                    grid.SetCol("NM_VESSEL", "호선명", 120);
                    grid.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_IN", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_REMAIN", "미출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("NM_SALES", "수주담당", 80);
                    grid.SetCol("NM_LOG", "물류담당", 80);
                    grid.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_DELAY", "지연일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("AM_SO", "수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_REMAIN", "미출고금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_RCP_A", "선수금", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                    grid.SetCol("DC_RMK_TEXT", "수주비고", 100);
                    grid.SetCol("DC_RMK_TEXT2", "물류비고", 100);
                    grid.SetCol("CD_RMK", "미출고비고", 100, this.수정가능여부);
                    grid.SetCol("DC_RMK", "미출고상세비고", 100, this.수정가능여부);

                    grid.SetDataMap("CD_RMK", this._biz.워크플로우비고(진행단계.장기미출고), "CD_SYSDEF", "NM_SYSDEF");

                    grid.ExtendLastCol = true;

                    sumEnum = SumPositionEnum.Top;
                    #endregion
                }
                else if (grid.Name == this._협조전작성H.Name)
                {
                    #region 협조전작성
                    grid.SetCol("NO_KEY", "파일번호", 80);
                    grid.SetCol("NO_PO_PARTNER", "매출처발주번호", 120);
                    grid.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("LN_PARTNER", "매출처명", 120);
                    grid.SetCol("NM_VESSEL", "호선명", 120);
                    grid.SetCol("WEIGHT", "무게", 40, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_PO", "발주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_STOCK", "재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_IN", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("NM_SALES", "영업담당", 80);
                    grid.SetCol("NM_LOG", "물류담당", 80);
                    grid.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

                    grid.SetCol("DC_RMK_TEXT", "수주비고", 100);
                    grid.SetCol("DC_RMK_TEXT2", "물류비고", 100);
                    grid.ExtendLastCol = true;

                    grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);

                    sumEnum = SumPositionEnum.Top;
                    #endregion
                }
                else if (grid.Name == this._협조전완결H.Name || grid.Name == this._인수증등록H.Name)
                {
                    #region 협조전완결, 인수증등록, 인수증확인
                    grid.SetCol("NO_KEY", "의뢰번호", 100);

                    if (grid.Name == this._인수증등록H.Name)
                    {
                        grid.SetCol("NO_IO", "출고번호", 100);
                        grid.SetCol("NO_SO_STATUS", "출고상태", 100);
                        grid.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
                    }
                    
                    grid.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_COMPLETE", "완료예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    
                    if (grid.Name == this._인수증등록H.Name)
                    {
                        grid.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        grid.SetCol("DT_LOADING", "선적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        grid.SetCol("YN_FILE", "인수증등록여부", 40, false, CheckTypeEnum.Y_N);
                    }

                    grid.SetCol("NM_TYPE1", "의뢰구분(대)", 90);
                    grid.SetCol("NM_TYPE2", "의뢰구분(중)", 90);
                    grid.SetCol("NM_TYPE3", "의뢰구분(소)", 90);
                    grid.SetCol("YN_PACKING", "포장여부", 60, false, CheckTypeEnum.Y_N);
                    grid.SetCol("NM_STA_GIR", "진행상태", 100);
                    grid.SetCol("NM_EMP_GIR", "의뢰자", 60);
                    grid.SetCol("NM_PARTNER", "매출처", 140);
                    grid.SetCol("NM_VESSEL", "호선명", 140);
                    grid.SetCol("NM_GIR_PARTNER", "납품처명", 140); ;
                    grid.SetCol("NM_RETURN", "출고구분", 80);
                    grid.SetCol("NM_COMPANY", "회사", 100);

                    grid.SetCol("DC_RMK", "비고", 180);
                    if (grid.Name == this._인수증등록H.Name)
                       grid.SetCol("DC_RMK2", "매출비고", 100, this.수정가능여부);

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._납품완결H.Name)
                {
                    #region 납품완결
                    if (this.수정가능여부 == true && this.btn완료.Visible == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("NO_KEY", "의뢰번호", 100);
                    grid.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_KOR", "의뢰자", 60);
                    grid.SetCol("NM_PARTNER", "매출처", 100);
                    grid.SetCol("NM_VESSEL", "호선", 100);
                    grid.SetCol("NM_PACK", "포장명", 100, true, typeof(decimal));
                    grid.SetCol("DT_PACK", "포장일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_TYPE", "포장유형", 100);
                    grid.SetCol("NM_SIZE", "포장사이즈", 150);
                    grid.SetCol("QT_NET_WEIGHT", "순중량", 60);
                    grid.SetCol("QT_GROSS_WEIGHT", "총중량", 60);
                    grid.SetCol("QT_WIDTH", "가로", 60);
                    grid.SetCol("QT_LENGTH", "세로", 60);
                    grid.SetCol("QT_HEIGHT", "높이", 60);
                    grid.SetCol("CD_LOCATION", "포장위치", 100);

                    grid.SetCol("DC_RMK", "비고", 100);
                    grid.ExtendLastCol = true;
                    #endregion
                }
                else if (grid.Name == this._계산서발행H.Name)
                {
					#region 계산서발행
					if (this.수정가능여부 == true)
					{
						grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
						grid.SetDummyColumn("S");
					}

					grid.SetCol("NO_KEY", "출고번호", 80);
                    grid.SetCol("NO_GIR", "협조전번호", 80);
					grid.SetCol("NO_SO_STATUS", "출고상태", 80, true);
					grid.SetCol("NO_PO_PARTNER", "매출처발주번호", 100, true);
                    grid.SetCol("LN_PARTNER", "매출처명", 120);
                    grid.SetCol("NM_VESSEL", "호선명", 120);
                    grid.SetCol("NM_SALES_EMP", "영업담당", 80);
                    grid.SetCol("NM_LOG_EMP", "물류담당", 80);
                    grid.SetCol("NM_MAIN_CATEGORY", "의뢰구분(중)", 100);
                    grid.SetCol("NM_SUB_CATEGORY", "의뢰구분(소)", 100);
                    grid.SetCol("QT_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("AM_OUT", "출고금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("AM_REMAIN", "잔액", 100, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("YN_PROFORMA", "선수금여부", 100, false, CheckTypeEnum.Y_N);
                    grid.SetCol("AM_RCP_A", "선수금", 100, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("DT_START", "작업시작일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_LOADING", "선적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("CD_FILE", "인수증", 80);
					grid.SetCol("YN_BILL", "인수증확인여부", 100, false, CheckTypeEnum.Y_N);
                    grid.SetCol("YN_AUTO", "자동처리대상", 100);
                    grid.SetCol("DT_BILL", "청구예정일", 80, this.수정가능여부, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("YN_STATUS1", "선적일자확인제외", 120, true, CheckTypeEnum.Y_N);

                    grid.SetCol("DC_RMK", "비고", 100);
                    grid.SetCol("DC_RMK2", "매출비고", 100, this.수정가능여부);
                    grid.ExtendLastCol = true;

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
					grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
					grid.AfterEdit += this._flex_AfterEdit;

					sumEnum = SumPositionEnum.Top;
                    #endregion
                }
                else if (grid.Name == this._클레임종결H.Name)
                {
                    #region 클레임종결
                    grid.SetCol("NO_KEY", "클레임번호", 80);
                    grid.SetCol("NO_SO", "수주번호", 80);
                    grid.SetCol("NM_EMP", "담당자", 80);
                    grid.SetCol("DT_INPUT", "발행일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_STATUS", "상태", 80);
                    grid.SetCol("NM_GW_STATUS", "결재상태", 80);
                    grid.SetCol("NM_PARTNER", "매출처", 120);
                    grid.SetCol("NM_VESSEL", "호선명", 120);
                    grid.SetCol("NM_CLAIM", "클레임사유", 80);
                    grid.SetCol("NM_CAUSE", "원인구분", 80);
                    grid.SetCol("NM_ITEM", "항목분류", 80);
                    grid.SetCol("QT_PROGRESS", "경과개월", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("AM_CLAIM", "대상금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                    grid.SetCol("DC_RMK", "비고", 150, this.수정가능여부);
                    grid.ExtendLastCol = true;

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
				else if (grid.Name == this._인수증확인H.Name)
				{
					#region 인수증확인
					if (this.수정가능여부 == true)
					{
						grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
						grid.SetDummyColumn("S");
					}

					grid.SetCol("NO_IO", "출고번호", 100);
					grid.SetCol("NO_KEY", "의뢰번호", 100);
					grid.SetCol("NM_EMP_SALES", "영업담당자", 60);
					grid.SetCol("NM_EMP_GIR", "물류담당자", 60);
					grid.SetCol("NM_PARTNER", "매출처", 140);
					grid.SetCol("NM_VESSEL", "호선명", 140);
					grid.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
					grid.SetCol("DT_LOADING", "선적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
					grid.SetCol("DT_BILL", "청구예정일", 80, this.수정가능여부, typeof(string), FormatTpType.YEAR_MONTH_DAY);
					grid.SetCol("NM_TYPE1", "의뢰구분(대)", 90);
					grid.SetCol("NM_TYPE2", "의뢰구분(중)", 90);
					grid.SetCol("NM_TYPE3", "의뢰구분(소)", 90);
					grid.SetCol("NO_SO_STATUS", "출고상태", 80, true);
					grid.SetCol("NO_PO_PARTNER", "매출처발주번호", 100, true);
					grid.SetCol("AM_OUT", "출고금액", 100, false, typeof(decimal), FormatTpType.MONEY);
					grid.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.MONEY);
					grid.SetCol("AM_REMAIN", "잔액", 100, false, typeof(decimal), FormatTpType.MONEY);

					if (this.수정가능여부 == true)
						grid.SetCol("CD_FILE", "인수증", 100);

					grid.SetCol("DC_RMK2", "매출비고", 100, this.수정가능여부);
					grid.ExtendLastCol = true;

					grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
					grid.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);

					sumEnum = SumPositionEnum.Top;
					#endregion
				}
                else if (grid.Name == this._계산서확인H.Name)
				{
                    #region 계산서확인
                    if (this.수정가능여부 == true)
                    {
                        grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                        grid.SetDummyColumn("S");
                    }

                    grid.SetCol("YN_RETURN", "회신여부", 40, false, CheckTypeEnum.Y_N);
                    grid.SetCol("NO_KEY", "순번", 60);
                    grid.SetCol("DT_ELAPSE", "경과시간", 100);
                    grid.SetCol("NO_IO", "입고번호", 100);
                    grid.SetCol("NO_PO", "발주번호", 100);
                    grid.SetCol("NO_ETAX", "계산서번호", 100);
                    grid.SetCol("LN_PARTNER", "매입처", 100);
                    grid.SetCol("DT_SEND", "발행일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_END", "마감일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("QT_END", "초과일수", 100);
                    grid.SetCol("NO_EMP", "확인담당자", false);
                    grid.SetCol("NM_EMP", "확인담당자", 100);
                    grid.SetCol("NO_EMP_TEAM", "팀장", false);
                    grid.SetCol("NM_EMP_TEAM", "팀장명", 100);
                    grid.SetCol("NM_INSERT", "정산담당자", 100);
                    
                    grid.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid3 });

                    grid.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
                    grid.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
                    #endregion
                }

				grid.SettingVersion = "0.0.0.4";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, sumEnum);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void SetLineGrid(FlexGrid grid)
        {
            SumPositionEnum sumEnum;

            try
            {
                sumEnum = SumPositionEnum.None;
                grid.BeginSetting(1, 1, false);

                if (grid.Name == this._고객문의등록L.Name || grid.Name == this._매입문의등록L.Name)
                {
                    #region 고객문의등록, 매입문의등록
                    grid.SetCol("YN_PARSING", "파싱", 45, false, CheckTypeEnum.Y_N);
                    grid.SetCol("NM_FILE", "파일명", 200);
                    grid.SetCol("NM_INSERT", "등록자", 80);
                    grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

                    grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._문의서검토L1.Name || grid.Name == this._문의서검토L2.Name)
                {
                    #region 문의서검토
                    if (grid.Name == this._문의서검토L2.Name)
                    {
                        grid.SetCol("NM_SUPPLIER", "매입처", 120);
                        grid.Cols["NM_SUPPLIER"].AllowMerging = true;
                    }

                    grid.SetCol("NM_FILE", "파일명", 200);
                    grid.SetCol("NM_INSERT", "등록자", 80);
                    grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_SEND", "발송일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DC_RMK", "비고", 150);

                    grid.ExtendLastCol = true;

                    grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
                    grid.Cols["DT_SEND"].TextAlign = TextAlignEnum.CenterCenter;
                    grid.Cols["DT_SEND"].Format = "####/##/##/##:##:##";

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._매입가등록L1.Name || grid.Name == this._매입가등록L2.Name)
                {
                    #region 매입가등록
                    if (grid.Name == this._매입가등록L1.Name)
                    {
                        grid.SetCol("YN_PARSING", "파싱가능", 40, false, CheckTypeEnum.Y_N);
                        grid.SetCol("TP_STATE", "상태", 60);
                        grid.SetCol("NM_SUPPLIER", "매입처", 120);
                        grid.SetCol("NM_FILE", "파일명", 200);
                        grid.SetCol("NM_INSERT", "등록자", 80);
                        grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        grid.SetCol("DC_RMK", "비고", 150);

                        grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                        grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                        grid.ExtendLastCol = true;

                        grid.AllowMerging = AllowMergingEnum.Free;
                        grid.Cols["NM_SUPPLIER"].AllowMerging = true;
                        grid.Cols["DC_RMK"].AllowMerging = true;

                        grid.SetDataMap("TP_STATE", Global.MainFrame.GetComboDataCombine("N;CZ_MA00006"), "CODE", "NAME");
                    }
                    else
                    {
                        grid.SetCol("NM_FILE", "파일명", 200);
                        grid.SetCol("NM_INSERT", "등록자", 80);
                        grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        grid.SetCol("DC_RMK", "비고", 150);

                        grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                        grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                        grid.ExtendLastCol = true;
                    }

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._견적작성L.Name)
                {
                    #region 견적작성
                    grid.SetCol("TP_STATE", "상태", 60, false);
                    grid.SetCol("SRM_STATUS", "SRM상태", 60, false);
                    grid.SetCol("NM_SUPPLIER", "매입처", 120);
                    grid.SetCol("AM_KR", "매입가", 100, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("DT_PU_REG", "매입등록", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("NM_FILE", "파일명", 200);
                    grid.SetCol("NM_INSERT", "등록자", 80);
                    grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DC_RMK_QTN", "비고", 150);

                    grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.ExtendLastCol = true;

                    grid.AllowMerging = AllowMergingEnum.Free;
                    grid.Cols["NM_SUPPLIER"].AllowMerging = true;
                    grid.Cols["AM_KR"].AllowMerging = true;
                    grid.Cols["TP_STATE"].AllowMerging = true;
                    grid.Cols["DC_RMK_QTN"].AllowMerging = true;

                    grid.SetDataMap("TP_STATE", Global.MainFrame.GetComboDataCombine("N;CZ_MA00006"), "CODE", "NAME");

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._재고견적L.Name)
                {
                    #region 재고견적
                    grid.SetCol("NO_DSP", "순번", 100);
                    grid.SetCol("NM_ITEMGRP", "유형", 100);
                    grid.SetCol("NM_SUBJECT", "주제", 100);
                    grid.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
                    grid.SetCol("NM_ITEM_PARTNER", "매출처품명", 100);
                    grid.SetCol("CD_ITEM", "재고코드", 100);
                    grid.SetCol("UNIT_QTN", "단위", 100);
                    grid.SetCol("QT_QTN", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("NM_SUPPLIER", "매입처", 100);
                    grid.SetCol("UM_EX_P", "매입단가(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("UM_KR_P", "매입단가(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_EX_P", "매입금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_KR_P", "매입금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("RT_PROFIT", "이윤(%)", 100, false, typeof(decimal), FormatTpType.RATE);
                    grid.SetCol("UM_EX_Q", "매출단가(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("UM_KR_Q", "매출단가(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_EX_Q", "매출금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_KR_Q", "매출금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("RT_DC", "DC(%)", 100, false, typeof(decimal), FormatTpType.RATE);
                    grid.SetCol("UM_EX_S", "최종단가(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("UM_KR_S", "최종단가(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_EX_S", "최종금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("AM_KR_S", "최종금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    grid.SetCol("RT_MARGIN", "최종이윤(%)", 100, false, typeof(decimal), FormatTpType.RATE);
                    grid.SetCol("LT", "납기", 100);
                    grid.SetCol("DC_RMK", "비고", 100);

                    sumEnum = SumPositionEnum.Top;
                    #endregion
                }
                else if (grid.Name == this._수주등록L1.Name ||
                         grid.Name == this._수주등록L2.Name ||
                         grid.Name == this._수주확인서L1.Name ||
                         grid.Name == this._수주확인서L2.Name ||
                         grid.Name == this._매입구매L1.Name ||
                         grid.Name == this._매입구매L2.Name)
                {
                    #region 수주등록, 수주확인서, 매입구매
                    grid.SetCol("NM_FILE", "파일명", 200);
                    grid.SetCol("NM_INSERT", "등록자", 80);
                    grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DC_RMK", "비고", 150);

                    grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.ExtendLastCol = true;

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._수주등록L3.Name || grid.Name == this._매입구매L3.Name)
                {
                    #region 수주등록, 매입구매
                    grid.SetCol("NM_SUPPLIER", "매입처", 120);
                    grid.SetCol("AM_KR", "매입가", 100, false, typeof(decimal), FormatTpType.MONEY);
                    grid.SetCol("NM_FILE", "매입견적서", 200);
                    grid.SetCol("NM_INSERT", "등록자", 80);
                    grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DC_RMK_QTN", "비고", 150);

                    grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.ExtendLastCol = true;

                    grid.AllowMerging = AllowMergingEnum.Free;
                    grid.Cols["NM_SUPPLIER"].AllowMerging = true;
                    grid.Cols["AM_KR"].AllowMerging = true;
                    grid.Cols["DC_RMK_QTN"].AllowMerging = true;

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._납품목록L.Name)
                {
                    #region 납품목록
                    grid.SetCol("NM_FILE", "파일명", 200);
                    grid.SetCol("NM_INSERT", "등록자", 80);
                    grid.SetCol("DTS_INSERT", "업로드일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DC_RMK", "비고", 150);

                    grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
                    grid.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

                    grid.ExtendLastCol = true;

                    grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    #endregion
                }
                else if (grid.Name == this._협조전작성L.Name)
                {
                    #region 협조전작성
                    grid.SetCol("NO_KEY", "수주번호", false);
                    grid.SetCol("NO_DSP", "순번", 60);
                    grid.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
                    grid.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
                    grid.SetCol("CD_ITEM", "재고코드", 100);
                    grid.SetCol("NM_SUPPLIER", "매입처", 120);
                    grid.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_REQGI", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    grid.SetCol("STND_ITEM", "규격", false);
                    grid.SetCol("UNIT_SO", "단위", 60);
                    grid.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_PO", "발주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_STOCK", "재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_IN", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                    grid.SetCol("STA_SO", "수주상태", 60);

                    grid.SetDataMap("STA_SO", Global.MainFrame.GetComboDataCombine("N;SA_B000016"), "CODE", "NAME");
                    sumEnum = SumPositionEnum.Top;
                    #endregion
                }
				else if (grid.Name == this._인수증확인L.Name)
				{
					grid.SetCol("NM_FILE", "파일명", 200);
					grid.SetCol("NM_INSERT", "등록자", 80);
					grid.SetCol("DTS_INSERT", "업로드일자", 90);

					grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;

					grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
				}
				else if (grid.Name == this._계산서발행L.Name)
				{
                    grid.SetCol("NM_FILE", "파일명", 200);
					grid.SetCol("NM_INSERT", "등록자", 80);
					grid.SetCol("DTS_INSERT", "업로드일자", 90);

                    if (Global.MainFrame.LoginInfo.GroupID == "ADM_SPO" ||
                        Global.MainFrame.LoginInfo.GroupID == "ADM" ||
                        Global.MainFrame.LoginInfo.GroupID == "ADMIN")
                        grid.SetCol("NO_REF", "참조번호", 100, true);
                    else
                        grid.SetCol("NO_REF", "참조번호", 100, false);

                    grid.AddDummyColumn("NO_REF");

					grid.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;

					grid.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
                    grid.AfterEdit += this._flex_AfterEdit;
				}

				grid.SettingVersion = "0.0.0.2";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, sumEnum);

                if (grid.Name == this._재고견적L.Name)
                    grid.SetExceptSumCol("RT_PROFIT", "RT_DC", "RT_MARGIN");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public bool SaveData(FlexGrid grid)
        {
            FlexGrid tmpGrid = null;

            try
            {
                if (grid == null)
                {
                    if (this.선택단계 == 진행단계.고객문의등록)
                        tmpGrid = this._고객문의등록H;
                    else if (this.선택단계 == 진행단계.견적작성)
                        tmpGrid = this._견적작성H;
                    else if (this.선택단계 == 진행단계.수주등록)
                        tmpGrid = this._수주등록H;
                    else if (this.선택단계 == 진행단계.수주확인서)
                        tmpGrid = this._수주확인서H;
                    else if (this.선택단계 == 진행단계.매입구매)
                        tmpGrid = this._매입구매H;
                    else if (this.선택단계 == 진행단계.수발주통보서)
                        tmpGrid = this._수발주통보서H;
                    else if (this.선택단계 == 진행단계.수발주통보서결재)
                        tmpGrid = this._수발주통보서결재H;
                    else if (this.선택단계 == 진행단계.납품목록)
                        tmpGrid = this._납품목록H;
                    else if (this.선택단계 == 진행단계.장기미출고)
                        tmpGrid = this._장기미출고H;
                    else if (this.선택단계 == 진행단계.인수증등록)
                        tmpGrid = this._인수증등록H;
                    else if (this.선택단계 == 진행단계.계산서발행)
                        tmpGrid = this._계산서발행H;
                    else if (this.선택단계 == 진행단계.클레임종결)
                        tmpGrid = this._클레임종결H;
                    else if (this.선택단계 == 진행단계.재고견적)
                        tmpGrid = this._재고견적H;
                    else if (this.선택단계 == 진행단계.인수증확인)
                        tmpGrid = this._인수증확인H;
                    else if (this.선택단계 == 진행단계.계산서확인)
                        tmpGrid = this._계산서확인H;
				}
                else
                {
                    tmpGrid = grid;
                }

                if (tmpGrid != null)
                {
                    foreach (DataRow dr in tmpGrid.GetChanges().Rows)
                    {
                        switch (this.선택단계)
                        {
                            case 진행단계.고객문의등록:
                                #region 고객문의등록
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.고객문의등록).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.매입문의등록).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.견적작성:
                                #region 견적작성
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.견적작성).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.견적제출).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.수주등록:
                            case 진행단계.수주확인서:
                            case 진행단계.매입구매:
                                #region 수주등록, 수주확인서, 매입구매
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.수주등록).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.수주확인서).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.매입구매).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.수발주통보서:
                                #region 수발주통보서
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.수발주통보서).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.수발주통보서결재:
                                #region 수발주통보서결재
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.수발주통보서결재).ToString("00"), D.GetString(dr["NO_KEY"]), D.GetString(dr["DC_COMMENT_FIRST"]), D.GetString(dr["DC_COMMENT_SECOND"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.납품목록:
                                #region 납품목록
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.납품목록).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.장기미출고:
                                #region 장기미출고
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.장기미출고).ToString("00"), D.GetString(dr["NO_KEY"]), D.GetString(dr["CD_RMK"]), D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.인수증등록:
                                #region 인수증등록
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.인수증등록).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK2"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.계산서발행:
                                #region 계산서발행
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.계산서발행).ToString("00"), D.GetString(dr["NO_GIR"]), string.Empty, D.GetString(dr["DC_RMK2"]), D.GetString(dr["DT_BILL"]));
                                #endregion
                                break;
                            case 진행단계.클레임종결:
                                #region 클레임종결
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.클레임종결).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
                            case 진행단계.재고견적:
                                #region 재고견적
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.재고견적).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK"]), string.Empty);
                                #endregion
                                break;
							case 진행단계.인수증확인:
								#region 인수증확인
								this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.인수증확인).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK2"]), D.GetString(dr["DT_BILL"]));
								#endregion
								break;
                            case 진행단계.계산서확인:
                                #region 계산서확인
                                this._biz.변경사항저장(this.선택회사코드, ((int)진행단계.계산서확인).ToString("00"), D.GetString(dr["NO_KEY"]), string.Empty, D.GetString(dr["DC_RMK_WF"]), string.Empty);
                                #endregion
                                break;
						}
                    }

                    DataTable dt = ComFunc.getGridGroupBy(tmpGrid.GetChanges(), new string[] { "CD_COMPANY", "NO_KEY" }, true);
                    dt.Columns["NO_KEY"].ColumnName = "NO_FILE";

                    DBMgr.ExecuteNonQuery("PX_CZ_SA_QTN_REG_IC_WF", dt);

                    tmpGrid.AcceptChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private void 임시파일제거()
        {
            DirectoryInfo dirInfo;
            bool isExistFile;

            try
            {
                dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, this.임시폴더));
                isExistFile = false;

                if (dirInfo.Exists == true)
                {
                    foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            isExistFile = true;
                            continue;
                        }
                    }

                    if (isExistFile == false)
                        dirInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion
    }
}