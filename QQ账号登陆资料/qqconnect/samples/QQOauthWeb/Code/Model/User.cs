using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QQOauthWeb.Code.Model
{
     [DataStore(Name = "Users")]
    public class User
    {
          [DataStore(Name = "ID", PrimaryKey = true)]
         public Guid ID { get; set; }
         [DataStore(Name = "Login")]
        public string Login { get; set; }
         [DataStore(Name = "Password")]
        public string Password { get; set; }

    }
}