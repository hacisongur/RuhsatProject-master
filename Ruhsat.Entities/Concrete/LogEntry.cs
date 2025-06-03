using System;

namespace RuhsaProject.Entities.Concrete
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string? UserId { get; set; } // nullable
        public string UserName { get; set; } // kullanıcı adı, nullable değil çünkü logda olmalı
        public string? Action { get; set; } // nullable
        public string? EntityName { get; set; } // nullable
        public DateTime Timestamp { get; set; }
        public string? Description { get; set; } // nullable
        public string? IpAddress { get; set; } // nullable
    }
}
