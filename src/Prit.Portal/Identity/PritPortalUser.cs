using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Prit.Portal.Identity
{
    // Add profile data for application users by adding properties to the PritPortalUser class
    public class PritPortalUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
