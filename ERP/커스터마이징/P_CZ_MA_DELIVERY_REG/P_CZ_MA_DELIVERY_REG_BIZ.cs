using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_CZ_MA_DELIVERY_REG_BIZ
	{

        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_DELIVERY_REG_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_MA_DELIVERY_REG_XML";
            si.SpParamsInsert = new string[] { "CD_COMPANY", "XML", "ID_INSERT" };

            return DBHelper.Save(si);
        }

        internal bool Confirm(string 회사코드, string 신청번호, string code)
        {
            DBHelper.ExecuteNonQuery("SP_CZ_MA_DELIVERY_REG_CONFIRM", new object[] { 회사코드,
                                                                                    code,
                                                                                    Global.MainFrame.LoginInfo.UserID.ToString(),
                                                                                    신청번호
                                                                                     });

            return true;
        }


        internal bool 취소(string 회사코드, string 신청번호, string 거래처코드)
        {
            DBHelper.ExecuteNonQuery("SP_CZ_MA_DELIVERY_REG_ROLLBACK", new object[] { 회사코드, 신청번호, 거래처코드 });

            return true;
        }
    }
}
