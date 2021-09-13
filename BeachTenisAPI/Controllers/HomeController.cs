using BeachTenisAPI.Data;
using BeachTenisAPI.Models;
using BeachTenisAPI.Security;
using BeachTenisAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;


namespace BeachTenisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Login into the application. This login is uses Bearer Token.
        /// </summary>
        /// <param name="login">The specified CPF/Password of your user.</param>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]// NEED TO REQUEST FROM BODY
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK, Type = typeof(HttpResponseMessage))]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NotFound, Type = typeof(UserTokenModel))]
        public ActionResult<dynamic> Authenticate([FromBody]LoginModel login)
        {
            HttpResponseMessage errorResponse;
            var user = _db.Users.Find(login.CPF);
            if (user == null || !new UserSecurity().CheckPassword(login.Password, user.Password))
            {
                errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent($"Username or password invalid!"),
                    ReasonPhrase = "Wrong username/password"
                };

                return errorResponse;
            }

            var token = TokenService.GenerateToken(user);
            user.Password = ""; // In order to hide it's password
            var userTokenModel =  new UserTokenModel
            {
                User = user,
                Token = token
            };
            return new JsonResult(userTokenModel);
        }
    }
}
