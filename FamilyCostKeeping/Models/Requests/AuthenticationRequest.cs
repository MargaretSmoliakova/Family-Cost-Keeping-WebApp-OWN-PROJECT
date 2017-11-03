using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FamilyCostKeeping.Models.Requests
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Please enter User name.")]
        public string LogInName { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
        public bool RememberCredentials { get; set; }
    }
}
