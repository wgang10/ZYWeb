
namespace QQOauthWeb.Code
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;

    internal static class Extensions
    {        
        public static T FirstOrNull<T>(this IEnumerable<T> collection, Func<T, bool> predicate = null) where T : class
        {
            if (predicate == null)
            {
                if (collection.Any())
                {
                    return collection.First();
                }

                return null;
            }

            if (collection.Any(predicate))
            {
                return collection.First(predicate);
            }

            return null;
        }  
      
        public static string Hash(this string input)
        {
            using (HashAlgorithm hash = new SHA512Managed())
            {
                byte[] tempHash = hash.ComputeHash(UnicodeEncoding.Unicode.GetBytes(input));
                StringBuilder hashStringBuilder = new StringBuilder(tempHash.Length);
                foreach (var item in tempHash)
                {
                    hashStringBuilder.Append(item.ToString("X2"));
                }

                return hashStringBuilder.ToString();
            }
        }
    }
}
