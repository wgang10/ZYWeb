using System.Collections.Generic;
using ZYSoft.Comm.Entity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ZYSoft.ORM.Operation
{
    public class MemberOP
    {
        public static IList<Member> GetMemberByOpenID(string OponID)
        {
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Member>(), "MemberXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                var query = session.QueryOver<Member>()
                    .Where(p => p.OpenId == OponID)
                    //.Where("Name like '%我的测试'")
                    .OrderBy(p => p.CreatTime).Desc
                        .List();
                return query;
            }
        }

        public static IList<Member> GetMemberByEmail(string Email)
        {
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Member>(), "MemberXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                var query = session.QueryOver<Member>()
                    .Where(p => p.Email == Email)
                    //.Where("Name like '%我的测试'")
                    .OrderBy(p => p.CreatTime).Desc
                        .List();
                return query;
            }
        }

        public static IList<HistoryOfMemberUpdate> GetHistoryOfMemberUpdateByOpenID(string OponID)
        {
            ISession session = NHibernateHelper.GetSession();
            //配置NHibernate
            var conf = new Configuration().Configure();
            //在Configuration中添加HbmMapping
            conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<HistoryOfMemberUpdate>(), "HistoryOfMemberUpdateXML");
            //配置数据库架构元数据
            SchemaMetadataUpdater.QuoteTableAndColumns(conf);

            //建立SessionFactory
            var factory = conf.BuildSessionFactory();
            //打开Session做持久化数据
            using (session = factory.OpenSession())
            {
                var query = session.QueryOver<HistoryOfMemberUpdate>()
                    .Where(p => p.OpenId == OponID)
                    //.Where("Name like '%我的测试'")
                    .OrderBy(p => p.CreatTime).Desc
                        .List();
                return query;
            }
        }

        /// <summary>
        /// 保存会员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveMember(Member model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Member>(), "MemberXML");
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
        /// 更新会员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateMember(Member model)
        {
            bool isSuccess = false;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<Member>(), "MemberXML");
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
                        isSuccess = true;
                    }
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// 保存会员历史信息(在每次创建会员及更新会员信息时产生一条历史信息)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SaveHistoryOfMemberUpdate(HistoryOfMemberUpdate model)
        {
            int id = -1;
            if (model != null)
            {
                var conf = new Configuration().Configure();
                ISession session = NHibernateHelper.GetSession();
                //配置NHibernate
                //在Configuration中添加HbmMapping
                conf.AddDeserializedMapping(NHibernateHelper.GetEntityMapping<HistoryOfMemberUpdate>(), "HistoryOfMemberUpdateXML");
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
