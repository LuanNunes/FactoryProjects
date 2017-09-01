using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neocom.Poc.EasyQ.Receiver.MessageHandlers.ConcreteHandlers;
using Neocom.Poc.EasyQ.Receiver.Model;

namespace Neocom.Poc.EasyQ.Receiver.MessageHandlers
{
    public static class MessageHandlerProcessor
    {
        public static IMessageHandlerProcessor Create(Message message)
        {
            switch (message.EventCode)
            {
                case "WIDGET-SPINUP":
                    return new WidgetSpinupMessageHandler();

                case "WIDGET-SPINDOWN":
                    return new WidgetSpindownMessageHandler();

                case "WIDGET-MALFUNCTION":
                    return new WidgetMalfunctionMessageHandler();

                default:
                    throw new Exception("Unknown message event code!");
            }
        }
    }
}