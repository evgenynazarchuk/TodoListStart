using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TodoListStart.Application
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
