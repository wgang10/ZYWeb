
namespace QQOauthWeb.Code
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SQLite;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using QQOauthWeb.Code.Model;    

    internal class Database
    {
        private static Database instance = new Database();
        private Lazy<List<User>> users = new Lazy<List<User>>(LoadUsers, LazyThreadSafetyMode.ExecutionAndPublication);
        private Lazy<List<QzoneOauth>> qzoneOauths = new Lazy<List<QzoneOauth>>(LoadQzoneOauths, LazyThreadSafetyMode.ExecutionAndPublication);
     
        public static string ConnectionString { get; set; }
        
        private Database() { }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "SQLite DB does not do a dispose when close is called, like other DB's and so this is a red herring.")]
        private static void DBAction(Action<SQLiteConnection> action)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    action(connection);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private static void SetList<T>(Lazy<List<T>> list)
        {
            if (!list.IsValueCreated)
            {
                list.Value.Any();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "It is done using SQL parameters")]
        private static List<T> Load<T>() where T : class, new()
        {
            using (DataSet dataSet = new DataSet())
            {
                dataSet.Locale = CultureInfo.CurrentCulture;

                DBAction(connection =>
                    {
                        string selectCommand = "SELECT ";
                        GetAttributedProperties(typeof(T), (property, attribute) =>
                        {
                            selectCommand += string.Format(CultureInfo.CurrentCulture, "[{0}], ", attribute.Name);
                        });

                        selectCommand = selectCommand.Remove(selectCommand.Length - 2);

                        selectCommand += string.Format(CultureInfo.CurrentCulture, " FROM [{0}]", ((DataStoreAttribute)typeof(T).GetCustomAttributes(typeof(DataStoreAttribute), false)[0]).Name);

                        using (SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(selectCommand, connection))
                        {
                            sqlDataAdapter.Fill(dataSet);
                        }
                    });

                List<T> items = new List<T>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    T newItem = ConvertFromDataRow<T>(row);
                    items.Add(newItem);
                }

                return items;
            }
        }

        private static void GetAttributedProperties(Type type, Action<PropertyInfo, DataStoreAttribute> action)
        {
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                DataStoreAttribute attribute = (DataStoreAttribute)property.GetCustomAttributes(typeof(DataStoreAttribute), false).FirstOrNull();
                if (attribute != null)
                {
                    action(property, attribute);
                }
            }
        }

        private static T ConvertFromDataRow<T>(DataRow row) where T : class, new()
        {
            T item = new T();

            GetAttributedProperties(typeof(T), (property, attribute) =>
                {
                    if (property.PropertyType == typeof(Uri))
                    {
                        property.SetValue(item, new Uri((string)row[attribute.Name]), null);
                    }
                    else
                    {
                        if (row.IsNull(attribute.Name))
                        {
                            property.SetValue(item, null, null);
                        }
                        else
                        {
                            property.SetValue(item, row[attribute.Name], null);
                        }
                    }
                });

            return item;
        }

        private static List<User> LoadUsers()
        {
            return Load<User>();
        }

        private static List<QzoneOauth> LoadQzoneOauths()
        {
            return Load<QzoneOauth>();
        }

       

        public static Database Instance
        {
            get
            {
                return instance;
            }
        }

        public ReadOnlyCollection<User> Users
        {
            get
            {
                return this.users.Value.AsReadOnly();
            }
        }

        public ReadOnlyCollection<QzoneOauth> QzoneOauth
        {
            get
            {
                return this.qzoneOauths.Value.AsReadOnly();
            }
        }

        

        public void InsertUser(User user)
        {
            SetList(this.users);
            Insert(user);
            this.users.Value.Add(user);
        }

        public void InsertQzoneOauth(QzoneOauth member)
        {
            SetList(this.qzoneOauths);
            Insert(member);
            this.qzoneOauths.Value.Add(member);
        }

      

        private static void ExecuteCommand(SQLiteCommand command)
        {
            DBAction(connection =>
            {
                command.Connection = connection;
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            });
        }

        private static void Insert<T>(T newItem)
        {
            using (SQLiteCommand command = ConvertToInsertCommand(newItem))
            {
                ExecuteCommand(command);    
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "It is done with SQL parameters")]
        private static SQLiteCommand ConvertToInsertCommand<T>(T newItem)
        {
            SQLiteCommand command = new SQLiteCommand();
            string insertCommandText = string.Format(CultureInfo.CurrentCulture, "INSERT INTO [{0}] (", ((DataStoreAttribute)typeof(T).GetCustomAttributes(typeof(DataStoreAttribute), false)[0]).Name);
            string valuesCommandText = string.Empty;
            int parameterCounter = 0;

            GetAttributedProperties(typeof(T), (property, attribute) =>
            {
                insertCommandText += string.Format(CultureInfo.CurrentCulture, "[{0}], ", attribute.Name);
                valuesCommandText += string.Format(CultureInfo.CurrentCulture, "@A{0}, ", parameterCounter);
                command.Parameters.AddWithValue(string.Format(CultureInfo.CurrentCulture, "A{0}", parameterCounter), property.GetValue(newItem, null));
                parameterCounter++;
            });

            insertCommandText = insertCommandText.Remove(insertCommandText.Length - 2);
            valuesCommandText = valuesCommandText.Remove(valuesCommandText.Length - 2);

            insertCommandText += string.Format(CultureInfo.CurrentCulture, ") VALUES ({0})", valuesCommandText);

            command.CommandText = insertCommandText;

            return command;
        }

        public static void Update<T>(T item)
        {
            using (SQLiteCommand command = ConvertToUpdateCommand(item))
            {
                ExecuteCommand(command);
            }
        }        

        public static void Delete<T>(T item)
        {
            using (SQLiteCommand command = ConvertToDeleteCommand(item))
            {
                ExecuteCommand(command);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "It is done with SQL parameters")]
        private static SQLiteCommand ConvertToDeleteCommand<T>(T item)
        {
            SQLiteCommand command = new SQLiteCommand();
            string deleteCommandText = string.Format(CultureInfo.CurrentCulture, "DELETE FROM [{0}] ", ((DataStoreAttribute)typeof(T).GetCustomAttributes(typeof(DataStoreAttribute), false)[0]).Name);

            object PKValue = null;
            string PKColumn = string.Empty;

            GetAttributedProperties(typeof(T), (property, attribute) =>
            {
                if (attribute.PrimaryKey)
                {
                    PKValue = property.GetValue(item, null);
                    PKColumn = attribute.Name;
                }
            });

            deleteCommandText += string.Format(CultureInfo.CurrentCulture, " WHERE [{0}]=@PK", PKColumn);
            command.Parameters.AddWithValue("PK", PKValue);
            command.CommandText = deleteCommandText;

            return command;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "It is done with SQL parameters")]
        private static SQLiteCommand ConvertToUpdateCommand<T>(T item)
        {
            SQLiteCommand command = new SQLiteCommand();
            string insertCommandText = string.Format(CultureInfo.CurrentCulture, "UPDATE [{0}] SET ", ((DataStoreAttribute)typeof(T).GetCustomAttributes(typeof(DataStoreAttribute), false)[0]).Name);
            int parameterCounter = 0;

            object PKValue = null;
            string PKColumn = string.Empty;

            GetAttributedProperties(typeof(T), (property, attribute) =>
            {
                if (!attribute.PrimaryKey)
                {
                    insertCommandText += string.Format(CultureInfo.CurrentCulture, "[{0}]=@A{1}, ", attribute.Name, parameterCounter);
                    command.Parameters.AddWithValue(string.Format(CultureInfo.CurrentCulture, "A{0}", parameterCounter), property.GetValue(item, null));
                    parameterCounter++;
                }
                else
                {
                    PKValue = property.GetValue(item, null);
                    PKColumn = attribute.Name;
                }
            });

            insertCommandText = insertCommandText.Remove(insertCommandText.Length - 2);
            insertCommandText += string.Format(CultureInfo.CurrentCulture, " WHERE [{0}]=@PK", PKColumn);
            command.Parameters.AddWithValue("PK", PKValue);
            command.CommandText = insertCommandText;

            return command;
        }

    }
}
