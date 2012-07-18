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
    public class Answers
    {
        /// <summary>
        /// 答案ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }

        /// <summary>
        /// 题目ID
        /// </summary>
        [DataMember]
        public virtual int QuestionId { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        [DataMember]
        public virtual string Answer { get; set; }

        /// <summary>
        /// 考试过程ID
        /// </summary>
        [DataMember]
        public virtual int ExamProcessID { get; set; }

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
        /// 得分
        /// </summary>
        [DataMember]
        public virtual double Fraction { get; set; }
    }
}
