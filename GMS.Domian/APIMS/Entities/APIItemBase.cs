using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Entities
{
    public class APIItemBase:BaseModel
    {
        public int ClassificationID { get; set; }
        [Display(Name = "API名")]
        public string Name { get; set; }
        [Display(Name = "API属性")]
        public int TypeID { get; set; }
        [Display(Name = "可见性")]
        public byte Visiable { get; set; }
    }
}
