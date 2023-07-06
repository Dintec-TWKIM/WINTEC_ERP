using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace trade
{
    class P_TR_EXSEAL_BIZ
    {
        string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;

        public DataSet Search(string NO_SEAL)
        {
            ResultData rd = (ResultData)Global.MainFrame.FillDataSet("UP_TR_EXSEAL_SELECT", new object[] { CD_COMPANY, NO_SEAL });
            DataSet ds = (DataSet)rd.DataValue;

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                        Col.DefaultValue = 0;
                }
            }

            ds.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            ds.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            ds.Tables[0].Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            ds.Tables[0].Columns["NM_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaName;
            ds.Tables[0].Columns["DT_BALLOT"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["DT_TRANS"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["DT_SEAL"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["DT_VALIDITY"].DefaultValue = Global.MainFrame.GetStringToday;

            ds.Tables[0].Columns["DTS_INSERT"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["ID_INSERT"].DefaultValue = Global.MainFrame.LoginInfo.UserID;
            ds.Tables[0].Columns["DTS_UPDATE"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["ID_UPDATE"].DefaultValue = Global.MainFrame.LoginInfo.UserID;

            ds.Tables[0].Columns["CD_EXCH"].DefaultValue = "000";
            ds.Tables[0].Columns["RT_EXCH"].DefaultValue = 1;

            return ds;
        }

        //계산서적용시 데이터조회
        public DataTable 계산서조회(string Multi계산서번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXSEAL_JOIN_SELECT";
            si.SpParamsSelect = new Object[] { CD_COMPANY, Multi계산서번호 };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)resultdata.DataValue;
        }

        //데이터 삭제
        public bool Delete(string NO_SEAL)
        {
            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_TR_EXSEAL_DELETE", new object[] { CD_COMPANY, NO_SEAL });
            return result.Result;
        }


        //데이터 저장
        public bool Save(DataTable header, DataTable flex)
        {
            SpInfoCollection sc = new SpInfoCollection();

            //상담접수
            if (header != null)
            {
                SpInfo si1 = new SpInfo();
                si1.DataValue = header;
                si1.CompanyID = CD_COMPANY;
                si1.SpNameInsert = "UP_TR_EXSEAL_H_INSERT";			// Insert 프로시저명
                si1.SpNameUpdate = "UP_TR_EXSEAL_H_INSERT";			// Update 프로시저명
                si1.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SEAL",  "DT_BALLOT",	"CD_BIZAREA",	"CD_PARTNER",	"CD_EXCH",
							                        "AM_SEAL",	"AM",			"CD_DEPT",		"NO_EMP",		"DT_TRANS",
							                        "DT_SEAL",	"DT_VALIDITY",	"REMARK",		"DTS_INSERT",	"ID_INSERT",
                                                    "DTS_UPDATE","ID_UPDATE","RT_EXCH","NO_IV"};
                si1.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SEAL",  "DT_BALLOT",	"CD_BIZAREA",	"CD_PARTNER",	"CD_EXCH",
							                        "AM_SEAL",	"AM",			"CD_DEPT",		"NO_EMP",		"DT_TRANS",
							                        "DT_SEAL",	"DT_VALIDITY",	"REMARK",		"DTS_INSERT",	"ID_INSERT",
                                                    "DTS_UPDATE","ID_UPDATE","RT_EXCH","NO_IV"};
                sc.Add(si1);
            }

            //고객정보
            if (flex != null)
            {
                SpInfo si2 = new SpInfo();
                si2.DataValue = flex;
                si2.CompanyID = CD_COMPANY;
                si2.SpNameInsert = "UP_TR_EXSEAL_L_INSERT";			// Insert 프로시저명
                si2.SpNameUpdate = "UP_TR_EXSEAL_L_INSERT";			// Update 프로시저명
                si2.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SEAL",		"NO_LINE",		"NO_IV",		"NO_IVLINE",	"CD_PLANT",
							                        "NO_LC",		"CD_ITEM",		"QT_GI_CLS",	"CD_EXCH",		"RT_EXCH",
							                        "UM_EX_CLS",	"AM_EX_CLS",	"UM_ITEM_CLS",	"AM_CLS",		"QT_CLS",
							                        "DTS_INSERT",	"ID_INSERT",    "DTS_UPDATE",   "ID_UPDATE" };
                si2.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SEAL",		"NO_LINE",		"NO_IV",		"NO_IVLINE",	"CD_PLANT",
							                        "NO_LC",		"CD_ITEM",		"QT_GI_CLS",	"CD_EXCH",		"RT_EXCH",
							                        "UM_EX_CLS",	"AM_EX_CLS",	"UM_ITEM_CLS",	"AM_CLS",		"QT_CLS",
							                        "DTS_INSERT",	"ID_INSERT",    "DTS_UPDATE",   "ID_UPDATE" };
                sc.Add(si2);
            }

            ResultData[] result = (ResultData[])Global.MainFrame.Save(sc);

            for (int i = 0; i < result.Length; i++)
                if (!result[i].Result) return false;

            return true;
        }
    }
}
