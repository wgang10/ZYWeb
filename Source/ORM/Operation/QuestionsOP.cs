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
    public class QuestionsOP
    {
        /// <summary>
        /// 保存问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveQuestion(Questions model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Questions>(), "QuestionsXML");
                //配置数据库架构元数据
                SchemaMetadataUpdater.QuoteTableAndColumns(conf);

                //建立SessionFactory
                var factory = conf.BuildSessionFactory();
                //打开Session做持久化数据
                using (session = factory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        var query = session.QueryOver<Questions>()
                            .Where(p => p.Id == model.Id)
                        .List();
                        if (query.Count > 0)
                        {
                            //更新
                            query[0].Title = model.Title;
                            query[0].Answer1 = model.Answer1;
                            query[0].Answer2 = model.Answer2;
                            query[0].Answer3 = model.Answer3;
                            query[0].Answer4 = model.Answer4;
                            query[0].Answer5 = model.Answer5;
                            query[0].Answer6 = model.Answer6;
                            query[0].Answer7 = model.Answer7;
                            query[0].Answer8 = model.Answer8;
                            query[0].Answer9 = model.Answer9;
                            query[0].Answer10 = model.Answer10;
                            query[0].Status = model.Status;
                            query[0].UpdateDateTime = DateTime.Now;
                            query[0].TypeID = model.TypeID;
                            query[0].ClassID = model.ClassID;
                            query[0].CorrectAnswer = model.CorrectAnswer;
                            query[0].Fraction = model.Fraction;
                            query[0].Remarks = model.Remarks;
                            session.Update(query[0]);
                        }
                        else
                        {
                            //新增
                            id = (int)session.Save(model);
                        }
                        tx.Commit();
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 查询问题列表
        /// </summary>
        /// <returns></returns>
        public static IList<Questions> GetQuestionsList()
        {
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Questions>(), "QuestionsXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                var query = session.QueryOver<Questions>()
                    //.Where(p => p.Id == TypeID)
                    //.Where("Name like '%我的测试'")
                    .OrderBy(p => p.UpdateDateTime).Desc
                        .List();
                return query;
            }
        }

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <returns></returns>
        public static int DeleteQuestion(Questions question)
        {
            int number = -1;
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Questions>(), "QuestionsXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    number = session.Delete("From Questions Q Where Q.Id=" + question.Id.ToString());
                    tx.Commit();
                    return number;
                }
            }
        }

        /// <summary>
        /// 查询问题列表
        /// </summary>
        /// <returns></returns>
        public static IList<Questions> GetQuestionsListByTypeID(int TypeID)
        {
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Questions>(), "QuestionsXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                var query = session.QueryOver<Questions>()
                    .Where(p => p.TypeID == TypeID)
                    //.Where("Name like '%我的测试'")
                    .OrderBy(p => p.UpdateDateTime).Desc
                        .List();
                return query;
            }
        }

        /// <summary>
        /// 查询问题列表
        /// </summary>
        /// <returns></returns>
        public static IList<Questions> GetQuestionsListByPaperID(int PaperID)
        {
            string SQL = @"Select Questions.* From Questions Inner Join PaperQuestions
On PaperQuestions.QuestionsId = Questions.Id 
Where PaperQuestions.PaperId = @PaperId
Order By Questions.TypeID ASC,Questions.Id ASC";
            DataTable tb = new DataTable();
            List<Questions> lists = new List<Questions>();
            Questions model;
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@PaperId", PaperID));
                    tb = helper.ExecuteDataTable(SQL, CommandType.Text, parameters);
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        model = new Questions();
                        model.Id = (int)tb.Rows[i]["Id"];
                        model.Title = tb.Rows[i]["Title"].ToString();
                        model.Answer1 = tb.Rows[i]["Answer1"].ToString();
                        model.Answer2 = tb.Rows[i]["Answer2"].ToString();
                        model.Answer3 = tb.Rows[i]["Answer3"].ToString();
                        model.Answer4 = tb.Rows[i]["Answer4"].ToString();
                        model.Answer5 = tb.Rows[i]["Answer5"].ToString();
                        model.Answer6 = tb.Rows[i]["Answer6"].ToString();
                        model.Answer7 = tb.Rows[i]["Answer7"].ToString();
                        model.Answer8 = tb.Rows[i]["Answer8"].ToString();
                        model.Answer9 = tb.Rows[i]["Answer9"].ToString();
                        model.Answer10 = tb.Rows[i]["Answer10"].ToString();



                        model.Status = tb.Rows[i]["Status"].ToString();
                        model.CreatDateTime = (DateTime)tb.Rows[i]["CreatDateTime"];
                        model.UpdateDateTime =(DateTime)tb.Rows[i]["UpdateDateTime"];
                        model.TypeID = (int)tb.Rows[i]["TypeID"];
                        model.CorrectAnswer = tb.Rows[i]["CorrectAnswer"].ToString();
                        model.Fraction = (double)tb.Rows[i]["Fraction"];
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
