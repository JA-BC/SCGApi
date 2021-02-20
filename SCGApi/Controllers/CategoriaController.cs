using Microsoft.AspNetCore.Mvc;
using SCG.Core.Interfaces;
using SCG.Core.Models;
using SCG.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public ActionResult Select()
        {
            try
            {
                return Ok(_service.Select().ToList());

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("[action]")]
        public ActionResult Add(CategoriaModel model)
        {
            try
            {
                return Ok(_service.Add(model));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("[action]")]
        public ActionResult Update(CategoriaModel model)
        {
            try
            {
                return Ok(_service.Update(model));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("[action]")]
        public ActionResult Delete(CategoriaModel model)
        {
            try
            {
                return Ok(_service.Delete(model));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
