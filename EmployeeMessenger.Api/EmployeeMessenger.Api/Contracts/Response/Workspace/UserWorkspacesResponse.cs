using EmployeeMessenger.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Contracts.Requests
{
    public class UserWorkspacesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChannelResponse> Channels { get; set; }
    }
}
