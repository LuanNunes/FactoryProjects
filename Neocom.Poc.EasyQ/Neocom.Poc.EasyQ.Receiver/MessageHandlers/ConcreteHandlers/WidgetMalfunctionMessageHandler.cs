using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neocom.Poc.EasyQ.Receiver.Model;

namespace Neocom.Poc.EasyQ.Receiver.MessageHandlers.ConcreteHandlers
{
    public class WidgetMalfunctionMessageHandler : IMessageHandlerProcessor
    {
        public string Process(Message message)
        {
            return string.Format("Received malfunction message for ID {0}", message.Value);
        }
    }
}