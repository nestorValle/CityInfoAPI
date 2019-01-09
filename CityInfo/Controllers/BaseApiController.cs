using CityInfo.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    public abstract class BaseApiController<TModel> : Controller
    {
        protected readonly IApiService<TModel> Service;

        protected BaseApiController(IApiService<TModel> service)
        {
            Service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TModel model)
        {
            var id = Service.Create(model);
            return Ok(id);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var models = Service.Get();
            return Ok(models);
        }

        [HttpGet("{id:int}")]
        public virtual IActionResult Get(int id)
        {
            var model = Service.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TModel model)
        {
            var id = Service.Update(model);
            return Ok(id);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
              Service.Delete(id);
              return Ok(true);

            }
            catch (Exception exc)
            {
                return NotFound();
            }
        }

#if DEBUG
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return Content($"<html><h1>Api up and running - Testing Build </h1> <h2>Controller: {controller}</h2> <h2>Action: {action}</h2> </html>", "text/html");
        }
#endif


    }
}
