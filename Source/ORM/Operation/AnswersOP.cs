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
    public class AnswersOP
    {
        /// <summary>
        /// 保存答案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveAnswers(Answers model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Answers>(), "AnswersXML");
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
        /// 更新答案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void UpdateAnswers(Answers model)
        {
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Answers>(), "AnswersXML");
                //配置数据库架构元数据
                SchemaMetadataUpdater.QuoteTableAndColumns(conf);

                //建立SessionFactory
                var factory = conf.BuildSessionFactory();
                //打开Session做持久化数据
                using (session = factory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        session.Update(model);
                        tx.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// 提交答案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateAnswers(IList<Answers> list)
        {
            int resoult = -1;
            if (list != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Answers>(), "AnswersXML");
                //配置数据库架构元数据
                SchemaMetadataUpdater.QuoteTableAndColumns(conf);

                //建立SessionFactory
                var factory = conf.BuildSessionFactory();
                //打开Session做持久化数据
                using (session = factory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            session.Save(list[i]);
                            resoult++;
                        }
                        tx.Commit();
                    }
                }
            }
            return resoult;
        }

        /// <summary>
        /// 取得考试者答题信息
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static IList<UserAnswer> GetUserAnswersList(int ProcessID)
        {
            string SQL = @"Select 
Paper.Name,
Process.Id As ProcessID,
Q.TypeID,
Q.Id As QuestionID,
Q.Title,
Q.Answer1,
Q.Answer2,
Q.Answer3,
Q.Answer4,
Q.CorrectAnswer,
A.Answer,
Q.Fraction,
Q.ClassID,
Q.Remarks
From ExamPaper Paper Inner Join ExamProcess Process
On Process.ExamPaperID = Paper.ID Inner Join PaperQuestions PQ
On PQ.PaperId = Process.ExamPaperID Inner Join Questions Q
On Q.ID = PQ.QuestionsId Left Join Answers A
On A.QuestionId = Q.Id And A.ExamProcessID = Process.Id
Where Process.Id=@ProcessID
Order By Process.Id,PQ.PaperId,Q.TypeID,Q.ID";
            DataTable tb = new DataTable();
            List<UserAnswer> lists = new List<UserAnswer>();
            UserAnswer model;
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@ProcessID", ProcessID));
                    tb = helper.ExecuteDataTable(SQL, CommandType.Text, parameters);
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        model = new UserAnswer();
                        model.PaperName = tb.Rows[i]["Name"].ToString();
                        model.ProcessID = (int)tb.Rows[i]["ProcessID"];
                        model.TypeID = (int)tb.Rows[i]["TypeID"];
                        model.Title = tb.Rows[i]["Title"].ToString().Trim();
                        model.Answer1 = string.IsNullOrEmpty(tb.Rows[i]["Answer1"].ToString().Trim()) ? "" : "【选项一】:" + tb.Rows[i]["Answer1"].ToString().Trim();
                        model.Answer2 = string.IsNullOrEmpty(tb.Rows[i]["Answer2"].ToString().Trim()) ? "" : "【选项二】:" + tb.Rows[i]["Answer2"].ToString().Trim();
                        model.Answer3 = string.IsNullOrEmpty(tb.Rows[i]["Answer3"].ToString().Trim()) ? "" : "【选项三】:" + tb.Rows[i]["Answer3"].ToString().Trim();
                        model.Answer4 = string.IsNullOrEmpty(tb.Rows[i]["Answer4"].ToString().Trim()) ? "" : "【选项四】:" + tb.Rows[i]["Answer4"].ToString().Trim();
                        model.CorrectAnswer = string.IsNullOrEmpty(tb.Rows[i]["CorrectAnswer"].ToString().Trim()) ? "" : "【参考答案】:" + tb.Rows[i]["CorrectAnswer"].ToString().Trim();
                        model.Answer = "【考试者答案】：" + tb.Rows[i]["Answer"].ToString();
                        model.Fraction = (double)tb.Rows[i]["Fraction"];
                        model.ClassID = string.IsNullOrEmpty(tb.Rows[i]["ClassID"].ToString())?  -1 : (int)tb.Rows[i]["ClassID"];
                        model.Remarks = string.IsNullOrEmpty(tb.Rows[i]["Remarks"].ToString().Trim()) ? "" : "【备注】:" + tb.Rows[i]["Remarks"].ToString().Trim();
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
    }
}
