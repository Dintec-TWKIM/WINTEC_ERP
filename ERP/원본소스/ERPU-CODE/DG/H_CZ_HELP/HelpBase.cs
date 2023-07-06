using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms.Help.Forms;
using Duzon.Common.Forms.Help;
using System.Data;

namespace cz
{
    /// <summary>
    /// 도움창상위클래스
    /// </summary>
    public class HelpBase : HelpUser
    {
        IHelp _IHelp;
        string _타이틀명;
        string _HelpID;
        List<string> _listParam;
        bool _IsYNCol = false;

        public HelpBase()
        {
        }
        public HelpBase(HelpParam helpParam)
            : base(helpParam)
        {
            Init(helpParam);
        }

        private void Init(HelpParam helpParam)
        {
            //파라미터List로변환
            SetHelpParam(helpParam.UserParams);
        }

        #region->ovride 메서드

        protected override void InitControl()
        {
            base.InitControl();
            
        }

        protected override DataTable Search()
        {
            return OnCodeSearch(_IHelp.Get검색);

        }

        public override DataTable OnCodeSearch(string val)
        {
            return _IHelp.SetDetail(_IHelp.Get검색);
        }

        protected override DataTable RefreshSearch()
        {
            return Search();
        }

        #endregion

        #region->private 메서드

        private void SetHelpParam(string param)
        {
            List<string> list = new List<string>();
            GetList(ref list, param);

            if (list.Count == 0) throw new Exception("도움창명이 설정되지 않았습니다");
            else if (list.Count == 1) throw new Exception("도움창ID가 설정되지 않았습니다");

            _타이틀명 = list[0];
            _HelpID = list[1];

            list.RemoveAt(0);
            list.RemoveAt(0);

            System.Diagnostics.Debug.WriteLine(param);

            _listParam = list;

        }

        private void GetList(ref List<string> list, string param)
        {
            string[] strs = param.Split(';');
            foreach (string st in strs)
                list.Add(st);
        }

        #endregion

        #region->속성

        internal IHelp SetIHelp { set { _IHelp = value; } }
        public List<string> GetListParam{get { return _listParam; }}
        public string Get타이틀명{get { return _타이틀명; }}
        public string GetHelpID{get { return _HelpID; }}
        public bool GetIIsYNCol{get { return _IsYNCol; }}

        #endregion

       
    }
}
