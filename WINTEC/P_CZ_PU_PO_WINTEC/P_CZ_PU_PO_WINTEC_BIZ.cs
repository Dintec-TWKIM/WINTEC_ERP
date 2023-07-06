using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using System;
using System.Collections;
using System.Data;

namespace cz
{
    internal class P_CZ_PU_PO_WINTEC_BIZ
    {
        private DataTable _dt_fg_post = new DataTable();
        private bool b_first_clone = false;
        private string Lang = Global.SystemLanguage.MultiLanguageLpoint.ToString();

        public DataSet Search(string NO_PO, string NO_POLINE)
        {
            ResultData resultData = (ResultData)Global.MainFrame.FillDataSet("UP_CZ_PU_PO_REG_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                      NO_PO,
                                                                                                                      this.Lang,
                                                                                                                      NO_POLINE });
            DataSet dataValue = (DataSet)resultData.DataValue;
            foreach (DataTable table in dataValue.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = !(column.ColumnName == "RT_PO") ? 0 : 1;
                    if (column.ColumnName == "FG_POCON")
                        column.DefaultValue = "001";
                    if (column.ColumnName == "FG_POST")
                        column.DefaultValue = "O";
                }
            }
            DataTable table1 = dataValue.Tables[0];
            table1.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            table1.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            table1.Columns["NM_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptName;
            table1.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            table1.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            table1.Columns["DT_PO"].DefaultValue = Global.MainFrame.GetStringToday;
            table1.Columns["FG_TAXP"].DefaultValue = "001";
            table1.Columns["TP_PROCESS"].DefaultValue = "2";
            table1.Columns["YN_BUDGET"].DefaultValue = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000007") == "100" ? "Y" : "N";
            table1.Columns["BUDGET_PASS"].DefaultValue = "N";
            dataValue.Tables[1].Columns["DATE_USERDEF1"].DefaultValue = Global.MainFrame.GetStringToday;
            dataValue.Tables[1].Columns["DATE_USERDEF2"].DefaultValue = Global.MainFrame.GetStringToday;
            dataValue.Tables[1].Columns["YN_BUDGET"].DefaultValue = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000007") == "100" ? "Y" : "N";
            dataValue.Tables[1].Columns["BUDGET_PASS"].DefaultValue = "N";
            if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                dataValue.Tables[1].Columns.Add("QT_WINFOOD_OUT", typeof(decimal));
            if (MA.ServerKey(true, new string[1] { "KMI" }))
                dataValue.Tables[1].Columns.Add("QT_KMI", typeof(decimal));
            if (Global.MainFrame.ServerKeyCommon == "STRAFFIC")
            {
                table1.Columns["DC50_PO"].DefaultValue = "입고 시 거래명세서에 품명과 자재코드를 반드시 기입해 주십시오";
                table1.Columns["DC_RMK2"].DefaultValue = "세금계산서는 월말 전에 도착할 수 있도록 해 주십시오";
                table1.Columns["DC_RMK_TEXT"].DefaultValue = "\u2460 납품장소(직납) : 에스트래픽㈜, 경기도 성남시 중원구 둔촌대로 474 선텍시티 304호\r\n\u2461 납품지체 보상금 : 납기지체 1일당 계약금액의 3/1000 % 에 해당하는 금액을 물품 대금에서 공제 합니다.\r\n\u2462 대금청구 : 주문제품 전체에 대하여 상기 검수조건에 합격한 경우에 청구 가능 함.\r\n\u2463 대금지급조건 : 대금청구관련 서류접수 (접수마감일 : 매월 25일)후 발주사의 정기 지불일에 지급\r\n\u2464 대금청구시 제출서류 : 세금계산서, 거래명세서, 주문서 사본 및 제 증권\r\n\u2465 무상 하자보증기간: 검수조건에 합격한 날로부터 24 개월\r\n\u2466 제출서류 : \r\n\u2467 하자보수용 자재의 공급: 납품한 제품의 원할한 운영을 위하여 납품 완료로부터 5년 이상 소요 부품을 공급";
                table1.Columns["DC_RMK_TEXT2"].DefaultValue = "1) 주문 해지(해제): 공급사가 아래 사항에 해당하는 경우 본 주문의 전부 또는 일부를 해지(해제)할 수 있으며, \r\n　　  이 경우 이행보증금은 발주사에 귀속되며 이와 별도로 발주사가 입은 손해액에 대하여 공급사에 손해배상을 청구할 수 있음.\r\n　   가. 공급사가 정당한 이유없이 납품에 착수하지 아니할 때\r\n　   나. 공급사의 귀책사유로 납기 내에 납품 할 수 없음이 명백할 때\r\n　   다. 공급사가 허위자료를 제출 하였을 때\r\n　   라. 공급사가 관청 또는 제 3자에 의하여 영업취소, 허가취소, 영업정지, 가압류, 가처분등의 처분을 받았을 때\r\n　   마. 공급사가 파산, 화의, 회사정리 절차신청, 해산결의, 타 회사와 합병 하였을 때\r\n　   바. 공급사가 담합, 결탁, 뇌물 제공 등의 부정 행위를 하였을 때\r\n　   사. 공급사가 발주사의 사전 승인 없이 계 약사항을 재 위임 하였을 때\r\n　   아. 공급사가 발행, 배서, 보증한 유가증권의 부도 등 공급사의 신용에 위험이 있을 때\r\n　   자. 공급사가 본 계약 수행 중 발주사로 부터 취득한 각종 기밀을 제 3자에게 누설 하는 경우\r\n2) 주문 효력\r\n　　  발주서에 첨부된 공급자(공급사)의 견적서, 제안사항 및 특약사항 등도 계약의 효력을 가짐\r\n3) 이의 제기\r\n　　  발주서 내용에 이의가 있는 경우 접수 후 3일 이내 주문담당자에게 통보하여 재 협의 하여야 하며, 별도 통보가 없는 경우 합의한 것으로 간주함.";
            }
            return (DataSet)resultData.DataValue;
        }

        public DataSet ItemInfo_Search(object[] m_obj)
        {
            ResultData resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_PO_ITEMINFO_SELECT", m_obj);
            DataSet dataValue = (DataSet)resultData.DataValue;
            if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD") && dataValue.Tables[1] != null && dataValue.Tables[1].Rows.Count > 1)
            {
                dataValue.Tables[1].Rows[0]["UM_ITEM"] = 0;
                dataValue.Tables[1].Rows[0]["NM_FG_UM"] = "";
            }
            return (DataSet)resultData.DataValue;
        }

        public DataTable GetItemSerial(object[] obj) => (Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_PO_ITEMINFO_SERIAL_SELECT",
            SpParamsSelect = obj
        }) as ResultData).DataValue as DataTable;

        public DataTable SearchDetail(string strCD_PLANT, string strNO_PO, decimal dNO_LINE)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_PU_PO_REG_SELECT_L",
                SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                strCD_PLANT,
                                                strNO_PO,   
                                                dNO_LINE,
                                                this.Lang }
            })).DataValue;
            dataValue.Columns["CHK"].DefaultValue = 'N';
            foreach (DataColumn column in dataValue.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataValue;
        }

        public bool Delete(string NO_PO, string _text_sub)
        {
            Global.MainFrame.ExecSp("UP_PU_POH_DELETE", new object[] { NO_PO,
                                                                       Global.MainFrame.LoginInfo.CompanyCode,
                                                                       Global.MainFrame.LoginInfo.UserID,
                                                                       _text_sub });
            return true;
        }

        public bool Save(
          DataTable dtH,
          DataTable dtL,
          bool lb_RcvSave,
          DataTable dt_RCVH,
          DataTable dt_RCVL,
          string 구분,
          DataTable dt_budget,
          string 구매발주번호,
          SpInfo si_subinfo,
          bool lb_RevSave,
          DataTable dtDD,
          DataTable dtTH,
          DataTable dtIV,
          DataTable dtLOT,
          string strNOIO,
          DataTable dtSERL)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dtH != null)
            {
                SpInfo spInfo = new SpInfo();
                if (구분 == "COPY")
                    spInfo.DataState = DataValueState.Added;
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "UP_PU_POH_I";
                spInfo.SpNameUpdate = "UP_PU_POH_UPDATE";
                spInfo.SpParamsInsert = new string[] { "NO_PO",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "CD_PARTNER",
                                                       "DT_PO",
                                                       "CD_PURGRP",
                                                       "NO_EMP",
                                                       "CD_TPPO",
                                                       "FG_UM",
                                                       "FG_PAYMENT",
                                                       "FG_TAX",
                                                       "TP_UM_TAX",
                                                       "CD_PJT",
                                                       "CD_EXCH",
                                                       "RT_EXCH",
                                                       "AM_EX",
                                                       "AM",
                                                       "VAT",
                                                       "DC50_PO",
                                                       "TP_PROCESS",
                                                       "FG_TAXP",
                                                       "YN_AM",
                                                       "DTS_INSERT",
                                                       "ID_USER",
                                                       "FG_TRANS",
                                                       "FG_TRACK",
                                                       "DC_RMK2",
                                                       "TP_TRANSPORT",
                                                       "COND_PAY",
                                                       "COND_PAY_DLV",
                                                       "COND_PRICE",
                                                       "ARRIVER",
                                                       "LOADING",
                                                       "YN_BUDGET",
                                                       "BUDGET_PASS",
                                                       "COND_PRICE_DLV",
                                                       "CD_ARRIVER",
                                                       "CD_LOADING",
                                                       "DC_RMK_TEXT",
                                                       "CD_AGENCY",
                                                       "AM_NEGO",
                                                       "COND_SHIPMENT",
                                                       "FREIGHT_CHARGE",
                                                       "DC_RMK_TEXT2",
                                                       "STND_PAY",
                                                       "COND_DAYS",
                                                       "CD_ORGIN",
                                                       "DELIVERY_TERMS",
                                                       "DELIVERY_TIME",
                                                       "VALIDITY",
                                                       "TP_PACKING",
                                                       "DELIVERY_COST",
                                                       "INSPECTION",
                                                       "DOCUMENT_REQUIRED",
                                                       "SUPPLIER",
                                                       "MANUFACTURER",
                                                       "NO_ORDER",
                                                       "NM_PACKING",
                                                       "SHIP_DATE",
                                                       "DACU_NO",
                                                       "TP_GR",
                                                       "DT_PROCESS_IV",
                                                       "DT_PAY_PRE_IV",
                                                       "DT_DUE_IV",
                                                       "FG_PAYBILL_IV",
                                                       "CD_DOCU_IV",
                                                       "AM_K_IV",
                                                       "VAT_TAX_IV",
                                                       "AM_EX_IV",
                                                       "TXT_USERDEF4",
                                                       "DC_RMK_IV",
                                                       "CD_USERDEF1",
                                                       "CD_USERDEF2",
                                                       "CD_USERDEF3",
                                                       "CD_USERDEF4",
                                                       "CD_BIZAREA_TAX",
                                                       "TXT_USERDEF3" };
                spInfo.SpParamsUpdate = new string[] { "NO_PO",
                                                       "CD_COMPANY",
                                                       "DT_PO",
                                                       "CD_PURGRP",
                                                       "NO_EMP",
                                                       "CD_PJT",
                                                       "AM_EX",
                                                       "AM",
                                                       "VAT",
                                                       "DC50_PO",
                                                       "FG_TAXP",
                                                       "DTS_INSERT",
                                                       "ID_USER",
                                                       "DC_RMK2",
                                                       "TP_TRANSPORT",
                                                       "COND_PAY",
                                                       "COND_PAY_DLV",
                                                       "COND_PRICE",
                                                       "ARRIVER",
                                                       "LOADING",
                                                       "YN_BUDGET",
                                                       "BUDGET_PASS",
                                                       "COND_PRICE_DLV",
                                                       "CD_ARRIVER",
                                                       "CD_LOADING",
                                                       "DC_RMK_TEXT",
                                                       "FG_PAYMENT",
                                                       "COND_SHIPMENT",
                                                       "FREIGHT_CHARGE",
                                                       "DC_RMK_TEXT2",
                                                       "STND_PAY",
                                                       "COND_DAYS",
                                                       "CD_ORGIN",
                                                       "DELIVERY_TERMS",
                                                       "DELIVERY_TIME",
                                                       "VALIDITY",
                                                       "TP_PACKING",
                                                       "DELIVERY_COST",
                                                       "INSPECTION",
                                                       "DOCUMENT_REQUIRED",
                                                       "SUPPLIER",
                                                       "MANUFACTURER",
                                                       "NO_ORDER",
                                                       "NM_PACKING",
                                                       "SHIP_DATE",
                                                       "DACU_NO",
                                                       "CD_USERDEF1",
                                                       "CD_USERDEF2",
                                                       "CD_USERDEF3",
                                                       "CD_USERDEF4",
                                                       "TXT_USERDEF3" };
                spInfoCollection.Add(spInfo);
                spInfo.SpParamsValues.Add(ActionState.Insert, "ID_USER", Global.MainFrame.LoginInfo.UserID);
                spInfo.SpParamsValues.Add(ActionState.Update, "ID_USER", Global.MainFrame.LoginInfo.UserID);
            }
            if (dtL != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "UP_PU_POL_INSERT";
                spInfo.SpNameUpdate = "UP_PU_POL_UPDATE";
                spInfo.SpNameDelete = "UP_PU_POL_DELETE";
                spInfo.SpParamsInsert = new string[] { "NO_PO",
                                                       "NO_LINE",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_CONTRACT",
                                                       "NO_CTLINE",
                                                       "NO_PR",
                                                       "NO_PRLINE",
                                                       "FG_TRANS",
                                                       "CD_ITEM",
                                                       "CD_UNIT_MM",
                                                       "FG_RCV",
                                                       "FG_PURCHASE",
                                                       "DT_LIMIT",
                                                       "QT_PO_MM",
                                                       "QT_PO",
                                                       "QT_REQ",
                                                       "QT_RCV",
                                                       "FG_TAX",
                                                       "UM_EX_PO",
                                                       "UM_EX",
                                                       "AM_EX",
                                                       "UM",
                                                       "AM",
                                                       "VAT",
                                                       "CD_SL",
                                                       "FG_POST",
                                                       "FG_POCON",
                                                       "YN_RCV",
                                                       "YN_AUTORCV",
                                                       "YN_RETURN",
                                                       "YN_ORDER",
                                                       "YN_SUBCON",
                                                       "YN_IMPORT",
                                                       "RT_PO",
                                                       "YN_REQ",
                                                       "CD_PJT",
                                                       "NO_APP",
                                                       "NO_APPLINE",
                                                       "DC1",
                                                       "DC2",
                                                       "SEQ_PROJECT",
                                                       "UMVAT_PO",
                                                       "AMVAT_PO",
                                                       "CD_CC",
                                                       "CD_BUDGET",
                                                       "CD_BGACCT",
                                                       "NO_SO",
                                                       "NO_SOLINE",
                                                       "GI_PARTNER",
                                                       "DC3",
                                                       "DUMMY_TEMP_P_NO_VMI",
                                                       "DUMMY_TEMP_P_SEQ_VMI",
                                                       "DUMMY_TEMP_P_NO_IO_MGMT",
                                                       "DUMMY_TEMP_P_NO_IOLINE_MGMT",
                                                       "DT_PLAN",
                                                       "DC4",
                                                       "UM_EX_AR",
                                                       "NO_WBS",
                                                       "NO_CBS",
                                                       "CD_USERDEF1",
                                                       "CD_USERDEF2",
                                                       "NM_USERDEF1",
                                                       "NM_USERDEF2",
                                                       "DUMMY_TEMP_NO_PREIV",
                                                       "DUMMY_TEMP_NO_PREIVLINE",
                                                       "TP_UM_TAX",
                                                       "DT_EXDATE",
                                                       "CD_ITEM_ORIGIN",
                                                       "AM_EX_TRANS",
                                                       "AM_TRANS",
                                                       "NO_LINE_PJTBOM",
                                                       "FG_PACKING",
                                                       "NUM_USERDEF1",
                                                       "AM_REBATE_EX",
                                                       "AM_REBATE",
                                                       "UM_REBATE",
                                                       "ID_INSERT",
                                                       "DATE_USERDEF1",
                                                       "DATE_USERDEF2",
                                                       "TXT_USERDEF1",
                                                       "TXT_USERDEF2",
                                                       "CDSL_USERDEF1",
                                                       "NUM_USERDEF2",
                                                       "CLS_L",
                                                       "CLS_M",
                                                       "CLS_S",
                                                       "GRP_ITEM",
                                                       "NUM_STND_ITEM_1",
                                                       "NUM_STND_ITEM_2",
                                                       "NUM_STND_ITEM_3",
                                                       "NUM_STND_ITEM_4",
                                                       "NUM_STND_ITEM_5",
                                                       "UM_WEIGHT",
                                                       "WEIGHT",
                                                       "TOT_WEIGHT",
                                                       "STND_ITEM",
                                                       "NO_RELATION",
                                                       "SEQ_RELATION",
                                                       "NUM_USERDEF3_PO",
                                                       "NUM_USERDEF4_PO",
                                                       "NUM_USERDEF5_PO",
                                                       "NO_QUO",
                                                       "NO_QUOLINE",
                                                       "CD_BIZPLAN",
                                                       "CD_USERDEF3_PO",
                                                       "CD_USERDEF4_PO",
                                                       "NM_USERDEF3_PO",
                                                       "NM_USERDEF4_PO",
                                                       "YN_BUDGET",
                                                       "BUDGET_PASS",
                                                       "NO_IO",
                                                       "UNIT_IM",
                                                       "NM_ITEM",
                                                       "PAGE_ID",
                                                       "CD_ACCT",
                                                       "NM_USERDEF5",
                                                       "STND_DETAIL_ITEM",
                                                       "MAT_ITEM",
                                                       "DC50_PO" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_VMI", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_SEQ_VMI", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_IO_MGMT", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_IOLINE_MGMT", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_PREIV", string.Empty);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_PREIVLINE", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_IO", strNOIO);
                spInfo.SpParamsValues.Add(ActionState.Insert, "PAGE_ID", Global.MainFrame.CurrentPageID);
                spInfo.SpParamsUpdate = new string[] { "NO_PO",
                                                       "NO_LINE",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "CD_UNIT_MM",
                                                       "DT_LIMIT",
                                                       "QT_PO_MM",
                                                       "QT_PO",
                                                       "UM_EX_PO",
                                                       "UM_EX",
                                                       "AM_EX",
                                                       "UM",
                                                       "AM",
                                                       "VAT",
                                                       "CD_SL",
                                                       "RT_PO",
                                                       "CD_PJT",
                                                       "DC1",
                                                       "DC2",
                                                       "UMVAT_PO",
                                                       "AMVAT_PO",
                                                       "CD_CC",
                                                       "CD_BUDGET",
                                                       "CD_BGACCT",
                                                       "GI_PARTNER",
                                                       "DC3",
                                                       "DT_PLAN",
                                                       "DC4",
                                                       "UM_EX_AR",
                                                       "NO_WBS",
                                                       "NO_CBS",
                                                       "CD_ITEM",
                                                       "CD_USERDEF1",
                                                       "CD_USERDEF2",
                                                       "NM_USERDEF1",
                                                       "NM_USERDEF2",
                                                       "TP_UM_TAX",
                                                       "DT_EXDATE",
                                                       "CD_ITEM_ORIGIN",
                                                       "AM_EX_TRANS",
                                                       "AM_TRANS",
                                                       "NO_LINE_PJTBOM",
                                                       "FG_PACKING",
                                                       "NUM_USERDEF1",
                                                       "AM_REBATE_EX",
                                                       "AM_REBATE",
                                                       "UM_REBATE",
                                                       "ID_INSERT",
                                                       "DATE_USERDEF1",
                                                       "DATE_USERDEF2",
                                                       "TXT_USERDEF1",
                                                       "TXT_USERDEF2",
                                                       "CDSL_USERDEF1",
                                                       "FG_TAX",
                                                       "NUM_USERDEF2",
                                                       "CLS_L",
                                                       "CLS_M",
                                                       "CLS_S",
                                                       "GRP_ITEM",
                                                       "NUM_STND_ITEM_1",
                                                       "NUM_STND_ITEM_2",
                                                       "NUM_STND_ITEM_3",
                                                       "NUM_STND_ITEM_4",
                                                       "NUM_STND_ITEM_5",
                                                       "UM_WEIGHT",
                                                       "WEIGHT",
                                                       "TOT_WEIGHT",
                                                       "STND_ITEM",
                                                       "NO_RELATION",
                                                       "SEQ_RELATION",
                                                       "NUM_USERDEF3_PO",
                                                       "NUM_USERDEF4_PO",
                                                       "NUM_USERDEF5_PO",
                                                       "CD_BIZPLAN",
                                                       "CD_USERDEF3_PO",
                                                       "CD_USERDEF4_PO",
                                                       "NM_USERDEF3_PO",
                                                       "NM_USERDEF4_PO",
                                                       "CD_ACCT",
                                                       "NM_USERDEF5",
                                                       "DC50_PO",
                                                       "SEQ_PROJECT" };
                spInfo.SpParamsDelete = new string[] { "NO_PO",
                                                       "NO_LINE",
                                                       "CD_COMPANY" };
                spInfoCollection.Add(spInfo);
            }
            if (lb_RevSave && dtL != null && dtH != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataState = DataValueState.Added,
                    DataValue = dtH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_REV_INSERT_AUTO",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_PO",
                                                    "ID_INSERT",
                                                    "DT_PO",
                                                    "CD_PARTNER",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "NO_EMP" }
                });
            if (lb_RcvSave && dt_RCVH != null && dt_RCVL != null && dt_RCVH.Rows.Count > 0 && dt_RCVL.Rows.Count > 0)
            {
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dt_RCVH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_RCVH_INSERT",
                    SpNameUpdate = "UP_PU_RCVH_UPDATE",
                    SpParamsInsert = new string[] { "NO_RCV",
                                                    "CD_COMPANY",
                                                      "CD_PLANT",
                                                      "CD_PARTNER",
                                                      "DT_REQ",
                                                      "NO_EMP",
                                                      "FG_TRANS",
                                                      "FG_PROCESS",
                                                      "CD_EXCH",
                                                      "CD_SL",
                                                      "YN_RETURN",
                                                      "YN_AM",
                                                      "DC_RMK",
                                                      "ID_INSERT",
                                                      "FG_RCV" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_RCV",
                                                    "DC_RMK",
                                                    "ID_INSERT" }
                });
                SpInfo spInfo1 = new SpInfo();
                spInfo1.DataValue = dt_RCVL;
                spInfo1.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo1.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo1.SpNameInsert = "UP_PU_REQ_INSERT";
                spInfo1.SpParamsInsert = new string[] { "NO_RCV",
                                                        "NO_LINE",
                                                        "CD_COMPANY",
                                                        "NO_PO",
                                                        "NO_POLINE",
                                                        "CD_PURGRP",
                                                        "DT_LIMIT",
                                                        "CD_ITEM",
                                                        "QT_REQ",
                                                        "YN_INSP",
                                                        "QT_PASS",
                                                        "QT_REJECTION",
                                                        "CD_UNIT_MM",
                                                        "QT_REQ_MM",
                                                        "CD_EXCH",
                                                        "RT_EXCH",
                                                        "UM_EX_PO",
                                                        "UM_EX",
                                                        "AM_EX",
                                                        "UM",
                                                        "AM",
                                                        "VAT",
                                                        "RT_CUSTOMS",
                                                        "CD_PJT",
                                                        "YN_PURCHASE",
                                                        "YN_RETURN",
                                                        "FG_TPPURCHASE",
                                                        "FG_RCV",
                                                        "FG_TRANS",
                                                        "FG_TAX",
                                                        "FG_TAXP",
                                                        "YN_AUTORCV",
                                                        "YN_REQ",
                                                        "CD_SL",
                                                        "NO_LC",
                                                        "NO_LCLINE",
                                                        "RT_SPEC",
                                                        "NO_EMP",
                                                        "NO_TO",
                                                        "NO_TO_LINE",
                                                        "CD_PLANT",
                                                        "CD_PARTNER",
                                                        "DT_REQ",
                                                        "DC_RMK",
                                                        "TP_UM_TAX" };
                spInfo1.SpParamsValues.Add(ActionState.Insert, "DC_RMK", string.Empty);
                spInfoCollection.Add(spInfo1);
                if (dt_RCVH.Rows[0]["YN_AUTORCV"].ToString() == "Y" && dt_RCVH.Rows[0].RowState.ToString() == "Added")
                {
                    string seq = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PU", "06", dtH.Rows[0]["DT_PO"].ToString().Substring(0, 6));
                    SpInfo spInfo2 = new SpInfo();
                    spInfo2.DataValue = dt_RCVH;
                    spInfo2.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    spInfo2.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    spInfo2.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";
                    spInfo2.SpParamsInsert = new string[] { "NO_IO1",
                                                            "CD_COMPANY",
                                                            "CD_PLANT",
                                                            "CD_PARTNER",
                                                            "FG_TRANS",
                                                            "YN_RETURN",
                                                            "DT_IO",
                                                            "GI_PARTNER",
                                                            "CD_DEPT",
                                                            "NO_EMP",
                                                            "DC_RMK",
                                                            "ID_INSERT",
                                                            "FG_RCV" };
                    spInfo2.SpParamsValues.Add(ActionState.Insert, "NO_IO1", seq);
                    spInfoCollection.Add(spInfo2);
                    SpInfo spInfo3 = new SpInfo();
                    spInfo3.DataValue = dt_RCVL;
                    spInfo3.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    spInfo3.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    spInfo3.SpNameInsert = "UP_PU_GR_INSERT";
                    spInfo3.SpParamsInsert = new string[] { "YN_RETURN",
                                                            "NO_IO1",
                                                            "NO_LINE",
                                                            "CD_COMPANY",
                                                            "CD_PLANT",
                                                            "CD_SL",
                                                            "DT_IO",
                                                            "NO_RCV",
                                                            "NO_LINE",
                                                            "NO_PO",
                                                            "NO_POLINE",
                                                            "FG_PS1",
                                                            "FG_TPPURCHASE",
                                                            "FG_IO1",
                                                            "FG_RCV",
                                                            "FG_TRANS",
                                                            "FG_TAX",
                                                            "CD_PARTNER",
                                                            "CD_ITEM",
                                                            "QT_REQ",
                                                            "QT_BAD1",
                                                            "CD_EXCH",
                                                            "RT_EXCH",
                                                            "UM_EX",
                                                            "UM",
                                                            "VAT",
                                                            "FG_TAXP",
                                                            "YN_AM1",
                                                            "CD_PJT",
                                                            "NO_LC",
                                                            "NO_LCLINE",
                                                            "NO_EMP1",
                                                            "CD_PURGRP",
                                                            "CD_UNIT_MM",
                                                            "QT_REQ_MM",
                                                            "QT_BAD_MM1",
                                                            "UM_EX_PO",
                                                            "YN_INSP",
                                                            "TP_UM_TAX" };
                    spInfo3.SpParamsValues.Add(ActionState.Insert, "YN_AM1", dtH.Rows[0]["YN_AM"].ToString());
                    spInfo3.SpParamsValues.Add(ActionState.Insert, "NO_IO1", seq);
                    spInfo3.SpParamsValues.Add(ActionState.Insert, "FG_PS1", "1");
                    spInfo3.SpParamsValues.Add(ActionState.Insert, "FG_IO1", "001");
                    spInfo3.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", dtH.Rows[0]["NO_EMP"].ToString());
                    spInfo3.SpParamsValues.Add(ActionState.Insert, "QT_BAD1", 0);
                    spInfo3.SpParamsValues.Add(ActionState.Insert, "QT_BAD_MM1", 0);
                    spInfoCollection.Add(spInfo3);
                }
            }
            Global.MainFrame.ExecSp("UP_PU_BUDGET_HST_DELETE", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              구매발주번호,
                                                                              "PU_PO_REG" });
            if (dt_budget != null && dt_budget.Rows.Count > 0)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dt_budget,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PU_BUDGET_HST_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_PU",
                                                    "NENU_TYPE",
                                                    "CD_BUDGET",
                                                    "NM_BUDGET",
                                                    "CD_BGACCT",
                                                    "NM_BGACCT",
                                                    "AM_ACTSUM",
                                                    "AM_JSUM",
                                                    "RT_JSUM",
                                                    "AM",
                                                    "AM_JAN",
                                                    "TP_BUNIT",
                                                    "ERROR_MSG",
                                                    "ID_INSERT" }
                });
            if (si_subinfo.DataValue != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataState = DataValueState.Added;
                spInfo.DataValue = si_subinfo.DataValue;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpNameInsert = si_subinfo.SpNameInsert;
                spInfo.SpParamsInsert = si_subinfo.SpParamsInsert;
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_PK", 구매발주번호);
                spInfoCollection.Add(spInfo);
            }
            if (dtDD != null)
            {
                SpInfo spInfo = new SpInfo();
                if (구분 == "COPY")
                    spInfo.DataState = DataValueState.Added;
                spInfo.DataValue = dtDD;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_PU_POLL_INSERT";
                spInfo.SpNameUpdate = "UP_PU_POLL_UPDATE";
                spInfo.SpNameDelete = "UP_PU_POLL_DELETE";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PO",
                                                       "NO_POLINE",
                                                       "NO_LINE",
                                                       "CD_MATL",
                                                       "QT_NEED",
                                                       "QT_NEED_UNIT",
                                                       "ID_INSERT",
                                                       "NO_RELATION",
                                                       "SEQ_RELATION",
                                                       "TXT_USERDEF1",
                                                       "NUM_USERDEF1",
                                                       "TXT_USERDEF2",
                                                       "TXT_USERDEF3",
                                                       "TXT_USERDEF4",
                                                       "TXT_USERDEF5",
                                                       "NUM_USERDEF2"
                };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PO",
                                                       "NO_POLINE",
                                                       "NO_LINE",
                                                       "CD_MATL",
                                                       "QT_NEED",
                                                       "QT_NEED_UNIT",
                                                       "ID_UPDATE",
                                                       "NO_RELATION",
                                                       "SEQ_RELATION",
                                                       "TXT_USERDEF1",
                                                       "NUM_USERDEF1",
                                                       "TXT_USERDEF2",
                                                       "TXT_USERDEF3",
                                                       "TXT_USERDEF4",
                                                       "TXT_USERDEF5",
                                                       "NUM_USERDEF2" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PO",
                                                       "NO_POLINE",
                                                       "NO_LINE",
                                                       "NO_RELATION",
                                                       "SEQ_RELATION",
                                                       "NUM_USERDEF1",
                                                       "QT_NEED",
                                                       "TXT_USERDEF1" };
                spInfoCollection.Add(spInfo);
            }
            if (dtTH != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtTH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpNameInsert = "UP_PU_Z_TOPES_PO_TAB_I";
                spInfo.SpNameUpdate = "UP_PU_Z_TOPES_PO_TAB_U";
                spInfo.SpNameDelete = "UP_PU_Z_TOPES_PO_TAB_D";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PO",
                                                       "SQ_1",
                                                       "FG_IV",
                                                       "DT_IV_PLAN",
                                                       "RT_IV",
                                                       "AM",
                                                       "VAT",
                                                       "DT_BAN_PLAN",
                                                       "RT_BAN",
                                                       "AM_BAN",
                                                       "AM_BANK",
                                                       "SERVERKEY",
                                                       "ID" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PO",
                                                       "SQ_1",
                                                       "FG_IV",
                                                       "DT_IV_PLAN",
                                                       "RT_IV",
                                                       "AM",
                                                       "VAT",
                                                       "DT_BAN_PLAN",
                                                       "RT_BAN",
                                                       "AM_BAN",
                                                       "AM_BANK",
                                                       "SERVERKEY",
                                                       "ID" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_PO",
                                                       "SQ_1",
                                                       "SERVERKEY" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "SERVERKEY", Global.MainFrame.ServerKeyCommon);
                spInfo.SpParamsValues.Add(ActionState.Update, "SERVERKEY", Global.MainFrame.ServerKeyCommon);
                spInfo.SpParamsValues.Add(ActionState.Delete, "SERVERKEY", Global.MainFrame.ServerKeyCommon);
                spInfo.SpParamsValues.Add(ActionState.Insert, "ID", Global.MainFrame.LoginInfo.UserID);
                spInfo.SpParamsValues.Add(ActionState.Update, "ID", Global.MainFrame.LoginInfo.UserID);
                spInfoCollection.Add(spInfo);
            }
            if (dtIV != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtIV,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = "UP_PU_PBL_INSERT",
                    SpNameUpdate = "UP_PU_PBL_UPDATE",
                    SpNameDelete = "UP_PU_PBL_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "NO_SEQ",
                                                    "FG_IV",
                                                    "DT_PUR_PLAN",
                                                    "RT_IV",
                                                    "AM",
                                                    "AM_K",
                                                    "AM_VAT",
                                                    "AM_SUM",
                                                    "AM_PUL",
                                                    "NM_TEXT",
                                                    "CD_PJT",
                                                    "SEQ_PROJECT",
                                                    "NM_TEXT2",
                                                    "ID_INSERT"
                  },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "NO_SEQ",
                                                    "FG_IV",
                                                    "DT_PUR_PLAN",
                                                    "RT_IV",
                                                    "AM",
                                                    "AM_K",
                                                    "AM_VAT",
                                                    "AM_SUM",
                                                    "AM_PUL",
                                                    "NM_TEXT",
                                                    "CD_PJT",
                                                    "SEQ_PROJECT",
                                                    "NM_TEXT2",
                                                    "ID_INSERT" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "NO_SEQ" }
                });
            if (dtLOT != null && dtLOT.Rows.Count > 0)
            {
                if (D.GetString(dtH.Rows[0]["YN_RETURN"]) == "Y")
                {
                    SpInfo spInfo = new SpInfo();
                    spInfo.DataValue = dtLOT;
                    spInfo.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                    spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                           "출고번호",
                                                           "출고항번",
                                                           "NO_LOT",
                                                           "CD_ITEM",
                                                           "출고일",
                                                           "FG_PS",
                                                           "수불구분",
                                                           "수불형태",
                                                           "창고코드",
                                                           "QT_GOOD_MNG",
                                                           "NO_IO",
                                                           "NO_IOLINE",
                                                           "NO_IOLINE2",
                                                           "YN_RETURN_QTIO",
                                                           "CD_PLANT_PR",
                                                           "NO_IO_PR",
                                                           "NO_LINE_IO_PR",
                                                           "NO_LINE_IO2_PR",
                                                           "FG_SLIP_PR",
                                                           "NO_LOT_PR",
                                                           "P_NO_SO",
                                                           "DT_LIMIT",
                                                           "DC_LOTRMK",
                                                           "P_CD_PLANT",
                                                           "P_ROOT_NO_LOT",
                                                           "P_ID_INSERT",
                                                           "P_BEF_NO_LOT",
                                                           "P_FG_LOT_ADD",
                                                           "P_BARCODE",
                                                           "CD_MNG1",
                                                           "CD_MNG2",
                                                           "CD_MNG3",
                                                           "CD_MNG4",
                                                           "CD_MNG5",
                                                           "CD_MNG6",
                                                           "CD_MNG7",
                                                           "CD_MNG8",
                                                           "CD_MNG9",
                                                           "CD_MNG10",
                                                           "CD_MNG11",
                                                           "CD_MNG12",
                                                           "CD_MNG13",
                                                           "CD_MNG14",
                                                           "CD_MNG15",
                                                           "CD_MNG16",
                                                           "CD_MNG17",
                                                           "CD_MNG18",
                                                           "CD_MNG19",
                                                           "CD_MNG20" };
                    spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                           "출고번호",
                                                           "출고항번",
                                                           "NO_IOLINE2",
                                                           "NO_LOT" };
                    spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN_QTIO", "Y");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "CD_PLANT_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_IO_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO_PR", 0);
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO2_PR", 0);
                    spInfo.SpParamsValues.Add(ActionState.Insert, "FG_SLIP_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LOT_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_NO_SO", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_CD_PLANT", D.GetString(dtH.Rows[0]["CD_PLANT"]));
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_ROOT_NO_LOT", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_ID_INSERT", Global.MainFrame.LoginInfo.UserID);
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_BEF_NO_LOT", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_FG_LOT_ADD", "N");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_BARCODE", "");
                    spInfoCollection.Add(spInfo);
                }
                else
                {
                    SpInfo spInfo = new SpInfo();
                    spInfo.DataValue = dtLOT;
                    spInfo.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                    spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                           "NO_IO",
                                                           "NO_IOLINE",
                                                           "NO_LOT",
                                                           "CD_ITEM",
                                                           "DT_IO",
                                                           "FG_PS",
                                                           "FG_IO",
                                                           "CD_QTIOTP",
                                                           "CD_SL",
                                                           "QT_IO",
                                                           "NO_IO",
                                                           "NO_IOLINE",
                                                           "NO_IOLINE2",
                                                           "YN_RETURN",
                                                           "CD_PLANT_PR",
                                                           "NO_IO_PR",
                                                           "NO_LINE_IO_PR",
                                                           "NO_LINE_IO2_PR",
                                                           "FG_SLIP_PR",
                                                           "NO_LOT_PR",
                                                           "P_NO_SO",
                                                           "DT_LIMIT",
                                                           "DC_LOTRMK",
                                                           "P_CD_PLANT",
                                                           "P_ROOT_NO_LOT",
                                                           "P_ID_INSERT",
                                                           "P_BEF_NO_LOT",
                                                           "P_FG_LOT_ADD",
                                                           "P_BARCODE",
                                                           "CD_MNG1",
                                                           "CD_MNG2",
                                                           "CD_MNG3",
                                                           "CD_MNG4",
                                                           "CD_MNG5",
                                                           "CD_MNG6",
                                                           "CD_MNG7",
                                                           "CD_MNG8",
                                                           "CD_MNG9",
                                                           "CD_MNG10",
                                                           "CD_MNG11",
                                                           "CD_MNG12",
                                                           "CD_MNG13",
                                                           "CD_MNG14",
                                                           "CD_MNG15",
                                                           "CD_MNG16",
                                                           "CD_MNG17",
                                                           "CD_MNG18",
                                                           "CD_MNG19",
                                                           "CD_MNG20" };
                    spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "N");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "CD_PLANT_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_IO_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO_PR", 0);
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO2_PR", 0);
                    spInfo.SpParamsValues.Add(ActionState.Insert, "FG_SLIP_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LOT_PR", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_NO_SO", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_CD_PLANT", D.GetString(dtH.Rows[0]["CD_PLANT"]));
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_ROOT_NO_LOT", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_ID_INSERT", Global.MainFrame.LoginInfo.UserID);
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_BEF_NO_LOT", "");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_FG_LOT_ADD", "N");
                    spInfo.SpParamsValues.Add(ActionState.Insert, "P_BARCODE", "");
                    spInfoCollection.Add(spInfo);
                }
            }
            if (dtSERL != null && dtSERL.Rows.Count > 0 && dtSERL != null && dtSERL.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtSERL;
                spInfo.SpNameInsert = "UP_MM_QTIODS_INSERT";
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_SERIAL",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_ITEM",
                                                       "CD_QTIOTP",
                                                       "FG_IO",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20",
                                                       "CD_PLANT",
                                                       "ID_INSERT" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", dtL.Rows[0]["CD_PLANT"].ToString());
                spInfo.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", Global.MainFrame.LoginInfo.UserID);
                spInfoCollection.Add(spInfo);
            }
            int num;
            if (dtH != null && dtL != null)
                num = !MA.ServerKey(false, new string[] { "SATREC" }) ? 1 : 0;
            else
                num = 1;
            if (num == 0)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    SpNameInsert = "UP_PU_Z_SATREC_PO_MAIL_SEND",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_PO" }
                });
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        internal DataTable 공장품목(string 멀티품목코드, string 공장)
        {
            ArrayListExt arrayListExt = this.arr엑셀(멀티품목코드);
            DataTable dataTable = null;
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
                {
                    SpNameSelect = "UP_PU_PO_EXCEL_SELECT",
                    SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                    공장,
                                                    arrayListExt[index].ToString() }
                })).DataValue;
                if (dataTable == null)
                {
                    dataTable = dataValue;
                }
                else
                {
                    foreach (DataRow row in dataValue.Rows)
                        dataTable.ImportRow(row);
                }
            }
            return dataTable;
        }

        internal DataTable 엑셀(DataTable dt_엑셀)
        {
            if (!dt_엑셀.Columns.Contains("NM_ITEM"))
            {
                dt_엑셀.Columns.Add("NM_ITEM", typeof(string));
                if (Global.MainFrame.ServerKeyCommon != "TANHAY")
                    dt_엑셀.Columns.Add("STND_ITEM", typeof(string));
                dt_엑셀.Columns.Add("UNIT_PO", typeof(string));
                dt_엑셀.Columns.Add("UNIT_IM", typeof(string));
                dt_엑셀.Columns.Add("NO_PO", typeof(string));
                dt_엑셀.Columns.Add("NO_LINE", typeof(string));
                dt_엑셀.Columns.Add("NM_PJT", typeof(string));
                dt_엑셀.Columns.Add("NM_SYSDEF", typeof(string));
                if (!dt_엑셀.Columns.Contains("UM_EX_PO"))
                    dt_엑셀.Columns.Add("UM_EX_PO", typeof(string));
                if (!dt_엑셀.Columns.Contains("AM_EX"))
                    dt_엑셀.Columns.Add("AM_EX", typeof(decimal));
                dt_엑셀.Columns.Add("QT_PO", typeof(decimal));
                dt_엑셀.Columns.Add("FG_TRANS", typeof(string));
                dt_엑셀.Columns.Add("FG_TPPURCHASE", typeof(string));
                dt_엑셀.Columns.Add("YN_AUTORCV", typeof(string));
                dt_엑셀.Columns.Add("YN_RCV", typeof(string));
                dt_엑셀.Columns.Add("YN_RETURN", typeof(string));
                dt_엑셀.Columns.Add("YN_IMPORT", typeof(string));
                dt_엑셀.Columns.Add("YN_ORDER", typeof(string));
                dt_엑셀.Columns.Add("YN_REQ", typeof(string));
                dt_엑셀.Columns.Add("FG_RCV", typeof(string));
                dt_엑셀.Columns.Add("YN_SUBCON", typeof(string));
                dt_엑셀.Columns.Add("FG_PURCHASE", typeof(string));
                dt_엑셀.Columns.Add("FG_TAX", typeof(string));
                dt_엑셀.Columns.Add("NO_PR", typeof(string));
                dt_엑셀.Columns.Add("CD_EXCH", typeof(string));
                dt_엑셀.Columns.Add("CD_SL", typeof(string));
                dt_엑셀.Columns.Add("NM_SL", typeof(string));
                dt_엑셀.Columns.Add("TP_ITEM", typeof(string));
                dt_엑셀.Columns.Add("UNIT_PO_FACT", typeof(string));
                dt_엑셀.Columns.Add("NM_USERDEF1", typeof(string));
                dt_엑셀.Columns.Add("NM_USERDEF2", typeof(string));
            }
            return dt_엑셀;
        }

        private ArrayListExt arr엑셀(string 멀티품목코드)
        {
            int num1 = 50;
            int num2 = 1;
            string str = string.Empty;
            ArrayListExt arrayListExt = new ArrayListExt();
            string[] strArray = 멀티품목코드.Split('|');
            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = str + strArray[index].ToString() + "|";
                if (num2 == num1)
                {
                    arrayListExt.Add(str);
                    str = string.Empty;
                    num2 = 0;
                }
                ++num2;
            }
            if (str != string.Empty)
                arrayListExt.Add(str);
            return arrayListExt;
        }

        public int GetFI_GWDOCU(string p_no_po)
        {
            int fiGwdocu = 999;
            string str1 = Global.MainFrame.LoginInfo.CdPc;
            if (Global.MainFrame.ServerKeyCommon == "DAEYONG")
                str1 = "";
            string str2 = "SELECT ST_STAT  FROM FI_GWDOCU WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND CD_PC = '" + str1 + "'  AND NO_DOCU = '" + p_no_po + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(str2);
            if (dataTable.Rows.Count > 0 && dataTable.Rows.Count > 0)
                fiGwdocu = Convert.ToInt32(dataTable.Rows[0]["ST_STAT"].ToString());
            return fiGwdocu;
        }

        public DataTable GetDOCU(string p_no_po)
        {
            string str = "SELECT ST_STAT, APP_DT_END  FROM FI_GWDOCU WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND CD_PC = '" + Global.MainFrame.LoginInfo.CdPc + "'  AND NO_DOCU = '" + p_no_po + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable DataSearch_GW_RPT(object[] obj) => DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT", obj);

        public DataTable DataSearch_GW_RPT_ONLY(object[] obj)
        {
            DataTable dataTable;
            if (D.GetString(obj[4]) == "WONIK")
                dataTable = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_WONIK", new object[] { D.GetString(obj[0]),
                                                                                               D.GetString(obj[1]),
                                                                                               D.GetString(obj[2]),
                                                                                               D.GetString(obj[3]) });
            else if (D.GetString(obj[4]) == "SAMTECH")
                dataTable = DBHelper.GetDataTable("UP_PU_PO_REG_PRINT_SAMTECH", new object[] { D.GetString(obj[0]),
                                                                                               D.GetString(obj[1]) });
            else if (D.GetString(obj[4]) == "DONGWOON")
                dataTable = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_DONGWOON", new object[] { D.GetString(obj[0]),
                                                                                               D.GetString(obj[1]) });
            else if (D.GetString(obj[4]) == "YDGLS")
                dataTable = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_YDGLS", new object[] { D.GetString(obj[0]),
                                                                                               D.GetString(obj[1]) });
            else if (D.GetString(obj[4]) == "CODECOS")
                dataTable = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_CODECOS", new object[] { D.GetString(obj[0]),
                                                                                                 D.GetString(obj[1]) });
            else if (D.GetString(obj[4]) == "SEMI")
                dataTable = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_SEMI", new object[] { D.GetString(obj[0]),
                                                                                              D.GetString(obj[1]) });
            else if (D.GetString(obj[4]) == "ISE")
                dataTable = DBHelper.GetDataTable("UP_PU_Z_INNOST_PO_REG2_GW_RPT", new object[] { D.GetString(obj[0]),
                                                                                                  D.GetString(obj[1]) });
            else if (MA.ServerKey(false, new string[] { "PARATH" }))
                dataTable = DBHelper.GetDataTable("UP_PU_Z_PARATH_PO_REG2_GW_RPT", obj);
            else
                dataTable = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_SANTEC", new object[] { D.GetString(obj[0]),
                                                                                                D.GetString(obj[1]) });
            return dataTable;
        }

        public DataTable GetPartnerCodeSearch()
        {
            string str = " SELECT CD_EXC    FROM MA_EXC   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'    AND EXC_TITLE = '여신한도' ";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable GetBizAreaCodeSearch(string arg_no_emp)
        {
            string empty = string.Empty;
            if (arg_no_emp == string.Empty)
                return null;
            return DBHelper.GetDataTable("SELECT  B.NM_BIZAREA, B.NO_BIZAREA, B.NO_COMPANY   FROM DZSN_MA_EMP A       INNER JOIN DZSN_MA_BIZAREA B ON A.CD_BIZAREA = B.CD_BIZAREA AND A.CD_COMPANY = B.CD_COMPANY WHERE A.NO_EMP = '" + arg_no_emp + "'   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public decimal ExchangeSearch(string dt_po, string nm_exch)
        {
            decimal num = 1M;
            string str = "SELECT RATE_BASE   FROM MA_EXCHANGE  WHERE YYMMDD = '" + dt_po + "'    AND CURR_SOUR = '" + nm_exch + "'    AND CURR_DEST = '000'    AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' ";
            DataTable dataTable = Global.MainFrame.FillDataTable(str);
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["RATE_BASE"] != DBNull.Value && dataTable.Rows[0]["RATE_BASE"].ToString().Trim() != string.Empty)
                    num = Convert.ToDecimal(dataTable.Rows[0]["RATE_BASE"]);
                if (num == 0M)
                    num = 1M;
            }
            return num;
        }

        public string EnvSearch()
        {
            string str1 = "N";
            string str2 = "SELECT CD_TP   FROM PU_ENV  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'    AND FG_TP = '001' ";
            DataTable dataTable = Global.MainFrame.FillDataTable(str2);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["CD_TP"] != DBNull.Value && dataTable.Rows[0]["CD_TP"].ToString().Trim() != string.Empty)
                str1 = dataTable.Rows[0]["CD_TP"].ToString();
            return str1;
        }

        public string GetCCCodeSearch(string arg_cd_cc)
        {
            string ccCodeSearch = string.Empty;
            if (arg_cd_cc == string.Empty)
                return "";
            DataTable dataTable = DBHelper.GetDataTable("SELECT NM_CC   FROM DZSN_MA_CC  WHERE CD_CC = '" + arg_cd_cc + "'   AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["NM_CC"] != DBNull.Value && dataTable.Rows[0]["NM_CC"].ToString().Trim() != string.Empty)
                ccCodeSearch = dataTable.Rows[0]["NM_CC"].ToString().Trim();
            return ccCodeSearch;
        }

        public DataTable GetCD_CC_CodeSearch(string arg_cd_purgrp)
        {
            string empty = string.Empty;
            return DBHelper.GetDataTable("SELECT A.CD_CC, MC.NM_CC, A.NO_TEL, A.NO_FAX, A.E_MAIL   FROM DZSN_MA_PURGRP A        LEFT OUTER JOIN DZSN_MA_CC MC ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY WHERE A.CD_PURGRP = '" + arg_cd_purgrp + "'  AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public DataTable GetCD_CC_CodeSearch_pjt(string arg_cd_pjt)
        {
            string empty = string.Empty;
            return DBHelper.GetDataTable("SELECT A.CD_CC, MC.NM_CC   FROM DZSN_SA_PROJECTH A        LEFT OUTER JOIN DZSN_MA_CC MC ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY WHERE A.NO_PROJECT = '" + arg_cd_pjt + "'  AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public DataTable GetCD_CC_CodeSearch_grp_item(string str_cd_item, string str_cd_plant)
        {
            string empty = string.Empty;
            return DBHelper.GetDataTable("SELECT CC.CD_CC, CC.NM_CC   FROM DZSN_MA_PITEM A        LEFT OUTER JOIN DZSN_MA_ITEMGRP GP ON GP.CD_COMPANY = A.CD_COMPANY AND GP.CD_ITEMGRP = A.GRP_ITEM        LEFT OUTER JOIN FI_PUMOKCC KCC ON KCC.CD_COMPANY = A.CD_COMPANY AND KCC.CD_ITEMGRP = GP.CD_ITEMGRP        LEFT OUTER JOIN DZSN_MA_CC CC ON CC.CD_COMPANY = KCC.CD_COMPANY AND CC.CD_CC = KCC.CD_CC WHERE A.CD_ITEM = '" + str_cd_item + "'   AND A.CD_PLANT = '" + str_cd_plant + "'   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public DataTable GetCD_CC(string CD_PLANT, string KEY, string FLAG) => DBHelper.GetDataTable("UP_PU_CD_CC_SEARCH", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                          CD_PLANT,
                                                                                                                                          KEY,
                                                                                                                                          FLAG });

        public DataTable GetCD_CC_CodeSearch_cd_item(string str_cd_item, string str_cd_plant)
        {
            string empty = string.Empty;
            return DBHelper.GetDataTable("SELECT A.CD_CC, MC.NM_CC   FROM DZSN_MA_PITEM A        LEFT OUTER JOIN DZSN_MA_CC MC ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY WHERE A.CD_ITEM = '" + str_cd_item + "'   AND A.CD_PLANT = '" + str_cd_plant + "'   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
        }

        public DataTable EnvSearch_CC()
        {
            string empty = string.Empty;
            string str = "SELECT EXC_TITLE,CD_EXC   FROM MA_EXC  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'     AND(EXC_TITLE = '발주등록-C/C설정' OR  EXC_TITLE ='발주라인-C/C설정수정유무')    AND CD_MODULE = 'PU'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataSet Get_TPPO_PURGRP(object[] obj) => DBHelper.GetDataSet("UP_PU_TPPO_PURGRP_INFO_SELECT", obj);

        public string GetGubunCodeSearch(string p_cd_field, string p_cd_sysdef)
        {
            if (!this.b_first_clone)
            {
                this._dt_fg_post.Columns.Add("CD_FIELD", typeof(string));
                this._dt_fg_post.Columns.Add("CD_SYSDEF", typeof(string));
                this._dt_fg_post.Columns.Add("NM_SYSDEF", typeof(string));
                this.b_first_clone = true;
            }
            string empty = string.Empty;
            object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                               p_cd_field,
                                               p_cd_sysdef };
            DataRow[] dataRowArray = this._dt_fg_post.Select("CD_FIELD = '" + p_cd_field + "' AND CD_SYSDEF = '" + p_cd_sysdef + "'");
            if (dataRowArray.Length == 0)
            {
                foreach (DataRow row1 in ComFunc.GetTableSearch("MA_CODEDTL", objArray).Rows)
                {
                    DataRow row2 = this._dt_fg_post.NewRow();
                    row2["CD_FIELD"] = D.GetString(row1["CD_FIELD"]);
                    row2["CD_SYSDEF"] = D.GetString(row1["CD_SYSDEF"]);
                    row2["NM_SYSDEF"] = D.GetString(row1["NM_SYSDEF"]);
                    this._dt_fg_post.Rows.Add(row2);
                }
                dataRowArray = this._dt_fg_post.Select("CD_FIELD = '" + p_cd_field + "' AND CD_SYSDEF = '" + p_cd_sysdef + "'");
            }
            if (dataRowArray.Length > 0)
                empty = D.GetString(dataRowArray[0]["NM_SYSDEF"]);
            return empty;
        }

        public DataTable search_partner(string cd_partner) => DBHelper.GetDataTable("SELECT NO_TEL, NO_FAX,FG_PAYBILL, DC_ADS1_H, DC_ADS1_D, CD_EMP_PARTNER, NO_TEL, NO_FAX, E_MAIL   FROM DZSN_MA_PARTNER  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'     AND CD_PARTNER = '" + cd_partner + "'");

        public DataTable Get_PJTInfo(string p_cd_pjt_pipe) => DBHelper.GetDataTable("UP_PU_COMMON_PJT_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                         p_cd_pjt_pipe,
                                                                                                                         this.Lang });

        public DataSet Print(string NO_PO, string CD_PLANT, string DT_PO)
        {
            object[] objArray;
            if (MA.ServerKey(false, new string[] { "CSA" }))
                objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                          NO_PO,
                                          DT_PO,
                                          CD_PLANT };
            else
                objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                          NO_PO,
                                          Global.MainFrame.LoginInfo.UserID,
                                          this.Lang };
            DataSet dataSet;
            if (MA.ServerKey(false, new string[] { "NEXTURN" }))
                dataSet = DBHelper.GetDataSet("UP_PU_Z_NEXTURN_PO_REG_PRT", objArray);
            else
                dataSet = !MA.ServerKey(false, new string[] { "CSA" }) ? DBHelper.GetDataSet("UP_CZ_PU_PO_REG_PRINT", objArray) : DBHelper.GetDataSet("UP_PU_Z_CSA_PO_REG_PRINT", objArray);
            return dataSet;
        }

        public DataTable Print_Detail(string NO_PO) => DBHelper.GetDataTable("UP_PU_PO_REG_PRINT_L", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                    NO_PO,
                                                                                                                    Global.MainFrame.LoginInfo.UserID,
                                                                                                                    this.Lang });

        public DataTable CheckBUDGET(
          string 예산단위,
          string 예산계정,
          string 체크일자,
          string CdBizplan,
          string chk_FI_BUDGET)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("AM_ACTSUM", typeof(decimal));
            dataTable.Columns.Add("AM_JSUM", typeof(decimal));
            dataTable.Columns.Add("TP_BUNIT", typeof(string));
            dataTable.Columns.Add("ERROR_MSG", typeof(string));
            ResultData resultData = new ResultData();
            string str = "";
            SpInfo spInfo = new SpInfo();
            if (chk_FI_BUDGET == "100")
            {
                spInfo.SpNameSelect = "UP_FI_DOCU_BUDGET_DATA";
                spInfo.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                       예산단위,
                                                       CdBizplan,
                                                       예산계정,
                                                       "1",
                                                       체크일자 };
            }
            else
            {
                spInfo.SpNameSelect = "UP_FI_BUDGET_ACTSUM_CHECK";
                spInfo.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                       예산단위,
                                                       예산계정,
                                                       체크일자 };
            }
            try
            {
                resultData = (ResultData)Global.MainFrame.FillDataTable(spInfo);
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            DataRow row = dataTable.NewRow();
            if (str != string.Empty)
            {
                row["ERROR_MSG"] = str;
            }
            else
            {
                row["AM_ACTSUM"] = resultData.OutParamsSelect[0, 0];
                row["AM_JSUM"] = resultData.OutParamsSelect[0, 1];
                row["TP_BUNIT"] = resultData.OutParamsSelect[0, 2].ToString();
            }
            dataTable.Rows.Add(row);
            return dataTable;
        }

        public DataSet PU_BUDGET_HST_SELECT(string _no_pr)
        {
            ResultData resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_BUDGET_HST_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                       _no_pr,
                                                                                                                       "PU_PO_REG" });
            DataSet dataValue = (DataSet)resultData.DataValue;
            return (DataSet)resultData.DataValue;
        }

        internal DataTable PU_BUDGET_HST() => new DataTable()
        {
            Columns = {
        {
          "NO_PU",
          typeof (string)
        },
        {
          "NENU_TYPE",
          typeof (string)
        },
        {
          "NO_HST",
          typeof (string)
        },
        {
          "CD_BUDGET",
          typeof (string)
        },
        {
          "NM_BUDGET",
          typeof (string)
        },
        {
          "CD_BGACCT",
          typeof (string)
        },
        {
          "NM_BGACCT",
          typeof (string)
        },
        {
          "CD_BIZPLAN",
          typeof (string)
        },
        {
          "NM_BIZPLAN",
          typeof (string)
        },
        {
          "AM_ACTSUM",
          typeof (decimal)
        },
        {
          "AM_JSUM",
          typeof (decimal)
        },
        {
          "RT_JSUM",
          typeof (decimal)
        },
        {
          "AM",
          typeof (decimal)
        },
        {
          "AM_JAN",
          typeof (decimal)
        },
        {
          "TP_BUNIT",
          typeof (string)
        },
        {
          "ERROR_MSG",
          typeof (string)
        },
        {
          "DTS_INSERT",
          typeof (string)
        },
        {
          "ID_INSERT",
          typeof (string)
        },
        {
          "DTS_UPDATE",
          typeof (string)
        },
        {
          "ID_UPDATE",
          typeof (string)
        }
      }
        };

        internal DataTable getMail_Adress(DataTable no_app)
        {
            DataTable mailAdress = no_app.Clone();
            foreach (DataRow row in no_app.Rows)
            {
                DataTable dataTable = DBHelper.GetDataTable("SELECT   PA.EMP_WRITE,\r\n                                        ME.NM_KOR,\r\n                                        ME.NO_EMAIL,\r\n                                        ME.NO_EMP\r\n                                FROM     PU_APPH PA INNER JOIN DZSN_MA_EMP ME ON PA.EMP_WRITE = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY \r\n                                WHERE    PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                AND      NO_APP ='" + D.GetString(row["NO_APP"]) + "'");
                if (dataTable != null && dataTable.Rows.Count != 0)
                    mailAdress.Merge(dataTable);
            }
            return mailAdress;
        }

        internal DataTable getMail_Adress_ABLBIO(DataTable no_pr, string no_po)
        {
            DataTable mailAdressAblbio = no_pr.Clone();
            foreach (DataRow row in no_pr.Rows)
            {
                DataTable dataTable = DBHelper.GetDataTable("SELECT DISTINCT\r\n                                            ME.NM_KOR,\r\n                                            ME.NO_EMAIL,\r\n                                            PA.NO_EMP \r\n                                   FROM     PU_PRH PA INNER JOIN DZSN_MA_EMP ME ON PA.NO_EMP = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY \r\n                                   WHERE    PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                   AND      NO_PR ='" + D.GetString(row["NO_PR"]) + "'\r\n                                    \r\n                                   UNION ALL\r\n\r\n                                            SELECT   \r\n                                            ME.NM_KOR,\r\n                                            ME.NO_EMAIL,\r\n                                            PA.NO_EMP \r\n                                 FROM     PU_POH PA INNER JOIN DZSN_MA_EMP ME ON PA.NO_EMP = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY \r\n                                 WHERE    PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                 AND      NO_PO ='" + no_po + "'");
                if (dataTable != null && dataTable.Rows.Count != 0)
                    mailAdressAblbio.Merge(dataTable);
            }
            return mailAdressAblbio;
        }

        public string GetNoPo(string id_memo)
        {
            string str = "SELECT A.NO_PO   FROM   PU_POL A  WHERE A.ID_MEMO = '" + id_memo + "'   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(str);
            return dataTable == null || dataTable.Rows.Count <= 0 ? "" : D.GetString(dataTable.Rows[0]["NO_PO"]);
        }

        internal DataTable item_pinvn(object[] obj) => DBHelper.GetDataTable("UP_PU_PINVN_USE_QT", obj);

        internal DataSet Search_um_prioritize_item(object[] obj) => DBHelper.GetDataSet("UP_PU_UM_TOTAL_SELECT", obj);

        internal string pjt_item_josun(string cd_pjt)
        {
            string str = "SELECT   CD_USERDEF13\r\n                               FROM     SA_PROJECTH_SUB \r\n                               WHERE    CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                               AND      NO_PROJECT ='" + cd_pjt + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(str);
            return dataTable == null || dataTable.Rows.Count == 0 ? "" : D.GetString(dataTable.Rows[0]["CD_USERDEF13"]);
        }

        internal void SaveContent(
          ContentType contentType,
          Dass.FlexGrid.CommandType commandType,
          string noIo,
          decimal no_ioline,
          string value)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            if (contentType == 0)
                str2 = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                str2 = "CHECK_PEN";
            if (commandType == 0)
                str1 = "UPDATE PU_POL SET " + str2 + " = '" + value + "' WHERE NO_PO  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'  AND NO_LINE = " + no_ioline;
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                str1 = "UPDATE PU_POL SET " + str2 + " = NULL WHERE NO_PO  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "' AND NO_LINE = " + no_ioline;
            Global.MainFrame.ExecuteScalar(str1);
        }

        internal DataTable GetInfo_ZOO(string NO_PROJECT)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_PU_Z_ZOO_PROJECT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                     NO_PROJECT });
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public DataTable GetYN_SU(string sCD_TPPO)
        {
            string empty = string.Empty;
            string str = "SELECT YN_SU, FG_TRANS   FROM PU_TPPO WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND CD_TPPO = '" + sCD_TPPO + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable GET_SU_BOM(
          string strCD_PLANT,
          string strCD_PARTNER,
          string strCD_ITEM)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_PU_PO_REG_SU_BOM_SELECT",
                SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                strCD_PLANT,
                                                strCD_PARTNER,
                                                strCD_ITEM }
            })).DataValue;
            foreach (DataColumn column in dataValue.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataValue;
        }

        public DataTable Check_ITEMGRP_SG(string CD_ITEM_GRP)
        {
            string str = "SELECT  CD_1 SG_TYPE, UM_RT_ETC_1 QT_SG, AM_1 UM_WEIGHT  FROM PU_CUST_PUBLIC WHERE  CD_NO_PK_1 = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  CD_CUST_SERVER_KEY = '" + Global.MainFrame.ServerKey + "'   AND  NM_BUSINESS    =  'P_PU_Z_SINJINSM_ITEMGRP_SG'   AND  SQ_BUSINESS    =  '1'   AND  CD_NO_PK_2     = '" + CD_ITEM_GRP + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable Check_ITEMGRP_UM(string CD_PARTNER)
        {
            string str = "SELECT  UM_RT_ETC_1,CD_NO_PK_3 AS CLS_L,CD_NO_PK_4 AS GRP_ITEM,CD_NO_PK_2 AS CD_PARTNER  FROM PU_CUST_PUBLIC WHERE  CD_NO_PK_1 = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  CD_CUST_SERVER_KEY = '" + Global.MainFrame.ServerKeyCommon + "'   AND  NM_BUSINESS    =  'P_PU_Z_SINJINSM_PO_AM_RT_REG'   AND  SQ_BUSINESS    =  '1'   AND  CD_NO_PK_2    IN (" + CD_PARTNER + ")";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable Check_EMP_SG(string NO_EMP)
        {
            string str = "SELECT  DC_RMK  FROM  MA_EMP WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  NO_EMP     = '" + NO_EMP + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable Check_PITEM(string CD_ITEM, string CD_PLANT, string CLS_S)
        {
            string str = "SELECT 1  FROM MA_PITEM WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  CD_ITEM = '" + CD_ITEM + "'   AND  CD_PLANT = '" + CD_PLANT + "'   AND  CLS_S = '" + CLS_S + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable get_SINJINSM_UM_BONSA_EM(string NO_RELATION, string SEQ_RELATION)
        {
            string str = "SELECT  UM_BONSA_EM  FROM  SA_Z_SINJINSM_EM_ORDERSUB WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  EMNO    = '" + NO_RELATION + "'   AND  EMSUBNO    = '" + SEQ_RELATION + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable Get_Project_Detail(string CD_PJT)
        {
            string str = "SELECT  H.CD_PARTNER, P.LN_PARTNER, H.NO_EMP, E.NM_KOR, H.END_USER  FROM SA_PROJECTH H  LEFT JOIN MA_PARTNER P ON P.CD_PARTNER = H.CD_PARTNER AND P.CD_COMPANY = H.CD_COMPANY  LEFT JOIN MA_EMP E ON E.NO_EMP = H.NO_EMP AND E.CD_COMPANY = H.CD_COMPANY WHERE  H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  H.NO_PROJECT = '" + CD_PJT + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable STND_PITEM(string STND_ITEM, string CD_PLANT)
        {
            string str = "SELECT CD_ITEM  FROM MA_PITEM WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  STND_ITEM = '" + STND_ITEM + "'   AND  CD_PLANT = '" + CD_PLANT + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        internal DataTable GetTopes(string strNoPo) => DBHelper.GetDataTable("UP_PU_Z_TOPES_PO_TAB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                      strNoPo,
                                                                                                                      Global.MainFrame.ServerKeyCommon });

        internal bool GetMHIK(string strNoPo)
        {
            bool mhik = false;
            if (DBHelper.GetDataTable("UP_PU_Z_MHIK_PO_TAB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              strNoPo,
                                                                              Global.MainFrame.ServerKeyCommon }).Rows.Count == 0)
                mhik = true;
            return mhik;
        }

        public DataTable search_dt(string strDate)
        {
            string str = "SELECT DT_CAL  FROM MA_CALENDAR  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'     AND DT_CAL > '" + strDate + "'    AND FG1_HOLIDAY = 'W'  ORDER BY DT_CAL";
            return Global.MainFrame.FillDataTable(str);
        }

        internal DataRow 계약잔량체크(string NO_CONTRACT, decimal NO_LINE)
        {
            string empty = string.Empty;
            DataTable dataTable = DBHelper.GetDataTable(" SELECT ISNULL(H.CD_TYPE,'001') CD_TYPE, L.QT_CON - ISNULL(SL.QT_PO_MM,0) QT_CON  FROM   SA_Z_KPCI_CONTRACT_H H         INNER JOIN SA_Z_KPCI_CONTRACT_L L ON H.CD_COMPANY = L.CD_COMPANY AND H.NO_CONTRACT = L.NO_CONTRACT         LEFT JOIN(SELECT NO_RELATION, SEQ_RELATION, SUM(QT_PO_MM) QT_PO_MM                   FROM  PU_POL                    WHERE  CD_COMPANY   = '" + MA.Login.회사코드 + "'                  AND    NO_RELATION  = '" + NO_CONTRACT + "'                  AND    SEQ_RELATION = '" + NO_LINE + "'                  GROUP BY NO_RELATION, SEQ_RELATION                   )SL ON SL.NO_RELATION = L.NO_CONTRACT AND SL.SEQ_RELATION = L.NO_LINE  WHERE  L.CD_COMPANY = '" + MA.Login.회사코드 + "' AND    L.NO_CONTRACT = '" + NO_CONTRACT + "' AND    L.NO_LINE = '" + NO_LINE + "'");
            return dataTable == null || dataTable.Rows.Count == 0 ? null : dataTable.Rows[0];
        }

        public DataSet Gw_DYPNF(object[] obj) => DBHelper.GetDataSet("UP_PU_PO_REG_PRINT_DYPNF", obj);

        public bool 미결전표처리(string no_po) => ((ResultData)Global.MainFrame.ExecSp("UP_PU_Z_KAHP_SLP_I", new object[] { no_po,
                                                                                                                            Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                            Global.MainFrame.LoginInfo.UserID })).Result;

        public bool 미결전표취소처리(string no_po, string no_docu) => ((ResultData)Global.MainFrame.ExecSp("UP_PU_Z_KAHP_SLP_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                                no_po,
                                                                                                                                                no_docu,
                                                                                                                                                "PU_POL" })).Result;

        internal DataTable dtLot_Schema(string strFlag)
        {
            DataTable dataTable = new DataTable();
            if (strFlag == "LOT")
            {
                dataTable.Columns.Add("FG_TRANS", typeof(string));
                dataTable.Columns.Add("CD_PJT", typeof(string));
                dataTable.Columns.Add("NM_PROJECT", typeof(string));
                dataTable.Columns.Add("UM_EX", typeof(decimal));
                dataTable.Columns.Add("AM_EX", typeof(decimal));
                dataTable.Columns.Add("AM", typeof(decimal));
            }
            else
            {
                dataTable.Columns.Add("NO_IO_MGMT", typeof(string));
                dataTable.Columns.Add("NO_IOLINE_MGMT", typeof(decimal));
            }
            dataTable.Columns.Add("YN_RETURN", typeof(string));
            dataTable.Columns.Add("NO_IO", typeof(string));
            dataTable.Columns.Add("NO_IOLINE", typeof(decimal));
            dataTable.Columns.Add("CD_QTIOTP", typeof(string));
            dataTable.Columns.Add("NM_QTIOTP", typeof(string));
            dataTable.Columns.Add("DT_IO", typeof(string));
            dataTable.Columns.Add("CD_SL", typeof(string));
            dataTable.Columns.Add("NM_SL", typeof(string));
            dataTable.Columns.Add("FG_IO", typeof(string));
            dataTable.Columns.Add("CD_PLANT", typeof(string));
            dataTable.Columns.Add("CD_ITEM", typeof(string));
            dataTable.Columns.Add("NM_ITEM", typeof(string));
            dataTable.Columns.Add("UNIT_IM", typeof(string));
            dataTable.Columns.Add("STND_ITEM", typeof(string));
            dataTable.Columns.Add("QT_GOOD_INV", typeof(decimal));
            return dataTable;
        }

        public string getLOTIO(string NO_PO)
        {
            string str = "\tSELECT MAX(NO_IO) AS NO_IO FROM MM_QTIO WHERE NO_PSO_MGMT ='" + NO_PO + "' AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(str);
            string lotio = "";
            if (dataTable != null && dataTable.Rows.Count > 0)
                lotio = D.GetString(dataTable.Rows[0]["NO_IO"]);
            return lotio;
        }

        public decimal GET_DKC_UM(string[] str)
        {
            string str1 = "\tSELECT UM_ITEM_PU\r\n                                      FROM(\r\n\t                                        SELECT RANK() OVER (PARTITION BY 1 ORDER BY SDT_UM ASC) AS R,\r\n\t\t                                           UM_ITEM_PU\r\n\t                                          FROM SA_Z_DKC_UMITEM \r\n\t                                         WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n\t                                           AND CD_PARTNER_SA = '" + str[0] + "'\r\n\t                                           AND CD_PARTNER_PU = '" + str[1] + "'\r\n\t                                           AND GI_PARTNER = '" + str[2] + "'\r\n\t                                           AND FG_UM = '" + str[3] + "'\r\n\t                                           AND CD_EXCH_SA = '" + str[4] + "'\r\n\t                                           AND CD_EXCH_PU = '" + str[5] + "'\r\n\t                                           AND CD_ITEM = '" + str[6] + "'\r\n\t                                           AND SDT_UM <= '" + str[7] + "'\r\n\t                                           AND EDT_UM >= '" + str[7] + "'\r\n                                        )A\r\n                                     WHERE A.R = 1";
            DataTable dataTable = Global.MainFrame.FillDataTable(str1);
            decimal dkcUm = 0M;
            if (dataTable != null && dataTable.Rows.Count > 0)
                dkcUm = D.GetDecimal(dataTable.Rows[0]["UM_ITEM_PU"]);
            return dkcUm;
        }

        public DataTable GET_JSREMK_DT(string[] str)
        {
            string str1 = "SELECT NUM_USERDEF1, NUM_USERDEF3\r\n                                     FROM MA_PITEM\r\n                                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n\t                                  AND CD_PLANT = '" + str[0] + "'\r\n\t                                  AND CD_ITEM = '" + str[1] + "'";
            return Global.MainFrame.FillDataTable(str1);
        }

        public DataTable GET_DC_CALCULATION(string[] str)
        {
            string str1 = "SELECT H.DC_CALCULATION, H.NUM_STND_ITEM_5, L.NUM_WEIGHT_STEEL\r\n                                     FROM MA_Z_JONGHAP_ITEM_H H\r\n                                     LEFT JOIN MA_Z_JONGHAP_ITEM_L L ON H.CLS_L = L.CLS_L AND H.CD_COMPANY = L.CD_COMPANY AND H.CD_PLANT = L.CD_PLANT\r\n                                     AND ISNULL(L.NUM_STND_ITEM_1,0) ='" + str[2] + "'\r\n                                     AND ISNULL(L.NUM_STND_ITEM_2,0) ='" + str[3] + "'\r\n                                     AND ISNULL(L.NUM_STND_ITEM_3,0) ='" + str[4] + "'\r\n                                     AND ISNULL(L.NUM_STND_ITEM_4,0) ='" + str[5] + "'\r\n\r\n                                    WHERE H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n\t                                  AND H.CD_PLANT = '" + str[0] + "'\r\n\t                                  AND H.CLS_L = '" + str[1] + "'";
            return Global.MainFrame.FillDataTable(str1);
        }

        public decimal GET_PIOLINK_UM(string[] str)
        {
            string empty = string.Empty;
            string str1;
            if (D.GetString(str[2]) == "004" || D.GetString(str[2]) == "005")
                str1 = "SELECT A.UM_EX_PO AS UM_EX\r\n                            FROM(\r\n\t                                      SELECT RANK() OVER (PARTITION BY 1 ORDER BY H.DT_PAY DESC) AS R,\r\n\t\t                                         L.UM_EX_PO\r\n\t                                        FROM TR_BL_IMH H\r\n                                 LEFT OUTER JOIN TR_BL_IML L ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_BL = H.NO_BL\r\n\t                                        WHERE H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n\t                                        AND L.CD_PLANT = '" + str[0] + "'\r\n\t                                        AND L.CD_ITEM = '" + str[1] + "'\r\n                            )A\r\n                            WHERE A.R = 1";
            else
                str1 = "SELECT A.UM_EX_CLS AS UM_EX\r\n                            FROM(\r\n\t                                      SELECT RANK() OVER (PARTITION BY 1 ORDER BY H.DT_PROCESS DESC) AS R,\r\n\t\t                                         L.UM_EX_CLS\r\n\t                                        FROM PU_IVH H\r\n                                 LEFT OUTER JOIN PU_IVL L ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_IV = H.NO_IV\r\n\t                                        WHERE H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n\t                                        AND L.CD_PLANT = '" + str[0] + "'\r\n\t                                        AND L.CD_ITEM = '" + str[1] + "'\r\n                            )A\r\n                            WHERE A.R = 1";
            DataTable dataTable = Global.MainFrame.FillDataTable(str1);
            decimal piolinkUm = 0M;
            if (dataTable != null && dataTable.Rows.Count > 0)
                piolinkUm = D.GetDecimal(dataTable.Rows[0]["UM_EX"]);
            return piolinkUm;
        }

        public DataTable Get_Gipartner(string strGipartner)
        {
            string str = "SELECT H.LN_PARTNER, H.CD_PARTNER, D.CD_FLAG1\r\n                                     FROM MA_PARTNER H\r\n                                      LEFT JOIN MA_CODEDTL D ON H.CLS_PARTNER = D.CD_SYSDEF AND D.CD_FIELD = 'MA_B000003' AND H.CD_COMPANY = D.CD_COMPANY \r\n                                    WHERE H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n\t                                  AND (H.LN_PARTNER = '" + strGipartner + "' OR H.CD_PARTNER = '" + strGipartner + "')";
            return Global.MainFrame.FillDataTable(str);
        }

        public decimal AM_IN(string strCdPartner, string strDt)
        {
            string str1 = strDt.Remove(6) + "01";
            string str2 = strDt.Remove(6) + "31";
            string str3 = "SELECT SUM(CASE WHEN OH.YN_RETURN = 'Y' THEN - O.AM ELSE O.AM END) AS AM\r\n\r\n                                     FROM PU_POH H\r\n                                    INNER JOIN PU_POL L ON H.NO_PO = L.NO_PO AND H.CD_COMPANY = L.CD_COMPANY\r\n                                    INNER JOIN MM_QTIO O ON O.NO_PSO_MGMT = L.NO_PO AND O.NO_PSOLINE_MGMT = L.NO_LINE AND O.CD_COMPANY = L.CD_COMPANY\r\n                                    INNER JOIN MM_QTIOH OH ON OH.NO_IO = O.NO_IO AND OH.CD_COMPANY = O.CD_COMPANY\r\n\r\n                                    WHERE H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                      AND H.DT_PO BETWEEN '" + str1 + "' AND '" + str2 + "'\r\n\t                                  AND H.CD_PARTNER = '" + strCdPartner + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(str3);
            decimal num = 0M;
            if (dataTable != null && dataTable.Rows.Count > 0)
                num = D.GetDecimal(dataTable.Rows[0]["AM"]);
            return num;
        }

        public DataTable AM_OUT(string strCdItem, string strCdPlant, string strDt)
        {
            string str = "SELECT * FROM TABLE(FN_PU_Z_WINFOOD_USEINV ('" + Global.MainFrame.LoginInfo.CompanyCode + "',\r\n                                                                       '" + strCdItem + "',  \r\n                                                                       '" + strCdPlant + "',\r\n                                                                       '" + strDt + "'))";
            return Global.MainFrame.FillDataTable(str);
        }

        public decimal GET_MA_ITEMGRP(string strCD_ITEMGRP)
        {
            string str = "SELECT H.DC_RMK\r\n                                     FROM MA_ITEMGRP H\r\n\r\n                                    WHERE H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n\t                                  AND H.CD_ITEMGRP = '" + strCD_ITEMGRP + "'";
            DataTable dataTable = Global.MainFrame.FillDataTable(str);
            decimal maItemgrp = 0M;
            if (dataTable != null && dataTable.Rows.Count > 0)
                maItemgrp = D.GetDecimal(dataTable.Rows[0]["DC_RMK"]);
            return maItemgrp;
        }

        public DataTable getDT_LIMIT(object[] obj) => DBHelper.GetDataTable("UP_PU_Z_KOINO_DUADATE_SEARCH", obj);

        public DataTable get_MA_PARTNER_SUB(string CD_PARTNER) => DBHelper.GetDataTable("SELECT  T.CD_TPPO, T.NM_TPPO, G.CD_PURGRP, G.NM_PURGRP  FROM  MA_PARTNER_SUB P  LEFT JOIN DZSN_PU_TPPO T ON T.CD_TPPO = P.CD_TPPO AND T.CD_COMPANY = P.CD_COMPANY   LEFT JOIN DZSN_MA_PURGRP G ON G.CD_PURGRP = P.CD_PURGRP AND G.CD_COMPANY = P.CD_COMPANY WHERE  P.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  P.CD_PARTNER    = '" + CD_PARTNER + "'");

        public DataTable Get_AppInfo(string p_app_item_pipe) => DBHelper.GetDataTable("UP_PU_Z_GLOZEN_PO_EXCEL_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                  p_app_item_pipe });

        public DataTable Get_UserPartner(string CD_PARTNER) => DBHelper.GetDataTable("SELECT  CD_PARTNER   FROM  MA_USER P WHERE  P.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  P.CD_PARTNER    = '" + CD_PARTNER + "'    AND  ID_USER = '" + Global.MainFrame.LoginInfo.UserID + "'");

        public DataTable Get_GwInfo(string no_po) => DBHelper.GetDataTable("UP_PU_Z_DAEYONG_GW_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                  no_po });

        public DataTable Get발주라인비고1생성(string 프로젝트코드) => DBHelper.GetDataTable("UP_PU_Z_YWD_PO_RMK_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                   프로젝트코드 });

        public DataTable getDT_WINIX(object[] obj) => DBHelper.GetDataTable("UP_PU_Z_WINIX_DUADATE_SEARCH", obj);

        public DataTable GetCD_CC_Priority() => DBHelper.GetDataTable("SELECT CD_SYSDEF, NM_SYSDEF, CD_FLAG1  FROM DZSN_MA_CODEDTL   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND CD_FIELD = 'PU_C000133'    AND USE_YN = 'Y'  ORDER BY CD_FLAG1 ");

        public DataTable getQT_KMI(object[] obj) => DBHelper.GetDataTable("UP_PU_Z_KMI_QT_REMAIN_S", obj);

        public DataTable search_partner_HOTEL(string cd_partner) => DBHelper.GetDataTable("SELECT *   FROM DZSN_MA_PARTNER  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'     AND CD_PARTNER = '" + cd_partner + "'");

        public string GET_CALENDAR(string CD_PLANT, string DT)
        {
            string str = "SELECT NEOE.SF_PR_CALENDAR_HOLIDAY_NEXT ('" + Global.MainFrame.LoginInfo.CompanyCode + "', '" + CD_PLANT + "', '" + DT + "')  AS DT";
            string empty = string.Empty;
            DataTable dataTable = DBHelper.GetDataTable(str);
            if (dataTable != null && dataTable.Rows.Count > 0)
                empty = dataTable.Rows[0][nameof(DT)].ToString();
            return empty;
        }

        public DataTable GETACCT_FDWL(object[] obj) => DBHelper.GetDataTable("UP_PU_Z_FDWL_GET_ACCT", obj);

        public DataTable Get_PREMP(string NO_PR) => DBHelper.GetDataTable("SELECT  P.NO_EMP   FROM  PU_PRH P WHERE  P.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'   AND  P.NO_PR    = '" + NO_PR + "' ");

        public string Get_CPGETNO(object[] obj)
        {
            string empty = string.Empty;
            object[] objArray = null;
            if (DBHelper.ExecuteNonQuery("CP_Z_ABLBIO_GETNO", obj, out objArray))
                empty = objArray[0].ToString();
            return empty;
        }

        public decimal GetMaxRtExch(string strCD_EXCH)
        {
            DataTable dataTable = DBHelper.GetDataTable(" SELECT RATE_BASE\r\n                                     FROM\r\n                                     (\r\n                                     SELECT RATE_BASE ,\r\n\t\t                                    ROW_NUMBER() OVER(ORDER BY YYMMDD DESC,NO_SEQ DESC) AS R\r\n                                      FROM MA_EXCHANGE\r\n                                     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' \r\n                                       AND CURR_SOUR = '" + strCD_EXCH + "' \r\n                                       AND CURR_DEST = '000' \r\n                                    )A\r\n                                    WHERE A.R = 1");
            decimal maxRtExch = 0M;
            if (dataTable != null && dataTable.Rows.Count > 0)
                maxRtExch = D.GetDecimal(dataTable.Rows[0]["RATE_BASE"]);
            return maxRtExch;
        }

        public string Search_SERIAL()
        {
            string str = "N";
            DataTable dataTable = DBHelper.GetDataTable("UP_PU_MNG_SER_SELECT", new string[] { Global.MainFrame.LoginInfo.CompanyCode });
            if (dataTable.Rows.Count > 0)
            {
                str = dataTable.Rows[0]["YN_SERIAL"].ToString();
                if (str == string.Empty)
                    str = "N";
            }
            return str;
        }
    }
}