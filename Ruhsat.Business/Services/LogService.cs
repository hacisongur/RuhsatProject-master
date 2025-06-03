using RuhsaProject.Business.IServices;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.DataAccess.Contexts;

namespace RuhsaProject.Business.Services
{
    public class LogService : ILogService
    {
        private readonly RuhsatDbContext _context;

        public LogService(RuhsatDbContext context)
        {
            _context = context;
        }

       public async Task AddLogAsync(string userId, string userName, string action, string entityName, string description, string ipAddress = null)
{
    var logEntry = new LogEntry
    {
        UserId = userId,
        UserName = userName ?? "Unknown",
        Action = action,
        EntityName = entityName,
        Description = description,
        IpAddress = ipAddress,
        Timestamp = DateTime.UtcNow
    };

    await _context.LogEntries.AddAsync(logEntry);
    await _context.SaveChangesAsync();
}



    }
}
