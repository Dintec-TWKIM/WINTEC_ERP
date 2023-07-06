using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using System.Data;
using Duzon.Common.Util;
using Dass.FlexGrid;
using System.Windows.Forms;

namespace sale
{
    class P_SA_SO_PHWA_BIZ
    {
        string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
        FlexGrid _flex = new FlexGrid();
        ResultData _rtn = new ResultData();

        public DataSet Search(string NO_SO)
        {

            ResultData rd = ( ResultData )Global.MainFrame.FillDataSet( "UP_SA_SO_SELECT", new object [] { CD_COMPANY, NO_SO } );
            DataSet ds = ( DataSet )rd.DataValue;

            foreach ( DataTable dt in ds.Tables )
            {
                foreach ( DataColumn Col in dt.Columns )
                {
                    if ( Col.DataType == Type.GetType( "System.Decimal" ) )
                        Col.DefaultValue = 0;
                }
            }

            // 헤더테이블 디퐅트값
            DataTable dtHeader = ds.Tables [0];

            dtHeader.Columns ["DT_SO"].DefaultValue = Global.MainFrame.GetStringToday;           //수주일자
            dtHeader.Columns ["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;    //사번
            dtHeader.Columns ["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;  //이름
            dtHeader.Columns ["CD_EXCH"].DefaultValue = "000";                                   //화폐단위
            dtHeader.Columns ["TP_PRICE"].DefaultValue = "001";                                   //단가유형
            dtHeader.Columns ["TP_VAT"].DefaultValue = "11";                                    //VAT구분
            dtHeader.Columns ["FG_VAT"].DefaultValue = "N";                                    //부가세
            dtHeader.Columns ["FG_TAXP"].DefaultValue = "001";                                   //계산서처리
            dtHeader.Columns ["NO_PROJECT"].DefaultValue = "";                                   //프로젝트
            dtHeader.Columns ["FG_TRANSPORT"].DefaultValue = "001";


            return ds;
        }

        public decimal UmSearch(string 품목, string 거래처, string 단가유형, string 환종, string 단가적용형태)
        {

            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_SO_SELECT1";
            si.SpParamsSelect = new object [] { CD_COMPANY, 품목, 거래처, 단가유형, 환종, 단가적용형태 };
            ResultData rtn = ( ResultData )Global.MainFrame.FillDataTable( si );
            return _flex.CDecimal( rtn.OutParamsSelect [0, 0].ToString() );
        }

        public bool CheckCredit(string 거래처, decimal 금액)
        {
            string 수주확정 = "001";

            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_CHECKCREDIT_SELECT";
            si.SpParamsSelect = new object [] { CD_COMPANY, 거래처, 금액, 수주확정 };
            _rtn = ( ResultData )Global.MainFrame.FillDataTable( si );

            if ( _rtn.OutParamsSelect [0, 0] != null && _rtn.OutParamsSelect [0, 0].ToString() != "" )
            {
                if ( _rtn.OutParamsSelect [0, 2].ToString() == "002" )
                {
                    if ( Global.MainFrame.ShowMessage( "여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : " + _rtn.OutParamsSelect [0, 0].ToString() + ", 잔액 : " + _rtn.OutParamsSelect [0, 1].ToString() + ")", "QY2" ) == DialogResult.Yes )
                        return true;
                    else
                        return false;
                }
                else if ( _rtn.OutParamsSelect [0, 2].ToString() == "003" )
                {
                    Global.MainFrame.ShowMessage( "여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : " + _rtn.OutParamsSelect [0, 0].ToString() + " 잔액 : " + _rtn.OutParamsSelect [0, 1].ToString() + ")" );
                    return false;
                }
            }
            return true;
        }

        public bool Delete(string NO_SO)
        {
            ResultData rtn = ( ResultData )Global.MainFrame.ExecSp( "UP_SA_SO_DELETE", new object [] { CD_COMPANY, NO_SO } );
            return rtn.Result;
        }

        public bool Save(DataTable dtH, DataTable dtL, string NO_SO, string 수주상태, string 거래구분, string 출하형태, string 매출형태, string 수출여부, string 과세구분, string 구분)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if ( dtH != null )
            {
                SpInfo siM = new SpInfo();

                if ( 구분 == "복사" )
                    siM.DataState = DataValueState.Added;

                siM.DataValue = dtH;
                siM.CompanyID = CD_COMPANY;
                siM.UserID = Global.MainFrame.LoginInfo.UserID;
                siM.SpNameInsert = "UP_SA_SO_INSERT";
                siM.SpNameUpdate = "UP_SA_SO_UPDATE";
                siM.SpParamsInsert = new string [] { "CD_COMPANY", "NO_SO", "CD_BIZAREA", "DT_SO", "CD_PARTNER", "CD_SALEGRP", "NO_EMP", "TP_SO", "CD_EXCH", "RT_EXCH", "TP_PRICE", "NO_PROJECT", "TP_VAT", "RT_VAT", "FG_VAT", "FG_TAXP", "DC_RMK", "FG_BILL", "FG_TRANSPORT", "NO_CONTRACT", "STA_SO", "ID_INSERT" };
                siM.SpParamsUpdate = new string [] { "CD_COMPANY", "NO_SO", "DT_SO", "CD_PARTNER", "CD_SALEGRP", "NO_EMP", "CD_EXCH", "RT_EXCH", "TP_PRICE", "NO_PROJECT", "TP_VAT", "FG_VAT", "FG_TAXP", "DC_RMK", "FG_BILL", "FG_TRANSPORT", "NO_CONTRACT", "ID_UPDATE" };
                siM.SpParamsValues.Add( ActionState.Insert, "STA_SO", 수주상태 );
                siM.SpParamsValues.Add( ActionState.Insert, "CD_BIZAREA", Global.MainFrame.LoginInfo.BizAreaCode );   //담당자의  사업장으로 수정할지도..
                sic.Add( siM );
            }

            if ( dtL != null )
            {
                SpInfo siD = new SpInfo();

                if ( 구분 == "복사" )
                    siD.DataState = DataValueState.Added;

                siD.DataValue = dtL;
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.UserID;
                siD.SpNameInsert = "UP_SA_SO_INSERT1";
                siD.SpNameUpdate = "UP_SA_SO_UPDATE1";
                siD.SpNameDelete = "UP_SA_SO_DELETE1";

                if ( 구분 == "복사" )
                    siD.SpParamsInsert = new string [] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_PLANT", "CD_ITEM", "UNIT_SO", "DT_DUEDATE", "DT_REQGI", "QT_SO", "UM_SO", "AM_SO", "AM_WONAMT", "AM_VAT", "UNIT_IM", "QT_IM", "CD_SL", "TP_ITEM", "STA_SO1", "TP_BUSI", "TP_GI", "TP_IV", "TRADE", "TP_VAT", "ID_INSERT" };
                else
                    siD.SpParamsInsert = new string [] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_PLANT", "CD_ITEM", "UNIT_SO", "DT_DUEDATE", "DT_REQGI", "QT_SO", "UM_SO", "AM_SO", "AM_WONAMT", "AM_VAT", "UNIT_IM", "QT_IM", "CD_SL", "TP_ITEM", "STA_SO", "TP_BUSI", "TP_GI", "TP_IV", "TRADE", "TP_VAT", "ID_INSERT" };

                siD.SpParamsUpdate = new string [] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_PLANT", "CD_ITEM", "UNIT_SO", "DT_DUEDATE", "DT_REQGI", "QT_SO", "UM_SO", "AM_SO", "AM_WONAMT", "AM_VAT", "UNIT_IM", "QT_IM", "CD_SL", "TP_ITEM", "ID_UPDATE" };
                siD.SpParamsDelete = new string [] { "CD_COMPANY", "NO_SO", "SEQ_SO" };

                siD.SpParamsValues.Add( ActionState.Insert, "NO_SO", NO_SO );

                if ( 구분 != "복사" )
                    siD.SpParamsValues.Add( ActionState.Insert, "STA_SO", 수주상태 );

                siD.SpParamsValues.Add( ActionState.Insert, "TP_BUSI", 거래구분 );
                siD.SpParamsValues.Add( ActionState.Insert, "TP_GI", 출하형태 );
                siD.SpParamsValues.Add( ActionState.Insert, "TP_IV", 매출형태 );
                siD.SpParamsValues.Add( ActionState.Insert, "TRADE", 수출여부 );
                siD.SpParamsValues.Add( ActionState.Insert, "TP_VAT", 과세구분 );
                siD.SpParamsValues.Add( ActionState.Update, "NO_SO", NO_SO );
                siD.SpParamsValues.Add( ActionState.Delete, "NO_SO", NO_SO );
                sic.Add( siD );
            }

            ResultData [] rtn = ( ResultData [] )Global.MainFrame.Save( sic );

            for ( int i = 0 ; i < rtn.Length ; i++ )
                if ( !rtn [i].Result )
                    return false;

            return true;
        }
    }
}
