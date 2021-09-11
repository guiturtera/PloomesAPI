using BeachTenisAPI.Data;
using BeachTenisAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

        [HttpGet]
        public IEnumerable<Spreadsheet> GetSpreadsheets()//string cpf, DateTime day)
        {
            var data = _db.Spreadsheets;
            return data;
        }

        [HttpPost]
        public void AddNew(string user_cpf, string coach_cpf, double distance, 
            double pace, DateTime dayAndHour)
        {
            var user = _db.Users.Find(user_cpf);
            var coach = _db.Users.Find(coach_cpf);
            if (user == null || coach == null)
                return;//return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var sheet = new Spreadsheet() 
            {
                User = user_cpf,
                Coach = coach_cpf,
                Day = dayAndHour,
                Pace = pace,
                Distance = distance
            };

            _db.Add(sheet);
            _db.SaveChanges();
        }

    }
}
