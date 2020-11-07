using EmployeeMessenger.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Services.Interfaces
{
    public interface IWorkspaceService
    {
        bool CreateWorkspace(string Name, string userId);
        List<Workspace> GetAllUserWorkspaces(string userId);

        bool AddMemberToWorkspace(string UserEmail, int workspaceId, int roleId);
    }
}
