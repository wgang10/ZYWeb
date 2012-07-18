
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ConfOrm;
using ConfOrm.NH;
using NHibernate.ByteCode.LinFu;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

namespace ZYSoft.ORM
{
    public static class NhConfig
    {
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS05;Initial Catalog=NHibernate;Integrated Security=True;Pooling=False";
        public static Configuration ConfigureNHibernate()
        {
            var configure = new Configuration();
            configure.SessionFactoryName("Demo");
            configure.Proxy(p => p.ProxyFactoryFactory<ProxyFactoryFactory>());
            configure.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2008Dialect>();
                db.Driver<SqlClientDriver>();
                db.ConnectionString = ConnectionString;
                db.LogSqlInConsole = true;//用于显示输出SQL
            });
            return configure;
        }

        public static string Serialize(HbmMapping hbmElement)
        {
            var setting = new XmlWriterSettings { Indent = true };
            var serializer = new XmlSerializer(typeof(HbmMapping));
            using (var memStream = new MemoryStream(2048))
            using (var xmlWriter = XmlWriter.Create(memStream, setting))
            {
                serializer.Serialize(xmlWriter, hbmElement);
                memStream.Flush();
                memStream.Position = 0;
                var sr = new StreamReader(memStream);
                return sr.ReadToEnd();
            }
        }

        
        //ConfORM配置
        //这一步非常重要，是ConfORM的核心所在，
        //实例化一个ObjectRelationalMapper对象，装配Domain对象，实例化Mapper对象，
        //调用Mapper对象的CompileMappingFor()方法自动生成HbmMapping。
        //public static HbmMapping GetMapping()
        //{
        //    ObjectRelationalMapper orm = new ObjectRelationalMapper();
        //    orm.TablePerClass<Domain>();
        //    Mapper mapper = new Mapper(orm);
            
        //    return mapper.CompileMappingFor(new[] { typeof(Domain) });
        //}

        
        //public static void JustForConfOrm()
        //{
        //    //配置NHibernate
        //    var conf = NhConfig.ConfigureNHibernate();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "Domain");
        //    //配置元数据
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
        //            var domain = new Domain { Name = "我的测试" };
        //            s.Save(domain);
        //            tx.Commit();
        //        }
        //    }
        //    //打开Session做删除数据
        //    using (var s = factory.OpenSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            s.CreateQuery("delete from Domain").ExecuteUpdate();
        //            tx.Commit();
        //        }
        //    }
        //    //删除数据库架构
        //    new SchemaExport(conf).Drop(false, true);
        //}

        //public static void ShowXmlMapping()
        //{
        //    var document = NhConfig.Serialize(GetMapping());
        //    File.WriteAllText("MyMapping.hbm.xml", document);
        //    Console.Write(document);
        //}

        //public static void CreatTable()
        //{
        //    //配置NHibernate
        //    var conf = NhConfig.ConfigureNHibernate();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "Domain");
        //    //配置元数据
        //    SchemaMetadataUpdater.QuoteTableAndColumns(conf);
        //    //创建数据库架构
        //    new SchemaExport(conf).Create(true, true);
        //}

        //public static void DropTable()
        //{
        //    //配置NHibernate
        //    var conf = NhConfig.ConfigureNHibernate();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "Domain");
        //    //配置元数据
        //    SchemaMetadataUpdater.QuoteTableAndColumns(conf);
        //    //删除数据库架构
        //    new SchemaExport(conf).Drop(true, true);
        //}

        //public static void Insert()
        //{
        //    //配置NHibernate
        //    var conf = NhConfig.ConfigureNHibernate();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "Domain");
        //    //配置元数据
        //    SchemaMetadataUpdater.QuoteTableAndColumns(conf);
        //    //建立SessionFactory
        //    var factory = conf.BuildSessionFactory();
        //    //打开Session做持久化数据
        //    using (var s = factory.OpenSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            var domain = new Domain { Name = "我的测试" };
        //            s.Save(domain);
        //            tx.Commit();
        //        }
        //    }
        //}

        //public static void Update()
        //{
        //    //配置NHibernate
        //    var conf = NhConfig.ConfigureNHibernate();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "Domain");
        //    //配置元数据
        //    SchemaMetadataUpdater.QuoteTableAndColumns(conf);
        //    //建立SessionFactory
        //    var factory = conf.BuildSessionFactory();
        //    //打开Session做持久化数据
        //    using (var s = factory.OpenSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            var domain = new Domain {Id=32768,Name = "我的测试"+DateTime.Now.ToString() };
        //            //s.Update(domain,1);
        //            s.SaveOrUpdate(domain);
        //            tx.Commit();
        //        }
        //    }
        //}

        //public static void Delete()
        //{
        //    //配置NHibernate
        //    var conf = NhConfig.ConfigureNHibernate();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "Domain");
        //    //配置元数据
        //    SchemaMetadataUpdater.QuoteTableAndColumns(conf);
        //    //建立SessionFactory
        //    var factory = conf.BuildSessionFactory();
        //    //打开Session做持久化数据
        //    //打开Session做删除数据
        //    using (var s = factory.OpenSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            s.CreateQuery("delete from Domain").ExecuteUpdate();
        //            //var domain = new Domain { Id = 1, Name = "我的测试" + DateTime.Now.ToString() };
        //            //s.Delete(domain);
        //            tx.Commit();
        //        }
        //    }
        //}

        //public static IList<Domain> GetList()
        //{
        //    //配置NHibernate
        //    var conf = NhConfig.ConfigureNHibernate();
        //    //在Configuration中添加HbmMapping
        //    conf.AddDeserializedMapping(GetMapping(), "Domain");
        //    //配置元数据
        //    SchemaMetadataUpdater.QuoteTableAndColumns(conf);
        //    //建立SessionFactory
        //    var factory = conf.BuildSessionFactory();
        //    //查询、排序
        //    using (var s = factory.OpenSession())
        //    {
        //        var query = s.QueryOver<Domain>()
        //            //.Where(p => p.Name == "我的测试")
        //            //.Where("Name like '%我的测试'")
        //            .OrderBy(p => p.Id).Asc
        //            .List();
        //        return query;
        //    }
        //}

        //[Test]
        //public static void UnidirectionalOneToOneMappingDemo()
        //{
        //    //show how work with one-to-one and how ConfORM understands OOP
        //    var orm = new ObjectRelationalMapper();
        //    var mapper = new Mapper(orm);
        //    var entities = new[] { typeof(Person), typeof(Address) };
        //    //use the definition of table-to-class strategy class by class
        //    orm.TablePerClass(entities);
        //    // Defining relations
        //    orm.OneToOne<Person, Address>();
        //    // Show the mapping to the console
        //    var mapping = mapper.CompileMappingFor(entities);
        //    Console.Write(mapping.AsString());
        //}
    }
}
