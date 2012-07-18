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
    public class ExamPaperOP
    {
        /// <summary>
        /// 保存试卷
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveExamPaper(ExamPaper model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<ExamPaper>(), "ExamPaperXML");
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
        /// 查询试卷列表
        /// </summary>
        /// <returns></returns>
        public static IList<ExamPaper> GetExamPapersList()
        {
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<ExamPaper>(), "ExamPaperXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                var query = session.QueryOver<ExamPaper>()
                    //.Where(p => p.Id == TypeID)
                    //.Where("Name like '%我的测试'")
                    .OrderBy(p => p.Id).Asc
                        .List();
                return query;
            }
        }

        /// <summary>
        /// 保存试卷试题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SavePaperQuestions(PaperQuestions model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<PaperQuestions>(), "PaperQuestionsXML");
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
        /// 根据试卷ID和试题ID删除试卷试题
        /// </summary>
        /// <param name="PaperId"></param>
        /// <param name="QuestionsId"></param>
        /// <returns></returns>
        public static int DeletePaperQuestions(int PaperId, int QuestionsId)
        {
            int number = -1;
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<PaperQuestions>(), "PaperQuestionsXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    //var query = session.QueryOver<PaperQuestions>()
                    //.Where(p => p.PaperId == PaperId)
                    //.Where(p => p.QuestionsId == QuestionsId)
                    ////.Where("Name like '%我的测试'")
                    ////.OrderBy(p => p.TypeID).Asc
                    //.List();
                    number = session.Delete ("from PaperQuestions p Where p.PaperId=" + PaperId.ToString() + " and p.QuestionsId = " + QuestionsId.ToString());
                    //number = session.CreateQuery("delete from PaperQuestions Where PaperId=" + PaperId.ToString() + " and QuestionsId = " + QuestionsId.ToString()).ExecuteUpdate();
                    //var domain = new Domain { Id = 1, Name = "我的测试" + DateTime.Now.ToString() };
                    //s.Delete(domain);
                    tx.Commit();
                    return number;
                }
            }
        }

        /// <summary>
        /// 更具试题ID获取试题列表
        /// </summary>
        /// <param name="QuestionsId"></param>
        /// <returns></returns>
        public static IList<string> GetPaperNameByQuestionID(int QuestionsId)
        {
            string SQL = @"Select distinct P.Name from ExamPaper P Inner Join
PaperQuestions PQ On PQ.PaperId = P.ID
where PQ.QuestionsId = @QuestionsId";
            DataTable tb = new DataTable();
            List<string> lists = new List<string>();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@QuestionsId", QuestionsId));
                    tb = helper.ExecuteDataTable(SQL, CommandType.Text, parameters);
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        lists.Add(tb.Rows[i]["Name"].ToString());
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
