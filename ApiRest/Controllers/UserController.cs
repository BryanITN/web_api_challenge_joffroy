using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using web_api_challenge.Dtos.Users;
using web_api_challenge.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/User")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {
            var result= _userService.GetAllUsers();
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        [HttpPost]
        public  HttpResponseMessage CreateUser(DtoCreateUpdateUser user)
        {
                _userService.CreateUser(user);
                return Request.CreateResponse(HttpStatusCode.Created);   
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage EditUser(int id, [FromBody] DtoCreateUpdateUser user)
        {
            _userService.EditUser(user, id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
