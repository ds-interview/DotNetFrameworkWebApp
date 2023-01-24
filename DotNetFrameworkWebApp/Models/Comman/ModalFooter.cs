﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetFrameworkWebApp.Models.Comman
{
    public class ModalFooter
    {
        public ModalFooter()
        {
            SubmitButtonText = "Submit";
            CancelButtonText = "Cancel";
            SubmitButtonID = "btn-submit";
            CancelButtonID = "btn-cancel";
            SubmitButtonCss = "btn btn-success";
            CancelButtonCss = "btn btn-info";
            ShowButtons = true;
            Message = string.Empty;
        }

        public bool Id { get; set; }
        public string SubmitButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string SubmitButtonID { get; set; }
        public string CancelButtonID { get; set; }
        public bool OnlyCancelButton { get; set; }
        public string SubmitButtonCss { get; set; }
        public string CancelButtonCss { get; set; }
        public bool ShowButtons { get; set; }
        public string Message { get; set; }
    }
}