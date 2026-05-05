using SQLite;
using HybridTrackerApp.Models;
using Plugin.Maui.Calendar.Models;

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
            }
            else
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

        public async Task<AttendanceRecord?> GetTodayAttendanceAsync()
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

        // Persist only the first event in the collection
        public async Task<int> SaveEventsAsync(DateTime date, IEnumerable<EventModel> eventsForDay)
        {
            await InitAsync();
            var first = eventsForDay?.FirstOrDefault();
            var record = new AttendanceRecord
            {
                Date = date.Date,
                EventName = first?.Name,
                EventDescription = first?.Description
            };

            return await conn.InsertOrReplaceAsync(record);
        }

        public async Task<int> DeleteAttendanceAsync(DateTime date)
        {
            await InitAsync();
            return await conn.DeleteAsync<AttendanceRecord>(date.Date);
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

        // Load events: convert stored first-event into an EventCollection entry (single-item list)
        public async Task<EventCollection> LoadEventCollectionAsync()
        {
            await InitAsync();
            var records = await conn.Table<AttendanceRecord>().ToListAsync();
            var events = new EventCollection();

            foreach (var r in records)
            {
                if (string.IsNullOrWhiteSpace(r.EventName))
                    continue;

                var list = new List<EventModel>
                {
                    new EventModel { Name = r.EventName, Description = r.EventDescription }
                };

                events.Add(r.Date, list);
            }

            return events;
        }
    }
}
