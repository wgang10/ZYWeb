using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSoft.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using ZYSoft.Comm.Entity;
using ZYSoft.ORM.Operation;

namespace ZYSoft.BLL
{
    public class UserBLL
    {
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public string GetUaerName()
        {
            string SQL = @"Select Top 1 Name From ExamPaper Order by UpdateDateTime desc";
            DataTable tb = new DataTable();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    tb = helper.ExecuteDataTable(SQL, CommandType.Text, parameters);
                    if (tb.Rows.Count > 0)
                    {
                        return tb.Rows[0][0].ToString();
                    }
                    else
                    {
                        return "没有数据！";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        /// <summary>
        /// 获取试卷列表
        /// </summary>
        /// <returns></returns>
        public IList<ExamPaper> GetExamPapersList()
        {
            return ExamPaperOP.GetExamPapersList();
        }

        /// <summary>
        /// 保存试卷
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveOrUpdateExamPaper(ExamPaper model)
        {
            return ExamPaperOP.SaveExamPaper(model);
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool LoginMember(string OponID,ref string Msg)
        {
            bool isSuccess = false;
            try
            {
                IList<Member> list = MemberOP.GetMemberByOpenID(OponID);
                if (list.Count > 0)
                {                    
                    list[0].LoginTimes += 1;
                    //如果最后登录时间不是今天（也就是今天第一次登录）积分+10
                    if (list[0].LastLoginDateTime.Date != DateTime.Now.Date && list[0].LastLoginDateTime <DateTime.Now)
                    {
                        list[0].Integral += 10;
                    }
                    list[0].LastLoginDateTime = list[0].CurrentLoginDateTime;
                    list[0].CurrentLoginDateTime = DateTime.Now;
                    list[0].UpdateTime = DateTime.Now;
                    isSuccess=MemberOP.UpdateMember(list[0]);
                }
                else
                {
                    Member model = new Member();
                    HistoryOfMemberUpdate modelHis = new HistoryOfMemberUpdate();
                    model.OpenId = OponID;
                    model.LastLoginDateTime = DateTime.Now;
                    model.CurrentLoginDateTime = DateTime.Now;
                    model.LoginTimes = 1;
                    model.Integral = 100;
                    //model.Birthday = new DateTime(1800, 1, 1, 1, 1, 1);
                    //model.VerifictionCodeLimit = new DateTime(1800, 1, 1, 1, 1, 1);
                    model.Status = 0;
                    model.UpdateTime = DateTime.Now;
                    model.CreatTime = DateTime.Now;
                    modelHis.MemberId = MemberOP.SaveMember(model);
                    if (modelHis.MemberId != -1)
                    {   
                        modelHis.CreatTime = DateTime.Now;

                        #region 会员历史信息
                        modelHis.OpenId = model.OpenId;
                        modelHis.Nickname = model.Nickname;
                        modelHis.Question1 = model.Question1;
                        modelHis.Question2 = model.Question2;
                        modelHis.Question3 = model.Question3;
                        modelHis.Anwser1 = model.Anwser1;
                        modelHis.Anwser2 = model.Anwser2;
                        modelHis.Anwser3 = model.Anwser3;
                        modelHis.Email = model.Email;
                        modelHis.Phone = model.Phone;
                        modelHis.LoginPWD = model.LoginPWD;
                        modelHis.Type = model.Type;
                        modelHis.Photo = model.Photo;
                        modelHis.PhotoURL = model.PhotoURL;
                        modelHis.Gender = model.Gender;
                        modelHis.Birthday = model.Birthday;
                        modelHis.Birthplace = model.Birthplace;
                        modelHis.Education = model.Education;
                        modelHis.Job = model.Job;
                        modelHis.Address = model.Address;
                        modelHis.LoginTimes = model.LoginTimes;
                        modelHis.LastLoginDateTime = model.LastLoginDateTime;
                        modelHis.CurrentLoginDateTime = model.CurrentLoginDateTime;
                        modelHis.Integral = model.Integral;
                        modelHis.Status = model.Status;
                        #endregion

                        MemberOP.SaveHistoryOfMemberUpdate(modelHis);
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                isSuccess = false;
            }
            return isSuccess;
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool LoginMember(string ID, string PWD, ref string Msg)
        {
            return true;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="OponID"></param>
        /// <returns></returns>
        public IList<Member> GetMemberByOpenID(string OponID)
        {
            return MemberOP.GetMemberByOpenID(OponID);
        }

        /// <summary>
        /// 获取用户修改历史信息
        /// </summary>
        /// <param name="OponID"></param>
        /// <returns></returns>
        public IList<HistoryOfMemberUpdate> GetHistoryOfMemberUpdateByOpenID(string OponID)
        {
            return MemberOP.GetHistoryOfMemberUpdateByOpenID(OponID);
        }

        /// <summary>
        /// 保存会员信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool SaveMemberInfo(Member model,ref string Msg)
        {
            return true;
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="Nickname"></param>
        /// <param name="Email"></param>
        /// <param name="PassWord"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool RegistMember(string Nickname, string Email, string PassWord, ref string Msg)
        {
            IList<Member> list = MemberOP.GetMemberByEmail(Email);
            if (list.Count > 0)
            {
                if (list[0].Status != 3)
                {
                    Msg = "邮箱已注册，请直接登录";
                    return false;
                }
                else
                {
                    list[0].Nickname = Nickname;
                    list[0].LoginPWD = PassWord;
                    list[0].UpdateTime = DateTime.Now;
                    list[0].CreatTime = DateTime.Now;
                    list[0].VerifictionCode = Comm.GlobalMethod.GenerateVerifictionCode();
                    list[0].VerifictionCodeLimit = DateTime.Now.AddMinutes(30);
                    return MemberOP.UpdateMember(list[0]);
                }
            }
            else
            {
                Member model = new Member();
                
                model.Nickname = Nickname;
                model.Email = Email;
                model.LoginPWD = PassWord;
                model.Status = 3;//刚注册未验证
                model.LoginTimes = 0;
                model.Integral = 0;
                model.UpdateTime = DateTime.Now;
                model.CreatTime = DateTime.Now;
                model.VerifictionCode = Comm.GlobalMethod.GenerateVerifictionCode();
                model.VerifictionCodeLimit = DateTime.Now.AddMinutes(30);
                model.Id = MemberOP.SaveMember(model);
                if (model.Id != -1)
                {
                    Msg = "注册失败！";
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
