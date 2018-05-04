using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Entities
{
    /// <summary>
    /// API的类别
    /// </summary>
    public class APIClassification : BaseModel
    {
        [Display(Name = "分类名")]
        public string Name { get; set; }
        [Display(Name = "归属系统")]
        public string BelongSystem { get; set; }
        [Display(Name = "可见性")]
        public bool Visiable { get; set; }
    }
}