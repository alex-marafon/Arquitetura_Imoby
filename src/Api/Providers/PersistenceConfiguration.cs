using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Custom_Architecture.Configuration;
using System.Data;
using MongoDB.Driver;
using static Custom_Architecture.Providers.PersistenceConfiguration;

namespace Custom_Architecture.Providers;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("QaCodersDatabase");

        services.AddDbContextPool<QaCodersDbContext>(options => options.UseSqlServer(connectionString));

        services.AddStartupTask<QaCodersDbContext>((service, cancelationToken) =>
            service.Database.MigrateAsync(cancelationToken));

        services.AddScoped<IDbConnection>(x => new SqlConnection(connectionString));


        return services;
    }




    //MongoDb
    public interface IDatabaseContext
    {
        IMongoDatabase Database { get; set; }

        IMongoCollection<T> GetCollection<T>();
    }

    public class DbMongoContext : IDatabaseContext
    {
        private MongoClient dbmsClient;
        public IMongoDatabase Database { get; set; }

        public DbMongoContext()
        {
            dbmsClient = new MongoClient(GetDatabaseConnectionString());
            Database = dbmsClient.GetDatabase("Imoby");
        }

        public string GetDatabaseConnectionString()
        {
            string response;
            var servername = Environment.GetEnvironmentVariable("SERVERNAME");
            switch (servername)
            {
                case "DEV":
                    response = "mongodb:// ........ :27012";
                    break;
                default:
                    response = "mongodb:// ........  :27012";
                    break;
            }

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"\n SERVERNAME: {servername} \n");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            return response;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(typeof(T).Name);
        }
    }
}
