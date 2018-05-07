using System;

namespace NotificationApp
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string NotificationText { get; set; }
        public DateTime NotifyTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}