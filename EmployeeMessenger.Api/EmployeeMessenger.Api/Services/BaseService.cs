using EmployeeMessenger.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Services
{
    public class BaseService
    {
        protected readonly DataContext _context;
        public BaseService(DataContext context)
        {
            _context = context;
        }
    }
}
