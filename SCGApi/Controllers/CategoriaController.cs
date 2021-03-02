using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<IActionResult> Select()
        {
            try
            {
                return Ok(await _service.Select());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Requery(CategoriaModel model)
        {
            try
            {
                return Ok(await _service.Requery(x => x.Id == model.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(CategoriaModel model)
        {
            try
            {
                return Ok(await _service.Add(model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(CategoriaModel model)
        {
            try
            {
                return Ok(await _service.Update(model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(CategoriaModel model)
        {
            try
            {
                return Ok(await _service.Delete(model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
