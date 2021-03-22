using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncDataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

       // [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //Finds a note by id
        [ApiExplorerSettings(IgnoreApi = true)]
        public Form findForm(string id)
        {
            using (var db = new FormsContext())
            {
                Form form = db.Forms.FirstOrDefault(form => form.FormId == id);
                return form;
            }
        }


        //Adds a new note
        //*Currently user provides an id*
        //*Better version would auto generate the id with Guid.NewGuid.ToString()*
        [HttpPost("/api/[controller]/addNote")]
        public IActionResult addNote(Form form)
        {
            using (var db = new FormsContext())
            {
                //Use this if user is not sending their own ID
                //note.Id = Guid.NewGuid().ToString();
                Form checkIfExists = findForm(form.FormId);

                if (checkIfExists == null)
                {
                    db.Add(form);
                    db.SaveChanges();
                    Console.WriteLine("Added note with id: " + form.FormId);
                    return Ok(form);
                }
                else
                {
                    Console.WriteLine("Note already exists with id: " + form.FormId);
                    return BadRequest("Note already exists with id: " + form.FormId);
                }
            }
        }
    }
}
