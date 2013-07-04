//=========================================================================  
//类名:DataSetZip  
/// <summary>  
/// 当DataSet中的数据量很大时,进行网络数据传递时,速度会很慢。  
/// 本类将Dataset转化为DataSetSurrogate对象用Binary进行序列化,  
/// 然后进行压缩之后进行传输,最后进行解压缩  
/// </summary>  
/// <remarks>  
/// 将DataSet中的DataTable中的数据进行转换或复原  
/// </remarks>  
/*=========================================================================  
 变更记录  
 序号       更新日期        开发者      变更内容  
 001        2008/7/22       wiki          新建  
 =========================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ZYSoft.Comm
{
    public class DataSetZip
    {
        //消息ID  
        private const string MSG_ERR_INTERNAL = "MFWE00016";

        /// <summary>  
        /// 取得将DataSet转化为DataSetSurrogate对象用Binary进行序列化,并压缩后的二进制数组  
        /// </summary>  
        /// <param name="dsData">需压缩的DataSet数据</param>  
        /// <returns>压缩后二进制数组</returns>  
        public static byte[] GetDataSetZipBytes(DataSet dsData)
        {
            try
            {
                DataSetSurrogate dss = new DataSetSurrogate(dsData);
                BinaryFormatter ser = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                ser.Serialize(ms, dss);
                byte[] buffer = ms.ToArray();
                byte[] Zipbuffer = Compress(buffer);
                return Zipbuffer;
            }
            catch (Exception ex)
            {
                //throw new DataSetConverterException(MSG_ERR_INTERNAL, new string[] { "DataSetZip", "GetDataSetZipBytes" }, ex, null);
                throw ex;
            }
        }

        /// <summary>  
        /// 用.net自带的Gzip对二进制数组进行压缩,压缩比率可能不是太好  
        /// </summary>  
        /// <param name="data">二进制数组</param>  
        /// <returns>压缩后二进制数组</returns>  
        public static byte[] Compress(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            Stream zipStream = null;
            zipStream = new GZipStream(ms, CompressionMode.Compress, true);
            zipStream.Write(data, 0, data.Length);
            zipStream.Close();
            ms.Position = 0;
            byte[] compressed_data = new byte[ms.Length];
            ms.Read(compressed_data, 0, int.Parse(ms.Length.ToString()));
            return compressed_data;
        }

        /// <summary>  
        /// 对二进制数组进行解压缩  
        /// </summary>  
        /// <param name="data">二进制数组</param>  
        /// <returns>解压缩后的DataSet</returns>  
        public static DataSet Decompress(byte[] data)
        {
            try
            {
                byte[] buffer = null;
                MemoryStream zipMs = new MemoryStream(data);
                buffer = EtractBytesFormStream(zipMs, data.Length);
                BinaryFormatter ser = new BinaryFormatter();
                DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
                DataSet dsData = dss.ConvertToDataSet();

                return dsData;
            }
            catch (Exception ex)
            {
                //throw new DataSetConverterException(MSG_ERR_INTERNAL, new string[] { "DataSetZip", "Decompress" }, ex, null);
                throw ex;
            }
        }

        /// <summary>  
        /// 用.net自带的Gzip对数据流进行解压缩  
        /// </summary>  
        /// <param name="zipMs">数据流</param>  
        /// <param name="dataBlock">数据长度</param>  
        /// <returns>解压缩后的二进制数组</returns>  
        public static byte[] EtractBytesFormStream(MemoryStream zipMs, int dataBlock)
        {
            byte[] data = null;
            int totalBytesRead = 0;
            Stream zipStream = null;
            zipStream = new GZipStream(zipMs, CompressionMode.Decompress);
            while (true)
            {
                Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                if (bytesRead == 0)
                {
                    break;
                }
                totalBytesRead += bytesRead;
            }
            Array.Resize(ref data, totalBytesRead);
            return data;
        }  
    }
}
