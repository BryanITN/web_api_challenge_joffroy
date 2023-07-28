using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using web_api_challenge.Dtos.StatusTasks;
using web_api_challenge.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/StatusTask")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class StatusTaskController : ApiController
    {
        private readonly IStatusTaskService _statusTaskService;

        public StatusTaskController(IStatusTaskService statusTaskService)
        {
            _statusTaskService = statusTaskService;
        }

        [HttpPost]
        public HttpResponseMessage CreateStatusTask(DtoStatusTask statusTask)
        {
            _statusTaskService.CreateStatusTask(statusTask);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage UpdateStatusTask( int id,[FromBody]DtoStatusTask statusTask)
        {
            _statusTaskService.UpdateStatusTask(statusTask,id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage GetAllStatusTask()
        {
           var result= _statusTaskService.GetAllStatusTask();
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }
    }
}
