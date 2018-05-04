using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMS.WebUI.Models
{
    public class AjaxResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}