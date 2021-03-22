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
    public class FormUpdateController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<FormUpdateController> _logger;

        public FormUpdateController(ILogger<FormUpdateController> logger)
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

        //Finds a form by id
        [ApiExplorerSettings(IgnoreApi = true)]
        public Form findForm(int id)
        {
            using (var db = new FormsContext())
            {
                Form form = db.Forms.FirstOrDefault(form => form.FormId == id);
                List<Field> fields = db.Fields.Where(field => field.FormId == id).ToList();
                if (form != null)
                {
                    form.Fields = fields;
                }
                return form;
            }
        }

        //Finds a form by id
        [ApiExplorerSettings(IgnoreApi = true)]
        private Field findField(int id)
        {
            using (var db = new FormsContext())
            {
                Field field = db.Fields.FirstOrDefault(field => field.FormId == id);
                return field;
            }
        }

        //Updates a form
        [HttpPost("/api/[controller]/updateForm")]
        public IActionResult updateForm(Form form)
        {
            using (var db = new FormsContext())
            {
                Form checkIfExists = findForm(form.FormId);

                if (checkIfExists != null)
                {
                    db.Update(form);
                    db.SaveChanges();
                    Console.WriteLine("Edited form with id: " + form.FormId);
                    return Ok(form);
                }
                else
                {
                    Console.WriteLine("Could not find form with id: " + form.FormId);
                    return BadRequest("Could not find form with id: " + form.FormId);
                }
            }
        }


        //Adds a new form
        //*Currently user provides an id*
        //*Better version would auto generate the id with Guid.NewGuid.ToString()*
        [HttpPost("/api/[controller]/addForm")]
        public IActionResult addForm(Form form)
        {
            using (var db = new FormsContext())
            {
                //Use this if user is not sending their own ID
                //form.Id = Guid.NewGuid().ToString();

                foreach (Field field in form.Fields)
                {
                    field.FormId = form.FormId;
                }

                Form checkIfExists = findForm(form.FormId);

                if (checkIfExists == null)
                {
                    db.Add(form);
                    db.SaveChanges();
                    Console.WriteLine("Added form with id: " + form.FormId);
                    return Ok(form);
                }
                else
                {
                    Console.WriteLine("Form already exists with id: " + form.FormId);
                    return BadRequest("Form already exists with id: " + form.FormId);
                }
            }
        }

        //Get a specific form by it's id
        [HttpGet("/api/[controller]/getForm/{id}")]
        public IActionResult getForm(int id)
        {
            using (var db = new FormsContext())
            {
                Console.WriteLine("Request for form with id: " + id);
                Form form = findForm(id);
                if (form != null)
                {
                    Console.WriteLine("Found form with id: " + id);
                    return Ok(form);
                }
                else
                {
                    Console.WriteLine("Could not find form with id: " + id);
                    return BadRequest("Could not find form with id: " + id);
                }
            }
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        private Boolean UpdateSubApplications(Form form)
        {
            //HttpClient requests to update sub applications

            //return true if updates succeed
            return true;
        }
    }
}
