﻿using DotNetFrameworkWebApp.Code.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetFrameworkWebApp.Model.Comman
{
    public class Notification
    {
        public string Heading { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public string Icon
        {
            get
            {
                switch (this.Type)
                {
                    case MessageType.Warning:
                        return "icon-warning-sign";
                    case MessageType.Success:
                        return "fa fa-check";
                    case MessageType.Danger:
                        return "icon-remove-sign";
                    case MessageType.Info:
                        return "icon-info-sign";
                    default:
                        return "icon-info-sign";
                }
            }
        }
    }
}