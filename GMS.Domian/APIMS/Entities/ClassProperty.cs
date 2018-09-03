using System.ComponentModel.DataAnnotations;

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
