using EmployeeMessenger.Api.Domain.Entities;
using EmployeeMessenger.Api.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Services.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetApplicationUser(string userId);

        bool CheckIfUserCanAddMembersToWorkspace(string UserId, int workspaceId);

        string GetIdUserByEmail(string email);
    }
}
