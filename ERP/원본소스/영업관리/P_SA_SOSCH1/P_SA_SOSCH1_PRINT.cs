using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Windows.Print;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    class P_SA_SOSCH1_PRINT
    {
        #region -> 멤버필드
        //프린트 출력 설정 값 넘겨받는 부분
        ReportHelper _rdf;
        string _수주일자fr; string _수주일자to; string _공장; string _bp거래처; string _bp영업그룹; string 출하예정일 = string.Empty;
        string _bp담당자; string _bp수주형태; string _수주상태; string _탭; string _멀티키; string _멀티수주번호; private string 출력물명;
        string _DT_SO_chk; string _DT_pymnt; string _납기일자FROM; string _납기일자TO; string _진행상태;

        string _영업담당자;
        string _품목멀티;
        string _영업조직멀티;

        string _거래처그룹;
        string _거래처그룹2;

        string _대분류;
        string _중분류;
        string _소분류;
        string _날짜구분;

        private P_SA_SOSCH1_BIZ _biz = new P_SA_SOSCH1_BIZ();
        #endregion

        #region -> 조회값 Param

        public string 수주일자fr { set { _수주일자fr = value; } }
        public string 수주일자to { set { _수주일자to = value; } }
        public string 공장 { set { _공장 = value; } }
        public string bp거래처 { set { _bp거래처 = value; } }
        public string bp영업그룹 { set { _bp영업그룹 = value; } }
        public string bp담당자 { set { _bp담당자 = value; } }
        public string bp수주형태 { set { _bp수주형태 = value; } }
        public string 수주상태 { set { _수주상태 = value; } }
        public string 멀티키 { set { _멀티키 = value; } }
        public string 멀티수주번호 { set { _멀티수주번호 = value; } }
        public string 탭 { set { _탭 = value; } }

        public string 수주기간chk { set { _DT_SO_chk = value; } }
        public string 납기기간chk { set { _DT_pymnt = value; } }
        public string 납기일자FROM { set { _납기일자FROM = value; } }
        public string 납기일자TO { set { _납기일자TO = value; } }

        public string 영업담당자 { set { _영업담당자 = value; } }
        public string 품목멀티 { set { _품목멀티 = value; } }
        public string 영업조직멀티 { set { _영업조직멀티 = value; } }
        public string 거래처그룹 { set { _거래처그룹 = value; } }
        public string 거래처그룹2 { set { _거래처그룹2 = value; } }

        public string 대분류 { set { _대분류 = value; } }
        public string 중분류 { set { _중분류 = value; } }
        public string 소분류 { set { _소분류 = value; } }
        public string 날짜구분 { set { _날짜구분 = value; } }

        public string 진행상태 { set { _진행상태 = value; } }

        public void ShowPrintDialog() { _rdf.Print(); }

        #endregion

        #region -> 헤더부분넘겨받는값
        public string n_dtSofr = string.Empty; //수주일 및 납기일
        public string n_dtSoto = string.Empty; //수주일 및 납기일
        public string n_Plant = string.Empty; // 공장명
        public string n_bpPartner = string.Empty; // 거래처명
        public string n_bpSaleGRP = string.Empty; // 영업그룹
        public string n_bpSaleGRPNAME = string.Empty; // 영업그룹명
        public string n_Empno = string.Empty; // 담당자
        public string n_SoState = string.Empty; // 수주상태
        public string n_SoType = string.Empty; // 수주형태
        public string n_PartnerGrp = string.Empty; //거래처그룹
        public string n_PartnerGrp2 = string.Empty; //거래처그룹2
        public string n_Cls_L = string.Empty;   //대분류
        public string n_Cls_M = string.Empty;   //중분류
        public string n_Cls_S = string.Empty;   //소분류
        #endregion

        #region -> 프린트 클래스 생성

        public P_SA_SOSCH1_PRINT(string 파일명, string 타이틀, bool 가로여부)
        {
            _rdf = new ReportHelper(파일명, 타이틀, 가로여부);
            _rdf.Printing += new ReportHelper.PrintEventHandler(_rdf_Printing);
        }
        #endregion

        #region -> 프린트
        bool _rdf_Printing(object sender, PrintArgs args)
        {
            try
            {
                if(args.Action == PrintActionEnum.ON_PREPARE_PRINT)
                {
                    _rdf.Initialize();
                    출력물명 = args.scriptFile.ToUpper();
                    SetBinding();
                }
                else if(args.Action == PrintActionEnum.ON_PRINT)
                {
                    // 출력물 선택한 이후의 이벤트 실행
                }

                return true;
            }
            catch(Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return false;
            }
        }

        #region -> SetBinding()
        private void SetBinding()
        {
            DataTable dt = new DataTable();

            object[] obj = new object[] { null };

            if (출력물명.ToUpper().Contains("R_SA_SOSCH_001") || 출력물명.ToUpper().Contains("R_SA_SOSCH_002"))
            {
                obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                        _수주일자fr, 
                                        _수주일자to,                          
                                        _공장, 
                                        _bp거래처, 
                                        _bp영업그룹, 
                                        _bp담당자,
                                        _bp수주형태, 
                                        _수주상태, 
                                        _멀티키, 
                                        _DT_SO_chk, 
                                        _DT_pymnt, 
                                        _납기일자FROM, 
                                        _납기일자TO, 
                                        _영업담당자, 
                                        _품목멀티, 
                                        _영업조직멀티,
                                        _거래처그룹,
                                        _거래처그룹2,
                                        _대분류,
                                        _중분류,
                                        _소분류,
                                        _날짜구분,
                                        _진행상태
                    };
                dt = _biz.Print_type1(obj, 출력물명);

                if (출력물명 == "R_SA_SOSCH_001_JW.RDF" ||
                    출력물명 == "R_SA_SOSCH_001_JW(샘플).RDF" ||
                    출력물명 == "R_SA_SOSCH_001_JW(미납).RDF")
                {
                    DataTable dtSum = dt.DefaultView.ToTable(true, "NO_SO");

                    string filter = string.Empty;
                    foreach (DataRow dr in dtSum.Rows)
                    {
                        string NO_SO = D.GetString(dr["NO_SO"]);
                        filter = "NO_SO = '" + NO_SO + "'";

                        decimal AM_WONAMT_SUM = D.GetDecimal(dt.Compute("SUM(AM_WONAMT)", filter));
                        decimal AM_VAT_SUM = D.GetDecimal(dt.Compute("SUM(AM_VAT)", filter));
                        decimal AM_TOT_SUM = AM_WONAMT_SUM + AM_VAT_SUM;

                        DataRow[] drs = dt.Select(filter);

                        foreach (DataRow row in drs)
                        {
                            row["AM_WONAMT_SUM"] = AM_WONAMT_SUM;
                            row["AM_VAT_SUM"] = AM_VAT_SUM;
                            row["AM_TOT_SUM"] = AM_TOT_SUM;
                        }
                    }
                    dt.AcceptChanges();
                }
            }
            else if (출력물명.ToUpper().Contains("R_SA_SOSCH_100") || 출력물명.ToUpper().Contains("R_SA_SOSCH_200"))
            {
                obj = new object[] {  Global.MainFrame.LoginInfo.CompanyCode,
                                            _수주일자fr, 
                                            _수주일자to,
                                            _공장, 
                                            _bp거래처, 
                                            _bp영업그룹, 
                                            _bp담당자,
                                            _bp수주형태, 
                                            _수주상태, 
                                            _멀티수주번호, 
                                            _DT_SO_chk, 
                                            _DT_pymnt, 
                                            _납기일자FROM, 
                                            _납기일자TO, 
                                            _영업담당자, 
                                            _품목멀티, 
                                            _영업조직멀티,
                                            _거래처그룹,
                                            _거래처그룹2,
                                            _대분류,
                                            _중분류,
                                            _소분류,
                                            _날짜구분,
                                            _진행상태
                    };

                dt = _biz.Print_type1(obj, 출력물명);
            }
            else
            {
                obj = new object[] {  Global.MainFrame.LoginInfo.CompanyCode, 
                                            _수주일자fr, 
                                            _수주일자to,                                 
                                            _공장, 
                                            _bp거래처, 
                                            _bp영업그룹, 
                                            _bp담당자,                                   
                                            _bp수주형태, 
                                            _수주상태, 
                                            _탭, 
                                            _멀티키,                               
                                            _DT_SO_chk,  
                                            _DT_pymnt,  
                                            _납기일자FROM,  
                                            _납기일자TO, 
                                            _영업담당자, 
                                            _품목멀티, 
                                            _영업조직멀티,
                                            _거래처그룹,
                                            _거래처그룹2,
                                            _대분류,
                                            _중분류,
                                            _소분류,
                                            _날짜구분,
                                            _진행상태
                    };
                dt = _biz.Print_type1(obj, 출력물명);
            }

            _rdf.SetData("수주일자Fr", n_dtSofr);
            _rdf.SetData("수주일자To", n_dtSoto);
            _rdf.SetData("영업그룹", n_bpSaleGRP);
            _rdf.SetData("영업그룹명", n_bpSaleGRPNAME);
            _rdf.SetData("공장", n_Plant);
            _rdf.SetData("거래처", n_bpPartner);
            _rdf.SetData("수주상태", n_SoState);
            _rdf.SetData("담당자", n_Empno);
            _rdf.SetData("수주형태", n_SoType);
            _rdf.SetData("거래처그룹", n_PartnerGrp);
            _rdf.SetData("거래처그룹2", n_PartnerGrp2);
            _rdf.SetData("대분류", n_Cls_L);
            _rdf.SetData("중분류", n_Cls_M);
            _rdf.SetData("소분류", n_Cls_S);

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                string sqlQuery = " SELECT  H.DT_SO, L.QT_SO, L.AM_SO"
                                + " FROM    SA_SOL L"
                                + "         INNER JOIN SA_SOH H ON L.CD_COMPANY = H.CD_COMPANY AND L.NO_SO = H.NO_SO"
                                + " WHERE   L.CD_COMPANY = '" + MA.Login.회사코드 + "'"
                                + " AND     H.DT_SO BETWEEN '" + n_dtSofr.Substring(0, 4) + "' + '0101' AND '" + n_dtSofr.Substring(0, 4) + "' + '1231'"
                                + " AND     H.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2('" + _멀티키 + "'))";
                DataTable dtTotal = DBHelper.GetDataTable(sqlQuery);

                decimal totalDayQt = D.GetDecimal(dtTotal.Compute("SUM(QT_SO)", "DT_SO = '" + n_dtSofr + "'"));
                decimal totalDayAm = D.GetDecimal(dtTotal.Compute("SUM(AM_SO)", "DT_SO = '" + n_dtSofr + "'"));
                decimal totalMonthQt = D.GetDecimal(dtTotal.Compute("SUM(QT_SO)", "SUBSTRING(DT_SO, 1, 6) = '" + n_dtSofr.Substring(0, 6) + "'"));
                decimal totalMonthAm = D.GetDecimal(dtTotal.Compute("SUM(AM_SO)", "SUBSTRING(DT_SO, 1, 6) = '" + n_dtSofr.Substring(0, 6) + "'"));
                decimal totalYearQt = D.GetDecimal(dtTotal.Compute("SUM(QT_SO)", ""));
                decimal totalYearAm = D.GetDecimal(dtTotal.Compute("SUM(AM_SO)", ""));

                _rdf.SetData("일수량합계", totalDayQt.ToString("###,###,###,##0.####"));
                _rdf.SetData("일금액합계", totalDayAm.ToString("###,###,###,##0.####"));
                _rdf.SetData("월수량합계", totalMonthQt.ToString("###,###,###,##0.####"));
                _rdf.SetData("월금액합계", totalMonthAm.ToString("###,###,###,##0.####"));
                _rdf.SetData("년수량합계", totalYearQt.ToString("###,###,###,##0.####"));
                _rdf.SetData("년금액합계", totalYearAm.ToString("###,###,###,##0.####"));
            }

            if (출력물명 == "R_SA_SOSCH_002.RDF")
            {
                if (dt.Rows[0]["DT_REQGI"].ToString() != null && dt.Rows[0]["DT_REQGI"].ToString() != string.Empty && dt.Rows[0]["DT_REQGI"].ToString() != "")
                {
                    출하예정일 = dt.Rows[0]["DT_REQGI"].ToString();
                }
                else
                {
                    출하예정일 = dt.Rows[0]["DT_DUEDATE"].ToString();
                }
                string 변환일 = 날짜영문변환(출하예정일, 0);
                string 변환일45일 = 날짜영문변환(출하예정일, 45);
                string 변환일60일 = 날짜영문변환(출하예정일, 60);

                _rdf.SetData("DT_REQGI", 변환일);
                _rdf.SetData("DT_SHIPMENT", 변환일45일);
                _rdf.SetData("DT_VALIDITY", 변환일60일);
                _rdf.SetDataTable(dt, 1);
                _rdf.SetDataTable(dt, 2);
            }
            else if (출력물명 == "R_SA_SOSCH_001_JW.RDF"
                    || 출력물명 == "R_SA_SOSCH_001_JW(샘플).RDF"
                    || 출력물명 == "R_SA_SOSCH_001_JW(미납).RDF")
            {
                _rdf.SetDataTable(dt, 1);
                _rdf.SetDataTable(dt, 2);
                _rdf.SetDataTable(dt, 3);
            }
            else if (출력물명 == "R_SA_SOSCH_001_DRF.DRF")
            {
                _rdf.SetDataTable("dt1", dt);
                //_rdf.SetDataTable("dt2", dt);
                //_rdf.SetDataTable("dt3", dt);
                //_rdf.SetDrfStringData("수주일자Fr", "20120101");
            }
            else
            {
                _rdf.SetDataTable(dt);
            }
        }
        #endregion

        #region -> 날짜영문변환

        private string 날짜영문변환(string 출하예정일, int 증가일)
        {
            DateTime BeforeDT_LOADING = DateTime.Parse(출하예정일.Substring(0, 4) + "-" + 출하예정일.Substring(4, 2) + "-" + 출하예정일.Substring(6, 2));
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            return BeforeDT_LOADING.AddDays(증가일).ToString("MMM. dd, yyyy", ci);
        }

        #endregion

        #endregion
    }
}
