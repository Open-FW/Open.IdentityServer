using System;
using Microsoft.EntityFrameworkCore;

public static class DatabaseProviderFactory
{
    public static DbContextOptionsBuilder UseProvider(
        this DbContextOptionsBuilder builder,
        string provider,
        string connectionString,
        string migrationAssembly)
    {
        switch(provider)
        {
            case "MSSQL":
                builder = builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationAssembly));
                break;
            case "PostgreSQL":
                builder = builder.UseNpgsql(connectionString, options => options.MigrationsAssembly(migrationAssembly));
                break;
            case "MySQL":
                builder = builder.UseMySql(connectionString, options => options.MigrationsAssembly(migrationAssembly));
                break;
            case "SQLite":
                builder = builder.UseSqlite(connectionString, options => options.MigrationsAssembly(migrationAssembly));
                break;
            default:
                throw new ArgumentException("Unknown DB provider");
        }

        return builder;
    }
}
