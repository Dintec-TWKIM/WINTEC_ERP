using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System.IO;
using System.Windows.Forms;
using Duzon.ERPU.MF;

namespace pur
{
    class P_PU_PO_REG2_GW
    {

        public string[] getGwSearch(string no_po,string str,string dt_po)
        {

            string HTML_FILE_NAME = string.Empty;
            string App_Form_Kind = string.Empty;
            string GW_URL = string.Empty;
            string cd_company = Global.MainFrame.LoginInfo.CompanyCode;
            string SUB_GW = string.Empty;

            string[] strs = new string[3];

            switch (str)    //시스템 통제설정에 따라 업체마다 파일 이름과 문서번호와 업체 URL 경로를 셋팅한다.
            {
                case "016": //정화선박의장 설정

                    HTML_FILE_NAME = "HT_P_PO_REG2_JEONGHWA_" + cd_company;
                    App_Form_Kind = "2001";
                    GW_URL = "http://www.jeonghwa21.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=";
                    break;

                case "020": //원익 설정

                    HTML_FILE_NAME = "HT_P_PO_REG2_WONIK_NEW";
                    App_Form_Kind = "2015";
                    GW_URL = "http://gw.wonik.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=";
                    break;

                case "025": //삼텍엔지니어링 설정

                    HTML_FILE_NAME = "HT_P_PO_REG2_SAMTEC";
                    App_Form_Kind = "2000";
                    GW_URL = "http://gw.samtecheng.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=";
                    break;

                case "027": //아바텍 설정

                    HTML_FILE_NAME = "HT_P_PO_REG2_AVATEC";
                    App_Form_Kind = "2100";
                    GW_URL = "http://61.106.24.20/kor_webroot/src/cm/tims/index.aspx?cd_company=";
                    break;

                case "028": //우리기술 설정
                    //국문 이면 RETURN : base 영문 이면 RETURN : item
                    P_PU_GWFORM_SUB sub = new P_PU_GWFORM_SUB("발주등록","1");

                    if (sub.ShowDialog() == DialogResult.OK)
                    {
                        SUB_GW = sub.data_return;

                        if (SUB_GW == "item")
                        {
                            HTML_FILE_NAME = "HT_P_PO_REG2_WOORI_ENG";
                            App_Form_Kind = "5000";
                        }
                        else
                        {
                            HTML_FILE_NAME = "HT_P_PO_REG2_WOORI_KOR";
                            App_Form_Kind = "4000";
                        }
                        GW_URL = "http://gw.wooritg.com/kor_webroot/src/cm/tims/index.aspx?cd_company=";
                    }
                    else
                        return null;

                    break;

                case "029": //원봉 설정

                    HTML_FILE_NAME = "HT_P_PO_REG2_WONBONG";
                    App_Form_Kind = "1200";
                    GW_URL = "http://gw.wonbong.com/kor_webroot/src/cm/tims/index.aspx?cd_company=";
                    break;

                case "036": //쎄트렉아이

                    HTML_FILE_NAME = "HT_P_PO_REG2_SATREC";
                    App_Form_Kind = "2300";
                    GW_URL = "";
                    break;

                case "039": //기가비스 

                    HTML_FILE_NAME = "HT_P_PO_REG2_GIGAVIS";
                    App_Form_Kind = "5600";
                    GW_URL = "";
                    break;

                case "042": //안전공업
                    HTML_FILE_NAME = "HT_P_PO_REG2_ANJUN";
                    App_Form_Kind = "1006";
                    GW_URL = "";
                    break;

                case "046": //유니콘미싱공업
                    HTML_FILE_NAME = "HT_P_PO_REG2_BROTHER1";
                    App_Form_Kind = "1006";
                    GW_URL = "";
                    break;

                case "053": //(주)케이피아이씨코포레이션
                    HTML_FILE_NAME = "HT_P_PO_REG2_KPIC";
                    App_Form_Kind = "5600";
                    GW_URL = "";
                    break;

                default:
                    return null;
            }

            string downPath_Html = Application.StartupPath + "\\download\\gw\\" + HTML_FILE_NAME + ".htm";  //HTML 파일경로
            string html_source = File.ReadAllText(downPath_Html, System.Text.UTF8Encoding.UTF8);            //HTML 파일읽기

            switch (str)    //시스템 통제설정에 따라 업체마다 파일을 만들어주는곳이다.
            {
                case "016": //정화선박의장
                    strs[0] = 전자결재양식생성_정화선박의장(html_source, no_po);   //HTML 을 가공한 Contents
                    break;

                case "020": //원익
                    strs[0] = 전자결재양식생성_원익(html_source, no_po, dt_po);   //HTML 을 가공한 Contents
                    break;

                case "025": //삼텍엔지니어링 설정
                    strs[0] = 전자결재양식생성_삼텍(html_source, no_po);   //HTML 을 가공한 Contents
                    break;

                case "027": //아바텍 설정
                    strs[0] = 전자결재양식생성_아바텍(html_source, no_po);   //HTML 을 가공한 Contents
                    break;

                case "028": //우리기술 설정
                    strs[0] = 전자결재양식생성_우리기술(html_source, no_po);  //HTML 을 가공한 Contents
                    break;

                case "029": //원봉 설정
                    strs[0] = 전자결재양식생성_원봉(html_source, no_po);  //HTML 을 가공한 Contents
                    break;

                case "036": //쎄트렉아이 설정
                    strs[0] = 전자결재양식생성_쎄트렉아이(html_source, no_po);  //HTML 을 가공한 Contents
                    break;

                case "039": //기가비스 설정
                    strs[0] = 전자결재양식생성_기가비스(html_source, no_po);  //HTML 을 가공한 Contents
                    break;

                case "042": //안전공업 설정
                    strs[0] = GW_ANJUN_html(html_source, no_po);   //HTML 을 가공한 Contents
                    break;

                case "046": //유니콘미싱공업 설정
                    strs[0] = 전자결재양식생성_유니콘미싱공업(html_source, no_po);   //HTML 을 가공한 Contents
                    break;
                case "053": //(주)케이피아이씨코포레이션
                    strs[0] = 전자결재양식생성_KPIC(html_source, no_po);   //HTML 을 가공한 Contents
                    break;

                default:
                    return null;
            }

            strs[1] = App_Form_Kind;
            strs[2] = GW_URL;

            return strs;
        }

        P_PU_PO_REG2_BIZ _biz = new P_PU_PO_REG2_BIZ();

        internal string 전자결재양식생성_정화선박의장(string html_source,string no_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_qt_po_mm, gw_um_ex_po, gw_am_ex, gw_nm_pjt, gw_dt_limit, gw_dc1;

            decimal sum_gw_qt_po_mm=decimal.Zero, sum_gw_am_ex =decimal.Zero, h_vat=decimal.Zero;

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_im = D.GetString(dr["UNIT_IM"]);
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,##0.####");
                gw_um_ex_po = D.GetDecimal(dr["UM_P"]).ToString("#,###,###,##0.####");
                gw_am_ex = D.GetDecimal(dr["AM"]).ToString("#,###,###,##0.####");
                gw_nm_pjt = D.GetString(dr["NM_PROJECT"]);
                if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                else
                    gw_dt_limit = string.Empty;

                gw_dc1 = D.GetString(dr["DC1"]);

                sum_gw_qt_po_mm += D.GetDecimal(dr["QT_PO_MM"]);
                sum_gw_am_ex += D.GetDecimal(dr["AM"]);

                html_source_LINE += @"<tr>
										<td height='24' align='center' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_no + @"&nbsp;</td>
										<td align='left' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_nm_item + @"&nbsp;</td>
										<td align='left' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_stnd_item + @"&nbsp;</td>
										<td align='left' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_unit_im + @"&nbsp;</td>
										<td align='right' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_qt_po_mm + @"&nbsp;</td>
										<td align='right' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_um_ex_po + @"&nbsp;</td>
										<td align='right' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_am_ex + @"&nbsp;</td>
										<td align='left' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_nm_pjt + @"&nbsp;</td>
										<td align='center' style='border-left: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_dt_limit + @"&nbsp;</td>
										<td align='left' style='border-left: 1px solid black;border-right: 1px solid black;border-bottom:1px solid black;'>
                                        " + gw_dc1 + @"&nbsp;</td>
									</tr>";

            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);
            html_source = html_source.Replace("@@발주일자", gw_dt_limit + "&nbsp;");
            html_source = html_source.Replace("@@발주번호", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@거래처명", D.GetString(dt_GWdata.Rows[0]["거래처명"]) + "&nbsp;");
            html_source = html_source.Replace("@@DC50", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_FG_PAYMENT", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@비고2", D.GetString(dt_GWdata.Rows[0]["DC_RMK2"]) + "&nbsp;");

            h_vat = D.GetDecimal(dt_GWdata.Rows[0]["H_VAT"]);
            html_source = html_source.Replace("@@QT_PO_MM", D.GetDecimal(sum_gw_qt_po_mm).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_AM_EX", D.GetDecimal(sum_gw_am_ex).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_VAT", D.GetDecimal(h_vat).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@H_HAP", D.GetDecimal(h_vat + sum_gw_am_ex).ToString("#,###,###,##0.####") + "&nbsp;");

            return html_source;
        }

        internal string 전자결재양식생성_원익(string html_source, string no_po, string dt_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, D.GetString(테이블구분.PU_POH.GetHashCode()), dt_po, "WONIK" };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT_ONLY(obj);

            string html_source_LINE = string.Empty;

            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_qt_po_mm, gw_um_ex_po, gw_am_ex, gw_qt_inv, gw_qt_po, gw_dc3, gw_am, sales_margin_per, gw_dt_limit, gw_dt_po,gw_dc1;

            decimal sum_gw_qt_po_mm = decimal.Zero, sales_margin = decimal.Zero, customs_price = decimal.Zero, customs_fee = decimal.Zero, sum_qtinv = decimal.Zero, sum_qt_po = decimal.Zero, total_qtinv = decimal.Zero, customs_per = decimal.Zero;
            decimal heder_qt_po = decimal.Zero, heder_qt_inv = decimal.Zero, heder_qt_inv_po = decimal.Zero, sum_cont_price = decimal.Zero, sum_sales_margin = decimal.Zero, total_qtpo_qtinv = decimal.Zero, sum_dc4 = decimal.Zero, gw_dc4 = decimal.Zero, gw_hap = decimal.Zero, gw_contract_price = decimal.Zero;

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_im = D.GetString(dr["UNIT_IM"]);
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,##0.###");
                gw_um_ex_po = D.GetDecimal(dr["UM_EX_PO"]).ToString("#,###,###,##0.####");
                gw_am_ex = D.GetDecimal(dr["AM_EX"]).ToString("#,###,###,##0.##");
                gw_am = D.GetDecimal(dr["AM"]).ToString("#,###,###,##0");
                gw_dc4 = D.GetDecimal(dr["DC4"]);
                gw_qt_inv = D.GetDecimal(dr["QT_INV"]).ToString("#,###,###,##0.####");
                heder_qt_inv += D.GetDecimal(dr["QT_INV"]);
                gw_qt_po = D.GetDecimal(dr["QT_PO"]).ToString("#,###,###,##0.####");
                heder_qt_po = heder_qt_po + D.GetDecimal(dr["QT_PO"]);
                gw_dc1 = D.GetString(dr["DC1"]);
                gw_contract_price = D.GetDecimal(dr["CONTRACT_PRICE"]);
                //if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                //    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                //else
                //    gw_dt_limit = string.Empty;

                //if (D.GetString(dr["DC3"]) != string.Empty)
                //{
                //    gw_dc3 = D.GetString(dr["DC3"]) + "%";
                //    customs_per = D.GetDecimal(dr["DC3"]) * 0.01m;
                //}
                //else
                //{
                //    gw_dc3 = string.Empty;
                //    customs_per = decimal.Zero;
                //}

                gw_dc3 = D.GetString(dr["COTOMS_FEE_PER"]);
                customs_fee = D.GetDecimal(dr["CUSTOMS_FEE_WON"]);
                customs_price = D.GetDecimal(dr["COST_PRICE"]);
                sales_margin_per = D.GetString(dr["SALES_MARGIN_PER"]);
                sales_margin = D.GetDecimal(dr["SALES_MARGIN_WON"]);
                gw_hap = D.GetDecimal(dr["SUM_QT_INV_QT_PO"]);

                //customs_fee = D.GetDecimal(dr["AM"]) * D.GetDecimal(customs_per);
                //customs_fee = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.PU, FormatTpType.MONEY, FormatFgType.INSERT, customs_fee));
                //customs_price = D.GetDecimal(gw_am) + customs_fee;
                //customs_price = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.PU, FormatTpType.MONEY, FormatFgType.INSERT, customs_price));
                //sales_margin = D.GetDecimal(gw_dc4) - customs_price;
                //sales_margin = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.PU, FormatTpType.MONEY, FormatFgType.INSERT, sales_margin));
                //if(gw_dc4 != decimal.Zero)
                //    sales_margin_per = D.GetString(D.GetDecimal(sales_margin / gw_dc4));
                //else
                //    sales_margin_per = string.Empty;

                //if (sales_margin_per != string.Empty)
                //{
                //    decimal SMP = D.GetDecimal(sales_margin_per) * 100m;
                //    sales_margin_per = (SMP).ToString("#,###,###,##0.##") + "%";
                //}

                total_qtinv = D.GetDecimal(gw_qt_inv) + D.GetDecimal(gw_qt_po) + D.GetDecimal(gw_qt_po_mm);
                total_qtinv = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.PU, FormatTpType.MONEY, FormatFgType.INSERT, total_qtinv));
                heder_qt_inv_po += total_qtinv;
                //sum_gw_qt_po_mm += D.GetDecimal(dr["QT_PO_MM"]);
                //sum_gw_am_ex += D.GetDecimal(dr["AM_EX"]);
                //sum_gw_qt_po_mm += D.GetDecimal(gw_qt_po_mm);
                //sum_am_ex += D.GetDecimal(gw_am_ex);
                //sum_am += D.GetDecimal(gw_am);
                //sum_customs_fee += D.GetDecimal(customs_fee);
                //sum_cost_price += D.GetDecimal(customs_price);
                sum_sales_margin += D.GetDecimal(sales_margin);
                sum_qtinv += D.GetDecimal(gw_qt_inv);
                sum_qt_po += D.GetDecimal(gw_qt_po);
                sum_cont_price += gw_contract_price;
                //sum_dc4 += D.GetDecimal(dr["DC4"]);
                html_source_LINE += @"<tr>
					                        <td height='20'>
                                            " + gw_cd_item + @"&nbsp;</td>
					                        <td align='center'>
                                            " + gw_stnd_item + @"&nbsp;</td>
					                        <td align='right'>
                                            " + gw_qt_po_mm + @"&nbsp;</td>
					                        <td align='right'>
                                            " + gw_um_ex_po + @"&nbsp;</td>
					                        <td align='right'>
                                            " + gw_am_ex + @"&nbsp;</td>
				                            <td align='right'>
                                            " + gw_am + @"&nbsp;</td>
					                        <td align='center'>
                                            " + gw_dc3 + @"&nbsp;</td>
					                        <td align='right'>
                                            " +  customs_fee.ToString("#,###,###,##0") +@"&nbsp;</td>
					                        <td align='right'>
                                            " + customs_price.ToString("#,###,###,##0") + @"&nbsp;</td>
					                        <td align='center'>
                                            " + gw_contract_price.ToString("#,###,###,##0") + @"&nbsp;</td>
					                        <td align='right'>
                                            " + sales_margin.ToString("#,###,###,##0") + @"&nbsp;</td>
					                        <td align='right'>
                                            " + sales_margin_per + @"&nbsp;</td>
					                        <td align='right'>
                                            " + gw_qt_inv + @"&nbsp;</td>
					                        <td align='right'>
                                            " + gw_qt_po + @"&nbsp;</td>
					                        <td align='right'>
                                            " + D.GetDecimal(total_qtinv).ToString("#,###,###,##0.####") + @"&nbsp;</td>
					                        <td>
                                            "+gw_dc1+@"&nbsp;</td>
				                     </tr>";

            }

            decimal total = decimal.Zero;
            //decimal sum_margin_rage = decimal.Zero;
            if (D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(0, 4) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(4, 2) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

             gw_dt_po = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            //total_qtpo_qtinv = sum_qtinv + sum_qt_po;

            //if (sum_cost_price != decimal.Zero)
            //    sum_margin_rage = sum_sales_margin / sum_dc4;

            total = D.GetDecimal(dt_GWdata.Rows[0]["SUM_CONTRACT_PRICE"]) + D.GetDecimal(dt_GWdata.Rows[0]["VAT"]);
            total = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.PU, FormatTpType.MONEY, FormatFgType.INSERT, total));

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);
            html_source = html_source.Replace("@@DT_PO", gw_dt_po + "&nbsp;");
            html_source = html_source.Replace("@@CUSTFEE", D.GetDecimal(dt_GWdata.Rows[0]["CUSTOMS_FEE_WON"]).ToString("#,###,###,##0") + "&nbsp;");
            html_source = html_source.Replace("@@NM_COND_SHIPMENT", D.GetString(dt_GWdata.Rows[0]["NM_COND_SHIPMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_COND_PANY", D.GetString(dt_GWdata.Rows[0]["NM_COND_PAY"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_PURGRP", D.GetString(dt_GWdata.Rows[0]["NM_PURGRP"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_USERDEF21", D.GetString(dt_GWdata.Rows[0]["NM_USERDEF21"]) + "&nbsp;");
            html_source = html_source.Replace("@@COND_DAYS", D.GetString(dt_GWdata.Rows[0]["COND_DAYS"]) + "&nbsp;");
            if(D.GetString(dt_GWdata.Rows[0]["COND_DAYS"]) !=string.Empty)
                html_source = html_source.Replace("@@NM_PROJECT", "[" + D.GetString(dt_GWdata.Rows[0]["NO_PROJECT"]) + "]" + D.GetString(dt_GWdata.Rows[0]["NM_PROJECT"]) + "&nbsp;");
            else
                html_source = html_source.Replace("@@NM_PROJECT", "&nbsp;");
            html_source = html_source.Replace("@@NO_PO", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@CUSTOMER", D.GetString(dt_GWdata.Rows[0]["NM_USERDEF1"]) + "&nbsp;");
            html_source = html_source.Replace("@@COND_PAY", D.GetString(dt_GWdata.Rows[0]["NM_COND_PAY"] + "&nbsp;" + dt_GWdata.Rows[0]["COND_DAYS"] + "&nbsp; days" + "&nbsp;"));
            html_source = html_source.Replace("@@PAYMENT", D.GetString(dt_GWdata.Rows[0]["NM_USERDEF2"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_PTR", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
            html_source = html_source.Replace("@@SUM_CONTRACT_PRICE",  D.GetDecimal(dt_GWdata.Rows[0]["SUM_CONTRACT_PRICE"]).ToString("#,###,###,##0")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_CONTRACT_PRICE"])*/ + "&nbsp;");
            html_source = html_source.Replace("@@VAT", D.GetDecimal(dt_GWdata.Rows[0]["VAT"]).ToString("#,###,###,##0")/*D.GetDecimal(dt_GWdata.Rows[0]["VAT"])*/ + "&nbsp;");
            html_source = html_source.Replace("@@TOTAL", D.GetDecimal(total).ToString("#,###,###,##0") + "&nbsp;");
            html_source = html_source.Replace("@@COST_PRICE",D.GetDecimal(dt_GWdata.Rows[0]["SUM_COST_PRICE"]).ToString("#,###,###,##0")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_COST_PRICE"])*/ + "&nbsp;");
            html_source = html_source.Replace("@@MARGIN", sum_sales_margin.ToString("#,###,###,##0.##") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_SALES_MARGIN", D.GetDecimal(dt_GWdata.Rows[0]["SUM_SALES_MARGIN"]).ToString("#,###,###,##0") + "&nbsp;");
            html_source = html_source.Replace("@@NM_SHIPMENT", D.GetString(dt_GWdata.Rows[0]["NM_TRANSPORT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_LOADING", D.GetString(dt_GWdata.Rows[0]["NM_LOADING"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_ORIGIN", D.GetString(dt_GWdata.Rows[0]["NM_ORIGIN"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_LIMIT", D.GetString(gw_dt_limit) + "&nbsp;");
            html_source = html_source.Replace("@@NM_ARRIVER", D.GetString(dt_GWdata.Rows[0]["NM_ARRIVER"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_COND_PAY", D.GetString(dt_GWdata.Rows[0]["NM_COND_PRICE"]) + "&nbsp;");
            html_source = html_source.Replace("@@RT_EXCH",  D.GetDecimal(dt_GWdata.Rows[0]["RT_EXCH"]).ToString("#,###,###,##0.##") + "&nbsp;");
            html_source = html_source.Replace("@@NM_EXCH", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]) + "&nbsp;");

            html_source = html_source.Replace("@@QTY", D.GetDecimal(dt_GWdata.Rows[0]["SUM_QT_PO_MM"]).ToString("#,###,###,##0.###") + "&nbsp;");
            html_source = html_source.Replace("@@AM_EX",  D.GetDecimal(dt_GWdata.Rows[0]["SUM_AM_EX"]).ToString("#,###,###,##0.##")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_AM_EX"]).ToString()*/ + "&nbsp;");
            html_source = html_source.Replace("@@SUM_AM", D.GetDecimal(dt_GWdata.Rows[0]["SUM_AM"]).ToString("#,###,###,##0") + "&nbsp;");
            html_source = html_source.Replace("@@AM", D.GetDecimal(dt_GWdata.Rows[0]["SUM_AM"]).ToString("#,###,###,##0") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_COST_PRICE", D.GetDecimal(dt_GWdata.Rows[0]["SUM_COST_PRICE"]).ToString("#,###,###,##0")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_COST_PRICE"])*/ + "&nbsp;");
            html_source = html_source.Replace("@@CUSTFRICE", D.GetDecimal(dt_GWdata.Rows[0]["SUM_COST_PRICE"]).ToString("#,###,###,##0") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_MARGIN_RATE", D.GetString(dt_GWdata.Rows[0]["SUM_MARGIN_RATE"]) + "&nbsp;");
            html_source = html_source.Replace("@@SALES", D.GetDecimal(dt_GWdata.Rows[0]["SUM_SALES_MARGIN_WON"]).ToString("#,###,###,##0") + "&nbsp;");
            html_source = html_source.Replace("@@INV", heder_qt_inv.ToString("#,###,###,##0.###")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_QT_INV"])*/ + "&nbsp;");
            html_source = html_source.Replace("@@PO", heder_qt_po.ToString("#,###,###,##0.###")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_QT_PO"])*/ + "&nbsp;");
            html_source = html_source.Replace("@@HAP", heder_qt_inv_po.ToString("#,###,###,##0.###")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_QT_INV_QT_PO"])*/ + "&nbsp;");
            html_source = html_source.Replace("@@SUM_CONT_PRICE", sum_cont_price.ToString("#,###,###,##0.###")/*D.GetDecimal(dt_GWdata.Rows[0]["SUM_QT_INV_QT_PO"])*/ + "&nbsp;");

            return html_source;  
        }

        internal string 전자결재양식생성_삼텍(string html_source, string no_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po,"","","SAMTEC" };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT_ONLY(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;
            string html_source_DETLAE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_qt_po, gw_um, gw_am, gw_dt_limit, gw_dc1;
            string gw_nm_cost, gw_cls_item, gw_gi_partner, gw_rate;
            decimal gw_sum_qt=0, gw_sum_am=0;
          

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);

                if (gw_no == 40 && dt_GWdata.Rows.Count != 40)
                {
                    gw_nm_item = D.GetString(dr["NM_ITEM"]) + "&nbsp; @@COUNT";
                    gw_qt_po = "@@QTSUM";
                    gw_um = "@@UMSUM";
                    gw_am = "@@AMSUM";
                }
                else
                {
                    gw_nm_item = D.GetString(dr["NM_ITEM"]);
                    gw_qt_po = D.GetDecimal(dr["QT_PO"]).ToString("#,###,###,##0.####");
                    gw_um = D.GetDecimal(dr["UM"]).ToString("#,###,###,##0.####");
                    gw_am = D.GetDecimal(dr["AM"]).ToString("#,###,###,##0.####");
                }

                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_im = D.GetString(dr["UNIT_IM"]);
                gw_nm_cost = D.GetString(dr["NM_COST"]);
                gw_cls_item = D.GetString(dr["NM_CLS_ITEM"]);

                gw_gi_partner = D.GetString(dr["GI_NM_PARTNER"]);
                if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                else
                    gw_dt_limit = string.Empty;

                gw_dc1 = D.GetString(dr["DC1"]);


                if (gw_no <= 40)
                {
                    html_source_LINE += @"	<tr>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_no + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_nm_item + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_cd_item + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_stnd_item + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_nm_cost + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_qt_po + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_unit_im + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_cls_item + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_um + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_am + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_dt_limit + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_gi_partner + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_dc1 + @"&nbsp; </font></td>
                                			
		                                </tr>";
                }
                if (gw_no >= 40)
                {
                    gw_sum_am += D.GetDecimal(dr["AM"]);
                    gw_sum_qt += D.GetDecimal(dr["QT_PO"]);
                }

            }

            DataTable dt_group = dt_GWdata.DefaultView.ToTable(true, "NO_CBS", "AM_PROJECT_CBS", "H_AM_CBS", "AM_DIF", "NO_SEQ", "NM_COST");
            int gw_no_cbs = 0;

            foreach (DataRow dr in dt_group.Rows)
            {
                string gw_am_pjt_cbs, gw_h_am_cbs, gw_am_dif, gw_no_seq,  gw_am_cbs;
                gw_no_cbs++;
                gw_am_pjt_cbs = D.GetDecimal(dr["AM_PROJECT_CBS"]).ToString("#,###,###,##0.####");
                gw_h_am_cbs = D.GetDecimal(dr["H_AM_CBS"]).ToString("#,###,###,##0.####");
                gw_am_dif = D.GetDecimal(dr["AM_DIF"]).ToString("#,###,###,##0.####");
                gw_no_seq = D.GetString(dr["NO_SEQ"]);
                gw_nm_cost = D.GetString(dr["NM_COST"]);
                gw_am_cbs = D.GetDecimal(dt_GWdata.Compute("SUM(AM)", "NO_CBS = '" + D.GetString(dr["NO_CBS"]) + "'")).ToString("#,###,###,##0.####");

                html_source_DETLAE += @"<tr>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_no_cbs + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_nm_cost + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_am_pjt_cbs + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_h_am_cbs + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_am_cbs + @"&nbsp; </font></td>
			                                <td colspan='3'><font face='돋움' style='font-size: 9pt'>" + gw_am_dif + @"&nbsp; </font></td>
			                                <td><font face='돋움' style='font-size: 9pt'>" + gw_no_seq + @"&nbsp; </font></td>
			                                <td colspan='4'><font face='돋움' style='font-size: 9pt'>" + "" + @"&nbsp; </font></td>
			
		                              </tr>";
            }

            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            if (D.GetDecimal(dt_GWdata.Rows[0]["AM_PROJECT"]) != 0)
                gw_rate = D.GetDecimal(D.GetDecimal(dt_GWdata.Rows[0]["H_AM_PJT"]) / D.GetDecimal(dt_GWdata.Rows[0]["AM_PROJECT"]) * 100).ToString("#,###,###,##0.00");
            else
                gw_rate = "";

            html_source = html_source.Replace("@@LINE", html_source_LINE);
            html_source = html_source.Replace("@@DETALIE", html_source_DETLAE);
            html_source = html_source.Replace("@CD_PJT", D.GetString(dt_GWdata.Rows[0]["CD_PJT"]) + "&nbsp;");
            html_source = html_source.Replace("@NM_PJT", D.GetString(dt_GWdata.Rows[0]["NM_PJT"]) + "&nbsp;");
            html_source = html_source.Replace("@DT_PO", gw_dt_limit + "&nbsp;");
            html_source = html_source.Replace("@NO_PO", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@NM_PARTNER", D.GetString(dt_GWdata.Rows[0]["NM_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@NM_FG_PAYMENT", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@NM_PURGRP", D.GetString(dt_GWdata.Rows[0]["NM_PURGRP"]) + "&nbsp;");
            html_source = html_source.Replace("@NM_KOR", D.GetString(dt_GWdata.Rows[0]["NM_KOR"]) + "&nbsp;");
            html_source = html_source.Replace("@AM_PROJECT", D.GetDecimal(dt_GWdata.Rows[0]["AM_PROJECT"]).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@H_PJT_AM", D.GetDecimal(dt_GWdata.Rows[0]["H_AM_PJT"]).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@H_AM", D.GetDecimal(dt_GWdata.Rows[0]["H_AM"]).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@H_VAT", D.GetDecimal(dt_GWdata.Rows[0]["H_VAT"]).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@H_TOTAL", D.GetDecimal(dt_GWdata.Rows[0]["H_TOTAL"]).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@RATE", gw_rate + "&nbsp;");
            html_source = html_source.Replace("@DC_RMK_TEXT", D.GetString(dt_GWdata.Rows[0]["DC_RMK_TEXT"]).Replace("\n", "<br>") + "&nbsp;");

            if (gw_no > 40)
            {
                html_source = html_source.Replace("@@COUNT", "외" + "&nbsp;" + D.GetString(gw_no - 40) + "&nbsp;");
                html_source = html_source.Replace("@@AMSUM", gw_sum_am.ToString("#,###,###,##0.####"));
                html_source = html_source.Replace("@@QTSUM", gw_sum_qt.ToString("#,###,###,##0.####"));

                if(gw_sum_qt != 0)
                    html_source = html_source.Replace("@@UMSUM", D.GetDecimal(gw_sum_am / gw_sum_qt).ToString("#,###,###,##0.####"));
                else
                    html_source = html_source.Replace("@@UMSUM", D.GetDecimal(0).ToString("#,###,###,##0.####"));
            }

            return html_source;
        }

        internal string 전자결재양식생성_아바텍(string html_source, string no_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_qt_po_mm, gw_um_ex_po, gw_am_ex, gw_nm_pjt, gw_dt_limit, gw_dc1;

            decimal sum_gw_qt_po_mm = decimal.Zero, sum_gw_am_ex = decimal.Zero;

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_im = D.GetString(dr["UNIT_IM"]);
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,##0.####");
                gw_um_ex_po = D.GetDecimal(dr["UM_P"]).ToString("#,###,###,##0.####");
                gw_am_ex = D.GetDecimal(dr["AM"]).ToString("#,###,###,##0.####");
                gw_nm_pjt = D.GetString(dr["NM_PROJECT"]);
                if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                else
                    gw_dt_limit = string.Empty;

                gw_dc1 = D.GetString(dr["DC1"]);

                sum_gw_qt_po_mm += D.GetDecimal(dr["QT_PO_MM"]);
                sum_gw_am_ex += D.GetDecimal(dr["AM"]);

                html_source_LINE += @"<tr>
					                    <td width='30' height='24' align='center' style='font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black; border-left: 1px solid black'>
                                        " + gw_no + @"&nbsp;</td>
					                    <td width='120' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                        " + gw_cd_item + @"&nbsp;</td>
					                    <td width='165' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                        " + gw_nm_item + @"&nbsp;</td>
					                    <td width='120' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                        " + gw_stnd_item + @"&nbsp;</td>
					                    <td width='30' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                        " + gw_unit_im + @"&nbsp;</td>
					                    <td width='50' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                       " + gw_qt_po_mm + @"&nbsp;</td>
					                    <td width='100' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                       " + gw_um_ex_po + @"&nbsp;</td>
					                    <td width='120' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                       " + gw_am_ex + @"&nbsp;</td>
					                    <td width='80' align='center' style='border-left: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                       " + gw_dt_limit + @"&nbsp;</td>
					                    <td width='130' align='center' style='font-weight: bold;border-left: 1px solid black;border-right: 1px solid black;font-weight: bold; border-top: 1px solid black; border-bottom: 1px solid black;'>
                                       " + gw_dc1 + @"&nbsp;</td>
				                    </tr>";

            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);
            html_source = html_source.Replace("@@발주일자", gw_dt_limit + "&nbsp;");
            html_source = html_source.Replace("@@발주번호", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@거래처명", D.GetString(dt_GWdata.Rows[0]["거래처명"]) + "&nbsp;");
            html_source = html_source.Replace("@@DC50", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_FG_PAYMENT", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@비고2", D.GetString(dt_GWdata.Rows[0]["DC_RMK2"]) + "&nbsp;");

            html_source = html_source.Replace("@@QT_PO_MM", D.GetDecimal(sum_gw_qt_po_mm).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_AM_EX", D.GetDecimal(sum_gw_am_ex).ToString("#,###,###,##0.####") + "&nbsp;");


            return html_source;
        }

        internal string 전자결재양식생성_우리기술(string html_source, string no_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_qt_po_mm, gw_um_ex_po, gw_am_ex, gw_nm_pjt, gw_dt_limit, gw_dc1, gw_nm_exch, gw_nm_maker, gw_detali_item;
            string gw_nm_userdef1, gw_nm_userdef2 = string.Empty;
            decimal sum_gw_qt_po_mm = decimal.Zero, sum_gw_am_ex = decimal.Zero;

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_im = D.GetString(dr["UNIT_IM"]);
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,##0.####");
                gw_um_ex_po = D.GetDecimal(dr["UM_EX_PO"]).ToString("#,###,###,##0.####");
                gw_am_ex = D.GetDecimal(dr["AM_EX"]).ToString("#,###,###,##0.####");
                gw_nm_pjt = D.GetString(dr["CD_PJT"]);
                gw_detali_item = D.GetString(dr["STND_DETAIL_ITEM"]);
                gw_nm_userdef1 = D.GetString(dr["NM_USERDEF1"]);
                gw_nm_userdef2 = D.GetString(dr["NM_USERDEF2"]);
                if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                else
                    gw_dt_limit = string.Empty;

                gw_dc1 = D.GetString(dr["DC1"]);
                gw_nm_exch = D.GetString(dr["NM_EXCH"]);
                gw_nm_maker = D.GetString(dr["NM_MAKER"]);

                sum_gw_qt_po_mm += D.GetDecimal(dr["QT_PO_MM"]);
                sum_gw_am_ex += D.GetDecimal(dr["AM_EX"]);



                html_source_LINE += @"<tr height='25'>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        "+gw_no+@"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_cd_item + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_nm_item + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>
                                        " + gw_stnd_item + @"/&nbsp; <br> " + gw_detali_item + @"</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_nm_maker + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_unit_im + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_qt_po_mm + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>
                                        " + gw_um_ex_po + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_am_ex + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_nm_userdef1 + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
                                        " + gw_nm_userdef2 + @"&nbsp;</td>
		                            </tr>";

            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(0, 4) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(4, 2) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);
            html_source = html_source.Replace("@@품목수", D.GetString(gw_no) + "&nbsp;");

            html_source = html_source.Replace("@@발주번호", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@납기일", gw_dt_limit + "&nbsp;");
            html_source = html_source.Replace("@@발주업체명", D.GetString(dt_GWdata.Rows[0]["EN_BIZAREA"]) + "&nbsp;");
            html_source = html_source.Replace("@@공급업체명", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@영업담당자", D.GetString(dt_GWdata.Rows[0]["CD_EMP_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@구매담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
            html_source = html_source.Replace("@@요청담당자", D.GetString(dt_GWdata.Rows[0]["NM_EMP_PR"]) + "&nbsp;");
            html_source = html_source.Replace("@@영업전화번호", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
            html_source = html_source.Replace("@@구매전화번호", D.GetString(dt_GWdata.Rows[0]["USER_NO_TEL"]) + "&nbsp;");

            html_source = html_source.Replace("@@영업팩스번호", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
            html_source = html_source.Replace("@@구매팩스번호", D.GetString(dt_GWdata.Rows[0]["NO_FAX_PUR"]) + "&nbsp;");

            html_source = html_source.Replace("@@비고", D.GetString(dt_GWdata.Rows[0]["DC_RMK_TEXT"]) + "&nbsp;");
            html_source = html_source.Replace("@@요청비고", D.GetString(dt_GWdata.Rows[0]["DC_RMK2_PR"]) + "&nbsp;");

            html_source = html_source.Replace("@@요청번호", D.GetString(dt_GWdata.Rows[0]["NO_PR"]) + "&nbsp;");
            html_source = html_source.Replace("@@지급조건", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@프로젝트코드", D.GetString(dt_GWdata.Rows[0]["CD_PJT"]) + "&nbsp;");
            html_source = html_source.Replace("@@환정보", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]) + "&nbsp;");

            html_source = html_source.Replace("@@SUM_AM_EX", D.GetDecimal(sum_gw_am_ex).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_QT_PO_MM", D.GetDecimal(sum_gw_qt_po_mm).ToString("#,###,###,##0.####") + "&nbsp;");


            return html_source;
        }

        internal string 전자결재양식생성_원봉(string html_source, string no_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_qt_po_mm, gw_um_ex_po, gw_am_ex, gw_nm_pjt, gw_dt_limit, gw_dc1, gw_nm_sl;

            decimal sum_gw_qt_po_mm = decimal.Zero;

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_im = D.GetString(dr["UNIT_IM"]);
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,##0.####");
                gw_um_ex_po = D.GetDecimal(dr["UM_EX_PO"]).ToString("#,###,###,##0.####");
                gw_am_ex = D.GetDecimal(dr["AM_EX"]).ToString("#,###,###,##0.####");
                gw_nm_pjt = D.GetString(dr["CD_PJT"]);
                if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                else
                    gw_dt_limit = string.Empty;

                gw_nm_sl = D.GetString(dr["NM_SL"]);
                gw_dc1 = D.GetString(dr["DC1"]);


                sum_gw_qt_po_mm += D.GetDecimal(dr["QT_PO_MM"]);

                html_source_LINE += @"<tr height='25'>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_no + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_cd_item + @"&nbsp;</td>
			                            <td style='font-size: 8pt; border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_nm_item + @"&nbsp;</td>
			                            <td style='font-size: 8pt; border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_stnd_item + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_unit_im + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_qt_po_mm + @"&nbsp;</td>
			                            <td style='font-size: 8pt; border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_dt_limit + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_nm_sl + @"&nbsp;</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>
			                            " + gw_dc1 + @"&nbsp;</td>
		                            </tr>";

            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);

            html_source = html_source.Replace("@@발주번호", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@발주일자", gw_dt_limit + "&nbsp;");
            html_source = html_source.Replace("@@발주처", "["+D.GetString(dt_GWdata.Rows[0]["CD_PARTNER"])+"]" + "&nbsp;" + D.GetString(dt_GWdata.Rows[0]["거래처명"]) + "&nbsp;");
            html_source = html_source.Replace("@@전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
            html_source = html_source.Replace("@@팩스", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
            html_source = html_source.Replace("@@대급결제", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@참고사항", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@연락처", D.GetString(dt_GWdata.Rows[0]["NO_TEL_PUR"]) + "&nbsp;");
            html_source = html_source.Replace("@@FAX", D.GetString(dt_GWdata.Rows[0]["NO_FAX_PUR"]) + "&nbsp;");
            html_source = html_source.Replace("@@담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
            html_source = html_source.Replace("@@구매담당자전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL_EMER"]) + "&nbsp;");
            html_source = html_source.Replace("@@총발주수량", sum_gw_qt_po_mm.ToString("#,###,###,##0.####") + "&nbsp;");


            return html_source;
        }

        internal string 전자결재양식생성_쎄트렉아이(string html_source, string no_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_qt_po_mm, gw_unit_po, gw_um_ex_po, gw_am_ex, gw_nm_pjt, gw_nm_cls_item, gw_vat;
            string gw_nm_exch, gw_rt_exch, gw_am, gw_dt_limit;

            decimal sum_am = decimal.Zero, sum_vat = decimal.Zero;

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_po = D.GetString(dr["UNIT_PO"]);
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO"]).ToString("#,###,###,###.####");
                gw_um_ex_po = D.GetDecimal(dr["UM_EX"]).ToString("#,###,###,###.####");
                gw_am_ex = D.GetDecimal(dr["AM_EX"]).ToString("#,###,###,###.####");
                gw_am = D.GetDecimal(dr["AM"]).ToString("#,###,###,###.####");
                gw_vat = D.GetDecimal(dr["VAT"]).ToString("#,###,###,###.####");
                gw_nm_pjt = D.GetString(dr["NM_PROJECT"]);
                gw_nm_exch = D.GetString(dr["NM_EXCH"]);
                gw_rt_exch = D.GetString(dr["RT_EXCH"]);
                gw_nm_cls_item = D.GetString(dr["NM_CLS_ITEM"]);

                sum_am += D.GetDecimal(dr["AM"]);
                sum_vat += D.GetDecimal(dr["VAT"]);

                html_source_LINE += @"<tr height='25'>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='3'>"
                                               + gw_no +@"</td>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='11' bgcolor='#99D5CC'>"
                                               + gw_cd_item +"&nbsp;"+ gw_nm_item+"&nbsp;" + gw_stnd_item + @"</td>
		                              </tr>
                                		
		                              <tr height='25'>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='5'>"
                                               + gw_qt_po_mm +"&nbsp;"+ gw_unit_po+"&nbsp;" + gw_um_ex_po + @"</td>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>"
                                               + gw_nm_exch + "&nbsp;" + gw_am_ex + @"&nbsp;</td>
			                                <td align='right' style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>"
                                               + gw_am + @"&nbsp;</td>
		                              </tr>
                                		
		                              <tr height='25'>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='5'>"
                                               + gw_nm_cls_item + @"</td>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>"
                                               + gw_nm_pjt + @"</td>
			                                <td align='right' style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>"
                                               + gw_vat + @"&nbsp;</td>
		                              </tr>
                    		          <tr height='25'>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='6'><b>합&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;계</b></td>
			                                <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'></td>
			                                <td align='right' style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>"
                                               + (D.GetDecimal(gw_am) + D.GetDecimal(gw_vat)).ToString("#,###,###,###.####") + @"&nbsp;</td>
		                              </tr>";

            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);

            html_source = html_source.Replace("@@NO_PO", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PO", gw_dt_limit + "&nbsp;");
            html_source = html_source.Replace("@@DC_RMK2", D.GetString(dt_GWdata.Rows[0]["DC_RMK2"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_PTR", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_DEPT", D.GetString(dt_GWdata.Rows[0]["NM_DEPT"]) + "&nbsp;");
            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@CD_EMP_PARTNER", D.GetString(dt_GWdata.Rows[0]["CD_EMP_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_TEL1", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
            html_source = html_source.Replace("@@DC_ADS1_H",  "&nbsp;" +D.GetString(dt_GWdata.Rows[0]["DC_ADS1_H"]) + "&nbsp;" + D.GetString(dt_GWdata.Rows[0]["DC_ADS1_D"]));
            html_source = html_source.Replace("@@NM_DEPOSIT", D.GetString(dt_GWdata.Rows[0]["NM_DEPOSIT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_CD_BANK", D.GetString(dt_GWdata.Rows[0]["NM_CD_BANK"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_DEPOSIT", D.GetString(dt_GWdata.Rows[0]["NO_DEPOSIT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_FG_PR_TP", D.GetString(dt_GWdata.Rows[0]["NM_FG_PR_TP"]) + "&nbsp;");
            html_source = html_source.Replace("@@SUM_AM", sum_am.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_VAT", sum_vat.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_HAP", (sum_vat + sum_am).ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@환종", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]));

            return html_source;
        }

        internal string 전자결재양식생성_기가비스(string html_source, string no_po)
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_qt_po_mm, gw_unit_po, gw_um_ex_po, gw_dc1;
            string gw_am, gw_dt_limit;

            decimal sum_am = decimal.Zero, sum_vat = decimal.Zero, sum_qt = decimal.Zero;
            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_po = D.GetString(dr["UNIT_IM"]);
                if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                else
                    gw_dt_limit = string.Empty;
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,###.####");
                gw_um_ex_po = D.GetDecimal(dr["UM_P"]).ToString("#,###,###,###.####");
                gw_am = D.GetDecimal(dr["AM"]).ToString("#,###,###,###.####");
                gw_dc1 = D.GetString(dr["DC1"]);


                sum_am += D.GetDecimal(dr["AM"]);
                sum_vat += D.GetDecimal(dr["VAT"]);
                sum_qt += D.GetDecimal(dr["QT_PO_MM"]);

                html_source_LINE += @"<tr bgcolor='#F0FBFA'>
					                    <td rowspan='2' bgcolor='#F6F6F6' width='40' style='font-family: 굴림; font-size: 12px; color: #000000'>
					                    <div align='center'>
						                " + gw_no + @"</div>
					                    </td>
					                    <td rowspan='2' width='150' bgcolor='#FFFFFF' style='font-family: 굴림; font-size: 12px; color: #000000'>
					                    <table width='95%' border='0' cellspacing='0' cellpadding='2' align='left'>
						                    <tr>
							                    <td height='20' style='font-family: 굴림; font-size: 12px; color: #000000'>　
                                                " + gw_cd_item + @"</td>
						                    </tr>
						                    <tr>
							                    <td height='20' style='font-family: 굴림; font-size: 12px; color: #000000'>　
                                                " + gw_nm_item + @"</td>
						                    </tr>
					                    </table>
					                    </td>
					                    <td rowspan='2' width='140' bgcolor='#FFFFFF' style='font-family: 굴림; font-size: 12px; color: #000000'>
					                    <table width='95%' border='0' cellspacing='0' cellpadding='2'>
						                    <tr>
							                    <td height='20' style='font-family: 굴림; font-size: 12px; color: #000000'>　
                                                " + gw_stnd_item + @"</td>
						                    </tr>
						                    <tr>
							                    <td height='20' style='font-family: 굴림; font-size: 12px; color: #000000'>　
                                                " + gw_unit_po + @"</td>
						                    </tr>
					                    </table>
					                    </td>
					                    <td rowspan='2' width='100' bgcolor='#FFFFFF' style='font-family: 굴림; font-size: 12px; color: #000000'>
					                    <table width='95%' border='0' cellspacing='0' cellpadding='2' align='right'>
						                    <tr>
							                    <td height='20' style='font-family: 굴림; font-size: 12px; color: #000000'>　
                                                " + gw_dt_limit + @"</td>
						                    </tr>
						                    <tr>
							                    <td height='20' style='font-family: 굴림; font-size: 12px; color: #000000'>　
                                                " + gw_qt_po_mm + @"</td>
						                    </tr>
					                    </table>
					                    </td>
					                    <td width='110' bgcolor='#FFFFFF' style='font-family: 굴림; font-size: 12px; color: #000000'>
					                    <div align='right'>
　                                       " + gw_um_ex_po + @"</div>
					                    </td>
					                    <td width='110' bgcolor='#FFFFFF' style='font-family: 굴림; font-size: 12px; color: #000000'>
					                    <div align='right'>
　                                       " + gw_am + @"</div>
					                    </td>
				                        </tr>
				                        <tr>
					                        <td width='200' colspan='2' bgcolor='#FFFFFF' style='font-family: 굴림; font-size: 12px; color: #000000'>
					                        <div align='left'>
　                                           " + gw_dc1 + @"</div>
					                        </td>
				                    </tr>";

            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE); 

            html_source = html_source.Replace("@@NM_PARTNER", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_TEL1", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_FAX1", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_PO", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PO", gw_dt_limit + "&nbsp;");

            html_source = html_source.Replace("@@NM_CEO", D.GetString(dt_GWdata.Rows[0]["NM_CEO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_AD",  D.GetString(dt_GWdata.Rows[0]["ADS"]) + "&nbsp;" + D.GetString(dt_GWdata.Rows[0]["ADS_DETAIL"]));
            html_source = html_source.Replace("@@NO_G_TEL", D.GetString(dt_GWdata.Rows[0]["NO_TEL"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_G_FAX", D.GetString(dt_GWdata.Rows[0]["NO_FAX"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_KOR", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");

            html_source = html_source.Replace("@@NM_SL", D.GetString(dt_GWdata.Rows[0]["NM_SL"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_G", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@SUM_AM", sum_am.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_QT", sum_qt.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@VAT", sum_vat.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@DC_RMK", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_EXCH", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]));

            return html_source;
        }

        #region 안전공업
        private string GW_ANJUN_html(string html_source, string no_po)
        {

            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            string gw_nm_item, gw_qt_pr, gw_um_pr, gw_am_pr, gw_nm_exch, gw_dc_rmk2, gw_dt_limit;
            decimal gw_sum_am_ex = 0;

            int gw_no = 0;

            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no++;
                gw_nm_item = D.GetString(dr["CD_ITEM"]) + " / " + D.GetString(dr["NM_ITEM"]);
                gw_dc_rmk2 = D.GetString(dr["DC1"]);
                gw_qt_pr = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,###.####");
                gw_um_pr = D.GetDecimal(dr["UM_EX_PO"]).ToString("#,###,###,###.####");
                gw_am_pr = D.GetDecimal(dr["AM_EX"]).ToString("#,###,###,###.####");
                gw_nm_exch = D.GetString(dr["NM_EXCH"]);
                gw_sum_am_ex += D.GetDecimal(gw_am_pr);

                html_source_LINE += @"<tr height='25'>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>"
                                        + gw_no + @"</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>"
                                        + gw_nm_item + @"</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>"
                                        + gw_qt_pr + @"</td>
			                            <td align='right' style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>"
                                        + gw_um_pr + "&nbsp" + @"</td>
			                            <td style='border-style: solid; font-size=8pt; border-width: 1 1 1 1; border-color: #000000'>"
                                        + gw_nm_exch + @"</td>
			                            <td align='right' style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>"
                                        + gw_am_pr + "&nbsp" + @"</td>
			                            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>"
                                        + gw_dc_rmk2 + @"</td>
		                            </tr>";
            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);
            html_source = html_source.Replace("@@NO_PR", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_KOR", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_DEPT", D.GetString(dt_GWdata.Rows[0]["NM_DEPT"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PR", gw_dt_limit + "&nbsp;");
            html_source = html_source.Replace("@@SUM_AM_EX", gw_sum_am_ex.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@환종", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]));

            return html_source;
        }
        #endregion

        private string 전자결재양식생성_유니콘미싱공업(string html_source, string no_po)
        {

            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";
            string html_source_LINE = string.Empty;

            int gw_no = 0;
            string gw_cd_item, gw_nm_item, gw_stnd_item, gw_qt_po_mm, gw_unit_po, gw_um_ex_po, gw_dc1;
            string gw_am, gw_dt_limit;

            decimal sum_am = decimal.Zero, sum_vat = decimal.Zero, sum_qt = decimal.Zero;
            foreach (DataRow dr in dt_GWdata.Rows)
            {
                gw_no += 1;
                gw_cd_item = D.GetString(dr["CD_ITEM"]);
                gw_nm_item = D.GetString(dr["NM_ITEM"]);
                gw_stnd_item = D.GetString(dr["STND_ITEM"]);
                gw_unit_po = D.GetString(dr["UNIT_IM"]);
                if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                    gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
                else
                    gw_dt_limit = string.Empty;
                gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,###.####");
                gw_um_ex_po = D.GetDecimal(dr["UM_P"]).ToString("#,###,###,###.####");
                gw_am = D.GetDecimal(dr["AM"]).ToString("#,###,###,###.####");
                gw_dc1 = D.GetString(dr["DC1"]);


                sum_am += D.GetDecimal(dr["AM"]);
                sum_vat += D.GetDecimal(dr["VAT"]);
                sum_qt += D.GetDecimal(dr["QT_PO_MM"]);

                html_source_LINE += @"<tr bgcolor='#F0FBFA'>  
			                            <td rowspan='2' bgcolor='#F6F6F6' width='40'> <div align='center'> " + gw_no + @"</div> 
			                            </td> 
			                            <td rowspan='2' width='150' bgcolor='#FFFFFF'> 
				                            <table width='95%' border='0' cellspacing='0' cellpadding='2' align='left'>  
				                            <tr>  
					                            <td height='20'> " + gw_cd_item + @"
					                            </td>  
				                            </tr>  
				                            <tr>  
					                            <td height='20'> " + gw_nm_item + @"
					                            </td>  
				                            </tr>   
				                            </table> 
			                            </td> 
			                            <td rowspan='2' width='140' bgcolor='#FFFFFF'> 
				                            <table width='95%' border='0' cellspacing='0' cellpadding='2'>  
				                            <tr>  
					                            <td height='20'> " + gw_stnd_item + @"
					                            </td>  
				                            </tr>  
				                            <tr>  
					                            <td height='20'> " + gw_unit_po + @"
					                            </td>  
				                            </tr>   
				                            </table> 
			                            </td> 
			                            <td rowspan='2' width='100' bgcolor='#FFFFFF'> 
				                            <table width='95%' border='0' cellspacing='0' cellpadding='2' align='right'>  
				                            <tr>  
					                            <td height='20'> <div align='right'>" + gw_dt_limit + @"</div> 
					                            </td>  
				                            </tr>  
				                            <tr>  
					                            <td height='20'> <div align='right'>" + gw_qt_po_mm + @"</div> 
					                            </td>  
				                            </tr>   
				                            </table> 
			                            </td> 
                                        <td  width='100' bgcolor='#FFFFFF'> <div align='right'>" + gw_um_ex_po + @"</div> 
			                            </td> 
			                            <td width='110' bgcolor='#FFFFFF'> <div align='right'>" + gw_am + @" </div> 
			                                  
				                        </tr>  
				                        <tr>  
			                                <td width='200' colspan='2' bgcolor='#FFFFFF'>  <div align='left'> " + gw_dc1 + @"&nbsp;</div> 
			                                </td>  
		                                </tr>  
		                            </tr>";

            }
            if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
                gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

            html_source = html_source.Replace("@@LINEDATA", html_source_LINE);

            html_source = html_source.Replace("@@NM_PARTNER", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_TEL", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_FAX", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_PO", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PO", gw_dt_limit + "&nbsp;");

            html_source = html_source.Replace("@@NM_BIZAREA", D.GetString(dt_GWdata.Rows[0]["NM_BIZAREA"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_CEO", D.GetString(dt_GWdata.Rows[0]["NM_CEO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_AD", D.GetString(dt_GWdata.Rows[0]["ADS"]) + "&nbsp;" + D.GetString(dt_GWdata.Rows[0]["ADS_DETAIL"]));
            html_source = html_source.Replace("@@NO_G_TEL", D.GetString(dt_GWdata.Rows[0]["NO_TEL"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_G_FAX", D.GetString(dt_GWdata.Rows[0]["NO_FAX"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_KOR", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");

            html_source = html_source.Replace("@@NM_SL", D.GetString(dt_GWdata.Rows[0]["NM_SL"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_G", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@SUM_AM", sum_am.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@SUM_QT", sum_qt.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@VAT", sum_vat.ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@DC_RMK", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_EXCH", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]));

            return html_source;
        }

        private string 전자결재양식생성_KPIC(string html_source, string no_po)
        {

            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, no_po, Global.MainFrame.LoginInfo.UserID };
            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);

            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
                return "";

            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_NATION", D.GetString(dt_GWdata.Rows[0]["NM_ORGIN"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_CON", D.GetString(dt_GWdata.Rows[0]["NM_COND_PAY"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_CRE", D.GetString(dt_GWdata.Rows[0]["NM_COND_PRICE"]) + "&nbsp;");
            html_source = html_source.Replace("@@UM", D.GetDecimal(dt_GWdata.Rows[0]["UM_EX_PO"]).ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@QT", D.GetDecimal(dt_GWdata.Rows[0]["QT_PO_MM"]).ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@AM", D.GetDecimal(dt_GWdata.Rows[0]["AM_EX"]).ToString("#,###,###,###.####") + "&nbsp;");
            html_source = html_source.Replace("@@DT_PRE", D.GetString(dt_GWdata.Rows[0]["DELIVERY_TIME"]) + "&nbsp;");
            html_source = html_source.Replace("@@DC_RMK", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@DC_TEXT", D.GetString(dt_GWdata.Rows[0]["DC_RMK_TEXT"]).Replace("\n", "<br>") + "&nbsp;");
            html_source = html_source.Replace("@@NM_ITEM", D.GetString(dt_GWdata.Rows[0]["NM_ITEM"]) + "&nbsp;");
            html_source = html_source.Replace("@@SPEC", D.GetString(dt_GWdata.Rows[0]["DC1"]) + "&nbsp;");

            return html_source;
        }

    }
}
