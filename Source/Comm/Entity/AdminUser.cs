using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 管理员
    /// </summary>
    [DataContract]
    public class AdminUser
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 登录ID
        /// </summary>
        [DataMember]
        public virtual string LoginID { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [DataMember]
        public virtual string LoginPWD { get; set; }

        /// <summary>
        /// 用户状态 0:正常 1:限制 2:删除
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime CreatTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DataMember]
        public virtual DateTime UpdateTime { get; set; }
    }
}
