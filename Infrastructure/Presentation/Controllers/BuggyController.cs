using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(); // 404
        }

        [HttpGet("serverError")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception(); // 500
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(); // 400  Validation Error
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized(); // 401 
        }

    }
}
