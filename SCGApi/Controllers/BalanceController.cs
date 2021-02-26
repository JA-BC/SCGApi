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
using WebApi.Utilities.Http;

namespace SCGApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController: ControllerBase
    {
        private readonly BalanceService _service;

        public BalanceController(BalanceService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public IActionResult Select(APIRequest request)
        {
            try
            {
                var result = _service.GetPage(request);
                var user = HttpContext.User;

                APIResponse<BalanceModel> response = new APIResponse<BalanceModel>()
                {
                    Data = result,
                    TotalCount = result.Capacity
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult Requery(BalanceModel model)
        {
            try
            {
                return Ok(_service.Requery(x => x.Id == model.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("[action]")]
        public ActionResult Add(BalanceModel model)
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
        public ActionResult Update(BalanceModel model)
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
        public ActionResult Delete(BalanceModel model)
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
