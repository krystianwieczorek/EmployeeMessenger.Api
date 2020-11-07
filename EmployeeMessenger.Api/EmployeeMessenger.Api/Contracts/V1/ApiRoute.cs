using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Contracts
{
    public static class ApiRoute
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
    
        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";
        }

        public static class User
        {
            public const string GetViewModel = Base + "/user/userdetails";
        }

        public static class Workspace
        {
            public const string CreateWorkspace = Base + "/workspace/createworkspace";

            public const string GetUserWorkspaces = Base + "/workspace/getuserworkspaces";

            public const string AddUserToWorkspace = Base + "/workspace/addusertoworkspace";
        }
    }
}
