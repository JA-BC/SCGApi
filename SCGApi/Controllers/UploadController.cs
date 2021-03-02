using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class UploadController: ControllerBase
    {
        private readonly UploadService _service;

        public UploadController(UploadService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromForm] UploadModel model)
        {
            try
            {
                return Ok(await _service.Add(model));

            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Requery(UploadModel model)
        {
            try
            {
                return Ok(await _service.Requery(x => x.UserId == model.UserId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Update(UploadModel model)
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
        public async Task<IActionResult> Delete(UploadModel model)
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
