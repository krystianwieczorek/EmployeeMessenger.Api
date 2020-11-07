using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMessenger.Api.Contracts;
using EmployeeMessenger.Api.Domain.Entities;
using EmployeeMessenger.Api.Domain.ViewModels;
using EmployeeMessenger.Api.Extensions;
using EmployeeMessenger.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMessenger.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly ApplicationUser _user;
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _user = _userService.GetApplicationUser(HttpContext.GetUserId());
        }

        [HttpGet(ApiRoute.User.GetViewModel)]
        public async Task<IActionResult> UserDetails()
        {
            UserViewModel userViewModel = new UserViewModel()
            {
                FirstName = _user.FirstName,
                LastName = _user.LastName
            };

            if(userViewModel != null)
            {
                return Ok(userViewModel);
            }
            return BadRequest(new { Error = "User doesn't exist :( " });
        }
    }
}
