using System;
using System.Data;
using System.Linq;
using FinishLine.Library.Repository.NHibernate.ClassMaps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

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
            var mapper = GetModelMapper();

            var cfg = new Configuration();

            cfg.DataBaseIntegration(c =>
                {
                    c.ConnectionString = _databaseSettings.ConnectionString;

                    c.Driver<SqlClientDriver>();
                    c.Dialect<MsSql2008Dialect>();

                    c.LogSqlInConsole = true;
                    c.LogFormattedSql = true;

                    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    c.SchemaAction = SchemaAutoAction.Create;
                });

            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            //TODO:  Add event listeners
            //cfg.SetListener(ListenerType.PostUpdate, null);
            //cfg.SetListener(ListenerType.PostInsert, null);
            //cfg.SetListener(ListenerType.PostDelete, null);

            return cfg;
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


        private ModelMapper GetModelMapper()
        {
            var mapper = new ModelMapper();

            var classMaps = typeof(NHibernateSessionSource).Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IClassMap).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .OfType<IClassMap>();

            foreach (var map in classMaps)
            {
                map.Map(mapper);
            }

            return mapper;
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
