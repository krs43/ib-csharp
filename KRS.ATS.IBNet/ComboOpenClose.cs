using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    public enum ComboOpenClose : int
    {
        SAME = 0, // open/close leg value is same as combo
        OPEN = 1,
        CLOSE = 2,
        UNKNOWN = 3,
    }
}
