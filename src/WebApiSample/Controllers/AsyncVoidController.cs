using System.Web.Http;
using WebApiSample.Loggers;

namespace WebApiSample.Controllers
{
    public class AsyncVoidController : ApiController
    {
        AsyncVoidLogger logger = new AsyncVoidLogger();

        [HttpGet, Route(""), Route("async-void")]
        public IHttpActionResult Index()
        {
            logger.Info("Some info logging");

            return Ok();
        }
    }
}
