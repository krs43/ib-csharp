using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    /// <summary>
    /// Scanner Parameters Event Arguments
    /// </summary>
    public class ScannerParametersEventArgs : EventArgs
    {
        private readonly string xml;

        public string Xml
        {
            get
            {
                return xml;
            }
        }

        public ScannerParametersEventArgs(string xml)
        {
            this.xml = xml;
        }
    }
}
