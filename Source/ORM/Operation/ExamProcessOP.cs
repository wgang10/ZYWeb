using System.Collections.Generic;
using ZYSoft.Comm.Entity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Data;
using ZYSoft.SQLDAL;
using System.Data.SqlClient;
using System;

namespace ZYSoft.ORM.Operation
{
    public class ExamProcessOP
    {
        /// <summary>
        /// 保存考试过程信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveExamProcess(ExamProcess model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<ExamProcess>(), "ExamProcessXML");
                //配置数据库架构元数据
                SchemaMetadataUpdater.QuoteTableAndColumns(conf);

                //建立SessionFactory
                var factory = conf.BuildSessionFactory();
                //打开Session做持久化数据
                using (session = factory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        id = (int)session.Save(model);
                        tx.Commit();
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 修改考试过程信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void UpdateExamProcess(ExamProcess model)
        {
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<ExamProcess>(), "ExamProcessXML");
                //配置数据库架构元数据
                SchemaMetadataUpdater.QuoteTableAndColumns(conf);

                //建立SessionFactory
                var factory = conf.BuildSessionFactory();
                //打开Session做持久化数据
                using (session = factory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        var list=session.CreateQuery("from ExamProcess E where E.Id='"+model.Id.ToString()+"'")
                            .List<ExamProcess>();
                        if (list.Count > 0)
                        {
                            if (model.EndTime > DateTime.Parse("2000/01/01") )
                            {
                                list[0].EndTime = model.EndTime;
                            }
                            list[0].Evaluate = model.Evaluate;
                        }
                        session.Update(list[0]);
                        tx.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// 取得考试过程列表信息
        /// </summary>
        public static IList<ExamProcessShowModel> GetExamProessList()
        {
            string SQL = @"Select P.Id,P.UserID,U.Name As UserName,P.ExamPaperID As PaperID,Paper.Name As PaperName,P.BeginTime,P.EndTime
,datediff(minute, P.BeginTime,P.EndTime ) As UsedTime,
P.Evaluate
from ExamProcess P Inner Join ExamUser U
	On U.Id = P.UserID Inner Join ExamPaper Paper
	On Paper.Id = P.ExamPaperID 
Order By P.EndTime Desc";
            DataTable tb = new DataTable();
            List<ExamProcessShowModel> lists = new List<ExamProcessShowModel>();
            ExamProcessShowModel model;
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    tb = helper.ExecuteDataTable(SQL, CommandType.Text, parameters);
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        model = new ExamProcessShowModel();
                        model.Id = (int)tb.Rows[i]["Id"];
                        model.UserID = (int)tb.Rows[i]["UserID"];
                        model.UserName = tb.Rows[i]["UserName"].ToString();
                        model.PaperID = (int)tb.Rows[i]["PaperID"];
                        model.PaperName = tb.Rows[i]["PaperName"].ToString();
                        model.BeginTime = (DateTime)tb.Rows[i]["BeginTime"];
                        model.EndTime = (DateTime)tb.Rows[i]["EndTime"];
                        model.UsedTime = tb.Rows[i]["UsedTime"].ToString() + "分钟";
                        model.Evaluate = tb.Rows[i]["Evaluate"].ToString();
                        lists.Add(model);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return lists;
        }

        /// <summary>
        /// 取得考试过程详细信息
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static ExamProcessShowModel GetExamProessInfo(int ProcessID)
        {
            string SQL = @"Select 
U.Name As UserName,
U.Sex,
U.Email,
U.PhoneNo,
U.Addres,
U.Education,
U.School,
U.Subject,
Process.BeginTime,
Process.EndTime,
datediff(minute, Process.BeginTime,Process.EndTime ) As UsedTime,
Process.Evaluate,
Paper.Name as PaperName
From ExamUser U Inner Join ExamProcess Process
On Process.UserID = U.ID Inner Join ExamPaper Paper
On Paper.ID = Process.ExamPaperID
Where Process.Id = @ProcessID";
            DataTable tb = new DataTable();
            ExamProcessShowModel model=null;
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@ProcessID", ProcessID));
                    tb = helper.ExecuteDataTable(SQL, CommandType.Text, parameters);
                    if (tb.Rows.Count > 0)
                    {
                        model = new ExamProcessShowModel();
                        model.UserName = tb.Rows[0]["UserName"].ToString().Trim();
                        model.UserSex = tb.Rows[0]["Sex"].ToString();
                        model.Email = tb.Rows[0]["Email"].ToString().Trim();
                        model.PhoneNo = tb.Rows[0]["PhoneNo"].ToString().Trim();
                        model.Addres = tb.Rows[0]["Addres"].ToString().Trim();
                        model.Education = tb.Rows[0]["Education"].ToString().Trim();
                        model.School = tb.Rows[0]["School"].ToString().Trim();
                        model.Subject = tb.Rows[0]["Subject"].ToString().Trim();
                        model.PaperName = tb.Rows[0]["PaperName"].ToString().Trim();
                        model.BeginTime = (DateTime)tb.Rows[0]["BeginTime"];
                        model.EndTime = (DateTime)tb.Rows[0]["EndTime"];
                        model.UsedTime = tb.Rows[0]["UsedTime"].ToString() + "分钟";
                        model.Evaluate = tb.Rows[0]["Evaluate"].ToString().Trim();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return model;
        }
    }
}
