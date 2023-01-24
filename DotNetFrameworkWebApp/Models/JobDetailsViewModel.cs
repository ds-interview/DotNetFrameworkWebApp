using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetFrameworkWebApp.Models
{
    public class JobDetailsViewModel
    {
        public int JobCode { get; set; }
        public string Title { get; set; }
        public string MinimumQualification { get; set; }
        public string JobDescription { get; set; }
        public DateTime ApplicationLastDate { get; set; }
    }
}