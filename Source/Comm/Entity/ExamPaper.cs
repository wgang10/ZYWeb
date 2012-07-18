using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 试卷
    /// </summary>
    [DataContract]
    public class ExamPaper
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }
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
    }
}
