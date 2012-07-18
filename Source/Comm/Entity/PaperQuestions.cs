using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 试卷-试题对应关系
    /// </summary>
    [DataContract]
    public class PaperQuestions
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }
        /// <summary>
        /// 试卷ID
        /// </summary>
        [DataMember]
        public virtual int PaperId { get; set; }
        /// <summary>
        /// 试题ID
        /// </summary>
        [DataMember]
        public virtual int QuestionsId { get; set; }

        /// <summary>
        /// 试题排序
        /// </summary>
        [DataMember]
        public virtual int QuestionsOrder { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        [DataMember]
        public virtual double Fraction { get; set; }
    }
}
