using AutoMapper;
using EmployeeMessenger.Api.Contracts;
using EmployeeMessenger.Api.Contracts.Requests;
using EmployeeMessenger.Api.Contracts.Response;
using EmployeeMessenger.Api.Domain.Entities;
using EmployeeMessenger.Api.Domain.Enums;
using EmployeeMessenger.Api.Extensions;
using EmployeeMessenger.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkspaceController : Controller
    {
        private ApplicationUser _user;
        private readonly IUserService _userService;
        private readonly IWorkspaceService _workspaceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public WorkspaceController(
            IHttpContextAccessor httpContextAccessor, 
            IUserService userService, 
            IWorkspaceService workspaceService, 
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _workspaceService = workspaceService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _user = _userService.GetApplicationUser(_httpContextAccessor.HttpContext.GetUserId());
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost(ApiRoute.Workspace.CreateWorkspace)]
        public IActionResult CreateWorkspace([FromBody]CreateWorkspaceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            bool result = _workspaceService.CreateWorkspace(request.Name, _user.Id);
            
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = "Server error, please contact with Artur Wiśniewski xD" });
            }
            return Ok();
        }

        [HttpGet(ApiRoute.Workspace.GetUserWorkspaces)]
        public IActionResult GetUserWorkspaces()
        {
            List<Workspace> userWorkspaces = _workspaceService.GetAllUserWorkspaces(_user.Id);

            if(userWorkspaces == null || userWorkspaces.Count() == 0)
            {
                return NotFound(new { Message = "User doesn't have permission to any workspace" });
            }
            List<UserWorkspacesResponse> response = _mapper.Map <List<UserWorkspacesResponse>>(userWorkspaces);
            return Ok(response);
        }

        [HttpPost(ApiRoute.Workspace.AddUserToWorkspace)]
        public IActionResult AddUserToWorkspace([FromBody]AddUserToWorkspaceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            if (_userManager.FindByEmailAsync(request.UserEmail.DeleteWhiteSpaces()).Result == null)
            {
                return BadRequest(new { Error = "User with the e-mail doesn't exist" });
            }

            if (_userService.CheckIfUserCanAddMembersToWorkspace(_user.Id, request.WorkspaceId))
            {
                var result = _workspaceService.AddMemberToWorkspace(_userService.GetIdUserByEmail(request.UserEmail), request.WorkspaceId, (int)WorkspaceRoleEnum.User);
                if (result)
                {
                    return Ok(new { Meassage = "Member added successful" });
                }
                return BadRequest(new { Error = "Member added fail" });
            }

            return BadRequest(new { Error = "User doeasn't have permission to adding members" });
        }
    }
}
