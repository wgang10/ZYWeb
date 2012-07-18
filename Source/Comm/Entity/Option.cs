using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    [DataContract]
    public class Option
    {
        /// <summary>
        /// 这是选项的文本
        /// </summary>
        [DataMember]
        public string Caption
        {
            get;
            set;
        }

        /// <summary>
        /// 标识该选项是否被用户选中
        /// 选中为true，未选中为false
        /// </summary>
        [DataMember]
        public bool IsChecked { get; set; }

    }
}
