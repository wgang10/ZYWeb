
namespace QQOauthWeb.Code
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    internal sealed class DataStoreAttribute : Attribute
    {
        public string Name { get; set; }
        public bool PrimaryKey { get; set; }

        public DataStoreAttribute()
        {
            this.PrimaryKey = false;
        }
    }
}
