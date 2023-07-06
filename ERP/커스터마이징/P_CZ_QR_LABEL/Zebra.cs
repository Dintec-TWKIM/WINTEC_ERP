using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Threading;
using System.Drawing.Printing;
using ZSDK_API.Comm;
using ZSDK_API.ApiException;
using ZSDK_API.Printer;
using Duzon.Common.Forms;

namespace cz
{
	public class Zebra
	{
		#region ================================================== 속성

		public LabelType LabelType { get; set; }
		public object CD_COMPANY { get; set; }
		public object NO_ACTION { get; set; }
		public object QT_WORK { get; set; }
		public object NM_BUYER { get; set; }
		public object NM_ITEM_PARTNER { get; set; }
		public object NO_DSP { get; set; }
		public object NO_FILE { get; set; }
		public object DT_GR { get; set; }
		public object CD_ITEM_PARTNER { get; set; }
		public object NO_ML { get; set; }
		public object CD_LOCATION { get; set; }
		public object QT_SO { get; set; }
		public object PACK_NO { get; set; }
		public object PACKING_LIST { get; set; }
		public object NM_EMP { get; set; }
		public object NO_PO { get; set; }
		public object NO_SO { get; set; }
		public object NO_REF { get; set; }
		//public object REQ_NO { set { req_no = Convert.ToString(value); } }
		public object NM_SUPPLIER { get; set; }
		//public object SEQ_NO { get; set; }
		public object STOCK_CODE { get; set; }
		//public object SUBJECT_CODE { set { subject_code = Convert.ToString(value); } }
		public object NM_SUBJECT { get; set; }
		public object NM_VESSEL { get; set; }
		public object NO_HULL { get; set; }
		public object NM_WORK { get; set; }
		public object NO_SEQ { get; set; }
		public object NO_LINE { get; set; }
		public object CNT_ITEM { get; set; }

		public object QR { get; set; }
		public object EQ { get; set; }
		public object PART { get; set; }
		public object LOCATION { get; set; }
		public object PART_DESC { get; set; }

		private ZebraPrinterConnection connection;
		private ZebraPrinter printer;
		private PrinterLanguage language;
		#endregion

		public Zebra()
		{
			//comport = new SerialPort("COM1");
			//comport.Encoding = Encoding.Default;
			//comport.Open();
		}

		public void Print(string 회사코드, string Printer, bool Network, bool 선용여부, bool 로고표시)
		{
			string qr = "";
			string zpl = "";

			switch (LabelType)
			{
				case LabelType.GRGI:
					#region ================================================== 일반품 입고라벨
					qr = "V01" + "/" +
						 "D02" + string.Format("{0:###0.##}", QT_WORK) + "/" +
						 "D01" + 회사코드 + "/" +
						 "D03" + NO_PO.ToString().Replace("-", "_2D") + "/" +
						 "D04" + NO_LINE + "/" +
						 "D05" + NO_SEQ;

					if (!string.IsNullOrEmpty(QR.ToString()))
					{
						zpl = ""
							+ StartStr()
							+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
							+ PrintWord(50, 130, "=============================================")
							+ PrintQR(50, 165, QR.ToString(), "4")
							+ PrintWord(150, 165, "Eq:") + PrintWord(190, 165, EQ)
							+ PrintWord(150, 200, "Part No:") + PrintWord(240, 200, PART)
							+ PrintWord(150, 235, "Location:") + PrintWord(250, 235, LOCATION)
							+ PrintWord(50, 305, PART_DESC, 500)
							+ PrintQR(630, 375, qr, "4")
							+ PrintWord(50, 450, "NO." + string.Format("{0:###0.##}", NO_DSP) + "/" + NO_FILE + "/QTY:" + string.Format("{0:#,##0.##}", QT_WORK), 500)
							+ PrintWord(50, 485, NM_VESSEL, 500)
							+ EndStr();
					}
					else
					{
						if (회사코드 == "K200" && NM_SUPPLIER.ToString() == "HSD엔진 주식회사")
						{
							zpl = ""
							+ StartStr()
							+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
							+ PrintWord(50, 130, "NO. " + string.Format("{0:###0.##}", NO_DSP), "60,40")
							+ PrintWord(50, 205, "YOUR ORDER NO.") + PrintWord(260, 205, ":") + PrintWord(275, 205, (NO_REF.ToString().Length > 20 ? NO_REF.ToString().Substring(0, 20) : NO_REF))
							+ PrintWord(50, 240, "OUR REF NO.") + PrintWord(260, 240, ":") + PrintWord(275, 240, NO_FILE.ToString() + " / " + NO_ACTION.ToString())
							+ PrintWord(50, 275, "BUYER") + PrintWord(260, 275, ":") + PrintWord(275, 275, NM_BUYER)
							+ PrintWord(50, 310, "SUBJECT NAME") + PrintWord(260, 310, ":") + PrintWord(275, 310, NM_SUBJECT)
							+ PrintWord(50, 345, "VESSEL NAME") + PrintWord(260, 345, ":") + PrintWord(275, 345, NM_VESSEL)
							+ PrintWord(50, 380, "ITEM CODE") + PrintWord(260, 380, ":") + PrintWord(275, 380, CD_ITEM_PARTNER)
							//+ PrintWord(600, 380, "QTY")			+ PrintWord(660, 380, ":") + PrintWord(680, 380, QT_WORK + (QT_SO == "" || QT_LABEL == QT_SO ? "" : " (" + QT_SO + ")"))
							+ PrintWord(600, 380, "QTY") + PrintWord(660, 380, ":") + PrintWord(680, 380, string.Format("{0:#,##0.##}", QT_WORK))
							+ PrintWord(50, 415, "ITEM NAME") + PrintWord(260, 415, ":") + PrintWord(275, 415, NM_ITEM_PARTNER, 500)
							//+ (location == "" ? "" : PrintWord(500, 520, stock_code + " / " + location))
							+ PrintQR(630, 100, qr, "4")
							+ EndStr();
						}
						else
						{
							string 호선명 = NM_VESSEL.ToString() + "/" + NO_HULL.ToString();

							zpl = ""
							+ StartStr()
							+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
							+ PrintWord(50, 130, "NO. " + string.Format("{0:###0.##}", NO_DSP), "60,40")
							+ PrintWord(50, 205, "YOUR ORDER NO.") + PrintWord(260, 205, ":") + PrintWord(275, 205, (NO_REF.ToString().Length > 20 ? NO_REF.ToString().Substring(0, 20) : NO_REF))
							+ PrintWord(50, 240, "OUR REF NO.") + PrintWord(260, 240, ":") + PrintWord(275, 240, NO_FILE)
							+ PrintWord(50, 275, "BUYER") + PrintWord(260, 275, ":") + PrintWord(275, 275, NM_BUYER)
							+ PrintWord(50, 310, "SUBJECT NAME") + PrintWord(260, 310, ":") + PrintWord(275, 310, NM_SUBJECT)
							+ PrintWord(50, 345, "VESSEL NAME") + PrintWord(260, 345, ":") + PrintWord(275, 345, (호선명.Length > 38 ? NM_VESSEL.ToString().Substring(0, (37 - NO_HULL.ToString().Length)) + "/" + NO_HULL.ToString() : 호선명))
							+ PrintWord(50, 380, "ITEM CODE") + PrintWord(260, 380, ":") + PrintWord(275, 380, CD_ITEM_PARTNER)
							//+ PrintWord(600, 380, "QTY")			+ PrintWord(660, 380, ":") + PrintWord(680, 380, QT_WORK + (QT_SO == "" || QT_LABEL == QT_SO ? "" : " (" + QT_SO + ")"))
							+ PrintWord(600, 380, "QTY") + PrintWord(660, 380, ":") + PrintWord(680, 380, string.Format("{0:#,##0.##}", QT_WORK))
							+ PrintWord(50, 415, "ITEM NAME") + PrintWord(260, 415, ":") + PrintWord(275, 415, NM_ITEM_PARTNER, 500)
							+ PrintWord(50, 485, "ORIGIN : KOREA")
							//+ (location == "" ? "" : PrintWord(500, 520, stock_code + " / " + location))
							+ PrintQR(630, 100, qr, "4")
							+ EndStr();
						}
					}
					#endregion
					break;
				case LabelType.GI:
					#region ================================================== 일반품 출고라벨
					qr = "V01" + "/" +
						 "D02" + string.Format("{0:###0.##}", QT_WORK) + "/" +
						 "D03" + NO_FILE + "/" +
						 "D04" + NO_LINE + "/" +
						 "D05" + NO_SEQ;

					if (!string.IsNullOrEmpty(QR.ToString()))
					{
						zpl = ""
							+ StartStr()
							+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
							+ PrintWord(50, 130, "=============================================")
							+ PrintQR(50, 165, QR.ToString(), "4")
							+ PrintWord(150, 165, "Eq:") + PrintWord(190, 165, EQ)
							+ PrintWord(150, 200, "Part No:") + PrintWord(240, 200, PART)
							+ PrintWord(150, 235, "Location:") + PrintWord(250, 235, LOCATION)
							+ PrintWord(50, 305, PART_DESC, 500)
							+ PrintQR(630, 375, qr, "4")
							+ PrintWord(50, 450, "NO." + string.Format("{0:###0.##}", NO_DSP) + "/" + NO_FILE + "/QTY:" + string.Format("{0:#,##0.##}", QT_WORK), 500)
							+ PrintWord(50, 485, NM_VESSEL, 500)
							+ EndStr();
					}
					else if (선용여부)
					{
						zpl = ""
							+ StartStr()
							+ PrintDintecLogo(230, 30)
							+ PrintDataMatrix(680, 30, qr, "5")
							+ PrintWord(400, 30, NO_FILE)
							+ PrintWord(400, 75, "NO." + string.Format("{0:###0.##}", NO_DSP))
							+ PrintWord(230, 150, (NM_VESSEL.ToString().Length > 25 ? NM_VESSEL.ToString().Substring(0, 25) : NM_VESSEL))
							+ PrintWord(230, 200, (NO_REF.ToString().Length > 25 ? NO_REF.ToString().Substring(0, 25) : NO_REF))
							+ PrintWord(230, 250, (CD_ITEM_PARTNER.ToString().Length > 14 ? CD_ITEM_PARTNER.ToString().Substring(0, 14) : CD_ITEM_PARTNER))
							+ PrintWord(600, 250, "QTY : " + string.Format("{0:#,##0.##}", QT_WORK))
							+ PrintWord(230, 300, (NM_ITEM_PARTNER.ToString().Length > 50 ? NM_ITEM_PARTNER.ToString().Substring(0, 50) : NM_ITEM_PARTNER), 600)
							+ EndStr();
					}
					else
					{
						zpl = ""
							+ StartStr()
							+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
							+ PrintWord(50, 130, "NO. " + string.Format("{0:###0.##}", NO_DSP), "60,40")
							+ PrintWord(50, 205, "YOUR ORDER NO.") + PrintWord(260, 205, ":") + PrintWord(275, 205, (NO_REF.ToString().Length > 20 ? NO_REF.ToString().Substring(0, 20) : NO_REF))
							+ PrintWord(50, 240, "OUR REF NO.") + PrintWord(260, 240, ":") + PrintWord(275, 240, NO_FILE)
							+ PrintWord(50, 275, "BUYER") + PrintWord(260, 275, ":") + PrintWord(275, 275, NM_BUYER)
							+ PrintWord(50, 310, "SUBJECT NAME") + PrintWord(260, 310, ":") + PrintWord(275, 310, NM_SUBJECT)
							+ PrintWord(50, 345, "VESSEL NAME") + PrintWord(260, 345, ":") + PrintWord(275, 345, NM_VESSEL)
							+ PrintWord(50, 380, "ITEM CODE") + PrintWord(260, 380, ":") + PrintWord(275, 380, CD_ITEM_PARTNER)
							//+ PrintWord(600, 380, "QTY")			+ PrintWord(660, 380, ":") + PrintWord(680, 380, QT_WORK + (QT_SO == "" || QT_LABEL == QT_SO ? "" : " (" + QT_SO + ")"))
							+ PrintWord(600, 380, "QTY") + PrintWord(660, 380, ":") + PrintWord(680, 380, string.Format("{0:#,##0.##}", QT_WORK))
							+ PrintWord(50, 415, "ITEM NAME") + PrintWord(260, 415, ":") + PrintWord(275, 415, NM_ITEM_PARTNER, 500)
							+ PrintWord(50, 485, "ORIGIN : KOREA")
							//+ (location == "" ? "" : PrintWord(500, 520, stock_code + " / " + location))
							+ PrintQR(630, 100, qr, "4")
							+ EndStr();
					}
					#endregion
					break;
				case LabelType.Location:
					#region ================================================== 일반품 로케이션라벨
					qr = "V01" + "/" +
						 "D01" + CD_COMPANY + "/" +
						 "D06" + NO_ML;

					zpl = ""
						+ StartStr()
						+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
						+ PrintWord(350, 270, NO_FILE)                      // 파일번호
						+ PrintWord(350, 364, NM_SUPPLIER)                  // 매입처
						+ PrintWord(350, 458, NM_VESSEL)                    // 선명
						+ PrintWord(350, 552, "")                           // 품명
						+ PrintWord(350, 646, CNT_ITEM + "종")              // 수량(몇종)
						+ PrintWord(350, 740, this.GetTO_Date(DT_GR))       // 입고일
						+ PrintWord(350, 834, NM_EMP + "   " + NO_ACTION)   // 비고
						+ PrintQR(640, 20, qr, "6")
						+ EndStr();
					#endregion
					break;
				case LabelType.GR_Stock:
					#region ================================================== 재고품 입고라벨
					qr = "V01" + "/" +
						 "D03" + NO_PO + "/" +
						 "D04" + NO_LINE + "/" +
						 "D05" + NO_SEQ;

					zpl = ""
						+ StartStr()
						+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
						+ PrintWord(350, 176, CD_LOCATION + " / " + CD_ITEM_PARTNER)
						+ PrintWord(350, 270, NO_PO)                                                            // 발주번호(ST)
						+ PrintWord(350, 364, NM_SUPPLIER)                                                      // 매입처
						+ PrintWord(350, 458, NM_ITEM_PARTNER, 480)                                             // 품명
						+ PrintWord(350, 646, string.Format("{0:#,##0.##}", QT_WORK))                           // 수량
						+ PrintWord(350, 740, DateTime.Now.ToShortDateString())                                 // 출력일
						+ PrintWord(350, 834, NM_EMP + "   " + NO_ACTION)                                       // 비고
						+ PrintQR(640, 20, qr, "6")
						+ EndStr();
					#endregion
					break;
				case LabelType.GI_Stock:
					#region ================================================== 재고품 출고라벨
					qr = "V01" + "/" +
						 "D02" + string.Format("{0:###0.##}", QT_WORK) + "/" +
						 "D03" + NO_FILE + "/" +
						 "D04" + NO_LINE + "/" +
						 "D05" + NO_SEQ;

					if (선용여부)
					{
						zpl = ""
							+ StartStr()
							+ PrintDintecLogo(230, 30)
							+ PrintDataMatrix(680, 30, qr, "5")
							+ PrintWord(400, 30, NO_FILE)
							+ PrintWord(400, 75, "NO." + string.Format("{0:###0.##}", NO_DSP))
							+ PrintWord(230, 150, (NM_VESSEL.ToString().Length > 25 ? NM_VESSEL.ToString().Substring(0, 25) : NM_VESSEL))
							+ PrintWord(230, 200, (NO_REF.ToString().Length > 25 ? NO_REF.ToString().Substring(0, 25) : NO_REF))
							+ PrintWord(230, 250, (CD_ITEM_PARTNER.ToString().Length > 14 ? CD_ITEM_PARTNER.ToString().Substring(0, 14) : CD_ITEM_PARTNER))
							+ PrintWord(600, 250, "QTY : " + string.Format("{0:#,##0.##}", QT_WORK))
							+ PrintWord(230, 300, (NM_ITEM_PARTNER.ToString().Length > 50 ? NM_ITEM_PARTNER.ToString().Substring(0, 50) : NM_ITEM_PARTNER), 600)
							+ EndStr();
					}
					else
					{
						zpl = ""
							+ StartStr()
							+ (로고표시 == true ? (회사코드 == "K100" ? PrintDintecLogo_New(50, 30) : (회사코드 == "K200" ? PrintDubhecoLogo_New(50, 30) : "")) : "")
							+ PrintWord(50, 130, "NO. " + string.Format("{0:###0.##}", NO_DSP), "60,40")
							+ PrintWord(50, 205, "YOUR ORDER NO.") + PrintWord(260, 205, ":") + PrintWord(275, 205, (NO_REF.ToString().Length > 20 ? NO_REF.ToString().Substring(0, 20) : NO_REF))
							+ PrintWord(50, 240, "OUR REF NO.") + PrintWord(260, 240, ":") + PrintWord(275, 240, NO_FILE)
							+ PrintWord(50, 275, "BUYER") + PrintWord(260, 275, ":") + PrintWord(275, 275, NM_BUYER)
							+ PrintWord(50, 310, "SUBJECT NAME") + PrintWord(260, 310, ":") + PrintWord(275, 310, NM_SUBJECT)
							+ PrintWord(50, 345, "VESSEL NAME") + PrintWord(260, 345, ":") + PrintWord(275, 345, NM_VESSEL)
							+ PrintWord(50, 380, "ITEM CODE") + PrintWord(260, 380, ":") + PrintWord(275, 380, CD_ITEM_PARTNER)
							//+ PrintWord(600, 380, "QTY")			+ PrintWord(660, 380, ":") + PrintWord(680, 380, QT_WORK + (QT_SO == "" || QT_LABEL == QT_SO ? "" : " (" + QT_SO + ")"))
							+ PrintWord(600, 380, "QTY") + PrintWord(660, 380, ":") + PrintWord(680, 380, string.Format("{0:#,##0.##}", QT_WORK))
							+ PrintWord(50, 415, "ITEM NAME") + PrintWord(260, 415, ":") + PrintWord(275, 415, NM_ITEM_PARTNER, 500)
							+ (CD_LOCATION == "" ? "" : PrintWord(500, 520, STOCK_CODE + " / " + CD_LOCATION))
							+ PrintQR(630, 100, qr, "4")
							+ EndStr();
					}
					#endregion
					break;
					//case LabelType.PackingList:
					//    qr = packing_list;

					//    zpl = ""
					//        + StartStr()
					//        + PrintWord(100, 50, "Packing List : " + pack_no + " (" + NM_WORK + ")", "60,40")
					//        + PrintQR(100, 100, packing_list, "6")
					//        + EndStr();
					//    break;
			}

			if (string.IsNullOrEmpty(Printer))
			{
				Global.MainFrame.ShowMessage("프린터 설정을 확인하세요!");
			}
			else
			{
				if (Network == true)
				{
					Encoding korean = Encoding.GetEncoding(949);
					byte[] bytes = korean.GetBytes(zpl);

					this.connection.Write(bytes);
				}
				else
				{
					#region USB 인쇄
					System.Text.Encoding korean = System.Text.Encoding.GetEncoding(949);
					byte[] bytes = korean.GetBytes(zpl);
					IntPtr pUnmanagedBytes = new IntPtr(0);
					int nLength = bytes.Length;
					pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
					Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);

					RawPrinterHelper.SendBytesToPrinter(Printer, pUnmanagedBytes, nLength);
					#endregion
				}
			}

			ClearValue();
		}

		private DateTime GetTO_Date(object value)
		{
			// 14글자를 변환
			string s = value.ToString();
			if (s.Length == 14)
			{
				s = s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2)
					+ " " + s.Substring(8, 2) + ":" + s.Substring(10, 2) + ":" + s.Substring(12, 2);
			}

			return Convert.ToDateTime(s);
		}

		private string StartStr()
		{
			string zpl = ""
				+ "^XA"                         // 명령 시작
				+ "^LH0,0"                      // Home 좌표 설정
				+ "^POI"                       // 전체 회전
				+ "^SEE:UHANGUL.DAT^FS"          // 한글폰트 설정
				+ "^CW1,E:KFONT3.FNT^CI26^FS";  // 한글폰트 설정
			return zpl;
		}

		private string EndStr()
		{
			string zpl = ""
				+ "^XZ";            // 명령 종료
			return zpl;
		}

		private string PrintWord(int x, int y, object value)
		{
			string size = "";
			if (LabelType == LabelType.GRGI || LabelType == LabelType.GI || LabelType == LabelType.GI_Stock)
				size = "35,22";
			if (LabelType == LabelType.Location || LabelType == LabelType.GR_Stock)
				size = "45,28";

			return PrintWord(x, y, value, size);
		}

		private string PrintWord(int x, int y, object value, string size)
		{
			string zpl = string.Format(""
				+ "^FO{0},{1}"      // 위치
				+ "^A1N,{3}"        // 한글FONT (가로x세로 사이즈)
				+ "^FD{2}^FS"       // DATA
				, x, y, value, size);
			return zpl;
		}

		private string PrintWord(int x, int y, object value, int wrapWidth)
		{
			string size = "";
			if (LabelType == LabelType.GRGI || LabelType == LabelType.GI || LabelType == LabelType.GI_Stock)
				size = "35,22";
			if (LabelType == LabelType.Location || LabelType == LabelType.GR_Stock)
				size = "45,28";

			string zpl = string.Format(""
				+ "^FO{0},{1}"      // 위치
				+ "^A1N,{3}"        // 한글FONT (가로x세로 사이즈)
				+ "^FB{4},4,,,"     // Wrap사이즈
				+ "^FD{2}^FS"       // DATA
				, x, y, value, size, wrapWidth);
			return zpl;
		}

		private string PrintQR(int x, int y, string value)
		{
			string zpl = string.Format(""
				+ "^FO{0},{1}"      // 위치
				+ "^BQN,2,4"        // 뒤에 숫자가 size (7, 8, 9, 10 ...)
				+ "^FH"             // HEX MODE
									//+ "^FDMM,A{2}^FS"	// QR CODE
				+ "^FDMM,B1000{2}^FS"   // QR CODE
				, x, y, value);
			return zpl;
		}

		private string PrintQR(int x, int y, string value, string size)
		{
			string zpl = string.Format(""
				+ "^FO{0},{1}"      // 위치
				+ "^BQN,2,{3}"      // 뒤에 숫자가 size (7, 8, 9, 10 ...)
				+ "^FH"             // HEX MODE
									//+ "^FDMM,A{2}^FS"	// QR CODE
				+ "^FDMM,B1000{2}^FS"   // QR CODE
				, x, y, value, size);
			return zpl;
		}

	    private string PrintDataMatrix(int x, int y, string value, string size)
		{
			string zpl = string.Format(@"^FO{0},{1}^BXN,{2},200^FD{3}^FS", x, y, size, value);

			return zpl;
		}

		private string PrintDintecLogo(int x, int y)
		{
			string zpl = string.Format(@"^FO{0},{1}^GFA,1938,1938,19,W0E38F,::W0E38E,:W0E38F,:W0E38E,::::W0E38F,::W0E38E,W0E38F,:::::::::::W0E38FR07C,7IF8I07FE07FE00E38F01FF9IFC003FF8,7JF8007FE07FF00E38F01FF9IFC00JF,7JFC007FE07FF00E38F01C01EJ01F01F,7JFE007FE07FF00E38E01801CJ03E00F,7KF007FE07FF00E38F01801CJ03C007,7KF007FE07FF00E38F01801CJ038003,7KF807FE07FF80E38F01801CJ07I01,7KF807FE07FF80E38F01801CJ0E,7KFC07FE07FFC0E38F01801CI01C,:7LF07FE07FFE0E38F01801CI038,7LF07FE07IF0E38F01801CI038,7FF7IF07FE07IF0E38F01801CI038,7FE1IF07FE07IF0E38E01801CI078,7FE07FF07FE07IF8E38E01801CI07,:7FE03FF87FE07IF8E38601801CI06,7FE03FF87FE07IFCE38601801EI06,7FE03FF87FE07IFCE38601801IF86,7FE01FF87FE07IFEE38F01801IF86,7FE01FF87FE07JFE38F01801EI06,7FE01FF87FE07FDFFE38F01801CI06,7FE01FF87FE07FCFFE38F01801CI06,7FE01FF87FE07FCFFE38E01801CI06,:7FE03FF87FE07FC7FE38E01801CI06,::7FE07FF07FE07FC3FE38F01801CI07,7FE0IF07FE07FC3FE38E01801CI078,7FF7IF07FE07FC1FE38E01801CI038,7LF07FE07FC1FE38E01801CI038,7LF07FE07FC0FE38E01801CI038,7KFE07FE07FC0FE38F01801CI03C,7KFC07FE07FC07E38E01801CI01E,7KFC07FE07FC07E38E01801CJ0E,7KF807FE07FC07E38F01801CJ07,7KF007FE07FC03E38F01801CJ038007,7JFE007FE07FC03E38E01801CJ03E00F,7JFE007FE07FC03E38E01801FJ01F01F,7JFE007FE07FC01E38E01801IFC01FC3F,7JF8007FE07FC01E38E01801IFC00IFE,7IFEI07FE07FC01E38E01801IFC007FFC,W0E38ER0FE,W0E38E,W0E38F,W0E38E,:W0E38F,W0E38E,::W0E38F,::W0E38E,:::::::W0E38F,:W0E38E,:::W0E38F,:::^FS", x, y);

			return zpl;
		}

		private string PrintDintecLogo_New(int x, int y)
		{
			string zpl = string.Format(@"^FO{0},{1}^GFA,4484,4484,59,,::W070C38,W070E38,:::::::::::::::::W070E38Q03FhS0C,3IFEI03FF83FF0070E380FFC7IF001FFEK0IFJ07E07EI07F1JFC7IFI03FFL01FF8001FFEO03F00JFE3FFC,3JFC003FF83FF8070E380FFC7IF007IF8I01IFEI0FF0FFI07F1JFC7IF801IF8K07FFC007IF8N07F80JFE3IFC,3KF003FF83FFC070E380FFC7IF00FC0FCI01JF800FF0FF8007F1JFC7IF803IF8J01IFE00JFEN07F80JFE3IFE,3KFC03FF83FFC070E380E007J01E001CI01JFE00FF0FFC007F1JFC7IF807IF8J03IFE03KFN07F80JFE3JF8,3KFE03FF83FFE070E380E007J03CI04I01KF00FF0FFE007F1JFC7IF80JF8J07IFE07KF8M07F80JFE3JFC,3LF03FF83FFE070E380E007J078M01KF80FF0FFE007F1JFC7IF81JF8J0JFE07KFCM07F80JFE3JFE,3LF03FF83IF070E380E007J0FN01FE7FF80FF0IF007F10FF847F8103JF8J0JFE0IF3FFCM07F8087F803F9IF,3LF83FF83IF870E380E007J0EN01FE07FC0FF0IF807F007F007FI03FE018I01FF80E1FF807FEM07F8003F803F81FF,3LF83FF83IF870E380E007I01CN01FE03FE0FF0IFC07F007F007FI07FC008I03FE0061FF001FEM07F8003F803F80FF8,3FF87FFC3FF83IFC70E380E007I01CN01FE01FE0FF0IFE07F007F007FI07F8L03FCI03FE001FFM07F8003F803F807F8,3FF83FFC3FF83IFC70E380E007I01CN01FE01FE0FF0IFE07F007F007FI0FFM03FCI03FCI0FFM07F8003F803F803FC,3FF81FFC3FF83IFE70E380E007I038N01FE00FF0FF0JF07F007F007IF0FFM07F8I03FCI07FM07F8003F803F803FC,3FF80FFC3FF83JF70E380E007FFE38N01FE00FF0FF0FF7F87F007F007IF0FFM07F8I03F8I07F8L07F8003F803F803FC,3FF80FFC3FF83JF70E380E007FFE38N01FE00FF0FF0FF7FC7F007F007IF0FFM07F8I03F8I07F8L07F8003F803F801FC,3FF80FFC3FF83KF0E380E007FFE38N01FE00FF0FF0FF3FC7F007F007IF0FEM07F8I03F8I07F8L07F8003F803F801FC,3FF80FFC3FF83FF7FF0E380E007I038N01FE00FF0FF0FF1FE7F007F007IF0FEM07F8I03F8I07F8L07F8003F803F801FC,3FF80FFC3FF83FF3FF0E380E007I038N01FE00FF0FF0FF0FF7F007F007IF0FFM07F8I03F8I07FM07F8003F803F803FC,3FF81FFC3FF83FF3FF0E380E007I038N01FE00FF0FF0FF0JF007F007F800FFM07F8I03FCI0FFM07F8003F803F803FC,3FF81FFC3FF83FF1FF0E380E007I038N01FE00FE0FF0FF07IF007F007FI0FFM03F8I03FCI0FFM07F8003F803F803FC,3FF83FFC3FF83FF1FF0E380E007I01CN01FE01FE0FF0FF03IF007F007FI07F8L03FCI01FE001FFM07F8003F803F807F8,3FF8IFC3FF83FF0FF0E380E007I01CN01FE03FE0FF0FF01IF007F007FI07FC008I03FE0061FF003FEM07F8003F803F80FF8,3LF83FF83FF0FF0E380E007I01EN01FE07FC0FF0FF00IF007F007FI07FE018I01FF80E1FF807FE03EJ07F8003F803F81FF01F,3LF83FF83FF07F0E380E007J0EN01FEIF80FF0FF00IF007F007IF03JF8I01JFE0LFC07E00FC7FBF83F803FBIF03F83LF03FF83FF03F0E380E007J07N01KF80FF0FF007FF007F007IF81JF8J0JFE07KFC0FF01F87IFC3F803JFE03FC3KFE03FF83FF03F0E380E007J078I04I01KF00FF0FF003FF007F007IF80JF8J07IFE07KF80FF01F87IF83F803JFC07FC3KFC03FF83FF01F0E380E007J03EI0CI01JFE00FF0FF001FF007F007IF807IF8J03IFE03KF00FF03F07IF83F803JF807FC3KF803FF83FF01F0E380E007J01F807CI01JF800FF0FF001FF007F007IF803IF8J01IFE00JFE00FF03F07IF83F803IFE003FC3KF003FF83FF00F0E380E007IF007IFCI01IFEI0FF0FFI0FF007F007IF800IF8K07FFE007IF8007F03E07IFC3F803IFC003F83JFC003FF83FF0070E380E007IF003IFK0IFJ0FE0FEI07F007F007IFI03FFL01FF8001FFEI07E07C03IF83F803FFCI01F,3IF8I03FF03FE0070E380C007IFI07F8hQ01EM07C,W070E38iT078,W070E38iT0F8,W070E38iT0F,:W070E38,::::::::::::::W070C38,,::^FS", x, y);

			return zpl;
		}

		private string PrintDubhecoLogo_New(int x, int y)
		{
			string zpl = string.Format(@"^FO{0},{1}^GFA,1944,1944,36,,hV03EJ018,hU0FFCK0F,hS01IF8600187F,hR03JF1E010E1FE,hQ01JFE3C210F8FFE,hQ0KFCFC718FE7FFC,hP03KFBFC71C7FBIFC,hP0NFCF1E7LF8,hO03NF9F1F3MF8,hO0E007KF9F9FBMF,hN018001KFBFBF9LFE,hN02J07LFBFDLF8,hS03LFBNF,hT0TFC,hT07SF,01KFI0FF8007FC7JFE007F800FFI07IF800IFC01IF803RFE,01KFE00FFI07F87KFC0FF801FF003JF007IF80JFE01RF8,01LF01FFI0FF8LFE0FF801FF00JFE01JF83KF007QF,01LF81FFI0FF8MF0FF801FF03JFC07JF0LF803PFC,03FE03FFC1FEI0FF0FF007FF8FF001FE07FFJ0FFE001FFC1FFC01PF,03FE00FFC3FE001FF1FF003FF9FF003FE0FF8I01FF8003FE007FE007NFE,03FC007FE3FE001FF1FF001FF9FF003FE1FFJ03FEI07FC003FE003NF8,07FC007FE3FE001FE1FE001FF1FE003FC3FEJ07FCI0FF8003FE001NF,07FC007FE3FC003FE3FE003FF3FE007FC7FCJ0FFCI0FFI03FEI07LFC,07F8003FE7FC003FE3FE007FE3FE007FCFF8J0FF8001FFI03FEI03LF,0FF8003FE7FC003FE3LFC3LF8LFDFFI03FEI03FEI01KFE,0FF8007FC7F8007FC7LF87LF8LF9FFI03FEI03FEJ07JF8,0FF8007FCFF8007FC7KFE07LF9LF1FFI03FCI03FEJ03JF,0FFI07FCFF8007FC7LF07LF1LF3FEI07FCI03FCJ01IFC,1FFI0FF8FFI07F87LF8MF1KFE3FEI07FCI07FCK07FF,1FFI0FF9FFI0FF8FF803FF8FF801FF1FFJ03FEI07FCI07F8K03FE,1FE001FF1FFI0FF8FF800FF8FF801FF1FFJ03FEI07FCI0FF8K01F8,3FE003FF1FFI0FF0FFI0FF8FF001FE1FFJ03FFI07FC001FFM06,3FE007FE1FF001FF1FF001FF9FF003FE1FF8I03FFI07FE003FE,3FC00FFC1FF001FF1FF003FF1FF003FE1FFCI01FF8003FF007FC,7FC07FF81FF801FE1FE00IF1FE003FC0FFEI01FFE003FF83FF8,7KFE00LFE3LFE3FE007FC0KFC0KF1LF,7KF800LFE3LF83FE007FC07JFC07IFE0KFC,7JFEI07KFE3KFE03FC007F801JF803IFC07JF,KFJ01KFC7KF007FC00FF8007IFI07FF801IF8,,::::gJ0FE1831F0819FC07E1F800187F9FC,gI01831833188110018060C001808306,gI018190221183300300C04001018202,gI01013067E1FF3F020180C003018202,gI03033066110220020180C002010606,gI030E30C43306600300C30102030418,gI03F01F0FC2067F01F87E3207F307E18,hI04,^FS", x, y);

			return zpl;
		}

		private void ClearValue()
		{
			NO_REF = "";
			NO_FILE = "";
			NM_BUYER = "";
			NM_SUBJECT = "";
			NM_VESSEL = "";
			CD_ITEM_PARTNER = "";
			QT_WORK = "";
			NM_ITEM_PARTNER = "";
			NO_PO = "";
		}

		public bool Connect(string ip, int port)
		{
			try
			{
				connection = new TcpPrinterConnection(ip, port);
			}
			catch (ZebraException)
			{
				MessageBox.Show("COMM Error! Disconnected");
				return false;
			}

			try
			{
				connection.Open();
			}
			catch (ZebraPrinterConnectionException)
			{
				MessageBox.Show("Unable to connect with printer");
				this.Disconnect();
				return false;
			}
			catch (ZebraGeneralException e)
			{
				MessageBox.Show(e.Message);
				this.Disconnect();
				return false;
			}
			catch (Exception)
			{
				MessageBox.Show("Error communicating with printer");
				this.Disconnect();
				return false;
			}

			printer = null;

			if (connection != null && connection.IsConnected())
			{
				try
				{
					printer = ZebraPrinterFactory.GetInstance(connection);
					language = printer.GetPrinterControlLanguage();
					return true;
				}
				catch (ZebraPrinterConnectionException)
				{
					MessageBox.Show("Unknown Printer Language");
					printer = null;
					this.Disconnect();
					return false;
				}
				catch (ZebraPrinterLanguageUnknownException)
				{
					MessageBox.Show("Unknown Printer Language");
					printer = null;
					this.Disconnect();
					return false;
				}
			}

			return false;
		}

		public void Disconnect()
		{
			try
			{
				if (connection != null && connection.IsConnected())
					connection.Close();
			}
			catch (ZebraException)
			{
				MessageBox.Show("COMM Error! Disconnected");
			}

			connection = null;
		}
	}

	public enum LabelType
	{
		None,
		GRGI,
		GI,
		GI_Stock,
		GR_Stock,
		Location,
		PackingList
	}
}