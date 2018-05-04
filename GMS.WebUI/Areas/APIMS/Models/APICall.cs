using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMS.WebUI.Areas.APIMS.Models
{
    public class APICall
    {
        public string Url { get; set; }
        public object Data { get; set; }
        public string Type { get; set; }
    }
}