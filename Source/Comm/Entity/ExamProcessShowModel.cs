using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 考试过程(业务展示用)
    /// </summary>
    [DataContract]
    public class ExamProcessShowModel
    {
        /// <summary>
        /// 过程ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }

        /// <summary>
        /// 答题人ID
        /// </summary>
        [DataMember]
        public virtual int UserID { get; set; }

        /// <summary>
        /// 答题人姓名
        /// </summary>
        [DataMember]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 答题人性别 性别 0：男 1：女
        /// </summary>
        [DataMember]
        public virtual string UserSex { get; set; }

        /// <summary>
        /// 答题人邮箱
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 答题人电话
        /// </summary>
        [DataMember]
        public virtual string PhoneNo { get; set; }

        /// <summary>
        /// 答题人住址
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
        /// 开始时间
        /// </summary>
        [DataMember]
        public virtual DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public virtual DateTime EndTime { get; set; }

        /// <summary>
        /// 试卷ID
        /// </summary>
        [DataMember]
        public virtual int PaperID { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        [DataMember]
        public virtual string PaperName { get; set; }

        /// <summary>
        /// 用时
        /// </summary>
        [DataMember]
        public virtual string UsedTime { get; set; }

        /// <summary>
        /// 结论
        /// </summary>
        [DataMember]
        public virtual string Evaluate { get; set; }
    }
}
