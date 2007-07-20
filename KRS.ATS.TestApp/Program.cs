using System;
using System.Collections.Generic;
using System.Text;
using Krs.Ats.IBNet;

namespace Krs.Ats.TestApp
{
    class Program
    {
        private static int NextOrderId = 0;
        private static Contract ER2;
        private static Contract TickNasdaq;
        private static Contract VolNasdaq;
        private static Contract AdNasdaq;
        private static Contract TickNyse;
        private static Contract VolNyse;
        private static Contract AdNyse;
        private static Contract YmEcbot;
        private static Contract ES;
        private static Contract SPY;
        private static Contract ZN;
        private static Contract ZB;
        private static Contract ZT;
        private static Contract ZF;
        private static IBClient client;
        static void Main(string[] args)
        {
            client = new IBClient();
            client.ThrowExceptions = true;

            client.TickPrice += client_TickPrice;
            client.TickSize += client_TickSize;
            client.Error += client_Error;
            client.NextValidId += client_NextValidId;
            client.UpdateMktDepth += client_UpdateMktDepth;
            client.OrderStatus += new EventHandler<OrderStatusEventArgs>(client_OrderStatus);

            client.Connect("127.0.0.1", 7496, 10);
            ER2 = new Contract("ER2", "GLOBEX", SecurityType.Future, "USD", "200709");
            YmEcbot = new Contract("YM", "ECBOT", SecurityType.Future, "USD", "200709");
            ES = new Contract("ES", "GLOBEX", SecurityType.Future, "USD", "200709");
            SPY = new Contract("SPY", "GLOBEX", SecurityType.Future, "USD", "200709");
            ZN = new Contract("ZN", "ECBOT", SecurityType.Future, "USD", "200709");
            ZB = new Contract("ZB", "ECBOT", SecurityType.Future, "USD", "200709");
            ZT = new Contract("ZT", "ECBOT", SecurityType.Future, "USD", "200709");
            ZF = new Contract("ZF", "ECBOT", SecurityType.Future, "USD", "200709");

            TickNasdaq = new Contract("TICK-NASD", "NASDAQ", SecurityType.Indice, "USD");
            VolNasdaq = new Contract("VOL-NASD", "NASDAQ", SecurityType.Indice, "USD");
            AdNasdaq = new Contract("AD-NASD", "NASDAQ", SecurityType.Indice, "USD");


            TickNyse = new Contract("TICK-NYSE", "NYSE", SecurityType.Indice, "USD");
            VolNyse = new Contract("VOL-NYSE", "NYSE", SecurityType.Indice, "USD");
            AdNyse = new Contract("AD-NYSE", "NYSE", SecurityType.Indice, "USD");

            client.ReqMktData(14, ZB, null);
            client.ReqMktDepth(15, ZB, 5);

            Order BuyContract = new Order();
            BuyContract.Action = ActionSide.Buy;
            BuyContract.RthOnly = false;
            BuyContract.LmtPrice = 820;
            BuyContract.OrderType = OrderType.Limit;
            BuyContract.TotalQuantity = 1;
            client.PlaceOrder(25, ER2, BuyContract);
        }

        static void client_OrderStatus(object sender, OrderStatusEventArgs e)
        {
            Console.WriteLine("Order Placed.");
        }

        static void client_UpdateMktDepth(object sender, UpdateMktDepthEventArgs e)
        {
            Console.WriteLine("Tick ID: " + e.TickerId + " Tick Side: " + EnumDescConverter.GetEnumDescription(e.Side) +
                              " Tick Size: " + e.Size + " Tick Price: " + e.Price + " Tick Position: " + e.Position +
                              " Operation: " + EnumDescConverter.GetEnumDescription(e.Operation));
        }

        static void client_NextValidId(object sender, NextValidIdEventArgs e)
        {
            Console.WriteLine("Next Valid Id: " + e.OrderId);
            NextOrderId = e.OrderId;
        }

        static void client_TickSize(object sender, TickSizeEventArgs e)
        {
            Console.WriteLine("Tick Size: " + e.Size + " Tick Type: " + EnumDescConverter.GetEnumDescription(e.TickerType));
        }

        static void client_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error: "+ e.ErrorMsg);
        }

        static void client_TickPrice(object sender, TickPriceEventArgs e)
        {
            Console.WriteLine("Price: " + e.Price + " Tick Type: " + EnumDescConverter.GetEnumDescription(e.TickType));
        }
    }
}
