using System;

namespace SignalRDev.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 