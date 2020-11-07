using EmployeeMessenger.Api.Contracts.Requests;
using EmployeeMessenger.Api.Contracts.V1.Requests;
using EmployeeMessenger.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest user);
        Task<AuthenticationResult> LoginAsync(UserLoginRequest user);
    }
}
