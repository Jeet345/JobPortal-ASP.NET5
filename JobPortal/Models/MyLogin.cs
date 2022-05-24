using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class MyLogin
    {
        [Required(ErrorMessage = "Email Filed Is Required")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "Password Field Is Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
