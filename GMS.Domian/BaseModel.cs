using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseModel
    {
        public BaseModel()
        {
            this.ModifyTime = DateTime.Now;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 修改账号
        /// </summary>
        public string ModifyAccount { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "描述")]
        public string Description { get; set; }
    }
}
