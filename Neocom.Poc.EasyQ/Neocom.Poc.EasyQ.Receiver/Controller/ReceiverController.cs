using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyNetQ;

namespace Neocom.Poc.EasyQ.Receiver.Controller
{
    [RoutePrefix("api/send")]
    public class ReceiverController : ApiController
    {
        private readonly IBus _bus;

        public ReceiverController(IBus bus)
        {
            this._bus = bus;
        }

        [Route("")]
        [HttpGet]
        public async Task<HttpResponseMessage> BuildMessage()
        {
            _bus.sub

            var result = new StringContent("All is good".SerializeToJsonLowerCase(), Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage
            {
                Content = result,
                StatusCode = HttpStatusCode.OK
            };

            return response;
        }
    }
}