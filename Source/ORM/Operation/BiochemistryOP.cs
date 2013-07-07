using System.Collections.Generic;
using ZYSoft.Comm.Entity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using ZYSoft.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using ZYSoft.Comm;

namespace ZYSoft.ORM.Operation
{
    public class BiochemistryOP
    {
        /// <summary>
        /// 保存生化数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SaveData(List<Biochemistry> list)
        {
            bool resoult = false;
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
                            int id =-1;
                            id  = (int)session.Save(list[i]);
                          
                            if (id == -1)
                            {
                                tx.Rollback();
                                break;
                            }
                        }
                        tx.Commit();
                        resoult = true;
                    }
                }
            }
            return resoult;
        }

        public static byte[] GetStatisticsData()
        {
          # region 
          string sql = @"SELECT [YearMonth],avg([{0}]) as Value
FROM [Biochemistry]
where {0}<>0
group by [YearMonth] 
order by YearMonth";
          #endregion
          string[] columns ={"HYTC","HYTG","HYHDLC","HYLDLC","HYAPOAI","HYAPOB","HYTBIL","HYDBIL",
"HYTP","HYALB","HYALT","HYAST","HYGT","HYALP","HY_GLU","HY_UREA","HY_CR","HYUA","HY_AFP","HY_CEA"};
          DataSet ds = new DataSet();
          using (SqlHelper helper = new SqlHelper())
          {
            List<SqlParameter> parameters = new List<SqlParameter>();
            //parameters.Add(new SqlParameter("@PaperId", PaperID));
            for (int i = 0; i < columns.Length; i++)
            {
              DataTable dt = helper.ExecuteDataTable(string.Format(sql,columns[i]), CommandType.Text, parameters);
              dt.TableName = columns[i];
              ds.Tables.Add(dt);
            }
            DataTable resoult = new DataTable("Statistics");
            DataColumn col = new DataColumn("YearMonth");
            resoult.Columns.Add(col);
            for (int j = 0; j < columns.Length; j++)
            {
              DataColumn cols = new DataColumn(columns[j]);
              resoult.Columns.Add(cols);
            }
            for (int k = 0; k < ds.Tables.Count; k++)
            {
              for(int kk=0;kk<ds.Tables[k].Rows.Count;kk++)
              {
                if (resoult.Select("YearMonth = " + ds.Tables[k].Rows[kk]["YearMonth"].ToString()).Length>0)
                {
                  //已有本年月=》修改
                  DataRow dr = resoult.Select("YearMonth = " + ds.Tables[k].Rows[kk]["YearMonth"].ToString())[0];
                  dr[ds.Tables[k].TableName] = ds.Tables[k].Rows[kk]["Value"].ToString();
                }
                else
                {
                  //没有本年月=》新增
                  DataRow dr=resoult.NewRow();
                  dr["YearMonth"]=ds.Tables[k].Rows[kk]["YearMonth"].ToString();
                  dr[ds.Tables[k].TableName] = ds.Tables[k].Rows[kk]["Value"].ToString();
                  resoult.Rows.Add(dr);
                }
              }
            }
            ds.Tables.Clear();
            ds.Tables.Add(resoult);
          }
          byte[] buffer = DataSetZip.GetDataSetZipBytes(ds);
          return buffer;
        }

        public static byte[] GetEmployeeInfoByID(string ID)
        {
          string sql = @"SELECT [EmployeeID]
      ,[YearMonth]
      ,[HYTC]
      ,[HYTG]
      ,[HYHDLC]
      ,[HYLDLC]
      ,[HYAPOAI]
      ,[HYAPOB]
      ,[HYTBIL]
      ,[HYDBIL]
      ,[HYTP]
      ,[HYALB]
      ,[HYALT]
      ,[HYAST]
      ,[HYGT]
      ,[HYALP]
      ,[HY_GLU]
      ,[HY_UREA]
      ,[HY_CR]
      ,[HYUA]
      ,[HY_AFP]
      ,[HY_CEA]
FROM [Biochemistry]
where [EmployeeID] = @EmployeeID
Order by YearMonth";

          DataSet ds = new DataSet();
          using (SqlHelper helper = new SqlHelper())
          {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmployeeID", ID));
            DataTable dt = helper.ExecuteDataTable(sql, CommandType.Text, parameters);
            ds.Tables.Add(dt);
            byte[] buffer = DataSetZip.GetDataSetZipBytes(ds);
            return buffer;
          }
        }
    }
}
