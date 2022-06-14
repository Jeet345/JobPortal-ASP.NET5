using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class MyChangePassword
    {
        [Required(ErrorMessage = "Old Password Field Is Required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password Field Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Field Is Required")]
        [Compare("Password", ErrorMessage = "Password And Confirm Password Not Match")]
        public string ConPassword { get; set; }
    }
}
