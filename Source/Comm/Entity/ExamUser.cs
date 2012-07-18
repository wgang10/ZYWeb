using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 考试用户
    /// </summary>
    [DataContract]
    public class ExamUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别 0：男 1：女
        /// </summary>
        [DataMember]
        public virtual string Sex { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [DataMember]
        public virtual string Birthplace { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        [DataMember]
        public virtual string Addres { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [DataMember]
        public virtual string Education { get; set; }
        /// <summary>
        /// 毕业学校
        /// </summary>
        [DataMember]
        public virtual string School { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [DataMember]
        public virtual string Subject { get; set; }        
        /// <summary>
        /// 状态 0:无效 1：正常
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime CreatDateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DataMember]
        public virtual DateTime UpdateDateTime { get; set; }
        /// <summary>
        /// 登记类型 0：初级 1：中级 2:高级
        /// </summary>
        [DataMember]
        public virtual int GradeTypeID { get; set; }
        /// <summary>
        /// Post类型 0：开发 1：设计 2:测试 3:管理 等等
        /// </summary>
        [DataMember]
        public virtual int PostTypeID { get; set; }
    }
}
