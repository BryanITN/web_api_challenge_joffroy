using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using web_api_challenge.Dtos.Users;
using web_api_challenge.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public HttpResponseMessage SingIn(DtoSingIn user)
        {
            var token = _userService.SingIn(user);
            return Request.CreateResponse(HttpStatusCode.OK, token);

        }
    }
}
