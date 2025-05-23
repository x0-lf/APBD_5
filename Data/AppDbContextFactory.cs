using APBD_5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace APBD_5.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private static ILoggerFactory? _loggerFactory;

    public AppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var enableDebugSql = args.Contains("--debug-sql");

        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        // optionsBuilder.UseSqlServer(connectionString);

        if (enableDebugSql)
        {
            _loggerFactory ??= LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information)
                    .AddConsole()
                    .AddFile("efcore-sql.log");
                /*
                 * Serilog.Extensions.Logging.File + Microsoft.Extensions.Logging.Console
                 * Both Terminal & File Logging
                 * To be honest it would be hard to look for N+1 Queries in this task because there are
                 * only 2 endpoints:
                 *    - Add new Prescription
                 *    - Get Patient Data
                 */
            });

            optionsBuilder
                .UseSqlServer(connectionString, opt =>
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        return new AppDbContext(optionsBuilder.Options);
    }
}