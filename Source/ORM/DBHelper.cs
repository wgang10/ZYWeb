using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Dialect;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

using NHibernate.ByteCode.LinFu;
using ConfOrm;
using ConfOrm.NH;

namespace ZYSoft.ORM
{
    public static class DBHelper
    {
        private const string _ConnectionString =@"server=.\WG_SQL2005;database=NHibernate;uid=sa;pwd=wangang10";

        public static Configuration GetConfiguration()
        {
            var configure = new Configuration();
            configure.SessionFactoryName("NHibernateDB");

            //Proxy扩展方法用于配置NHibernate延迟加载的字节码提供程序
            configure.Proxy(p => p.ProxyFactoryFactory<ProxyFactoryFactory>());

            configure.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2005Dialect>();
                db.Driver<SqlClientDriver>();
                db.ConnectionString = _ConnectionString;
            });

            return configure;

        }

        //public static HbmMapping GetMapping()
        //{
        //    var orm = new ObjectRelationalMapper();
        //    orm.TablePerClass<BlogTag>();
        //    //orm.Poid<BlogTag>();
        //    var mapper = new Mapper(orm);
        //    return mapper.CompileMappingFor(new[] { typeof(BlogTag) });
        //}

        //public static void Test()
        //{
        //    //配置NHibernate
        //    var conf = GetConfiguration();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "BlogTag");
        //    //配置数据库架构元数据
        //    SchemaMetadataUpdater.QuoteTableAndColumns(conf);
        //    //创建数据库架构
        //    new SchemaExport(conf).Create(false, true);
        //    //建立SessionFactory
        //    var factory = conf.BuildSessionFactory();
        //    //打开Session做持久化数据
        //    using (var s = factory.OpenSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            var blogTag = new BlogTag { TagName = "我的测试" };
        //            s.Save(blogTag);
        //            tx.Commit();
        //        }
        //    }

        //    //查询、排序
        //    using (var s = factory.OpenSession())
        //    {
        //        var query = s.QueryOver<BlogTag>()
        //        .Where(p => p.TagName == "我的测试")
        //        .OrderBy(p => p.Tag_ID).Asc
        //        .List();
        //    }

        //    //打开Session做删除数据
        //    using (var s = factory.OpenSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            s.CreateQuery("delete from BlogTag").ExecuteUpdate();
        //            tx.Commit();
        //        }
        //    }
        //    //删除数据库架构
        //    new SchemaExport(conf).Drop(false, true);

        //}
    }
}
