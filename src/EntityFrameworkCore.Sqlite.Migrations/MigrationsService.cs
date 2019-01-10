using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Sqlite.Migrations
{
    public class MigrationsService
    {
        private DbContextOptions _dbContextOptions;

        public MigrationsService(DbContextOptions dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
       
        /// <summary>
        /// Create database if it was not created and apply migrations. 
        /// </summary>
        /// <param name="migrationsListFilePath">Path to the file, that contains list of migratinons in asc order.</param>
        /// <returns></returns>
        public async Task MigrateAsync(string migrationsListFilePath, bool includeFileSource = false)
        {
            var baseDir = Path.GetDirectoryName(migrationsListFilePath);
            var list = File.ReadAllLines(migrationsListFilePath);

            var files = new List<string>();
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (!File.Exists(item))
                    {
                        var tmp = Path.Combine(baseDir, item);
                        if (!File.Exists(tmp))
                        {
                            throw new Exception($"File `{item}` not found.");
                        }
                        else
                        {
                            files.Add(tmp);
                        }
                    }
                    else
                    {
                        files.Add(item);
                    }
                }
            }

            using (var context = new MigrationsContext(_dbContextOptions))
            {
                await context.Database.EnsureCreatedAsync();
                foreach (var item in files)
                {
                    var fileName = Path.GetFileName(item);
                    var m = await context.__EFSqliteMigrationsHistory.FirstOrDefaultAsync(x => x.MigrationFileName == fileName);
                    if (m == null)
                    {
                        var sql = await File.ReadAllTextAsync(item);
                        await context.Database.ExecuteSqlCommandAsync(sql);
                        var migration = new __EFSqliteMigrationsHistory
                        {
                            MigrationFileSourceSha1 = sql.Sha1(),
                            MigrationFileName = fileName,
                            Created = DateTime.UtcNow
                        };
                        if (includeFileSource)
                        {
                            migration.MigrationFileSource = sql;

                        }
                        context.__EFSqliteMigrationsHistory.Add(migration);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
