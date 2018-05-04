using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Entities
{
    public class Result : BaseModel
    {
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "类型")]
        public int ClassTypeID { get; set; }
    }
}
