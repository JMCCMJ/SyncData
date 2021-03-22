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

        //Adds a new form
        //*Currently user provides an id*
        //*Better version would auto generate the id with Guid.NewGuid.ToString()*
        [Authorize]
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

                    updateSubApplications(form);

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

        //Finds a field by id
        //Future use
        [ApiExplorerSettings(IgnoreApi = true)]
        private Field findField(int id)
        {
            using (var db = new FormsContext())
            {
                Field field = db.Fields.FirstOrDefault(field => field.FormId == id);
                return field;
            }
        }

        //Get a specific form by it's id
        [Authorize]
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

        //Get all logs
        [Authorize]
        [HttpGet("/api/[controller]/getLogs")]
        public IActionResult getLogs()
        {
            using (var db = new FormsContext())
            {
                List<LogData> logDatas = db.LogDatas.ToList();
                return Ok(logDatas);
            }
        }

        //Finds a form by id
        [ApiExplorerSettings(IgnoreApi = true)]
        private List<SubApplication> getSubApplications()
        {
            using (var db = new FormsContext())
            {
                List<SubApplication> subApplications = db.SubApplications.ToList();

                return subApplications;
            }
        }

        //Updates a form
        [Authorize]
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
                    updateSubApplications(form);
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

        //Initialize db with sub applications
        [Authorize]
        [HttpGet("/api/[controller]/populateSubApplications")]
        public IActionResult populateSubApplications()
        {
            using (var db = new FormsContext())
            {
                if (db.LogDatas.Count() > 0)
                {
                    return BadRequest("DB already initalized");
                }

                SubApplication subApplication1 = new SubApplication();
                subApplication1.Id = Guid.NewGuid().ToString();

                SubApplication subApplication2 = new SubApplication();
                subApplication2.Id = Guid.NewGuid().ToString();

                SubApplication subApplication3 = new SubApplication();
                subApplication3.Id = Guid.NewGuid().ToString();

                db.Add(subApplication1);
                db.Add(subApplication2);
                db.Add(subApplication3);

                db.SaveChanges();

                return Ok("DB Sub Applications Initialized");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private Boolean updateSubApplications(Form form)
        {
            //HttpClient requests to update sub applications
            using (var db = new FormsContext())
            {
                List<SubApplication> subApplications = getSubApplications();

                for (int i = 0; i < form.Fields.Count; i++)
                {
                    Field field = form.Fields[i];

                    for (int j = 0; j < subApplications.Count; j++)
                    {
                        LogData logData = new LogData(form.FormId, subApplications[j].Id, field.FieldId, field.InventoryCount, field.InventoryDesc, form.UpdatedBy);
                        db.Add(logData);
                    }
                    db.SaveChanges();
                }
            }

            //return true if updates succeed
            return true;
        }
    }
}
