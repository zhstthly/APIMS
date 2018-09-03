using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Entities
{
    public class CustomerClass : BaseModel
    {
        public CustomerClass()
        {
            this.Properties = new List<ClassProperty>();
        }

        public int ClassificationID { get; set; }

        [Display(Name="名称")]
        public string Name { get; set; }

        public byte IsCommon { get; set; }

        public ICollection<ClassProperty> Properties { get; set; }
    }
}
