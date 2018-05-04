using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Entities
{
    /// <summary>
    /// API项
    /// </summary>
    public class APIItem : APIItemBase
    {
        [Display(Name = "例子")]
        public string Demo { get; set; }
        public ICollection<InputParameter> InputParameters { get; set; }
        public Result Result { get; set; }
        public APIItem()
        {
            Result = new Result();
            InputParameters = new List<InputParameter>();
        }
    }
}
