//********************************************************************
// 작   성   자 : 장은경
// 작   성   일 : 2010.06.07
// 수   정   자 : 
// 수   정   일 : 
// 모   듈   명 : 영업
// 시 스  템 명 : 출하의뢰관리
// 서브시스템명 : 
// 페 이 지  명 : 납품의뢰현황 인쇄 도움창
// 프로젝트  명 : P_SA_GIRSCH_SUB
//********************************************************************
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    public partial class P_SA_GIRSCH_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_SA_GIRSCH_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
        }

        internal bool Get출고검사요청출력 { get { return rdo출고검사.Checked; } }
    }
}
