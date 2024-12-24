using DapperRelations.Application.Context.Contract;
using System.Data;
using System.Data.SQLite;


namespace DapperRelations.Application.Context.Database
{
    internal class DatabaseContext : IDatabaseContext
    {
        private readonly string _connectionString;
        private string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        public DatabaseContext()
        {
           //Temp
            _connectionString = "Data Source=" + Directory.GetParent(baseDir).Parent.Parent.Parent.Parent.FullName + @"\Database\AppDatabase.db; New=True;Compress=True";

           //Prod
           //_connectionString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + @"\Database\AppDatabase.db; New=True;Compress=True";

        }

        public IDbConnection CreateConnection() => new SQLiteConnection(_connectionString);

    }

}
