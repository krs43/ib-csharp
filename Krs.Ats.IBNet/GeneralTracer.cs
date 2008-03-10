using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// General Tracer handles the switch and writeline functions
    /// </summary>
    public class GeneralTracer : TraceSwitch
    {
        /// <summary>
        /// Used for parameter based write line to the system tracer
        /// </summary>
        /// <param name="condition">Condition for writing the trace (often derived atleast in part from the switch)</param>
        /// <param name="message">Message containing parameter arguments</param>
        /// <param name="args">Prameter Arguments</param>
        public static void WriteLineIf(bool condition, string message, params object[] args)
        {
            Trace.WriteLineIf(condition, String.Format(message, args));
        }

        /// <summary>
        /// Create New General Tracer
        /// </summary>
        /// <param name="displayName">The name to display on a user interface.</param>
        /// <param name="description">The description of the switch.</param>
        public GeneralTracer(string displayName, string description)
            : base(displayName, description)
        {
        }
    }
}
