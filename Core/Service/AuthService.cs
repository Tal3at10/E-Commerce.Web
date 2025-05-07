using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UnAuthorizedException();
            
            var flag = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!flag) throw new UnAuthorizedException();

            
            return new UserResultDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "ssssss"
            }; 
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Username,
            };
            var result = await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {

                var errors = result.Errors.Select(err => err.Description);

                throw new ValidationException(errors);
            }

            return new UserResultDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "ssssss"
            };
        }
    }
}
