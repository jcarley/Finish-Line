using System.Data;
using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace FinishLine.Library.Repository.NHibernate
{
    public class NHibernateSessionSource : ISessionSource
    {
        private readonly DatabaseSettings _databaseSettings;
        private readonly object _locker = new object();
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        public NHibernateSessionSource(DatabaseSettings databaseSettings)
        {
            if (_sessionFactory != null)
                return;

            lock (_locker)
            {
                if (_sessionFactory != null)
                    return;

                _databaseSettings = databaseSettings;
                _configuration = AssemblyConfiguration(null);
                _sessionFactory = _configuration.BuildSessionFactory();
            }
        }

        private Configuration AssemblyConfiguration(string mappingExportPath)
        {
            var persistenceConfigurer = GetConfiguration();
            var persistenceModel = new PersistenceModel();

            var configuration = persistenceConfigurer.ConfigureProperties(new Configuration());

            //TODO:  Add event listeners
            //configuration.SetListener(ListenerType.PostUpdate, null);
            //configuration.SetListener(ListenerType.PostInsert, null);
            //configuration.SetListener(ListenerType.PostDelete, null);

            // specify the assembly that has the class mappings
            persistenceModel.AddMappingsFromAssembly(Assembly.GetExecutingAssembly());
            persistenceModel.Configure(configuration);

            // writes out the NHibernate mapping file
            if (!string.IsNullOrEmpty(mappingExportPath))
            {
                persistenceModel.WriteMappingsTo(mappingExportPath);
            }

            return configuration;
        }

        public ISession CreateSession()
        {
            return _sessionFactory.OpenSession();
        }

        public ISession CreateSession(IInterceptor interceptor)
        {
            return _sessionFactory.OpenSession(interceptor);
        }

        public void BuildSchema()
        {
            ISession session = CreateSession();

            IDbConnection connection = session.Connection;

            Dialect dialect = Dialect.GetDialect(_databaseSettings.GetProperties());

            string[] drops = _configuration.GenerateDropSchemaScript(dialect);

            ExecuteScripts(drops, connection);

            string[] scrips = _configuration.GenerateSchemaCreationScript(dialect);

            ExecuteScripts(scrips, connection);

            //var configuration = BuildConfiguration();

            //var schemaExport = new SchemaExport(configuration);
            //schemaExport.Execute(true, true, false);
        }

        private IPersistenceConfigurer GetConfiguration()
        {
            var configuration = MsSqlConfiguration.MsSql2008
                .ConnectionString(_databaseSettings.ConnectionString);
            //.Provider(_databaseSettings.Provider)
            //.Driver(_databaseSettings.Driver)
            //.Dialect(_databaseSettings.Dialect);

            //if (_databaseSettings.UseOuterJoin)
            //    configuration.UseOuterJoin();

            //if (_databaseSettings.ShowSql)
            //    configuration.ShowSql();

            return configuration;
        }

        private void ExecuteScripts(string[] scripts, IDbConnection connection)
        {
            foreach (string script in scripts)
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
    }
}
