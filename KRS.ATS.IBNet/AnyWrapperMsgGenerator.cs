using System;

namespace KRS.ATS.IBNet
{
    public class AnyWrapperMsgGenerator
    {
        public static System.String error(System.Exception ex)
        {
            //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            return "Error - " + ex;
        }
        public static System.String error(System.String str)
        {
            return str;
        }
		
        public static System.String error(int id, int errorCode, System.String errorMsg)
        {
            System.String err = System.Convert.ToString(id);
            err += " | ";
            err += System.Convert.ToString(errorCode);
            err += " | ";
            err += errorMsg;
            return err;
        }
		
        public static System.String connectionClosed()
        {
            return "Connection Closed";
        }
    }
}