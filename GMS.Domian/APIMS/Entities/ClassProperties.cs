using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Entities
{
    public class ClassProperty : BaseModel
    {
        [Display(Name = "属性名")]
        public string Name { get; set; }
        [Display(Name = "类型")]
        public int ClassTypeID { get; set; }
        [Display(Name = "长度")]
        public int Length { get; set; }
        [Display(Name = "值(默认)")]
        public string DefaultValue { get; set; }
    }
}
