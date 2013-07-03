using System.Collections.Generic;
using ZYSoft.Comm.Entity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ZYSoft.ORM.Operation
{
    public class BiochemistryOP
    {
        /// <summary>
        /// 保存生化数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveAdminUser(List<Biochemistry> list)
        {
            int id = -1;
            if (list != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Biochemistry>(), "BiochemistryXML");
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
                            id = (int)session.Save(list[i]);
                            if (id == -1)
                            {
                                tx.Rollback();
                                break;
                            }
                        }
                        tx.Commit();
                    }
                }
            }
            return id;
        }
    }
}
