using Duzon.Common.Forms;
using System;
using System.Data;

namespace Dintec
{
	//    exec [BX].[PMS_MSG_C] @nGrpID=2330,   @nCOID=1,   @nUserID=97,   @sBiz_yn=N'0',@sRev_yn=N'0',@sContent=N'TESTTEST',@sTarget_url=N'',@sCOIDs=N'1,',@sUserIDs=N'97,',@sDeptIDs=N'',@sUserNMs=N'김도영',@sDeptNMs=N'',@nEffect=1073741824,@nHeight=180   ,@nOffSet=0   ,@nTextclr=0   ,@sFaceNM=N'맑은 고딕',@nAttachMID=0   ,@nAttachDID=0   ,@sFileNMs=N'',@sFileSZs=N'',@sMsgID=N'',@reservation_dt=N'',@reservation_time=N'',@reservation_minute=N'',@reservateion_id=NULL,@sInsertINF=N'Y',@sSecu_yn=N'0'
	//exec bx.PMS_MSG_C     @nGrpID=N'2330',@nCOID=N'1',@nUserID=N'97',@sBiz_yn=N'0',@sRev_yn=N'0',@sContent=N'333',     @sTarget_url=N'',@sCOIDs=N'1', @sUserIDs=N'195',@sDeptIDs=N'',@sUserNMs=N'김기현',@sDeptNMs=N'',@nEffect=N'0'      ,@nHeight=N'180',@nOffSet=N'0',@nTextclr=N'0',@sFaceNM=N'맑은 고딕',@nAttachMID=N'0',@nAttachDID=N'0',@sFileNMs=N'',@sFileSZs=N'',@sMsgID=N'',@reservation_dt=N'',@reservation_time=N'',@reservation_minute=N'',@reservateion_id=N'0',@sInsertINF=N'N',@sSecu_yn=N'0',@sGrpIDs=N'2330',@nAttachGID=N'0'

	public class Messenger
	{
		public static bool SendMSG(string[] Recipient_IDs, string Contents)
		{
			// 그룹웨어 user_id 불러오기
			string IDs = "";

			foreach (string id in Recipient_IDs)
			{
				IDs += ",'" + id + "'";
			}

			IDs = IDs.Substring(1);

			DBMgr dbmUser = new DBMgr(DBConn.GroupWare);
			dbmUser.Query = "SELECT * FROM BX.TCMG_USER WHERE logon_cd IN (" + IDs + ")";
			DataTable dtUser = dbmUser.GetDataTable();

			if (dtUser.Rows.Count == 0) return false;

			// 수신자 리스트 작성
			string sCOIDs = "";
			string sUserIDs = "";
			string sUserNMs = "";
			string sGrpIDs = "";

			foreach (DataRow row in dtUser.Rows)
			{
				sCOIDs += ",1";
				sUserIDs += "," + row["user_id"];
				sUserNMs += "," + row["user_nm_kr"];
				sGrpIDs += ",2330";
			}

			sCOIDs = sCOIDs.Substring(1);
			sUserIDs = sUserIDs.Substring(1);
			sUserNMs = sUserNMs.Substring(1);
			sGrpIDs = sGrpIDs.Substring(1);

			// 실행
			DBMgr dbm = new DBMgr(DBConn.GroupWare);
			dbm.Procedure = "BX.PMS_MSG_C";
			dbm.AddParameter("@nGrpID", 2330);
			dbm.AddParameter("@nCOID", 1);
			dbm.AddParameter("@nUserID", 242);  // 발신자 ID (고정)
			dbm.AddParameter("@sBiz_yn", "0");
			dbm.AddParameter("@sRev_yn", "0");
			dbm.AddParameter("@sContent", Contents);
			dbm.AddParameter("@sTarget_url", "");
			dbm.AddParameter("@sCOIDs", sCOIDs);
			dbm.AddParameter("@sUserIDs", sUserIDs);
			dbm.AddParameter("@sDeptIDs", "");
			dbm.AddParameter("@sUserNMs", sUserNMs);
			dbm.AddParameter("@sDeptNMs", "");
			dbm.AddParameter("@nEffect", 1073741824);
			dbm.AddParameter("@nHeight", 180);
			dbm.AddParameter("@nOffSet", 0);
			dbm.AddParameter("@nTextclr", 0);
			dbm.AddParameter("@sFaceNM", "맑은 고딕");
			dbm.AddParameter("@nAttachMID", 0);
			dbm.AddParameter("@nAttachDID", 0);
			dbm.AddParameter("@sFileNMs", "");
			dbm.AddParameter("@sFileSZs", "");
			dbm.AddParameter("@sMsgID", "");
			dbm.AddParameter("@reservation_dt", "");
			dbm.AddParameter("@reservation_time", "");
			dbm.AddParameter("@reservation_minute", "");
			dbm.AddParameter("@reservateion_id", DBNull.Value);
			dbm.AddParameter("@sInsertINF", "Y");
			dbm.AddParameter("@sSecu_yn", "0");
			dbm.AddParameter("@sGrpIDs", sGrpIDs);
			dbm.ExecuteNonQuery();

			return true;
		}

		public static void SendStockMsg(string itemXml, string fromText)
		{
			// 메세지 보낼 정보 가져오기
			DataTable dtMsg = DBMgr.GetDataTable("PX_CZ_MM_SEND_BOOK_MSG", false, true, itemXml, fromText);

			// 사원별로 구분			
			DataTable dtEmp = dtMsg.DefaultView.ToTable(true, "NO_EMP");

			foreach (DataRow empRow in dtEmp.Rows)
			{
				string user = empRow["NO_EMP"].ToString();

				// 본문 생성
				string body = "";

				if (Global.MainFrame.LoginInfo.Language == "KR")
					body = @"** 재고 입고 알람
{0}

해당 수량 출고예약 완료되었습니다.


※ 본 쪽지는 발신 전용 쪽지입니다.";
				else
					body = @"** Notice for Received Stock
{0}

Please be sure to reserve as many as 'GI Booking' quantity.

※ You can't reply back to this message.";

				// 재고코드 라인 생성
				string item = "";

				foreach (DataRow itemRow in dtMsg.Select("NO_EMP = '" + user + "'"))
				{
					if (Global.MainFrame.LoginInfo.Language == "KR")
						item += string.Format(@"
-. {0} / {1} / 입고예약 : {2:#,##0} → 출고예약 : {3:#,##0}", itemRow["NO_FILE"], itemRow["CD_ITEM"], itemRow["QT_HOLD"], itemRow["QT_BOOK"]);
					else
						item += string.Format(@"
-. {0} / {1} / GR Booking : {2:#,##0} → GI Booking : {3:#,##0}", itemRow["NO_FILE"], itemRow["CD_ITEM"], itemRow["QT_HOLD"], itemRow["QT_BOOK"]);
				}

				string contents = string.Format(body, item);
				SendMSG(new string[] { user }, contents);
			}
		}
	}
}
