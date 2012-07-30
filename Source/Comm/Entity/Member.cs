﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    /// <summary>
    /// 会员实体对象
    /// </summary>
    [DataContract]
    public class Member
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }

        /// <summary>
        /// QQ登陆后返回ID
        /// </summary>
        [DataMember]
        public virtual string OpenId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember]
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DataMember]
        public virtual string Phone { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [DataMember]
        public virtual string LoginPWD { get; set; }

        /// <summary>
        /// 会员类型
        /// </summary>
        [DataMember]
        public virtual string Type { get; set; }

        /// <summary>
        /// 问题一
        /// </summary>
        [DataMember]
        public virtual string Question1 { get; set; }

        /// <summary>
        /// 问题二
        /// </summary>
        [DataMember]
        public virtual string Question2 { get; set; }

        /// <summary>
        /// 问题三
        /// </summary>
        [DataMember]
        public virtual string Question3 { get; set; }

        /// <summary>
        /// 答案一
        /// </summary>
        [DataMember]
        public virtual string Anwser1 { get; set; }        

        /// <summary>
        /// 答案二
        /// </summary>
        [DataMember]
        public virtual string Anwser2 { get; set; }

        /// <summary>
        /// 答案三
        /// </summary>
        [DataMember]
        public virtual string Anwser3 { get; set; }

        /// <summary>
        /// 头像图片
        /// </summary>
        [DataMember]
        public virtual byte[] Photo { get; set; }

        /// <summary>
        /// 头像URL
        /// </summary>
        [DataMember]
        public virtual string PhotoURL { get; set; }

        /// <summary>
        /// 登陆次数
        /// </summary>
        [DataMember]
        public virtual int LoginTimes{ get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        [DataMember]
        public virtual DateTime LastLoginDateTime { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        [DataMember]
        public virtual int Integral { get; set; }

        /// <summary>
        /// 会员状态 0:正常 1:限制 2:删除
        /// </summary>
        [DataMember]
        public virtual int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime CreatTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DataMember]
        public virtual DateTime UpdateTime { get; set; }
    }
}
