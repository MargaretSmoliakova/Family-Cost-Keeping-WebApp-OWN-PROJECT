using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models.Requests
{
    public class SignupRequest
    {
        [Required(ErrorMessage = "Please enter login.")]
        public string LogInName { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address.")]
        public string Mail { get; set; }        
        public bool RememberCredentials { get; set; }
    }
}
