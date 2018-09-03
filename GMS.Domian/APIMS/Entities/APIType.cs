using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Entities
{
    /// <summary>
    /// API属性
    /// </summary>
    public class APIType : BaseModel
    {
        [Display(Name = "属性名")]
        public string Name { get; set; }

        [Display(Name = "颜色")]
        public string Color { get; set; }
    }
}
