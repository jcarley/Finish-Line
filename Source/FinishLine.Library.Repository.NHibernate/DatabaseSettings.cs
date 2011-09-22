using System;
using System.Collections.Generic;

namespace FinishLine.Library.Repository.NHibernate
{
    public class DatabaseSettings
    {
        public string Provider { get; set; }

        public string Driver { get; set; }

        public string Dialect { get; set; }

        public bool UseOuterJoin { get; set; }

        public string ConnectionString { get; set; }

        public bool ShowSql { get; set; }

        public string ProxyFactory { get; set; }

        public IDictionary<string, string> GetProperties()
        {
            if (string.IsNullOrEmpty(Provider))
                throw new ApplicationException("DatabaseSettings unavailable. Make sure your application configuration file has appSetting entries for the necessary DatabaseSettings properties.");

            var properties = new Dictionary<string, string>
                {
                    {"connection.provider", Provider},
                    {"connection.driver_class", Driver},
                    {"dialect", Dialect},
                    {"use_outer_join", UseOuterJoin.ToString()},
                    {"connection.connection_string", ConnectionString},
                    {"show_sql", ShowSql.ToString()},
                    {"proxyfactory.factory_class", ProxyFactory}
                };

            return properties;
        }
    }
}
