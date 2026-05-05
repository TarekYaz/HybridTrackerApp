using SQLite;
using HybridTrackerApp.Models;

namespace HybridTrackerApp.Services
{
    public class DatabaseService
    {
        string _dbPath;
        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
                return;
            
            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<AttendanceRecord>();
        }

        public DatabaseService(string dbPath)
        {
            _dbPath = dbPath;
        }
    }
}
