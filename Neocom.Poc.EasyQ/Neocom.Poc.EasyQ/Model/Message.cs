using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Neocom.Poc.EasyQ.Model
{
    public class Message
    {
        public string EventCode { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        private static byte[] BuildMessage()
        {
            var message = new Message();
            var r = new Random().Next(0, 2);
            switch (r)
            {
                case 0:
                    message.EventCode = "WIDGET-SPINUP";
                    message.Key = "ID";
                    message.Value = "111";
                    break;

                case 1:
                    message.EventCode = "WIDGET-SPINDOWN";
                    message.Key = "ID";
                    message.Value = "333";
                    break;

                default:
                    message.EventCode = "WIDGET-MALFUNCTION";
                    message.Key = "ID";
                    message.Value = "222";
                    break;
            }

            var serializedMessage = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(serializedMessage);
        }
    }
}