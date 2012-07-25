using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QQOauthWeb.Code.Model
{
    [Serializable]
    [DataStore(Name = "QzoneOauth")]
    public class QzoneOauth 
    {
        [DataStore(Name = "ID", PrimaryKey = true)]
        public Guid ID { get; set; }
         [DataStore(Name = "OpenId", PrimaryKey = true)]
        public string OpenId { get; set; }
         [DataStore(Name = "AccessTokenKey", PrimaryKey = true)]
        public string AccessTokenKey { get; set; }
         [DataStore(Name = "AccessTokenSecret", PrimaryKey = true)]
        public string AccessTokenSecret { get; set; }
         [DataStore(Name = "UserId", PrimaryKey = true)]
        public Guid UserId { get; set; }
       
    }
}