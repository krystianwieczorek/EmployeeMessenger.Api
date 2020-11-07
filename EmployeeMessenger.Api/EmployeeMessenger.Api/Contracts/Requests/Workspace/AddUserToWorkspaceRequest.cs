using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Contracts.Requests
{
    public class AddUserToWorkspaceRequest
    {
        public int WorkspaceId { get; set; }
        [EmailAddress]
        public string UserEmail  { get; set; }

    }
}
