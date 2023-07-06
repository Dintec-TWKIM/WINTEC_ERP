using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ZSDK_API.ApiException;
using ZSDK_API.Comm;
using ZSDK_API.Printer;

namespace Dintec
{
	public class Zebra
	{
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        private string printerUSB;

        private ZebraPrinterConnection connection;
        private ZebraPrinter printer;
        private PrinterLanguage language;

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        private bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }

            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        private bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }
        private bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        public bool ConnectUSBPrinter()
		{
			foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
			{
				if (printer.IndexOf("ZD230") > 0)
				{
					this.printerUSB = printer;
					break;
				}
			}

            if (string.IsNullOrEmpty(this.printerUSB))
                return false;
            else
                return true;            
		}

        public bool Connect(string ip, int port)
        {
            try
            {
                this.connection = new TcpPrinterConnection(ip, port);
            }
            catch (ZebraException)
            {
                //COMM Error! Disconnected
                return false;
            }

            try
            {
                this.connection.Open();
            }
            catch (ZebraPrinterConnectionException)
            {
                //Unable to connect with printer
                this.Disconnect();
                return false;
            }
            catch (ZebraGeneralException e)
            {
                this.Disconnect();
                return false;
            }
            catch (Exception)
            {
                //Error communicating with printer
                this.Disconnect();
                return false;
            }

            printer = null;

            if (this.connection != null && this.connection.IsConnected())
            {
                try
                {
                    printer = ZebraPrinterFactory.GetInstance(connection);
                    language = printer.GetPrinterControlLanguage();
                    return true;
                }
                catch (ZebraPrinterConnectionException)
                {
                    //Unknown Printer Language
                    printer = null;
                    this.Disconnect();
                    return false;
                }
                catch (ZebraPrinterLanguageUnknownException)
                {
                    //Unknown Printer Language
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
                //COMM Error! Disconnected
            }

            connection = null;
        }

        public bool PrintUSBQR(string qr)
		{
            if (string.IsNullOrEmpty(this.printerUSB))
                return false;

            string zpl = string.Empty;

            zpl += this.StartStr();

			zpl += string.Format("^FO355,20^BXN,10,200^FD{0}^FS", qr); // DataMatrix
            
			zpl += this.EndStr();

            System.Text.Encoding korean = System.Text.Encoding.GetEncoding(949);
            byte[] bytes = korean.GetBytes(zpl);
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength = bytes.Length;
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);

            this.SendBytesToPrinter(this.printerUSB, pUnmanagedBytes, nLength);

            return true;
        }

        public bool PrintUSBText(string text)
        {
            if (string.IsNullOrEmpty(this.printerUSB))
                return false;

            string zpl = string.Empty;

            zpl += this.StartStr();

            zpl += string.Format("^XA^FO280,75^A0N,50,50^FD{0}^FS^XZ", text); // Text

            zpl += this.EndStr();

            System.Text.Encoding korean = System.Text.Encoding.GetEncoding(949);
            byte[] bytes = korean.GetBytes(zpl);
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength = bytes.Length;
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);

            this.SendBytesToPrinter(this.printerUSB, pUnmanagedBytes, nLength);

            return true;
        }

        public bool PrintLANQR(string qr)
		{
            if (this.printer == null) return false;

            string zpl = string.Empty;

            zpl += this.StartStr();

            zpl += string.Format("^FO175,20^BXN,10,200^FD{0}^FS", qr); // DataMatrix

            zpl += this.EndStr();

            Encoding korean = Encoding.GetEncoding(949);
            byte[] bytes = korean.GetBytes(zpl);

            this.connection.Write(bytes);

            return true;
        }

        public bool PrintLANText(string[] text)
        {
            if (this.printer == null) return false;

            string zpl = string.Empty;

            zpl += this.StartStr();

            if (text.Length > 0)
                zpl += string.Format(@"^FO100,25^A0B,35,35^FD{0}^FS", text[0]); // Text
            if (text.Length > 1)
                zpl += string.Format(@"^FO150,25^A0B,35,35^FD{0}^FS", text[1]);
            if (text.Length > 2)
                zpl += string.Format(@"^FO200,25^A0B,35,35^FD{0}^FS", text[2]);
            if (text.Length > 3)
                zpl += string.Format(@"^FO250,25^A0B,35,35^FD{0}^FS", text[3]);
            if (text.Length > 4)
                zpl += string.Format(@"^FO300,25^A0B,35,35^FD{0}^FS", text[4]);
            if (text.Length > 5)
                zpl += string.Format(@"^FO350,25^A0B,35,35^FD{0}^FS", text[5]);

            zpl += this.EndStr();

            Encoding korean = Encoding.GetEncoding(949);
            byte[] bytes = korean.GetBytes(zpl);

            this.connection.Write(bytes);

            return true;
        }

        public bool PrintShippingMark(DataRow dr)
		{
            if (this.printer == null) return false;

            string zpl = string.Empty;
            string 주소 = string.Empty;
            string 연락처 = string.Empty;
            string 로고 = string.Empty;

            switch (dr["CD_COMPANY"].ToString())
			{
                case "K100":
                    주소 = "DINTEC Bldg., Jungang-Darero 309, Dong-Gu, Busan, 601-836, Korea";
                    연락처 = "Tel : +82-(0)51-664-1000/1100,  Fax: +82-(0)51-462-7907-9,  E-mail : service@dintec.co.kr";
                    로고 = "^FO200,500^GFA,7982,7982,13,,::M01OF,M03OF,:::::::::::M03FF8I07FF,M03FF8I03FF,:M03FF8I07FF,M03FFCI07FF,:M03FFCI0IF,M01FFCI0IF,M01IF001IF,M01IF803FFE,N0NFE,N0NFC,::N07MF8,N03MF8,N03MF,N01LFE,O0LFC,O07KFC,O01KF,P0JFE,Q0FFC,,::::M01C008J03,M03OF,:::::::::::M01OF,,::::::M01IF7KF,M03OF,:::::::::M01IFEKF,R03JF,R03IFE,R07IFC,Q01JF,Q03IFC,P01JF,P03IFE,P0JF8,O01JF,O07IF8,O0JF,N03IFC,N0JFC,07PFEPF8,0gHFC,:07gGF8,,:::gH03,0gHF8,0gHFC,07gGF8,,::::07gGF8,0gHF8,:,:::::::::M01OF,M03OF,M01OF,V07,V03,:::::::,:::M01OF,M03OF,::M03CI038007,M03CI03I03,M03CI038003,::::::::::M03CI03I03,:M018I03I03,,::Q07F8,P07IFC,P0JFE,O03F003F8,O07CI0FC,O0FJ03F,N01FJ01F,N03CK078,N078K03C,N0FL01C,N0FL01E,N0EM0E,M01CM0F,M01CM07,M03CM03,M038M03,M038M038,:M03N038,::M038M038,:M03CM03,M03CM07,M01CM07,M01CM0F,N0EM0E,N0FL01E,N07L03C,N03L03C,,:::::::::::::::::::::N06I02I0C,N0OF,::::::::N0FFK0FF,N0FEK0FF,:::N0FFK0FF,:N0FFJ01FF,N0FF8I03FE,N0FFCI03FE,N07FEI0FFC,N07FFI0FFC,N03FFE1IFC,N03MF8,N01MF,O0MF,O07KFE,O03KFC,O01KF,P0KF,P03IF,Q0FFE,,:::::N0NFE,N0OF,:::::::N0NFE,,:::::N0OF,:::::::N0JFCJF,S07FFE,S0IFC,S0IF8,R03FFE,R07FFC,Q01IF8,Q03IF,Q0IFC,Q0IF8,P03IF,P07FFE,P0IF8,O01IF,O07FFC,O0IFC,N03FFE,N07FFC,N0IFE,N0JF7IFE,N0OF,:::::::,:::U0FF,::::::T01FF,N0OF,:::::::N04K01FF,U0FF,::::::,:::N0NFE,N0OF,:::::::N0FF8FFE1FF,N0FE03FC0FF,::::::::N0FE03F80FF,N048,,:::Q01F,P07IFC,P0JFE,O03KF8,O03KFC,O0LFE,O0MF,N03MF8,:N07FFE0IFC,N07FF803FFC,N0FFEI0FFE,N0FFCI07FF,N0FF8I03FF,:M01FFJ01FF,M01FFK0FF,:M03FEK0FF,:M03FFK0FF,M01FFK0FF,:M01FFJ01FF,N0FF8I03FF,N0FFCI03FF,N0FFCI07FE,P08,,::::::::::::::::::::P03IF,P07IFC,O01KF,O03KF8,O07KFE,O0MF,N01MF,N03MF8,N03MFC,N07FFC0IFC,N0IF001FFE,N0FFEI0FFE,N0FFCI03FF,N0FF8I03FF,M01FFJ01FF,M01FFK0FF,::M03FEK0FF,:M01FFK0FF,::M01FFJ01FF,N0FF8I03FF,N0FFCI03FF,N07FCI03FC,,::::P01IF8,P03IFC,O01KF8,O03KFC,O0LFE,O0MF,N03MF,N03MF8,N07IF9IFC,N0IFE07FFC,N0IFI0FFE,N0FFEI0IF,N0FFCI03FF,M01FF8I03FF,M01FFJ01FF,M01FFK0FF,M03FFK0FF,M03FEK0FF8,:::M03FFK0FF8,M03FFK0FF,M01FFK0FF,M01FFJ01FF,M01FF8I03FF,N0FFCI07FF,N0FFEI0FFE,N0IF803FFE,N07FFC07FFC,N03MFC,N03MF8,N01MF,O0MF,O07KFC,O03KFC,P0KF,P07IFC,Q07FE,,::::N07E,N0FF,N0FF8,M01FF8,M03FFC,:M01FF8,N0FF8,N0FF,N07F,,:::::L07C,L0FE,L0FFE,L07FF,L03IF,L01IF,M03FF,M01FF,N07F,N03F,O07,O03,,::::N06C04I07E,N0OF,:::::::N0FF,N0FE,::::::::N0FEK0FF,N0FCK0FF,U0FF,:::::N0OF,:::::::N04K01FF,U0FF,::::::,:::N0NFE,N0OF,:::::::N0FFJ01FF,N0FEK0FF,:::N0FFK0FF,:N0FFJ01FF,N0FFJ01FE,N0FF8I03FE,N0FFCI07FC,N07FEI0FFC,N03FF003FFC,N03MFC,N03MF8,N01MF,O0MF,O07KFC,O03KFC,P0KF,P0JFE,Q0IF,Q07FC,,:::N03C,N07E,N0FF,M01FF8,:M03FFC,:M01FF8,N0FF,:N03C,,::^FS";
                    break;
                case "K200":
                    주소 = "297, Jungang-daero, Dong-gu, Busan, Korea / Postcode: 48792";
                    연락처 = "Tel: +82 70 7496 2100, Fax: +82 51 465 2105, e-mail: service@dubheco.com";
                    로고 = "^FO200,630^GFA,6356,6356,14,O0E,O0F,O0FF8,O0FFC,O0IFE,O0JF8,O0KFC,O0LF,O0MF,O0MFC,O0NFE,O0OF8,O0PF,O0PF8,::O0FF3MF8,O0FE1MF8,O0FE01LF8,O0FE007KF8,O0FEI03JF8,O0FEJ0JF8,O0FEK0IF8,O0FEK03FF8,O0FEL03F8,:O0FFL03F8,:::O07FL03F8,O07F8K03F8,O03FCK03F8,:O03FEK03F8,O03FFK03F8,O03FF8J07F8,O01FFCJ0FF8,P0IFJ0FF8,P0IF8001FF8,P0JF807FF8,P07NF,P03NF,::P01NF,Q0NF,Q07LFE,Q03LFC,Q01LFC,R0LF8,R07KF,R01KF,S0JFE,T0IF,T03FE,Q0FC,P03FF,P0JF,P0JFC,O01KFE,O03LF8,O03MFC,O03NF,O07OF,O0PF8,::::O0FFC3LF8,O0FF807KF8,O0FFI07JF8,O0FFI01JF8,O0FEK0IF8,O0FEK03FF8,O0FEL03F8,O0FEM0F8,O0FE,::::::::O0FF,O0FF8,O0FFE,O0JF,O0JF8,O0KFC,O0LF,O0MF8,O0MFE,O0NFE,O0OF8,O0PF,O0PF8,O01OF8,P07NF8,Q07MF8,Q01MF8,S0LF8,S03KF8,O0F8I03JF8,O0FEJ0JF8,O0IFJ07FF8,O0IF8I01FF8,O0JFCJ0F8,O0KFJ078,O0LF8,O0LFC,O0MFE,O0NF8,O0OFC,O0OFE,O0PF,O0PF8,::O0FE0MF8,O0FE03LF8,O0FE00LF8,O0FE007KF8,:O0FE007F1IF8,O0FE007F01FF8,O0FE007F007F8,O0FE007F003F8,:::O0FF007F003F8,::O0FF00FF003F8,O07F80FF003F8,O07FC0FF003F8,O07FE1FF003F8,O03FF3FF803F8,O03KF803F8,O03KFC07F8,O03KFC0FF8,O03KFE0FF8,O01OF8,P0OF8,P0OF,:P07NF,P03NF,:P01NF,Q0FFE3IFE,Q03FC3IFE,R0701IFC,O0EK0IFC,O0FEJ0IFC,O0FFJ07FF8,O0IF8001FF,O0IFEI0FE,O0KF,O0KFC,O0LFE,O0MF,O0NF8,O0NFE,O0PF,:O07OF8,O03OF8,P01NF8,Q07MF8,R03LF8,R01LF8,S0LF8,S07KF8,S07F1IF8,S07F07FF8,S07F003F8,S07F001F8,S07FI03,S07F,::::O0F8007F,O0FC007F,O0IF07F,O0IF8FF,01M0LF,03EL0LF,07FFCJ0LF8,0JF8I0LFC,0FBIFC00MFE,071IFE00NF8,J01FF00OFC,K07F00OFE,L0300PF8,I0E003007OF8,I0F3K07NF8,I0F3K01NF8,I0F387K0MF8,I0F387K03LF8,I0F3C7L03KF8,I0F3C7M0KF8,I0F3C7N07IF8,020F3C7N03IF8,07FF3C3J01FC001FF8,07FF1E7J07FFI07F8,07FF1FFI01JFI038,07FF0FFI03JF8001,07FF0FFI07KF,03FF0FFI0LF,003F0FFI0LFC,I0F0FF001LFC,I0F1FF003MF,I0F3FF003MF,I0F3E7003MFC,I0F3C7007MFC,I0F3C7007MFE,I0F3C700OF,:I0F38700IFCKF,I0F38700FFE07JF8,060030700FFC07JFC,0FJ0300FF807F1FFC,0FJ0300FF007F0FFC,07FEK0FF007F07FE,03FF8J0FF007F03FE,01KF00FE007F01FF,00KF00FE007F01FF,I01IF00FE007F00FF,J07FF00FE007F00FF,O0FE007F007F,:O0FE007F003F8,0038K0FE007F003F8,003CK0FE007F003F8,:003C3FC00FE007F003F8,003C3FF00FE007F003F8,003C7FF007E007F003F8,003C7FF003E007F003F8,003C7FFI0F007F003F8,003C7FFI0E007F003F8,07FC7DFL07F003F8,07FC78FL03F003F8,07FC78FL01F003F8,07FC70FM0F003F8,07FC70FM03003F8,:03FC70FJ01FCI03F8,007C70FJ03FFI01FC,003C70FI01IFEI07C,003C70FI03JF8003C,003C70FI07JFEI08,003C78FI0LF,003C78FI0LFC,003C70F001LFC,003C70F003MF,:003C78F003MFC,003C30F007MFC,J030F007MFE,L0700OF,O0OF,O0IFC3JF,O0FFE001IF8,0038K0FFCI0IFC,007FEJ0FF8I03FFC,00IFCI0FFJ01FFC,00KF00FFK07FC,00KF00FFK03FE,00KF00FEK03FF,00F87FF00FEK01FF,00F03FF00FEL0FF,00F03E300FEL0FF,00FF3CI0FEL07F,00IFEI0FEL07F,00JFE00FEL03F,007JF00FEL03F8,001JF00FEL03F8,I03IF007EL03F8,0700FBF003EL03F8,0FC0F0F001EL03F8,0JFK0FL03F8,07IF8J02L03F8,03KFP03F8,:0303IFJ07FF8003F8,03007FFJ0IFC003F8,07FC00FI03JFC03F8,07FFC03I07JFC03F8,0KFJ0LF00F8,07JFEI0LF807C,00KF001LFE03C,001JF003MF018,J01FF003MF8,K07F003MFC,O07MFC,O07MFE,03M0OF,07M0OF,07M0IF007IF8,078L0FFE001IF8,07807J0FF8I03FFC,0700F0600FFJ03FFC,0700F0F00FFK0FFC,0780F0F00FFK07FE,07C0F0F00FFK03FE,07F0F0F00FEK01FF,07FC70F00FEL0FF,:07FC70F00FEL0FF01,07FC70F00FEL07F00C,07FC70F00FFL03F00C,07FCF0F00FFL03F80C,0780F0F00FFL03F806,0700F0F007FL03F803,0780F0F007F8K03F803,0700F0F007FCK03F8038,070CF0F003FCK03F8038,070CF0F003FEK03F803C,070FFCF003FFK03F801C,070JF003FFK07F801C,070JF001FFCJ07F801E,070JFI0FFEJ0FF801E,070JFI0IFC003FF801F,0307IFI0IFE003FF801F,J07FEI07NF001F,K0FCI07NF003F8,P03NF003F8,P03NF003FC,Q0NF003FC,Q0MFE003FC,Q03LFE007FC,Q03LFC007FC,R0LFC00FFE,R0LF800FFE,R03KF001FFE,S0JFE001IF,S01IFC003IF,T0IFI07IF,g0JF,:Y01JF,Y03JF,Y07JF8,:Y0KF8,X01KF8,X03KF8,:X07KF8,X0LF8,W01LF8,W01LFC,W03LFC,W07LFE,W0MFE,W0KFCFE,V01KFC7E,V03KFC3E,V07KFC1E,V0LFC1E,V0LFE0E,U01MF07,U03MF83,U03MFC1,U07MFC,U0NFC,T01NFE,T03KFE3FE,T03KFE07F,T07KFE01F,T0MF8008,T0MFC,S01MFE,S03NF8,S07NFE,S0PF,S0PF8,R01PFC,R03OFC,R03NFE,R0NF8,R0NF,:R0NF8,R07PF8,R03PF8,R03PF,R01OFE,S0OFC,S0OF8,S07MFE,S03MFC018,S03MF003,S03MF01F,S01LFC07E,T0LFC1FE,T0OFC,T07NF8,T03NF8,T03NF82,T01NF06,U0MFE06,U0MFE0C,U07LFC18,:U03LFC38,U03LFCF8,U01NF8,V0NF,:V07MF,V03LFE,:V01LFE,W0LFE,W0LFC,W07KFC,W03KFC,W03KF8,W01KF8,:X0KF8,:X07JF,X03JF,X01IFE,:Y0IFE,:Y07FFC,Y03FFC,:Y01FF8,g0FF8,:g07F8,g03F,::g01F,gG0E,gG06,:gG02,,:^FS";
                    break;
                case "S100":
                    주소 = "30 Toh Guan Road, #07-07 ODC DISTRI CENTRE, Singapore 608840";
                    연락처 = "Tel: +65 6896 4434,   Fax: +65 6896 4244,   E-mail: service@dintec.com.sg";
                    로고 = "^FO200,500^GFA,6600,6600,11,,:L01NF,L03NF,::::::::::L03FFI03FF,:::L03FF8003FF,L03FF8007FF,L01FFC007FF,L01FFC00IF,L01FFE01IF,L01MFE,:M0MFC,M07LF8,:M03LF,M01LF,N0KFE,N07JF8,N01JF,O0IFE,O03FF,,::::L03NF,::::::::::,::::::L03NF,:::::::::Q0JF,:P01IFC,P07IF,O01IFC,O07IF,N01IFC,:N07IF,M01IFC,M07IF,L01JF,0YFC,::,::::0YFC,:,::::0YFC,:,::::::::L01KF7FB,L03NF,L01NF,T07,:::::::T03,,::L03NF,::L03C001E007,L03CI0E007,:::::::::::L038I04007,,:P07C,O03FF,O0IFC,N03JF8,N0FC007C,M01FI01F,M03CJ07,M038J078,M07K038,M06K01E,L01EL0E,L01CL0F,L01CL07,L03CL07,:L038L038,:::::L038L07,L03CL07,L01CL0F,L01EL0F,L01EL0E,M0EK01E,M07K018,,::::::::::::::::::::I01,I0780038,I0FC01FF007LFE,I0FC03FFC0MFE,001FE07FFE0MFE,001FC07FFE0MFE,001FC0IFE0MFE,001F80JF0MFE,003F00FC1F8MFE,003F00FC0F8FCJ07E,003E01F80F8FCJ07E,003C01F80F8FCJ07E,::003E01F00F8FCJ07E,003F03F01F87CJ07E,003F07E01F87CJ07E,001F8FE03F07EJ07E,001IFE07F07EJ0FE,001IFC0FE07FJ0FC,001IFC0FE07F8001FC,I0IF807C03FE007FC,I07FF801801LF8,I01FE001800LF8,J0FEL0LF,R07KF,R03JFC,R01JF,S03FFC,,:001KF1F,::::001JFE1E,Q07LFE,Q0MFE,::::001KFI07LFE,001KF,::::L07E,L01F,:M0FI07LFE,M0FI0MFE,L01FI0MFE,L03FI0MFE,001KFI0MFE,:001JFEI07LFE,001JFCN0FFC,001JFCM01FF8,001JFN03FF,U0FF8,T03FF,T0FFC,S01FF8,S03FF,K0FCL07FC,00387FF8J03FF,00F8IFCJ07FE,01F9IFEI01FF8,01FBJFI07LFC,01FBJFI07LFE,01FBF87FI0MFE,03F7E01FI0MFE,03E7C00FI0MFE,03E7C007I0MFE,:03E7C007I07LFE,03E3E01F,01F3E03F,01LF,::00LF,007KF,001KFO07E,W07E,:::::I07F8Q07E,I0FFC7CO07E,I0FFE3EI07LFE,001FFE3EI0MFE,:001IF3FI0MFE,001F1F3FI0MFE,001E1F0FI0MFE,001C0F07I07LFE,001C0F07I03JFEFE,001E0F07O07E,001F0787O07E,001F879FO07E,001KFO07E,::001JFEO07E,:001FA18,,:::::03LFI07LFE,03LFI0MFE,::::I0FC07FI0MFE,I0F803FI0FE07F07E,I0F001FI0FC07E07E,001F001FI0FC03E07E,001FI0FI0FC03E07E,001F001FI0FC03E07E,001F801FI0FC03E07E,001FC03FI0FC03E07E,I0FC07FI0FC03E07E,I0KFI0FC03E07E,I0JFEI0FC03E07E,I07IFCI0FC03E07E,I03IF8I0FC03E07E,J0FFEJ0FC03E07E,J07FCJ0FC03E07E,Q0FC03E07E,:Q0FCJ07E,J03FK078,J0FFE,I01IF,I07IFC,I0JFE,001JFE,001FE0FFL07C,001F803FK03FFE,001F803FK0IFE,003F001FJ03JF8,003EI0FJ07JFE,003CI0FJ0LF,003F001FI01LF8,001F001FI03LFC,001F803FI07FF83FFC,001FC07FI07FC003FC,001JFEI07F8I0FE,I0JFEI0FEJ07E,I07IFCI0FCJ07E,I03IF8I0FCJ03E,I01IFJ0FCJ03E,J07FCJ0FCJ03E,Q0FCJ03E,:Q0FCJ07E,:Q0FEJ07E,I0JFEI0FEJ0FE,001KFI07FC001FE,001KFI07FE007FC,001KFI01FE007FC,001KFI01FE007F8,001KFJ0FE003F8,L0FEJ07E003F,L03EJ03C0038,L03FK0C,L01F,:::,::J07FC,J0FFE,I03IF8,I07IFC,I0JFE,001KF,001FCF3F,001F8F3F,001F0F1F,003F0F0F,003C0F07,003E0F1F,003F0F1F,001F0F3F,001F0IF,001FCFFE,:I0F8FFC,I078FF,I078FE,I030F8,,::::::::::::::::::::001LFE,001MF,::::I081FC21F,K0FC01F,K07800F,K0F800F,::K0FC01F,:K07C03F,K07F0FE,K03IFE,:K01IFC,:L0IF8,L03FE,,::M06,M0F,L01F,J0KFC,I0LFE,001LFE,:::003F001F,003CI0F,003C001F,001C001F,001CI06,,:J07FC,I03IF,I03IF8,I07IFC,I0JFE,001KF,001FCF3F,001F8F3F,001F0F1F,003F0F0F,003E0F07,003C0F0F,003F0F1F,001F0F3F,001F0IF,001F8IF,001FCFFE,I0FCFFC,I078FF8,I038FC,I030F8,,::::::001F8,:::::,:::::::::::::::::::::::001MF,:::::001F,001E,:001F,:::::::001E,,:M0F,:L01F,:I0LFE,001LFE,::003LFE,003F001F,003CI0F,:001CI06,,::J018,I01FFE,I07IF8,I0JFC,I0JFE,001JFE,001FE1FF,003F803F,003E001F,003C001F,::003E001F,001F803E,001FE1FF,001MF,::::,::::::I0F,001F8,:::::I0F,,:::::::::::::^FS";
                    break;
            }

            zpl += string.Format(@"^XA

^SEE:UHANGUL.DAT^FS
^CW1,E:KFONT3.FNT^CI26^FS

^FO50,50^GB1080,1570,3^FS
^CF0,100
^FO1000,250^A0R^FDSHIP'S SPARES IN TRANSIT^FS
^CFA,70
^FO850,100^A0R^FDMASTER OF^FS
^FO850,550^A1R,55^FD{0}^FS
^FO850,550^GB3,1000,3^FS
^FO750,100^A0R^FDORDER NO.^FS
^FO750,550^A1R,40^FB1000,2^FD{1}^FS
^FO750,550^GB3,1000,3^FS
^FO650,100^A0R^FDCASE NO.^FS
^FO650,550^A1R,55^FD{2}^FS
^FO650,550^GB3,1000,3^FS
^FO550,100^A0R^FDDESTINATION^FS
^FO550,550^A1R,55^FD{3}^FS
^FO550,550^GB3,1000,3^FS
^FO450,100^A0R^FDORIGIN^FS
^FO450,550^A1R,55^FDMADE IN {4}^FS
^FO450,550^GB3,1000,3^FS
^FO350,100^A1R,55^FD{5}^FS
^FO350,100^GB3,1150,3^FS

{9}

^FO220,1300^BQN,2,5^FDMM,B1000{6}^FS

^FO50,50^GB110,1570,3^FS
^CFA,30
^FO110,350^A1R^FD{7}^FS
^FO60,100^A1R^FD{8}^FS

^XZ", (dr["NM_VESSEL"].ToString() + " / " + dr["NO_HULL"].ToString()),
      (dr["NO_PO_PARTNER"].ToString().Length > 82 ? dr["NO_PO_PARTNER"].ToString().Left(82) : dr["NO_PO_PARTNER"].ToString()),
      (dr["QT_WIDTH"].ToString() != "0" ? dr["NM_PACK"].ToString() + " / " + dr["QT_PACK"].ToString() + " (" + dr["QT_WIDTH"].ToString() + "X" + dr["QT_LENGTH"].ToString() + "X" + dr["QT_HEIGHT"].ToString() + ")" : 
                                          dr["NM_PACK"].ToString() + " / " + dr["QT_PACK"].ToString()),
      dr["PORT_ARRIVER"].ToString(),
      (dr["NM_ORIGIN"].ToString() + "   " + dr["DT_COMPLETE"].ToString()),
      dr["DC_RMK_SHIPPING"].ToString(),
      dr["NM_VESSEL"].ToString() + " / " + dr["NO_PO_PARTNER"].ToString(),
      주소,
      연락처,
      로고);

            Encoding korean = Encoding.GetEncoding(949);
            byte[] bytes = korean.GetBytes(zpl);

            this.connection.Write(bytes);

            return true;
        }

        public bool PrintPackingList(DataRow dr, DataRow[] dataRowArray)
        {
            if (this.printer == null) return false;

            DataRow dataRow;
            Encoding korean = Encoding.GetEncoding(949);
            byte[] bytes;
            string zpl = string.Empty;
            string zpl1 = @"^FO70,{4}^A1N^FD{0}^FS
^FO150,{4}^A1N^FD{1}^FS
^FO350,{4}^A1N^FD{2}^FS
^FO1150,{4}^A1N^FD{3}^FS";
            string zpl2 = string.Empty;
            string 회사명 = string.Empty;
            string 납품처 = string.Empty;
            string 호선 = string.Empty;

            int 행위치 = 760;

            switch (dr["CD_COMPANY"].ToString())
			{
                case "K100":
                    회사명 = "DINTEC CO., LTD.";
                    break;
                case "K200":
                    회사명 = "DUBHE CO., LTD";
                    break;
                case "S100":
                    회사명 = "DINTEC SINGAPORE PTE.LTD.";
                    break;
            }

            납품처 = dr["DC_RMK_PACKING"].ToString();
            납품처 = (납품처.Length > 155 ? 납품처.Left(155) : 납품처);

            호선 = "MASTER OF " + (dr["NM_VESSEL"].ToString() + " / " + dr["NO_HULL"].ToString());
            호선 = (호선.Length > 34 ? 호선.Left(34) : 호선);

            dataRowArray = dataRowArray.ToDataTable().Select("TP_ROW = 'I'");

            if (dataRowArray.Length < 17)
			{
                #region 한페이지
                foreach (DataRow dr1 in dataRowArray)
                {
                    zpl2 += string.Format(zpl1, string.Format("{0:#,###}", dr1["NO_DSP"]),
                                                (dr1["CD_ITEM_PARTNER"].ToString().Length > 11 ? dr1["CD_ITEM_PARTNER"].ToString().Left(11) : dr1["CD_ITEM_PARTNER"].ToString()),
                                                (dr1["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ").Length > 45 ? dr1["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ").Left(45) : dr1["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ")),
                                                string.Format("{0:#,###}", Convert.ToInt32(dr1["QT_PACK"])),
                                                행위치.ToString());

                    행위치 += 50;
                }

                zpl = string.Format(@"^XA

^SEE:UHANGUL.DAT^FS
^CW1,E:KFONT3.FNT^CI26^FS

^CF0,50
^FO400,50^FDPACKING LIST {0}^FS

^FO100,30^BXN,7,200^FD{13}^FS

^CF0,30
^FO800,150^FDFrom. {15}^FS
^FO800,200^FD{16}^FS
^FO60,170^A1N^FDTo. {1}^FS
^FO110,220^A1N^FD{2}^FS

^CF0,40
^FO60,250^A1N^FB1120,3^FD{14}^FS

^CF0,30

^FO60,400^GB1120,270,3^FS

^FO70,420^FDDATE :^FS
^FO160,420^A1N^FD{3}^FS
^FO70,470^FDINVOICE NO :^FS
^FO240,470^A1N^FD{4}^FS
^FO70,520^FDOUR REF. NO :^FS
^FO260,520^A1N^FD{5}^FS
^FO70,570^A1N^FDMADE IN {11}^FS
^FO70,620^FDYR ORD NO.:^FS
^FO240,620^A1N^FD{6}^FS

^FO650,420^FDGROSS WEIGHT :^FS
^FO870,420^A1N^FD{7} KGS^FS
^FO650,470^FDNET WEIGHT :^FS
^FO830,470^A1N^FD{8} KGS^FS
^FO650,520^FDMEASUREMENT :^FS
^FO870,520^A1N^FD{9} (MM)^FS
^FO650,570^FDPACKING STYLE :^FS
^FO870,570^A1N^FD{10}^FS

^FO70,700^FDNO^FS
^FO150,700^FDCODE^FS
^FO350,700^FDDESCRIPTION^FS
^FO1100,700^FDQTY^FS
^FO70,750^GB1100,3,3^FS

^CF0,25

{12}

^FO70,1610^GB1100,3,3^FS

^FO70,1640^FD* WE DECLARE THAT OUR PRODUCTS ARE FREE OF ASBESTOS.^FS

^XZ", ("(" + dr["NM_PACK"].ToString() + "/" + dr["QT_PACK"].ToString() + ")"),
      (dr["NM_PARTNER"].ToString().Length > 34 ? dr["NM_PARTNER"].ToString().Left(34) : dr["NM_PARTNER"].ToString()),
      호선,
      Util.GetTo_DateStringS(dr["DT_IO"].ToString()),
      dr["NO_IO"].ToString(),
      dataRowArray[0]["NO_FILE"].ToString(),
      (dr["NO_PO_PARTNER"].ToString().Length > 56 ? dr["NO_PO_PARTNER"].ToString().Left(56) : dr["NO_PO_PARTNER"].ToString()),
      string.Format("{0:#,###.#}", Convert.ToDecimal(dr["QT_GROSS_WEIGHT"])),
      string.Format("{0:#,###.#}", Convert.ToDecimal(dr["QT_NET_WEIGHT"])),
      (dr["QT_WIDTH"].ToString() + "X" + dr["QT_LENGTH"].ToString() + "X" + dr["QT_HEIGHT"].ToString()),
      dr["NM_TYPE"].ToString(),
      dr["NM_ORIGIN"].ToString(),
      zpl2,
      string.Format("V01/D07{0}/D08{1}", dr["NO_PACK"].ToString(), dr["NO_GIR"].ToString()),
      납품처,
      회사명,
      dr["DC_RMK_PACKING1"].ToString());

                bytes = korean.GetBytes(zpl);

                this.connection.Write(bytes);
                #endregion
            }
			else
			{
                #region 여러페이지

                #region 첫페이지
                for (int index = 0; index < 17; index++)
				{
                    dataRow = dataRowArray[index];

                    zpl2 += string.Format(zpl1, string.Format("{0:#,###}", dataRow["NO_DSP"]),
                                                (dataRow["CD_ITEM_PARTNER"].ToString().Length > 11 ? dataRow["CD_ITEM_PARTNER"].ToString().Left(11) : dataRow["CD_ITEM_PARTNER"].ToString()),
                                                (dataRow["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ").Length > 45 ? dataRow["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ").Left(45) : dataRow["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ")),
                                                string.Format("{0:#,###}", Convert.ToInt32(dataRow["QT_PACK"])),
                                                행위치.ToString());

                    행위치 += 50;
                }

                zpl = string.Format(@"^XA

^SEE:UHANGUL.DAT^FS
^CW1,E:KFONT3.FNT^CI26^FS

^CF0,50
^FO400,50^FDPACKING LIST {0}^FS

^FO100,30^BXN,7,200^FD{13}^FS

^CF0,30
^FO800,150^FDFrom. {15}^FS
^FO800,200^FD{16}^FS
^FO60,170^A1N^FDTo. {1}^FS
^FO110,220^A1N^FD{2}^FS

^CF0,40
^FO60,250^A1N^FB1120,3^FD{14}^FS

^CF0,30

^FO60,400^GB1120,270,3^FS

^FO70,420^FDDATE :^FS
^FO160,420^A1N^FD{3}^FS
^FO70,470^FDINVOICE NO :^FS
^FO240,470^A1N^FD{4}^FS
^FO70,520^FDOUR REF. NO :^FS
^FO260,520^A1N^FD{5}^FS
^FO70,570^A1N^FDMADE IN {11}^FS
^FO70,620^FDYR ORD NO.:^FS
^FO240,620^A1N^FD{6}^FS

^FO650,420^FDGROSS WEIGHT :^FS
^FO870,420^A1N^FD{7} KGS^FS
^FO650,470^FDNET WEIGHT :^FS
^FO830,470^A1N^FD{8} KGS^FS
^FO650,520^FDMEASUREMENT :^FS
^FO870,520^A1N^FD{9} (MM)^FS
^FO650,570^FDPACKING STYLE :^FS
^FO870,570^A1N^FD{10}^FS

^FO70,700^FDNO^FS
^FO150,700^FDCODE^FS
^FO350,700^FDDESCRIPTION^FS
^FO1100,700^FDQTY^FS
^FO70,750^GB1100,3,3^FS

^CF0,25

{12}

^FO70,1610^GB1100,3,3^FS

^FO70,1640^FD* WE DECLARE THAT OUR PRODUCTS ARE FREE OF ASBESTOS.^FS

^XZ", ("(" + dr["NM_PACK"].ToString() + "/" + dr["QT_PACK"].ToString() + ")"),
          (dr["NM_PARTNER"].ToString().Length > 34 ? dr["NM_PARTNER"].ToString().Left(34) : dr["NM_PARTNER"].ToString()),
          호선,
          Util.GetTo_DateStringS(dr["DT_IO"].ToString()),
          dr["NO_IO"].ToString(),
          dataRowArray[0]["NO_FILE"].ToString(),
          (dr["NO_PO_PARTNER"].ToString().Length > 56 ? dr["NO_PO_PARTNER"].ToString().Left(56) : dr["NO_PO_PARTNER"].ToString()),
          string.Format("{0:#,###.#}", Convert.ToDecimal(dr["QT_GROSS_WEIGHT"])),
          string.Format("{0:#,###.#}", Convert.ToDecimal(dr["QT_NET_WEIGHT"])),
          (dr["QT_WIDTH"].ToString() + "X" + dr["QT_LENGTH"].ToString() + "X" + dr["QT_HEIGHT"].ToString()),
          dr["NM_TYPE"].ToString(),
          dr["NM_ORIGIN"].ToString(),
          zpl2,
          string.Format("V01/D07{0}/D08{1}", dr["NO_PACK"].ToString(), dr["NO_GIR"].ToString()),
          납품처,
          회사명,
          dr["DC_RMK_PACKING1"].ToString());
                #endregion

                #region 다음페이지
                int groupUnit = 32;
                
                for (int i = 17; i < dataRowArray.Length; i += groupUnit)
                {
                    행위치 = 110;
                    zpl2 = string.Empty;

                    for (int index = i; index < (i + groupUnit > dataRowArray.Length ? dataRowArray.Length : i + groupUnit); index++)
                    {
                        dataRow = dataRowArray[index];

                        zpl2 += string.Format(zpl1, string.Format("{0:#,###}", dataRow["NO_DSP"]),
                                                    (dataRow["CD_ITEM_PARTNER"].ToString().Length > 11 ? dataRow["CD_ITEM_PARTNER"].ToString().Left(11) : dataRow["CD_ITEM_PARTNER"].ToString()),
                                                    (dataRow["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ").Length > 45 ? dataRow["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ").Left(45) : dataRow["NM_ITEM_PARTNER"].ToString().Replace(Environment.NewLine, " ")),
                                                    string.Format("{0:#,###}", Convert.ToInt32(dataRow["QT_PACK"])),
                                                    행위치.ToString());

                        행위치 += 50;
                    }

                    zpl += string.Format(@"^XA

^SEE:UHANGUL.DAT^FS
^CW1,E:KFONT3.FNT^CI26^FS

^CF0,30

^FO70,50^FDNO^FS
^FO150,50^FDCODE^FS
^FO350,50^FDDESCRIPTION^FS
^FO1150,50^FDQTY^FS
^FO70,100^GB1150,3,3^FS

^CF0,25

{0}

^XZ", zpl2);
                }

                bytes = korean.GetBytes(zpl);

                this.connection.Write(bytes);
                #endregion

                #endregion
            }

			return true;
        }

        private string StartStr()
        {
            return "^XA";
        }

        private string EndStr()
        {
            return "^XZ";
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
    }
}
