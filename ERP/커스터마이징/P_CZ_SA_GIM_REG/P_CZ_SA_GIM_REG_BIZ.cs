using System;
using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Auth;
using System.IO;

namespace cz
{
    class P_CZ_SA_GIM_REG_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(object[] obj)
        {
            //UP_SA_GIM_REG_SELECT
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIM_REG_H_S", obj, "NO_IO ASC");
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            //UP_SA_GIM_REG_SELECT1
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIM_REG_L_S", obj, "NO_IO ASC, NO_IOLINE ASC");
            T.SetDefaultValue(dt);

            AuthUserMenu AuthMenu = new AuthUserMenu(dt, Global.MainFrame.CurrentPageID);

            string[] AM = new string[] { "AM_EX", "AM", "VAT" }; //금액컬럼 설정
            string[] QT = new string[] { "QT_IO", "QT_UNIT_MM", "QT_CLS" };   //수량컬럼 설정
            string[] UM = new string[] { "UM_EX", "UM" };   //단가컬럼 설정

            AuthMenu.SetAuthMenu(QT, UM, AM);

            return dt;
        }

        public DataTable CloseMessage(string 수불번호)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIM_REG_SELECT_MSG", new Object[] { 회사코드, 수불번호 });
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = dt;
            si.CompanyID = 회사코드;
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.SpNameDelete = "SP_CZ_SA_GIM_REG_D";
            si.SpNameUpdate = "SP_CZ_SA_GIM_REG_U";

            si.SpParamsDelete = new string[] { "CD_COMPANY",
                                               "NO_IO",
                                               "NO_ISURCV",
                                               "ID_DELETE" };

            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_IO",
                                               "NO_ISURCV",
                                               "DT_LOADING",
                                               "DC_RMK",
                                               "FILE_PATH_MNG",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Delete, "ID_DELETE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIo, string value)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
                sqlQuery = "UPDATE MM_QTIOH SET " + columnName + " = '" + value + "' WHERE NO_IO = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'";
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                sqlQuery = "UPDATE MM_QTIOH SET " + columnName + " = NULL WHERE NO_IO = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'";

            Global.MainFrame.ExecuteScalar(sqlQuery);
        }

        public string SearchFileInfo(string companyCode, string fileCode)
        {
            string query, result = string.Empty;

            try
            {
                query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                          FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + companyCode + "'" + Environment.NewLine +
                        @"AND CD_MODULE = 'SA'
                          AND ID_MENU = 'P_CZ_SA_GIM_REG'
                          AND CD_FILE = '" + fileCode + "'";

                result = D.GetString(Global.MainFrame.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        public void SaveFileInfo(string cdCompany, string cdFile, FileInfo fileInfo, string 업로드위치)
        {
            string query;

            try
            {
                query = @"SELECT MAX(NO_SEQ) 
                          FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + cdCompany + "'" + Environment.NewLine +
                        @"AND CD_MODULE = 'SA'
                          AND ID_MENU = 'P_CZ_SA_GIM_REG'
                          AND CD_FILE = '" + cdFile + "'";

                DBHelper.ExecuteNonQuery("UP_MA_FILEINFO_INSERT", new object[] { cdCompany,
                                                                                 "SA",
                                                                                 "P_CZ_SA_GIM_REG",
                                                                                 cdFile,
                                                                                 (D.GetDecimal(DBHelper.ExecuteScalar(query)) + 1),
                                                                                 fileInfo.Name,
                                                                                 업로드위치,
                                                                                 fileInfo.Extension.Replace(".", ""),
                                                                                 fileInfo.LastWriteTime.ToString("yyyyMMdd"),
                                                                                 fileInfo.LastWriteTime.ToString("hhmm"),
                                                                                 string.Format("{0:0.00}M", ((Decimal)fileInfo.Length / new Decimal(1048576))),
                                                                                 Global.MainFrame.LoginInfo.UserID });       
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public string 협조전존재여부(string 회사코드, string 협조전번호)
        {
            string query;

            try
            {
                query = @"SELECT GH.DT_GIR
                          FROM MM_QTIO OL WITH(NOLOCK)
                          LEFT JOIN SA_GIRH GH WITH(NOLOCK) ON GH.CD_COMPANY = OL.CD_COMPANY AND GH.NO_GIR = OL.NO_ISURCV
                          WHERE OL.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
                        @"AND OL.NO_ISURCV = '" + 협조전번호 + "'";

                return D.GetString(DBHelper.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return string.Empty;
        }
    }
}
