using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetFrameworkWebApp.Model
{
    public class JobApplicationViewModel
    {
        public int Id { get; set; }
        public string Job_Code { get; set; }
        public string Title { get; set; }
        public string Minimum_Qualification { get; set; }
        public string Sort_Description { get; set; }
        public Nullable<System.DateTime> Application_Last_Date { get; set; }
    }
}