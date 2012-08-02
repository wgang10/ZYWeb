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
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime CreatTime { get; set; }

        #region 会员信息

        /// <summary>
        /// 会员ID
        /// </summary>
        [DataMember]
        public virtual int MemberId { get; set; }

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
        /// 性别
        /// </summary>
        [DataMember]
        public virtual string Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DataMember]
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 出生地
        /// </summary>
        [DataMember]
        public virtual string Birthplace { get; set; }

        /// <summary>
        /// 教育背景
        /// </summary>
        [DataMember]
        public virtual string Education { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [DataMember]
        public virtual string Job { get; set; }

        /// <summary>
        /// 居住地
        /// </summary>
        [DataMember]
        public virtual string Address { get; set; }        

        /// <summary>
        /// 登陆次数
        /// </summary>
        [DataMember]
        public virtual int LoginTimes { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        [DataMember]
        public virtual DateTime LastLoginDateTime { get; set; }

        /// <summary>
        /// 本次登陆时间
        /// </summary>
        [DataMember]
        public virtual DateTime CurrentLoginDateTime { get; set; }

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

        #endregion
    }
}
