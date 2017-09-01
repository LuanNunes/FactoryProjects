using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neocom.Poc.EasyQ.Receiver.Model;

namespace Neocom.Poc.EasyQ.Receiver.MessageHandlers.ConcreteHandlers
{
    public class WidgetSpindownMessageHandler : IMessageHandlerProcessor
    {
        public string Process(Message message)
        {
            return string.Format("Received spindown message for ID {0}", message.Value);
        }
    }
}