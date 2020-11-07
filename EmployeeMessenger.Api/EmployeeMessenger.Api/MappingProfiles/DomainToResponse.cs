using AutoMapper;
using EmployeeMessenger.Api.Contracts.Requests;
using EmployeeMessenger.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<Workspace, UserWorkspacesResponse>()
                .ForMember(dest => dest.Channels, opt => opt
                .MapFrom(scr => scr.WorkspaceChannels
                .Select(x => new ChannelResponse(){ Id = x.Id, Name = x.Name })));
            CreateMap<Channel, ChannelResponse>();

        }
    }
}
