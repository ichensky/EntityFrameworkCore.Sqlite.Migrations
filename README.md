This lib provide ability to create migrations for Sqlite database with EF Core 3.0

# Problem 

 - You want update Sqlite database schema with migrations.
 - You want write migrations for database with SQL. 
 - You use EF Core.

# Solution

 - Put migrations with SQL scripts in the separate files.
 - List all this migrations files in the `migrations.txt` file in asc order.
 - Apply mirgations from `migrations.txt` to database.
 
### Example of directory with migrations
```
.
├── migrations
│   ├── add table `Cat`.sql
│   ├── add table `User`.sql
│   └── init db.sql
└── migrations.txt
```

### Example of `migrations.txt`
```
migrations/init db.sql
migrations/add table `User`.sql
migrations/add table `Cat`.sql
```

# How to use lib
```csharp
// Path to the database file
var dbPath = @"..\..\..\..\..\data\test.db";
            
// Path to the file that contains list of files with migrations
var migrationsPath = @"..\..\..\..\..\db\migrations.txt";

var builder = new DbContextOptionsBuilder() { };
builder.UseSqlite($"Data Source={dbPath}");

var migrationService = new MigrationsService(builder.Options);
await migrationService.MigrateAsync(migrationsPath);
```

### Interesting to know
`migrationService.MigrateAsync` method will 
 - create database if it is not exists 
 - create `__EFSqliteMigrationsHistory` table in the database, that contains rows:
   - `MigrationFileSourceSha1` for storing sha1 hash of migration file source
   - `MigrationFileName` migration file name, it can be not uniq, so you can apply one migration several times
   - `MigrationFileSource` - it can include source of migration
   - `Created` - date time of applying migration

# Example of database structure
![Example of database structure](/doc/example_of_db.structure.png)

# Example of migrations table
![Example of migrations table](/doc/example_of_migrations_table.png)

# Example of migrations table with source of migrations
![Example of migrations table](/doc/example_of_migrations_table_with_source.png)
