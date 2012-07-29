using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 会员更改历史记录实体
    /// </summary>
    [DataContract]
    public class HistoryOfMemberUpdate
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }

        /// <summary>
        /// 会员信息
        /// </summary>
        [DataMember]
        public virtual Member UpdateMember { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime CreatTime { get; set; }
    }
}
