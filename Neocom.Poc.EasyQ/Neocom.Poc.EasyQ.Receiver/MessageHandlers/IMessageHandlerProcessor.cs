using Neocom.Poc.EasyQ.Receiver.Model;

namespace Neocom.Poc.EasyQ.Receiver.MessageHandlers
{
    public interface IMessageHandlerProcessor
    {
        string Process(Message message);
    }
}
