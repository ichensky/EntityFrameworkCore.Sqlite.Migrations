using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFrameworkCore.Sqlite.Migrations
{
    public class __EFSqliteMigrationsHistory
    {
        [Key]
        public string MigrationFileSourceSha1 { get; set; }
        public string MigrationFileName { get; set; }
        public string MigrationFileSource { get; set; }
        public DateTime Created { get; set; }
    }
}
