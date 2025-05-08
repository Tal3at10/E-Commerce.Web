using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
        }
        //Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userResult = await _serviceManager.AuthService.LoginAsync(loginDto);
            return Ok(userResult);
        }


        //Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var userResult = await _serviceManager.AuthService.RegisterAsync(registerDto);
            return Ok(userResult);
        }
    }
}
