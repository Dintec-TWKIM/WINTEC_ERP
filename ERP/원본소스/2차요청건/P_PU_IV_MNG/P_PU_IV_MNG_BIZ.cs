using System;
using System.Data;
using System.Collections.Generic;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Dass.FlexGrid;
using Duzon.ERPU.MF; 
namespace pur
{
    public class P_PU_IV_MNG_BIZ
    {
        string 로그인 = Global.MainFrame.LoginInfo.UserID;
        public DataTable SettingDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID_INSERT", typeof(string));
            dt.Columns.Add("CD_COMPANY", typeof(string));
            dt.Columns.Add("MODULE", typeof(string));

            return dt;
        }

        #region-> 조회
        public DataTable Search(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_IV_MNG_SELECT_H";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;
            dt.Columns.Add("AFTER_OK");
            return dt;
        }

        public DataTable Search()
        {
            string strSp = "";

            if (Global.MainFrame.ServerKeyCommon == "KIHA")
                strSp = "UP_PU_Z_KIHA_IV_MNG_SELECT_L";
            else
                strSp = "UP_PU_IV_MNG_SELECT_L";

            SpInfo si2 = new SpInfo();
            si2.SpNameSelect = strSp;
            si2.SpParamsSelect = new Object[] { "xxx", "xxx" };
            ResultData result2 = (ResultData)Global.MainFrame.FillDataTable(si2);
            return (DataTable)result2.DataValue;
        }

        
        public DataTable Search(string CD_COMPANY, string NO_IV)
        {
            string strSp = "";

            if (Global.MainFrame.ServerKeyCommon == "KIHA")
                strSp = "UP_PU_Z_KIHA_IV_MNG_SELECT_L";
            else
                strSp = "UP_PU_IV_MNG_SELECT_L";

            SpInfo si2 = new SpInfo();
            si2.SpNameSelect = strSp;
            si2.SpParamsSelect = new Object[] { CD_COMPANY, NO_IV };
            ResultData result2 = (ResultData)Global.MainFrame.FillDataTable(si2);
            return (DataTable)result2.DataValue;
        }
        #endregion

        #region -> 프린트
        public DataSet Print(string CD_COMPANY, string Multikey)
        {
            //    SpInfo si2 = new SpInfo();
            //    si2.SpNameSelect = "UP_PU_IV_MNG_SELECT_PRINT";
            //    si2.SpParamsSelect = new Object[] {CD_COMPANY, Multikey };
            //    ResultData rd = (ResultData)Global.MainFrame.FillDataTable(si2);   
            //    DataTable dt = (DataTable)rd.DataValue;
            DataSet ds = null;
            if (Global.MainFrame.ServerKeyCommon == "INITECH" )
            {
                ds = DBHelper.GetDataSet("UP_PU_IV_MNG_SELECT_PRINT_NURUN", new object[] { CD_COMPANY, Multikey });
            }
            else
            {
                ds = DBHelper.GetDataSet("UP_PU_IV_MNG_SELECT_PRINT", new object[] { CD_COMPANY, Multikey });
            }
            return ds;
        }
        #endregion


        #region -> save
        public bool Save(DataTable dt)
        {
            SpInfoCollection sic = new SpInfoCollection();

            Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
            si.DataState = DataValueState.Modified;


            dt.RemotingFormat = SerializationFormat.Binary;

            si.DataValue = dt; 					//저장할 데이터 테이블
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.SpNameUpdate = "UP_PU_IVH_UPDATE";			//update 프로시저명

            /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
            si.SpParamsUpdate = new string[] { "NO_IV", "CD_COMPANY", "CD_DOCU", "FG_PAYBILL", "DT_PAY_PREARRANGED", "CD_PARTNER" ,"DT_DUE", "YN_JEONJA","DC_RMK","NO_BIZAREA","TXT_USERDEF1"};

            sic.Add(si);

      
            if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
            {
                SpInfo si2 = new SpInfo();
                si2.DataState = DataValueState.Modified;

                si2.DataValue = dt;
                si2.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si2.SpNameUpdate = "UP_PU_Z_UNIPOINT_PRERCV";

                si2.SpParamsUpdate = new string[] { "CD_COMPANY1", "NO_IV", "ID_INSERT_N" };
                si2.SpParamsValues.Add(ActionState.Update, "ID_INSERT_N", Global.MainFrame.LoginInfo.UserID);
                si2.SpParamsValues.Add(ActionState.Update, "CD_COMPANY1", Global.MainFrame.LoginInfo.CompanyCode);

                sic.Add(si2);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;

        }
        #endregion

        #region -> Delete
        public void Delete(object [] p_param)
        {
            Global.MainFrame.ExecSp( "UP_PU_IVH_DELETE", p_param );
        }
        #endregion

        #region -> 관리구분배부취소
        public void Delete_ad_dist(object[] p_param)
        {
            string sql = "";
            sql += " DELETE FROM PU_IVL_SETR_SUB ";
            sql += " WHERE ";
            sql += "        CD_COMPANY = '" + D.GetString(p_param[0]) + "' ";
            sql += " AND	NO_IV ='" + D.GetString(p_param[1]) + "' ";

            Global.MainFrame.ExecuteScalar(sql);

            
        }
        #endregion

        #region -> 미결전표처리
        public bool 미결전표처리(string P_CD_COMPANY, string P_NO_IV, string P_MODULE, string Tx_내역표시구분, string Tx_내역표시_Text, string Tx_품목표시구분)
        {
            ResultData result;
            string option = Duzon.ERPU.MF.ComFunc.전용코드("매입전표-그룹옵션설정");
            if (option.Equals(string.Empty))
                option = "000";

            if (Global.MainFrame.ServerKeyCommon == "YWD")
            {
                result = (ResultData)Global.MainFrame.ExecSp("UP_PU_IV_MNG_TRANS_DOCU_YWD", new object[] { P_CD_COMPANY, P_NO_IV, P_MODULE });
            }
            else if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                result = (ResultData)Global.MainFrame.ExecSp("UP_PU_Z_CHBA_IVMNG_DOCU", new object[] { P_CD_COMPANY, P_NO_IV, P_MODULE, option });
            }
            else if (BASIC.GetMAEXC("업체별프로세스") == "002")
            {
                result = (ResultData)Global.MainFrame.ExecSp("UP_PU_Z_SATREC_IVMNG_DOCU", new object[] { P_CD_COMPANY, P_NO_IV, P_MODULE, option });
            }
            else if (Global.MainFrame.ServerKeyCommon == "HANMIIT")
            {
                result = (ResultData)Global.MainFrame.ExecSp("UP_PU_Z_HANMIIT_IVMNG_DOCU", new object[] { P_CD_COMPANY, P_NO_IV, P_MODULE, option });
            }
            else if (Global.MainFrame.ServerKeyCommon == "KBSM")
            {
                result = (ResultData)Global.MainFrame.ExecSp("UP_PU_IV_MNG_TRANS_DOCU_KBS", new object[] { P_CD_COMPANY, P_NO_IV, P_MODULE, option });
            }
            else
            {
                result = (ResultData)Global.MainFrame.ExecSp("UP_PU_IV_MNG_TRANS_DOCU", new object[] { P_CD_COMPANY, P_NO_IV, P_MODULE, option, Tx_내역표시구분, Tx_내역표시_Text, Tx_품목표시구분, Global.MainFrame.LoginInfo.EmployeeNo });

            }
            return result.Result;

        }
        #endregion

        #region -> 미결전표취소
        public bool 미결전표취소(string 전표유형, string 전표번호)
        {
            //string 담당자 = Global.MainFrame.LoginInfo.UserID;
            //ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_FI_DOCU_AUTODEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 전표유형, 전표번호, 담당자 });
            //return result.Result;


            DataTable dtL = new DataTable();
            dtL.Columns.Add("CD_COMPANY", typeof(string));
            dtL.Columns.Add("NO_MODULE", typeof(string));
            dtL.Columns.Add("NO_MDOCU", typeof(string));
            dtL.Columns.Add("ID_UPDATE", typeof(string));
            dtL.Rows.Add();
            dtL.Rows[0]["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
            dtL.Rows[0]["NO_MODULE"] = 전표유형;
            dtL.Rows[0]["NO_MDOCU"] = 전표번호;
            dtL.Rows[0]["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserID;


            SpInfoCollection sic = new SpInfoCollection();
            SpInfo siL = new SpInfo();

            siL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            siL.UserID = Global.MainFrame.LoginInfo.UserID;
            siL.DataValue = dtL.Copy();
            siL.DataState = DataValueState.Deleted;
            siL.SpNameDelete = "UP_FI_DOCU_AUTODEL";
            siL.SpParamsDelete = new string[] { "CD_COMPANY", "NO_MODULE", "NO_MDOCU", "ID_UPDATE" };

            sic.Add(siL);
            if (Global.MainFrame.ServerKeyCommon == "SQL_" || Global.MainFrame.ServerKeyCommon == "TRIGEM" || Global.MainFrame.ServerKeyCommon == "DZSQL")
            {

                SpInfo siL1 = new SpInfo();

                dtL.Rows[0]["NO_MDOCU"] = 전표번호;

                siL1.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL1.UserID = Global.MainFrame.LoginInfo.UserID;
                siL1.DataValue = dtL.Copy();
                siL1.DataState = DataValueState.Deleted;
                siL1.SpNameDelete = "UP_PU_REVERSE_ETRS_TRANS_DEL";
                siL1.SpParamsDelete = new string[] { "CD_COMPANY", "NO_MODULE", "NO_MDOCU", "ID_UPDATE" };
                sic.Add(siL1);

                SpInfo siL2 = new SpInfo();

                dtL.Rows[0]["NO_MDOCU"] = 전표번호 + "_A";

                siL2.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL2.UserID = Global.MainFrame.LoginInfo.UserID;
                siL2.DataValue = dtL.Copy();
                siL2.DataState = DataValueState.Deleted;
                siL2.SpNameDelete = "UP_FI_DOCU_AUTODEL";
                siL2.SpParamsDelete = new string[] { "CD_COMPANY", "NO_MODULE", "NO_MDOCU", "ID_UPDATE" };
                sic.Add(siL2);

            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);
            for (int i = 0; i < rtn.Length; i++)
            {
                if (!rtn[i].Result) return false;
            }

            return true;


        }
        #endregion

        #region -> 전자결제
        public int GetFI_GWDOCU(string NO_INV)
        {
            int rtn_value = 999;
            string SelectQuery = "SELECT ST_STAT" +
                                "  FROM FI_GWDOCU" +
                                " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                "  AND CD_PC = '" + Global.MainFrame.LoginInfo.CdPc + "'" +
                                "  AND NO_DOCU = '" + NO_INV + "'"
                                ;


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    rtn_value = Convert.ToInt32(dt.Rows[0]["ST_STAT"].ToString());
                }

            }

            return rtn_value;
        }

        // 전자결재 데이터 출력
        public DataTable DataSearch_GW_RPT(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT", obj);

            return dt;
        }

        //전자결재_실제사용
        public bool 전자결재_실제사용(DataRow drHeader, string Html_Code, string App_Form_Kind, string Nm_Pumn)
        {
            List<object> list = new List<object>();
            list.Add(Global.MainFrame.LoginInfo.CompanyCode);
            list.Add(Global.MainFrame.LoginInfo.CdPc);
            list.Add(drHeader["NO_IV"].ToString());
            list.Add(drHeader["NO_EMP"].ToString());
            list.Add(drHeader["DT_PROCESS"].ToString());
            list.Add(App_Form_Kind); // 이건 정해진것
            list.Add(Html_Code);
            list.Add(Nm_Pumn); //nm_pumm
            list.Add(테이블구분.NONE.GetHashCode());//CD_MENU

            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_PU_GWDOCU", list.ToArray()); //업데이트 트리거도 같이 고쳐야함
            return result.Result;
        }
        public bool 전자결재_CNP_실제사용(DataRow drHeader, string Html_Code, string App_Form_Kind, string Nm_Pumn)
        {
            List<object> list = new List<object>();
            list.Add(Global.MainFrame.LoginInfo.CompanyCode);
            list.Add(Global.MainFrame.LoginInfo.CdPc);
            list.Add(drHeader["NO_IV"].ToString());
            list.Add(MA.Login.사원번호);
            list.Add(drHeader["DT_PROCESS"].ToString());
            list.Add(App_Form_Kind); // 이건 정해진것
            list.Add(Html_Code);
            list.Add(Nm_Pumn); //nm_pumm
            list.Add(테이블구분.NONE.GetHashCode());//CD_MENU
            list.Add("PU");
            list.Add(drHeader["AM_SUP"]);

            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_PU_Z_CNP_GWDOCU", list.ToArray()); //업데이트 트리거도 같이 고쳐야함
            return result.Result;
        }

        #endregion

        #region -> 고정자산처리
        public bool 고정자산처리(string P_NO_IV)
        {
            ResultData result;

            if (Global.MainFrame.ServerKeyCommon == "KIHA")
            {
                result = (ResultData)Global.MainFrame.ExecSp("UP_PU_Z_KIHA_IV_ASSET_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode, P_NO_IV, Global.MainFrame.LoginInfo.UserID });
            }
            else
                result = null;

            return result.Result;

        }
        #endregion

        #region -> KPCI 전용

        public void KPCI_PAYMENT_INSERT(string CD_COMPANY, string NO_IV, string DT_LIMIT_D, string DT_LIMIT_C)
        {
            Global.MainFrame.ExecSp("UP_SA_Z_KPCI_PAYMENT_I", new object[] { CD_COMPANY, NO_IV, Global.MainFrame.LoginInfo.EmployeeNo.ToString(), DT_LIMIT_D, DT_LIMIT_C });
        }

        public void KPCI_PAYMENT_DELETE(string CD_COMPANY, string NO_IV)
        {
            Global.MainFrame.ExecSp("UP_SA_Z_KPCI_PAYMENT_D", new object[] { CD_COMPANY, NO_IV });
        }

        public DataTable search_dt(string strDate)
        {
            string SelectQuery = "SELECT DT_CAL" +
                                 "  FROM MA_CALENDAR " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  " +
                                 "   AND DT_CAL > '" + strDate + "' " +
                                 "   AND FG1_HOLIDAY = 'W' " +
                                 " ORDER BY DT_CAL";

            ;
            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }

        #endregion

        #region ♣ 메모  & 체크팬
        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIo, decimal no_ioline, string value)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
                sqlQuery = "UPDATE PU_IVL SET " + columnName + " = '" + value + "' WHERE NO_IV  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'  AND NO_LINE = " + no_ioline;
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                sqlQuery = "UPDATE PU_IVL SET " + columnName + " = NULL WHERE NO_IV  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "' AND NO_LINE = " + no_ioline;

            Global.MainFrame.ExecuteScalar(sqlQuery);

        }
        #endregion
    }
}
