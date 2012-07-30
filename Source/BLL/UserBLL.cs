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
                    list[0].LastLoginDateTime = DateTime.Now;
                    list[0].LoginTimes += 1;
                    //如果最后登录时间不是今天（也就是今天第一次登录）积分+10
                    if (list[0].LastLoginDateTime.Date != DateTime.Now.Date)
                    {
                        list[0].Integral += 10;
                    }
                    list[0].UpdateTime = DateTime.Now;
                    isSuccess=MemberOP.UpdateMember(list[0]);
                    if (isSuccess)
                    {
                        HistoryOfMemberUpdate modelHis = new HistoryOfMemberUpdate();
                        modelHis.CreatTime = DateTime.Now;
                        modelHis.UpdateMember = list[0];
                        MemberOP.SaveHistoryOfMemberUpdate(modelHis);
                    }
                }
                else
                {
                    Member model = new Member();
                    model.OpenId = OponID;
                    model.LastLoginDateTime = DateTime.Now;
                    model.LoginTimes = 1;
                    model.Integral = 100;
                    model.Status = 0;
                    model.UpdateTime = DateTime.Now;
                    model.CreatTime = DateTime.Now;
                    if (MemberOP.SaveMember(model) != -1)
                    {
                        HistoryOfMemberUpdate modelHis = new HistoryOfMemberUpdate();
                        modelHis.CreatTime = DateTime.Now;
                        modelHis.UpdateMember = model;
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
        /// 获取用户信息
        /// </summary>
        /// <param name="OponID"></param>
        /// <returns></returns>
        public IList<Member> GetMemberByOpenID(string OponID)
        {
            return MemberOP.GetMemberByOpenID(OponID);
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool LoginMember(string ID,string PWD,ref string Msg)
        {
            return true;
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
    }
}
