using EmployeeMessenger.Api.Data;
using EmployeeMessenger.Api.Domain.Entities;
using EmployeeMessenger.Api.Domain.Enums;
using EmployeeMessenger.Api.Domain.ViewModels;
using EmployeeMessenger.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager, DataContext context) 
            : base(context)
        {
            _userManager = userManager;
        }

        public ApplicationUser GetApplicationUser(string userId)
        {
            try
            {
                return _userManager.FindByIdAsync(userId).Result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool CheckIfUserCanAddMembersToWorkspace(string UserId, int workspaceId)
        {
            try
            {
                WorkspaceUser workspaceUser = _context.WorkspaceUsers
                    .FirstOrDefault(wu => wu.UserId == UserId && wu.WorkspaceId == workspaceId);

                if (workspaceUser.RoleId == (int)WorkspaceRoleEnum.Admin ||
                    workspaceUser.RoleId == (int)WorkspaceRoleEnum.Owner)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public string GetIdUserByEmail(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            return user is null ? user.Id : null;
        }
    }
}
