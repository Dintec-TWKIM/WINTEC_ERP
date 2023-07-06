using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace pur
{
    public class P_PU_EVAL_REG_BIZ
    {
        private IMainFrame _mf;

        private P_PU_EVAL_REG_BIZ()
        {
        }

        public P_PU_EVAL_REG_BIZ(IMainFrame mf)
        {
            _mf = mf;
        }


        public ResultData Eval(object[] m_obj)
        {

                if (Config.MA_ENV.PJT������ != "Y" || ((Config.MA_ENV.PJT������ == "Y") && (Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡴���") == "000")))
                {
                    switch (Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡹��"))
                    {

                        case "000": // ����չ�
                            if (Global.MainFrame.ServerKeyCommon == "KBSM")
                                return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_TOTAL_KBS", m_obj);
                            else
                                return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_TOTAL", m_obj);
                        case "100": // ���Լ����
                            return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_FIFO", m_obj);

                        case "200": // �̵���չ� 20100316
                            return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_MOVE", m_obj);

                        default:
                            _mf.ShowMessage(Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡹��"));
                            return null;

                    }
                }
                else{


                    if(Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡴���") == "100")
                    {
                        switch (Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡹��"))
                        {

                            case "000": // ����չ�
                                if (Global.MainFrame.ServerKeyCommon == "NTS2")
                                {
                                    return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_TOTAL_PJT_NTS", m_obj);

                                }
                                return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_TOTAL_PJT", m_obj);
                            
                            case "100": // ���Լ����
                                return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_FIFO_PJT", m_obj);

                        }
                    }
                    else if (Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡴���") == "200")
                    {
                        switch (Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡹��"))
                        {

                            case "000": // ����չ�
                                return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_TOTAL_UNIT", m_obj);


                        }
                    }

                }


                 return null;
   


        }


        public ResultData Eval_WONIK(object[] m_obj)
        {
            if ((string)m_obj[1] == "3000")
                return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_TOTAL", m_obj);
            else
            {
                switch (Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡹��"))
                {

                    case "000": // ����չ�
                            return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_TOTAL", m_obj);
                    case "100": // ���Լ����
                        return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_FIFO", m_obj);

                    case "200": // �̵���չ� 20100316
                        return (ResultData)_mf.ExecSp("UP_PU_AMINV_PROCESS_MOVE", m_obj);

                    default:
                        _mf.ShowMessage(Duzon.ERPU.MF.ComFunc.�����ڵ�("����򰡹��"));
                        return null;

                }
            }
        }



        public DataTable Search(object[] args)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_EVAL_SELECT";
            si.SpParamsSelect = args;
            ResultData result = (ResultData)_mf.FillDataTable(si);
            return (DataTable)result.DataValue;

        }

        public ResultData Delete(object[] m_obj)
        {
            return (ResultData)_mf.ExecSp("UP_PU_EVAL_DELETE", m_obj);
        }

         
        #region ������� ���� üũ 
         
        public bool �򰡱Ⱓüũ(string CD_PLANT, string YM_FROM, string YM_TO, string GUBUN)
        {
            string YY = D.GetString(YM_TO.Substring(0, 4) );
            string Query = string.Empty;

            if (GUBUN == "SEARCH")
            {
                Query = "  SELECT COUNT(*) AS COUNT  " +
                            "  FROM MM_AMINVH" +
                            "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                            "  AND  CD_PLANT = '" + CD_PLANT + "'" +
                           // "  AND   YY_AIS =  '" + YY + "' " +
                            "  AND  YM_STANDARD >='" + YM_FROM + "' "
                            ;
            }
            else
            {
                Query = "  SELECT COUNT(*) AS COUNT   " +
                            "  FROM MM_AMINVH" +
                            "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                            "  AND  CD_PLANT = '" + CD_PLANT + "'" +
                            //"  AND   YY_AIS =  '" + YY + "' " +
                            "  AND  YM_STANDARD >'" + YM_TO + "' "
                            ;
            }


            DataTable dt = DBHelper.GetDataTable(Query);

            if (D.GetDecimal(dt.Rows[0]["COUNT"]) > 0 && GUBUN == "SEARCH")
            {
                Global.MainFrame.ShowMessage("����򰡰� �̷���� �Ⱓ �Դϴ�.�ٽ� Ȯ�� �� �����ּ���.");
                return false;

            }
            else if(D.GetDecimal(dt.Rows[0]["COUNT"]) > 0 && GUBUN == "DELETE")
            {
                Global.MainFrame.ShowMessage("�������������� ����򰡵Ǿ��ֽ��ϴ�. ������ �� �����ϴ�.");
                return false;
            }

            return true;
            //return D.GetDecimal(dt.Rows[0]["COUNT"]); 

        }

        public bool Send(string CD_PLANT, string YM_STANDARD_TO)
        {
            bool rtn = false;

            rtn = DBHelper.ExecuteNonQuery("UP_PU_Z_DAEYONG_MM_AMINVL_LOG_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode, CD_PLANT, YM_STANDARD_TO });

            return rtn;
        }
      
        #endregion


    }
}
