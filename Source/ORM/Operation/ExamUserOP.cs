using System.Collections.Generic;
using ZYSoft.Comm.Entity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
namespace ZYSoft.ORM.Operation
{
    public class ExamUserOP
    {
        /// <summary>
        /// 保存考试人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveExamUser(ExamUser model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<ExamUser>(), "ExamUserXML");
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
    }
}
