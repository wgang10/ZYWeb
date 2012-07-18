using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 考试过程
    /// </summary>
    [DataContract]
    public class ExamProcess
    {
        /// <summary>
        /// 过程ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }

        /// <summary>
        /// 答题人
        /// </summary>
        [DataMember]
        public virtual int UserID { get; set; }

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
        public virtual int ExamPaperID { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        [DataMember]
        public virtual double Score { get; set; }

        /// <summary>
        /// 评价
        /// </summary>
        [DataMember]
        public virtual string Evaluate { get; set; }
    }
}
