using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DotNetFrameworkWebApp.Models
{
    public class RegistrationViewModel
    {
        public Guid UserId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Address.")]

        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [Required(ErrorMessage = "Required")]

        public string ConfirmPassword { get; set; }
        
        public string SaltKey { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}