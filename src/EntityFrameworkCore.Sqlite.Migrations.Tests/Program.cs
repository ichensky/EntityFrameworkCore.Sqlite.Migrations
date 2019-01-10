using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Sqlite.Migrations.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Path to the database file
            var dbPath = @"..\..\..\..\..\data\test.db";
            
            // Path to the file that contains list of files with migrations
            var migrationsPath = @"..\..\..\..\..\db\migrations.txt";

            var builder = new DbContextOptionsBuilder() { };
            builder.UseSqlite($"Data Source={dbPath}");

            var migrationService = new MigrationsService(builder.Options);
            await migrationService.MigrateAsync(migrationsPath);
        }
    }
}