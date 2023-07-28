using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using web_api_challenge.Dtos.Tasks;
using web_api_challenge.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/Task")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpPost]
        public HttpResponseMessage CreateTask(DtoCreateUpdateTask task)
        {
            _taskService.CreateTask(task);
            return Request.CreateResponse(HttpStatusCode.Created);
        }


        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage UpdateTask(int id, [FromBody] DtoCreateUpdateTask task)
        {
            _taskService.UpdateTask(task,id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpGet]
        public HttpResponseMessage GetAllTask(int userId)
        {
          var result=  _taskService.GetAllTaskByUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetTaskById(int id)
        {
            var result = _taskService.GetTaskById(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage DeleteTask(int id)
        {
          _taskService.DeleteTask(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
