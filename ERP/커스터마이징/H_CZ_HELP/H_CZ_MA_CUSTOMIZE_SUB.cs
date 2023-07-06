using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using System;
using Dintec;

namespace cz
{
    [UserHelpDescription("CODE", "NAME", "CODE", "NAME")]
    public partial class H_CZ_MA_CUSTOMIZE_SUB : HelpBase, IHelp
    {
        string _helpId, _query, _회사;
        bool _isOracle;
        
        public H_CZ_MA_CUSTOMIZE_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_MA_CUSTOMIZE_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();

            base.SetIHelp = this as IHelp;

            this._helpId = helpParam.P11_ID_MENU;
            this._isOracle = (helpParam.P21_FG_MODULE == "Y" ? true : false);
            this.txt검색.Text = helpParam.P92_DETAIL_SEARCH_CODE.Trim();
            this._query = helpParam.P34_CD_MNG;
            this._회사 = helpParam.P01_CD_COMPANY;

            this.InitControl();
            this.InitGrid();

            base.SetDefault(base.Get타이틀명, this._flex, this.btn확인, this.btn검색, this.btn취소, this.btn멀티검색, this.txt검색); //페이지 컨트롤들을 해당페이지와 연결
            helpParam.QueryAction = QueryAction.RealTime; //조회할때마다 서버에서 가져옴
        }

        #region -> 초기화
        /// <summary>
        /// 그리드 초기화 
        /// </summary>
        private void InitGrid()
        {
            ArrayList list = new ArrayList();

            if (this._helpId == "CZ_SA_GIR_SUB" || 
                this._helpId == "CZ_SA_GIR_PACK_SUB")
            {
                list.Add(new object[] { "NO_SO", "수주번호", 100 });
                list.Add(new object[] { "DT_SO", "수주일자", 80 });
                list.Add(new object[] { "LN_PARTNER", "매출처명", 200 });
                list.Add(new object[] { "NM_VESSEL", "호선명", 100 });
            }
            else if (this._helpId == "H_SA_SO_SUB" || this._helpId == "H_SA_SO_GI_SUB")
            {
                list.Add(new object[] { "NO_SO", "수주번호", 100 });
                list.Add(new object[] { "DT_SO", "수주일자", 80 });
                list.Add(new object[] { "NM_SALEGRP", "영업그룹", 80 });
                list.Add(new object[] { "NM_KOR", "담당자", 80 });
            }
            else if (this._helpId == "H_PU_PO_SUB")
            {
                list.Add(new object[] { "NO_PO", "발주번호", 100 });
                list.Add(new object[] { "DT_PO", "발주일자", 80 });
                list.Add(new object[] { "LN_PARTNER", "매입처", 80 });
                list.Add(new object[] { "NM_PURGRP", "구매그룹", 80 });
                list.Add(new object[] { "NM_KOR", "담당자", 80 });
            }
            else if (this._helpId == "H_MA_PITEM_SUB")
            {
                list.Add(new object[] { "CD_ITEM", "품목코드", 100});
                list.Add(new object[] { "NM_ITEM", "품목명", 100 });
                list.Add(new object[] { "NM_CLS_ITEM", "계정구분", 100 });
                list.Add(new object[] { "STND_DETAIL_ITEM", "U코드", 100 });
                list.Add(new object[] { "STND_ITEM", "파트번호", 100 });
                list.Add(new object[] { "MAT_ITEM", "아이템번호", 100 });
                list.Add(new object[] { "NM_CLS_L", "대분류", 100 });
                list.Add(new object[] { "NM_CLS_M", "중분류", 100 });
                list.Add(new object[] { "NM_CLS_S", "소분류", 100 });
            }
            else if (this._helpId == "H_SA_MEETING_MEMO_SUB")
            {
                list.Add(new object[] { "NO_MEETING", "미팅번호", 100 });
                list.Add(new object[] { "DT_MEETING", "미팅일자", 100 });
                list.Add(new object[] { "NM_INSERT", "작성자", 100 });
                list.Add(new object[] { "LN_PARTNER", "거래처", 100 });
                list.Add(new object[] { "DC_SUBJECT", "주제", 100 });
            }
            else if (this._helpId == "H_MA_EMP_SUB")
            {
                list.Add(new object[] { "NM_COMPANY", "회사", 100 });
                list.Add(new object[] { "NO_EMP", "사번", 100 });
                list.Add(new object[] { "NM_KOR", "이름", 100 });
                list.Add(new object[] { "NM_DEPT", "부서", 100 });
                list.Add(new object[] { "NM_DUTY_RANK", "직위", 100 });
            }
            else
            {
                list.Add(new object[] { "CODE", "코드", 80 });
                list.Add(new object[] { "NAME", "이름", 80 });
            }

            _flex.Cols.Frozen = 1;
            base.InitGrid(_flex, list);  //그리드는 PageBase의 그리드와 다르다   

        }

        private void InitControl()
        {
            if (this._helpId == "H_SA_SO_SUB" || this._helpId == "H_SA_SO_GI_SUB")
            {
                this.lbl기간.Text = "수주일자";

                this.dtp기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
                this.dtp기간.EndDateToString = Global.MainFrame.GetStringToday;
            }
            else if (this._helpId == "H_PU_PO_SUB")
            {
                this.lbl기간.Text = "발주일자";

                this.dtp기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
                this.dtp기간.EndDateToString = Global.MainFrame.GetStringToday;
            }
            else if (this._helpId == "H_SA_MEETING_MEMO_SUB")
            {
                this.lbl기간.Text = "미팅일자";

                this.dtp기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
                this.dtp기간.EndDateToString = Global.MainFrame.GetStringToday;
            }
            else
            {
                this.oneGrid1.ItemCollection.Remove(this.oneGridItem2);
                this.oneGrid1.Controls.Remove(this.oneGridItem2);
            }
        }
        #endregion

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;
            string exeQuery;

            if (this._isOracle == true)
            {
                exeQuery = string.Format(this._query, 검색);
                dt = this.DirectQuery(this._회사, exeQuery);
            }
            else
            {
                if (!string.IsNullOrEmpty(this._query))
                {
                    exeQuery = string.Format(this._query, 검색);
                    dt = Global.MainFrame.FillDataTable(exeQuery);
                }
                else
                {
                    dt = DBHelper.GetDataTable("SP_H_CZ_MA_CUSTOMIZE_SUB", new object[] { this._회사,
                                                                                          this._helpId,
                                                                                          this.dtp기간.StartDateToString,
                                                                                          this.dtp기간.EndDateToString,
                                                                                          검색 });
                }
            }
            
            if (dt == null || dt.Rows.Count == 0)
            {
                Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                return dt;
            }

            return dt.DefaultView.ToTable();
        }

        public DataTable OpenQuery(string Query)
        {
            DataTable dt = null;

            try
            {
                return DBHelper.GetDataTable("SELECT * FROM OPENQUERY(DINTEC, '" + Query.Replace("'", "''") + "')");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        public DataTable DirectQuery(string 회사, string Query)
        {
            DataTable dt = null;
            DBMgr dbMgr;

            try
            {
                dbMgr = new DBMgr((DBConn)D.GetInt(회사));
                dbMgr.Query = Query;
                return dbMgr.GetDataTable();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        public string Get검색
        {
            get { return this.txt검색.Text; }
        }
    }
}