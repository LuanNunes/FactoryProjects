using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Data.Access.Layer.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApi.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private UserRepository _userRepository;
        public HomeController()
        {
            _userRepository = new UserRepository();
        }

        [Route("search/{query}")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SimpleSearch(string query)
        {
            var response = this.Request.CreateResponse();
            var settings = new JsonSerializerSettings();
            response.Content = new PushStreamContent(async (outputStream, httpContent, transportContext) =>
            {
                try
                {
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    foreach (var model in _userRepository.GetUsers(query))
                    {
                        var payload = JsonConvert.SerializeObject(model, Formatting.None, settings);
                        var buffer = UTF8Encoding.UTF8.GetBytes(payload);

                        await outputStream.WriteAsync(buffer, 0, buffer.Length);
                    }
                }
                catch (HttpException e)
                {
                    return;
                }
                finally
                {
                    var buffer = GetEndOfFileIndicator(settings);
                    await outputStream.WriteAsync(buffer, 0, buffer.Length);
                    outputStream.Close();
                }
            });

            return response;
        }

        private static byte[] GetEndOfFileIndicator(JsonSerializerSettings settings)
        {
            var eof = new {eof = 1};
            var payload = JsonConvert.SerializeObject(eof, Formatting.None, settings);
            var buffer = UTF32Encoding.UTF8.GetBytes(payload);
            return buffer;
        }
    }
}
