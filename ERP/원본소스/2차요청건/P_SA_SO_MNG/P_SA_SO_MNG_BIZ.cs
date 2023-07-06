using System;
using System.Collections.Generic;
using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System.Windows.Forms;

namespace sale
{
    class P_SA_SO_MNG_BIZ
    {
        string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
        string _excCredit = "000";
        string ATP사용여부 = "000";

        public P_SA_SO_MNG_BIZ()
        {
            BASIC.CacheDataClear(BASIC.CacheEnums.ALL);
            _excCredit = BASIC.GetMAEXC("여신한도");
            ATP사용여부 = BASIC.GetMAEXC("ATP사용여부");
        }

        #region -> 조회
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_SO_MNG_SELECT", obj);
            SearchH(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_SO_MNG_SELECT1", obj);
            SearchL(dt, D.GetString(obj[2]));
            return dt;
        }

        internal DataTable SearchCheckHeader(string 멀티수주번호, object[] obj)
        {
            DataTable dt결과 = null;
            try
            {
                string[] arr = D.StringConvert.GetPipes(멀티수주번호, 150);
                string arrLen = D.GetString(arr.Length);
                int cnt = 1;

                foreach (string 수주번호 in arr)
                {
                    MsgControl.ShowMsg("자료를 조회중 입니다." + "(" + D.GetString(cnt++) + "/" + arrLen + ")");
                    obj[1] = 수주번호;
                    DataTable dt = DBHelper.GetDataTable("UP_SA_SO_MNG_CHECK_S", obj);

                    if (dt결과 == null)
                        dt결과 = dt;
                    else
                        dt결과.Merge(dt);
                }
            }
            finally
            {
                MsgControl.CloseMsg();
            }
            return dt결과;
        }
        #endregion

        #region -> 저장
        public bool Save(DataTable dt_H, DataTable dt_L, string 버튼유형)
        {
            SpInfoCollection sic = new SpInfoCollection();
            if (dt_H != null)
                dt_H.RemotingFormat = SerializationFormat.Binary;
            if (dt_L != null)
                dt_L.RemotingFormat = SerializationFormat.Binary;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                //수주상태 log 남기기
                if (dt_L != null && (버튼유형 == "종결" || 버튼유형 == "종결취소"))
                {
                    SpInfo siLog = new SpInfo();
                    siLog.DataValue = dt_L;
                    siLog.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    siLog.UserID = Global.MainFrame.LoginInfo.UserID;
                    siLog.SpNameUpdate = "UP_SA_SOL_STA_LOG_I";
                    siLog.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_PLANT", "NO_SO", "SEQ_SO", "CD_ITEM", "FG_GUBUN", "ID_INSERT", "NM_KOR1", "NM_INSERT1", "NO_EMP1" };
                    siLog.SpParamsValues.Add(ActionState.Update, "FG_GUBUN", 버튼유형);
                    siLog.SpParamsValues.Add(ActionState.Update, "NM_INSERT1", MA.Login.사용자명);
                    siLog.SpParamsValues.Add(ActionState.Update, "NM_KOR1", MA.Login.사원명);
                    siLog.SpParamsValues.Add(ActionState.Update, "NO_EMP1", MA.Login.사원번호);
                    sic.Add(siLog);
                }
            }
            if (dt_H != null)
            {
                SpInfo siM = new SpInfo();
                siM.DataValue = dt_H;
                siM.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siM.UserID = Global.MainFrame.LoginInfo.UserID;
                siM.SpNameUpdate = "UP_SA_SOH_MNG_UPDATE";
                siM.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SO", "DC_RMK" };
                sic.Add(siM);
            }

            if (dt_L != null)
            {
                SpInfo siL = new SpInfo();
                siL.DataValue = dt_L;
                siL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL.UserID = Global.MainFrame.LoginInfo.UserID;
                siL.SpNameUpdate = "UP_SA_SO_MNG_UPDATE";
                siL.SpNameDelete = "UP_SA_SO_DELETE1";
                siL.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "STA_SO", "DC1", "DC2", "FG_USE", "FG_USE2", "ID_UPDATE" };
                siL.SpParamsDelete = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO" };
                sic.Add(siL);
            }

            return DBHelper.Save(sic);
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noSo, string value)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
                sqlQuery = "UPDATE SA_SOH SET " + columnName + " = '" + value + "' WHERE NO_SO = '" + noSo + "' AND CD_COMPANY = '" + CD_COMPANY + "'";
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                sqlQuery = "UPDATE SA_SOH SET " + columnName + " = NULL WHERE NO_SO = '" + noSo + "' AND CD_COMPANY = '" + CD_COMPANY + "'";

            Global.MainFrame.ExecuteScalar(sqlQuery);
        }
        
        #endregion

        #region -> 삭제
        public bool Delete(string 수주번호)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_SO_DELETE_HL", new object[] { CD_COMPANY, 수주번호 });
        }
        #endregion

        #region -> 여신체크

        public bool CheckCredit(string 거래처, decimal 금액)
        {
            string 수주확정 = "001";
            object[] objOut = new object[3];

            List<object> list = new List<object>();
            list.Add(CD_COMPANY);
            list.Add(거래처);
            list.Add(금액);
            list.Add(수주확정);

            if (BASIC.GetMAEXC("수주관리-여신체크방법") == "001")   //001:만기일 이전 어음 제외(PIMS:D20120116074)
            {
                list.Add("001");
                list.Add(Global.MainFrame.GetStringToday);
            }

            DBHelper.GetDataTable("UP_SA_CHECKCREDIT_SELECT", list.ToArray(), out objOut);

            if (objOut[0] != DBNull.Value)
            {
                string 여신잔액 = D.GetDecimal(objOut[0]).ToString("###,###,###,###,##0.####").PadLeft(15);
                string 수주금액 = D.GetDecimal(objOut[1]).ToString("###,###,###,###,##0.####").PadLeft(15);
                //string 초과금액 = (D.GetDecimal(objOut[0]) - D.GetDecimal(objOut[1])).ToString("###,###,###,###,##0.####");
                string 거래처명 = D.GetString(CodeSearch.GetCodeInfo(Duzon.ERPU.MF.MasterSearch.MA_PARTNER, new object[] { MA.Login.회사코드, 거래처 })["LN_PARTNER"]);
                string msgDetail = "- 거 래 처  :   " + 거래처명 + " (" + 거래처 + ") \n"
                                 + "- 여신잔액  : " + 여신잔액 + "\n"
                                 + "- 수주금액  : " + 수주금액 + "\n";

                if (D.GetString(objOut[2]) == "002")
                {
                    string msg = "여신금액을 초과하였습니다. 그래도 확정 하시겠습니까?\n\n" + msgDetail;

                    if (Global.MainFrame.ShowMessage(msg, "QY2") == DialogResult.Yes) return true;
                    //if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. [거래처 : " + 거래처명 + "(" + 거래처 + ")]\n(여신총액 : " + 여신총액 + ", 잔액 : " + 잔액 + ")\n저장하시겠습니까 ?", "QY2") == DialogResult.Yes) return true;
                    return false;
                }
                else if (D.GetString(objOut[2]) == "003")
                {
                    string msg = "여신금액을 초과하여 확정 할 수 없습니다.\n\n" + msgDetail;
                    Global.MainFrame.ShowMessage(msg);
                    //Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. [거래처 : " + 거래처명 + "(" + 거래처 + ")]\n(여신총액 : " + 여신총액 + ", 잔액 : " + 잔액 + ")");
                    return false;
                }
            }
            return true;
        }

        internal bool CheckCreditExec(string cdPartner, string cdExch, decimal amEx)
        {
            object[] objOut = new object[3];

            DBHelper.GetDataTable("UP_SA_CHECKCREDIT_EXCH_SELECT", new object[] { CD_COMPANY, cdPartner, cdExch, amEx, "001" }, out objOut);

            if (D.GetString(objOut[0]) != string.Empty)
            {
                if (D.GetString(objOut[2]) == "002")
                {
                    if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. [거래처 : " + cdPartner + "]\n(여신총액(" + cdExch + ") : " + D.GetString(objOut[0]) + ", 잔액 : " + D.GetString(objOut[1]) + ")\n저장하시겠습니까 ?", "QY2") == System.Windows.Forms.DialogResult.Yes) return true;
                    return false;
                }
                else if (D.GetString(objOut[2]) == "003")
                {
                    Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액(" + cdExch + ") : " + D.GetString(objOut[0]) + " 잔액 : " + D.GetString(objOut[1]) + ")");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 삼보컴퓨터 전용 여신체크
        /// </summary>
        /// <param name="거래처">거래처</param>
        /// <param name="금액">여신체크하려는 금액</param>
        /// <param name="status">상태값(R:확정하려는경우, C:종결취소하려는경우)</param>
        /// <returns></returns>
        public bool CheckCredit_TRIGEM(string 거래처, decimal 금액, string status)
        {
            string checkLevel = "001";
            object[] objOut = new object[4];

            DBHelper.GetDataTable("UP_SA_Z_TRIGEM_CHECKCREDIT_S", new object[] { CD_COMPANY, 거래처, status == "R" ? decimal.Zero : 금액, checkLevel, string.Empty }, out objOut);

            if (objOut[0] != DBNull.Value)
            {
                //여신잔액 = 여신총금액 - 채권잔액(미수채권 + 미출하된 수주건)
                //위 프로시져에서 구해온 채권잔액에는 지금 확정하려는 수주건에 대한 금액까지 포함되어 있으므로 그 금액을 빼준다.
                string 여신잔액 = (D.GetDecimal(objOut[0]) - (D.GetDecimal(objOut[1]) - (status == "R" ? 금액 : decimal.Zero))).ToString("###,###,###,###,##0.####");
                string 수주금액 = 금액.ToString("###,###,###,###,##0.####");

                if (D.GetString(objOut[2]).ToString() == "002")
                {
                    if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. [거래처 : " + 거래처 + "]\n" + "진행하시겠습니까 ?\n(여신잔액 : " + 여신잔액 + ", 수주금액 : " + 수주금액 + ")", "QY2") == DialogResult.Yes)
                        return true;
                    else
                        return false;
                }
                else if (D.GetString(objOut[2]).ToString() == "003")
                {
                    Global.MainFrame.ShowMessage("여신금액을 초과하여 진행 할 수 없습니다. [거래처 : " + 거래처 + "]\n(여신잔액 : " + 여신잔액 + ", 수주금액 : " + 수주금액 + ")");
                    return false;
                }
            }
            else
            {
                if (D.GetString(objOut[3]) == string.Empty) return true;
                if (D.GetDecimal(objOut[3]) < D.GetDecimal(Global.MainFrame.GetStringToday))
                {
                    string dtValid = D.GetDecimal(objOut[3]).ToString("####/##/##");
                    Global.MainFrame.ShowMessage("해당 거래처[" + 거래처 + "]의 여신에 대해 유효일자가 경과 되었습니다.\n\n- 여신 유효일자 : " + dtValid + "");
                    return false;
                }
            }
            return true;
        }


        #endregion

        #region -> 확정여부 조회

        internal bool IsConfirm(string NO_SO)
        {
            string SQL = string.Empty;
            string 회사코드 = MA.Login.회사코드;

            SQL = " SELECT STA_SO "
                + " FROM   SA_SOL "
                + " WHERE  CD_COMPANY = '" + 회사코드 + "' AND NO_SO = '" + NO_SO + "' AND STA_SO <> 'O'";

            DataTable dt = DBHelper.GetDataTable(SQL);

            if (dt == null || dt.Rows.Count == 0)
                return false;

            return true;
        }

        #endregion

        private void SearchH(DataTable dt)
        {
            if (Global.MainFrame.ServerKeyCommon.ToUpper() != "SONYENT") return;

            DataTable dtGroup = dt.DefaultView.ToTable(true, new string[] { "CD_PARTNER" });
            string multiPartner = Common.MultiString(dtGroup, "CD_PARTNER", "|");
            DataTable dtCredit = DBHelper.GetDataTable("UP_SA_SO_BASE_CREDIT_S", new object[] { MA.Login.회사코드, multiPartner });
            dtCredit.PrimaryKey = new DataColumn[] { dtCredit.Columns["CD_PARTNER"] };
            dt.Columns.Add("AM_CREDIT_JAN", typeof(decimal));
            foreach (DataRow row in dt.Rows)
            {
                DataRow rowFind = dtCredit.Rows.Find(row["CD_PARTNER"]);
                if (rowFind == null) continue;
                row["AM_CREDIT_JAN"] = D.GetDecimal(rowFind["AM_CREDIT"]);
            }
            dt.AcceptChanges();
        }

        private void SearchL(DataTable dt, string cdPlant)
        {
            if (Global.MainFrame.ServerKeyCommon.ToUpper() != "SONYENT") return;

            DataTable dtGroup = dt.DefaultView.ToTable(true, new string[] { "CD_ITEM" });
            string multiPartner = Common.MultiString(dtGroup, "CD_ITEM", "|");
            DataTable dtCredit = DBHelper.GetDataTable("UP_SA_Z_SONYENT_USEINV_S", new object[] { MA.Login.회사코드, cdPlant, multiPartner });
            dtCredit.PrimaryKey = new DataColumn[] { dtCredit.Columns["CD_ITEM"] };
            dt.Columns.Add("QT_USEINV", typeof(decimal));
            dt.Columns.Add("UM_FIXED", typeof(decimal));
            foreach (DataRow row in dt.Rows)
            {
                DataRow rowFind = dtCredit.Rows.Find(row["CD_ITEM"]);
                if (rowFind == null) continue;
                row["QT_USEINV"] = D.GetDecimal(rowFind["QT_USEINV"]);
                row["UM_FIXED"] = D.GetDecimal(rowFind["UM_FIXED"]);
            }
            dt.AcceptChanges();
        }

        #region -> Search_EstimateCost
        public DataTable Search_EstimateCost(string 공장, string multiCdItem)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_ESTIMATE_COST_S", new object[] { MA.Login.회사코드, 공장, multiCdItem });
            return dt;
        } 
        #endregion

        internal string GetExcCredit
        {
            get
            {
                return _excCredit;
            }
        }

        internal string GetATP사용여부
        {
            get
            {
                return ATP사용여부;
            }
        }


        #region -> 출력
        public DataTable Search_Print(string NO_SO)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_SO_PRINT_S", new object[] { MA.Login.회사코드, NO_SO });
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion
    }
}