using Chess.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Chess.Core.Api.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ProcessResponse(IResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.ResponseModel);
                case HttpStatusCode.Created:
                    return Created(Request.Path.Value, response.ResponseModel);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.Message);
                case HttpStatusCode.Forbidden:
                    return Forbid(response.Message);
                default:
                    return StatusCode((int)response.StatusCode, response.Message);
            }
        }
    }
}
