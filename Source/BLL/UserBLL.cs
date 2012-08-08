using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSoft.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using ZYSoft.Comm.Entity;
using ZYSoft.ORM.Operation;
using System.Configuration;

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
        /// <param name="OponID"></param>
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
                    if (list[0].LastLoginDateTime.Value.Date != DateTime.Now.Date && list[0].LastLoginDateTime <DateTime.Now)
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
        /// <param name="LoginID"></param>
        /// <param name="PWD"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool LoginMember(string LoginID, string PWD, ref string Msg,ref Member model)
        {
            bool isSuccess = false;
            IList<Member> list = MemberOP.GetNormalMemberByEmail(LoginID);
            if (list.Count > 0)
            {
                if (!list[0].LoginPWD.Trim().Equals(Comm.GlobalMethod.EncryptPWD(PWD)))
                {
                    Msg = "用户名或密码错误";
                    return false;
                }

                list[0].LoginTimes += 1;
                //如果最后登录时间不是今天（也就是今天第一次登录）积分+10
                if (list[0].LastLoginDateTime.HasValue && list[0].LastLoginDateTime.Value.Date != DateTime.Now.Date && list[0].LastLoginDateTime < DateTime.Now)
                {
                    list[0].Integral += 10;
                }
                list[0].LastLoginDateTime = list[0].CurrentLoginDateTime;
                list[0].CurrentLoginDateTime = DateTime.Now;
                list[0].UpdateTime = DateTime.Now;
                model = list[0];
                return MemberOP.UpdateMember(list[0]);
            }
            else
            {
                Msg = "会员不存在";
                return false;
            }
        }
        
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool LoginMember(int ID, ref string Msg,ref Member model)
        {
            bool isSuccess = false;
            IList<Member> list = MemberOP.GetNormalMemberByID(ID);
            if (list.Count > 0)
            {                    
                list[0].LoginTimes += 1;
                //如果最后登录时间不是今天（也就是今天第一次登录）积分+10
                if (list[0].LastLoginDateTime.HasValue && list[0].LastLoginDateTime.Value.Date != DateTime.Now.Date && list[0].LastLoginDateTime <DateTime.Now)
                {
                    list[0].Integral += 10;
                }
                list[0].LastLoginDateTime = list[0].CurrentLoginDateTime;
                list[0].CurrentLoginDateTime = DateTime.Now;
                list[0].UpdateTime = DateTime.Now;
                model = list[0];
                return MemberOP.UpdateMember(list[0]);
            }
            else
            {
                Msg = "会员不存在";
                return false;
            }
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
        public bool RegistMember(string Nickname, string Email, string PassWord, ref string Msg ,ref int ID)
        {
            IList<Member> list = MemberOP.GetAllMemberByEmail(Email);
            if (list.Count > 0)
            {
                if (list[0].Status != 3)
                {
                    Msg = "邮箱已注册，请直接登录";
                    ID = list[0].Id;
                    return false;
                }
                else
                {
                    list[0].Nickname = Nickname;
                    list[0].LoginPWD = Comm.GlobalMethod.EncryptPWD(PassWord);
                    list[0].UpdateTime = DateTime.Now;
                    list[0].CreatTime = DateTime.Now;
                    list[0].VerifictionCode = Comm.GlobalMethod.GenerateVerifictionCode();
                    int limitMinutes = 30;
                    int.TryParse(ConfigurationManager.AppSettings["VerifictionCodeLimitMinutes"], out limitMinutes);
                    list[0].VerifictionCodeLimit = DateTime.Now.AddMinutes(limitMinutes);
                    Msg = String.Format("{0} [过期时间：{1:yyyy/MM/dd HH:mm:ss}]", list[0].VerifictionCode, list[0].VerifictionCodeLimit.Value);
                    ID = list[0].Id;
                    return MemberOP.UpdateMember(list[0]);
                }
            }
            else
            {
                Member model = new Member();
                
                model.Nickname = Nickname;
                model.Email = Email;
                model.LoginPWD = Comm.GlobalMethod.EncryptPWD(PassWord);
                model.Status = 3;//刚注册未验证
                model.LoginTimes = 0;
                model.Integral = 0;
                model.UpdateTime = DateTime.Now;
                model.CreatTime = DateTime.Now;
                model.VerifictionCode = Comm.GlobalMethod.GenerateVerifictionCode();
                int limitMinutes = 30;
                int.TryParse(ConfigurationManager.AppSettings["VerifictionCodeLimitMinutes"], out limitMinutes);
                model.VerifictionCodeLimit = DateTime.Now.AddMinutes(limitMinutes);
                model.Id = MemberOP.SaveMember(model);
                if (model.Id == -1)
                {
                    Msg = "注册失败！";
                    return false;
                }
                else
                {
                    Msg = String.Format("{0} [过期时间:{1:yyyy/MM/dd HH:mm:ss}]", model.VerifictionCode, model.VerifictionCodeLimit.Value);
                    ID = model.Id;
                    return true;
                }
            }
        }

        public bool ActivatMember(int MemberID, string VerifictionCode,ref string Msg)
        {
            IList<Member> list = MemberOP.GetAllMemberByID(MemberID);
            if (list.Count > 0)
            {
                if (list[0].Status == 3)
                {
                    if (list[0].VerifictionCodeLimit < DateTime.Now)
                    {
                        Msg = "验证码有效期已过，请重新发送验证码";
                        return false;
                    }
                    else
                    {
                        if (list[0].VerifictionCode.Trim().Equals(VerifictionCode.Trim()))
                        {
                            //修改用户状态为正常
                            list[0].Status = 0;
                            list[0].UpdateTime = DateTime.Now;
                            bool isSuccess = MemberOP.UpdateMember(list[0]);
                            //添加用户历史信息
                            if (isSuccess)
                            {
                                #region 会员历史信息
                                HistoryOfMemberUpdate modelHis = new HistoryOfMemberUpdate();
                                modelHis.CreatTime = DateTime.Now;
                                modelHis.MemberId = list[0].Id;
                                modelHis.OpenId = list[0].OpenId;
                                modelHis.Nickname = list[0].Nickname;
                                modelHis.Question1 = list[0].Question1;
                                modelHis.Question2 = list[0].Question2;
                                modelHis.Question3 = list[0].Question3;
                                modelHis.Anwser1 = list[0].Anwser1;
                                modelHis.Anwser2 = list[0].Anwser2;
                                modelHis.Anwser3 = list[0].Anwser3;
                                modelHis.Email = list[0].Email;
                                modelHis.Phone = list[0].Phone;
                                modelHis.LoginPWD = list[0].LoginPWD;
                                modelHis.Type = list[0].Type;
                                modelHis.Photo = list[0].Photo;
                                modelHis.PhotoURL = list[0].PhotoURL;
                                modelHis.Gender = list[0].Gender;
                                modelHis.Birthday = list[0].Birthday;
                                modelHis.Birthplace = list[0].Birthplace;
                                modelHis.Education = list[0].Education;
                                modelHis.Job = list[0].Job;
                                modelHis.Address = list[0].Address;
                                modelHis.LoginTimes = list[0].LoginTimes;
                                modelHis.LastLoginDateTime = list[0].LastLoginDateTime;
                                modelHis.CurrentLoginDateTime = list[0].CurrentLoginDateTime;
                                modelHis.Integral = list[0].Integral;
                                modelHis.Status = list[0].Status;
                                #endregion

                                if (MemberOP.SaveHistoryOfMemberUpdate(modelHis) == -1)
                                {
                                    Msg = "保存会员历史信息发生错误";
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                Msg = "保存会员信息发生错误";
                                return false;
                            }
                        }
                        else
                        {
                            Msg = "验证码错误";
                            return false;
                        }
                    }
                }
                else
                {
                    Msg = "已经激活，请直接登录。";
                    return false;
                }
            }
            else
            {
                Msg = "没有找到本会员信息";
                return false;
            }
        }
    }
}
