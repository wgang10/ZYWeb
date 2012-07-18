using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 用户答案
    /// </summary>
    [DataContract]
    public class UserAnswer
    {
        /// <summary>
        /// 试卷名称
        /// </summary>
        [DataMember]
        public virtual string PaperName { get; set; }

        /// <summary>
        /// 过程ID
        /// </summary>
        [DataMember]
        public virtual int ProcessID { get; set; }

        /// <summary>
        /// 题型ID
        /// </summary>
        [DataMember]
        public virtual int TypeID { get; set; }

        /// <summary>
        /// 试题ID
        /// </summary>
        [DataMember]
        public virtual int QuestionID { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        [DataMember]
        public virtual string Title { get; set; }

        /// <summary>
        /// 选项一
        /// </summary>
        [DataMember]
        public virtual string Answer1 { get; set; }

        /// <summary>
        /// 选项二
        /// </summary>
        [DataMember]
        public virtual string Answer2 { get; set; }

        /// <summary>
        /// 选项三
        /// </summary>
        [DataMember]
        public virtual string Answer3 { get; set; }

        /// <summary>
        /// 选项四
        /// </summary>
        [DataMember]
        public virtual string Answer4 { get; set; }

        /// <summary>
        /// 参考答案
        /// </summary>
        [DataMember]
        public virtual string CorrectAnswer { get; set; }

        /// <summary>
        /// 用户答案
        /// </summary>
        [DataMember]
        public virtual string Answer { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        [DataMember]
        public virtual double Fraction { get; set; }

        /// <summary>
        /// 考察点
        /// </summary>
        [DataMember]
        public virtual int ClassID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public virtual string Remarks { get; set; }
    }
}
