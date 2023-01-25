using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetFrameworkWebApp.Models
{
    public class JobDetailsViewModel
    {
        public int JobCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Qualification")]
        public string MinimumQualification { get; set; }
        [DisplayName("Job Description")]
        [Required(ErrorMessage = "Required")]
        public string JobDescription { get; set; }
        [DisplayName("Last Date")]
        [Required(ErrorMessage = "Required")]
        public DateTime ApplicationLastDate { get; set; }
    }
}