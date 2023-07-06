using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	class P_CZ_SA_LOG_PLAN_MNG_BIZ
	{
        public DataTable Search차수(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_REV_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search협조전(bool isShip, object[] obj)
        {
            DataTable dt;

            if (isShip == true)
                dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_GIR_SHIP_S", obj);
            else
                dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_GIR_DELIVERY_S", obj);
            
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search협조전1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_GIR_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search선적(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_VESSEL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search선적Detail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_SHIP_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search전달(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_DELIVERY_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search상용구(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_WORD_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search대행실적(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_OLD_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search대행실적Line(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_LOG_PLAN_MNG_OLD_L_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(DataTable dt차수, DataTable dt상용구, DataTable dt선적H, DataTable dt선적L, DataTable dt전달, DataTable dt대행실적, DataTable dt대행실적L)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt차수 != null && dt차수.Rows.Count != 0)
            {
                #region 차수
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt차수);
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_LOG_PLAN_MNG_REV_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt상용구 != null && dt상용구.Rows.Count != 0)
            {
                #region 상용구
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt상용구);
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_LOG_PLAN_MNG_WORD_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt선적H != null && dt선적H.Rows.Count != 0)
            {
                #region 선적 Header
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt선적H);
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_LOG_PLAN_MNG_VESSEL_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt선적L != null && dt선적L.Rows.Count != 0)
            {
                #region 선적 Line
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt선적L);
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_LOG_PLAN_MNG_SHIP_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt전달 != null && dt전달.Rows.Count != 0)
            {
                #region 전달
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt전달);
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_LOG_PLAN_MNG_DELIVERY_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt대행실적 != null && dt대행실적.Rows.Count != 0)
            {
                #region 대행실적
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt대행실적);
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_LOG_PLAN_MNG_OLD_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt대행실적L != null && dt대행실적L.Rows.Count != 0)
            {
                #region 대행실적 Line
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt대행실적L);
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_LOG_PLAN_MNG_OLD_L_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            return DBHelper.Save(sc);
        }
    }
}
