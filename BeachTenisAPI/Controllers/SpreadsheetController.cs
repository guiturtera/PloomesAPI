using BeachTenisAPI.Data;
using BeachTenisAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace BeachTenisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpreadsheetController : ControllerBase
    {
        ApplicationDbContext _db;
        public SpreadsheetController(ApplicationDbContext db) 
        {
            _db = db;
        }

        /// <summary>
        /// Gets a specific Spreadsheet from the user.
        /// </summary>
        /// <param name="day">Day of the Spreadsheet in the format `yyyy-mm-dd`</param>
        [HttpGet("filter")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Spreadsheet))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(HttpResponseMessage))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(void))]
        [Authorize]
        public ActionResult GetSpreadsheetsFromUserByDay(DateTime day)
        {
            var claims = User.Identities.First().Claims.ToList();
            string cpf = claims?.FirstOrDefault(x => x.Type.Equals("cpf", StringComparison.OrdinalIgnoreCase)).Value;

            var data = _db.Spreadsheets.Where(x => x.User == cpf && x.Day == day).ToList();
            if (data.Count == 0)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Sheet of user {cpf} and date {day} not found!"),
                    ReasonPhrase = "Sheet not found."
                };
                return new JsonResult(errorResponse);
            }

            return new JsonResult(data);
        }

        /// <summary>
        /// Get the spreadsheet from the authorized token.
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Spreadsheet>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(void))]
        [Authorize]
        public ActionResult GetSpreadsheetsFromUser()
        {
            var claims = User.Identities.First().Claims.ToList();
            string cpf = claims?.FirstOrDefault(x => x.Type.Equals("cpf", StringComparison.OrdinalIgnoreCase)).Value;

            var data = _db.Spreadsheets.Where(x => x.User == cpf);
            return new JsonResult(data);
        }

        /// <summary>
        /// Gets all spreadsheets in the DB. Only works for admin tokens.
        /// </summary>
        [HttpGet("admin/all")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Spreadsheet>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(void))]
        [Authorize(Roles = "admin")]
        public IEnumerable<Spreadsheet> GetAllSpreadsheets()
        {
            var data = _db.Spreadsheets;
            return data;
        }

        /// <summary>
        /// Adds a new Spreadsheet to the user. Only works for admin tokens.
        /// </summary>
        /// <param name="sheet">Sheet to add.</param>
        [HttpPost("admin/add")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(HttpResponseMessage))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(HttpResponseMessage))]
        [Authorize(Roles = "admin")]
        public ActionResult AddNew([FromBody]Spreadsheet sheet)
        {
            HttpResponseMessage errorResponse;
            if (!ValidSpreadsheet(sheet, out errorResponse) || !ValidCpfs(sheet, out errorResponse) || !ValidSpreadsheetDoesntExist(sheet, out errorResponse))
                return new JsonResult(errorResponse);
            
            _db.Spreadsheets.Add(sheet);
            _db.SaveChanges();

            return new JsonResult(sheet);
        }

        /// <summary>
        /// Edits a new Spreadsheet to the user. Only works for admin tokens.
        /// </summary>
        /// <param name="sheet">Sheet to edit.</param>
        [HttpPut("admin/edit")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(HttpResponseMessage))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(HttpResponseMessage))]
        [Authorize(Roles = "admin")]
        public ActionResult EditSpreadsheet([FromBody] Spreadsheet sheet)
        {
            HttpResponseMessage errorResponse;
            if (!ValidSpreadsheet(sheet, out errorResponse) || !ValidCpfs(sheet, out errorResponse))
                return new JsonResult(errorResponse);
            
            _db.Spreadsheets.Update(sheet);
            try
            {
                _db.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                errorResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Sheet not found! Try adding before editing."),
                    ReasonPhrase = "Sheet doesn't exist"
                };
                return new JsonResult(errorResponse);
            }

            return Ok();
        }

        /// <summary>
        /// Removes the Spreadsheet specified. Only works for admin tokens.
        /// </summary>
        /// <param name="sheet">Sheet to edit.</param>
        [HttpDelete("admin/remove")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(HttpResponseMessage))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(HttpResponseMessage))]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteSpreadsheet([FromBody] Spreadsheet sheet)
        {
            HttpResponseMessage errorResponse;
            if (!ValidSpreadsheet(sheet, out errorResponse) || !ValidCpfs(sheet, out errorResponse))
                return new JsonResult(errorResponse);

            _db.Spreadsheets.Remove(sheet);
            try
            {
                _db.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                errorResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Sheet not found! Try adding before editing."),
                    ReasonPhrase = "Sheet doesn't exist"
                };
                return new JsonResult(errorResponse);
            }

            return Ok();
        }

        private bool ValidSpreadsheetDoesntExist(Spreadsheet sheet, out HttpResponseMessage errorResponse)
        {
            errorResponse = null;
            bool contains = _db.Spreadsheets.Contains(sheet);

            if (contains)
            {
                errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Sheet already exists. Try editing."),
                    ReasonPhrase = "Sheet already exists."
                };
                return false;
            }

            return true;
        }

        private bool ValidCpfs(Spreadsheet sheet, out HttpResponseMessage errorResponse)
        {
            errorResponse = null;
            var user = _db.Users.Find(sheet.User);
            if (user == null)
            {
                errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"User with CPF `{sheet.User}` not found!"),
                    ReasonPhrase = "User not found!"
                };
                return false;
            }
            var coach = _db.Users.Find(sheet.Coach);
            if (coach == null)
            {
                errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Coach with CPF `{sheet.User}` not found!"),
                    ReasonPhrase = "Coach not found!"
                };
                return false;
            }
            if (coach.Role != EnumUserRoles.Admin)
            {
                errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Coach {coach.CPF} is not an admin!"),
                    ReasonPhrase = "Coach not an admin!"
                };
                return false;
            }

            return true;
        }

        private bool ValidSpreadsheet(Spreadsheet sheet, out HttpResponseMessage errorResponse)
        {
            errorResponse = null;
           
            if (!ModelState.IsValid)
            {
                var allErrors = (string[])ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(String.Join(',', allErrors)),
                    ReasonPhrase = "Invalid parameters."
                };
                return false;
            }

            return true;
        }

    }
}
