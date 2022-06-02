using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit_proj.Message
{
    public enum PageName
    {
        M_P, Selected,Login
    }
    class GoToPage
    {
        public GoToPage(PageName pageNm)
        {
            PageName = pageNm;
        }

        public GoToPage(PageName pageNm, object _param)
        {
            PageName = pageNm;
            Param = _param;
        }
        public PageName PageName { get; private set; }
        public object Param { get; private set; }
    }
}
