using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.Common.Forms;
using Dintec;
using System.Net;

namespace cz
{
    internal class P_CZ_MA_FAX_SUB_BIZ
    {
        internal void 팩스보내기(string 받는사람, string 받는사람명, string 첨부파일, string 날짜, string 파일번호)
        {
            DBMgr dbMgr;
            string query, 첨부파일경로;

            try
            {
                첨부파일경로 = @"D:\CallNet\Fax\FaxData\Send\" + 날짜 + "_" + 첨부파일;

                query = @"INSERT INTO SENDINGDATA
                          (
                            WDate,
                            UserID,
                            RecverName,
                            FaxNum,
                            Title,
                            Contents,
                            FileName,
                            Reserve,
                            SendingDate,
                            Cover,
                            cnt_retry,
                            Send_Cnt,
                            GroupName,
                            USE_YN,
                            MAKEBMP_YN,
                            Part_code,
                            RcvTel,
                            ATTN
                          )
                          VALUES" + Environment.NewLine +
                         "('" + 날짜 + "'," + Environment.NewLine +
                          "'" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
                          "'" + 받는사람명 + "'," + Environment.NewLine +
                          "'" + 받는사람 + "'," + Environment.NewLine +
                          "'" + 파일번호 + "'," + Environment.NewLine +
                          "'" + 첨부파일 + "'," + Environment.NewLine +
                          "'" + 첨부파일경로 + "'," + Environment.NewLine +
                          "''," + Environment.NewLine +
                          "'" + 날짜 + "'," + Environment.NewLine +
                         @"'0',
                           '0',
                           '2',
                           '',
                           'N',
                           '0',
                           '',
                           '',
                           ''
                          )";

                dbMgr = new DBMgr(DBConn.Fax);
                dbMgr.Query = query;
                dbMgr.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
