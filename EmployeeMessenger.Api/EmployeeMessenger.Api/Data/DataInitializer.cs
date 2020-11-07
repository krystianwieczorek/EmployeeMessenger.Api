using EmployeeMessenger.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Data
{
    public static class DataInitializer
    {
        public static void InitializeData(this ModelBuilder builder)
        {
            builder
                .Entity<WorkspaceRole>().HasData(
                new { Id = 1, Name = "Owner" },
                new { Id = 2, Name = "Admin" },
                new { Id = 3, Name = "User" });

            builder
                .Entity<ChannelType>().HasData(
                new { Id = 1, Name = "Public" },
                new { Id = 2, Name = "Private" },
                new { Id = 3, Name = "Conversation" });
        }
    }
}
