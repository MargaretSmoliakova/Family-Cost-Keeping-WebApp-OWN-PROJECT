using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models.Requests
{
    public class AuthenticationRequest
    {
        public string LogInName { get; set; }
        public string Password { get; set; }
        public bool RememberCredentials { get; set; }
    }
}
