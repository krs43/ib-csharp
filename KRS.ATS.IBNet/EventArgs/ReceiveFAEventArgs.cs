using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    public class ReceiveFAEventArgs : EventArgs
    {
        private readonly int faDataType;
        public int FaDataType
        {
            get
            {
                return faDataType;
            }
        }
        private readonly string xml;
        public string Xml
        {
            get
            {
                return xml;
            }
        }

        public ReceiveFAEventArgs(int faDataType)
        {
            this.faDataType = faDataType;
        }
    }
}
