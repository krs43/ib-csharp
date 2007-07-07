using System;
using System.Collections.Generic;
using System.Text;
using Krs.Ats.IBNet;

namespace Krs.Ats.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IBClient client = new IBClient();

            client.TickPrice += client_TickPrice;
            client.TickSize += client_TickSize;
            client.Error += client_Error;
            client.Connect("127.0.0.1", 7496, 10);
            Contract ER2 = new Contract("ER2", "GLOBEX", SecurityType.Future, "USD", "200709");
            client.ReqMktData(12, ER2, null);
        }

        static void client_TickSize(object sender, TickSizeEventArgs e)
        {
            Console.WriteLine("Tick Size: " + e.Size.ToString());
        }

        static void client_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error: "+ e.ErrorMsg);
        }

        static void client_TickPrice(object sender, TickPriceEventArgs e)
        {
            Console.WriteLine("Price: "+e.Price.ToString());
        }
    }
}
