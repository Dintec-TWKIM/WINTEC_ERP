using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_HR_PEVALU_OBJECT_BIZ
    {
        private string 로그인 = Global.MainFrame.LoginInfo.UserID;
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
        private string 사원번호 = Global.MainFrame.LoginInfo.EmployeeNo;

        internal DataTable Search(string 평가코드, string 평가유형)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PEVALU_OBJECT_S", new object[] { this.회사코드,
                                                                                 평가코드,
                                                                                 평가유형,
                                                                                 this.사원번호 });
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return true;

            SpInfoCollection spCollection = new SpInfoCollection();
            SpInfo spInfo = new SpInfo();
            spCollection.Add(new SpInfo()
            {
                DataValue = (object)dt,
                CompanyID = this.회사코드,
                UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                SpNameInsert = "SP_CZ_HR_PEVALU_OBJECT_I",
                SpNameUpdate = "SP_CZ_HR_PEVALU_OBJECT_U",
                SpNameDelete = "SP_CZ_HR_PEVALU_OBJECT_D",
                SpParamsInsert = new string[] { "CD_COMPANY",
                                                "CD_EVALU",
                                                "CD_EVTYPE",
                                                "CD_EVOTYPE",
                                                "NO_EMP",
                                                "CD_OTASK",
                                                "NM_OTASK",
                                                "DC_DOBJECT",
                                                "CD_SCALE",
                                                "RT_WEIGHT",
                                                "YN_APP",
                                                "DT_APP",
                                                "ID_INSERT" },
                SpParamsUpdate = new string[] { "CD_COMPANY",
                                                "CD_EVALU",
                                                "CD_EVTYPE",
                                                "CD_EVOTYPE",
                                                "NO_EMP",
                                                "CD_OTASK",
                                                "NM_OTASK",
                                                "DC_DOBJECT",
                                                "CD_SCALE",
                                                "RT_WEIGHT",
                                                "YN_APP",
                                                "DT_APP",
                                                "ID_UPDATE" },
                SpParamsDelete = new string[] { "CD_COMPANY",
                                                "CD_EVALU",
                                                "CD_EVTYPE",
                                                "CD_EVOTYPE",
                                                "NO_EMP" }
            });

            Global.MainFrame.Save(spCollection);
            return true;
        }
    }
}
