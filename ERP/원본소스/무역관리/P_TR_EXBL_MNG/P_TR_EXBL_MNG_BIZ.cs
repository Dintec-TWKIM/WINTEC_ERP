using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;

namespace trade
{
    class P_TR_EXBL_MNG_BIZ
    {
        #region ♣ 멤버변수 / 생성자

        #region -> 멤버변수

        #endregion

        #region -> 생성자

        public P_TR_EXBL_MNG_BIZ()
        {
        }

        #endregion

        #endregion

        #region -> 조회

        public DataTable search(object[] args)
        {
            SpInfo si = new SpInfo();
            si.SpParamsSelect = args;
            si.SpNameSelect = "UP_TR_EXBL_MNG_H_SELECT";
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            return dt;
        }

        public DataTable SearchDetail(object[] args)
        {
            SpInfo si = new SpInfo();
            si.SpParamsSelect = args;
            si.SpNameSelect = "UP_TR_EXBL_MNG_L_SELECT";
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            return dt;
        }

        #endregion

        #region -> 저장

        public bool Save(DataTable dt_H)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dt_H != null)
            {
                SpInfo siM = new SpInfo();

                siM.DataValue = dt_H;
                siM.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siM.UserID = Global.MainFrame.LoginInfo.UserID;

                siM.SpNameUpdate = "UP_TR_EXBL_MNG_UPDATE";
                siM.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_BL", "FILE_PATH_MNG", "DT_PAYABLE" };
                sic.Add(siM);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);

            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }

        #endregion

        #region -> 삭제

        public bool delete(DataTable dtH)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo siH = new SpInfo();
                siH.DataValue = dtH;
                siH.DataState = DataValueState.Deleted;
                siH.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siH.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siH.SpNameDelete = "UP_TR_EXBL_MNG_DELETE";
                siH.SpParamsDelete = new string[] { "CD_COMPANY", "NO_BL" };
                sc.Add(siH);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sc);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }

        #endregion

        #region -> 미결전표처리

        public bool TransDocu(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.DataState = DataValueState.Added;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SEEGENE")    // SEEGENE 추가
                    si.SpNameInsert = "UP_TR_EXBL_DOCU_SEEGENE";
                else if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KPCI")  //(주)케이피아이씨코포레이션
                    si.SpNameInsert = "UP_TR_EXBL_DOCU_KPCI";
                else
                    si.SpNameInsert = "UP_TR_EXBL_DOCU";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_BL" };
                sc.Add(si);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sc);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }

        #endregion

        #region -> 전표처리취소

        public bool CancelTransDocu(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.DataState = DataValueState.Deleted;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.SpNameDelete = "UP_FI_DOCU_AUTODEL";
                si.SpParamsDelete = new string[] { "CD_COMPANY", "NO_MODULE", "NO_BL", "ID_UPDATE" };
                si.SpParamsValues.Add(ActionState.Delete, "NO_MODULE", "170");
                sc.Add(si);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sc);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }

        #endregion
    }
}
