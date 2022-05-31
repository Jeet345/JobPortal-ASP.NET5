using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace JobPortal.Models
{
    public class MyUser
    {
        [Required]
        public string id { get; set; }

        [Required(ErrorMessage = "Name Field Is Required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Date Of Birth Field Is Required")]
        public string dob { get; set; }

        [Required(ErrorMessage = "City Field Is Required")]
        public string city { get; set; }

        [Required(ErrorMessage = "State Field Is Required")]
        public string state { get; set; }

        [Required(ErrorMessage = "Country Field Is Required")]
        public string country { get; set; }

        [Required(ErrorMessage = "Gender Field Is Required")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Languages Field Is Required")]
        public string languages { get; set; }

        [Required(ErrorMessage = "WorkStatus Field Is Required")]
        public string workStatus { get; set; }

        [Required(ErrorMessage = "Address Field Is Required")]
        public string address { get; set; }

    }
}
