using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.Sqlite.Migrations
{
    public class MigrationsContext : DbContext
    {
        public DbSet<__EFSqliteMigrationsHistory> __EFSqliteMigrationsHistory { get; set; }
        public MigrationsContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
        }
    }
}
