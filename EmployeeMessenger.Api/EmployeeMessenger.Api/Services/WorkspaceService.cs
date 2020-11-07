using EmployeeMessenger.Api.Data;
using EmployeeMessenger.Api.Domain.Entities;
using EmployeeMessenger.Api.Domain.Enums;
using EmployeeMessenger.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Services
{
    public class WorkspaceService : BaseService, IWorkspaceService
    {
        public WorkspaceService(DataContext context) : base(context) { }

        public bool CreateWorkspace(string Name, string userId)
        {
            try
            {
                Workspace newWorkspace = new Workspace();

                newWorkspace.Name = Name;
                newWorkspace.CreatedDate = DateTime.Now;

                _context.Workspaces.Add(newWorkspace);
                _context.SaveChanges();

                Channel channel = new Channel()
                {
                    Name = "General",
                    WorkspaceId = newWorkspace.WorkspaceId,
                    ChannelTypeId = (int) ChannelTypeEnum.Public
                };

                _context.Channels.Add(channel);
                _context.SaveChanges();

                WorkspaceUser workspaceUser = new WorkspaceUser()
                {
                    WorkspaceId = newWorkspace.WorkspaceId,
                    UserId = userId,
                    RoleId = (int) WorkspaceRoleEnum.Owner
                };

                _context.WorkspaceUsers.Add(workspaceUser);
                _context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<Workspace> GetAllUserWorkspaces(string userId)
        {
            try
            {
                var workspacesDbo = _context.WorkspaceUsers
                    .Include(wu => wu.Workspace)           
                        .ThenInclude(w => w.WorkspaceChannels)   
                    .Where(wu => wu.UserId == userId)
                    .Select(wu => wu.Workspace)
                    .ToList();

                return workspacesDbo;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool AddMemberToWorkspace(string userId, int workspaceId, int roleId)
        {
            try
            {
                WorkspaceUser workspaceUser = new WorkspaceUser()
                {
                    WorkspaceId = workspaceId,
                    UserId = userId,
                    RoleId = roleId
                };

                _context.WorkspaceUsers.Add(workspaceUser);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
