using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
    internal class P_CZ_HR_PPSRPT_BIZ
    {
        internal bool 급여메일인증사용여부()
        {
            ResultData resultData = (ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_HR_PPERRPT_SELECT",
                SpParamsSelect = new object[1]
              {
          (object) Global.MainFrame.LoginInfo.CompanyCode
              }
            });
            bool flag = true;
            if (resultData.OutParamsSelect[0, 0].Equals((object)"2"))
                flag = false;
            return flag;
        }

        internal string 급여메일암호선택(string 회사코드) => Duzon.ERPU.D.GetString(((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_HR_PPERRPT_PASSWORD_TYPE_S",
            SpParamsSelect = new object[1] { (object)회사코드 }
        })).OutParamsSelect[0, 0]);

        public DataTable 지급일자조회(string 회사코드, string 사번, string 귀속년월) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_HR_PTITLE_SELECT5",
            SpParamsSelect = new object[3]
          {
        (object) 회사코드,
        (object) 귀속년월,
        (object) 사번
          }
        })).DataValue;

        public DataSet Search(
          string 회사코드,
          string 사번,
          string 귀속년월,
          string 지급일자,
          string 양식구분,
          string 급여구분,
          int 급여순번,
          string multi_lang)
        {
            return (DataSet)((ResultData)Global.MainFrame.FillDataSet("UP_HR_PPSRPT_SELECT", new object[8]
            {
        (object) 회사코드,
        (object) 귀속년월,
        (object) 사번,
        (object) 지급일자,
        (object) 양식구분,
        (object) 급여구분,
        (object) 급여순번,
        (object) multi_lang
            })).DataValue;
        }

        internal string GetPrintType() => Duzon.ERPU.HR.HR.FUNC.GetSysConfigCode("MASTER", "COM022");

        internal DataTable Search_tm_etec(string 회사코드, string 사번, string 귀속년월, string 양식구분, int 급여순번) => DBHelper.GetDataTable("UP_HR_Z_ETEC_PPSRPT_TM_S", new object[5]
        {
      (object) 회사코드,
      (object) 귀속년월,
      (object) 사번,
      (object) 양식구분,
      (object) 급여순번
        });
    }
}
