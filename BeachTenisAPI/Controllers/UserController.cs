using BeachTenisAPI.Data;
using BeachTenisAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeachTenisAPI.Controllers
{
    /*
        Controller has the following logic:
           RETRIEVES DATA
            GET -> returns specific data. Doesn't change logic
            HEAD -> same as get, but does not retrieve the data. used to validate GET request.
           ADDS DADA:
            POST -> update resources
            PUT -> similar to POST, though, the same command will not affect others. it is idempotent (no side effect).
                It means that, if you call a PUT command multiple times, it will generate the same result
            PATCH -> 'incremental', to update some resource
           DELETES DATA
            DELETE -> Delete some resource.
     */
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns a list of all Users from the DB.
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK, Type = typeof(IEnumerable<User>))]
        public IEnumerable<User> GetUserInfo()
        {
            var users = _db.Users;
            return users;
        }

        /// <summary>
        /// Returns a specific user by his CPF.
        /// </summary>
        /// <param name="cpf">CPF of the user to search.</param>
        [HttpGet("{cpf}")]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK, Type = typeof(User))]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NotFound, Type = typeof(HttpResponseMessage))]
        public ActionResult GetUserInfo(string cpf)
        {
            // If no user is found, will let it raise 204 error.
            // Depending, Could add cpf validation
            User user = _db.Users.Find(cpf);
            if (user == null)
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"User with CPF `{cpf}` not found!"),
                    ReasonPhrase = "User not found!"
                };
                return new JsonResult(response);
            }

            return new JsonResult(user);
        }

        /// <summary>
        /// Adds a new user to the DB.
        /// </summary>
        /// <param name="name">Name of the new user</param>
        /// <param name="cpf">CPF of the new user. Note that if a CPF already exists, it will return 403 code.</param>
        /// <param name="birth">Birth of the new user</param>
        [HttpPost("add")]
        [ProducesResponseType((int) System.Net.HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int) System.Net.HttpStatusCode.Forbidden, Type = typeof(HttpResponseMessage))]
        public ActionResult Post(string name, string cpf, DateTime birth)
        {
            var user = _db.Users.Find(cpf);
            if (user != null)
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent($"User with CPF `{cpf}` already exists"),
                    ReasonPhrase = "User with existing CPF"
                };
                return new JsonResult(response);
            }

            var newUser = new User()
            {
                Name = name,
                CPF = cpf,
                BirthDate = birth
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return Ok();
        }
    }
}
