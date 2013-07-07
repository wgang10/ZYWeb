using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZYSoft.Comm;
using ZYSoft.Comm.Entity;

namespace Web
{
    /// <summary>
    /// ZiYangWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ZiYangWebService : System.Web.Services.WebService
    {   

        [WebMethod(Description = "导入生化数据")]
        public bool InportBiochemistryData(byte[] buffer)
        {
          bool resoult = false;
          if (buffer != null)
          {
            DataSet ds = DataSetZip.Decompress(buffer);
            if (ds.Tables.Count > 0)
            {
              
              List<Biochemistry> list = new List<Biochemistry>();
              for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
              {
                Biochemistry entity = new Biochemistry();                
                entity.UPDATE_DATETIME = ds.Tables[0].Rows[i]["UPDATE_DATETIME"].ToString();
                entity.EmployeeID = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                entity.YearMonth = ds.Tables[0].Rows[i]["YearMoth"].ToString();
                entity.HYNo = ds.Tables[0].Rows[i]["HYNo"].ToString();
                entity.HYTC = ConvertFloat(ds.Tables[0].Rows[i]["HYTC"].ToString());
                entity.HYTG = ConvertFloat(ds.Tables[0].Rows[i]["HYTG"].ToString());
                entity.HYHDLC = ConvertFloat(ds.Tables[0].Rows[i]["HYHDLC"].ToString());
                entity.HYLDLC = ConvertFloat(ds.Tables[0].Rows[i]["HYLDLC"].ToString());
                entity.HYAPOAI = ConvertFloat(ds.Tables[0].Rows[i]["HYAPOAI"].ToString());
                entity.HYAPOB = ConvertFloat(ds.Tables[0].Rows[i]["HYAPOB"].ToString());
                entity.HYTBIL = ConvertFloat(ds.Tables[0].Rows[i]["HYTBIL"].ToString());
                entity.HYDBIL = ConvertFloat(ds.Tables[0].Rows[i]["HYDBIL"].ToString());
                entity.HYTP = ConvertFloat(ds.Tables[0].Rows[i]["HYTP"].ToString());
                entity.HYALB = ConvertFloat(ds.Tables[0].Rows[i]["HYALB"].ToString());
                entity.HYALT = ConvertFloat(ds.Tables[0].Rows[i]["HYALT"].ToString());
                entity.HYAST = ConvertFloat(ds.Tables[0].Rows[i]["HYAST"].ToString());
                entity.HYGT = ConvertFloat(ds.Tables[0].Rows[i]["HYGT"].ToString());
                entity.HYALP = ConvertFloat(ds.Tables[0].Rows[i]["HYALP"].ToString());
                entity.HYHBsAg = ds.Tables[0].Rows[i]["HYHBsAg"].ToString();
                entity.HYHBsAb = ds.Tables[0].Rows[i]["HYHBsAb"].ToString();
                entity.HYHBeAg = ds.Tables[0].Rows[i]["HYHBeAg"].ToString();
                entity.HYHBeAb = ds.Tables[0].Rows[i]["HYHBeAb"].ToString();
                entity.HYHBcAb = ds.Tables[0].Rows[i]["HYHBcAb"].ToString();
                entity.HY_GLU = ConvertFloat(ds.Tables[0].Rows[i]["HY_GLU"].ToString());
                entity.HY_UREA = ConvertFloat(ds.Tables[0].Rows[i]["HY_UREA"].ToString());
                entity.HY_CR = ConvertFloat(ds.Tables[0].Rows[i]["HY_CR"].ToString());
                entity.HYUA = ConvertFloat(ds.Tables[0].Rows[i]["HYUA"].ToString());
                entity.HY_AFP = ConvertFloat(ds.Tables[0].Rows[i]["HY_AFP"].ToString());
                entity.HY_CEA = ConvertFloat(ds.Tables[0].Rows[i]["HY_CEA"].ToString());
                list.Add(entity);
              }
              resoult = ZYSoft.ORM.Operation.BiochemistryOP.SaveData(list);
            }
          }
          return resoult;
        }

        private float ConvertFloat(string str)
        {
          float r= 0;
          float.TryParse(str,out r);
          return r;
        }

        [WebMethod(Description = "取得统计数据")]
        public byte[] GetStatisticsData()
        {
          return ZYSoft.ORM.Operation.BiochemistryOP.GetStatisticsData();
        }

        [WebMethod(Description = "取得单个人的统计数据")]
        public byte[] GetStatisticsDataByID(string ID)
        {
          return ZYSoft.ORM.Operation.BiochemistryOP.GetEmployeeInfoByID(ID);
        }
    }
}
