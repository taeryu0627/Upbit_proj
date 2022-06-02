using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit_proj.Message
{
    public enum PopupName { Buy, Result, Check, Close }
    public class PopupPage
    {
        public PopupPage(PopupName popupName)
        {
            PopupName = popupName;
        }

        public PopupPage(PopupName popupName, object _param)
        {
            PopupName = popupName;
            Param = _param;
        }

        public PopupPage(PopupName popupName, object _param, object __param)
        {
            PopupName = popupName;
            Param = _param;
            Params = __param;
        }

        public PopupName PopupName { get; private set; }
        public object Param { get; private set; }
        public object Params { get; private set; }
    }
}
