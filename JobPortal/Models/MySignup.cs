using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class MySignup
    {
        [Required (ErrorMessage ="Username Field Is Required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Email Filed Is Required")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "Password Field Is Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password Filed Is Required")]
        [Compare("password",ErrorMessage = "Password And Confirm Password Not Match")]
        public string conPassword { get; set; }

        [Required(ErrorMessage = "Please Select Account Type")]
        [Range(2,3,ErrorMessage = "Please Select Valid Account Type")]
        public string groupId { get; set; }
    }
}
