using System;

namespace RegisterClient
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
