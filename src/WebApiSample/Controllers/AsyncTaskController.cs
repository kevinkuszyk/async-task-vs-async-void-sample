using System.Web.Http;
using WebApiSample.Loggers;

namespace WebApiSample.Controllers
{
    public class AsyncTaskController : ApiController
    {
        AsyncTaskLogger logger = new AsyncTaskLogger();
        
        [HttpGet, Route("async-task")]
        public IHttpActionResult Index()
        {
            logger.Info("Some info logging");

            return Ok();
        }
    }
}
