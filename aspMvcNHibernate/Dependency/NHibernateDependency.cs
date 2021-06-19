using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

//classe de configuracao para ser chamada no startup como dependencia

namespace aspMvcNHibernate.Dependency
{
    public static class NHibernateDependency
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var Mapper = new ModelMapper();
            Mapper.AddMappings(typeof(NHibernateDependency).Assembly.ExportedTypes);
            HbmMapping entityMapping = Mapper.CompileMappingForAllExplicitlyAddedEntities();

            // definindo a configuracao com o bd

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Update;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });

            configuration.AddMapping(entityMapping);
            var sessionFactory = configuration.BuildSessionFactory();
            services.AddSingleton(sessionFactory);

            //abrindo a sessão
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}
