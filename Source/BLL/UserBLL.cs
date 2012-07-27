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
    }
}
