using SQLite;
using HybridTrackerApp.Models;

namespace HybridTrackerApp.Services
{
    public class DatabaseService
    {
        string _dbPath;
        private SQLiteAsyncConnection conn;

        private async Task InitAsync()
        {
            if (conn != null)
            {
                return;

            } else
            {
                conn = new SQLiteAsyncConnection(_dbPath);
                await conn.CreateTableAsync<AttendanceRecord>();
            }
            
        }

        public DatabaseService(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<AttendanceRecord>> GetAttendanceAsync(DateTime from, DateTime to)
        {
            await InitAsync();
            var f = from.Date;
            var t = to.Date;
            return await conn.Table<AttendanceRecord>()
                .Where(rec => rec.Date >= f && rec.Date <= t)
                .OrderBy(rec => rec.Date)
                .ToListAsync();
        }

        public async Task<AttendanceRecord> GetTodayAttendanceAsync()
        {
            await InitAsync();
            var today = DateTime.Today;
            return await conn.Table<AttendanceRecord>()
                .Where(rec => rec.Date == today)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveAttendanceAsync(AttendanceRecord record)
        {
            await InitAsync();
            record.Date = record.Date.Date; // Ensure time component is zeroed out
            return await conn.InsertOrReplaceAsync(record);
        }

        public async Task<int> DeleteAttendanceAsync(DateTime date)
        {
            await InitAsync();
            return await conn.DeleteAsync<AttendanceRecord>(date);
        }

        public async Task<int> DeleteAllAttendanceAsync()
        {
            await InitAsync();
            return await conn.ExecuteAsync("DELETE FROM AttendanceRecord");
        }

        public async Task<int> CountDaysInMonthAsync(int year, int month)
        {
            await InitAsync();
            var firstDay = new DateTime(year, month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            return await conn.Table<AttendanceRecord>()
                .Where(rec => rec.Date >= firstDay && rec.Date <= lastDay)
                .CountAsync();
        }
    }
}
