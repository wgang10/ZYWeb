using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 试题
    /// </summary>
    [DataContract]
    public class Questions
    {
        /// <summary>
        /// 试题ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }
        /// <summary>
        /// 题目
        /// </summary>
        [DataMember]
        public virtual string Title { get; set; }
        /// <summary>
        /// 备选答案1
        /// </summary>
        [DataMember]
        public virtual string Answer1 { get; set; }
        /// <summary>
        /// 备选答案2
        /// </summary>
        [DataMember]
        public virtual string Answer2{ get; set; }
        /// <summary>
        /// 备选答案3
        /// </summary>
        [DataMember]
        public virtual string Answer3 { get; set; }
        /// <summary>
        /// 备选答案4
        /// </summary>
        [DataMember]
        public virtual string Answer4 { get; set; }
        /// <summary>
        /// 备选答案5
        /// </summary>
        [DataMember]
        public virtual string Answer5 { get; set; }
        /// <summary>
        /// 备选答案6
        /// </summary>
        [DataMember]
        public virtual string Answer6{ get; set; }
        /// <summary>
        /// 备选答案7
        /// </summary>
        [DataMember]
        public virtual string Answer7{ get; set; }
        /// <summary>
        /// 备选答案8
        /// </summary>
        [DataMember]
        public virtual string Answer8 { get; set; }
        /// <summary>
        /// 备选答案9
        /// </summary>
        [DataMember]
        public virtual string Answer9 { get; set; }
        /// <summary>
        /// 备选答案10
        /// </summary>
        [DataMember]
        public virtual string Answer10 { get; set; }
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
        /// 参数类型  0:单选 1：多选 2:填空题 3：问答题
        /// </summary>
        [DataMember]
        public virtual int TypeID { get; set; }
        /// <summary>
        /// 考察类型  0:基础知识 1：C# 2:Asp.Net 3：SilverLight 4:测试 5:架构 6:项目管理
        /// </summary>
        [DataMember]
        public virtual int ClassID { get; set; }
        /// <summary>
        /// 正确答案
        /// </summary>
        [DataMember]
        public virtual string CorrectAnswer { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        [DataMember]
        public virtual double Fraction { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public virtual string Remarks { get; set; }

        //private List<Option> _options = new List<Option>();
        //[DataMember]
        //public List<Option> Options
        //{
        //    get
        //    {
        //        return this._options;
        //    }
        //}

    }
}
