using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSoft.SQLDAL;
using System.Data.SqlClient;
using System.Data;

namespace ZYSoft.BLL
{
    public class UserBLL
    {
        public string GetUaerName()
        {
            string SQL = @"Select Top 1 Name From TUser";
            DataTable tb = new DataTable();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    tb = helper.ExecuteDataTable(SQL, CommandType.Text, parameters);
                    if (tb.Rows.Count > 0)
                    {
                        return tb.Rows[0][0].ToString();
                    }
                    else
                    {
                        return "没有数据！";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
